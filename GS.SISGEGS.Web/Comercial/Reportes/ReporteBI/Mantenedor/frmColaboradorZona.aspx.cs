using GS.SISGEGS.DM;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.ItemWCF;
using GS.SISGEGS.Web.ReporteVentaWCF;
using GS.SISGEGS.Web.EmpresaWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using GS.SISGEGS.Web.Helpers;
using System.Drawing;
using xi = Telerik.Web.UI.ExportInfrastructure;
using Telerik.Web.UI.GridExcelBuilder;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Windows;
using Telerik.Windows.Documents.FormatProviders;
using Telerik.Windows.Documents;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Diagnostics;
using System.Net;
using GS.SISGEGS.Web.LoginWCF;
using ICSharpCode.SharpZipLib;
using ICSharpCode;
using ICSharpCode.SharpZipLib.Zip;
using GS.SISGEGS.Web.ReportesRRHHWCF;

namespace GS.SISGEGS.Web.Comercial.Reportes.ReporteBI.Mantenedor
{
    public partial class frmColaboradorZona : System.Web.UI.Page
    {
        private void Reporte_Cargar(int idUsuario)
        {
            ReporteVentaWCFClient objVentas = new ReporteVentaWCFClient(); 

            try
            {
                cboReporte.DataSource = objVentas.ReportesIntranet_Lista(idUsuario);
                cboReporte.DataBind();
                cboReporte.Items.Insert(0, new RadComboBoxItem("SELECCIONAR", "0"));
                cboReporte.SelectedIndex = 0;
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
                if (!Page.IsPostBack) {

                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    Empresa_Cargar();
                    
                    cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();
                    Reporte_Cargar(0);


              

                }
            }

            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void Personal_Listar(int idEmpresa, int idUsario, string codigoEmpresa, string codigoCargo, string descripcion, int reporte)
        {
            ReporteVentaWCFClient objReporteVenta = new ReporteVentaWCFClient(); 

            try
            {

                List<PermisosUsuarios_ListarResult> lstPersonal = objReporteVenta.PermisosUsuarios_ListarResult(idEmpresa, idUsario, descripcion, reporte).ToList();
                grdPersonal.DataSource = lstPersonal;
                grdPersonal.DataBind();
                lblPersonalDS.Text = "1";

                ViewState["lstPersonal"] = JsonHelper.JsonSerializer(lstPersonal);

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void Empresa_Cargar()
        {
            EmpresaWCFClient objEmpresaWCF = new EmpresaWCFClient();
            try
            {
                cboEmpresa.DataSource = objEmpresaWCF.Empresa_Listar(0, null);
                cboEmpresa.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdPersonal_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (lblPersonalDS.Text == "1")
                {
                    List<PermisosUsuarios_ListarResult> lstPersonal = JsonHelper.JsonDeserialize<List<PermisosUsuarios_ListarResult>>((string)ViewState["lstPersonal"]);
                    grdPersonal.DataSource = lstPersonal;
                    grdPersonal.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdPersonal_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.CommandName == "Editar")
                {
                    PermisosUsuarios_ListarResult objPersonal = JsonHelper.JsonDeserialize<List<PermisosUsuarios_ListarResult>>((string)ViewState["lstPersonal"]).Find(x => x.idPermiso.ToString() == e.CommandArgument.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreate('" + JsonHelper.JsonSerializer(objPersonal) + "');", true);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdPersonal_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                  

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
                Personal_Listar(int.Parse(cboEmpresa.SelectedValue), ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, cboEmpresa.SelectedValue, null, txtBuscar.Text, int.Parse(cboReporte.SelectedValue));

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdPersonal_selectedindexchanged(object sender, EventArgs e)
        {
            try
            {
                //GridDataItem item = (sender as Button).NamingContainer as GridDataItem;
                

                GridDataItem gridItem = (GridDataItem)grdPersonal.SelectedItems[0];
                int myId = Convert.ToInt32(gridItem.GetDataKeyValue("idPermiso").ToString());
                //int idPermiso = Convert.ToInt32(gridItem["idPermiso"].Text);

                ZonaPersonal_Detalle(myId);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void ZonaPersonal_Detalle(int idPermiso)
        {
            ReporteVentaWCFClient objVentas = new ReporteVentaWCFClient(); 
         
            List<ZonasPermisos_ListarResult> lstdetalle;

            try
            {
                lstdetalle = objVentas.ZonaPersonal_Listar(int.Parse(cboEmpresa.SelectedValue), ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario , idPermiso).ToList();
                grdZonas.DataSource = lstdetalle;
                grdZonas.DataBind();
                lblZonaDS.Text = "1"; 
                ViewState["lstDetalleZona"] = JsonHelper.JsonSerializer(lstdetalle);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }


        protected void grdZonas_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.CommandName == "Editar")
                {
                    ZonasPermisos_ListarResult objZona = JsonHelper.JsonDeserialize<List<ZonasPermisos_ListarResult>>((string)ViewState["lstDetalleZona"]).Find(x => x.id.ToString() == e.CommandArgument.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreateZona('" + JsonHelper.JsonSerializer(objZona) + "');", true);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdZonas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (lblZonaDS.Text == "1")
                {
                    List<ZonasPermisos_ListarResult> lstZona = JsonHelper.JsonDeserialize<List<ZonasPermisos_ListarResult>>((string)ViewState["lstDetalleZona"]);
                    grdZonas.DataSource = lstZona;
                    grdZonas.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }



        protected void grdUnidades_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.CommandName == "Editar")
                {
                    //ZonasPermisos_ListarResult objZona = JsonHelper.JsonDeserialize<List<ZonasPermisos_ListarResult>>((string)ViewState["lstDetalleZona"]).Find(x => x.id.ToString() == e.CommandArgument.ToString());
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreateZona('" + JsonHelper.JsonSerializer(objZona) + "');", true);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdUnidades_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (lblZonaDS.Text == "1")
                {
                    //List<ZonasPermisos_ListarResult> lstZona = JsonHelper.JsonDeserialize<List<ZonasPermisos_ListarResult>>((string)ViewState["lstDetalleZona"]);
                    //grdZonas.DataSource = lstZona;
                    //grdZonas.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdReporte_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.CommandName == "Editar")
                {
                    //ZonasPermisos_ListarResult objZona = JsonHelper.JsonDeserialize<List<ZonasPermisos_ListarResult>>((string)ViewState["lstDetalleZona"]).Find(x => x.id.ToString() == e.CommandArgument.ToString());
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreateZona('" + JsonHelper.JsonSerializer(objZona) + "');", true);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdReporte_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (lblZonaDS.Text == "1")
                {
                    //List<ZonasPermisos_ListarResult> lstZona = JsonHelper.JsonDeserialize<List<ZonasPermisos_ListarResult>>((string)ViewState["lstDetalleZona"]);
                    //grdZonas.DataSource = lstZona;
                    //grdZonas.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }



        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreate('');", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void ramColaborador_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (e.Argument.Split(',')[0] == "Registro")
                {
                    int idPermiso = Convert.ToInt32(e.Argument.Split(',')[1]); 

                    grdPersonal.MasterTableView.SortExpressions.Clear();
                    grdPersonal.MasterTableView.GroupByExpressions.Clear();

                    Personal_Listar(int.Parse(cboEmpresa.SelectedValue), ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario , cboEmpresa.SelectedValue, null, txtBuscar.Text, int.Parse(cboReporte.SelectedValue));

                    ZonaPersonal_Detalle(idPermiso);

                    lblMensaje.Text = "Se realizo el registro del Personal";
                    lblMensaje.CssClass = "mensajeExito";
                }

                if (e.Argument.Split(',')[0] == "UpdateZona")
                {
                    int idPermiso = Convert.ToInt32(e.Argument.Split(',')[1]);

                    grdPersonal.MasterTableView.SortExpressions.Clear();
                    grdPersonal.MasterTableView.GroupByExpressions.Clear();


                    Personal_Listar(int.Parse(cboEmpresa.SelectedValue), ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, cboEmpresa.SelectedValue, null, txtBuscar.Text, int.Parse(cboReporte.SelectedValue));

                    ZonaPersonal_Detalle(idPermiso);

                    lblMensaje.Text = "Se realizo el registro del Personal";
                    lblMensaje.CssClass = "mensajeExito";
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }


    }
}