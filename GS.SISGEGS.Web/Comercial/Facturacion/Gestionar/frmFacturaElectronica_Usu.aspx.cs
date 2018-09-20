using GS.SISGEGS.DM;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.FacturaElectronica2WCF;
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
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.OrdenVentaWCF;
using GS.SISGEGS.Web.LetrasEmitidasWCF;
using System.Drawing;
using xi = Telerik.Web.UI.ExportInfrastructure;
using Telerik.Web.UI.GridExcelBuilder;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Web.UI.HtmlControls;
using GS.SISGEGS.Web.FacturacionElectronicaOkWCF;
using System.ComponentModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
 
using ICSharpCode.SharpZipLib.Zip;
using Excel = Microsoft.Office.Interop.Excel;
using GS.Helpdesk.entities.Commons;
using System.Web.Script.Serialization;

namespace GS.SISGEGS.Web.Comercial.Facturacion.Gestionar
{
    public partial class frmFacturaElectronica_Usu : System.Web.UI.Page
    {
        FacturacionElectronicaOkWCF.ENEmpresa objEmpresa = new FacturacionElectronicaOkWCF.ENEmpresa();
        FacturacionElectronicaOkWCF.WSComprobanteSoapClient oServicio = new FacturacionElectronicaOkWCF.WSComprobanteSoapClient();
        FacturacionElectronicaOkWCF.General oGeneral = new FacturacionElectronicaOkWCF.General();
        FacturacionElectronicaOkWCF.ENComprobante objComprobante = new FacturacionElectronicaOkWCF.ENComprobante();
        DateTime dpInicio;
        DateTime dpFinal; 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (!Page.IsPostBack)
                {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    rmyPeriodo.SelectedDate = DateTime.Now;
                    dpInicio = rmyPeriodo.SelectedDate.Value.AddDays(-rmyPeriodo.SelectedDate.Value.Day + 1);
                    dpFinal = dpInicio.AddMonths(+1).AddDays(-1); 

                    lblDate.Text = "";
                    TipoDocumento_Cargar();
                }
            }

            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        private void ListarFacturaElectronica(string codAgenda,  DateTime fechaInicial, DateTime fechaFinal, int TipoDoc)
        {
            FacturaElectronica2WCFClient objFacturaWCF = new FacturaElectronica2WCFClient();

            try
            {
                List<object> parametros = new List<object>();
                parametros.Add(((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString());
                parametros.Add(((Usuario_LoginResult)Session["Usuario"]).codigoUsuario);
                string responseFromServer = GSbase.POSTResult("ListarMasterTen", 28, parametros);
                MasterTenCollection collection = new JavaScriptSerializer().Deserialize<MasterTenCollection>(responseFromServer);
                string codigoVendedor = "";
                if (collection.Rows.Count > 0)
                    codigoVendedor = collection.Rows[0].v01.ToString();

                List<VBG04694Result> lst = objFacturaWCF.FacturaElectronica_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, fechaInicial, fechaFinal, codAgenda, codigoVendedor, 0, TipoDoc, 0, null, 0).ToList().FindAll(x => x.TablaOrigen!= "tblRetencionIGV");
       
                ViewState["lstFacturaElectronica"] = JsonHelper.JsonSerializer(lst);
                grdFacturaElectronica.DataSource = lst;
                grdFacturaElectronica.DataBind();
                lblDate.Text = "1";
    
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se encontrarón resultados, "+  ex.Message.ToString();
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void BuscarFechas_ListarFacturaElectronica()
        {
            DateTime fecha1;
            DateTime fecha2;
            string cliente;
            int TipoDoc;
            string item;

            item = "";
            cliente = "";
            TipoDoc = 0;

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (Validar_Variables() == 0)
                {
                    dpInicio = rmyPeriodo.SelectedDate.Value.AddDays(-rmyPeriodo.SelectedDate.Value.Day + 1);
                    dpFinal = dpInicio.AddMonths(+1).AddDays(-1);

                    fecha1 = dpInicio;
                    fecha2 = dpFinal;
                    //fecha2 = dpInicio.SelectedDate.Value;

                    if (acbCliente == null || acbCliente.Text.Split('-')[0] == "" || acbCliente.Text == "")
                    {
                        cliente = null;
                    }
                    else { cliente = acbCliente.Text.Split('-')[0]; }

                    if (cboTipoDoc == null || cboTipoDoc.SelectedValue == "" || cboTipoDoc.SelectedIndex == 0)
                    {
                        TipoDoc = 0;
                    }
                    else { TipoDoc = Convert.ToInt32(cboTipoDoc.SelectedValue.ToString()); }


                    ListarFacturaElectronica(cliente, fecha1, fecha2, TipoDoc);
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
            try
            {
                lblMensajeFecha.Text = "";
                BuscarFechas_ListarFacturaElectronica();
            }

            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
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


        public int Validar_Variables()
        {
            int dias;
            DateTime inicial = new DateTime();
            DateTime final = new DateTime() ;

            int valor = 0;

            if ( dpInicio == null || dpInicio.ToString() == "")
            {
                valor = 1;
                lblMensajeFecha.Text = lblMensaje.Text + "Seleccionar fecha final de emisión. ";
                lblMensajeFecha.CssClass = "mensajeError";
              
            }
            else
            { inicial = dpInicio; }
            if (dpFinal == null || dpFinal.ToString() == "")
            {
                valor = 1;
                lblMensajeFecha.Text = lblMensaje.Text + "Seleccionar fecha final de emisión. ";
                lblMensajeFecha.CssClass = "mensajeError";
                
            }
            else
            { final = dpFinal; }
            
           if(valor == 0)
            {
                dias = final.Subtract(inicial).Days;
                if( dias < 0)
                {
                    valor = 1;
                    lblMensajeFecha.Text = lblMensaje.Text + "Fecha final debe ser igual o mayor que fecha inicial. ";
                    lblMensajeFecha.CssClass = "mensajeError";
                  
                }

                if (dias > 30)
                {
                    valor = 1;
                    lblMensajeFecha.Text = lblMensaje.Text + "El rango de fechas, no puede superar los 30 días. ";
                    lblMensajeFecha.CssClass = "mensajeError";
                   
                }
            }

            if (cboTipoDoc == null || cboTipoDoc.SelectedValue == "" || cboTipoDoc.SelectedIndex == 0)
            {
                valor = 1;
                lblMensajeFecha.Text = lblMensaje.Text + "Debe seleccionar un Tipo de documento. ";
                lblMensajeFecha.CssClass = "mensajeError";
              
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
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString, 1);
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

        protected void grdFacturaElectronica_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (lblDate.Text == "1")
                {
                    List<VBG04694Result> lst = JsonHelper.JsonDeserialize<List<VBG04694Result>>((string)ViewState["lstFacturaElectronica"]);
                    grdFacturaElectronica.DataSource = lst;
                    //grdFacturaElectronica.Rebind();

                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message.ToString() + " Need,  No se encontrarón resultados.";
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void TipoDocumento_Cargar()
        {
            try
            {
                FacturaElectronica2WCFClient objFacturaWCF = new FacturaElectronica2WCFClient();
                gsComboDocElectronicoResult objTipoDoc = new gsComboDocElectronicoResult();
                List<gsComboDocElectronicoResult> lst2 = new List<gsComboDocElectronicoResult>(); 

                List<gsComboDocElectronicoResult> lst = objFacturaWCF.ComboDocElectronico(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList() ;
                foreach(gsComboDocElectronicoResult item in lst)
                {
                    if(!item.Nombre.Contains("Retenci"))
                    {
                        lst2.Add(item);
                    }
                  
                }

                lst2.Insert(0, objTipoDoc);
                objTipoDoc.Nombre = "Todos";
                objTipoDoc.ID_Documento = 0;
 

                cboTipoDoc.DataSource = lst2;
                cboTipoDoc.DataTextField = "Nombre";
                cboTipoDoc.DataValueField = "ID_Documento";
                cboTipoDoc.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarFecha_Facturacion()
        {
            try
            {
                FacturaElectronica2WCFClient objFacturaWCF = new FacturaElectronica2WCFClient();
                gsComboDocElectronicoResult objTipoDoc = new gsComboDocElectronicoResult();

                List<gsComboDocElectronicoResult> lst = objFacturaWCF.ComboDocElectronico(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();


                lst.Insert(0, objTipoDoc);
                objTipoDoc.Nombre = "Todos";
                objTipoDoc.ID_Documento = 0;

                cboTipoDoc.DataSource = lst;
                cboTipoDoc.DataTextField = "Nombre";
                cboTipoDoc.DataValueField = "ID_Documento";
                cboTipoDoc.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grdFacturaElectronica_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    GridDataItem dataitem = (GridDataItem)e.Item;

                    CheckBox Ok_XML = (CheckBox)item["Ok_XML"].Controls[0];
                    CheckBox Ok_SunatRpta = (CheckBox)item["Ok_SunatRpta"].Controls[0];
                    CheckBox OK_Proceso = (CheckBox)item["OK_Proceso"].Controls[0];

             
                    ImageButton imgFactura = (ImageButton)item.Controls[0].FindControl("ibImprimir");
                    ImageButton imLetra = (ImageButton)item.Controls[0].FindControl("ibImprimirLetra");

                    if (!item["SunatRpta_NroTicket"].Text.Contains("  (L)"))
                    {
                        if (!item["SunatRpta_NroTicket"].Text.Contains("Letras L"))
                        {
                            ImageButton ibImprimirLetra = (ImageButton)item.FindControl("ibImprimirLetra");
                            if (Convert.ToInt32(item["SunatEnvio_NroIntento"].Text) == 0)
                            {
                                ibImprimirLetra.ImageUrl = "~/Images/Icons/sign-error-20.png";
                            }
                            else if (Ok_SunatRpta.Checked)
                            {
                                ibImprimirLetra.ImageUrl = "~/Images/Icons/reloj.png";
                            }
                            else
                            {
                                ibImprimirLetra.ImageUrl = "~/Images/Icons/sunat.png";
                            }

                            ibImprimirLetra.Width = 18;
                            ibImprimirLetra.Height = 18;
                            //ibImprimirLetra.Visible = false;
                        }

                    }

                    if (!item["SunatRpta_NroTicket"].Text.Contains("L"))
                    {
                        ImageButton ibEditar = (ImageButton)item.FindControl("ibEditar");
                        ibEditar.Visible = false;

                        ImageButton ibImprimirLetra = (ImageButton)item.FindControl("ibImprimirLetra");
                        ibImprimirLetra.Visible = false;

                        CheckBox Check = (CheckBox)item.FindControl("Check");
                        Check.Enabled = false;
                    }


                    //''

                    //TableCell cell = dataitem["Check_XML"];
                    //CheckBox Check_Ok_XML = (CheckBox)cell.Controls[0].FindControl("Check_Ok_XML");
                    //CheckBox Check_Ok_SunatRpta = (CheckBox)cell.Controls[0].FindControl("Check_Ok_SunatRpta");
                    //CheckBox Check_OK_Proceso = (CheckBox)cell.Controls[0].FindControl("Check_OK_Proceso");



                    //if (Ok_SunatRpta.Checked == true)
                    //{
                    //    imgFactura.Visible = true;
                    //}
                    //else
                    //{
                    //    imgFactura.Visible = false;
                    //}

                    //if (OK_Proceso.Checked == true)
                    //{
                    //    imLetra.Visible = true;
                    //}
                    //{
                    //    imLetra.Visible = false;
                    //}

                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        protected void grdFacturaElectronica_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                VBG04694Result objFactura = new VBG04694Result();
                VBG04694_FacturacionResult objFacturaDoc = new VBG04694_FacturacionResult();

                DataTable tblDocumentos = new DataTable();
                DataColumn col1 = new DataColumn("OP_OV");
                DataColumn col2 = new DataColumn("OP_DOC");
 
                tblDocumentos.Columns.Add(col1);
                tblDocumentos.Columns.Add(col2);


                if (e.CommandName == "Editar")
                {

                    if (e.Item is GridDataItem)
                    {
                        GridDataItem item = (GridDataItem)e.Item;
                        GridDataItem dataitem = (GridDataItem)e.Item;

                        int diascredito = Convert.ToInt32(item["DiasCredito"].Text.Trim() == string.Empty ? "0" : item["DiasCredito"].Text.Trim());
                        DateTime FechaInicio =  Convert.ToDateTime(item["Fecha"].Text ) ;
                        //DateTime FechaFin = Convert.ToDateTime(item["Vencimiento"].Text ) ;
                        DateTime FechaFin = FechaInicio.AddDays(diascredito);



                        int intOP_OV   = Convert.ToInt32(item["OP_OV"].Text);
                        //int DiasCredito = Convert.ToInt32(item["DiasCredito"].Text);

                        //ibEditar


                        int idOrdenVenta = 0;
                        char Cero = Convert.ToChar("0");

                        //string strFechaInicio = FechaInicio.ToShortDateString();
                        //string strFechaFin = FechaFin.ToShortDateString(); 

                        string strFechaInico = FechaInicio.Year.ToString() + FechaInicio.Month.ToString().PadLeft(2, Cero) + FechaInicio.Day.ToString().PadLeft(2, Cero);
                        string strFechaFin = FechaFin.Year.ToString() + FechaFin.Month.ToString().PadLeft(2, Cero) + FechaFin.Day.ToString().PadLeft(2, Cero);


                        //idOrdenVenta = int.Parse((Request.QueryString["idOrdenVenta"]));
                        //int OpOrigen = Convert.ToInt32(e.CommandArgument.ToString());
                        DataRow linea = tblDocumentos.NewRow();   
                        linea[0] = intOP_OV;
                        tblDocumentos.Rows.Add(linea); 

                        ViewState["tblDocumentos"] = tblDocumentos;

                        var RUC = "000";
                        if (acbCliente.Text.Split('-').Count() >= 2)
                            RUC = acbCliente.Text.Split('-')[0];
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowInsertFormLetras(" + strFechaInico + "," + strFechaFin + "," + intOP_OV + "," + RUC + ");", true);
                        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowInsertForm(" + e.CommandArgument + ");", true);

                    }
                }

                if (e.CommandName == "Imprimir")
                {
                    
                    decimal OpElectronica = 0;
                    OpElectronica = Convert.ToDecimal( e.CommandArgument.ToString()); 
 
                    Exportar_PDF(OpElectronica); 
                }

                if (e.CommandName == "ImprimirLetra")
                {
                    var item = (GridDataItem)e.Item;
                    CheckBox Ok_SunatRpta = (CheckBox)item["Ok_SunatRpta"].Controls[0];
                    ImageButton ibImprimirLetra = (ImageButton)item.FindControl("ibImprimirLetra");
                    if (ibImprimirLetra.ImageUrl == "~/Images/Icons/sign-error-20.png")
                    {
                        string Message = "Por favor, vuelva a planificar porque no se completó el proceso";
                        rwmVidaLey.RadAlert(Message, 400, null, "Mensaje de error", null);
                    }
                    else if (ibImprimirLetra.ImageUrl == "~/Images/Icons/reloj.png")
                    {
                        string Message = "Por favor espere 3 minutos, y realice la busqueda nuevamente para imprimir la(s) letra(s)";
                        rwmVidaLey.RadAlert(Message, 400, null, "Mensaje de error", null);
                    }
                    else if (ibImprimirLetra.ImageUrl == "~/Images/Icons/sunat.png")
                    {
                        string Message = "El documento no puede ser descargado,  aún no está aprobado por SUNAT.";
                        rwmVidaLey.RadAlert(Message, 400, null, "Mensaje de error", null);
                    }
                    else
                    {

                        decimal OpElectronica = 0;
                        OpElectronica = Convert.ToDecimal(e.CommandArgument.ToString());
                        ExportarLetra_PDF(OpElectronica);
                        
                            
                    }
                }

                if (e.CommandName == "download_file")
                {
                    FacturacionElectronicaOkWCF.WSComprobanteSoapClient oServicioOK2 = new FacturacionElectronicaOkWCF.WSComprobanteSoapClient();
                    string CadenaError;
                    FacturacionElectronicaOkWCF.ENPeticion oPeticionPDF = new FacturacionElectronicaOkWCF.ENPeticion();
                    FacturacionElectronicaOkWCF.ENRespuestaPDF oRespuestaPDF = new FacturacionElectronicaOkWCF.ENRespuestaPDF();

                    int numero = int.Parse(objFactura.Numero);
                    string strTipoComprobante = "";

                    if (objFactura.DocSunat.ToString().Length == 1)
                    {
                        strTipoComprobante = "0" + objFactura.DocSunat.ToString();
                    }

                    oPeticionPDF.Numero = numero.ToString();
                    oPeticionPDF.Serie = objFactura.Serie;
                    oPeticionPDF.TipoComprobante = strTipoComprobante;  //"01";
                    oPeticionPDF.Ruc = ((Usuario_LoginResult)Session["Usuario"]).RUC;
                    oPeticionPDF.IndicadorComprobante = 1;
                    CadenaError = "";
                    oRespuestaPDF = oServicioOK2.Obtener_PDF(oPeticionPDF, ref CadenaError);
                 
                    //ShowPdf(oRespuestaPDF.ArchivoPDF, oRespuestaPDF.NombrePDF);
                    //string filename = e.CommandArgument.ToString();
                    //string path = MapPath("~/files/" + filename);
                    //byte[] bts = System.IO.File.ReadAllBytes(path);

                    byte[] bts = oRespuestaPDF.ArchivoPDF;

                    Response.Clear();
                    Response.ClearHeaders();
                    Response.AddHeader("Content-Type", "Application/octet-stream");
                    Response.AddHeader("Content-Length", bts.Length.ToString());
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + oRespuestaPDF.NombrePDF);
                    Response.BinaryWrite(bts);
                    Response.Flush();
                    Response.End();
                }


            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;

            }
        }

        private void Exportar_PDF(decimal OpElectronica)
        {
            try
            {
                FacturacionElectronicaOkWCF.WSComprobanteSoapClient oServicioOK2 = new FacturacionElectronicaOkWCF.WSComprobanteSoapClient();
                string CadenaError;
                VBG04694Result objFactura = new VBG04694Result();
                List<VBG04694Result> lst = JsonHelper.JsonDeserialize<List<VBG04694Result>>((string)ViewState["lstFacturaElectronica"]);
                objFactura = lst.FindAll(x => x.Op == OpElectronica).Single();
                FacturacionElectronicaOkWCF.ENPeticion oPeticionPDF = new FacturacionElectronicaOkWCF.ENPeticion();
                FacturacionElectronicaOkWCF.ENRespuestaPDF oRespuestaPDF = new FacturacionElectronicaOkWCF.ENRespuestaPDF();

                int numero = int.Parse(objFactura.Numero);
                string strTipoComprobante = "";

                if (objFactura.DocSunat.ToString().Length == 1)
                {
                    strTipoComprobante = "0" + objFactura.DocSunat.ToString();
                }

                oPeticionPDF.Numero = numero.ToString();
                oPeticionPDF.Serie = objFactura.Serie;
                oPeticionPDF.TipoComprobante = strTipoComprobante;  //"01";
                oPeticionPDF.Ruc = ((Usuario_LoginResult)Session["Usuario"]).RUC;
                oPeticionPDF.IndicadorComprobante = 1;
                CadenaError = "";

                oRespuestaPDF = oServicioOK2.Obtener_PDF(oPeticionPDF, ref CadenaError);


                if(oRespuestaPDF != null)
                {
                    try
                    {
                        ShowPdf(oRespuestaPDF.ArchivoPDF, oRespuestaPDF.NombrePDF);
                    }
                    catch (Exception ex)
                    {
                        lblMensaje.Text = ex.Message;
                        lblMensaje.CssClass = "mensajeError";
                    }
                }
                else
                {
                    string Message = "El documento no puede ser descargado,  aún no está aprobado por SUNAT.";
                    rwmVidaLey.RadAlert(Message, 400, null, "Mensaje de error", null);
                }
 

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
         }

        private void ExportarLetra_PDF(decimal OpElectronica)
        {
            LetrasEmitidasWCFClient objLetrasWCF = new LetrasEmitidasWCFClient();
            List<gsLetrasElectronicas_ListarResult> listLetras = new List<gsLetrasElectronicas_ListarResult>();
            try
            {

                try
                {
 
                    listLetras = objLetrasWCF.LetrasElectronicas_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, Convert.ToInt32(OpElectronica.ToString()) ).ToList();

 
                    if (listLetras.Count > 0 )
                    {
                        
                        

                        byte[] Respuesta = CreatePDF(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).idEmpresa, OpElectronica.ToString(), listLetras);
                        string NombrePDF = "LetrasEletronicas_" + OpElectronica.ToString();

                        ShowPdf(Respuesta, NombrePDF);
                    }
                    else
                    {
                        string Message = "El documento no puede ser descargado,  aún no tiene letras generadas.";
                        rwmVidaLey.RadAlert(Message, 400, null, "Mensaje de error", null);
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
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private byte[] CreatePDF(int idEmpresa, int codUsuario, string Op, List<gsLetrasElectronicas_ListarResult> listLetras)
        {
            Document doc = new Document(PageSize.LETTER, 50, 50, 50, 50);

            using (MemoryStream output = new MemoryStream())
            {
                PdfPTable tableLayout = new PdfPTable(22);

                doc = new Document();
                doc = new Document(PageSize.LETTER, 20, 20, 20, 20);
                MemoryStream memoryStream = new MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(doc, output);
                doc.Open();
                doc.Add(Add_Content_To_PDF(tableLayout, idEmpresa, codUsuario, Op, listLetras));

                writer.CloseStream = false;
                doc.Close();
                memoryStream.Position = 0;

                return output.ToArray();
            }

        }

        private PdfPTable Add_Content_To_PDF(PdfPTable tableLayout, int idEmpresa, int codUsuario, string Op, List<gsLetrasElectronicas_ListarResult> listLetras)
        {
            string urlImagen;
            decimal OpInt = 0;
            OpInt = Convert.ToDecimal(Op);

            EmpresaWCFClient objEmpresaWCF = new EmpresaWCFClient();
            List<gsEgresosVarios_ListarCajaChicaResult> lstEgresosVarios = new List<gsEgresosVarios_ListarCajaChicaResult>();
            gsEgresosVarios_ListarCajaChicaResult EgresosVarios = new gsEgresosVarios_ListarCajaChicaResult();

            Empresa_BuscarDetalleResult objEmpresa;
            Empresa_BuscarDetalleResult[] lst = objEmpresaWCF.Empresa_BuscarDetalle(idEmpresa);
            objEmpresa = lst[0];
            urlImagen = objEmpresa.logotipo.ToString();

            float[] values = new float[22];
            values[0] = 100;
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

            values[11] = 100;
            values[12] = 100;
            values[13] = 100;
            values[14] = 100;
            values[15] = 100;
            values[16] = 100;
            values[17] = 100;
            values[18] = 100;
            values[19] = 100;
            values[20] = 100;
            values[21] = 100;

            tableLayout.SetWidths(values);        //Set the pdf headers
            tableLayout.WidthPercentage = 100;       //Set the PDF File witdh percentage

            //Add Title to the PDF file at the top
  
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/Logos/grupo.png"));

            logo.ScaleAbsolute(205, 90);
            PdfPCell imageCell = new PdfPCell(logo);
            imageCell.Colspan = 2; // either 1 if you need to insert one cell
            imageCell.Border = 0;
            imageCell.HorizontalAlignment = Element.ALIGN_LEFT;

            tableLayout = ListaLetraElectronica_PDF(tableLayout, Convert.ToInt32(OpInt), objEmpresa.razonSocial, listLetras);


            return tableLayout;
        }

        private PdfPTable ListaLetraElectronica_PDF(PdfPTable PDFCreator, int idOperacion, string NombreEmpresa, List<gsLetrasElectronicas_ListarResult> listLetras)
        {
            PdfPTable tableLayout;
            tableLayout = PDFCreator;
            LetrasEmitidasWCFClient objLetrasWCF = new LetrasEmitidasWCFClient();
            try
            {
                if (idOperacion != 0)
                {
              
 
                    foreach(gsLetrasElectronicas_ListarResult letra in listLetras.OrderBy(x=> x.ID_Amarre))
                    {
                        List<VBG01425Result> Letra = new List<VBG01425Result>();
                        EmpresaWCFClient objEmpresaWCF = new EmpresaWCFClient();
                        Empresa_BuscarDetalleResult objEmpresa;
                        string urlImagen;

                       


                        int id_Amarre = Convert.ToInt32(letra.ID_Amarre);
 
                        Letra = objLetrasWCF.LetrasElectronicas_Individual(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                                                                                ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, id_Amarre).ToList();

                        foreach (VBG01425Result row in Letra)
                        {

                            
                            //string usuario = Session["Usuario"]);

                            
                            objLetrasWCF.Registrar_LogLetrasDescargadas(
                                ((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                                Convert.ToInt32(row.ID_Letra), ((Usuario_LoginResult)Session["Usuario"]).idUsuario.ToString(),idOperacion);
                            
                            //--------------------------------------------------
                            iTextSharp.text.Font chunk0 = new iTextSharp.text.Font();
                            iTextSharp.text.Font chunk1 = new iTextSharp.text.Font();
                            iTextSharp.text.Font chunk11 = new iTextSharp.text.Font();

                            iTextSharp.text.Font chunk2 = new iTextSharp.text.Font();
                            iTextSharp.text.Font chunk3 = new iTextSharp.text.Font();

                            iTextSharp.text.Font chunk4 = new iTextSharp.text.Font();


                            chunk0 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY);
                            chunk1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA,10, 1, iTextSharp.text.BaseColor.BLACK);
                            chunk11 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK);

                            chunk2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 0, iTextSharp.text.BaseColor.BLACK);
                            chunk3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 0, iTextSharp.text.BaseColor.DARK_GRAY);

                            chunk4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, 1, iTextSharp.text.BaseColor.DARK_GRAY);

                            // ----------------------------------------------------
                            #region cabecera
                            Empresa_BuscarDetalleResult[] lst = objEmpresaWCF.Empresa_BuscarDetalle(((Usuario_LoginResult)Session["Usuario"]).idEmpresa);
                            objEmpresa = lst[0];
                            urlImagen = objEmpresa.logotipo.ToString();


                            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/Logos/grupo.png"));

                            if (((Usuario_LoginResult)Session["Usuario"]).idEmpresa == 1 || ((Usuario_LoginResult)Session["Usuario"]).idEmpresa == 3)
                            {
                               logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/Logos/L_sil.jpg"));
                            }
                            if (((Usuario_LoginResult)Session["Usuario"]).idEmpresa == 2)
                            {
                               logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/Logos/L_neo.jpg"));
                            }
                            if (((Usuario_LoginResult)Session["Usuario"]).idEmpresa == 6)
                            {
                                logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/Logos/L_inatec.jpg"));
                            }

                            //-------------------------------------------------------

                            logo.ScalePercent(13); 
                            PdfPCell imageCell = new PdfPCell(logo);
                            imageCell.Colspan = 4; // either 1 if you need to insert one cell
                            imageCell.Rowspan = 3; 
                           
                            imageCell.BorderWidthBottom = 0;
                            imageCell.BorderWidthLeft = 0;
                            imageCell.BorderWidthRight = 0;
                            imageCell.BorderWidthTop = 0;

                            imageCell.HorizontalAlignment = Element.ALIGN_LEFT;

                            string mensaje = "";
                            mensaje = @"
                                        CLAUSULAS ESPECIALES: 
                                        (1) En caso de mora esta Letra de Cambio generará las tasas de interés compensatorio más altas             
                                        que la Ley permita a su último Tenedor.  
                                        (2)	El plazo de su vencimiento podrá ser prorrogado por el Tenedor, por el plazo que este señale,  
                                        sin que sea necesaria la intervención de obligado principal ni de los solidarios.  
                                        (3)	Esta Letra de Cambio no requiere ser protestada por falta de pago.
                                        (4)	Su importe debe ser pagado sólo en la misma moneda que expresa este título valor. 




                                        Nombre del Representante(s)                                    Nombre del Representante(s)
                                        D.O.I / R.U.C.                                                              D.O.I / R.U.C.
                                        ";

                            
                            string CodigoBarra = row.ID_Letra.ToString();
                            RadBarcode telerikRB = new RadBarcode();
                            telerikRB.Type = BarcodeType.Code128;
                            telerikRB.Font.Size = 16;
                            //telerikRB.Height = new Unit(75);
                            //telerikRB.Width = new Unit(150);
                            telerikRB.Text = row.ID_Letra.ToString();
                            telerikRB.GetImage();
                            iTextSharp.text.Image logobarra = iTextSharp.text.Image.GetInstance(telerikRB.GetImage(),new BaseColor(Color.White));

                            PdfPCell imageCellbarra = new PdfPCell(logobarra);
                            imageCellbarra.Colspan = 8; // either 1 if you need to insert one cell
                            imageCellbarra.Rowspan = 1;

                            imageCellbarra.BorderWidthBottom = 0;
                            imageCellbarra.BorderWidthLeft = 0;
                            imageCellbarra.BorderWidthRight = 0;
                            imageCellbarra.BorderWidthTop = 0;

                            imageCellbarra.HorizontalAlignment = Element.ALIGN_RIGHT;
                            

                            #endregion
                            // fila 1
                            tableLayout.AddCell(new PdfPCell(new Phrase(mensaje, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA,6, 0, iTextSharp.text.BaseColor.DARK_GRAY)))
                            { Colspan = 4, Rotation = 90, Rowspan = 23, Border = 0, PaddingBottom = 2, HorizontalAlignment = Element.ALIGN_LEFT });


                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY)))
                            { Colspan = 10, Border = 0, PaddingBottom = 3, HorizontalAlignment = Element.ALIGN_CENTER });

                            tableLayout.AddCell(imageCellbarra);

                            //--------------------------------------------------
                            // Nivel 0

                            // fila 2
                            //tableLayout.AddCell(new PdfPCell(new Phrase("", chunk1)) { Colspan = 2, Border=0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 0, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                            //4 texto
                            tableLayout.AddCell(imageCell);//4=> //8
                            
                            //tableLayout.AddCell(new PdfPCell(new Phrase("", chunk1)) { Colspan = 3, Border = 0,  HorizontalAlignment = Element.ALIGN_CENTER, Padding = 0, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase(objEmpresa.razonSocial.ToUpper() , chunk1)) { Colspan = 10, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding =0, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("", chunk1)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 0, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("" , chunk1)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding =0, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                            // fila 3
                            //tableLayout.AddCell(new PdfPCell(new Phrase("", chunk2)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding =0, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            //tableLayout.AddCell(new PdfPCell(new Phrase("", chunk2)) { Colspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 0, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase(objEmpresa.direccion, chunk2)) { Colspan = 10, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding =0, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("", chunk2)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 0, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("", chunk2)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 0, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                            // fila 4
                            //tableLayout.AddCell(new PdfPCell(new Phrase("", chunk2)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding =1, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            //tableLayout.AddCell(new PdfPCell(new Phrase("", chunk2)) { Colspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding =1, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("TELF.: 617-3300 - FAX: 617-3311", chunk2)) { Colspan = 10, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding =1, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase(row.ID_Letra.ToString(), chunk2)) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding =1, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("RUC: " + row.BDRuc, chunk2)) { Colspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, BackgroundColor = iTextSharp.text.BaseColor.WHITE });



                            // fila 5
                            // Nivel 1
                            //tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk0)) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                            tableLayout.AddCell(new PdfPCell(new Phrase("NUMERO", chunk11)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("REF. DEL GIRADOR", chunk11)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("F. DE GIRO", chunk11)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("LUGAR DE GIRO", chunk11)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("F. DE VENCIMIENTO", chunk11)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("IMPORTE", chunk11)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                            // fila 6
                            //tableLayout.AddCell(new PdfPCell(new Phrase("", chunk0)) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                            tableLayout.AddCell(new PdfPCell(new Phrase(row.Transaccion, chunk2)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase(row.DocumentosCanjeados, chunk2)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase(row.Emision.ToShortDateString(), chunk2)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase(row.Acep_DirDepartamento, chunk2)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase(row.Vcmto.ToShortDateString(), chunk2)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase(decimal.Round(row.Importe, 2).ToString() , chunk2)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                            // fila 7
                            // Nivel 2

                            //tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk0)) { Colspan = 2, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });
                            tableLayout.AddCell(new PdfPCell(new Phrase("Por esta LETRA DE CAMBIO, se servirá(n) pagar incondicionalmente a la orden de: ", chunk3)) { Colspan = 12, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 4, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase(row.BDNombre.ToString(), chunk3)) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 4, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("", chunk3)) { Colspan = 2, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });

                            // Nivel 3
                            // fila 8
                            string ImporteLetras = enletras(row.Importe.ToString());
                            ImporteLetras = ImporteLetras + " " + row.MonedaNombre.ToString(); 

                            //tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk1)) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                            tableLayout.AddCell(new PdfPCell(new Phrase("La cantidad de ", chunk1)) { Colspan = 18, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                            // fila 9
                            //tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk2)) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                            tableLayout.AddCell(new PdfPCell(new Phrase(ImporteLetras, chunk2)) { Colspan = 18, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                            // Nivel 4
                            // fila 9
                            //tableLayout.AddCell(new PdfPCell(new Phrase("", chunk1)) { Colspan = 2, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });
                            tableLayout.AddCell(new PdfPCell(new Phrase("En el Siguiente lugar de pago, o con cargo en la cuenta corriente del Banco ", chunk1)) { Colspan = 16, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding =4, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("", chunk1)) { Colspan = 2, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });

                            // Nivel 5
                            // fila 10
                            //tableLayout.AddCell(new PdfPCell(new Phrase("", chunk1)) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                            tableLayout.AddCell(new PdfPCell(new Phrase("Aceptante: ", chunk1)) { Colspan = 12, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("BANCO", chunk1)) { Colspan = 6, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                            // fila 11
                            //tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk2)) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                            tableLayout.AddCell(new PdfPCell(new Phrase(row.RucAceptante, chunk2)) { Colspan = 12, BorderWidthBottom = 0, HorizontalAlignment = Element.ALIGN_LEFT,  Padding = 2,  BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk2)) { Colspan = 6, BorderWidthBottom = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                            // fila 12
                            //tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk2)) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                            tableLayout.AddCell(new PdfPCell(new Phrase(row.No_Aceptante, chunk2)) { Colspan = 12, BorderWidthBottom = 0, BorderWidthTop=0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk2)) { Colspan = 6, BorderWidthBottom = 0, BorderWidthTop = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                            // fila 13
                            //tableLayout.AddCell(new PdfPCell(new Phrase("", chunk2)) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                            tableLayout.AddCell(new PdfPCell(new Phrase(row.DireccionAceptante, chunk2)) { Colspan = 12,   BorderWidthTop = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("", chunk2)) { Colspan = 6,   BorderWidthTop = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                            // Nivel 5
                            // fila 14
                            //tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk1)) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                            tableLayout.AddCell(new PdfPCell(new Phrase("Aval Permanente ", chunk1)) { Colspan = 12, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("NUMERO DE CUENTA", chunk1)) { Colspan = 6, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                            // fila 15
                            //tableLayout.AddCell(new PdfPCell(new Phrase("", chunk2)) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk2)) { Colspan = 12,   HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk2)) { Colspan = 6,   HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                            // Nivel 6
                            // fila 16
                            //tableLayout.AddCell(new PdfPCell(new Phrase("", chunk1)) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                            tableLayout.AddCell(new PdfPCell(new Phrase("Domicilio ", chunk1)) { Colspan = 8, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk1)) { Colspan = 10, Border=0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                            // fila 17
                            //tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk2)) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk2)) { Colspan = 8, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("Firma", chunk2)) { Colspan = 10, Border=0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                            //filq18
                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk2)) { Colspan = 8, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("", chunk2)) { Colspan = 10, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk2)) { Colspan = 8, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("", chunk2)) { Colspan = 10, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                            // Nivel 7
                            // fila 19
                            //tableLayout.AddCell(new PdfPCell(new Phrase("", chunk1)) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                            tableLayout.AddCell(new PdfPCell(new Phrase("D.O.I", chunk1)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("Teléfono", chunk1)) { Colspan = 3 , HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("Firma", chunk1)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("Nombre del Representante Legal:", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 10, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                            // fila 19
                            //tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk2)) { Colspan =2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk2)) { Colspan =2, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk2)) { Colspan =3, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk2)) { Colspan =3, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                            tableLayout.AddCell(new PdfPCell(new Phrase("D.O.I", chunk2)) { Colspan = 10, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                            // fila 20
                            //tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk2)) { Colspan = 2, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });
                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk2)) { Colspan = 18, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });

                            // fila 21
                            //tableLayout.AddCell(new PdfPCell(new Phrase(" ", chunk2)) { Colspan = 2, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });
                            PdfPCell cellGastos = new PdfPCell(new Phrase(" ", chunk2)) { Colspan = 22, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, Border = 1, BorderColor = iTextSharp.text.BaseColor.BLACK, BackgroundColor = iTextSharp.text.BaseColor.WHITE };
                            cellGastos.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER; // | Rectangle.TOP_BORDER;
                            cellGastos.BorderWidthBottom = 1f;
                            cellGastos.BorderWidthTop = 0f;
                            cellGastos.PaddingBottom = 1f;
                            cellGastos.PaddingLeft = 0f;
                            cellGastos.PaddingTop = 0f;
                            tableLayout.AddCell(cellGastos);

                        }


                    }

   

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tableLayout;
        }


        public string enletras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try

            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "/100 ";
            }

            res = toText(Convert.ToDouble(entero)) + dec;
            return res;
        }

        private string toText(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;

        }


        private void ShowPdf(byte[] strS, string Nombre_PDF)
        {

            try
            {
                //string fecha = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                string fileName = Nombre_PDF;
                //if (!File.Exists(rutaArchivo)) return;

                var file = strS;
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ClearHeaders();
                System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
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
                lblMensaje.CssClass = "mensajeError";
            }
        }

        void LlenarEmpresa(ref string NroBanco)
        {
            EmpresaWCFClient objEmpresaWCFC = new EmpresaWCFClient();
            AgendaWCFClient objAgendaWCF = new AgendaWCFClient();
            Agenda_BuscarEmpresaResult objEmpresaGenesys = new Agenda_BuscarEmpresaResult();
            List<Agenda_BuscarEmpresaResult> lstEmpresaGenesys = new List<Agenda_BuscarEmpresaResult>();
            List<Empresa_ComboBoxResult> lstComboGenesys = new List<Empresa_ComboBoxResult>();
            int idEmpresa;
            string id_agenda = "";
            
            try
            {
                idEmpresa = ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa;
                lstComboGenesys = objEmpresaWCFC.Empresa_ComboBox().ToList();

                var query_Empresa = from c in lstComboGenesys
                                    where c.idEmpresa == idEmpresa
                                    select new
                                    {
                                        c.RUC
                                    };
                var QueryRUC = from cust in query_Empresa
                               select cust.RUC;

                foreach (string s in QueryRUC)
                {
                    id_agenda = s;
                }

                lstEmpresaGenesys = objAgendaWCF.Agenda_BuscarEmpresa(idEmpresa, ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, id_agenda).ToList();
                objEmpresaGenesys = lstEmpresaGenesys[0];

                // Llenar WS 
                objEmpresa.Ruc = objEmpresaGenesys.RUC.ToString(); // "20191503482"; //"20112811096"; //
                objEmpresa.RazonSocial = objEmpresaGenesys.AgendaNombre.ToString(); //   "SILVESTRE PERU S.A.C.";  //""; // \"TCI\" TRANSPORTE CONFIDENCIAL DE INFORMACION
                objEmpresa.CodigoTipoDocumento = "6";
                objEmpresa.CodDistrito = objEmpresaGenesys.CodigoSunat.ToString(); // "150131";
                objEmpresa.Correo = objEmpresaGenesys.Cliente_Correo;

                NroBanco = objEmpresaGenesys.NroCuentaBN; 
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void rmyPeriodo_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            //rmyPeriodo.SelectedDate = DateTime.Now;
            dpInicio = rmyPeriodo.SelectedDate.Value.AddDays(-rmyPeriodo.SelectedDate.Value.Day + 1);
            dpFinal = dpInicio.AddMonths(+1).AddDays(-1);

            //lblDate.Text = "";
            //TipoDocumento_Cargar();
        }

        protected void btnLetras_Click(object sender, EventArgs e)
        {
            DateTime FechaInicio;
            DateTime FechaFin;
            string strFechaInicioL;
            string strFechaFinL;

            int idOpFactura = 0;
            int count = 0;
            int intFechaInicio= 0;
            int intFechaFin = 0;
            int intFechaInicioNew = 0;
            int intFechaFinNew = 0;

            char Cero = Convert.ToChar("0");

            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            DataTable tblDocumentos = new DataTable();
            DataColumn col1 = new DataColumn("OP_OV");
            DataColumn col2 = new DataColumn("OP_DOC");
            tblDocumentos.Columns.Add(col1);
            tblDocumentos.Columns.Add(col2);

            try
            {

                foreach (GridItem rowitem in grdFacturaElectronica.MasterTableView.Items)
                {
                   

                    GridDataItem dataitem = (GridDataItem)rowitem;
                    TableCell cell = dataitem["CheckColumn"];
                    CheckBox checkBox = (CheckBox)cell.Controls[0].FindControl("Check");
                    if (checkBox.Checked == true && checkBox.Enabled == true)
                    {
                        count++;
                        //int Op_OV = Convert.ToInt32(dataitem.GetDataKeyValue("OP_OV").ToString());
                        int Op_OV = Convert.ToInt32(dataitem["OP_OV"].Text);
                        int diascredito = Convert.ToInt32(dataitem["DiasCredito"].Text.Trim() == string.Empty ? "0" : dataitem["DiasCredito"].Text.Trim());


                        DataRow linea = tblDocumentos.NewRow();
                        linea[0] = Op_OV;
                        tblDocumentos.Rows.Add(linea);
                        ViewState["tblDocumentos"] = tblDocumentos;
 

                        FechaInicio = Convert.ToDateTime(dataitem["Fecha"].Text);
                        //FechaFin = Convert.ToDateTime(dataitem["Vencimiento"].Text);
                        FechaFin = FechaInicio.AddDays(diascredito);



                        string strFechaInico = FechaInicio.Year.ToString() + FechaInicio.Month.ToString().PadLeft(2, Cero) + FechaInicio.Day.ToString().PadLeft(2, Cero);
                        string strFechaFin = FechaFin.Year.ToString() + FechaFin.Month.ToString().PadLeft(2, Cero) + FechaFin.Day.ToString().PadLeft(2, Cero);

                        intFechaInicioNew = Convert.ToInt32(strFechaInico);
                        intFechaFinNew = Convert.ToInt32(strFechaFin);

                        if (count==1)
                        {
                            intFechaInicio = intFechaInicioNew;
                            intFechaFin = intFechaFinNew; 
                        }

                        if(intFechaInicio > intFechaInicioNew)
                        {
                            intFechaInicio = intFechaInicioNew;
                        }

                        if (intFechaFin > intFechaFinNew)
                        {
                            intFechaFin = intFechaFinNew;
                        }

                    }
                }

                if(count>0)
                {
                    strFechaInicioL = intFechaInicio.ToString();
                    strFechaFinL = intFechaFin.ToString();
                    idOpFactura = 0;

                    var RUC = "000";
                    if (acbCliente.Text.Split('-').Count() >= 2)
                        RUC = acbCliente.Text.Split('-')[0];
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowInsertFormLetras(" + strFechaInico + "," + strFechaFin + "," + intOP_OV + "," + RUC + ");", true);

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowInsertFormLetras(" + strFechaInicioL + "," + strFechaFinL + "," + idOpFactura + "," + RUC + ");", true);
                }
                //{
                //    string Message = "Seleccionar al menos un documento, para planificar.";
                //    rwmVidaLey.RadAlert(Message, 400, null, "Mensaje de error", null);
                //}


            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
 
        protected void ramPedidoMng_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if (e.Argument.Split('(')[0].Trim() == "RebindAndNavigateLetras")
                {
                    string LetrasTXT;
                    LetrasTXT = (string)Session["objLetras"];
                    //lblLetras.Text = LetrasTXT;

                    //List<gsPedidos_FechasLetrasSelectResult> lstFechas = new List<gsPedidos_FechasLetrasSelectResult>();
                    gsPedidos_FechasLetrasSelectResult[] lstFechas = ((List<gsPedidos_FechasLetrasSelectResult>)Session["lstFechasNew"]).ToArray() ;

                    OrdenVentaWCFClient objOrdenVentaWCF = new OrdenVentaWCFClient();

                    DataTable tblDocumentos = new DataTable();
                    tblDocumentos = (DataTable)ViewState["tblDocumentos"];


                    objOrdenVentaWCF.OrdenVenta_Registrar_Fechas(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                                                                           ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, tblDocumentos , lstFechas);

                    lblMensaje.Text = "Se planifico letras al pedido.";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                rwmVidaLey.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }


 
    }
}