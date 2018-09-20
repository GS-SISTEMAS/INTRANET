using GS.SISGEGS.DM;
using GS.SISGEGS.Web.AgendaWCF;
//using GS.SISGEGS.Web.ItemWCF;
using GS.SISGEGS.Web.EstadoCuentaWCF;
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
using System.Drawing;
//using xi = Telerik.Web.UI.ExportInfrastructure;
//using Telerik.Web.UI.GridExcelBuilder;
using System.IO;
//using System.Data.SqlClient;
//using System.Configuration;
//using System.Text;
//using System.Web.UI.HtmlControls;
using System.Data.OleDb;
//using System.Windows;
//using Telerik.Windows.Documents.FormatProviders;
//using Telerik.Windows.Documents;
//using System.Collections;
//using System.Web.Security;
//using System.Web.UI.WebControls.WebParts;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
//using System.Diagnostics;
//using System.Net;
using GS.SISGEGS.Web.LoginWCF;
//using ICSharpCode.SharpZipLib;
//using ICSharpCode;
//using ICSharpCode.SharpZipLib.Zip;

namespace GS.SISGEGS.Web.Finanzas.EstadoCuenta
{
    public partial class frmEstadoCuenta : System.Web.UI.Page
    {
        protected PdfTemplate total;
        protected BaseFont helv;
        private bool settingFont = false;
        iTextSharp.text.Image oImagen;
        PdfContentByte cbPie;
        PdfContentByte cbEncabezado;

        private List<gsReporte_DocumentosPendientesResult> ListarEstadoCuenta(string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos, int id_zona)
        {
            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();

            try
            {
                List<gsReporte_DocumentosPendientesResult> lstEstadoCuenta = new List<gsReporte_DocumentosPendientesResult>();

                if (codVendedor != null)
                {
                    if (codVendedor.Length > 3)
                    {
                        if (codVendedor == "666666")
                        {
                            codVendedor = null;

                            List<gsReporte_DocumentosPendientesResult> lst = objEstadoCuentaWCF.EstadoCuenta_ListarxCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos, id_zona).OrderBy(e => e.ClienteNombre).OrderBy(e => e.FechaVencimiento).ToList();

                            var query_Estado = from c in lst
                                               where c.EstadoCliente == "LEGAL"
                                               orderby c.Fecha, c.ZonaCobranza ascending
                                               select new
                                               {
                                                   c.Banco,
                                                   c.ClienteNombre,
                                                   c.DeudaDolares,
                                                   c.DeudaSoles,
                                                   c.DiasMora,
                                                   c.EstadoCliente,
                                                   c.EstadoDoc,
                                                   c.FAceptada,
                                                   c.Fecha,
                                                   c.FechaVencimiento,
                                                   c.ID_Agenda,
                                                   c.ID_Doc,
                                                   c.ID_EstadoDoc,
                                                   c.ID_Moneda,
                                                   c.Id_TipoDoc,
                                                   c.ID_Vendedor,
                                                   c.ID_Zona,
                                                   c.Importe,
                                                   c.ImporteFinanciado,
                                                   c.ImportePagado,
                                                   c.ImportePendiente,
                                                   c.ImportePendiente_01a08,
                                                   c.ImportePendiente_01a30,
                                                   c.ImportePendiente_09a30,
                                                   c.ImportePendiente_121a360,
                                                   c.ImportePendiente_121aMas,
                                                   c.ImportePendiente_31a60,
                                                   c.ImportePendiente_361a720,
                                                   c.ImportePendiente_361aMas,
                                                   c.ImportePendiente_61a120,
                                                   c.ImportePendiente_61a90,
                                                   c.ImportePendiente_721aMas,
                                                   c.ImportePendiente_91a120,
                                                   c.ImportePendiente_NoVencido,
                                                   c.ImportePendiente_PorVencer30,
                                                   c.ImportePendiente_VenceHoy,
                                                   c.Moneda,
                                                   c.monedasigno,
                                                   c.No_Vendedor,
                                                   c.NroDocumento,
                                                   c.Numero,
                                                   c.NumeroUnico,
                                                   c.Origen,
                                                   c.OrigenOp,
                                                   c.Referencia,
                                                   c.Sede,
                                                   c.Serie,
                                                   c.Situacion,
                                                   c.TC,
                                                   c.TipoDocumento,
                                                   c.ZonaCobranza
                                                   ,c.ID_Financiamiento
                                               };

                            foreach (var QEstado in query_Estado.ToList())
                            {
                                gsReporte_DocumentosPendientesResult rowEstado = new gsReporte_DocumentosPendientesResult();
                                rowEstado.Banco = QEstado.Banco;
                                rowEstado.ClienteNombre = QEstado.ClienteNombre;
                                rowEstado.DeudaDolares = QEstado.DeudaDolares;
                                rowEstado.DeudaSoles = QEstado.DeudaSoles;
                                rowEstado.DiasMora = QEstado.DiasMora;
                                rowEstado.EstadoCliente = QEstado.EstadoCliente;
                                rowEstado.EstadoDoc = QEstado.EstadoDoc;
                                rowEstado.FAceptada = QEstado.FAceptada;
                                rowEstado.Fecha = QEstado.Fecha;
                                rowEstado.FechaVencimiento = QEstado.FechaVencimiento;
                                rowEstado.ID_Agenda = QEstado.ID_Agenda;
                                rowEstado.ID_Doc = QEstado.ID_Doc;
                                rowEstado.ID_EstadoDoc = QEstado.ID_EstadoDoc;
                                rowEstado.ID_Moneda = QEstado.ID_Moneda;
                                rowEstado.Id_TipoDoc = QEstado.Id_TipoDoc;
                                rowEstado.ID_Vendedor = QEstado.ID_Vendedor;
                                rowEstado.ID_Zona = QEstado.ID_Zona;
                                rowEstado.Importe = QEstado.Importe;
                                rowEstado.ImporteFinanciado = QEstado.ImporteFinanciado;
                                rowEstado.ImportePagado = QEstado.ImportePagado;
                                rowEstado.ImportePendiente = QEstado.ImportePendiente;
                                rowEstado.ImportePendiente_01a08 = QEstado.ImportePendiente_01a08;
                                rowEstado.ImportePendiente_01a30 = QEstado.ImportePendiente_01a30;
                                rowEstado.ImportePendiente_09a30 = QEstado.ImportePendiente_09a30;
                                rowEstado.ImportePendiente_121a360 = QEstado.ImportePendiente_121a360;
                                rowEstado.ImportePendiente_121aMas = QEstado.ImportePendiente_121aMas;
                                rowEstado.ImportePendiente_31a60 = QEstado.ImportePendiente_31a60;
                                rowEstado.ImportePendiente_361a720 = QEstado.ImportePendiente_361a720;
                                rowEstado.ImportePendiente_361aMas = QEstado.ImportePendiente_361aMas;
                                rowEstado.ImportePendiente_61a120 = QEstado.ImportePendiente_61a120;
                                rowEstado.ImportePendiente_61a90 = QEstado.ImportePendiente_61a90;
                                rowEstado.ImportePendiente_721aMas = QEstado.ImportePendiente_721aMas;

                                rowEstado.ImportePendiente_91a120 = QEstado.ImportePendiente_91a120;
                                rowEstado.ImportePendiente_NoVencido = QEstado.ImportePendiente_NoVencido;
                                rowEstado.ImportePendiente_PorVencer30 = QEstado.ImportePendiente_PorVencer30;
                                rowEstado.ImportePendiente_VenceHoy = QEstado.ImportePendiente_VenceHoy;
                                rowEstado.Moneda = QEstado.Moneda;
                                rowEstado.monedasigno = QEstado.monedasigno;
                                rowEstado.No_Vendedor = QEstado.No_Vendedor;
                                rowEstado.NroDocumento = QEstado.NroDocumento;
                                rowEstado.Numero = QEstado.Numero;
                                rowEstado.NumeroUnico = QEstado.NumeroUnico;
                                rowEstado.Origen = QEstado.Origen;
                                rowEstado.OrigenOp = QEstado.OrigenOp;
                                rowEstado.Referencia = QEstado.Referencia;
                                rowEstado.Sede = QEstado.Sede;

                                rowEstado.Serie = QEstado.Serie;
                                rowEstado.Situacion = QEstado.Situacion;
                                rowEstado.TC = QEstado.TC;
                                rowEstado.TipoDocumento = QEstado.TipoDocumento;
                                rowEstado.ZonaCobranza = QEstado.ZonaCobranza;
                                rowEstado.ID_Financiamiento = QEstado.ID_Financiamiento;

                                lstEstadoCuenta.Add(rowEstado);
                            }
                        }
                        else
                        {
                            List<gsReporte_DocumentosPendientesResult> lst = objEstadoCuentaWCF.EstadoCuenta_ListarxCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos, id_zona).OrderBy(e => e.ClienteNombre).OrderBy(e => e.FechaVencimiento).ToList();

                            var query_Estado = from c in lst
                                               where c.EstadoCliente != "LEGAL"
                                               orderby c.Fecha, c.ZonaCobranza ascending
                                               select new
                                               {
                                                   c.Banco,
                                                   c.ClienteNombre,
                                                   c.DeudaDolares,
                                                   c.DeudaSoles,
                                                   c.DiasMora,
                                                   c.EstadoCliente,
                                                   c.EstadoDoc,
                                                   c.FAceptada,
                                                   c.Fecha,
                                                   c.FechaVencimiento,
                                                   c.ID_Agenda,
                                                   c.ID_Doc,
                                                   c.ID_EstadoDoc,
                                                   c.ID_Moneda,
                                                   c.Id_TipoDoc,
                                                   c.ID_Vendedor,
                                                   c.ID_Zona,
                                                   c.Importe,
                                                   c.ImporteFinanciado,
                                                   c.ImportePagado,
                                                   c.ImportePendiente,
                                                   c.ImportePendiente_01a08,
                                                   c.ImportePendiente_01a30,
                                                   c.ImportePendiente_09a30,
                                                   c.ImportePendiente_121a360,
                                                   c.ImportePendiente_121aMas,
                                                   c.ImportePendiente_31a60,
                                                   c.ImportePendiente_361a720,
                                                   c.ImportePendiente_361aMas,
                                                   c.ImportePendiente_61a120,
                                                   c.ImportePendiente_61a90,
                                                   c.ImportePendiente_721aMas,
                                                   c.ImportePendiente_91a120,
                                                   c.ImportePendiente_NoVencido,
                                                   c.ImportePendiente_PorVencer30,
                                                   c.ImportePendiente_VenceHoy,
                                                   c.Moneda,
                                                   c.monedasigno,
                                                   c.No_Vendedor,
                                                   c.NroDocumento,
                                                   c.Numero,
                                                   c.NumeroUnico,
                                                   c.Origen,
                                                   c.OrigenOp,
                                                   c.Referencia,
                                                   c.Sede,
                                                   c.Serie,
                                                   c.Situacion,
                                                   c.TC,
                                                   c.TipoDocumento,
                                                   c.ZonaCobranza
                                                   ,c.ID_Financiamiento
                                               };

                            foreach (var QEstado in query_Estado.ToList())
                            {
                                gsReporte_DocumentosPendientesResult rowEstado = new gsReporte_DocumentosPendientesResult();
                                rowEstado.Banco = QEstado.Banco;
                                rowEstado.ClienteNombre = QEstado.ClienteNombre;
                                rowEstado.DeudaDolares = QEstado.DeudaDolares;
                                rowEstado.DeudaSoles = QEstado.DeudaSoles;
                                rowEstado.DiasMora = QEstado.DiasMora;
                                rowEstado.EstadoCliente = QEstado.EstadoCliente;
                                rowEstado.EstadoDoc = QEstado.EstadoDoc;
                                rowEstado.FAceptada = QEstado.FAceptada;
                                rowEstado.Fecha = QEstado.Fecha;
                                rowEstado.FechaVencimiento = QEstado.FechaVencimiento;
                                rowEstado.ID_Agenda = QEstado.ID_Agenda;
                                rowEstado.ID_Doc = QEstado.ID_Doc;
                                rowEstado.ID_EstadoDoc = QEstado.ID_EstadoDoc;
                                rowEstado.ID_Moneda = QEstado.ID_Moneda;
                                rowEstado.Id_TipoDoc = QEstado.Id_TipoDoc;
                                rowEstado.ID_Vendedor = QEstado.ID_Vendedor;
                                rowEstado.ID_Zona = QEstado.ID_Zona;
                                rowEstado.Importe = QEstado.Importe;
                                rowEstado.ImporteFinanciado = QEstado.ImporteFinanciado;
                                rowEstado.ImportePagado = QEstado.ImportePagado;
                                rowEstado.ImportePendiente = QEstado.ImportePendiente;
                                rowEstado.ImportePendiente_01a08 = QEstado.ImportePendiente_01a08;
                                rowEstado.ImportePendiente_01a30 = QEstado.ImportePendiente_01a30;
                                rowEstado.ImportePendiente_09a30 = QEstado.ImportePendiente_09a30;
                                rowEstado.ImportePendiente_121a360 = QEstado.ImportePendiente_121a360;
                                rowEstado.ImportePendiente_121aMas = QEstado.ImportePendiente_121aMas;
                                rowEstado.ImportePendiente_31a60 = QEstado.ImportePendiente_31a60;
                                rowEstado.ImportePendiente_361a720 = QEstado.ImportePendiente_361a720;
                                rowEstado.ImportePendiente_361aMas = QEstado.ImportePendiente_361aMas;
                                rowEstado.ImportePendiente_61a120 = QEstado.ImportePendiente_61a120;
                                rowEstado.ImportePendiente_61a90 = QEstado.ImportePendiente_61a90;
                                rowEstado.ImportePendiente_721aMas = QEstado.ImportePendiente_721aMas;

                                rowEstado.ImportePendiente_91a120 = QEstado.ImportePendiente_91a120;
                                rowEstado.ImportePendiente_NoVencido = QEstado.ImportePendiente_NoVencido;
                                rowEstado.ImportePendiente_PorVencer30 = QEstado.ImportePendiente_PorVencer30;
                                rowEstado.ImportePendiente_VenceHoy = QEstado.ImportePendiente_VenceHoy;
                                rowEstado.Moneda = QEstado.Moneda;
                                rowEstado.monedasigno = QEstado.monedasigno;
                                rowEstado.No_Vendedor = QEstado.No_Vendedor;
                                rowEstado.NroDocumento = QEstado.NroDocumento;
                                rowEstado.Numero = QEstado.Numero;
                                rowEstado.NumeroUnico = QEstado.NumeroUnico;
                                rowEstado.Origen = QEstado.Origen;
                                rowEstado.OrigenOp = QEstado.OrigenOp;
                                rowEstado.Referencia = QEstado.Referencia;
                                rowEstado.Sede = QEstado.Sede;

                                rowEstado.Serie = QEstado.Serie;
                                rowEstado.Situacion = QEstado.Situacion;
                                rowEstado.TC = QEstado.TC;
                                rowEstado.TipoDocumento = QEstado.TipoDocumento;
                                rowEstado.ZonaCobranza = QEstado.ZonaCobranza;
                                rowEstado.ID_Financiamiento = QEstado.ID_Financiamiento;

                                lstEstadoCuenta.Add(rowEstado);
                            }
                        }
                    }

                }
                else
                {
                    List<gsReporte_DocumentosPendientesResult> lst = objEstadoCuentaWCF.EstadoCuenta_ListarxCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos, id_zona).OrderBy(e => e.ClienteNombre).OrderBy(e => e.FechaVencimiento).ToList();
                    lstEstadoCuenta = lst;
                }

                ViewState["lstEstadoCuenta"] = JsonHelper.JsonSerializer(lstEstadoCuenta);
                grdEstadoCuenta.DataSource = lstEstadoCuenta;
                grdEstadoCuenta.DataBind();
                lblDateDetalle1.Text = "1";
                return lstEstadoCuenta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<gsReporte_DocumentosPendientesResumenClienteResult> ListarEstadoCuentaResumenCliente(string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {

            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();
            try
            {
                List<gsReporte_DocumentosPendientesResumenClienteResult> lstDocumentos = objEstadoCuentaWCF.EstadoCuenta_ListarResumenCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos).ToList();
                ViewState["lstEstadoCuentaResumenClienteTotal"] = JsonHelper.JsonSerializer(lstDocumentos);

                grdEstadoCuentaCliente.DataSource = lstDocumentos;
                grdEstadoCuentaCliente.DataBind();

                lblMensaje.Text = "Se han encontrado " + lstDocumentos.Count.ToString() + " registro.";
                lblMensaje.CssClass = "mensajeExito";

                lblDateResumen1.Text = "1";
                return lstDocumentos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<gsReporte_DocumentosPendientes_LegalResult> ListarEstadoCuentaLegal(string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {

            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();
            List<gsReporte_DocumentosPendientes_LegalResult> lstDocumentos = new List<gsReporte_DocumentosPendientes_LegalResult>(); 
            try
            {
                lstDocumentos = objEstadoCuentaWCF.EstadoCuenta_Listar_Legal(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos).ToList();

                ViewState["lstEstadoCuentaLegal"] = JsonHelper.JsonSerializer(lstDocumentos);

                grdEstadoCuenta_Legal.DataSource = lstDocumentos;
                grdEstadoCuenta_Legal.DataBind();

                lblMensaje.Text = "Se han encontrado " + lstDocumentos.Count.ToString() + " registro.";
                lblMensaje.CssClass = "mensajeExito";

                lblDateLegal1.Text = "1";
                
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            return lstDocumentos;
        }

        private List<gsReporte_DocumentosPendientes_ProvisionResult> ListarEstadoCuentaProvision(string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {

            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();
            List<gsReporte_DocumentosPendientes_ProvisionResult> lstDocumentos = new List<gsReporte_DocumentosPendientes_ProvisionResult>();
            try
            {
                lstDocumentos = objEstadoCuentaWCF.EstadoCuenta_Listar_Provision(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos).ToList();

                ViewState["lstEstadoCuentaProvision"] = JsonHelper.JsonSerializer(lstDocumentos);

                grdEstadoCuenta_Provision.DataSource = lstDocumentos;
                grdEstadoCuenta_Provision.DataBind();

                lblMensaje.Text = "Se han encontrado " + lstDocumentos.Count.ToString() + " registro.";
                lblMensaje.CssClass = "mensajeExito";

                lblDateProvision1.Text = "1";

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            return lstDocumentos;
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                grdEstadoCuentaCliente.ExportSettings.Excel.Format = (PivotGridExcelFormat)Enum.Parse(typeof(PivotGridExcelFormat), "Xlsx");
                grdEstadoCuentaCliente.ExportSettings.FileName = "ReporteCostoMensual_" + DateTime.Now.ToString("yyyyMMddHmm");
                grdEstadoCuentaCliente.ExportToExcel();
            }
            catch (Exception ex)
            {
                rwmVidaLey.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }
        }

        protected void btnExcelDetalle_Click(object sender, EventArgs e)
        {
            ExportarExcelDetalle();
        }

        protected void btnPDFDetalle_Click(object sender, EventArgs e)
        {
            ExportarPDF_Detalle();
        }

        protected void grdEstadoCuentaCliente_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (lblDateResumen1.Text == "1")
                {
                    grdEstadoCuentaCliente.DataSource = JsonHelper.JsonDeserialize<List<gsReporte_DocumentosPendientesResumenClienteResult>>((string)ViewState["lstEstadoCuentaResumenClienteTotal"]);
                }

            }
            catch (Exception ex)
            {
                rwmVidaLey.RadAlert(ex.Message, 500, 100, "Error al consultar el archivo", "");
            }
        }

        protected void grdEstadoCuenta_Legal_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (lblDateLegal1.Text == "1")
                {
                    grdEstadoCuenta_Legal.DataSource = JsonHelper.JsonDeserialize<List<gsReporte_DocumentosPendientes_LegalResult>>((string)ViewState["lstEstadoCuentaLegal"]);
                }

            }
            catch (Exception ex)
            {
                rwmVidaLey.RadAlert(ex.Message, 500, 100, "Error al consultar el archivo", "");
            }
        }

        protected void grdEstadoCuenta_Provision_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (lblDateProvision1.Text == "1")
                {
                    grdEstadoCuenta_Provision.DataSource = JsonHelper.JsonDeserialize<List<gsReporte_DocumentosPendientes_ProvisionResult>>((string)ViewState["lstEstadoCuentaProvision"]);
                }

            }
            catch (Exception ex)
            {
                rwmVidaLey.RadAlert(ex.Message, 500, 100, "Error al consultar el archivo", "");
            }
        }


        protected void grdEstadoCuentaCliente_PivotGridCellExporting(object sender, PivotGridCellExportingArgs e)
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

        protected void grdEstadoCuenta_Legal_PivotGridCellExporting(object sender, PivotGridCellExportingArgs e)
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

        protected void grdEstadoCuenta_Provision_PivotGridCellExporting(object sender, PivotGridCellExportingArgs e)
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

        DataTable FormatoTabla()
        {
            DataTable dtTabla = new DataTable();
            dtTabla.TableName = "Agenda";
            dtTabla.Columns.Add("idEmpresa");
            dtTabla.Columns.Add("cod_usuario");
            dtTabla.Columns.Add("ID_Agenda");
            return dtTabla;

        }


        private void ListarClientesResumen(List<gsReporte_DocumentosPendientesResult> lst)
        {
            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();
            List<gsAgendaCliente_BuscarLimiteCreditoResult> lstLimiteCreditoAgenda;
            try
            {
                int contar = 0;
                int cantidad = 0;
                int cod_usuario = 0;
                int idEmpresa = 0; 

                cod_usuario = ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario;
                idEmpresa = ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa;

                lstLimiteCreditoAgenda = new List<gsAgendaCliente_BuscarLimiteCreditoResult>();
                var queryAllAgenda = from DocumentosPendientes in lst select DocumentosPendientes.ID_Agenda;
                var queryAgenda = from AgendaPendiente in queryAllAgenda.Distinct() orderby AgendaPendiente ascending select AgendaPendiente;

                cantidad = queryAgenda.ToList().Count();

                DataTable TablaAgenda = new DataTable();
                TablaAgenda = FormatoTabla(); 

                foreach (var agenda in queryAgenda)
                {
                    DataRow row = TablaAgenda.NewRow();
                    row["idEmpresa"] = idEmpresa;
                    row["cod_usuario"] = cod_usuario;
                    row["ID_Agenda"] = agenda.ToString();
                    TablaAgenda.Rows.Add(row);
                }

                objEstadoCuentaWCF.RiesgoCliente_RegistrarBulkCopy(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, TablaAgenda);

                cantidad = queryAgenda.ToList().Count();

                int Todos = 1; 
                List<gsAgendaCliente_BuscarLimiteCreditoResult> LimiteCreditoAgenda = objEstadoCuentaWCF.EstadoCuenta_LimiteCreditoxCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, null, Todos).ToList();

                ViewState["lstResumenCliente"] = JsonHelper.JsonSerializer(LimiteCreditoAgenda);
                //grdResumenCliente.DataSource = lstLimiteCreditoAgenda;
                //grdResumenCliente.DataBind();
                lblDateDetalle1.Text = "1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ListarClientesResumenVencidos()
        {
            List<gsAgendaCliente_BuscarLimiteCreditoResult> lstClienteResumen;
            List<gsAgendaCliente_BuscarLimiteCreditoResult> lstClienteResumenFinal = new List<gsAgendaCliente_BuscarLimiteCreditoResult>();
            gsAgendaCliente_BuscarLimiteCreditoResult ClienteResumenFinal;
            List<gsReporte_DocumentosPendientesResult> lstClienteDetalle;
            lstClienteResumen = ClienteResumen();
            lstClienteDetalle = ClienteDetalle();

            //DataTable dtTablaresumen = TablaLimiteCredito();
            try
            {
                foreach (gsAgendaCliente_BuscarLimiteCreditoResult resumen in lstClienteResumen)
                {
                    ClienteResumenFinal = new gsAgendaCliente_BuscarLimiteCreditoResult();
                    var query_Detalle = from c in lstClienteDetalle
                                        where c.ID_Agenda == resumen.ID_Agenda
                                        orderby c.ClienteNombre, c.FechaVencimiento
                                        select new
                                        {
                                            c.TC,
                                            c.ID_Moneda,
                                            c.ID_Agenda,


                                            Pendiente = c.ID_Moneda == 0 ? c.ImportePendiente :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente / c.TC :
                                                        c.ImportePendiente,

                                            Pendiente_PorVencer30 = c.ID_Moneda == 0 ? c.ImportePendiente_PorVencer30 :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_PorVencer30 / c.TC :
                                                        c.ImportePendiente_PorVencer30,

                                            Pendiente_NoVencido = c.ID_Moneda == 0 ? c.ImportePendiente_NoVencido :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_NoVencido / c.TC :
                                                        c.ImportePendiente_NoVencido,

                                            Pendiente_VenceHoy = c.ID_Moneda == 0 ? c.ImportePendiente_VenceHoy :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_VenceHoy / c.TC :
                                                        c.ImportePendiente_VenceHoy,
                                            Pendiente_01a30 = c.ID_Moneda == 0 ? c.ImportePendiente_01a30 :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_01a30 / c.TC :
                                                        c.ImportePendiente_01a30,
                                            Pendiente_31a60 = c.ID_Moneda == 0 ? c.ImportePendiente_31a60 :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_31a60 / c.TC :
                                                        c.ImportePendiente_31a60,
                                            Pendiente_61a120 = c.ID_Moneda == 0 ? c.ImportePendiente_61a120 :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_61a120 / c.TC :
                                                        c.ImportePendiente_61a120,
                                            Pendiente_121a360 = c.ID_Moneda == 0 ? c.ImportePendiente_121a360 :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_121a360 / c.TC :
                                                        c.ImportePendiente_121a360,
                                            Pendiente_361aMas = c.ID_Moneda == 0 ? c.ImportePendiente_361aMas :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_361aMas / c.TC :
                                                        c.ImportePendiente_361aMas
                                        };

                    var sumImportePendiente = query_Detalle.ToList().Select(c => c.Pendiente).Sum();
                    var sumImportePendiente_NoVencido = query_Detalle.ToList().Select(c => c.Pendiente_NoVencido).Sum();
                    var sumImportePendiente_VenceHoy = query_Detalle.ToList().Select(c => c.Pendiente_VenceHoy).Sum();

                    var sumImporte_PorVencer30 = query_Detalle.ToList().Select(c => c.Pendiente_PorVencer30).Sum();

                    var sumImportePendiente_01a30 = query_Detalle.ToList().Select(c => c.Pendiente_01a30).Sum();
                    var sumImportePendiente_31a60 = query_Detalle.ToList().Select(c => c.Pendiente_31a60).Sum();
                    var sumImportePendiente_61a120 = query_Detalle.ToList().Select(c => c.Pendiente_61a120).Sum();
                    var sumImportePendiente_121a360 = query_Detalle.ToList().Select(c => c.Pendiente_121a360).Sum();
                    var sumImportePendiente_361aMas = query_Detalle.ToList().Select(c => c.Pendiente_361aMas).Sum();

                    decimal NoVencido;
                    decimal DeudaVencida;
                    decimal CreditoDisponible;
                    NoVencido = Convert.ToDecimal(sumImportePendiente_NoVencido) + Convert.ToDecimal(sumImportePendiente_VenceHoy);
                    DeudaVencida = Convert.ToDecimal(sumImportePendiente_01a30) + Convert.ToDecimal(sumImportePendiente_31a60) + Convert.ToDecimal(sumImportePendiente_61a120) + Convert.ToDecimal(sumImportePendiente_121a360) + Convert.ToDecimal(sumImportePendiente_361aMas);
                    CreditoDisponible = Convert.ToDecimal(resumen.LineaCredito) - Convert.ToDecimal(sumImportePendiente);

                    string strsumNoVencido = string.Format("{0:#,##0.00}", NoVencido);

                    string strsumImporte_PorVencer30 = string.Format("{0:#,##0.00}", sumImporte_PorVencer30);

                    string strsumImportePendiente_01a30 = string.Format("{0:#,##0.00}", sumImportePendiente_01a30);
                    string strsumImportePendiente_31a60 = string.Format("{0:#,##0.00}", sumImportePendiente_31a60);
                    string strsumImportePendiente_61a120 = string.Format("{0:#,##0.00}", sumImportePendiente_61a120);
                    string strsumImportePendiente_121a360 = string.Format("{0:#,##0.00}", sumImportePendiente_121a360);
                    string strsumImportePendiente_361aMas = string.Format("{0:#,##0.00}", sumImportePendiente_361aMas);
                    string strsumImportePendiente = string.Format("{0:#,##0.00}", sumImportePendiente);
                    string strsumDeudaVencida = string.Format("{0:#,##0.00}", DeudaVencida);
                   
                    //string strTotalCredito = string.Format("{0:$ #,##0.00}", ClienteResumen.TotalCredito);
                    string strCreditoDisponible = string.Format("{0:#,##0.00}", CreditoDisponible);


                    ClienteResumenFinal.ID_Agenda = resumen.ID_Agenda;
                    ClienteResumenFinal.AgendaNombre = resumen.AgendaNombre;

                    ClienteResumenFinal.Aprobacion = resumen.Aprobacion;
                    ClienteResumenFinal.AprobadoDes = resumen.AprobadoDes;

                    ClienteResumenFinal.DiasCredito = resumen.DiasCredito;
                    ClienteResumenFinal.BloqueoLineaCredito = resumen.BloqueoLineaCredito;

                    ClienteResumenFinal.Estado = resumen.Estado;
                    ClienteResumenFinal.EstadoDes = resumen.EstadoDes;
                    ClienteResumenFinal.FechaVCMTLinea = resumen.FechaVCMTLinea;

                    ClienteResumenFinal.NoVencido = Convert.ToDecimal(strsumNoVencido);
                    ClienteResumenFinal.PorVencer30 = Convert.ToDecimal(strsumImporte_PorVencer30);

                    ClienteResumenFinal.Vencido01a30 = Convert.ToDecimal(strsumImportePendiente_01a30);
                    ClienteResumenFinal.Vencido31a60 = Convert.ToDecimal(strsumImportePendiente_31a60);
                    ClienteResumenFinal.Vencido61a120 = Convert.ToDecimal(strsumImportePendiente_61a120);
                    ClienteResumenFinal.Vencido121a360 = Convert.ToDecimal(strsumImportePendiente_121a360);
                    ClienteResumenFinal.Vencido361amas = Convert.ToDecimal(strsumImportePendiente_361aMas);

                   

                    ClienteResumenFinal.DeudaVencida = Convert.ToDecimal(strsumDeudaVencida);

                    string strsumDeudaTotal = string.Format("{0:#,##0.00}", resumen.TotalRiesgo);
                    //ClienteResumenFinal.DeudaTotal = Convert.ToDecimal(strsumImportePendiente);
                    ClienteResumenFinal.DeudaTotal = Convert.ToDecimal(strsumDeudaTotal);

                    string strLineaCredito = string.Format("{0:#,##0.00}", resumen.LineaCredito);
                    ClienteResumenFinal.LineaCredito = Convert.ToDecimal(strLineaCredito);

                    strCreditoDisponible = string.Format("{0:#,##0.00}", resumen.CreditoDisponible);
                    ClienteResumenFinal.CreditoDisponible = Convert.ToDecimal(strCreditoDisponible);

                    ClienteResumenFinal.XFacturar = resumen.XFacturar; 
                    lstClienteResumenFinal.Add(ClienteResumenFinal);

                }

                ViewState["lstResumenCliente"] = JsonHelper.JsonSerializer(lstClienteResumenFinal);
                grdResumenCliente.DataSource = lstClienteResumenFinal;
                grdResumenCliente.DataBind();

                lblDateDetalle1.Text = "1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable TablaLimiteCredito()
        {
            DataTable dttabla = new DataTable();
            try
            {
                dttabla.Columns.Add("id_agenda", typeof(string));
                dttabla.Columns.Add("AgendaNombre", typeof(string));

                dttabla.Columns.Add("NoVencido", typeof(string));

                dttabla.Columns.Add("Vencido01a30", typeof(string));
                dttabla.Columns.Add("Vencido31a60", typeof(string));
                dttabla.Columns.Add("Vencido61a120", typeof(string));
                dttabla.Columns.Add("Vencido121a360", typeof(string));
                dttabla.Columns.Add("Vencido361amas", typeof(string));
                dttabla.Columns.Add("DeudaVencida", typeof(string));

                dttabla.Columns.Add("DeudaTotal", typeof(string));

                dttabla.Columns.Add("LineaCredito", typeof(string));
                dttabla.Columns.Add("CreditoDisponible", typeof(string));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            return dttabla;
        }

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

                    dpFinalEmision.SelectedDate = DateTime.Now;
                    dpFechaHastaCliente.SelectedDate = DateTime.Now;
                    dpFechaLegal.SelectedDate = DateTime.Now;
                    dpFechaProvision.SelectedDate = DateTime.Now; 
                    lblDateDetalle1.Text = "";
                    Zona_Cargar(0);


                    //btnBuscarResumenCliente.Click += new EventHandler(btnBuscarResumenCliente_Click);
                    //Reporte_CargarResumen();

                    List<gsReporte_DocumentosPendientesResumenClienteResult> lstDocumentos = new List<gsReporte_DocumentosPendientesResumenClienteResult>();
                    ViewState["lstEstadoCuentaResumenClienteTotal"] = JsonHelper.JsonSerializer(lstDocumentos);
                    lblDateResumen1.Text = "1";

                    List<gsReporte_DocumentosPendientes_LegalResult> lstDocumentosLegal = new List<gsReporte_DocumentosPendientes_LegalResult>();
                    ViewState["lstEstadoCuentaLegal"] = JsonHelper.JsonSerializer(lstDocumentosLegal);
                    lblDateLegal1.Text = "1";

                    List<gsReporte_DocumentosPendientes_ProvisionResult> lstDocumentosProvision = new List<gsReporte_DocumentosPendientes_ProvisionResult>();
                    ViewState["lstEstadoCuentaProvision"] = JsonHelper.JsonSerializer(lstDocumentosProvision);
                    lblDateProvision1.Text = "1";


                }
            }

            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }


        private void Zona_Cargar(int id_zona)
        {
            try
            {
                AgendaWCFClient objAgendaWCF = new AgendaWCFClient();
                gsZona_ListarResult objZona = new gsZona_ListarResult();
                List<gsZona_ListarResult> lstZona;

                lstZona = objAgendaWCF.Agenda_ListarZona(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, id_zona).ToList();

                lstZona.Insert(0, objZona);
                objZona.Zona = "Todos";
                objZona.ID_Zona = 0;

                var lstZonas = from x in lstZona
                               select new
                               {
                                   x.ID_Zona,
                                   DisplayID = String.Format("{0}", x.ID_Zona),
                                   DisplayField = String.Format("{0}", x.Zona)
                                   //DisplayField = String.Format("{0} {1} {2} {3}", x.Placa, x.Modelo, x.Marca, x.Agenda)
                               };

                cboZona.DataSource = lstZonas;
                cboZona.DataTextField = "DisplayField";
                cboZona.DataValueField = "DisplayID";
                cboZona.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }


        protected void grdResumenVencimientos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (lblDateDetalle1.Text == "1")
                {
                    List<gsAgendaCliente_BuscarLimiteCreditoResult> lst = JsonHelper.JsonDeserialize<List<gsAgendaCliente_BuscarLimiteCreditoResult>>((string)ViewState["lstResumenVencidos"]);
                    //grdResumenVencimientos.DataSource = lst;
                    //grdResumenVencimientos.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se encontrarón resultados.";
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            Reporte_Cargar();

        }

        private void Reporte_Cargar()
        {
            grdGarantia.Visible = false;
            try
            {
                DateTime fecha1;
                DateTime fecha2;
                DateTime fecha3;
                DateTime fecha4;
                string Cliente;
                string Vendedor;
                int id_zona; 
                int vencidos;

                Cliente = "";
                Vendedor = "";
                List<gsReporte_DocumentosPendientesResult> lst;


                lblMensaje.Text = "";
  
                if (Session["Usuario"] == null)
                    Response.Redirect("~/Security/frmCerrar.aspx");

                try
                {
                    if (Validar_Variables() == 0)
                    {
                        fecha2 = dpFinalEmision.SelectedDate.Value;
                        fecha1 = fecha2.AddYears(-50);
                        fecha3 = fecha2.AddYears(-50);
                        fecha4 = fecha2.AddYears(50);

                        if (acbCliente == null || acbCliente.Text.Split('-')[0] == "" || acbCliente.Text == "")
                        {
                            Cliente = null;
                        }
                        else { Cliente = acbCliente.Text.Split('-')[0]; }
                        if (acbVendedor == null || acbVendedor.Text.Split('-')[0] == "" || acbVendedor.Text == "")
                        {
                            Vendedor = null;
                        }
                        else
                        { Vendedor = acbVendedor.Text.Split('-')[0]; }

                        if (rbtTipo == null || rbtTipo.SelectedValue == "0")
                        {
                            vencidos = 0;
                        }
                        else
                        {
                            vencidos = 1;
                        }

                        id_zona = Convert.ToInt32(cboZona.SelectedValue.ToString()); 
                             

                        var lstParametros = new List<string> { Cliente, Vendedor, fecha1.ToShortDateString(), fecha2.ToShortDateString(), fecha3.ToShortDateString(), fecha4.ToShortDateString(), vencidos.ToString() };
                        ViewState["lstParametros"] = JsonHelper.JsonSerializer(lstParametros);

                        lst = ListarEstadoCuenta(Cliente, Vendedor, fecha1, fecha2, fecha3, fecha4, vencidos, id_zona);
                        ListarClientesResumen(lst);
                        ListarClientesResumenVencidos();
                        ListarGarantia(Cliente);


                        AgendaWCFClient clienteAgenda = new AgendaWCFClient();
                        this.rdg_Observaciones.DataSource = clienteAgenda.ListarObservacionesAgenda(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, Cliente);
                        this.rdg_Observaciones.DataBind();

                        this.rdg_LineaCredito.DataSource = clienteAgenda.ListarLogLineaCredito(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, Cliente);
                        this.rdg_LineaCredito.DataBind();

                        gsAgenda_ContactoResult objeto = new gsAgenda_ContactoResult();
                        List<gsAgenda_ContactoResult> Lista = new List<gsAgenda_ContactoResult>();


                        Lista = clienteAgenda.Agenda_ListarContacto_Estado(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, Cliente).ToList() ;

                        if(!string.IsNullOrEmpty(Lista.Count().ToString()))
                        {
                            objeto = Lista[0];
                            lblContacto.Text =  objeto.Contacto;
                            lblCelular.Text = objeto.TelefonoCelular;
                            lblOficina.Text = objeto.TelefonoOficina;
                            lbleMail.Text = "Contacto: " + objeto.eMail;
                        }
                        else
                        {
                            lblContacto.Text = "";
                            lblCelular.Text = "";
                            lblOficina.Text = "";
                            lbleMail.Text = "";
                        }

 
                    }

                }
                catch (Exception ex)
                {
                    lblMensaje.Text = ex.Message;
                    lblMensaje.CssClass = "mensajeError";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se encontrarón resultados.";
                lblMensaje.CssClass = "mensajeError";
            }
        }

        public void ListarGarantia(string Cliente)
        {
            List<gsAgenda_ListarGarantiaResult> lista = new List<gsAgenda_ListarGarantiaResult>(); 

            AgendaWCFClient objAgendaGarantiaWCF = new AgendaWCFClient();
            List<gsAgenda_ListarGarantiaResult> lstGaran = objAgendaGarantiaWCF.Agenda_ListarGarantia(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, 1, Cliente).ToList();
            var liq = from c in lstGaran select c; // select new { c.FechaVencimiento, c.Valor };

            if (lstGaran.Count > 0 && Cliente != "")
            {
                grdGarantia.DataSource = liq.ToList();
                grdGarantia.DataBind();
            }
            else
            {
                grdGarantia.DataSource = lista;
                grdGarantia.DataBind();
            }
            grdGarantia.Visible = true;

        }

        public int Validar_Variables()
        {
            int valor = 0;

            if (dpFinalEmision == null || dpFinalEmision.SelectedDate.Value.ToString() == "")
            {
                valor = 1;
                lblMensaje.Text = lblMensaje.Text + "Seleccionar fecha final de emisión. ";
                lblMensaje.CssClass = "mensajeError";
            }
            if (acbCliente == null || acbCliente.Text.Length < 4)
            {
                if (acbVendedor == null || acbVendedor.Text.Length < 4)
                {
                    if(cboZona.SelectedValue == "0")
                    {
                        acbCliente = null;
                        acbVendedor = null;
                        valor = 1;
                        lblMensaje.Text = lblMensaje.Text + "Seleccionar cliente, vendedor o Zona.";
                        lblMensaje.CssClass = "mensajeError";
                    }
               

                }
            }

            return valor;
        }

        public int Validar_Variables2()
        {
            int valor = 0;

            if (dpFechaHastaCliente == null || dpFechaHastaCliente.SelectedDate.Value.ToString() == "")
            {
                valor = 1;
                lblMensaje.Text = lblMensaje.Text + "Seleccionar fecha final de emisión. ";
                lblMensaje.CssClass = "mensajeError";
            }

            return valor;
        }

        #region Métodos web
        [WebMethod]
        public static AutoCompleteBoxData Agenda_BuscarCliente(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarClienteResult[] lst = objAgendaWCFClient.Agenda_ListarCliente(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,

                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString, 0);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsAgenda_ListarClienteResult agenda in lst)
                {
                    AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                    childNode.Text = agenda.ID_Agenda + "-" + agenda.Nombre;
                    childNode.Value = agenda.ID_Agenda;
                    result.Add(childNode);
                }
                res.Items = result.ToArray();
            }
            return res;
        }

        [WebMethod]
        public static AutoCompleteBoxData Agenda_BuscarVendedor(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarVendedorResult[] lst = objAgendaWCFClient.Agenda_ListarVendedor(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsAgenda_ListarVendedorResult agenda in lst)
                {
                    AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                    childNode.Text = agenda.ID_Agenda + "-" + agenda.Nombre;
                    childNode.Value = agenda.ID_Agenda;
                    result.Add(childNode);
                }
                res.Items = result.ToArray();
            }
            return res;
        }
        #endregion

        protected void grdEstadoCuenta_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (lblDateDetalle1.Text == "1")
                {
                    List<gsReporte_DocumentosPendientesResult> lst = JsonHelper.JsonDeserialize<List<gsReporte_DocumentosPendientesResult>>((string)ViewState["lstEstadoCuenta"]);
                    grdEstadoCuenta.DataSource = lst;
                    grdEstadoCuenta.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se encontrarón resultados.";
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdResumenCliente_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (lblDateDetalle1.Text == "1")
                {
                    List<gsAgendaCliente_BuscarLimiteCreditoResult> lst = JsonHelper.JsonDeserialize<List<gsAgendaCliente_BuscarLimiteCreditoResult>>((string)ViewState["lstResumenCliente"]);
                    grdResumenCliente.DataSource = lst;
                    grdResumenCliente.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se encontrarón resultados.";
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnExpResumen_Click(object sender, ImageClickEventArgs e)
        {
            if (lblDateResumen1.Text == "1")
            {
                ExportGridToExcel_Resumen();
            }
        }

        protected void btnExpDetalle_Click(object sender, ImageClickEventArgs e)
        {
            ExportarExcelDetalle();
        }

        protected void ExportarExcelDetalle()
        {
            string Cliente;
            string Vendedor;

            if (lblDateDetalle1.Text == "1")
            {
                List<gsReporte_DocumentosPendientesResult> lst = JsonHelper.JsonDeserialize<List<gsReporte_DocumentosPendientesResult>>((string)ViewState["lstEstadoCuenta"]);

                var query_Detalle = from c in lst
                                    orderby c.ClienteNombre, c.FechaVencimiento
                                    select new
                                    {
                                        c.ClienteNombre,
                                        c.TipoDocumento,
                                        c.EstadoDoc,
                                        FechaGiro = DateTime.Parse(c.Fecha.ToString()).ToString("dd/MM/yyyy"),
                                        FechaVencimiento = DateTime.Parse(c.FechaVencimiento.ToString()).ToString("dd/MM/yyyy"),
                                        c.DiasMora,
                                        c.NroDocumento,
                                        c.Referencia,
                                        c.Situacion,
                                        c.Banco,
                                        NumeroUnico = Convert.ToString(c.NumeroUnico),
                                        c.monedasigno,
                                        Importe = string.Format("{0:#,##0.00}", c.Importe),
                                        DeudaSoles = string.Format("{0:#,##0.00}", c.DeudaSoles),
                                        DeudaDolares = string.Format("{0:#,##0.00}", c.DeudaDolares)
                                    };



                GridView GridView1 = new GridView();
                GridView1.AllowPaging = false;
                GridView1.DataSource = query_Detalle;
                GridView1.DataBind();
                //ExportGridToExcel_Detalle();
                //ExporttoExcel(GridView1);

                string ArchivoExcel = "EstadoCuenta";

                if (acbCliente == null || acbCliente.Text.Split('-')[0] == "" || acbCliente.Text == "")
                {
                }
                else
                {
                    Cliente = acbCliente.Text.Split('-')[0];
                    ArchivoExcel = ArchivoExcel + "_" + Cliente;
                }
                if (acbVendedor == null || acbVendedor.Text.Split('-')[0] == "" || acbVendedor.Text == "")
                {
                }
                else
                {
                    Vendedor = acbVendedor.Text.Split('-')[0];
                    ArchivoExcel = ArchivoExcel + "_" + Vendedor;
                }

                ExporttoExcel_V2(GridView1, ArchivoExcel);
   
            }

        }


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
                                orderby c.ClienteNombre, c.FechaVencimiento
                                select new
                                {
                                    c.ClienteNombre,
                                    c.TipoDocumento,
                                    c.EstadoDoc,
                                    FechaGiro = DateTime.Parse(c.Fecha.ToString()).ToString("dd/MM/yyyy"),
                                    FechaVencimiento = DateTime.Parse(c.FechaVencimiento.ToString()).ToString("dd/MM/yyyy"),
                                    c.DiasMora,
                                    c.NroDocumento,
                                    c.Referencia,
                                    c.Situacion,
                                    c.Banco,
                                    NumeroUnico = Convert.ToString(c.NumeroUnico),
                                    c.monedasigno,
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

        private void ExporttoExcel(GridView GridView1)
        {
            DataTable table = new DataTable();
            table = funConvertGVToDatatable(GridView1);

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");

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
            HttpContext.Current.Response.Write("Estado de Cuenta por Cliente");
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("</TR>");

            HttpContext.Current.Response.Write("<TR>");
            HttpContext.Current.Response.Write("<Td colspan= \"" + columnscount.ToString() + "\" >");
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

        protected void btnExpPDFResumen_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void ExportarPDF_Detalle()
        {

            int idEmpresa;
            string fechaHasta;
            int codUsuario;

            fechaHasta = dpFinalEmision.SelectedDate.Value.ToString("dd/MM/yyyy");
            idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;

            codUsuario = Convert.ToInt32(((Usuario_LoginResult)Session["Usuario"]).nroDocumento);

            if (lblDateDetalle1.Text == "1")
            {
                ExportarPDF(idEmpresa, fechaHasta);

            }
        }

        protected void btnExpPDFDetalle_Click(object sender, ImageClickEventArgs e)
        {
            ExportarPDF_Detalle();
        }

        private DataTable funConvertGVToDatatable(GridView dtgrv)
        {
            string campo;

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
                        campo = row.Cells[i].Text.Replace(" ", "");
                        campo = campo.Replace("&nbsp;", "");
                        //campo = campo.Replace(",", "");

                        dr[i] = campo;
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
            string fileName;
            int x;
            string path2 = this.Server.MapPath(".") + "\\tempArchivos\\";
            for (x = 1; x < 3; x++)
            {
                fileName = GetFileName(idEmpresa);
                PdfPTable tableLayout = new PdfPTable(11);

                if (!System.IO.Directory.Exists(path2))
                {
                    System.IO.Directory.CreateDirectory(path2);
                }

                string destFile = System.IO.Path.Combine(path2, fileName);

                Document doc = new Document();
                doc = new Document(PageSize.LETTER, 20, 20, 20, 20);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(destFile, FileMode.Create));

                doc.Open();
                doc.Add(Add_Content_To_PDF(tableLayout, fechaHasta, idEmpresa));
                doc.Close();

                AbriVentana(fileName);

            }

        }

        public static byte[] StreamFile(string filename)
        {
            var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var imageData = new byte[fs.Length];
            fs.Read(imageData, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            return imageData;
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
            if (idEmpresa == 1)
            { empresa = "Sil"; }
            else
            { empresa = "Neo"; }

            file = empresa + "EstadoCuenta_" + anho + mes + dia + minutos + segundo + miliseg + ".pdf";

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
            values[0] = 140;
            values[1] = 120;
            values[2] = 105;
            values[3] = 120;
            values[4] = 110;
            values[5] = 120;
            values[6] = 105;
            values[7] = 120;
            values[8] = 110;
            values[9] = 80;
            values[10] = 80;

            tableLayout.SetWidths(values);        //Set the pdf headers
            tableLayout.WidthPercentage = 100;       //Set the PDF File witdh percentage

            //Add Title to the PDF file at the top

            //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath(urlImagen));
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/Logos/grupo.png"));

            logo.ScaleAbsolute(205, 90);
            PdfPCell imageCell = new PdfPCell(logo);
            imageCell.Colspan = 2; // either 1 if you need to insert one cell
            imageCell.Border = 0;
            imageCell.HorizontalAlignment = Element.ALIGN_LEFT;

            //tableLayout.AddCell(imageCell);
            tableLayout.AddCell(new PdfPCell(new Phrase("Estado de cuenta al " + fechaHasta, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 10, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });
            tableLayout.AddCell(new PdfPCell(new Phrase(DateTime.Now.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 5, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 1, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });

            tableLayout.AddCell(new PdfPCell(new Phrase(objEmpresa.ruc + " " + objEmpresa.razonSocial, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 6, Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT });
            tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 5, Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER });

            tableLayout.AddCell(new PdfPCell(new Phrase(objEmpresa.direccion, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 6, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_LEFT });
            tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 5, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });

            //Add Cliente
            int cont = 0;

            foreach (gsAgendaCliente_BuscarLimiteCreditoResult resumen in lstClienteResumen)
            {
                cont = cont + 1;
                // ADD Cliente

                tableLayout.AddCell(new PdfPCell(new Phrase("1. Razón Social", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                List<gsAgenda_BuscarClienteDetalleResult> LimiteAgenda = objAgendaWCF.Agenda_BuscarClienteDetalle(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, resumen.ID_Agenda.ToString()).ToList();
                if (LimiteAgenda.Count > 0)
                {
                    gsAgenda_BuscarClienteDetalleResult AgendaResumen = LimiteAgenda[0];
                    tableLayout.AddCell(new PdfPCell(new Phrase(AgendaResumen.ruc + " " + AgendaResumen.Agendanombre, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase(AgendaResumen.Direccion + " " + AgendaResumen.Distrito + " - " + AgendaResumen.Provincia + " - " + AgendaResumen.Departamento + " - " + AgendaResumen.Pais, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, BorderColorLeft = BaseColor.RED, BorderColorTop = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                }
                else
                {
                    tableLayout.AddCell(new PdfPCell(new Phrase(resumen.ruc + " " + resumen.AgendaNombre, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Registrar dirección fiscal. ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, BorderColorLeft = BaseColor.RED, BorderColorTop = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                }

                tableLayout.AddCell(new PdfPCell(new Phrase("   ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 11, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });

                //ADD Resumen cliente
                decimal TCResumen = Convert.ToDecimal(resumen.TipoCambio);
                if (TCResumen < 0)
                {
                    TCResumen = TCResumen * -1;
                }


                var query_Detalle = from c in lstClienteDetalle
                                    where c.ID_Agenda == resumen.ID_Agenda
                                    orderby c.ClienteNombre, c.FechaVencimiento
                                    select new
                                    {
                                        c.TC,
                                        c.ID_Moneda,
                                        c.ID_Agenda,

                                        Pendiente = c.ID_Moneda == 0 ? c.ImportePendiente :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente / c.TC :
                                                    c.ImportePendiente,

                                        Pendiente_NoVencido = c.ID_Moneda == 0 ? c.ImportePendiente_NoVencido :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_NoVencido / c.TC :
                                                    c.ImportePendiente_NoVencido,

                                        Pendiente_VenceHoy = c.ID_Moneda == 0 ? c.ImportePendiente_VenceHoy :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_VenceHoy / c.TC :
                                                    c.ImportePendiente_VenceHoy,

                                        Pendiente_01a30 = c.ID_Moneda == 0 ? c.ImportePendiente_01a30 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_01a30 / c.TC :
                                                    c.ImportePendiente_01a30,
                                        Pendiente_31a60 = c.ID_Moneda == 0 ? c.ImportePendiente_31a60 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_31a60 / c.TC :
                                                    c.ImportePendiente_31a60,
                                        Pendiente_61a120 = c.ID_Moneda == 0 ? c.ImportePendiente_61a120 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_61a120 / c.TC :
                                                    c.ImportePendiente_61a120,
                                        Pendiente_121a360 = c.ID_Moneda == 0 ? c.ImportePendiente_121a360 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_121a360 / c.TC :
                                                    c.ImportePendiente_121a360,
                                        Pendiente_361aMas = c.ID_Moneda == 0 ? c.ImportePendiente_361aMas :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_361aMas / c.TC :
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

                decimal NoVencido;
                decimal DeudaVencida;
                decimal DeudaTotal;
                NoVencido = Convert.ToDecimal(sumImportePendiente_NoVencido) + Convert.ToDecimal(sumImportePendiente_VenceHoy);
                DeudaVencida = Convert.ToDecimal(sumImportePendiente_01a30) + Convert.ToDecimal(sumImportePendiente_31a60) + Convert.ToDecimal(sumImportePendiente_61a120) + Convert.ToDecimal(sumImportePendiente_121a360) + Convert.ToDecimal(sumImportePendiente_361aMas);
                DeudaTotal = NoVencido + DeudaVencida;

                string strsumNoVencido = string.Format("{0:$ #,##0.00}", NoVencido);
                string strsumImportePendiente_01a30 = string.Format("{0:$ #,##0.00}", sumImportePendiente_01a30);
                string strsumImportePendiente_31a60 = string.Format("{0:$ #,##0.00}", sumImportePendiente_31a60);
                string strsumImportePendiente_61a120 = string.Format("{0:$ #,##0.00}", sumImportePendiente_61a120);
                string strsumImportePendiente_121a360 = string.Format("{0:$ #,##0.00}", sumImportePendiente_121a360);
                string strsumImportePendiente_361aMas = string.Format("{0:$ #,##0.00}", sumImportePendiente_361aMas);
                string strsumImportePendiente = string.Format("{0:$ #,##0.00}", sumImportePendiente);

                string strsumDedudaTotal = string.Format("{0:$ #,##0.00}", DeudaTotal);
                string strsumDeudaVencida = string.Format("{0:$ #,##0.00}", DeudaVencida);

                string strLineaCredito = string.Format("{0:$ #,##0.00}", resumen.LineaCredito);
                string strDeudaVencida = string.Format("{0:$ #,##0.00}", strsumDeudaVencida);
                string strTotalCredito = string.Format("{0:$ #,##0.00}", strsumDedudaTotal);
                string strCreditoDisponible = string.Format("{0:$ #,##0.00}", resumen.CreditoDisponible);


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
                tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 31a60", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 61a120", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 121a360", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
                tableLayout.AddCell(new PdfPCell(new Phrase("Vencido 361aMas", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });
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
                                         where c.ID_Agenda == resumen.ID_Agenda
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
                int count = 0;
                string TipoDoc = "";
                string strTipoDoc = "";

                foreach (var query_Clientel in query_DetalleTotal)
                {
                    count = count + 1;
                    TipoDoc = query_Clientel.TipoDocumento;

                    string correctString = TipoDoc.Replace("(Venta)", "");

                    strTipoDoc = count.ToString() + ". " + correctString;
                    tableLayout.AddCell(new PdfPCell(new Phrase(strTipoDoc, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                    AddCellToBody(tableLayout, query_Clientel.NroDocumento);
                    AddCellToBody(tableLayout, query_Clientel.Fecha.Value.ToShortDateString());
                    AddCellToBody(tableLayout, query_Clientel.FechaVencimiento.Value.ToShortDateString());
                    AddCellToBody(tableLayout, query_Clientel.DiasMora.ToString());
                    AddCellToBody(tableLayout, query_Clientel.EstadoDoc);

                    AddCellToBodyColspan(tableLayout, query_Clientel.Banco, 2);

                    AddCellToBody(tableLayout, query_Clientel.NumeroUnico);

                    string Importe;
                    string ImportePendiente;

                    if (query_Clientel.ID_Moneda == 0)
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
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.WHITE))) { Colspan = intCol, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.BLACK });

        }

        private static void AddCellToBody(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

        }

        private static void AddCellToBodyColspan(PdfPTable tableLayout, string cellText, int intCol)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = intCol, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

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
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
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
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
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

        protected void btnBuscarResumenCliente_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Text = "";
                lblDateResumen1.Text = "";
                Reporte_CargarResumen();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscarLegal_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Text = "";
                lblDateLegal1.Text = "";
                Reporte_CargarResumenLegal();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscarProvision_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Text = "";
                lblDateProvision1.Text = "";
                Reporte_CargarResumenProvision();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }


        private void Reporte_CargarResumenProvision()
        {

            DateTime fecha1;
            DateTime fecha2;
            DateTime fecha3;
            DateTime fecha4;
            string Cliente;
            string Vendedor;
            int vencidos;

            Cliente = "";
            Vendedor = "";
            List<gsReporte_DocumentosPendientes_ProvisionResult> lst;

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (Validar_Variables2() == 0)
                {
                    fecha2 = dpFechaProvision.SelectedDate.Value;
                    fecha1 = fecha2.AddYears(-50);
                    fecha3 = fecha2.AddYears(-50);
                    fecha4 = fecha2.AddYears(50);

                    Cliente = null;
                    Vendedor = null;

                    if (rbtVencidosProvision == null || rbtVencidosProvision.SelectedValue == "0")
                    {
                        vencidos = 0;
                    }
                    else
                    {
                        vencidos = 1;
                    }

                    lst = ListarEstadoCuentaProvision(Cliente, Vendedor, fecha1, fecha2, fecha3, fecha4, vencidos);

                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }

        }


        private void Reporte_CargarResumenLegal()
        {

            DateTime fecha1;
            DateTime fecha2;
            DateTime fecha3;
            DateTime fecha4;
            string Cliente;
            string Vendedor;
            int vencidos;

            Cliente = "";
            Vendedor = "";
            List<gsReporte_DocumentosPendientes_LegalResult> lst;

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (Validar_Variables2() == 0)
                {
                    fecha2 = dpFechaLegal.SelectedDate.Value;
                    fecha1 = fecha2.AddYears(-50);
                    fecha3 = fecha2.AddYears(-50);
                    fecha4 = fecha2.AddYears(50);

                    Cliente = null;
                    Vendedor = null;

                    if (rbtVencidosLegal == null || rbtVencidosLegal.SelectedValue == "0")
                    {
                        vencidos = 0;
                    }
                    else
                    {
                        vencidos = 1;
                    }

                    lst = ListarEstadoCuentaLegal(Cliente, Vendedor, fecha1, fecha2, fecha3, fecha4, vencidos);

                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
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
            int vencidos;

            Cliente = "";
            Vendedor = "";
            List<gsReporte_DocumentosPendientesResumenClienteResult> lst;

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (Validar_Variables2() == 0)
                {
                    fecha2 = dpFechaHastaCliente.SelectedDate.Value;
                    fecha1 = fecha2.AddYears(-50);
                    fecha3 = fecha2.AddYears(-50);
                    fecha4 = fecha2.AddYears(50);

                    Cliente = null;
                    Vendedor = null;

                    if (rbtResumenCliente == null || rbtResumenCliente.SelectedValue == "0")
                    {
                        vencidos = 0;
                    }
                    else
                    {
                        vencidos = 1;
                    }

                    lst = ListarEstadoCuentaResumenCliente(Cliente, Vendedor, fecha1, fecha2, fecha3, fecha4, vencidos);

                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }

        }

        private void ExporttoExcel_V2(GridView GridView1, string Archivo)
        {
            string stringPeriodo;
            int year1;
            int mes1, mes2, mesDefoult;
            int year2, year3;
            string Origen;
            string Destino;

            try
            {
                DataTable table = new DataTable();
                table = funConvertGVToDatatable(GridView1);
                string strFecha = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

                //Origen = "C:\\Users\\cesar.coronel\\Desktop\\Pruebas_TXT\\Plantillas\\Origen\\EstadoCuentaDetalle.xls";
                Origen = "C:\\inetpub\\www\\IntranetGS\\Finanzas\\Plantillas\\EstadoCuentaDetalle.xls";

                //Destino = "C:\\Users\\cesar.coronel\\Desktop\\Pruebas_TXT\\Plantillas\\Destino\\" + Archivo + "_" + strFecha + ".xls";
                Destino = "C:\\temp\\uploads\\" + Archivo + "_" + strFecha + ".xls";


                File.Copy(Origen, Destino, true);

                ESCRIBIR_EXCEL_GENERAL(Destino, "Detalle", table, "MANTENIMIENTO", "CREDITOS");

                System.IO.FileInfo toDownload = new System.IO.FileInfo(Destino);

                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
                Response.AddHeader("Content-Length", toDownload.Length.ToString());
                Response.ContentType = "application/xls";
                string tab = string.Empty;
                Response.WriteFile(Destino);
                Response.End();

                //Response.ClearContent();
                //Response.ClearHeaders();
                //Response.ContentType = "application/vnd.ms-excel";
                //string document = toDownload.Name;
                //Response.AddHeader("Content-Disposition", string.Format("attachment; filename = {0}", document));
                //Response.WriteFile(Destino);
                //Response.Flush();



                //HttpContext.Current.Response.Clear();
                //HttpContext.Current.Response.AddHeader("Content-Disposition",
                //           "attachment; filename=" + toDownload.Name);
                //HttpContext.Current.Response.AddHeader("Content-Length",
                //           toDownload.Length.ToString());
                //HttpContext.Current.Response.ContentType = "application/octet-stream";
                //HttpContext.Current.Response.WriteFile(Destino);
                //HttpContext.Current.Response.End();


            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }



        }


        private void Exportar_DetalleEstado(RadGrid GridView1, string Archivo)
        {
            try
            {

                //grdEstadoCuenta = GridView1;

                grdEstadoCuenta.ExportSettings.FileName = Archivo;

                grdEstadoCuenta.ExportSettings.ExportOnlyData = true;
                grdEstadoCuenta.ExportSettings.IgnorePaging = true;
                grdEstadoCuenta.ExportSettings.OpenInNewWindow = true;
                grdEstadoCuenta.MasterTableView.ExportToExcel();

                Page.Response.ClearHeaders();
                Page.Response.ClearContent();

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }



        }


        public void ESCRIBIR_EXCEL_GENERAL(string Directorio, string Nombre_Hoja, System.Data.DataTable dtDatos, string stTipo, string stSubTipo)
        {
            try
            {
                if (stTipo == "MANTENIMIENTO")
                {
                    if (stSubTipo == "CREDITOS")
                    {
                        Escribir_Excel_Proyectado(Directorio, Nombre_Hoja, dtDatos);
                        //PintarFormatoPareto(Directorio, Nombre_Hoja, dtDatos); 
                    }
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }
        }

        public void Escribir_Excel_Proyectado(string Directorio, string NombreArchivo, DataTable Tabla)
        {
            string stringPeriodo;
            int year1;
            int mes1, mes2, mesDefoult;
            int year2, year3;

            string quote = "\"";
            string strConnnectionOle = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directorio + ";Extended Properties=" + quote + "Excel 8.0;HDR=NO" + quote + "";
            //string strConnnectionOle = "Microsoft.ACE.OLEDB.12.0;Data Source=" + Directorio + ";Extended Properties=Excel 8.0;HDR=NO";

            OleDbConnection oleConn = new OleDbConnection(strConnnectionOle);
            try
            {
                oleConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = oleConn;

                int countColumn = Tabla.Columns.Count - 1;
                int y = 0;
                char a;
                int cod;
                string Letra = "A";
                a = Convert.ToChar(Letra);
                cod = (int)a;


                //foreach (DataColumn col in Tabla.Columns)
                //{//write in new col

                //    cmd.CommandText = "UPDATE [" + NombreArchivo + "$" + Letra + "4:" + Letra + "4] SET F1=" + "'" + col.ColumnName.ToString() + "'";
                //    cmd.ExecuteNonQuery();

                //    cod = cod + 1;
                //    a = (char)cod;
                //    Letra = Convert.ToString(a);
                //}

                int fila = 5;

                foreach (DataRow row in Tabla.Rows)
                {//write in new row
                    string pareto = row[countColumn].ToString();
                    Letra = "A";
                    a = Convert.ToChar(Letra);
                    cod = (int)a;
                    string Texto = "";


                    Texto = "UPDATE [" + NombreArchivo + "$" + "A" + fila.ToString() + ":" + "O" + fila.ToString() + "]   ";
                    Texto = Texto + " SET F1=" + "'" + row[0].ToString() + "', ";
                    Texto = Texto + "  F2=" + "'" + row[1].ToString() + "', ";
                    Texto = Texto + "  F3=" + "'" + row[2].ToString() + "', ";
                    Texto = Texto + "  F4=" + "'" + row[3].ToString() + "', ";
                    Texto = Texto + "  F5=" + "'" + row[4].ToString() + "', ";
                    Texto = Texto + "  F6=" + "'" + row[5].ToString() + "', ";
                    Texto = Texto + "  F7=" + "'" + row[6].ToString() + "', ";
                    Texto = Texto + "  F8=" + "'" + row[7].ToString() + "', ";
                    Texto = Texto + "  F9=" + "'" + row[8].ToString() + "', ";
                    Texto = Texto + "  F10=" + "'" + row[9].ToString() + "', ";
                    Texto = Texto + "  F11=" + "'" + row[10].ToString() + "', ";
                    Texto = Texto + "  F12=" + "'" + row[11].ToString() + "', ";
                    Texto = Texto + "  F13=" + "'" + row[12].ToString() + "', ";
                    Texto = Texto + "  F14=" + "'" + row[13].ToString() + "', "; 
                    Texto = Texto + "  F15=" + "'" + row[14].ToString() + "' ";

                    cmd.CommandText = Texto;
                    cmd.ExecuteNonQuery();

                    fila = fila + 1;
                }


                oleConn.Close();
            }
            catch (Exception ex)
            {
                oleConn.Close();
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }

        }


        protected void ibExcel_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
       
             
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }
        }

        protected void grdEstadoCuenta_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                decimal idFinan;

                idFinan = ((gsReporte_DocumentosPendientesResult)(e.Item.DataItem)).ID_Financiamiento.Value;



                if (idFinan == 0)
                {
                    LinkButton img = (LinkButton)item["GridTemplateColumn"].Controls[0]; //Accessing EditCommandColumn
                    img.Visible = false;
                }
            }
        }

        protected void grdEstadoCuenta_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");
            decimal? intID_Financiamiento = 0; 
            try
            {
                if (e.CommandName == "Traza")
                {
                    gsReporte_DocumentosPendientesResult objLetra = JsonHelper.JsonDeserialize<List<gsReporte_DocumentosPendientesResult>>((string)ViewState["lstEstadoCuenta"]).Find(x => x.ID_Financiamiento.ToString() == e.CommandArgument.ToString());

                    if(objLetra.ID_Financiamiento.ToString().Length > 0)
                    {
                        intID_Financiamiento = objLetra.ID_Financiamiento; 
                    }
                    else
                    {
                        intID_Financiamiento = 0; 
                    }


                    var objTraza = new gsReporte_DocumentosPendientesResult {ID_Financiamiento = intID_Financiamiento };

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowHistorial('" + JsonHelper.JsonSerializer(objTraza) + "');", true);
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void RadButton1_OnClick(object sender, EventArgs e)
        {
            List<string> objLetra = JsonHelper.JsonDeserialize<List<string>>((string)ViewState["lstParametros"]);
            var aux = JsonHelper.JsonSerializer(objLetra);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowGraficoPie('" + aux + "');", true);
        }

        protected void rdbVencidosPlazo_OnClick(object sender, EventArgs e)
        {
            List<string> objLetra = JsonHelper.JsonDeserialize<List<string>>((string)ViewState["lstParametros"]);
            var aux = JsonHelper.JsonSerializer(objLetra);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowVencidosPie('" + aux + "');", true);
        }

        protected void rabVencidosMayorPlazo_OnClick(object sender, EventArgs e)
        {
            List<string> objLetra = JsonHelper.JsonDeserialize<List<string>>((string)ViewState["lstParametros"]);
            var aux = JsonHelper.JsonSerializer(objLetra);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowVencidosMayorPlazoPie('" + aux + "');", true);
        }

        protected void rdg_Observaciones_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

        }

        protected void rdg_LineaCredito_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

        }



    }
}