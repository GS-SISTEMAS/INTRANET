using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.EstadoCuentaWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.LoginWCF;
using Telerik.Web.UI;

namespace GS.SISGEGS.Web.Finanzas.Aprobacion
{
    public partial class frmEstadoCuentaDetalleMng : System.Web.UI.Page
    {
        private void Empresa_ComboBox()
        {
            EmpresaWCFClient objEmpresaWCFC;
            try
            {
                objEmpresaWCFC = new EmpresaWCFClient();
                cboEmpresa.DataSource = objEmpresaWCFC.Empresa_ComboBox();
                cboEmpresa.DataTextField = "nombreComercial";
                cboEmpresa.DataValueField = "idEmpresa";
                cboEmpresa.DataBind();
                cboEmpresa.SelectedValue = "1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (!Page.IsPostBack)
                {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    if (!string.IsNullOrEmpty(Request.QueryString["aprobacion"]))
                    {
                        List<string> objAprobacion = Request.QueryString["aprobacion"].Split(',').ToList();
                        lblMensaje.Text = "";
                        acbCliente.Text = objAprobacion[0].ToString() + '-' + objAprobacion[1];
                        Empresa_ComboBox();
                        Reporte_Cargar();
                    }
                }
            }

            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private List<GS_ReporteEstadoCuentaMngResult> ListarEstadoCuenta(string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();

            try
            {
                var idEmpresa = Convert.ToInt32(cboEmpresa.SelectedValue);
                List<GS_ReporteEstadoCuentaMngResult> lstEstadoCuenta = new List<GS_ReporteEstadoCuentaMngResult>();
                List<GS_ReporteEstadoCuentaMngResult> lst = objEstadoCuentaWCF.EstadoCuenta_ListarAprobacion(idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos).OrderBy(e => e.ClienteNombre).OrderBy(e => e.FechaVencimiento).ToList();
                    lstEstadoCuenta = lst;
                

                ViewState["lstEstadoCuenta"] = JsonHelper.JsonSerializer(lstEstadoCuenta);
                grdEstadoCuentaMng.DataSource = lstEstadoCuenta;
                grdEstadoCuentaMng.DataBind();
                lblDate.Text = "1";
                return lstEstadoCuenta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Reporte_Cargar()
        {
            //grdGarantia.Visible = false;
            try
            {
                DateTime fecha1;
                DateTime fecha2;
                DateTime fecha3;
                DateTime fecha4;
                string Cliente;
                string Vendedor;
                int vencidos;

                Cliente = "";
                Vendedor = null;
                List<GS_ReporteEstadoCuentaMngResult> lst;

                lblMensaje.Text = "";
                if (Session["Usuario"] == null)
                    Response.Redirect("~/Security/frmCerrar.aspx");

                try
                {
                    if (Validar_Variables() == 0)
                    {
                        fecha2 = DateTime.Now;
                        fecha1 = fecha2.AddYears(-50);
                        fecha3 = fecha2.AddYears(-50);
                        fecha4 = fecha2.AddYears(50);

                        if (acbCliente == null || acbCliente.Text.Split('-')[0] == "" || acbCliente.Text == "")
                        {
                            Cliente = null;
                        }
                        else { Cliente = acbCliente.Text.Split('-')[0]; }
                       
                            vencidos = 0;
                        
                       

                        var lstParametros = new List<string> { Cliente, Vendedor, fecha1.ToShortDateString(), fecha2.ToShortDateString(), fecha3.ToShortDateString(), fecha4.ToShortDateString(), vencidos.ToString() };
                        ViewState["lstParametros"] = JsonHelper.JsonSerializer(lstParametros);

                        lst = ListarEstadoCuenta(Cliente, Vendedor, fecha1, fecha2, fecha3, fecha4, vencidos);
                        ListarClientesResumen(lst);
                        ListarClientesResumenVencidos();
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

        private void ListarClientesResumen(List<GS_ReporteEstadoCuentaMngResult> lst)
        {
            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();
            List<gsAgendaCliente_BuscarLimiteCreditoResult> lstLimiteCreditoAgenda;
            try
            {
                lstLimiteCreditoAgenda = new List<gsAgendaCliente_BuscarLimiteCreditoResult>();
                var queryAllAgenda = from DocumentosPendientes in lst select DocumentosPendientes.ID_Agenda;
                var queryAgenda = from AgendaPendiente in queryAllAgenda.Distinct() orderby AgendaPendiente ascending select AgendaPendiente;

                foreach (var agenda in queryAgenda)
                {
                    List<gsAgendaCliente_BuscarLimiteCreditoResult> LimiteCreditoAgenda = objEstadoCuentaWCF.EstadoCuenta_LimiteCreditoxCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, agenda.ToString(), 0).ToList();
                    gsAgendaCliente_BuscarLimiteCreditoResult Limite = LimiteCreditoAgenda[0];
                    lstLimiteCreditoAgenda.Add(Limite);
                }

                ViewState["lstResumenCliente"] = JsonHelper.JsonSerializer(lstLimiteCreditoAgenda);
                //grdResumenCliente.DataSource = lstLimiteCreditoAgenda;
                //grdResumenCliente.DataBind();
                lblDate.Text = "1";
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
            List<GS_ReporteEstadoCuentaMngResult> lstClienteDetalle;
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
                    string strLineaCredito = string.Format("{0:#,##0.00}", resumen.LineaCredito);
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
                    ClienteResumenFinal.DeudaTotal = Convert.ToDecimal(strsumImportePendiente);
                    ClienteResumenFinal.LineaCredito = Convert.ToDecimal(strLineaCredito);
                    ClienteResumenFinal.CreditoDisponible = Convert.ToDecimal(strCreditoDisponible);
                    lstClienteResumenFinal.Add(ClienteResumenFinal);

                }
                ViewState["lstResumenCliente"] = JsonHelper.JsonSerializer(lstClienteResumenFinal);
                grdResumenClienteMng.DataSource = lstClienteResumenFinal;
                grdResumenClienteMng.DataBind();

                lblDate.Text = "1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<gsAgendaCliente_BuscarLimiteCreditoResult> ClienteResumen()
        {
            List<gsAgendaCliente_BuscarLimiteCreditoResult> lst = JsonHelper.JsonDeserialize<List<gsAgendaCliente_BuscarLimiteCreditoResult>>((string)ViewState["lstResumenCliente"]);
            return lst;
        }

        private List<GS_ReporteEstadoCuentaMngResult> ClienteDetalle()
        {
            //DataTable dtTabla;
            List<GS_ReporteEstadoCuentaMngResult> lst = JsonHelper.JsonDeserialize<List<GS_ReporteEstadoCuentaMngResult>>((string)ViewState["lstEstadoCuenta"]);
            return lst;
        }

        public int Validar_Variables()
        {
            int valor = 0;

            return valor;
        }


        protected void grdEstadoCuentaMng_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (lblDate.Text == "1")
                {
                    List<GS_ReporteEstadoCuentaMngResult> lst = JsonHelper.JsonDeserialize<List<GS_ReporteEstadoCuentaMngResult>>((string)ViewState["lstEstadoCuenta"]);
                    grdEstadoCuentaMng.DataSource = lst;
                    grdEstadoCuentaMng.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se encontrarón resultados.";
                lblMensaje.CssClass = "mensajeError";
            }
        }


        protected void cboEmpresa_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grdEstadoCuentaMng.DataSource = new List<GS_ReporteEstadoCuentaMngResult>();
            grdEstadoCuentaMng.DataBind();

            grdResumenClienteMng.DataSource = new List<gsAgendaCliente_BuscarLimiteCreditoResult>();
            grdResumenClienteMng.DataBind();
            Reporte_Cargar();
        }
    }
}