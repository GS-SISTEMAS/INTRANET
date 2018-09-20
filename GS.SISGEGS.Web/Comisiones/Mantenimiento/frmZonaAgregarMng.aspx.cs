using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.Web.ComisionesWCF;
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.DM;
using Telerik.Web.UI;
using System.Web.Services;
using System.Globalization;

namespace GS.SISGEGS.Web.Comision.Mantenimiento
{
    public partial class frmZonaAgregarMng : System.Web.UI.Page
    {
        private void Empresa_Cargar()
        {
            EmpresaWCFClient objEmpresaWCF = new EmpresaWCFClient();
            try
            {
                List<Empresa_ListarResult> empresas = new List<Empresa_ListarResult>();
                empresas = objEmpresaWCF.Empresa_Listar(0,null).ToList();
                cboEmpresa.DataSource = empresas; 
                cboEmpresa.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Cargo_Cargar(string IdEmpresa)
        {
            ComisionWCFClient objComision = new ComisionWCFClient();

            try
            {
                cboCargo.DataSource = objComision.CargoRH_Listar(IdEmpresa);
                cboCargo.DataBind();
                cboCargo.Items.Insert(0, new RadComboBoxItem("SELECCIONAR", "0"));
                cboCargo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void Personal_Cargar()
        {
            ComisionWCFClient objPersonalWCF = new ComisionWCFClient();
            try
            {
                List<Personal_ListarTotalResult> lstPersonal = objPersonalWCF.Personal_ListarTotal(int.Parse(cboEmpresa.SelectedValue), ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario , cboEmpresa.SelectedValue, cboCargo.SelectedValue, "" , 0,0).ToList();
                cboPersonal.DataSource = lstPersonal;
                cboPersonal.DataBind();
                cboPersonal.Items.Insert(0, new RadComboBoxItem("SELECCIONAR", "0"));
                cboPersonal.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Zona_Cargar(string NroDocumento)
        {
            ComisionWCFClient objPersonalWCF = new ComisionWCFClient();
            try
            {
                List<gsZonas_ListarResult> lstPersonal = objPersonalWCF.Zonas_Listar(int.Parse(cboEmpresa.SelectedValue), ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,NroDocumento , 0 ).ToList();
                cboZonas.DataSource = lstPersonal;
                cboZonas.DataBind();
       

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

                    Cargo_Cargar(cboEmpresa.SelectedValue);
                    Personal_Cargar();
                    Zona_Cargar("0"); 

  
                    if (Request.QueryString["objZona"] == "") {
                        Title = "Registrar Zona";
                        cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();

                        lblMensaje.Text = "Listo para registrar Zona";
                        lblMensaje.CssClass = "mensajeExito";

                    
                    }
                    else {
                        decimal numero;
                        Title = "Modificar Zona";
                        string obj = Request.QueryString["objZona"];
                        ZonaPersonal_ListarResult objZona = JsonHelper.JsonDeserialize<ZonaPersonal_ListarResult>(Request.QueryString["objZona"]);
                        ViewState["ID"] = objZona.id;

                        numero = Convert.ToDecimal(objZona.PorcentajeZona);
                 

                        cboEstado.SelectedValue = Convert.ToInt32(objZona.activo).ToString();
                        cboEmpresa.SelectedValue = objZona.id_empresa.ToString();
                        cboCargo.SelectedValue = objZona.id_cargo;

                        cboPersonal.SelectedValue = objZona.ID_Agenda;
                        cboZonas.SelectedValue = objZona.ID_Zona.ToString(); 

                        txtComision.Text = string.Format("{0:#,##0.00}", numero);

                        cboEmpresa.Enabled = false;
                        cboCargo.Enabled = false;
                        cboPersonal.Enabled = false;
                        cboZonas.Enabled = false; 


                        lblMensaje.Text = "Listo para modificar Colaborador";
                        lblMensaje.CssClass = "mensajeExito";
                    }

                    Session["idempresa"] = cboEmpresa.SelectedValue;
                    Session["idCargo"] = cboCargo.SelectedValue;

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

            ComisionWCFClient objComison = new ComisionWCFClient();

            int idEmpresa = 0;
            string nroDocumento = null;

            try
            {

                nroDocumento = cboPersonal.SelectedValue;

                idEmpresa = int.Parse(cboEmpresa.SelectedValue); 
                decimal porcentaje;
                porcentaje =  Convert.ToDecimal(txtComision.Text);
                porcentaje = (porcentaje / 100); 

                objComison.Zona_Registrar(idEmpresa, int.Parse(cboZonas.SelectedValue) , nroDocumento, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, 
                   cboCargo.SelectedValue, porcentaje, int.Parse(cboEstado.SelectedValue) ); 
    
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + nroDocumento + ");", true);

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void cboCargo_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Personal_Cargar();
            Zona_Cargar(cboPersonal.SelectedValue);
            btnGuardar.Enabled = true;

        }
        protected void cboPersonal_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Zona_Cargar(cboPersonal.SelectedValue);
            btnGuardar.Enabled = true; 
        }

        protected void cboZonas_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            btnGuardar.Enabled = true;
        }
    }
}