using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.DM;
using GS.SISGEGS.Web.ReporteVentaWCF;
using System.Drawing;
using Telerik.Web.UI;
using GS.SISGEGS.Web.Helpers;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace GS.SISGEGS.Web.Comercial.Reportes.ReporteBI
{
    public partial class frmReporteVenta_Familias : System.Web.UI.Page
    {
        private void Vendedor_Listar() {
            ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient();
            List<ReporteVentas_R3_1Result> Lista = new List<ReporteVentas_R3_1Result>();

            try {
                DateTime fechaInicio = new DateTime(dpFecInicio.SelectedDate.Value.Year, dpFecInicio.SelectedDate.Value.Month, 1);
                DateTime fechaFinal = dpFecFinal.SelectedDate.Value.AddMonths(1);
                fechaFinal = (new DateTime(fechaFinal.Year, fechaFinal.Month, 1)).AddDays(-1);
                Lista = objReporteVentaWCF.ReporteVentas_R3_1(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, fechaInicio, fechaFinal, 0 ,null).ToList();

                ViewState["ListaReporte"] = JsonHelper.JsonSerializer(Lista);

                if(rbContraer.Checked==true)
                {
                    gsReporteVentas_Familia.RowGroupsDefaultExpanded = false;
                }
                else
                {
                    gsReporteVentas_Familia.RowGroupsDefaultExpanded = true;
                }

                gsReporteVentas_Familia.DataSource = Lista;
                gsReporteVentas_Familia.DataBind();

                lblReporte.Value = "1"; 

            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void gsReporteVentas_Familia_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (lblReporte.Value == "1")
                {
                    gsReporteVentas_Familia.DataSource = JsonHelper.JsonDeserialize<List<ReporteVentas_R3_1Result>>((string)ViewState["ListaReporte"]);
                }

            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al consultar NeedDataSource", "");
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

                    //Telerik.Web.UI.RadSkinManager.GetCurrent(this.Page).ApplySkin(gsReporteVentas_Familia, "BlackMetroTouch");
                    dpFecInicio.SelectedDate = DateTime.Now.AddMonths(-6);
                    dpFecFinal.SelectedDate = DateTime.Now;
                 
                    Vendedor_Listar();
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

            try {
                Vendedor_Listar();
            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void gsReporteVentas_Familia_CellDataBound(object sender, PivotGridCellDataBoundEventArgs e)
        {
            try
            {
                if (e.Cell is PivotGridDataCell)
                {
                    PivotGridDataCell cell = e.Cell as PivotGridDataCell;
                    cell.Font.Bold = false;
                    cell.BackColor = Color.Lavender;
                    cell.HorizontalAlign = HorizontalAlign.Right;

                    if (cell.CellType == PivotGridDataCellType.DataCell)
                    {
                        cell.Font.Bold = false; 
                        cell.BackColor = Color.Lavender;
                        cell.HorizontalAlign = HorizontalAlign.Right;

                        if (cell.ParentColumnIndexes[2].ToString() == "Margen_cal")
                        {
                            
                            cell.HorizontalAlign = HorizontalAlign.Right;

                            int price = Convert.ToInt32(cell.DataItem);
                            if (price < 30)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                            else if (price >= 30 & price < 50)
                            {
                                cell.BackColor = Color.Yellow;
                                cell.ForeColor = Color.Black;
                            }
                            else if (price >= 50 )
                            {
                                cell.BackColor = Color.LawnGreen;
                                cell.ForeColor = Color.Black;
                            }
                        }

                        if (cell.ParentColumnIndexes[2].ToString() == "Variacion_P_c")
                        {
                            cell.HorizontalAlign = HorizontalAlign.Right;

                            int price = Convert.ToInt32(cell.DataItem);
                            if (price < 0)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                        }

                    }

                    //Black - color the Sub totals rows cells
                    if (cell.CellType == PivotGridDataCellType.RowTotalDataCell)
                    {
                        cell.BackColor = Color.LightSteelBlue;
                        cell.Font.Bold = true;
                        cell.HorizontalAlign = HorizontalAlign.Right;


                        if (cell.ParentColumnIndexes[2].ToString() == "Margen_cal")
                        {

                            cell.HorizontalAlign = HorizontalAlign.Right;

                            int price = Convert.ToInt32(cell.DataItem);
                            if (price < 30)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                            else if (price >= 30 & price < 50)
                            {
                                cell.BackColor = Color.Yellow;
                                cell.ForeColor = Color.Black;
                            }
                            else if (price >= 50)
                            {
                                cell.BackColor = Color.LawnGreen;
                                cell.ForeColor = Color.Black;
                            }
                        }

                        if (cell.ParentColumnIndexes[2].ToString() == "Variacion_P_c")
                        {
                            cell.HorizontalAlign = HorizontalAlign.Right;

                            int price = Convert.ToInt32(cell.DataItem);
                            if (price < 0)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                        }


                    }
                    //Yellow - color the column/row / grand total rows   
                    if (cell.CellType == PivotGridDataCellType.RowGrandTotalDataCell)
                    {
                        cell.BackColor = Color.Silver;
                        cell.Font.Bold = true;
                        cell.HorizontalAlign = HorizontalAlign.Right;

                        if (cell.ParentColumnIndexes[2].ToString() == "Margen_cal")
                        {

                            cell.HorizontalAlign = HorizontalAlign.Right;

                            int price = Convert.ToInt32(cell.DataItem);
                            if (price < 30)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                            else if (price >= 30 & price < 50)
                            {
                                cell.BackColor = Color.Yellow;
                                cell.ForeColor = Color.Black;
                            }
                            else if (price >= 50)
                            {
                                cell.BackColor = Color.LawnGreen;
                                cell.ForeColor = Color.Black;
                            }
                        }

                        if (cell.ParentColumnIndexes[2].ToString() == "Variacion_P_c")
                        {
                            cell.HorizontalAlign = HorizontalAlign.Right;

                            int price = Convert.ToInt32(cell.DataItem);
                            if (price < 0)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                        }

                    }
                    //Red - color the totals Column cells
                    if (cell.CellType == PivotGridDataCellType.ColumnTotalDataCell)
                    {
                        cell.BackColor = Color.LightGray;
                        cell.Font.Bold = true;
                        cell.HorizontalAlign = HorizontalAlign.Right;

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 Margen_cal")
                        {

                            cell.HorizontalAlign = HorizontalAlign.Right;

                            int price = Convert.ToInt32(cell.DataItem);
                            if (price < 30)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                            else if (price >= 30 & price < 50)
                            {
                                cell.BackColor = Color.Yellow;
                                cell.ForeColor = Color.Black;
                            }
                            else if (price >= 50)
                            {
                                cell.BackColor = Color.LawnGreen;
                                cell.ForeColor = Color.Black;
                            }
                        }

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 Variacion_P_c")
                        {
                            cell.HorizontalAlign = HorizontalAlign.Right;

                            int price = Convert.ToInt32(cell.DataItem);
                            if (price < 0)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                        }

                    }
                    //Orange - color SubTotales column cells
                    if (cell.CellType == PivotGridDataCellType.RowAndColumnTotal)
                    {
                        cell.BackColor = Color.LightSteelBlue;
                        cell.Font.Bold = true;

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 Margen_cal")
                        {

                            cell.HorizontalAlign = HorizontalAlign.Right;

                            int price = Convert.ToInt32(cell.DataItem);
                            if (price < 30)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                            else if (price >= 30 & price < 50)
                            {
                                cell.BackColor = Color.Yellow;
                                cell.ForeColor = Color.Black;
                            }
                            else if (price >= 50)
                            {
                                cell.BackColor = Color.LawnGreen;
                                cell.ForeColor = Color.Black;
                            }
                        }

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 Variacion_P_c")
                        {
                            cell.HorizontalAlign = HorizontalAlign.Right;

                            int price = Convert.ToInt32(cell.DataItem);
                            if (price < 0)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                        }
                    }
                    // Azul - Color GrandTotal Column 
                    if (cell.CellType == PivotGridDataCellType.RowGrandTotalColumnTotal)
                    {
                        cell.BackColor = Color.Silver;
                        cell.Font.Bold = true;

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 Margen_cal")
                        {

                            cell.HorizontalAlign = HorizontalAlign.Right;

                            int price = Convert.ToInt32(cell.DataItem);
                            if (price < 30)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                            else if (price >= 30 & price < 50)
                            {
                                cell.BackColor = Color.Yellow;
                                cell.ForeColor = Color.Black;
                            }
                            else if (price >= 50)
                            {
                                cell.BackColor = Color.LawnGreen;
                                cell.ForeColor = Color.Black;
                            }
                        }

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 Variacion_P_c")
                        {
                            cell.HorizontalAlign = HorizontalAlign.Right;

                            int price = Convert.ToInt32(cell.DataItem);
                            if (price < 0)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                        }
                    }

                    //color the column AND row grand total cell 
                    if (cell.CellType == PivotGridDataCellType.RowAndColumnGrandTotal)
                    {
                        cell.BackColor = Color.Blue;
                        cell.Font.Bold = true;
                    }

                }

                ////style the row headers based on their level
                else if (e.Cell is PivotGridRowHeaderCell)
                {
                    PivotGridRowHeaderCell cell = e.Cell as PivotGridRowHeaderCell;
                    if (cell.ParentIndexes != null && cell.ParentIndexes.Length == 1)
                    {
                        cell.Font.Bold = true;
                    }
                    else
                    {
                        //cell.Font.Italic = false;
                    }
                }

                ////style the column headers based on their level
                else if (e.Cell is PivotGridColumnHeaderCell)
                {
                    PivotGridColumnHeaderCell cell = e.Cell as PivotGridColumnHeaderCell;
                    if (cell.ParentIndexes != null)
                    {
                        cell.Font.Bold = false;
                        //cell.HorizontalAlign = HorizontalAlign.Center;
                    }
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            //give a color to the various cells cells

        }

        protected void gsReporteVentas_Familia_PreRender(object sender, EventArgs e)
        {
            PivotGridAggregateField field1 = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "KgLt").FirstOrDefault() as PivotGridAggregateField;
            field1.CellStyle.Font.Bold = true;
            field1.CellStyle.BackColor = Color.Khaki;

            PivotGridAggregateField KgLtPPTO = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "KgLtPPTO").FirstOrDefault() as PivotGridAggregateField;
            KgLtPPTO.CellStyle.Font.Bold = true;
            KgLtPPTO.CellStyle.BackColor = Color.LightGreen;

            PivotGridAggregateField SaldoDolares = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "SaldoDolares").FirstOrDefault() as PivotGridAggregateField;
            SaldoDolares.CellStyle.Font.Bold = true;
            SaldoDolares.CellStyle.BackColor = Color.Khaki;

            PivotGridAggregateField VentaPPTO_Dolares = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "VentaPPTO_Dolares").FirstOrDefault() as PivotGridAggregateField;
            VentaPPTO_Dolares.CellStyle.Font.Bold = true;
            VentaPPTO_Dolares.CellStyle.BackColor = Color.LightGreen;


            PivotGridAggregateField CostoDolares = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "CostoDolares").FirstOrDefault() as PivotGridAggregateField;
            CostoDolares.CellStyle.Font.Bold = true;
            CostoDolares.CellStyle.BackColor = Color.Khaki;

            PivotGridAggregateField CostoPPTO_Dolares = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "CostoPPTO_Dolares").FirstOrDefault() as PivotGridAggregateField;
            CostoPPTO_Dolares.CellStyle.Font.Bold = true;
            CostoPPTO_Dolares.CellStyle.BackColor = Color.LightGreen;


            PivotGridAggregateField UtilidadDolares = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "UtilidadDolares").FirstOrDefault() as PivotGridAggregateField;
            UtilidadDolares.CellStyle.Font.Bold = true;
            UtilidadDolares.CellStyle.BackColor = Color.Orange;


            PivotGridAggregateField PUnitario = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "PUnitario_c").FirstOrDefault() as PivotGridAggregateField;
            PUnitario.CellStyle.Font.Bold = true;
            PUnitario.CellStyle.BackColor = Color.Khaki;

            PivotGridAggregateField PUnitarioPPT = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "PUnitarioPPT_c").FirstOrDefault() as PivotGridAggregateField;
            PUnitarioPPT.CellStyle.Font.Bold = true;
            PUnitarioPPT.CellStyle.BackColor = Color.LightGreen;

            PivotGridAggregateField CUnitario = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "CUnitario_c").FirstOrDefault() as PivotGridAggregateField;
            CUnitario.CellStyle.Font.Bold = true;
            CUnitario.CellStyle.BackColor = Color.Khaki;

            PivotGridAggregateField CUnitarioPPTO_c = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "CUnitarioPPTO_c").FirstOrDefault() as PivotGridAggregateField;
            CUnitarioPPTO_c.CellStyle.Font.Bold = true;
            CUnitarioPPTO_c.CellStyle.BackColor = Color.LightGreen;


            PivotGridAggregateField Margen_c = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "Margen_cal").FirstOrDefault() as PivotGridAggregateField;
            Margen_c.CellStyle.Font.Bold = true;
            Margen_c.CellStyle.BackColor = Color.Khaki;

            PivotGridAggregateField Margen_PPTO_c = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "Margen_PPTO_cal").FirstOrDefault() as PivotGridAggregateField;
            Margen_PPTO_c.CellStyle.Font.Bold = true;
            Margen_PPTO_c.CellStyle.BackColor = Color.LightGreen;

            PivotGridAggregateField Variacion_P_c = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "Variacion_P_c").FirstOrDefault() as PivotGridAggregateField;
            Variacion_P_c.CellStyle.Font.Bold = true;
            Variacion_P_c.CellStyle.BackColor = Color.Orange;


        }

        protected void gsReporteVentas_Familia_ItemNeedCalculation(object sender, PivotGridCalculationEventArgs e)
        {

            if (e.DataField.ToString() == "Margen_cal")
            {
                if (e.CalculatedValue != null)
                {
                    if (double.IsNaN((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                    else if (double.IsInfinity((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                }
            }

            if (e.DataField.ToString() == "Margen_PPTO_cal")
            {
                if (e.CalculatedValue != null)
                {
                    if (double.IsNaN((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                    else if (double.IsInfinity((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                }
            }


            if (e.DataField.ToString() == "Variacion_P_c")
            {
                if (e.CalculatedValue != null)
                {
                    if (double.IsNaN((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                    else if (double.IsInfinity((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                }
            }
            if (e.DataField.ToString() == "PUnitario_c")
            {
                if (e.CalculatedValue != null)
                {
                    if (double.IsNaN((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                    else if (double.IsInfinity((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                }
            }
            

            if (e.DataField.ToString() == "PUnitarioPPT_c")
            {
                if (e.CalculatedValue != null)
                {
                    if (double.IsNaN((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                    else if (double.IsInfinity((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                }
            }

            if (e.DataField.ToString() == "CUnitario_c")
            {
                if (e.CalculatedValue != null)
                {
                    if (double.IsNaN((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                    else if (double.IsInfinity((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                }
            }

            if (e.DataField.ToString() == "CUnitarioPPTO_c")
            {
                if (e.CalculatedValue != null)
                {
                    if (double.IsNaN((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                    else if (double.IsInfinity((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                }
            }

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string alternateText = "Xlsx";
            gsReporteVentas_Familia.ExportSettings.Excel.Format = (PivotGridExcelFormat)Enum.Parse(typeof(PivotGridExcelFormat), alternateText);
            gsReporteVentas_Familia.ExportSettings.IgnorePaging = true;
            gsReporteVentas_Familia.ExportToExcel();
        }

        protected void RadPivotGrid1_PivotGridCellExporting(object sender, PivotGridCellExportingArgs e)
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
    }
}