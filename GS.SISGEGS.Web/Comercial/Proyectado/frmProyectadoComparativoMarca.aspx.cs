using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.Web.ReporteVentaWCF;
using GS.SISGEGS.DM;
using Telerik.Web.UI;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.AgendaWCF;

namespace GS.SISGEGS.Web.Comercial.Proyectado
{
    public partial class frmProyectadoComparativoMarca : System.Web.UI.Page
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

                    dpPeriodoInicio.DbSelectedDate = new DateTime(DateTime.Now.Year, 1, 1);
                    dpPeriodoFinal.DbSelectedDate = DateTime.Now;

                    Zona_Cargar(0);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                ReporteVenta_Listar();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void ReporteVenta_Listar()
        {
            ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient();
            List<gsPronostico_Reporte_Planeamiento_MarcaResult> lst;

            try
            {
                DateTime fechaInicio = dpPeriodoInicio.SelectedDate.Value;
                DateTime fechaFinal = new DateTime(dpPeriodoFinal.SelectedDate.Value.Year, dpPeriodoFinal.SelectedDate.Value.Month, 1).AddMonths(1).AddDays(-1);

                int id_zona = int.Parse(cboZona.SelectedValue);
                string id_vendedor = "";

                lst = objReporteVentaWCF.Pronostico_Planeamiento_Marca(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, fechaInicio, fechaFinal, id_zona, id_vendedor).ToList();

                grdComparativo.DataSource = lst;
                grdComparativo.DataBind();


                //ViewState["fechaInicio"] = firstDayOfMonth.ToString("dd/MM/yyyy");
                ViewState["lstPlaneamiento"] = JsonHelper.JsonSerializer(lst);

                lblMensaje.Text = "Se han encontrado " + lst.Count.ToString() + " registro.";
                lblMensaje.CssClass = "mensajeExito";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                //string alternateText = (sender as RadButton).AlternateText;

                grdComparativo.ExportSettings.FileName = "Reporte_Pronostico_vs_presupuesto" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day;
                grdComparativo.ExportSettings.ExportOnlyData = true;
                grdComparativo.ExportSettings.IgnorePaging = true;
                grdComparativo.ExportSettings.OpenInNewWindow = true;
                grdComparativo.MasterTableView.ExportToExcel();

                Page.Response.ClearHeaders();
                Page.Response.ClearContent();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }
        }

        protected void grdComparativo_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (ViewState["lstPlaneamiento"] != null)
                {
                    grdComparativo.DataSource = JsonHelper.JsonDeserialize<List<gsPronostico_Reporte_Planeamiento_MarcaResult>>((string)ViewState["lstPlaneamiento"]);
                    //grdProducto.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

    }
}