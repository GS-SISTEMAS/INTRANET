using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.UsuarioWCF;
using GS.SISGEGS.Web.PerfilWCF;
using GS.SISGEGS.DM;
using System.Data.Sql;
using System.Data.OleDb;
using System.Data;

namespace GS.SISGEGS.Web.Mantenimiento.IntranetGS.Usuario
{
    public partial class frmUsuario : System.Web.UI.Page
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
                    Perfil_Cargar(int.Parse(cboEmpresa.SelectedValue), "");
                    Usuario_Cargar(int.Parse(cboEmpresa.SelectedValue), int.Parse(cboPerfil.SelectedValue),  "");
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

                Usuario_Cargar(int.Parse(cboEmpresa.SelectedValue), int.Parse(cboPerfil.SelectedValue), txtBuscar.Text);
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

        private void Usuario_Cargar(int idEmpresa, int idPerfil, string descripcion)
        {
            UsuarioWCFClient objUsuarioWCF = new UsuarioWCFClient();
            try
            {
                List<Usuario_BuscarResult> listUsuario = objUsuarioWCF.Usuario_Buscar(idEmpresa, idPerfil, descripcion).ToList();
                grdUsuario.DataSource = listUsuario;
                grdUsuario.DataBind();

                ViewState["listUsuario"] = JsonHelper.JsonSerializer(listUsuario);
                ViewState["idEmpresa"] = idEmpresa;
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
                cboEmpresa.DataSource = objEmpresaWCF.Empresa_ComboBox();
                cboEmpresa.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Perfil_Cargar(int idEmpresa, string descripcion)
        {
            string ValorCero;
            PerfilWCFClient objPerfilWCF = new PerfilWCFClient();
            List<Perfil_ListarResult> listPerfil = new List<Perfil_ListarResult>();

            ValorCero = "SELECCIONAR";

            cboPerfil.Items.Clear();
            cboPerfil.Items.Insert(0, ValorCero);
            cboPerfil.Items.FindItemByText(ValorCero).Value = "0";
            try
            {
                listPerfil = objPerfilWCF.Perfil_Listar(idEmpresa, "").ToList();
                if(listPerfil.Count > 0 )
                {
                    foreach (Perfil_ListarResult objPerfil in listPerfil)
                    {
                        Telerik.Web.UI.RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem(objPerfil.nombrePerfil.ToString().Trim(), objPerfil.idPerfil.ToString());
                        cboPerfil.Items.Add(item);
                    }
                }
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
                    Usuario_BuscarResult objUsuario = JsonHelper.JsonDeserialize<List<Usuario_BuscarResult>>((string)ViewState["listUsuario"]).Find(x => x.idUsuario.ToString() == e.CommandArgument.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreateEdit('" + JsonHelper.JsonSerializer(objUsuario) + "');", true);
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
                    Usuario_Cargar(int.Parse(e.Argument.Split(',')[1]), int.Parse(cboPerfil.SelectedValue), txtBuscar.Text);

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