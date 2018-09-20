using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GS.SISGEGS.Web.Security
{
    public partial class frmCerrar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try {
                if (!Page.IsPostBack) {
                    Session.Clear();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "closeWin();", true);
                }
            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}