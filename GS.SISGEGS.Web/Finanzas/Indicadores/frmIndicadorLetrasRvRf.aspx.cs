using GS.SISGEGS.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;
using GS.SISGEGS.Web.IndicadoresWCF;
using System.ServiceModel;
using GS.SISGEGS.Web.Helpers;

namespace GS.SISGEGS.Web.Finanzas.Indicadores
{
    public partial class frmIndicadorLetrasRvRf : System.Web.UI.Page
    {
        private void CargarPivot(DateTime fechaCorte) {
            List<GS_ReporteLtasREF_RENOVResult> lista = new List<GS_ReporteLtasREF_RENOVResult>();

            try {
                //var binding = new WSHttpBinding();
                //binding.MaxReceivedMessageSize = Int32.MaxValue;
                IndicadoresWCFClient servicio = new IndicadoresWCFClient();

                BasicHttpBinding binding = (BasicHttpBinding)servicio.Endpoint.Binding;
                binding.MaxReceivedMessageSize = Int32.MaxValue;
                binding.ReceiveTimeout= new TimeSpan(0, 10, 0);
                binding.SendTimeout= new TimeSpan(0, 10, 0);
                binding.CloseTimeout= new TimeSpan(0, 10, 0);                
                servicio.InnerChannel.OperationTimeout= new TimeSpan(0, 10, 0);
                
                //servicio.Endpoint.Binding
                
                int IdEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
                lista = servicio.Indicadores_LetrasRenyRef(IdEmpresa, CodigoUsuario, fechaCorte).ToList();
                ViewState["tmpPivot"] = JsonHelper.JsonSerializer(lista);
                pivotIndicador.DataSource = lista;
                pivotIndicador.DataBind();
                //throw new ArgumentException("Error al procesar la info");
            }
            catch (Exception ex) {
                this.rwmMensaje.RadAlert(ex.Message, 500, 100, "Error", "");
            }

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            if (!Page.IsPostBack) {
                LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                    ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500;
                
                //manager.AsyncPostBackTimeout
                dpFechaMes.SelectedDate= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                //CargarPivot(dpFechaMes.SelectedDate.Value);
                pivotIndicador.DataSource = new List<GS_ReporteLtasREF_RENOVResult>();
                //this.rwmMensaje.RadAlert("Mostrando mensaje", 500, 100, "Error", "");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarPivot(dpFechaMes.SelectedDate.Value);
        }

        protected void pivotIndicador_NeedDataSource(object sender, Telerik.Web.UI.PivotGridNeedDataSourceEventArgs e)
        {
            //PivotAgrupamiento.DataSource = JsonHelper.JsonDeserialize<List<DetalleOperacionFamiliaResult>>((string)ViewState["tmpPivot"]);

            pivotIndicador.DataSource=JsonHelper.JsonDeserialize<List<GS_ReporteLtasREF_RENOVResult>>((string)ViewState["tmpPivot"]);

        }

        protected void pivotIndicador_ItemNeedCalculation(object sender, Telerik.Web.UI.PivotGridCalculationEventArgs e)
        {
            if (e.DataField == "Indicador") {
                AggregateValue TotalRenovada = e.GetAggregateValue("TotalRenovada");
                AggregateValue TotalRefinanciada = e.GetAggregateValue("TotalRefinanciada");
                AggregateValue DeudaTotal = e.GetAggregateValue("DeudaTotal");

                if (TotalRenovada != null && TotalRefinanciada != null && DeudaTotal != null) {
                    decimal val_TotalRenovada = (decimal)TotalRenovada.GetValue();
                    decimal val_TotalRefinanciada = (decimal)TotalRefinanciada.GetValue();
                    decimal val_DeudaTotal = (decimal)DeudaTotal.GetValue();


                    if (val_DeudaTotal == 0)
                    {
                        e.CalculatedValue = new DoubleAggregateValue(0);
                    }
                    else {
                        double resultado =Convert.ToDouble(((val_TotalRenovada + val_TotalRefinanciada) / val_DeudaTotal) * 100);
                        e.CalculatedValue = new DoubleAggregateValue(resultado);
                    }
                }
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            this.pivotIndicador.ExportToExcel();
        }
    }
}