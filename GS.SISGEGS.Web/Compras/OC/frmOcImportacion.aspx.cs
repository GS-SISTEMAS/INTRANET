using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using GS.SISGEGS.DM;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.DocumentoWCF;
using GS.SISGEGS.Web.FormaPagoWCF;
using GS.SISGEGS.Web.OrdenVentaWCF;
using GS.SISGEGS.Web.LoginWCF;
using GS.SISGEGS.Web.OrdenCompraWCF;

namespace GS.SISGEGS.Web.Compras.OC
{
    public partial class frmOcImportacion : System.Web.UI.Page
    {
        #region PROCEDIMIENTOS
        private void CargarOC()
        {
            DateTime fechainicial = Convert.ToDateTime(dpFechaInicio.SelectedDate);
            DateTime fechafinal = Convert.ToDateTime(dpFechaFinal.SelectedDate);
            string agendaNombre = txtproveedor.Text;

            OrdenCompraWCFClient objOrdenCompraWCF = new OrdenCompraWCFClient();
            

            List<USP_Sel_OCResult> lst = new List<USP_Sel_OCResult>();
            lst = objOrdenCompraWCF.ListarOcImportacion(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                fechainicial, fechafinal, agendaNombre).ToList();
            grdOC.DataSource = lst;
            grdOC.DataBind();
            ViewState["lstOcImp"] = JsonHelper.JsonSerializer(lst);
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (!Page.IsPostBack)
                {
                    LoginWCFClient objLoginWCF = new LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idUsuario);

                    //dpFechaInicio.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    dpFechaInicio.SelectedDate = new DateTime(DateTime.Now.Year, 5, 1);
                    dpFechaFinal.SelectedDate = DateTime.Now;
                    CargarOC();

                    lblMensaje.Text = "La página cargo correctamente";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdOC_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                grdOC.DataSource = JsonHelper.JsonDeserialize<List<USP_Sel_OCResult>>((string)ViewState["lstOcImp"]);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        protected void grdOC_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "CrearParcial")
                {
                    Response.Redirect("~/Compras/OC/frmOcImpParcial.aspx?Op=" + e.CommandArgument.ToString());
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowForm(" + e.CommandArgument.ToString() + ");", true);
                }

                if (e.CommandName == "EliminarParcial")
                {
                    Int32 op = Convert.ToInt32(e.CommandArgument.ToString());
                    OrdenCompraWCFClient objOrdenCompraWCF = new OrdenCompraWCFClient();

                    objOrdenCompraWCF.Eliminar_Oc_Parcial(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, op, "0");
                    
                    lblMensaje.CssClass = "Se eliminó el registro seleccionado";
                    CargarOC();
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
                CargarOC();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}