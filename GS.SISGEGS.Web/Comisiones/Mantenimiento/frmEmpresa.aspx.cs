using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.Web.Comision.Mantenimiento
{
    public partial class frmEmpresa : System.Web.UI.Page
    {
        private void Empresa_Listar(int idEmpresa, string descripcion) {
            EmpresaWCFClient objEmpresaWCF = new EmpresaWCFClient();
            try {
                List<Empresa_ListarResult> lstEmpresa = objEmpresaWCF.Empresa_Listar(idEmpresa, descripcion).ToList();
                grdEmpresa.DataSource = lstEmpresa;
                grdEmpresa.DataBind();

                ViewState["lstEmpresa"] = JsonHelper.JsonSerializer(lstEmpresa);
                ViewState["idEmpresa"] = idEmpresa;
                ViewState["descripcion"] = descripcion;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private void Empresa_Cargar() {
            EmpresaWCFClient objEmpresaWCF = new EmpresaWCFClient();
            try {
                cboEmpresa.DataSource = objEmpresaWCF.Empresa_ComboBox();
                cboEmpresa.DataBind();
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
                    Empresa_Listar(int.Parse(cboEmpresa.SelectedValue), txtBuscar.Text);
                }
            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdEmpresa_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try {
                if (e.CommandName == "Editar") {
                    Empresa_ListarResult objEmpresa = JsonHelper.JsonDeserialize<List<Empresa_ListarResult>>((string)ViewState["lstEmpresa"]).Find(x => x.idEmpresa.ToString() == e.CommandArgument.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreate('" + JsonHelper.JsonSerializer(objEmpresa) + "');", true);
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
                Empresa_Listar(int.Parse(cboEmpresa.SelectedValue), txtBuscar.Text);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreate('');", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void ramEmpresa_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (e.Argument.Split(',')[0] == "Registro")
                {
                    grdEmpresa.MasterTableView.SortExpressions.Clear();
                    grdEmpresa.MasterTableView.GroupByExpressions.Clear();
                    Empresa_Listar(int.Parse(e.Argument.Split(',')[1]), "");

                    lblMensaje.Text = "Se realizo el registro del Empresa";
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