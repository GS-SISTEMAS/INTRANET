using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.Web.ReporteVentaWCF;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.CobranzasWCF;
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.DM;
using Telerik.Web.UI;
using System.Web.Services;
using System.Globalization;

namespace GS.SISGEGS.Web.Mantenimiento.IntranetGS.Mantenedor
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
                  
                   

                    if (Request.QueryString["objPersonal"] == "") {
                        Title = "Registrar Colaborador";
                        cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();

                        lblMensaje.Text = "Listo para registrar Colaborador";
                        lblMensaje.CssClass = "mensajeExito";

                        btnGuardar.Enabled = false;
                        ZonasReporte_Cargar(int.Parse(cboEmpresa.SelectedValue), "0");
                        Reporte_Cargar(cboEmpresa.SelectedValue, 0);
                    }
                    else {

                        Title = "Modificar Colaborador";
                        string obj = Request.QueryString["objPersonal"];
                        string idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();

                        ZonasSectorista_ListarResult objPersonal = JsonHelper.JsonDeserialize<ZonasSectorista_ListarResult>(Request.QueryString["objPersonal"]);
                        ViewState["ID_Agenda"] = objPersonal.ID_Agenda;
                        hfIdUsuario.Value = objPersonal.ID_Agenda.ToString();

                        ZonasReporte_Cargar(int.Parse(cboEmpresa.SelectedValue), objPersonal.ID_Agenda);
                        Reporte_Cargar(cboEmpresa.SelectedValue, 0);

                        txtNroDocumento.Text = objPersonal.nroDocumento;
                        txtNombre.Text = objPersonal.nombres;
                        //cboReporte.SelectedValue = "1002";

                        cboEstado.SelectedValue = "1";

                        ZonasReporte_CargarxUsuario(int.Parse(idEmpresa), objPersonal.ID_Agenda);

                        cboReporte.Enabled = false;
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

            CobranzasWCFClient objCorbanza = new CobranzasWCFClient();

            string  id_agenda = null;
            int idPermiso = 0;
            int ActivoPermiso = 0; 
            string imagen = "Defoult.jpg"; 
            try
            {
                id_agenda =  hfIdUsuario.Value.ToString() ;
               
                ActivoPermiso = int.Parse(cboEstado.SelectedValue.ToString());

                

                int ActivoZona = 0;
                int idZona = 0; 

 
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

                        idPermiso = objCorbanza.PermisosZona_Registrar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, id_agenda, idZona, ActivoZona, ActivoPermiso);

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
            List<ReportesIntranet_ListarResult> lista = new List<ReportesIntranet_ListarResult>();
            try
            {
                lista = objReporteVenta.ReportesIntranet_Lista(idUsuario).ToList().FindAll(x => x.NombreReporte.Contains("Cobranza"));

                cboReporte.DataSource = lista; 
                cboReporte.DataBind();
                cboReporte.Items.Insert(0, new RadComboBoxItem("SELECCIONAR", "0"));
                cboReporte.SelectedIndex = 1;
            
                cboReporte.Enabled = false;
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
                AgendaWCFClient objAgenda = new AgendaWCFClient(); 
             
                idempresa = HttpContext.Current.Session["idempresa"].ToString();
            

                 
                List<gsAgenda_ListarClienteResult> lst = objAgenda.Agenda_ListarCliente(int.Parse(idempresa),
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario,searchString,0).ToList();
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsAgenda_ListarClienteResult usuario in lst)
                {
                    if (result.FindAll(x => x.Text == usuario.ID_Agenda + "-" + usuario.Nombre ).Count == 0)
                    {
                        AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                        childNode.Text = usuario.ID_Agenda + "-" + usuario.Nombre;
                        childNode.Value = usuario.ID_Agenda.ToString();
                        
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
            AgendaWCFClient objAgenda = new AgendaWCFClient();

            gsAgenda_ListarClienteResult objUsuario = new gsAgenda_ListarClienteResult();
            try
            {

                objUsuario = objAgenda.Agenda_ListarCliente(
                    int.Parse(cboEmpresa.SelectedValue),((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                     acbUsuario.Text.Split('-')[1],0).Single();

                hfIdUsuario.Value = objUsuario.ID_Agenda.ToString();

                txtNroDocumento.Text = objUsuario.ID_Agenda; 
                txtNombre.Text = objUsuario.Nombre ;

                btnGuardar.Enabled = true; 
                btnGuardar.Visible = true;
                btnCancelar.Visible = true;
                btnEditar.Visible = false;

                txtNombre.Enabled = false;
                txtNroDocumento.Enabled = false;
                cboEmpresa.Enabled = false;

                cboReporte.Enabled = false;
                cboEstado.Enabled = true;

                ltbZonas.Enabled = true;

                if(objUsuario.Estado == "Activo")
                {
                    cboEstado.SelectedValue = "1";
                    lblMensaje.Text = "Listo para registrar Colaborador";
                    lblMensaje.CssClass = "mensajeExito";
                }
                else
                {
                    cboEstado.SelectedValue = "0";
                    lblMensaje.Text = "La agenda está bloqueada. Comuníquese con el responsable.";
                    lblMensaje.CssClass = "mensajeError";
                }


                //Reporte_Cargar(cboEmpresa.SelectedValue.ToString(), objUsuario.idUsuario);

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

                cboReporte.Enabled = false;
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

                ZonasSectorista_ListarResult objPersonal = JsonHelper.JsonDeserialize<ZonasSectorista_ListarResult>(Request.QueryString["objPersonal"]);
                ViewState["ID_Agenda"] = objPersonal.ID_Agenda;

                hfIdUsuario.Value = objPersonal.ID_Agenda.ToString();

                txtNroDocumento.Text = objPersonal.nroDocumento;
                txtNombre.Text = objPersonal.nombres;
                cboReporte.SelectedValue = "1002";
                cboEstado.SelectedValue = "1"; 

                ZonasReporte_CargarxUsuario(int.Parse(idEmpresa), objPersonal.ID_Agenda );

                cboReporte.Enabled = false;
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

        private void ZonasReporte_CargarxUsuario(int idEmpresa, string id_Agenda)
        {
            CobranzasWCFClient objCorbanza = new CobranzasWCFClient();
            List<ZonasSectoristaPermiso_ListarResult> lstdetalle;

            try
            {
                ltbZonas.ClearChecked();

                //List<ZonasReporte_ListarResult> lst = objReporte.ZonasReporte_Listar(idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idUsuario, idReporte).ToList();
                lstdetalle = objCorbanza.ZonasSectoristaPermiso_Listar(int.Parse(cboEmpresa.SelectedValue), ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, id_Agenda).ToList();

                foreach (RadListBoxItem item in ltbZonas.Items)
                {
                    if (lstdetalle.FindAll(x => x.ID_Zona.ToString() == item.Value).Count > 0)
                        item.Checked = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ZonasReporte_Cargar(int idEmpresa, string id_sectorista)
        {
            CobranzasWCFClient objCorbanza = new CobranzasWCFClient();
            try
            {
                List<Zonas_Reportes_CobranzaResult> lst = objCorbanza.Zonas_Listar_Cobranza(idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, id_sectorista).ToList();

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