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
    public partial class frmIndicadorLetrasProtes : System.Web.UI.Page
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

                    List<GS_ReporteIndicadorLetrasProtestadasResult> lst = new List<GS_ReporteIndicadorLetrasProtestadasResult>();

                    grdIndicadores.DataSource = lst;
                    dpFechaHastaCliente.SelectedDate = DateTime.Now;

                    grdIndicadores.DataSource = lst;
                    grdIndicadores.DataBind();
                }
            }
                 
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
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
                List<GS_ReporteIndicadorLetrasProtestadasResult> lst;

                lblMensaje.Text = "";
                if (Session["Usuario"] == null)
                    Response.Redirect("~/Security/frmCerrar.aspx");

                try
                {

                        fecha2 = dpFechaHastaCliente.SelectedDate.Value;
                        fecha1 = fecha2.AddYears(-30);
                        fecha3 = fecha2.AddYears(-30);
                        fecha4 = fecha2.AddYears(30);

                        Cliente = null;
                        Vendedor = null;


                        vencidos = 0;



                        var lstParametros = new List<string> { Cliente, Vendedor, fecha1.ToShortDateString(), fecha2.ToShortDateString(), fecha3.ToShortDateString(), fecha4.ToShortDateString(), vencidos.ToString() };
                        ViewState["lstParametros"] = JsonHelper.JsonSerializer(lstParametros);

                        lst = ListarIndicadoreLetrasProtestadas(Cliente, Vendedor, fecha1, fecha2, fecha3, fecha4);

                    

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

        private List<GS_ReporteIndicadorLetrasProtestadasResult> ListarIndicadoreLetrasProtestadas(string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal)
        {

            IndicadoresWCFClient objIndicadoresWCF = new IndicadoresWCFClient();
            try
            {

                BasicHttpBinding binding = (BasicHttpBinding)objIndicadoresWCF.Endpoint.Binding;
                binding.MaxReceivedMessageSize = Int32.MaxValue;
                objIndicadoresWCF.InnerChannel.OperationTimeout = new TimeSpan(0, 10, 0);
                List<GS_ReporteIndicadorLetrasProtestadasResult> lstDocumentos = objIndicadoresWCF.Indicadores_LetrasProtestadas(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, 0).ToList();

                //foreach (var item in lstDocumentos)
                //{
                //    if (item.importeProtestado > 0 && item.ImportePagado > 0)
                //        item.IndicadorProtes = item.importeProtestado/item.ImportePagado;
                //    else
                //        item.IndicadorProtes = 0;
                //}
                
                 
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

        protected void grdIndicadores_OnNeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (lblDate2.Text == "2")
                {
                    grdIndicadores.DataSource = JsonHelper.JsonDeserialize<List<GS_ReporteIndicadorLetrasProtestadasResult>>((string)ViewState["lstIndicadores"]);
                }

            }
            catch (Exception ex)
            {
                rwmVidaLey.RadAlert(ex.Message, 500, 100, "Error al consultar el archivo", "");
            }
        }

        protected void btnBuscar_OnClick(object sender, EventArgs e)
        {
            Reporte_Cargar();
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                this.grdIndicadores.ExportSettings.Excel.Format = (PivotGridExcelFormat)Enum.Parse(typeof(PivotGridExcelFormat), "Xlsx");
                this.grdIndicadores.ExportSettings.FileName = "ReporteIndicadoresLetProtes_" + DateTime.Now.ToString("yyyyMMddHmm");
                this.grdIndicadores.ExportToExcel();
            }
            catch (Exception ex)
            {
                rwmVidaLey.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }
        }
    }
}