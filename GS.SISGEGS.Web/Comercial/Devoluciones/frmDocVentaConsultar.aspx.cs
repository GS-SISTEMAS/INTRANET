using GS.SISGEGS.DM;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.SolDevolucionWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using GS.SISGEGS.Web.Helpers;

namespace GS.SISGEGS.Web.Comercial.Devoluciones
{
    public partial class frmDocVentaConsultar : System.Web.UI.Page
    {
        //[WebMethod]
        //public static AutoCompleteBoxData Agenda_BuscarCliente(object context)
        //{
        //    AutoCompleteBoxData res = new AutoCompleteBoxData();
        //    string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
        //    if (searchString.Length > 2)
        //    {
        //        AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
        //        gsAgenda_ListarClienteResult[] lst = objAgendaWCFClient.Agenda_ListarCliente(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
        //            ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString, 1);
        //        List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

        //        foreach (gsAgenda_ListarClienteResult agenda in lst)
        //        {
        //            AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
        //            childNode.Text = agenda.ID_Agenda + "-" + agenda.Nombre;
        //            childNode.Value = agenda.ID_Agenda;
        //            result.Add(childNode);
        //        }
        //        res.Items = result.ToArray();
        //    }
        //    return res;
        //}

        private void DocVenta_Cargar()
        {
            SolDevolucionWCFClient objSolDevolucionWCF = new SolDevolucionWCFClient();
            try
            {
                string ID_Vendedor = null;
                if (!((Usuario_LoginResult)Session["Usuario"]).modificarPedido)
                    ID_Vendedor = ((Usuario_LoginResult)Session["Usuario"]).nroDocumento;

                List<gsDocVenta_ListarResult> lstDocVenta = objSolDevolucionWCF.DocVenta_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, dpFechaInicio.SelectedDate.Value, dpFechaFinal.SelectedDate.Value, ID_Vendedor).ToList();
                grdDocVenta.DataSource = lstDocVenta;
                grdDocVenta.DataBind();

                ViewState["lstDocVenta"] = JsonHelper.JsonSerializer(lstDocVenta);
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

                    dpFechaInicio.SelectedDate = DateTime.Now.AddDays(-7);
                    dpFechaFinal.SelectedDate = DateTime.Now;

                    DocVenta_Cargar();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdDocVenta_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.CommandName == "Devolucion")
                {
                    Response.Redirect("~/Comercial/Devoluciones/frmSolDevolucionRegistrar.aspx?Op=" + e.CommandArgument + "&idDevolucionSolicitud=0");
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
                DocVenta_Cargar();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdDocVenta_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                grdDocVenta.DataSource = JsonHelper.JsonDeserialize<List<gsDocVenta_ListarResult>>((string)ViewState["lstDocVenta"]);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}