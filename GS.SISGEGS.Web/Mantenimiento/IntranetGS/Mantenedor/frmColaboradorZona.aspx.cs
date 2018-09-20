using GS.SISGEGS.DM;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.ItemWCF;
using GS.SISGEGS.Web.ReporteVentaWCF;
using GS.SISGEGS.Web.CobranzasWCF;
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

namespace GS.SISGEGS.Web.Mantenimiento.IntranetGS.Mantenedor
{
    public partial class frmColaboradorZona : System.Web.UI.Page
    {
        private void Reporte_Cargar(int idUsuario)
        {
            ReporteVentaWCFClient objVentas = new ReporteVentaWCFClient();
            List<ReportesIntranet_ListarResult> lista = new List<ReportesIntranet_ListarResult>(); 

            try
            {
                lista = objVentas.ReportesIntranet_Lista(idUsuario).ToList().FindAll(x=> x.NombreReporte.Contains("Cobranza"));

                cboReporte.DataSource = lista; 
                cboReporte.DataBind();
                cboReporte.Items.Insert(0, new RadComboBoxItem("SELECCIONAR", "0"));
                cboReporte.SelectedIndex = 1;
             
                cboReporte.Enabled = false; 
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
           
            CobranzasWCFClient objCorbanza = new CobranzasWCFClient(); 
            try
            {

                List<ZonasSectorista_ListarResult> lstPersonal = objCorbanza.ZonasSectorista_Listar(idEmpresa, idUsario, descripcion, reporte).ToList();
                grdPersonal.DataSource = lstPersonal;
                grdPersonal.DataBind();
                lblPersonalDS.Text = "1";

                ViewState["lstPersonal"] = JsonHelper.JsonSerializer(lstPersonal);
                lblMensaje.Text = "Se encontraron "+ lstPersonal.Count().ToString() + " registros ";
                lblMensaje.CssClass = "mensajeExito";
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
                    List<ZonasSectorista_ListarResult> lstPersonal = JsonHelper.JsonDeserialize<List<ZonasSectorista_ListarResult>>((string)ViewState["lstPersonal"]);
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
                    ZonasSectorista_ListarResult objPersonal = JsonHelper.JsonDeserialize<List<ZonasSectorista_ListarResult>>((string)ViewState["lstPersonal"]).Find(x => x.ID_Agenda.ToString() == e.CommandArgument.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreate('" + JsonHelper.JsonSerializer(objPersonal) + "');", true);
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
                string ID_Agenda = gridItem.GetDataKeyValue("ID_Agenda").ToString() ;
                //int idPermiso = Convert.ToInt32(gridItem["idPermiso"].Text);

                ZonaPersonal_Detalle(ID_Agenda);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void ZonaPersonal_Detalle(string id_agenda)
        {
            CobranzasWCFClient objCorbanza = new CobranzasWCFClient();

            List<ZonasSectoristaPermiso_ListarResult> lstdetalle;

            try
            {
                lstdetalle = objCorbanza.ZonasSectoristaPermiso_Listar(int.Parse(cboEmpresa.SelectedValue), ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario , id_agenda).ToList();

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
                    ZonasSectoristaPermiso_ListarResult objZona = JsonHelper.JsonDeserialize<List<ZonasSectoristaPermiso_ListarResult>>((string)ViewState["lstDetalleZona"]).Find(x => x.ID_Zona.ToString() == e.CommandArgument.ToString());
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
                    List<ZonasSectoristaPermiso_ListarResult> lstZonas = JsonHelper.JsonDeserialize<List<ZonasSectoristaPermiso_ListarResult>>((string)ViewState["lstDetalleZona"]);
                    grdZonas.DataSource = lstZonas;
                    grdZonas.DataBind();
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
                    string id_agenda =  e.Argument.Split(',')[1] ; 

                    grdPersonal.MasterTableView.SortExpressions.Clear();
                    grdPersonal.MasterTableView.GroupByExpressions.Clear();

                    Personal_Listar(int.Parse(cboEmpresa.SelectedValue), ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario , cboEmpresa.SelectedValue, null, txtBuscar.Text, int.Parse(cboReporte.SelectedValue));

                    ZonaPersonal_Detalle(id_agenda);

                    lblMensaje.Text = "Se realizo el registro del Personal";
                    lblMensaje.CssClass = "mensajeExito";
                }

                if (e.Argument.Split(',')[0] == "UpdateZona")
                {
                    string  id_Agenda =  e.Argument.Split(',')[1] ;

                    grdPersonal.MasterTableView.SortExpressions.Clear();
                    grdPersonal.MasterTableView.GroupByExpressions.Clear();


                    Personal_Listar(int.Parse(cboEmpresa.SelectedValue), ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, cboEmpresa.SelectedValue, null, txtBuscar.Text, int.Parse(cboReporte.SelectedValue));

                    ZonaPersonal_Detalle(id_Agenda);

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