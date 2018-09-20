using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.ImpuestoWCF;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.ItemWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.AgendaWCF;
using System.Text.RegularExpressions;

namespace GS.SISGEGS.Web.Comercial.Pedido
{
    public partial class frmCorreoCliente : System.Web.UI.Page
    {
        #region Métodos protegidos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if (!Page.IsPostBack)
                {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    Title = "Registrar Correo Electronico";

                    string obj = Request.QueryString["objCliente"];
                    VBG01134_validarCorreoResult objCliente = JsonHelper.JsonDeserialize<VBG01134_validarCorreoResult>(Request.QueryString["objCliente"]);
                    ViewState["ID_Agenda"] = objCliente.ID_Agenda;
                    txtCodigo.Text = objCliente.ID_Agenda;
                    lblId_Agenda.Text = objCliente.ID_Agenda; 
                    txtCliente.Text = objCliente.Nombre;

                    txtCodigo.Enabled = false;
                    txtCliente.Enabled = false;

                    txtObservacion.Font.Bold = true;
                    txtObservacion.ForeColor = System.Drawing.Color.Red;
                    txtObservacion.Text = "Por facturación electrónica es necesario registrar un correo electrónico para el cliente. ";
                    txtObservacion.Enabled = false;

                }
                lblMensaje.Text = "Datos del cliente se cargo correctamente.";
                lblMensaje.CssClass = "mensajeExito";
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
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            VBG01134_validarCorreoResult objCliente = new VBG01134_validarCorreoResult();
            AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
            int? Correlativo = 0; 
            try
            {

               if(IsValidEmail(txtCorreo.Text) == true)
                {
                    objCliente.ID_Agenda = lblId_Agenda.Text;
                    objCliente.Email = txtCorreo.Text;


                    objAgendaWCFClient = new AgendaWCFClient();
                    objAgendaWCFClient.Agenda_RegistrarCorreo(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                        ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, objCliente.ID_Agenda, objCliente.Email, ref Correlativo);

                    if (Correlativo > 0)
                    {
                        lblMensaje.Text = "El correo se registro correctamente, verificar.";
                        lblMensaje.CssClass = "mensajeExito";

                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + objCliente.ID_Agenda + ");", true);

                    }
                    else
                    {

                        lblMensaje.Text = "El correo no pudo ser registro, verificar.";
                        lblMensaje.CssClass = "mensajeError";
                    }
                }
               else
                {
                    lblMensaje.Text = "El correo electrónico está mal escrito. ";
                    lblMensaje.CssClass = "mensajeError";

                }


            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private Boolean email_bien_escrito(String email)
        {
            String expresion;
            //expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            expresion = "//^(?:[^<>()[\\].,;:\\s@]+(\\.[^<>()[\\].,;:\\s@]+)*|[^\\n]+)@(?:[^<>()[\\].,;:\\s@]+\\.)+[^<>()[\\]\\.,;:\\s@]{2,63}$//i"; 
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidEmail(string strMailAddress)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strMailAddress, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }
        #endregion
    }
}