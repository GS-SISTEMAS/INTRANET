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
    public partial class frmReporteVenta_Rentabilidad : System.Web.UI.Page
    {
        private void Vendedor_Listar() {
            ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient();
            List<ReporteVentas_FamiliaResult> Lista = new List<ReporteVentas_FamiliaResult>();

            try {
                DateTime fechaInicio = new DateTime(dpFecInicio.SelectedDate.Value.Year, dpFecInicio.SelectedDate.Value.Month, 1);
                DateTime fechaFinal = dpFecFinal.SelectedDate.Value.AddMonths(1);
                fechaFinal = (new DateTime(fechaFinal.Year, fechaFinal.Month, 1)).AddDays(-1);
                Lista = objReporteVentaWCF.ReporteVentas_Familia(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, fechaInicio, fechaFinal, 0 ,null).ToList();

                ViewState["ListaReporte"] = JsonHelper.JsonSerializer(Lista);

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
                    gsReporteVentas_Zonas.DataSource = JsonHelper.JsonDeserialize<List<ReporteVentas_FamiliaResult>>((string)ViewState["ListaReporte"]);
                }

            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al consultar NeedDataSource", "");
            }
        }

        protected void gsReporteVentas_Zonas_CellDataBound(object sender, PivotGridCellDataBoundEventArgs e)
        {
            int importe = 0;
            try
            {

                if (e.Cell is PivotGridDataCell)
                {
                    PivotGridDataCell cell = e.Cell as PivotGridDataCell;

                    //color all data cells
                    if (cell.CellType == PivotGridDataCellType.DataCell)
                    {
                        
                        cell.Font.Bold = false;
                        cell.BackColor = Color.White;
                        cell.HorizontalAlign = HorizontalAlign.Right;

                        if (cell.ParentColumnIndexes[2].ToString() == "MargenGASTOD")
                        {
                            cell.HorizontalAlign = HorizontalAlign.Right;

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe > 10)
                            {
                                cell.BackColor = Color.Pink;
                                cell.ForeColor = Color.Red;
                            }
                        }
                        if (cell.ParentColumnIndexes[2].ToString() == "UtilidadOperativaP")
                        {
                            cell.HorizontalAlign = HorizontalAlign.Right;

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe > 35)
                            {
                                cell.BackColor = Color.Plum;
                                cell.ForeColor = Color.Purple;
                            }
                        }
                    }

                    //Black - color the Sub totals rows cells
                    if (cell.CellType == PivotGridDataCellType.RowTotalDataCell)
                    {
                        cell.BackColor = Color.PaleGreen;
                        cell.Font.Bold = true;
                        cell.HorizontalAlign = HorizontalAlign.Right;

                        if (cell.ParentColumnIndexes[2].ToString() == "MargenGASTOD")
                        {
                            cell.HorizontalAlign = HorizontalAlign.Right;

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe > 10)
                            {
                                cell.BackColor = Color.Pink;
                                cell.ForeColor = Color.Red;
                            }
                        }
                        if (cell.ParentColumnIndexes[2].ToString() == "UtilidadOperativaP")
                        {
                            cell.HorizontalAlign = HorizontalAlign.Right;

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe > 35)
                            {
                                cell.BackColor = Color.Plum;
                                cell.ForeColor = Color.Purple;
                            }
                        }
                    }
                    //Yellow - color the column/row / grand total rows   
                    if (cell.CellType == PivotGridDataCellType.RowGrandTotalDataCell)
                    {
                        cell.BackColor = Color.Silver;
                        cell.Font.Bold = true;
                        cell.HorizontalAlign = HorizontalAlign.Right;

                        if (cell.ParentColumnIndexes[2].ToString() == "MargenGASTOD")
                        {
                            cell.HorizontalAlign = HorizontalAlign.Right;

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe > 10)
                            {
                                cell.BackColor = Color.Pink;
                                cell.ForeColor = Color.Red;
                            }
                        }
                        if (cell.ParentColumnIndexes[2].ToString() == "UtilidadOperativaP")
                        {
                            cell.HorizontalAlign = HorizontalAlign.Right;

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe > 35)
                            {
                                cell.BackColor = Color.Plum;
                                cell.ForeColor = Color.Purple;
                            }
                        }

                    }



                    //Red 
                    if (cell.CellType == PivotGridDataCellType.ColumnTotalDataCell)
                    {
                        cell.BackColor = Color.LightGray;
                        cell.Font.Bold = true;
                        cell.HorizontalAlign = HorizontalAlign.Right;

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 MargenGASTOD")
                        {
                            cell.HorizontalAlign = HorizontalAlign.Right;

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe > 10)
                            {
                                cell.BackColor = Color.Pink;
                                cell.ForeColor = Color.Red;
                            }
                        }
                        if (cell.ParentColumnIndexes[1].ToString() == "2017 UtilidadOperativaP")
                        {
                            cell.HorizontalAlign = HorizontalAlign.Right;

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe > 35)
                            {
                                cell.BackColor = Color.Plum;
                                cell.ForeColor = Color.Purple;
                            }
                        }

                    }
                    //Orange - color SubTotales column cells
                    if (cell.CellType == PivotGridDataCellType.RowAndColumnTotal)
                    {
                        cell.BackColor = Color.PaleGreen;
                        cell.Font.Bold = true;
                        cell.HorizontalAlign = HorizontalAlign.Right;

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 MargenGASTOD")
                        {
                            cell.HorizontalAlign = HorizontalAlign.Right;

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe > 10)
                            {
                                cell.BackColor = Color.Pink;
                                cell.ForeColor = Color.Red;
                            }
                        }
                        if (cell.ParentColumnIndexes[1].ToString() == "2017 UtilidadOperativaP")
                        {
                            cell.HorizontalAlign = HorizontalAlign.Right;
  
                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe > 35)
                            {
                                cell.BackColor = Color.Plum;
                                cell.ForeColor = Color.Purple;
                            }
                        }
                    }
                    // Azul - Color GrandTotal Column 
                    if (cell.CellType == PivotGridDataCellType.RowGrandTotalColumnTotal)
                    {
                        cell.BackColor = Color.Silver;
                        cell.Font.Bold = true;
                        cell.HorizontalAlign = HorizontalAlign.Right;
                        if (cell.ParentColumnIndexes[1].ToString() == "2017 MargenGASTOD")
                        {
                            cell.HorizontalAlign = HorizontalAlign.Right;

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe > 10)
                            {
                                cell.BackColor = Color.Pink;
                                cell.ForeColor = Color.Red;
                            }
                        }
                        if (cell.ParentColumnIndexes[1].ToString() == "2017 UtilidadOperativaP")
                        {
                            cell.HorizontalAlign = HorizontalAlign.Right;

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe > 35)
                            {
                                cell.BackColor = Color.Plum;
                                cell.ForeColor = Color.Purple;
                            }
                        }
                    }




                }

                ////style the row headers based on their level
                else if (e.Cell is PivotGridRowHeaderCell)
                {
                    PivotGridRowHeaderCell cell = e.Cell as PivotGridRowHeaderCell;
                    if (cell.ParentIndexes != null && cell.ParentIndexes.Length == 1)
                    {
                        cell.Font.Bold = true;
                        //cell.HorizontalAlign = HorizontalAlign.Center;
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
                        cell.BorderColor = Color.Black; 
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
            //provide custom CSS class for the TotalPrice aggregate field and column headers
            PivotGridAggregateField SaldoDolares = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "SaldoDolares").FirstOrDefault() as PivotGridAggregateField;
            SaldoDolares.CellStyle.Font.Bold = true;
            SaldoDolares.CellStyle.BackColor = Color.Orange;
            //field1.CellStyle.CssClass = "aggregateCustom1";
            //field1.RenderingControl.CssClass = "aggregateCustom1";

            //provide custom CSS class for the Quantity aggregate field and column headers
            PivotGridAggregateField CostoDolares = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "CostoDolares").FirstOrDefault() as PivotGridAggregateField;
            CostoDolares.CellStyle.Font.Bold = true;
            CostoDolares.CellStyle.BackColor = Color.Orange;
            //field2.CellStyle.CssClass = "aggregateCustom2";
            //field2.RenderingControl.CssClass = "aggregateCustom2";

            PivotGridAggregateField Margen = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "Margen").FirstOrDefault() as PivotGridAggregateField;
            Margen.CellStyle.Font.Bold = true;
            Margen.CellStyle.BackColor = Color.Orange;

            PivotGridAggregateField MargenP = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "MargenPorcentaje").FirstOrDefault() as PivotGridAggregateField;
            MargenP.CellStyle.Font.Bold = true;
            MargenP.CellStyle.BackColor = Color.Orange;

            PivotGridAggregateField GastoVentaDirecto = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "GastoVentaDirecto").FirstOrDefault() as PivotGridAggregateField;
            GastoVentaDirecto.CellStyle.Font.Bold = true;
            GastoVentaDirecto.CellStyle.BackColor = Color.Orange;

            PivotGridAggregateField GastoVentaIndirecto = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "GastoVentaIndirecto").FirstOrDefault() as PivotGridAggregateField;
            GastoVentaIndirecto.CellStyle.Font.Bold = true;
            GastoVentaIndirecto.CellStyle.BackColor = Color.Orange;


            PivotGridAggregateField GastoVentaDolares = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "GastoVentaDolares").FirstOrDefault() as PivotGridAggregateField;
            GastoVentaDolares.CellStyle.Font.Bold = true;
            GastoVentaDolares.CellStyle.BackColor = Color.Orange;


            PivotGridAggregateField MargenGD = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "MargenGASTOD").FirstOrDefault() as PivotGridAggregateField;
            MargenGD.CellStyle.Font.Bold = true;
            MargenGD.CellStyle.BackColor = Color.Orange;

            PivotGridAggregateField MargenGI = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "MargenGASTOI").FirstOrDefault() as PivotGridAggregateField;
            MargenGI.CellStyle.Font.Bold = true;
            MargenGI.CellStyle.BackColor = Color.Orange;

            PivotGridAggregateField MargenG = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "MargenGASTO").FirstOrDefault() as PivotGridAggregateField;
            MargenG.CellStyle.Font.Bold = true;
            MargenG.CellStyle.BackColor = Color.Orange;
 
            PivotGridAggregateField UtilidadOperativa = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "UtilidadOperativa").FirstOrDefault() as PivotGridAggregateField;
            UtilidadOperativa.CellStyle.Font.Bold = true;
            UtilidadOperativa.CellStyle.BackColor = Color.Orange;

            PivotGridAggregateField UtilidadOperativaP = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "UtilidadOperativaP").FirstOrDefault() as PivotGridAggregateField;
            UtilidadOperativaP.CellStyle.Font.Bold = true;
            UtilidadOperativaP.CellStyle.BackColor = Color.Orange;

            PivotGridAggregateField GastoAdministrativoD = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "GastoAdministrativoD").FirstOrDefault() as PivotGridAggregateField;
            GastoAdministrativoD.CellStyle.Font.Bold = true;
            GastoAdministrativoD.CellStyle.BackColor = Color.Orange;

            PivotGridAggregateField GastoAdministrativoD_P = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "GastoAdministrativoD_P").FirstOrDefault() as PivotGridAggregateField;
            GastoAdministrativoD_P.CellStyle.Font.Bold = true;
            GastoAdministrativoD_P.CellStyle.BackColor = Color.Orange;
 

            PivotGridAggregateField CostoUnitarioAnterior = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "UtilidadGA").FirstOrDefault() as PivotGridAggregateField;
            CostoUnitarioAnterior.CellStyle.Font.Bold = true;
            CostoUnitarioAnterior.CellStyle.BackColor = Color.Orange;


            PivotGridAggregateField UtilidadGAP = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "UtilidadGAP").FirstOrDefault() as PivotGridAggregateField;
            UtilidadGAP.CellStyle.Font.Bold = true;
            UtilidadGAP.CellStyle.BackColor = Color.Orange;

            PivotGridAggregateField Margen2016 = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "GastoFinanDolares").FirstOrDefault() as PivotGridAggregateField;
            Margen2016.CellStyle.Font.Bold = true;
            Margen2016.CellStyle.BackColor = Color.Orange;


            PivotGridAggregateField GastoFinanDolares_P = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "GastoFinanDolares_P").FirstOrDefault() as PivotGridAggregateField;
            GastoFinanDolares_P.CellStyle.Font.Bold = true;
            GastoFinanDolares_P.CellStyle.BackColor = Color.Orange;

            PivotGridAggregateField UtilidadGF = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "UtilidadGF").FirstOrDefault() as PivotGridAggregateField;
            UtilidadGF.CellStyle.Font.Bold = true;
            UtilidadGF.CellStyle.BackColor = Color.Khaki;
 

            PivotGridAggregateField Participacion = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "Participacion").FirstOrDefault() as PivotGridAggregateField;
            Participacion.CellStyle.Font.Bold = true;
            Participacion.CellStyle.BackColor = Color.Khaki;


            PivotGridAggregateField Impuestos = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "Impuestos").FirstOrDefault() as PivotGridAggregateField;
            Impuestos.CellStyle.Font.Bold = true;
            Impuestos.CellStyle.BackColor = Color.Khaki;


            PivotGridAggregateField UtilidadNeta = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "UtilidadNeta").FirstOrDefault() as PivotGridAggregateField;
            UtilidadNeta.CellStyle.Font.Bold = true;
            UtilidadNeta.CellStyle.BackColor = Color.Khaki;

            PivotGridAggregateField UtilidadNeta_P = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "UtilidadNeta_P").FirstOrDefault() as PivotGridAggregateField;
            UtilidadNeta_P.CellStyle.Font.Bold = true;
            UtilidadNeta_P.CellStyle.BackColor = Color.Khaki;

            PivotGridAggregateField GastoOtrosDolares = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "GastoOtrosDolares").FirstOrDefault() as PivotGridAggregateField;
            GastoOtrosDolares.CellStyle.Font.Bold = true;
            GastoOtrosDolares.CellStyle.BackColor = Color.Khaki;

            PivotGridAggregateField GastoOtrosDolares_P = gsReporteVentas_Zonas.Fields.Where(x => x.DataField == "GastoOtrosDolares_P").FirstOrDefault() as PivotGridAggregateField;
            GastoOtrosDolares_P.CellStyle.Font.Bold = true;
            GastoOtrosDolares_P.CellStyle.BackColor = Color.Khaki;

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

        protected void gsReporteVentas_Zonas_ItemNeedCalculation(object sender, PivotGridCalculationEventArgs e)
        {
 
            if (e.DataField.ToString() == "MargenPorcentaje")
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

            if (e.DataField.ToString() == "MargenGASTOD")
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

            if (e.DataField.ToString() == "MargenGASTOI")
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

            if (e.DataField.ToString() == "MargenGASTO")
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

            if (e.DataField.ToString() == "UtilidadOperativaP")
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

            if (e.DataField.ToString() == "GastoAdministrativoD_P")
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

            if (e.DataField.ToString() == "UtilidadGAP")
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

            if (e.DataField.ToString() == "GastoFinanDolares_P")
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

            if (e.DataField.ToString() == "GastoOtrosDolares_P")
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

            //------------

            if (e.DataField.ToString() == "UtilidadNeta_P")
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

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string alternateText = "Xlsx"; 
            gsReporteVentas_Zonas.ExportSettings.Excel.Format = (PivotGridExcelFormat)Enum.Parse(typeof(PivotGridExcelFormat), alternateText);
            gsReporteVentas_Zonas.ExportSettings.IgnorePaging = true;
            gsReporteVentas_Zonas.ExportToExcel();
        }

        protected void RadPivotGrid1_PivotGridCellExporting(object sender, PivotGridCellExportingArgs e)
        {
            //if (!CheckBox3.Checked)
            //{
            //    return;
            //}

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