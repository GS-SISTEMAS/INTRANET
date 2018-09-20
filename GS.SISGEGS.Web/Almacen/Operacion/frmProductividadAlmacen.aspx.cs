using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;
using System.Web.Services;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.GuiaWCF;
using GS.SISGEGS.Web.PerfilWCF;
using GS.SISGEGS.DM;
using System.Data.Sql;
using System.Data.OleDb;
using System.Data;
using System.Drawing;


namespace GS.SISGEGS.Web.Almacen.Operacion
{
    public partial class frmProductividadAlmacen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if (!Page.IsPostBack)
                {
                    txtProductividad.Focus();
                    dpInicio.SelectedDate = DateTime.Now;
                    dpFinal.SelectedDate = DateTime.Now;
                    dpInicio.SelectedDate = DateTime.Now.AddDays(-DateTime.Now.Day + 1);

                    Productividad_Almacen_Cargar(((Usuario_LoginResult)Session["Usuario"]).nombreComercial, dpInicio.SelectedDate.Value, dpFinal.SelectedDate.Value);

                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void Productividad_Almacen_Cargar(string Empresa, DateTime fechaInicio, DateTime fechaFin)
        {
            GuiaWCFClient objUsuariosWCF = new GuiaWCFClient();
            try
            {
                txtProductividad.Text = "";
                txtProductividad.Focus();

                List<USP_SEL_Productividad_AlmacenResult> listProductividad = objUsuariosWCF.Productividad_Almacen_Listar(Empresa, fechaInicio, fechaFin).ToList();

                grdProductividad.DataSource = listProductividad;
                grdProductividad.DataBind();
                Session["LstProductividad"] = JsonHelper.JsonSerializer(listProductividad);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static string Registrar(string Op)
        {
            if (((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]) == null)
            {
                return "";
            }
            try
            {
                GuiaWCFClient objItemWCF = new GuiaWCFClient();
                var result = objItemWCF.Productividad_Almacen_Registrar(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).nombreComercial, int.Parse(Op));
                return result;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                Productividad_Almacen_Cargar(((Usuario_LoginResult)Session["Usuario"]).nombreComercial, dpInicio.SelectedDate.Value, dpFinal.SelectedDate.Value);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }


        protected void grdLetras_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    var IsActivo = false;
                    var FechaInicio = item["FechaInicio"].Text;
                    var FechaFin = item["FechaFin"].Text;


                    if (FechaInicio != "&nbsp;" && FechaFin != "&nbsp;")
                        IsActivo = true;

                    if (IsActivo)
                    {
                        ((System.Web.UI.WebControls.Image)e.Item.FindControl("ibDesactivar")).ImageUrl = "~/Images/Icons/circle-green-16.png";

                    }
                    else
                    {
                        ((System.Web.UI.WebControls.Image)e.Item.FindControl("ibDesactivar")).ImageUrl = "~/Images/Icons/circle-red-16.png";

                    }
                }
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
                if (grdProductividad.Items.Count == 0)
                {
                    throw new System.ArgumentException("No se puede descargar el reporte porque no hay registros en el listado");
                }


                List<USP_SEL_Productividad_AlmacenResult> lista = new List<USP_SEL_Productividad_AlmacenResult>();
                lista = JsonHelper.JsonDeserialize<List<USP_SEL_Productividad_AlmacenResult>>((string)Session["LstProductividad"]);

                grdProductividad.DataSource = lista.ToList();
                grdProductividad.DataBind();

                grdProductividad.ExportSettings.FileName = "ReporteLetras_" + DateTime.Now.ToString("yyyyMMddHmm");
                grdProductividad.ExportSettings.ExportOnlyData = true;
                grdProductividad.ExportSettings.IgnorePaging = true;
                grdProductividad.ExportSettings.OpenInNewWindow = true;
                grdProductividad.MasterTableView.ExportToExcel();
                Page.Response.ClearHeaders();
                Page.Response.ClearContent();
            }
            catch (ArgumentException ex1)
            {
                rwmReporte.RadAlert(ex1.Message, 300, 120, "Mensaje", "");
            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = ex.Message + "- btnExcel_Click ";
                lblMensaje.CssClass = "mensajeError";
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }

        }


    }
}