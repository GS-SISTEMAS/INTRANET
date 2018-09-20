using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using GS.SISGEGS.Web.Helpers;
using System.Web.UI.WebControls;
using GS.SISGEGS.Web.MarcasWCF;
using GS.SISGEGS.DM;
using System.IO;
using Telerik.Web.UI;
using System.Data;
using System.Net;
using System.Configuration;
namespace GS.SISGEGS.Web.Contratos.Reportes
{
    public partial class frmMarcasDocs : System.Web.UI.Page
    {
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
                    var idMarca = int.Parse((Request.QueryString["idMarca"]));

                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    lblTitulo.Text = "Documentos Marca";
                    Page.Title = "Documentos Marca";
                    TipoDoc_Cargar();
                    BuscarDocumentos();


                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
         }

        private void TipoDoc_Cargar()
        {
            try
            {
                MarcasWCFClient objMarcaWCF = new MarcasWCFClient();
                TipoDocumentoMarca_ListarResult objTipoDoc = new TipoDocumentoMarca_ListarResult();
                List<TipoDocumentoMarca_ListarResult> lstTipoDoc;

                lstTipoDoc = objMarcaWCF.TipoDocumentoMarca_Listar().ToList();

                lstTipoDoc.Insert(0, objTipoDoc);
                objTipoDoc.idTipoDocumentoMarca = 0;
                objTipoDoc.nombreTipoDocumento = "Todos";


                var lstTipoDocs = from x in lstTipoDoc
                                  select new
                                  {
                                      x.idTipoDocumentoMarca,
                                      DisplayID = String.Format("{0}", x.idTipoDocumentoMarca),
                                      DisplayField = String.Format("{0}", x.nombreTipoDocumento)
                                  };

                cboTipoDocumento.DataSource = lstTipoDocs;
                cboTipoDocumento.DataTextField = "DisplayField";
                cboTipoDocumento.DataValueField = "DisplayID";
                cboTipoDocumento.DataBind();

            }
            catch (Exception ex)
            {
            }

        }

        protected void RadAsyncUpload1_FileUploaded1(object sender, FileUploadedEventArgs e)
        {
            var idMarca = int.Parse(Request.QueryString["idMarca"]);

            string fullPath;
            string Destino = "";
            var ruta = ConfigurationSettings.AppSettings.Get("RutaDocumentosMarca");
            string savePath = ruta  + idMarca+ "\\";

            List<DataTable> tablas = new List<DataTable>();
            DataTable tabla = new DataTable();

            try
            {

                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                if (cboTipoDocumento.SelectedIndex == 0)
                {
                    throw new Exception("No ha seleccionado ningun tipo de documento.");
                }

                string strFecha = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_";
                var fileName = cboTipoDocumento.SelectedItem.Text + "_" + e.File.GetName();
                fullPath = Path.Combine(savePath, fileName);

                e.File.SaveAs(fullPath);

                Destino = fullPath;


                ViewState["savePath"] = savePath;
                ViewState["fileName"] = fileName;

                RegistrarDocumento(savePath,fileName);

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarDocumentos();
        }

        private void RegistrarDocumento(string savePath, string fileName)
        {
            var idMarca = int.Parse(Request.QueryString["idMarca"]);

            MarcasWCFClient objMarcaWCF = new MarcasWCFClient();
            var idTipoDoc = int.Parse(cboTipoDocumento.SelectedValue);
            var usuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
            objMarcaWCF.DocumentoMarca_Registrar(idMarca, idTipoDoc, fileName, savePath, usuario);
            lblMensaje.Text = "El archivo se cargó correctamente.";

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind();", true);
        
        }
        
        private void BuscarDocumentos() {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                var idMarca = int.Parse((Request.QueryString["idMarca"]));
                var idTipoDocumento = int.Parse(cboTipoDocumento.SelectedValue);

                MarcasWCFClient objMarcasWCF = new MarcasWCFClient();

                grdGeneralMarcas.DataSource = objMarcasWCF.DocumentoMarca_Listar(idMarca, idTipoDocumento);
                grdGeneralMarcas.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdGeneralMarcas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var link = (HyperLink)e.Row.FindControl("hlDescarga");
                var document = e.Row.Cells[2].Text;

                if (link != null)
                {
                    var idMarca =(Request.QueryString["idMarca"]);

                    link.NavigateUrl = "~/Registros/Marca/Documentos/" + idMarca + "/" + document;
                   
                }
            }
        }

    }
}