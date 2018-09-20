using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.DM;
using GS.SISGEGS.Web.LoginWCF;

namespace GS.SISGEGS.Web.Security
{
    public partial class mstPrincipal : System.Web.UI.MasterPage
    {
        private void Usuario_Login(int idUsuario)
        {
            LoginWCFClient objLoginWCF;
            Usuario_LoginResult objUsuario;
            try
            {
                objLoginWCF = new LoginWCFClient();
                objUsuario = new Usuario_LoginResult();

                objUsuario = objLoginWCF.Usuario_Login(idUsuario);
                lblNombre.Text = objUsuario.nombres.ToUpper();
                lblPerfil.Text = objUsuario.nombrePerfil.ToUpper();
                //imgLogo.ImageUrl = objUsuario.logotipo;

                Menu_Cargar(objUsuario.idEmpresa, objUsuario.codigoUsuario, objUsuario.idPerfil);
                Session["Usuario"] = objUsuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Menu_Cargar(int idEmpresa, int codigoUsuario, int idPerfil)
        {
            LoginWCFClient objLoginWCF;
            VBG00004Result objEmpresa = new VBG00004Result();
            try
            {
                objLoginWCF = new LoginWCFClient();
                rmMenuPrincipal.DataSource = objLoginWCF.Menu_CargarInicio(idEmpresa, codigoUsuario, idPerfil, ref objEmpresa);
                rmMenuPrincipal.DataTextField = "nombreMenu";
                rmMenuPrincipal.DataNavigateUrlField = "url";
                rmMenuPrincipal.DataFieldID = "codigo";
                rmMenuPrincipal.DataFieldParentID = "codigoPadre";
                rmMenuPrincipal.DataBind();
                Session["Empresa"] = objEmpresa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    Usuario_Login(Int32.Parse(Context.User.Identity.Name));
                }
            }
            catch (Exception ex)
            {
                Session.Clear();
            }
        }
    }
}