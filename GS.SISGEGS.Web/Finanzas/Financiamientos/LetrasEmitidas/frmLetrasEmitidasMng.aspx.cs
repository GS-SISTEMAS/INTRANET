using GS.SISGEGS.DM;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.ItemWCF;
using GS.SISGEGS.Web.EstadoCuentaWCF;
using GS.SISGEGS.Web.EmpresaWCF;
using System.IO;
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

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using GS.SISGEGS.BE;
using GS.SISGEGS.Web.DireccionWCF;
using GS.SISGEGS.Web.SedeWCF;
using GS.SISGEGS.Web.EnvioWCF;
using GS.SISGEGS.Web.CreditoWCF;
using GS.SISGEGS.Web.DocumentoWCF;
using GS.SISGEGS.Web.OrdenVentaWCF;
using GS.SISGEGS.Web.DespachoWCF;
using GS.SISGEGS.Web.MonedaWCF;
using GS.SISGEGS.Web.FormaPagoWCF;
using GS.SISGEGS.Web.ImpuestoWCF;
using GS.SISGEGS.Web.VarianteWCF;
using GS.SISGEGS.Web.LetrasEmitidasWCF;
using System.Data.OleDb;
using System.Windows;
using Telerik.Windows.Documents.FormatProviders;
using Telerik.Windows.Documents;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Diagnostics;
using System.Net;
using GS.SISGEGS.Web.LoginWCF;

namespace GS.SISGEGS.Web.Finanzas.Financiamientos.LetrasEmitidas
{
    public partial class frmLetrasEmitidasMng : System.Web.UI.Page
    {
        #region Métodos privados
        private void Variante_Cargar()
        {
            VarianteWCFClient objVarianteWCF;
            Variante_BuscarResult objVariante;
            try
            {
                objVarianteWCF = new VarianteWCFClient();
                objVariante = objVarianteWCF.Variante_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, "PEDMNG");
                //    cboAlmacen.SelectedValue = cboAlmacen.Items.FindItem(x => x.Text.ToUpper() == objVariante.parametro1.ToUpper()).Value;
                //    cboOpDocVenta.SelectedValue = cboOpDocVenta.Items.FindItem(x => x.Text.ToUpper() == objVariante.parametro2.ToUpper()).Value;
                //    cboFormaPago.SelectedValue = cboFormaPago.Items.FindItem(x => x.Text.ToUpper() == objVariante.parametro3.ToUpper()).Value;
                //    cboSede.SelectedValue = cboSede.Items.FindItem(x => x.Text.ToUpper() == objVariante.parametro4.ToUpper()).Value;
                //    lblTrans.Text = objVariante.parametro5;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LetrasEmitidas_Cabecera(string idOperacion)
        {

            LetrasEmitidasWCFClient objLetrasEmitidasWCF = new LetrasEmitidasWCFClient();

            gsLetrasEmitidas_CabeceraResult[] objLetrasEmitidasCab;

            try
            {
                Session["Id_Transporte"] = null;

                objLetrasEmitidasCab = objLetrasEmitidasWCF.LetrasEmitidas_Cabecera(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idOperacion);

                ViewState["lstCabecera"] = JsonHelper.JsonSerializer(objLetrasEmitidasCab);


                txtRUC.Text = objLetrasEmitidasCab[0].ID_Aceptante.ToString();
                lblidAgenda.Value = objLetrasEmitidasCab[0].ID_Aceptante.ToString();

                txtClienteNombre.Text = objLetrasEmitidasCab[0].agendanombre.ToString();
                dpFechaCanje.SelectedDate = objLetrasEmitidasCab[0].Fecha;

                Direccion_Cargar(lblidAgenda.Value, cboDireccion);
                Cliente_Buscar(lblidAgenda.Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LetrasEmitidas_Documentos(string idOperacion)
        {
            LetrasEmitidasWCFClient objLetrasEmitidasWCF = new LetrasEmitidasWCFClient();
            gsLetrasEmitidas_DocumentosResult[] list;

            try
            {
                list = objLetrasEmitidasWCF.LetrasEmitidas_Documentos(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idOperacion);
                ViewState["lstDocumentos"] = JsonHelper.JsonSerializer(list);

                grdDocuementos.DataSource = list;
                grdDocuementos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LetrasEmitidas_Letras(string idOperacion)
        {
            LetrasEmitidasWCFClient objLetrasEmitidasWCF = new LetrasEmitidasWCFClient();
            gsLetrasEmitidas_LetrasResult[] list;

            try
            {
                list = objLetrasEmitidasWCF.LetrasEmitidas_Letras(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idOperacion);
                ViewState["lstLetras"] = JsonHelper.JsonSerializer(list);

                grdLetras.DataSource = list;
                grdLetras.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Referencia_Cargar(string idAgenda, int idSucursal, RadComboBox cbobox)
        {
            AgendaWCFClient objAgendaWCF;
            VBG02699Result objReferencia;
            List<VBG02699Result> lstReferencias;
            try
            {
                objAgendaWCF = new AgendaWCFClient();
                objReferencia = new VBG02699Result();
                objReferencia.ID = -1;
                objReferencia.Nombre = "Ninguno";

                lstReferencias = objAgendaWCF.AgendaAnexoReferencia_ListarPorSucursal(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idSucursal, idAgenda).ToList();
                lstReferencias.Insert(0, objReferencia);

                cbobox.DataSource = lstReferencias;
                cbobox.DataTextField = "Nombre";
                cbobox.DataValueField = "ID";
                cbobox.DataBind();

                if (cbobox.Items.Count > 0)
                    cbobox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Sucursal_Cargar(string idAgenda, RadComboBox cbobox)
        {
            AgendaWCFClient objAgendaWCFC;
            List<VBG00167Result> lstSucursal;
            VBG00167Result objSucursal;
            try
            {
                objAgendaWCFC = new AgendaWCFClient();
                objSucursal = new VBG00167Result();

                lstSucursal = objAgendaWCFC.AgendaAnexo_ListarDireccionCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda).ToList();
                objSucursal.ID = -1;
                objSucursal.Nombre = "Central";
                lstSucursal.Insert(0, objSucursal);

                cbobox.DataSource = lstSucursal;
                cbobox.DataTextField = "Nombre";
                cbobox.DataValueField = "ID";
                cbobox.DataBind();

                if (cbobox.Items.Count > 0)
                    cbobox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Direccion_Cargar(string idAgenda, RadComboBox cbobox)
        {
            DireccionWCFClient objDireccionWCF;
            try
            {
                objDireccionWCF = new DireccionWCFClient();

                var dirFiscal = from x in objDireccionWCF.Direccion_ListarCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                                 ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda).ToList().FindAll(x => x.TipoDireccion == 104)
                                select new
                                {
                                    x.ID,
                                    DisplayField = String.Format("{0} {1} {2} {3}", x.Abreviatura, x.Direccion, x.Numero, x.Distrito)
                                };

                cbobox.DataSource = dirFiscal;
                cbobox.DataValueField = "ID";
                cbobox.DataTextField = "DisplayField";
                cbobox.DataBind();

                if (cbobox.Items.Count > 0)
                    cbobox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Cliente_Buscar(string idAgenda)
        {
            AgendaWCFClient objAgendaWCFClient;
            VBG01134Result objAgendaCliente;
            decimal? lineaCredito = null;
            decimal? TC = null;
            DateTime? fechaVecimiento = null;
            try
            {
                objAgendaWCFClient = new AgendaWCFClient();
                objAgendaCliente = new VBG01134Result();

                objAgendaCliente = objAgendaWCFClient.Agenda_BuscarCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda, ref lineaCredito, ref fechaVecimiento, ref TC);

                if (!string.IsNullOrEmpty(objAgendaCliente.RUC))
                {
                    //txtRUCCliente.Text = objAgendaCliente.RUC;
                    //txtNombreCliente.Text = objAgendaCliente.Nombre;
                    //lblCodigoCliente.Value = objAgendaCliente.RUC;
                }
                else
                {
                    //txtRUCCliente.Text = idAgenda;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Métodos Protegidos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (!Page.IsPostBack)
                {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    lblOp.Value = int.Parse((Request.QueryString["idOperacion"])).ToString();
                    Session["idOperacion"] = int.Parse((Request.QueryString["idOperacion"])).ToString();

                    Title = "Ver financiamiento";
                    LetrasEmitidas_Cabecera(lblOp.Value);
                    LetrasEmitidas_Documentos(lblOp.Value);
                    LetrasEmitidas_Letras(lblOp.Value);

                    lblMensaje.Text = "Listo para visualizar financiamiento" + (Request.QueryString["idOrdenVenta"]);

                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        protected void grdItem_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            if (!ConnectionHelpers.CheckForInternetConnection())
                throw new ArgumentException("ERROR: Revisar su conexión a internet.");

            try
            {
                if (e.CommandName == "Eliminar")
                {
                    List<gsItem_BuscarResult> lstProductos = (List<gsItem_BuscarResult>)Session["lstProductos"];
                    lstProductos.Find(x => x.Item_ID.ToString() == e.CommandArgument.ToString() && x.Estado == 1).Estado = 0;

                    List<gsImpuesto_ListarPorItemResult> lstImpuestos;
                    lstImpuestos = ((List<gsImpuesto_ListarPorItemResult>)Session["lstImpuestos"]).FindAll(x => x.ID_Item == lstProductos.Find(p => p.Item_ID.ToString() == e.CommandArgument.ToString()).Item_ID);
                    foreach (gsImpuesto_ListarPorItemResult objImpuesto in lstImpuestos)
                    {
                        ((List<gsImpuesto_ListarPorItemResult>)Session["lstImpuestos"]).Remove(objImpuesto);
                    }

                    //Calcular_Glosa();

                    //grdItem.DataSource = ((List<gsItem_BuscarResult>)Session["lstProductos"]).FindAll(x => x.Estado == 1).OrderBy(x => x.Item);
                    //grdItem.DataBind();

                    lblMensaje.Text = "Se eliminó el producto del LetrasEmitidas con código " + e.CommandArgument.ToString() + " del LetrasEmitidas.";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        protected void ramLetrasEmitidasMng_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if (e.Argument == "Rebind")
                {
                    //grdItem.MasterTableView.SortExpressions.Clear();
                    //grdItem.MasterTableView.GroupByExpressions.Clear();
                    //grdItem.DataSource = (List<gsItem_BuscarResult>)Session["lstProductos"];
                    //grdItem.DataBind();
                    //Calcular_Glosa();

                    lblMensaje.Text = "Se agregó el producto al LetrasEmitidas.";
                    lblMensaje.CssClass = "mensajeExito";

                    //acbProducto.Entries.Clear();
                    //acbProducto.Focus();
                }

                if (e.Argument.Split('(')[0].Trim() == "RebindAndNavigate")
                {
                    //grdItem.MasterTableView.SortExpressions.Clear();
                    //grdItem.MasterTableView.GroupByExpressions.Clear();
                    //grdItem.MasterTableView.CurrentPageIndex = grdItem.MasterTableView.PageCount - 1;
                    //grdItem.Rebind();
                    //grdItem.MasterTableView.SortExpressions.Clear();
                    //grdItem.MasterTableView.GroupByExpressions.Clear();
                    //grdItem.DataSource = ((List<gsItem_BuscarResult>)Session["lstProductos"]).FindAll(x => x.Estado == 1).OrderBy(x => x.Item);
                    //grdItem.DataBind();

                    //Calcular_Glosa();

                    lblMensaje.Text = "Se agregó el producto con nro. kardex " + e.Argument.Split('(')[1].Trim().Split(')')[0] + " al LetrasEmitidas.";
                    lblMensaje.CssClass = "mensajeExito";

                    //acbProducto.Entries.Clear();
                    //acbProducto.Focus();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            //GuiaWCFClient objLetrasEmitidasWCF = new GuiaWCFClient();
            //lblMensaje.Text = "";
            //try
            //{
            //    if (validarFecha() == 0)
            //    {
            //        objLetrasEmitidasWCF.GuiaVenta_ActualizarTransporte(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
            //        GuiaVenta_ObtenerCambios(), Convert.ToInt32(lblOp.Value));

            //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind();", true);
            //    }



        //}
        //    catch (Exception ex)
        //    {
        //        lblMensaje.Text = "ERROR: " + ex.Message;
        //        lblMensaje.CssClass = "mensajeError";
        //    }
        }
        #endregion

        protected void stripLetrasEmitidas_TabClick(object sender, RadTabStripEventArgs e)
        {

        }
        protected void btnExpPDFDetalle_Click(object sender, ImageClickEventArgs e)
        {
            int idEmpresa;
            string fechaHasta;

            fechaHasta = dpFechaCanje.SelectedDate.Value.ToString("dd/MM/yyyy");
            idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;

            ExportarPDF(idEmpresa, fechaHasta);
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

            AbriVentana(fileName);

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

            file = empresa + "LetrasEmitidas_" + anho + mes + dia + minutos + segundo + miliseg + ".pdf";

            return file;
        }
        private void AbriVentana(string variable)
        {
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "AbrirNuevoVentana( " + variable + ");", true);
            //ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "AbrirNuevoVentana(" + variable + ");", true);
            RegisterStartupScript("script", "<script>window.open('frmExportarPDF.aspx?strFileNombre=" + variable + "', 'toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=0,resizable=0,width=500,height=500,top=0,left=0')</script>");
        }
        private static void AddCellToBody(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

        }
        private static void AddCellToBodyColspan(PdfPTable tableLayout, string cellText, int intCol)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = intCol, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

        }
        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
        }
        private static void AddCellToHeaderColspan(PdfPTable tableLayout, string cellText, int intCol)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = intCol, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
        }
        private PdfPTable Add_Content_To_PDF(PdfPTable tableLayout, string fechaHasta, int idEmpresa)
        {
            EmpresaWCFClient objEmpresaWCF = new EmpresaWCFClient();
            AgendaWCFClient objAgendaWCF = new AgendaWCFClient();
            List<gsLetrasEmitidas_CabeceraResult> lstCabecera;
            List<gsLetrasEmitidas_DocumentosResult> lstDocumentos;
            List<gsLetrasEmitidas_LetrasResult> lstLetras;

            Empresa_BuscarDetalleResult objEmpresa;
            string urlImagen;
            lstCabecera = FinanciamientoCabecera();
            lstDocumentos = FinancimientoDocumentos();
            lstLetras = FinancimientoLetras();


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
            tableLayout.AddCell(new PdfPCell(new Phrase("CARTA CANJE DE LETRA", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 10, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });
            tableLayout.AddCell(new PdfPCell(new Phrase(DateTime.Now.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 5, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 1, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });

            tableLayout.AddCell(new PdfPCell(new Phrase(objEmpresa.ruc + " " + objEmpresa.razonSocial, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 6, Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT });
            tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 5, Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER });

            tableLayout.AddCell(new PdfPCell(new Phrase(objEmpresa.direccion, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 6, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_LEFT });
            tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 5, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });

            //Add Cliente
            int cont = 0;

            foreach (gsLetrasEmitidas_CabeceraResult FinanResumen in lstCabecera)
            {
                cont = cont + 1;
                // ADD Cliente

                tableLayout.AddCell(new PdfPCell(new Phrase("1. Razón Social", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                List<gsAgenda_BuscarClienteDetalleResult> LimiteAgenda = objAgendaWCF.Agenda_BuscarClienteDetalle(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, FinanResumen.ID_Aceptante.ToString()).ToList();
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
                    tableLayout.AddCell(new PdfPCell(new Phrase(FinanResumen.ID_Aceptante + " " + FinanResumen.agendanombre, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Registrar dirección fiscal. ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, BorderColorLeft = BaseColor.RED, BorderColorTop = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                }

                tableLayout.AddCell(new PdfPCell(new Phrase("   ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 11, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });

                //ADD Resumen cliente

                tableLayout.AddCell(new PdfPCell(new Phrase("2. Línea de crédito", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Deuda vencida", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Deuda total", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Línea disponible", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 7, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });



                //tableLayout.AddCell(new PdfPCell(new Phrase(strLineaCredito, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                //tableLayout.AddCell(new PdfPCell(new Phrase(strDeudaVencida, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                //tableLayout.AddCell(new PdfPCell(new Phrase(strTotalCredito, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                //tableLayout.AddCell(new PdfPCell(new Phrase(strCreditoDisponible, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                //tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 7, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 11, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });


                tableLayout.AddCell(new PdfPCell(new Phrase("3. NoVencido", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 01a30", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 31a60", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 61a120", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 121a360", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 361aMas", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Deuda total", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 4, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });

                //tableLayout.AddCell(new PdfPCell(new Phrase(strsumNoVencido, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                //tableLayout.AddCell(new PdfPCell(new Phrase(strsumImportePendiente_01a30, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                //tableLayout.AddCell(new PdfPCell(new Phrase(strsumImportePendiente_31a60, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                //tableLayout.AddCell(new PdfPCell(new Phrase(strsumImportePendiente_61a120, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                //tableLayout.AddCell(new PdfPCell(new Phrase(strsumImportePendiente_121a360, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                //tableLayout.AddCell(new PdfPCell(new Phrase(strsumImportePendiente_361aMas, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                //tableLayout.AddCell(new PdfPCell(new Phrase(strsumImportePendiente, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                //tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 4, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });


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


                int count = 0;
                string TipoDoc = "";
                string strTipoDoc = "";

                tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 11, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });
                tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 11, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });


            }

            return tableLayout;
        }
        private List<gsLetrasEmitidas_CabeceraResult> FinanciamientoCabecera()
        {
            List<gsLetrasEmitidas_CabeceraResult> lst = JsonHelper.JsonDeserialize<List<gsLetrasEmitidas_CabeceraResult>>((string)ViewState["lstCabecera"]);
            return lst;
        }
        private List<gsLetrasEmitidas_DocumentosResult> FinancimientoDocumentos()
        {
            List<gsLetrasEmitidas_DocumentosResult> lst = JsonHelper.JsonDeserialize<List<gsLetrasEmitidas_DocumentosResult>>((string)ViewState["lstDocumentos"]);
            return lst;
        }
        private List<gsLetrasEmitidas_LetrasResult> FinancimientoLetras()
        {
            List<gsLetrasEmitidas_LetrasResult> lst = JsonHelper.JsonDeserialize<List<gsLetrasEmitidas_LetrasResult>>((string)ViewState["lstLetras"]);
            return lst;
        }
    }
}