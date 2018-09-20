using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.CobranzasWCF;
using GS.SISGEGS.Web.UsuarioWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.PerfilWCF;
using System.Web.Services;
using Telerik.Web.UI;

namespace GS.SISGEGS.Web.Finanzas.Reportes
{
    public partial class frmDetalle : System.Web.UI.Page
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

                    //Empresa_Cargar();
                    if (Request.QueryString["strYear"] != "")
                    {
                        Title = "Detalle de cobranza";
                        string strCliente = Request.QueryString["strCliente"].ToString();
                        string strVendedor = Request.QueryString["strVendedor"].ToString();
                        string strYear =  Request.QueryString["strYear"].ToString();
                        string strMes = Request.QueryString["strMes"].ToString();

                        int year = Convert.ToInt32(strYear);
                        int mes = Convert.ToInt32(strMes);

                        if (strCliente == null || strCliente == "" )
                        {
                            strCliente = null;
                        }

                        if (strVendedor == null || strVendedor == "")
                        {
                            strVendedor = null;
                        }

                        Cobranza_Buscar(strCliente, strVendedor, year, mes );


                        lblMensaje.Text = "Listo para mostrar cobranza";
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

        private void Perfil_Cargar(int idEmpresa, string descripcion)
        {
            string ValorCero;
            PerfilWCFClient objPerfilWCF = new PerfilWCFClient();
            List<Perfil_ListarResult> listPerfil = new List<Perfil_ListarResult>();

            ValorCero = "SELECCIONAR";

            try
            {
                listPerfil = objPerfilWCF.Perfil_Listar(idEmpresa, "").ToList();
                if (listPerfil.Count > 0)
                {
                    foreach (Perfil_ListarResult objPerfil in listPerfil)
                    {
                        Telerik.Web.UI.RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem(objPerfil.nombrePerfil.ToString().Trim(), objPerfil.idPerfil.ToString());
                        //cboPerfil.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void Cobranza_Buscar(string Cliente, string Vendedor, int year, int mes)
        {
            CobranzasWCFClient objCobranzaWCF;
           gsReporteCobranzaWeb_DetalleMesResult[] objCobranzaDetalle;

            int idEmpresa;
            try
            {
                objCobranzaWCF = new CobranzasWCFClient();
                idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;

                objCobranzaDetalle = objCobranzaWCF.Reporte_CancelacionesResumenDetalleMes(idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,Cliente,Vendedor,year, mes);

                var query_Venta = from c in objCobranzaDetalle
                                  where c.ClaseCliente != "LEGAL"
                                  select new
                                  {
                                      c.periodoMesC,
                                      c.periodoMesE,
                                      c.periodoYearC,
                                      c.periodoYearE,
                                      c.TotalVentaMes,
                                      c.totalMes
                                  };


                grdCobranzasMes.DataSource = query_Venta;
                grdCobranzasMes.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void cboEmpresa_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //Session["IdEmpresa"] = cboEmpresa.SelectedValue;        
            //Perfil_Cargar(int.Parse(cboEmpresa.SelectedValue), "");

        }

    }
}