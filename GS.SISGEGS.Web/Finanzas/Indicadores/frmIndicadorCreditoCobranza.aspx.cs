using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using GS.SISGEGS.Web.Helpers;
using System.Drawing;
using System.Globalization;
using System.ServiceModel;
using System.Web.UI;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.IndicadoresWCF;


namespace GS.SISGEGS.Web.Finanzas.Indicadores
{
    public partial class frmIndicadorCreditoCobranza : System.Web.UI.Page
    {
        private decimal _vencidos = 0m;
        private decimal _deuda = 0m;
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

                    List<gsReporte_IndicadoresCreditosResult> lst = new List<gsReporte_IndicadoresCreditosResult>();

                    grdIndicadores.DataSource = lst;
                    dpFechaHastaCliente.SelectedDate = DateTime.Now;
                    //dpFechaDesdeCliente.SelectedDate = DateTime.Now;

                }
            }

            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscarResumenCliente_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensajeResumenCliente.Text = "";
                lblDate2.Text = "";
                Reporte_CargarResumen();
            }
            catch (Exception ex)
            {
                lblMensajeResumenCliente.Text = ex.Message;
                lblMensajeResumenCliente.CssClass = "mensajeError";
            }
        }

        private void Reporte_CargarResumen()
        {

            DateTime fecha1;
            DateTime fecha2;
            DateTime fecha3;
            DateTime fecha4;
            string Cliente;
            string Vendedor;
            int divisor = 0;
            int verTodo =0;
            int verCartera=0;

            Cliente = "";
            Vendedor = "";
            List<gsReporte_IndicadoresCreditosResult> lst;

            lblMensajeResumenCliente.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (Validar_Variables2() == 0)
                {
                    fecha2 = dpFechaHastaCliente.SelectedDate.Value;
                    fecha1 = fecha2.AddYears(-50); //dpFechaDesdeCliente.SelectedDate.Value;
                    fecha3 = fecha2.AddYears(-50);
                    fecha4 = fecha2.AddYears(50);

                    Cliente = null;
                    Vendedor = null;

                    verTodo = ConvertBoleanToInt(chkClientes);
                    verCartera = ConvertBoleanToInt(chkCartera);

                    lst = ListarEstadoCuentaResumenCliente(Cliente, Vendedor, fecha1, fecha2, fecha3, fecha4, divisor, verTodo, verCartera);

                }

            }
            catch (Exception ex)
            {
                lblMensajeResumenCliente.Text = ex.Message;
                lblMensajeResumenCliente.CssClass = "mensajeError";
            }

        }

        private int ConvertBoleanToInt(ICheckBoxControl objCheck)
        {
            return objCheck.Checked ? 1 : 0;
        }

        public int Validar_Variables2()
        {
            int valor = 0;

            if (dpFechaHastaCliente == null || dpFechaHastaCliente.SelectedDate.Value.ToString() == "" )//|| dpFechaDesdeCliente == null || dpFechaDesdeCliente.SelectedDate.Value.ToString() == "")
            {
                valor = 1;
                lblMensajeResumenCliente.Text = lblMensaje.Text + "Seleccionar fecha de emisión. ";
                lblMensajeResumenCliente.CssClass = "mensajeError";
            }

            return valor;
        }

        private List<gsReporte_IndicadoresCreditosResult> ListarEstadoCuentaResumenCliente(string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int divisor, int verTodo, int verCartera)
        {

            IndicadoresWCFClient objIndicadoresWCF = new IndicadoresWCFClient();
            try
            {

                BasicHttpBinding binding = (BasicHttpBinding)objIndicadoresWCF.Endpoint.Binding;
                binding.MaxReceivedMessageSize = Int32.MaxValue;
                objIndicadoresWCF.InnerChannel.OperationTimeout = new TimeSpan(0, 10, 0);
                List<gsReporte_IndicadoresCreditosResult> lstDocumentos = objIndicadoresWCF.Indicadores_CreditosCobranzas(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, 0, divisor, verTodo, verCartera).ToList();
                

                ViewState["lstIndicadores"] = JsonHelper.JsonSerializer(lstDocumentos);

                grdIndicadores.DataSource = lstDocumentos;
                grdIndicadores.DataBind();

                lblMensajeResumenCliente.Text = "Se han encontrado " + lstDocumentos.Count.ToString() + " registro.";
                lblMensajeResumenCliente.CssClass = "mensajeExito";

                lblDate2.Text = "2";
                return lstDocumentos;
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
                this.grdIndicadores.ExportSettings.Excel.Format = (PivotGridExcelFormat)Enum.Parse(typeof(PivotGridExcelFormat), "Xlsx");
                this.grdIndicadores.ExportSettings.FileName = "ReporteIndicadoresCobranzas_" + DateTime.Now.ToString("yyyyMMddHmm");
                this.grdIndicadores.ExportToExcel();
            }
            catch (Exception ex)
            {
                rwmVidaLey.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }
        }

        protected void grdEstadoCuentaCliente_PivotGridCellExporting(object sender, PivotGridCellExportingArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdIndicadores_OnNeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (lblDate2.Text == "2")
                {
                    grdIndicadores.DataSource = JsonHelper.JsonDeserialize<List<gsReporte_IndicadoresCreditosResult>>((string)ViewState["lstIndicadores"]);
                }

            }
            catch (Exception ex)
            {
                rwmVidaLey.RadAlert(ex.Message, 500, 100, "Error al consultar el archivo", "");
            }
        }
        protected void grdIndicadores_OnPivotGridCellExporting(object sender, PivotGridCellExportingArgs e)
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


        
    }
}