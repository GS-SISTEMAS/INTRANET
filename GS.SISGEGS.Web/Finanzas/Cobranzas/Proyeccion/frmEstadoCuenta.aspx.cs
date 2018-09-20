using GS.SISGEGS.DM;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.CobranzasWCF;
using GS.SISGEGS.Web.EstadoCuentaWCF;
using GS.SISGEGS.Web.EmpresaWCF;
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
//using xi = Telerik.Web.UI.ExportInfrastructure;
//using Telerik.Web.UI.GridExcelBuilder;
using System.IO;
//using System.Data.SqlClient;
//using System.Configuration;
//using System.Text;
//using System.Web.UI.HtmlControls;
using System.Data.OleDb;
//using System.Windows;
//using Telerik.Windows.Documents.FormatProviders;
//using Telerik.Windows.Documents;
//using System.Collections;
//using System.Web.Security;
//using System.Web.UI.WebControls.WebParts;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
//using System.Diagnostics;
//using System.Net;
using GS.SISGEGS.Web.LoginWCF;
//using ICSharpCode.SharpZipLib;
//using ICSharpCode;
//using ICSharpCode.SharpZipLib.Zip;

namespace GS.SISGEGS.Web.Finanzas.Cobranzas.Proyeccion
{
    public partial class frmEstadoCuenta : System.Web.UI.Page
    {
        protected PdfTemplate total;
        protected BaseFont helv;
        private bool settingFont = false;
        iTextSharp.text.Image oImagen;
        PdfContentByte cbPie;
        PdfContentByte cbEncabezado;

        private List<spEstadoCuenta_Proyectado_ClienteResult> ListarEstadoCuenta(string codAgenda, int periodo, string id_sectorista, int id_zona, int anho, int mes)
        {
            CobranzasWCFClient objProyectadoWCF = new CobranzasWCFClient();
            List<spEstadoCuenta_Proyectado_ClienteResult> lstProyectado = new List<spEstadoCuenta_Proyectado_ClienteResult>();

            try
            {
                lstProyectado = objProyectadoWCF.EstadoCuenta_Proyectado_Cliente(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                       ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, codAgenda, periodo, id_sectorista, id_zona, anho, mes).ToList();

                var varProyectado = lstProyectado.OrderByDescending(x => x.DiasMora).ToList(); 

                ViewState["lstEstadoCuenta"] = JsonHelper.JsonSerializer(lstProyectado);
                grdEstadoCuenta.DataSource = varProyectado;
                grdEstadoCuenta.DataBind();
                lblDate.Text = "1";
                return lstProyectado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                int año = dpFinalEmision.SelectedDate.Value.Year;
                int mes = dpFinalEmision.SelectedDate.Value.Month;
                año = año * 100;
                año = año + mes;

                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + año.ToString() + ");", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }


        protected void btnExcelDetalle_Click(object sender, EventArgs e)
        {
            ExportarExcelDetalle();
        }

        protected void btnPDFDetalle_Click(object sender, EventArgs e)
        {
            ExportarPDF_Detalle();
        }

        protected void grdEstadoCuentaCliente_PivotGridCellExporting(object sender, PivotGridCellExportingArgs e)
        {
            try
            {
                PivotGridBaseModelCell modelDataCell = e.PivotGridModelCell as PivotGridBaseModelCell;
                if (modelDataCell != null)
                {
                    AddStylesToDataCells(modelDataCell, e);
                }

                if (modelDataCell.TableCellType == PivotGridTableCellType.RowHeaderCell)
                {
                    AddStylesToRowHeaderCells(modelDataCell, e);
                }

                if (modelDataCell.TableCellType == PivotGridTableCellType.ColumnHeaderCell)
                {
                    AddStylesToColumnHeaderCells(modelDataCell, e);
                }

                if (modelDataCell.IsGrandTotalCell)
                {
                    e.ExportedCell.Style.BackColor = Color.FromArgb(128, 128, 128);
                    e.ExportedCell.Style.Font.Bold = true;
                }

                if (IsTotalDataCell(modelDataCell))
                {
                    e.ExportedCell.Style.BackColor = Color.FromArgb(150, 150, 150);
                    e.ExportedCell.Style.Font.Bold = true;
                    AddBorders(e);
                }

                if (IsGrandTotalDataCell(modelDataCell))
                {
                    e.ExportedCell.Style.BackColor = Color.FromArgb(128, 128, 128);
                    e.ExportedCell.Style.Font.Bold = true;
                    AddBorders(e);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void AddStylesToDataCells(PivotGridBaseModelCell modelDataCell, PivotGridCellExportingArgs e)
        {
            if (modelDataCell.Data != null && modelDataCell.Data.GetType() == typeof(decimal))
            {
                decimal value = Convert.ToDecimal(modelDataCell.Data);
                if (value > 100000)
                {
                    e.ExportedCell.Style.BackColor = Color.FromArgb(51, 204, 204);
                    AddBorders(e);
                }

                e.ExportedCell.Format = "$0.0";
            }
        }

        private void AddStylesToColumnHeaderCells(PivotGridBaseModelCell modelDataCell, PivotGridCellExportingArgs e)
        {
            if (e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width == 0)
            {
                e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width = 200D;
            }

            if (modelDataCell.IsTotalCell)
            {
                e.ExportedCell.Style.BackColor = Color.FromArgb(150, 150, 150);
                e.ExportedCell.Style.Font.Bold = true;
            }
            else
            {
                e.ExportedCell.Style.BackColor = Color.FromArgb(192, 192, 192);
            }
            AddBorders(e);
        }

        private void AddStylesToRowHeaderCells(PivotGridBaseModelCell modelDataCell, PivotGridCellExportingArgs e)
        {
            if (e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width == 0)
            {
                e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width = 80D;
            }
            if (modelDataCell.IsTotalCell)
            {
                e.ExportedCell.Style.BackColor = Color.FromArgb(150, 150, 150);
                e.ExportedCell.Style.Font.Bold = true;
            }
            else
            {
                e.ExportedCell.Style.BackColor = Color.FromArgb(192, 192, 192);
            }

            AddBorders(e);
        }

        private static void AddBorders(PivotGridCellExportingArgs e)
        {
            e.ExportedCell.Style.BorderBottomColor = Color.FromArgb(128, 128, 128);
            e.ExportedCell.Style.BorderBottomWidth = new Unit(1);
            e.ExportedCell.Style.BorderBottomStyle = BorderStyle.Solid;

            e.ExportedCell.Style.BorderRightColor = Color.FromArgb(128, 128, 128);
            e.ExportedCell.Style.BorderRightWidth = new Unit(1);
            e.ExportedCell.Style.BorderRightStyle = BorderStyle.Solid;

            e.ExportedCell.Style.BorderLeftColor = Color.FromArgb(128, 128, 128);
            e.ExportedCell.Style.BorderLeftWidth = new Unit(1);
            e.ExportedCell.Style.BorderLeftStyle = BorderStyle.Solid;

            e.ExportedCell.Style.BorderTopColor = Color.FromArgb(128, 128, 128);
            e.ExportedCell.Style.BorderTopWidth = new Unit(1);
            e.ExportedCell.Style.BorderTopStyle = BorderStyle.Solid;
        }

        private bool IsTotalDataCell(PivotGridBaseModelCell modelDataCell)
        {
            return modelDataCell.TableCellType == PivotGridTableCellType.DataCell &&
               (modelDataCell.CellType == PivotGridDataCellType.ColumnTotalDataCell ||
                 modelDataCell.CellType == PivotGridDataCellType.RowTotalDataCell ||
                 modelDataCell.CellType == PivotGridDataCellType.RowAndColumnTotal);
        }

        private bool IsGrandTotalDataCell(PivotGridBaseModelCell modelDataCell)
        {
            return modelDataCell.TableCellType == PivotGridTableCellType.DataCell &&
                (modelDataCell.CellType == PivotGridDataCellType.ColumnGrandTotalDataCell ||
                    modelDataCell.CellType == PivotGridDataCellType.ColumnGrandTotalRowTotal ||
                    modelDataCell.CellType == PivotGridDataCellType.RowGrandTotalColumnTotal ||
                    modelDataCell.CellType == PivotGridDataCellType.RowGrandTotalDataCell ||
                    modelDataCell.CellType == PivotGridDataCellType.RowAndColumnGrandTotal);
        }

        private void ListarClientesResumen(List<spEstadoCuenta_Proyectado_ClienteResult> lst)
        {
            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();
            List<gsAgendaCliente_BuscarLimiteCreditoResult> lstLimiteCreditoAgenda;
            try
            {
                lstLimiteCreditoAgenda = new List<gsAgendaCliente_BuscarLimiteCreditoResult>();
                var queryAllAgenda = from DocumentosPendientes in lst select DocumentosPendientes.ID_Agenda;
                var queryAgenda = from AgendaPendiente in queryAllAgenda.Distinct() orderby AgendaPendiente ascending select AgendaPendiente;

                foreach (var agenda in queryAgenda)
                {
                    List<gsAgendaCliente_BuscarLimiteCreditoResult> LimiteCreditoAgenda = objEstadoCuentaWCF.EstadoCuenta_LimiteCreditoxCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, agenda.ToString(), 0).ToList();
                    gsAgendaCliente_BuscarLimiteCreditoResult Limite = LimiteCreditoAgenda[0];
                    lstLimiteCreditoAgenda.Add(Limite);
                }

                ViewState["lstResumenCliente"] = JsonHelper.JsonSerializer(lstLimiteCreditoAgenda);
             
                lblDate.Text = "1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ListarClientesResumenVencidos()
        {
            List<gsAgendaCliente_BuscarLimiteCreditoResult> lstClienteResumen;
            List<gsAgendaCliente_BuscarLimiteCreditoResult> lstClienteResumenFinal = new List<gsAgendaCliente_BuscarLimiteCreditoResult>();
            gsAgendaCliente_BuscarLimiteCreditoResult ClienteResumenFinal;
            List<gsReporte_DocumentosPendientesResult> lstClienteDetalle;
            lstClienteResumen = ClienteResumen();
            lstClienteDetalle = ClienteDetalle();

            //DataTable dtTablaresumen = TablaLimiteCredito();
            try
            {
                foreach (gsAgendaCliente_BuscarLimiteCreditoResult ClienteResumen in lstClienteResumen)
                {
                    ClienteResumenFinal = new gsAgendaCliente_BuscarLimiteCreditoResult();
                    var query_Detalle = from c in lstClienteDetalle
                                        where c.ID_Agenda == ClienteResumen.ID_Agenda
                                        orderby c.ClienteNombre, c.FechaVencimiento
                                        select new
                                        {
                                            c.TC,
                                            c.ID_Moneda,
                                            c.ID_Agenda,


                                            Pendiente = c.ID_Moneda == 0 ? c.ImportePendiente :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente / c.TC :
                                                        c.ImportePendiente,

                                            Pendiente_PorVencer30 = c.ID_Moneda == 0 ? c.ImportePendiente_PorVencer30 :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_PorVencer30 / c.TC :
                                                        c.ImportePendiente_PorVencer30,

                                            Pendiente_NoVencido = c.ID_Moneda == 0 ? c.ImportePendiente_NoVencido :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_NoVencido / c.TC :
                                                        c.ImportePendiente_NoVencido,

                                            Pendiente_VenceHoy = c.ID_Moneda == 0 ? c.ImportePendiente_VenceHoy :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_VenceHoy / c.TC :
                                                        c.ImportePendiente_VenceHoy,
                                            Pendiente_01a30 = c.ID_Moneda == 0 ? c.ImportePendiente_01a30 :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_01a30 / c.TC :
                                                        c.ImportePendiente_01a30,
                                            Pendiente_31a60 = c.ID_Moneda == 0 ? c.ImportePendiente_31a60 :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_31a60 / c.TC :
                                                        c.ImportePendiente_31a60,
                                            Pendiente_61a120 = c.ID_Moneda == 0 ? c.ImportePendiente_61a120 :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_61a120 / c.TC :
                                                        c.ImportePendiente_61a120,
                                            Pendiente_121a360 = c.ID_Moneda == 0 ? c.ImportePendiente_121a360 :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_121a360 / c.TC :
                                                        c.ImportePendiente_121a360,
                                            Pendiente_361aMas = c.ID_Moneda == 0 ? c.ImportePendiente_361aMas :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_361aMas / c.TC :
                                                        c.ImportePendiente_361aMas
                                        };

                    var sumImportePendiente = query_Detalle.ToList().Select(c => c.Pendiente).Sum();
                    var sumImportePendiente_NoVencido = query_Detalle.ToList().Select(c => c.Pendiente_NoVencido).Sum();
                    var sumImportePendiente_VenceHoy = query_Detalle.ToList().Select(c => c.Pendiente_VenceHoy).Sum();

                    var sumImporte_PorVencer30 = query_Detalle.ToList().Select(c => c.Pendiente_PorVencer30).Sum();

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
                    CreditoDisponible = Convert.ToDecimal(ClienteResumen.LineaCredito) - Convert.ToDecimal(sumImportePendiente);

                    string strsumNoVencido = string.Format("{0:#,##0.00}", NoVencido);

                    string strsumImporte_PorVencer30 = string.Format("{0:#,##0.00}", sumImporte_PorVencer30);

                    string strsumImportePendiente_01a30 = string.Format("{0:#,##0.00}", sumImportePendiente_01a30);
                    string strsumImportePendiente_31a60 = string.Format("{0:#,##0.00}", sumImportePendiente_31a60);
                    string strsumImportePendiente_61a120 = string.Format("{0:#,##0.00}", sumImportePendiente_61a120);
                    string strsumImportePendiente_121a360 = string.Format("{0:#,##0.00}", sumImportePendiente_121a360);
                    string strsumImportePendiente_361aMas = string.Format("{0:#,##0.00}", sumImportePendiente_361aMas);
                    string strsumImportePendiente = string.Format("{0:#,##0.00}", sumImportePendiente);
                    string strsumDeudaVencida = string.Format("{0:#,##0.00}", DeudaVencida);
                    string strLineaCredito = string.Format("{0:#,##0.00}", ClienteResumen.LineaCredito);
                    //string strTotalCredito = string.Format("{0:$ #,##0.00}", ClienteResumen.TotalCredito);
                    string strCreditoDisponible = string.Format("{0:#,##0.00}", CreditoDisponible);


                    ClienteResumenFinal.ID_Agenda = ClienteResumen.ID_Agenda;
                    ClienteResumenFinal.AgendaNombre = ClienteResumen.AgendaNombre;

                    ClienteResumenFinal.Aprobacion = ClienteResumen.Aprobacion;
                    ClienteResumenFinal.AprobadoDes = ClienteResumen.AprobadoDes;

                    ClienteResumenFinal.DiasCredito = ClienteResumen.DiasCredito;
                    ClienteResumenFinal.BloqueoLineaCredito = ClienteResumen.BloqueoLineaCredito;

                    ClienteResumenFinal.Estado = ClienteResumen.Estado;
                    ClienteResumenFinal.EstadoDes = ClienteResumen.EstadoDes;
                    ClienteResumenFinal.FechaVCMTLinea = ClienteResumen.FechaVCMTLinea;

                    ClienteResumenFinal.NoVencido = Convert.ToDecimal(strsumNoVencido);
                    ClienteResumenFinal.PorVencer30 = Convert.ToDecimal(strsumImporte_PorVencer30);

                    ClienteResumenFinal.Vencido01a30 = Convert.ToDecimal(strsumImportePendiente_01a30);
                    ClienteResumenFinal.Vencido31a60 = Convert.ToDecimal(strsumImportePendiente_31a60);
                    ClienteResumenFinal.Vencido61a120 = Convert.ToDecimal(strsumImportePendiente_61a120);
                    ClienteResumenFinal.Vencido121a360 = Convert.ToDecimal(strsumImportePendiente_121a360);
                    ClienteResumenFinal.Vencido361amas = Convert.ToDecimal(strsumImportePendiente_361aMas);
                    ClienteResumenFinal.DeudaVencida = Convert.ToDecimal(strsumDeudaVencida);
                    ClienteResumenFinal.DeudaTotal = Convert.ToDecimal(strsumImportePendiente);
                    ClienteResumenFinal.LineaCredito = Convert.ToDecimal(strLineaCredito);
                    ClienteResumenFinal.CreditoDisponible = Convert.ToDecimal(strCreditoDisponible);
                    lstClienteResumenFinal.Add(ClienteResumenFinal);

                }
                ViewState["lstResumenCliente"] = JsonHelper.JsonSerializer(lstClienteResumenFinal);
                grdResumenCliente.DataSource = lstClienteResumenFinal;
                grdResumenCliente.DataBind();

                lblDate.Text = "1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable TablaLimiteCredito()
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
                dttabla.Columns.Add("DeudaVencida", typeof(string));

                dttabla.Columns.Add("DeudaTotal", typeof(string));

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
                if (!Page.IsPostBack)
                {

                    LoginWCFClient objLoginWCF = new LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idUsuario);

                    dpFinalEmision.SelectedDate = DateTime.Now;
                    //dpFechaHastaCliente.SelectedDate = DateTime.Now;
                    lblDate.Text = "";

                    string ID_Sectorista = Request.QueryString["ID_Sectorista"];
                    string id_cliente = Request.QueryString["id_cliente"];
                    string ID_zona = Request.QueryString["ID_zona"];
                    int len1 = 0;
                    int len2 = 0;

                    len1 = id_cliente.Length;
                    len1 = len1 - 1; 
                    id_cliente = id_cliente.Substring(1, len1);

                    len2 = ID_Sectorista.Length;
                    len2 = len2 - 1;
                    ID_Sectorista = ID_Sectorista.Substring(1, len2);

                    string Periodo = Request.QueryString["fechaInicial"];
                    int year = int.Parse( Periodo.Substring(0, 4));
                    int mes = int.Parse( Periodo.Substring(4, 2));

                    DateTime fecha;
                    DateTime firstOfNextMonth;
                    DateTime lastOfThisMonth;

                    firstOfNextMonth = new DateTime(year, mes, 1).AddMonths(1);
                    lastOfThisMonth = firstOfNextMonth.AddDays(-1);
                    fecha = lastOfThisMonth;
                    int dias = fecha.Day;
                    fecha = fecha.AddDays(-dias);

                    ViewState["fechaInicial"] = fecha;
                    dpFinalEmision.SelectedDate = fecha;

                    BuscarCliente(id_cliente);
                    Reporte_Cargar(year, mes);

                    List<gsReporte_DocumentosPendientesResumenClienteResult> lstDocumentos = new List<gsReporte_DocumentosPendientesResumenClienteResult>();
                    ViewState["lstEstadoCuentaResumenClienteTotal"] = JsonHelper.JsonSerializer(lstDocumentos);
                    lblDate2.Text = "2";

                }
            }

            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
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
                    //grdResumenVencimientos.DataSource = lst;
                    //grdResumenVencimientos.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se encontrarón resultados.";
                lblMensaje.CssClass = "mensajeError";
            }
        }

        //protected void btnBuscar_Click(object sender, EventArgs e)
        //{
        //    lblMensaje.Text = "";
        //    Reporte_Cargar();

        //}

        private void Reporte_Cargar(int anho, int mes)
        {
            grdGarantia.Visible = false;
            try
            {
                DateTime fecha1;
                DateTime fecha2;
                DateTime fecha3;
                DateTime fecha4;
                string Cliente;
                string Vendedor;
                int vencidos;

                Cliente = "";
                Vendedor = "";
                List<spEstadoCuenta_Proyectado_ClienteResult> lst;

                lblMensaje.Text = "";
                if (Session["Usuario"] == null)
                    Response.Redirect("~/Security/frmCerrar.aspx");

                try
                {
                    if (Validar_Variables() == 0)
                    {

                        string ID_Sectorista = Request.QueryString["ID_Sectorista"];
                        string id_cliente = Request.QueryString["id_cliente"];
                        int id_zona = int.Parse(Request.QueryString["id_zona"]);
                        int Periodo = int.Parse(Request.QueryString["fechaInicial"]);
                        int len1 = 0;
                        int len2 = 0;

                        len1 = id_cliente.Length;
                        len1 = len1 - 1;
                        id_cliente = id_cliente.Substring(1, len1);
                        len2 = ID_Sectorista.Length;
                        len2 = len2 - 1;
                        ID_Sectorista = ID_Sectorista.Substring(1, len2);


                        fecha2 = dpFinalEmision.SelectedDate.Value;

                        lblTitulo.Text = "Estado de Cuenta al " + fecha2.ToShortDateString();

                        if (acbCliente == null || acbCliente.Text.Split('-')[0] == "" || acbCliente.Text == "")
                        {
                            Cliente = null;
                        }
                        else { Cliente = acbCliente.Text.Split('-')[0]; }

                        lst = ListarEstadoCuenta(Cliente, Periodo, ID_Sectorista, id_zona, anho, mes);

                        ListarClientesResumen(lst);
                        ListarClientesResumenVencidos();

                        ListarGarantia(Cliente);
                    }

                }
                catch (Exception ex)
                {
                    lblMensaje.Text = ex.Message;
                    lblMensaje.CssClass = "mensajeError";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se encontrarón resultados.";
                lblMensaje.CssClass = "mensajeError";
            }
        }

        public void ListarGarantia(string Cliente)
        {
            AgendaWCFClient objAgendaGarantiaWCF = new AgendaWCFClient();
            List<gsAgenda_ListarGarantiaResult> lstGaran = objAgendaGarantiaWCF.Agenda_ListarGarantia(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, 1, Cliente).ToList();
            var liq = from c in lstGaran select c; // select new { c.FechaVencimiento, c.Valor };

            if (lstGaran.Count > 0 && Cliente != "")
            {
                grdGarantia.DataSource = liq.ToList();
                grdGarantia.DataBind();
            }
            else
            {
                grdGarantia.DataSource = null;
                grdGarantia.DataBind();
            }
            grdGarantia.Visible = true;

        }

        public int Validar_Variables()
        {
            int valor = 0;

            if (dpFinalEmision == null || dpFinalEmision.SelectedDate.Value.ToString() == "")
            {
                valor = 1;
                lblMensaje.Text = lblMensaje.Text + "Seleccionar fecha final de emisión. ";
                lblMensaje.CssClass = "mensajeError";
            }
            if (acbCliente == null || acbCliente.Text.Length < 4)
            {
                //if (acbVendedor == null || acbVendedor.Text.Length < 4)
                //{
                    acbCliente = null;
                    //acbVendedor = null;
                    valor = 1;
                    lblMensaje.Text = lblMensaje.Text + "Seleccionar cliente o vendedor.";
                    lblMensaje.CssClass = "mensajeError";
                //}
            }

            return valor;
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

        [WebMethod]
        public static AutoCompleteBoxData Agenda_BuscarVendedor(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarVendedorResult[] lst = objAgendaWCFClient.Agenda_ListarVendedor(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsAgenda_ListarVendedorResult agenda in lst)
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

        protected void btnExpResumen_Click(object sender, ImageClickEventArgs e)
        {
            if (lblDate2.Text == "2")
            {
                ExportGridToExcel_Resumen();
            }
        }

        protected void btnExpDetalle_Click(object sender, ImageClickEventArgs e)
        {
            ExportarExcelDetalle();
        }

        protected void ExportarExcelDetalle()
        {
            string Cliente;
            string Vendedor;

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
                //ExporttoExcel(GridView1);

                string ArchivoExcel = "EstadoCuenta";

                if (acbCliente == null || acbCliente.Text.Split('-')[0] == "" || acbCliente.Text == "")
                {
                }
                else
                {
                    Cliente = acbCliente.Text.Split('-')[0];
                    ArchivoExcel = ArchivoExcel + "_" + Cliente;
                }
                //if (acbVendedor == null || acbVendedor.Text.Split('-')[0] == "" || acbVendedor.Text == "")
                //{
                //}
                //else
                //{
                //    Vendedor = acbVendedor.Text.Split('-')[0];
                //    ArchivoExcel = ArchivoExcel + "_" + Vendedor;
                //}

                ExporttoExcel_V2(GridView1, ArchivoExcel);
   
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
            foreach (DataColumn col in table.Columns)
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

        protected void ExportarPDF_Detalle()
        {

            int idEmpresa;
            string fechaHasta;
            int codUsuario;

            fechaHasta = dpFinalEmision.SelectedDate.Value.ToString("dd/MM/yyyy");
            idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;

            codUsuario = Convert.ToInt32(((Usuario_LoginResult)Session["Usuario"]).nroDocumento);

            if (lblDate.Text == "1")
            {
                ExportarPDF(idEmpresa, fechaHasta);

            }
        }

        protected void btnExpPDFDetalle_Click(object sender, ImageClickEventArgs e)
        {
            ExportarPDF_Detalle();
        }

        private DataTable funConvertGVToDatatable(GridView dtgrv)
        {
            string campo;

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
                        campo = row.Cells[i].Text.Replace(" ", "");
                        campo = campo.Replace("&nbsp;", "");
                        //campo = campo.Replace(",", "");

                        dr[i] = campo;
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
            string fileName;
            int x;
            string path2 = this.Server.MapPath(".") + "\\tempArchivos\\";
            for (x = 1; x < 3; x++)
            {
                fileName = GetFileName(idEmpresa);
                PdfPTable tableLayout = new PdfPTable(11);

                if (!System.IO.Directory.Exists(path2))
                {
                    System.IO.Directory.CreateDirectory(path2);
                }

                string destFile = System.IO.Path.Combine(path2, fileName);

                Document doc = new Document();
                doc = new Document(PageSize.LETTER, 20, 20, 20, 20);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(destFile, FileMode.Create));

                doc.Open();
                doc.Add(Add_Content_To_PDF(tableLayout, fechaHasta, idEmpresa));
                doc.Close();

                AbriVentana(fileName);

            }

        }

        public static byte[] StreamFile(string filename)
        {
            var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var imageData = new byte[fs.Length];
            fs.Read(imageData, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            return imageData;
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
            if (idEmpresa == 1)
            { empresa = "Sil"; }
            else
            { empresa = "Neo"; }

            file = empresa + "EstadoCuenta_" + anho + mes + dia + minutos + segundo + miliseg + ".pdf";

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

            logo.ScaleAbsolute(205, 90);
            PdfPCell imageCell = new PdfPCell(logo);
            imageCell.Colspan = 2; // either 1 if you need to insert one cell
            imageCell.Border = 0;
            imageCell.HorizontalAlignment = Element.ALIGN_LEFT;

            //tableLayout.AddCell(imageCell);
            tableLayout.AddCell(new PdfPCell(new Phrase("Estado de cuenta al " + fechaHasta, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 10, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });
            tableLayout.AddCell(new PdfPCell(new Phrase(DateTime.Now.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 5, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 1, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });

            tableLayout.AddCell(new PdfPCell(new Phrase(objEmpresa.ruc + " " + objEmpresa.razonSocial, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 6, Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT });
            tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 5, Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER });

            tableLayout.AddCell(new PdfPCell(new Phrase(objEmpresa.direccion, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 6, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_LEFT });
            tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 5, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });

            //Add Cliente
            int cont = 0;

            foreach (gsAgendaCliente_BuscarLimiteCreditoResult ClienteResumen in lstClienteResumen)
            {
                cont = cont + 1;
                // ADD Cliente

                tableLayout.AddCell(new PdfPCell(new Phrase("1. Razón Social", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                List<gsAgenda_BuscarClienteDetalleResult> LimiteAgenda = objAgendaWCF.Agenda_BuscarClienteDetalle(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, ClienteResumen.ID_Agenda.ToString()).ToList();
                if (LimiteAgenda.Count > 0)
                {
                    gsAgenda_BuscarClienteDetalleResult AgendaResumen = LimiteAgenda[0];
                    tableLayout.AddCell(new PdfPCell(new Phrase(AgendaResumen.ruc + " " + AgendaResumen.Agendanombre, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase(AgendaResumen.Direccion + " " + AgendaResumen.Distrito + " - " + AgendaResumen.Provincia + " - " + AgendaResumen.Departamento + " - " + AgendaResumen.Pais, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, BorderColorLeft = BaseColor.RED, BorderColorTop = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                }
                else
                {
                    tableLayout.AddCell(new PdfPCell(new Phrase(ClienteResumen.ruc + " " + ClienteResumen.AgendaNombre, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Registrar dirección fiscal. ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, BorderColorLeft = BaseColor.RED, BorderColorTop = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                }

                tableLayout.AddCell(new PdfPCell(new Phrase("   ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 11, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });

                //ADD Resumen cliente
                decimal TCResumen = Convert.ToDecimal(ClienteResumen.TipoCambio);
                if (TCResumen < 0)
                {
                    TCResumen = TCResumen * -1;
                }


                var query_Detalle = from c in lstClienteDetalle
                                    where c.ID_Agenda == ClienteResumen.ID_Agenda
                                    orderby c.ClienteNombre, c.FechaVencimiento
                                    select new
                                    {
                                        c.TC,
                                        c.ID_Moneda,
                                        c.ID_Agenda,

                                        Pendiente = c.ID_Moneda == 0 ? c.ImportePendiente :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente / c.TC :
                                                    c.ImportePendiente,

                                        Pendiente_NoVencido = c.ID_Moneda == 0 ? c.ImportePendiente_NoVencido :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_NoVencido / c.TC :
                                                    c.ImportePendiente_NoVencido,

                                        Pendiente_VenceHoy = c.ID_Moneda == 0 ? c.ImportePendiente_VenceHoy :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_VenceHoy / c.TC :
                                                    c.ImportePendiente_VenceHoy,

                                        Pendiente_01a30 = c.ID_Moneda == 0 ? c.ImportePendiente_01a30 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_01a30 / c.TC :
                                                    c.ImportePendiente_01a30,
                                        Pendiente_31a60 = c.ID_Moneda == 0 ? c.ImportePendiente_31a60 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_31a60 / c.TC :
                                                    c.ImportePendiente_31a60,
                                        Pendiente_61a120 = c.ID_Moneda == 0 ? c.ImportePendiente_61a120 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_61a120 / c.TC :
                                                    c.ImportePendiente_61a120,
                                        Pendiente_121a360 = c.ID_Moneda == 0 ? c.ImportePendiente_121a360 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_121a360 / c.TC :
                                                    c.ImportePendiente_121a360,
                                        Pendiente_361aMas = c.ID_Moneda == 0 ? c.ImportePendiente_361aMas :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_361aMas / c.TC :
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
                decimal DeudaTotal;
                NoVencido = Convert.ToDecimal(sumImportePendiente_NoVencido) + Convert.ToDecimal(sumImportePendiente_VenceHoy);
                DeudaVencida = Convert.ToDecimal(sumImportePendiente_01a30) + Convert.ToDecimal(sumImportePendiente_31a60) + Convert.ToDecimal(sumImportePendiente_61a120) + Convert.ToDecimal(sumImportePendiente_121a360) + Convert.ToDecimal(sumImportePendiente_361aMas);
                DeudaTotal = NoVencido + DeudaVencida;

                string strsumNoVencido = string.Format("{0:$ #,##0.00}", NoVencido);
                string strsumImportePendiente_01a30 = string.Format("{0:$ #,##0.00}", sumImportePendiente_01a30);
                string strsumImportePendiente_31a60 = string.Format("{0:$ #,##0.00}", sumImportePendiente_31a60);
                string strsumImportePendiente_61a120 = string.Format("{0:$ #,##0.00}", sumImportePendiente_61a120);
                string strsumImportePendiente_121a360 = string.Format("{0:$ #,##0.00}", sumImportePendiente_121a360);
                string strsumImportePendiente_361aMas = string.Format("{0:$ #,##0.00}", sumImportePendiente_361aMas);
                string strsumImportePendiente = string.Format("{0:$ #,##0.00}", sumImportePendiente);

                string strsumDedudaTotal = string.Format("{0:$ #,##0.00}", DeudaTotal);
                string strsumDeudaVencida = string.Format("{0:$ #,##0.00}", DeudaVencida);

                string strLineaCredito = string.Format("{0:$ #,##0.00}", ClienteResumen.LineaCredito);
                string strDeudaVencida = string.Format("{0:$ #,##0.00}", strsumDeudaVencida);
                string strTotalCredito = string.Format("{0:$ #,##0.00}", strsumDedudaTotal);
                string strCreditoDisponible = string.Format("{0:$ #,##0.00}", ClienteResumen.CreditoDisponible);


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
                tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 31a60", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 61a120", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 121a360", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 361aMas", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
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
                                         where c.ID_Agenda == ClienteResumen.ID_Agenda
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

                    if (query_Clientel.ID_Moneda == 0)
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
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = intCol, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });

        }

        private static void AddCellToBody(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

        }

        private static void AddCellToBodyColspan(PdfPTable tableLayout, string cellText, int intCol)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = intCol, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

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
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
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

        private void PDF(int idEmpresa, string fechaHasta)
        {
            PdfPTable tableLayout = new PdfPTable(11);
            string fileName = "ReporteEstadoCuenta_" + DateTime.Now.Millisecond.ToString() + ".pdf";

            GridView GridView2 = new GridView();
            GridView2.DataBind();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            GridView2.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document doc = new Document();
            doc = new Document(PageSize.LETTER, 20, 20, 20, 20);

            HTMLWorker htmlparser = new HTMLWorker(doc);
            PdfWriter.GetInstance(doc, Response.OutputStream);
            doc.Open();
            doc.Add(Add_Content_To_PDF(tableLayout, fechaHasta, idEmpresa));
            htmlparser.Parse(sr);
            doc.Close();

            Response.Write(doc);
            Response.End();
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


        private void ExporttoExcel_V2(GridView GridView1, string Archivo)
        {
            string stringPeriodo;
            int year1;
            int mes1, mes2, mesDefoult;
            int year2, year3;
            string Origen;
            string Destino;

            try
            {
                DataTable table = new DataTable();
                table = funConvertGVToDatatable(GridView1);
                string strFecha = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

                //Origen = "C:\\Users\\cesar.coronel.GS\\Desktop\\Plantillas\\Origen\\EstadoCuentaDetalle.xls";
                Origen = "C:\\inetpub\\www\\IntranetGS\\Finanzas\\Plantillas\\EstadoCuentaDetalle.xls";


                //Destino = "C:\\Users\\cesar.coronel.GS\\Desktop\\Plantillas\\Destino\\" + Archivo + "_" + strFecha + ".xls";
                Destino = "C:\\temp\\uploads\\" + Archivo + "_" + strFecha + ".xls";

                File.Copy(Origen, Destino, true);

                ESCRIBIR_EXCEL_GENERAL(Destino, "Detalle", table, "MANTENIMIENTO", "CREDITOS");

                System.IO.FileInfo toDownload = new System.IO.FileInfo(Destino);

                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
                Response.AddHeader("Content-Length", toDownload.Length.ToString());
                Response.ContentType = "application/xls";
                string tab = string.Empty;
                Response.WriteFile(Destino);
                Response.End();

                //Response.ClearContent();
                //Response.ClearHeaders();
                //Response.ContentType = "application/vnd.ms-excel";
                //string document = toDownload.Name;
                //Response.AddHeader("Content-Disposition", string.Format("attachment; filename = {0}", document));
                //Response.WriteFile(Destino);
                //Response.Flush();



                //HttpContext.Current.Response.Clear();
                //HttpContext.Current.Response.AddHeader("Content-Disposition",
                //           "attachment; filename=" + toDownload.Name);
                //HttpContext.Current.Response.AddHeader("Content-Length",
                //           toDownload.Length.ToString());
                //HttpContext.Current.Response.ContentType = "application/octet-stream";
                //HttpContext.Current.Response.WriteFile(Destino);
                //HttpContext.Current.Response.End();


            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }



        }

        private void Exportar_DetalleEstado(RadGrid GridView1, string Archivo)
        {
            try
            {

                //grdEstadoCuenta = GridView1;

                grdEstadoCuenta.ExportSettings.FileName = Archivo;

                grdEstadoCuenta.ExportSettings.ExportOnlyData = true;
                grdEstadoCuenta.ExportSettings.IgnorePaging = true;
                grdEstadoCuenta.ExportSettings.OpenInNewWindow = true;
                grdEstadoCuenta.MasterTableView.ExportToExcel();

                Page.Response.ClearHeaders();
                Page.Response.ClearContent();

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }



        }

        public void ESCRIBIR_EXCEL_GENERAL(string Directorio, string Nombre_Hoja, System.Data.DataTable dtDatos, string stTipo, string stSubTipo)
        {
            try
            {
                if (stTipo == "MANTENIMIENTO")
                {
                    if (stSubTipo == "CREDITOS")
                    {
                        Escribir_Excel_Proyectado(Directorio, Nombre_Hoja, dtDatos);
                        //PintarFormatoPareto(Directorio, Nombre_Hoja, dtDatos); 
                    }
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }
        }

        public void Escribir_Excel_Proyectado(string Directorio, string NombreArchivo, DataTable Tabla)
        {
            string stringPeriodo;
            int year1;
            int mes1, mes2, mesDefoult;
            int year2, year3;

            string quote = "\"";
            string strConnnectionOle = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directorio + ";Extended Properties=" + quote + "Excel 8.0;HDR=NO" + quote + "";
            //string strConnnectionOle = "Microsoft.ACE.OLEDB.12.0;Data Source=" + Directorio + ";Extended Properties=Excel 8.0;HDR=NO";

            OleDbConnection oleConn = new OleDbConnection(strConnnectionOle);
            try
            {
                oleConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = oleConn;

                int countColumn = Tabla.Columns.Count - 1;
                int y = 0;
                char a;
                int cod;
                string Letra = "A";
                a = Convert.ToChar(Letra);
                cod = (int)a;


                //foreach (DataColumn col in Tabla.Columns)
                //{//write in new col

                //    cmd.CommandText = "UPDATE [" + NombreArchivo + "$" + Letra + "4:" + Letra + "4] SET F1=" + "'" + col.ColumnName.ToString() + "'";
                //    cmd.ExecuteNonQuery();

                //    cod = cod + 1;
                //    a = (char)cod;
                //    Letra = Convert.ToString(a);
                //}

                int fila = 5;

                foreach (DataRow row in Tabla.Rows)
                {//write in new row
                    string pareto = row[countColumn].ToString();
                    Letra = "A";
                    a = Convert.ToChar(Letra);
                    cod = (int)a;
                    string Texto = "";


                    Texto = "UPDATE [" + NombreArchivo + "$" + "A" + fila.ToString() + ":" + "O" + fila.ToString() + "]   ";
                    Texto = Texto + " SET F1=" + "'" + row[0].ToString() + "', ";
                    Texto = Texto + "  F2=" + "'" + row[1].ToString() + "', ";
                    Texto = Texto + "  F3=" + "'" + row[2].ToString() + "', ";
                    Texto = Texto + "  F4=" + "'" + row[3].ToString() + "', ";
                    Texto = Texto + "  F5=" + "'" + row[4].ToString() + "', ";
                    Texto = Texto + "  F6=" + "'" + row[5].ToString() + "', ";
                    Texto = Texto + "  F7=" + "'" + row[6].ToString() + "', ";
                    Texto = Texto + "  F8=" + "'" + row[7].ToString() + "', ";
                    Texto = Texto + "  F9=" + "'" + row[8].ToString() + "', ";
                    Texto = Texto + "  F10=" + "'" + row[9].ToString() + "', ";
                    Texto = Texto + "  F11=" + "'" + row[10].ToString() + "', ";
                    Texto = Texto + "  F12=" + "'" + row[11].ToString() + "', ";
                    Texto = Texto + "  F13=" + "'" + row[12].ToString() + "', ";
                    Texto = Texto + "  F14=" + "'" + row[13].ToString() + "', "; 
                    Texto = Texto + "  F15=" + "'" + row[14].ToString() + "' ";

                    cmd.CommandText = Texto;
                    cmd.ExecuteNonQuery();

                    fila = fila + 1;
                }


                oleConn.Close();
            }
            catch (Exception ex)
            {
                oleConn.Close();
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }

        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (string.IsNullOrEmpty(Request.QueryString["fechaInicial"]))
                    Response.Redirect("~/Finanzas/Cobranzas/Proyeccion/frmProyectado.aspx");
                else
                    Response.Redirect("~/Finanzas/Cobranzas/Proyección/frmProyectado.aspx?fechaInicio=" + ((DateTime)ViewState["fechaInicial"]).ToString("dd/MM/yyyy") + "&ID_Sectorista=" + Request.QueryString["ID_Sectorista"] + "&ID_zona=" + Request.QueryString["ID_zona"] + "&ID_Vendedor=" + Request.QueryString["ID_Vendedor"]);
            }
            catch (Exception ex)
            {
                rwmVidaLey.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

        public void BuscarCliente(string context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();

            AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
            gsAgenda_ListarClienteResult[] lst = objAgendaWCFClient.Agenda_ListarCliente(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, context, 0);

            List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

            foreach (gsAgenda_ListarClienteResult agenda in lst)
            {
                AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                childNode.Text = agenda.ID_Agenda + "-" + agenda.Nombre;
                childNode.Value = agenda.ID_Agenda;
                result.Add(childNode);
            }

            res.Items = result.ToArray();
            acbCliente.Entries.Add(new AutoCompleteBoxEntry(res.Items[0].Text, res.Items[0].Value));
        }

        //public void BuscarVendedor(string context)
        //{
        //    AutoCompleteBoxData res = new AutoCompleteBoxData();
        //    AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();

        //    gsAgenda_ListarVendedorResult[] lst = objAgendaWCFClient.Agenda_ListarVendedor(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, context);

        //    List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

        //    foreach (gsAgenda_ListarVendedorResult agenda in lst)
        //    {
        //        AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
        //        childNode.Text = agenda.ID_Agenda + "-" + agenda.Nombre;
        //        childNode.Value = agenda.ID_Agenda;
        //        result.Add(childNode);
        //    }

        //    res.Items = result.ToArray();
        //    acbVendedor.Entries.Add(new AutoCompleteBoxEntry(res.Items[0].Text, res.Items[0].Value));
        //}

        protected void grdEstadoCuenta_ItemCommand(object sender, GridCommandEventArgs e)
        {
            string Origen;
            string OrigenOp;
            string id_Sectorista;
            string id_cliente;
            string stringPeriodo;
            //string year;
            //string mes;
            string day;

            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if (e.CommandName == "Proyectar")
                {
                    //year = dpFinalEmision.SelectedDate.Value.Year.ToString();
                    //mes = dpFinalEmision.SelectedDate.Value.Month.ToString();

                    //if(mes.Length == 1)
                    //{
                    //    mes = "0" + mes;
                    //}

                    string Periodo = Request.QueryString["fechaInicial"];
                    string year = Periodo.Substring(0, 4);
                    string mes = Periodo.Substring(4, 2);


                    GridDataItem dataitem = (GridDataItem)e.Item;

                    id_Sectorista = Request.QueryString["id_Sectorista"];
                    Origen = dataitem["Origen"].Text;
                    OrigenOp = dataitem["OrigenOp"].Text;

                    stringPeriodo = year + "" + mes;

                    spEstadoCuenta_Proyectado_ClienteResult objDocumento = new spEstadoCuenta_Proyectado_ClienteResult();
                    spEstadoCuenta_Proyectado_ClienteResult objDocumentoE = new spEstadoCuenta_Proyectado_ClienteResult();

                    List<spEstadoCuenta_Proyectado_ClienteResult> lst = JsonHelper.JsonDeserialize<List<spEstadoCuenta_Proyectado_ClienteResult>>((string)ViewState["lstEstadoCuenta"]);

                    objDocumentoE = lst.FindAll(x => x.Origen == Origen && x.OrigenOp == decimal.Parse(OrigenOp)).Single();
                    objDocumento.NroDocumento = objDocumentoE.NroDocumento;
                    objDocumento.Origen = objDocumentoE.Origen;
                    objDocumento.OrigenOp = objDocumentoE.OrigenOp;
                    objDocumento.TipoDocumento = objDocumentoE.TipoDocumento;
                    objDocumento.id_Cliente = objDocumentoE.id_Cliente;
                    objDocumento.ImportePendiente = objDocumentoE.ImportePendiente;
                    objDocumento.monedasigno = objDocumentoE.monedasigno;

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreateViewProyectar('" + JsonHelper.JsonSerializer(objDocumento) + "," + stringPeriodo + "');", true);
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreateViewProyectar(" + id_cliente  + "," + id_Sectorista + "," + id_proyectado + "," + stringPeriodo + ");", true);

                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void ramEstadoCuenta_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if (e.Argument == "Rebind")
                {
                    //grdDocVenta.MasterTableView.SortExpressions.Clear();
                    //grdDocVenta.MasterTableView.GroupByExpressions.Clear();
                    //Buscar_ListarProyectado();
                    //grdDocVenta.Rebind();

                    lblMensaje.Text = "Proyección cargada con éxito.";
                    lblMensaje.CssClass = "mensajeExito";
                }


                if (e.Argument.Split('(')[0].Trim() == "RebindAndNavigateBuscar")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "buscar();", true);

                }

                if (e.Argument.Split('(')[0].Trim() == "RebindAndNavigate")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "buscarHistorico();", true);
                }

                if (e.Argument.Split('(')[0].Trim() == "RebindAndNavigateProyectado")
                {
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "buscar();", true);

                    if (lblDate.Text == "1")
                    {
                        string Periodo = Request.QueryString["fechaInicial"];
                        int year = int.Parse(Periodo.Substring(0, 4));
                        int mes = int.Parse(Periodo.Substring(4, 2));

                        Reporte_Cargar(year, mes);
                    }


                }


            }
            catch (Exception ex)
            {
                rwmVidaLey.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

    }
}