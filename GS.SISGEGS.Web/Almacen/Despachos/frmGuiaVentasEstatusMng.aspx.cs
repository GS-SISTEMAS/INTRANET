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

namespace GS.SISGEGS.Web.Almacen.Despachos
{
    public partial class frmGuiaVentasEstatusMng : System.Web.UI.Page
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

        private void GuiaVentasEstatus_Cargar(int idOperacion)
        {
            GuiaWCFClient objGuiaVentasEstatusWCF = new GuiaWCFClient();
            gsGuiaVenta_ListarxOPResult[] objGuiaVentasEstatusCab;

            try
            {
                Session["Id_Transporte"] = null;

                objGuiaVentasEstatusCab = objGuiaVentasEstatusWCF.GuiaVenta_ListarxOP(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idOperacion);

                lblCodigoCliente.Value = objGuiaVentasEstatusCab[0].id_agenda;
                lblCodigoOrigen.Value = objGuiaVentasEstatusCab[0].DireccionOrigenAgenda;
                lblCodigoDestino.Value = objGuiaVentasEstatusCab[0].DireccionDestinoAgenda;
                lblCodigoTransportista.Value = objGuiaVentasEstatusCab[0].ID_Transportista;

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

                Transporte_Buscar(lblCodigoTransportista.Value);

                txtIDChofer.Text = objGuiaVentasEstatusCab[0].ID_Chofer.ToString();
                txtChofer.Text =  objGuiaVentasEstatusCab[0].TransportistaChofer.ToString();
                txtLicencia.Text = objGuiaVentasEstatusCab[0].TransportistaLicencia.ToString();

                txtVehiculoPlaca.Text = objGuiaVentasEstatusCab[0].ID_Vehiculo.ToString();
                txtVehiculoMarca.Text = objGuiaVentasEstatusCab[0].TransportistaMarca.ToString();
                txtVehiculoModelo.Text = objGuiaVentasEstatusCab[0].TransportistaModelo.ToString();
                txtCertificado.Text = objGuiaVentasEstatusCab[0].TransportistaCertInscripcion.ToString();

                txtTransaccion.Text = objGuiaVentasEstatusCab[0].Transaccion.ToString();

                lblUsuE.Value = objGuiaVentasEstatusCab[0].usuarioEmision.ToString();
                lblUsuS.Value = objGuiaVentasEstatusCab[0].usuarioSeguridad.ToString();
                lblUsuC.Value = objGuiaVentasEstatusCab[0].usuarioAgencia.ToString();

                cboFacturacionCliente.SelectedValue = objGuiaVentasEstatusCab[0].ID_AgendaDireccion.ToString();
                cboDespacho.SelectedValue = objGuiaVentasEstatusCab[0].ID_AgendaDireccion2.ToString();

                cboFacturacionOrigen.SelectedValue = objGuiaVentasEstatusCab[0].DireccionOrigenDireccion.ToString();
                cboSucursalOrigen.SelectedValue = objGuiaVentasEstatusCab[0].DireccionOrigenSucursal.ToString();
                cboReferenciaOrigen.SelectedValue = objGuiaVentasEstatusCab[0].DireccionOrigenReferencia.ToString();

                cboFacturacionDestino.SelectedValue = objGuiaVentasEstatusCab[0].DireccionDestinoDireccion.ToString();
                cboSucursalDestino.SelectedValue = objGuiaVentasEstatusCab[0].DireccionDestinoSucursal.ToString();
                cboReferenciaDestino.SelectedValue = objGuiaVentasEstatusCab[0].DireccionDestinoReferencia.ToString();

                //------------------------------------
                dpFechaEmision.SelectedDate = objGuiaVentasEstatusCab[0].FechaEmision;
                cboHoraE.SelectedValue = objGuiaVentasEstatusCab[0].FechaEmision.Hour.ToString();
                cboMinE.SelectedValue = objGuiaVentasEstatusCab[0].FechaEmision.Minute.ToString();
                cboSegE.SelectedValue = objGuiaVentasEstatusCab[0].FechaEmision.Second.ToString();

                cboTransporte.SelectedValue = objGuiaVentasEstatusCab[0].id_placa.ToString();

                if (objGuiaVentasEstatusCab[0].fechaSeguridad != null )
                {
                    dpFechaSeguridad.SelectedDate = objGuiaVentasEstatusCab[0].fechaSeguridad;

                    cboHoraS.SelectedValue = objGuiaVentasEstatusCab[0].fechaSeguridad.Value.Hour.ToString();
                    cboMinS.SelectedValue = objGuiaVentasEstatusCab[0].fechaSeguridad.Value.Minute.ToString();
                    cboSegS.SelectedValue = objGuiaVentasEstatusCab[0].fechaSeguridad.Value.Second.ToString();
                }
                if (objGuiaVentasEstatusCab[0].fechaAgencia != null)
                {
                    dpFechaCliente.SelectedDate = objGuiaVentasEstatusCab[0].fechaAgencia;
                    cboHoraC.SelectedValue = objGuiaVentasEstatusCab[0].fechaAgencia.Value.Hour.ToString();
                    cboMinC.SelectedValue = objGuiaVentasEstatusCab[0].fechaAgencia.Value.Minute.ToString();
                    cboSegC.SelectedValue = objGuiaVentasEstatusCab[0].fechaAgencia.Value.Second.ToString();
                }



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
            gsGuiaVenta_ListarxOPResult objGuiaVentasEstatus;
            try
            {
                objGuiaVentasEstatus = new gsGuiaVenta_ListarxOPResult();

                objGuiaVentasEstatus.id_agenda = lblCodigoCliente.Value;
                objGuiaVentasEstatus.ID_AgendaDireccion = int.Parse(cboFacturacionCliente.SelectedValue);
                objGuiaVentasEstatus.ID_AgendaDireccion2 = int.Parse(cboDespacho.SelectedValue);

                objGuiaVentasEstatus.DireccionOrigenAgenda = lblCodigoOrigen.Value;
                objGuiaVentasEstatus.DireccionOrigenSucursal = int.Parse(cboSucursalOrigen.SelectedValue);
                objGuiaVentasEstatus.DireccionOrigenReferencia = int.Parse(cboReferenciaOrigen.SelectedValue);
                objGuiaVentasEstatus.DireccionOrigenDireccion = int.Parse(cboFacturacionOrigen.SelectedValue);

                objGuiaVentasEstatus.DireccionDestinoAgenda = lblCodigoDestino.Value;
                objGuiaVentasEstatus.DireccionDestinoSucursal = int.Parse(cboSucursalDestino.SelectedValue);
                objGuiaVentasEstatus.DireccionDestinoReferencia = int.Parse(cboReferenciaDestino.SelectedValue);
                objGuiaVentasEstatus.DireccionDestinoDireccion = int.Parse(cboFacturacionDestino.SelectedValue);

                objGuiaVentasEstatus.ID_Transportista = lblCodigoTransportista.Value;

                objGuiaVentasEstatus.ID_Chofer = txtIDChofer.Text;
                objGuiaVentasEstatus.TransportistaChofer = txtChofer.Text;
                objGuiaVentasEstatus.TransportistaCertInscripcion = txtCertificado.Text;
                objGuiaVentasEstatus.TransportistaLicencia = txtLicencia.Text;
                objGuiaVentasEstatus.TransportistaMarca = txtVehiculoMarca.Text;

                objGuiaVentasEstatus.ID_Vehiculo  = txtVehiculoPlaca.Text;
                objGuiaVentasEstatus.TransportistaModelo = txtVehiculoModelo.Text;
                objGuiaVentasEstatus.TransportistaPlaca = txtVehiculoPlaca.Text;
                objGuiaVentasEstatus.TransportistaRUC = txtRUCTransporte.Text;

                //objGuiaVentasEstatus.FechaDespacho = dpFechaDespacho.SelectedDate.Value;
                //objGuiaVentasEstatus.FechaTraslado = dpFechaTraslado.SelectedDate.Value;
                objGuiaVentasEstatus.FechaEmision = dpFechaEmision.SelectedDate.Value;

                return objGuiaVentasEstatus;
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
            decimal? TC = null;
            DateTime? fechaVecimiento = null;
            try
            {
                objAgendaWCFClient = new AgendaWCFClient();
                objAgendaCliente = new VBG01134Result();

                objAgendaCliente = objAgendaWCFClient.Agenda_BuscarCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda, ref lineaCredito, ref fechaVecimiento, ref TC);

                if (!string.IsNullOrEmpty(objAgendaCliente.ID_Agenda))
                {
                    txtRUCCliente.Text = objAgendaCliente.RUC;
                    txtNombreCliente.Text = objAgendaCliente.Nombre;
                    lblCodigoCliente.Value = objAgendaCliente.ID_Agenda;
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

                    Title = "Modificar una Guía de Venta Fechas: ";

                    CargarCombosHoras(cboHoraE, cboMinE, cboSegE);
                    CargarCombosHoras(cboHoraS, cboMinS, cboSegS);
                    CargarCombosHoras(cboHoraC, cboMinC, cboSegC);
                    Transporte_Cargar();
                    GuiaVentasEstatus_Cargar(int.Parse((Request.QueryString["idOperacion"])));

                    ActivarBotones(false);

                    lblMensaje.Text = "Listo para modificar la Guia de Venta Fechas " + (Request.QueryString["idOrdenVenta"]);
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

                    lblMensaje.Text = "Se eliminó el producto del GuiaVentasEstatus con código " + e.CommandArgument.ToString() + " del GuiaVentasEstatus.";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void ramGuiaVentasEstatusMng_AjaxRequest(object sender, AjaxRequestEventArgs e)
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

                    lblMensaje.Text = "Se agregó el producto al GuiaVentasEstatus.";
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

                    lblMensaje.Text = "Se agregó el producto con nro. kardex " + e.Argument.Split('(')[1].Trim().Split(')')[0] + " al GuiaVentasEstatus.";
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
            int usuE;
            int usuS;
            int usuC;
            int usu;
            int estado;
            int vehiculo;
            DateTime fechaE = new DateTime();
            DateTime fechaS = new DateTime();
            DateTime fechaC = new DateTime();

            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            GuiaWCFClient objGuiaVentasEstatusWCF = new GuiaWCFClient();
            lblMensaje.Text = "";

            usu = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario; 

            if (lblUsuE.Value == null || lblUsuE.Value == "")
            { usuE = usu;   }
            else
            { usuE = Convert.ToInt32(lblUsuE.Value);}

            if (lblUsuS.Value == null || lblUsuS.Value == "")
            {usuS = usu;}
            else
            {  usuS = Convert.ToInt32(lblUsuS.Value); }

            if (lblUsuC.Value == null || lblUsuC.Value == "")
            { usuC = usu; }
            else
            { usuC = Convert.ToInt32(lblUsuC.Value); }

            fechaE = dpFechaEmision.SelectedDate.Value;

            if (dpFechaSeguridad.SelectedDate != null && dpFechaSeguridad.SelectedDate.GetValueOrDefault() != DateTime.MinValue)
            {
                fechaS = new DateTime(dpFechaSeguridad.SelectedDate.Value.Year, dpFechaSeguridad.SelectedDate.Value.Month, dpFechaSeguridad.SelectedDate.Value.Day, Convert.ToInt32(cboHoraS.SelectedValue), Convert.ToInt32(cboMinS.SelectedValue), Convert.ToInt32(cboSegS.SelectedValue));
            }

            if (dpFechaCliente.SelectedDate != null && dpFechaCliente.SelectedDate.GetValueOrDefault() != DateTime.MinValue)
            {
                fechaC = new DateTime(dpFechaCliente.SelectedDate.Value.Year, dpFechaCliente.SelectedDate.Value.Month, dpFechaCliente.SelectedDate.Value.Day, Convert.ToInt32(cboHoraC.SelectedValue), Convert.ToInt32(cboMinC.SelectedValue), Convert.ToInt32(cboSegC.SelectedValue));
            }

            if (cboTransporte == null || cboTransporte.SelectedValue == "" || cboTransporte.SelectedIndex == 0)
            {
                vehiculo = Convert.ToInt32(null);
            }
            else { vehiculo = Convert.ToInt32(cboTransporte.SelectedValue.ToString()); }


            try
            {
                estado = validarFecha(fechaS, fechaC, fechaE);

                if (estado == 1 || estado == 2)
                {
                    objGuiaVentasEstatusWCF.GuiaVenta_Modificar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, usu,
                    Convert.ToInt32(lblOp.Value), txtTransaccion.Text, lblCodigoCliente.Value, usuE, fechaE, usuS, fechaS, usuC, fechaC, estado, vehiculo);

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind();", true);
                }

        }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected int validarFecha( DateTime FSeguridad, DateTime FCliente, DateTime FEmision)
        {

            int result = 0;

            if (cboTransporte == null || cboTransporte.SelectedValue == "" || cboTransporte.SelectedIndex == 0)
            {
                lblMensaje.Text = "ERROR: " + "Se debe asignar placa para realizar el despacho. ";
                lblMensaje.CssClass = "mensajeError";
                result = 100;
            }
            else
            {
                if (dpFechaSeguridad.SelectedDate != null && dpFechaSeguridad.SelectedDate.GetValueOrDefault() != DateTime.MinValue)
                {
                    int comp = DateTime.Compare(FEmision, FSeguridad);
                    if (comp > 0)
                    {
                        lblMensaje.Text = "ERROR: " + "Fecha Seguridad debe ser mayor o igual  a Fecha Emisión. ";
                        lblMensaje.CssClass = "mensajeError";
                        result = 100;
                    }
                    else
                    {
                        result = 1;
                        if (dpFechaCliente.SelectedDate != null && dpFechaCliente.SelectedDate.GetValueOrDefault() != DateTime.MinValue)
                        {
                            int comp1 = DateTime.Compare(FSeguridad, FCliente);
                            if (comp1 > 0)
                            {
                                lblMensaje.Text = "ERROR: " + "Fecha Cliente debe ser mayor o igual a Fecha Seguridad. ";
                                lblMensaje.CssClass = "mensajeError";
                                result = 100;
                            }
                            else
                            { result = 2; }
                        }
                    }
                }
            }

            return result;
        }

        //protected void btnBuscarOrigen_Click(object sender, EventArgs e)
        //{
        //    if (Session["Usuario"] == null)
        //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

        //    try
        //    {
        //        if (acbOrigen.Entries.Count <= 0 || acbOrigen.Entries[0].Text.Length <= 0)
        //            throw new ArgumentException("Debe seleccionar un cliente valido");

        //        lblCodigoOrigen.Value = acbOrigen.Text.Split('-')[0];

        //        Despacho_Cargar(lblCodigoOrigen.Value, cboFacturacionOrigen);
        //        Sucursal_Cargar(lblCodigoOrigen.Value, cboSucursalOrigen);
        //        Origen_Buscar(lblCodigoOrigen.Value);

        //        if (!string.IsNullOrEmpty(cboSucursalOrigen.SelectedValue))
        //            Referencia_Cargar(lblCodigoOrigen.Value, Int32.Parse(cboSucursalOrigen.SelectedValue), cboReferenciaOrigen);

        //    }
        //    catch (Exception ex)
        //    {
        //        lblMensaje.Text = "ERROR: " + ex.Message;
        //        lblMensaje.CssClass = "mensajeError";
        //    }
        //}

        //protected void btnBuscarDestino_Click(object sender, EventArgs e)
        //{
        //    if (Session["Usuario"] == null)
        //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

        //    try
        //    {
        //        if (acbDestino.Entries.Count <= 0 || acbDestino.Entries[0].Text.Length <= 0)
        //            throw new ArgumentException("Debe seleccionar un cliente valido");

        //        lblCodigoDestino.Value = acbDestino.Text.Split('-')[0];

        //        Despacho_Cargar(lblCodigoDestino.Value, cboFacturacionDestino);
        //        Sucursal_Cargar(lblCodigoDestino.Value, cboSucursalDestino);
        //        Destino_Buscar(lblCodigoDestino.Value);

        //        if (!string.IsNullOrEmpty(cboSucursalDestino.SelectedValue))
        //            Referencia_Cargar(lblCodigoDestino.Value, Int32.Parse(cboSucursalDestino.SelectedValue), cboReferenciaDestino);

        //    }
        //    catch (Exception ex)
        //    {
        //        lblMensaje.Text = "ERROR: " + ex.Message;
        //        lblMensaje.CssClass = "mensajeError";
        //    }
        //}

        //protected void btnBuscarTransporte_Click(object sender, EventArgs e)
        //{
        //    if (Session["Usuario"] == null)
        //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

        //    try
        //    {
        //        if (acbTransporte.Entries.Count <= 0 || acbTransporte.Entries[0].Text.Length <= 0)
        //            throw new ArgumentException("Debe seleccionar un cliente valido");

        //        lblCodigoTransportista.Value = acbTransporte.Text.Split('-')[0];
        //        Session["Id_Transporte"] = lblCodigoTransportista.Value;

        //        Transporte_Buscar(lblCodigoTransportista.Value);
                
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMensaje.Text = "ERROR: " + ex.Message;
        //        lblMensaje.CssClass = "mensajeError";
        //    }
        //}

        //protected void btnBuscarVehiculo_Click(object sender, EventArgs e)
        //{
        //    if (Session["Usuario"] == null)
        //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
        //    try
        //    {
        //        if ( acbVehiculo.Entries.Count <= 0 || acbVehiculo.Entries[0].Text.Length <= 0)
        //            throw new ArgumentException("Debe seleccionar un cliente valido");

        //        lblCodigoVehiculo.Value = acbVehiculo.Text.Split('/')[0];
        //        Session["Id_Vehiculo"] = lblCodigoVehiculo.Value;

        //        Vehiculo_Buscar(lblCodigoVehiculo.Value, lblCodigoTransportista.Value, null);

        //    }
        //    catch (Exception ex)
        //    {
        //        lblMensaje.Text = "ERROR: " + ex.Message;
        //        lblMensaje.CssClass = "mensajeError";
        //    }
        //}

        #endregion

        private void CargarCombosHoras(RadComboBox cboHora, RadComboBox cboMinutos, RadComboBox cboSegundo)
        {
            for (var i = 0; i < 24; i++)
            {
                cboHora.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
            }

            for (var i = 0; i < 60; i++)
            {
                cboMinutos.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
            }
            for (var i = 0; i < 60; i++)
            {
                cboSegundo.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
            }
        }

        private void ActivarBotones(bool activar)
        {
            dpFechaSeguridad.Enabled = activar;
            dpFechaCliente.Enabled = activar;

            cboHoraS.Enabled = activar;
            cboHoraC.Enabled = activar;
            cboMinS.Enabled = activar;
            cboMinC.Enabled = activar;
            cboSegS.Enabled = activar;
            cboSegC.Enabled = activar;
            cboTransporte.Enabled = activar;
            btnGuardar.Enabled = activar;

            if(activar == false)
            {  btnEditar.Enabled = true; }
            else
            { btnEditar.Enabled = false; }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            ActivarBotones(true);

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            ActivarBotones(false);

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

        private void Transporte_Cargar()
        {
            try
            {
                AgendaWCFClient objAgendaWCF = new AgendaWCFClient();
                gsPlaca_DespachoResult objPlaca = new gsPlaca_DespachoResult();
                List<gsPlaca_DespachoResult> lstPlaca;

                lstPlaca = objAgendaWCF.Agenda_ListarPlaca_Despacho(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, null, null, null, "DESPACHO").ToList();
                lstPlaca.Insert(0, objPlaca);
                objPlaca.Placa = "Seleccionar";
                objPlaca.ID = 0;

        
                var lstPlacas = from x in lstPlaca
                                select new
                                {
                                    x.ID,
                                    DisplayField = String.Format("{0}", x.Placa)
                                    //DisplayField = String.Format("{0} {1} {2} {3}", x.Placa, x.Modelo, x.Marca, x.Agenda)
                                };

                cboTransporte.DataSource = lstPlacas;
                cboTransporte.DataTextField = "DisplayField";
                cboTransporte.DataValueField = "ID";
                cboTransporte.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}