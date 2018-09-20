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
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.PerfilWCF;

namespace GS.SISGEGS.Web.Mantenimiento.IntranetGS.Menu
{
    public partial class frmMenu : System.Web.UI.Page
    {
        #region Métodos privados
        private void MenuPerfil_Cargar(int idEmpresa, int idMenu) {
            MenuWCFClient objMenuWCF = new MenuWCFClient();
            try {
                ltbPerfil.ClearChecked();
                List<MenuPerfil_ListarResult> lst = objMenuWCF.MenuPerfil_Listar(idEmpresa, idMenu).ToList();
                foreach (RadListBoxItem item in ltbPerfil.Items) {
                    if (lst.FindAll(x => x.idPerfil.ToString() == item.Value).Count > 0)
                        item.Checked = true;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private void Perfil_Cargar(int idEmpresa) {
            PerfilWCFClient objPerfilWCF = new PerfilWCFClient();
            try {
                ltbPerfil.DataSource = objPerfilWCF.Perfil_Listar(idEmpresa, "");
                ltbPerfil.DataBind();
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
                if (!Page.IsPostBack)
                {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    Menu_Listar();
                    Empresa_Cargar();
                    cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();

                    Perfil_Cargar(int.Parse(cboEmpresa.SelectedValue));
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
                MenuPerfil_Cargar(int.Parse(cboEmpresa.SelectedValue), int.Parse(e.Node.Value));
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
                cboEmpresa.Enabled = true;
                ltbPerfil.Enabled = true;

                rtvMenu.Enabled = false;

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
                cboEmpresa.Enabled = false;
                ltbPerfil.Enabled = false;

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

        protected void cboEmprsa_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                Perfil_Cargar(int.Parse(cboEmpresa.SelectedValue));
                MenuPerfil_Cargar(int.Parse(cboEmpresa.SelectedValue), int.Parse((string)ViewState["idMenu"]));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            DM.Menu objMenu = new DM.Menu();
            MenuWCFClient objMenuWCF = new MenuWCFClient();
            try
            {
                objMenu.idMenu = int.Parse((string)ViewState["idMenu"]);
                objMenu.nombreMenu = txtNombre.Text;
                objMenu.url = txtURL.Text;
                objMenu.activo = ckbActivo.Checked;
                objMenu.defecto = ckbDefecto.Checked;
                objMenu.idUsuarioModifico = ((Usuario_LoginResult)Session["Usuario"]).idUsuario;
                string lstPerfil = "";
                if (ltbPerfil.CheckedItems.Count > 0) {
                    foreach (RadListBoxItem item in ltbPerfil.Items)
                    {
                        if (item.Checked)
                            lstPerfil = lstPerfil + "," + item.Value;
                    }
                    lstPerfil = lstPerfil.Substring(1, lstPerfil.Length - 1);
                }

                objMenuWCF.Menu_Modificar(objMenu, int.Parse(cboEmpresa.SelectedValue), lstPerfil);

                btnGuardar.Visible = false;
                btnCancelar.Visible = false;
                btnEditar.Visible = true;

                txtNombre.Enabled = false;
                txtURL.Enabled = false;
                cboEmpresa.Enabled = false;
                ltbPerfil.Enabled = false;

                rtvMenu.Enabled = true;

                lblMensaje.Text = "Se guardo el menú con código: " + txtCodigo.Text;
                lblMensaje.CssClass = "mensajeExito";
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