using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.UsuarioWCF;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.Helpers;

namespace GS.SISGEGS.Web
{
    public partial class frmLogin : System.Web.UI.Page
    {
        private void Empresa_ComboBox() {
            EmpresaWCFClient objEmpresaWCFC;
            try {
                objEmpresaWCFC = new EmpresaWCFClient();
                cboEmpresa.DataSource = objEmpresaWCFC.Empresa_ComboBox();
                cboEmpresa.DataTextField = "nombreComercial";
                cboEmpresa.DataValueField = "idEmpresa";
                cboEmpresa.DataBind();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try {
                if (!Page.IsPostBack) {
                    Session.Clear();
                    Empresa_ComboBox();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            UsuarioWCFClient objUsuarioWCF = new UsuarioWCFClient();
            Usuario_AutenticarResult objUsuario = new Usuario_AutenticarResult();
            try
            {
                if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtContrasena.Text))
                    throw new ArgumentException("Se debe ingresar un usuario y/o contraseña.");

                objUsuario = objUsuarioWCF.Usuario_Autenticar(int.Parse(cboEmpresa.SelectedValue), txtUsuario.Text, txtContrasena.Text);
                if (!objUsuario.activo)
                    throw new ArgumentException("El usuario ha sido bloqueado o eliminado. Comunicarse con el área de sistemas");

                if (!(bool)objUsuario.cambioPassword)
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowPasswordChange('" +
                        JsonHelper.JsonSerializer(objUsuario) + "');", true);
                else
                    FormsAuthentication.RedirectFromLoginPage(objUsuario.idUsuario.ToString(), true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void ramLogin_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            try
            {
                FormsAuthentication.RedirectFromLoginPage(e.Argument, true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}