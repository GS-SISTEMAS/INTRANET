using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.CierreCostoWCF;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.Helpers;
using Telerik.Web.UI;
using System.Drawing;

namespace GS.SISGEGS.Web.Contabilidad.Costos
{
    public partial class frmControlCostos : System.Web.UI.Page
    {
        private void Periodos_ComboBox() {
            CierreCostoWCFClient objCierreCostoWCF = new CierreCostoWCFClient();
            try {
                Usuario_LoginResult usuario = (Usuario_LoginResult)Session["Usuario"];
                cboPeriodo.DataSource = objCierreCostoWCF.CierreCosto_ComboBox(usuario.idEmpresa, usuario.codigoUsuario);
                cboPeriodo.DataBind();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private void Productos_Cargar(){
            CierreCostoWCFClient objCierreCostoWCF = new CierreCostoWCFClient();
            List<gsDocVenta_ControlCosto_MarcaProductoResult> lstProductos;
            try {
                int periodo = int.Parse(cboPeriodo.SelectedValue);
                DateTime fechaInicio = new DateTime((periodo - periodo % 100) / 100, periodo % 100, 1);
                DateTime fechaFinal = fechaInicio.AddMonths(1).AddDays(-1);

                Usuario_LoginResult usuario = (Usuario_LoginResult)Session["Usuario"];
                lstProductos = objCierreCostoWCF.DocVenta_ControlCosto_MarcaProducto(usuario.idEmpresa, usuario.codigoUsuario, fechaInicio, fechaFinal, null).ToList();
                grdControlCostos.DataSource = lstProductos;
                grdControlCostos.DataBind();

                ViewState["lstProductos"] = JsonHelper.JsonSerializer(lstProductos);
                ViewState["periodo"] = periodo;
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
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    cboPeriodo.SelectedIndex = 0;
                    Periodos_ComboBox();
                    Productos_Cargar();
                    lblMensaje.Text = "Se cargo la información con éxito";
                    lblMensaje.CssClass = "mensajeExito";
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
                Productos_Cargar();
                lblMensaje.Text = "Se realizó la busqueda con éxito";
                lblMensaje.CssClass = "mensajeExito";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdControlCostos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                grdControlCostos.DataSource = JsonHelper.JsonDeserialize<List<gsDocVenta_ControlCosto_MarcaProductoResult>>((string)ViewState["lstProductos"]);
                lblMensaje.Text = "Se filtro la información con éxito";
                lblMensaje.CssClass = "mensajeExito";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdControlCostos_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Item is GridDataItem) {
                    GridDataItem item = (GridDataItem)e.Item;
                    if (double.Parse(item["PSTD"].Text) > 10 || double.Parse(item["PSTD"].Text) < -10)
                        item.BackColor = ColorTranslator.FromHtml("#FFAAAA");
                    if ((double.Parse(item["PSTD"].Text) > 7 && double.Parse(item["PSTD"].Text) <= 10) || (double.Parse(item["PSTD"].Text) >= -10 && double.Parse(item["PSTD"].Text) < -7))
                        item.BackColor = ColorTranslator.FromHtml("#FFFDAA");
                    if ((double.Parse(item["PSTD"].Text) >= 0 && double.Parse(item["PSTD"].Text) <= 7) || (double.Parse(item["PSTD"].Text) >= -7 && double.Parse(item["PSTD"].Text) <= 0))
                        item.BackColor = ColorTranslator.FromHtml("#85BC5E");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void ibExcel_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                string alternateText = (sender as ImageButton).AlternateText;

                grdControlCostos.ExportSettings.FileName = "ControlCostos_" + DateTime.Now.ToString("yyyyMMddHmm");
                grdControlCostos.ExportSettings.ExportOnlyData = true;
                grdControlCostos.ExportSettings.IgnorePaging = true;
                grdControlCostos.ExportSettings.OpenInNewWindow = true;
                grdControlCostos.MasterTableView.ExportToExcel();

                Page.Response.ClearHeaders();
                Page.Response.ClearContent();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }
        }

        protected void grdControlCostos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.CommandName == "OrdenProd")
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowForm(" + e.CommandArgument.ToString() + "," + ViewState["periodo"] + ");", true);
                if (e.CommandName == "HistControl")
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowHistorial(" + e.CommandArgument.ToString() + "," + ViewState["periodo"] + ");", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }
        }
    }
}