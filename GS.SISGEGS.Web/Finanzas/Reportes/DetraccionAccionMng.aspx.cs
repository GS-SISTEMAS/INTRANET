using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.FinanzasWCF;
using GS.SISGEGS.Web.Helpers;

namespace GS.SISGEGS.Web.Finanzas.Reportes
{
    public partial class DetraccionAccionMng : System.Web.UI.Page
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

                    Title = "Registrar Accion";
                    string obj = Request.QueryString["op"];
                    string idAgenda = Request.QueryString["idAgenda"];
                    string serie = Request.QueryString["Serie"];
                    string numero = Request.QueryString["Numero"];
                    
                    int op = JsonHelper.JsonDeserialize<int>(Request.QueryString["op"]);
                    ViewState["op"] = op;
                    ViewState["idAgenda"] = idAgenda;
                    ViewState["serie"] = serie;
                    ViewState["numero"] = numero;
                    
                    CargarAccion(op);
                }
            }
            catch (Exception ex)
            {
                //lblMensaje.Text = "ERROR: " + ex.Message;
                //lblMensaje.CssClass = "mensajeError";
            }
        }

        private void CargarAccion(int op)
        {
            FinanzasWCFClient objFinanzasWCF = new FinanzasWCFClient();

            try
            {
                GS_DetraccionAccionGetResult accion =
                    objFinanzasWCF.GetDetraccionAccion(((Usuario_LoginResult) Session["Usuario"]).idEmpresa,
                        ((Usuario_LoginResult) Session["Usuario"]).codigoUsuario, op);

                ViewState["accion"] = JsonHelper.JsonSerializer(accion);

                if (string.IsNullOrEmpty(accion.NroConstancia)) return;
                rtbNroContancia.Text = accion.NroConstancia;
                dpFechaConstancia.SelectedDate = accion.FechaPago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnGuardar_OnClick(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            FinanzasWCFClient objFinanzasWCF = new FinanzasWCFClient();

            try
            {
                var accion = JsonHelper.JsonDeserialize<GS_DetraccionAccionGetResult>((string)ViewState["accion"]);
                if (!string.IsNullOrEmpty(accion.NroConstancia))
                {
                    lblMensaje.Text = "Ya existe un pago registrado";
                }
                else
                {
                    var objInsert = new GS_DetraccionAccionGetResult
                    {
                        NroConstancia = rtbNroContancia.Text,
                        FechaPago = dpFechaConstancia.SelectedDate.Value,
                        Op = Convert.ToInt32(ViewState["op"].ToString())
                    };
                    objFinanzasWCF.Accion_Registrar(((Usuario_LoginResult) Session["Usuario"]).idEmpresa,
                        ((Usuario_LoginResult) Session["Usuario"]).codigoUsuario, objInsert);

                    //proceso de envio de correo
                    AgendaWCF.AgendaWCFClient AgendaClient = new AgendaWCF.AgendaWCFClient();                    
                    //recupera el correo
                    
                    string idAgenda = ViewState["idAgenda"].ToString() ;                    
                    string serie = ViewState["serie"].ToString();
                    string numero = ViewState["numero"].ToString();

                    string fecha = objInsert.FechaPago.ToString();

                    List< GS_RecuperaCorreoAgendaResult > lista= AgendaClient.RecuperaCorreoAgenda(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                        ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda).ToList();

                    

                    string mensaje = @"
<html>
	<head>
	</head>
	<body>
		<table style='width: 700px; ' cellpadding='0' cellspacing='0'  >
			<tr>
				<td style='width: 25%;'>
					<img src='https://intranet.gruposilvestre.com.pe/IntranetGS/Images/Logos/grupo.png' alt='' height=80 />
				</td>
				<td style='width: 75%; font-size: 18pt; font-weight: bold;'>
					CONSTANCIA DE DETRACCI&Oacute;N
				</td>
			</tr>
			<tr>
				<td colspan='2' style='padding-top:20px;'>
					Estimado cliente, se ha generado la constancia de detracci&oacute;n n&uacute;mero {0} por la factura {1}. El registro de la constancia se realizó el día {2}.
				</td>
			</tr>
			<tr>
				<td colspan='2' style='padding-top:20px;'>
					Cualquier duda o consulta, no duden en comunicarse con nosotros al telefono 617-3300 o por correo electronico a sec@gruposilvestre.com.pe
				</td>
			</tr>
			<tr>
				<td colspan='2' style='padding-top:20px;'>	
					Atte.
				</td>
			</tr>
			<tr>
				<td colspan='2' style='padding-top:20px;'>	
					<strong>Grupo Silvestre</strong>
				</td>
			</tr>
		</table>
	</body>
</html>
";

                    //con diez ceros

                    int largonumero = numero.Length;
                    int faltaceros = 10 - largonumero;
                    int cerosActuales = 0;
                    string completaceros = "";

                    if (faltaceros > 0) {
                        while (cerosActuales < faltaceros)
                        {
                            completaceros += "0";
                            cerosActuales = completaceros.Length;
                        }
                    }

                    string mail = "";
                    
                    string formatMensaje = string.Format(mensaje, objInsert.NroConstancia, serie + "-" + completaceros + numero, fecha);

                    CorreoWCF.CorreoWCFClient CorreoClient = new CorreoWCF.CorreoWCFClient();
                    //Enviar el mensaje si se tiene mail
                    if (lista != null && lista.Count > 0)
                    {
                        mail = lista[0].email;

                        //CorreoClient.MerlinEnviarCorreo("cbocanegra@inclouds.biz", "cbocanegra@inclouds.biz", null, null, "Constancia de detracción", formatMensaje);
                        CorreoClient.MerlinEnviarCorreo(mail, mail, null, null, "Constancia de detracción", formatMensaje);

                    }

                    //ViewState["op"] = null;
                    ViewState.Clear();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind();", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}