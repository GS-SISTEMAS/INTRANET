using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.EstadoCuentaWCF;
using GS.SISGEGS.Web.CobranzasWCF;
using GS.SISGEGS.Web.UsuarioWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.PerfilWCF;
using System.Web.Services;
using Telerik.Web.UI;

namespace GS.SISGEGS.Web.Finanzas.Reportes
{
    public partial class frmDetalleVenta : System.Web.UI.Page
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
                        Title = "Detalle de Ventas";
                        string strCliente = Request.QueryString["strCliente"].ToString();
                        string strVendedor = Request.QueryString["strVendedor"].ToString();
                        string strYear = Request.QueryString["strYear"].ToString();
                        string strMes = Request.QueryString["strMes"].ToString();

                        if (strCliente == null || strCliente == "")
                        {
                            strCliente = null;
                        }

                        if (strVendedor == null || strVendedor == "")
                        {
                            strVendedor = null;
                        }


                        int year = Convert.ToInt32(strYear);
                        int mes = Convert.ToInt32(strMes);


                        VentasxPeriodo(strCliente, strVendedor, year, mes);


                        lblMensaje.Text = "Listo para mostrar ventas";
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

        protected void btnCerrar_Click(object sender, EventArgs e)
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
            int idUsuarioRegistro;
            bool activo;
            int result;
            int idUsuario;

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            UsuarioWCFClient objUsuarioWCF = new UsuarioWCFClient();

            try
            {
                //if (Validar_Variables() == 0 )
                //{

                codigoUsuario = int.Parse(lblCodigoUsuario.Value);
                password = lblClaveUsuario.Value;

                idUsuarioRegistro = ((Usuario_LoginResult)Session["Usuario"]).idUsuario;
                //activo = Convert.ToBoolean(int.Parse(cboEstado.SelectedValue));
                //idUsuario = int.Parse(lblIdUsuario.Value);

                result = 1; // objUsuarioWCF.Usuario_Actualizar(idEmpresa, idUsuario, codigoUsuario, password, nombreUsuario, LoginUsuario, idPerfil, correo, nroDocumento, cambioPassword, idUsuarioRegistro, activo);

                if (result > 0)
                {
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + cboEmpresa.SelectedValue + ");", true);

                    lblMensaje.Text = "Exito: " + " Usuario se guardo exitosamente. ";
                    lblMensaje.CssClass = "mensajeExito";
                }
                else
                {
                    lblMensaje.Text = "ERROR: " + "Usuario ya se encuentra registrado. ";
                    lblMensaje.CssClass = "mensajeError";
                }
                //}

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        public void VentasxPeriodo(string id_agenda, string id_vendedor, int anho, int mes)
        {
            try
            {
                List<gsReporteVentasxDiasCobranzaResult> lstPendientes = BuscarVentasPeriodo(id_agenda, id_vendedor, anho, mes);
                var query_Venta = from c in lstPendientes
                                  where c.EstadoCliente != "AFILIADA" & c.EstadoCliente != "LEGAL"
                                  orderby c.diascredito
                                  select new
                                  {
                                      c.anho,
                                      c.mes,
                                      c.diascredito,
                                      Venta = (c.Venta * 118) / 100
                                  };


                grdVencidos.DataSource = query_Venta;
                grdVencidos.DataBind();

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }


        }

        public List<gsReporteVentasxDiasCobranzaResult> BuscarVentasPeriodo(string id_agenda, string id_vendedor, int anho, int mes)
        {
            List<gsReporteVentasxDiasCobranzaResult> lst = new List<gsReporteVentasxDiasCobranzaResult>();

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                    lst = ListarVentasPeriodo(id_agenda, id_vendedor,  anho, mes);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            return lst;
        }

        private List<gsReporteVentasxDiasCobranzaResult> ListarVentasPeriodo(string id_agenda, string id_vendedor, int anho, int mes)
        {
            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();
            try
            {
                List<gsReporteVentasxDiasCobranzaResult> lst = objEstadoCuentaWCF.EstadoCuenta_VentasDiasCobranza(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, id_agenda, id_vendedor, anho, mes).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grdVencidos_ItemDataBound1(object sender, GridItemEventArgs e)
        {
            int colum;
            int year;
            string Cliente;
            string Vendedor;
            colum = e.Item.RowIndex;

            //if (e.Item is GridDataItem)// to access a row 
            //{
            //    if (colum == 14 || colum == 16)
            //    {
            //        GridDataItem item = (GridDataItem)e.Item;
            //        DataRowView oRow = (DataRowView)(e.Item.DataItem);

            //        //string total = oRow["Periodo"].ToString();
            //        //GridDataItem itemPeriodo = (GridDataItem)item["Periodo"].Controls[0];
            //        //itemPeriodo.ForeColor = System.Drawing.Color.Black;
            //        item.Font.Bold = true;


            //    }
            //}

        }
    }
}