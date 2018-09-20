using GS.SISGEGS.DM;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.ItemWCF;
using GS.SISGEGS.Web.ComisionesWCF;
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

namespace GS.SISGEGS.Web.Comision.Mantenimiento 
{
    public partial class frmPromotorZona : System.Web.UI.Page
    {
        private void Reporte_Cargar(string IdEmpresa)
        {
            ComisionWCFClient objComision = new ComisionWCFClient();

            try
            {
                cboReporte.DataSource = objComision.combo_Reporte();
                cboReporte.DataBind();
                cboReporte.Items.Insert(0, new RadComboBoxItem("SELECCIONAR", "0"));
                cboReporte.SelectedIndex = 0;
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
                if (!Page.IsPostBack) {

                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    Empresa_Cargar();
                    cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();
                    
                    Reporte_Cargar(cboEmpresa.SelectedValue);
                    cboReporte.SelectedValue = "1"; 
                    cboReporte.Enabled = false; 

                    Zona_Listar(int.Parse(cboEmpresa.SelectedValue), ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, cboEmpresa.SelectedValue, null, txtBuscar.Text, int.Parse(cboReporte.SelectedValue));

                }
            }

            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void Zona_Listar(int idEmpresa, int idUsario, string codigoEmpresa, string codigoCargo, string descripcion, int reporte)
        {

            ComisionWCFClient objZonaWCF = new ComisionWCFClient();
            try
            {
                List<gsZonasComision_ListarResult> lstZona = objZonaWCF.ZonasComision_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();

                grdZona.DataSource = lstZona;
                grdZona.DataBind();
                lblCorrida.Text = "1";

                ViewState["lstZona"] = JsonHelper.JsonSerializer(lstZona);

            }
            catch (Exception ex)
            {
                throw ex;
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
                throw ex;
            }
        }

        protected void grdZona_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (lblCorrida.Text == "1")
                {
                    List<gsZonasComision_ListarResult> lstPersonal = JsonHelper.JsonDeserialize<List<gsZonasComision_ListarResult>>((string)ViewState["lstZona"]);
                    grdZona.DataSource = lstPersonal;
                    grdZona.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se encontrarón resultados.";
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdZona_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.CommandName == "Editar")
                {
                    gsZonasComision_ListarResult objZona = JsonHelper.JsonDeserialize<List<gsZonasComision_ListarResult>>((string)ViewState["lstZona"]).Find(x => x.id_zona.ToString() == e.CommandArgument.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreate('" + JsonHelper.JsonSerializer(objZona) + "');", true);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdPromotores_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.CommandName == "Editar")
                {
                    Personal_ListarTotalResult objPromotor = JsonHelper.JsonDeserialize<List<Personal_ListarTotalResult>>((string)ViewState["lstdetalle"]).Find(x => x.NroDocumento.ToString() == e.CommandArgument.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreatePromotor('" + JsonHelper.JsonSerializer(objPromotor) + "');", true);
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
                Zona_Listar(int.Parse(cboEmpresa.SelectedValue), ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, cboEmpresa.SelectedValue, null, txtBuscar.Text, int.Parse(cboReporte.SelectedValue));

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
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreatePromotor('');", true);
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
                    int id_zona = int.Parse( e.Argument.Split(',')[1]); 

                    grdZona.MasterTableView.SortExpressions.Clear();
                    grdZona.MasterTableView.GroupByExpressions.Clear();

                    Zona_Listar(int.Parse(cboEmpresa.SelectedValue), ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario , cboEmpresa.SelectedValue, null, txtBuscar.Text, int.Parse(cboReporte.SelectedValue));

                    PromotoresZona_Detalle( int.Parse(cboReporte.SelectedValue), "0", id_zona , null);

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

        protected void grdZona_selectedindexchanged(object sender, EventArgs e)
        {
            try
            {
                GridDataItem gridItem = (GridDataItem)grdZona.SelectedItems[0];
                int id_zona = int.Parse( gridItem["id_zona"].Text) ;

                PromotoresZona_Detalle(int.Parse(cboReporte.SelectedValue), "0", id_zona, null );
            }
            catch (Exception ex) { throw ex; }
        }

        private void PromotoresZona_Detalle(int reporte, string NroDocumento, int id_zona, string texto)
        {
            ComisionWCFClient objComision = new ComisionWCFClient(); 
            List<Personal_ListarTotalResult> lstdetalle;

            try
            {
                lstdetalle = objComision.Personal_ListarTotal(int.Parse(cboEmpresa.SelectedValue), ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, cboEmpresa.SelectedValue,null,texto, reporte, id_zona).ToList();
                grdPromotores.DataSource = lstdetalle;
                grdPromotores.DataBind();
                ViewState["lstdetalle"] = JsonHelper.JsonSerializer(lstdetalle);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}