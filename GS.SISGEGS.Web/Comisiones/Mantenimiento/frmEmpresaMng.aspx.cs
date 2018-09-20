using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.Web.Comision.Mantenimiento
{
    public partial class frmEmpresaMng : System.Web.UI.Page
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

                    if (Request.QueryString["objEmpresa"] == "") {
                        Title = "Registrar Empresa";
                        cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();

                        lblMensaje.Text = "Listo para registrar Empresa";
                        lblMensaje.CssClass = "mensajeExito";
                    }
                    else {
                        Title = "Modificar Empresa";
                        string obj = Request.QueryString["objEmpresa"];
                        Empresa_ListarResult objEmpresa = JsonHelper.JsonDeserialize<Empresa_ListarResult>(Request.QueryString["objEmpresa"]);
                        ViewState["idEmpresa"] = objEmpresa.idEmpresa;
                        //String.Format("S/{0:#,##0.00}", sumSoles);

                        decimal porcent = decimal.Parse(objEmpresa.Provision.ToString()); 

                        txtPorcentaje.Text = string.Format("{0:#,##0}", porcent);
                        cboEstado.SelectedValue = 1.ToString(); 
                        //cboEmpresa.SelectedValue = objEmpresa.idEmpresa.ToString();

                        cboEmpresa.Enabled = false;

                        lblMensaje.Text = "Listo para modificar Empresa";
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

            EmpresaWCFClient objEmpresaWCF = new EmpresaWCFClient();
            int idEmpresa = 0;
            decimal provision; 

            try
            {
                if (Request.QueryString["objEmpresa"] != "")
                    idEmpresa = (int)ViewState["idEmpresa"];

                provision = decimal.Parse(txtPorcentaje.Text);
                provision = (provision / 100) ; 
                objEmpresaWCF.Empresa_Registrar(idEmpresa , provision, 1 );

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