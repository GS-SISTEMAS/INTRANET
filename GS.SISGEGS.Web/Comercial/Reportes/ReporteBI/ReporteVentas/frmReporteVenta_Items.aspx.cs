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
    public partial class frmReporteVenta_Items : System.Web.UI.Page
    {
        private void Vendedor_Listar() {
            ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient();
            List<ReporteVentas_ItemsResult> Lista = new List<ReporteVentas_ItemsResult>();

            try {
                DateTime fechaInicio = new DateTime(dpFecInicio.SelectedDate.Value.Year, dpFecInicio.SelectedDate.Value.Month, 1);
                DateTime fechaFinal = dpFecFinal.SelectedDate.Value.AddMonths(1);
                fechaFinal = (new DateTime(fechaFinal.Year, fechaFinal.Month, 1)).AddDays(-1);
                Lista = objReporteVentaWCF.ReporteVentas_Items(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, fechaInicio, fechaFinal, 0 ,null).ToList();

                ViewState["ListaReporteItem"] = JsonHelper.JsonSerializer(Lista);

                if(rbContraer.Checked==true)
                {
                    gsReporteVentas_Item.RowGroupsDefaultExpanded = false;
                }
                else
                {
                    gsReporteVentas_Item.RowGroupsDefaultExpanded = true;
                }

                gsReporteVentas_Item.DataSource = Lista;
                gsReporteVentas_Item.DataBind();

                lblReporte.Value = "1"; 

            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void gsReporteVentas_Item_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (lblReporte.Value == "1")
                {
                    gsReporteVentas_Item.DataSource = JsonHelper.JsonDeserialize<List<ReporteVentas_ItemsResult>>((string)ViewState["ListaReporteItem"]);
                }

            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al consultar NeedDataSource", "");
            }
        }

        protected void gsReporteVentas_Item_CellDataBound(object sender, PivotGridCellDataBoundEventArgs e)
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

                        importe = 0;
                        cell.Font.Bold = false;
                        cell.BackColor = Color.White;
                        cell.HorizontalAlign = HorizontalAlign.Right;

                        if (cell.ParentColumnIndexes[2].ToString() == "Margen_2017_c")
                        {
         
                            cell.HorizontalAlign = HorizontalAlign.Right;

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe <= 30)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                            else if (importe > 30 & importe <= 50)
                            {
                                cell.BackColor = Color.Yellow;
                                cell.ForeColor = Color.Black;
                            }
                            else if (importe > 50)
                            {
                                cell.BackColor = Color.LawnGreen;
                                cell.ForeColor = Color.Black;
                            }
                        }

                        if (cell.ParentColumnIndexes[2].ToString() == "Margen_2016_c")
                        {
                            //cell.BackColor = Color.Yellow;
                            //cell.Font.Italic = true;
                            cell.HorizontalAlign = HorizontalAlign.Right;

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe <= 30)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                            else if (importe > 30 & importe <= 50)
                            {
                                cell.BackColor = Color.Yellow;
                                cell.ForeColor = Color.Black;
                            }
                            else if (importe > 50)
                            {
                                cell.BackColor = Color.LawnGreen;
                                cell.ForeColor = Color.Black;
                            }
                        }

                        if (cell.ParentColumnIndexes[2].ToString() == "VPU_2016_c")
                        {
                            cell.HorizontalAlign = HorizontalAlign.Right;


                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe < 0.00)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                            else
                            {
                                //cell.BackColor = Color.Lavender;
                                cell.ForeColor = Color.Black;
                            }

                        }

                        if (cell.ParentColumnIndexes[2].ToString() == "VPU_PPTO_c")  
                        {
                            cell.HorizontalAlign = HorizontalAlign.Right;
                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe < 0.00)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                            else
                            {
                                //cell.BackColor = Color.Lavender;
                                cell.ForeColor = Color.Black;
                            }
                        }

                        if (cell.ParentColumnIndexes[2].ToString() == "VMg17_16_c")
                        {
                            cell.HorizontalAlign = HorizontalAlign.Right;
                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe < 0.00)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                            else
                            {
                                //cell.BackColor = Color.Lavender;
                                cell.ForeColor = Color.Black;
                            }
                        }
                    }

                    //Black - color the Sub totals rows cells
                    if (cell.CellType == PivotGridDataCellType.RowTotalDataCell)
                    {
                        cell.BackColor = Color.LightSteelBlue;
                        cell.Font.Bold = true;
                        cell.HorizontalAlign = HorizontalAlign.Right;

                        if (cell.ParentColumnIndexes[2].ToString() == "Margen_2017_c")
                        {

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe <= 30)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                            else if (importe > 30 & importe <= 50)
                            {
                                cell.BackColor = Color.Yellow;
                                cell.ForeColor = Color.Black;
                            }
                            else if (importe > 50)
                            {
                                cell.BackColor = Color.LawnGreen;
                                cell.ForeColor = Color.Black;
                            }
                        }

                        if (cell.ParentColumnIndexes[2].ToString() == "Margen_2016_c")
                        {

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe <= 30)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                            else if (importe > 30 & importe <= 50)
                            {
                                cell.BackColor = Color.Yellow;
                                cell.ForeColor = Color.Black;
                            }
                            else if (importe > 50)
                            {
                                cell.BackColor = Color.LawnGreen;
                                cell.ForeColor = Color.Black;
                            }
                        }

                        if (cell.ParentColumnIndexes[2].ToString() == "VPU_2016_c")
                        {

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe < 0.00)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }

                        }

                        if (cell.ParentColumnIndexes[2].ToString() == "VPU_PPTO_c")
                        {
                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe < 0.00)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }

                        }

                        if (cell.ParentColumnIndexes[2].ToString() == "VMg17_16_c")
                        {
                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe < 0.00)
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

                        if (cell.ParentColumnIndexes[2].ToString() == "Margen_2017_c")
                        {

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe <= 30)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                            else if (importe > 30 & importe <= 50)
                            {
                                cell.BackColor = Color.Yellow;
                                cell.ForeColor = Color.Black;
                            }
                            else if (importe > 50)
                            {
                                cell.BackColor = Color.LawnGreen;
                                cell.ForeColor = Color.Black;
                            }
                        }

                        if (cell.ParentColumnIndexes[2].ToString() == "Margen_2016_c")
                        {

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe <= 30)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                            else if (importe > 30 & importe <= 50)
                            {
                                cell.BackColor = Color.Yellow;
                                cell.ForeColor = Color.Black;
                            }
                            else if (importe > 50)
                            {
                                cell.BackColor = Color.LawnGreen;
                                cell.ForeColor = Color.Black;
                            }
                        }

                        if (cell.ParentColumnIndexes[2].ToString() == "VPU_2016_c")
                        {

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe < 0.00)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }

                        }

                        if (cell.ParentColumnIndexes[2].ToString() == "VPU_PPTO_c")
                        {
                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe < 0.00)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }

                        }

                        if (cell.ParentColumnIndexes[2].ToString() == "VMg17_16_c")
                        {
                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe < 0.00)
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

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 Margen_2017_c")
                        {

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe <= 30)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                            else if (importe > 30 & importe <= 50)
                            {
                                cell.BackColor = Color.Yellow;
                                cell.ForeColor = Color.Black;
                            }
                            else if (importe > 50)
                            {
                                cell.BackColor = Color.LawnGreen;
                                cell.ForeColor = Color.Black;
                            }
                        }

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 Margen_2016_c")
                        {

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe <= 30)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                            else if (importe > 30 & importe <= 50)
                            {
                                cell.BackColor = Color.Yellow;
                                cell.ForeColor = Color.Black;
                            }
                            else if (importe > 50)
                            {
                                cell.BackColor = Color.LawnGreen;
                                cell.ForeColor = Color.Black;
                            }
                        }

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 VPU_2016_c")
                        {

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe < 0.00)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }

                        }

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 VPU_PPTO_c")
                        {
                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe < 0.00)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }

                        }

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 VMg17_16_c")
                        {
                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe < 0.00)
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
                        cell.HorizontalAlign = HorizontalAlign.Right;

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 Margen_2017_c")
                        {

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe <= 30)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                            else if (importe > 30 & importe <= 50)
                            {
                                cell.BackColor = Color.Yellow;
                                cell.ForeColor = Color.Black;
                            }
                            else if (importe > 50)
                            {
                                cell.BackColor = Color.LawnGreen;
                                cell.ForeColor = Color.Black;
                            }
                        }

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 Margen_2016_c")
                        {

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe <= 30)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                            else if (importe > 30 & importe <= 50)
                            {
                                cell.BackColor = Color.Yellow;
                                cell.ForeColor = Color.Black;
                            }
                            else if (importe > 50)
                            {
                                cell.BackColor = Color.LawnGreen;
                                cell.ForeColor = Color.Black;
                            }
                        }

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 VPU_2016_c")
                        {

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe < 0.00)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }

                        }

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 VPU_PPTO_c")
                        {
                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe < 0.00)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }

                        }

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 VMg17_16_c")
                        {
                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe < 0.00)
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
                        cell.HorizontalAlign = HorizontalAlign.Right;

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 Margen_2017_c")
                        {

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe <= 30)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                            else if (importe > 30 & importe <= 50)
                            {
                                cell.BackColor = Color.Yellow;
                                cell.ForeColor = Color.Black;
                            }
                            else if (importe > 50)
                            {
                                cell.BackColor = Color.LawnGreen;
                                cell.ForeColor = Color.Black;
                            }
                        }

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 Margen_2016_c")
                        {

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe <= 30)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }
                            else if (importe > 30 & importe <= 50)
                            {
                                cell.BackColor = Color.Yellow;
                                cell.ForeColor = Color.Black;
                            }
                            else if (importe > 50)
                            {
                                cell.BackColor = Color.LawnGreen;
                                cell.ForeColor = Color.Black;
                            }
                        }

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 VPU_2016_c")
                        {

                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe < 0.00)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }

                        }

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 VPU_PPTO_c")
                        {
                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe < 0.00)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }

                        }

                        if (cell.ParentColumnIndexes[1].ToString() == "2017 VMg17_16_c")
                        {
                            importe = Convert.ToInt32(cell.DataItem);
                            if (importe < 0.00)
                            {
                                cell.BackColor = Color.Tomato;
                                cell.ForeColor = Color.White;
                            }

                        }
                    }


                    //color the column AND row grand total cell 
                    else if (cell.CellType == PivotGridDataCellType.RowAndColumnGrandTotal)
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
                        //cell.HorizontalAlign = HorizontalAlign.;
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

        protected void gsReporteVentas_Item_PreRender(object sender, EventArgs e)
        {
            //provide custom CSS class for the TotalPrice aggregate field and column headers
            PivotGridAggregateField field1 = gsReporteVentas_Item.Fields.Where(x => x.DataField == "Cantidad").FirstOrDefault() as PivotGridAggregateField;
            field1.CellStyle.Font.Bold = true;
            field1.CellStyle.BackColor = Color.Khaki;


            //provide custom CSS class for the Quantity aggregate field and column headers
            PivotGridAggregateField CantidadPPTO = gsReporteVentas_Item.Fields.Where(x => x.DataField == "CantidadPPTO").FirstOrDefault() as PivotGridAggregateField;
            CantidadPPTO.CellStyle.Font.Bold = true;
            CantidadPPTO.CellStyle.BackColor = Color.SandyBrown;

            PivotGridAggregateField SaldoDolares = gsReporteVentas_Item.Fields.Where(x => x.DataField == "SaldoDolares").FirstOrDefault() as PivotGridAggregateField;
            SaldoDolares.CellStyle.Font.Bold = true;
            SaldoDolares.CellStyle.BackColor = Color.Khaki;

            PivotGridAggregateField VentaPPTO_Dolares = gsReporteVentas_Item.Fields.Where(x => x.DataField == "VentaPPTO_Dolares").FirstOrDefault() as PivotGridAggregateField;
            VentaPPTO_Dolares.CellStyle.Font.Bold = true;
            VentaPPTO_Dolares.CellStyle.BackColor = Color.SandyBrown;

            PivotGridAggregateField CostoDolares = gsReporteVentas_Item.Fields.Where(x => x.DataField == "CostoDolares").FirstOrDefault() as PivotGridAggregateField;
            CostoDolares.CellStyle.Font.Bold = true;
            CostoDolares.CellStyle.BackColor = Color.Khaki;


            PivotGridAggregateField UtilidadDolares = gsReporteVentas_Item.Fields.Where(x => x.DataField == "UtilidadDolares").FirstOrDefault() as PivotGridAggregateField;
            UtilidadDolares.CellStyle.Font.Bold = true;
            UtilidadDolares.CellStyle.BackColor = Color.Gold;

       

            PivotGridAggregateField PUnitario_c = gsReporteVentas_Item.Fields.Where(x => x.DataField == "PUnitario_c").FirstOrDefault() as PivotGridAggregateField;
            PUnitario_c.CellStyle.Font.Bold = true;
            PUnitario_c.CellStyle.BackColor = Color.Khaki;

            PivotGridAggregateField PUnitarioPPT_c = gsReporteVentas_Item.Fields.Where(x => x.DataField == "PUnitarioPPT_c").FirstOrDefault() as PivotGridAggregateField;
            PUnitarioPPT_c.CellStyle.Font.Bold = true;
            PUnitarioPPT_c.CellStyle.BackColor = Color.SandyBrown;
 
            PivotGridAggregateField PUnitarioAnterior_c = gsReporteVentas_Item.Fields.Where(x => x.DataField == "PUnitarioAnterior_c").FirstOrDefault() as PivotGridAggregateField;
            PUnitarioAnterior_c.CellStyle.Font.Bold = true;
            PUnitarioAnterior_c.CellStyle.BackColor = Color.SkyBlue;

            //-----------------------------------------------------------

            PivotGridAggregateField CostoUnitario = gsReporteVentas_Item.Fields.Where(x => x.DataField == "CUnitario_c").FirstOrDefault() as PivotGridAggregateField;
            CostoUnitario.CellStyle.Font.Bold = true;
            CostoUnitario.CellStyle.BackColor = Color.Khaki;

            PivotGridAggregateField CostoUnitarioAnterior = gsReporteVentas_Item.Fields.Where(x => x.DataField == "CUnitarioAnterior_c").FirstOrDefault() as PivotGridAggregateField;
            CostoUnitarioAnterior.CellStyle.Font.Bold = true;
            CostoUnitarioAnterior.CellStyle.BackColor = Color.SkyBlue;


            //-------------------------------------------------------------
            PivotGridAggregateField Margen = gsReporteVentas_Item.Fields.Where(x => x.DataField == "Margen_2017_c").FirstOrDefault() as PivotGridAggregateField;
            Margen.CellStyle.Font.Bold = true;
            Margen.CellStyle.BackColor = Color.SandyBrown;

            PivotGridAggregateField Margen2016 = gsReporteVentas_Item.Fields.Where(x => x.DataField == "Margen_2016_c").FirstOrDefault() as PivotGridAggregateField;
            Margen2016.CellStyle.Font.Bold = true;
            Margen2016.CellStyle.BackColor = Color.SkyBlue;

            //-------------------------------------------------------------
            PivotGridAggregateField VPU2017x2016 = gsReporteVentas_Item.Fields.Where(x => x.DataField == "VPU_2016_c").FirstOrDefault() as PivotGridAggregateField;
            VPU2017x2016.CellStyle.Font.Bold = true;
            VPU2017x2016.CellStyle.BackColor = Color.Khaki;

            PivotGridAggregateField VPU2017xPPTO = gsReporteVentas_Item.Fields.Where(x => x.DataField == "VPU_PPTO_c").FirstOrDefault() as PivotGridAggregateField;
            VPU2017xPPTO.CellStyle.Font.Bold = true;
            VPU2017xPPTO.CellStyle.BackColor = Color.Khaki;

            PivotGridAggregateField VMg17_16 = gsReporteVentas_Item.Fields.Where(x => x.DataField == "VMg17_16_c").FirstOrDefault() as PivotGridAggregateField;
            VMg17_16.CellStyle.Font.Bold = true;
            VMg17_16.CellStyle.BackColor = Color.DarkOrange;


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

        protected void gsReporteVentas_Item_ItemNeedCalculation(object sender, PivotGridCalculationEventArgs e)
        {
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

            if (e.DataField.ToString() == "PUnitarioAnterior_c")
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
            if (e.DataField.ToString() == "CUnitarioAnterior_c")
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

          

            if (e.DataField.ToString() == "Margen_2017_c")
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

           

            if (e.DataField.ToString() == "Margen_2016_c")
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

            //----------------------------------------------------
            if (e.DataField.ToString() == "VPU_2016_c")
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

            if (e.DataField.ToString() == "VPU_PPTO_c")
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

            if (e.DataField.ToString() == "VMg17_16_c")
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
            gsReporteVentas_Item.ExportSettings.Excel.Format = (PivotGridExcelFormat)Enum.Parse(typeof(PivotGridExcelFormat), alternateText);
            gsReporteVentas_Item.ExportSettings.IgnorePaging = true;
            gsReporteVentas_Item.ExportToExcel();
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