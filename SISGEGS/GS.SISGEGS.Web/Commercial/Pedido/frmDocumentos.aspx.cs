using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.Web.GuiaWCF;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.Web.Commercial.Pedido
{
    public partial class frmDocumentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try {
                if (!Page.IsPostBack) {
                    Title = "Estado del pedido";
                    GuiaWCFClient objGuiaWCF = new GuiaWCFClient();
                    grdDocGuia.DataSource = objGuiaWCF.GuiaVenta_ListarxPedido(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                                     ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, int.Parse(Request.QueryString["idPedido"]));
                    grdDocGuia.DataBind();
                }
            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}