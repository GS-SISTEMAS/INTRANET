using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.DM;
using GS.SISGEGS.Web.CierreCostoWCF;

namespace GS.SISGEGS.Web.Contabilidad.Costos
{
    public partial class frmCierreCostos : System.Web.UI.Page
    {
        private void CierreCosto_Listar() {
            CierreCostoWCFClient objCierreCostoWCF = new CierreCostoWCFClient();
            DateTime fecha;
            try {
                List<gsCierreCosto_ListarResult> lstCierre = objCierreCostoWCF.CierreCosto_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();
                if (lstCierre.Count > 0)
                {   
                    grdCierreCosto.DataSource = lstCierre;
                    grdCierreCosto.DataBind();

                   
                    fecha = new DateTime(lstCierre.First().anho, lstCierre.First().mes, 1);
                    mpPeriodo.SelectedDate = fecha;
                    ViewState["fecha"] = fecha;
                }
                else {
                    ViewState["fecha"] = new DateTime(1900, 1, 1);
                    mpPeriodo.SelectedDate = DateTime.Now;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            try {
                if (!Page.IsPostBack) {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    CierreCosto_Listar();
                }
            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            CierreCostoWCFClient objCierreCostoWCF = new CierreCostoWCFClient();
            try {
 
                objCierreCostoWCF.CierreCosto_Registrar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, 
                    mpPeriodo.SelectedDate.Value.Month, mpPeriodo.SelectedDate.Value.Year);
                CierreCosto_Listar();
               }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}