using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using GS.SISGEGS.Web.Helpers;
using System.Web.UI.WebControls;
using GS.SISGEGS.Web.ContratosWCF;
using GS.SISGEGS.Web.MarcasWCF;
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.Web.Contratos.Reportes
{
    public partial class frmMarcasHist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            if (!ConnectionHelpers.CheckForInternetConnection())
                throw new ArgumentException("ERROR: Revisar su conexión a internet.");

            try
            {
                if (!Page.IsPostBack)
                {

                    var idMarca = int.Parse((Request.QueryString["idMarca"]));
                    rdpFechaIni.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-3).Month, 1);
                    rdpFechaFin.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(1);

                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    lblTitulo.Text = "Historico Marca";
                    Page.Title = "Historico Marca";

                    BuscarHistorial();

                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
         }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarHistorial();
        }

        private void BuscarHistorial() {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                var idMarca = int.Parse((Request.QueryString["idMarca"]));

                MarcasWCFClient objMarcasWCF = new MarcasWCFClient();

                grdGeneralMarcas.DataSource = objMarcasWCF.HistoricoMarca_Listar(idMarca, rdpFechaIni.SelectedDate.Value, rdpFechaFin.SelectedDate.Value);
                grdGeneralMarcas.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
}
}