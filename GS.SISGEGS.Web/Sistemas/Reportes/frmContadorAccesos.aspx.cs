using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.DM;
using System.Drawing;
using Telerik.Web.UI;
using GS.SISGEGS.Web.Helpers;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace GS.SISGEGS.Web.Sistemas.Reportes
{
    public partial class frmContadorAccesos : System.Web.UI.Page
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

                    dpFechaInicial.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    dpFechaFinal.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1).AddDays(-1);

                    dpFechaInicialS.SelectedDate = dpFechaInicial.SelectedDate;
                    dpFechaFinalS.SelectedDate = dpFechaFinal.SelectedDate;

                    CargarData();
                    CargarDataSinAcceso();
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
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {

                CargarData();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        protected void btnBuscarS_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {

                CargarDataSinAcceso();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        protected void gsContadorAccesos_PreRender(object sender, EventArgs e)
        {
            PivotGridAggregateField field1 = gsContadorAccesos.Fields.Where(x => x.DataField == "idMenu").FirstOrDefault() as PivotGridAggregateField;
            field1.CellStyle.Font.Bold = true;
            field1.CellStyle.BackColor = Color.FromArgb(255, 255, 213);


        }
        protected void gsContadorSinAccesos_PreRender(object sender, EventArgs e)
        {
            PivotGridAggregateField field1 = gsContadorSinAccesos.Fields.Where(x => x.DataField == "idMenu").FirstOrDefault() as PivotGridAggregateField;
            field1.CellStyle.Font.Bold = true;
            field1.CellStyle.BackColor = Color.FromArgb(255, 255, 213);


        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string alternateText = "Xlsx";
            gsContadorAccesos.ExportSettings.Excel.Format = (PivotGridExcelFormat)Enum.Parse(typeof(PivotGridExcelFormat), alternateText);
            gsContadorAccesos.ExportSettings.IgnorePaging = true;
            gsContadorAccesos.ExportToExcel();
        }
        protected void btnExcelS_Click(object sender, EventArgs e)
        {
            string alternateText = "Xlsx";
            this.gsContadorSinAccesos.ExportSettings.Excel.Format = (PivotGridExcelFormat)Enum.Parse(typeof(PivotGridExcelFormat), alternateText);
            gsContadorSinAccesos.ExportSettings.IgnorePaging = true;
            gsContadorSinAccesos.ExportToExcel();


        }


        #region PROCEDIMIENTOS
        protected void gsContadorAccesos_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (lblReporte.Value == "1")
                {
                    gsContadorAccesos.DataSource = JsonHelper.JsonDeserialize<List<usp_SelCantidadAccesosPorMenuResult>>((string)ViewState["ListaMenus"]);
                }


            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al consultar NeedDataSource", "");
            }
        }

        protected void gsContadorSinAccesos_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (lblReporteS.Value == "1")
                {
                    this.gsContadorSinAccesos.DataSource = JsonHelper.JsonDeserialize<List<usp_Sel_MenusNoAccedidosResult>>((string)ViewState["ListaMenusS"]);
                }


            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al consultar NeedDataSource", "");
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
        private void CargarData()
        {
            UsuarioWCF.UsuarioWCFClient objUsuarioWCF = new UsuarioWCF.UsuarioWCFClient();
            List<usp_SelCantidadAccesosPorMenuResult> Lista = new List<usp_SelCantidadAccesosPorMenuResult>();

            try
            {
                DateTime fechainicial = Convert.ToDateTime(dpFechaInicial.SelectedDate.Value);
                DateTime fechafinal = Convert.ToDateTime(dpFechaFinal.SelectedDate.Value);

                //Lista = objReporteVentaWCF.ReporteVentas_R3_1(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, fechaInicio, fechaFinal, 0, null).ToList();
                //Lista= objUsuarioWCF.usuario_
                Lista = objUsuarioWCF.Usuario_ListarMenusPorUsuario(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, fechainicial, fechafinal).ToList();

                ViewState["ListaMenus"] = JsonHelper.JsonSerializer(Lista);

                if (chkexpandido.Checked)
                    gsContadorAccesos.RowGroupsDefaultExpanded = true;
                else
                    gsContadorAccesos.RowGroupsDefaultExpanded = false;
                //if (rbContraer.Checked == true)
                //    gsContadorAccesos.RowGroupsDefaultExpanded = false;
                //else
                //    gsContadorAccesos.RowGroupsDefaultExpanded = true;


                gsContadorAccesos.DataSource = Lista;
                gsContadorAccesos.DataBind();

                lblReporte.Value = "1";

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        private void CargarDataSinAcceso()
        {
            UsuarioWCF.UsuarioWCFClient objUsuarioWCF = new UsuarioWCF.UsuarioWCFClient();
            List<usp_Sel_MenusNoAccedidosResult> ListaSinAcceso = new List<usp_Sel_MenusNoAccedidosResult>();

            try
            {
                DateTime fechainicial = Convert.ToDateTime(dpFechaInicialS.SelectedDate.Value);
                DateTime fechafinal = Convert.ToDateTime(dpFechaFinalS.SelectedDate.Value);

                //Lista = objReporteVentaWCF.ReporteVentas_R3_1(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, fechaInicio, fechaFinal, 0, null).ToList();
                //Lista= objUsuarioWCF.usuario_
                ListaSinAcceso = objUsuarioWCF.Usuario_ListarMenusNoAccedidos(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, fechainicial, fechafinal).ToList();

                ViewState["ListaMenusS"] = JsonHelper.JsonSerializer(ListaSinAcceso);

                if (chkexpandidoS.Checked)
                    gsContadorSinAccesos.RowGroupsDefaultExpanded = true;
                else
                    gsContadorSinAccesos.RowGroupsDefaultExpanded = false;

                //if (rbContraer.Checked == true)
                //    gsContadorAccesos.RowGroupsDefaultExpanded = false;
                //else
                //    gsContadorAccesos.RowGroupsDefaultExpanded = true;


                gsContadorSinAccesos.DataSource = ListaSinAcceso;
                gsContadorSinAccesos.DataBind();

                lblReporteS.Value = "1";
                
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        #endregion
        protected void gsContadorAccesos_CellDataBound(object sender, PivotGridCellDataBoundEventArgs e)
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
            //give a color to the various cells cells

        }
        protected void gsContadorSinAccesos_CellDataBound(object sender, PivotGridCellDataBoundEventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            //give a color to the various cells cells

        }

        //protected void gsContadorAccesos_ItemNeedCalculation(object sender, PivotGridCalculationEventArgs e)
        //{
        //    //if (e.DataField.ToString() == "Margen_cal")
        //    //{
        //    //    if (e.CalculatedValue != null)
        //    //    {
        //    //        if (double.IsNaN((double)e.CalculatedValue.GetValue()))
        //    //        {
        //    //            e.CalculatedValue = new DoubleAggregateValue(0);
        //    //        }
        //    //        else if (double.IsInfinity((double)e.CalculatedValue.GetValue()))
        //    //        {
        //    //            e.CalculatedValue = new DoubleAggregateValue(0);
        //    //        }
        //    //    }
        //    //}
        //}
        protected void gsContadorSinAccesos_ItemNeedCalculation(object sender, PivotGridCalculationEventArgs e)
        {

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


    }
}