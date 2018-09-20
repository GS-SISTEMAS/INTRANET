using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using GS.SISGEGS.BE;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.DocumentoWCF;
using GS.SISGEGS.Web.FormaPagoWCF;
using GS.SISGEGS.Web.OrdenVentaWCF;
using GS.SISGEGS.Web.LoginWCF;
using GS.SISGEGS.Web.OrdenCompraWCF;
using GS.SISGEGS.Web.PedidoWCF;
using GS.SISGEGS.Web.CreditoWCF;
using GS.SISGEGS.Web.EstadoCuentaWCF;



namespace GS.SISGEGS.Web.Finanzas.Aprobacion
{
    public partial class frmOrdenVentaSecAprob2 : System.Web.UI.Page
    {
        CreditoWCFClient objCreditoWCF = new CreditoWCFClient();
        string IdAgenda;
        Int32 IdPedido;
        Int32 Op;
        string idSectorista;
        string comentario;
        decimal ValorVenta;
        int Id_moneda;
        string Aprobacion;
        Int32 Guia;
        decimal totalDeudaVencida;
        decimal totalletras;

        //private void CargarData()
        //{
        //    txtTotalDeudaVen.Text = totalDeudaVencida.ToString("#,##0.00");
        //    txtTotalLetras.Text = totalletras.ToString("#,##0.00");
            
        //}
        private void Registrar_Aprobacion()//string idAgenda, int IdPedido, int Op, string idSectorista, string comentario, decimal ValorVenta, int id_moneda, string Aprobacion, int Guia)
        {
            PedidoWCFClient objPedidoWCF = new PedidoWCFClient();
            gsAgendaCliente_BuscarLimiteCreditoResult objLimite = new gsAgendaCliente_BuscarLimiteCreditoResult();

            decimal porcentaje = 0;
            decimal ValorPedido = 0;
            decimal LineaMaximo = 0;
            decimal LineaCredito = 0;
            decimal Deuda = 0;
            decimal DeudaIncluida = 0;
            decimal TC = 0;

            var idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
            var idUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;


            IdAgenda = ((Request.QueryString["Id_Cliente"]));
            idSectorista = ((Request.QueryString["idSectorista"]));
            comentario = ((Request.QueryString["comentario"]));
            Aprobacion = ((Request.QueryString["Aprobacion"]));

            IdPedido = Int32.Parse((Request.QueryString["IdPedido"]));
            Op = Int32.Parse((Request.QueryString["Op"]));
            ValorVenta = decimal.Parse((Request.QueryString["ValorVenta"]));
            Id_moneda = Int32.Parse((Request.QueryString["Id_moneda"]));
            Guia = Int32.Parse((Request.QueryString["Guia"]));

            totalDeudaVencida = decimal.Parse((Request.QueryString["totalDeudaVencida"]));
            totalletras = decimal.Parse((Request.QueryString["totalletras"]));

            try
            {
                if (((Usuario_LoginResult)Session["Usuario"]).aprobarPedido == true)
                {
                    ValorPedido = ValorVenta;
                    porcentaje = (decimal.Parse(((Usuario_LoginResult)Session["Usuario"]).aprobarPorcentaje.ToString()) / 100);
                    objLimite = ListarClientesResumen(IdAgenda);

                    if (!string.IsNullOrEmpty(objLimite.ID_Agenda))
                    {

                        TC = (decimal)objLimite.TC;
                        LineaCredito = decimal.Parse(objLimite.LineaCredito.ToString());
                        if (Id_moneda == 1)
                        {
                            LineaCredito = LineaCredito * TC;
                        }
                        LineaMaximo = LineaCredito + (LineaCredito * porcentaje);
                        Deuda = decimal.Parse(objLimite.TotalRiesgo.ToString());

                        if (Id_moneda == 1)
                        {
                            Deuda = Deuda * TC;
                        }


                        if (Guia == 1 || Guia == 2)
                        {
                            DeudaIncluida = Deuda;
                        }
                        else
                        {
                            if (Aprobacion == "True")
                            {
                                DeudaIncluida = Deuda;
                            }
                            else
                            {
                                DeudaIncluida = Deuda + ValorPedido;
                            }
                        }


                        if (DeudaIncluida <= LineaMaximo)
                        {
                            objPedidoWCF.Pedido_Aprobar(idEmpresa, idUsuario, IdPedido, Op, idSectorista, true, comentario);
                            objPedidoWCF.gsDocVentaAprobacion_Registrar(idEmpresa, IdPedido, Op, IdAgenda, idUsuario);
                            rwmOrdenVenta.RadAlert("Se aprobó el pedido correctamente. ", 200, 150, "Aprobación de Pedidos", null);
                            objCreditoWCF = new CreditoWCFClient();
                           


                            objCreditoWCF.Enviar_Notificacion_Aprobacion2(idEmpresa, idUsuario, IdAgenda, txtCliente.Text, Op.ToString(),
                                Convert.ToDecimal(txtTotalLetrasS.Text.Trim() == string.Empty ? "0" : txtTotalLetrasS.Text.Trim()),
                                Convert.ToDecimal(txtTotalDeudaVenS.Text.Trim() == string.Empty ? "0" : txtTotalDeudaVenS.Text.Trim()),
                                Convert.ToDecimal(txtTotalLetrasN.Text.Trim() == string.Empty ? "0" : txtTotalLetrasN.Text.Trim()),
                                Convert.ToDecimal(txtTotalDeudaVenN.Text.Trim() == string.Empty ? "0" : txtTotalDeudaVenN.Text.Trim()),
                                Convert.ToDecimal(txtTotalLetrasI.Text.Trim() == string.Empty ? "0" : txtTotalLetrasI.Text.Trim()),
                                Convert.ToDecimal(txtTotalDeudaVenI.Text.Trim() == string.Empty ? "0" : txtTotalDeudaVenI.Text.Trim()),
                                ((Usuario_LoginResult)Session["Usuario"]).nombres,txtcomentarios.Text);


                            objCreditoWCF.RegistrarLog_AprobacionDeudaVencida(idEmpresa, idUsuario, IdAgenda, txtCliente.Text, Op.ToString(),
                                Convert.ToDecimal(txtTotalLetrasS.Text == string.Empty ? "0" : txtTotalLetrasS.Text), Convert.ToDecimal(txtTotalDeudaVenS.Text == string.Empty ? "0" : txtTotalDeudaVenS.Text),
                                Convert.ToDecimal(txtTotalLetrasN.Text == string.Empty ? "0" : txtTotalLetrasN.Text), Convert.ToDecimal(txtTotalDeudaVenN.Text == string.Empty ? "0" : txtTotalDeudaVenN.Text),
                                Convert.ToDecimal(txtTotalLetrasI.Text == string.Empty ? "0" : txtTotalLetrasI.Text), Convert.ToDecimal(txtTotalDeudaVenI.Text == string.Empty ? "0" : txtTotalDeudaVenI.Text),txtcomentarios.Text);

                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + Op.ToString() + ");", true);
                        }
                        else
                        {
                            
                            rwmOrdenVenta.RadAlert("El porcentaje de sobregiro asignado no es suficiente. Solicitar la aprobación de su Superior. ", 400, null, "Mensaje de error", null);
                        }
                    }
                    else
                    {
                        rwmOrdenVenta.RadAlert("El cliente no tiene linea de crédito, solicitar a C&C. ", 400, null, "Mensaje de error", null);
                    }
                }
                else
                {
                    rwmOrdenVenta.RadAlert("No tiene autorización para aprobar pedidos. ", 400, null, "Mensaje de error", null);
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }

        }
        private gsAgendaCliente_BuscarLimiteCreditoResult ListarClientesResumen(string IdAgenda)
        {
            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();
            gsAgendaCliente_BuscarLimiteCreditoResult Limite = new gsAgendaCliente_BuscarLimiteCreditoResult();
            try
            {
                List<gsAgendaCliente_BuscarLimiteCreditoResult> LimiteCreditoAgenda = objEstadoCuentaWCF.EstadoCuenta_LimiteCreditoxCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, IdAgenda, 0).ToList();
                if (LimiteCreditoAgenda.Count > 0)
                {
                    Limite = LimiteCreditoAgenda[0];
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Limite;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (!Page.IsPostBack)
                {
                    LoginWCFClient objLoginWCF = new LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idUsuario);

                    USP_SEL_Verificacion_DeudaVencidaResult[] lstDv = null;
                    USP_SEL_Verificacion_LetrasxAceptarResult[] lstlet = null;
                    IdAgenda = ((Request.QueryString["Id_Cliente"]));
                    objCreditoWCF.ObtenerVerificacionAprobacion2(1, ((Usuario_LoginResult)Session["Usuario"]).idEmpresa, IdAgenda, ref lstDv, ref lstlet);

                    decimal totaldeuda;//decimal.Parse((Request.QueryString["totalDeudaVencida"]));
                    decimal totalletra;//= decimal.Parse((Request.QueryString["totalletras"]));

                    //txtTotalDeudaVen.Text = totaldeuda.ToString("#,##0.00");
                    //txtTotalLetras.Text = totalletra.ToString("#,##0.00");

                    totaldeuda=Convert.ToDecimal(lstDv.Where(x => x.Empresa == "Silvestre").Select(x => x.DeudaVencida).Sum());
                    txtTotalDeudaVenS.Text = totaldeuda.ToString("#,##0.0000");

                    totaldeuda = Convert.ToDecimal(lstDv.Where(x => x.Empresa == "NeoAgrum").Select(x => x.DeudaVencida).Sum());
                    txtTotalDeudaVenN.Text = totaldeuda.ToString("#,##0.0000");

                    totaldeuda = Convert.ToDecimal(lstDv.Where(x => x.Empresa == "Inatec").Select(x => x.DeudaVencida).Sum());
                    txtTotalDeudaVenI.Text = totaldeuda.ToString("#,##0.0000");

                    totalletra = Convert.ToDecimal(lstlet.Where(x => x.Empresa == "Silvestre").Select(x => x.Deuda_mayor30Dias).Sum());
                    txtTotalLetrasS.Text = totalletra.ToString("#,##0.0000");

                    totalletra = Convert.ToDecimal(lstlet.Where(x => x.Empresa == "NeoAgrum").Select(x => x.Deuda_mayor30Dias).Sum());
                    txtTotalLetrasN.Text = totalletra.ToString("#,##0.0000");

                    totalletra = Convert.ToDecimal(lstlet.Where(x => x.Empresa == "Inatec").Select(x => x.Deuda_mayor30Dias).Sum());
                    txtTotalLetrasI.Text = totalletra.ToString("#,##0.0000");

                    txtRuccliente.Text= ((Request.QueryString["Id_Cliente"]));
                    
                    lblTitulo.Text = "Verificación de Deudas : Nro de Op " + (Request.QueryString["Op"]).ToString();
                    txtCliente.Text =Convert.ToString(Session["NombreClienteAprobacion"]); //((Request.QueryString["NombreCliente"]));
                    Session.Remove("NombreClienteAprobacion");

                    lblMensaje.Text = "La página cargo correctamente";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnAprobar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            lblMensaje.Text = "";
            try
            {
                Registrar_Aprobacion();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Op = Int32.Parse((Request.QueryString["Op"]));
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + Op.ToString() + ");", true);
        }
    }
}