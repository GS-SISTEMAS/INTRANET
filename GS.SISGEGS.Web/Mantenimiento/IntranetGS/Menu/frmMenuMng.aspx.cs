using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.DM;

using GS.SISGEGS.Web.MenuWCF;

namespace GS.SISGEGS.Web.Mantenimiento.IntranetGS.Menu
{
    public partial class frmMenuMng : System.Web.UI.Page
    {
        private GS.SISGEGS.DM.Menu Menu_Obtener()
        {
            GS.SISGEGS.DM.Menu objMenu;
            try
            {
                objMenu = new GS.SISGEGS.DM.Menu();
                objMenu.idMenu = Convert.ToInt32(Request.QueryString["idMenu"]);
                objMenu.nombreMenu = txtNombre.Text;
                objMenu.url = txtURL.Text;
                objMenu.defecto = ckbDefecto.Checked;
                objMenu.activo = ckbActivo.Checked;
                objMenu.idUsuarioRegistro = ((Usuario_LoginResult)Session["Usuario"]).idUsuario;

                return objMenu;
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
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    if (Request.QueryString["idMenu"] == "0")
                        this.Title = "Registrar Item en Raíz";
                    else
                        this.Title = "Registrar Item";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            MenuWCFClient objMenuWCF;
            try
            {
                objMenuWCF = new MenuWCFClient();
                objMenuWCF.Menu_Registrar(Menu_Obtener());

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind();", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}