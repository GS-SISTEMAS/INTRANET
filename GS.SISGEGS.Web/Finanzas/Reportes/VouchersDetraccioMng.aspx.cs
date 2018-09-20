using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.FinanzasWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.PlanificacionWCF;

namespace GS.SISGEGS.Web.Finanzas.Reportes
{
    public partial class VouchersDetraccioMng : System.Web.UI.Page
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

                    if (!string.IsNullOrEmpty(Request.QueryString["op"]))
                    {
                        Title = "Vouchers";
                        string obj = Request.QueryString["op"];
                        int op = JsonHelper.JsonDeserialize<int>(Request.QueryString["op"]);
                        //CargarGridEdit();


                        var idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                        var codigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
                        FinanzasWCFClient objFinanzas = new FinanzasWCFClient();
                        var lstSource = objFinanzas.GetVoucherDetraccione(idEmpresa, codigoUsuario, Convert.ToInt32(op));
                        grdVouchers.DataSource = lstSource;
                        grdVouchers.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                //lblMensaje.Text = "ERROR: " + ex.Message;
                //lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}