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
    public partial class VencPorPlazoFacturasPieMng : System.Web.UI.Page
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


                        var porcentajeVencMayorPlazo = (lstSource.Count(x => x.DiasMora > 0 && x.DiasMora > x.DiasCredito) * 100) / lstSource.Count();

                        var lstChart = new List<ChartClass>();

                        var noVencidos = new ChartClass { AxisX = "Letras Vencidas Menor al Plazo", SerieDecimal = 100 - porcentajeVencMayorPlazo };
                        lstChart.Add(noVencidos);
                        var vencidos = new ChartClass { AxisX = "Letras Vencidas Mayor al plazo", SerieDecimal = porcentajeVencMayorPlazo };
                        lstChart.Add(vencidos);

                        RadHtmlChart1.ChartTitle.Text = "Letras Vencidas hasta " + objCliente[3];

                        RadHtmlChart1.DataSource = lstChart;
                        RadHtmlChart1.DataBind();

                        var lstFacturaSource = objEstadoCuentaWCF.EstadoCuenta_FacturasVencPorPlazo(idEmpresa, codigoUsuario, objCliente[0]
                            , objCliente[1], Convert.ToDateTime(objCliente[2]), Convert.ToDateTime(objCliente[3])
                            , Convert.ToDateTime(objCliente[4]), Convert.ToDateTime(objCliente[5]));


                        var porcentajeFactVencMayorPlazo = (lstFacturaSource.Count(x => x.DiasMora > 0 && x.DiasMora > x.DiasCredito) * 100) / lstSource.Count();

                        var lstFacturaChart = new List<ChartClass>();

                        var facturasNoVencidas = new ChartClass { AxisX = "Facturas Vencidas Menor al Plazo", SerieDecimal = 100 - porcentajeFactVencMayorPlazo };
                        lstFacturaChart.Add(facturasNoVencidas);
                        var facturasVencidas = new ChartClass { AxisX = "Facturas Vencidas Mayor al plazo", SerieDecimal = porcentajeFactVencMayorPlazo };
                        lstFacturaChart.Add(facturasVencidas);

                        RadHtmlChart2.ChartTitle.Text = "Facturas Vencidas hasta " + objCliente[3];

                        RadHtmlChart2.DataSource = lstFacturaChart;
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