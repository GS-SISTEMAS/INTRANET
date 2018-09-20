using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.EstadoCuentaWCF;
using GS.SISGEGS.Web.Helpers;

namespace GS.SISGEGS.Web.Finanzas.EstadoCuenta
{
    public partial class VencPorPlazoLetrasPieMng : System.Web.UI.Page
    {
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

                    if (!string.IsNullOrEmpty(Request.QueryString["objCliente"]))
                    {
                        List<string> objCliente = JsonHelper.JsonDeserialize<List<string>>(Request.QueryString["objCliente"]);


                        var idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                        var codigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
                        EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();
                        var lstSource = objEstadoCuentaWCF.EstadoCuenta_LetrasVencPorPlazo(idEmpresa, codigoUsuario, objCliente[0]
                            , objCliente[1], Convert.ToDateTime(objCliente[2]), Convert.ToDateTime(objCliente[3])
                            , Convert.ToDateTime(objCliente[4]), Convert.ToDateTime(objCliente[5]));

                        var lstPlazos= new List<int>
                        {
                            30,
                            45,
                            60,
                            90,
                            120
                        };

                        List<GS_GetLetrasVencPorPlazoResult>  lstFiltrada = lstSource.Where(x => x.DiasMora > 0 && x.DiasMora < x.DiasCredito).ToList();

                        var lstChart = new List<ChartClass>();

                        foreach (var item in lstPlazos)
                        {
                            int sum = 0;
                            switch (item)
                            {
                                case 30:
                                    sum = lstFiltrada.Count(x => x.DiasCreditoDoc > 0 && x.DiasCreditoDoc <= item);
                                    lstChart.Add(new ChartClass { AxisX = item.ToString(), SerieDecimal = ((sum * 100) / lstFiltrada.Count()) });
                                    break;
                                case 45:
                                    sum = lstFiltrada.Count(x => x.DiasCreditoDoc > 30 && x.DiasCreditoDoc <= item);
                                    lstChart.Add(new ChartClass { AxisX = item.ToString(), SerieDecimal = ((sum * 100) / lstFiltrada.Count()) });
                                    break;
                                case 60:
                                    sum = lstFiltrada.Count(x => x.DiasCreditoDoc > 45 && x.DiasCreditoDoc <= item);
                                    lstChart.Add(new ChartClass { AxisX = item.ToString(), SerieDecimal = ((sum * 100) / lstFiltrada.Count()) });
                                    break;
                                case 90:
                                    sum = lstFiltrada.Count(x => x.DiasCreditoDoc > 60 && x.DiasCreditoDoc <= item);
                                    lstChart.Add(new ChartClass { AxisX = item.ToString(), SerieDecimal = ((sum * 100) / lstFiltrada.Count()) });
                                    break;
                                case 120:
                                    sum = lstFiltrada.Count(x => x.DiasCreditoDoc > 90);
                                    lstChart.Add(new ChartClass { AxisX = item.ToString(), SerieDecimal = ((sum * 100) / lstFiltrada.Count()) });
                                    break;
                            }
                            
                        }

                        RadHtmlChart1.ChartTitle.Text = "Letras Vencidas hasta " + objCliente[3];

                        RadHtmlChart1.DataSource = lstChart;
                        RadHtmlChart1.DataBind();

                        var lstFacturaSource = objEstadoCuentaWCF.EstadoCuenta_FacturasVencPorPlazo(idEmpresa, codigoUsuario, objCliente[0]
                            , objCliente[1], Convert.ToDateTime(objCliente[2]), Convert.ToDateTime(objCliente[3])
                            , Convert.ToDateTime(objCliente[4]), Convert.ToDateTime(objCliente[5]));


                        List<GS_GetFacturasVencPorPlazoResult> lstFacturaFiltrada = lstFacturaSource.Where(x => x.DiasMora > 0 && x.DiasMora < x.DiasCredito).ToList();

                        var lstChartFactura = new List<ChartClass>();

                        foreach (var item in lstPlazos)
                        {
                            int sum = 0;
                            switch (item)
                            {
                                case 30:
                                    sum = lstFacturaFiltrada.Count(x => x.DiasCreditoDoc > 0 && x.DiasCreditoDoc <= item);
                                    lstChartFactura.Add(new ChartClass { AxisX = item.ToString(), SerieDecimal = ((sum * 100) / lstFacturaFiltrada.Count()) });
                                    break;
                                case 45:
                                    sum = lstFacturaFiltrada.Count(x => x.DiasCreditoDoc > 30 && x.DiasCreditoDoc <= item);
                                    lstChartFactura.Add(new ChartClass { AxisX = item.ToString(), SerieDecimal = ((sum * 100) / lstFacturaFiltrada.Count()) });
                                    break;
                                case 60:
                                    sum = lstFacturaFiltrada.Count(x => x.DiasCreditoDoc > 45 && x.DiasCreditoDoc <= item);
                                    lstChartFactura.Add(new ChartClass { AxisX = item.ToString(), SerieDecimal = ((sum * 100) / lstFacturaFiltrada.Count()) });
                                    break;
                                case 90:
                                    sum = lstFacturaFiltrada.Count(x => x.DiasCreditoDoc > 60 && x.DiasCreditoDoc <= item);
                                    lstChartFactura.Add(new ChartClass { AxisX = item.ToString(), SerieDecimal = ((sum * 100) / lstFacturaFiltrada.Count()) });
                                    break;
                                case 120:
                                    sum = lstFacturaFiltrada.Count(x => x.DiasCreditoDoc > 90);
                                    lstChartFactura.Add(new ChartClass { AxisX = item.ToString(), SerieDecimal = ((sum * 100) / lstFacturaFiltrada.Count()) });
                                    break;
                            }

                        }

                        RadHtmlChart2.ChartTitle.Text = "Facturas Vencidas hasta " + objCliente[3];

                        RadHtmlChart2.DataSource = lstChartFactura;
                        RadHtmlChart2.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                //lblMensaje.Text = "ERROR: " + ex.Message;
                //lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}