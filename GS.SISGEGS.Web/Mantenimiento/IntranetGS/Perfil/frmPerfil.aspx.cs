using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.PerfilWCF;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.Web.Mantenimiento.IntranetGS.Perfil
{
    public partial class frmPerfil : System.Web.UI.Page
    {
        private void Perfil_Cargar(int idEmpresa, string descripcion) {
            PerfilWCFClient objPerfilWCF = new PerfilWCFClient();
            try {
                List<Perfil_ListarResult> lstPerfil = objPerfilWCF.Perfil_Listar(idEmpresa, descripcion).ToList();
                grdPerfil.DataSource = lstPerfil;
                grdPerfil.DataBind();

                ViewState["lstPerfil"] = JsonHelper.JsonSerializer(lstPerfil);
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
                    Perfil_Cargar(int.Parse(cboEmpresa.SelectedValue), txtBuscar.Text);
                }
            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdPerfil_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try {
                if (e.CommandName == "Editar") {
                    Perfil_ListarResult objPerfil = JsonHelper.JsonDeserialize<List<Perfil_ListarResult>>((string)ViewState["lstPerfil"]).Find(x => x.idPerfil.ToString() == e.CommandArgument.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreate('" + JsonHelper.JsonSerializer(objPerfil) + "');", true);
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
                Perfil_Cargar(int.Parse(cboEmpresa.SelectedValue), txtBuscar.Text);
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

        protected void ramPerfil_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (e.Argument.Split(',')[0] == "Registro")
                {
                    grdPerfil.MasterTableView.SortExpressions.Clear();
                    grdPerfil.MasterTableView.GroupByExpressions.Clear();
                    Perfil_Cargar(int.Parse(e.Argument.Split(',')[1]), "");

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