using GS.SISGEGS.DM;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.ReporteContabilidadWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GS.SISGEGS.Web.Contabilidad.Reporte
{
    public partial class frmTransGratuitas : System.Web.UI.Page
    {
        private void Reporte_Cargar()
        {
            List<gsDocVenta_ListarTransGratuitasResult> lstDocumentos;
            try
            {
                ReporteContabilidadWCFClient objReporteWCF = new ReporteContabilidadWCFClient();
                lstDocumentos = objReporteWCF.DocVenta_ListarTransGratuitas(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                    dpFechaInicio.SelectedDate.Value, dpFechaFin.SelectedDate.Value).ToList();
                grdDocumentos.DataSource = lstDocumentos;
                grdDocumentos.DataBind();
                lblMensaje.Text = "Se han encontrado " + lstDocumentos.Count.ToString() + " registro.";
                lblMensaje.CssClass = "mensajeExito";

                ViewState["lstDocumentos"] = JsonHelper.JsonSerializer(lstDocumentos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

                    dpFechaInicio.SelectedDate = DateTime.Now.AddMonths(-1);
                    dpFechaFin.SelectedDate = DateTime.Now;

                    Reporte_Cargar();
                }
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                Reporte_Cargar();
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al realizar la busqueda", "");
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                grdDocumentos.ExportSettings.FileName = "TransGratuitas_" + DateTime.Now.ToString("yyyyMMddHmm");
                grdDocumentos.ExportSettings.ExportOnlyData = true;
                grdDocumentos.ExportSettings.IgnorePaging = true;
                grdDocumentos.ExportSettings.OpenInNewWindow = true;
                grdDocumentos.MasterTableView.ExportToExcel();

                Page.Response.ClearHeaders();
                Page.Response.ClearContent();
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }
        }

        protected void grdDocumentos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                grdDocumentos.DataSource = JsonHelper.JsonDeserialize<List<gsDocVenta_ListarTransGratuitasResult>>((string)ViewState["lstDocumentos"]);
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error recargar la informacion en el formulario.", "");
            }
        }
    }
}