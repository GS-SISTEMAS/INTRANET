using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.Web.Helpers;


using GS.SISGEGS.Web.OrdenCompraWCF;
using GS.SISGEGS.DM;
using System.IO;
using Telerik.Web.UI;
using System.Data;
using System.Net;
using System.Configuration;


namespace GS.SISGEGS.Web.Compras.OC
{
    public partial class frmOCDoc : System.Web.UI.Page
    {
        int _idSeguimiento = 0;
        string _archivo = string.Empty;
        OrdenCompraWCFClient objOrdenCompraWCF = new OrdenCompraWCFClient();

        #region Procedimientos
        private void BuscarDocumentos()
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                _idSeguimiento = Convert.ToInt32(Session["IdSeguimiento"]);
                objOrdenCompraWCF = new OrdenCompraWCFClient();

                gvwOCDocs.DataSource = objOrdenCompraWCF.DocumentosSegImportacion_Seleccionar(
                    ((Usuario_LoginResult)Session["Usuario"]).idEmpresa, 
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, _idSeguimiento);

                gvwOCDocs.DataBind();
                
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void RegistrarDocumento(string savePath, string fileName)
        {
            objOrdenCompraWCF = new OrdenCompraWCFClient();
            _idSeguimiento = Convert.ToInt32(Session["IdSeguimiento"]);

            var usuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
            objOrdenCompraWCF.DocumentosSegImportacion_Registrar(
                ((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                    _idSeguimiento, fileName, savePath);

            
            lblMensaje.Text = "El archivo se cargó correctamente.";

            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind();", true);
            BuscarDocumentos();
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            if (!ConnectionHelpers.CheckForInternetConnection())
                throw new ArgumentException("ERROR: Revisar su conexión a internet.");

            try
            {
                if (!Page.IsPostBack)
                {
                    _idSeguimiento = int.Parse((Request.QueryString["Id_SegImp"]));
                    Session["IdSeguimiento"] = _idSeguimiento;
                    Session["archivo"] = string.Empty;
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    lblTitulo.Text = "Listado de Archivos.";
                    Page.Title = "Gestor de Archivos";
                    
                    BuscarDocumentos();


                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        protected void RadAsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            _idSeguimiento = Convert.ToInt32(Session["IdSeguimiento"]);
            string fullPath;
            
            var ruta = ConfigurationSettings.AppSettings.Get("RutaDocumentosOC");
            string savePath = ruta + "\\"+ ((Usuario_LoginResult)Session["Usuario"]).nombreComercial.ToString() +"\\"+  _idSeguimiento + "\\";

            List<DataTable> tablas = new List<DataTable>();
            DataTable tabla = new DataTable();

            try
            {

                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

               

                string strFecha = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_";
                var fileName = e.File.GetName();
                fullPath = Path.Combine(savePath, fileName);

                e.File.SaveAs(fullPath);

                //Destino = fullPath;


                Session["savePath"] = savePath;
                Session["fileName"] = fileName;

                RegistrarDocumento(savePath, fileName);
                
                //RadAsyncUpload1.Dispose();

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }



        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            BuscarDocumentos();
        }

        protected void gvwOCDocs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var link = (HyperLink)e.Row.FindControl("hlDescarga");
                var document = e.Row.Cells[0].Text;

                if (link != null)
                {
                    _idSeguimiento = Convert.ToInt32(Session["IdSeguimiento"]);
                    //string savePath = ruta + "\\"+ ((Usuario_LoginResult)Session["Usuario"]).nombreComercial.ToString() +"\\"+  _idSeguimiento + "\\";
                    //link.NavigateUrl = "~/Registros/Marca/Documentos/" + idMarca + "/" + document;
                    //ConfigurationSettings.AppSettings.Get("RutaDocumentosOC")


                    link.NavigateUrl = "~/Compras/OC/Documentos/" + ((Usuario_LoginResult)Session["Usuario"]).nombreComercial.ToString() + "/" + _idSeguimiento + "/"+ document;

                    //link.NavigateUrl = ConfigurationSettings.AppSettings.Get("RutaDocumentosOC") + "\\" + 
                    //    ((Usuario_LoginResult)Session["Usuario"]).nombreComercial.ToString() + "\\" + _idSeguimiento + "\\" + document;
                    //link.NavigateUrl = "www.google.com";

                }
            }
        }

        protected void gvwOCDocs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                _idSeguimiento = Convert.ToInt32(Session["IdSeguimiento"]);
                string fullPath, filename;

                var ruta = ConfigurationSettings.AppSettings.Get("RutaDocumentosOC");
                string DelPath = ruta + "\\" + ((Usuario_LoginResult)Session["Usuario"]).nombreComercial.ToString() + "\\" + _idSeguimiento + "\\";

                try
                {
                    if (gvwOCDocs.Rows.Count == 0)
                        return;
                    objOrdenCompraWCF = new OrdenCompraWCFClient();

                    filename = e.CommandArgument.ToString();
                    fullPath = Path.Combine(DelPath, filename);
                    objOrdenCompraWCF.DocumentosSegImportacion_Eliminar(
                        ((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, _idSeguimiento, filename);
                    if ((System.IO.File.Exists(fullPath)))
                        System.IO.File.Delete(fullPath);

                    BuscarDocumentos();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = ex.Message;
                    lblMensaje.CssClass = "mensajeError";
                }
            }
        }
    }
}