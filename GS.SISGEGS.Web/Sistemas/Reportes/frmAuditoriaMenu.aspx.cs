using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.DM;
using GS.SISGEGS.Web.ReporteSistemasWCF;
using GS.SISGEGS.Web.EmpresaWCF;

namespace GS.SISGEGS.Web.Sistemas.Reportes
{
    public partial class frmAuditoriaMenu : System.Web.UI.Page
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

        private void Auditoria_ReporteCargar() {
            ReporteSistemasWCFClient objReporteSistemasWCF = new ReporteSistemasWCFClient();
            try {
                rpgAuditoriaMenu.DataSource = objReporteSistemasWCF.AuditoriaMenu_Reporte(int.Parse(cboEmpresa.SelectedValue), 
                    dpFechaInicio.SelectedDate.Value, dpFechaFinal.SelectedDate.Value);
                rpgAuditoriaMenu.DataBind();
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
                    dpFechaInicio.SelectedDate = DateTime.Today.AddDays(-7);
                    dpFechaFinal.SelectedDate = DateTime.Today;
                    Auditoria_ReporteCargar();

                    lblMensaje.Text = "El reporte ha sido cargado con éxito";
                    lblMensaje.CssClass = "mensajeExito";
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

            try
            {
                Auditoria_ReporteCargar();

                lblMensaje.Text = "El reporte ha sido cargado con éxito";
                lblMensaje.CssClass = "mensajeExito";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}