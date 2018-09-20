using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.DM;
using GS.SISGEGS.Web.ReporteVentaWCF;
using Telerik.Web.UI;
using System.Globalization;
using GS.SISGEGS.Web.LoginWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.CreditoWCF;
using System.Drawing;

namespace GS.SISGEGS.Web.Finanzas.Reportes
{
    public partial class frmControlLetrasSinCanje : System.Web.UI.Page
    {
        CreditoWCFClient objCredito = new CreditoWCFClient();
        AgendaWCF.AgendaWCFClient objAgenda = new AgendaWCF.AgendaWCFClient();

        #region procedimientos

        private void CargarZonas()
        {
            objAgenda = new AgendaWCF.AgendaWCFClient();
            List<gsZona_ListarResult> lst = new List<gsZona_ListarResult>();
            lst = objAgenda.Agenda_ListarZona(
                ((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, 0).ToList();


            cbzona.DataSource = lst;
            cbzona.DataTextField = "Zona";
            cbzona.DataValueField = "ID_Zona";
            cbzona.DataBind();

            foreach (RadComboBoxItem itm in cbzona.Items)
                itm.Checked = true;
        }

        private void CrearPie(List<USP_SEL_LetrasSinCanjeV2Result> lstletras)
        {
            decimal Total = (decimal)lstletras.Sum(x => x.Saldo);
            var lstplazos = from p in lstletras
                            group p by new { p.TipoPlazo } into g
                            select new
                            {
                                TipoPlazo = g.Key.TipoPlazo,
                                Cantidad = g.Sum(x => x.Saldo)

                            };

            RadHtmlChart scatterChart = new RadHtmlChart();
            scatterChart.ID = "ScatterChart";
            scatterChart.Width = Unit.Pixel(680);
            scatterChart.Height = Unit.Pixel(500);

            scatterChart.Legend.Appearance.Position = Telerik.Web.UI.HtmlChart.ChartLegendPosition.Bottom;

            scatterChart.PlotArea.XAxis.TitleAppearance.Text = "Plazos";
            scatterChart.PlotArea.YAxis.TitleAppearance.Text = "Plazos";

            //ScatterLineSeries theoreticalData = new ScatterLineSeries();
            //theoreticalData.Name = "Theoretical Data";
            //theoreticalData.LabelsAppearance.Visible = false;
            //theoreticalData.TooltipsAppearance.Color = System.Drawing.Color.White;
            //theoreticalData.TooltipsAppearance.DataFormatString = "{0} Volts, {1} mA";

            PieSeries serieY = new PieSeries();
            serieY.Name = "Plazo";
            serieY.LabelsAppearance.Visible = true;
            serieY.LabelsAppearance.DataFormatString = "{0}%";
            serieY.TooltipsAppearance.Color = System.Drawing.Color.White;
            serieY.TooltipsAppearance.DataFormatString = "{0}%";

            //serieY.TooltipsAppearance.ClientTemplate =  "#=category#";


            PieSeriesItem psitem = new PieSeriesItem();

            foreach (var Eplazo in lstplazos)
            {
                psitem = new PieSeriesItem(Math.Round(((((decimal)Eplazo.Cantidad) / Total) * 100), 2));
                //psitem.Name = Eplazo.TipoPlazo;
                psitem.Name = string.Format("{0}: Saldo Total: ${1}, Cantidad Letras: " + lstletras.Where(x => x.TipoPlazo == Eplazo.TipoPlazo).Count(),
                    Eplazo.TipoPlazo, Math.Round(((decimal)Eplazo.Cantidad), 2).ToString("#,##0.00"));

                if (Eplazo.TipoPlazo == "1 a " + txtCantidadDias.Text.Trim() + " Días")
                {
                    psitem.BackgroundColor = System.Drawing.Color.DarkOrange;
                }
                else if (Eplazo.TipoPlazo == "Mayor a " + txtCantidadDias.Text.Trim() + " Días")
                {
                    psitem.BackgroundColor = System.Drawing.Color.Red;
                }

                serieY.SeriesItems.Add(psitem);


            }

            scatterChart.PlotArea.Series.Add(serieY);

            paneltorta.Controls.Add(scatterChart);
        }
        private void CargarData()
        {
            List<USP_SEL_LetrasSinCanjeV2Result> lstletras = new List<USP_SEL_LetrasSinCanjeV2Result>();
            DateTime? dtInicio = Convert.ToDateTime("01/01/2000"); //dtpfechainicial.SelectedDate;
            DateTime? dtFinal = dtpfechafinal.SelectedDate;
            int dias = txtCantidadDias.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtCantidadDias.Text.Trim());

            string strzonas = string.Empty;
            var collection = cbzona.CheckedItems;
            foreach (var x in collection)
                strzonas = strzonas + x.Value.ToString() + ",";

            strzonas = strzonas.Trim() == string.Empty ? string.Empty : strzonas.Substring(0, strzonas.Length - 1);

            objCredito = new CreditoWCFClient();
            lstletras = objCredito.ObtenerLetrasSinCanje(
                ((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                376,
                Convert.ToDateTime(dtInicio),
                Convert.ToDateTime(dtFinal),
                dias, strzonas).ToList();

            
            if (lstletras.Any())
            {
                Session["lstletras"] = JsonHelper.JsonSerializer(lstletras);

                //rchardTorta.PlotArea.Series[0].Items.Clear();

                decimal Total = (decimal)lstletras.Sum(x => x.Saldo);

                lblTitulotarta.Text = "Saldos por Categoria : Monto Total En Dolares $" + Total.ToString("#,##.00");

                var lstplazos = from p in lstletras
                                group p by new { p.TipoPlazo } into g
                                select new
                                {
                                    TipoPlazo = g.Key.TipoPlazo,
                                    Cantidad = g.Sum(x => x.Saldo)

                                };

                SeriesItem item = new SeriesItem();


                CrearPie(lstletras);

                //rchardTorta.DataSource = lstplazos;
                //rchardTorta.DataBind();




                ////fin ------------------//















                //rchardTorta.PlotArea.Series[0].Items.Clear();

                //foreach (var Eplazo in lstplazos)
                //{
                //    item = new SeriesItem();
                //    item.YValue = Math.Round(((((decimal)Eplazo.Cantidad) / Total) * 100), 2); //((int)((cliente.ValorVenta / sum) * 100));

                //    item.Name = string.Format("{0}<br/> Saldo Total: ${1}<br/> Cantidad Letras: " + lstletras.Where(x => x.TipoPlazo == Eplazo.TipoPlazo).Count(),
                //        Eplazo.TipoPlazo, Math.Round(((decimal)Eplazo.Cantidad), 2).ToString("#,##0.00"));

                //    if (Eplazo.TipoPlazo == "1 a " + txtCantidadDias.Text.Trim() + " Días")
                //    {
                //        item.BackgroundColor = System.Drawing.Color.DarkOrange;
                //    }
                //    else if (Eplazo.TipoPlazo == "Mayor a " + txtCantidadDias.Text.Trim() + " Días")
                //    {
                //        item.BackgroundColor = System.Drawing.Color.Red;
                //    }


                //    rchardTorta.PlotArea.Series[0].Items.Add(item);
                //}


                //int plazos = lstplazos.Select(x => x.TipoPlazo).Count();




                //foreach (var Eplazo in lstplazos)
                //{
                //    item = new SeriesItem();

                //    rchardTorta.PlotArea.Series[0].Items[0].Name = string.Format("{0}<br/> Saldo Total: ${1}<br/> Cantidad Letras: " + lstletras.Where(x => x.TipoPlazo == Eplazo.TipoPlazo).Count(),
                //            Eplazo.TipoPlazo, Math.Round(((decimal)Eplazo.Cantidad), 2).ToString("#,##0.00"));

                //    plazos = plazos + 1;
                //}


                rchardLineas.PlotArea.Series[0].Items.Clear();
                var lstzonas = from p in lstletras
                               group p by new { p.NombreZona } into g
                               select new
                               {
                                   NombreZona = g.Key.NombreZona,
                                   Cantidad = g.Sum(x => x.Saldo)
                               };


                rchardLineas.PlotArea.XAxis.TitleAppearance.Text = "Zonas";
                rchardLineas.PlotArea.YAxis.TitleAppearance.Text = "Saldo $";

                rchardLineas.PlotArea.XAxis.LabelsAppearance.TextStyle.FontSize = 8;


                rchardLineas.PlotArea.YAxis.MinValue = 0;
                rchardLineas.PlotArea.XAxis.Items.Clear();
                LineSeries lserie = new LineSeries();
                foreach (var vendedor in lstzonas)
                {

                    rchardLineas.PlotArea.XAxis.Items.Add(vendedor.NombreZona);


                    item = new SeriesItem();
                    item.Name = string.Format("{0}<br/>Saldo Total:$ {1}<br/>",
                        vendedor.NombreZona, Math.Round(((decimal)vendedor.Cantidad), 2).ToString("#,##0.00"));
                    item.YValue = Math.Round((decimal)vendedor.Cantidad, 2);

                    this.rchardLineas.PlotArea.Series[0].Items.Add(item);


                }

                rpgDetalle.DataSource = lstletras;
                rpgDetalle.DataBind();

                rpgResumenZona.DataSource = lstletras;
                rpgResumenZona.DataBind();
                Session["lstletrasresumen"] = JsonHelper.JsonSerializer(lstletras);
            }


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

                    
                    Session["lstletras"] = null;
                   
                    
                    dtpfechafinal.SelectedDate = DateTime.Now;
                    txtCantidadDias.Text = "20";
                    CargarZonas();
                    CargarData();
                    lblMensaje.Text = "El reporte cargó correctamente";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }


        protected void rpgDetalle_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                List<USP_SEL_LetrasSinCanjeV2Result> lstletras = new List<USP_SEL_LetrasSinCanjeV2Result>();
                lstletras = JsonHelper.JsonDeserialize<List<USP_SEL_LetrasSinCanjeV2Result>>((string)Session["lstletras"]);
                if (lstletras.Any())
                {
                    rpgDetalle.DataSource = lstletras;
                    CrearPie(lstletras);
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void rpgDetalle_CellDataBound(object sender, PivotGridCellDataBoundEventArgs e)
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

        protected void btnExcel_Click(object sender, EventArgs e)
        {

            string alternateText = "Xlsx";
            rpgDetalle.ExportSettings.Excel.Format = (PivotGridExcelFormat)Enum.Parse(typeof(PivotGridExcelFormat), alternateText);
            rpgDetalle.ExportSettings.IgnorePaging = true;
            rpgDetalle.ExportToExcel();
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

        protected void rpgDetalle_PivotGridCellExporting(object sender, PivotGridCellExportingArgs e)
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






        #endregion

        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                CargarData();

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnExcelZona_Click(object sender, EventArgs e)
        {
            string alternateText = "Xlsx";
            
            rpgResumenZona.ExportSettings.Excel.Format = (PivotGridExcelFormat)Enum.Parse(typeof(PivotGridExcelFormat), alternateText);
            rpgResumenZona.ExportSettings.IgnorePaging = true;
            rpgResumenZona.ExportToExcel();
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

        protected void rpgResumenZona_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                List<USP_SEL_LetrasSinCanjeV2Result> lstletras = new List<USP_SEL_LetrasSinCanjeV2Result>();
                lstletras = JsonHelper.JsonDeserialize<List<USP_SEL_LetrasSinCanjeV2Result>>((string)Session["lstletrasresumen"]);
                if (lstletras.Any())
                {
                    rpgResumenZona.DataSource = lstletras;
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void rchardTorta_DataBound(object sender, EventArgs e)
        {
            
   
        }
    }
}