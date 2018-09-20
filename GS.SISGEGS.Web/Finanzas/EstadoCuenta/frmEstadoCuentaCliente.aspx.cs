using GS.SISGEGS.DM;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.ItemWCF;
using GS.SISGEGS.Web.EstadoCuentaWCF;
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.CobranzasWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using GS.SISGEGS.Web.Helpers;
using System.Drawing;
using xi = Telerik.Web.UI.ExportInfrastructure;
using Telerik.Web.UI.GridExcelBuilder;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Windows;
using Telerik.Windows.Documents.FormatProviders;
using Telerik.Windows.Documents;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Diagnostics;
using System.Net;
using GS.SISGEGS.Web.LoginWCF;

namespace GS.SISGEGS.Web.Finanzas.EstadoCuenta 
{
    public partial class frmEstadoCuentaCliente : System.Web.UI.Page
    {
        protected PdfTemplate total;
        protected BaseFont helv;
        private bool settingFont = false;
        iTextSharp.text.Image oImagen;
        PdfContentByte cbPie;
        PdfContentByte cbEncabezado;
        public string codigoCliente;


        private List<gsReporte_DocumentosPendientesResult> ListarEstadoCuenta(string codAgenda, string codVendedor,  DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();
            try
            {
                List<gsReporte_DocumentosPendientesResult> lst = objEstadoCuentaWCF.EstadoCuenta_ListarxCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, codAgenda, codVendedor,  fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos,0).OrderBy(e => e.FechaVencimiento ).ToList();
       
                if(lst.Count > 0 )
                {
                    ViewState["lstEstadoCuenta"] = JsonHelper.JsonSerializer(lst);
                    grdEstadoCuenta.DataSource = lst;
                    grdEstadoCuenta.DataBind();
                    lblDate.Text = "1";
                }
             
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ListarClientesResumen(List<gsReporte_DocumentosPendientesResult> lst, string  codAgenda)
        {
            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();
            List<gsAgendaCliente_BuscarLimiteCreditoResult> lstLimiteCreditoAgenda;
            try
            {
                lstLimiteCreditoAgenda = new List<gsAgendaCliente_BuscarLimiteCreditoResult>();
                var queryAllAgenda = from DocumentosPendientes in lst select DocumentosPendientes.ID_Agenda;
                var queryAgenda = from AgendaPendiente in queryAllAgenda.Distinct() orderby AgendaPendiente ascending select AgendaPendiente ;

                List<gsAgendaCliente_BuscarLimiteCreditoResult> LimiteCreditoAgenda = objEstadoCuentaWCF.EstadoCuenta_LimiteCreditoxCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, codAgenda, 0).ToList();

                if(LimiteCreditoAgenda.Count > 0)
                {
                    gsAgendaCliente_BuscarLimiteCreditoResult Limite = LimiteCreditoAgenda[0];
                    lstLimiteCreditoAgenda.Add(Limite); 

                    ViewState["lstResumenCliente"] = JsonHelper.JsonSerializer(lstLimiteCreditoAgenda);
                    grdResumenCliente.DataSource = lstLimiteCreditoAgenda;
                    grdResumenCliente.DataBind();
                    lblDate.Text = "1";
                }
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ListarClientesResumenVencidos()
        {
            List<gsAgendaCliente_BuscarLimiteCreditoResult> lstClienteResumen;
            List<gsReporte_DocumentosPendientesResult> lstClienteDetalle;
            lstClienteResumen = ClienteResumen();
            lstClienteDetalle = ClienteDetalle();
            DataTable dtTablaVencidos = TablaVencidos();
            DataTable dtTablaresumen = TablaLimiteCredito();
            try
            {
                foreach (gsAgendaCliente_BuscarLimiteCreditoResult ClienteResumenObj in lstClienteResumen)
                {
                    DataRow dtRow = dtTablaVencidos.NewRow();
                    DataRow dtRowR = dtTablaresumen.NewRow();

                    decimal TCResumen = Convert.ToDecimal(ClienteResumenObj.TipoCambio);
                    if (TCResumen < 0)
                    {
                        TCResumen = TCResumen * -1;
                    }
                    var query_Detalle = from c in lstClienteDetalle
                                        where c.ID_Agenda == ClienteResumenObj.ID_Agenda
                                        orderby c.ClienteNombre, c.FechaVencimiento
                                        select new
                                        {
                                            c.TC,
                                            c.ID_Moneda,
                                            c.ID_Agenda,

                                            Pendiente = c.ID_Moneda == 0 ? c.ImportePendiente :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente / TCResumen :
                                                        c.ImportePendiente,

                                            Pendiente_NoVencido = c.ID_Moneda == 0 ? c.ImportePendiente_NoVencido :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_NoVencido / TCResumen :
                                                        c.ImportePendiente_NoVencido,

                                            Pendiente_VenceHoy = c.ID_Moneda == 0 ? c.ImportePendiente_VenceHoy :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_VenceHoy / TCResumen :
                                                        c.ImportePendiente_VenceHoy,

                                            Pendiente_01a30 = c.ID_Moneda == 0 ? c.ImportePendiente_01a30 :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_01a30 / TCResumen :
                                                        c.ImportePendiente_01a30,
                                            Pendiente_31a60 = c.ID_Moneda == 0 ? c.ImportePendiente_31a60 :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_31a60 / TCResumen :
                                                        c.ImportePendiente_31a60,
                                            Pendiente_61a120 = c.ID_Moneda == 0 ? c.ImportePendiente_61a120 :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_61a120 / TCResumen :
                                                        c.ImportePendiente_61a120,
                                            Pendiente_121a360 = c.ID_Moneda == 0 ? c.ImportePendiente_121a360 :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_121a360 / TCResumen :
                                                        c.ImportePendiente_121a360,
                                            Pendiente_361aMas = c.ID_Moneda == 0 ? c.ImportePendiente_361aMas :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_361aMas / TCResumen :
                                                        c.ImportePendiente_361aMas

                                            //DeudaVencida = c.ImportePendiente_01a30 + c.ImportePendiente_31a60 + c.ImportePendiente_61a120 + c.ImportePendiente_121a360 + c.ImportePendiente_361aMas
                                        };

                    var sumImportePendiente = query_Detalle.ToList().Select(c => c.Pendiente).Sum();
                    var sumImportePendiente_NoVencido = query_Detalle.ToList().Select(c => c.Pendiente_NoVencido).Sum();
                    var sumImportePendiente_VenceHoy = query_Detalle.ToList().Select(c => c.Pendiente_VenceHoy).Sum();

                    var sumImportePendiente_01a30 = query_Detalle.ToList().Select(c => c.Pendiente_01a30).Sum();
                    var sumImportePendiente_31a60 = query_Detalle.ToList().Select(c => c.Pendiente_31a60).Sum();
                    var sumImportePendiente_61a120 = query_Detalle.ToList().Select(c => c.Pendiente_61a120).Sum();
                    var sumImportePendiente_121a360 = query_Detalle.ToList().Select(c => c.Pendiente_121a360).Sum();
                    var sumImportePendiente_361aMas = query_Detalle.ToList().Select(c => c.Pendiente_361aMas).Sum();

                    decimal NoVencido;
                    decimal DeudaVencida;
                    decimal CreditoDisponible;
                    NoVencido = Convert.ToDecimal(sumImportePendiente_NoVencido) + Convert.ToDecimal(sumImportePendiente_VenceHoy);
                    DeudaVencida = Convert.ToDecimal(sumImportePendiente_01a30) + Convert.ToDecimal(sumImportePendiente_31a60) + Convert.ToDecimal(sumImportePendiente_61a120) + Convert.ToDecimal(sumImportePendiente_121a360) + Convert.ToDecimal(sumImportePendiente_361aMas);
                    CreditoDisponible = Convert.ToDecimal(ClienteResumenObj.LineaCredito) - Convert.ToDecimal(sumImportePendiente);

                    string strsumNoVencido = string.Format("{0:$ #,##0.00}", NoVencido);
                    string strsumImportePendiente_01a30 = string.Format("{0:$ #,##0.00}", sumImportePendiente_01a30);
                    string strsumImportePendiente_31a60 = string.Format("{0:$ #,##0.00}", sumImportePendiente_31a60);
                    string strsumImportePendiente_61a120 = string.Format("{0:$ #,##0.00}", sumImportePendiente_61a120);
                    string strsumImportePendiente_121a360 = string.Format("{0:$ #,##0.00}", sumImportePendiente_121a360);
                    string strsumImportePendiente_361aMas = string.Format("{0:$ #,##0.00}", sumImportePendiente_361aMas);
                    string strsumImportePendiente = string.Format("{0:$ #,##0.00}", sumImportePendiente);
                    string strsumDeudaVencida = string.Format("{0:$ #,##0.00}", DeudaVencida);
                    string strLineaCredito = string.Format("{0:$ #,##0.00}", ClienteResumenObj.LineaCredito);
                    string strDeudaVencida = string.Format("{0:$ #,##0.00}", strsumDeudaVencida);
                    string strTotalCredito = string.Format("{0:$ #,##0.00}", ClienteResumenObj.TotalCredito);
                    string strCreditoDisponible = string.Format("{0:$ #,##0.00}", CreditoDisponible);


                    dtRow[0] = ClienteResumenObj.ID_Agenda;
                    dtRow[1] = ClienteResumenObj.AgendaNombre;
                    dtRow[2] = strsumNoVencido;
                    dtRow[3] = strsumImportePendiente_01a30;
                    dtRow[4] = strsumImportePendiente_31a60;
                    dtRow[5] = strsumImportePendiente_61a120;
                    dtRow[6] = strsumImportePendiente_121a360;
                    dtRow[7] = strsumImportePendiente_361aMas;
                    dtRow[8] = strsumImportePendiente;

                    dtTablaVencidos.Rows.Add(dtRow);

                    dtRowR[0] = ClienteResumenObj.ID_Agenda;
                    dtRowR[1] = strsumNoVencido;
                    dtRowR[2] = strDeudaVencida;
                    dtRowR[3] = strsumImportePendiente;
                    dtRowR[4] = ClienteResumenObj.LineaCredito;
                    dtRowR[5] = strCreditoDisponible;
                    dtTablaresumen.Rows.Add(dtRowR);
                }

                //ViewState["lstResumenVencidos"] = JsonHelper.JsonSerializer(dtTablaVencidos);
                grdResumenVencimientos.DataSource = dtTablaVencidos;
                grdResumenVencimientos.DataBind();

                grdResumenCliente.DataSource = dtTablaresumen;
                grdResumenCliente.DataBind();


                lblDate.Text = "1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable TablaVencidos()
        {
            DataTable dttabla = new DataTable();
            try
            {
                dttabla.Columns.Add("id_agenda", typeof(string));
                dttabla.Columns.Add("AgendaNombre", typeof(string));
                dttabla.Columns.Add("NoVencido", typeof(string));
                dttabla.Columns.Add("Vencido01a30", typeof(string));
                dttabla.Columns.Add("Vencido31a60", typeof(string));
                dttabla.Columns.Add("Vencido61a120", typeof(string));
                dttabla.Columns.Add("Vencido121a360", typeof(string));
                dttabla.Columns.Add("Vencido361amas", typeof(string));
                dttabla.Columns.Add("DeudaTotal", typeof(string));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            return dttabla;
        }

        private DataTable TablaLimiteCredito()
        {
            DataTable dttabla = new DataTable();
            try
            {
                dttabla.Columns.Add("id_agenda", typeof(string));
                dttabla.Columns.Add("NoVencida", typeof(string));
                dttabla.Columns.Add("DeudaVencida", typeof(string));
                dttabla.Columns.Add("TotalCredito", typeof(string));
                dttabla.Columns.Add("LineaCredito", typeof(string));
                dttabla.Columns.Add("CreditoDisponible", typeof(string));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            return dttabla;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (!Page.IsPostBack) {
                    LoginWCFClient objLoginWCF = new LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idUsuario);

                    dpFinalEmision.SelectedDate = DateTime.Now;
                    lblDate.Text = "";
                    codigoCliente = ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).nroDocumento;

                    string idVendedor;
                    idVendedor = null;
                    txtIdAgenda.Text = Convert.ToString(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).nroDocumento);
                    txtAgendaNombre.Text = Convert.ToString(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).nombres);

                    BuscarCliente(codigoCliente, idVendedor, dpFinalEmision.SelectedDate.Value);
                    //BuscarPromedioCobranzas(codigoCliente, idVendedor, dpFinalEmision.SelectedDate.Value);

                }
            }

            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string idCliente;
            string idVendedor;
            idCliente = ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).nroDocumento;
            idVendedor = null;

            BuscarCliente(idCliente, idVendedor, dpFinalEmision.SelectedDate.Value);
        }

        public int Validar_Variables()
        {
            int valor = 0;

            if ( dpFinalEmision == null || dpFinalEmision.SelectedDate.Value.ToString() == "")
            {
                valor = 1;
                lblMensaje.Text = lblMensaje.Text + "Seleccionar fecha final de emisión. ";
                lblMensaje.CssClass = "mensajeError";
            }
            return valor;
        }

        public void BuscarCliente( string idCliente, string idVendedor, DateTime fechaForm2 )
        {
            DateTime fecha1;
            DateTime fecha2;
            DateTime fecha3;
            DateTime fecha4;
            string Cliente;
            string Vendedor;

            Cliente = null;
            Vendedor = null;
            List<gsReporte_DocumentosPendientesResult> lst;

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (Validar_Variables() == 0)
                {
                    fecha2 = fechaForm2;
                    fecha1 = fecha2.AddYears(-50);
                    fecha3 = fecha2.AddYears(-50);
                    fecha4 = fecha2.AddYears(50);

                    Cliente = idCliente;
                    Vendedor = idVendedor;

                    lst = ListarEstadoCuenta(Cliente, Vendedor, fecha1, fecha2, fecha3, fecha4, 0);
                    ListarClientesResumen(lst, Cliente);
                    if(lst.Count > 0 )
                    {
                        ListarClientesResumenVencidos();
                    }

                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }

        }


        #region Métodos web
        [WebMethod]
        public static AutoCompleteBoxData Agenda_BuscarCliente(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarClienteResult[] lst = objAgendaWCFClient.Agenda_ListarCliente(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString, 0);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsAgenda_ListarClienteResult agenda in lst)
                {
                    AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                    childNode.Text = agenda.ID_Agenda + "-" + agenda.Nombre;
                    childNode.Value = agenda.ID_Agenda;
                    result.Add(childNode);
                }
                res.Items = result.ToArray();
            }
            return res;
        }

        #endregion

        protected void grdEstadoCuenta_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (lblDate.Text == "1")
                {
                    List<gsReporte_DocumentosPendientesResult> lst = JsonHelper.JsonDeserialize<List<gsReporte_DocumentosPendientesResult>>((string)ViewState["lstEstadoCuenta"]);
                    grdEstadoCuenta.DataSource = lst;
                    grdEstadoCuenta.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se encontrarón resultados.";
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdResumenCliente_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (lblDate.Text == "1")
                {
                    List<gsAgendaCliente_BuscarLimiteCreditoResult> lst = JsonHelper.JsonDeserialize<List<gsAgendaCliente_BuscarLimiteCreditoResult>>((string)ViewState["lstResumenCliente"]);
                    grdResumenCliente.DataSource = lst;
                    grdResumenCliente.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se encontrarón resultados.";
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdResumenVencimientos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (lblDate.Text == "1")
                {
                    List<gsAgendaCliente_BuscarLimiteCreditoResult> lst = JsonHelper.JsonDeserialize<List<gsAgendaCliente_BuscarLimiteCreditoResult>>((string)ViewState["lstResumenVencidos"]);
                    grdResumenVencimientos.DataSource = lst;
                    grdResumenVencimientos.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se encontrarón resultados.";
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnExpResumen_Click(object sender, ImageClickEventArgs e)
        {
            if (lblDate.Text == "1")
            {
                ExportGridToExcel_Resumen();
            }
        }

        protected void btnExpDetalle_Click(object sender, ImageClickEventArgs e)
        {
            if (lblDate.Text == "1")
            {
                List<gsReporte_DocumentosPendientesResult> lst = JsonHelper.JsonDeserialize<List<gsReporte_DocumentosPendientesResult>>((string)ViewState["lstEstadoCuenta"]);

                var query_Detalle = from c in lst
                                    orderby c.ClienteNombre, c.FechaVencimiento
                                    select new
                                    {
                                        c.ClienteNombre,
                                        c.TipoDocumento,
                                        c.EstadoDoc,
                                        FechaGiro = DateTime.Parse(c.Fecha.ToString()).ToString("dd/MM/yyyy"),
                                        FechaVencimiento = DateTime.Parse(c.FechaVencimiento.ToString()).ToString("dd/MM/yyyy"),
                                        c.DiasMora,
                                        c.NroDocumento,
                                        c.Referencia,
                                        c.Situacion,
                                        c.Banco,
                                        NumeroUnico = Convert.ToString(c.NumeroUnico),
                                        c.monedasigno,
                                        Importe = string.Format("{0:#,##0.00}", c.Importe),
                                        DeudaSoles = string.Format("{0:#,##0.00}", c.DeudaSoles),
                                        DeudaDolares = string.Format("{0:#,##0.00}", c.DeudaDolares)
                                    };


                GridView GridView1 = new GridView();
                GridView1.AllowPaging = false;
                GridView1.DataSource = query_Detalle;
                GridView1.DataBind();
                //ExportGridToExcel_Detalle();
                ExporttoExcel(GridView1);
            }
        }

        private void ExportGridToExcel_Resumen()
        {
            //Get the data from database into datatable
            List<gsAgendaCliente_BuscarLimiteCreditoResult> lst = JsonHelper.JsonDeserialize<List<gsAgendaCliente_BuscarLimiteCreditoResult>>((string)ViewState["lstResumenCliente"]);

            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = lst;
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ResumenEstadoCuenta_" + DateTime.Now.Millisecond.ToString() + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                //Apply text style to each Row
                GridView1.Rows[i].Attributes.Add("class", "textmode");
            }
            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        private void ExportGridToExcel_Detalle()
        {
            List<gsReporte_DocumentosPendientesResult> lst = JsonHelper.JsonDeserialize<List<gsReporte_DocumentosPendientesResult>>((string)ViewState["lstEstadoCuenta"]);

            var query_Detalle = from c in lst
                                orderby c.ClienteNombre, c.FechaVencimiento
                                select new
                                {
                                    c.ClienteNombre, c.TipoDocumento, c.EstadoDoc,
                                    FechaGiro = DateTime.Parse(c.Fecha.ToString()).ToString("dd/MM/yyyy"),
                                    FechaVencimiento = DateTime.Parse(c.FechaVencimiento.ToString()).ToString("dd/MM/yyyy"),
                                    c.DiasMora, c.NroDocumento,  c.Referencia, c.Situacion, c.Banco,
                                    NumeroUnico = Convert.ToString(c.NumeroUnico), c.monedasigno,
                                    Importe = string.Format("{0:#,##0.00}", c.Importe),
                                    DeudaSoles = string.Format("{0:#,##0.00}", c.DeudaSoles),
                                    DeudaDolares = string.Format("{0:#,##0.00}", c.DeudaDolares)
                                };


            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = query_Detalle;
            GridView1.DataBind();
            //--------------------------------

            //string file = Server.MapPath("..\Plantilla\PRODUCTO_TERMINADO\Plantilla_producto_terminado_AC.xls");
            //string destino = objUTIL.ObtenerParametro("PAPELERA", "PAPELERA", ConfigurationManager.ConnectionStrings("Conection").ConnectionString) & "\" & "Plantilla_producto_terminado_AC_" & CType(Session("Session_Usuario"), CSesion).usu_codigo & ".xls"
            //string file = "C:\\Users\\cesar.coronel\\Desktop\\Pruebas_TXT\\Origen\\Reporte.xlsx";
            //string destino = "C:\\Users\\cesar.coronel\\Desktop\\Pruebas_TXT\\Destino\\Reporte.xlsx";

            //System.IO.File.Copy(file, destino, true);

            //DataTable dt;
            //dt =  funConvertGVToDatatable(GridView1);

            //---------------------------------
            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=DetalleEstadoCuenta_" + DateTime.Now.Millisecond.ToString() + ".xls");
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);

            ////GridView1.HeaderRow.BackColor = Color.LawnGreen;

            //for (int i = 0; i < GridView1.Columns.Count; i++)
            //{
            //    //GridView1.Columns[i].HeaderStyle.BackColor = Color.LightSeaGreen;
            //    //GridView1.HeaderRow.Cells[i].BackColor = Color.LightSeaGreen;
            //    GridView1.HeaderRow.Cells[i].BackColor = Color.LightGray;
            //    GridView1.HeaderRow.Cells[i].Style["background-color"] = "#ccc"; //Your own color
            //}


            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    //Apply text style to each Row
            //    GridView1.Rows[i].Attributes.Add("class", "textmode");
            //    //GridView1.Rows[i].BackColor = Color.LightSkyBlue;
            //}

            //GridView1.RenderControl(hw);

            ////style to format numbers to string
            //string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            //Response.Write(style);
            //Response.Output.Write(sw.ToString());
            //Response.Flush();
            //Response.End();


            ////Dim toDownload = New System.IO.FileInfo(destino)
            //FileInfo toDownload = new FileInfo(destino);
            
            //Response.Clear();
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
            //Response.AddHeader("Content-Length", toDownload.Length.ToString());
            //Response.ContentType = "application/xls";
            //Response.WriteFile(destino);
            //Response.End();


        }

        private void LoadExcelData(string fileName)
        {
            string Connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\";";

            OleDbConnection con = new OleDbConnection(Connection);

            OleDbCommand command = new OleDbCommand();

            DataTable dt = new DataTable();
            OleDbDataAdapter myCommand = new OleDbDataAdapter("select * from [Sheet1$] WHERE LastName <> '' ORDER BY LastName, FirstName", con);

            myCommand.Fill(dt);

        }

        private void ExporttoExcel(GridView GridView1)
        {
            DataTable table = new DataTable();
            table = funConvertGVToDatatable(GridView1);

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");

            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height

            int columnscount = GridView1.Columns.Count;

            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'>");
            HttpContext.Current.Response.Write("<TR>");
            HttpContext.Current.Response.Write("</TR>");


            HttpContext.Current.Response.Write("<TR>");
            HttpContext.Current.Response.Write("<Td style=\"border - width: 1px; border: solid; border - color:RED;\"  colspan=\"" + columnscount.ToString() + "\" >");
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write("Estado de Cuenta por Cliente");
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("</TR>");

            HttpContext.Current.Response.Write("<TR>");
            HttpContext.Current.Response.Write("<Td colspan= \"" + columnscount.ToString() + "\" >");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("</TR>");


            HttpContext.Current.Response.Write("<TR>");
            foreach (DataColumn  col in table.Columns)
            {//write in new col
                HttpContext.Current.Response.Write("<Td BGCOLOR=" + "#66FF66" + " border='1' >");
                //Get column headers  and make it as bold in excel columns
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(col.ColumnName.ToString());
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");


            foreach (DataRow row in table.Rows)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td border='1'>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        protected void btnExpPDFResumen_Click(object sender, ImageClickEventArgs e)
        {
            
        }

        protected void btnExpPDFDetalle_Click(object sender, ImageClickEventArgs e)
        {
            int idEmpresa;
            string fechaHasta;

            fechaHasta = dpFinalEmision.SelectedDate.Value.ToString("dd/MM/yyyy");
            idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;

            if (lblDate.Text == "1")
            {
                ExportarPDF(idEmpresa, fechaHasta);
               
                //ShowPdf(CreatePDF2(idEmpresa, fechaHasta));
               // CreatePDFFromMemoryStream(idEmpresa, fechaHasta);
            }
        }

        private DataTable funConvertGVToDatatable(GridView dtgrv)
        {
            try
            {
                DataTable dt = new DataTable();

                if (dtgrv.HeaderRow != null)
                {
                    for (int i = 0; i < dtgrv.HeaderRow.Cells.Count; i++)
                    {
                        dt.Columns.Add(dtgrv.HeaderRow.Cells[i].Text);
                    }
                }

                foreach (GridViewRow row in dtgrv.Rows)
                {
                    DataRow dr;
                    dr = dt.NewRow();

                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        dr[i] = row.Cells[i].Text.Replace(" ", "");
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void ExportarPDF(int idEmpresa, string fechaHasta)
        {
            string fileName = GetFileName(idEmpresa);

            PdfPTable tableLayout = new PdfPTable(11);

            string path2 = this.Server.MapPath(".") + "\\tempArchivos\\";

            if (!System.IO.Directory.Exists(path2))
            { System.IO.Directory.CreateDirectory(path2); }

            string destFile = System.IO.Path.Combine(path2, fileName);

            Document doc = new Document();
            doc = new Document(PageSize.LETTER, 20, 20, 20, 20);
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(destFile, FileMode.Create));

            doc.Open();
            doc.Add(Add_Content_To_PDF(tableLayout, fechaHasta, idEmpresa));
            doc.Close();
            ////-------------------------------------

            //string WatermarkLocation = "D:\\Images\\superseded.png";
            //WatermarkLocation = Server.MapPath("~/Images/Logos/grupo.png");

            //string FileLocation = destFile;
            //PdfReader pdfReader = new PdfReader(FileLocation);
            //FileStream stream = new FileStream(FileLocation.Replace(".pdf", "[temp][file].pdf"), FileMode.OpenOrCreate);
            //PdfStamper pdfStamper = new PdfStamper(pdfReader, stream);
            //iTextSharp.text.Rectangle rect;
            //iTextSharp.text.pdf.BaseFont watermarkFont;
            //iTextSharp.text.pdf.PdfGState gstate;
            //iTextSharp.text.pdf.PdfContentByte underContent;
            //Single watermarkFontOpacity = 0.3F;
            //Single watermarkRotation = 45.0F;
            //iTextSharp.text.BaseColor watermarkFontColor;
            //Single watermarkFontSize = 48;


            //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/Logos/Neo_med.png"));
            //img.ScaleToFit(500, 300);
            //img.Alignment = iTextSharp.text.Image.UNDERLYING;
            //img.SetAbsolutePosition(100, 150);
            //img.ScaleAbsoluteHeight(500);
            //img.ScaleAbsoluteWidth(500);


            //PdfGState graphicsState = new PdfGState();
            //graphicsState.FillOpacity = 0.4f;
            //watermarkFont = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
            //gstate = new iTextSharp.text.pdf.PdfGState();
            //gstate.FillOpacity = watermarkFontOpacity;
            //gstate.StrokeOpacity = watermarkFontOpacity;
            //watermarkFontColor = iTextSharp.text.BaseColor.BLUE;


            //PdfContentByte waterMark;

            //for (int page = 1; page <= pdfReader.NumberOfPages; page++)
            //{

            //    //waterMark = pdfStamper.GetOverContent(page);
            //    //waterMark.AddImage(img);
            //    //waterMark.SetGState(graphicsState);
            //    rect = pdfReader.GetPageSizeWithRotation(page);
            //    underContent = pdfStamper.GetUnderContent(page);


            //    underContent.SaveState();
            //    underContent.SetGState(gstate);
            //    underContent.SetColorFill(watermarkFontColor);
            //    underContent.BeginText();
            //    underContent.SetFontAndSize(watermarkFont, watermarkFontSize);
            //    underContent.SetTextMatrix(30, 30);
            //    underContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_CENTER, "Marca de agua silvestre", rect.Width / 2, rect.Height / 2, watermarkRotation);
            //    underContent.EndText();
            //    underContent.RestoreState();


            //}
            ////pdfStamper.FormFlattening = true;

            //pdfStamper.Close();
            //pdfReader.Close();

            //File.Delete(FileLocation);
            //File.Move(FileLocation.Replace(".pdf", "[temp][file].pdf"), FileLocation);
            ////-------------------------------------



            //Response.Redirect("frmExportarPDF.aspx?sFileName=" + fileName, false);
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowInsertForm(" + fileName + ");", true);

            AbriVentana(fileName);
 
        }

        private void AbriVentana(string variable)
        {
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "AbrirNuevoVentana( " + variable + ");", true);
            //ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "AbrirNuevoVentana(" + variable + ");", true);
            RegisterStartupScript("script", "<script>window.open('frmExportarPDF.aspx?strFileNombre=" + variable + "', 'toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=0,resizable=0,width=500,height=500,top=0,left=0')</script>");
        }

        protected string GetFileName(int idEmpresa)
        {
            string file, empresa;

            string anho, mes, dia, minutos, segundo, miliseg;
            anho = DateTime.Now.Year.ToString();
            mes = DateTime.Now.Month.ToString();
            dia = DateTime.Now.Day.ToString();
            minutos = DateTime.Now.Minute.ToString();
            segundo = DateTime.Now.Second.ToString();
            miliseg = DateTime.Now.Millisecond.ToString();
            if(idEmpresa == 1)
            { empresa = "Sil"; }
            else
            { empresa = "Neo"; }

            file = empresa +  "EstadoCuenta_" + anho + mes + dia + minutos +  segundo + miliseg + ".pdf";

            return file;
        }

        private PdfPTable Add_Content_To_PDF(PdfPTable tableLayout, string fechaHasta, int idEmpresa)
        {
            EmpresaWCFClient objEmpresaWCF = new EmpresaWCFClient();
            AgendaWCFClient objAgendaWCF = new AgendaWCFClient();
            List<gsAgendaCliente_BuscarLimiteCreditoResult> lstClienteResumen;
            List<gsReporte_DocumentosPendientesResult> lstClienteDetalle;

            Empresa_BuscarDetalleResult objEmpresa;
            string urlImagen;
            lstClienteResumen = ClienteResumen();
            lstClienteDetalle = ClienteDetalle();


            Empresa_BuscarDetalleResult[] lst = objEmpresaWCF.Empresa_BuscarDetalle(idEmpresa);
            objEmpresa = lst[0];
            urlImagen = objEmpresa.logotipo.ToString();

            float[] values = new float[11];
            values[0] = 140;
            values[1] = 120;
            values[2] = 105;
            values[3] = 120;
            values[4] = 110;
            values[5] = 120;
            values[6] = 105;
            values[7] = 120;
            values[8] = 110;
            values[9] = 80;
            values[10] = 80;

            tableLayout.SetWidths(values);        //Set the pdf headers
            tableLayout.WidthPercentage = 100;       //Set the PDF File witdh percentage

            //Add Title to the PDF file at the top

            //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath(urlImagen));
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/Logos/grupo.png"));

            logo.ScaleAbsolute(205,90);
            PdfPCell imageCell = new PdfPCell(logo);
            imageCell.Colspan = 2; // either 1 if you need to insert one cell
            imageCell.Border = 0;
            imageCell.HorizontalAlignment = Element.ALIGN_LEFT;

            //tableLayout.AddCell(imageCell);
            tableLayout.AddCell(new PdfPCell(new Phrase("Estado de cuenta al " + fechaHasta , new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, 1,  iTextSharp.text.BaseColor.DARK_GRAY ))) { Colspan = 10, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });
            tableLayout.AddCell(new PdfPCell(new Phrase( DateTime.Now.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 5, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 1, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });

            tableLayout.AddCell(new PdfPCell(new Phrase(objEmpresa.ruc +" "+ objEmpresa.razonSocial , new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 6, Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT });
            tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 5, Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER });

            tableLayout.AddCell(new PdfPCell(new Phrase(objEmpresa.direccion , new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 6, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_LEFT });
            tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 5, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });

            //Add Cliente
            int cont = 0;

            foreach (gsAgendaCliente_BuscarLimiteCreditoResult ClienteResumenObj in lstClienteResumen)
            {
                cont = cont + 1;
                // ADD Cliente

                tableLayout.AddCell(new PdfPCell(new Phrase("1. Razón Social", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                List<gsAgenda_BuscarClienteDetalleResult> LimiteAgenda = objAgendaWCF.Agenda_BuscarClienteDetalle(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, ClienteResumenObj.ID_Agenda.ToString()).ToList();
                if(LimiteAgenda.Count > 0)
                {
                    gsAgenda_BuscarClienteDetalleResult AgendaResumen = LimiteAgenda[0];
                    tableLayout.AddCell(new PdfPCell(new Phrase(AgendaResumen.ruc + " " + AgendaResumen.Agendanombre, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase(AgendaResumen.Direccion + " " + AgendaResumen.Distrito + " - " + AgendaResumen.Provincia + " - " + AgendaResumen.Departamento + " - " + AgendaResumen.Pais, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, BorderColorLeft = BaseColor.RED, BorderColorTop = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                }
                else
                {
                    tableLayout.AddCell(new PdfPCell(new Phrase(ClienteResumenObj.ruc + " " + ClienteResumenObj.AgendaNombre, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Registrar dirección fiscal. ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, BorderColorLeft = BaseColor.RED, BorderColorTop = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                }


                tableLayout.AddCell(new PdfPCell(new Phrase("   ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 11, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });


                //ADD Resumen cliente
                decimal TCResumen = Convert.ToDecimal(ClienteResumenObj.TipoCambio);
                if(TCResumen < 0)
                {
                    TCResumen = TCResumen * -1;
                }


                var query_Detalle = from c in lstClienteDetalle
                                    where c.ID_Agenda == ClienteResumenObj.ID_Agenda
                                    orderby c.ClienteNombre, c.FechaVencimiento
                                    select new
                                    {
                                        c.TC, c.ID_Moneda,
                                        c.ID_Agenda,

                                        Pendiente = c.ID_Moneda == 0 ? c.ImportePendiente :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente / TCResumen :
                                                    c.ImportePendiente,

                                        Pendiente_NoVencido = c.ID_Moneda == 0 ? c.ImportePendiente_NoVencido :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_NoVencido / TCResumen : 
                                                    c.ImportePendiente_NoVencido,

                                        Pendiente_VenceHoy = c.ID_Moneda == 0 ? c.ImportePendiente_VenceHoy :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_VenceHoy/ TCResumen :
                                                    c.ImportePendiente_VenceHoy,

                                        Pendiente_01a30 = c.ID_Moneda == 0 ? c.ImportePendiente_01a30 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_01a30 / TCResumen :
                                                    c.ImportePendiente_01a30,
                                        Pendiente_31a60 = c.ID_Moneda == 0 ? c.ImportePendiente_31a60 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_31a60 / TCResumen :
                                                    c.ImportePendiente_31a60,
                                        Pendiente_61a120 = c.ID_Moneda == 0 ? c.ImportePendiente_61a120 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_61a120 / TCResumen :
                                                    c.ImportePendiente_61a120,
                                        Pendiente_121a360 = c.ID_Moneda == 0 ? c.ImportePendiente_121a360 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_121a360 / TCResumen :
                                                    c.ImportePendiente_121a360,
                                        Pendiente_361aMas = c.ID_Moneda == 0 ? c.ImportePendiente_361aMas :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_361aMas / TCResumen :
                                                    c.ImportePendiente_361aMas

                                        //DeudaVencida = c.ImportePendiente_01a30 + c.ImportePendiente_31a60 + c.ImportePendiente_61a120 + c.ImportePendiente_121a360 + c.ImportePendiente_361aMas
                                    };

                var sumImportePendiente = query_Detalle.ToList().Select(c => c.Pendiente).Sum();
                var sumImportePendiente_NoVencido = query_Detalle.ToList().Select(c => c.Pendiente_NoVencido).Sum();
                var sumImportePendiente_VenceHoy = query_Detalle.ToList().Select(c => c.Pendiente_VenceHoy).Sum();

                var sumImportePendiente_01a30 = query_Detalle.ToList().Select(c => c.Pendiente_01a30).Sum();
                var sumImportePendiente_31a60 = query_Detalle.ToList().Select(c => c.Pendiente_31a60).Sum();
                var sumImportePendiente_61a120 = query_Detalle.ToList().Select(c => c.Pendiente_61a120).Sum();
                var sumImportePendiente_121a360 = query_Detalle.ToList().Select(c => c.Pendiente_121a360).Sum();
                var sumImportePendiente_361aMas = query_Detalle.ToList().Select(c => c.Pendiente_361aMas).Sum();

                int NoVencido;
                int  DeudaVencida;
                NoVencido = Convert.ToInt32(sumImportePendiente_NoVencido) + Convert.ToInt32(sumImportePendiente_VenceHoy);
                DeudaVencida = Convert.ToInt32(sumImportePendiente_01a30) + Convert.ToInt32(sumImportePendiente_31a60) + Convert.ToInt32(sumImportePendiente_61a120) + Convert.ToInt32(sumImportePendiente_121a360) + Convert.ToInt32(sumImportePendiente_361aMas);

    
                string strsumNoVencido = string.Format("{0:$ #,##0.00}", NoVencido);
                string strsumImportePendiente_01a30 = string.Format("{0:$ #,##0.00}", sumImportePendiente_01a30);
                string strsumImportePendiente_31a60 = string.Format("{0:$ #,##0.00}", sumImportePendiente_31a60);
                string strsumImportePendiente_61a120 = string.Format("{0:$ #,##0.00}", sumImportePendiente_61a120);
                string strsumImportePendiente_121a360 = string.Format("{0:$ #,##0.00}", sumImportePendiente_121a360);
                string strsumImportePendiente_361aMas = string.Format("{0:$ #,##0.00}", sumImportePendiente_361aMas);
                string strsumImportePendiente = string.Format("{0:$ #,##0.00}", sumImportePendiente);

                string strsumDeudaVencida = string.Format("{0:$ #,##0.00}", DeudaVencida);

                string strLineaCredito = string.Format("{0:$ #,##0.00}", ClienteResumenObj.LineaCredito);
                string strDeudaVencida = string.Format("{0:$ #,##0.00}", strsumDeudaVencida);
                string strTotalCredito = string.Format("{0:$ #,##0.00}", ClienteResumenObj.TotalCredito);
                string strCreditoDisponible = string.Format("{0:$ #,##0.00}", ClienteResumenObj.CreditoDisponible);


                tableLayout.AddCell(new PdfPCell(new Phrase("2. Línea de crédito", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Deuda vencida", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Deuda total", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Línea disponible", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 7, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

               

                tableLayout.AddCell(new PdfPCell(new Phrase(strLineaCredito, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                tableLayout.AddCell(new PdfPCell(new Phrase(strDeudaVencida, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                tableLayout.AddCell(new PdfPCell(new Phrase(strTotalCredito, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                tableLayout.AddCell(new PdfPCell(new Phrase(strCreditoDisponible, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 7, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 11, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });


                tableLayout.AddCell(new PdfPCell(new Phrase("3. NoVencido", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                 tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 01a30", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 31a60", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan =1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 61a120", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan =1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 121a360", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 361aMas", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan =1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Deuda total", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 4, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });

                tableLayout.AddCell(new PdfPCell(new Phrase(strsumNoVencido, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                tableLayout.AddCell(new PdfPCell(new Phrase(strsumImportePendiente_01a30, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                tableLayout.AddCell(new PdfPCell(new Phrase(strsumImportePendiente_31a60, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                tableLayout.AddCell(new PdfPCell(new Phrase(strsumImportePendiente_61a120, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                tableLayout.AddCell(new PdfPCell(new Phrase(strsumImportePendiente_121a360, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                tableLayout.AddCell(new PdfPCell(new Phrase(strsumImportePendiente_361aMas, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                tableLayout.AddCell(new PdfPCell(new Phrase(strsumImportePendiente, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 4, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });


                tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 11, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });


                // ADD DETALLE
                tableLayout.AddCell(new PdfPCell(new Phrase("4. TipoDoc.", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                AddCellToHeader(tableLayout, "N° Documento");
                AddCellToHeader(tableLayout, "Fecha Emis.");
                AddCellToHeader(tableLayout, "Fecha Vcto.");
                AddCellToHeader(tableLayout, "DíasMora");
                AddCellToHeader(tableLayout, "Estado Doc.");
                AddCellToHeaderColspan(tableLayout, "Letra Banco", 2);
                AddCellToHeader(tableLayout, "N° Unico");
                AddCellToHeader(tableLayout, "Importe");
                AddCellToHeader(tableLayout, "Saldo Doc.");

     
                var query_DetalleTotal = from c in lstClienteDetalle
                                    where c.ID_Agenda == ClienteResumenObj.ID_Agenda
                                    orderby c.ClienteNombre, c.FechaVencimiento
                                    select new
                                    {
                                        c.TipoDocumento,
                                        c.NroDocumento,
                                        c.Fecha,
                                        c.FechaVencimiento,
                                        c.EstadoDoc,
                                        c.Banco,
                                        c.NumeroUnico,
                                        c.Importe,
                                        c.ImportePendiente,
                                        c.monedasigno,
                                        c.ID_Moneda,
                                        c.TC,
                                        c.DiasMora
                                    };
                int count = 0;
                string TipoDoc = "";
                string strTipoDoc = "";

                foreach (var query_Clientel in query_DetalleTotal)
                {
                    count = count + 1;
                    TipoDoc = query_Clientel.TipoDocumento;

                    string correctString = TipoDoc.Replace("(Venta)", "");

                    strTipoDoc = count.ToString() + ". " + correctString;
                    tableLayout.AddCell(new PdfPCell(new Phrase(strTipoDoc, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                    AddCellToBody(tableLayout, query_Clientel.NroDocumento);
                    AddCellToBody(tableLayout, query_Clientel.Fecha.Value.ToShortDateString());
                    AddCellToBody(tableLayout, query_Clientel.FechaVencimiento.Value.ToShortDateString());
                    AddCellToBody(tableLayout, query_Clientel.DiasMora.ToString());
                    AddCellToBody(tableLayout, query_Clientel.EstadoDoc);

                    AddCellToBodyColspan(tableLayout, query_Clientel.Banco, 2);

                    AddCellToBody(tableLayout, query_Clientel.NumeroUnico);

                    string Importe;
                    string ImportePendiente;

                    if(query_Clientel.ID_Moneda == 0)
                    {
                        Importe = string.Format("{0:$ #,##0.00}", query_Clientel.Importe);
                        ImportePendiente = string.Format("{0:$ #,##0.00}", query_Clientel.ImportePendiente);
                    }
                    else
                    {
                        Importe = string.Format("{0:S/ #,##0.00}", query_Clientel.Importe);
                        ImportePendiente = string.Format("{0:S/ #,##0.00}", query_Clientel.ImportePendiente);
                    }
                    tableLayout.AddCell(new PdfPCell(new Phrase(Importe.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase(ImportePendiente.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                }

                tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 11, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });
                tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 11, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });


            }

            return tableLayout;
        }

        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
        }

        private static void AddCellToHeaderColspan(PdfPTable tableLayout, string cellText, int intCol)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = intCol,  HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
        }

        private static void AddCellToBody(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

        }

        private static void AddCellToBodyColspan(PdfPTable tableLayout, string cellText, int intCol)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan= intCol, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

        }

        private List<gsAgendaCliente_BuscarLimiteCreditoResult> ClienteResumen()
        {
            List<gsAgendaCliente_BuscarLimiteCreditoResult> lst = JsonHelper.JsonDeserialize<List<gsAgendaCliente_BuscarLimiteCreditoResult>>((string)ViewState["lstResumenCliente"]);
            return lst;
        }

        private List<gsReporte_DocumentosPendientesResult> ClienteDetalle()
        {
            //DataTable dtTabla;
            List<gsReporte_DocumentosPendientesResult> lst = JsonHelper.JsonDeserialize<List<gsReporte_DocumentosPendientesResult>>((string)ViewState["lstEstadoCuenta"]);
            return lst;
        }

        private byte[] CreatePDF2(int idEmpresa, string fechaHasta)
        {
          
            PdfPTable tableLayout = new PdfPTable(11);

            Document document = new Document();
            document = new Document(PageSize.LETTER, 20, 20, 20, 20);

            using (MemoryStream output = new MemoryStream())
            {
                PdfWriter wri = PdfWriter.GetInstance(document, output);
                PdfWriter.GetInstance(document, Response.OutputStream);

                document.Open();
                document.Add(Add_Content_To_PDF(tableLayout, fechaHasta, idEmpresa));
                document.Close();

                return output.ToArray();
            }

        }

        private void ShowPdf(byte[] strS)
        {
            string fileName = "ReporteEstadoCuenta_" + DateTime.Now.Millisecond.ToString() + ".pdf";
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);

            Response.BinaryWrite(strS);
            Response.End();
            Response.Flush();
            Response.Clear();
        }

        private void DownloadAsPDF(MemoryStream ms)
        {

            string fileName = "ReporteEstadoCuenta_" + DateTime.Now.Millisecond.ToString() + ".pdf";
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment;filename="+ fileName);
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.End();
            ms.Close();

        }

        public void CreatePDFFromMemoryStream(int idEmpresa, string fechaHasta)
        {
            //(1)using PDFWriter
            PdfPTable tableLayout = new PdfPTable(11);

            Document doc = new Document();
            doc = new Document(PageSize.LETTER, 20, 20, 20, 20);
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
            doc.Open();
            doc.Add(Add_Content_To_PDF(tableLayout, fechaHasta, idEmpresa));

            writer.CloseStream = false;
            doc.Close();
            memoryStream.Position = 0;

            DownloadAsPDF(memoryStream);

        }


        private void AbrirArchivo(string destino)
        {
            System.IO.FileInfo toDownload = new System.IO.FileInfo(destino);
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
            Response.AddHeader("Content-Length", toDownload.Length.ToString());
            Response.ContentType = "application/pdf";
            Response.WriteFile(destino);
            Response.End();
        }



    }
}