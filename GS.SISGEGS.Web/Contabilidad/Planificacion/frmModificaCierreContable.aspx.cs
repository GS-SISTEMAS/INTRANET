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
    public partial class frmModificaCierreContable : System.Web.UI.Page
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

                    if (string.IsNullOrEmpty(Request.QueryString["objCierreContable"]))
                    {
                       Title = "Modificar Cierre";
                       string obj = Request.QueryString["objCierrePeriodo"];
                       GS_GetPlanificacionDetalleByIdPlanResult objCierreContable = JsonHelper.JsonDeserialize<GS_GetPlanificacionDetalleByIdPlanResult>(Request.QueryString["objCierrePeriodo"]);
                        ViewState["idPlanificacion"] = objCierreContable.idPlanificacion;
                        //CargarGridEdit();
                        lblMensaje.Text = "Listo para modificar Cierre Contable";
                        lblMensaje.CssClass = "mensajeExito";
                        Label1.Text = objCierreContable.Modulo;
                        txtDetalle.Text = objCierreContable.Detalle;

                        var idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                        var codigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
                        PlanificacionWCFClient objPlanificacionWCF = new PlanificacionWCFClient();
                        var lstSource = objPlanificacionWCF.GetAgendaLista(idEmpresa, codigoUsuario);
                        ddlAgenda.DataTextField = "AgendaNombre";
                        ddlAgenda.DataValueField = "id_Agenda";
                        ddlAgenda.DataSource = lstSource;
                        ddlAgenda.SelectedValue = objCierreContable.Responsable;
                        ddlAgenda.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnGuardarUp_OnClick(object sender, EventArgs e)
        {
            try
            {
                var idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                var codigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
                PlanificacionWCFClient objPlanificacionWCF = new PlanificacionWCFClient();

                GS_GetPlanificacionDetalleByIdPlanResult objCierreContable = JsonHelper.JsonDeserialize<GS_GetPlanificacionDetalleByIdPlanResult>(Request.QueryString["objCierrePeriodo"]);

                string id_Agenda = ddlAgenda.SelectedValue;
                string detalle = txtDetalle.Text.Trim();
                string observacion = txtObservacion.Text.Trim();
                DateTime fechaCierre = dpUpdate.SelectedDate.Value;
                objPlanificacionWCF.PlanificacionHistorial_Insertar(idEmpresa, codigoUsuario, objCierreContable.id_Detalle, objCierreContable.id_Modulo, id_Agenda, detalle,
                    observacion, fechaCierre, codigoUsuario.ToString());

                objPlanificacionWCF.PlanificacionDetalle_Actualizar(idEmpresa, codigoUsuario, objCierreContable.id_Detalle, detalle,
                    observacion, fechaCierre, codigoUsuario.ToString());


                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind();", true);
            }
            catch (Exception ex)
            {

                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            
        }
    }
}