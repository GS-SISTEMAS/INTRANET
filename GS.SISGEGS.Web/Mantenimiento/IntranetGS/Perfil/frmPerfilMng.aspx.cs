using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.PerfilWCF;

namespace GS.SISGEGS.Web.Mantenimiento.IntranetGS.Perfil
{
    public partial class frmPerfilMng : System.Web.UI.Page
    {
        private void Empresa_Cargar()
        {
            EmpresaWCFClient objEmpresaWCF = new EmpresaWCFClient();
            try
            {
                cboEmpresa.DataSource = objEmpresaWCF.Empresa_ComboBox();
                cboEmpresa.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try {
                if (!Page.IsPostBack)
                {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    Empresa_Cargar();
                    cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();

                    if (Request.QueryString["objPerfil"] == "") {
                        Title = "Registrar perfil";
                        cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();

                        lblMensaje.Text = "Listo para registrar perfil";
                        lblMensaje.CssClass = "mensajeExito";
                    }
                    else {
                        Title = "Modificar perfil";
                        string obj = Request.QueryString["objPerfil"];
                        Perfil_ListarResult objPerfil = JsonHelper.JsonDeserialize<Perfil_ListarResult>(Request.QueryString["objPerfil"]);
                        ViewState["idPerfil"] = objPerfil.idPerfil;
                        txtNombre.Text = objPerfil.nombrePerfil;
                        cboEstado.SelectedValue = Convert.ToInt32(objPerfil.activo).ToString();
                        cboEmpresa.SelectedValue = objPerfil.idEmpresa.ToString();
                        btnRevisarPedido.Checked = objPerfil.modificarPedido;
                        btnAprobarPlanilla0.Checked = objPerfil.aprobarPlanilla0;
                        btnAprobarPlanilla1.Checked = objPerfil.aprobarPlanilla1;
                        cboEmpresa.Enabled = false;

                        lblMensaje.Text = "Listo para modificar perfil";
                        lblMensaje.CssClass = "mensajeExito";
                    }
                }
            }
            catch (Exception ex) {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            PerfilWCFClient objPerfilWCF = new PerfilWCFClient();
            int idPerfil = 0;
            try
            {
                if (Request.QueryString["objPerfil"] != "")
                    idPerfil = (int)ViewState["idPerfil"];
                    
                objPerfilWCF.Perfil_Registrar(idPerfil, txtNombre.Text, int.Parse(cboEmpresa.SelectedValue), ((Usuario_LoginResult)Session["Usuario"]).idUsuario,
                    Convert.ToBoolean(int.Parse(cboEstado.SelectedValue)), btnAprobarPlanilla0.Checked, btnAprobarPlanilla1.Checked, btnRevisarPedido.Checked);

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + cboEmpresa.SelectedValue + ");", true);

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}