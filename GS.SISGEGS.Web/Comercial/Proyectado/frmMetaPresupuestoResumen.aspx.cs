using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using GS.SISGEGS.BE;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.ItemWCF;
using GS.SISGEGS.Web.DocumentoWCF;
using GS.SISGEGS.Web.FormaPagoWCF;
using GS.SISGEGS.Web.OrdenVentaWCF;
using GS.SISGEGS.Web.LoginWCF;
using GS.SISGEGS.Web.OrdenCompraWCF;
using GS.SISGEGS.Web.PlanificacionWCF;
using System.Net;
using System.Drawing;


namespace GS.SISGEGS.Web.Comercial.Proyectado
{
    public partial class frmMetaPresupuestoResumen : System.Web.UI.Page
    {
        PlanificacionWCFClient objPlanificacion = new PlanificacionWCFClient();
        List<USP_SEL_VENTA_POR_VENDEDOR2> _lstresumen = new List<USP_SEL_VENTA_POR_VENDEDOR2>();
        #region Procedimientos
        private void CargarResumen()
        {
            _lstresumen = new List<USP_SEL_VENTA_POR_VENDEDOR2>();
            objPlanificacion = new PlanificacionWCFClient();
            _lstresumen = objPlanificacion.Obtener_PresupuestoResumen(
                 ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario,
                Convert.ToDateTime(rmyPre.SelectedDate), DateTime.Now).ToList();

            Session["lstPresupuestoResumen"] = JsonHelper.JsonSerializer(_lstresumen);
            rpgResumenZona.DataSource = _lstresumen;
            rpgResumenZona.DataBind();

            lblTitulo.Text = "Resumen de Presupuestos Confirmados " + RetornaMes(rmyPre.SelectedDate.Value.Month) + " " + rmyPre.SelectedDate.Value.Year.ToString();
        }
        private string RetornaMes(int mes)
        {
            string mes2 = string.Empty;
            switch (mes)
            {
                case 1:
                    mes2 = "Enero";
                    break;
                case 2:
                    mes2 = "Feberero";
                    break;
                case 3:
                    mes2 = "Marzo";
                    break;
                case 4:
                    mes2 = "Abril";
                    break;
                case 5:
                    mes2 = "Mayo";
                    break;
                case 6:
                    mes2 = "Junio";
                    break;
                case 7:
                    mes2 = "Julio";
                    break;
                case 8:
                    mes2 = "Agosto";
                    break;
                case 9:
                    mes2 = "Setiembre";
                    break;
                case 10:
                    mes2 = "Octubre";
                    break;
                case 11:
                    mes2 = "Noviembre";
                    break;
                case 12:
                    mes2 = "Diciembre";
                    break;

            }
            return mes2;
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
                    
                    
                    rmyPre.SelectedDate= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    Session["lstPresupuestoResumen"] = null;
                    CargarResumen();


                    //lblMensaje.Text = "La página cargo correctamente";
                    //lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                //lblMensaje.Text = ex.Message;
                //lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                CargarResumen();
            }
            catch (Exception ex)
            {
                rwmPre.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
            
        }

        protected void rpgResumenZona_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if(Session["lstPresupuestoResumen"] != null)
                    rpgResumenZona.DataSource = JsonHelper.JsonDeserialize<List<USP_SEL_VENTA_POR_VENDEDOR2>>((string)Session["lstPresupuestoResumen"]);
            }
            catch (Exception ex)
            {
                rwmPre.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

        protected void rpgResumenZona_CellDataBound(object sender, PivotGridCellDataBoundEventArgs e)
        {
            try
            {
                if (e.Cell is PivotGridDataCell)
                {
                    PivotGridDataCell cell = e.Cell as PivotGridDataCell;

                    if (cell.CellType == PivotGridDataCellType.DataCell)
                    {
                        cell.BackColor = Color.FromArgb(255, 255, 247);
                        cell.Font.Bold = false;
                        //if (cell.ParentColumnIndexes[2].ToString() == "Sum of TotalPrice")
                        //{
                        //    cell.Font.Italic = true;
                        //}
                    }
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void rpgResumenZona_PivotGridCellExporting(object sender, PivotGridCellExportingArgs e)
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


        #region EXPORTAR_XLS
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

        #endregion

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string alternateText = "Xlsx";
            
            rpgResumenZona.ExportSettings.Excel.Format = (PivotGridExcelFormat)Enum.Parse(typeof(PivotGridExcelFormat), alternateText);
            rpgResumenZona.ExportSettings.IgnorePaging = true;
            rpgResumenZona.ExportToExcel();
        }
    }
}