using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.MenuWCF;
using Telerik.Web.UI;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.Helpers;

namespace GS.SISGEGS.Web.Management.Security.Menu
{
    public partial class frmMenu : System.Web.UI.Page
    {
        #region Métodos privados
        private void MenuItem_Buscar(string idMenu)
        {
            try
            {
                Menu_ListarResult objMenu = JsonHelper.JsonDeserialize<Menu_ListarResult[]>((string)ViewState["lstMenu"]).ToList().Find(x => x.idMenu.ToString() == idMenu);
                lblIdMenu.Text = objMenu.idMenu.ToString();
                txtCodigo.Text = objMenu.codigo;
                txtNombre.Text = objMenu.nombreMenu;
                ckbActivo.Checked = objMenu.activo;
                ckbDefecto.Checked = objMenu.defecto;
                if (!string.IsNullOrEmpty(objMenu.codigoPadre))
                {
                    txtCodPadre.Text = objMenu.codigoPadre;
                    txtNomPadre.Text = JsonHelper.JsonDeserialize<Menu_ListarResult[]>((string)ViewState["lstMenu"]).ToList().Find(x => x.codigo == objMenu.codigoPadre).nombreMenu;
                }
                else
                {
                    txtCodPadre.Text = string.Empty;
                    txtNomPadre.Text = string.Empty;
                }
                txtURL.Text = objMenu.url;

                if (objMenu.url.Trim() == string.Empty)
                    btnAgregarOpcion.Visible = true;
                else
                    btnAgregarOpcion.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Menu_Listar()
        {
            MenuWCFClient objMenuWCF;
            try
            {
                objMenuWCF = new MenuWCFClient();
                ViewState["lstMenu"] = JsonHelper.JsonSerializer<Menu_ListarResult[]>(objMenuWCF.Menu_Listar());

                rtvMenu.DataSource = JsonHelper.JsonDeserialize<Menu_ListarResult[]>((string)ViewState["lstMenu"]);
                rtvMenu.DataTextField = "nombreMenu";
                rtvMenu.DataValueField = "idMenu";
                rtvMenu.DataFieldParentID = "codigoPadre";
                rtvMenu.DataFieldID = "codigo";
                rtvMenu.DataBind();

                lblMensaje.Text = "Información cargada con éxito.";
                lblMensaje.CssClass = "mensajeExito";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Métodos protegidos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            lblMensaje.Text = "";

            try
            {
                if (!IsPostBack)
                {
                    Menu_Listar();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void rtvMenu_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                MenuItem_Buscar(e.Node.Value);
                ViewState["idMenu"] = e.Node.Value;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                btnGuardar.Visible = true;
                btnCancelar.Visible = true;
                btnEditar.Visible = false;
                btnAgregarOpcion.Visible = false;

                txtNombre.Enabled = true;
                txtURL.Enabled = true;

                rtvMenu.Enabled = false;

                //MenuItem_Buscar((string)ViewState["idMenu"]);

                lblMensaje.Text = "Se puede modificar: " + txtCodigo.Text;
                lblMensaje.CssClass = "mensajeExito";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                btnGuardar.Visible = false;
                btnCancelar.Visible = false;
                btnEditar.Visible = true;

                txtNombre.Enabled = false;
                txtURL.Enabled = false;

                rtvMenu.Enabled = true;

                MenuItem_Buscar((string)ViewState["idMenu"]);

                lblMensaje.Text = "Se cancelo la edición";
                lblMensaje.CssClass = "mensajeExito";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnAgregarOpcion_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowEditWin(" + lblIdMenu.Text + ");", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void mngMenu_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Argument == "Rebind")
                {
                    Menu_Listar();
                }

                if (e.Argument == "RebindAndNavigate")
                {
                    //grdPerfil.MasterTableView.SortExpressions.Clear();
                    //grdPerfil.MasterTableView.GroupByExpressions.Clear();
                    //grdPerfil.MasterTableView.CurrentPageIndex = grdPerfil.MasterTableView.PageCount - 1;
                    //grdPerfil.Rebind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnAgregarRaiz_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowEditWin(0);", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        #endregion
    }
}