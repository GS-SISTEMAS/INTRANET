using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.ItemWCF;
using GS.SISGEGS.DM;
using System.Web.Services;
using Telerik.Web.UI;
using GS.SISGEGS.Web.AgendaWCF;

namespace GS.SISGEGS.Web.Mantenimiento.IntranetGS.Producto
{
    public partial class frmProductoCliente1 : System.Web.UI.Page
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

        private void Item_Listar(string ID_Agenda, string descripcion)
        {
            ItemWCFClient objItemWCF;
            try
            {
                objItemWCF = new ItemWCFClient();
                grdProducto.DataSource = objItemWCF.Item_ListarPrecioCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, ID_Agenda, descripcion);
                grdProducto.DataBind();
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

                    Page.Form.DefaultButton = btnBuscar.UniqueID;
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

            string ID_Cliente = null, descripcion = null;
            try
            {
                if (!string.IsNullOrEmpty(acbCliente.Text))
                    ID_Cliente = acbCliente.Text.Split('-')[0];

                if (!string.IsNullOrEmpty(txtDescripcion.Text))
                    descripcion = txtDescripcion.Text;

                Item_Listar(ID_Cliente, descripcion);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdProducto_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.CommandName == "Editar")
                {
                   
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "OpenWindowMng(" + e.CommandArgument + ");", true);
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
                rwProducto.Width = 900;
                rwProducto.Height = 600;
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "OpenWindowMng(0);", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void ramProducto_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Argument == "Rebind")
                {
                    string ID_Cliente = null, descripcion = null;

                    grdProducto.MasterTableView.SortExpressions.Clear();
                    grdProducto.MasterTableView.GroupByExpressions.Clear();
                    if (!string.IsNullOrEmpty(acbCliente.Text))
                        ID_Cliente = acbCliente.Text.Split('-')[0];

                    if (!string.IsNullOrEmpty(txtDescripcion.Text))
                        descripcion = txtDescripcion.Text;

                    Item_Listar(ID_Cliente, descripcion);

                    lblMensaje.Text = "Se agregó el producto al pedido.";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdProducto_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            ItemWCFClient objItemWCF = new ItemWCFClient();
            string ID_Cliente = null, descripcion = null;
            try
            {
                objItemWCF.Item_EliminarProductoCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, Convert.ToInt32(((GridDataItem)e.Item).GetDataKeyValue("ID")));

                if (!string.IsNullOrEmpty(acbCliente.Text))
                    ID_Cliente = acbCliente.Text.Split('-')[0];

                if (!string.IsNullOrEmpty(txtDescripcion.Text))
                    descripcion = txtDescripcion.Text;

                Item_Listar(ID_Cliente, descripcion);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}