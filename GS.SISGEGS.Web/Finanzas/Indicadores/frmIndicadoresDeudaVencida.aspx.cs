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
    public partial class frmIndicadoresDeudaVencida : System.Web.UI.Page
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

                    List<gsReporte_IndicadoresDeudaVencidaResult> lst = new List<gsReporte_IndicadoresDeudaVencidaResult>();

                    grdIndicadores.DataSource = lst;

                    DateTime fecha = DateTime.Now;
                    dpFechaHastaCliente.SelectedDate = fecha;
                    fecha =  fecha.AddYears(-20);
                    dpFechaDesdeCliente.SelectedDate = fecha; 

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
            List<gsReporte_IndicadoresDeudaVencidaResult> lst;

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
                    //fecha1 = fecha3; 
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

        private List<gsReporte_IndicadoresDeudaVencidaResult> ListarEstadoCuentaResumenCliente(string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int divisor, int verTodo, int verCartera)
        {

            IndicadoresWCFClient objIndicadoresWCF = new IndicadoresWCFClient();
            try
            {

                BasicHttpBinding binding = (BasicHttpBinding)objIndicadoresWCF.Endpoint.Binding;
                binding.MaxReceivedMessageSize = Int32.MaxValue;
                objIndicadoresWCF.InnerChannel.OperationTimeout = new TimeSpan(0, 10, 0);
                List<gsReporte_IndicadoresDeudaVencidaResult> lstDocumentos = objIndicadoresWCF.Indicadores_DeudaVencida(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, 0, divisor, verTodo, verCartera).ToList();

                //var newLstDocumentos01 = mapObjIndicadoreVencido(lstDocumentos);


                //var nroClientesDeuda =
                //    newLstDocumentos01.GroupBy(x => x.ZonaCobranza)
                //        .Select(g => new gsReporte_IndicadoresDeudaVencidaResult
                //        {
                //            ZonaCobranza = g.Key
                //            ,

                //            Vencido30a180 = (g.Sum(x => x.numeroVenc30180))
                //            ,
                //            sumaDeuda = g.Sum(x => x.numeroDeudaTotal)
                //            ,
                //            DeudaVencida = g.Sum(x => x.numeroDeudaVenc)
                //        }).ToList();




                List<gsReporte_IndicadoresDeudaVencidaResult> lstDoc;
                if(chkClientes.Checked)
                    lstDoc = lstDocumentos;
                else

                    lstDoc = lstDocumentos.Where(x => x.numeroVenc30180 > 0).ToList();

                //foreach (var item in nroClientesDeuda)
                //{
                //    foreach (var itemDoc in lstDoc)
                //    {
                //        //var numeroVenc30180 = nroClientesDeuda.Where(x =>x.ZonaCobranza = itemDoc.ZonaCobranza && x.ClienteNombre = )
                //        if (itemDoc.ZonaCobranza != item.ZonaCobranza) continue;
                //        itemDoc.numeroDeudaTotal = Convert.ToInt32(item.sumaDeuda);
                //        itemDoc.numeroDeudaVenc = Convert.ToInt32(item.DeudaVencida);
                //        itemDoc.indDeudaVencida = Convert.ToDecimal(GetIndicador(itemDoc));
                //        //itemDoc.numeroVenc30180 = Convert.ToInt32(item.Vencido30a180);
                //    }
                //}

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

        //private double GetIndicador(gsReporte_IndicadoresDeudaVencidaResult itemDoc)
        //{
        //    //if (itemDoc.numeroVenc30180 == 0 || itemDoc.numeroDeudaVenc == 0)
        //    //    return 0;

        //    //if (rbtDeudaCliente.SelectedValue == "0")
        //    //    return (itemDoc.numeroVenc30180*0.8)/itemDoc.numeroDeudaVenc;
        //    //return (itemDoc.numeroVenc30180 * 0.8) / itemDoc.numeroDeudaTotal;
        //}

        private List<gsReporte_IndicadoresDeudaVencidaResult> mapObjIndicadoreVencido(List<gsReporte_IndicadoresDeudaVencidaResult> lstDocumentos)
        {
            var newLstDocumentos = new List<gsReporte_IndicadoresDeudaVencidaResult>();

            foreach (var item in lstDocumentos)
            {
                var obj = new gsReporte_IndicadoresDeudaVencidaResult();
                obj.ZonaCobranza = item.ZonaCobranza;
                //obj.ClienteNombre = item.ClienteNombre;
                obj.Vencido30a180 = item.Vencido30a180;
                obj.sumaDeuda = item.sumaDeuda;
                obj.DeudaVencida = item.DeudaVencida;
                obj.numeroDeudaTotal = item.numeroDeudaTotal;
                obj.numeroDeudaVenc = item.numeroDeudaVenc;
                obj.numeroVenc30180 = item.numeroVenc30180;
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
                    grdIndicadores.DataSource = JsonHelper.JsonDeserialize<List<gsReporte_IndicadoresDeudaVencidaResult>>((string)ViewState["lstIndicadores"]);
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