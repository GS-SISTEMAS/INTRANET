using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.ReporteContabilidadWCF;
using Telerik.Web.UI;

namespace GS.SISGEGS.Web.Contabilidad.Reporte
{
    public partial class frmVentaTransferencia : System.Web.UI.Page
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

                    ViewState["lstDocumentos"] = null;
                    dpFecInicio.SelectedDate = DateTime.Now.AddMonths(-1);
                    dpFecFinal.SelectedDate = DateTime.Now;
                    ddlFormaPago.SelectedValue = "2";
              
                    Reporte_Cargar();
                }
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }
        }

        private void Reporte_Cargar()
        {
            List<Reporte_Venta_TransfResult> lstDocumentos = new List<Reporte_Venta_TransfResult>();
            try
            {
                ReporteContabilidadWCFClient objReporteContabilidadWCF = new ReporteContabilidadWCFClient();
                DateTime fechaInicio = new DateTime(dpFecInicio.SelectedDate.Value.Year, dpFecInicio.SelectedDate.Value.Month, 1);
                DateTime fechaFinal = new DateTime(dpFecFinal.SelectedDate.Value.Year, dpFecFinal.SelectedDate.Value.Month, 1).AddMonths(1).AddDays(-1);
                int formaPago = Convert.ToInt32(ddlFormaPago.SelectedValue);
                int tipoMoneda = Convert.ToInt32(ddlMoneda.SelectedValue);
                int?  kardex = null;
                if (!string.IsNullOrEmpty(txtKardex.Text))
                    kardex = Convert.ToInt32(txtKardex.Text);
                if (fechaFinal < fechaInicio)
                {
                    rwmReporte.RadAlert("Ud. debe ingresar un periodo final mayor o igual al periodo inicial", 500, 100,
                        "Validación de fechas", "");

                }
                else
                {
                    lstDocumentos =
                        objReporteContabilidadWCF.ReporteVenta(((Usuario_LoginResult) Session["Usuario"]).idEmpresa,
                            ((Usuario_LoginResult) Session["Usuario"]).codigoUsuario,
                            fechaInicio, fechaFinal, formaPago, tipoMoneda, kardex).ToList();

                }


                    
                    grdVentas.DataSource = lstDocumentos;
                    string mensaje = "Se han encontrado " + lstDocumentos.Count.ToString() + " registros.";
                    lblMensaje.Text = mensaje;
                    lblMensaje.CssClass = "mensajeExito";
                    grdVentas.DataBind();

                    ViewState["lstDocumentos"] = JsonHelper.JsonSerializer(lstDocumentos);
                    
                    Session["mensaje"] = mensaje;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                grdVentas.ExportSettings.FileName = "ReporteVentas_" + DateTime.Now.ToString("yyyyMMddHmm");
                grdVentas.ExportSettings.ExportOnlyData = true;
                grdVentas.ExportSettings.IgnorePaging = true;
                grdVentas.ExportSettings.OpenInNewWindow = true;
                grdVentas.MasterTableView.ExportToExcel();
                Page.Response.ClearHeaders();
                Page.Response.ClearContent();
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }
        }

        protected void btnGraph_OnClick(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");
            try
            {

                var source = JsonHelper.JsonDeserialize<List<Reporte_Venta_TransfResult>>((string)ViewState["lstDocumentos"]);

                    grdGraph.Visible = true;
                    grdVentas.Visible = false;
                    PanelGraph.Visible = true;
                    PanelGrid.Visible = false;
                    btnBack.Visible = true;
                    btnGraph.Visible = false;
                    grdGraph.DataSource = source;
                    grdGraph.DataBind();

                Label2.Text = ((Usuario_LoginResult) Session["Usuario"]).nombreComercial;
                Label4.Text = ddlFormaPago.SelectedItem.Text;
                Label6.Text = ddlMoneda.SelectedItem.Text;

            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al mostrar imagen", "");
            }
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                grdGraph.Visible = false;
                grdVentas.Visible = true;
                PanelGraph.Visible = false;
                PanelGrid.Visible = true;
                btnBack.Visible = false;
                btnGraph.Visible = true;
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al regresar", "");
            }
        }

        protected void btnBuscar_OnClick(object sender, EventArgs e)
        {
            Reporte_Cargar();
        }

        protected void grdVentas_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                int startRowIndex = grdVentas.CurrentPageIndex * grdVentas.PageSize;
                int maximumRows = grdVentas.PageSize;
                var lstDocumntos =JsonHelper.JsonDeserialize<List<Reporte_Venta_TransfResult>>((string)ViewState["lstDocumentos"]);

                grdVentas.AllowCustomPaging = true;
                grdVentas.DataSource =
                    lstDocumntos.Where(x => x.Row_Index > startRowIndex && x.Row_Index <= (startRowIndex + maximumRows));
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error recargar la informacion en el formulario.", "");
            }
            
        }
    }
}