using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.PlanificacionWCF;

namespace GS.SISGEGS.Web.Contabilidad.Planificacion
{
    public partial class frmHistorialCierreContable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (!Page.IsPostBack)
                {

                    if (!string.IsNullOrEmpty(Request.QueryString["objHistorial"]))
                    {
                        LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                        objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                            ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                        Title = "Historial";
                        string obj = Request.QueryString["objHistorial"];
                        GS_GetPlanificacionDetalleByIdPlanResult objCierreContable = JsonHelper.JsonDeserialize<GS_GetPlanificacionDetalleByIdPlanResult>(Request.QueryString["objHistorial"]);
                        ViewState["idPlanificacion"] = objCierreContable.idPlanificacion;
                        //CargarGridEdit();
                        
                        Label1.Text = objCierreContable.Modulo;
                        

                        var idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                        var codigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
                        PlanificacionWCFClient objPlanificacionWCF = new PlanificacionWCFClient();
                        var lstSource = objPlanificacionWCF.GetHistorialCambios(idEmpresa, codigoUsuario,objCierreContable.id_Detalle);
                        grdHistorial.DataSource = lstSource;
                        grdHistorial.DataBind();
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