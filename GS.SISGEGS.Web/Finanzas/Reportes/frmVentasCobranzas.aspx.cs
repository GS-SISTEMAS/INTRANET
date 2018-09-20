using GS.SISGEGS.DM;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.CobranzasWCF;
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.EstadoCuentaWCF;
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
using System.IO;
using System.Data.OleDb;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using GS.SISGEGS.Web.LoginWCF;
using System.ComponentModel; 

namespace GS.SISGEGS.Web.Finanzas.Reportes
{
    public partial class frmVentasCobranzas : System.Web.UI.Page
    {
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

                    Cargar_comboAnhos();
                    cboAnhos.SelectedValue = DateTime.Now.Year.ToString();
                    lblDate.Text = "";

                }
            }

            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime fecha1;
            DateTime fecha2;
            int year;
            string Cliente;
            string Vendedor; 

            Cliente = "";
            Vendedor = "";

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if(Validar_Variables() == 0)
                {
                    fecha1 = DateTime.Now.AddYears(-50);
                    fecha2 = DateTime.Now.AddYears(50);


                    if (acbCliente == null || acbCliente.Text.Split('-')[0] == "" || acbCliente.Text == "")
                    {
                        Cliente = null;
                    }
                    else { Cliente = acbCliente.Text.Split('-')[0]; }

                    if (acbVendedor == null || acbVendedor.Text.Split('-')[0] == "" || acbVendedor.Text == "")
                    {
                        Vendedor = null;
                    }
                    else
                    { Vendedor = acbVendedor.Text.Split('-')[0]; }

                    year =  Convert.ToInt32(cboAnhos.SelectedValue) ; 

                    BuscarVentasCobranzas(Cliente, Vendedor, fecha1, fecha2, year);
                  
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        public void Cargar_comboAnhos()
        {
            for (int i = 2016; i <= 2025; i++)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = i.ToString();
                item.Value  = i.ToString();
                cboAnhos.Items.Add(item);
            }
        }

        public void BuscarVentasCobranzas(string idCliente, string idVendedor, DateTime fechaForm1, DateTime fechaForm2, int year)
        {
            DateTime fecha1;
            DateTime fecha2;

            string Cliente;
            string Vendedor;
            int mesActual; 

            Cliente = null;
            Vendedor = null;
            mesActual = DateTime.Now.Month;

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");
            try
            {
                if (Validar_Variables() == 0)
                {
                    fecha1 = fechaForm1;
                    fecha2 = fechaForm2;

                    Cliente = idCliente;
                    Vendedor = idVendedor;

                    ListarVentasCobranzas(Cliente, Vendedor, fecha1, fecha2, year, mesActual);
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }

        }

        private void ListarVentasCobranzas(string codAgenda, string codVendedor, DateTime fechaInicial, DateTime fechaFinal,int year, int mesActual)
        {
            DataTable dtTablaCruce = TablaVentasCobranzas();

            CobranzasWCFClient objCobranzasWCF = new CobranzasWCFClient();
            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();

            try
            {
                List<gsVentasCobranzas_ListarResult> lstCobranzas = objCobranzasWCF.Reporte_VentasCobranzasAnual(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, year).ToList();

                dtTablaCruce = ConvertToDataTable(lstCobranzas); 

                rhcVentaCobranza.DataSource = GetData(dtTablaCruce);
                rhcVentaCobranza.DataBind();

                grdVentasCobranzas.DataSource = dtTablaCruce;
                grdVentasCobranzas.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }

            return table;

        }

        private DataTable PivotTabla(DataTable dtTabla)
        {
            DataTable dtReturn = new DataTable() ;
            dtReturn = TablaVentasCobranzasChart();
            int column;
            int rows;
            column = dtTabla.Columns.Count - 1 ;
            rows = 4;

            int x;
            int y;

            try
            {
            for (x = 1; x < column - 2 ; x++)
              {
                DataRow dtRow = dtReturn.NewRow();
                

                for (y = 0; y < rows; y++)
                {
                        if( y==0 || y == 3)
                        {
                            dtRow[0] = dtTabla.Rows[y][14];
                            dtRow[1] = x;
                            if (y == 0)
                            {
                                dtRow[2] = dtTabla.Rows[y][x];
                            }
                            else
                            {
                                dtRow[3] = dtTabla.Rows[y][x];
                            }
                        }
                }
                dtReturn.Rows.Add(dtRow); 
                }
              }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            return dtReturn; 
        }

        public decimal DeudaVencidaMes(string codAgenda, string codVendedor, int anho, int mes)
        {
            decimal deudaVencidaMes = 0;
            DateTime fecha;

            if( mes == DateTime.Now.Month )
            {
                fecha = DateTime.Now;
            }
            else
            {
                DateTime firstOfNextMonth = new DateTime(anho, mes, 1).AddMonths(1);
                DateTime lastOfThisMonth = firstOfNextMonth.AddDays(-1);
                fecha = lastOfThisMonth;
            }

            try
            {
                List<gsReporteVencidosPorMesResult> lstPendientes = BuscarDocumentosPendientes_VencidosMes( fecha);
                ViewState["lstEstadoCuentaAfiliadas"] = JsonHelper.JsonSerializer(lstPendientes);

                var query_Detalle = from c in lstPendientes
                                where c.EstadoCliente != "AFILIADA"
                                select new
                                {
                                    c.DeudaMes
                                };

            var QueryDeudaMes = query_Detalle.ToList().Select(c => c.DeudaMes).Sum();

            decimal DeudaVencida;
            DeudaVencida = Convert.ToDecimal(QueryDeudaMes);
            deudaVencidaMes = DeudaVencida;

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            return deudaVencidaMes; 

        }

        public decimal DeudaVencidaAfiliados()
        {
            decimal deudaVencidaMes = 0;

            try
            {

                List<gsReporteVencidosPorMesResult> lstPendientes = JsonHelper.JsonDeserialize<List<gsReporteVencidosPorMesResult>>((string)ViewState["lstEstadoCuentaAfiliadas"]);

                var query_Detalle = from c in lstPendientes
                                    where c.EstadoCliente == "AFILIADA"
                                    select new
                                    {
                                        c.DeudaMes
                                    };

                var QueryDeudaMes = query_Detalle.ToList().Select(c => c.DeudaMes).Sum();

                decimal DeudaVencida;
                DeudaVencida = Convert.ToDecimal(QueryDeudaMes);
                deudaVencidaMes = DeudaVencida;

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            return deudaVencidaMes;

        }

        public decimal CobradoMesActual(string codAgenda, string codVendedor, int anho, int mes)
        {
            decimal decimalTotalMes = 0;
            CobranzasWCFClient objCobranzasWCF = new CobranzasWCFClient();
            List<gsReporteCanceladosResumenMes_actualResult> lstCobranzas = new List<gsReporteCanceladosResumenMes_actualResult>();
            try
            {
         
                lstCobranzas = objCobranzasWCF.Reporte_CancelacionesResumenActual_v2(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, codAgenda, codVendedor, anho, mes).ToList();

                    var query_Detalle = from c in lstCobranzas
                                        where c.periodoYearE == anho & c.periodoMesE == mes
                                        select new
                                        {
                                        c.periodoYearE,
                                        c.periodoMesE,
                                        c.totalMes
                                    };

                var sumTotalMes = query_Detalle.ToList().Select(c => c.totalMes).Sum();
                decimalTotalMes = Convert.ToDecimal(sumTotalMes); 
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            return decimalTotalMes;

        }

        public List<gsReporteVencidosPorMesResult> BuscarDocumentosPendientes_VencidosMes( DateTime fechaForm2)
        {
            List<gsReporteVencidosPorMesResult> lst = new List<gsReporteVencidosPorMesResult>();

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (Validar_Variables() == 0)
                {
                    lst = ListarEstadoCuenta_VencidoMes(fechaForm2);
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            return lst;
        }

        private List<gsReporteVencidosPorMesResult> ListarEstadoCuenta_VencidoMes( DateTime fechaEmisionFinal)
        {
            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();
            try
            {
                List<gsReporteVencidosPorMesResult> lst = objEstadoCuentaWCF.EstadoCuenta_VencidosMes(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, fechaEmisionFinal).ToList();

                if (lst.Count > 0)
                {
                    lblDate.Text = "1";
                }

                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataSet GetData(DataTable dtTabla)
        {
            DataSet ds = new DataSet("ChargeCurrentTimeRatio");
            DataTable dt = new DataTable("ChargeData");
            dt = PivotTabla(dtTabla); 

            ds.Tables.Add(dt);
            return ds;
        }

        private DataTable TablaVentasCobranzas()
        {
            DataTable dttabla = new DataTable();
            try
            {
                
                dttabla.Columns.Add("conceptos", typeof(string));
                dttabla.Columns.Add("enero", typeof(string));
                dttabla.Columns.Add("febrero", typeof(string));
                dttabla.Columns.Add("marzo", typeof(string));
                dttabla.Columns.Add("abril", typeof(string));
                dttabla.Columns.Add("mayo", typeof(string));
                dttabla.Columns.Add("junio", typeof(string));
                dttabla.Columns.Add("julio", typeof(string));
                dttabla.Columns.Add("agosto", typeof(string));
                dttabla.Columns.Add("septiembre", typeof(string));
                dttabla.Columns.Add("octubre", typeof(string));
                dttabla.Columns.Add("noviembre", typeof(string));
                dttabla.Columns.Add("diciembre", typeof(string));
                dttabla.Columns.Add("total", typeof(string));
                dttabla.Columns.Add("id_cliente", typeof(string));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            return dttabla;
        }

        private DataTable TablaVentasCobranzasChart()
        {
            DataTable dttabla = new DataTable();
            try
            {
                dttabla.Columns.Add("id_cliente", Type.GetType("System.String"));
                dttabla.Columns.Add("mes", Type.GetType("System.Int32"));
                dttabla.Columns.Add("ventas", Type.GetType("System.Decimal"));
                dttabla.Columns.Add("cobranzas", Type.GetType("System.Decimal"));
               
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            return dttabla;
        }
        public int Validar_Variables()
        {
            int valor = 0;

            if ( cboAnhos == null || cboAnhos.SelectedValue.ToString() == "")
            {
                valor = 1;
                lblMensaje.Text = lblMensaje.Text + "Seleccionar fecha desde. ";
                lblMensaje.CssClass = "mensajeError";
            }

            //if (acbCliente == null || acbCliente.Text.Length < 4 )
            //{
            //    if (acbVendedor == null || acbVendedor.Text.Length < 4)
            //    {
            //        acbCliente = null;
            //        acbVendedor = null;
            //        valor = 1;
            //        lblMensaje.Text = lblMensaje.Text + "Seleccionar cliente o vendedor.";
            //        lblMensaje.CssClass = "mensajeError";
            //    }
            //}

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

        protected void grdVentasCobranzas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (lblDate.Text == "1")
                {
                    List<gsReporteCanceladosWebResult> lst = JsonHelper.JsonDeserialize<List<gsReporteCanceladosWebResult>>((string)ViewState["lstCobranzas"]);
                    grdVentasCobranzas.DataSource = lst;
                    grdVentasCobranzas.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se encontrarón resultados.";
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnExpDetalle_Click(object sender, ImageClickEventArgs e)
        {
            if (lblDate.Text == "1")
            {
                List<gsReporte_DocumentosPendientesResult> lst = JsonHelper.JsonDeserialize<List<gsReporte_DocumentosPendientesResult>>((string)ViewState["lstEstadoCuenta"]);

                var query_Detalle = from c in lst
                                    orderby c.ClienteNombre, c.Fecha
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
                                orderby c.ClienteNombre, c.Fecha
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

        protected void btnExpPDFDetalle_Click(object sender, ImageClickEventArgs e)
        {
            int idEmpresa;
            string fechaDesde;
            string fechaHasta;
            int codUsuario;

            //fechaDesde = dpDesde.SelectedDate.Value.ToString("dd/MM/yyyy");
        

            idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;

            codUsuario = Convert.ToInt32(((Usuario_LoginResult)Session["Usuario"]).nroDocumento);

            if (lblDate.Text == "1")
            {
                //ExportarPDF(idEmpresa, fechaHasta);
                //PDF(idEmpresa, fechaHasta);
                //ShowPdf(CreatePDF2(idEmpresa, fechaHasta));
                //CreatePDFFromMemoryStream(idEmpresa, fechaHasta);
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


            //PdfReader pdfReader = new PdfReader(destFile);
            //string FileLocation = destFile;

            //PdfStamper stamp = new PdfStamper(pdfReader, new FileStream(FileLocation.Replace(".pdf", "[temp][file].pdf"), FileMode.Create));

            //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/Logos/grupo.png"));
            //img.ScaleToFit(1700, 800);
            //img.Alignment = iTextSharp.text.Image.UNDERLYING;
            //img.SetAbsolutePosition(100, 150);
            //img.ScaleAbsoluteHeight(500);
            //img.ScaleAbsoluteWidth(500);
          
            //PdfGState graphicsState = new PdfGState();
            //graphicsState.FillOpacity = 0.02f;
            //graphicsState.StrokeOpacity = 0.03f;
            //graphicsState.AlphaIsShape = true;

            //PdfContentByte waterMark;
            //for (int page = 1; page <= pdfReader.NumberOfPages; page++)
            //{
            //    waterMark = stamp.GetOverContent(page);
            //    waterMark.AddImage(img);
            //    waterMark.SetGState(graphicsState);
            //}
            //stamp.FormFlattening = true;

            //stamp.Close();
            //pdfReader.Close();
           


            //// now delete the original file and rename the temp file to the original file
            //File.Delete(FileLocation);
            //File.Move(FileLocation.Replace( ".pdf", "[temp][file].pdf"), FileLocation);
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
            values[0] = 125;
            values[1] = 120;
            values[2] = 110;
            values[3] = 120;
            values[4] = 115;
            values[5] = 125;
            values[6] = 120;
            values[7] = 120;
            values[8] = 110;
            values[9] = 90;
            values[10] = 90;

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
                                    orderby c.ClienteNombre, c.Fecha
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
                foreach (var query_Clientel in query_DetalleTotal)
                {
                    AddCellToBody(tableLayout, query_Clientel.TipoDocumento);
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

        private void PDF(int idEmpresa, string fechaHasta)
        {
            PdfPTable tableLayout = new PdfPTable(11);
            string fileName = "ReporteEstadoCuenta_" + DateTime.Now.Millisecond.ToString() + ".pdf";

            GridView GridView2 = new GridView();
            GridView2.DataBind();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename="+ fileName);
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

        protected void grdVentasCobranzas_ItemDataBound(object sender, GridItemEventArgs e)
        {
            int colum;
            int year;
            string Cliente;
            string Vendedor;
            colum = e.Item.RowIndex;

            if(colum == 2 || colum == 16 || colum == 22 || colum == 24  || colum == 26 )
            {

                if (colum == 2)
                {
                    if (e.Item is GridDataItem)// to access a row 
                    {
                        if (acbCliente == null || acbCliente.Text.Split('-')[0] == "" || acbCliente.Text == "")
                        {
                            Cliente = null;
                        }
                        else { Cliente = acbCliente.Text.Split('-')[0]; }

                        if (acbVendedor == null || acbVendedor.Text.Split('-')[0] == "" || acbVendedor.Text == "")
                        {
                            Vendedor = null;
                        }
                        else
                        { Vendedor = acbVendedor.Text.Split('-')[0]; }

                        year = Convert.ToInt32(cboAnhos.SelectedValue);


                        GridDataItem item = (GridDataItem)e.Item;
                        DataRowView oRow = (DataRowView)(e.Item.DataItem);


                        string enero = oRow["enero"].ToString();
                        HyperLink link1 = (HyperLink)item["enero"].Controls[0];
                        link1.Text = enero;
                        link1.NavigateUrl = "javascript:ShowCreateViewVenta('" + Cliente + "','" + Vendedor + "','" + year + "','1');";
                        link1.ForeColor = System.Drawing.Color.Blue;

                        string febrero = oRow["febrero"].ToString();
                        HyperLink link2 = (HyperLink)item["febrero"].Controls[0];
                        link2.Text = febrero;
                        link2.NavigateUrl = "javascript:ShowCreateViewVenta('" + Cliente + "','" + Vendedor + "','" + year + "','2');";
                        link2.ForeColor = System.Drawing.Color.Blue;

                        string marzo = oRow["marzo"].ToString();
                        HyperLink link3 = (HyperLink)item["marzo"].Controls[0];
                        link3.Text = marzo;
                        link3.NavigateUrl = "javascript:ShowCreateViewVenta('" + Cliente + "','" + Vendedor + "','" + year + "','3');";
                        link3.ForeColor = System.Drawing.Color.Blue;

                        string abril = oRow["abril"].ToString();
                        HyperLink link4 = (HyperLink)item["abril"].Controls[0];
                        link4.Text = abril;
                        link4.NavigateUrl = "javascript:ShowCreateViewVenta('" + Cliente + "','" + Vendedor + "','" + year + "','4');";
                        link4.ForeColor = System.Drawing.Color.Blue;

                        string mayo = oRow["mayo"].ToString();
                        HyperLink link5 = (HyperLink)item["mayo"].Controls[0];
                        link5.Text = mayo;
                        link5.NavigateUrl = "javascript:ShowCreateViewVenta('" + Cliente + "','" + Vendedor + "','" + year + "','5');";
                        link5.ForeColor = System.Drawing.Color.Blue;

                        string junio = oRow["junio"].ToString();
                        HyperLink link6 = (HyperLink)item["junio"].Controls[0];
                        link6.Text = junio;
                        link6.NavigateUrl = "javascript:ShowCreateViewVenta('" + Cliente + "','" + Vendedor + "','" + year + "','6');";
                        link6.ForeColor = System.Drawing.Color.Blue;

                        string julio = oRow["julio"].ToString();
                        HyperLink link7 = (HyperLink)item["julio"].Controls[0];
                        link7.Text = julio;
                        link7.NavigateUrl = "javascript:ShowCreateViewVenta('" + Cliente + "','" + Vendedor + "','" + year + "','7');";
                        link7.ForeColor = System.Drawing.Color.Blue;

                        string agosto = oRow["agosto"].ToString();
                        HyperLink link8 = (HyperLink)item["agosto"].Controls[0];
                        link8.Text = agosto;
                        link8.NavigateUrl = "javascript:ShowCreateViewVenta('" + Cliente + "','" + Vendedor + "','" + year + "','8');";
                        link8.ForeColor = System.Drawing.Color.Blue;

                        string septiembre = oRow["septiembre"].ToString();
                        HyperLink link9 = (HyperLink)item["septiembre"].Controls[0];
                        link9.Text = septiembre;
                        link9.NavigateUrl = "javascript:ShowCreateViewVenta('" + Cliente + "','" + Vendedor + "','" + year + "','9');";
                        link9.ForeColor = System.Drawing.Color.Blue;

                        string octubre = oRow["octubre"].ToString();
                        HyperLink link10 = (HyperLink)item["octubre"].Controls[0];
                        link10.Text = octubre;
                        link10.NavigateUrl = "javascript:ShowCreateViewVenta('" + Cliente + "','" + Vendedor + "','" + year + "','10');";
                        link10.ForeColor = System.Drawing.Color.Blue;


                        string noviembre = oRow["noviembre"].ToString();
                        HyperLink link11 = (HyperLink)item["noviembre"].Controls[0];
                        link11.Text = noviembre;
                        link11.NavigateUrl = "javascript:ShowCreateViewVenta('" + Cliente + "','" + Vendedor + "','" + year + "','11');";
                        link11.ForeColor = System.Drawing.Color.Blue;

                        string diciembre = oRow["diciembre"].ToString();
                        HyperLink link12 = (HyperLink)item["diciembre"].Controls[0];
                        link12.Text = diciembre;
                        link12.NavigateUrl = "javascript:ShowCreateViewVenta('" + Cliente + "','" + Vendedor + "','" + year + "','12');";
                        link12.ForeColor = System.Drawing.Color.Blue;

                    }
                }


                if (colum == 16)
                {
                    if (e.Item is GridDataItem)// to access a row 
                    {

                        if (acbCliente == null || acbCliente.Text.Split('-')[0] == "" || acbCliente.Text == "")
                        {
                            Cliente = null;
                        }
                        else { Cliente = acbCliente.Text.Split('-')[0]; }

                        if (acbVendedor == null || acbVendedor.Text.Split('-')[0] == "" || acbVendedor.Text == "")
                        {
                            Vendedor = null;
                        }
                        else
                        { Vendedor = acbVendedor.Text.Split('-')[0]; }

                        year = Convert.ToInt32(cboAnhos.SelectedValue);


                        GridDataItem item = (GridDataItem)e.Item;
                        DataRowView oRow = (DataRowView)(e.Item.DataItem);


                        string enero = oRow["enero"].ToString();
                        HyperLink link1 = (HyperLink)item["enero"].Controls[0];
                        link1.Text = enero;
                        link1.NavigateUrl = "javascript:ShowCreateView('" + Cliente + "','" + Vendedor + "','" + year + "','1');";
                        link1.ForeColor = System.Drawing.Color.Blue;

                        string febrero = oRow["febrero"].ToString();
                        HyperLink link2 = (HyperLink)item["febrero"].Controls[0];
                        link2.Text = febrero;
                        link2.NavigateUrl = "javascript:ShowCreateView('" + Cliente + "','" + Vendedor + "','" + year + "','2');";
                        link2.ForeColor = System.Drawing.Color.Blue;

                        string marzo = oRow["marzo"].ToString();
                        HyperLink link3 = (HyperLink)item["marzo"].Controls[0];
                        link3.Text = marzo;
                        link3.NavigateUrl = "javascript:ShowCreateView('" + Cliente + "','" + Vendedor + "','" + year + "','3');";
                        link3.ForeColor = System.Drawing.Color.Blue;

                        string abril = oRow["abril"].ToString();
                        HyperLink link4 = (HyperLink)item["abril"].Controls[0];
                        link4.Text = abril;
                        link4.NavigateUrl = "javascript:ShowCreateView('" + Cliente + "','" + Vendedor + "','" + year + "','4');";
                        link4.ForeColor = System.Drawing.Color.Blue;

                        string mayo = oRow["mayo"].ToString();
                        HyperLink link5 = (HyperLink)item["mayo"].Controls[0];
                        link5.Text = mayo;
                        link5.NavigateUrl = "javascript:ShowCreateView('" + Cliente + "','" + Vendedor + "','" + year + "','5');";
                        link5.ForeColor = System.Drawing.Color.Blue;

                        string junio = oRow["junio"].ToString();
                        HyperLink link6 = (HyperLink)item["junio"].Controls[0];
                        link6.Text = junio;
                        link6.NavigateUrl = "javascript:ShowCreateView('" + Cliente + "','" + Vendedor + "','" + year + "','6');";
                        link6.ForeColor = System.Drawing.Color.Blue;

                        string julio = oRow["julio"].ToString();
                        HyperLink link7 = (HyperLink)item["julio"].Controls[0];
                        link7.Text = julio;
                        link7.NavigateUrl = "javascript:ShowCreateView('" + Cliente + "','" + Vendedor + "','" + year + "','7');";
                        link7.ForeColor = System.Drawing.Color.Blue;

                        string agosto = oRow["agosto"].ToString();
                        HyperLink link8 = (HyperLink)item["agosto"].Controls[0];
                        link8.Text = agosto;
                        link8.NavigateUrl = "javascript:ShowCreateView('" + Cliente + "','" + Vendedor + "','" + year + "','8');";
                        link8.ForeColor = System.Drawing.Color.Blue;

                        string septiembre = oRow["septiembre"].ToString();
                        HyperLink link9 = (HyperLink)item["septiembre"].Controls[0];
                        link9.Text = septiembre;
                        link9.NavigateUrl = "javascript:ShowCreateView('" + Cliente + "','" + Vendedor + "','" + year + "','9');";
                        link9.ForeColor = System.Drawing.Color.Blue;

                        string octubre = oRow["octubre"].ToString();
                        HyperLink link10 = (HyperLink)item["octubre"].Controls[0];
                        link10.Text = octubre;
                        link10.NavigateUrl = "javascript:ShowCreateView('" + Cliente + "','" + Vendedor + "','" + year + "','10');";
                        link10.ForeColor = System.Drawing.Color.Blue;


                        string noviembre = oRow["noviembre"].ToString();
                        HyperLink link11 = (HyperLink)item["noviembre"].Controls[0];
                        link11.Text = noviembre;
                        link11.NavigateUrl = "javascript:ShowCreateView('" + Cliente + "','" + Vendedor + "','" + year + "','11');";
                        link11.ForeColor = System.Drawing.Color.Blue;

                        string diciembre = oRow["diciembre"].ToString();
                        HyperLink link12 = (HyperLink)item["diciembre"].Controls[0];
                        link12.Text = diciembre;
                        link12.NavigateUrl = "javascript:ShowCreateView('" + Cliente + "','" + Vendedor + "','" + year + "','12');";
                        link12.ForeColor = System.Drawing.Color.Blue;


                    }
                }
                if (colum == 22)
                {
                    if (e.Item is GridDataItem)// to access a row 
                    {
                        if (acbCliente == null || acbCliente.Text.Split('-')[0] == "" || acbCliente.Text == "")
                        {
                            Cliente = null;
                        }
                        else { Cliente = acbCliente.Text.Split('-')[0]; }

                        if (acbVendedor == null || acbVendedor.Text.Split('-')[0] == "" || acbVendedor.Text == "")
                        {
                            Vendedor = null;
                        }
                        else
                        { Vendedor = acbVendedor.Text.Split('-')[0]; }

                        year = Convert.ToInt32(cboAnhos.SelectedValue);

                        GridDataItem item = (GridDataItem)e.Item;
                        DataRowView oRow = (DataRowView)(e.Item.DataItem);


                        string enero = oRow["enero"].ToString();
                        HyperLink link1 = (HyperLink)item["enero"].Controls[0];
                        link1.Text = enero;
                        link1.NavigateUrl = "javascript:ShowCreateViewVencido('" + Cliente + "','" + Vendedor + "','" + year + "','1');";
                        link1.ForeColor = System.Drawing.Color.Blue;

                        string febrero = oRow["febrero"].ToString();
                        HyperLink link2 = (HyperLink)item["febrero"].Controls[0];
                        link2.Text = febrero;
                        link2.NavigateUrl = "javascript:ShowCreateViewVencido('" + Cliente + "','" + Vendedor + "','" + year + "','2');";
                        link2.ForeColor = System.Drawing.Color.Blue;

                        string marzo = oRow["marzo"].ToString();
                        HyperLink link3 = (HyperLink)item["marzo"].Controls[0];
                        link3.Text = marzo;
                        link3.NavigateUrl = "javascript:ShowCreateViewVencido('" + Cliente + "','" + Vendedor + "','" + year + "','3');";
                        link3.ForeColor = System.Drawing.Color.Blue;

                        string abril = oRow["abril"].ToString();
                        HyperLink link4 = (HyperLink)item["abril"].Controls[0];
                        link4.Text = abril;
                        link4.NavigateUrl = "javascript:ShowCreateViewVencido('" + Cliente + "','" + Vendedor + "','" + year + "','4');";
                        link4.ForeColor = System.Drawing.Color.Blue;

                        string mayo = oRow["mayo"].ToString();
                        HyperLink link5 = (HyperLink)item["mayo"].Controls[0];
                        link5.Text = mayo;
                        link5.NavigateUrl = "javascript:ShowCreateViewVencido('" + Cliente + "','" + Vendedor + "','" + year + "','5');";
                        link5.ForeColor = System.Drawing.Color.Blue;

                        string junio = oRow["junio"].ToString();
                        HyperLink link6 = (HyperLink)item["junio"].Controls[0];
                        link6.Text = junio;
                        link6.NavigateUrl = "javascript:ShowCreateViewVencido('" + Cliente + "','" + Vendedor + "','" + year + "','6');";
                        link6.ForeColor = System.Drawing.Color.Blue;

                        string julio = oRow["julio"].ToString();
                        HyperLink link7 = (HyperLink)item["julio"].Controls[0];
                        link7.Text = julio;
                        link7.NavigateUrl = "javascript:ShowCreateViewVencido('" + Cliente + "','" + Vendedor + "','" + year + "','7');";
                        link7.ForeColor = System.Drawing.Color.Blue;

                        string agosto = oRow["agosto"].ToString();
                        HyperLink link8 = (HyperLink)item["agosto"].Controls[0];
                        link8.Text = agosto;
                        link8.NavigateUrl = "javascript:ShowCreateViewVencido('" + Cliente + "','" + Vendedor + "','" + year + "','8');";
                        link8.ForeColor = System.Drawing.Color.Blue;

                        string septiembre = oRow["septiembre"].ToString();
                        HyperLink link9 = (HyperLink)item["septiembre"].Controls[0];
                        link9.Text = septiembre;
                        link9.NavigateUrl = "javascript:ShowCreateViewVencido('" + Cliente + "','" + Vendedor + "','" + year + "','9');";
                        link9.ForeColor = System.Drawing.Color.Blue;

                        string octubre = oRow["octubre"].ToString();
                        HyperLink link10 = (HyperLink)item["octubre"].Controls[0];
                        link10.Text = octubre;
                        link10.NavigateUrl = "javascript:ShowCreateViewVencido('" + Cliente + "','" + Vendedor + "','" + year + "','10');";
                        link10.ForeColor = System.Drawing.Color.Blue;


                        string noviembre = oRow["noviembre"].ToString();
                        HyperLink link11 = (HyperLink)item["noviembre"].Controls[0];
                        link11.Text = noviembre;
                        link11.NavigateUrl = "javascript:ShowCreateViewVencido('" + Cliente + "','" + Vendedor + "','" + year + "','11');";
                        link11.ForeColor = System.Drawing.Color.Blue;

                        string diciembre = oRow["diciembre"].ToString();
                        HyperLink link12 = (HyperLink)item["diciembre"].Controls[0];
                        link12.Text = diciembre;
                        link12.NavigateUrl = "javascript:ShowCreateViewVencido('" + Cliente + "','" + Vendedor + "','" + year + "','12');";
                        link12.ForeColor = System.Drawing.Color.Blue;

                    }
                }

                if (colum == 24)
                {
                    if (e.Item is GridDataItem)// to access a row 
                    {
                        if (acbCliente == null || acbCliente.Text.Split('-')[0] == "" || acbCliente.Text == "")
                        {
                            Cliente = null;
                        }
                        else { Cliente = acbCliente.Text.Split('-')[0]; }

                        if (acbVendedor == null || acbVendedor.Text.Split('-')[0] == "" || acbVendedor.Text == "")
                        {
                            Vendedor = null;
                        }
                        else
                        { Vendedor = acbVendedor.Text.Split('-')[0]; }

                        year = Convert.ToInt32(cboAnhos.SelectedValue);

                        GridDataItem item = (GridDataItem)e.Item;
                        DataRowView oRow = (DataRowView)(e.Item.DataItem);


                        string enero = oRow["enero"].ToString();
                        HyperLink link1 = (HyperLink)item["enero"].Controls[0];
                        link1.Text = enero;
                        link1.NavigateUrl = "javascript:ShowCreateViewVencidoAfiliado('" + Cliente + "','" + Vendedor + "','" + year + "','1');";
                        link1.ForeColor = System.Drawing.Color.Blue;

                        string febrero = oRow["febrero"].ToString();
                        HyperLink link2 = (HyperLink)item["febrero"].Controls[0];
                        link2.Text = febrero;
                        link2.NavigateUrl = "javascript:ShowCreateViewVencidoAfiliado('" + Cliente + "','" + Vendedor + "','" + year + "','2');";
                        link2.ForeColor = System.Drawing.Color.Blue;

                        string marzo = oRow["marzo"].ToString();
                        HyperLink link3 = (HyperLink)item["marzo"].Controls[0];
                        link3.Text = marzo;
                        link3.NavigateUrl = "javascript:ShowCreateViewVencidoAfiliado('" + Cliente + "','" + Vendedor + "','" + year + "','3');";
                        link3.ForeColor = System.Drawing.Color.Blue;

                        string abril = oRow["abril"].ToString();
                        HyperLink link4 = (HyperLink)item["abril"].Controls[0];
                        link4.Text = abril;
                        link4.NavigateUrl = "javascript:ShowCreateViewVencidoAfiliado('" + Cliente + "','" + Vendedor + "','" + year + "','4');";
                        link4.ForeColor = System.Drawing.Color.Blue;

                        string mayo = oRow["mayo"].ToString();
                        HyperLink link5 = (HyperLink)item["mayo"].Controls[0];
                        link5.Text = mayo;
                        link5.NavigateUrl = "javascript:ShowCreateViewVencidoAfiliado('" + Cliente + "','" + Vendedor + "','" + year + "','5');";
                        link5.ForeColor = System.Drawing.Color.Blue;

                        string junio = oRow["junio"].ToString();
                        HyperLink link6 = (HyperLink)item["junio"].Controls[0];
                        link6.Text = junio;
                        link6.NavigateUrl = "javascript:ShowCreateViewVencidoAfiliado('" + Cliente + "','" + Vendedor + "','" + year + "','6');";
                        link6.ForeColor = System.Drawing.Color.Blue;

                        string julio = oRow["julio"].ToString();
                        HyperLink link7 = (HyperLink)item["julio"].Controls[0];
                        link7.Text = julio;
                        link7.NavigateUrl = "javascript:ShowCreateViewVencidoAfiliado('" + Cliente + "','" + Vendedor + "','" + year + "','7');";
                        link7.ForeColor = System.Drawing.Color.Blue;

                        string agosto = oRow["agosto"].ToString();
                        HyperLink link8 = (HyperLink)item["agosto"].Controls[0];
                        link8.Text = agosto;
                        link8.NavigateUrl = "javascript:ShowCreateViewVencidoAfiliado('" + Cliente + "','" + Vendedor + "','" + year + "','8');";
                        link8.ForeColor = System.Drawing.Color.Blue;

                        string septiembre = oRow["septiembre"].ToString();
                        HyperLink link9 = (HyperLink)item["septiembre"].Controls[0];
                        link9.Text = septiembre;
                        link9.NavigateUrl = "javascript:ShowCreateViewVencidoAfiliado('" + Cliente + "','" + Vendedor + "','" + year + "','9');";
                        link9.ForeColor = System.Drawing.Color.Blue;

                        string octubre = oRow["octubre"].ToString();
                        HyperLink link10 = (HyperLink)item["octubre"].Controls[0];
                        link10.Text = octubre;
                        link10.NavigateUrl = "javascript:ShowCreateViewVencidoAfiliado('" + Cliente + "','" + Vendedor + "','" + year + "','10');";
                        link10.ForeColor = System.Drawing.Color.Blue;


                        string noviembre = oRow["noviembre"].ToString();
                        HyperLink link11 = (HyperLink)item["noviembre"].Controls[0];
                        link11.Text = noviembre;
                        link11.NavigateUrl = "javascript:ShowCreateViewVencidoAfiliado('" + Cliente + "','" + Vendedor + "','" + year + "','11');";
                        link11.ForeColor = System.Drawing.Color.Blue;

                        string diciembre = oRow["diciembre"].ToString();
                        HyperLink link12 = (HyperLink)item["diciembre"].Controls[0];
                        link12.Text = diciembre;
                        link12.NavigateUrl = "javascript:ShowCreateViewVencidoAfiliado('" + Cliente + "','" + Vendedor + "','" + year + "','12');";
                        link12.ForeColor = System.Drawing.Color.Blue;

                    }
                }
                    if (colum == 26)
                    {
                        if (e.Item is GridDataItem)// to access a row 
                        {
                            if (acbCliente == null || acbCliente.Text.Split('-')[0] == "" || acbCliente.Text == "")
                            {
                                Cliente = null;
                            }
                            else { Cliente = acbCliente.Text.Split('-')[0]; }

                            if (acbVendedor == null || acbVendedor.Text.Split('-')[0] == "" || acbVendedor.Text == "")
                            {
                                Vendedor = null;
                            }
                            else
                            { Vendedor = acbVendedor.Text.Split('-')[0]; }

                            year = Convert.ToInt32(cboAnhos.SelectedValue);

                            GridDataItem item = (GridDataItem)e.Item;
                            DataRowView oRow = (DataRowView)(e.Item.DataItem);


                            string enero = oRow["enero"].ToString();
                            HyperLink link1 = (HyperLink)item["enero"].Controls[0];
                            link1.Text = enero;
                            link1.NavigateUrl = "javascript:ShowCreateViewVencidoLegal('" + Cliente + "','" + Vendedor + "','" + year + "','1');";
                            link1.ForeColor = System.Drawing.Color.Blue;

                            string febrero = oRow["febrero"].ToString();
                            HyperLink link2 = (HyperLink)item["febrero"].Controls[0];
                            link2.Text = febrero;
                            link2.NavigateUrl = "javascript:ShowCreateViewVencidoLegal('" + Cliente + "','" + Vendedor + "','" + year + "','2');";
                            link2.ForeColor = System.Drawing.Color.Blue;

                            string marzo = oRow["marzo"].ToString();
                            HyperLink link3 = (HyperLink)item["marzo"].Controls[0];
                            link3.Text = marzo;
                            link3.NavigateUrl = "javascript:ShowCreateViewVencidoLegal('" + Cliente + "','" + Vendedor + "','" + year + "','3');";
                            link3.ForeColor = System.Drawing.Color.Blue;

                            string abril = oRow["abril"].ToString();
                            HyperLink link4 = (HyperLink)item["abril"].Controls[0];
                            link4.Text = abril;
                            link4.NavigateUrl = "javascript:ShowCreateViewVencidoLegal('" + Cliente + "','" + Vendedor + "','" + year + "','4');";
                            link4.ForeColor = System.Drawing.Color.Blue;

                            string mayo = oRow["mayo"].ToString();
                            HyperLink link5 = (HyperLink)item["mayo"].Controls[0];
                            link5.Text = mayo;
                            link5.NavigateUrl = "javascript:ShowCreateViewVencidoLegal('" + Cliente + "','" + Vendedor + "','" + year + "','5');";
                            link5.ForeColor = System.Drawing.Color.Blue;

                            string junio = oRow["junio"].ToString();
                            HyperLink link6 = (HyperLink)item["junio"].Controls[0];
                            link6.Text = junio;
                            link6.NavigateUrl = "javascript:ShowCreateViewVencidoLegal('" + Cliente + "','" + Vendedor + "','" + year + "','6');";
                            link6.ForeColor = System.Drawing.Color.Blue;

                            string julio = oRow["julio"].ToString();
                            HyperLink link7 = (HyperLink)item["julio"].Controls[0];
                            link7.Text = julio;
                            link7.NavigateUrl = "javascript:ShowCreateViewVencidoLegal('" + Cliente + "','" + Vendedor + "','" + year + "','7');";
                            link7.ForeColor = System.Drawing.Color.Blue;

                            string agosto = oRow["agosto"].ToString();
                            HyperLink link8 = (HyperLink)item["agosto"].Controls[0];
                            link8.Text = agosto;
                            link8.NavigateUrl = "javascript:ShowCreateViewVencidoLegal('" + Cliente + "','" + Vendedor + "','" + year + "','8');";
                            link8.ForeColor = System.Drawing.Color.Blue;

                            string septiembre = oRow["septiembre"].ToString();
                            HyperLink link9 = (HyperLink)item["septiembre"].Controls[0];
                            link9.Text = septiembre;
                            link9.NavigateUrl = "javascript:ShowCreateViewVencidoLegal('" + Cliente + "','" + Vendedor + "','" + year + "','9');";
                            link9.ForeColor = System.Drawing.Color.Blue;

                            string octubre = oRow["octubre"].ToString();
                            HyperLink link10 = (HyperLink)item["octubre"].Controls[0];
                            link10.Text = octubre;
                            link10.NavigateUrl = "javascript:ShowCreateViewVencidoLegal('" + Cliente + "','" + Vendedor + "','" + year + "','10');";
                            link10.ForeColor = System.Drawing.Color.Blue;


                            string noviembre = oRow["noviembre"].ToString();
                            HyperLink link11 = (HyperLink)item["noviembre"].Controls[0];
                            link11.Text = noviembre;
                            link11.NavigateUrl = "javascript:ShowCreateViewVencidoLegal('" + Cliente + "','" + Vendedor + "','" + year + "','11');";
                            link11.ForeColor = System.Drawing.Color.Blue;

                            string diciembre = oRow["diciembre"].ToString();
                            HyperLink link12 = (HyperLink)item["diciembre"].Controls[0];
                            link12.Text = diciembre;
                            link12.NavigateUrl = "javascript:ShowCreateViewVencidoLegal('" + Cliente + "','" + Vendedor + "','" + year + "','12');";
                            link12.ForeColor = System.Drawing.Color.Blue;

                        }
                    }
                

            }
            else
            {
                if (e.Item is GridDataItem)// to access a row 
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    DataRowView oRow = (DataRowView)(e.Item.DataItem);

                    string enero = oRow["enero"].ToString();
                    HyperLink link1 = (HyperLink)item["enero"].Controls[0];
                    link1.Text = enero;
                    link1.ForeColor = System.Drawing.Color.Black;

                    string febrero = oRow["febrero"].ToString();
                    HyperLink link2 = (HyperLink)item["febrero"].Controls[0];
                    link2.Text = febrero;
                    link2.ForeColor = System.Drawing.Color.Black;

                    string marzo = oRow["marzo"].ToString();
                    HyperLink link3 = (HyperLink)item["marzo"].Controls[0];
                    link3.Text = marzo;
                    link3.ForeColor = System.Drawing.Color.Black;

                    string abril = oRow["abril"].ToString();
                    HyperLink link4 = (HyperLink)item["abril"].Controls[0];
                    link4.Text = abril;
                    link4.ForeColor = System.Drawing.Color.Black;

                    string mayo = oRow["mayo"].ToString();
                    HyperLink link5 = (HyperLink)item["mayo"].Controls[0];
                    link5.Text = mayo;
                    link5.ForeColor = System.Drawing.Color.Black;

                    string junio = oRow["junio"].ToString();
                    HyperLink link6 = (HyperLink)item["junio"].Controls[0];
                    link6.Text = junio;
                    link6.ForeColor = System.Drawing.Color.Black;

                    string julio = oRow["julio"].ToString();
                    HyperLink link7 = (HyperLink)item["julio"].Controls[0];
                    link7.Text = julio;
                    link7.ForeColor = System.Drawing.Color.Black;

                    string agosto = oRow["agosto"].ToString();
                    HyperLink link8 = (HyperLink)item["agosto"].Controls[0];
                    link8.Text = agosto;
                    link8.ForeColor = System.Drawing.Color.Black;

                    string septiembre = oRow["septiembre"].ToString();
                    HyperLink link9 = (HyperLink)item["septiembre"].Controls[0];
                    link9.Text = septiembre;
                    link9.ForeColor = System.Drawing.Color.Black;

                    string octubre = oRow["octubre"].ToString();
                    HyperLink link10 = (HyperLink)item["octubre"].Controls[0];
                    link10.Text = octubre;
                    link10.ForeColor = System.Drawing.Color.Black;

                    string noviembre = oRow["noviembre"].ToString();
                    HyperLink link11 = (HyperLink)item["noviembre"].Controls[0];
                    link11.Text = noviembre;
                    link11.ForeColor = System.Drawing.Color.Black;

                    string diciembre = oRow["diciembre"].ToString();
                    HyperLink link12 = (HyperLink)item["diciembre"].Controls[0];
                    link12.Text = diciembre;
                    link12.ForeColor = System.Drawing.Color.Black;

                }
            }
           
        }
    }
}