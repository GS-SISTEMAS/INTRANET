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

namespace GS.SISGEGS.Web.Comercial.Reportes.ReporteBI
{
    public partial class frmReporteVenta_Resultados_bk : System.Web.UI.Page
    {
        private void Vendedor_Listar() {
            ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient();
            List<ReporteVentas_EstadoResultadosResult> Lista = new List<ReporteVentas_EstadoResultadosResult>();
            string nombre_zona = "";

            try {
                nombre_zona = cboZona.SelectedItem.Text; 

                DateTime fechaInicio = new DateTime(dpFecInicio.SelectedDate.Value.Year, dpFecInicio.SelectedDate.Value.Month, 1);
                DateTime fechaFinal = dpFecFinal.SelectedDate.Value.AddMonths(1);
                fechaFinal = (new DateTime(fechaFinal.Year, fechaFinal.Month, 1)).AddDays(-1);
                Lista = objReporteVentaWCF.ReporteVentas_Resultados(
                    ((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, fechaInicio, fechaFinal, nombre_zona, null).ToList();



                ViewState["ListaReporte"] = JsonHelper.JsonSerializer(Lista);

                gsReporteVentas_Familia.DataSource = Lista;
                gsReporteVentas_Familia.DataBind();

                lblReporte.Value = "1"; 

            }
            catch (Exception ex) {
                throw ex;
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
                    gsReporteVentas_Familia.DataSource = JsonHelper.JsonDeserialize<List<Reporte_R1Result>>((string)ViewState["ListaReporte"]);
                }

            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al consultar NeedDataSource", "");
            }
        }

        protected void gsReporteVentas_Familia_CellDataBound(object sender, PivotGridCellDataBoundEventArgs e)
        {

            try
            {

                if (e.Cell is PivotGridDataCell)
                {
                    PivotGridDataCell cell = e.Cell as PivotGridDataCell;

                    //color all data cells
                    if (cell.CellType == PivotGridDataCellType.DataCell)
                    {
                        int importe = 0;
                        cell.Font.Bold = false;
                        cell.BackColor = Color.Lavender;
                        cell.HorizontalAlign = HorizontalAlign.Right;

                        if (cell.RowIndex == 2 || cell.RowIndex == 7 || cell.RowIndex == 9 || cell.RowIndex == 10 || cell.RowIndex == 13)
                        {
                            cell.BackColor = Color.Silver;
                        }

                        if (cell.ParentColumnIndexes[4].ToString() == "Sum of ImporteMes")
                        {
                            //cell.BackColor = Color.Khaki;
                            //cell.Font.Italic = true;
                            //cell.HorizontalAlign = HorizontalAlign.Right;
                            
                            //if(cell.RowIndex==2)
                            //{
                            //    cell.BackColor = Color.Silver;
                            //}
                          
                            //if (importe <= 30)
                            //{
                            //    cell.BackColor = Color.LightGreen;
                            //    cell.ForeColor = Color.Black;
                            //}
                            //else if (importe > 30)
                            //{
                            //    cell.BackColor = Color.Red;
                            //    cell.ForeColor = Color.White;
                            //}

                        }


                        //if (cell.ParentColumnIndexes[1].ToString() == "Sum of PorcentajeMes")
                        //{
                        //    cell.BackColor = Color.LightGreen;
                        //    //cell.Font.Italic = true;
                        //    cell.HorizontalAlign = HorizontalAlign.Center;
                        //    //importe = Convert.ToInt32(cell.DataItem);
                        //    //if (importe <= 0)
                        //    //{
                        //    //    cell.BackColor = Color.Tomato;
                        //    //    cell.ForeColor = Color.White;
                        //    //}
                        //    //else
                        //    //{
                        //    //    cell.BackColor = Color.White;
                        //    //    cell.ForeColor = Color.Black;
                        //    //}
                        //}
                    }
                    //color the totals cells
                    else if (cell.CellType == PivotGridDataCellType.ColumnTotalDataCell || cell.CellType == PivotGridDataCellType.RowTotalDataCell)
                    {
                        switch ((cell.Field as PivotGridAggregateField).DataField)
                        {
                            //color the cells showing totals for TotalPrice based on their value
                            case "KgLt":

                                //cell.Field.CellStyle.CssClass = "aggregateCustom1";
                                cell.BackColor = Color.Silver;
                                cell.Font.Bold = true;
                                cell.HorizontalAlign = HorizontalAlign.Right;
                                //if (cell.DataItem != null && cell.DataItem.ToString().Length > 0)
                                //{
                                //    int price = Convert.ToInt32(cell.DataItem);
                                //    if (price > 200)
                                //    {
                                //        cell.BackColor = Color.SteelBlue;
                                //    }
                                //    else if (price > 500)
                                //    {
                                //        cell.BackColor = Color.Tomato;
                                //    }
                                //    else
                                //    {
                                //        cell.BackColor = Color.Violet;
                                //    }
                                //}
                                break;

                            //color the cells showing totals for Quantity based on their value
                            case "KgLtPro":
                                //if (cell.DataItem != null && cell.DataItem.ToString().Length > 0)
                                //{
                                cell.BackColor = Color.Silver;
                                cell.Font.Bold = true;
                                cell.HorizontalAlign = HorizontalAlign.Right;
                                //double quantity = Convert.ToDouble(cell.DataItem);
                                //if (quantity > 200)
                                //{
                                //    cell.BackColor = Color.GreenYellow;
                                //}
                                //else if (quantity > 100)
                                //{
                                //    cell.BackColor = Color.Khaki;
                                //}
                                //else
                                //{
                                //    cell.BackColor = Color.IndianRed;
                                //}
                                //}
                                break;

                            default:
                                cell.BackColor = Color.Silver;
                                cell.Font.Bold = true;
                                cell.HorizontalAlign = HorizontalAlign.Right;
                                break;


                        }

                    }

                    //color the column/row grand total cells 
                    else if (cell.CellType == PivotGridDataCellType.RowGrandTotalDataCell || cell.CellType == PivotGridDataCellType.ColumnGrandTotalDataCell)
                    {
                        switch ((cell.Field as PivotGridAggregateField).DataField)
                        {
                            case "KgLt":

                                cell.BackColor = Color.Silver;
                                cell.Font.Bold = true;
                                cell.HorizontalAlign = HorizontalAlign.Right;

                                int price = Convert.ToInt32(cell.DataItem);

                                //if (price > 100)
                                //{
                                //    cell.BackColor = Color.Orange;
                                //}
                                //else
                                //{
                                //    cell.BackColor = Color.Pink;
                                //}

                                break;
                            case "KgLtPro":
                                double quantity = Convert.ToDouble(cell.DataItem);
                                cell.BackColor = Color.Silver;
                                cell.Font.Bold = true;
                                cell.HorizontalAlign = HorizontalAlign.Right;
                                //cell.Field.CellStyle.BackColor = Color.Red;
                                //if (quantity > 20)
                                //{
                                //    cell.BackColor = Color.Red;
                                //}
                                //else
                                //{
                                //    cell.BackColor = Color.Yellow;
                                //}
                                break;
                            default:
                                cell.BackColor = Color.Silver;
                                cell.Font.Bold = true;
                                cell.HorizontalAlign = HorizontalAlign.Right;
                                break;

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
        protected void gsReporteVentas_Familia_PreRender(object sender, EventArgs e)
        {
            //provide custom CSS class for the TotalPrice aggregate field and column headers
            PivotGridAggregateField ImporteMes = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "ImporteMes").FirstOrDefault() as PivotGridAggregateField;
            ImporteMes.CellStyle.Font.Bold = true;
            ImporteMes.CellStyle.BackColor = Color.Khaki;
       
 
            //provide custom CSS class for the Quantity aggregate field and column headers
            PivotGridAggregateField field2 = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "PorcentajeMes").FirstOrDefault() as PivotGridAggregateField;
             field2.CellStyle.Font.Bold = true;
            field2.CellStyle.BackColor = Color.LightGreen;
        

            //PivotGridAggregateField SaldoDolares = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "Margen").FirstOrDefault() as PivotGridAggregateField;
            //SaldoDolares.CellStyle.Font.Bold = true;
            //SaldoDolares.CellStyle.BackColor = Color.Khaki;

            //PivotGridAggregateField PRONOSTICADO_Dolares = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "PorcentajeMargen").FirstOrDefault() as PivotGridAggregateField;
            //PRONOSTICADO_Dolares.CellStyle.Font.Bold = true;
            //PRONOSTICADO_Dolares.CellStyle.BackColor = Color.Khaki;

            //PivotGridAggregateField CostoDolares = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "GastoVentaDolares").FirstOrDefault() as PivotGridAggregateField;
            //CostoDolares.CellStyle.Font.Bold = true;
            //CostoDolares.CellStyle.BackColor = Color.Khaki;


            //PivotGridAggregateField UtilidadDolares = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "PorcentajeGV").FirstOrDefault() as PivotGridAggregateField;
            //UtilidadDolares.CellStyle.Font.Bold = true;
            //UtilidadDolares.CellStyle.BackColor = Color.Khaki;



            //PivotGridAggregateField PrecioUnitario = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "UtilidadGV").FirstOrDefault() as PivotGridAggregateField;
            //PrecioUnitario.CellStyle.Font.Bold = true;
            //PrecioUnitario.CellStyle.BackColor = Color.Khaki;

            //PivotGridAggregateField PrecioUnitarioPres = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "PorcentajeUtilidadGV").FirstOrDefault() as PivotGridAggregateField;
            //PrecioUnitarioPres.CellStyle.Font.Bold = true;
            //PrecioUnitarioPres.CellStyle.BackColor = Color.Khaki;

            //PivotGridAggregateField PrecioUnitarioAnterior = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "GastoAdminDolares").FirstOrDefault() as PivotGridAggregateField;
            //PrecioUnitarioAnterior.CellStyle.Font.Bold = true;
            //PrecioUnitarioAnterior.CellStyle.BackColor = Color.Khaki;


            //PivotGridAggregateField CostoUnitario = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "PorcentajeGA").FirstOrDefault() as PivotGridAggregateField;
            //CostoUnitario.CellStyle.Font.Bold = true;
            //CostoUnitario.CellStyle.BackColor = Color.Khaki;

            //PivotGridAggregateField CostoUnitarioAnterior = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "UtilidadGA").FirstOrDefault() as PivotGridAggregateField;
            //CostoUnitarioAnterior.CellStyle.Font.Bold = true;
            //CostoUnitarioAnterior.CellStyle.BackColor = Color.Khaki;


            //PivotGridAggregateField Margen = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "PorcentajeUtilidadGA").FirstOrDefault() as PivotGridAggregateField;
            //Margen.CellStyle.Font.Bold = true;
            //Margen.CellStyle.BackColor = Color.Khaki;

            //PivotGridAggregateField Margen2016 = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "GastoFinanDolares").FirstOrDefault() as PivotGridAggregateField;
            //Margen2016.CellStyle.Font.Bold = true;
            //Margen2016.CellStyle.BackColor = Color.Khaki;


            //PivotGridAggregateField VPU2017x2016 = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "PorcentajeGF").FirstOrDefault() as PivotGridAggregateField;
            //VPU2017x2016.CellStyle.Font.Bold = true;
            //VPU2017x2016.CellStyle.BackColor = Color.Khaki;

            //PivotGridAggregateField VPU2017xPPTO = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "UtilidadGF").FirstOrDefault() as PivotGridAggregateField;
            //VPU2017xPPTO.CellStyle.Font.Bold = true;
            //VPU2017xPPTO.CellStyle.BackColor = Color.Khaki;




            //PivotGridAggregateField Participacion = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "Participacion").FirstOrDefault() as PivotGridAggregateField;
            //Participacion.CellStyle.Font.Bold = true;
            //Participacion.CellStyle.BackColor = Color.Khaki;


            //PivotGridAggregateField IMPUESTO = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "IMPUESTO").FirstOrDefault() as PivotGridAggregateField;
            //IMPUESTO.CellStyle.Font.Bold = true;
            //IMPUESTO.CellStyle.BackColor = Color.Khaki;


            //PivotGridAggregateField UtilidadNeta = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "UtilidadNeta").FirstOrDefault() as PivotGridAggregateField;
            //UtilidadNeta.CellStyle.Font.Bold = true;
            //UtilidadNeta.CellStyle.BackColor = Color.Khaki;

            //PivotGridAggregateField PorcentajeUtilidadNeta = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "PorcentajeUtilidadNeta").FirstOrDefault() as PivotGridAggregateField;
            //PorcentajeUtilidadNeta.CellStyle.Font.Bold = true;
            //PorcentajeUtilidadNeta.CellStyle.BackColor = Color.Khaki;

            //PivotGridAggregateField GastoVentaD = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "GastoVentaD").FirstOrDefault() as PivotGridAggregateField;
            //GastoVentaD.CellStyle.Font.Bold = true;
            //GastoVentaD.CellStyle.BackColor = Color.Khaki;

            //PivotGridAggregateField GastoVentaI = gsReporteVentas_Familia.Fields.Where(x => x.DataField == "GastoVentaI").FirstOrDefault() as PivotGridAggregateField;
            //GastoVentaI.CellStyle.Font.Bold = true;
            //GastoVentaI.CellStyle.BackColor = Color.Khaki;



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
                    Zona_Cargar();
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


        private void Zona_Cargar()
        {
            try
            {
                ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient();

                List<Listar_Zona_BIResult> lista = new List<Listar_Zona_BIResult>();

                DateTime fechaInicio = new DateTime(dpFecInicio.SelectedDate.Value.Year, dpFecInicio.SelectedDate.Value.Month, 1);
                DateTime fechaFinal = dpFecFinal.SelectedDate.Value.AddMonths(1);
                fechaFinal = (new DateTime(fechaFinal.Year, fechaFinal.Month, 1)).AddDays(-1);

                lista = objReporteVentaWCF.Listar_Zona_BI(
                           ((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, fechaInicio, fechaFinal, "Lima").ToList();

              

                var lstSect = from x in lista
                              select new
                              {
                                  
                                  DisplayField = x.Nombre_Zona
                                  //DisplayField = String.Format("{0} {1} {2} {3}", x.Placa, x.Modelo, x.Marca, x.Agenda)
                              };

                cboZona.DataSource = lstSect;
                cboZona.DataTextField = "DisplayField";
                cboZona.DataValueField = "DisplayField";
                cboZona.DataBind();

                cboZona.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}