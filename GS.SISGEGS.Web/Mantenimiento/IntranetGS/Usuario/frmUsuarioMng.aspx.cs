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

namespace GS.SISGEGS.Web.Mantenimiento.IntranetGS.Usuario
{
    public partial class frmUsuarioMng : System.Web.UI.Page
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
                    cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();
                    Perfil_Cargar(int.Parse(cboEmpresa.SelectedValue), "");

                    if (Request.QueryString["objUsuario"] == "")
                    {
                        Title = "Registrar Usuario";
                        cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();
                        Session["IdEmpresa"] = cboEmpresa.SelectedValue;

                        lblMensaje.Text = "Listo para registrar usuario";
                        lblMensaje.CssClass = "mensajeExito";
                    }
                    else {
                        //Title = "Modificar Usuario";
                        //string obj = Request.QueryString["objUsuario"];

                        //Perfil_ListarResult objPerfil = JsonHelper.JsonDeserialize<Perfil_ListarResult>(Request.QueryString["objUsuario"]);
                        //ViewState["idPerfil"] = objPerfil.idPerfil;

                        //txtNombre.Text = objPerfil.nombrePerfil;
                        //cboEstado.SelectedValue = Convert.ToInt32(objPerfil.activo).ToString();
                        //cboEmpresa.SelectedValue = objPerfil.idEmpresa.ToString();
                        //cboEmpresa.Enabled = false;

                        //lblMensaje.Text = "Listo para modificar usuario";
                        //lblMensaje.CssClass = "mensajeExito";
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
            bool cambioPasswordAmbos;
            int idUsuarioRegistro;
            bool activo;
            int result;

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            UsuarioWCFClient objUsuarioWCF = new UsuarioWCFClient();
         
            try
            {
                if (Validar_Variables() == 0 )
                {
                    idEmpresa = int.Parse(cboEmpresa.SelectedValue);
                    codigoUsuario = int.Parse(lblCodigoUsuario.Value);
                    password = lblClaveUsuario.Value;
                    nombreUsuario = txtNombre.Text.Trim();
                    LoginUsuario = txtLogin.Text.Trim();
                    idPerfil = int.Parse(cboPerfil.SelectedValue);
                    correo = txtCorreo.Text.Trim();
                    nroDocumento = txtNroDocumento.Text.Trim();

                    if(btnCambioClave.Checked == true)
                    {
                        cambioPassword = false;
                    }
                    else
                    {
                        cambioPassword = true; 
                    }

                    cambioPasswordAmbos = btnCambioAmbos.Checked;

                    idUsuarioRegistro = ((Usuario_LoginResult)Session["Usuario"]).idUsuario;
                    activo = Convert.ToBoolean(int.Parse(cboEstado.SelectedValue));

                    result = objUsuarioWCF.Usuario_Registrar(idEmpresa, codigoUsuario, password, nombreUsuario, LoginUsuario, idPerfil, correo, nroDocumento, cambioPassword, cambioPasswordAmbos, idUsuarioRegistro, activo);

                    //result = 0;

                    if (result> 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + cboEmpresa.SelectedValue + ");", true);
                        Limpiar_Variables();
                        lblMensaje.Text = "Exito: " + " Usuario se registro exitosamente. ";
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


            if (acbUsuario == null || acbUsuario.Text.Length < 4)
            {
                    valor = 1;
                    lblMensaje.Text = "";
                    lblMensaje.Text = lblMensaje.Text + "Seleccionar un usuario registrado en " + cboEmpresa.Text.ToString() + ".";
                    lblMensaje.CssClass = "mensajeError";
                    return valor;
            }
            else
            {
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
            }

            return valor;
        }

        public void Limpiar_Variables()
        {
            if(acbUsuario != null & acbUsuario.Text.Length > 0)
            {
                acbUsuario.Entries[0].Text = "";
            }
            txtNombre.Text = "";
            txtLogin.Text = "";
            txtCorreo.Text = "";
            txtNroDocumento.Text = "";
            cboPerfil.SelectedValue = "0";
            lblClaveUsuario.Value = "";
            lblCodigoUsuario.Value = "";
            lblMensaje.Text = "";
        }

        #region Métodos Web

        [WebMethod]
        public static AutoCompleteBoxData Agenda_UsuarioBuscar(object context)
        {
            int idEmpresa;

            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                idEmpresa = int.Parse( HttpContext.Current.Session["IdEmpresa"].ToString());
        

                UsuarioWCFClient objUsuario = new UsuarioWCFClient();

                gsUsuario_BuscarResult[] lst = objUsuario.Usuario_BuscarGenesys(idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, 0, searchString);

                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsUsuario_BuscarResult usuario in lst)
                {
                    AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                    childNode.Text = usuario.CodUsuario.ToString() + "-" +usuario.NomUsuario ;
                    childNode.Value = usuario.CodUsuario.ToString();
                    result.Add(childNode);
                }
                res.Items = result.ToArray();
            }
            return res;
        }
        #endregion

        protected void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if (acbUsuario.Entries.Count <= 0 || acbUsuario.Entries[0].Text.Length <= 0)
                    throw new ArgumentException("Debe seleccionar un cliente valido");

                lblCodigoUsuario.Value = acbUsuario.Text.Split('-')[0];
                Session["IdUsuario"] = lblCodigoUsuario.Value;

                Usuario_Buscar( int.Parse(lblCodigoUsuario.Value), null);

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
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
                    txtClave.Text = objAgendaUsuario[0].clave; 
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
            Limpiar_Variables();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar_Variables();
        }
    }
}