using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.Web.ReporteVentaWCF;
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.DM;
using Telerik.Web.UI;
using System.Web.Services;
using System.Globalization;

namespace GS.SISGEGS.Web.Comercial.Reportes.ReporteBI.Mantenedor
{
    public partial class frmPersonalZonaMng : System.Web.UI.Page
    {
        private void Empresa_Cargar()
        {
            EmpresaWCFClient objEmpresaWCF = new EmpresaWCFClient();
            try
            {
                List<Empresa_ListarResult> empresas = new List<Empresa_ListarResult>();
                empresas = objEmpresaWCF.Empresa_Listar(0, null).ToList();
                cboEmpresa.DataSource = empresas; 
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
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try {
                if (!Page.IsPostBack)
                {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    Empresa_Cargar();
                    cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();
                    ZonasReporte_Cargar(int.Parse(cboEmpresa.SelectedValue)); 
                    Reporte_Cargar(cboEmpresa.SelectedValue, 0); 

                    if (Request.QueryString["objPersonal"] == "") {
                        Title = "Registrar Colaborador";
                        cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();

                        lblMensaje.Text = "Listo para registrar Colaborador";
                        lblMensaje.CssClass = "mensajeExito";

                        btnGuardar.Enabled = false; 
                    }
                    else {

                        Title = "Modificar Colaborador";
                        string obj = Request.QueryString["objPersonal"];
                        string idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();

                        PermisosUsuarios_ListarResult objPersonal = JsonHelper.JsonDeserialize<PermisosUsuarios_ListarResult>(Request.QueryString["objPersonal"]);
                        ViewState["idPermiso"] = objPersonal.idPermiso;

                        hfIdUsuario.Value = objPersonal.idUsuario.ToString(); 

                        txtNroDocumento.Text = objPersonal.nroDocumento;
                        txtNombre.Text = objPersonal.nombres; 
                        //cboReporte.SelectedValue = objPersonal.idReporte.ToString(); 
                        cboEstado.SelectedValue = Convert.ToInt32(objPersonal.Activo).ToString();

                        ZonasReporte_CargarxUsuario(int.Parse(idEmpresa), objPersonal.idUsuario, int.Parse(objPersonal.idReporte.ToString()));

                        //cboReporte.Enabled = false;
                        cboEmpresa.Enabled = false;
                        txtNroDocumento.Enabled = false;
                        txtNombre.Enabled = false;

                        btnBuscarUsuario.Enabled = false;
                        acbUsuario.Enabled = false;

                        lblMensaje.Text = "Listo para modificar Colaborador";
                        lblMensaje.CssClass = "mensajeExito";
                    }

                    Session["idempresa"] = cboEmpresa.SelectedValue;
                }
            }
            catch (Exception ex) {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            ReporteVentaWCFClient objReporteVenta = new ReporteVentaWCFClient();

            int idUsuario = 0;
            int idEmpresa = 0;
            int idPermiso = 0;
            int idReporte = 0;
            int ActivoPermiso = 0; 
            string imagen = "Defoult.jpg"; 
            try
            {
                idUsuario = int.Parse(hfIdUsuario.Value.ToString());
                //idReporte = int.Parse(cboReporte.SelectedValue.ToString());
                ActivoPermiso = int.Parse(cboEstado.SelectedValue.ToString());

                idPermiso = objReporteVenta.PermisosReportes_Registrar(idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idUsuario,idReporte, ActivoPermiso);

                int ActivoZona = 0;
                int idZona = 0; 

                if(idPermiso > 0)
                {
                    foreach (RadListBoxItem item in ltbZonas.Items)
                    {
                        idZona = int.Parse(item.Value.ToString());
                        if (item.Checked == true)
                        {
                            ActivoZona = 1;
                        }
                        else
                        {
                            ActivoZona = 0;
                        }

                        objReporteVenta.Zona_Registrar(idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idPermiso, idZona, ActivoZona);

                    }
                }

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebindZona(" + idPermiso + ");", true);

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void Reporte_Cargar(string IdEmpresa, int idUsuario)
        {

            ReporteVentaWCFClient objReporteVenta = new ReporteVentaWCFClient();

            try
            {
                //cboReporte.DataSource = objReporteVenta.ReportesIntranet_Lista(idUsuario);
                //cboReporte.DataBind();
                //cboReporte.Items.Insert(0, new RadComboBoxItem("SELECCIONAR", "0"));
                //cboReporte.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        protected void cboEmpresa_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Session["idempresa"] = cboEmpresa.SelectedValue;
        }


        [WebMethod]
        public static AutoCompleteBoxData Agenda_UsuarioBuscar(object context)
        {
            string idempresa;
            string idCargo;

            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (!string.IsNullOrEmpty(searchString) && searchString.Length >= 4)
            {
                ReporteVentaWCFClient objReporte = new ReporteVentaWCFClient(); 
             
                idempresa = HttpContext.Current.Session["idempresa"].ToString();
            

                 
                List<UsuarioReporte_ListarResult> lst = objReporte.UsuarioReporte_Listar(int.Parse(idempresa),
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, 0, searchString).ToList();
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (UsuarioReporte_ListarResult usuario in lst)
                {
                    if (result.FindAll(x => x.Text == usuario.NroDocumento + "-" + usuario.NombreUsuario ).Count == 0)
                    {
                        AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                        childNode.Text = usuario.NroDocumento + "-" + usuario.NombreUsuario ;
                        childNode.Value = usuario.idUsuario.ToString();
                       
                        result.Add(childNode);
                    }
                }
                res.Items = result.ToArray();
            }
            return res;
        }


        protected void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            ReporteVentaWCFClient objReporte = new ReporteVentaWCFClient();
            UsuarioReporte_ListarResult objUsuario = new UsuarioReporte_ListarResult();
            try
            {
                objUsuario = objReporte.UsuarioReporte_Listar(int.Parse(cboEmpresa.SelectedValue),((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,0, acbUsuario.Text.Split('-')[0]).Single();

                hfIdUsuario.Value = objUsuario.idUsuario.ToString();

                txtNroDocumento.Text = objUsuario.NroDocumento; 
                txtNombre.Text = objUsuario.NombreUsuario;

                btnGuardar.Enabled = true;

                Reporte_Cargar(cboEmpresa.SelectedValue.ToString(), objUsuario.idUsuario);

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
  
                txtNombre.Enabled = false;
                txtNroDocumento.Enabled = false; 
                cboEmpresa.Enabled = false ;

                //cboReporte.Enabled = false;
                cboEstado.Enabled = true;

                ltbZonas.Enabled = true; 

                lblMensaje.Text = "Se puede modificar: " + txtNroDocumento.Text;
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

                cboEmpresa.Enabled = false;


                string obj = Request.QueryString["objPersonal"];
                string idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();

                PermisosUsuarios_ListarResult objPersonal = JsonHelper.JsonDeserialize<PermisosUsuarios_ListarResult>(Request.QueryString["objPersonal"]);
                ViewState["idPermiso"] = objPersonal.idPermiso;

                hfIdUsuario.Value = objPersonal.idUsuario.ToString();

                txtNroDocumento.Text = objPersonal.nroDocumento;
                txtNombre.Text = objPersonal.nombres;
                //cboReporte.SelectedValue = objPersonal.idReporte.ToString();
                cboEstado.SelectedValue = Convert.ToInt32(objPersonal.Activo).ToString();

                ZonasReporte_CargarxUsuario(int.Parse(idEmpresa), objPersonal.idUsuario, int.Parse(objPersonal.idReporte.ToString()));

                //cboReporte.Enabled = false;
                cboEmpresa.Enabled = false;
                txtNroDocumento.Enabled = false;
                txtNombre.Enabled = false;

                btnBuscarUsuario.Enabled = false;
                acbUsuario.Enabled = false;

                cboEstado.Enabled = false;
                ltbZonas.Enabled = false; 

                lblMensaje.Text = "Se cancelo la edición";
                lblMensaje.CssClass = "mensajeExito";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void ZonasReporte_CargarxUsuario(int idEmpresa, int idUsuario, int idReporte)
        {
            ReporteVentaWCFClient objReporte = new ReporteVentaWCFClient(); 
            try
            {
                ltbZonas.ClearChecked();

                List<ZonasReporte_ListarResult> lst = objReporte.ZonasReporte_Listar(idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idUsuario, idReporte).ToList();

                foreach (RadListBoxItem item in ltbZonas.Items)
                {
                    if (lst.FindAll(x => x.ID_Zona.ToString() == item.Value).Count > 0)
                        item.Checked = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ZonasReporte_Cargar(int idEmpresa)
        {
            ReporteVentaWCFClient objReporte = new ReporteVentaWCFClient();
            try
            {
                List<Zonas_ReportesResult> lst = objReporte.Zonas_Listar(idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,0).ToList();

                ltbZonas.DataSource = lst;
                ltbZonas.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}