using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using GS.SISGEGS.DM;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.DocumentoWCF;
using GS.SISGEGS.Web.FormaPagoWCF;
using GS.SISGEGS.Web.OrdenVentaWCF;
using GS.SISGEGS.Web.LoginWCF;
using GS.SISGEGS.Web.ReporteSistemasWCF;

namespace GS.SISGEGS.Web.Sistemas.Reportes
{
    public partial class frmConsultaDocMaximo : System.Web.UI.Page
    {
        #region PROCEDIMIENTOS
        private void CargarDocumentos()
        {
            
            DateTime fechainicial = Convert.ToDateTime(dpFechaInicio.SelectedDate);
            DateTime fechafinal=Convert.ToDateTime(dpFechaFinal.SelectedDate);
            int procesados = chkprocesados.Checked == true ? 1 : 0;

            ReporteSistemasWCFClient objReporteSistemasWCF = new ReporteSistemasWCFClient();
            
            List<USP_Sel_ControlFacturasMaximoResult> lst = new List<USP_Sel_ControlFacturasMaximoResult>();
            lst = objReporteSistemasWCF.ListarControlFacturasMaximo(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                fechainicial, fechafinal, procesados).ToList();
            grdMaximo.DataSource = lst;
            grdMaximo.DataBind();
            //ViewState["lstfacturas"] = lst;
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (!Page.IsPostBack)
                {
                    LoginWCFClient objLoginWCF = new LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idUsuario);

                    dpFechaInicio.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    dpFechaFinal.SelectedDate = DateTime.Now;
                    CargarDocumentos();
                    
                    lblMensaje.Text = "La página cargo correctamente";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        protected void grdMaximo_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                //grdMaximo.DataSource = JsonHelper.JsonDeserialize<List<gsOV_ListarResult>>((string)ViewState["lstOrdenVenta"]);
            }
            catch (Exception ex)
            {
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
                CargarDocumentos();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

    }
}