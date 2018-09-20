using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.DM;
using GS.SISGEGS.Web.ReporteVentaWCF;
using Telerik.Web.UI;
using GS.SISGEGS.Web.LoginWCF;

namespace GS.SISGEGS.Web.Comercial.Reportes.ReporteVenta
{
    public partial class frmReporteVenta_Producto : System.Web.UI.Page
    {
        private void ReporteProducto_Cargar(int? ID_Marca)
        {
            ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient();
            int count = 0;
            rhcProducto.PlotArea.Series[0].Items.Clear();
            rhcProducto.PlotArea.Series[1].Items.Clear();
            rhcProducto.PlotArea.XAxis.Items.Clear();

            rhcProdVendedor.PlotArea.Series[0].Items.Clear();
            rhcProdVendedor.PlotArea.Series[1].Items.Clear();
            rhcProdVendedor.PlotArea.XAxis.Items.Clear();

            rhcProdCliente.PlotArea.Series[0].Items.Clear();
            rhcProdCliente.PlotArea.Series[1].Items.Clear();
            rhcProdCliente.PlotArea.XAxis.Items.Clear();

            try {
                DateTime primerDia = new DateTime(((DateTime)ViewState["periodo"]).Year, ((DateTime)ViewState["periodo"]).Month, 1);

                List<gsDocVenta_ReporteVenta_MarcaProductoResult> lstProducto = objReporteVentaWCF.DocVenta_ReporteVenta_MarcaProducto(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, ID_Marca, null, null, primerDia, primerDia.AddMonths(1).AddDays(-1)).ToList();

                foreach(gsDocVenta_ReporteVenta_MarcaProductoResult producto in lstProducto)
                {
                    if (count <= 20)
                    {
                        if (producto.Descripcion.Length > 30)
                            rhcProducto.PlotArea.XAxis.Items.Add(producto.Descripcion.Substring(0, 30).Replace("'", string.Empty));
                        else
                            rhcProducto.PlotArea.XAxis.Items.Add(producto.Descripcion.Replace("'", string.Empty));

                        SeriesItem item = new SeriesItem();

                        item = new SeriesItem();
                        item.Name = producto.Descripcion.Replace("'", string.Empty);
                        item.YValue = Math.Round((decimal)producto.ValorVenta / 1000, 2);
                        this.rhcProducto.PlotArea.Series[0].Items.Add(item);

                        item = new SeriesItem();
                        item.Name = producto.Descripcion.Replace("'", string.Empty);
                        item.YValue = Math.Round((decimal)producto.ValorPlanificado / 1000, 2);
                        this.rhcProducto.PlotArea.Series[1].Items.Add(item);
                        count++;
                    }
                    else {
                        break;
                    }
                }

                if (ID_Marca != null) {
                    grdCliente.DataSource = objReporteVentaWCF.DocVenta_ReporteVenta_MarcaCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, ID_Marca, primerDia, primerDia.AddMonths(1).AddDays(-1));
                    grdCliente.DataBind();

                    grdVendedor.DataSource = objReporteVentaWCF.DocVenta_ReporteVenta_MarcaVendedor(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, ID_Marca, primerDia, primerDia.AddMonths(1).AddDays(-1));
                    grdVendedor.DataBind();
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private void ReporteMarca_Cargar() {
            ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient();
            try {
                DateTime primerDia = new DateTime(rmpPeriodo.SelectedDate.Value.Year, rmpPeriodo.SelectedDate.Value.Month, 1);                

                grdProducto.DataSource = objReporteVentaWCF.DocVenta_ReporteVenta_Marca(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, null, primerDia, primerDia.AddMonths(1).AddDays(-1));
                grdProducto.DataBind();

                ViewState["periodo"] = rmpPeriodo.SelectedDate.Value;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try {
                if (!Page.IsPostBack) {
                    LoginWCFClient objLoginWCF = new LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idUsuario);

                    rmpPeriodo.SelectedDate = DateTime.Today;
                    ReporteMarca_Cargar();
                    ReporteProducto_Cargar(null);
                }
            }
            catch (Exception ex) {
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
                ReporteMarca_Cargar();
                ReporteProducto_Cargar(null);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                ReporteProducto_Cargar(int.Parse(grdProducto.SelectedItems[0].Cells[9].Text.ToString()));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnVerTodo_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                ReporteMarca_Cargar();
                ReporteProducto_Cargar(null);
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
                if (e.Item is GridFooterItem)
                {
                    GridFooterItem footerItem = e.Item as GridFooterItem;
                    string strValorVenta = footerItem["ValorVenta"].Text;
                    strValorVenta = strValorVenta.Replace(",", string.Empty).Replace("$",string.Empty).Trim();

                    string strValorPlanificado = footerItem["ValorPlanificado"].Text;
                    strValorPlanificado = strValorPlanificado.Replace(",", string.Empty).Replace("$", string.Empty).Trim();

                    string strCostoVenta = footerItem["CostoVenta"].Text;
                    strCostoVenta = strCostoVenta.Replace(",", string.Empty).Replace("$", string.Empty).Trim();

                    if (decimal.Parse(strValorPlanificado) > 0)
                        footerItem["Avance"].Text = Math.Round((decimal.Parse(strValorVenta) / decimal.Parse(strValorPlanificado) * 100), 0).ToString() + "%";
                    else
                        footerItem["Avance"].Text = "100%";

                    if (decimal.Parse(strValorVenta) > 0)
                        footerItem["Rentabilidad"].Text = Math.Round(((decimal.Parse(strValorVenta) - decimal.Parse(strCostoVenta)) * 100 / decimal.Parse(strValorVenta)), 0).ToString() + "%";
                    else
                        footerItem["Rentabilidad"].Text = "100%";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdCliente_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Item is GridFooterItem)
                {
                    GridFooterItem footerItem = e.Item as GridFooterItem;
                    string strValorVenta = footerItem["ValorVenta"].Text;
                    strValorVenta = strValorVenta.Replace(",", string.Empty).Replace("$", string.Empty).Trim();

                    string strValorPlanificado = footerItem["ValorPlanificado"].Text;
                    strValorPlanificado = strValorPlanificado.Replace(",", string.Empty).Replace("$", string.Empty).Trim();

                    string strCostoVenta = footerItem["CostoVenta"].Text;
                    strCostoVenta = strCostoVenta.Replace(",", string.Empty).Replace("$", string.Empty).Trim();

                    if (decimal.Parse(strValorPlanificado) > 0)
                        footerItem["Avance"].Text = Math.Round((decimal.Parse(strValorVenta) / decimal.Parse(strValorPlanificado) * 100), 0).ToString() + "%";
                    else
                        footerItem["Avance"].Text = "100%";

                    if (decimal.Parse(strValorVenta) > 0)
                        footerItem["Rentabilidad"].Text = Math.Round(((decimal.Parse(strValorVenta) - decimal.Parse(strCostoVenta)) * 100 / decimal.Parse(strValorVenta)), 0).ToString() + "%";
                    else
                        footerItem["Rentabilidad"].Text = "100%";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdVendedor_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Item is GridFooterItem)
                {
                    GridFooterItem footerItem = e.Item as GridFooterItem;
                    string strValorVenta = footerItem["ValorVenta"].Text;
                    strValorVenta = strValorVenta.Replace(",", string.Empty).Replace("$", string.Empty).Trim();

                    string strValorPlanificado = footerItem["ValorPlanificado"].Text;
                    strValorPlanificado = strValorPlanificado.Replace(",", string.Empty).Replace("$", string.Empty).Trim();

                    string strCostoVenta = footerItem["CostoVenta"].Text;
                    strCostoVenta = strCostoVenta.Replace(",", string.Empty).Replace("$", string.Empty).Trim();

                    if (decimal.Parse(strValorPlanificado) > 0)
                        footerItem["Avance"].Text = Math.Round((decimal.Parse(strValorVenta) / decimal.Parse(strValorPlanificado) * 100), 0).ToString() + "%";
                    else
                        footerItem["Avance"].Text = "100%";

                    if (decimal.Parse(strValorVenta) > 0)
                        footerItem["Rentabilidad"].Text = Math.Round(((decimal.Parse(strValorVenta) - decimal.Parse(strCostoVenta)) * 100 / decimal.Parse(strValorVenta)), 0).ToString() + "%";
                    else
                        footerItem["Rentabilidad"].Text = "100%";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient();
            int count = 0;
            rhcProdCliente.PlotArea.Series[0].Items.Clear();
            rhcProdCliente.PlotArea.Series[1].Items.Clear();
            rhcProdCliente.PlotArea.XAxis.Items.Clear();
            try
            {
                DateTime primerDia = new DateTime(((DateTime)ViewState["periodo"]).Year, ((DateTime)ViewState["periodo"]).Month, 1);

                List<gsDocVenta_ReporteVenta_MarcaProductoResult> lstProducto = objReporteVentaWCF.DocVenta_ReporteVenta_MarcaProducto(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, int.Parse(grdCliente.SelectedItems[0].Cells[9].Text.ToString()), null,
                    grdCliente.SelectedItems[0].Cells[10].Text.ToString(), primerDia, primerDia.AddMonths(1).AddDays(-1)).ToList();

                foreach (gsDocVenta_ReporteVenta_MarcaProductoResult producto in lstProducto)
                {
                    if (producto.Descripcion.Length > 30)
                        rhcProdCliente.PlotArea.XAxis.Items.Add(producto.Descripcion.Substring(0, 30).Replace("'", string.Empty));
                    else
                        rhcProdCliente.PlotArea.XAxis.Items.Add(producto.Descripcion.Replace("'", string.Empty));

                    SeriesItem item = new SeriesItem();

                    item = new SeriesItem();
                    item.Name = producto.Descripcion.Replace("'", string.Empty);
                    item.YValue = Math.Round((decimal)producto.ValorVenta / 1000, 2);
                    this.rhcProdCliente.PlotArea.Series[0].Items.Add(item);

                    item = new SeriesItem();
                    item.Name = producto.Descripcion.Replace("'", string.Empty);
                    item.YValue = Math.Round((decimal)producto.ValorPlanificado / 1000, 2);
                    this.rhcProdCliente.PlotArea.Series[1].Items.Add(item);
                    count++;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdVendedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient();
            int count = 0;
            rhcProdVendedor.PlotArea.Series[0].Items.Clear();
            rhcProdVendedor.PlotArea.Series[1].Items.Clear();
            rhcProdVendedor.PlotArea.XAxis.Items.Clear();
            try
            {
                DateTime primerDia = new DateTime(((DateTime)ViewState["periodo"]).Year, ((DateTime)ViewState["periodo"]).Month, 1);

                List<gsDocVenta_ReporteVenta_MarcaProductoResult> lstProducto = objReporteVentaWCF.DocVenta_ReporteVenta_MarcaProducto(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, int.Parse(grdVendedor.SelectedItems[0].Cells[10].Text.ToString()), int.Parse(grdVendedor.SelectedItems[0].Cells[11].Text.ToString()),
                    null, primerDia, primerDia.AddMonths(1).AddDays(-1)).ToList();

                foreach (gsDocVenta_ReporteVenta_MarcaProductoResult producto in lstProducto)
                {
                    if (producto.Descripcion.Length > 30)
                        rhcProdVendedor.PlotArea.XAxis.Items.Add(producto.Descripcion.Substring(0, 30).Replace("'", string.Empty));
                    else
                        rhcProdVendedor.PlotArea.XAxis.Items.Add(producto.Descripcion.Replace("'", string.Empty));

                    SeriesItem item = new SeriesItem();

                    item = new SeriesItem();
                    item.Name = producto.Descripcion.Replace("'", string.Empty);
                    item.YValue = Math.Round((decimal)producto.ValorVenta / 1000, 2);
                    this.rhcProdVendedor.PlotArea.Series[0].Items.Add(item);

                    item = new SeriesItem();
                    item.Name = producto.Descripcion.Replace("'", string.Empty);
                    item.YValue = Math.Round((decimal)producto.ValorPlanificado / 1000, 2);
                    this.rhcProdVendedor.PlotArea.Series[1].Items.Add(item);
                    count++;
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