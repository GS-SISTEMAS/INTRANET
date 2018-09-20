using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.ReporteContabilidadWCF;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.DocumentoWCF;
using Telerik.Web.UI;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;
using GS.SISGEGS.Web.Helpers;
//using System.Drawing;

namespace GS.SISGEGS.Web.Finanzas.Reportes
{
    public partial class frmResumAfiliadas : System.Web.UI.Page
    {

        private void CargarGrillas(DateTime fechaCorte, decimal moneda)
        {
            List<ReporteAfiliadasResult> Lista = new List<ReporteAfiliadasResult>();
            try
            {
                SetearNombresColumna();
                ReporteContabilidadWCFClient servicio = new ReporteContabilidadWCFClient();
                //Obtencion de datos 
                int IdEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
                //DateTime fechaCorte = new DateTime(dpFechaCorte.SelectedDate.Value.Year, dpFechaCorte.SelectedDate.Value.Month, 1).AddMonths(1).AddDays(-1);
                //decimal moneda =Convert.ToDecimal(ddlMoneda.SelectedValue);
                List<int> RegistrosGrillas = new List<int>();
                Lista = servicio.ResumenAfiliadas(IdEmpresa, CodigoUsuario, "I", fechaCorte, moneda).ToList();
                RegistrosGrillas.Add(Lista.Count());

                grdCobrar.DataSource = Lista;
                grdCobrar.DataBind();

                Lista = servicio.ResumenAfiliadas(IdEmpresa, CodigoUsuario, "E", fechaCorte, moneda).ToList();
                RegistrosGrillas.Add(Lista.Count());
                grdPagar.DataSource = Lista;
                grdPagar.DataBind();

                this.lblRegistros.Text = "Número de registros cuentas por cobrar: " + RegistrosGrillas[0].ToString() + " &deg; Número de registros de cuentas por pagar: " + RegistrosGrillas[1].ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void MostrarGrafico(string operacion)
        {
            List<ReporteAfiliadasResult> Lista = new List<ReporteAfiliadasResult>();
            try
            {

                List<string> ListaColores = new List<string>();
                ListaColores.Add("Purple");
                ListaColores.Add("Orange");
                ListaColores.Add("Green");
                ListaColores.Add("Blue");
                ListaColores.Add("Red");

                ReporteContabilidadWCFClient servicio = new ReporteContabilidadWCFClient();
                //Obtencion de datos 
                int IdEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
                DateTime fechaCorte = new DateTime(dpFechaCorte.SelectedDate.Value.Year, dpFechaCorte.SelectedDate.Value.Month, 1).AddMonths(1).AddDays(-1);
                decimal moneda = Convert.ToDecimal(ddlMoneda.SelectedValue);

                Lista = servicio.ResumenAfiliadas(IdEmpresa, CodigoUsuario, operacion, fechaCorte, moneda).ToList();

                decimal totalMonto = Convert.ToDecimal(Lista.Sum(x => x.MontoPendiente));

                int index = 0;

                var NewLista = (from tbl in Lista
                                select (new
                                {
                                    NombreEmpresa = tbl.nombreempresa,
                                    valorp = decimal.Round(Convert.ToDecimal(tbl.MontoPendiente) * 100m / totalMonto, 2, MidpointRounding.ToEven),
                                    MayorValor = false,
                                    colorColumn = ListaColores[index++]
                                })).ToList();

                string monedaText = "";

                if (operacion == "I")
                {
                    //GraficoPie.ChartTitle.Text = "Distribucion de cuentas por cobrar " + fechaCorte.Year.ToString();
                    lblTituloGraph.Text = "Distribucion de cuentas por cobrar";
                }
                else
                {
                    lblTituloGraph.Text = "Distribucion de cuentas por pagar";
                }

                monedaText = moneda == 0 ? "Dólares" : "Soles";
                lblMontoTotal.Text = totalMonto.ToString("#,00.00") + " " + monedaText;

                GraficoPie.DataSource = NewLista;
                GraficoPie.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GenerarExcel(DateTime fechaCorte, decimal moneda)
        {

            List<ReporteAfiliadasResult> ListaIngresos = new List<ReporteAfiliadasResult>();
            List<ReporteAfiliadasResult> ListaEgresos = new List<ReporteAfiliadasResult>();

            try
            {
                ReporteContabilidadWCFClient servicio = new ReporteContabilidadWCFClient();
                //Obtencion de datos 
                int IdEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
                //DateTime fechaCorte = new DateTime(dpFechaCorte.SelectedDate.Value.Year, dpFechaCorte.SelectedDate.Value.Month, 1).AddMonths(1).AddDays(-1);
                //decimal moneda = Convert.ToDecimal(ddlMoneda.SelectedValue);


                ListaIngresos = servicio.ResumenAfiliadas(IdEmpresa, CodigoUsuario, "I", fechaCorte, moneda).ToList();
                ListaEgresos = servicio.ResumenAfiliadas(IdEmpresa, CodigoUsuario, "E", fechaCorte, moneda).ToList();

                if (ListaIngresos.Count > 0 && ListaEgresos.Count > 0)
                {
                    FileInfo nombreArchivo = new FileInfo("ResumenAfiliados.xlsx");
                    var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    var fileStream = new MemoryStream();
                    using (var package = new ExcelPackage(nombreArchivo))
                    {
                        var ws1 = package.Workbook.Worksheets.Add("Cuentas por cobrar");
                        //ws1.Cells.LoadFromCollection(ListaIngresos);

                        //Titulos
                        ws1.Cells["A1"].Value = "Empresa";
                        ws1.Cells["B1"].Value = "Deuda inicial al " + fechaCorte.Year.ToString();
                        ws1.Cells["C1"].Value = "Deuda acumulada al " + fechaCorte.Year.ToString();
                        ws1.Cells["D1"].Value = "Cargos " + fechaCorte.Year.ToString();
                        ws1.Cells["E1"].Value = "Abonos " + fechaCorte.Year.ToString();
                        ws1.Cells["F1"].Value = "Nivel de cumplimiento";

                        //Filas
                        int i = 2;
                        foreach (ReporteAfiliadasResult item in ListaIngresos)
                        {
                            ws1.Cells[i, 1].Value = item.nombreempresa;
                            ws1.Cells[i, 2].Value = item.montoinicio;
                            ws1.Cells[i, 3].Value = item.MontoPendiente;
                            ws1.Cells[i, 4].Value = item.Cargos;
                            ws1.Cells[i, 5].Value = item.Abonos;
                            ws1.Cells[i, 6].Value = item.Cumplimiento;
                            i++;
                        }

                        using (var range = ws1.Cells[1, 1, 1, 6])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(0, 201, 74));
                            range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        }
                        ws1.Cells.AutoFitColumns();

                        //Segunda hoja

                        var ws2 = package.Workbook.Worksheets.Add("Cuentas por pagar");
                        //ws1.Cells.LoadFromCollection(ListaIngresos);

                        //Titulos
                        ws2.Cells["A1"].Value = "Empresa";
                        ws2.Cells["B1"].Value = "Deuda inicial al " + fechaCorte.Year.ToString();
                        ws2.Cells["C1"].Value = "Deuda acumulada al " + fechaCorte.Year.ToString();
                        ws2.Cells["D1"].Value = "Cargos " + fechaCorte.Year.ToString();
                        ws2.Cells["E1"].Value = "Abonos " + fechaCorte.Year.ToString();
                        ws2.Cells["F1"].Value = "Nivel de cumplimiento";

                        //Filas
                        i = 2;
                        foreach (ReporteAfiliadasResult item in ListaEgresos)
                        {
                            ws2.Cells[i, 1].Value = item.nombreempresa;
                            ws2.Cells[i, 2].Value = item.montoinicio;
                            ws2.Cells[i, 3].Value = item.MontoPendiente;
                            ws2.Cells[i, 4].Value = item.Cargos;
                            ws2.Cells[i, 5].Value = item.Abonos;
                            ws2.Cells[i, 6].Value = item.Cumplimiento;
                            i++;
                        }

                        using (var range = ws2.Cells[1, 1, 1, 6])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(0, 201, 74));
                            range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        }
                        ws2.Cells.AutoFitColumns();

                        //Fin segunda hoja                        
                        fileStream = new MemoryStream(package.GetAsByteArray());
                    }

                    Response.ContentType = contentType;
                    Response.AddHeader("content-disposition", "attachment; filename=" + nombreArchivo.Name);
                    Response.BinaryWrite(fileStream.ToArray());
                    Response.End();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void SetearNombresColumna()
        {
            //Formatear los nombres de columna
            grdCobrar.Columns[1].HeaderText = "Deuda inicial al " + this.dpFechaCorte.SelectedDate.Value.Year.ToString();
            grdCobrar.Columns[2].HeaderText = "Deuda acumulada al " + this.dpFechaCorte.SelectedDate.Value.Year.ToString();
            grdCobrar.Columns[3].HeaderText = "Cargos al " + this.dpFechaCorte.SelectedDate.Value.Year.ToString();
            grdCobrar.Columns[4].HeaderText = "Abonos al " + this.dpFechaCorte.SelectedDate.Value.Year.ToString();

            grdPagar.Columns[1].HeaderText = "Deuda inicial al " + this.dpFechaCorte.SelectedDate.Value.Year.ToString();
            grdPagar.Columns[2].HeaderText = "Deuda acumulada al " + this.dpFechaCorte.SelectedDate.Value.Year.ToString();
            grdPagar.Columns[3].HeaderText = "Cargos al " + this.dpFechaCorte.SelectedDate.Value.Year.ToString();
            grdPagar.Columns[4].HeaderText = "Abonos al " + this.dpFechaCorte.SelectedDate.Value.Year.ToString();

        }

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

                    this.dpFechaCorte.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                    //DateTime fechaCorte = new DateTime(dpFechaCorte.SelectedDate.Value.Year, dpFechaCorte.SelectedDate.Value.Month, 1).AddMonths(1).AddDays(-1);
                    //decimal moneda =Convert.ToDecimal(ddlMoneda.SelectedValue);
                    ddlMoneda.SelectedValue = "0";

                    this.Label1.Text = Label1.Text + " (" + ddlMoneda.SelectedItem.Text + ")";
                    this.Label2.Text = Label2.Text + " (" + ddlMoneda.SelectedItem.Text + ")";

                    CargarGrillas(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1), 0);

                    List<DetalleOperacionFamiliaResult> listaDetalle = new List<DetalleOperacionFamiliaResult>();
                    ViewState["tmpPivot"] = JsonHelper.JsonSerializer(listaDetalle);

                    DocumentoWCFClient cliente = new DocumentoWCFClient();
                    int IdEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                    int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
                    List<ListarFamiliasResult> lista = cliente.ListarFamilias(IdEmpresa, CodigoUsuario).ToList();

                    this.ddlFamilias.DataSource = lista;
                    ddlFamilias.DataTextField = "NombreFamilia";
                    ddlFamilias.DataValueField = "IdTipoFamilia";
                    ddlFamilias.DataBind();

                    ddlFamilias.Items.Insert(0, new DropDownListItem() { Text = "Todos", Value = "-1" });

                    this.RadDropDownList1.DataSource = lista;
                    RadDropDownList1.DataTextField = "NombreFamilia";
                    RadDropDownList1.DataValueField = "IdTipoFamilia";
                    RadDropDownList1.DataBind();

                    RadDropDownList1.Items.Insert(0, new DropDownListItem() { Text = "Todos", Value = "-1" });

                    //RadDropDownList1

                }
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error", "");
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            DateTime fechaCorte = new DateTime(dpFechaCorte.SelectedDate.Value.Year, dpFechaCorte.SelectedDate.Value.Month, 1).AddMonths(1).AddDays(-1);
            decimal moneda = Convert.ToDecimal(ddlMoneda.SelectedValue);
            GenerarExcel(fechaCorte, moneda);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {

                DateTime fechaCorte = new DateTime(dpFechaCorte.SelectedDate.Value.Year, dpFechaCorte.SelectedDate.Value.Month, 1).AddMonths(1).AddDays(-1);
                decimal moneda = Convert.ToDecimal(ddlMoneda.SelectedValue);

                this.Label1.Text = "Cuentas por cobrar" + " (" + ddlMoneda.SelectedItem.Text + ")";
                this.Label2.Text = "Cuentas por pagar" + " (" + ddlMoneda.SelectedItem.Text + ")";

                CargarGrillas(fechaCorte, moneda);
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error", "");
            }

        }

        protected void btnGraph2_Click(object sender, EventArgs e)
        {
            try
            {
                this.modalGrafico.Title = "Resumen de afiliadas - Cuentas por pagar";
                this.modalGrafico.VisibleStatusbar = false;
                modalGrafico.Behaviors = WindowBehaviors.Pin | WindowBehaviors.Close | WindowBehaviors.Move;

                MostrarGrafico("E");

                string scriptModal = "function f(){$find(\"" + modalGrafico.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", scriptModal, true);
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error", "");
            }

        }

        protected void btnGraph1_Click(object sender, EventArgs e)
        {
            try
            {
                this.modalGrafico.Title = "Resumen de afiliadas - Cuentas por cobrar";
                this.modalGrafico.VisibleStatusbar = false;
                modalGrafico.Behaviors = WindowBehaviors.Pin | WindowBehaviors.Close | WindowBehaviors.Move;

                MostrarGrafico("I");

                string scriptModal = "function f(){$find(\"" + modalGrafico.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", scriptModal, true);
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error", "");
            }

        }

        protected void grdPagar_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                Label control = (Label)e.Item.FindControl("lblNivelCumple");
                Image controlImagen = (Image)e.Item.FindControl("imgSemaforo");
                //control.Text = "Server";
                GridDataItem datos = (GridDataItem)e.Item;
                ReporteAfiliadasResult listaDatos = (ReporteAfiliadasResult)datos.DataItem;
                decimal valor = Convert.ToDecimal(listaDatos.Cumplimiento);
                control.Text = valor.ToString("#,00.00") + "%";

                if (valor >= 90)
                {
                    controlImagen.ImageUrl = "~/Images/Icons/circle-green-16.png";
                }
                else if (valor >= 75)
                {
                    controlImagen.ImageUrl = "~/Images/Icons/circle-yellow-16.png";
                }
                else
                {
                    controlImagen.ImageUrl = "~/Images/Icons/circle-red-16.png";
                }
            }
        }

        protected void grdCobrar_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                Label control = (Label)e.Item.FindControl("lblNivelCumple");
                Image controlImagen = (Image)e.Item.FindControl("imgSemaforo");
                //control.Text = "Server";
                GridDataItem datos = (GridDataItem)e.Item;
                ReporteAfiliadasResult listaDatos = (ReporteAfiliadasResult)datos.DataItem;
                decimal valor = Convert.ToDecimal(listaDatos.Cumplimiento);
                control.Text = valor.ToString("#,00.00") + "%";

                if (valor >= 90)
                {
                    controlImagen.ImageUrl = "~/Images/Icons/circle-green-16.png";
                }
                else if (valor >= 75)
                {
                    controlImagen.ImageUrl = "~/Images/Icons/circle-yellow-16.png";
                }
                else
                {
                    controlImagen.ImageUrl = "~/Images/Icons/circle-red-16.png";
                }

            }
        }

        protected void RadGrid0_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

        }

        protected void grdCobrar_ItemCommand(object sender, GridCommandEventArgs e)
        {
            string scriptModal = "";

            try
            {
                if (e.CommandName == "Agrupamiento")
                {

                    this.pnlAgrupamiento1.Visible = true;
                    this.pnlDetalleMov.Visible = false;
                    this.pnlAnticuamiento.Visible = false;
                    this.pnlDetalleAntiMov.Visible = false;

                    int IdEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                    int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
                    string codigoEmpresaComparar = e.CommandArgument.ToString();
                    //rwmReporte.RadAlert("Ejecutado Agrupamiento", 500, 100, "Error", "");
                    string mes = ("00" + dpFechaCorte.SelectedDate.Value.Month.ToString()).Substring(("00" + dpFechaCorte.SelectedDate.Value.Month.ToString()).Length - 2);
                    this.lbltituloPopupDetalle.Text = "Resumen de afiliadas: al " + mes + "-" + dpFechaCorte.SelectedDate.Value.Year.ToString();

                    string nombreEmpresa = ((Usuario_LoginResult)Session["Usuario"]).nombreComercial;

                    ViewState["codigoEmpresa"] = JsonHelper.JsonSerializer(codigoEmpresaComparar);

                    gsAgenda_ListarClienteResult[] agenda;
                    AgendaWCFClient ServicioAgenda = new AgendaWCFClient();
                    agenda = ServicioAgenda.Agenda_ListarCliente(IdEmpresa, CodigoUsuario, codigoEmpresaComparar, null);

                    lblTituloEmpresaAEmpresa.Text = "Cuentas por cobrar : " + nombreEmpresa.ToUpper() + " a " + agenda[0].Nombre.ToUpper() + " (" + ddlMoneda.SelectedItem.Text + ")";

                    this.ModalDetalle.Title = "Resumen por agrupamiento";
                    this.ModalDetalle.VisibleStatusbar = false;
                    ModalDetalle.Behaviors = WindowBehaviors.Pin | WindowBehaviors.Close | WindowBehaviors.Move;


                    //Cargar Pivot



                    DateTime fechaCorte = new DateTime(dpFechaCorte.SelectedDate.Value.Year, dpFechaCorte.SelectedDate.Value.Month, 1).AddMonths(1).AddDays(-1);
                    decimal moneda = Convert.ToDecimal(ddlMoneda.SelectedValue);

                    List<DetalleOperacionFamiliaResult> ListaDetalle = new List<DetalleOperacionFamiliaResult>();
                    ReporteContabilidadWCFClient servicio = new ReporteContabilidadWCFClient();
                    ListaDetalle = servicio.DetalleOperacionFamilia(IdEmpresa, CodigoUsuario, fechaCorte, "I", codigoEmpresaComparar, moneda).ToList();
                    ViewState["tmpPivot"] = JsonHelper.JsonSerializer(ListaDetalle);
                    ViewState["Operacion"] = "I";
                    this.PivotAgrupamiento.DataSource = ListaDetalle;
                    this.PivotAgrupamiento.DataBind();

                    scriptModal = "function f(){$find(\"" + ModalDetalle.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key1", scriptModal, true);
                }
                else if (e.CommandName == "Anticuamiento")
                {
                    //rwmReporte.RadAlert("Ejecutado anticuamiento", 500, 100, "Error", "");

                    this.pnlAgrupamiento1.Visible = false;
                    this.pnlDetalleMov.Visible = false;
                    this.pnlAnticuamiento.Visible = true;
                    this.pnlDetalleAntiMov.Visible = false;

                    int IdEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                    int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
                    string codigoEmpresaComparar = e.CommandArgument.ToString();
                    //rwmReporte.RadAlert("Ejecutado Agrupamiento", 500, 100, "Error", "");
                    string mes = ("00" + dpFechaCorte.SelectedDate.Value.Month.ToString()).Substring(("00" + dpFechaCorte.SelectedDate.Value.Month.ToString()).Length - 2);
                    this.Label3.Text = "Resumen de afiliadas: al " + mes + "-" + dpFechaCorte.SelectedDate.Value.Year.ToString();

                    string nombreEmpresa = ((Usuario_LoginResult)Session["Usuario"]).nombreComercial;

                    ViewState["codigoEmpresa"] = JsonHelper.JsonSerializer(codigoEmpresaComparar);

                    gsAgenda_ListarClienteResult[] agenda;
                    AgendaWCFClient ServicioAgenda = new AgendaWCFClient();
                    agenda = ServicioAgenda.Agenda_ListarCliente(IdEmpresa, CodigoUsuario, codigoEmpresaComparar, null);

                    Label4.Text = "Cuentas por cobrar : " + nombreEmpresa.ToUpper() + " a " + agenda[0].Nombre.ToUpper() + " (" + ddlMoneda.SelectedItem.Text + ")";

                    this.ModalDetalle.Title = "Resumen por anticuamiento";
                    this.ModalDetalle.VisibleStatusbar = false;
                    ModalDetalle.Behaviors = WindowBehaviors.Pin | WindowBehaviors.Close | WindowBehaviors.Move;


                    //Cargar Pivot



                    DateTime fechaCorte = new DateTime(dpFechaCorte.SelectedDate.Value.Year, dpFechaCorte.SelectedDate.Value.Month, 1).AddMonths(1).AddDays(-1);
                    decimal moneda = Convert.ToDecimal(ddlMoneda.SelectedValue);

                    List<DetalleOperacionFamiliaAnticuamientoResult> ListaDetalle = new List<DetalleOperacionFamiliaAnticuamientoResult>();
                    ReporteContabilidadWCFClient servicio = new ReporteContabilidadWCFClient();
                    ListaDetalle = servicio.DetalleOperacionFamiliaAnticuamiento(IdEmpresa, CodigoUsuario, fechaCorte, "I", codigoEmpresaComparar, moneda).ToList();
                    ViewState["tmpPivot"] = JsonHelper.JsonSerializer(ListaDetalle);
                    ViewState["Operacion"] = "I";
                    this.RadPivotGrid1.DataSource = ListaDetalle;
                    this.RadPivotGrid1.DataBind();

                    scriptModal = "function f(){$find(\"" + ModalDetalle.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key1", scriptModal, true);

                }
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error", "");
            }



        }

        protected void PivotAgrupamiento_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            //ViewState["tmpPivot"]= JsonHelper.JsonSerializer(listaDetalle);
            PivotAgrupamiento.DataSource = JsonHelper.JsonDeserialize<List<DetalleOperacionFamiliaResult>>((string)ViewState["tmpPivot"]);
        }

        protected void PivotAgrupamiento_CellDataBound(object sender, PivotGridCellDataBoundEventArgs e)
        {
            if (e.Cell is PivotGridDataCell)
            {
                //Label control = (Label)e.Cell.FindControl("lblCumplimiento");
                PivotGridDataCell celda = (PivotGridDataCell)e.Cell;

                if (celda.CellType == PivotGridDataCellType.DataCell || celda.CellType == PivotGridDataCellType.RowTotalDataCell || celda.CellType == PivotGridDataCellType.RowGrandTotalDataCell)
                {
                    Label control = (Label)celda.FindControl("lblCumplimiento");
                    Image controlImagen = (Image)celda.FindControl("imgSemaforo");

                    if (control != null)
                    {

                        decimal valor = Convert.ToDecimal(celda.DataItem);
                        //decimal valor = Convert.ToDecimal(listaDatos[1]);
                        control.Text = valor.ToString("#0.0") + "%";

                        if (valor >= 90)
                        {
                            controlImagen.ImageUrl = "~/Images/Icons/circle-green-16.png";
                        }
                        else if (valor >= 75)
                        {
                            controlImagen.ImageUrl = "~/Images/Icons/circle-yellow-16.png";
                        }
                        else
                        {
                            controlImagen.ImageUrl = "~/Images/Icons/circle-red-16.png";
                        }

                    }

                    if (celda.CellType == PivotGridDataCellType.RowGrandTotalDataCell && control != null)
                    {
                        control.Text = "";
                        controlImagen.Visible = false;
                    }

                }

                //PivotGridAggregateField                
            }
        }

        protected void btnExcelPivot_Click(object sender, EventArgs e)
        {
            try
            {
                this.PivotAgrupamiento.ExportToExcel();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void PivotAgrupamiento_ItemCommand(object sender, PivotGridCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalleFam")
            {
                this.pnlAgrupamiento1.Visible = false;
                this.pnlDetalleMov.Visible = true;
                this.pnlAnticuamiento.Visible = false;
                this.pnlDetalleAntiMov.Visible = false;
                //this.lblDetalle.Text = "Enviado desde el servidor";
                DocumentoWCFClient servicio = new DocumentoWCFClient();

                List<DetalleOperacionDocumentoResult> Lista = new List<DetalleOperacionDocumentoResult>();

                int IdEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;

                string nombreEmpresa = ((Usuario_LoginResult)Session["Usuario"]).nombreComercial;
                string codigoEmpresa = JsonHelper.JsonDeserialize<string>(ViewState["codigoEmpresa"].ToString());
                gsAgenda_ListarClienteResult[] agenda;
                AgendaWCFClient ServicioAgenda = new AgendaWCFClient();
                agenda = ServicioAgenda.Agenda_ListarCliente(IdEmpresa, CodigoUsuario, codigoEmpresa, null);

                this.lblTituloDetalle.Text = "Cuentas por cobrar: Detalle de Movimientos : " + nombreEmpresa.ToUpper() + " a " + agenda[0].Nombre.ToUpper();
                //ViewState["codigoEmpresa"] = JsonHelper.JsonSerializer(codigoEmpresaComparar);
                //JsonHelper.JsonDeserialize<List<DetalleOperacionFamiliaResult>>((string)ViewState["tmpPivot"]);

                int codigoTpoDoc = Convert.ToInt32(e.CommandArgument);

                Lista = servicio.ListarDetalleDocumentos(IdEmpresa, CodigoUsuario, dpFechaCorte.SelectedDate.Value, Convert.ToChar(ViewState["Operacion"]), codigoEmpresa, codigoTpoDoc, -1).ToList();

                this.rdDetalle.DataSource = Lista;
                this.rdDetalle.DataBind();

            }
            else if (e.CommandName == "VerDetalleDoc")
            {
                this.pnlAgrupamiento1.Visible = false;
                this.pnlDetalleMov.Visible = true;
                this.pnlAnticuamiento.Visible = false;
                this.pnlDetalleAntiMov.Visible = false;
                //this.lblDetalle.Text = "Enviado desde el servidor";
                DocumentoWCFClient servicio = new DocumentoWCFClient();

                List<DetalleOperacionDocumentoResult> Lista = new List<DetalleOperacionDocumentoResult>();

                int IdEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;

                string nombreEmpresa = ((Usuario_LoginResult)Session["Usuario"]).nombreComercial;
                string codigoEmpresa = JsonHelper.JsonDeserialize<string>(ViewState["codigoEmpresa"].ToString());
                gsAgenda_ListarClienteResult[] agenda;
                AgendaWCFClient ServicioAgenda = new AgendaWCFClient();
                agenda = ServicioAgenda.Agenda_ListarCliente(IdEmpresa, CodigoUsuario, codigoEmpresa, null);

                this.lblTituloDetalle.Text = "Cuentas por cobrar: Detalle de Movimientos : " + nombreEmpresa.ToUpper() + " a " + agenda[0].Nombre.ToUpper();
                //ViewState["codigoEmpresa"] = JsonHelper.JsonSerializer(codigoEmpresaComparar);
                //JsonHelper.JsonDeserialize<List<DetalleOperacionFamiliaResult>>((string)ViewState["tmpPivot"]);

                int codigoTpoDoc = Convert.ToInt32(e.CommandArgument);

                Lista = servicio.ListarDetalleDocumentos(IdEmpresa, CodigoUsuario, dpFechaCorte.SelectedDate.Value, Convert.ToChar(ViewState["Operacion"]), codigoEmpresa, -1, codigoTpoDoc).ToList();

                this.rdDetalle.DataSource = Lista;
                this.rdDetalle.DataBind();
            }
        }

        protected void btnExcelDetalle_Click(object sender, EventArgs e)
        {
            this.rdDetalle.MasterTableView.ExportToExcel();
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            this.pnlAgrupamiento1.Visible = true;
            this.pnlDetalleMov.Visible = false;
            this.pnlAnticuamiento.Visible = false;
            this.pnlDetalleAntiMov.Visible = false;
        }

        protected void RadPivotGrid1_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            RadPivotGrid1.DataSource = JsonHelper.JsonDeserialize<List<DetalleOperacionFamiliaAnticuamientoResult>>((string)ViewState["tmpPivot"]);
        }

        protected void RadPivotGrid1_CellDataBound(object sender, PivotGridCellDataBoundEventArgs e)
        {

        }

        protected void RadPivotGrid1_ItemCommand(object sender, PivotGridCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalleFam")
            {
                this.pnlAnticuamiento.Visible = false;
                this.pnlDetalleAntiMov.Visible = true;
                this.pnlAgrupamiento1.Visible = false;
                this.pnlDetalleMov.Visible = false;
                //this.lblDetalle.Text = "Enviado desde el servidor";
                DocumentoWCFClient servicio = new DocumentoWCFClient();

                List<DetalleOperacionFamiliaAnticuamientoDocumentoResult> Lista = new List<DetalleOperacionFamiliaAnticuamientoDocumentoResult>();

                int IdEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;

                string nombreEmpresa = ((Usuario_LoginResult)Session["Usuario"]).nombreComercial;
                string codigoEmpresa = JsonHelper.JsonDeserialize<string>(ViewState["codigoEmpresa"].ToString());
                gsAgenda_ListarClienteResult[] agenda;
                AgendaWCFClient ServicioAgenda = new AgendaWCFClient();
                agenda = ServicioAgenda.Agenda_ListarCliente(IdEmpresa, CodigoUsuario, codigoEmpresa, null);
                int codigoTipo = Convert.ToInt32(e.CommandArgument);

                if (ViewState["Operacion"].ToString() == "I")
                {
                    this.Label5.Text = "Cuentas por cobrar: Detalle de Movimientos : " + nombreEmpresa.ToUpper() + " a " + agenda[0].Nombre.ToUpper();
                }
                else
                {
                    this.Label5.Text = "Cuentas por pagar: Detalle de Movimientos : " + nombreEmpresa.ToUpper() + " a " + agenda[0].Nombre.ToUpper();
                }



                Lista = servicio.DetalleOperacionFamiliaAnticuamientoDocumento(IdEmpresa, CodigoUsuario, dpFechaCorte.SelectedDate.Value, Convert.ToChar(ViewState["Operacion"]), codigoEmpresa, codigoTipo, -1).ToList();

                this.RadGrid1.DataSource = Lista;
                this.RadGrid1.DataBind();

            }
            else if (e.CommandName == "VerDetalleDoc")
            {
                this.pnlAnticuamiento.Visible = false;
                this.pnlDetalleAntiMov.Visible = true;
                this.pnlAgrupamiento1.Visible = false;
                this.pnlDetalleMov.Visible = false;
                //this.lblDetalle.Text = "Enviado desde el servidor";
                DocumentoWCFClient servicio = new DocumentoWCFClient();

                List<DetalleOperacionFamiliaAnticuamientoDocumentoResult> Lista = new List<DetalleOperacionFamiliaAnticuamientoDocumentoResult>();

                int IdEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;

                string nombreEmpresa = ((Usuario_LoginResult)Session["Usuario"]).nombreComercial;
                string codigoEmpresa = JsonHelper.JsonDeserialize<string>(ViewState["codigoEmpresa"].ToString());
                gsAgenda_ListarClienteResult[] agenda;
                AgendaWCFClient ServicioAgenda = new AgendaWCFClient();
                agenda = ServicioAgenda.Agenda_ListarCliente(IdEmpresa, CodigoUsuario, codigoEmpresa, null);
                int codigoTipo = Convert.ToInt32(e.CommandArgument);
                if (ViewState["Operacion"].ToString() == "I")
                {
                    this.Label5.Text = "Cuentas por cobrar: Detalle de Movimientos : " + nombreEmpresa.ToUpper() + " a " + agenda[0].Nombre.ToUpper();
                }
                else
                {
                    this.Label5.Text = "Cuentas por pagar: Detalle de Movimientos : " + nombreEmpresa.ToUpper() + " a " + agenda[0].Nombre.ToUpper();
                }

                Lista = servicio.DetalleOperacionFamiliaAnticuamientoDocumento(IdEmpresa, CodigoUsuario, dpFechaCorte.SelectedDate.Value, Convert.ToChar(ViewState["Operacion"]), codigoEmpresa, -1, codigoTipo).ToList();

                this.RadGrid1.DataSource = Lista;
                this.RadGrid1.DataBind();
            }
        }

        protected void btnExportar1_Click(object sender, EventArgs e)
        {
            try
            {
                //PivotAgrupamiento.DataSource = JsonHelper.JsonDeserialize<List<DetalleOperacionFamiliaResult>>((string)ViewState["tmpPivot"]);
                List<DetalleOperacionFamiliaResult> lista = JsonHelper.JsonDeserialize<List<DetalleOperacionFamiliaResult>>((string)ViewState["tmpPivot"]);

                if (lista.Count() > 0)
                {
                    FileInfo nombreArchivo = new FileInfo("ResumenAgrupamiento.xlsx");
                    var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    var fileStream = new MemoryStream();
                    using (var package = new ExcelPackage(nombreArchivo))
                    {
                        var ws = package.Workbook.Worksheets.Add("Resumen Afiliadas Agrup");

                        //Titulos
                        ws.Cells["A1"].Value = "Familia";
                        ws.Cells["B1"].Value = "Documento";
                        ws.Cells["C1"].Value = "Deuda al inicio";
                        ws.Cells["D1"].Value = "Pendiente";
                        ws.Cells["E1"].Value = "Cargos";
                        ws.Cells["F1"].Value = "Abonos";
                        ws.Cells["G1"].Value = "Cumplimiento";

                        int i = 2;

                        foreach (DetalleOperacionFamiliaResult item in lista) {
                            ws.Cells[i, 1].Value = item.Familia.Split(new string[] { "|" }, StringSplitOptions.None)[1];
                            ws.Cells[i, 2].Value = item.Documento.Split(new string[] { "|" }, StringSplitOptions.None)[1];
                            ws.Cells[i, 3].Value = item.MontoInicio;
                            ws.Cells[i, 4].Value = item.MontoAlCorte;
                            ws.Cells[i, 5].Value = item.Cargos;
                            ws.Cells[i, 6].Value = item.Abonos;
                            ws.Cells[i, 7].Value = item.Cumplimiento;

                            i++;
                        }

                        using (var range = ws.Cells[1, 1, 1, 7])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(0, 201, 74));
                            range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        }

                        ws.Cells.AutoFitColumns();
                        fileStream = new MemoryStream(package.GetAsByteArray());
                    }

                    Response.ContentType = contentType;
                    Response.AddHeader("content-disposition", "attachment; filename=" + nombreArchivo.Name);
                    Response.BinaryWrite(fileStream.ToArray());
                    Response.End();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        protected void ddlFamilias_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            DocumentoWCFClient servicio = new DocumentoWCFClient();
            List<DetalleOperacionDocumentoResult> Lista = new List<DetalleOperacionDocumentoResult>();
            int IdEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
            int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
            string codigoEmpresa = JsonHelper.JsonDeserialize<string>(ViewState["codigoEmpresa"].ToString());
            int codigoFamilia = Convert.ToInt32(this.ddlFamilias.SelectedValue);
            Lista = servicio.ListarDetalleDocumentos(IdEmpresa, CodigoUsuario, dpFechaCorte.SelectedDate.Value, Convert.ToChar(ViewState["Operacion"]), codigoEmpresa, codigoFamilia, -1).ToList();
            this.rdDetalle.DataSource = Lista;
            this.rdDetalle.DataBind();
        }

        protected void RadDropDownList1_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            DocumentoWCFClient servicio = new DocumentoWCFClient();
            List<DetalleOperacionFamiliaAnticuamientoDocumentoResult> Lista = new List<DetalleOperacionFamiliaAnticuamientoDocumentoResult>();
            int IdEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
            int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
            string codigoEmpresa = JsonHelper.JsonDeserialize<string>(ViewState["codigoEmpresa"].ToString());
            int codigoFamilia = Convert.ToInt32(this.RadDropDownList1.SelectedValue);
            Lista = servicio.DetalleOperacionFamiliaAnticuamientoDocumento(IdEmpresa, CodigoUsuario, dpFechaCorte.SelectedDate.Value, Convert.ToChar(ViewState["Operacion"]), codigoEmpresa, codigoFamilia, -1).ToList();
            this.RadGrid1.DataSource = Lista;
            this.RadGrid1.DataBind();
        }

        protected void grdPagar_ItemCommand(object sender, GridCommandEventArgs e)
        {

            string scriptModal = "";

            try
            {
                if (e.CommandName == "Agrupamiento")
                {
                    this.pnlAgrupamiento1.Visible = true;
                    this.pnlDetalleMov.Visible = false;
                    this.pnlAnticuamiento.Visible = false;
                    this.pnlDetalleAntiMov.Visible = false;

                    int IdEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                    int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
                    string codigoEmpresaComparar = e.CommandArgument.ToString();
                    //rwmReporte.RadAlert("Ejecutado Agrupamiento", 500, 100, "Error", "");
                    string mes = ("00" + dpFechaCorte.SelectedDate.Value.Month.ToString()).Substring(("00" + dpFechaCorte.SelectedDate.Value.Month.ToString()).Length - 2);
                    this.lbltituloPopupDetalle.Text = "Resumen de afiliadas: al " + mes + "-" + dpFechaCorte.SelectedDate.Value.Year.ToString();

                    string nombreEmpresa = ((Usuario_LoginResult)Session["Usuario"]).nombreComercial;

                    ViewState["codigoEmpresa"] = JsonHelper.JsonSerializer(codigoEmpresaComparar);

                    gsAgenda_ListarClienteResult[] agenda;
                    AgendaWCFClient ServicioAgenda = new AgendaWCFClient();
                    agenda = ServicioAgenda.Agenda_ListarCliente(IdEmpresa, CodigoUsuario, codigoEmpresaComparar, null);

                    lblTituloEmpresaAEmpresa.Text = "Cuentas por pagar : " + nombreEmpresa.ToUpper() + " a " + agenda[0].Nombre.ToUpper() + " (" + ddlMoneda.SelectedItem.Text + ")";

                    this.ModalDetalle.Title = "Resumen por agrupamiento";
                    this.ModalDetalle.VisibleStatusbar = false;
                    ModalDetalle.Behaviors = WindowBehaviors.Pin | WindowBehaviors.Close | WindowBehaviors.Move;


                    //Cargar Pivot



                    DateTime fechaCorte = new DateTime(dpFechaCorte.SelectedDate.Value.Year, dpFechaCorte.SelectedDate.Value.Month, 1).AddMonths(1).AddDays(-1);
                    decimal moneda = Convert.ToDecimal(ddlMoneda.SelectedValue);

                    List<DetalleOperacionFamiliaResult> ListaDetalle = new List<DetalleOperacionFamiliaResult>();
                    ReporteContabilidadWCFClient servicio = new ReporteContabilidadWCFClient();
                    ListaDetalle = servicio.DetalleOperacionFamilia(IdEmpresa, CodigoUsuario, fechaCorte, "E", codigoEmpresaComparar, moneda).ToList();
                    ViewState["tmpPivot"] = JsonHelper.JsonSerializer(ListaDetalle);
                    ViewState["Operacion"] = "E";

                    this.PivotAgrupamiento.DataSource = ListaDetalle;
                    this.PivotAgrupamiento.DataBind();

                    scriptModal = "function f(){$find(\"" + ModalDetalle.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key1", scriptModal, true);
                }
                else if (e.CommandName == "Anticuamiento")
                {
                    //rwmReporte.RadAlert("Ejecutado anticuamiento", 500, 100, "Error", "");

                    this.pnlAgrupamiento1.Visible = false;
                    this.pnlDetalleMov.Visible = false;
                    this.pnlAnticuamiento.Visible = true;
                    this.pnlDetalleAntiMov.Visible = false;

                    int IdEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                    int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
                    string codigoEmpresaComparar = e.CommandArgument.ToString();
                    //rwmReporte.RadAlert("Ejecutado Agrupamiento", 500, 100, "Error", "");
                    string mes = ("00" + dpFechaCorte.SelectedDate.Value.Month.ToString()).Substring(("00" + dpFechaCorte.SelectedDate.Value.Month.ToString()).Length - 2);
                    this.Label3.Text = "Resumen de afiliadas: al " + mes + "-" + dpFechaCorte.SelectedDate.Value.Year.ToString();

                    string nombreEmpresa = ((Usuario_LoginResult)Session["Usuario"]).nombreComercial;

                    ViewState["codigoEmpresa"] = JsonHelper.JsonSerializer(codigoEmpresaComparar);

                    gsAgenda_ListarClienteResult[] agenda;
                    AgendaWCFClient ServicioAgenda = new AgendaWCFClient();
                    agenda = ServicioAgenda.Agenda_ListarCliente(IdEmpresa, CodigoUsuario, codigoEmpresaComparar, null);

                    Label4.Text = "Cuentas por pagar : " + nombreEmpresa.ToUpper() + " a " + agenda[0].Nombre.ToUpper() +" (" + ddlMoneda.SelectedItem.Text + ")";

                    this.ModalDetalle.Title = "Resumen por anticuamiento";
                    this.ModalDetalle.VisibleStatusbar = false;
                    ModalDetalle.Behaviors = WindowBehaviors.Pin | WindowBehaviors.Close | WindowBehaviors.Move;


                    //Cargar Pivot



                    DateTime fechaCorte = new DateTime(dpFechaCorte.SelectedDate.Value.Year, dpFechaCorte.SelectedDate.Value.Month, 1).AddMonths(1).AddDays(-1);
                    decimal moneda = Convert.ToDecimal(ddlMoneda.SelectedValue);

                    List<DetalleOperacionFamiliaAnticuamientoResult> ListaDetalle = new List<DetalleOperacionFamiliaAnticuamientoResult>();
                    ReporteContabilidadWCFClient servicio = new ReporteContabilidadWCFClient();
                    ListaDetalle = servicio.DetalleOperacionFamiliaAnticuamiento(IdEmpresa, CodigoUsuario, fechaCorte, "E", codigoEmpresaComparar, moneda).ToList();
                    ViewState["tmpPivot"] = JsonHelper.JsonSerializer(ListaDetalle);
                    ViewState["Operacion"] = "E";

                    this.RadPivotGrid1.DataSource = ListaDetalle;
                    this.RadPivotGrid1.DataBind();

                    scriptModal = "function f(){$find(\"" + ModalDetalle.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key1", scriptModal, true);

                }
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error", "");
            }

        }



        protected void RadButton2_Click(object sender, EventArgs e)
        {

        }


        protected void btnDetalleAnti_Click(object sender, EventArgs e)
        {
            this.RadGrid1.MasterTableView.ExportToExcel();
        }


        protected void btnRegresarDetAnti_Click(object sender, EventArgs e)
        {
            this.pnlAgrupamiento1.Visible = false;
            this.pnlDetalleMov.Visible = false;
            this.pnlAnticuamiento.Visible = true;
            this.pnlDetalleAntiMov.Visible = false;
        }

        protected void btnEXportAnti_Click(object sender, EventArgs e)
        {
            try
            {
                //PivotAgrupamiento.DataSource = JsonHelper.JsonDeserialize<List<DetalleOperacionFamiliaResult>>((string)ViewState["tmpPivot"]);
                List<DetalleOperacionFamiliaAnticuamientoResult> lista = JsonHelper.JsonDeserialize<List<DetalleOperacionFamiliaAnticuamientoResult>>((string)ViewState["tmpPivot"]);

                if (lista.Count() > 0)
                {
                    FileInfo nombreArchivo = new FileInfo("ResumenAnticuamiento.xlsx");
                    var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    var fileStream = new MemoryStream();
                    using (var package = new ExcelPackage(nombreArchivo))
                    {
                        var ws = package.Workbook.Worksheets.Add("Resumen Afiliadas Antic");

                        //Titulos
                        ws.Cells["A1"].Value = "Familia";
                        ws.Cells["B1"].Value = "Documento";
                        ws.Cells["C1"].Value = "Por vencer 1 a 30";
                        ws.Cells["D1"].Value = "Por vencer 31 a 60";
                        ws.Cells["E1"].Value = "Por vencer 61 a 90";
                        ws.Cells["F1"].Value = "Por vencer 91 a 120";
                        ws.Cells["G1"].Value = "Vencido";

                        int i = 2;

                        foreach (DetalleOperacionFamiliaAnticuamientoResult item in lista)
                        {
                            ws.Cells[i, 1].Value = item.Familia.Split(new string[] { "|" }, StringSplitOptions.None)[1];
                            ws.Cells[i, 2].Value = item.Documento.Split(new string[] { "|" }, StringSplitOptions.None)[1];
                            ws.Cells[i, 3].Value = item.Vence1a30;
                            ws.Cells[i, 4].Value = item.Vence31a60;
                            ws.Cells[i, 5].Value = item.Vence61a90;
                            ws.Cells[i, 6].Value = item.Vence91a120;
                            ws.Cells[i, 7].Value = item.Vencido;

                            i++;
                        }

                        using (var range = ws.Cells[1, 1, 1, 7])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(0, 201, 74));
                            range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        }

                        ws.Cells.AutoFitColumns();
                        fileStream = new MemoryStream(package.GetAsByteArray());
                    }

                    Response.ContentType = contentType;
                    Response.AddHeader("content-disposition", "attachment; filename=" + nombreArchivo.Name);
                    Response.BinaryWrite(fileStream.ToArray());
                    Response.End();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}