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
    public partial class frmTrazabilidadMng : System.Web.UI.Page
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

                    if (!string.IsNullOrEmpty(Request.QueryString["objHistorial"]))
                    {

                        Title = "Historial";
                        string obj = Request.QueryString["objHistorial"];
                        gsReporte_DocumentosPendientesResult objCierreContable = JsonHelper.JsonDeserialize<gsReporte_DocumentosPendientesResult>(Request.QueryString["objHistorial"]);
                        ViewState["ID_Financiamiento"] = objCierreContable.ID_Financiamiento;
                        //CargarGridEdit();

                        //Label1.Text = objCierreContable.ID_Financiamiento.ToString();


                        var idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                        var codigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
                        EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();
                        var lstSource = objEstadoCuentaWCF.GetCicloLetras(idEmpresa, codigoUsuario, (decimal)objCierreContable.ID_Financiamiento);
                        grdHistorial.DataSource = lstSource;
                        grdHistorial.DataBind();

                        var lstEstados = new List<string>
                        {
                            "Cancelado",
                            "Letra Aprobada",
                            "Letra Renovada",
                            "Letra Protestada"
                        };

                        var lstChart = (from item in lstEstados let sum = lstSource.Count(x => x.Estado == item) select new ChartClass {AxisX = item, SerieDecimal = sum}).ToList();

                        //RadHtmlChart1.DataSource = lstChart;
                        //RadHtmlChart1.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                //lblMensaje.Text = "ERROR: " + ex.Message;
                //lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                grdHistorial.ExportSettings.FileName = "Trazabilidad_" + DateTime.Now.ToString("yyyyMMddHmm");
                grdHistorial.ExportSettings.ExportOnlyData = true;
                grdHistorial.ExportSettings.IgnorePaging = true;
                grdHistorial.ExportSettings.OpenInNewWindow = true;
                grdHistorial.MasterTableView.ExportToExcel();
                Page.Response.ClearHeaders();
                Page.Response.ClearContent();
            }
            catch (Exception ex)
            {
                
            }
        }
    }

    public class ChartClass
    {
        public string AxisX { get; set; }
        public decimal SerieDecimal { get; set; }
    }
}