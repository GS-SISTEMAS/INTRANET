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
using GS.SISGEGS.Web.UsuarioWCF;
using GS.SISGEGS.Web.PerfilWCF;
using GS.SISGEGS.DM;
using System.Data.Sql;
using System.Data.OleDb;
using System.Data;


namespace GS.SISGEGS.Web.RRHH.Procesos
{
    public partial class frmInactivacionUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");
            try
            {
                if (!Page.IsPostBack)
                {

                    var loginUsuario = (string)null;
                    Usuario_Cargar(loginUsuario);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }

        }

        private void Usuario_Cargar(string loginUsuario)
        {
            UsuarioWCFClient objUsuariosWCF = new UsuarioWCFClient();
            try
            {
                List<USP_Sel_Usuarios_GeneralResult> listUsuario = objUsuariosWCF.Usuario_Listar_Usuarios(loginUsuario).ToList();
                grdUsuario.DataSource = listUsuario;
                grdUsuario.DataBind();

                ViewState["listUsuario"] = JsonHelper.JsonSerializer(listUsuario);
                ViewState["loginUsuario"] = loginUsuario;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grdUsuario_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.CommandName == "Editar")
                {
                    USP_Sel_Usuarios_GeneralResult objUsuario = JsonHelper.JsonDeserialize<List<USP_Sel_Usuarios_GeneralResult>>((string)ViewState["listUsuario"]).Find(x => x.loginUsuario.ToString() == e.CommandArgument.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreate('" + objUsuario.loginUsuario + "');", true);
                }

                if (e.CommandName == "Desactivar")
                {
                    USP_Sel_Usuarios_GeneralResult objUsuario = JsonHelper.JsonDeserialize<List<USP_Sel_Usuarios_GeneralResult>>((string)ViewState["listUsuario"]).Find(x => x.loginUsuario.ToString() == e.CommandArgument.ToString());
                    UsuarioWCFClient objUsuariosWCF = new UsuarioWCFClient();

                    if (((Image)e.Item.FindControl("ibDesactivar")).ImageUrl == "~/Images/Icons/circle-green-16.png")
                    {
                        objUsuariosWCF.Actualizar_Estado_Usuarios_General(objUsuario.loginUsuario, false);
                    }
                    else
                    {
                        objUsuariosWCF.Actualizar_Estado_Usuarios_General(objUsuario.loginUsuario, true);
                    }
                    Usuario_Cargar(null);
                    
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }



        public void Actualiza_Usuarios(string login, bool activo)
        {
            UsuarioWCFClient objUsuariosWCF = new UsuarioWCFClient();

            objUsuariosWCF.Actualizar_Estado_Usuarios_General(login, activo);
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");
            try
            {

                Usuario_Cargar(txtBuscar.Text);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }


        protected void grdOrdenVenta_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Item is GridDataItem)
                {

                    GridDataItem item = (GridDataItem)e.Item;
                    var IsActivo = false;

                    if (((CheckBox)item["Silvestre"].Controls[0]).Checked)
                        IsActivo = true;
                    if (((CheckBox)item["NeoAgrum"].Controls[0]).Checked)
                        IsActivo = true;
                    if (((CheckBox)item["Inatec"].Controls[0]).Checked)
                        IsActivo = true;
                    if (((CheckBox)item["Intranet"].Controls[0]).Checked)
                        IsActivo = true;
                    if (((CheckBox)item["Ticket"].Controls[0]).Checked)
                        IsActivo = true;

                    if (IsActivo)
                    {
                        ((Image)e.Item.FindControl("ibDesactivar")).ImageUrl = "~/Images/Icons/circle-green-16.png";

                    }
                    else
                    {
                        ((Image)e.Item.FindControl("ibDesactivar")).ImageUrl = "~/Images/Icons/circle-red-16.png";

                    }

                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void ramUsuario_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (e.Argument.Split(',')[0] == "Registro")
                {
                    grdUsuario.MasterTableView.SortExpressions.Clear();
                    grdUsuario.MasterTableView.GroupByExpressions.Clear();
                    Usuario_Cargar(txtBuscar.Text);

                    lblMensaje.Text = "Se realizo el registro del usuario";
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