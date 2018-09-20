using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.UsuarioWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.PerfilWCF;
using System.Web.Services;
using Telerik.Web.UI;
using System.Configuration;

namespace GS.SISGEGS.Web.Mantenimiento.IntranetGS.Usuario
{
    public partial class frmUsuarioMngEdit : System.Web.UI.Page
    {
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

                    Empresa_Cargar();
                    if (Request.QueryString["objUsuario"] != "")
                    {
                        Title = "Modificar Usuario";
                        string obj = Request.QueryString["objUsuario"];

                        Usuario_BuscarResult objUsuario = JsonHelper.JsonDeserialize<Usuario_BuscarResult>(Request.QueryString["objUsuario"]);
                        ViewState["lstUsuario"] = JsonHelper.JsonSerializer(objUsuario);

                        cboEmpresa.SelectedValue = objUsuario.idEmpresa.ToString();
                        Perfil_Cargar(int.Parse(cboEmpresa.SelectedValue), "");
                        cboEmpresa.Enabled = false;

                        txtNombre.Text = objUsuario.nombres;
                        txtLogin.Text = objUsuario.loginUsuario;
                        txtCorreo.Text = objUsuario.correo;

                        cboPerfil.SelectedValue = Convert.ToInt32(objUsuario.idPerfil).ToString();
                        txtNroDocumento.Text = objUsuario.nroDocumento;

                        txtClave.Text = objUsuario.password;
                        txtClaveGenesys.Text = objUsuario.passwordGenesys;
                        txtClaveGenesys.ReadOnly = true; 

                        lblCodigoUsuario.Value = objUsuario.codigoUsuario.ToString();

                        btnCambioClave.Checked = !objUsuario.cambioPassword;
                        btnCambioAmbos.Checked = objUsuario.cambiarAmbos; 

                        lblIdUsuario.Value = objUsuario.idUsuario.ToString();

                        cboEstado.SelectedValue = Convert.ToInt32(objUsuario.activo).ToString();


                        lblMensaje.Text = "Listo para modificar usuario";
                        lblMensaje.CssClass = "mensajeExito";
                    }

                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
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
        private void Perfil_Cargar(int idEmpresa, string descripcion)
        {
            string ValorCero;
            PerfilWCFClient objPerfilWCF = new PerfilWCFClient();
            List<Perfil_ListarResult> listPerfil = new List<Perfil_ListarResult>();

            ValorCero = "SELECCIONAR";

            cboPerfil.Items.Clear();
            cboPerfil.Items.Insert(0, ValorCero);
            cboPerfil.Items.FindItemByText(ValorCero).Value = "0";
            try
            {
                listPerfil = objPerfilWCF.Perfil_Listar(idEmpresa, "").ToList();
                if (listPerfil.Count > 0)
                {
                    foreach (Perfil_ListarResult objPerfil in listPerfil)
                    {
                        Telerik.Web.UI.RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem(objPerfil.nombrePerfil.ToString().Trim(), objPerfil.idPerfil.ToString());
                        cboPerfil.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int idEmpresa;
            int codigoUsuario;
            string password;
            string nombreUsuario;
            string LoginUsuario;
            int idPerfil;
            string correo;
            string nroDocumento;
            bool cambioPassword;
            bool cambioAmbos;
            int idUsuarioRegistro;
            bool activo;
            int result;
            int idUsuario;

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            UsuarioWCFClient objUsuarioWCF = new UsuarioWCFClient();
            result = 0;
            try
            {
                if (Validar_Variables() == 0 )
                {
                    idEmpresa = int.Parse(cboEmpresa.SelectedValue);
                    codigoUsuario = int.Parse(lblCodigoUsuario.Value);
                    password = txtClave.Text;
                    nombreUsuario = txtNombre.Text.Trim();
                    LoginUsuario = txtLogin.Text.Trim();
                    idPerfil = int.Parse(cboPerfil.SelectedValue);
                    correo = txtCorreo.Text.Trim();
                    nroDocumento = txtNroDocumento.Text.Trim();
                    cambioPassword = !btnCambioClave.Checked;

                    cambioAmbos = btnCambioAmbos.Checked;

                    idUsuarioRegistro = ((Usuario_LoginResult)Session["Usuario"]).idUsuario;
                    activo = Convert.ToBoolean(int.Parse(cboEstado.SelectedValue));
                    idUsuario = int.Parse(lblIdUsuario.Value);

                    result = objUsuarioWCF.Usuario_Actualizar(idEmpresa, idUsuario, codigoUsuario, password, nombreUsuario, LoginUsuario, idPerfil, correo, nroDocumento, cambioPassword, cambioAmbos,  idUsuarioRegistro, activo);

                    if(result> 0)
                    {

                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + cboEmpresa.SelectedValue + ");", true);
                        lblMensaje.Text = "Exito: " + " Usuario se guardo exitosamente. ";
                        lblMensaje.CssClass = "mensajeExito";
                    }
                    else
                    {
                        lblMensaje.Text = "ERROR: " + "Usuario ya se encuentra registrado. ";
                        lblMensaje.CssClass = "mensajeError";
                    }
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        public int Validar_Variables()
        {
            int valor = 0;

                if (cboEmpresa == null || cboEmpresa.SelectedValue.ToString() == "")
                {
                    valor = 1;
                    lblMensaje.Text = "";
                    lblMensaje.Text = lblMensaje.Text + "Seleccionar una empresa. ";
                    lblMensaje.CssClass = "mensajeError";
                    return valor;
                }
                if (txtNombre == null || txtNombre.Text.ToString() == "" || txtNombre.Text.Length < 4)
                {
                    valor = 1;
                    lblMensaje.Text = "";
                    lblMensaje.Text = lblMensaje.Text + "Ingresar un nombre de usuario correcto. ";
                    lblMensaje.CssClass = "mensajeError";
                    return valor;
                }
                if (txtLogin == null || txtLogin.Text.ToString() == "" || txtLogin.Text.Length < 4)
                {
                    valor = 1;
                    lblMensaje.Text = "";
                    lblMensaje.Text = lblMensaje.Text + "Ingresar un Login correcto. ";
                    lblMensaje.CssClass = "mensajeError";
                    return valor;
                }
                if (txtCorreo == null || txtCorreo.Text.ToString() == "" || txtCorreo.Text.Length < 4)
                {
                    valor = 1;
                    lblMensaje.Text = "";
                    lblMensaje.Text = lblMensaje.Text + "Ingresar un correo correcto. ";
                    lblMensaje.CssClass = "mensajeError";
                    return valor;
                }

                if (cboPerfil == null || cboPerfil.SelectedValue.ToString() == "" || cboPerfil.SelectedValue.ToString() == "0")
                {
                    valor = 1;
                    lblMensaje.Text = "";
                    lblMensaje.Text = lblMensaje.Text + "Seleccionar un perfil. ";
                    lblMensaje.CssClass = "mensajeError";
                    return valor;
                }
                if (txtNroDocumento == null || txtNroDocumento.Text.ToString() == "" || txtNroDocumento.Text.Length < 4)
                {
                    valor = 1;
                    lblMensaje.Text = "";
                    lblMensaje.Text = lblMensaje.Text + "Ingresar un número de documento correcto. ";
                    lblMensaje.CssClass = "mensajeError";
                    return valor;
                }
          
            return valor;
        }
        private void Usuario_Buscar(int idUsuario, string descripcion)
        {
            UsuarioWCFClient objUsuarioWCF;
            gsUsuario_BuscarResult[] objAgendaUsuario;
            int idEmpresa;
            try
            {
                objUsuarioWCF = new UsuarioWCFClient();
                idEmpresa = int.Parse(HttpContext.Current.Session["IdEmpresa"].ToString());

                objAgendaUsuario = objUsuarioWCF.Usuario_BuscarGenesys(idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idUsuario,  descripcion);

                if (!string.IsNullOrEmpty(objAgendaUsuario[0].CodUsuario.ToString()))
                {
                    txtNombre.Text = objAgendaUsuario[0].NomUsuario;
                    txtLogin.Text = objAgendaUsuario[0].LoginUsuario;
                    txtCorreo.Text = objAgendaUsuario[0].correo;
                    cboEstado.SelectedValue = objAgendaUsuario[0].Activo.ToString();
                    lblClaveUsuario.Value = objAgendaUsuario[0].clave;
                    lblCodigoUsuario.Value = objAgendaUsuario[0].CodUsuario.ToString();
                    txtNroDocumento.Text = "";
                    cboPerfil.SelectedValue = "0";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cboEmpresa_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Session["IdEmpresa"] = cboEmpresa.SelectedValue;        
            Perfil_Cargar(int.Parse(cboEmpresa.SelectedValue), "");

        }
        protected void btnCambiarClave_Click(object sender, EventArgs e)
        {
            Usuario_AutenticarResult objUsuario = new Usuario_AutenticarResult();

            int idEmpresa;
            int codigoUsuario;
            int idUsuario;
            string password;
            bool cambioAmbos;
            lblMensaje.Text = "";
            try
            {
                objUsuario = JsonHelper.JsonDeserialize<Usuario_AutenticarResult>((string)ViewState["lstUsuario"]);
                if (!objUsuario.activo)
                {
                    throw new ArgumentException("El usuario ha sido bloqueado o eliminado. Comunicarse con el área de sistemas");
                }

                idEmpresa = int.Parse(cboEmpresa.SelectedValue);
                codigoUsuario = int.Parse(lblCodigoUsuario.Value);
                password = txtClave.Text;
                cambioAmbos = btnCambioAmbos.Checked;
                idUsuario = int.Parse(lblIdUsuario.Value);
                UsuarioWCFClient objUsuarioWCF = new UsuarioWCFClient();
                    
                objUsuarioWCF.Usuario_CambiarContrasena(idEmpresa, idUsuario, codigoUsuario, password, cambioAmbos);

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + cboEmpresa.SelectedValue + ");", true);

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

    }
}