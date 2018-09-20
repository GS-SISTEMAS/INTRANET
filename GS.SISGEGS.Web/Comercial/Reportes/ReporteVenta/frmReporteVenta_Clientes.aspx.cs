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
    public partial class frmReporteVenta_Clientes : System.Web.UI.Page
    {
        private void ReporteVenta_Cliente(int mes, int anho)
        {
            ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient();
            try
            {
                string EstadoCliente = null;
                EstadoCliente = cboEstadoCredito.SelectedItem.Text;
                if (EstadoCliente == "TODOS")
                {
                    EstadoCliente = null;
                }
                DateTime fechaInicio = new DateTime(anho, mes, 1);
                DateTime fechaFinal = fechaInicio.AddMonths(1).AddDays(-1);

                grdCliente.DataSource = objReporteVentaWCF.DocVenta_ReporteVenta_Cliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, null, fechaInicio, fechaFinal, EstadoCliente);
                grdCliente.DataBind();
                //lblTitulo.Text = "Reporte " + fechaInico.Year.ToString() + "-" + fechaInico.Month.ToString() + " de " + rv.Vendedor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void EstadoCliente_Cargar()
        {
            try
            {
                cboEstadoCredito.Items.Insert(0, new RadComboBoxItem("TODOS", string.Empty));
                cboEstadoCredito.Items.Insert(1, new RadComboBoxItem("LEGAL", string.Empty));
                cboEstadoCredito.Items.Insert(2, new RadComboBoxItem("NORMAL", string.Empty));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
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
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    EstadoCliente_Cargar(); 
                    rmpPeriodo.SelectedDate = DateTime.Now;
                    ReporteVenta_Cliente(rmpPeriodo.SelectedDate.Value.Month, rmpPeriodo.SelectedDate.Value.Year);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void ramRepCliente_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            //if (Session["Usuario"] == null)
            //    Response.Redirect("~/Security/frmCerrar.aspx");

            //try
            //{
            //    if (e.Argument.Split(',')[0] == "ChangePageSize")
            //    {
            //        rmpRepCliente.Height = new Unit(e.Argument.Split(',')[1] + "px");
            //        decimal altura = 470 + decimal.Parse(e.Argument.Split(',')[1]) - 531;
            //        rhcDiario.Height = new Unit(altura.ToString() + "px");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    lblMensaje.Text = ex.Message;
            //    lblMensaje.CssClass = "mensajeError";
            //}
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {

        }

        //protected void grdProducto_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    if (Session["Usuario"] == null)
        //        Response.Redirect("~/Security/frmCerrar.aspx");

        //    try
        //    {
        //        if (e.Item is GridGroupFooterItem)
        //        {
        //            GridGroupFooterItem footerItem = e.Item as GridGroupFooterItem;
        //            string strValorVenta = footerItem["ValorVenta"].Text;
        //            strValorVenta = strValorVenta.Substring(1, strValorVenta.Length - 1);
        //            strValorVenta = strValorVenta.Replace(",", string.Empty);
        //            string strValorPlanificado = footerItem["ValorPlanificado"].Text;
        //            strValorPlanificado = strValorPlanificado.Substring(1, strValorPlanificado.Length - 1);
        //            strValorPlanificado = strValorPlanificado.Replace(",", string.Empty);
        //            string strCostoVenta = footerItem["CostoVenta"].Text;
        //            strCostoVenta = strCostoVenta.Substring(1, strCostoVenta.Length - 1);
        //            strCostoVenta = strCostoVenta.Replace(",", string.Empty);

        //            if (decimal.Parse(strValorPlanificado) > 0)
        //                footerItem["Avance"].Text = Math.Round((decimal.Parse(strValorVenta) / decimal.Parse(strValorPlanificado)) * 100, 0).ToString() + "%";
        //            else
        //                footerItem["Avance"].Text = "100%";

        //            if (decimal.Parse(strValorVenta) > 0)
        //                footerItem["Rentabiliad"].Text = Math.Round(((decimal.Parse(strValorVenta) - decimal.Parse(strCostoVenta)) * 100 / decimal.Parse(strValorVenta)), 0).ToString() + "%";
        //            else
        //                footerItem["Rentabiliad"].Text = "100%";
        //        }

        //        if (e.Item is GridFooterItem)
        //        {
        //            GridFooterItem footerItem = e.Item as GridFooterItem;
        //            string strValorVenta = footerItem["ValorVenta"].Text;
        //            strValorVenta = strValorVenta.Substring(1, strValorVenta.Length - 1);
        //            strValorVenta = strValorVenta.Replace(",", string.Empty);

        //            string strValorPlanificado = footerItem["ValorPlanificado"].Text;
        //            strValorPlanificado = strValorPlanificado.Substring(1, strValorPlanificado.Length - 1);
        //            strValorPlanificado = strValorPlanificado.Replace(",", string.Empty);

        //            string strCostoVenta = footerItem["CostoVenta"].Text;
        //            strCostoVenta = strCostoVenta.Substring(1, strCostoVenta.Length - 1);
        //            strCostoVenta = strCostoVenta.Replace(",", string.Empty);

        //            if (decimal.Parse(strValorPlanificado) > 0)
        //                footerItem["Avance"].Text = Math.Round((decimal.Parse(strValorVenta) / decimal.Parse(strValorPlanificado) * 100), 0).ToString() + "%";
        //            else
        //                footerItem["Avance"].Text = "100%";

        //            if (decimal.Parse(strCostoVenta) > 0)
        //                footerItem["Rentabiliad"].Text = Math.Round(((decimal.Parse(strValorVenta) - decimal.Parse(strCostoVenta)) * 100 / decimal.Parse(strValorVenta)), 0).ToString() + "%";
        //            else
        //                footerItem["Rentabiliad"].Text = "100%";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMensaje.Text = ex.Message;
        //        lblMensaje.CssClass = "mensajeError";
        //    }
        //}

        //protected void grdProducto_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        //{
        //    if (Session["Usuario"] == null)
        //        Response.Redirect("~/Security/frmCerrar.aspx");

        //    try {
        //        grdProducto.DataSource = JsonHelper.JsonDeserialize<List<gsDocVenta_ReporteVenta_ProductoResult>>((string)ViewState["lstProductos"]);
        //    }
        //    catch (Exception ex) {
        //        lblMensaje.Text = ex.Message;
        //        lblMensaje.CssClass = "mensajeError";
        //    }
        //}

        //protected void ibExcel_Click(object sender, ImageClickEventArgs e)
        //{
        //    if (Session["Usuario"] == null)
        //        Response.Redirect("~/Security/frmCerrar.aspx");

        //    try
        //    {
        //        string alternateText = (sender as ImageButton).AlternateText;

        //        grdProducto.ExportSettings.FileName = "ReporteVenta_Cliente_Producto_" + DateTime.Now.ToString("yyyyMMddhhmm");
        //        grdProducto.ExportSettings.ExportOnlyData = true;
        //        grdProducto.ExportSettings.IgnorePaging = true;
        //        grdProducto.ExportSettings.OpenInNewWindow = true;
        //        grdProducto.MasterTableView.ExportToExcel();

        //        Page.Response.ClearHeaders();
        //        Page.Response.ClearContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMensaje.Text = ex.Message;
        //        lblMensaje.CssClass = "MensajeError";
        //    }
        //}

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                ReporteVenta_Cliente(rmpPeriodo.SelectedDate.Value.Month, rmpPeriodo.SelectedDate.Value.Year);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}