using GS.SISGEGS.DM;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.ReporteVentaWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace GS.SISGEGS.Web.Comercial.Consulta
{
    public partial class frmLibroVenta : System.Web.UI.Page
    {
        [WebMethod]
        public static AutoCompleteBoxData Agenda_BuscarCliente(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarClienteResult[] lst = objAgendaWCFClient.Agenda_ListarCliente(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString, 1);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsAgenda_ListarClienteResult agenda in lst)
                {
                    AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                    childNode.Text = agenda.ID_Agenda + "-" + agenda.Nombre;
                    childNode.Value = agenda.ID_Agenda;
                    result.Add(childNode);
                }
                res.Items = result.ToArray();
            }
            return res;
        }

        private void Producto_Cargar()
        {
            ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient();
            string ID_Agenda = null;
            try
            {
                if (!string.IsNullOrEmpty(acbCliente.Text))
                    ID_Agenda = acbCliente.Text.Split('-')[0];

                List<gsDocVenta_ConsultarVentaProductoResult> lst = objReporteVentaWCF.DocVenta_ConsultarVentaProducto(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, ID_Agenda, dpFechaInicio.SelectedDate.Value, dpFechaFinal.SelectedDate.Value).ToList();

                grdProducto.DataSource = lst;
                grdProducto.DataBind();

                ViewState["fechaInicio"] = dpFechaInicio.SelectedDate.Value.ToString("dd/MM/yyyy");
                ViewState["lstProductos"] = JsonHelper.JsonSerializer(lst);
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
                if (!Page.IsPostBack)
                {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    dpFechaInicio.SelectedDate = DateTime.Now.AddMonths(-3);
                    dpFechaFinal.SelectedDate = DateTime.Now;
                    //Producto_Cargar();
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
                if (string.IsNullOrEmpty(acbCliente.Text))
                    throw new ArgumentException("Se debe elegir un cliente.");

                Producto_Cargar();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdProducto_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (ViewState["lstProductos"] != null)
                {
                    grdProducto.DataSource = JsonHelper.JsonDeserialize<List<gsDocVenta_ConsultarVentaProductoResult>>((string)ViewState["lstProductos"]);
                    //grdProducto.DataBind();
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
                //string alternateText = (sender as RadButton).AlternateText;

                grdProducto.ExportSettings.FileName = "ReporteVenta_" + DateTime.Now.ToString("ddMMyyyyhhmm");
                grdProducto.ExportSettings.ExportOnlyData = true;
                grdProducto.ExportSettings.IgnorePaging = true;
                grdProducto.ExportSettings.OpenInNewWindow = true;
                grdProducto.MasterTableView.ExportToExcel();

                Page.Response.ClearHeaders();
                Page.Response.ClearContent();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }
        }
    }
}