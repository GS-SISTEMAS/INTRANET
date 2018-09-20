using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.EgresosWCF;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.Web.Comercial.Gastos
{
    public partial class frmGastosResumen : System.Web.UI.Page
    {
        private void EgresosVarios_Cargar(int idOperacion)
        {
            EgresosWCFClient objEgresoWCF = new EgresosWCFClient();
            bool? bloqueado = null;
            string mensajeBloqueado = null;
            gsEgresosVarios_BuscarDetalleResult[] lstEgresosVarios_Detalle = null;
            gsEgresosVarios_BuscarCabeceraResult objEgresosVarios;
            try
            {
                objEgresosVarios = objEgresoWCF.EgresosVarios_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idOperacion, ref bloqueado, ref mensajeBloqueado,
                    ref lstEgresosVarios_Detalle);

                lblSerieNro.Text = objEgresosVarios.Serie + "-" + objEgresosVarios.NroDocumento;
                lblNomVendedor.Text = objEgresosVarios.ID_Agenda + "-" + objEgresosVarios.AgendaNombre;
                lblFechaEmision.Text = objEgresosVarios.Vcmto.ToString("dd/MM/yyyy");
                lblMotivo.Text = objEgresosVarios.NombreDocumento;
                lblFechaInicio.Text = objEgresosVarios.FechaInicio.ToString("dd/MM/yyyy");
                lblFechaFinal.Text = objEgresosVarios.Vcmto.ToString("dd/MM/yyyy");

                grdResumen.DataSource = lstEgresosVarios_Detalle.ToList().FindAll(x => x.Estado == 1); ;
                grdResumen.DataBind();
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

            try
            {
                if (!Page.IsPostBack)
                {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    EgresosVarios_Cargar(int.Parse((Request.QueryString["idOperacion"])));
                    this.Title = "Resumen de planilla de pago";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}