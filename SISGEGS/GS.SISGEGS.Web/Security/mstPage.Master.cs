using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.MenuWCF;
using GS.SISGEGS.Web.UsuarioWCF;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.Web.Security
{
    public partial class mstPage : System.Web.UI.MasterPage
    {
        private void Usuario_Login(int idUsuario) {
            UsuarioWCFClient objUsuarioWCF;
            Usuario_LoginResult objUsuario;
            try
            {
                objUsuarioWCF = new UsuarioWCFClient();
                objUsuario = new Usuario_LoginResult();

                objUsuario = objUsuarioWCF.Usuario_Login(idUsuario);
                lblNombre.Text = objUsuario.nombres.ToUpper();
                lblPerfil.Text = objUsuario.nombrePerfil.ToUpper();
                imgLogo.ImageUrl = objUsuario.logotipo;

                Menu_Cargar(objUsuario.idPerfil);
                Session["Usuario"] = objUsuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Menu_Cargar(int idPerfil) {
            MenuWCFClient objMenuWCF;
            try {
                objMenuWCF = new MenuWCFClient();
                rmMenuPrincipal.DataSource = objMenuWCF.Menu_CargarInicio(idPerfil);
                rmMenuPrincipal.DataTextField = "nombreMenu";
                rmMenuPrincipal.DataNavigateUrlField = "url";
                rmMenuPrincipal.DataFieldID = "codigo";
                rmMenuPrincipal.DataFieldParentID = "codigoPadre";
                rmMenuPrincipal.DataBind();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try {
                if (!Page.IsPostBack) {
                    Usuario_Login(Int32.Parse(Context.User.Identity.Name));
                }
            }
            catch (Exception ex) {
                Session.Clear();
            }
        }
    }
}