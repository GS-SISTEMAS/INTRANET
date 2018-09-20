using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.EstadoCuentaWCF;
using GS.SISGEGS.Web.Helpers;
using Telerik.Web.UI;

namespace GS.SISGEGS.Web.Finanzas.EstadoCuenta
{
    public partial class EstadoLetrasPieMng : System.Web.UI.Page
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
                        var lstSource = objEstadoCuentaWCF.EstadoCuenta_GraficoPie(idEmpresa, codigoUsuario, objCliente[0]
                            , objCliente[1], Convert.ToDateTime(objCliente[2]), Convert.ToDateTime(objCliente[3])
                            , Convert.ToDateTime(objCliente[4]), Convert.ToDateTime(objCliente[5]), Convert.ToInt32(objCliente[6]));

                        var porcentajeCancelados = (lstSource.Count(x => x.FechaVencimiento < x.fechaRenovacion)*100)/lstSource.Count();

                        var lstChart = new List<ChartClass>();

                        var noVencidos = new ChartClass{AxisX = "Letras No Vencidas", SerieDecimal = 100-porcentajeCancelados};
                        lstChart.Add(noVencidos);
                        var vencidos = new ChartClass{AxisX = "Letras Vencidas", SerieDecimal = porcentajeCancelados};
                        lstChart.Add(vencidos);

                        RadHtmlChart1.ChartTitle.Text = "Letras Emitidas hasta " + objCliente[3];

                         RadHtmlChart1.DataSource = lstChart;
                        RadHtmlChart1.DataBind();

                        var lstSource01 = objEstadoCuentaWCF.EstadoCuenta_LetrasPorEstados(idEmpresa, codigoUsuario, objCliente[0]
                            , objCliente[1], Convert.ToDateTime(objCliente[2]), Convert.ToDateTime(objCliente[3])
                            , Convert.ToDateTime(objCliente[4]), Convert.ToDateTime(objCliente[5]), Convert.ToInt32(objCliente[6]));

                        int contadorLetrasEstados = lstSource01.Count();

                        var lstEstados = new List<string>
                        {
                            "Letra en Descuento",
                            "Letra en Cobranza",
                            "Cancelado",
                            "Letra Renovada",
                            "Letra Protestada",
                            "Letra en Garantia"
                        };

                        var lstChart01 = new List<ChartClass>();
                        foreach (var item in lstEstados)
                        {
                            int sum = lstSource01.Count(x => x.nombreEstado == item);
                            lstChart01.Add(new ChartClass {AxisX = item, SerieDecimal = ((sum*100)/contadorLetrasEstados)});
                        }
                        RadHtmlChart2.DataSource = lstChart01;
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