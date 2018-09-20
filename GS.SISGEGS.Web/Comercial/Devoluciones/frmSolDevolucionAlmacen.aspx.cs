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
    public partial class frmSolDevolucionAlmacen : System.Web.UI.Page
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

        private void DevolucionSolicitud_Listar()
        {
            SolDevolucionWCFClient objSolDevolucionWCFC = new SolDevolucionWCFClient();
            string ID_Cliente = null;
            string ID_Vendedor = null;
            try
            {
                //if (!string.IsNullOrEmpty(acbCliente.Text))
                //    ID_Cliente = acbCliente.Text.Split('-')[1];

                //if (!((Usuario_LoginResult)Session["Usuario"]).aprobarDevolucionSol1)
                //    ID_Vendedor = ((Usuario_LoginResult)Session["Usuario"]).nroDocumento;

                List<gsDevolucionSolicitud_ListarResult> lstSolicitudes = objSolDevolucionWCFC.DevolucionSolicitud_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, dpFechaInicio.SelectedDate.Value, dpFechaFinal.SelectedDate.Value,
                    ID_Vendedor, ID_Cliente).ToList().FindAll(x=>x.aprobacion1);
                if (!string.IsNullOrEmpty(cboGuia.SelectedValue))
                    grdDevolucionSolicitud.DataSource = lstSolicitudes.FindAll(x => (bool)x.GuiadoDev == Convert.ToBoolean(cboGuia.SelectedValue));
                else
                    grdDevolucionSolicitud.DataSource = lstSolicitudes;
                grdDevolucionSolicitud.DataBind();

                ViewState["lstSolicitudes"] = JsonHelper.JsonSerializer(lstSolicitudes);
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
                    cboGuia.SelectedIndex = 1;

                    DevolucionSolicitud_Listar();
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
                DevolucionSolicitud_Listar();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdDevolucionSolicitud_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");
            try
            {
                if (e.CommandName == "Editar")
                {
                    Response.Redirect("~/Comercial/Devoluciones/frmSolDevolucionRegistrar.aspx?Op=" +
                        e.CommandArgument.ToString().Split(',')[1] + "&idDevolucionSolicitud=" + e.CommandArgument.ToString().Split(',')[0] +
                        "&revisar=true");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdDevolucionSolicitud_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                grdDevolucionSolicitud.DataSource = JsonHelper.JsonDeserialize<List<gsDevolucionSolicitud_ListarResult>>((string)ViewState["lstSolicitudes"]);
                //grdDevolucionSolicitud.Rebind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        //protected void grdDevolucionSolicitud_DeleteCommand(object sender, GridCommandEventArgs e)
        //{
        //    if (Session["Usuario"] == null)
        //        Response.Redirect("~/Security/frmCerrar.aspx");

        //    SolDevolucionWCFClient objSolDevolucionWCFC = new SolDevolucionWCFClient();
        //    try
        //    {
        //        int idDevolucionSolicitud = int.Parse(((GridDataItem)e.Item).GetDataKeyValue("idDevolucionSolicitud").ToString());
        //        objSolDevolucionWCFC.DevolucionSolicitud_Eliminar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
        //            ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idDevolucionSolicitud);
        //        List<gsDevolucionSolicitud_ListarResult> lstSolicitudes = JsonHelper.JsonDeserialize<List<gsDevolucionSolicitud_ListarResult>>((string)ViewState["lstSolicitudes"]);
        //        lstSolicitudes = lstSolicitudes.FindAll(x => x.idDevolucionSolicitud != idDevolucionSolicitud);
        //        grdDevolucionSolicitud.DataSource = lstSolicitudes;
        //        grdDevolucionSolicitud.Rebind();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMensaje.Text = ex.Message;
        //        lblMensaje.CssClass = "mensajeError";
        //    }
        //}
    }
}