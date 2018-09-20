using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.PerfilWCF;
using GS.SISGEGS.Web.PlanificacionWCF;

namespace GS.SISGEGS.Web.Contabilidad.Planificacion
{
    public partial class frmMantModulosMng : System.Web.UI.Page
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


                    Title = "Modificar perfil";
                        string obj = Request.QueryString["objMantModulos"];
                        GS_GetAllModulosResult objModulo = JsonHelper.JsonDeserialize<GS_GetAllModulosResult>(Request.QueryString["objMantModulos"]);
                        ViewState["id_Modulo"] = objModulo.id_Modulo;
                        txtModulo.Text = objModulo.nombre;
                        cboEstado.SelectedValue = Convert.ToInt32(objModulo.id_Estado).ToString();
                        lblMensaje.Text = "Listo para modificar estado";
                        lblMensaje.CssClass = "mensajeExito";
                    
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
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            PlanificacionWCFClient objPlanificacionWCF = new PlanificacionWCFClient();
            int idModulo = 0;
            try
            {
                if (Request.QueryString["objMantModulos"] != "")
                    idModulo = (int)ViewState["id_Modulo"];

                 GS_GetAllModulosResult objModulo = JsonHelper.JsonDeserialize<GS_GetAllModulosResult>(Request.QueryString["objMantModulos"]);

                var idEmpresa=((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                var codigoUsuario=((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
                int estadoModulo=Convert.ToInt32(cboEstado.SelectedValue);

                objPlanificacionWCF.Modulos_Actualizar(idEmpresa,codigoUsuario,objModulo.id_Modulo,objModulo.Detalle,objModulo.id_Agenda,estadoModulo, codigoUsuario.ToString());
                lblMensaje.Text = "Registro se modificó correctamente";
                lblMensaje.CssClass = "mensajeExito";
                
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }



      
    }
}