using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.CierreCostoWCF;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.Web.Contabilidad.Costos
{
    public partial class frmControlCostosPlanProd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try {

                if (!Page.IsPostBack) {

                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    Title = "Plan de producción del periodo " + Request.QueryString["periodo"];

                    CierreCostoWCFClient objCierreCostoWCF = new CierreCostoWCFClient();
                    int periodo = int.Parse(Request.QueryString["periodo"]);
                    int kardex = int.Parse(Request.QueryString["kardex"]);
                    DateTime fechaInicio = new DateTime((periodo - periodo % 100) / 100, periodo % 100, 1);
                    DateTime fechaFinal = fechaInicio.AddMonths(1).AddDays(-1);

                    grdMateriaPrima.DataSource = objCierreCostoWCF.Produccion_Listar_PlanProd(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                        ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, fechaInicio, fechaFinal, kardex);
                    grdMateriaPrima.DataBind();

                    lblMensaje.Text = "Los datos han sido cargados correctamente.";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}