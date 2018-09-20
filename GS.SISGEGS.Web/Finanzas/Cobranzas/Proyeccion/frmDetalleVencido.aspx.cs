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

namespace GS.SISGEGS.Web.Finanzas.Cobranzas
{
    public partial class frmDetalleVencido : System.Web.UI.Page
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
                    if (Request.QueryString["strCliente"] != "")
                    {
                        Title = "Detalle de vencidos";
                        string strCliente = Request.QueryString["strCliente"].ToString();
                        int year = DateTime.Now.Year;
                        int mes = DateTime.Now.Month;
                        int lenth; 

                        if (strCliente == null || strCliente == "")
                        {
                            strCliente = null;
                        }

                        lenth = strCliente.Length; 
                        strCliente = strCliente.Substring(1, lenth - 1); 

                        DeudaVencidaMes(strCliente, null, year, mes);

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
                List<gsReporte_DocumentosPendientesResult> lstPendientes = BuscarDocumentosPendientes(codAgenda, codVendedor, fecha);
                var query_Detalle = from c in lstPendientes
                                    where c.EstadoCliente != "AFILIADA" & c.ID_EstadoDoc != 683
                                    orderby c.ClienteNombre, c.Fecha
                                    select new
                                    {
                                        c.TC,
                                        c.ID_Moneda,
                                        c.ID_Agenda,


                                        PorVencer = c.ID_Moneda == 0 ? c.ImportePendiente_NoVencido :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_NoVencido / c.TC :
                                                    c.ImportePendiente_NoVencido,
                                        PorVencer_01a30 = c.ID_Moneda == 0 ? c.ImportePendiente_PorVencer30 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_PorVencer30 / c.TC :
                                                    c.ImportePendiente_NoVencido,

                                        Pendiente_01a08 = c.ID_Moneda == 0 ? c.ImportePendiente_01a08 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_01a08 / c.TC :
                                                    c.ImportePendiente_01a08,

                                        Pendiente_09a30 = c.ID_Moneda == 0 ? c.ImportePendiente_09a30 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_09a30 / c.TC :
                                                    c.ImportePendiente_09a30,
                                        Pendiente_31a60 = c.ID_Moneda == 0 ? c.ImportePendiente_31a60 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_31a60 / c.TC :
                                                    c.ImportePendiente_31a60,
                                        Pendiente_61a120 = c.ID_Moneda == 0 ? c.ImportePendiente_61a120 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_61a120 / c.TC :
                                                    c.ImportePendiente_61a120,
                                        Pendiente_121a360 = c.ID_Moneda == 0 ? c.ImportePendiente_121a360 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_121a360 / c.TC :
                                                    c.ImportePendiente_121a360,
                                        Pendiente_361aMas = c.ID_Moneda == 0 ? c.ImportePendiente_361aMas :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_361aMas / c.TC :
                                                    c.ImportePendiente_361aMas,

                                        Pendiente_361a720 = c.ID_Moneda == 0 ? c.ImportePendiente_361a720 :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_361a720 / c.TC :
                                                    c.ImportePendiente_361a720,

                                        Pendiente_720aMas = c.ID_Moneda == 0 ? c.ImportePendiente_721aMas :
                                                    c.ID_Moneda == 1 ? c.ImportePendiente_721aMas / c.TC :
                                                    c.ImportePendiente_721aMas
                                    };
                var sumImportePendiente_PorVencer01a30 = query_Detalle.ToList().Select(c => c.PorVencer_01a30).Sum();
                var sumImportePendiente_PorVencer = query_Detalle.ToList().Select(c => c.PorVencer).Sum();
                var sumImportePendiente_01a08 = query_Detalle.ToList().Select(c => c.Pendiente_01a08).Sum();
                var sumImportePendiente_09a30 = query_Detalle.ToList().Select(c => c.Pendiente_09a30).Sum();
                var sumImportePendiente_31a60 = query_Detalle.ToList().Select(c => c.Pendiente_31a60).Sum();
                var sumImportePendiente_61a120 = query_Detalle.ToList().Select(c => c.Pendiente_61a120).Sum();
                var sumImportePendiente_121a360 = query_Detalle.ToList().Select(c => c.Pendiente_121a360).Sum();
                var sumImportePendiente_361aMas = query_Detalle.ToList().Select(c => c.Pendiente_361aMas).Sum();
                var sumImportePendiente_361a720 = query_Detalle.ToList().Select(c => c.Pendiente_361a720).Sum();
                var sumImportePendiente_721aMas = query_Detalle.ToList().Select(c => c.Pendiente_720aMas).Sum();

                decimal DeudaTotal;
                decimal DeudaVencida;
                decimal DeudaVencidaTotal;
                decimal Pendiente01a08;
                decimal Pendiente09a30;
                decimal Pendiente31a60;
                decimal Pendiente61a120;
                decimal Pendiente121a360;
                decimal Pendiente361a720;
                decimal Pendiente721aMas;
                decimal Pendiente_PorVencer;
                decimal Pendiente_PorVencer01a30;

                DeudaVencida = Convert.ToDecimal(sumImportePendiente_09a30) + Convert.ToDecimal(sumImportePendiente_31a60) + Convert.ToDecimal(sumImportePendiente_61a120) + Convert.ToDecimal(sumImportePendiente_121a360) + Convert.ToDecimal(sumImportePendiente_361aMas);
                deudaVencidaMes = DeudaVencida;

                DeudaVencidaTotal = Convert.ToDecimal(sumImportePendiente_01a08) + Convert.ToDecimal(sumImportePendiente_09a30) + Convert.ToDecimal(sumImportePendiente_31a60) + Convert.ToDecimal(sumImportePendiente_61a120) + Convert.ToDecimal(sumImportePendiente_121a360) + Convert.ToDecimal(sumImportePendiente_361aMas);
                deudaVencidaMesTotal = DeudaVencidaTotal;
                Pendiente_PorVencer = Convert.ToDecimal(sumImportePendiente_PorVencer);
                Pendiente_PorVencer01a30 = Convert.ToDecimal(sumImportePendiente_PorVencer01a30);
                DeudaTotal = DeudaVencidaTotal + Pendiente_PorVencer;

                Pendiente01a08 = Convert.ToDecimal(sumImportePendiente_01a08);
                Pendiente09a30 = Convert.ToDecimal(sumImportePendiente_09a30);
                Pendiente31a60 = Convert.ToDecimal(sumImportePendiente_31a60);
                Pendiente61a120 = Convert.ToDecimal(sumImportePendiente_61a120);
                Pendiente121a360 = Convert.ToDecimal(sumImportePendiente_121a360);
                Pendiente361a720 = Convert.ToDecimal(sumImportePendiente_361a720);
                Pendiente721aMas = Convert.ToDecimal(sumImportePendiente_721aMas);

                DataRow dtRow000 = dtTablaCruce.NewRow();
                dtRow000[0] = "PorVencer 01 - 30";
                dtRow000[1] = string.Format("{0:#,##0.00}", Pendiente_PorVencer01a30);
                dtTablaCruce.Rows.Add(dtRow000);

                DataRow dtRow00 = dtTablaCruce.NewRow();
                dtRow00[0] = "PorVencer";
                dtRow00[1] = string.Format("{0:#,##0.00}", Pendiente_PorVencer);
                dtTablaCruce.Rows.Add(dtRow00);

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
                dtRow6[0] = "ParcialVencida: ";
                dtRow6[1] = string.Format("{0:#,##0.00}", deudaVencidaMes);
                dtTablaCruce.Rows.Add(dtRow6);

                DataRow dtRow7 = dtTablaCruce.NewRow();
                dtRow7[0] = "TotalVencida: ";
                dtRow7[1] = string.Format("{0:#,##0.00}", deudaVencidaMesTotal);
                dtTablaCruce.Rows.Add(dtRow7);

                DataRow dtRow8 = dtTablaCruce.NewRow();
                dtRow8[0] = "TotalDeuda: ";
                dtRow8[1] = string.Format("{0:#,##0.00}", DeudaTotal);
                dtTablaCruce.Rows.Add(dtRow8);

                grdVencidos.DataSource = dtTablaCruce;
                grdVencidos.DataBind();

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }


        }

        public List<gsReporte_DocumentosPendientesResult> BuscarDocumentosPendientes(string idCliente, string idVendedor, DateTime fechaForm2)
        {
            DateTime fecha1;
            DateTime fecha2;
            DateTime fecha3;
            DateTime fecha4;
            string Cliente;
            string Vendedor;

            Cliente = null;
            Vendedor = null;
            List<gsReporte_DocumentosPendientesResult> lst = new List<gsReporte_DocumentosPendientesResult>();

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

        private List<gsReporte_DocumentosPendientesResult> ListarEstadoCuenta(string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal)
        {
            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();
            try
            {
                List<gsReporte_DocumentosPendientesResult> lst = objEstadoCuentaWCF.EstadoCuenta_ListarxCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, 0,0).ToList();
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
                if (colum == 20 || colum == 22 || colum == 24)
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