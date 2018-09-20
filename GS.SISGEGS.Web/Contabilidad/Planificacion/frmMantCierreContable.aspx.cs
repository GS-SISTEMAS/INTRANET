using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.PlanificacionWCF;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using Telerik.Charting.Styles;
using Telerik.Web.UI;
using Telerik.Pdf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using PdfWriter = iTextSharp.text.pdf.PdfWriter;

namespace GS.SISGEGS.Web.Contabilidad.Planificacion
{
    public partial class frmMantCierreContable : Page
    {
        private void CierreContable_Cargar(int idEmpresa, int codigoUsuario, string periodo)
        {
            PlanificacionWCFClient objPlanificacionWCF = new PlanificacionWCFClient();
            try
            {
                List<GS_GetPlanificacionByPeriodoResult> lstCierrePeriodo = objPlanificacionWCF.GetPlanificacionByPeriodo(idEmpresa, codigoUsuario, periodo).ToList();
                grdCierrePorPeriodo.DataSource = lstCierrePeriodo;
                grdCierrePorPeriodo.DataBind();

                ViewState["lstCierrePeriodo"] = JsonHelper.JsonSerializer(lstCierrePeriodo);
                ViewState["idEmpresa"] = idEmpresa;
                ViewState["codigoUsuario"] = codigoUsuario;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void CierreContableByPlan_Cargar(int idEmpresa, int codigoUsuario, string idPlanificacion)
        {
            PlanificacionWCFClient objCierreByPlanWCF = new PlanificacionWCFClient();
            try
            {
                List<GS_GetPlanificacionDetalleByIdPlanResult> lstCierreByPlan = objCierreByPlanWCF.GetPlanificacionDetalleByIdPlan(idEmpresa, codigoUsuario, idPlanificacion).ToList();
                grdCierreContable.DataSource = lstCierreByPlan;
                grdCierreContable.DataBind();

                ViewState["lstCierreByPlan"] = JsonHelper.JsonSerializer(lstCierreByPlan);
                ViewState["idEmpresa"] = idEmpresa;
                ViewState["codigoUsuario"] = codigoUsuario;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
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

                    dpBuscar.SelectedDate = DateTime.Now;
                    var idEmpresa = ((Usuario_LoginResult) Session["Usuario"]).idEmpresa;
                    var codigoUsuario = ((Usuario_LoginResult) Session["Usuario"]).codigoUsuario;
                    var periodo = dpBuscar.SelectedDate.Value.Month.ToString("D2") + "/" + dpBuscar.SelectedDate.Value.Year;
                    CierreContable_Cargar(idEmpresa, codigoUsuario, periodo);
                    CierreContableByPlan_Cargar(idEmpresa, codigoUsuario, "0");

                    lblMensaje.Text = "Se cargó con exito.";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdCierrePorPeriodo_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.CommandName == "Editar")
                {
                    GS_GetPlanificacionByPeriodoResult objCierrePeriodo = JsonHelper.JsonDeserialize<List<GS_GetPlanificacionByPeriodoResult>>((string)ViewState["lstCierrePeriodo"]).Find(x => x.idPlanificacion.ToString() == e.CommandArgument.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreate('" + JsonHelper.JsonSerializer(objCierrePeriodo) + "');", true);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdCierreContable_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.CommandName == "Editar")
                {
                    GS_GetPlanificacionDetalleByIdPlanResult objCierrePeriodo = JsonHelper.JsonDeserialize<List<GS_GetPlanificacionDetalleByIdPlanResult>>((string)ViewState["lstCierreByPlan"]).Find(x => x.id_Detalle.ToString() == e.CommandArgument.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreateEdit('" + JsonHelper.JsonSerializer(objCierrePeriodo) + "');", true);
                }

                if (e.CommandName == "Detalle")
                {
                    GS_GetPlanificacionDetalleByIdPlanResult objCierrePeriodo = JsonHelper.JsonDeserialize<List<GS_GetPlanificacionDetalleByIdPlanResult>>((string)ViewState["lstCierreByPlan"]).Find(x => x.id_Detalle.ToString() == e.CommandArgument.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowHistorial('" + JsonHelper.JsonSerializer(objCierrePeriodo) + "');", true);
                }
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
                var idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                var codigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
                var periodo = dpBuscar.SelectedDate.Value.Month.ToString("D2") + "/" + dpBuscar.SelectedDate.Value.Year;

                CierreContable_Cargar(idEmpresa, codigoUsuario, periodo); 
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

        protected void ramCierreContable_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (e.Argument.Split(',')[0] == "Registro")
                {
                    grdCierreContable.MasterTableView.SortExpressions.Clear();
                    grdCierreContable.MasterTableView.GroupByExpressions.Clear();
                    grdCierrePorPeriodo.MasterTableView.SortExpressions.Clear();
                    grdCierrePorPeriodo.MasterTableView.GroupByExpressions.Clear();
                    var idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                    var codigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
                    CierreContable_Cargar(idEmpresa, codigoUsuario,e.Argument.Split(',')[1]);

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

        protected void grdCierrePorPeriodo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GridDataItem item = (GridDataItem)grdCierrePorPeriodo.SelectedItems[0];
            var idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
            var codigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
            var idPlanificacion = item.GetDataKeyValue("idPlanificacion").ToString();
            CierreContableByPlan_Cargar(idEmpresa, codigoUsuario, idPlanificacion);
            var lstDoc = JsonHelper.JsonDeserialize<List<GS_GetPlanificacionByPeriodoResult>>((string)ViewState["lstCierrePeriodo"]);
            var doc = lstDoc.Where(x => x.idPlanificacion == Convert.ToInt32(idPlanificacion)).FirstOrDefault();
            ViewState["fecInicial"] = doc.FechaInicio.Day.ToString("D2");
            ViewState["fecFinal"] = doc.FechaFin.Day.ToString("D2");
             RadCalendar1.EnableMultiSelect = false;
             RadCalendar2.EnableMultiSelect = false;
           
        }
        protected void CustomizeDay(object sender, Telerik.Web.UI.Calendar.DayRenderEventArgs e)
        {
            DateTime CurrentDate = e.Day.Date;
            string startDate = string.IsNullOrEmpty((string)ViewState["fecInicial"]) ? "1": (string)ViewState["fecInicial"];
            
            //if (startDate <= CurrentDate && CurrentDate <= endDate)
            //{
            if (CurrentDate.Day == Convert.ToInt32(startDate))
                {
                    TableCell currentCell = e.Cell;
                    currentCell.Style["background-color"] = "Green";
                    currentCell.Text = startDate;
                }
            //}
        }

        protected void CustomizeDay2(object sender, Telerik.Web.UI.Calendar.DayRenderEventArgs e)
        {
            DateTime CurrentDate = e.Day.Date;
            string endDate = string.IsNullOrEmpty((string)ViewState["fecFinal"]) ? "1" : (string)ViewState["fecFinal"];

            //if (startDate <= CurrentDate && CurrentDate <= endDate)
            //{
            if (CurrentDate.Day == Convert.ToInt32(endDate))
            {
                TableCell currentCell = e.Cell;
                currentCell.Style["background-color"] = "Green";
                currentCell.Text = endDate;
            }
            //}
        }

        protected void btnPdf_OnClick(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                grdCierreContable.ExportSettings.FileName = "CierreContable_" + DateTime.Now.ToString("yyyyMMddHmm");
                grdCierreContable.ExportSettings.Pdf.BorderType = GridPdfSettings.GridPdfBorderType.AllBorders;
                //grdCierreContable.ExportSettings.Pdf.PageHeader.MiddleCell.Text = headerMiddleCell;
                grdCierreContable.ExportSettings.Pdf.PageHeader.MiddleCell.TextAlign = GridPdfPageHeaderFooterCell.CellTextAlign.Center;
                //grdCierreContable.ExportSettings.Pdf.PageHeader.LeftCell.Text = headerLeftCell;
                grdCierreContable.ExportSettings.Pdf.PageHeader.LeftCell.TextAlign = GridPdfPageHeaderFooterCell.CellTextAlign.Center;
                //grdCierreContable.ExportSettings.Pdf.PageFooter.MiddleCell.Text = footerMiddleCell;
                grdCierreContable.ExportSettings.Pdf.PageFooter.MiddleCell.TextAlign = GridPdfPageHeaderFooterCell.CellTextAlign.Center;
                grdCierreContable.ExportSettings.Pdf.PageHeader.MiddleCell.Text = string.Empty;
                grdCierreContable.ExportSettings.Pdf.PageHeader.LeftCell.Text = string.Empty;
                grdCierreContable.ExportSettings.Pdf.PageFooter.MiddleCell.Text = string.Empty;

               
                grdCierreContable.MasterTableView.ExportToPdf();
                
                //Page.Response.ClearHeaders();
                //Page.Response.ClearContent();

             
                    
            }
            catch (Exception ex)
            {
                //rwmReporte.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }
        }

        protected void grdCierreContable_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                grdCierreContable.DataSource = JsonHelper.JsonDeserialize<List<GS_GetPlanificacionDetalleByIdPlanResult>>((string)ViewState["lstCierreByPlan"]);
                
               
            }
            catch (Exception ex)
            {
                //rwmReporte.RadAlert(ex.Message, 500, 100, "Error recargar la informacion en el formulario.", "");
            }
        }


        
    }
}