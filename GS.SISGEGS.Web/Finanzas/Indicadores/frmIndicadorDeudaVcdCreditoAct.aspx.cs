using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.IndicadoresWCF;
using Telerik.Web.UI;

namespace GS.SISGEGS.Web.Finanzas.Indicadores
{
    public partial class frmIndicadorDeudaVcdCreditoAct : System.Web.UI.Page
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

                    List<gsReporte_IndicadoresDeudaVencCreditoActResult> lst = new List<gsReporte_IndicadoresDeudaVencCreditoActResult>();

                    grdIndicadores.DataSource = lst;
                    dpFechaHastaCliente.SelectedDate = DateTime.Now;
                    dpFechaDesdeCliente.SelectedDate = DateTime.Now;

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
            int verTodo = 0;
            int verCartera = 0;

            Cliente = "";
            Vendedor = "";
            List<gsReporte_IndicadoresDeudaVencCreditoActResult> lst;

            lblMensajeResumenCliente.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (Validar_Variables2() == 0)
                {
                    fecha2 = dpFechaHastaCliente.SelectedDate.Value;
                    fecha1 = dpFechaDesdeCliente.SelectedDate.Value;
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

            if (dpFechaHastaCliente == null || dpFechaHastaCliente.SelectedDate.Value.ToString() == "" || dpFechaDesdeCliente == null || dpFechaDesdeCliente.SelectedDate.Value.ToString() == "")
            {
                valor = 1;
                lblMensajeResumenCliente.Text = lblMensaje.Text + "Seleccionar fecha de emisión. ";
                lblMensajeResumenCliente.CssClass = "mensajeError";
            }

            return valor;
        }

        private List<gsReporte_IndicadoresDeudaVencCreditoActResult> ListarEstadoCuentaResumenCliente(string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int divisor, int verTodo, int verCartera)
        {

            IndicadoresWCFClient objIndicadoresWCF = new IndicadoresWCFClient();
            try
            {

                BasicHttpBinding binding = (BasicHttpBinding)objIndicadoresWCF.Endpoint.Binding;
                binding.MaxReceivedMessageSize = Int32.MaxValue;
                objIndicadoresWCF.InnerChannel.OperationTimeout = new TimeSpan(0, 10, 0);
                List<gsReporte_IndicadoresDeudaVencCreditoActResult> lstDocumentos = objIndicadoresWCF.Indicadores_DeudaVencidaCreditoAct(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, 0, divisor, verTodo, verCartera).ToList();

                var newLstDocumentos01 = mapObjIndicadoreVencido(lstDocumentos);


                var nroClientesDeuda =
                    newLstDocumentos01.GroupBy(x => x.ZonaCobranza)
                        .Select(g => new gsReporte_IndicadoresDeudaVencCreditoActResult
                        {
                            ZonaCobranza = g.Key
                            //,

                            //numeroVenc30Mas = (g.Sum(x => x.numeroVenc30Mas))
                            ,
                            CreditoDisponible = g.Sum(x => x.CreditoDisponible)
    
                        }).ToList();


                List<gsReporte_IndicadoresDeudaVencCreditoActResult> lstDoc;
                if (chkClientes.Checked)
                    lstDoc = lstDocumentos;
                else

                    lstDoc = lstDocumentos.Where(x => x.numeroVenc30Mas > 0).ToList();

                foreach (var item in nroClientesDeuda)
                {
                    foreach (var itemDoc in lstDoc)
                    {
                        //var numeroVenc30180 = nroClientesDeuda.Where(x =>x.ZonaCobranza = itemDoc.ZonaCobranza && x.ClienteNombre = )
                        if (itemDoc.ZonaCobranza != item.ZonaCobranza) continue;
                        itemDoc.CreditoDisponible = Convert.ToInt32(item.CreditoDisponible);
                        //itemDoc.numeroVenc30Mas = Convert.ToInt32(item.numeroVenc30Mas);
                        itemDoc.indDeudaCliente = Convert.ToDecimal(GetIndicador(itemDoc));
                        //itemDoc.numeroVenc30180 = Convert.ToInt32(item.Vencido30a180);
                    }
                }

                ViewState["lstIndicadores"] = JsonHelper.JsonSerializer(lstDoc);//.Where(x => x.Vencido30a180 > 0).ToList()

                grdIndicadores.DataSource = lstDoc;//.Where(x => x.Vencido30a180 > 0)
                grdIndicadores.DataBind();

                lblMensajeResumenCliente.Text = "Se han encontrado " + lstDoc.Count.ToString() + " registro.";
                lblMensajeResumenCliente.CssClass = "mensajeExito";

                lblDate2.Text = "2";
                return lstDocumentos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private decimal GetIndicador(gsReporte_IndicadoresDeudaVencCreditoActResult itemDoc)
        {
            if (itemDoc.numeroVenc30Mas == 0 || itemDoc.CreditoDisponible == 0)
                return 0;

            return itemDoc.numeroVenc30Mas * 1.0m / itemDoc.CreditoDisponible;
        }

        private List<gsReporte_IndicadoresDeudaVencCreditoActResult> mapObjIndicadoreVencido(List<gsReporte_IndicadoresDeudaVencCreditoActResult> lstDocumentos)
        {
            var newLstDocumentos = new List<gsReporte_IndicadoresDeudaVencCreditoActResult>();

            foreach (var item in lstDocumentos)
            {
                var obj = new gsReporte_IndicadoresDeudaVencCreditoActResult();
                obj.ZonaCobranza = item.ZonaCobranza;
                //obj.ClienteNombre = item.ClienteNombre;
                obj.CreditoDisponible = item.CreditoDisponible;
                obj.numeroVenc30Mas = item.numeroVenc30Mas;

                newLstDocumentos.Add(obj);
            }
            return newLstDocumentos;
        }

        protected void grdIndicadores_OnNeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (lblDate2.Text == "2")
                {
                    grdIndicadores.DataSource = JsonHelper.JsonDeserialize<List<gsReporte_IndicadoresDeudaVencCreditoActResult>>((string)ViewState["lstIndicadores"]);
                }

            }
            catch (Exception ex)
            {
                rwmVidaLey.RadAlert(ex.Message, 500, 100, "Error al consultar el archivo", "");
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
    }
}