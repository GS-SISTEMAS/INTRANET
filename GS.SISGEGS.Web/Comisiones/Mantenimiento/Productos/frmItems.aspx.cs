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

namespace GS.SISGEGS.Web.Comision.Mantenimiento.Productos
{
    public partial class frmItems : System.Web.UI.Page
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

                    Empresa_Cargar();
                    
                    cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();
                    Reporte_Cargar(cboEmpresa.SelectedValue); 

                }
            }

            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void Personal_Listar(int idEmpresa, int idUsario, string codigoEmpresa, string codigoCargo, string descripcion, int reporte)
        {
            ComisionWCFClient objPersonalWCF = new ComisionWCFClient(); 

            try
            {
                List<Personal_ListarTotalResult> lstPersonal = objPersonalWCF.Personal_ListarTotal(idEmpresa, idUsario, codigoEmpresa, codigoCargo, descripcion, reporte, 0).ToList();
                grdPersonal.DataSource = lstPersonal;
                grdPersonal.DataBind();
                lblCorrida.Text = "1";

                ViewState["lstPersonal"] = JsonHelper.JsonSerializer(lstPersonal);
                ViewState["descripcion"] = descripcion;
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

        private void Reporte_Cargar(string IdEmpresa)
        {
            ComisionWCFClient objComision = new ComisionWCFClient(); 
            try
            {
                cboReporte.DataSource = objComision.Reportes_Listar(IdEmpresa);
                cboReporte.DataBind();
                cboReporte.Items.Insert(0, new RadComboBoxItem("SELECCIONAR", "0"));
                cboReporte.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void grdPersonal_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (lblCorrida.Text == "1")
                {
                    List<Personal_ListarTotalResult> lstPersonal = JsonHelper.JsonDeserialize<List<Personal_ListarTotalResult>>((string)ViewState["lstPersonal"]);
                    grdPersonal.DataSource = lstPersonal;
                    grdPersonal.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se encontrarón resultados.";
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
                    Personal_ListarTotalResult objPersonal = JsonHelper.JsonDeserialize<List<Personal_ListarTotalResult>>((string)ViewState["lstPersonal"]).Find(x => x.NroDocumento.ToString() == e.CommandArgument.ToString());
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
                Personal_Listar(int.Parse(cboEmpresa.SelectedValue), ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, cboEmpresa.SelectedValue, "0", txtBuscar.Text, int.Parse(cboReporte.SelectedValue));
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
                    //grdPersonal.MasterTableView.SortExpressions.Clear();
                    //grdPersonal.MasterTableView.GroupByExpressions.Clear();

                    Personal_Listar(int.Parse(cboEmpresa.SelectedValue), ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario , cboEmpresa.SelectedValue, "0", txtBuscar.Text, int.Parse( cboReporte.SelectedValue));

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