using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.UsuarioWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.PerfilWCF;
using System.Web.Services;
using Telerik.Web.UI;


namespace GS.SISGEGS.Web.RRHH.Procesos
{
    public partial class frmInactivacionUsuarioMng : System.Web.UI.Page
    {
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

                    //string obj = CStr(Request.QueryString["objUsuario"]);


                    string obj = Server.UrlDecode(Request.QueryString["objUsuario"]);


                    UsuarioWCFClient objUsuariosWCF = new UsuarioWCFClient();
                    List<USP_Sel_Usuarios_GeneralResult> listUsuario = objUsuariosWCF.Usuario_Listar_Usuarios(obj).ToList();


                    ViewState["loginUsuario"] = listUsuario[0].loginUsuario.ToString();

                    txtLogin.Text = listUsuario[0].loginUsuario.ToString();
                    txtNombre.Text = listUsuario[0].NomUsuario.ToString();
                    txtCorreo.Text = listUsuario[0].EMail.ToString();

                    chekSilvestre.Checked = listUsuario[0].Silvestre;
                    checkNeoaground.Checked = listUsuario[0].NeoAgrum;
                    checkInatec.Checked = listUsuario[0].Inatec;
                    checkIntranet.Checked = listUsuario[0].Intranet;
                    checkTicket.Checked = listUsuario[0].Ticket;
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
            int result = 0;
            UsuarioWCFClient objUsuariosWCF = new UsuarioWCFClient();
            result = objUsuariosWCF.Actualizar_Estado_Usuarios_Empresa(txtLogin.Text, chekSilvestre.Checked, checkNeoaground.Checked, checkInatec.Checked, checkIntranet.Checked, checkTicket.Checked);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(1);", true);


        }

        public void Close()
        {

        }

    }
}