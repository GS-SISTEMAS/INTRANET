using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.Web.ReporteVentaWCF;
using GS.SISGEGS.DM;
using Telerik.Web.UI;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.AgendaWCF;
using System.Drawing;
using System.Globalization;

namespace GS.SISGEGS.Web.Comercial.Proyectado
{
    public partial class frmVentaPronosticoVsReal : System.Web.UI.Page
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

                    dpPeriodoInicio.DbSelectedDate = new DateTime(DateTime.Now.Year, 1, 1);
                    dpPeriodoFinal.DbSelectedDate = DateTime.Now;
                   
                    Zona_Cargar(0); 

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Zona_Cargar(int id_zona)
        {
            try
            {
                AgendaWCFClient objAgendaWCF = new AgendaWCFClient();
                gsZona_ListarResult objZona = new gsZona_ListarResult();
                List<gsZona_ListarResult> lstZona;

                lstZona = objAgendaWCF.Agenda_ListarZona(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, id_zona).ToList();

                lstZona.Insert(0, objZona);
                objZona.Zona = "Todos";
                objZona.ID_Zona = 0;

                var lstZonas = from x in lstZona
                               select new
                               {
                                   x.ID_Zona,
                                   DisplayID = String.Format("{0}", x.ID_Zona),
                                   DisplayField = String.Format("{0}", x.Zona)
                                   //DisplayField = String.Format("{0} {1} {2} {3}", x.Placa, x.Modelo, x.Marca, x.Agenda)
                               };

                cboZona.DataSource = lstZonas;
                cboZona.DataTextField = "DisplayField";
                cboZona.DataValueField = "DisplayID";
                cboZona.DataBind();
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
                if (chkExpandir.Checked) grdComparativoPivot.RowGroupsDefaultExpanded = true;
                else grdComparativoPivot.RowGroupsDefaultExpanded = false;

                ReporteVenta_Listar();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void ReporteVenta_Listar()
        {
            ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient();
            List<gsVentaPronostico_vs_RealResult> lst;

            try
            {
                DateTime fechaInicio = dpPeriodoInicio.SelectedDate.Value;
                DateTime fechaFinal = new DateTime(dpPeriodoFinal.SelectedDate.Value.Year, dpPeriodoFinal.SelectedDate.Value.Month, 1).AddMonths(1).AddDays(-1);

                int id_zona = int.Parse(cboZona.SelectedValue);
                string id_vendedor = "";

                lst = objReporteVentaWCF.gsVentaPronostico_vs_Real(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, fechaInicio, fechaFinal, id_zona, id_vendedor).ToList();

                //grdComparativo.DataSource = lst;
                //grdComparativo.DataBind();

                grdComparativoPivot.DataSource = lst;
                grdComparativoPivot.DataBind();

                //ViewState["fechaInicio"] = firstDayOfMonth.ToString("dd/MM/yyyy");
                ViewState["lstPlaneamiento"] = JsonHelper.JsonSerializer(lst);

                lblMensaje.Text = "Se han encontrado " + lst.Count.ToString() + " registro.";
                lblMensaje.CssClass = "mensajeExito";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                //string alternateText = (sender as RadButton).AlternateText;

                //grdComparativo.ExportSettings.FileName = "Reporte_Pronostico_vs_real_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day;
                //grdComparativo.ExportSettings.ExportOnlyData = true;
                //grdComparativo.ExportSettings.IgnorePaging = true;
                //grdComparativo.ExportSettings.OpenInNewWindow = true;
                //grdComparativo.MasterTableView.ExportToExcel();
                
                grdComparativoPivot.ExportSettings.Excel.Format = (PivotGridExcelFormat)Enum.Parse(typeof(PivotGridExcelFormat), "Xlsx");
                grdComparativoPivot.ExportSettings.FileName = "Reporte_Pronostico_vs_real_" + DateTime.Now.ToString("yyyyMMddHmm");
                grdComparativoPivot.ExportToExcel();

                Page.Response.ClearHeaders();
                Page.Response.ClearContent();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }
        }


      
        //protected void grdComparativo_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        //{
        //    if (Session["Usuario"] == null)
        //        Response.Redirect("~/Security/frmCerrar.aspx");

        //    try
        //    {
        //        if (ViewState["lstPlaneamiento"] != null)
        //        {
        //            grdComparativo.DataSource = JsonHelper.JsonDeserialize<List<gsPronostico_vs_RealResult>>((string)ViewState["lstPlaneamiento"]);
        //            //grdProducto.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMensaje.Text = ex.Message;
        //        lblMensaje.CssClass = "mensajeError";
        //    }
        //}
        protected void grdComparativoPivot_CellDataBound(object sender, Telerik.Web.UI.PivotGridCellDataBoundEventArgs e)
        {
            //&month& patterns is added in the DataFormatString, so it could be catched and parsed to month name
            if (e.Cell is PivotGridColumnHeaderCell)
            {
                int month = 0;
                string strMes = "";

                PivotGridColumnHeaderCell cell = e.Cell as PivotGridColumnHeaderCell;
                strMes = cell.Text.ToString();


                if (strMes.Length > 0 && strMes.Length < 3)
                {
                    string cellValue = strMes;
                    if (int.TryParse(cellValue, out month))
                    {
                        cell.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(cellValue));
                    }
                }
            }
        }

        protected void grdComparativoPivot_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (ViewState["lstPlaneamiento"] != null)
                {
                    grdComparativoPivot.DataSource = JsonHelper.JsonDeserialize<List<gsVentaPronostico_vs_RealResult>>((string)ViewState["lstPlaneamiento"]);
                    //grdProducto.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
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

        protected void grdComparativoPivot_PivotGridCellExporting(object sender, PivotGridCellExportingArgs e)
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

    }
}