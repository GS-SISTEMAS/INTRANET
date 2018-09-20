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
    public partial class frmMantenimientoModulos : System.Web.UI.Page
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");
            try
            {
                if (!Page.IsPostBack)
                {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    Cargar_Grid(); 
                }                    
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void Cargar_Grid()
        {
            List<GS_GetAllModulosResult> lstModulos;
            try
            {
                PlanificacionWCFClient objPlanificacionWCF = new PlanificacionWCFClient();

                lstModulos = objPlanificacionWCF.Perfil_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();

                grdMantModulos.DataSource = lstModulos;
                grdMantModulos.DataBind();

                ViewState["lstModulos"] = JsonHelper.JsonSerializer(lstModulos);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected void grdMantModulos_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.CommandName == "Editar")
                {
                    GS_GetAllModulosResult objModulo = JsonHelper.JsonDeserialize<List<GS_GetAllModulosResult>>((string)ViewState["lstModulos"]).Find(x => x.id_Modulo.ToString() == e.CommandArgument.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowUpdateMod('" + JsonHelper.JsonSerializer(objModulo) + "');", true);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

       

        protected void ramMantModulos_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (e.Argument.Split(',')[0] == "Registro")
                {
                    grdMantModulos.MasterTableView.SortExpressions.Clear();
                    grdMantModulos.MasterTableView.GroupByExpressions.Clear();
                    Cargar_Grid();

                    lblMensaje.Text = "Se realizo el registro del perfil";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}