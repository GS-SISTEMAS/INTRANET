using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.DM;
using GS.SISGEGS.Web.ReporteVentaWCF;
using Telerik.Web.UI;
using System.Globalization;
using GS.SISGEGS.Web.LoginWCF;
using GS.SISGEGS.Web.Helpers;

namespace GS.SISGEGS.Web.Comercial.Reportes.ReporteVenta
{
    public partial class frmReporteVenta_Vendedor : System.Web.UI.Page
    {
        private void ReporteVenta_Cliente(string ID_Vendedor, DateTime fechaInico, DateTime fechaFinal)
        {
            ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient();
            rhcParticipacion.PlotArea.Series[0].Items.Clear();
            try
            {
                #region Venta de productos
                List<gsDocVenta_ReporteVenta_ProductoResult> lstProductos = objReporteVentaWCF.DocVenta_ReporteVenta_Producto(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, ID_Vendedor, fechaInico, fechaFinal).ToList();

                grdProducto.DataSource = lstProductos;
                grdProducto.DataBind();

                ViewState["lstProductos"] = JsonHelper.JsonSerializer(lstProductos);
                #endregion

                #region Venta al 80% y 20%
                List<gsDocVenta_ReporteVenta_ClienteResult> lst = objReporteVentaWCF.DocVenta_ReporteVenta_Cliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, ID_Vendedor, fechaInico, fechaFinal, null ).ToList().OrderByDescending(x => x.ValorVenta).ToList();

                decimal sum = (decimal)lst.Sum(i => i.ValorVenta);
                //decimal sumPlanif = (decimal)lst.Sum(i => i.ValorPlanificado);
                decimal acumulado = 0;
                bool paso80 = false;

                foreach (var cliente in lst)
                {
                    SeriesItem item = new SeriesItem();
                    if (sum > 0)
                        if (((int)((cliente.ValorVenta / sum) * 100)) > 0)
                        {
                            item.YValue = ((int)((cliente.ValorVenta / sum) * 100));
                            item.Name = string.Format("{0}<br/>Valor Venta:${1}K<br/>Presupuesto:${2}K<br/>Avance:{3}%<br/>Participación:{4}%",
                                cliente.ID_Cliente + " - " + cliente.Cliente.Replace("'", string.Empty),
                                Math.Round((decimal)cliente.ValorVenta / 1000, 2),
                                Math.Round((decimal)cliente.ValorPlanificado / 1000, 2),
                                cliente.Avance,
                                item.YValue);
                            rhcParticipacion.PlotArea.Series[0].Items.Add(item);
                        }

                    if (cliente.ValorVenta > 0)
                    {
                        acumulado = acumulado + (decimal)cliente.ValorVenta;
                        if (!paso80)
                        {
                            if (cliente.Cliente.Length > 30)
                                rhcCliente80.PlotArea.XAxis.Items.Add(cliente.Cliente.Substring(0, 30).Replace("'", string.Empty));
                            else
                                rhcCliente80.PlotArea.XAxis.Items.Add(cliente.Cliente.Replace("'", string.Empty));

                            item = new SeriesItem();
                            item.Name = cliente.Cliente.Replace("'", string.Empty);
                            item.YValue = Math.Round((decimal)cliente.ValorVenta / 1000, 2);
                            this.rhcCliente80.PlotArea.Series[0].Items.Add(item);

                            item = new SeriesItem();
                            item.Name = cliente.Cliente.Replace("'", string.Empty);
                            item.YValue = Math.Round((decimal)cliente.ValorPlanificado / 1000, 2);
                            this.rhcCliente80.PlotArea.Series[1].Items.Add(item);
                        }
                        else
                        {
                            if (cliente.Cliente.Length > 30)
                                rhcCliente20.PlotArea.XAxis.Items.Add(cliente.Cliente.Substring(0, 30).Replace("'", string.Empty));
                            else
                                rhcCliente20.PlotArea.XAxis.Items.Add(cliente.Cliente.Replace("'", string.Empty));

                            item = new SeriesItem();
                            item.Name = cliente.Cliente.Replace("'", string.Empty);
                            item.YValue = Math.Round((decimal)cliente.ValorVenta / 1000, 2);
                            this.rhcCliente20.PlotArea.Series[0].Items.Add(item);

                            item = new SeriesItem();
                            item.Name = cliente.Cliente.Replace("'", string.Empty);
                            item.YValue = Math.Round((decimal)cliente.ValorPlanificado / 1000, 2);
                            this.rhcCliente20.PlotArea.Series[1].Items.Add(item);
                        }

                        if (acumulado / sum * 100 >= 80)
                            paso80 = true;
                    }
                }
                #endregion

                #region Datos principales
                //Datos principales de la venta
                gsDocVenta_ReporteVenta_VendedorResult rv = objReporteVentaWCF.DocVenta_ReporteVenta_Vendedor(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, ID_Vendedor, fechaInico, fechaFinal).ToList().Single();
                rrgAvance.Pointer.Value = rv.Avance;
                lblPronostico.Text = string.Format("${0}", Math.Round((decimal)rv.ValorPlanificado, 0).ToString("#,#", CultureInfo.InvariantCulture));
                lblValorVenta.Text = string.Format("${0}", Math.Round((decimal)rv.ValorVenta, 0).ToString("#,#", CultureInfo.InvariantCulture));
                lblDiferencia.Text = string.Format("${0}", Math.Round((decimal)rv.Diferencia, 0).ToString("#,#", CultureInfo.InvariantCulture));
                lblAvance.Text = "Avance: " + Math.Round((decimal)rv.Avance, 0).ToString() + "%";
                lblCantTotal.Text = lst.Count.ToString();
                lblCantVenta.Text = lst.FindAll(x => x.ValorVenta > 0).Count.ToString();
                lblCantNoVenta.Text = lst.FindAll(x => x.ValorVenta <= 0).Count.ToString();
                #endregion

                #region Cliente sin venta
                grdCliente.DataSource = lst.FindAll(x => x.ValorVenta <= 0).OrderByDescending(x => x.ValorPlanificado);
                grdCliente.DataBind();
                #endregion

                #region Venta diaria
                List<gsDocVenta_ReporteVenta_FechaResult> lstFecha = objReporteVentaWCF.DocVenta_ReporteVenta_Fecha(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, ID_Vendedor, fechaInico, fechaFinal).ToList();

                acumulado = 0;
                for (DateTime day = fechaInico; DateTime.Compare(day, fechaFinal) <= 0; day = day.AddDays(1))
                {
                    AxisItem xitem = new AxisItem();
                    xitem.LabelText = day.ToString("dd/MM/yyyy");
                    rhcDiario.PlotArea.XAxis.Items.Add(xitem);

                    gsDocVenta_ReporteVenta_FechaResult objVFecha = lstFecha.Find(x => x.Fecha == day);
                    SeriesItem serie1 = new SeriesItem();
                    if (objVFecha != null)
                    {
                        acumulado = acumulado + (decimal)objVFecha.ValorVenta;
                        serie1.YValue = Math.Round((decimal)objVFecha.ValorVenta / 1000, 2);
                    }
                    else
                        serie1.YValue = 0;

                    rhcDiario.PlotArea.Series[0].Items.Add(serie1);
                    SeriesItem serie2 = new SeriesItem();
                    serie2.YValue = Math.Round(acumulado / (decimal)rv.ValorPlanificado * 100, 0);
                    rhcDiario.PlotArea.Series[1].Items.Add(serie2);
                }
                //rhcDiario.PlotArea.Series[1].AxisName = "AdditionalAxis";
                #endregion

                lblTitulo.Text = "Reporte " + fechaInico.Year.ToString() + "-" + fechaInico.Month.ToString() + " de " + rv.Vendedor;
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

                    IFormatProvider culture = new System.Globalization.CultureInfo("es-PE", true);
                    DateTime dtInicio = DateTime.Parse(Request.QueryString["fechaInicio"], culture, System.Globalization.DateTimeStyles.AssumeLocal);
                    DateTime dtFinal = DateTime.Parse(Request.QueryString["fechaFinal"], culture, System.Globalization.DateTimeStyles.AssumeLocal);
                    ReporteVenta_Cliente(Request.QueryString["ID_Vendedor"], dtInicio, dtFinal);

                    lblMensaje.Text = "El reporte del vendedor ha sido cargado.";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                Response.Redirect("~/Comercial/Reportes/ReporteVenta/frmReporteVenta.aspx?fechaInicio=" +
                    Request.QueryString["fechaInicio"] + "&fechaFinal=" + Request.QueryString["fechaFinal"]);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeExito";
            }
        }

        protected void ramRepCliente_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Argument.Split(',')[0] == "ChangePageSize")
                {
                    rmpRepCliente.Height = new Unit(e.Argument.Split(',')[1] + "px");
                    decimal altura = 470 + decimal.Parse(e.Argument.Split(',')[1]) - 531;
                    rhcDiario.Height = new Unit(altura.ToString() + "px");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdProducto_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Item is GridGroupFooterItem)
                {
                    GridGroupFooterItem footerItem = e.Item as GridGroupFooterItem;
                    string strValorVenta = footerItem["ValorVenta"].Text;
                    strValorVenta = strValorVenta.Substring(1, strValorVenta.Length - 1);
                    strValorVenta = strValorVenta.Replace(",", string.Empty);
                    string strValorPlanificado = footerItem["ValorPlanificado"].Text;
                    strValorPlanificado = strValorPlanificado.Substring(1, strValorPlanificado.Length - 1);
                    strValorPlanificado = strValorPlanificado.Replace(",", string.Empty);
                    string strCostoVenta = footerItem["CostoVenta"].Text;
                    strCostoVenta = strCostoVenta.Substring(1, strCostoVenta.Length - 1);
                    strCostoVenta = strCostoVenta.Replace(",", string.Empty);

                    if (decimal.Parse(strValorPlanificado) > 0)
                        footerItem["Avance"].Text = Math.Round((decimal.Parse(strValorVenta) / decimal.Parse(strValorPlanificado)) * 100, 0).ToString() + "%";
                    else
                        footerItem["Avance"].Text = "100%";

                    if (decimal.Parse(strValorVenta) > 0)
                        footerItem["Rentabiliad"].Text = Math.Round(((decimal.Parse(strValorVenta) - decimal.Parse(strCostoVenta)) * 100 / decimal.Parse(strValorVenta)), 0).ToString() + "%";
                    else
                        footerItem["Rentabiliad"].Text = "100%";
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

                    string strCostoVenta = footerItem["CostoVenta"].Text;
                    strCostoVenta = strCostoVenta.Substring(1, strCostoVenta.Length - 1);
                    strCostoVenta = strCostoVenta.Replace(",", string.Empty);

                    if (decimal.Parse(strValorPlanificado) > 0)
                        footerItem["Avance"].Text = Math.Round((decimal.Parse(strValorVenta) / decimal.Parse(strValorPlanificado) * 100), 0).ToString() + "%";
                    else
                        footerItem["Avance"].Text = "100%";

                    if (decimal.Parse(strCostoVenta) > 0)
                        footerItem["Rentabiliad"].Text = Math.Round(((decimal.Parse(strValorVenta) - decimal.Parse(strCostoVenta)) * 100 / decimal.Parse(strValorVenta)), 0).ToString() + "%";
                    else
                        footerItem["Rentabiliad"].Text = "100%";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdProducto_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                grdProducto.DataSource = JsonHelper.JsonDeserialize<List<gsDocVenta_ReporteVenta_ProductoResult>>((string)ViewState["lstProductos"]);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                string alternateText = "ExcelML";

                grdProducto.ExportSettings.FileName = "ReporteVenta_Cliente_Producto_" + DateTime.Now.ToString("yyyyMMddhhmm");
                grdProducto.ExportSettings.ExportOnlyData = true;
                grdProducto.ExportSettings.IgnorePaging = true;
                grdProducto.ExportSettings.OpenInNewWindow = true;
                grdProducto.MasterTableView.ExportToExcel();

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