﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.ReporteContabilidadWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.Web.Contabilidad.Reporte
{
    public partial class frmFecCt_FecOp : System.Web.UI.Page
    {
        private void Reporte_Cargar() {
            List<gsVoucher_fechContaVSfechaAplicResult> lstDocumentos;
            try {
                ReporteContabilidadWCFClient objReporteWCF = new ReporteContabilidadWCFClient();
                lstDocumentos = objReporteWCF.Voucher_FechContaVSFechaAplic(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                    dpFecInicio.SelectedDate.Value, dpFecFinal.SelectedDate.Value).ToList();
                grdDocumentos.DataSource = lstDocumentos;
                grdDocumentos.DataBind();
                lblMensaje.Text = "Se han encontrado " + lstDocumentos.Count.ToString() + " registro.";
                lblMensaje.CssClass = "mensajeExito";

                ViewState["lstDocumentos"] = JsonHelper.JsonSerializer(lstDocumentos);
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

                    dpFecInicio.SelectedDate = DateTime.Now.AddMonths(-3);
                    dpFecFinal.SelectedDate = DateTime.Now;

                    Reporte_Cargar();
                }
            }
            catch (Exception ex) {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try {
                Reporte_Cargar();
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                grdDocumentos.ExportSettings.FileName = "ReporteContaFCvcFO_" + DateTime.Now.ToString("yyyyMMddHmm");
                grdDocumentos.ExportSettings.ExportOnlyData = true;
                grdDocumentos.ExportSettings.IgnorePaging = true;
                grdDocumentos.ExportSettings.OpenInNewWindow = true;
                grdDocumentos.MasterTableView.ExportToExcel();

                Page.Response.ClearHeaders();
                Page.Response.ClearContent();
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }
        }

        protected void grdDocumentos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                grdDocumentos.DataSource = JsonHelper.JsonDeserialize<List<gsVoucher_fechContaVSfechaAplicResult>>((string)ViewState["lstDocumentos"]);
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }
        }
    }
}