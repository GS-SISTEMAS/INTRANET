using GS.SISGEGS.DM;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.ComisionesWCF;
using GS.SISGEGS.Web.EmpresaWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using GS.SISGEGS.Web.Helpers;
using System.IO;
using System.Data.OleDb;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using GS.SISGEGS.Web.LoginWCF;
using System.Drawing;

namespace GS.SISGEGS.Web.Comisiones.Reporte
{
    public partial class frmReporteComision : System.Web.UI.Page
    {
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

                    rmyPeriodoGerencia.SelectedDate = DateTime.Now.AddMonths(-1);
                    rmyReporte.SelectedDate = DateTime.Now.AddMonths(-1); 
                    rmyPromotores.SelectedDate = DateTime.Now.AddMonths(-1);
                    rmyPeriodoSemilla.SelectedDate = DateTime.Now.AddMonths(-1);

                    lblPromotor.Text = "1";
                    lblVendedor.Text = "0";
                    lblGerente.Text = "3";
                    lblSemilla.Text = "4";

                    List<gsComisiones_JefaturasResult> lstGerente = new List<gsComisiones_JefaturasResult>();
                    ViewState["lstGerente"] = JsonHelper.JsonSerializer(lstGerente);
                    List<gsComisiones_PromotoresResult> lstPromotor = new List<gsComisiones_PromotoresResult>();
                    ViewState["lstPromotor"] = JsonHelper.JsonSerializer(lstPromotor);
                    List<gsComisiones_SemillasResult> lstSemilla = new List<gsComisiones_SemillasResult>();
                    ViewState["lstSemilla"] = JsonHelper.JsonSerializer(lstSemilla);

                }
            }

            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            int anho, mes;

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                //DateTime firstDayOfMonth = new DateTime(rmyReporte.SelectedDate.Value.Year, rmyReporte.SelectedDate.Value.Month, 1);
                //DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                anho = rmyReporte.SelectedDate.Value.Year;
                mes = rmyReporte.SelectedDate.Value.Month;
                ListarComisionesVendedores(anho, mes);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscarPromotor_Click(object sender, EventArgs e)
        {
            int anho, mes;

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                //DateTime firstDayOfMonth = new DateTime(rmyReporte.SelectedDate.Value.Year, rmyReporte.SelectedDate.Value.Month, 1);
                //DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                anho = rmyPromotores.SelectedDate.Value.Year;
                mes = rmyPromotores.SelectedDate.Value.Month;
                ListarComisionesPromotores(anho, mes);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscarGerencia_Click(object sender, EventArgs e)
        {
            int anho, mes;
            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                anho = rmyPeriodoGerencia.SelectedDate.Value.Year;
                mes = rmyPeriodoGerencia.SelectedDate.Value.Month;
                ListarComisionesGerentes(anho, mes);
            }
            catch (Exception ex)
            {
                lblMesajeResumen.Text = ex.Message;
                lblMesajeResumen.CssClass = "mensajeError";
            }
        }

        protected void btnBuscarSemilla_Click(object sender, EventArgs e)
        {
            int anho, mes;
            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                anho = rmyPeriodoSemilla.SelectedDate.Value.Year;
                mes = rmyPeriodoSemilla.SelectedDate.Value.Month;
                ListarComisionesSemillas(anho, mes);
            }
            catch (Exception ex)
            {
                lblMesajeResumen.Text = ex.Message;
                lblMesajeResumen.CssClass = "mensajeError";
            }
        }

        private void ListarComisionesPromotores(int anho, int mes)
        {

            ComisionWCFClient objComisionWCF = new ComisionWCFClient();
            try
            {
                List<gsComisiones_PromotoresResult> lstPromotor = objComisionWCF.gsComisiones_Promotores(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,anho,mes ).ToList();
                ViewState["lstPromotor"] = JsonHelper.JsonSerializer(lstPromotor);

                grdPromotoresPivot.DataSource = lstPromotor;
                grdPromotoresPivot.DataBind();


                lblMensaje.Text = "Mensaje " + lstPromotor.Count.ToString() + " registro.";
                lblMesajeResumen.CssClass = "mensajeExito";

                lblPromotor.Text = "1";
              
            }
            catch (Exception ex)
            {

                lblMesajeResumen.Text = ex.Message;
                lblMesajeResumen.CssClass = "mensajeError";

            }
        }
 
        private void ListarComisionesVendedores(int anho, int mes)
        {
            ComisionWCFClient objComisionWCF = new ComisionWCFClient(); 
            try
            {
                List<gsComisiones_VendedoresResult> lstVendedor = objComisionWCF.gsComisiones_Vendedores(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, anho, mes ).ToList();

                ViewState["lstVendedor"] = JsonHelper.JsonSerializer(lstVendedor);
                grdVendedor.DataSource = lstVendedor;
                grdVendedor.DataBind();
                lblVendedor.Text = "2";
            }
            catch (Exception ex)
            {
                lblMesajeResumen.Text = ex.Message;
                lblMesajeResumen.CssClass = "mensajeError";
            }
        }

        private void ListarComisionesGerentes(int anho, int mes)
        {
            ComisionWCFClient objComisionWCF = new ComisionWCFClient(); 
            try
            {
                List<gsComisiones_JefaturasResult> lstGerente = objComisionWCF.gsComisiones_Jefaturas(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, anho, mes).ToList();

                ViewState["lstGerente"] = JsonHelper.JsonSerializer(lstGerente);
                grdGerentePivot.DataSource = lstGerente;
                grdGerentePivot.DataBind();
                lblGerente.Text = "3";
            }
            catch (Exception ex)
            {
                lblMesajeResumen.Text = ex.Message;
                lblMesajeResumen.CssClass = "mensajeError";
            }
        }

        private void ListarComisionesSemillas(int anho, int mes)
        {
            ComisionWCFClient objComisionWCF = new ComisionWCFClient();
            try
            {
                List<gsComisiones_SemillasResult> lstSemilla = objComisionWCF.gsComisiones_Semillas(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, anho, mes).ToList();

                ViewState["lstSemilla"] = JsonHelper.JsonSerializer(lstSemilla);

                grdSemillaPivot.DataSource = lstSemilla;
                grdSemillaPivot.DataBind();
                lblSemilla.Text = "4";
            }
            catch (Exception ex)
            {
                lblMesajeResumen.Text = ex.Message;
                lblMesajeResumen.CssClass = "mensajeError";
            }
        }

        //-------------------------------------------------------------
        protected void grdPromotoresPivot_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (lblPromotor.Text == "1")
                {

                    List<gsComisiones_PromotoresResult> lista = JsonHelper.JsonDeserialize<List<gsComisiones_PromotoresResult>>((string)ViewState["lstPromotor"]);
                    grdPromotoresPivot.DataSource = lista;


                }

            }
            catch (Exception ex)
            {
                rwmVidaLey.RadAlert(ex.Message, 500, 100, "Error al consultar el archivo", "");
            }
        }

        protected void grdVendedor_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (lblVendedor.Text == "2")
                {
                    List<gsComisiones_VendedoresResult> lst = JsonHelper.JsonDeserialize<List<gsComisiones_VendedoresResult>>((string)ViewState["lstVendedor"]);
                    grdVendedor.DataSource = lst;
                    grdVendedor.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se encontrarón resultados.";
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdGerentePivot_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (lblGerente.Text == "3")
                {
                    grdGerentePivot.DataSource = JsonHelper.JsonDeserialize<List<gsComisiones_JefaturasResult>>((string)ViewState["lstGerente"]);
                }

            }
            catch (Exception ex)
            {
                rwmVidaLey.RadAlert(ex.Message, 500, 100, "Error al consultar el archivo", "");
            }
        }

        protected void grdSemillaPivot_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (lblSemilla.Text == "4")
                {
                    grdSemillaPivot.DataSource = JsonHelper.JsonDeserialize<List<gsComisiones_SemillasResult>>((string)ViewState["lstSemilla"]);
                }

            }
            catch (Exception ex)
            {
                rwmVidaLey.RadAlert(ex.Message, 500, 100, "Error al consultar el archivo", "");
            }
        }

        protected void grdPromotoresPivot_PivotGridCellExporting(object sender, PivotGridCellExportingArgs e)
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
        protected void grdGerentePivot_PivotGridCellExporting(object sender, PivotGridCellExportingArgs e)
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
        protected void grdSemillaPivot_PivotGridCellExporting(object sender, PivotGridCellExportingArgs e)
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

        //-------------------------------------------------------------

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

        // ---------------------- Exportar 
        protected void btnExcelGerencia_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                List<gsComisiones_JefaturasResult> lst = JsonHelper.JsonDeserialize<List<gsComisiones_JefaturasResult>>((string)ViewState["lstGerente"]);

                var query_Detalle = from c in lst
                                    orderby c.AgendaNombreG
                                    select new
                                    {
                                        Anho = c.AñoCobro,
                                        Mes = c.MesCobro,
                                        NroDocumento = c.nroDocumentoG,
                                        NombreyApellido = c.AgendaNombreG,
                                        
                                        Cargo = c.CargoG,
                                        Zona = c.zona,
                                        Cobrado = string.Format("{0:#,##0.00}", c.cobradoCumplimiento_D),
                                        Cobrado0a60 = string.Format("{0:#,##0.00}", c.cobrado0a60_D),
                                        Cobrado60a120 = string.Format("{0:#,##0.00}", c.cobrado60a120_D),
                                        Cobrado120aMas = string.Format("{0:#,##0.00}", c.cobrado120aMas_D),
                                        ComisionDolares = string.Format("{0:#,##0.00}", c.Comision_D),
                                        ComisionSoles = string.Format("{0:#,##0.00}", c.Comision_Soles)
                                    };
      
                GridView GridView1 = new GridView();
                GridView1.AllowPaging = false;
                GridView1.DataSource = query_Detalle;
                GridView1.DataBind();

                //ExportGridToExcel_Detalle();
                var FechaTC = lst.Select(x => new { x.FechaTC }).Distinct().Single();
                var TC = lst.Select(x => new { x.TC }).Distinct().Single();

                DateTime dtFecha = Convert.ToDateTime(FechaTC.FechaTC.ToString());
                string strFecha = dtFecha.ToShortDateString().ToString();
                string strTC = TC.TC.ToString();


                ExporttoExcel_Moneda(GridView1, "Gerencia", strFecha, strTC);
            }
            catch (Exception ex)
            {
                rwmVidaLey.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }
        }

        protected void btnExcelVendedor_Click(object sender, EventArgs e)
        {
            if (lblVendedor.Text == "2")
            {
                List<gsComisiones_VendedoresResult> lst = JsonHelper.JsonDeserialize<List<gsComisiones_VendedoresResult>>((string)ViewState["lstVendedor"]);

                var query_Detalle = from c in lst
                                    orderby c.zona 
                                    select new
                                    {
                                        Anho = c.AñoCobro,
                                        Mes = c.MesCobro,
                                        Zona = c.zona,
                                        NroDocumento = c.nroDocumento,
                                        NombreyApellido = c.agendanombre,
                                        Cargo = c.Cargo,
                                        Porcentaje0a60 = c.porcentajeVendedor,
                                        Porcentaje60a120 = 0.75,
                                        Cobrado = string.Format("{0:#,##0.00}", c.cobradoCumplimiento_D),
                                        Cobrado0a60 = string.Format("{0:#,##0.00}", c.cobrado0a60_D),
                                        Cobrado60a120 = string.Format("{0:#,##0.00}", c.cobrado60a120_D),
                                        Cobrado120aMas = string.Format("{0:#,##0.00}", c.cobrado120aMas_D),
                                        ComisionDolares = string.Format("{0:#,##0.00}", c.Comision_D),
                                        ComisionSoles = string.Format("{0:#,##0.00}", c.Comision_Soles)
                                    };

                var FechaTC = lst.Select(x => new { x.FechaTC }).Distinct().Single();
                var TC = lst.Select(x => new { x.TC }).Distinct().Single();

                DateTime dtFecha = Convert.ToDateTime(FechaTC.FechaTC.ToString());
                string strFecha = dtFecha.ToShortDateString().ToString();
                string strTC = TC.TC.ToString();

                GridView GridView1 = new GridView();
                GridView1.AllowPaging = false;
                GridView1.DataSource = query_Detalle;
                GridView1.DataBind();
                //ExportGridToExcel_Detalle();
                ExporttoExcel_Moneda(GridView1, "Vendedor", strFecha, strTC);
            }
        }

        protected void btnExcelPromotor_Click(object sender, EventArgs e)
        {
            if (lblPromotor.Text == "1")
            {
                List<gsComisiones_PromotoresResult> lst = JsonHelper.JsonDeserialize<List<gsComisiones_PromotoresResult>>((string)ViewState["lstPromotor"]);
              

                var query_Detalle = from c in lst
                                    orderby c.Zona
                                    select new
                                    {
                                        Anho = c.AñoCobro,
                                        Mes = c.MesCobro,
                                        Zona = c.Zona,
                                        NroDocumento = c.nroDocumento,
                                        NombreyApellido = c.agendanombre,
                                        Cargo = c.Cargo,
                                     
                                        Cobrado = string.Format("{0:#,##0.00}", c.CobradoComision_D),
                                        ComisionDolares = string.Format("{0:#,##0.00}", c.Comision_D),
                                        ComisionSoles = string.Format("{0:#,##0.00}", c.Comision_Soles)
                                    };

                var FechaTC = lst.Select(x => new { x.FechaTC }).Distinct().Single();
                var TC = lst.Select(x => new { x.TC }).Distinct().Single();

                DateTime dtFecha = Convert.ToDateTime(FechaTC.FechaTC.ToString());
                string strFecha = dtFecha.ToShortDateString().ToString() ;
                string strTC = TC.TC.ToString(); 


                GridView GridView1 = new GridView();
                GridView1.AllowPaging = false;
                GridView1.DataSource = query_Detalle;
                GridView1.DataBind();
                //ExportGridToExcel_Detalle();
                ExporttoExcel_Moneda(GridView1, "Promotor", strFecha, strTC);
            }
        }

        protected void btnExcelSemilla_Click(object sender, EventArgs e)
        {
            if (lblSemilla.Text == "4")
            {
                List<gsComisiones_SemillasResult> lst = JsonHelper.JsonDeserialize<List<gsComisiones_SemillasResult>>((string)ViewState["lstSemilla"]);

                var query_Detalle = from c in lst
                                    orderby c.zona
                                    select new
                                    {
                                        Anho = c.AñoCobro,
                                        Mes = c.MesCobro,
                                        Zona = c.zona,
                                        NroDocumento = c.nroDocumentoG,
                                        NombreyApellido = c.AgendaNombreG,
                                        Cargo = c.CargoG,
                                        Cobrado = string.Format("{0:#,##0.00}", c.cobradoD),
                                        CobradoSinIGV = string.Format("{0:#,##0.00}", c.cobradoSinIGV_D),
                                        CobradoSemillas = string.Format("{0:#,##0.00}", c.cobradoSinIGV_DSemilla),
                                        Porcentaje = string.Format("{0:#,##0.00}", c.PorcentajeCom),
                                        Cobrado0a60 = string.Format("{0:#,##0.00}", c.cobrado0a60_D),
                                        Cobrado60a120 = string.Format("{0:#,##0.00}", c.cobrado60a120_D),
                                        ComisionDolares = string.Format("{0:#,##0.00}", c.Comision_D),
                                        ComisionSoles = string.Format("{0:#,##0.00}", c.Comision_Soles)
                                    };


                GridView GridView1 = new GridView();
                GridView1.AllowPaging = false;
                GridView1.DataSource = query_Detalle;
                GridView1.DataBind();
                //ExportGridToExcel_Detalle();
                ExporttoExcel(GridView1, "Semilla");
            }
        }
        // ---------------------- Exportar 

        private void ExportGridToExcel_Resumen()
        {
            //Get the data from database into datatable
            List<gsAgendaCliente_BuscarLimiteCreditoResult> lst = JsonHelper.JsonDeserialize<List<gsAgendaCliente_BuscarLimiteCreditoResult>>((string)ViewState["lstResumenCliente"]);

            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = lst;
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ResumenEstadoCuenta_" + DateTime.Now.Millisecond.ToString() + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                //Apply text style to each Row
                GridView1.Rows[i].Attributes.Add("class", "textmode");
            }
            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        private void ExportGridToExcel_Detalle()
        {
            List<gsReporte_DocumentosPendientesResult> lst = JsonHelper.JsonDeserialize<List<gsReporte_DocumentosPendientesResult>>((string)ViewState["lstEstadoCuenta"]);

            var query_Detalle = from c in lst
                                orderby c.ClienteNombre, c.Fecha
                                select new
                                {
                                    c.ClienteNombre, c.TipoDocumento, c.EstadoDoc,
                                    FechaGiro = DateTime.Parse(c.Fecha.ToString()).ToString("dd/MM/yyyy"),
                                    FechaVencimiento = DateTime.Parse(c.FechaVencimiento.ToString()).ToString("dd/MM/yyyy"),
                                    c.DiasMora, c.NroDocumento,  c.Referencia, c.Situacion, c.Banco,
                                    NumeroUnico = Convert.ToString(c.NumeroUnico), c.monedasigno,
                                    Importe = string.Format("{0:#,##0.00}", c.Importe),
                                    DeudaSoles = string.Format("{0:#,##0.00}", c.DeudaSoles),
                                    DeudaDolares = string.Format("{0:#,##0.00}", c.DeudaDolares)
                                };


            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = query_Detalle;
            GridView1.DataBind();
            //--------------------------------

            //string file = Server.MapPath("..\Plantilla\PRODUCTO_TERMINADO\Plantilla_producto_terminado_AC.xls");
            //string destino = objUTIL.ObtenerParametro("PAPELERA", "PAPELERA", ConfigurationManager.ConnectionStrings("Conection").ConnectionString) & "\" & "Plantilla_producto_terminado_AC_" & CType(Session("Session_Usuario"), CSesion).usu_codigo & ".xls"
            //string file = "C:\\Users\\cesar.coronel\\Desktop\\Pruebas_TXT\\Origen\\Reporte.xlsx";
            //string destino = "C:\\Users\\cesar.coronel\\Desktop\\Pruebas_TXT\\Destino\\Reporte.xlsx";

            //System.IO.File.Copy(file, destino, true);

            //DataTable dt;
            //dt =  funConvertGVToDatatable(GridView1);

            //---------------------------------
            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=DetalleEstadoCuenta_" + DateTime.Now.Millisecond.ToString() + ".xls");
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);

            ////GridView1.HeaderRow.BackColor = Color.LawnGreen;

            //for (int i = 0; i < GridView1.Columns.Count; i++)
            //{
            //    //GridView1.Columns[i].HeaderStyle.BackColor = Color.LightSeaGreen;
            //    //GridView1.HeaderRow.Cells[i].BackColor = Color.LightSeaGreen;
            //    GridView1.HeaderRow.Cells[i].BackColor = Color.LightGray;
            //    GridView1.HeaderRow.Cells[i].Style["background-color"] = "#ccc"; //Your own color
            //}


            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    //Apply text style to each Row
            //    GridView1.Rows[i].Attributes.Add("class", "textmode");
            //    //GridView1.Rows[i].BackColor = Color.LightSkyBlue;
            //}

            //GridView1.RenderControl(hw);

            ////style to format numbers to string
            //string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            //Response.Write(style);
            //Response.Output.Write(sw.ToString());
            //Response.Flush();
            //Response.End();


            ////Dim toDownload = New System.IO.FileInfo(destino)
            //FileInfo toDownload = new FileInfo(destino);
            
            //Response.Clear();
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
            //Response.AddHeader("Content-Length", toDownload.Length.ToString());
            //Response.ContentType = "application/xls";
            //Response.WriteFile(destino);
            //Response.End();


        }

        private void LoadExcelData(string fileName)
        {
            string Connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\";";

            OleDbConnection con = new OleDbConnection(Connection);

            OleDbCommand command = new OleDbCommand();

            DataTable dt = new DataTable();
            OleDbDataAdapter myCommand = new OleDbDataAdapter("select * from [Sheet1$] WHERE LastName <> '' ORDER BY LastName, FirstName", con);

            myCommand.Fill(dt);

        }

        private void ExporttoExcel(GridView GridView1, string TipoReporte)
        {
            string Fecha;
            string NombreReporte;
           
            Fecha = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString()+ "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString();
            DataTable table = new DataTable();
            table = funConvertGVToDatatable(GridView1);
            NombreReporte = "Reporte" + TipoReporte + "_" + Fecha;

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename="+ NombreReporte + ".xls");

            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height

            int columnscount = GridView1.Columns.Count;

            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'>");
            HttpContext.Current.Response.Write("<TR>");
            HttpContext.Current.Response.Write("</TR>");


            HttpContext.Current.Response.Write("<TR>");
            HttpContext.Current.Response.Write("<Td style=\"border - width: 1px; border: solid; border - color:RED;\"  colspan=\"" + columnscount.ToString() + "\" >");
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write("Reporte Comisiones " + TipoReporte );
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("</TR>");

            HttpContext.Current.Response.Write("<TR>");
            HttpContext.Current.Response.Write("<Td colspan= \"" + columnscount.ToString() + "\" >");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("</TR>");


            HttpContext.Current.Response.Write("<TR>");
            foreach (DataColumn  col in table.Columns)
            {//write in new col
                HttpContext.Current.Response.Write("<Td BGCOLOR=" + "#66FF66" + " border='1' >");
                //Get column headers  and make it as bold in excel columns
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(col.ColumnName.ToString());
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");


            foreach (DataRow row in table.Rows)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td border='1'>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        private void ExporttoExcel_Moneda(GridView GridView1, string TipoReporte, string strFechaTC, string strTC)
        {
            string Fecha;
            string NombreReporte;

            Fecha = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString();
            DataTable table = new DataTable();
            table = funConvertGVToDatatable(GridView1);
            NombreReporte = "Reporte" + TipoReporte + "_" + Fecha;

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + NombreReporte + ".xls");

            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height

            int columnscount = GridView1.Columns.Count;

            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'>");
            HttpContext.Current.Response.Write("<TR>");
            HttpContext.Current.Response.Write("</TR>");


            HttpContext.Current.Response.Write("<TR>");
            HttpContext.Current.Response.Write("<Td style='border - width: 1px; border: solid; border - color:RED;'  colspan='" + columnscount.ToString() + "' >");
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write("Reporte Comisiones " + TipoReporte);
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");

            HttpContext.Current.Response.Write("<Td style='border - width: 1px; border: solid; border - color:RED;'  colspan='" + columnscount.ToString() + "' >");
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write("Fecha TC: " + strFechaTC);
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td style='border - width: 1px; border: solid; border - color:RED;'  colspan='" + columnscount.ToString() + "' >");
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write("TC: " + strTC);
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");



            HttpContext.Current.Response.Write("</TR>");

            HttpContext.Current.Response.Write("<TR>");
            HttpContext.Current.Response.Write("<Td colspan='" + columnscount.ToString() + "'>");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("</TR>");


            HttpContext.Current.Response.Write("<TR>");
            foreach (DataColumn col in table.Columns)
            {//write in new col
                HttpContext.Current.Response.Write("<Td BGCOLOR=" + "#66FF66" + " border='1' >");
                //Get column headers  and make it as bold in excel columns
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(col.ColumnName.ToString());
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");


            foreach (DataRow row in table.Rows)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td border='1'>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        private DataTable funConvertGVToDatatable(GridView dtgrv)
        {
            try
            {
                DataTable dt = new DataTable();

                if (dtgrv.HeaderRow != null)
                {
                    for (int i = 0; i < dtgrv.HeaderRow.Cells.Count; i++)
                    {
                        dt.Columns.Add(dtgrv.HeaderRow.Cells[i].Text);
                    }
                }

                foreach (GridViewRow row in dtgrv.Rows)
                {
                    DataRow dr;
                    dr = dt.NewRow();

                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        dr[i] = row.Cells[i].Text.Replace(" ", "");
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void ExportarPDF(int idEmpresa, string fechaHasta)
        {
            string fileName = GetFileName(idEmpresa);

            PdfPTable tableLayout = new PdfPTable(11);

            string path2 = this.Server.MapPath(".") + "\\tempArchivos\\";

            if (!System.IO.Directory.Exists(path2))
            { System.IO.Directory.CreateDirectory(path2); }

            string destFile = System.IO.Path.Combine(path2, fileName);

            Document doc = new Document();
            doc = new Document(PageSize.LETTER, 20, 20, 20, 20);
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(destFile, FileMode.Create));

            doc.Open();
            doc.Add(Add_Content_To_PDF(tableLayout, fechaHasta, idEmpresa));
            doc.Close();
            ////-------------------------------------

            //string WatermarkLocation = "D:\\Images\\superseded.png";
            //WatermarkLocation = Server.MapPath("~/Images/Logos/grupo.png");


            //PdfReader pdfReader = new PdfReader(destFile);
            //string FileLocation = destFile;

            //PdfStamper stamp = new PdfStamper(pdfReader, new FileStream(FileLocation.Replace(".pdf", "[temp][file].pdf"), FileMode.Create));

            //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/Logos/grupo.png"));
            //img.ScaleToFit(1700, 800);
            //img.Alignment = iTextSharp.text.Image.UNDERLYING;
            //img.SetAbsolutePosition(100, 150);
            //img.ScaleAbsoluteHeight(500);
            //img.ScaleAbsoluteWidth(500);
          
            //PdfGState graphicsState = new PdfGState();
            //graphicsState.FillOpacity = 0.02f;
            //graphicsState.StrokeOpacity = 0.03f;
            //graphicsState.AlphaIsShape = true;

            //PdfContentByte waterMark;
            //for (int page = 1; page <= pdfReader.NumberOfPages; page++)
            //{
            //    waterMark = stamp.GetOverContent(page);
            //    waterMark.AddImage(img);
            //    waterMark.SetGState(graphicsState);
            //}
            //stamp.FormFlattening = true;

            //stamp.Close();
            //pdfReader.Close();
           


            //// now delete the original file and rename the temp file to the original file
            //File.Delete(FileLocation);
            //File.Move(FileLocation.Replace( ".pdf", "[temp][file].pdf"), FileLocation);
            ////-------------------------------------



            //Response.Redirect("frmExportarPDF.aspx?sFileName=" + fileName, false);
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowInsertForm(" + fileName + ");", true);
            AbriVentana(fileName);
 
        }

        private void AbriVentana(string variable)
        {
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "AbrirNuevoVentana( " + variable + ");", true);
            //ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "AbrirNuevoVentana(" + variable + ");", true);
            RegisterStartupScript("script", "<script>window.open('frmExportarPDF.aspx?strFileNombre=" + variable + "', 'toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=0,resizable=0,width=500,height=500,top=0,left=0')</script>");
        }

        protected string GetFileName(int idEmpresa)
        {
            string file, empresa;

            string anho, mes, dia, minutos, segundo, miliseg;
            anho = DateTime.Now.Year.ToString();
            mes = DateTime.Now.Month.ToString();
            dia = DateTime.Now.Day.ToString();
            minutos = DateTime.Now.Minute.ToString();
            segundo = DateTime.Now.Second.ToString();
            miliseg = DateTime.Now.Millisecond.ToString();
            if(idEmpresa == 1)
            { empresa = "Sil"; }
            else
            { empresa = "Neo"; }

            file = empresa +  "EstadoCuenta_" + anho + mes + dia + minutos +  segundo + miliseg + ".pdf";

            return file;
        }

        private PdfPTable Add_Content_To_PDF(PdfPTable tableLayout, string fechaHasta, int idEmpresa)
        {
            EmpresaWCFClient objEmpresaWCF = new EmpresaWCFClient();
            AgendaWCFClient objAgendaWCF = new AgendaWCFClient();
            List<gsAgendaCliente_BuscarLimiteCreditoResult> lstClienteResumen;
            List<gsReporte_DocumentosPendientesResult> lstClienteDetalle;

            Empresa_BuscarDetalleResult objEmpresa;
            string urlImagen;
            lstClienteResumen = ClienteResumen();
            lstClienteDetalle = ClienteDetalle();


            Empresa_BuscarDetalleResult[] lst = objEmpresaWCF.Empresa_BuscarDetalle(idEmpresa);
            objEmpresa = lst[0];
            urlImagen = objEmpresa.logotipo.ToString();

            float[] values = new float[11];
            values[0] = 125;
            values[1] = 120;
            values[2] = 110;
            values[3] = 120;
            values[4] = 115;
            values[5] = 125;
            values[6] = 120;
            values[7] = 120;
            values[8] = 110;
            values[9] = 90;
            values[10] = 90;

            tableLayout.SetWidths(values);        //Set the pdf headers
            tableLayout.WidthPercentage = 100;       //Set the PDF File witdh percentage

            //Add Title to the PDF file at the top

            //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath(urlImagen));
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/Logos/grupo.png"));

            logo.ScaleAbsolute(205,90);
            PdfPCell imageCell = new PdfPCell(logo);
            imageCell.Colspan = 2; // either 1 if you need to insert one cell
            imageCell.Border = 0;
            imageCell.HorizontalAlignment = Element.ALIGN_LEFT;

            //tableLayout.AddCell(imageCell);
            tableLayout.AddCell(new PdfPCell(new Phrase("Estado de cuenta al " + fechaHasta , new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, 1,  iTextSharp.text.BaseColor.DARK_GRAY ))) { Colspan = 10, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });
            tableLayout.AddCell(new PdfPCell(new Phrase( DateTime.Now.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 5, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 1, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });

            tableLayout.AddCell(new PdfPCell(new Phrase(objEmpresa.ruc +" "+ objEmpresa.razonSocial , new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 6, Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT });
            tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 5, Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER });

            tableLayout.AddCell(new PdfPCell(new Phrase(objEmpresa.direccion , new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 6, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_LEFT });
            tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 5, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });

            //Add Cliente
            int cont = 0;

            foreach (gsAgendaCliente_BuscarLimiteCreditoResult ClienteResumenObj in lstClienteResumen)
            {
                cont = cont + 1;
                // ADD Cliente

                tableLayout.AddCell(new PdfPCell(new Phrase("1. Razón Social", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                List<gsAgenda_BuscarClienteDetalleResult> LimiteAgenda = objAgendaWCF.Agenda_BuscarClienteDetalle(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, ClienteResumenObj.ID_Agenda.ToString()).ToList();
                if(LimiteAgenda.Count > 0)
                {
                    gsAgenda_BuscarClienteDetalleResult AgendaResumen = LimiteAgenda[0];
                    tableLayout.AddCell(new PdfPCell(new Phrase(AgendaResumen.ruc + " " + AgendaResumen.Agendanombre, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase(AgendaResumen.Direccion + " " + AgendaResumen.Distrito + " - " + AgendaResumen.Provincia + " - " + AgendaResumen.Departamento + " - " + AgendaResumen.Pais, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, BorderColorLeft = BaseColor.RED, BorderColorTop = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                }
                else
                {
                    tableLayout.AddCell(new PdfPCell(new Phrase(ClienteResumenObj.ruc + " " + ClienteResumenObj.AgendaNombre, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Registrar dirección fiscal. ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, BorderColorLeft = BaseColor.RED, BorderColorTop = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                }


                tableLayout.AddCell(new PdfPCell(new Phrase("   ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 11, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });


                //ADD Resumen cliente
                decimal TCResumen = Convert.ToDecimal(ClienteResumenObj.TipoCambio);
                if(TCResumen < 0)
                {
                    TCResumen = TCResumen * -1;
                }


                var query_Detalle = from c in lstClienteDetalle
                                    where c.ID_Agenda == ClienteResumenObj.ID_Agenda
                                    orderby c.ClienteNombre, c.Fecha
                                    select new
                                    {
                                        c.TC, c.ID_Moneda,
                                        c.ID_Agenda,

                                        Pendiente = c.ID_Moneda == 0 ? c.ImportePendiente :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente / TCResumen :
                                                    c.ImportePendiente,

                                        Pendiente_NoVencido = c.ID_Moneda == 0 ? c.ImportePendiente_NoVencido :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_NoVencido / TCResumen : 
                                                    c.ImportePendiente_NoVencido,

                                        Pendiente_VenceHoy = c.ID_Moneda == 0 ? c.ImportePendiente_VenceHoy :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_VenceHoy/ TCResumen :
                                                    c.ImportePendiente_VenceHoy,

                                        Pendiente_01a30 = c.ID_Moneda == 0 ? c.ImportePendiente_01a30 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_01a30 / TCResumen :
                                                    c.ImportePendiente_01a30,
                                        Pendiente_31a60 = c.ID_Moneda == 0 ? c.ImportePendiente_31a60 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_31a60 / TCResumen :
                                                    c.ImportePendiente_31a60,
                                        Pendiente_61a120 = c.ID_Moneda == 0 ? c.ImportePendiente_61a120 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_61a120 / TCResumen :
                                                    c.ImportePendiente_61a120,
                                        Pendiente_121a360 = c.ID_Moneda == 0 ? c.ImportePendiente_121a360 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_121a360 / TCResumen :
                                                    c.ImportePendiente_121a360,
                                        Pendiente_361aMas = c.ID_Moneda == 0 ? c.ImportePendiente_361aMas :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_361aMas / TCResumen :
                                                    c.ImportePendiente_361aMas

                                        //DeudaVencida = c.ImportePendiente_01a30 + c.ImportePendiente_31a60 + c.ImportePendiente_61a120 + c.ImportePendiente_121a360 + c.ImportePendiente_361aMas
                                    };

                var sumImportePendiente = query_Detalle.ToList().Select(c => c.Pendiente).Sum();
                var sumImportePendiente_NoVencido = query_Detalle.ToList().Select(c => c.Pendiente_NoVencido).Sum();
                var sumImportePendiente_VenceHoy = query_Detalle.ToList().Select(c => c.Pendiente_VenceHoy).Sum();

                var sumImportePendiente_01a30 = query_Detalle.ToList().Select(c => c.Pendiente_01a30).Sum();
                var sumImportePendiente_31a60 = query_Detalle.ToList().Select(c => c.Pendiente_31a60).Sum();
                var sumImportePendiente_61a120 = query_Detalle.ToList().Select(c => c.Pendiente_61a120).Sum();
                var sumImportePendiente_121a360 = query_Detalle.ToList().Select(c => c.Pendiente_121a360).Sum();
                var sumImportePendiente_361aMas = query_Detalle.ToList().Select(c => c.Pendiente_361aMas).Sum();

                int NoVencido;
                int  DeudaVencida;
                NoVencido = Convert.ToInt32(sumImportePendiente_NoVencido) + Convert.ToInt32(sumImportePendiente_VenceHoy);
                DeudaVencida = Convert.ToInt32(sumImportePendiente_01a30) + Convert.ToInt32(sumImportePendiente_31a60) + Convert.ToInt32(sumImportePendiente_61a120) + Convert.ToInt32(sumImportePendiente_121a360) + Convert.ToInt32(sumImportePendiente_361aMas);

    
                string strsumNoVencido = string.Format("{0:$ #,##0.00}", NoVencido);
                string strsumImportePendiente_01a30 = string.Format("{0:$ #,##0.00}", sumImportePendiente_01a30);
                string strsumImportePendiente_31a60 = string.Format("{0:$ #,##0.00}", sumImportePendiente_31a60);
                string strsumImportePendiente_61a120 = string.Format("{0:$ #,##0.00}", sumImportePendiente_61a120);
                string strsumImportePendiente_121a360 = string.Format("{0:$ #,##0.00}", sumImportePendiente_121a360);
                string strsumImportePendiente_361aMas = string.Format("{0:$ #,##0.00}", sumImportePendiente_361aMas);
                string strsumImportePendiente = string.Format("{0:$ #,##0.00}", sumImportePendiente);

                string strsumDeudaVencida = string.Format("{0:$ #,##0.00}", DeudaVencida);

                string strLineaCredito = string.Format("{0:$ #,##0.00}", ClienteResumenObj.LineaCredito);
                string strDeudaVencida = string.Format("{0:$ #,##0.00}", strsumDeudaVencida);
                string strTotalCredito = string.Format("{0:$ #,##0.00}", ClienteResumenObj.TotalCredito);
                string strCreditoDisponible = string.Format("{0:$ #,##0.00}", ClienteResumenObj.CreditoDisponible);


                tableLayout.AddCell(new PdfPCell(new Phrase("2. Línea de crédito", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Deuda vencida", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Deuda total", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Línea disponible", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 7, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

               

                tableLayout.AddCell(new PdfPCell(new Phrase(strLineaCredito, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                tableLayout.AddCell(new PdfPCell(new Phrase(strDeudaVencida, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                tableLayout.AddCell(new PdfPCell(new Phrase(strTotalCredito, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                tableLayout.AddCell(new PdfPCell(new Phrase(strCreditoDisponible, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 7, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 11, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });


                tableLayout.AddCell(new PdfPCell(new Phrase("3. NoVencido", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                 tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 01a30", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 31a60", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan =1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 61a120", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan =1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 121a360", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 361aMas", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan =1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Deuda total", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 4, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });

                tableLayout.AddCell(new PdfPCell(new Phrase(strsumNoVencido, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                tableLayout.AddCell(new PdfPCell(new Phrase(strsumImportePendiente_01a30, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                tableLayout.AddCell(new PdfPCell(new Phrase(strsumImportePendiente_31a60, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                tableLayout.AddCell(new PdfPCell(new Phrase(strsumImportePendiente_61a120, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                tableLayout.AddCell(new PdfPCell(new Phrase(strsumImportePendiente_121a360, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                tableLayout.AddCell(new PdfPCell(new Phrase(strsumImportePendiente_361aMas, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                tableLayout.AddCell(new PdfPCell(new Phrase(strsumImportePendiente, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 4, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });


                tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 11, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });


                // ADD DETALLE
                tableLayout.AddCell(new PdfPCell(new Phrase("4. TipoDoc.", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                AddCellToHeader(tableLayout, "N° Documento");
                AddCellToHeader(tableLayout, "Fecha Emis.");
                AddCellToHeader(tableLayout, "Fecha Vcto.");
                AddCellToHeader(tableLayout, "DíasMora");
                AddCellToHeader(tableLayout, "Estado Doc.");
                AddCellToHeaderColspan(tableLayout, "Letra Banco", 2);
                AddCellToHeader(tableLayout, "N° Unico");
                AddCellToHeader(tableLayout, "Importe");
                AddCellToHeader(tableLayout, "Saldo Doc.");

                var query_DetalleTotal = from c in lstClienteDetalle
                                    where c.ID_Agenda == ClienteResumenObj.ID_Agenda
                                    orderby c.ClienteNombre, c.FechaVencimiento
                                    select new
                                    {
                                        c.TipoDocumento,
                                        c.NroDocumento,
                                        c.Fecha,
                                        c.FechaVencimiento,
                                        c.EstadoDoc,
                                        c.Banco,
                                        c.NumeroUnico,
                                        c.Importe,
                                        c.ImportePendiente,
                                        c.monedasigno,
                                        c.ID_Moneda,
                                        c.TC,
                                        c.DiasMora
                                    };
                foreach (var query_Clientel in query_DetalleTotal)
                {
                    AddCellToBody(tableLayout, query_Clientel.TipoDocumento);
                    AddCellToBody(tableLayout, query_Clientel.NroDocumento);
                    AddCellToBody(tableLayout, query_Clientel.Fecha.Value.ToShortDateString());
                    AddCellToBody(tableLayout, query_Clientel.FechaVencimiento.Value.ToShortDateString());
                    AddCellToBody(tableLayout, query_Clientel.DiasMora.ToString());
                    AddCellToBody(tableLayout, query_Clientel.EstadoDoc);

                    AddCellToBodyColspan(tableLayout, query_Clientel.Banco, 2);

                    AddCellToBody(tableLayout, query_Clientel.NumeroUnico);

                    string Importe;
                    string ImportePendiente;

                    if(query_Clientel.ID_Moneda == 0)
                    {
                        Importe = string.Format("{0:$ #,##0.00}", query_Clientel.Importe);
                        ImportePendiente = string.Format("{0:$ #,##0.00}", query_Clientel.ImportePendiente);
                    }
                    else
                    {
                        Importe = string.Format("{0:S/ #,##0.00}", query_Clientel.Importe);
                        ImportePendiente = string.Format("{0:S/ #,##0.00}", query_Clientel.ImportePendiente);
                    }
                    tableLayout.AddCell(new PdfPCell(new Phrase(Importe.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase(ImportePendiente.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                }

                tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 11, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });
                tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 11, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });


            }

            return tableLayout;
        }

        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });

        }

        private static void AddCellToHeaderColspan(PdfPTable tableLayout, string cellText, int intCol)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = intCol,  HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });

        }

        private static void AddCellToBody(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

        }

        private static void AddCellToBodyColspan(PdfPTable tableLayout, string cellText, int intCol)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan= intCol, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

        }

        private List<gsAgendaCliente_BuscarLimiteCreditoResult> ClienteResumen()
        {
            List<gsAgendaCliente_BuscarLimiteCreditoResult> lst = JsonHelper.JsonDeserialize<List<gsAgendaCliente_BuscarLimiteCreditoResult>>((string)ViewState["lstResumenCliente"]);
            return lst;
        }

        private List<gsReporte_DocumentosPendientesResult> ClienteDetalle()
        {
            //DataTable dtTabla;
            List<gsReporte_DocumentosPendientesResult> lst = JsonHelper.JsonDeserialize<List<gsReporte_DocumentosPendientesResult>>((string)ViewState["lstEstadoCuenta"]);
            return lst;
        }

        private byte[] CreatePDF2(int idEmpresa, string fechaHasta)
        {
          
            PdfPTable tableLayout = new PdfPTable(11);

            Document document = new Document();
            document = new Document(PageSize.LETTER, 20, 20, 20, 20);

            using (MemoryStream output = new MemoryStream())
            {
                PdfWriter wri = PdfWriter.GetInstance(document, output);
                PdfWriter.GetInstance(document, Response.OutputStream);

                document.Open();
                document.Add(Add_Content_To_PDF(tableLayout, fechaHasta, idEmpresa));
                document.Close();

                return output.ToArray();
            }

        }

        private void ShowPdf(byte[] strS)
        {
            string fileName = "ReporteEstadoCuenta_" + DateTime.Now.Millisecond.ToString() + ".pdf";
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);

            Response.BinaryWrite(strS);
            Response.End();
            Response.Flush();
            Response.Clear();
        }

        private void DownloadAsPDF(MemoryStream ms)
        {

            string fileName = "ReporteEstadoCuenta_" + DateTime.Now.Millisecond.ToString() + ".pdf";
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment;filename="+ fileName);
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.End();
            ms.Close();

        }

        public void CreatePDFFromMemoryStream(int idEmpresa, string fechaHasta)
        {
            //(1)using PDFWriter
            PdfPTable tableLayout = new PdfPTable(11);

            Document doc = new Document();
            doc = new Document(PageSize.LETTER, 20, 20, 20, 20);
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
            doc.Open();
            doc.Add(Add_Content_To_PDF(tableLayout, fechaHasta, idEmpresa));

            writer.CloseStream = false;
            doc.Close();
            memoryStream.Position = 0;

            DownloadAsPDF(memoryStream);

        }

        private void PDF(int idEmpresa, string fechaHasta)
        {
            PdfPTable tableLayout = new PdfPTable(11);
            string fileName = "ReporteEstadoCuenta_" + DateTime.Now.Millisecond.ToString() + ".pdf";

            GridView GridView2 = new GridView();
            GridView2.DataBind();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename="+ fileName);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            GridView2.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document doc = new Document();
            doc = new Document(PageSize.LETTER, 20, 20, 20, 20);

            HTMLWorker htmlparser = new HTMLWorker(doc);
            PdfWriter.GetInstance(doc, Response.OutputStream);
            doc.Open();
            doc.Add(Add_Content_To_PDF(tableLayout, fechaHasta, idEmpresa));
            htmlparser.Parse(sr);
            doc.Close();

            Response.Write(doc);
            Response.End();
        }

        private void AbrirArchivo(string destino)
        {
            System.IO.FileInfo toDownload = new System.IO.FileInfo(destino);
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
            Response.AddHeader("Content-Length", toDownload.Length.ToString());
            Response.ContentType = "application/pdf";
            Response.WriteFile(destino);
            Response.End();
        }

    }
}