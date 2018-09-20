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
                    Reporte_Cargar(cboEmpresa.SelectedValue); 

                    if (Request.QueryString["objPersonal"] == "") {
                        Title = "Registrar Colaborador";
                        cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();

                        lblMensaje.Text = "Listo para registrar Colaborador";
                        lblMensaje.CssClass = "mensajeExito";

                        btnGuardar.Enabled = false; 
                    }
                    else {
                        decimal numero;
                        Title = "Modificar Colaborador";
                        string obj = Request.QueryString["objPersonal"];
                        Personal_ListarTotalResult objPersonal = JsonHelper.JsonDeserialize<Personal_ListarTotalResult>(Request.QueryString["objPersonal"]);
                        ViewState["NroDocumento"] = objPersonal.NroDocumento;

                        numero = Convert.ToDecimal(objPersonal.porcentaje);
       

                        txtNroDocumento.Text = objPersonal.NroDocumento; 
                        txtApellidos.Text = objPersonal.ApPaterno + " " + objPersonal.ApMaterno; 
                        txtNombre.Text = objPersonal.Nombres;

                        txtComision.Text = string.Format("{0:#,##0.00}", numero);

                        txtImagen.Text = objPersonal.ImagenURL;

                        cboReporte.SelectedValue = objPersonal.reporte.ToString(); 
                        cboEstado.SelectedValue = Convert.ToInt32(objPersonal.Comision).ToString();
                        cboEmpresa.SelectedValue = objPersonal.CodEmpresaGys.ToString();
                        cboCargo.SelectedValue = objPersonal.codCargo;

                        cboReporte.Enabled = false; 
                        cboEmpresa.Enabled = false;
                        cboCargo.Enabled = false;
                        txtNroDocumento.Enabled = false;
                        txtNombre.Enabled = false;
                        txtApellidos.Enabled = false;
                        txtImagen.Visible = false;
                        acbUsuario.Enabled = true;
                        btnBuscarUsuario.Enabled = true; 

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
            string imagen = "Defoult.jpg"; 
            try
            {

                if(txtImagen.Text.Length > 0 )
                { imagen = txtImagen.Text;
                }
                nroDocumento = txtNroDocumento.Text;

                idEmpresa = int.Parse(cboEmpresa.SelectedValue); 
                decimal porcentaje;
                porcentaje =  Convert.ToDecimal(txtComision.Text);
                porcentaje = (porcentaje / 100); 

                objComison.Personal_Registrar(idEmpresa, nroDocumento , imagen, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, 
                    cboEmpresa.SelectedValue, cboCargo.SelectedValue, porcentaje, int.Parse(cboEstado.SelectedValue), int.Parse(cboReporte.SelectedValue)); 
    
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + cboEmpresa.SelectedValue + ");", true);

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void Reporte_Cargar(string IdEmpresa)
        {
            ComisionWCFClient objComision = new ComisionWCFClient();

            try
            {
                cboReporte.DataSource = objComision.combo_Reporte();
                cboReporte.DataBind();
                cboReporte.Items.Insert(0, new RadComboBoxItem("SELECCIONAR", "0"));
                cboReporte.SelectedIndex = 0;
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
        protected void cboCargo_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Session["idCargo"] = cboCargo.SelectedValue;
        }



        [WebMethod]
        public static AutoCompleteBoxData Item_BuscarUsuario(object context)
        {
            string idempresa;
            string idCargo;

            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (!string.IsNullOrEmpty(searchString) && searchString.Length >= 4)
            {
                ComisionWCFClient objUsuarioWCF = new ComisionWCFClient();
                idempresa = HttpContext.Current.Session["idempresa"].ToString();
                idCargo = HttpContext.Current.Session["idCargo"].ToString();

                 
                List<Personal_BuscarResult> lst = objUsuarioWCF.Personal_Buscar(int.Parse(idempresa),
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, idempresa,idCargo, searchString).ToList();
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (Personal_BuscarResult usuario in lst)
                {
                    if (result.FindAll(x => x.Text == usuario.NroDocumento + "-" + usuario.Nombres ).Count == 0)
                    {
                        AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                        childNode.Text = usuario.NroDocumento + "-" + usuario.Nombres + " " + usuario.ApPaterno;
                        childNode.Value = usuario.NroDocumento;
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

            ComisionWCFClient objComisionWCF = new ComisionWCFClient(); 
          
            Personal_BuscarResult objUsuario;
            try
            {
                decimal numero; 

                objUsuario = objComisionWCF.Personal_Buscar(int.Parse(cboEmpresa.SelectedValue),((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,cboEmpresa.SelectedValue,cboCargo.SelectedValue, acbUsuario.Text.Split('-')[0]).Single();

                txtNroDocumento.Text = objUsuario.NroDocumento; 
                txtNombre.Text = objUsuario.Nombres;
                txtApellidos.Text = objUsuario.ApPaterno + " " + objUsuario.ApMaterno;
                cboEstado.SelectedValue = Convert.ToInt32(objUsuario.Comision).ToString();

                numero = Convert.ToDecimal(objUsuario.porcentaje);
                numero = numero * 100;

                txtComision.Text = numero.ToString();  

                btnGuardar.Enabled = true;

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}