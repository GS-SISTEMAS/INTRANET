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
using GS.SISGEGS.Web.CobranzasWCF;

namespace GS.SISGEGS.Web.Mantenimiento.IntranetGS.Mantenedor
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

        private void Zona_Cargar(string NroDocumento)
        {
            CobranzasWCFClient objCorbanza = new CobranzasWCFClient();

            try
            {
                //List<Zonas_ReportesResult> lstPersonal = objPersonalWCF.Zonas_Listar(int.Parse(cboEmpresa.SelectedValue), ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, 0, NroDocumento).ToList();
                List<Zonas_Reportes_CobranzaResult> lst = objCorbanza.Zonas_Listar_Cobranza(int.Parse(cboEmpresa.SelectedValue), ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, NroDocumento).ToList();

                cboZonas.DataSource = lst;
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
                        ZonasPermisos_ListarResult objZona = JsonHelper.JsonDeserialize<ZonasPermisos_ListarResult>(Request.QueryString["objZona"]);
                        ViewState["ID"] = objZona.idPermiso;


                        cboEstado.SelectedValue = Convert.ToInt32(objZona.activo).ToString();
                        cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();

                        cboZonas.SelectedValue = objZona.ID_Zona.ToString(); 
                        cboEmpresa.Enabled = false;
                        cboZonas.Enabled = false; 


                        lblMensaje.Text = "Listo para modificar Zona";
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

            int idEmpresa = 0;
            int idPermiso = int.Parse(ViewState["ID"].ToString());
            int Mensaje = idPermiso;

            try
            {

                idEmpresa = int.Parse(cboEmpresa.SelectedValue);


                objReporteVenta.Zona_Registrar(idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idPermiso, 
                    int.Parse(cboZonas.SelectedValue),  int.Parse(cboEstado.SelectedValue));

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebindZona(" + Mensaje + ");", true);

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void cboZonas_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            btnGuardar.Enabled = true;
        }
    }
}