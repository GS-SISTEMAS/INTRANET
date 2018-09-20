using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.DocumentoWCF;
using Telerik.Web.UI;

namespace GS.SISGEGS.Web.Mantenimiento.IntranetGS.Generales
{
    public partial class frmFamDocumentos : System.Web.UI.Page
    {
        private void RefrescarDocumentos()
        {
            List<ListarDocumentosResult> Lista = new List<ListarDocumentosResult>();
            try {
                DocumentoWCFClient cliente = new DocumentoWCFClient();
                int IdEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;

                Lista = cliente.ListarDocumentos(IdEmpresa, CodigoUsuario).ToList();

                this.lstDocumentos.DataSource = Lista;
                this.DataBind();

            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private void RefrescarGrillas() {
            List<ListarDocumentosFamiliaResult> Lista = new List<ListarDocumentosFamiliaResult>();
            List<ListarDocumentosFamiliaResult> ListaNoC = new List<ListarDocumentosFamiliaResult>();
            try {
                DocumentoWCFClient cliente = new DocumentoWCFClient();
                int IdEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;

                Lista = cliente.ListarDocumentosFamilia(IdEmpresa, CodigoUsuario, 1 ).ToList();
                ListaNoC= cliente.ListarDocumentosFamilia(IdEmpresa, CodigoUsuario, 0).ToList();

                this.rgComercial.DataSource = Lista;
                this.rgComercial.DataBind();

                this.rgNoComercial.DataSource = ListaNoC;
                this.rgNoComercial.DataBind();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");


            try
            {
                if (!Page.IsPostBack) {

                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    RefrescarDocumentos();
                    RefrescarGrillas();
                }
            }
            catch (Exception ex) {
                this.lblmensaje.Text = ex.Message;
            }
        }

        protected void btnComercial_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try {

                if (lstDocumentos.CheckedItems.Count == 0)
                {
                    this.lblmensaje.Text = "No ha seleccionado ningun documento";
                    return;
                }
                else {
                    this.lblmensaje.Text = "";
                }

                DocumentoWCFClient cliente = new DocumentoWCFClient();

                int IdEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;

                //Recorrer la lista

                List<RadListBoxItem> seleccionado = this.lstDocumentos.CheckedItems.ToList();

                foreach (RadListBoxItem item in seleccionado) {
                    cliente.RegistrarDocumentoFamilia(IdEmpresa, CodigoUsuario, 1, Convert.ToInt32(item.Value));
                }

                //cliente.RegistrarDocumentoFamilia(IdEmpresa, CodigoUsuario, 0, 0);

                //this.lblmensaje.Text = "Enviado desde el servidor";
                RefrescarDocumentos();
                RefrescarGrillas();
            }
            catch (Exception ex) {
                this.lblmensaje.Text = ex.Message;
            }

            
        }

        protected void btnNoComercial_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {

                if (lstDocumentos.CheckedItems.Count == 0)
                {
                    this.lblmensaje.Text = "No ha seleccionado ningun documento";
                    return;
                }
                else
                {
                    this.lblmensaje.Text = "";
                }

                DocumentoWCFClient cliente = new DocumentoWCFClient();

                int IdEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;

                //Recorrer la lista

                List<RadListBoxItem> seleccionado = this.lstDocumentos.CheckedItems.ToList();

                foreach (RadListBoxItem item in seleccionado)
                {
                    cliente.RegistrarDocumentoFamilia(IdEmpresa, CodigoUsuario, 0, Convert.ToInt32(item.Value));
                }

                //cliente.RegistrarDocumentoFamilia(IdEmpresa, CodigoUsuario, 0, 0);

                //this.lblmensaje.Text = "Enviado desde el servidor";
                RefrescarDocumentos();
                RefrescarGrillas();
            }
            catch (Exception ex)
            {
                this.lblmensaje.Text = ex.Message;
            }
        }

        protected void rgComercial_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");


            try
            {
                if (e.CommandName == "Eliminar")
                {
                    decimal codigo = Convert.ToDecimal(e.CommandArgument);
                    int IdEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                    int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;

                    DocumentoWCFClient cliente = new DocumentoWCFClient();
                    cliente.EliminarDocumentoFamilia(IdEmpresa, CodigoUsuario, codigo);
                }
            }
            catch (Exception ex)
            {
                lblmensaje.Text = ex.Message;
            }
            finally {
                RefrescarDocumentos();
                RefrescarGrillas();
            }

            
        }

        protected void rgNoComercial_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");
            
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    decimal codigo = Convert.ToDecimal(e.CommandArgument);
                    int IdEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                    int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;

                    DocumentoWCFClient cliente = new DocumentoWCFClient();
                    cliente.EliminarDocumentoFamilia(IdEmpresa, CodigoUsuario, codigo);
                }
            }
            catch (Exception ex)
            {
                lblmensaje.Text = ex.Message;
            }
            finally
            {
                RefrescarDocumentos();
                RefrescarGrillas();
            }
        }
    }
}