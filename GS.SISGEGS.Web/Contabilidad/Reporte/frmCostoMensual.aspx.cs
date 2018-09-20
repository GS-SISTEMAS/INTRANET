using GS.SISGEGS.DM;
using GS.SISGEGS.Web.CierreCostoWCF;
using GS.SISGEGS.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace GS.SISGEGS.Web.Contabilidad.Reporte
{
    public partial class frmCostoMensual : System.Web.UI.Page
    {
        private void Reporte_Cargar()
        {
            List<gsDocVenta_ControlCosto_MarcaProductoResult> lstDocumentos;
            try
            {
                CierreCostoWCFClient objCierreCostoWCF = new CierreCostoWCFClient();
                DateTime fechaInicio = new DateTime(dpFecInicio.SelectedDate.Value.Year, dpFecInicio.SelectedDate.Value.Month, 1);
                DateTime fechaFinal = new DateTime(dpFecFinal.SelectedDate.Value.Year, dpFecFinal.SelectedDate.Value.Month, 1).AddMonths(1).AddDays(-1);

                lstDocumentos = objCierreCostoWCF.DocVenta_ControlCosto_MarcaProducto(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                    dpFecInicio.SelectedDate.Value, dpFecFinal.SelectedDate.Value, null).ToList();

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

        private void AddStylesToDataCells(PivotGridBaseModelCell modelDataCell, PivotGridCellExportingArgs e)
        {
            if (modelDataCell.Data != null && modelDataCell.Data.GetType() == typeof(decimal))
            {
                decimal value = Convert.ToDecimal(modelDataCell.Data);
                if (value > 100000)
                {
                    e.ExportedCell.Style.BackColor = Color.FromArgb(51, 204, 204);
                    AddBorders(e);
                }

                e.ExportedCell.Format = "$0.0";
            }
        }

        private void AddStylesToColumnHeaderCells(PivotGridBaseModelCell modelDataCell, PivotGridCellExportingArgs e)
        {
            if (e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width == 0)
            {
                e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width = 200D;
            }

            if (modelDataCell.IsTotalCell)
            {
                e.ExportedCell.Style.BackColor = Color.FromArgb(150, 150, 150);
                e.ExportedCell.Style.Font.Bold = true;
            }
            else
            {
                e.ExportedCell.Style.BackColor = Color.FromArgb(192, 192, 192);
            }
            AddBorders(e);
        }

        private void AddStylesToRowHeaderCells(PivotGridBaseModelCell modelDataCell, PivotGridCellExportingArgs e)
        {
            if (e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width == 0)
            {
                e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width = 80D;
            }
            if (modelDataCell.IsTotalCell)
            {
                e.ExportedCell.Style.BackColor = Color.FromArgb(150, 150, 150);
                e.ExportedCell.Style.Font.Bold = true;
            }
            else
            {
                e.ExportedCell.Style.BackColor = Color.FromArgb(192, 192, 192);
            }

            AddBorders(e);
        }

        private static void AddBorders(PivotGridCellExportingArgs e)
        {
            e.ExportedCell.Style.BorderBottomColor = Color.FromArgb(128, 128, 128);
            e.ExportedCell.Style.BorderBottomWidth = new Unit(1);
            e.ExportedCell.Style.BorderBottomStyle = BorderStyle.Solid;

            e.ExportedCell.Style.BorderRightColor = Color.FromArgb(128, 128, 128);
            e.ExportedCell.Style.BorderRightWidth = new Unit(1);
            e.ExportedCell.Style.BorderRightStyle = BorderStyle.Solid;

            e.ExportedCell.Style.BorderLeftColor = Color.FromArgb(128, 128, 128);
            e.ExportedCell.Style.BorderLeftWidth = new Unit(1);
            e.ExportedCell.Style.BorderLeftStyle = BorderStyle.Solid;

            e.ExportedCell.Style.BorderTopColor = Color.FromArgb(128, 128, 128);
            e.ExportedCell.Style.BorderTopWidth = new Unit(1);
            e.ExportedCell.Style.BorderTopStyle = BorderStyle.Solid;
        }

        private bool IsTotalDataCell(PivotGridBaseModelCell modelDataCell)
        {
            return modelDataCell.TableCellType == PivotGridTableCellType.DataCell &&
               (modelDataCell.CellType == PivotGridDataCellType.ColumnTotalDataCell ||
                 modelDataCell.CellType == PivotGridDataCellType.RowTotalDataCell ||
                 modelDataCell.CellType == PivotGridDataCellType.RowAndColumnTotal);
        }

        private bool IsGrandTotalDataCell(PivotGridBaseModelCell modelDataCell)
        {
            return modelDataCell.TableCellType == PivotGridTableCellType.DataCell &&
                (modelDataCell.CellType == PivotGridDataCellType.ColumnGrandTotalDataCell ||
                    modelDataCell.CellType == PivotGridDataCellType.ColumnGrandTotalRowTotal ||
                    modelDataCell.CellType == PivotGridDataCellType.RowGrandTotalColumnTotal ||
                    modelDataCell.CellType == PivotGridDataCellType.RowGrandTotalDataCell ||
                    modelDataCell.CellType == PivotGridDataCellType.RowAndColumnGrandTotal);
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

                    dpFecInicio.SelectedDate = DateTime.Now.AddMonths(-1);
                    dpFecFinal.SelectedDate = DateTime.Now;

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
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                grdDocumentos.ExportSettings.Excel.Format = (PivotGridExcelFormat)Enum.Parse(typeof(PivotGridExcelFormat), "Xlsx");
                grdDocumentos.ExportSettings.FileName = "ReporteCostoMensual_" + DateTime.Now.ToString("yyyyMMddHmm");
                grdDocumentos.ExportToExcel();
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }
        }

        protected void grdDocumentos_PivotGridCellExporting(object sender, PivotGridCellExportingArgs e)
        {
            try
            {
                PivotGridBaseModelCell modelDataCell = e.PivotGridModelCell as PivotGridBaseModelCell;
                if (modelDataCell != null)
                {
                    AddStylesToDataCells(modelDataCell, e);
                }

                if (modelDataCell.TableCellType == PivotGridTableCellType.RowHeaderCell)
                {
                    AddStylesToRowHeaderCells(modelDataCell, e);
                }

                if (modelDataCell.TableCellType == PivotGridTableCellType.ColumnHeaderCell)
                {
                    AddStylesToColumnHeaderCells(modelDataCell, e);
                }

                if (modelDataCell.IsGrandTotalCell)
                {
                    e.ExportedCell.Style.BackColor = Color.FromArgb(128, 128, 128);
                    e.ExportedCell.Style.Font.Bold = true;
                }

                if (IsTotalDataCell(modelDataCell))
                {
                    e.ExportedCell.Style.BackColor = Color.FromArgb(150, 150, 150);
                    e.ExportedCell.Style.Font.Bold = true;
                    AddBorders(e);
                }

                if (IsGrandTotalDataCell(modelDataCell))
                {
                    e.ExportedCell.Style.BackColor = Color.FromArgb(128, 128, 128);
                    e.ExportedCell.Style.Font.Bold = true;
                    AddBorders(e);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdDocumentos_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                grdDocumentos.DataSource = JsonHelper.JsonDeserialize<List<gsDocVenta_ControlCosto_MarcaProductoResult>>((string)ViewState["lstDocumentos"]);
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }
        }
    }
}