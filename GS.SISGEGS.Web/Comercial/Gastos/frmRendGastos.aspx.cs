using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.Web.EgresosWCF;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.Helpers;
using Telerik.Web.UI;
using GS.SISGEGS.Web.LoginWCF;
using GS.SISGEGS.Web.AgendaWCF;
using System.IO;
using GS.SISGEGS.Web.EmpresaWCF;
using ICSharpCode.SharpZipLib.Zip;
using Excel = Microsoft.Office.Interop.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.Services;


namespace GS.SISGEGS.Web.Comercial.Gastos
{
    public partial class frmRendGastos : System.Web.UI.Page
    {
        private void EgresosVarios_Listar(string idAgenda, DateTime fechaInicio, DateTime fechaFinal, string IdProveedor)
        {
            EgresosWCFClient objEgresosWCF;
            List<gsEgresosVariosInt_ListarCajaChicaResult> lstEgresosVarios;
            bool verPlanillas = false;
            try
            {
                objEgresosWCF = new EgresosWCFClient();

                if (chkVerPlanillas.Checked) verPlanillas = true;

                lstEgresosVarios = objEgresosWCF.EgresosVariosInt_ListarCajaChica(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda, fechaInicio, fechaFinal, verPlanillas, IdProveedor).ToList();

                grdEgresosVarios.DataSource = lstEgresosVarios;
                grdEgresosVarios.DataBind();

                ViewState["lstEgresosVarios"] = JsonHelper.JsonSerializer(lstEgresosVarios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");
           

            if (!ConnectionHelpers.CheckForInternetConnection())
                throw new ArgumentException("Revisar su conexión a internet.");

            try
            {
                if (!Page.IsPostBack)
                {
                    LoginWCFClient objLoginWCF = new LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idUsuario);

                    dpFechaInicio.SelectedDate = DateTime.Now.AddMonths(-1);
                    dpFechaFinal.SelectedDate = DateTime.Now;


                    EgresosVarios_Listar(((Usuario_LoginResult)Session["Usuario"]).nroDocumento, dpFechaInicio.SelectedDate.Value, dpFechaFinal.SelectedDate.Value, null);

                    lblMensaje.Text = "Se encontraron " + grdEgresosVarios.Items.Count.ToString() + " registros.";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string Cliente = null;

            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if ((dpFechaFinal.SelectedDate.Value - dpFechaInicio.SelectedDate.Value).TotalDays < 0)
                    throw new ArgumentException("ERROR: Las fecha de inicio debe ser menor a la fecha final.");

                if (acbCliente == null || acbCliente.Text.Split('-')[0] == "" || acbCliente.Text == "")
                {
                    Cliente = null;
                }
                else { Cliente = acbCliente.Text.Split('-')[0]; }

                EgresosVarios_Listar(((Usuario_LoginResult)Session["Usuario"]).nroDocumento, dpFechaInicio.SelectedDate.Value, dpFechaFinal.SelectedDate.Value, Cliente);

                lblMensaje.Text = "Se encontraron " + grdEgresosVarios.Items.Count.ToString() + " registros.";
                lblMensaje.CssClass = "mensajeExito";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void ramEgresosVarios_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Argument == "Rebind")
                {
                    grdEgresosVarios.MasterTableView.SortExpressions.Clear();
                    grdEgresosVarios.MasterTableView.GroupByExpressions.Clear();
                    EgresosVarios_Listar(((Usuario_LoginResult)Session["Usuario"]).nroDocumento, dpFechaInicio.SelectedDate.Value, dpFechaFinal.SelectedDate.Value, null);
                    grdEgresosVarios.DataBind();

                    lblMensaje.Text = "Se agregó el gasto al sistema.";
                    lblMensaje.CssClass = "mensajeExito";
                }

                if (e.Argument.Split('(')[0].Trim() == "RebindAndNavigate")
                {
                    
                }

                if (e.Argument.Split(',')[0] == "ChangePageSize")
                {
                    grdEgresosVarios.Height = new Unit(e.Argument.Split(',')[1] + "px");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                Response.Redirect("~/Comercial/Gastos/frmRendGastosMng.aspx?idOperacion=0");
                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowForm(0);", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdEgresosVarios_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    //if (item["Ok1"].Text == "False")
                    //{
                    //    ((System.Web.UI.WebControls.Image)e.Item.FindControl("imgEstado1")).ImageUrl = "~/Images/Icons/sign-error-16.png";
                    //    ((System.Web.UI.WebControls.Image)e.Item.FindControl("imgEstado1")).ToolTip = "Por aprobar";
                    //}
                    //else
                    //{
                    //    ((System.Web.UI.WebControls.Image)e.Item.FindControl("imgEstado1")).ImageUrl = "~/Images/Icons/sign-check-16.png";
                    //}

                    //if (item["Ok0"].Text == "False")
                    //{
                    //    ((System.Web.UI.WebControls.Image)e.Item.FindControl("imgEstado0")).ImageUrl = "~/Images/Icons/sign-error-16.png";
                    //    ((System.Web.UI.WebControls.Image)e.Item.FindControl("imgEstado0")).ToolTip = "Por aprobar";
                    //}
                    //else
                    //{
                    //    ((System.Web.UI.WebControls.Image)e.Item.FindControl("imgEstado0")).ImageUrl = "~/Images/Icons/sign-check-16.png";
                    //    item["Elim"].Visible = false;
                    //}
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdEgresosVarios_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            EgresosWCFClient objEgresosWCF = new EgresosWCFClient();

            

            List<gsEgresosVariosInt_ListarCajaChicaResult> lstEgresosVarios;
            try
            {
                int Id = Convert.ToInt32(((GridDataItem)e.Item).GetDataKeyValue("Id"));

                var listFlujo = objEgresosWCF.EgresoCajaFlujo(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                        ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, Id);

                if (listFlujo.ToList().Count == 0)
                {
                    objEgresosWCF.EgresosVariosInt_Eliminar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, Id);
                    lstEgresosVarios = JsonHelper.JsonDeserialize<List<gsEgresosVariosInt_ListarCajaChicaResult>>((string)ViewState["lstEgresosVarios"]);
                    lstEgresosVarios.Remove(lstEgresosVarios.Find(x => x.Id == Id));
                    ViewState["lstEgresosVarios"] = JsonHelper.JsonSerializer(lstEgresosVarios);
                    grdEgresosVarios.DataSource = lstEgresosVarios;
                    grdEgresosVarios.DataBind();
                }
                else {
                    lstEgresosVarios = JsonHelper.JsonDeserialize<List<gsEgresosVariosInt_ListarCajaChicaResult>>((string)ViewState["lstEgresosVarios"]);
                    grdEgresosVarios.DataSource = lstEgresosVarios;
                    grdEgresosVarios.DataBind();
                    lblMensaje.Text = "INFO: " + "No se puede eliminar el registro, este ya se encuentra en el flujo de aprobaciones.";
                }

                
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdEgresosVarios_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if (e.CommandName == "Editar")
                {
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowInsertForm(" + e.CommandArgument + ");", true);

                    var consulta = 0;
                    if (chkVerPlanillas.Checked) consulta = 1;
                    Response.Redirect("~/Comercial/Gastos/frmRendGastosMng.aspx?idOperacion="+ e.CommandArgument + "&consulta="+consulta);
                }

                if (e.CommandName == "Resumen")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowResumen(" + e.CommandArgument + ");", true);
                }
                if (e.CommandName == "DescargarPDF")
                {
                    string Op = e.CommandArgument.ToString();
                    int idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                    ShowPdf(CreatePDF(idEmpresa, DateTime.Now.ToShortDateString(), Op), Op);
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdEgresosVarios_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                grdEgresosVarios.DataSource = JsonHelper.JsonDeserialize<List<gsEgresosVariosInt_ListarCajaChicaResult>>((string)ViewState["lstEgresosVarios"]);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }


        protected void btnPDFDetalle_Click(object sender, EventArgs e)
        {
            //List<gsReporte_DocumentosPendientesResult> lst = JsonHelper.JsonDeserialize<List<gsReporte_DocumentosPendientesResult>>((string)ViewState["lstEstadoCuenta"]);
            try
            {
                string fecha = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                //string fileName = "Planilla_OP" + Op + "_F" + fecha + ".pdf";
                string FileName = "MasivoPlantillas" + "F" + fecha + ".zip";
                DownloadPDF(FileName);
                //DownloadXLS(FileName);
            }
            catch
            {
                lblMensaje.Text = "Error";
            }
        }




        private void DownloadPDF(string Name)
        {
            try
            {
                byte[] buffer = new byte[4096];

                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ClearContent();
                System.Web.HttpContext.Current.Response.ClearHeaders();

                System.Web.HttpContext.Current.Response.ContentType = "application/zip";
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Name + "\"");

                ZipOutputStream ZipOutputStream = new ZipOutputStream(Response.OutputStream);
                ZipOutputStream.SetLevel(3);

                foreach (GridItem rowitem in grdEgresosVarios.MasterTableView.Items)
                {
                    GridDataItem dataitem = (GridDataItem)rowitem;
                    TableCell cell = dataitem["CheckColumn"];
                    CheckBox checkBox = (CheckBox)cell.Controls[0].FindControl("Check");
                    string Op;

                    if (checkBox.Checked)
                    {
                        Op = dataitem["Op"].Text;

                        string fecha = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        string fileName = "Planilla_OP" + Op + "_F" + fecha + ".pdf";
                        int idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa; 

                        Stream fs = CreatePDFFromMemoryStream(idEmpresa, DateTime.Now.ToShortDateString(), Op);
                        ZipEntry zipEntry = new ZipEntry(ZipEntry.CleanName(fileName));
                        zipEntry.Size = fs.Length;
                        ZipOutputStream.PutNextEntry(zipEntry);
                        int count = fs.Read(buffer, 0, buffer.Length);
                        while (count > 0)
                        {
                            ZipOutputStream.Write(buffer, 0, count);
                            count = fs.Read(buffer, 0, buffer.Length);
                            if (!Response.IsClientConnected)
                            {
                                break;
                            }
                            Response.Flush();
                        }
                        fs.Close();
                    }
                }

                ZipOutputStream.Close();
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }

        }

        private void DownloadXLS(string Name)
        {
            try
            {
                byte[] buffer = new byte[4096];

                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ClearContent();
                System.Web.HttpContext.Current.Response.ClearHeaders();

                System.Web.HttpContext.Current.Response.ContentType = "application/zip";
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Name + "\"");

                ZipOutputStream ZipOutputStream = new ZipOutputStream(Response.OutputStream);
                ZipOutputStream.SetLevel(3);

                foreach (GridItem rowitem in grdEgresosVarios.MasterTableView.Items)
                {
                    GridDataItem dataitem = (GridDataItem)rowitem;
                    TableCell cell = dataitem["CheckColumn"];
                    CheckBox checkBox = (CheckBox)cell.Controls[0].FindControl("Check");
                    string Op;

                    if (checkBox.Checked)
                    {
                        Op = dataitem["Op"].Text;
                        //CreatePDFFromMemoryStream(1, DateTime.Now.ToShortDateString(), Op);
                        string fileName = "Planilla_OP" + Op + ".xls";

                        Stream fs = CreateXLSFromMemoryStream(1, DateTime.Now.ToShortDateString(), Op);

                        ZipEntry zipEntry = new ZipEntry(ZipEntry.CleanName(fileName));
                        zipEntry.Size = fs.Length;
                        ZipOutputStream.PutNextEntry(zipEntry);
                        int count = fs.Read(buffer, 0, buffer.Length);
                        while (count > 0)
                        {
                            ZipOutputStream.Write(buffer, 0, count);
                            count = fs.Read(buffer, 0, buffer.Length);
                            if (!Response.IsClientConnected)
                            {
                                break;
                            }
                            Response.Flush();
                        }
                        fs.Close();
                    }
                }

                ZipOutputStream.Close();
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }

        }

        public Stream CreatePDFFromMemoryStream(int idEmpresa, string fechaHasta, string Op)
        {
            Stream file;
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                PdfPTable tableLayout = new PdfPTable(11);

                Document doc = new Document();
                doc = new Document(PageSize.LETTER, 20, 20, 20, 20);
                
                PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
                doc.Open();
                doc.Add(Add_Content_To_PDF(tableLayout, fechaHasta, idEmpresa, Op));
                writer.CloseStream = false;
                doc.Close();
                memoryStream.Position = 0;
               
            }
            catch(Exception ex)
            {
                lblMensaje.Text = ex.Message; 
            }
            file = memoryStream;
            return file; 
        }

        public Stream CreateXLSFromMemoryStream(int idEmpresa, string fechaHasta, string Op)
        {
            Stream file;
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                MemoryStream stream = new MemoryStream(ExportSampleFile(Op)); 
                memoryStream = stream ;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
            file = memoryStream;
            return file;
        }

        private void DownloadAsPDF(MemoryStream ms, string Op)
        {
            try
            {

                string fileName = "ReporteEstadoCuenta_OP" + Op + ".pdf";
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
            catch
            {
                //lblMensaje.Text = ex.Message; 
            }
        }

        private byte[] CreatePDF(int idEmpresa, string fechaHasta, string Op)
        {
            Document doc = new Document(PageSize.LETTER, 50, 50, 50, 50);

            using (MemoryStream output = new MemoryStream())
            {
                PdfPTable tableLayout = new PdfPTable(11);

                doc = new Document();
                doc = new Document(PageSize.LETTER, 20, 20, 20, 20);
                MemoryStream memoryStream = new MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(doc, output);
                doc.Open();
                doc.Add(Add_Content_To_PDF(tableLayout, fechaHasta, idEmpresa, Op));

                writer.CloseStream = false;
                doc.Close();
                memoryStream.Position = 0;

                return output.ToArray();
            }

        }

        private void ShowPdf(byte[] strS, string Op)
        {

            try
            {
                string fecha = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                string fileName = "Planilla_OP" + Op + "_F" + fecha + ".pdf";
                //if (!File.Exists(rutaArchivo)) return;

                var file = strS;
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ClearHeaders();
                System.Web.HttpContext.Current.Response.ContentType = "application/file";
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");
                System.Web.HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString(System.Globalization.CultureInfo.InvariantCulture));
                System.Web.HttpContext.Current.Response.OutputStream.Write(file, 0, file.Length);

                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                Response.BufferOutput = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest(); //
                Response.Close();

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }


        }

        private PdfPTable Add_Content_To_PDF(PdfPTable tableLayout, string fechaHasta, int idEmpresa, string Id)
        {
            string urlImagen;
            decimal OpInt = 0;
            var IdInt = Convert.ToDecimal(Id); 

            EmpresaWCFClient objEmpresaWCF = new EmpresaWCFClient();
            List<gsEgresosVariosInt_ListarCajaChicaResult> lstEgresosVarios = new List<gsEgresosVariosInt_ListarCajaChicaResult>();
            gsEgresosVariosInt_ListarCajaChicaResult EgresosVarios = new gsEgresosVariosInt_ListarCajaChicaResult(); 

            Empresa_BuscarDetalleResult objEmpresa;
            Empresa_BuscarDetalleResult[] lst = objEmpresaWCF.Empresa_BuscarDetalle(idEmpresa);
            objEmpresa = lst[0];
            urlImagen = objEmpresa.logotipo.ToString();

            float[] values = new float[11];
            values[0] = 120;
            values[1] = 100;
            values[2] = 100;
            values[3] = 100;
            values[4] = 100;
            values[5] = 100;
            values[6] = 100;
            values[7] = 100;
            values[8] = 100;
            values[9] = 100;
            values[10] = 100;

            tableLayout.SetWidths(values);        //Set the pdf headers
            tableLayout.WidthPercentage = 100;       //Set the PDF File witdh percentage

            //Add Title to the PDF file at the top

            //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath(urlImagen));
            lstEgresosVarios = JsonHelper.JsonDeserialize<List<gsEgresosVariosInt_ListarCajaChicaResult>>((string)ViewState["lstEgresosVarios"]);
            lstEgresosVarios.Find(x => x.Id == OpInt);
            EgresosVarios = lstEgresosVarios.Find(x => x.Id == OpInt);



            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/Logos/grupo.png"));

            logo.ScaleAbsolute(205, 90);
            PdfPCell imageCell = new PdfPCell(logo);
            imageCell.Colspan = 2; // either 1 if you need to insert one cell
            imageCell.Border = 0;
            imageCell.HorizontalAlignment = Element.ALIGN_LEFT;

            tableLayout = EgresosVarios_PDF(tableLayout, Convert.ToInt32(OpInt), objEmpresa.razonSocial); 

            return tableLayout;
        }

        private PdfPTable EgresosVarios_PDF(PdfPTable PDFCreator, int idOperacion, string NombreEmpresa)
        {
            PdfPTable tableLayout;
            tableLayout = PDFCreator;
            EgresosWCFClient objEgresoWCF = new EgresosWCFClient();
            bool? bloqueado = null;
            string mensajeBloqueado = null;
            gsEgresosVariosDInt_BuscarDetalleResult[] lstEgresosVarios_Detalle = null;
            gsEgresosVariosDInt_BuscarDetalleResult[] lstEgresosVarios_DetalleGasto = null;

            gsEgresosVariosInt_BuscarCabeceraResult objEgresosVarios;
            try
            {
                if (idOperacion != 0)
                {
                    objEgresosVarios = objEgresoWCF.EgresosVariosInt_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idOperacion, ref bloqueado, ref mensajeBloqueado,
                    ref lstEgresosVarios_Detalle);

                    //tableLayout.AddCell(imageCell);
                    tableLayout.AddCell(new PdfPCell(new Phrase(NombreEmpresa, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 4, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_LEFT });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 4, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableLayout.AddCell(new PdfPCell(new Phrase(DateTime.Now.ToShortDateString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 5, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 2, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableLayout.AddCell(new PdfPCell(new Phrase(DateTime.Now.ToShortTimeString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 5, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 1, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Egreso de Caja", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 11, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 11, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 11, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 11, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 11, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                    //Add Cliente
                    int cont = 0;

                    tableLayout.AddCell(new PdfPCell(new Phrase("Fecha : ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase(objEgresosVarios.Fecha.ToShortDateString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 9, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Documento : ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase(objEgresosVarios.NombreDocumento, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                    //tableLayout.AddCell(new PdfPCell(new Phrase("Egreso Nro : ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    //tableLayout.AddCell(new PdfPCell(new Phrase(objEgresosVarios.Serie + " - " + objEgresosVarios.Numero + " (OP:" + objEgresosVarios.Op + ")", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    //tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Moneda : ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                    if (objEgresosVarios.ID_Moneda == 0)
                    {
                        tableLayout.AddCell(new PdfPCell(new Phrase("Dolares", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    }
                    else
                    {
                        tableLayout.AddCell(new PdfPCell(new Phrase("Soles", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    }
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 9, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });


                    tableLayout.AddCell(new PdfPCell(new Phrase("Orden de : ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase(objEgresosVarios.AgendaNombre, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });


                    tableLayout.AddCell(new PdfPCell(new Phrase("Codigo : ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase(objEgresosVarios.ID_Agenda, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 9, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Concepto : ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase(objEgresosVarios.Concepto, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 11, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 11, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });


                    PdfPCell cellGastos = new PdfPCell(new Phrase("  Gastos", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, Border = 1, BorderColor = iTextSharp.text.BaseColor.BLACK, BackgroundColor = iTextSharp.text.BaseColor.WHITE };
                    cellGastos.Border = Rectangle.BOTTOM_BORDER; // | Rectangle.TOP_BORDER;
                    cellGastos.BorderWidthBottom = 2f;
                    cellGastos.BorderWidthTop = 0f;
                    cellGastos.PaddingBottom = 3f;
                    cellGastos.PaddingLeft = 0f;
                    cellGastos.PaddingTop = 0f;
                    tableLayout.AddCell(cellGastos);

                    PdfPCell cellImporte = new PdfPCell(new Phrase("  Importe  ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 1, BorderColor = iTextSharp.text.BaseColor.BLACK, BackgroundColor = iTextSharp.text.BaseColor.WHITE };
                    cellImporte.Border = Rectangle.BOTTOM_BORDER; // | Rectangle.TOP_BORDER;
                    cellImporte.BorderWidthBottom = 2f;
                    cellImporte.BorderWidthTop = 0f;
                    cellImporte.PaddingBottom = 3f;
                    cellImporte.PaddingLeft = 0f;
                    cellImporte.PaddingTop = 0f;
                    tableLayout.AddCell(cellImporte);

                    tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 11, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                    lstEgresosVarios_Detalle.OrderByDescending(x => x.ID_Item);

                    var ListaGasto = lstEgresosVarios_Detalle.Select(x => new { x.ID_Item, x.Item }).Distinct().ToList();


                    decimal Total = 0;
                    foreach (var lineaGasto in ListaGasto)
                    {
                        tableLayout.AddCell(new PdfPCell(new Phrase(lineaGasto.ID_Item.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                        tableLayout.AddCell(new PdfPCell(new Phrase(lineaGasto.Item.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 8, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                        decimal sumaTotal = 0;
                        foreach (gsEgresosVariosDInt_BuscarDetalleResult lineaDetalle in lstEgresosVarios_Detalle)
                        {
                            if (lineaDetalle.ID_Item == lineaGasto.ID_Item)
                            {
                                tableLayout.AddCell(new PdfPCell(new Phrase(lineaDetalle.ID_Agenda, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 0, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                                tableLayout.AddCell(new PdfPCell(new Phrase(lineaDetalle.Agenda, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 0, iTextSharp.text.BaseColor.BLACK))) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                                tableLayout.AddCell(new PdfPCell(new Phrase(lineaDetalle.NombreDocumento, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 0, iTextSharp.text.BaseColor.BLACK))) { Colspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                                tableLayout.AddCell(new PdfPCell(new Phrase(lineaDetalle.Serie + "-" + lineaDetalle.Numero, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 0, iTextSharp.text.BaseColor.BLACK))) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                                tableLayout.AddCell(new PdfPCell(new Phrase(decimal.Round(lineaDetalle.Importe, 2).ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 0, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                                sumaTotal = sumaTotal + decimal.Round(lineaDetalle.Importe, 2);
                                Total = Total + decimal.Round(lineaDetalle.Importe, 2);
                            }
                        }

                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 0, iTextSharp.text.BaseColor.BLACK))) { Colspan = 9, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                        cellImporte = new PdfPCell(new Phrase("S/.", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 1, BorderColor = iTextSharp.text.BaseColor.BLACK, BackgroundColor = iTextSharp.text.BaseColor.WHITE };
                        cellImporte.Border = Rectangle.BOTTOM_BORDER; // | Rectangle.TOP_BORDER;
                        cellImporte.BorderWidthBottom = 0f;
                        cellImporte.BorderWidthTop = 2f;
                        cellImporte.PaddingBottom = 3f;
                        cellImporte.PaddingLeft = 0f;
                        cellImporte.PaddingTop = 0f;
                        tableLayout.AddCell(cellImporte);

                        cellImporte = new PdfPCell(new Phrase(decimal.Round(sumaTotal, 2).ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 1, BorderColor = iTextSharp.text.BaseColor.BLACK, BackgroundColor = iTextSharp.text.BaseColor.WHITE };
                        cellImporte.Border = Rectangle.BOTTOM_BORDER; // | Rectangle.TOP_BORDER;
                        cellImporte.BorderWidthBottom = 0f;
                        cellImporte.BorderWidthTop = 2f;
                        cellImporte.PaddingBottom = 3f;
                        cellImporte.PaddingLeft = 0f;
                        cellImporte.PaddingTop = 0f;
                        tableLayout.AddCell(cellImporte);

                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 11, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    }
                    cellImporte = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 9, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 1, BorderColor = iTextSharp.text.BaseColor.BLACK, BackgroundColor = iTextSharp.text.BaseColor.WHITE };
                    cellImporte.Border = Rectangle.BOTTOM_BORDER; // | Rectangle.TOP_BORDER;
                    cellImporte.BorderWidthBottom = 0f;
                    cellImporte.BorderWidthTop = 2f;
                    cellImporte.PaddingBottom = 3f;
                    cellImporte.PaddingLeft = 0f;
                    cellImporte.PaddingTop = 0f;
                    tableLayout.AddCell(cellImporte);

                    cellImporte = new PdfPCell(new Phrase("S/.", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 1, BorderColor = iTextSharp.text.BaseColor.BLACK, BackgroundColor = iTextSharp.text.BaseColor.WHITE };
                    cellImporte.Border = Rectangle.BOTTOM_BORDER; // | Rectangle.TOP_BORDER;
                    cellImporte.BorderWidthBottom = 0f;
                    cellImporte.BorderWidthTop = 2f;
                    cellImporte.PaddingBottom = 3f;
                    cellImporte.PaddingLeft = 0f;
                    cellImporte.PaddingTop = 0f;
                    tableLayout.AddCell(cellImporte);

                    cellImporte = new PdfPCell(new Phrase(decimal.Round(Total, 2).ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 1, BorderColor = iTextSharp.text.BaseColor.BLACK, BackgroundColor = iTextSharp.text.BaseColor.WHITE };
                    cellImporte.Border = Rectangle.BOTTOM_BORDER; // | Rectangle.TOP_BORDER;
                    cellImporte.BorderWidthBottom = 0f;
                    cellImporte.BorderWidthTop = 2f;
                    cellImporte.PaddingBottom = 3f;
                    cellImporte.PaddingLeft = 0f;
                    cellImporte.PaddingTop = 0f;
                    tableLayout.AddCell(cellImporte);



                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tableLayout;
        }

        public void ExportReport( string fileName, string fileType, bool inline, MemoryStream memory )
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                stream = memory;
                Response.Clear();

                Response.ContentType = "application/" + fileType;
                Response.AddHeader("Accept-Header", stream.Length.ToString());
                Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}.{2}", (inline ? "Inline" : "Attachment"), fileName, fileType));
                Response.AddHeader("Content-Length", stream.Length.ToString());
                Response.ContentEncoding = System.Text.Encoding.Default;
                Response.BinaryWrite(stream.ToArray());

                Response.End();
            }
            catch(Exception ex)
            {
                lblMensaje.Text = ex.Message; 
            }

         
        }

        public byte[] ExportSampleFile(string Op)
        {
            int[] values = { 4, 6, 18, 2, 1, 76, 0, 3, 11 };
            
            return CreateWorkbook(values, @"C:\Temp\SampleWorkbook_" + Op + "_" + DateTime.Now.Millisecond.ToString() +  ".xls");
        }
        private byte[] CreateWorkbook(int[] values, string filePath)
        {
            Excel.Application excelApp = null;
            Excel.Workbooks workBooks = null;
            Excel.Workbook workBook = null;
            FileInfo tempFile = null;
            FileInfo convertedTempFile = null;

            try
            {
              
            
                // Start Excel and create a workbook and worksheet.
                Excel.Application _application = new Excel.Application();
                Excel.Workbook _workbook;
                Excel.Worksheet _worksheet;

                _application.WorkbookBeforeClose +=
                    (Excel.Workbook wb, ref bool cancel) => Dispose();

                _workbook = _application.Workbooks.Add();
                _worksheet = _workbook.Sheets.Add() as Excel.Worksheet;
                _worksheet.Name = "Sample Worksheet";

                for (int i = 1; i < values.Length; i++)
                {
                    _worksheet.Cells[i, 1] = values[i];
                }

                _application.DisplayAlerts = false;

                string folderPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                _workbook.SaveAs(filePath);
                _workbook.Close();

                //_application.Visible = true;
                //_workbook.Activate();
                tempFile = new FileInfo(filePath);
                convertedTempFile = new FileInfo(tempFile.FullName);

            }
            catch
            {
                //Dispose();
            }
            return File.ReadAllBytes(convertedTempFile.FullName);
        }

        #region Métodos web
        [WebMethod]
        public static AutoCompleteBoxData Agenda_BuscarProveedor(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarProveedorResult[] lst = objAgendaWCFClient.Agenda_ListarProveedor(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString);

                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsAgenda_ListarProveedorResult agenda in lst)
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

    }
}