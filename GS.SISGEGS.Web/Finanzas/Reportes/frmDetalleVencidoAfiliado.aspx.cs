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
    public partial class frmDetalleVencidoAfiliado : System.Web.UI.Page
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
                        Title = "Detalle de vencidos";
                        string strCliente = Request.QueryString["strCliente"].ToString();
                        string strVendedor = Request.QueryString["strVendedor"].ToString();
                        string strYear = Request.QueryString["strYear"].ToString();
                        string strMes = Request.QueryString["strMes"].ToString();

                        int year = Convert.ToInt32(strYear);
                        int mes = Convert.ToInt32(strMes);

                        if (strCliente == null || strCliente == "")
                        {
                            strCliente = null;
                        }

                        if (strVendedor == null || strVendedor == "")
                        {
                            strVendedor = null;
                        }

                        DeudaVencidaMes(strCliente, strVendedor, year, mes);


                        lblMensaje.Text = "Listo para mostrar vencidos";
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

        protected void cboEmpresa_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //Session["IdEmpresa"] = cboEmpresa.SelectedValue;        
            //Perfil_Cargar(int.Parse(cboEmpresa.SelectedValue), "");

        }

        public void DeudaVencidaMes(string codAgenda, string codVendedor, int anho, int mes)
        {
            DateTime fecha;
            DataTable dtTablaCruce = TablaVencido();

            if (mes == DateTime.Now.Month)
            {
                fecha = DateTime.Now;
            }
            else
            {
                DateTime firstOfNextMonth = new DateTime(anho, mes, 1).AddMonths(1);
                DateTime lastOfThisMonth = firstOfNextMonth.AddDays(-1);
                fecha = lastOfThisMonth;
            }

            try
            {
                List<gsReporteVencidosPorMesClienteResult> lstPendientes = BuscarDocumentosPendientes(codAgenda, codVendedor, fecha);
                var query_Detalle = from c in lstPendientes
                                    where c.EstadoCliente == "AFILIADA"
                                    orderby c.clientenombre ascending
                                    select new
                                    {
                                        c.id_agenda,
                                        c.clientenombre,
                                        c.DeudaMes
                                    };

                //var agencyContracts = query_Detalle
                //        .GroupBy(ac => new
                //        {
                //            ac.ID_Agenda,
                //            ac.ClienteNombre
                //        })
                //        .Select(ac => new
                //        {
                //            ID_Agenda = ac.Key.ID_Agenda,
                //            ClienteNombre = ac.Key.ClienteNombre,
                //            TotalVencido = (ac.Sum(acs => acs.Pendiente_09a30) + ac.Sum(acs => acs.Pendiente_31a60) + ac.Sum(acs => acs.Pendiente_61a120) + ac.Sum(acs => acs.Pendiente_121a360) + ac.Sum(acs => acs.Pendiente_361aMas))
                //        });


                grdVencidos.DataSource = query_Detalle;
                grdVencidos.DataBind();

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }


        }

        public List<gsReporteVencidosPorMesClienteResult> BuscarDocumentosPendientes(string idCliente, string idVendedor, DateTime fechaForm2)
        {
            DateTime fecha2;
            string Cliente;
            string Vendedor;

            Cliente = null;
            Vendedor = null;
            List<gsReporteVencidosPorMesClienteResult> lst = new List<gsReporteVencidosPorMesClienteResult>();

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (Validar_Variables() == 0)
                {
                    fecha2 = fechaForm2;
                    Cliente = idCliente;
                    Vendedor = idVendedor;

                    lst = ListarEstadoCuenta(Cliente, Vendedor,  fecha2);
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            return lst;
        }

        private DataTable TablaVencido()
        {
            DataTable dttabla = new DataTable();
            try
            {

                dttabla.Columns.Add("Periodo", typeof(string));
                dttabla.Columns.Add("Monto", typeof(string));

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            return dttabla;
        }


        private List<gsReporteVencidosPorMesClienteResult> ListarEstadoCuenta(string codAgenda, string codVendedor, DateTime fechaEmisionFinal)
        {
            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();
            try
            {
                List<gsReporteVencidosPorMesClienteResult> lst = objEstadoCuentaWCF.EstadoCuenta_VencidosMesCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, fechaEmisionFinal).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Validar_Variables()
        {
            int valor = 0;
            return valor;
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