using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.NoticiasWCF;
using GS.SISGEGS.DM;
using Telerik.Web.UI;

namespace GS.SISGEGS.Web.Mantenimiento.DashboardTV.Mural.Noticia
{
    public partial class frmNoticia : System.Web.UI.Page
    {
        private void Noticias_Cargar(int idEmpresa, string descripcion)
        {
            NoticiasWCFClient objNoticiasWCF = new NoticiasWCFClient();
            try
            {
                List<Noticia_ListarResult> lstNoticias = objNoticiasWCF.Noticia_Listar(idEmpresa, descripcion, DateTime.Now).ToList();
                grdNoticias.DataSource = lstNoticias;
                grdNoticias.DataBind();

                ViewState["lstNoticias"] = JsonHelper.JsonSerializer(lstNoticias);
                ViewState["idEmpresa"] = idEmpresa;
                ViewState["descripcion"] = descripcion;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (!Page.IsPostBack)
                {
                    Empresa_Cargar();
                    cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();
                    Noticias_Cargar(int.Parse(cboEmpresa.SelectedValue), txtBuscar.Text);

                    lblMensaje.Text = "Los datos de la página han sido cargados con éxito";
                    lblMensaje.CssClass = "mensajeExito";

                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdNoticias_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.CommandName == "Editar")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreate('" + e.CommandArgument.ToString() + "');", true);
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
                Noticias_Cargar(int.Parse(cboEmpresa.SelectedValue), txtBuscar.Text);

                lblMensaje.Text = "Se encontraron " + grdNoticias.Items.Count.ToString() + " resultados.";
                lblMensaje.CssClass = "mensajeExito";
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

        protected void ramNoticias_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (e.Argument.Split(',')[0] == "Registro")
                {
                    grdNoticias.MasterTableView.SortExpressions.Clear();
                    grdNoticias.MasterTableView.GroupByExpressions.Clear();
                    Noticias_Cargar(int.Parse(e.Argument.Split(',')[1]), "");

                    lblMensaje.Text = "Se realizo el registro del Noticias";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdNoticias_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                NoticiasWCFClient objNoticiasWCF = new NoticiasWCFClient();
                objNoticiasWCF.Noticia_Eliminar(Convert.ToInt32(((GridDataItem)e.Item).GetDataKeyValue("idNoticia")), ((Usuario_LoginResult)Session["Usuario"]).idUsuario);
                Noticias_Cargar(int.Parse(cboEmpresa.SelectedValue), txtBuscar.Text);

                lblMensaje.Text = "La noticia se eliminó con éxito";
                lblMensaje.CssClass = "mensajeExito";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}