using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.ComisionesWCF;
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.Web.Comision.Mantenimiento
{
    public partial class frmZona : System.Web.UI.Page
    {
        private void Empresa_Cargar()
        {
            EmpresaWCFClient objEmpresaWCF = new EmpresaWCFClient();
            try
            {
                cboEmpresa.DataSource = objEmpresaWCF.Empresa_Listar(0, null);
                cboEmpresa.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Zona_Listar() {
            ComisionWCFClient objZonaWCF = new ComisionWCFClient();
            try {
                List<gsZonasComision_ListarResult> lstZona = objZonaWCF.ZonasComision_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario ).ToList();
                grdZona.DataSource = lstZona;
                grdZona.DataBind();

                ViewState["lstZona"] = JsonHelper.JsonSerializer(lstZona);

            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try {
                if (!Page.IsPostBack) {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    Empresa_Cargar();
                    cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();
                    Zona_Listar();
                    cboEmpresa.Enabled = false; 
                }
            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdZona_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try {
                if (e.CommandName == "Editar") {
                    gsZonasComision_ListarResult objZona = JsonHelper.JsonDeserialize<List<gsZonasComision_ListarResult>>((string)ViewState["lstZona"]).Find(x => x.id_zona.ToString() == e.CommandArgument.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreate('" + JsonHelper.JsonSerializer(objZona) + "');", true);
                }
            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try {
                Zona_Listar();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            //if (Session["Usuario"] == null)
            //    Response.Redirect("~/Security/frmCerrar.aspx");

            //try
            //{
            //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreate('');", true);
            //}
            //catch (Exception ex)
            //{
            //    lblMensaje.Text = ex.Message;
            //    lblMensaje.CssClass = "mensajeError";
            //}
        }

        protected void ramZona_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (e.Argument.Split(',')[0] == "Registro")
                {
                    grdZona.MasterTableView.SortExpressions.Clear();
                    grdZona.MasterTableView.GroupByExpressions.Clear();
                    Zona_Listar();

                    lblMensaje.Text = "Se realizo el registro del Zona";
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