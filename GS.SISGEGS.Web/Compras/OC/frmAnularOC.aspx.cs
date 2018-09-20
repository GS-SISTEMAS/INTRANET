using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.OrdenCompraWCF;
using Telerik.Web.UI;

namespace GS.SISGEGS.Web.Compras.OC
{
    public partial class frmAnularOC : System.Web.UI.Page
    {

        private void Reporte_Cargar() {
            List<VBG00536XResult> lista = new List<VBG00536XResult>();
            try {
                OrdenCompraWCFClient objWCF = new OrdenCompraWCFClient();
                this.lblRegistros.Text = "Ingreso Reporte_Cargar";

                DateTime FechaDesde =Convert.ToDateTime(dpFecDesde.SelectedDate);
                DateTime FechaHasta =Convert.ToDateTime(dpFecHasta.SelectedDate);

                string idAgenda = txtProveedor.Text;

                int IdEmpresa= ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;

                int Estado = Convert.ToInt32(this.ddlEstados.SelectedValue);

                this.lblRegistros.Text = "Ingreso OrdenCompraListar";
                lista = objWCF.OrdenCompraListar(IdEmpresa, CodigoUsuario, idAgenda, FechaDesde, FechaHasta, Estado).ToList();
                this.lblRegistros.Text = "Salio OrdenCompraListar";

                grdDocumentos.DataSource = lista;
                grdDocumentos.DataBind();

                
                lblRegistros.CssClass = "mensajeExito";
            }
            catch (Exception ex)
            {
                this.lblRegistros.Text =  ex.Message + "- Reporte_Cargar ";
                lblRegistros.CssClass = "mensajeError";

                rwmReporte.RadAlert(ex.Message, 500, 100, "Error", "");
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

                    //Fecha Desde, primer dia del mes anterior
                    dpFecDesde.SelectedDate = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, 1);
                    //Fecha Hasta, ultimo dia del mes anterior
                    dpFecHasta.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);

                    //dpFecDesde.SelectedDate = new DateTime(2017, 4, 1);
                    //dpFecHasta.SelectedDate = new DateTime(2017, 7, 18);

                    Reporte_Cargar();

                }
            }
            catch (Exception ex) {

                this.lblRegistros.Text = ex.Message + "- Load ";
                lblRegistros.CssClass = "mensajeError";
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error", "");
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {

            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (grdDocumentos.Items.Count == 0)
                {
                    throw new System.ArgumentException("No se puede descargar el reporte porque no hay registros en el listado");
                }

                //grdDocumentos.MasterTableView.GetColumn("Uniquename").Visible = false;
                //grdDocumentos.MasterTableView.ForeColor = System.Drawing.Color.FromArgb(0,0,0);
                grdDocumentos.ExportSettings.FileName = "ReporteOrdenesCompra_" + DateTime.Now.ToString("yyyyMMddHmm");
                grdDocumentos.ExportSettings.ExportOnlyData = true;
                grdDocumentos.ExportSettings.IgnorePaging = true;
                grdDocumentos.ExportSettings.OpenInNewWindow = true;
                grdDocumentos.MasterTableView.ExportToExcel();

                Page.Response.ClearHeaders();
                Page.Response.ClearContent();
            }
            catch (ArgumentException ex1)
            {
                rwmReporte.RadAlert(ex1.Message, 300, 120, "Mensaje", "");
            }
            catch (Exception ex)
            {
                this.lblRegistros.Text = ex.Message + "- btnExcel_Click ";
                lblRegistros.CssClass = "mensajeError";
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }

        }

        protected void btnAnular_Click(object sender, EventArgs e)
        {

            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            GridItemCollection itemSel = grdDocumentos.SelectedItems;

            List<string> listaErrores = new List<string>();

            try
            {
                //if (itemSel.Count > 0)
                //{

                OrdenCompraWCFClient objWCF = new OrdenCompraWCFClient();
                int IdEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                int CodigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
                //foreach (GridItem item in itemSel)
                //{
                //    try
                //    {
                //        objWCF.Anular_OC(IdEmpresa, CodigoUsuario, Convert.ToInt32(item.Cells[3].Text));
                //    }
                //    catch /*(Exception ex)*/
                //    {
                //        this.lblRegistros.Text = " Anular_OC ";
                //        lblRegistros.CssClass = "mensajeError";

                //        listaErrores.Add(item.Cells[3].Text);
                //    }
                //}


                foreach (GridItem rowitem in grdDocumentos.MasterTableView.Items)
                {
                    GridDataItem dataitem = (GridDataItem)rowitem;
                    TableCell cell = dataitem["CheckColumn"];
                    CheckBox checkBox = (CheckBox)cell.Controls[0].FindControl("Check");
                    if (checkBox.Checked == true && checkBox.Enabled == true)
                    {
                        try
                        {
                            objWCF.Anular_OC(IdEmpresa, CodigoUsuario, Convert.ToInt32(rowitem.Cells[4].Text));
                        }
                        catch /*(Exception ex)*/
                        {
                            this.lblRegistros.Text = " Anular_OC ";
                            lblRegistros.CssClass = "mensajeError";

                            listaErrores.Add(rowitem.Cells[4].Text);
                        }
                    }

                }


                //}
            }
            catch (Exception ex)
            {
                this.lblRegistros.Text = ex.Message + "- btnAnular_Click ";
                lblRegistros.CssClass = "mensajeError";


                if (listaErrores.Count > 0)
                {
                    string cadenaErrores = string.Join(",", listaErrores);
                    rwmReporte.RadAlert("Las Ops " + cadenaErrores + " presentaron errores, no se efectuaron cambios sobre estas OPs", 500, 100, "Error", "");
                }
                else
                {
                    rwmReporte.RadAlert(ex.Message, 500, 100, "Error", "");
                }


            }
            finally {
                Reporte_Cargar();
            }
            
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");


            try {
                Reporte_Cargar();
            }
            catch (Exception ex) {

                this.lblRegistros.Text = ex.Message + "- btnBuscar_Click ";
                lblRegistros.CssClass = "mensajeError";

                rwmReporte.RadAlert(ex.Message, 500, 100, "Error", "");
            }

            
        }

        protected void grdDocumentos_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    if (item["observaciones"].Text.Length > 20)
                    {
                        item["observaciones"].ToolTip = item["observaciones"].Text.ToString();
                        item["observaciones"].Text = (item["observaciones"].Text).Substring(0, 20) + "...";
                    }

                    VBG00536XResult rowItem = (VBG00536XResult)item.DataItem;

                    if (rowItem.Anulable == 0 || rowItem.fechaorden >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1))
                    {
                        item.SelectableMode = GridItemSelectableMode.None;
                        //item.ForeColor = System.Drawing.Color.FromArgb(75,72,72);
                        item.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);

                        TableCell cell = item["CheckColumn"];
                        CheckBox checkBox = (CheckBox)cell.Controls[0].FindControl("Check");
                        checkBox.Checked = true;
                        checkBox.Enabled = false;
                    }

                }
            }
            catch
            {
                this.lblRegistros.Text = "grdDocumentos_ItemDataBound ";
                lblRegistros.CssClass = "mensajeError";
            }
            
        }
    }
}