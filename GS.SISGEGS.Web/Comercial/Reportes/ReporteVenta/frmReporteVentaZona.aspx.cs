using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.ReporteVentaWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.DM;
using System.Globalization;
using Telerik.Web.UI;
using GS.SISGEGS.Web.LoginWCF;

namespace GS.SISGEGS.Web.Comercial.Reportes.ReporteVenta
{
    public partial class frmReporteVentaZona : System.Web.UI.Page
    {
        private void ReporteVenta_Listar()
        {
            ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient();
            List<gsDocVenta_ReporteVenta_VendedorV2Result> lst;

            try
            {
                DateTime firstDayOfMonth = new DateTime(rmyReporte.SelectedDate.Value.Year, rmyReporte.SelectedDate.Value.Month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                lst = objReporteVentaWCF.DocVenta_ReporteVenta_VendedorZona(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, null, firstDayOfMonth, lastDayOfMonth).ToList();


                grdDocVenta.DataSource = lst;
                grdDocVenta.DataBind();



                lblTitulo.Text = "Reporte de ventas (" + rmyReporte.SelectedDate.Value.Year.ToString() + "-" + rmyReporte.SelectedDate.Value.Month.ToString() + ")";

                ViewState["lstDocVenta"] = JsonHelper.JsonSerializer(lst);
                ViewState["fechaInicio"] = firstDayOfMonth.ToString("dd/MM/yyyy");
                ViewState["fechaFinal"] = lastDayOfMonth.ToString("dd/MM/yyyy");
                ViewState["lstReporte"] = JsonHelper.JsonSerializer(lst);

                this.grdDocVenta.ClientSettings.Scrolling.FrozenColumnsCount = 2;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (!Page.IsPostBack)
                {
                    LoginWCFClient objLoginWCF = new LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idUsuario);

                    if (string.IsNullOrEmpty(Request.QueryString["fechaInicio"]))
                    {
                        rmyReporte.SelectedDate = DateTime.Now;
                    }
                    else
                    {
                        IFormatProvider culture = new System.Globalization.CultureInfo("es-PE", true);
                        rmyReporte.SelectedDate = DateTime.Parse(Request.QueryString["fechaInicio"], culture, System.Globalization.DateTimeStyles.AssumeLocal);
                    }
                    ReporteVenta_Listar();
                }
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

        protected void ramReporteVenta_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Argument.Split(',')[0] == "ChangePageSize")
                {
                    grdDocVenta.Height = new Unit(e.Argument.Split(',')[1] + "px");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdDocVenta_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                grdDocVenta.DataSource = JsonHelper.JsonDeserialize<List<gsDocVenta_ReporteVenta_VendedorResult>>((string)ViewState["lstReporte"]);
                grdDocVenta.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdDocVenta_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.CommandName == "Grafico")
                {
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowReportbySalesman('" + e.CommandArgument.ToString() + "','" + 
                    //    (string)ViewState["fechaInicio"] + "','" + (string)ViewState["fechaFinal"] + "');", true);
                    Response.Redirect("~/Comercial/Reportes/ReporteVenta/frmReporteVenta_Vendedor.aspx?ID_Vendedor=" + e.CommandArgument.ToString()
                        + "&fechaInicio=" + (string)ViewState["fechaInicio"] + "&fechaFinal=" + (string)ViewState["fechaFinal"]);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdDocVenta_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if(e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                item["CostoVentaReal"].BackColor = System.Drawing.ColorTranslator.FromHtml("#D6EAF8");
                item["Rentabilidad"].BackColor = System.Drawing.ColorTranslator.FromHtml("#D6EAF8");

                item["CostoVentaCalculado"].BackColor = System.Drawing.ColorTranslator.FromHtml("#F7DC6F");
                item["RentabilidadCalculado"].BackColor = System.Drawing.ColorTranslator.FromHtml("#F7DC6F");

                item["CostoVentaEstandar"].BackColor = System.Drawing.ColorTranslator.FromHtml("#EDBB99");
                item["RentabilidadEstandar"].BackColor = System.Drawing.ColorTranslator.FromHtml("#EDBB99");
            }

            if (e.Item is GridFooterItem)
            {
                GridFooterItem footerItem = e.Item as GridFooterItem;
                string strValorVenta = footerItem["ValorVenta"].Text;
                strValorVenta = strValorVenta.Substring(1, strValorVenta.Length - 1);
                strValorVenta = strValorVenta.Replace(",", string.Empty);
                string strValorPlanificado = footerItem["ValorPlanificado"].Text;
                strValorPlanificado = strValorPlanificado.Substring(1, strValorPlanificado.Length - 1);
                strValorPlanificado = strValorPlanificado.Replace(",", string.Empty);
                
                string strCostoVentaR = footerItem["CostoVentaReal"].Text;
                strCostoVentaR = strCostoVentaR.Substring(1, strCostoVentaR.Length - 1);
                strCostoVentaR = strCostoVentaR.Replace(",", string.Empty);

                string strCostoVentaC = footerItem["CostoVentaCalculado"].Text;
                strCostoVentaC = strCostoVentaC.Substring(1, strCostoVentaC.Length - 1);
                strCostoVentaC = strCostoVentaC.Replace(",", string.Empty);

                string strCostoVentaE = footerItem["CostoVentaEstandar"].Text;
                strCostoVentaE = strCostoVentaE.Substring(1, strCostoVentaC.Length - 1);
                strCostoVentaE = strCostoVentaE.Replace(",", string.Empty);

                if (!string.IsNullOrEmpty(strValorPlanificado))
                {
                    if (decimal.Parse(strValorPlanificado) > 0)
                        footerItem["Avance"].Text = Math.Round((decimal.Parse(strValorVenta) / decimal.Parse(strValorPlanificado) * 100), 0).ToString() + "%";
                    else
                        footerItem["Avance"].Text = "100%";
                }

                if (!string.IsNullOrEmpty(strValorVenta))
                {
                    if (decimal.Parse(strValorVenta) > 0)
                        footerItem["Rentabilidad"].Text = Math.Round(((decimal.Parse(strValorVenta) - decimal.Parse(strCostoVentaR)) * 100 / decimal.Parse(strValorVenta)), 0).ToString() + "%";
                    else
                        footerItem["Rentabilidad"].Text = "100%";
                }

                if (!string.IsNullOrEmpty(strValorVenta))
                {
                    if (decimal.Parse(strValorVenta) > 0)
                        footerItem["RentabilidadCalculado"].Text = Math.Round(((decimal.Parse(strValorVenta) - decimal.Parse(strCostoVentaC)) * 100 / decimal.Parse(strValorVenta)), 0).ToString() + "%";
                    else
                        footerItem["RentabilidadCalculado"].Text = "100%";
                }

                if (!string.IsNullOrEmpty(strValorVenta))
                {
                    if (decimal.Parse(strValorVenta) > 0)
                        footerItem["RentabilidadEstandar"].Text = Math.Round(((decimal.Parse(strValorVenta) - decimal.Parse(strCostoVentaE)) * 100 / decimal.Parse(strValorVenta)), 0).ToString() + "%";
                    else
                        footerItem["RentabilidadEstandar"].Text = "100%";
                }

                //if (!string.IsNullOrEmpty(strValorVenta))
                //{
                //    if (decimal.Parse(strValorVenta) > 0)
                //        footerItem["RentabilidadCalculado"].Text = Math.Round(((decimal.Parse(strValorVenta) - decimal.Parse(strCostoVenta)) * 100 / decimal.Parse(strValorVenta)), 0).ToString() + "%";
                //    else
                //        footerItem["RentabilidadCalculado"].Text = "100%";
                //}
                //



            }
        }

        protected void ibExcel_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                string alternateText = (sender as ImageButton).AlternateText;

                grdDocVenta.ExportSettings.FileName = "ReporteVenta_Vendedor" + ((string)ViewState["fechaInicio"]).Split('/')[2] +
                    ((string)ViewState["fechaInicio"]).Split('/')[1] + ((string)ViewState["fechaInicio"]).Split('/')[0];
                grdDocVenta.ExportSettings.ExportOnlyData = true;
                grdDocVenta.ExportSettings.IgnorePaging = true;
                grdDocVenta.ExportSettings.OpenInNewWindow = true;
                grdDocVenta.MasterTableView.ExportToExcel();

                Page.Response.ClearHeaders();
                Page.Response.ClearContent();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }
        }
    }
}