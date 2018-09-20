using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.DM;
using GS.SISGEGS.Web.GuiaWCF;
using GS.SISGEGS.Web.LoginWCF;

namespace GS.SISGEGS.Web.Compras.Pedido
{
    public partial class frmOrdenCompraDocs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (!Page.IsPostBack)
                {
                    LoginWCFClient objLoginWCF = new LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idUsuario);

                    Title = "Estado del pedido";
                    GuiaWCFClient objGuiaWCF = new GuiaWCFClient();
                    grdDocGuia.DataSource = objGuiaWCF.GuiaVenta_ListarxPedido(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                                     ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, int.Parse(Request.QueryString["idOrdenVenta"]));
                    grdDocGuia.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}