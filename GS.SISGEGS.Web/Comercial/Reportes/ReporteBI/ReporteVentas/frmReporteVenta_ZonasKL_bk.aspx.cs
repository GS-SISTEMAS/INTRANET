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
    public partial class frmReporteVenta_ZonasKL_bk : System.Web.UI.Page
    {
        private void Vendedor_Listar() {
            ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient();
            List<ReporteVentas_ZonasResult> Lista = new List<ReporteVentas_ZonasResult>();

            try {
                DateTime fechaInicio = new DateTime(dpFecInicio.SelectedDate.Value.Year, dpFecInicio.SelectedDate.Value.Month, 1);
                DateTime fechaFinal = dpFecFinal.SelectedDate.Value.AddMonths(1);
                fechaFinal = (new DateTime(fechaFinal.Year, fechaFinal.Month, 1)).AddDays(-1);
                Lista = objReporteVentaWCF.ReporteVentas_Zonas(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, fechaInicio, fechaFinal, 0 ,null).ToList();

                ViewState["ListaReporteZonas"] = JsonHelper.JsonSerializer(Lista);

                if(rbContraer.Checked==true)
                {
                    gsReporteVentas_Zonas.RowGroupsDefaultExpanded = false;
                }
                else
                {
                    gsReporteVentas_Zonas.RowGroupsDefaultExpanded = true;
                }

                gsReporteVentas_Zonas.DataSource = Lista;
                gsReporteVentas_Zonas.DataBind();

                lblReporte.Value = "1"; 

            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void gsReporteVentas_Zonas_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (lblReporte.Value == "1")
                {
                    gsReporteVentas_Zonas.DataSource = JsonHelper.JsonDeserialize<List<ReporteVentas_ZonasResult>>((string)ViewState["ListaReporteZonas"]);
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
                    dpFecInicio.SelectedDate = DateTime.Now.AddMonths(-2);
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


        protected void gsReporteVentas_Zonas_CellDataBound(object sender, PivotGridCellDataBoundEventArgs e)
        {
            try
            {


                if (e.Cell is PivotGridDataCell)
                {
                    PivotGridDataCell cell = e.Cell as PivotGridDataCell;
                    cell.Font.Bold = false;
                    cell.BackColor = Color.White;
                    cell.HorizontalAlign = HorizontalAlign.Right;
 
                    if (cell.CellType == PivotGridDataCellType.DataCell)
                    {
                        cell.Font.Bold = false;
                        cell.BackColor = Color.White;
                        cell.HorizontalAlign = HorizontalAlign.Right;

                        if (cell.ParentColumnIndexes[2].ToString() == "Crecimiento")
                        {
                            //cell.BackColor = Color.Yellow;
                            //cell.Font.Italic = true;
                            cell.HorizontalAlign = HorizontalAlign.Right;

                            decimal porcentaje = Convert.ToDecimal(cell.DataItem);
                            if (porcentaje <= 0)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                        }

                        //if (cell.ParentColumnIndexes[1].ToString() == "VariacionPrecio")
                        //{
                        //    //cell.BackColor = Color.Yellow;
                        //    //cell.Font.Italic = true;
                        //    cell.HorizontalAlign = HorizontalAlign.Right;

                        //    int price = Convert.ToInt32(cell.DataItem);
                        //    if (price <= 0)
                        //    {
                        //        cell.BackColor = Color.Tomato;
                        //        cell.ForeColor = Color.White;
                        //    }
                        //    else if (price > 0)
                        //    {
                        //        cell.BackColor = Color.LawnGreen;
                        //        cell.ForeColor = Color.Black;
                        //    }

                        //}

                    }

                    //Black - color the Sub totals rows cells
                    if (cell.CellType == PivotGridDataCellType.RowTotalDataCell)
                    {
                        cell.BackColor = Color.PaleGreen;
                        cell.Font.Bold = true;
                        cell.HorizontalAlign = HorizontalAlign.Right;

                        switch ((cell.Field as PivotGridAggregateField).DataField)
                        {

                            //color the cells showing totals for TotalPrice based on their value
                            //case "SaldoDolares":

                            //    //cell.Field.CellStyle.CssClass = "aggregateCustom1";
                            //    cell.BackColor = Color.Black;
                            //    cell.Font.Bold = true;
                            //    cell.HorizontalAlign = HorizontalAlign.Right;

                            //    break;

                            ////color the cells showing totals for Quantity based on their value
                            //case "KgLtPro":
                            //    //if (cell.DataItem != null && cell.DataItem.ToString().Length > 0)
                            //    //{
                            //    cell.BackColor = Color.Silver;
                            //    cell.Font.Bold = true;
                            //    cell.HorizontalAlign = HorizontalAlign.Right;

                            //    break;

                            //default:
                            //    cell.BackColor = Color.Silver;
                            //    cell.Font.Bold = true;
                            //    cell.HorizontalAlign = HorizontalAlign.Right;
                            //    break;


                        }
                    }
                    //Yellow - color the column/row / grand total rows   
                    if (cell.CellType == PivotGridDataCellType.RowGrandTotalDataCell)
                    {
                        cell.BackColor = Color.Silver;
                        cell.Font.Bold = true;
                        cell.HorizontalAlign = HorizontalAlign.Right;

                        switch ((cell.Field as PivotGridAggregateField).DataField)
                        {
                            //case "KgLt":

                            //    cell.BackColor = Color.Silver;
                            //    cell.Font.Bold = true;
                            //    cell.HorizontalAlign = HorizontalAlign.Right;

                            //    break;
                            //case "KgLtPro":
                            //    double quantity = Convert.ToDouble(cell.DataItem);
                            //    cell.BackColor = Color.Silver;
                            //    cell.Font.Bold = true;
                            //    cell.HorizontalAlign = HorizontalAlign.Right;

                            //    break;
                            //default:
                            //    cell.BackColor = Color.Silver;
                            //    cell.Font.Bold = true;
                            //    cell.HorizontalAlign = HorizontalAlign.Right;
                            //    break;

                        }
                    }
                    //Red - color the totals Column cells
                    if (cell.CellType == PivotGridDataCellType.ColumnTotalDataCell)
                    {
                        cell.BackColor = Color.LightGray;
                        cell.Font.Bold = true;
                        cell.HorizontalAlign = HorizontalAlign.Right;

                    }
                    //Orange - color SubTotales column cells
                    if (cell.CellType == PivotGridDataCellType.RowAndColumnTotal)
                    {
                        cell.BackColor = Color.Silver;
                        cell.Font.Bold = true;
                    }
                    // Azul - Color GrandTotal Column 
                    if (cell.CellType == PivotGridDataCellType.RowGrandTotalColumnTotal)
                    {
                        cell.BackColor = Color.Orange;
                        cell.Font.Bold = true;
                    }


                    ///////// Sin efecto 
                    //color the column/row grand total cells 
                    if (cell.CellType == PivotGridDataCellType.ColumnGrandTotalDataCell)
                    {
                        cell.BackColor = Color.Blue;
                        cell.Font.Bold = true;
                        cell.HorizontalAlign = HorizontalAlign.Right;

                        switch ((cell.Field as PivotGridAggregateField).DataField)
                        {
                            //case "KgLt":

                            //    cell.BackColor = Color.Silver;
                            //    cell.Font.Bold = true;
                            //    cell.HorizontalAlign = HorizontalAlign.Right;

                            //    break;
                            //case "KgLtPro":
                            //    double quantity = Convert.ToDouble(cell.DataItem);
                            //    cell.BackColor = Color.Silver;
                            //    cell.Font.Bold = true;
                            //    cell.HorizontalAlign = HorizontalAlign.Right;

                            //    break;
                            //default:
                            //    cell.BackColor = Color.Silver;
                            //    cell.Font.Bold = true;
                            //    cell.HorizontalAlign = HorizontalAlign.Right;
                            //    break;

                        }
                    }
                    //color the column AND row grand total cell 
                    if (cell.CellType == PivotGridDataCellType.ColumnGrandTotalRowTotal)
                    {
                        cell.BackColor = Color.Pink;
                        cell.Font.Bold = true;
                    }
                    if (cell.CellType == PivotGridDataCellType.RowAndColumnGrandTotal)
                    {
                        cell.BackColor = Color.Violet;
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

        protected void gsReporteVentas_Zonas_PreRender(object sender, EventArgs e)
        {
            ////provide custom CSS class for the TotalPrice aggregate field and column headers
            //PivotGridAggregateField field1 = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "KgLt").FirstOrDefault() as PivotGridAggregateField;
            //field1.CellStyle.Font.Bold = true;
            //field1.CellStyle.BackColor = Color.Khaki;
            ////field1.CellStyle.CssClass = "aggregateCustom1";
            ////field1.RenderingControl.CssClass = "aggregateCustom1";

            ////provide custom CSS class for the Quantity aggregate field and column headers
            //PivotGridAggregateField KgLtPPTO = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "KgLtPPTO").FirstOrDefault() as PivotGridAggregateField;
            //KgLtPPTO.CellStyle.Font.Bold = true;
            //KgLtPPTO.CellStyle.BackColor = Color.LightGreen;
            ////field2.CellStyle.CssClass = "aggregateCustom2";
            ////field2.RenderingControl.CssClass = "aggregateCustom2";

            //PivotGridAggregateField SaldoDolares = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "SaldoDolares").FirstOrDefault() as PivotGridAggregateField;
            //SaldoDolares.CellStyle.Font.Bold = true;
            //SaldoDolares.CellStyle.BackColor = Color.Khaki;

            //PivotGridAggregateField VentaPPTO_Dolares = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "VentaPPTO_Dolares").FirstOrDefault() as PivotGridAggregateField;
            //VentaPPTO_Dolares.CellStyle.Font.Bold = true;
            //VentaPPTO_Dolares.CellStyle.BackColor = Color.LightGreen;


            //PivotGridAggregateField CostoDolares = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "CostoDolares").FirstOrDefault() as PivotGridAggregateField;
            //CostoDolares.CellStyle.Font.Bold = true;
            //CostoDolares.CellStyle.BackColor = Color.Khaki;

            //PivotGridAggregateField CostoPPTO_Dolares = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "CostoPPTO_Dolares").FirstOrDefault() as PivotGridAggregateField;
            //CostoPPTO_Dolares.CellStyle.Font.Bold = true;
            //CostoPPTO_Dolares.CellStyle.BackColor = Color.Khaki;


            //PivotGridAggregateField UtilidadDolares = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "UtilidadDolares").FirstOrDefault() as PivotGridAggregateField;
            //UtilidadDolares.CellStyle.Font.Bold = true;
            //UtilidadDolares.CellStyle.BackColor = Color.Khaki;

            //PivotGridAggregateField UtilidadDolares_P = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "UtilidadDolares_P").FirstOrDefault() as PivotGridAggregateField;
            //UtilidadDolares_P.CellStyle.Font.Bold = true;
            //UtilidadDolares_P.CellStyle.BackColor = Color.Orange;

            //PivotGridAggregateField PUnitario = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "PUnitario").FirstOrDefault() as PivotGridAggregateField;
            //PUnitario.CellStyle.Font.Bold = true;
            //PUnitario.CellStyle.BackColor = Color.Khaki;

            //PivotGridAggregateField PUnitarioPPT = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "PUnitarioPPT").FirstOrDefault() as PivotGridAggregateField;
            //PUnitarioPPT.CellStyle.Font.Bold = true;
            //PUnitarioPPT.CellStyle.BackColor = Color.LightGreen;


            //PivotGridAggregateField CUnitario = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "CUnitario").FirstOrDefault() as PivotGridAggregateField;
            //CUnitario.CellStyle.Font.Bold = true;
            //CUnitario.CellStyle.BackColor = Color.Khaki;

            //PivotGridAggregateField Margen_P = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "Margen_P").FirstOrDefault() as PivotGridAggregateField;
            //Margen_P.CellStyle.Font.Bold = true;
            //Margen_P.CellStyle.BackColor = Color.Khaki;

            //PivotGridAggregateField Variacion_P = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "Variacion_P").FirstOrDefault() as PivotGridAggregateField;
            //Variacion_P.CellStyle.Font.Bold = true;
            //Variacion_P.CellStyle.BackColor = Color.Orange;



        }

        protected void gsReporteVentas_Zonas_ItemNeedCalculation(object sender, PivotGridCalculationEventArgs e)
        {
            //if (e.GroupName.ToString() == "Forecast for 1999")
            //{
            //    //Calculation of AggregateSummaryValue for our CalculatedItem, based on the values from other items
            //    try
            //    {
            //        LagrangeInterpolate interpolate = new LagrangeInterpolate();
            //        interpolate.Add(1996, double.Parse(e.GetAggregateSummaryValue(1996).GetValue().ToString()));
            //        interpolate.Add(1997, double.Parse(e.GetAggregateSummaryValue(1997).GetValue().ToString()));
            //        interpolate.Add(1998, double.Parse(e.GetAggregateSummaryValue(1998).GetValue().ToString()));

            //        //Set new calulated value for given field.
            //        e.CalculatedValue = new DoubleAggregateValue(Math.Max(interpolate.InterpolateX(1998.5), 0));
            //    }
            //    catch (Exception)
            //    {
            //        //If unable to predict the forecast, leave the cell empty.
            //        e.CalculatedValue = null;
            //    }
            //}
            //else 
            if (e.DataField.ToString() == "Cumplimiento")
            {
                //The data calculated by CalculationExpression="{0}/{1}" added in the markup is additionally modified here
                if (e.CalculatedValue != null)
                {
                    if (double.IsNaN((double)e.CalculatedValue.GetValue()))
                    {
                        //case when "Sum of TotalPrice" is 0 and "Sum of Quantity" is 0
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                    else if (double.IsInfinity((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                }
            }

            if (e.DataField.ToString() == "Crecimiento")
            {
                //The data calculated by CalculationExpression="{0}/{1}" added in the markup is additionally modified here
                if (e.CalculatedValue != null)
                {
                    if (double.IsNaN((double)e.CalculatedValue.GetValue()))
                    {
                        //case when "Sum of TotalPrice" is 0 and "Sum of Quantity" is 0
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                    else if (double.IsInfinity((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                }
            }

            if (e.DataField.ToString() == "CumplimientoKL")
            {
                //The data calculated by CalculationExpression="{0}/{1}" added in the markup is additionally modified here
                if (e.CalculatedValue != null)
                {
                    if (double.IsNaN((double)e.CalculatedValue.GetValue()))
                    {
                        //case when "Sum of TotalPrice" is 0 and "Sum of Quantity" is 0
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                    else if (double.IsInfinity((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                }
            }
            if (e.DataField.ToString() == "CrecimientoKL")
            {
                //The data calculated by CalculationExpression="{0}/{1}" added in the markup is additionally modified here
                if (e.CalculatedValue != null)
                {
                    if (double.IsNaN((double)e.CalculatedValue.GetValue()))
                    {
                        //case when "Sum of TotalPrice" is 0 and "Sum of Quantity" is 0
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                    else if (double.IsInfinity((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                }
            }
            if (e.DataField.ToString() == "PrecioUnitario")
            {
                //The data calculated by CalculationExpression="{0}/{1}" added in the markup is additionally modified here
                if (e.CalculatedValue != null)
                {
                    if (double.IsNaN((double)e.CalculatedValue.GetValue()))
                    {
                        //case when "Sum of TotalPrice" is 0 and "Sum of Quantity" is 0
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                    else if (double.IsInfinity((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                }
            }
            if (e.DataField.ToString() == "PrecioUnitario2016")
            {
                //The data calculated by CalculationExpression="{0}/{1}" added in the markup is additionally modified here
                if (e.CalculatedValue != null)
                {
                    if (double.IsNaN((double)e.CalculatedValue.GetValue()))
                    {
                        //case when "Sum of TotalPrice" is 0 and "Sum of Quantity" is 0
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                    else if (double.IsInfinity((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                }
            }
            //////
            if (e.DataField.ToString() == "PrecioUnitarioPPTO")
            {
                //The data calculated by CalculationExpression="{0}/{1}" added in the markup is additionally modified here
                if (e.CalculatedValue != null)
                {
                    if (double.IsNaN((double)e.CalculatedValue.GetValue()))
                    {
                        //case when "Sum of TotalPrice" is 0 and "Sum of Quantity" is 0
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                    else if (double.IsInfinity((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                }
            }
            if (e.DataField.ToString() == "VP_Real")
            {
                //The data calculated by CalculationExpression="{0}/{1}" added in the markup is additionally modified here
                if (e.CalculatedValue != null)
                {
                    if (double.IsNaN((double)e.CalculatedValue.GetValue()))
                    {
                        //case when "Sum of TotalPrice" is 0 and "Sum of Quantity" is 0
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                    else if (double.IsInfinity((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                }
            }
            if (e.DataField.ToString() == "VP_PPTO")
            {
                //The data calculated by CalculationExpression="{0}/{1}" added in the markup is additionally modified here
                if (e.CalculatedValue != null)
                {
                    if (double.IsNaN((double)e.CalculatedValue.GetValue()))
                    {
                        //case when "Sum of TotalPrice" is 0 and "Sum of Quantity" is 0
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                    else if (double.IsInfinity((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                }
            }
            if (e.DataField.ToString() == "EfectoPr_2017x2016")
            {
                //The data calculated by CalculationExpression="{0}/{1}" added in the markup is additionally modified here
                if (e.CalculatedValue != null)
                {
                    if (double.IsNaN((double)e.CalculatedValue.GetValue()))
                    {
                        //case when "Sum of TotalPrice" is 0 and "Sum of Quantity" is 0
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                    else if (double.IsInfinity((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                }
            }

            ////////

            if (e.DataField.ToString() == "EfectoPr_2017xPPTO")
            {
                //The data calculated by CalculationExpression="{0}/{1}" added in the markup is additionally modified here
                if (e.CalculatedValue != null)
                {
                    if (double.IsNaN((double)e.CalculatedValue.GetValue()))
                    {
                        //case when "Sum of TotalPrice" is 0 and "Sum of Quantity" is 0
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                    else if (double.IsInfinity((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                }
            }

            if (e.DataField.ToString() == "EfectoVol_Real")
            {
                //The data calculated by CalculationExpression="{0}/{1}" added in the markup is additionally modified here
                if (e.CalculatedValue != null)
                {
                    if (double.IsNaN((double)e.CalculatedValue.GetValue()))
                    {
                        //case when "Sum of TotalPrice" is 0 and "Sum of Quantity" is 0
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                    else if (double.IsInfinity((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                }
            }

            if (e.DataField.ToString() == "EfectoVol_PPTO")
            {
                //The data calculated by CalculationExpression="{0}/{1}" added in the markup is additionally modified here
                if (e.CalculatedValue != null)
                {
                    if (double.IsNaN((double)e.CalculatedValue.GetValue()))
                    {
                        //case when "Sum of TotalPrice" is 0 and "Sum of Quantity" is 0
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                    else if (double.IsInfinity((double)e.CalculatedValue.GetValue()))
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                }
            }


        }




    }
}