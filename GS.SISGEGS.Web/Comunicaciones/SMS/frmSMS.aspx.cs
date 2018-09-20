using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.DM;
using GS.SISGEGS.Web.SmsWFC;
using GS.SISGEGS.Web.EmpresaWCF;
using Telerik.Web.UI;
using GS.SISGEGS.Web.Helpers;


namespace GS.SISGEGS.Web.Comunicaciones.SMS
{
    public partial class frmSMS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (!Page.IsPostBack)
                {

                    //txtMensaje.MaxLength = 159;
                  

                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);
                    Lista_Perfiles();

                }
           

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }

        }

        private void Lista_Perfiles()
        {
            try
            {
                SmsWCFClient objMarcaWCF = new SmsWCFClient();
                SP_PerfilesEmpresaResult objPerfil = new SP_PerfilesEmpresaResult();
                List<SP_PerfilesEmpresaResult> lstPerfil;

                int empresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;

                lstPerfil = objMarcaWCF.Lista_PerfilesEmpresa(empresa).ToList();


                cboPerfil.DataSource = lstPerfil;
                cboPerfil.DataTextField = "nombrePerfil";
                cboPerfil.DataValueField = "idPerfil";
                cboPerfil.DataBind();
            }
            catch (Exception ex)
            {
            }

        }



        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                
                    SmsWCFClient objSMSWFC = new SmsWCFClient();
                    var textos = txtMensaje.Text;
                    var id_perfil = (cboPerfil.SelectedValue);
                    objSMSWFC.Registro_SMS(textos, id_perfil);

                lblMensaje.Text = "Mensaje Enviado.";
                lblMensaje.CssClass = "mensajeExito";
                throw new ArgumentException("Mensaje Enviado.");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }


    }
}