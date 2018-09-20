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
using System.Globalization; 

namespace GS.SISGEGS.Web.Comercial.Reportes.ReportesBI.Mantenedor
{
    public partial class frmZonaMng : System.Web.UI.Page
    {
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
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try {
                if (!Page.IsPostBack)
                {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    Empresa_Cargar();
                    cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();

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
                        gsZonasComision_ListarResult objZona = JsonHelper.JsonDeserialize<gsZonasComision_ListarResult>(Request.QueryString["objZona"]);
                        ViewState["id_zona"] = objZona.id_zona;

                        numero = Convert.ToDecimal(objZona.porcentajeZona);
                     
                        txtCodigoZona.Text = objZona.id_zona.ToString(); 
                        txtZona.Text = objZona.zona;
                        txtProcentaje.Text = String.Format("{0:F0}", numero);  // numero.ToString("P1", CultureInfo.InvariantCulture);

                        if(objZona.Activo == 1)
                        {
                            cboEstado.SelectedValue = "1"; 
                        }
                        else
                        {
                            cboEstado.SelectedValue = "0";
                        }

 
                        txtCodigoZona.Enabled = false;
                        txtZona.Enabled = false; 
                        cboEmpresa.Enabled = false;

                        lblMensaje.Text = "Listo para modificar Empresa";
                        lblMensaje.CssClass = "mensajeExito";
                    }
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

            ComisionWCFClient objComsion = new ComisionWCFClient();
            decimal numero;
            bool estado = false;

            try
            {
                numero = Convert.ToDecimal(txtProcentaje.Text);
                numero = (numero / 100);
                if (cboEstado.SelectedValue == "1")
                {
                    estado = true;
                }
                else
                {
                    estado = false; 
                }


                objComsion.ZonasComision_Insert( int.Parse(cboEmpresa.SelectedValue), int.Parse(txtCodigoZona.Text),
                    ((Usuario_LoginResult)Session["Usuario"]).idUsuario, numero, estado);

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + txtCodigoZona.Text + ");", true);

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}