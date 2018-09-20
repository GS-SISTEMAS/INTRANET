using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Services;
using Telerik.Web.UI;
using GS.SISGEGS.BE;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.DireccionWCF;
using GS.SISGEGS.Web.SedeWCF;
using GS.SISGEGS.Web.EnvioWCF;
using GS.SISGEGS.Web.CreditoWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.DocumentoWCF;
using GS.SISGEGS.Web.OrdenVentaWCF;
using GS.SISGEGS.Web.DespachoWCF;
using GS.SISGEGS.Web.MonedaWCF;
using GS.SISGEGS.Web.FormaPagoWCF;
using GS.SISGEGS.Web.ItemWCF;
using GS.SISGEGS.Web.ImpuestoWCF;
using GS.SISGEGS.Web.VarianteWCF;
using GS.SISGEGS.Web.GuiaWCF;

namespace GS.SISGEGS.Web.Almacen.Operacion
{
    public partial class frmGuiaVentasMng : System.Web.UI.Page
    {
        #region Métodos privados
        private void Variante_Cargar()
        {
            VarianteWCFClient objVarianteWCF;
            Variante_BuscarResult objVariante;
            try
            {
                objVarianteWCF = new VarianteWCFClient();
                objVariante = objVarianteWCF.Variante_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, "PEDMNG");
                //    cboAlmacen.SelectedValue = cboAlmacen.Items.FindItem(x => x.Text.ToUpper() == objVariante.parametro1.ToUpper()).Value;
                //    cboOpDocVenta.SelectedValue = cboOpDocVenta.Items.FindItem(x => x.Text.ToUpper() == objVariante.parametro2.ToUpper()).Value;
                //    cboFormaPago.SelectedValue = cboFormaPago.Items.FindItem(x => x.Text.ToUpper() == objVariante.parametro3.ToUpper()).Value;
                //    cboSede.SelectedValue = cboSede.Items.FindItem(x => x.Text.ToUpper() == objVariante.parametro4.ToUpper()).Value;
                //    lblTrans.Text = objVariante.parametro5;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GuiaVentas_Cargar(int idOperacion)
        {
            GuiaWCFClient objGuiaVentasWCF = new GuiaWCFClient();

            gsGuiaVenta_ListarxOPResult[] objGuiaVentasCab;

            try
            {
                Session["Id_Transporte"] = null;

                objGuiaVentasCab = objGuiaVentasWCF.GuiaVenta_ListarxOP(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idOperacion);

                lblCodigoCliente.Value = objGuiaVentasCab[0].id_agenda;
                lblCodigoOrigen.Value = objGuiaVentasCab[0].DireccionOrigenAgenda;
                lblCodigoDestino.Value = objGuiaVentasCab[0].DireccionDestinoAgenda;
                lblCodigoTransportista.Value = objGuiaVentasCab[0].ID_Transportista;

                Direccion_Cargar(lblCodigoCliente.Value, cboFacturacionCliente);
                Despacho_Cargar(lblCodigoCliente.Value, cboDespacho);
                Cliente_Buscar(lblCodigoCliente.Value);

                Despacho_Cargar(lblCodigoOrigen.Value, cboFacturacionOrigen);
                Sucursal_Cargar(lblCodigoOrigen.Value, cboSucursalOrigen);
                Origen_Buscar(lblCodigoOrigen.Value);

                if (!string.IsNullOrEmpty(cboSucursalOrigen.SelectedValue))
                    Referencia_Cargar(lblCodigoOrigen.Value, Int32.Parse(cboSucursalOrigen.SelectedValue), cboReferenciaOrigen);

                Despacho_Cargar(lblCodigoDestino.Value, cboFacturacionDestino);
                Sucursal_Cargar(lblCodigoDestino.Value, cboSucursalDestino);
                Destino_Buscar(lblCodigoDestino.Value);

                if (!string.IsNullOrEmpty(cboSucursalDestino.SelectedValue))
                    Referencia_Cargar(lblCodigoDestino.Value, Int32.Parse(cboSucursalDestino.SelectedValue), cboReferenciaDestino);


                if(lblCodigoTransportista.Value != "0")
                {
                    Transporte_Buscar(lblCodigoTransportista.Value);
                }

 
                txtIDChofer.Text = objGuiaVentasCab[0].ID_Chofer.ToString();
                txtChofer.Text =  objGuiaVentasCab[0].TransportistaChofer.ToString();
                txtLicencia.Text = objGuiaVentasCab[0].TransportistaLicencia.ToString();

                txtVehiculoPlaca.Text = objGuiaVentasCab[0].ID_Vehiculo.ToString();
                txtVehiculoMarca.Text = objGuiaVentasCab[0].TransportistaMarca.ToString();
                txtVehiculoModelo.Text = objGuiaVentasCab[0].TransportistaModelo.ToString();
                txtCertificado.Text = objGuiaVentasCab[0].TransportistaCertInscripcion.ToString();

                cboFacturacionCliente.SelectedValue = objGuiaVentasCab[0].ID_AgendaDireccion.ToString();
                cboDespacho.SelectedValue = objGuiaVentasCab[0].ID_AgendaDireccion2.ToString();

                cboFacturacionOrigen.SelectedValue = objGuiaVentasCab[0].DireccionOrigenDireccion.ToString();
                cboSucursalOrigen.SelectedValue = objGuiaVentasCab[0].DireccionOrigenSucursal.ToString();
                cboReferenciaOrigen.SelectedValue = objGuiaVentasCab[0].DireccionOrigenReferencia.ToString();

                cboFacturacionDestino.SelectedValue = objGuiaVentasCab[0].DireccionDestinoDireccion.ToString();
                cboSucursalDestino.SelectedValue = objGuiaVentasCab[0].DireccionDestinoSucursal.ToString();
                cboReferenciaDestino.SelectedValue = objGuiaVentasCab[0].DireccionDestinoReferencia.ToString();

                //------------------------------------
                dpFechaDespacho.SelectedDate = objGuiaVentasCab[0].FechaDespacho;
                dpFechaTraslado.SelectedDate = objGuiaVentasCab[0].FechaTraslado;
                dpFechaEmision.SelectedDate = objGuiaVentasCab[0].FechaEmision;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<GlosaBE> Impuesto_Obtener()
        {
            List<GlosaBE> lstGlosa;
            try
            {
                lstGlosa = JsonHelper.JsonDeserialize<List<GlosaBE>>((string)ViewState["lstGlosa"]);
                lstGlosa = lstGlosa.FindAll(x => x.Descripcion != "Neto" && x.Descripcion != "Descuento" && x.Descripcion != "SubTotal" && x.Descripcion != "Total");
                return lstGlosa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private gsGuiaVenta_ListarxOPResult GuiaVenta_ObtenerCambios()
        {
            gsGuiaVenta_ListarxOPResult objGuiaVentas;
            try
            {
                objGuiaVentas = new gsGuiaVenta_ListarxOPResult();

                objGuiaVentas.id_agenda = lblCodigoCliente.Value;
                objGuiaVentas.ID_AgendaDireccion = int.Parse(cboFacturacionCliente.SelectedValue);
                objGuiaVentas.ID_AgendaDireccion2 = int.Parse(cboDespacho.SelectedValue);

                objGuiaVentas.DireccionOrigenAgenda = lblCodigoOrigen.Value;
                objGuiaVentas.DireccionOrigenSucursal = int.Parse(cboSucursalOrigen.SelectedValue);
                objGuiaVentas.DireccionOrigenReferencia = int.Parse(cboReferenciaOrigen.SelectedValue);
                objGuiaVentas.DireccionOrigenDireccion = int.Parse(cboFacturacionOrigen.SelectedValue);

                objGuiaVentas.DireccionDestinoAgenda = lblCodigoDestino.Value;
                objGuiaVentas.DireccionDestinoSucursal = int.Parse(cboSucursalDestino.SelectedValue);
                objGuiaVentas.DireccionDestinoReferencia = int.Parse(cboReferenciaDestino.SelectedValue);
                objGuiaVentas.DireccionDestinoDireccion = int.Parse(cboFacturacionDestino.SelectedValue);


                if(lblCodigoTransportista.Value!="0" && !string.IsNullOrEmpty(lblCodigoTransportista.Value))
                {
                    objGuiaVentas.ID_Transportista = lblCodigoTransportista.Value;
                }
                else
                {
                    objGuiaVentas.ID_Transportista = null;
                }

                if (txtIDChofer.Text != "0" && !string.IsNullOrEmpty(txtIDChofer.Text) )
                {
                    objGuiaVentas.ID_Chofer = txtIDChofer.Text;
                    objGuiaVentas.TransportistaChofer = txtChofer.Text;
                    objGuiaVentas.TransportistaCertInscripcion = txtCertificado.Text;
                    objGuiaVentas.TransportistaLicencia = txtLicencia.Text;
                    objGuiaVentas.TransportistaMarca = txtVehiculoMarca.Text;
                }
                else
                {
                    objGuiaVentas.ID_Chofer = null;
                    objGuiaVentas.TransportistaChofer = null;
                    objGuiaVentas.TransportistaCertInscripcion = null;
                    objGuiaVentas.TransportistaLicencia = null;
                    objGuiaVentas.TransportistaMarca = null;
                }

                if (txtVehiculoPlaca.Text != "0" && !string.IsNullOrEmpty(txtVehiculoPlaca.Text))
                {
                    objGuiaVentas.ID_Vehiculo = txtVehiculoPlaca.Text;
                    objGuiaVentas.TransportistaModelo = txtVehiculoModelo.Text;
                    objGuiaVentas.TransportistaPlaca = txtVehiculoPlaca.Text;
                    objGuiaVentas.TransportistaRUC = txtRUCTransporte.Text;
                }
                else
                {
                    objGuiaVentas.ID_Vehiculo = null;
                    objGuiaVentas.TransportistaModelo = null;
                    objGuiaVentas.TransportistaPlaca = null;
                    objGuiaVentas.TransportistaRUC = null;
                }


                objGuiaVentas.FechaDespacho = dpFechaDespacho.SelectedDate.Value;
                objGuiaVentas.FechaTraslado = dpFechaTraslado.SelectedDate.Value;
                objGuiaVentas.FechaEmision = dpFechaEmision.SelectedDate.Value;

                return objGuiaVentas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Referencia_Cargar(string idAgenda, int idSucursal, RadComboBox cbobox)
        {
            AgendaWCFClient objAgendaWCF;
            VBG02699Result objReferencia;
            List<VBG02699Result> lstReferencias;
            try
            {
                objAgendaWCF = new AgendaWCFClient();
                objReferencia = new VBG02699Result();
                objReferencia.ID = -1;
                objReferencia.Nombre = "Ninguno";

                lstReferencias = objAgendaWCF.AgendaAnexoReferencia_ListarPorSucursal(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idSucursal, idAgenda).ToList();
                lstReferencias.Insert(0, objReferencia);

                cbobox.DataSource = lstReferencias;
                cbobox.DataTextField = "Nombre";
                cbobox.DataValueField = "ID";
                cbobox.DataBind();

                if (cbobox.Items.Count > 0)
                    cbobox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void Despacho_CargarTipo()
        //{
        //    DespachoWCFClient objDespachoWVF;
        //    try
        //    {
        //        objDespachoWVF = new DespachoWCFClient();
        //        cboOpDespacho.DataSource = objDespachoWVF.Despacho_ListarTipo(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
        //            ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario);
        //        cboOpDespacho.DataTextField = "Nombre";
        //        cboOpDespacho.DataValueField = "ID";
        //        cboOpDespacho.DataBind();

        //        if (cboOpDespacho.Items.Count > 0)
        //            cboOpDespacho.SelectedIndex = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void GuiaVentas_CargarTipo()
        //{
        //    GuiaVentasWCFClient objGuiaVentasWCF;
        //    try
        //    {
        //        objGuiaVentasWCF = new GuiaVentasWCFClient();
        //        cboOpTipoGuiaVentas.DataSource = objGuiaVentasWCF.GuiaVentas_ListarTipo(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
        //            ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario);
        //        cboOpTipoGuiaVentas.DataTextField = "Nombre";
        //        cboOpTipoGuiaVentas.DataValueField = "ID";
        //        cboOpTipoGuiaVentas.DataBind();

        //        if (cboOpTipoGuiaVentas.Items.Count > 0)
        //            cboOpTipoGuiaVentas.SelectedIndex = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void Documento_CagarTipoVenta()
        //{
        //    DocumentoWCFClient objDocumentoWCF;
        //    try
        //    {
        //        objDocumentoWCF = new DocumentoWCFClient();
        //        cboOpDocVenta.DataSource = objDocumentoWCF.Documento_ListarDocVenta(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
        //            ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario);
        //        cboOpDocVenta.DataTextField = "Nombre";
        //        cboOpDocVenta.DataValueField = "ID";
        //        cboOpDocVenta.DataBind();

        //        if (cboOpDocVenta.Items.Count > 0)
        //            cboOpDocVenta.SelectedIndex = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void Almacen_Cargar()
        //{
        //    AgendaWCFClient objAgendaWCF;
        //    try
        //    {
        //        objAgendaWCF = new AgendaWCFClient();
        //        cboAlmacen.DataSource = objAgendaWCF.AgendaAnexo_ListarAlmacen(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario);
        //        cboAlmacen.DataTextField = "AlmacenAnexo";
        //        cboAlmacen.DataValueField = "ID_AlmacenAnexo";
        //        cboAlmacen.DataBind();

        //        if (cboAlmacen.Items.Count > 0)
        //            cboAlmacen.SelectedIndex = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void Credito_Cargar(string idAgenda)
        //{
        //    CreditoWCFClient objCreditoWCF;
        //    try
        //    {
        //        objCreditoWCF = new CreditoWCFClient();

        //        var datasource = from x in objCreditoWCF.Credito_ListarCondicion(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
        //            ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda)
        //                         select new
        //                         {
        //                             ValueField = String.Format("{0},{1}", x.ID_CondicionCredito, x.DiasCredito),
        //                             TextField = String.Format("{0}", x.Nombre)
        //                         };

        //        cboTipoCredito.DataSource = datasource;
        //        cboTipoCredito.DataTextField = "TextField";
        //        cboTipoCredito.DataValueField = "ValueField";
        //        cboTipoCredito.DataBind();

        //        if (cboTipoCredito.Items.Count > 0)
        //        {
        //            cboTipoCredito.SelectedIndex = 0;
        //            txtDiasCredito.Text = cboTipoCredito.SelectedValue.Split(',')[1];
        //            dpFechaEmision.SelectedDate = DateTime.Now;
        //            dpFechaVencimiento.SelectedDate = DateTime.Now.AddDays(Int32.Parse(txtDiasCredito.Text));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void Envio_Cargar()
        //{
        //    EnvioWCFClient objEnvioWCF;
        //    try
        //    {
        //        objEnvioWCF = new EnvioWCFClient();

        //        cboTipoEnvio.DataSource = objEnvioWCF.Envio_ListarTipo(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario);
        //        cboTipoEnvio.DataTextField = "Nombre";
        //        cboTipoEnvio.DataValueField = "ID";
        //        cboTipoEnvio.DataBind();

        //        if (cboTipoEnvio.Items.Count > 0)
        //            cboTipoEnvio.SelectedValue = "2";
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void Sede_Cagar()
        //{
        //    SedeWCFClient objSedeWCF;
        //    VBG02689Result objSede;
        //    List<VBG02689Result> lstSedes;
        //    try
        //    {
        //        objSedeWCF = new SedeWCFClient();
        //        objSede = new VBG02689Result();
        //        objSede.Nombre = "Ninguno";
        //        objSede.ID_Sede = -1;

        //        lstSedes = objSedeWCF.RRHHSede_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();
        //        lstSedes.Insert(0, objSede);
        //        cboSede.DataSource = lstSedes;
        //        cboSede.DataTextField = "Nombre";
        //        cboSede.DataValueField = "ID_Sede";
        //        cboSede.DataBind();

        //        if (cboSede.Items.Count > 0)
        //            cboSede.SelectedIndex = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private void Sucursal_Cargar(string idAgenda, RadComboBox cbobox)
        {
            AgendaWCFClient objAgendaWCFC;
            List<VBG00167Result> lstSucursal;
            VBG00167Result objSucursal;
            try
            {
                objAgendaWCFC = new AgendaWCFClient();
                objSucursal = new VBG00167Result();

                lstSucursal = objAgendaWCFC.AgendaAnexo_ListarDireccionCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda).ToList();
                objSucursal.ID = -1;
                objSucursal.Nombre = "Central";
                lstSucursal.Insert(0, objSucursal);

                cbobox.DataSource = lstSucursal;
                cbobox.DataTextField = "Nombre";
                cbobox.DataValueField = "ID";
                cbobox.DataBind();

                if (cbobox.Items.Count > 0)
                    cbobox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Direccion_Cargar(string idAgenda, RadComboBox cbobox)
        {
            DireccionWCFClient objDireccionWCF;
            try
            {
                objDireccionWCF = new DireccionWCFClient();

                var dirFiscal = from x in objDireccionWCF.Direccion_ListarCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                                 ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda).ToList().FindAll(x => x.TipoDireccion == 104)
                                select new
                                {
                                    x.ID,
                                    DisplayField = String.Format("{0} {1} {2} {3}", x.Abreviatura, x.Direccion, x.Numero, x.Distrito)
                                };

                cbobox.DataSource = dirFiscal;
                cbobox.DataValueField = "ID";
                cbobox.DataTextField = "DisplayField";
                cbobox.DataBind();

                if (cbobox.Items.Count > 0)
                    cbobox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Origen_Buscar(string idAgenda)
        {
            AgendaWCFClient objAgendaWCFClient;
            VBG01134Result objAgendaCliente;
            decimal? lineaCredito = null;
            decimal? TC = null;
            DateTime? fechaVecimiento = null;
            try
            {
                objAgendaWCFClient = new AgendaWCFClient();
                objAgendaCliente = new VBG01134Result();

                objAgendaCliente = objAgendaWCFClient.Agenda_BuscarCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda, ref lineaCredito, ref fechaVecimiento, ref TC);

                txtOrigen.Text = objAgendaCliente.Nombre;
                if (!string.IsNullOrEmpty(objAgendaCliente.RUC))
                    txtRUCOrigen.Text = objAgendaCliente.RUC;
                else
                {
                    txtRUCOrigen.Text = idAgenda;
                    lblRUC.Text = "DNI";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Cliente_Buscar(string idAgenda)
        {
            AgendaWCFClient objAgendaWCFClient;
            VBG01134Result objAgendaCliente;
            decimal? lineaCredito = null;
            DateTime? fechaVecimiento = null;
            decimal? TC = null;
            try
            {
                objAgendaWCFClient = new AgendaWCFClient();
                objAgendaCliente = new VBG01134Result();

                objAgendaCliente = objAgendaWCFClient.Agenda_BuscarCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda, ref lineaCredito, ref fechaVecimiento, ref TC);

                if (!string.IsNullOrEmpty(objAgendaCliente.RUC))
                {
                    txtRUCCliente.Text = objAgendaCliente.RUC;
                    txtNombreCliente.Text = objAgendaCliente.Nombre;
                    lblCodigoCliente.Value = objAgendaCliente.RUC;
                }
                else
                {
                    txtRUCCliente.Text = idAgenda;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Transporte_Buscar(string idAgenda)
        {
            AgendaWCFClient objAgendaWCFClient;
           gsAgenda_ListarTransportistaResult[]  objAgendaCliente;

            try
            {
                objAgendaWCFClient = new AgendaWCFClient();

                objAgendaCliente = objAgendaWCFClient.Agenda_ListarTransportista(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda );

                if (!string.IsNullOrEmpty(objAgendaCliente[0].Ruc))
                    {
                    txtRUCTransporte.Text = objAgendaCliente[0].Ruc;
                    txtTransporte.Text = objAgendaCliente[0].Nombre;
                    lblCodigoTransportista.Value = idAgenda;
                    Session["Id_Transporte"] = idAgenda;
                }
                else
                {
                    txtRUCTransporte.Text = idAgenda;
                    lblCodigoTransportista.Value = idAgenda;
                    Session["Id_Transporte"] = idAgenda;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Destino_Buscar(string idAgenda)
        {
            AgendaWCFClient objAgendaWCFClient;
            VBG01134Result objAgendaCliente;
            decimal? lineaCredito = null;
            DateTime? fechaVecimiento = null;
            decimal? TC = null;
            try
            {
                objAgendaWCFClient = new AgendaWCFClient();
                objAgendaCliente = new VBG01134Result();

                objAgendaCliente = objAgendaWCFClient.Agenda_BuscarCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda, ref lineaCredito, ref fechaVecimiento, ref TC);

                txtDestino.Text = objAgendaCliente.Nombre;
                if (!string.IsNullOrEmpty(objAgendaCliente.RUC))
                    txtRUCDestino.Text = objAgendaCliente.RUC;
                else
                {
                    txtRUCDestino.Text = idAgenda;
                    lblRUC.Text = "DNI";
                }
                //txtTEA.Text = objAgendaCliente.TEA.ToString();
                //txtDiasCredito.Text = objAgendaCliente.DiasCredito.ToString();
                //cboMoneda.SelectedValue = objAgendaCliente.ID_MonedaCompra.ToString();
                ViewState["LineaCredito"] = lineaCredito;
                ViewState["FechaVencimiento"] = fechaVecimiento;
                if (lineaCredito <= 0)
                {
                    //lblLineaCredito.Text = "Linea de crédito insuficiente $." + Math.Round(Convert.ToDouble(lineaCredito.ToString()), 3).ToString();
                    //lblLineaCredito.CssClass = "mensajeError";
                }
                else
                {
                    //lblLineaCredito.Text = "Linea de crédito disponible $." + Math.Round(Convert.ToDouble(lineaCredito.ToString()), 3).ToString();
                    //lblLineaCredito.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Despacho_Cargar(string idAgenda, RadComboBox cboBox)
        {
            DireccionWCFClient objDireccionWCF;
            try
            {
                objDireccionWCF = new DireccionWCFClient();

                var datasource = from x in objDireccionWCF.Direccion_ListarCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                                 ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda)
                                 select new
                                 {
                                     x.ID,
                                     DisplayField = String.Format("{0} {1} {2} {3}", x.Abreviatura, x.Direccion, x.Numero, x.Distrito)
                                 };

                cboBox.DataSource = datasource;
                cboBox.DataValueField = "ID";
                cboBox.DataTextField = "DisplayField";
                cboBox.DataBind();

                if (cboDespacho.Items.Count > 0)
                    cboDespacho.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Chofer_Buscar(string idChofer, string idTransporte, string descripcion)
        {
            AgendaWCFClient objAgendaWCFClient;
            gsChofer_ListarResult[] objAgendaCliente;

            try
            {
                objAgendaWCFClient = new AgendaWCFClient();

                objAgendaCliente = objAgendaWCFClient.Agenda_ListarChofer(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,idTransporte, idChofer, descripcion);

                txtChofer.Text = objAgendaCliente[0].Nombre;
                if (!string.IsNullOrEmpty(objAgendaCliente[0].ID_Chofer))
                {
                    txtIDChofer.Text = objAgendaCliente[0].ID_Chofer;
                    txtChofer.Text = objAgendaCliente[0].Nombre;
                    txtLicencia.Text = objAgendaCliente[0].NroLicencia;
                }  
                else
                {
                    txtIDChofer.Text = idChofer;
                    lblRUC.Text = "DNI";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Vehiculo_Buscar(string idVehiculo, string idTransporte, string descripcion)
        {
            AgendaWCFClient objAgendaWCFClient;
            gsPlaca_ListarResult[] objAgendaCliente;

            try
            {
                objAgendaWCFClient = new AgendaWCFClient();

                objAgendaCliente = objAgendaWCFClient.Agenda_ListarPlaca(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idTransporte, idVehiculo, descripcion);

              
                if (!string.IsNullOrEmpty(objAgendaCliente[0].ID_Vehiculo))
                {
                    txtVehiculoPlaca.Text = objAgendaCliente[0].Placa;
                    txtVehiculoModelo.Text = objAgendaCliente[0].Modelo + " " + objAgendaCliente[0].Linea;
                    txtVehiculoMarca.Text = objAgendaCliente[0].Marca;
                    txtCertificado.Text = objAgendaCliente[0].CertInscripcion;
                }
                else
                {
                    txtVehiculoPlaca.Text = idVehiculo;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Métodos Web

        [WebMethod]
        public static AutoCompleteBoxData Agenda_TransporteBuscar(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarTransportistaResult[] lst = objAgendaWCFClient.Agenda_ListarTransportista(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsAgenda_ListarTransportistaResult agenda in lst)
                {
                    AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                    childNode.Text = agenda.ID_Agenda + "-" + agenda.Nombre;
                    childNode.Value = agenda.ID_Agenda;
                    result.Add(childNode);
                }
                res.Items = result.ToArray();
            }
            return res;
        }

        [WebMethod]
        public static AutoCompleteBoxData Agenda_BuscarOrigen(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarClienteResult[] lst = objAgendaWCFClient.Agenda_ListarCliente(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString, 1);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsAgenda_ListarClienteResult agenda in lst)
                {
                    AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                    childNode.Text = agenda.ID_Agenda + "-" + agenda.Nombre;
                    childNode.Value = agenda.ID_Agenda;
                    result.Add(childNode);
                }
                res.Items = result.ToArray();
            }
            return res;
        }

        [WebMethod]
        public static AutoCompleteBoxData Agenda_BuscarDestino(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarClienteResult[] lst = objAgendaWCFClient.Agenda_ListarCliente(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString, 1);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsAgenda_ListarClienteResult agenda in lst)
                {
                    AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                    childNode.Text = agenda.ID_Agenda + "-" + agenda.Nombre;
                    childNode.Value = agenda.ID_Agenda;
                    result.Add(childNode);
                }
                res.Items = result.ToArray();
            }
            return res;
        }

        [WebMethod]
        public static AutoCompleteBoxData Agenda_BuscarCliente(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarClienteResult[] lst = objAgendaWCFClient.Agenda_ListarCliente(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString, 1);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsAgenda_ListarClienteResult agenda in lst)
                {
                    AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                    childNode.Text = agenda.ID_Agenda + "-" + agenda.Nombre;
                    childNode.Value = agenda.ID_Agenda;
                    result.Add(childNode);
                }
                res.Items = result.ToArray();
            }
            return res;
        }

        [WebMethod]
        public static AutoCompleteBoxData Agenda_ChoferBuscar(object context)
        {
            string transporte;
            string chofer;

            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                transporte = HttpContext.Current.Session["Id_Transporte"].ToString();
                chofer = null;


                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsChofer_ListarResult[] lst = objAgendaWCFClient.Agenda_ListarChofer(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, transporte, chofer,  searchString);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();


                foreach (gsChofer_ListarResult agenda in lst)
                {
                    AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                    childNode.Text = agenda.ID_Chofer + "-" + agenda.Nombre;
                    childNode.Value = agenda.ID_Chofer;
                    result.Add(childNode);
                }
                res.Items = result.ToArray();
            }
            return res;
        }

        [WebMethod]
        public static AutoCompleteBoxData Agenda_VehiculoBuscar(object context)
        {
            string transporte;
            string Vehiculo;

            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                transporte = HttpContext.Current.Session["Id_Transporte"].ToString();
                Vehiculo = null;

                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsPlaca_ListarResult [] lst = objAgendaWCFClient.Agenda_ListarPlaca(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, transporte, Vehiculo, searchString);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();


                foreach (gsPlaca_ListarResult agenda in lst)
                {
                    AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                    childNode.Text = agenda.ID_Vehiculo + "/" + agenda.Marca + "/" + agenda.Modelo + "/" + agenda.Linea ;
                    childNode.Value = agenda.ID_Vehiculo;
                    result.Add(childNode);
                }
                res.Items = result.ToArray();
            }
            return res;
        }

        #endregion

        #region Métodos Protegidos
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

                    lblOp.Value = int.Parse((Request.QueryString["idOperacion"])).ToString();
                    Session["idOperacion"] = int.Parse((Request.QueryString["idOperacion"])).ToString();

                    Title = "Modificar una Guía de Venta";
                    GuiaVentas_Cargar(int.Parse((Request.QueryString["idOperacion"])));

                    lblMensaje.Text = "Listo para modificar la Guia de Venta " + (Request.QueryString["idOrdenVenta"]);

                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdItem_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            if (!ConnectionHelpers.CheckForInternetConnection())
                throw new ArgumentException("ERROR: Revisar su conexión a internet.");

            try
            {
                if (e.CommandName == "Eliminar")
                {
                    List<gsItem_BuscarResult> lstProductos = (List<gsItem_BuscarResult>)Session["lstProductos"];
                    lstProductos.Find(x => x.Item_ID.ToString() == e.CommandArgument.ToString() && x.Estado == 1).Estado = 0;

                    List<gsImpuesto_ListarPorItemResult> lstImpuestos;
                    lstImpuestos = ((List<gsImpuesto_ListarPorItemResult>)Session["lstImpuestos"]).FindAll(x => x.ID_Item == lstProductos.Find(p => p.Item_ID.ToString() == e.CommandArgument.ToString()).Item_ID);
                    foreach (gsImpuesto_ListarPorItemResult objImpuesto in lstImpuestos)
                    {
                        ((List<gsImpuesto_ListarPorItemResult>)Session["lstImpuestos"]).Remove(objImpuesto);
                    }

                    //Calcular_Glosa();

                    //grdItem.DataSource = ((List<gsItem_BuscarResult>)Session["lstProductos"]).FindAll(x => x.Estado == 1).OrderBy(x => x.Item);
                    //grdItem.DataBind();

                    lblMensaje.Text = "Se eliminó el producto del GuiaVentas con código " + e.CommandArgument.ToString() + " del GuiaVentas.";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void ramGuiaVentasMng_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if (e.Argument == "Rebind")
                {
                    //grdItem.MasterTableView.SortExpressions.Clear();
                    //grdItem.MasterTableView.GroupByExpressions.Clear();
                    //grdItem.DataSource = (List<gsItem_BuscarResult>)Session["lstProductos"];
                    //grdItem.DataBind();
                    //Calcular_Glosa();

                    lblMensaje.Text = "Se agregó el producto al GuiaVentas.";
                    lblMensaje.CssClass = "mensajeExito";

                    //acbProducto.Entries.Clear();
                    //acbProducto.Focus();
                }

                if (e.Argument.Split('(')[0].Trim() == "RebindAndNavigate")
                {
                    //grdItem.MasterTableView.SortExpressions.Clear();
                    //grdItem.MasterTableView.GroupByExpressions.Clear();
                    //grdItem.MasterTableView.CurrentPageIndex = grdItem.MasterTableView.PageCount - 1;
                    //grdItem.Rebind();
                    //grdItem.MasterTableView.SortExpressions.Clear();
                    //grdItem.MasterTableView.GroupByExpressions.Clear();
                    //grdItem.DataSource = ((List<gsItem_BuscarResult>)Session["lstProductos"]).FindAll(x => x.Estado == 1).OrderBy(x => x.Item);
                    //grdItem.DataBind();

                    //Calcular_Glosa();

                    lblMensaje.Text = "Se agregó el producto con nro. kardex " + e.Argument.Split('(')[1].Trim().Split(')')[0] + " al GuiaVentas.";
                    lblMensaje.CssClass = "mensajeExito";

                    //acbProducto.Entries.Clear();
                    //acbProducto.Focus();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            GuiaWCFClient objGuiaVentasWCF = new GuiaWCFClient();
            lblMensaje.Text = "";
            try
            {
                if (validarFecha() == 0)
                {
                    objGuiaVentasWCF.GuiaVenta_ActualizarTransporte(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                    GuiaVenta_ObtenerCambios(), Convert.ToInt32(lblOp.Value));

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind();", true);
                }



        }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected int validarFecha()
        {
            DateTime Emision = dpFechaEmision.SelectedDate.Value;
            DateTime Despacho = dpFechaDespacho.SelectedDate.Value;
            DateTime Traslado = dpFechaTraslado.SelectedDate.Value;
            int result = 0;

            int comp = DateTime.Compare(Emision, Despacho);
            if (comp > 0)
            {
                lblMensaje.Text = "ERROR: " + "Fecha Despacho debe ser mayor o igual  a Fecha Emisión. ";
                lblMensaje.CssClass = "mensajeError";
                result = 1; 
            }
            else 
            {
                int comp1 = DateTime.Compare(Despacho, Traslado);
                if (comp1 > 0)
                {
                    lblMensaje.Text = "ERROR: " + "Fecha Traslado debe ser mayor o igual a Fecha Despacho. ";
                    lblMensaje.CssClass = "mensajeError";
                    result = 1;
                }

            }
            return result;
        }

        protected void btnBuscarOrigen_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (acbOrigen.Entries.Count <= 0 || acbOrigen.Entries[0].Text.Length <= 0)
                    throw new ArgumentException("Debe seleccionar un cliente valido");

                lblCodigoOrigen.Value = acbOrigen.Text.Split('-')[0];

                Despacho_Cargar(lblCodigoOrigen.Value, cboFacturacionOrigen);
                Sucursal_Cargar(lblCodigoOrigen.Value, cboSucursalOrigen);
                Origen_Buscar(lblCodigoOrigen.Value);

                if (!string.IsNullOrEmpty(cboSucursalOrigen.SelectedValue))
                    Referencia_Cargar(lblCodigoOrigen.Value, Int32.Parse(cboSucursalOrigen.SelectedValue), cboReferenciaOrigen);

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscarDestino_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (acbDestino.Entries.Count <= 0 || acbDestino.Entries[0].Text.Length <= 0)
                    throw new ArgumentException("Debe seleccionar un cliente valido");

                lblCodigoDestino.Value = acbDestino.Text.Split('-')[0];

                Despacho_Cargar(lblCodigoDestino.Value, cboFacturacionDestino);
                Sucursal_Cargar(lblCodigoDestino.Value, cboSucursalDestino);
                Destino_Buscar(lblCodigoDestino.Value);

                if (!string.IsNullOrEmpty(cboSucursalDestino.SelectedValue))
                    Referencia_Cargar(lblCodigoDestino.Value, Int32.Parse(cboSucursalDestino.SelectedValue), cboReferenciaDestino);

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscarTransporte_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (acbTransporte.Entries.Count <= 0 || acbTransporte.Entries[0].Text.Length <= 0)
                    throw new ArgumentException("Debe seleccionar un cliente valido");

                lblCodigoTransportista.Value = acbTransporte.Text.Split('-')[0];
                Session["Id_Transporte"] = lblCodigoTransportista.Value;

                Transporte_Buscar(lblCodigoTransportista.Value);
                
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscarChofer_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if (acbChofer.Entries.Count <= 0 || acbChofer.Entries[0].Text.Length <= 0)
                    throw new ArgumentException("Debe seleccionar un cliente valido");

                lblCodigoChofer.Value = acbChofer.Text.Split('-')[0];
                Session["Id_Chofer"] = lblCodigoChofer.Value;

                Chofer_Buscar( lblCodigoChofer.Value, lblCodigoTransportista.Value, null);

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscarVehiculo_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if ( acbVehiculo.Entries.Count <= 0 || acbVehiculo.Entries[0].Text.Length <= 0)
                    throw new ArgumentException("Debe seleccionar un cliente valido");

                lblCodigoVehiculo.Value = acbVehiculo.Text.Split('/')[0];
                Session["Id_Vehiculo"] = lblCodigoVehiculo.Value;

                Vehiculo_Buscar(lblCodigoVehiculo.Value, lblCodigoTransportista.Value, null);

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }


        #endregion

    }
}