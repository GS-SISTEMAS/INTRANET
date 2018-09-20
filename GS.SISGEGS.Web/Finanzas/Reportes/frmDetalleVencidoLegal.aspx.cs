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
    public partial class frmDetalleVencidoLegal : System.Web.UI.Page
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
            decimal deudaVencidaMes = 0;
            decimal deudaVencidaMesTotal = 0;
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
                List<gsReporte_DocumentosPendientesBIResult> lstPendientes = BuscarDocumentosPendientes(codAgenda, codVendedor, fecha);
                var query_Detalle = from c in lstPendientes
                                    where c.EstadoCliente == "LEGAL"
                                    //orderby c.ClienteNombre, c.Fecha
                                    select new
                                    {
                                        //c.TC,
                                        //c.ID_Moneda,
                                        //c.ID_Agenda,
                                        Pendiente_01a08 =  c.ImportePendiente_01a08 ,

                                        Pendiente_09a30 = c.ImportePendiente_09a30,

                                        Pendiente_31a60 = c.ImportePendiente_31a60,
                                        Pendiente_61a120 =  c.ImportePendiente_61a120,
                                        Pendiente_121a360 =  c.ImportePendiente_121a360 ,
                                        Pendiente_361aMas = c.ImportePendiente_361aMas ,
                                        Pendiente_361a720 = c.ImportePendiente_361a720 ,
                                        Pendiente_720aMas =  c.ImportePendiente_721aMas 
                                    };

                var sumImportePendiente_01a08 = query_Detalle.ToList().Select(c => c.Pendiente_01a08).Sum();
                var sumImportePendiente_09a30 = query_Detalle.ToList().Select(c => c.Pendiente_09a30).Sum();
                var sumImportePendiente_31a60 = query_Detalle.ToList().Select(c => c.Pendiente_31a60).Sum();
                var sumImportePendiente_61a120 = query_Detalle.ToList().Select(c => c.Pendiente_61a120).Sum();
                var sumImportePendiente_121a360 = query_Detalle.ToList().Select(c => c.Pendiente_121a360).Sum();
                var sumImportePendiente_361aMas = query_Detalle.ToList().Select(c => c.Pendiente_361aMas).Sum();
                var sumImportePendiente_361a720 = query_Detalle.ToList().Select(c => c.Pendiente_361a720).Sum();
                var sumImportePendiente_721aMas = query_Detalle.ToList().Select(c => c.Pendiente_720aMas).Sum();

                decimal DeudaVencida;
                decimal DeudaVencidaTotal;
                decimal Pendiente01a08;
                decimal Pendiente09a30;
                decimal Pendiente31a60;
                decimal Pendiente61a120;
                decimal Pendiente121a360;
                decimal Pendiente361a720;
                decimal Pendiente721aMas;


                DeudaVencida = Convert.ToDecimal(sumImportePendiente_09a30) + Convert.ToDecimal(sumImportePendiente_31a60) + Convert.ToDecimal(sumImportePendiente_61a120) + Convert.ToDecimal(sumImportePendiente_121a360) + Convert.ToDecimal(sumImportePendiente_361aMas);
                deudaVencidaMes = DeudaVencida;

                DeudaVencidaTotal = Convert.ToDecimal(sumImportePendiente_01a08) + Convert.ToDecimal(sumImportePendiente_09a30) + Convert.ToDecimal(sumImportePendiente_31a60) + Convert.ToDecimal(sumImportePendiente_61a120) + Convert.ToDecimal(sumImportePendiente_121a360) + Convert.ToDecimal(sumImportePendiente_361aMas);
                deudaVencidaMesTotal = DeudaVencidaTotal;

                Pendiente01a08 = Convert.ToDecimal(sumImportePendiente_01a08);
                Pendiente09a30 = Convert.ToDecimal(sumImportePendiente_09a30);
                Pendiente31a60 = Convert.ToDecimal(sumImportePendiente_31a60);
                Pendiente61a120 = Convert.ToDecimal(sumImportePendiente_61a120);
                Pendiente121a360 = Convert.ToDecimal(sumImportePendiente_121a360);
                Pendiente361a720 = Convert.ToDecimal(sumImportePendiente_361a720);
                Pendiente721aMas = Convert.ToDecimal(sumImportePendiente_721aMas);

                DataRow dtRow0 = dtTablaCruce.NewRow();
                dtRow0[0] = "Vencido 01 - 08";
                dtRow0[1] = string.Format("{0:#,##0.00}", Pendiente01a08);
                dtTablaCruce.Rows.Add(dtRow0);

                DataRow dtRow11 = dtTablaCruce.NewRow();
                dtRow11[0] = "Vencido 09 - 30";
                dtRow11[1] = string.Format("{0:#,##0.00}", Pendiente09a30);
                dtTablaCruce.Rows.Add(dtRow11);

                DataRow dtRow12 = dtTablaCruce.NewRow();
                dtRow12[0] = "Vencido 31 - 60";
                dtRow12[1] = string.Format("{0:#,##0.00}", Pendiente31a60);
                dtTablaCruce.Rows.Add(dtRow12);

                DataRow dtRow2 = dtTablaCruce.NewRow();
                dtRow2[0] = "Vencido 61 - 120";
                dtRow2[1] = string.Format("{0:#,##0.00}", Pendiente61a120);
                dtTablaCruce.Rows.Add(dtRow2);

                DataRow dtRow3 = dtTablaCruce.NewRow();
                dtRow3[0] = "Vencido 121 - 360";
                dtRow3[1] = string.Format("{0:#,##0.00}", Pendiente121a360);
                dtTablaCruce.Rows.Add(dtRow3);

                DataRow dtRow4 = dtTablaCruce.NewRow();
                dtRow4[0] = "Vencido 361 - 720";
                dtRow4[1] = string.Format("{0:#,##0.00}", Pendiente361a720);
                dtTablaCruce.Rows.Add(dtRow4);

                DataRow dtRow5 = dtTablaCruce.NewRow();
                dtRow5[0] = "Vencido 721 - más";
                dtRow5[1] = string.Format("{0:#,##0.00}", Pendiente721aMas);
                dtTablaCruce.Rows.Add(dtRow5);

                DataRow dtRow6 = dtTablaCruce.NewRow();
                dtRow6[0] = "Total Parcial: ";
                dtRow6[1] = string.Format("{0:#,##0.00}", deudaVencidaMes);
                dtTablaCruce.Rows.Add(dtRow6);

                DataRow dtRow7 = dtTablaCruce.NewRow();
                dtRow7[0] = "Total Mes: ";
                dtRow7[1] = string.Format("{0:#,##0.00}", deudaVencidaMesTotal);
                dtTablaCruce.Rows.Add(dtRow7);

                grdVencidos.DataSource = dtTablaCruce;
                grdVencidos.DataBind();

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }


        }

        public List<gsReporte_DocumentosPendientesBIResult> BuscarDocumentosPendientes(string idCliente, string idVendedor, DateTime fechaForm2)
        {
            DateTime fecha1;
            DateTime fecha2;
            DateTime fecha3;
            DateTime fecha4;
            string Cliente;
            string Vendedor;

            Cliente = null;
            Vendedor = null;
            List<gsReporte_DocumentosPendientesBIResult> lst = new List<gsReporte_DocumentosPendientesBIResult>();

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (Validar_Variables() == 0)
                {
                    fecha2 = fechaForm2;
                    fecha1 = fecha2.AddYears(-50);
                    fecha3 = fecha2.AddYears(-50);
                    fecha4 = fecha2.AddYears(50);

                    Cliente = idCliente;
                    Vendedor = idVendedor;

                    lst = ListarEstadoCuenta(Cliente, Vendedor, fecha1, fecha2, fecha3, fecha4);
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


        private List<gsReporte_DocumentosPendientesBIResult> ListarEstadoCuenta(string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal)
        {
            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();
            try
            {
                List<gsReporte_DocumentosPendientesBIResult> lst = objEstadoCuentaWCF.EstadoCuenta_ListarxClienteBI(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, 0).ToList();
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

            if (e.Item is GridDataItem)// to access a row 
            {
                if (colum == 16 || colum == 18)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    DataRowView oRow = (DataRowView)(e.Item.DataItem);

                    //string total = oRow["Periodo"].ToString();
                    //GridDataItem itemPeriodo = (GridDataItem)item["Periodo"].Controls[0];
                    //itemPeriodo.ForeColor = System.Drawing.Color.Black;
                    item.Font.Bold = true;


                }
            }

        }
    }
}