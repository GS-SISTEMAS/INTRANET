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
using GS.SISGEGS.Web.PedidoWCF;
using GS.SISGEGS.Web.LoginWCF;
using GS.Helpdesk.entities.Commons;
using System.Web.Script.Serialization;

namespace GS.SISGEGS.Web.Comercial.Pedido
{
    public partial class frmOrdenVentaMng : System.Web.UI.Page
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

                cboAlmacen.SelectedValue = cboAlmacen.Items.FindItem(x => x.Text.ToUpper() == objVariante.parametro1.ToUpper()).Value;

                cboOpDocVenta.SelectedValue = cboOpDocVenta.Items.FindItem(x => x.Text.ToUpper() == objVariante.parametro2.ToUpper()).Value;
                cboFormaPago.SelectedValue = cboFormaPago.Items.FindItem(x => x.Text.ToUpper() == objVariante.parametro3.ToUpper()).Value;
                cboSede.SelectedValue = cboSede.Items.FindItem(x => x.Text.ToUpper() == objVariante.parametro4.ToUpper()).Value;
                lblTrans.Text = objVariante.parametro5;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Pedido_Cargar(int idPedido)
        {
            OrdenVentaWCFClient objOrdenVentaWCF = new OrdenVentaWCFClient(); ;
            gsOV_BuscarCabeceraResult objOrdenVentaCab;
            List<GlosaBE> lstGlosa = new List<GlosaBE>();
            ImpuestoWCFClient objImpuestoWCF = new ImpuestoWCFClient();
            gsOV_BuscarImpuestoResult[] lstImpuestos = null;
            List<gsImpuesto_ListarPorItemResult> lstImpuestoItem = new List<gsImpuesto_ListarPorItemResult>();
            gsOV_BuscarDetalleResult[] objOrdenVentaDet = null;
            List<gsItem_BuscarResult> lstProductos = new List<gsItem_BuscarResult>();
            bool? bloqueado = false;
            string mensajeBloqueo = null;
            try
            {
                objOrdenVentaCab = objOrdenVentaWCF.OrdenVenta_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idPedido,
                    ref objOrdenVentaDet, ref lstImpuestos, ref bloqueado, ref mensajeBloqueo);

                lblCodigoCliente.Value = objOrdenVentaCab.ID_Agenda;
                Cliente_Buscar(objOrdenVentaCab.ID_Agenda);
                Direccion_Cargar(objOrdenVentaCab.ID_Agenda);
                Sucursal_Cargar(objOrdenVentaCab.ID_Agenda);

                //AGF


                List<object> parametros = new List<object>();
                parametros.Add(((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString());
                parametros.Add(lblCodigoCliente.Value);
                string responseFromServer = GSbase.POSTResult("ListarMasterTen", 27, parametros);
                MasterTenCollection collection = new JavaScriptSerializer().Deserialize<MasterTenCollection>(responseFromServer);

                if (collection.Rows.Count > 0)
                    foreach (var row in collection.Rows)
                    {
                        switch (row.v02)
                        {
                            case "Pedido con Factura":
                                rbtFacturas.Visible = true;
                                break;
                            case "Pedido con Letra":
                                rbtLetras.Visible = true;
                                break;
                        }
                    }
                else
                {
                    rbtFacturas.Visible = true;
                    rbtLetras.Visible = true;
                }


                List<gsCredito_ListarCondicionResult> lista = new CreditoWCFClient().Credito_ListarCondicion(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, objOrdenVentaCab.ID_Agenda).ToList();
                cboFormaPago.SelectedValue = objOrdenVentaCab.Id_Pago.ToString();
                Credito_Cargar(objOrdenVentaCab.ID_Agenda, objOrdenVentaCab.Id_Pago);

                List <gsCredito_ListarCondicionResult> resultCondicionCredito = lista.Where(be => be.ID_CondicionCredito == objOrdenVentaCab.ID_CondicionCredito).ToList();
                cboTipoCredito.Visible = true;
                if (resultCondicionCredito[0].Nombre.Contains("L"))
                {

                    lblTexto.Visible = false;
                    btnLetras.Visible = true;
                    lblLetras.Visible = true;
                    rbtFacturas.Visible = true;
                    rbtLetras.Visible = true;
                    rbtLetras.Checked = true;
                    CheckLetra();

                    Session["objLetras"] = resultCondicionCredito[0].Nombre;
                    lblLetras.Text = resultCondicionCredito[0].Nombre;

                }
                else
                {
                    lblTexto.Visible = false;
                    btnLetras.Visible = false;
                    lblLetras.Visible = false;
                    rbtFacturas.Visible = true;
                    rbtLetras.Visible = true;
                    rbtFacturas.Checked = true;
                    cboTipoCredito.SelectedValue = String.Format("{0},{1}", resultCondicionCredito[0].ID_CondicionCredito.ToString(), resultCondicionCredito[0].DiasCredito) ;
                    CheckFActura();

                }

                //

                //Cliente_Buscar(objOrdenVentaCab.ID_Agenda);
                Cargar_Glosa();

                //if (!string.IsNullOrEmpty(cboSucursal.SelectedValue))
                //    Referencia_Cargar(lblCodigoCliente.Value, int.Parse(cboSucursal.SelectedValue));

                AutoCompleteBoxEntry objEntry = new AutoCompleteBoxEntry();
                objEntry.Text = objOrdenVentaCab.ID_Vendedor.ToString() + "-" + objOrdenVentaCab.Vendedor;
                acbVendedor.Entries.Add(objEntry);
                objEntry = new AutoCompleteBoxEntry();
                objEntry.Text = objOrdenVentaCab.ID_Agenda.ToString() + "-" + txtCliente.Text;
                acbCliente.Entries.Add(objEntry);
                acbCliente.Enabled = false;
                btnBuscarCliente.Enabled = false;
                cboSucursal.SelectedValue = objOrdenVentaCab.ID_AgendaAnexo.ToString();
                cboFacturacion.SelectedValue = objOrdenVentaCab.ID_AgendaDireccion.ToString();
                cboDespacho.SelectedValue = objOrdenVentaCab.ID_AgendaDireccion2.ToString();
                cboTipoEnvio.SelectedValue = objOrdenVentaCab.ID_Envio.ToString();
                cboPrioridad.SelectedValue = (objOrdenVentaCab.Prioridad + 1).ToString();
                cboMoneda.SelectedValue = objOrdenVentaCab.ID_Moneda.ToString();
                cboFormaPago.SelectedValue = objOrdenVentaCab.Id_Pago.ToString();
                acbVendedor.Enabled = false;
                txtOrden.Text = objOrdenVentaCab.NroOrdenCliente;
                dpFechaEmision.SelectedDate = objOrdenVentaCab.FechaEmision;
                dpFechaVencimiento.SelectedDate = objOrdenVentaCab.FechaVencimiento;
                txtDiasCredito.Value = ((DateTime)objOrdenVentaCab.FechaVencimiento - (DateTime)objOrdenVentaCab.FechaEmision).TotalDays;

                //try
                //{
                //    cboTipoCredito.SelectedValue = cboTipoCredito.FindItem(x => x.Value.Split(',')[0] == objOrdenVentaCab.ID_CondicionCredito.ToString()).Value;
                //}
                //catch (Exception ex)
                //{
                //    string mensaje = "No cuenta con tipos de crédito, revisar “Tipo de Venta” o  comunicarse con créditos y cobranzas.";
                //    rwmPedidoMng.RadAlert(mensaje, 400, null, "Mensaje de error", null);
                //}

                txtObservacion.Text = objOrdenVentaCab.Observaciones;
                if (string.IsNullOrEmpty(objOrdenVentaCab.ID_Sede.ToString().Trim()))
                    cboSede.SelectedIndex = 0;
                else
                    cboSede.SelectedValue = objOrdenVentaCab.ID_Sede.ToString();
                //cboReferencia.SelectedValue = objOrdenVentaCab.ID_AgendaAnexoReferencia.ToString();
                cboOpDespacho.SelectedValue = objOrdenVentaCab.ID_TipoDespacho.ToString();
                cboOpTipoPedido.SelectedValue = objOrdenVentaCab.ID_TipoPedido.ToString();
                cboOpDocVenta.SelectedValue = objOrdenVentaCab.ID_DocumentoVenta.ToString();
                cboAlmacen.SelectedValue = objOrdenVentaCab.ID_Almacen.ToString();
                Session["idAlmacen"] = objOrdenVentaCab.ID_Almacen;
                cboAlmacen.Enabled = false;
                txtNroRegistro.Text = objOrdenVentaCab.NoRegistro;

                if (!string.IsNullOrEmpty(objOrdenVentaCab.ID_AgendaDestino))
                {
                    objEntry = new AutoCompleteBoxEntry();
                    objEntry.Text = objOrdenVentaCab.ID_AgendaDestino.ToString() + "-" + objOrdenVentaCab.Transporte;
                    acbTransporte.Entries.Add(objEntry);
                }

                //if (!string.IsNullOrEmpty(objOrdenVentaCab.ID_ReferenciaAgenda))
                //{
                //    objEntry = new AutoCompleteBoxEntry();
                //    objEntry.Text = objOrdenVentaCab.ID_ReferenciaAgenda.ToString() + "-" + objOrdenVentaCab.Contacto;
                //    acbContacto.Entries.Add(objEntry);
                //}

                lstGlosa = JsonHelper.JsonDeserialize<List<GlosaBE>>((string)ViewState["lstGlosa"]);
                lstGlosa.Find(x => x.Descripcion == "Neto").Importe = objOrdenVentaCab.Neto;
                lstGlosa.Find(x => x.Descripcion == "Descuento").Importe = objOrdenVentaCab.Dcto;
                lstGlosa.Find(x => x.Descripcion == "SubTotal").Importe = objOrdenVentaCab.SubTotal;
                lstGlosa.Find(x => x.Descripcion == "Total").Importe = objOrdenVentaCab.Total;

                foreach (gsOV_BuscarImpuestoResult objImpuesto in lstImpuestos)
                {
                    GlosaBE objGlosaBE = new GlosaBE();
                    objGlosaBE.IdGlosa = objImpuesto.ID_Impuesto;
                    objGlosaBE.Descripcion = objImpuesto.Abreviacion;
                    objGlosaBE.Importe = objImpuesto.Importe;
                    objGlosaBE.BaseImponible = objImpuesto.BaseImponible;
                    lstGlosa.Add(objGlosaBE);
                }
                grdGlosa.DataSource = lstGlosa.OrderBy(x => x.IdGlosa);
                grdGlosa.DataBind();
                ViewState["lstGlosa"] = JsonHelper.JsonSerializer(lstGlosa);
                Session["Impuestos"] = "";

                ViewState["fecha"] = objOrdenVentaCab.FechaOrden;

                foreach (gsOV_BuscarDetalleResult objDetalle in objOrdenVentaDet)
                {
                    gsItem_BuscarResult objItem = new gsItem_BuscarResult();
                    //gsOV_BuscarDetalleResult obj = new gsOV_BuscarDetalleResult();
                    //obj = objDetalle; 

                    objItem.Item = objDetalle.Item;

                    objItem.Codigo = objDetalle.ID_Item;
                    objItem.Cantidad = Convert.ToInt32(objDetalle.Cantidad);
                    objItem.DctoMax = objDetalle.DctoMax;
                    objItem.Descuento = objDetalle.Dcto;
                    objItem.ID_Moneda = objDetalle.ID_Moneda;
                    objItem.ID_UnidadControl = objDetalle.ID_UnidadDoc;
                    objItem.ID_UnidadInv = objDetalle.ID_UnidadInv;
                    objItem.Importe = objDetalle.Importe;
                    objItem.Item = objDetalle.Item;
                    objItem.Item_ID = objDetalle.Item_ID;
                    objItem.NombreMoneda = objDetalle.NombreMoneda;
                    objItem.Observacion = objDetalle.Observaciones;
                    objItem.Precio = objDetalle.Precio;
                    objItem.PrecioInicial = objDetalle.PrecioMinimo;
                    objItem.Signo = objDetalle.Signo;
                    if (objOrdenVentaCab.Aprobacion1)
                        objItem.Stock = objDetalle.Stock + objDetalle.Cantidad;
                    else
                        objItem.Stock = objDetalle.Stock;
                    objItem.FactorUnidadInv = objDetalle.FactorUnidadInv;
                    objItem.UnidadPresentacion = objDetalle.UnidadPresentacion;
                    objItem.ID_Amarre = objDetalle.ID_Amarre;
                    objItem.Estado = 1;
                    objItem.CostoUnitario = objDetalle.CostoUnitario;

                    objItem.Peso2 = objDetalle.Peso2;
                    objItem.Peso_Kg2 = objDetalle.Peso_Kg2;
                    objItem.FactorConversion2 = objDetalle.FactorConversion2;


                    lstImpuestoItem.AddRange(objImpuestoWCF.Impuesto_ListarPorItem(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                        ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, objDetalle.ID_Item, DateTime.Now));
                  


                    if(objDetalle.UsaImpuestoPrincipal > 0)
                    {
                        Session["Impuestos"] = "IGV"; 
                    }
                    else
                    {
                        Session["Impuestos"] = "SINIGV";
                    }

                    objItem.TieneImpuesto = objDetalle.UsaImpuestoPrincipal; 

                    lstProductos.Add(objItem);
                }

                Session["lstProductos"] = lstProductos;
                Session["lstImpuestos"] = lstImpuestoItem;
                grdItem.DataSource = lstProductos;
                grdItem.DataBind();

                lblRentabilidad.Text = "Rentabilidad: " + Math.Round((double)((lstProductos.Sum(x => x.Precio) - 
                    lstProductos.Sum(x => x.CostoUnitario)) * 100) / (double)lstProductos.Sum(x => x.Precio), 0).ToString() + "%";

                //Se borra
                //PedidosFechas_Buscar(idPedido, 0);
                /*
                try
                {
                    cboTipoCredito.SelectedValue = cboTipoCredito.FindItem(x => x.Value.Split(',')[0] == objOrdenVentaCab.ID_CondicionCredito.ToString()).Value;
                }
                catch (Exception ex)
                {
                    if (lblLetras.Text.Length == 0)
                    {
                        string mensaje = "No cuenta con tipos de crédito, revisar “Tipo de Venta” o  comunicarse con créditos y cobranzas.";
                        rwmPedidoMng.RadAlert(mensaje, 400, null, "Mensaje de error", null);
                    }
                }
                */
                if ((bool)bloqueado)
                {
                    btnLetras.Enabled = false; 
                    btnGuardar.Enabled = false;
                    throw new ArgumentException(mensajeBloqueo);
                }

                if (((Usuario_LoginResult)Session["Usuario"]).aprobarTransfGratuita && objOrdenVentaCab.Id_Pago == 2)
                    btnAprobar.Visible = true;
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

        private List<gsOV_BuscarDetalleResult> OrdenVenta_ObtenerDetalle()
        {
            List<gsOV_BuscarDetalleResult> lstPedidoDet;
            List<gsItem_BuscarResult> lstProductos = (List<gsItem_BuscarResult>)Session["lstProductos"];
            gsOV_BuscarDetalleResult objProducto;
            try
            {
                lstPedidoDet = new List<gsOV_BuscarDetalleResult>();
                foreach (gsItem_BuscarResult producto in lstProductos)
                {
                    objProducto = new gsOV_BuscarDetalleResult();
                    objProducto.Item = producto.Item;
                    objProducto.ID_Amarre = producto.ID_Amarre;
                    objProducto.TablaOrigen = "OV";
                    objProducto.ID_Item = producto.Codigo;
                    objProducto.ID_ItemPedido = null;
                    objProducto.Item_ID = producto.Item_ID;
                    objProducto.Cantidad = producto.Cantidad;
                    objProducto.Precio = producto.Precio;
                    objProducto.Dcto = producto.Descuento;
                    objProducto.DctoValor = Math.Round(((producto.Descuento/ 100))* producto.Precio , 4);
                    objProducto.Importe = producto.Importe;
                    objProducto.ID_ItemAnexo = null;
                    objProducto.ID_CCosto = null;
                    objProducto.ID_UnidadGestion = null;
                    objProducto.ID_UnidadProyecto = null;
                    objProducto.ID_UnidadInv = producto.ID_UnidadInv;
                    objProducto.FactorUnidadInv = producto.FactorUnidadInv;
                    objProducto.CantidadUnidadInv = producto.Cantidad; // Consultar como se calcula realmente
                    objProducto.ID_UnidadDoc = producto.ID_UnidadControl;
                    objProducto.CantidadUnidadDoc = producto.Cantidad; // Consultar como se calcula realmente
                    objProducto.Observaciones = producto.Observacion;
                    objProducto.Estado = (int)producto.Estado;
                    objProducto.Stock = producto.Stock;

                    objProducto.Peso_Kg2 = producto.Peso_Kg2;
                    objProducto.Peso2 = producto.Peso2;
                    objProducto.FactorConversion2 = producto.FactorConversion2;

                    lstPedidoDet.Add(objProducto);
                }
                return lstPedidoDet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private gsOV_BuscarCabeceraResult OrdenVenta_ObtenerCabecera()
        {
            gsOV_BuscarCabeceraResult objOrdenVentaCab;
            decimal impuesto = 0;
            try
            {
                objOrdenVentaCab = new gsOV_BuscarCabeceraResult();
                objOrdenVentaCab.ID_Agenda = lblCodigoCliente.Value;
                if (!string.IsNullOrEmpty(txtNroRegistro.Text))
                    objOrdenVentaCab.NoRegistro = txtNroRegistro.Text;
                else
                    objOrdenVentaCab.NoRegistro = "0"; ;

                if ((Request.QueryString["idOrdenVenta"]) == "0")
                {
                    objOrdenVentaCab.FechaDespacho = DateTime.Now.Date;
                    objOrdenVentaCab.FechaEntrega = DateTime.Now.Date;
                    objOrdenVentaCab.FechaVigencia = DateTime.Now.Date;
                    objOrdenVentaCab.FechaEmision = DateTime.Now.Date;
                }
                else
                {
                    objOrdenVentaCab.FechaDespacho = (DateTime)ViewState["fecha"];
                    objOrdenVentaCab.FechaEntrega = (DateTime)ViewState["fecha"];
                    objOrdenVentaCab.FechaVigencia = (DateTime)ViewState["fecha"];
                    objOrdenVentaCab.FechaEmision = (DateTime)ViewState["fecha"];
                }

                objOrdenVentaCab.FechaOrden = dpFechaEmision.SelectedDate.Value;
                objOrdenVentaCab.FechaVencimiento = dpFechaVencimiento.SelectedDate.Value;
                objOrdenVentaCab.ID_Envio = int.Parse(cboTipoEnvio.SelectedValue);
                objOrdenVentaCab.ID_AgendaAnexoReferencia = null;
                //if (cboReferencia.SelectedValue != "-1")
                //    objOrdenVentaCab.ID_AgendaAnexoReferencia = int.Parse(cboReferencia.SelectedValue);
                //else
                //    objOrdenVentaCab.ID_AgendaAnexoReferencia = null;
                objOrdenVentaCab.ID_Vendedor = acbVendedor.Text.Split('-')[0].Trim();
                objOrdenVentaCab.ID_Moneda = int.Parse(cboMoneda.SelectedValue);
                List<GlosaBE> lstGlosa = JsonHelper.JsonDeserialize<List<GlosaBE>>((string)ViewState["lstGlosa"]);
                objOrdenVentaCab.Neto = lstGlosa.Find(x => x.Descripcion == "Neto").Importe;
                objOrdenVentaCab.Dcto = lstGlosa.Find(x => x.Descripcion == "Descuento").Importe;
                objOrdenVentaCab.SubTotal = lstGlosa.Find(x => x.Descripcion == "SubTotal").Importe;
                objOrdenVentaCab.Total = lstGlosa.Find(x => x.Descripcion == "Total").Importe;
                lstGlosa = (JsonHelper.JsonDeserialize<List<GlosaBE>>((string)ViewState["lstGlosa"])).FindAll(x => x.Descripcion != "Neto" && x.Descripcion != "Descuento" && x.Descripcion != "SubTotal" && x.Descripcion != "Total");
                foreach (GlosaBE objGlosaBE in lstGlosa)
                {
                    impuesto = impuesto + objGlosaBE.Importe;
                }
                objOrdenVentaCab.Impuestos = Math.Round(impuesto, 4);
                objOrdenVentaCab.Observaciones = txtObservacion.Text;
                objOrdenVentaCab.Prioridad = cboPrioridad.SelectedIndex;
                objOrdenVentaCab.EntregaParcial = false; //Flag para poder hacer entregas parcialmente
                objOrdenVentaCab.Estado = 376; //Cuenta Corriente en StandBy
                objOrdenVentaCab.Id_Pago = int.Parse(cboFormaPago.SelectedValue);
                if (cboSucursal.SelectedValue != "-1")
                    objOrdenVentaCab.ID_AgendaAnexo = int.Parse(cboSucursal.SelectedValue);
                else
                    objOrdenVentaCab.ID_AgendaAnexo = null;
                objOrdenVentaCab.TEA = decimal.Parse(txtTEA.Text);
                objOrdenVentaCab.ID_AgendaDireccion = int.Parse(cboFacturacion.SelectedValue);
                objOrdenVentaCab.ID_AgendaDireccion2 = int.Parse(cboDespacho.SelectedValue);
                objOrdenVentaCab.ModoPago = null; //No estoy seguro
                objOrdenVentaCab.NotasDespacho = null;
                objOrdenVentaCab.ID_CondicionCredito = int.Parse(cboTipoCredito.SelectedValue.Split(',')[0]);
                if (string.IsNullOrEmpty(txtOrden.Text))
                    objOrdenVentaCab.NroOrdenCliente = null;
                else

                    objOrdenVentaCab.NroOrdenCliente = txtOrden.Text;
                objOrdenVentaCab.ID_NaturalezaGastoIngreso = null;
                objOrdenVentaCab.ID_AgendaOrigen = null;
                objOrdenVentaCab.DireccionOrigenSucursal = null;
                objOrdenVentaCab.DireccionOrigenReferencia = null;
                objOrdenVentaCab.DireccionOrigenDireccion = null;
                objOrdenVentaCab.ID_AgendaDestino = null;
                objOrdenVentaCab.DireccionDestinoSucursal = null;
                objOrdenVentaCab.DireccionDestinoReferencia = null;
                objOrdenVentaCab.DireccionDestinoDireccion = null;
                objOrdenVentaCab.ID_TipoDespacho = int.Parse(cboOpDespacho.SelectedValue);
                objOrdenVentaCab.ID_TipoPedido = int.Parse(cboOpTipoPedido.SelectedValue);
                objOrdenVentaCab.ID_DocumentoVenta = int.Parse(cboOpDocVenta.SelectedValue);
                objOrdenVentaCab.ID_Almacen = int.Parse(cboAlmacen.SelectedValue);
                objOrdenVentaCab.ID_Transportista = null;
                objOrdenVentaCab.ID_Chofer = null;
                objOrdenVentaCab.ID_Vehiculo1 = null;
                objOrdenVentaCab.ID_Vehiculo2 = null;
                objOrdenVentaCab.ID_Vehiculo3 = null;
                objOrdenVentaCab.HoraAtencionOpcion1_Desde = 0;
                objOrdenVentaCab.HoraAtencionOpcion1_Hasta = 0;
                objOrdenVentaCab.HoraAtencionOpcion2_Desde = 0;
                objOrdenVentaCab.HoraAtencionOpcion2_Hasta = 0;
                objOrdenVentaCab.HoraAtencionOpcion3_Desde = 0;
                objOrdenVentaCab.HoraAtencionOpcion3_Hasta = 0;
                if (cboSede.SelectedValue != "-1")
                    objOrdenVentaCab.ID_Sede = int.Parse(cboSede.SelectedValue);
                else
                    objOrdenVentaCab.ID_Sede = null;

                objOrdenVentaCab.Contacto = null;
                //if (acbContacto.Entries.Count <= 0)
                //    objOrdenVentaCab.Contacto = null;
                //else
                //    objOrdenVentaCab.Contacto = acbContacto.Entries[0].Text.Split('-')[0];

                if (acbTransporte.Entries.Count <= 0)
                {
                    objOrdenVentaCab.ID_Transportista = null;
                    objOrdenVentaCab.ID_AgendaDestino = null;
                }
                else
                {
                    objOrdenVentaCab.ID_Transportista = lblTrans.Text;
                    objOrdenVentaCab.ID_AgendaDestino = acbTransporte.Entries[0].Text.Split('-')[0];
                }

                return objOrdenVentaCab;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<gsPedidos_FechasLetrasSelectResult> OrdenVenta_FechasLetras()
        {
            List<gsPedidos_FechasLetrasSelectResult> lstFechas = new List<gsPedidos_FechasLetrasSelectResult>();
            try
            {
                lstFechas = ((List<gsPedidos_FechasLetrasSelectResult>)Session["lstFechas"]);
                return lstFechas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Calcular_Glosa()
        {
            decimal neto = 0;
            decimal descuento = 0;
            decimal impuestos = 0;
            decimal subtotal = 0;
            try
            {
                List<gsImpuesto_ListarPorItemResult> lstImpuestos = ((List<gsImpuesto_ListarPorItemResult>)Session["lstImpuestos"]);
                List<GlosaBE> lstGlosa = JsonHelper.JsonDeserialize<List<GlosaBE>>((string)ViewState["lstGlosa"]);
                List<gsItem_BuscarResult> lstProductos = ((List<gsItem_BuscarResult>)Session["lstProductos"]).FindAll(x => x.Estado == 1);

                foreach (gsItem_BuscarResult objProducto in lstProductos)
                {
                    subtotal = subtotal + objProducto.Importe;
                    descuento = descuento + (((objProducto.Descuento / 100) * (objProducto.Cantidad)) * objProducto.Precio);
                }

                neto = subtotal  + descuento;

                lstGlosa.Find(x => x.Descripcion == "Neto").Importe = Math.Round(neto, 4);
                lstGlosa.Find(x => x.Descripcion == "Descuento").Importe = Math.Round(descuento, 4);
                lstGlosa.Find(x => x.Descripcion == "SubTotal").Importe = Math.Round(subtotal, 4);

                List<GlosaBE> lstGlosaImpuestos = lstGlosa.FindAll(x => x.Descripcion != "Neto" && x.Descripcion != "Descuento" && x.Descripcion != "SubTotal" && x.Descripcion != "Total").ToList();
                foreach (GlosaBE objGlosaImpuestos in lstGlosaImpuestos)
                {
                    lstGlosa.Remove(objGlosaImpuestos);
                }
                foreach (gsImpuesto_ListarPorItemResult objImpuesto in lstImpuestos)
                {
                    GlosaBE objGlosaBE = new GlosaBE();
                    gsItem_BuscarResult objProducto = lstProductos.Find(x => x.Item_ID == objImpuesto.ID_Item && x.Estado == 1);
                    if (lstGlosa.FindAll(x => x.IdGlosa == objImpuesto.ID_Impuesto).Count <= 0)
                    {
                        objGlosaBE.IdGlosa = objImpuesto.ID_Impuesto;
                        objGlosaBE.Descripcion = objImpuesto.Abreviacion;
                        if (objImpuesto.Valor > 0)
                            objGlosaBE.BaseImponible = objProducto.Importe;
                        else
                            objGlosaBE.BaseImponible = 0;
                        objGlosaBE.Importe = Math.Round(((decimal)objImpuesto.Valor / 100) * (objProducto.Importe), 4);
                     
                    }
                    else
                    {
                        objGlosaBE = lstGlosa.Find(x => x.IdGlosa == objImpuesto.ID_Impuesto);
                        lstGlosa.Remove(objGlosaBE);
                        if (objImpuesto.Valor > 0)
                            objGlosaBE.BaseImponible = objGlosaBE.BaseImponible + objProducto.Importe;
                        objGlosaBE.Importe = Math.Round(objGlosaBE.Importe + ((decimal)objImpuesto.Valor / 100) * (objProducto.Importe), 4);
                    }
                    lstGlosa.Add(objGlosaBE);
                }

                lstGlosaImpuestos.Clear();
                lstGlosaImpuestos = lstGlosa.FindAll(x => x.Descripcion != "Neto" && x.Descripcion != "Descuento" && x.Descripcion != "SubTotal" && x.Descripcion != "Total").ToList();
                foreach (GlosaBE objGlosaImpuestos in lstGlosaImpuestos)
                {
                    impuestos = impuestos + objGlosaImpuestos.Importe;
                }
                lstGlosa.Find(x => x.Descripcion == "Total").Importe = Math.Round(subtotal + impuestos, 4);

                lstGlosa = lstGlosa.OrderBy(x => x.IdGlosa).ToList();

                grdGlosa.DataSource = lstGlosa;
                grdGlosa.DataBind();

                ViewState["lstGlosa"] = JsonHelper.JsonSerializer<List<GlosaBE>>(lstGlosa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Cargar_Glosa()
        {
            List<GlosaBE> lstGlosa;
            try
            {
                lstGlosa = new List<GlosaBE>();
                for (int i = 0; i < 4; i++)
                {
                    GlosaBE objGlosaBE = new GlosaBE();
                    objGlosaBE.IdGlosa = (i + 1) * (-1);
                    objGlosaBE.BaseImponible = 0;
                    objGlosaBE.Importe = 0;
                    lstGlosa.Add(objGlosaBE);
                }
                lstGlosa[0].Descripcion = "SubTotal";
                lstGlosa[1].Descripcion = "Descuento";
                lstGlosa[2].Descripcion = "Neto";
                lstGlosa[3].Descripcion = "Total";
                lstGlosa[3].IdGlosa = 9999;

                lstGlosa = lstGlosa.OrderBy(x => x.IdGlosa).ToList();
                grdGlosa.DataSource = lstGlosa;
                grdGlosa.DataBind();
                ViewState["lstGlosa"] = JsonHelper.JsonSerializer<List<GlosaBE>>(lstGlosa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Item_Buscar(string idItem, string idCliente)
        {
            List<gsItem_BuscarResult> lstProductos;
            try
            {
                if (Session["lstProductos"] != null)
                {
                    lstProductos = (List<gsItem_BuscarResult>)Session["lstProductos"];
                    if (lstProductos.FindAll(x => x.Codigo == idItem && x.Estado == 1).Count > 0)
                    {
                        throw new ArgumentException("El producto ya ha sido seleccionado.");
                    }
                    else  
                    {
                        int count = 0; 
                        foreach(gsItem_BuscarResult item in lstProductos.FindAll( x => x.Estado ==1 ))
                        {
                            if( item.TieneImpuesto > 0)
                            {
                                count = 1; 
                            }
                            else
                            {
                                count = 2; 
                            }
                        }

                        if(count == 0)
                        {
                            Session["Impuestos"] = "";
                        }
                        else if( count == 1)
                        {
                            Session["Impuestos"] = "IGV";
                        }
                        else if (count == 2)
                        {
                            Session["Impuestos"] = "SINIGV";
                        }

                    }
                        
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FormaPago_Cargar()
        {
            FormaPagoWCFClient objFormaPagoWCF;
            try
            {
                objFormaPagoWCF = new FormaPagoWCFClient();
                cboFormaPago.DataSource = objFormaPagoWCF.FormaPago_Listar(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario);
                cboFormaPago.DataTextField = "Nombre";
                cboFormaPago.DataValueField = "ID";
                cboFormaPago.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Moneda_Cargar()
        {
            MonedaWCFClient objMonedaWCF;
            try
            {
                objMonedaWCF = new MonedaWCFClient();
                cboMoneda.DataSource = objMonedaWCF.Moneda_Listar(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario);
                cboMoneda.DataTextField = "Nombre";
                cboMoneda.DataValueField = "ID";
                cboMoneda.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Despacho_CargarTipo()
        {
            DespachoWCFClient objDespachoWVF;
            try
            {
                objDespachoWVF = new DespachoWCFClient();
                cboOpDespacho.DataSource = objDespachoWVF.Despacho_ListarTipo(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario);
                cboOpDespacho.DataTextField = "Nombre";
                cboOpDespacho.DataValueField = "ID";
                cboOpDespacho.DataBind();

                if (cboOpDespacho.Items.Count > 0)
                    cboOpDespacho.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Pedido_CargarTipo()
        {
            PedidoWCFClient objPedidoWCF;
            try
            {
                objPedidoWCF = new PedidoWCFClient();
                cboOpTipoPedido.DataSource = objPedidoWCF.Pedido_ListarTipo(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario);
                cboOpTipoPedido.DataTextField = "Nombre";
                cboOpTipoPedido.DataValueField = "ID";
                cboOpTipoPedido.DataBind();

                if (cboOpTipoPedido.Items.Count > 0)
                    cboOpTipoPedido.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Documento_CagarTipoVenta()
        {
            DocumentoWCFClient objDocumentoWCF;
            try
            {
                objDocumentoWCF = new DocumentoWCFClient();
                cboOpDocVenta.DataSource = objDocumentoWCF.Documento_ListarDocVenta(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList().FindAll(x => x.Nombre.Contains("lectronic") );
                cboOpDocVenta.DataTextField = "Nombre";
                cboOpDocVenta.DataValueField = "ID";
                cboOpDocVenta.DataBind();

                if (cboOpDocVenta.Items.Count > 0)
                {
                    cboOpDocVenta.SelectedIndex = 0;
                }
                   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Almacen_Cargar()
        {
            AgendaWCFClient objAgendaWCF;
            try
            {
                objAgendaWCF = new AgendaWCFClient();
                cboAlmacen.DataSource = objAgendaWCF.AgendaAnexo_ListarAlmacen(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario);
                cboAlmacen.DataTextField = "AlmacenAnexo";
                cboAlmacen.DataValueField = "ID_AlmacenAnexo";
                cboAlmacen.DataBind();

                if (cboAlmacen.Items.Count > 0)
                    cboAlmacen.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Credito_Cargar(string idAgenda, int ID_Pago)
        {
            CreditoWCFClient objCreditoWCF;
            try
            {
                objCreditoWCF = new CreditoWCFClient();

                List<gsCredito_ListarCondicionResult> lst = new List<gsCredito_ListarCondicionResult>();

                List<gsCredito_ListarCondicionResult> lista = objCreditoWCF.Credito_ListarCondicion(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda).ToList();

                /*
                lst = lista.ToList().FindAll(x => x.ID_Pago == ID_Pago && !x.Nombre.Contains("Obse"));
                //lst = lst.ToList().FindAll(x => x.ID_Pago == ID_Pago && !x.Nombre.Contains("L"));
                */
                var datasource = from x in lista.Where(be => be.ID_Pago == -1)
                                 select new
                                 {
                                     ValueField = String.Format("{0},{1}", x.ID_CondicionCredito, x.DiasCredito),
                                     TextField = String.Format("{0}", x.Nombre)
                                 };

                switch (ID_Pago)
                {
                    case 0:
                        datasource = from x in lista.Where(be => be.ID_Pago == 0).ToList().OrderBy(be => be.Nombre)
                                     select new
                                     {
                                         ValueField = String.Format("{0},{1}", x.ID_CondicionCredito, x.DiasCredito),
                                         TextField = String.Format("{0}", x.Nombre)
                                     };
                        break;
                    case 1:
                        datasource = from x in lista.Where(be => be.Nombre.Trim().ToUpper().Substring(0, 1).Equals("F")).ToList()
                        select new
                        {
                            ValueField = String.Format("{0},{1}", x.ID_CondicionCredito, x.DiasCredito),
                            TextField = String.Format("{0}", x.Nombre)
                        };
                        break;
                    case 2:
                        datasource = from x in lista.Where(be => be.ID_Pago == 2).ToList().OrderBy(be => be.Nombre)
                                     select new
                                     {
                                         //ValueField = String.Format("{0},{1}", x.ID_CondicionCredito, x.DiasCredito),
                                         ValueField = String.Format("{0},{1}", x.ID_CondicionCredito, "0"),
                                         TextField = String.Format("{0}", x.Nombre)
                                     };
                        break;
                }
                cboTipoCredito.DataSource = datasource;
                cboTipoCredito.DataTextField = "TextField";
                cboTipoCredito.DataValueField = "ValueField";
                cboTipoCredito.DataBind();

                if (cboTipoCredito.Items.Count > 0)
                {
                    cboTipoCredito.SelectedIndex = 0;
                    txtDiasCredito.Text = cboTipoCredito.SelectedValue.Split(',')[1];
                    dpFechaEmision.SelectedDate = DateTime.Now;
                    dpFechaVencimiento.SelectedDate = DateTime.Now.AddDays(Int32.Parse(txtDiasCredito.Text));

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Envio_Cargar()
        {
            EnvioWCFClient objEnvioWCF;
            try
            {
                objEnvioWCF = new EnvioWCFClient();

                cboTipoEnvio.DataSource = objEnvioWCF.Envio_ListarTipo(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario);
                cboTipoEnvio.DataTextField = "Nombre";
                cboTipoEnvio.DataValueField = "ID";
                cboTipoEnvio.DataBind();

                if (cboTipoEnvio.Items.Count > 0)
                    cboTipoEnvio.SelectedValue = "2";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Sede_Cagar()
        {
            SedeWCFClient objSedeWCF;
            VBG02689Result objSede;
            List<VBG02689Result> lstSedes;
            try
            {
                objSedeWCF = new SedeWCFClient();
                objSede = new VBG02689Result();
                objSede.Nombre = "Ninguno";
                objSede.ID_Sede = -1;

                lstSedes = objSedeWCF.RRHHSede_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();
                lstSedes.Insert(0, objSede);
                cboSede.DataSource = lstSedes;
                cboSede.DataTextField = "Nombre";
                cboSede.DataValueField = "ID_Sede";
                cboSede.DataBind();

                if (cboSede.Items.Count > 0)
                    cboSede.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Sucursal_Cargar(string idAgenda)
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

                cboSucursal.DataSource = lstSucursal;
                cboSucursal.DataTextField = "Nombre";
                cboSucursal.DataValueField = "ID";
                cboSucursal.DataBind();

                if (cboSucursal.Items.Count > 0)
                    cboSucursal.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Direccion_Cargar(string idAgenda)
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

                var dirFiscal = from x in objDireccionWCF.Direccion_ListarCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                                 ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda).ToList().FindAll(x => x.TipoDireccion == 104)
                                select new
                                {
                                    x.ID,
                                    DisplayField = String.Format("{0} {1} {2} {3}", x.Abreviatura, x.Direccion, x.Numero, x.Distrito)
                                };

                cboFacturacion.DataSource = dirFiscal;
                cboFacturacion.DataValueField = "ID";
                cboFacturacion.DataTextField = "DisplayField";
                cboFacturacion.DataBind();


                cboDespacho.DataSource = datasource;
                cboDespacho.DataValueField = "ID";
                cboDespacho.DataTextField = "DisplayField";
                cboDespacho.DataBind();

                if (cboFacturacion.Items.Count > 0)
                    cboFacturacion.SelectedIndex = 0;

                string ValorCero;
                ValorCero = "SELECCIONAR";
                cboDespacho.Items.Insert(0, ValorCero);
                cboDespacho.Items.FindItemByText(ValorCero).Value = "0";

                //if (cboDespacho.Items.Count > 0)
                //    cboDespacho.SelectedIndex = 0;
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

                if(TC<=0)
                {
                    btnGuardar.Enabled = false;
                    rwmPedidoMng.RadAlert("El cliente no tiene asignado linea de credito, comuniquese con su sectorista", 400, null, "Mensaje de error", null);
                    return;
                }
                
                txtCliente.Text = objAgendaCliente.Nombre;
                if (!string.IsNullOrEmpty(objAgendaCliente.RUC))
                    txtRUC.Text = objAgendaCliente.RUC;
                else
                {
                    txtRUC.Text = idAgenda;
                    txtRUC.Label = "RUC";
                }

                txtTEA.Text = objAgendaCliente.TEA.ToString();
                txtDiasCredito.Text = objAgendaCliente.DiasCredito.ToString();
                cboMoneda.SelectedValue = objAgendaCliente.ID_MonedaCompra.ToString();
                ViewState["LineaCredito"] = lineaCredito;
                ViewState["FechaVencimiento"] = fechaVecimiento;
                Session["TC"] = TC;
                ViewState["TipoCliente"] = objAgendaCliente.TipoCliente;


                if(string.IsNullOrEmpty(objAgendaCliente.DiasCredito.ToString()))
                {
                    ViewState["DiasBase"] = 0;
                }
                else
                {
                    ViewState["DiasBase"] = objAgendaCliente.DiasCredito;
                }

              


                if (lineaCredito <= 0)
                {
                    lblLineaCredito.Text = "Linea de crédito insuficiente $." + Math.Round(Convert.ToDouble(lineaCredito.ToString()), 3).ToString();
                    lblLineaCredito.CssClass = "mensajeError";
                }
                else
                {
                    lblLineaCredito.Text = "Linea de crédito disponible $." + Math.Round(Convert.ToDouble(lineaCredito.ToString()), 3).ToString();
                    lblLineaCredito.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void Cliente_ValidarCorreo(string idAgenda)
        {
            AgendaWCFClient objAgendaWCFClient;
            VBG01134_validarCorreoResult objValidarCorreo;
            bool? existeCliente = null;
            bool? existeCorreo = null;
            try
            {
                objAgendaWCFClient = new AgendaWCFClient();
                objValidarCorreo = new VBG01134_validarCorreoResult();

                objValidarCorreo = objAgendaWCFClient.Agenda_ValidarCorreo(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,  
                                                                           ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda, ref existeCliente, ref existeCorreo);

                if (!string.IsNullOrEmpty(objValidarCorreo.ID_Agenda))
                {

                    if (existeCorreo == true)
                    {
                        txtCorreo.Text = objValidarCorreo.Email.ToString();

                        Direccion_Cargar(lblCodigoCliente.Value);
                        Sucursal_Cargar(lblCodigoCliente.Value);
                        Credito_Cargar(lblCodigoCliente.Value, int.Parse(cboFormaPago.SelectedValue));
                        Cliente_Buscar(lblCodigoCliente.Value);
                        acbCliente.Enabled = false;
                        cboAlmacen.Enabled = false;
                        btnBuscarCliente.Enabled = false;


                        //-----------------------
                        rbtFacturas.Checked = false;
                        rbtLetras.Checked = false;
                        cboTipoCredito.Visible = false;
                        btnLetras.Visible = false;
                        lblLetras.Visible = false;


                        //-----------------------

                        acbVendedor.Focus();
                        Session["idAlmacen"] = cboAlmacen.SelectedValue;

                        List<object> parametros = new List<object>();
                        parametros.Add(((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString());
                        parametros.Add(lblCodigoCliente.Value);
                        string responseFromServer = GSbase.POSTResult("ListarMasterTen", 27, parametros);
                        MasterTenCollection collection = new JavaScriptSerializer().Deserialize<MasterTenCollection>(responseFromServer);

                        if (collection.Rows.Count > 0)
                            foreach (var row in collection.Rows)
                            {
                                switch (row.v02)
                                {
                                    case "Pedido con Factura":
                                        rbtFacturas.Visible = true;
                                        //cboTipoCredito.Visible = true;
                                        //rbtLetras.Visible = false;
                                        ////lblTexto.Visible = true;
                                        //btnLetras.Visible = false;
                                        break;
                                    case "Pedido con Letra":
                                        //btnLetras.Visible = true;
                                        rbtLetras.Visible = true;
                                        //cboTipoCredito.Visible = false;
                                        //rbtFacturas.Visible = false;
                                        ////lblTexto.Visible = false;
                                        break;
                                }
                            }
                        else {
                            rbtFacturas.Visible = true;
                            rbtLetras.Visible = true;
                        }
                            

                        lblMensaje.Text = "Los datos del cliente fueron cargados.";
                        lblMensaje.CssClass = "mensajeExito";

                       
                    }
                    else
                    {
                        char OldChar = Convert.ToChar("&");
                        char NewChar = Convert.ToChar("Y");

                        objValidarCorreo.Nombre = objValidarCorreo.Nombre.Replace(OldChar, NewChar);
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowInsertFormCorreo('" + JsonHelper.JsonSerializer(objValidarCorreo) + "');", true);

                    }

                }
                else
                {
                    if (string.IsNullOrEmpty(lblCodigoCliente.Value))
                        throw new ArgumentException("ERROR: Debe buscar y seleccionar un cliente antes de ingresar un producto.");
                    else
                        throw new ArgumentException("ERROR: Debe buscar el nombre del producto antes de agregarlo.");
                }
            }
            catch (Exception ex)
            {
                rwmPedidoMng.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }
        #endregion

        public void VisibleFormaPago()
        {
            List<object> parametros = new List<object>();
            parametros.Add(((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString());
            parametros.Add(lblCodigoCliente.Value);
            string responseFromServer = GSbase.POSTResult("ListarMasterTen", 27, parametros);
            MasterTenCollection collection = new JavaScriptSerializer().Deserialize<MasterTenCollection>(responseFromServer);
            rbtFacturas.Visible = false;
            rbtLetras.Visible = false;
            if (collection.Rows.Count > 0)
                foreach (var row in collection.Rows)
                {
                    switch (row.v02)
                    {
                        case "Pedido con Factura":
                            rbtFacturas.Enabled = true;
                            rbtFacturas.Visible = true;
                            break;
                        case "Pedido con Letra":
                            rbtFacturas.Enabled = true;
                            if (cboFormaPago.SelectedValue == "1")
                                rbtLetras.Visible = true;
                            else
                                rbtLetras.Visible = false;
                            break;
                    }
                }
            else
            {
                rbtFacturas.Visible = true;
                rbtLetras.Visible = true;
            }
        }

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
        public static AutoCompleteBoxData Agenda_BuscarContacto(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarContactoResult[] lst = objAgendaWCFClient.Agenda_ListarContacto(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString, 1);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsAgenda_ListarContactoResult agenda in lst)
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
        public static AutoCompleteBoxData Item_BuscarProducto(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (!string.IsNullOrEmpty(searchString) && searchString.Length >= 4)
            {
                string idalmacen;

                idalmacen = HttpContext.Current.Session["idAlmacen"].ToString(); 

                ItemWCFClient objItemWCF = new ItemWCFClient();
                List<gsItem_ListarProductoResult> lst = objItemWCF.Item_ListarProducto(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString).ToList().FindAll(x => x.idAlmacen == int.Parse(HttpContext.Current.Session["idAlmacen"].ToString()));


                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsItem_ListarProductoResult producto in lst)
                {
                    AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                    childNode.Text = producto.ID_Item + "-" + producto.Nombre;
                    childNode.Value = producto.ID_Item;
            
                    result.Add(childNode);
                }
                res.Items = result.ToArray();
            }
            return res;
        }

        [WebMethod]
        public static AutoCompleteBoxData Agenda_BuscarVendedor(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarVendedorResult[] lst = objAgendaWCFClient.Agenda_ListarVendedor(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsAgenda_ListarVendedorResult agenda in lst)
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

                    hdValues.Value =
                        string.Format("{0}|{1}",
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idEmpresa);

                    dpFechaEmision.SelectedDate = DateTime.Now;
                    cboTipoCredito.Visible = false;
                    lblTexto.Visible = false;
                    btnLetras.Visible = false;
                    lblLetras.Visible = false;
                    //No visible a todo
                    cboTipoCredito.Visible = false;
                    rbtFacturas.Visible = false;


                    ViewState["DiasCredito"] = 0;

                    Session["idAlmacen"] = null;
                    if (Session["lstProductos"] != null)
                    {
                        Session["lstProductos"] = null;
                        Session["lstImpuestos"] = null;
                    }

                    Cargar_Glosa();
                    Sede_Cagar();
                    Envio_Cargar();
                    Almacen_Cargar();
                    Documento_CagarTipoVenta();
                    Pedido_CargarTipo();
                    Despacho_CargarTipo();
                    Moneda_Cargar();
                    FormaPago_Cargar();
                    Session["Impuestos"] = "";
                    Session["DiasCredito"] = 0;

                    List<gsPedidos_FechasLetrasSelectResult> lstFechas = new List<gsPedidos_FechasLetrasSelectResult>();
                    Session["lstFechas"] = lstFechas; 

                    int idOrdenVenta = int.Parse((Request.QueryString["idOrdenVenta"]));

                    if ((Request.QueryString["idOrdenVenta"]) == "0")
                    {
                        Title = "Registrar un pedido";
                        lblMensaje.Text = "Listo para registrar un pedido.";
                      
                        dpFechaEmision.SelectedDate = DateTime.Now;
                        dpFechaVencimiento.SelectedDate = DateTime.Now.AddDays(180);
                        Variante_Cargar();
                        PedidosFechas_Buscar(idOrdenVenta, 0);
                        cboTipoCredito.Visible = false;
                    }
                    else
                    {
                        Title = "Modificar un pedido";
                       
                        Pedido_Cargar(int.Parse((Request.QueryString["idOrdenVenta"])));
                        lblMensaje.Text = "Listo para modificar el pedido " + (Request.QueryString["idOrdenVenta"]);
                       
                    }
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                rwmPedidoMng.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

        protected void cboTipoCredito_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            if (!ConnectionHelpers.CheckForInternetConnection())
                throw new ArgumentException("ERROR: Revisar su conexión a internet.");

            try
            {
                DateTime FechaInicio = new DateTime(); 

                txtDiasCredito.Text = cboTipoCredito.SelectedValue.Split(',')[1];

                FechaInicio = dpFechaEmision.SelectedDate.Value; 
                dpFechaVencimiento.SelectedDate = FechaInicio.AddDays(Int32.Parse(txtDiasCredito.Text));

                List<gsPedidos_FechasLetrasSelectResult> lstFechas = new List<gsPedidos_FechasLetrasSelectResult>();
                lblLetras.Text = "";

                Session["lstFechasNew"] = lstFechas;
                Session["DiasCredito"] = 0;
                Session["objLetras"] = "";
                Session["lstFechas"] = Session["lstFechasNew"];

            }
            catch (Exception ex)
            {
                rwmPedidoMng.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
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

                    int count = 0;
                    foreach (gsItem_BuscarResult item in lstProductos.FindAll(x => x.Estado == 1).ToList())
                    {
                        if (item.TieneImpuesto > 0)
                        {
                            count = 1;
                        }
                        else
                        {
                            count = 2;
                        }
                    }

                    if (count == 0)
                    {
                        Session["Impuestos"] = "";
                    }
                    else if (count == 1)
                    {
                        Session["Impuestos"] = "IGV";
                    }
                    else if (count == 2)
                    {
                        Session["Impuestos"] = "SINIGV";
                    }

                    Calcular_Glosa();

                    grdItem.DataSource = ((List<gsItem_BuscarResult>)Session["lstProductos"]).FindAll(x => x.Estado == 1);
                    grdItem.DataBind();

                    lblMensaje.Text = "Se eliminó el producto del pedido con código " + e.CommandArgument.ToString() + " del pedido.";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                rwmPedidoMng.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

        protected void ramPedidoMng_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if (e.Argument == "Rebind")
                {

                    grdItem.MasterTableView.SortExpressions.Clear();
                    grdItem.MasterTableView.GroupByExpressions.Clear();
                    grdItem.DataSource = (List<gsItem_BuscarResult>)Session["lstProductos"];
                    grdItem.DataBind();
                    Calcular_Glosa();

                    lblMensaje.Text = "Se agregó el producto al pedido.";
                    lblMensaje.CssClass = "mensajeExito";

                    acbProducto.Entries.Clear();
                    acbProducto.Focus();
                }

                if (e.Argument.Split('(')[0].Trim() == "RebindAndNavigate")
                {
                        grdItem.MasterTableView.SortExpressions.Clear();
                        grdItem.MasterTableView.GroupByExpressions.Clear();
                        grdItem.DataSource = ((List<gsItem_BuscarResult>)Session["lstProductos"]).FindAll(x => x.Estado == 1);
                        grdItem.DataBind();

                        Calcular_Glosa();

                        lblMensaje.Text = "Se agregó el producto con nro. kardex " + e.Argument.Split('(')[1].Trim().Split(')')[0] + " al pedido.";
                        lblMensaje.CssClass = "mensajeExito";

                        acbProducto.Entries.Clear();
                        acbProducto.Focus();
                }

                if (e.Argument.Split('(')[0].Trim() == "RebindAndNavigateCorreo")
                 {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "buscar();", true);
                 }

                if (e.Argument.Split('(')[0].Trim() == "RebindAndNavigateLetras")
                {
                    string LetrasTXT;
                    LetrasTXT = (string)Session["objLetras"];
                    lblLetras.Text = LetrasTXT;

                    List<gsPedidos_FechasLetrasSelectResult> lstFechas = new List<gsPedidos_FechasLetrasSelectResult>();
                    lstFechas = ((List<gsPedidos_FechasLetrasSelectResult>)Session["lstFechasNew"]);

                    PedidosFechas_Letras(lstFechas);

                    int DiasCredito = (int)Session["DiasCredito"];
                    if (DiasCredito != 0)
                    {
                        //cboTipoCredito.SelectedValue = cboTipoCredito.FindItem(x => Convert.ToInt32(x.Value.Split(',')[1]) <= DiasCredito).Value;
                        txtDiasCredito.Text = DiasCredito.ToString();
                        dpFechaVencimiento.SelectedDate = dpFechaEmision.SelectedDate.Value.AddDays(Int32.Parse(txtDiasCredito.Text));
                    }


                    lblMensaje.Text = "Se planifico letras al pedido.";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                rwmPedidoMng.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            List<object> parametros = new List<object>();
            parametros.Add(Convert.ToInt32(hdReturn.Value));
            parametros.Add(((Usuario_LoginResult)Session["Usuario"]).idEmpresa);
            string responseFromServer = GSbase.POSTResult("ListarObjectFifty", 37, parametros);

            ObjectFiftyCollection collection = new JavaScriptSerializer().Deserialize<ObjectFiftyCollection>(responseFromServer);
            List<GlosaBE> lstGlosa = new List<GlosaBE>();
            List<gsItem_BuscarResult> lstProductos = new List<gsItem_BuscarResult>();
            var index = 0;
            if (collection.Rows.Count > 0)
                foreach (var row in collection.Rows)
                {
                    if (index == 0) {
                        AutoCompleteBoxEntry objEntry = new AutoCompleteBoxEntry();
                        objEntry.Text = row.v14.ToString() + "-" + row.v15.ToString();
                        acbCliente.Entries.Add(objEntry);
                        acbCliente.Enabled = false;
                        cboAlmacen.SelectedValue = row.v16.ToString();
                        btnBuscarCliente_Click(sender, e);
                    }

                    Item_Buscar(row.v03.ToString(), row.v14.ToString(), Convert.ToInt32(row.v16), true, row.v17.ToString(), Convert.ToInt32(row.v05));
                    Impuesto_Guardar(row.v03.ToString(), true);
                    index++;
                }


            grdItem.MasterTableView.SortExpressions.Clear();
            grdItem.MasterTableView.GroupByExpressions.Clear();
            grdItem.DataSource = (List<gsItem_BuscarResult>)Session["lstProductos"];
            grdItem.DataBind();
            Calcular_Glosa();

            btnVerCotizacion.Enabled = false;

            //grdGlosa.DataSource = lstGlosa.OrderBy(x => x.IdGlosa);
            //    grdGlosa.DataBind();
            //    ViewState["lstGlosa"] = JsonHelper.JsonSerializer(lstGlosa);
            //    Session["Impuestos"] = "";
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string id_agenda = "";
            bool? existe = false;
            int id_Almacen = 0; 


            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            OrdenVentaWCFClient objOrdenVentaWCF = new OrdenVentaWCFClient();
            try
            {

                // --------------- Validaciones 
                if (cboDespacho.Items.Count == 0 || cboDespacho.SelectedItem.Text.Trim() == "SELECCIONAR" || cboDespacho.SelectedItem.Text.Trim() == "" || string.IsNullOrEmpty(cboDespacho.SelectedItem.Text.Trim()))
                {
                    stripPedido.Tabs[0].Selected = true;
                    pagesPedido.PageViews[0].Selected = true;
                    cboDespacho.Focus();
                    throw new ArgumentException("No cuenta con dirección de despacho, revisar “Despacho” o  comunicarse con Facturación para que se registre.");
                }


                if (cboTipoCredito.Items.Count == 0  || cboTipoCredito.SelectedItem.Text.Trim() == "" || string.IsNullOrEmpty(cboTipoCredito.SelectedItem.Text.Trim()))
                {
                    stripPedido.Tabs[0].Selected = true;
                    pagesPedido.PageViews[0].Selected = true;
                    cboTipoCredito.Focus();
                    throw new ArgumentException("No cuenta con tipos de crédito, revisar “Tipo de Venta” o  comunicarse con créditos y cobranzas.");
                }

                if (string.IsNullOrEmpty(acbTransporte.Text))
                {
                    stripPedido.Tabs[0].Selected = true;
                    pagesPedido.PageViews[0].Selected = true;
                    acbTransporte.Focus();
                    throw new ArgumentException("No se pudo guardar el pedido porque debe seleccionar una empresa de transporte.");
                }
                else
                {
                   
                    try
                    {
                        id_agenda = acbTransporte.Entries[0].Text.Split('-')[0];

                        AgendaWCFClient objAgenda = new  AgendaWCFClient();
                        objAgenda.Agenda_BucarProveedor(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                                                                           ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, id_agenda, ref existe);
                        
                        if(existe==false)
                        {
                            stripPedido.Tabs[0].Selected = true;
                            pagesPedido.PageViews[0].Selected = true;
                            acbTransporte.Focus();
                            throw new ArgumentException("No se pudo guardar el pedido porque debe seleccionar una empresa de transporte.");
                        }
                      

                    }
                    catch
                    {
                        if (existe == false)
                        {
                            stripPedido.Tabs[0].Selected = true;
                            pagesPedido.PageViews[0].Selected = true;
                            acbTransporte.Focus();
                            throw new ArgumentException("No se pudo guardar el pedido porque debe seleccionar una empresa de transporte.");
                        }
                    }
                }


                if (rbtFacturas.Checked == false & rbtLetras.Checked == false)
                {
                    throw new ArgumentException("Seleccionar un Tipo de Credito (Factura o Letra).");
                }

                if (rbtFacturas.Checked == true)
                {
                    if (cboTipoCredito.SelectedItem.Text.Trim() == "Seleccionar")
                    {
                        stripPedido.Tabs[0].Selected = true;
                        pagesPedido.PageViews[0].Selected = true;
                        cboTipoCredito.Focus();
                        throw new ArgumentException("Seleccionar un Tipo de Credito");
                    }
                }

                if (rbtLetras.Checked == true)
                {
                    if (lblLetras.Text.Length == 0)
                    {
                        stripPedido.Tabs[0].Selected = true;
                        pagesPedido.PageViews[0].Selected = true;
                        cboTipoCredito.Focus();
                        throw new ArgumentException("Seleccionar Fechas de Letras");
                    }
                }



                if (string.IsNullOrEmpty(acbVendedor.Text))
                {
                    stripPedido.Tabs[0].Selected = true;
                    pagesPedido.PageViews[0].Selected = true;
                    acbVendedor.Focus();
                    throw new ArgumentException("No se pudo guardar el pedido porque debe seleccionar un vendedor.");
                }

                if (grdItem.Items.Count <= 0)
                {
                    stripPedido.Tabs[1].Selected = true;
                    pagesPedido.PageViews[1].Selected = true;
                    acbProducto.Focus();
                    throw new ArgumentException("No se pudo guardar el pedido porque debe ingresar por lo menos un producto.");
                }

                if (string.IsNullOrEmpty(acbVendedor.Text))
                {
                    stripPedido.Tabs[0].Selected = true;
                    pagesPedido.PageViews[0].Selected = true;
                    acbVendedor.Focus();
                    throw new ArgumentException("No se pudo guardar el pedido porque debe seleccionar un vendedor.");
                }


                //----------------------------------------------------- Completar datos para el registro
                int? idPedido = null;
                if ((Request.QueryString["idOrdenVenta"]) != "0")
                {
                    idPedido = int.Parse((Request.QueryString["idOrdenVenta"]));
                }
 
                //-----------------------------------------------------



                string PlanLetras = lblLetras.Text;

                decimal LineaCredito = 0;
                decimal TC = 0;

                LineaCredito = (decimal)ViewState["LineaCredito"];
                TC = (decimal)Session["TC"];



                string TipoCliente = (string)ViewState["TipoCliente"];
                string ID_Agenda = lblCodigoCliente.Value;
                string ID_Moneda = cboMoneda.SelectedValue.ToString();
                int KardexFlete = 0;

                if (TipoCliente != "FUNDO")
                {
                    // ------ Registrar Flete 
                    id_Almacen = Convert.ToInt32(cboAlmacen.SelectedValue.ToString());
                    KardexFlete = RegistrarItemFlete(id_Almacen, ID_Agenda, ID_Moneda, TC);
                    Calcular_Glosa();

                    //--------------------------
                }

                List<GlosaBE> lstGlosa = JsonHelper.JsonDeserialize<List<GlosaBE>>((string)ViewState["lstGlosa"]);

                gsOV_BuscarCabeceraResult objCabeceraPedido = new gsOV_BuscarCabeceraResult();
                objCabeceraPedido = OrdenVenta_ObtenerCabecera(); 

                if(objCabeceraPedido.ID_Moneda == 1)
                {
                    LineaCredito = LineaCredito * TC; 
                }

                DateTime dtFechaVencimiento = (DateTime)ViewState["FechaVencimiento"];


                int codEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                int codUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
                gsOV_BuscarCabeceraResult cabecera = objCabeceraPedido;


                gsOV_BuscarDetalleResult[] detalle = OrdenVenta_ObtenerDetalle().ToArray();

                //-----------------------------------------------------------
                gsOV_BuscarDetalleResult objetoFlete = new gsOV_BuscarDetalleResult(); 
                if (KardexFlete>0)
                {
                    objetoFlete = detalle.ToList().Find(x => x.Item_ID == KardexFlete && x.Estado == 1);
                }
                // ----------------------------------------------------------


                GlosaBE[] impuesto = Impuesto_Obtener().ToArray();
                gsPedidos_FechasLetrasSelectResult[] fechas = OrdenVenta_FechasLetras().ToArray();

                // ----------------------------------------------------------

                gsPedido_EliminarOP_WMSResult objApto = new gsPedido_EliminarOP_WMSResult();
                objApto = objOrdenVentaWCF.Pedido_Apto_Modificacion(codEmpresa, codUsuario, Convert.ToInt32(idPedido), Convert.ToInt32(idPedido));

                if((objApto.Column1 == 1))
                {
                    stripPedido.Tabs[0].Selected = true;
                    pagesPedido.PageViews[0].Selected = true;
                    acbCliente.Focus();
                    throw new ArgumentException("El pedido esta procesado en WMS, no se puede realizar la modificación.");
                }

                //------------------------------------------
                Session["DiasCredito"] = Session["DiasCredito"] == null ? "0" : Session["DiasCredito"].ToString();
                Session["DiasCredito"] = Session["DiasCredito"].ToString() == "" ? "0" : Session["DiasCredito"].ToString();
                int DiasCredito = Convert.ToInt32(Session["DiasCredito"].ToString());

                if (DiasCredito != 0)
                {
                    //cboTipoCredito.SelectedValue = cboTipoCredito.FindItem(x => Convert.ToInt32(x.Value.Split(',')[1]) <= DiasCredito).Value;
                    txtDiasCredito.Text = DiasCredito.ToString();
                    dpFechaVencimiento.SelectedDate = dpFechaEmision.SelectedDate.Value.AddDays(Int32.Parse(txtDiasCredito.Text));
                }
                //------------------------------------------
                cabecera.ID_Vehiculo3 = hdReturn.Value;
                objOrdenVentaWCF.OrdenVenta_Registrar(codEmpresa, codUsuario, cabecera , detalle, impuesto, idPedido, LineaCredito,
                   dtFechaVencimiento, fechas, PlanLetras, KardexFlete, objetoFlete, DiasCredito);
                Session.Remove("TC");

                if ((Request.QueryString["idOrdenVenta"]) == "0")
                    Response.Redirect("~/Comercial/Pedido/frmOrdenVenta.aspx");
                else
                    Response.Redirect("~/Comercial/Pedido/frmOrdenVenta.aspx?fechaInicial=" + Request.QueryString["fechaInicial"] + "&fechafinal=" + Request.QueryString["fechafinal"]);
            }
            catch (Exception ex)
            {
                rwmPedidoMng.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

        private void Item_Buscar(string idItem, string idCliente, decimal idAlmacen, bool nuevo, string stridMoneda, int cantidad)
        {
            decimal? stockDisponible = null;
            double? TC_Cambio = null;

            try
            {
                ItemWCFClient objItemWCF = new ItemWCFClient();
                gsItem_BuscarResult objProducto = new gsItem_BuscarResult();
                gsItem_BuscarResult objProductoActual = new gsItem_BuscarResult();

                if (nuevo)
                {
                    objProducto = objItemWCF.Item_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                        ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idItem, idCliente, DateTime.Now.Date, 0, null, null, null,
                        idAlmacen, ref stockDisponible, ref TC_Cambio);
                    //objProducto.Cantidad = cantidad;
                }
                else
                {
                    objProducto = ((List<gsItem_BuscarResult>)Session["lstProductos"]).Find(x => x.Item_ID.ToString() == idItem && x.Estado == 1);

                    objProductoActual = objItemWCF.Item_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                        ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, objProducto.Codigo, idCliente, DateTime.Now.Date,
                        0, null, null, null, idAlmacen, ref stockDisponible, ref TC_Cambio);
                    //objProductoActual.Cantidad = cantidad;
                }

                List<gsItem_BuscarResult> lstProductos = new List<gsItem_BuscarResult>();
                var objItem = new gsItem_BuscarResult();
                //objItem = JsonHelper.JsonDeserialize<gsItem_BuscarResult>((string)ViewState["objItem"]);
                objItem = objProducto;
                objItem.Precio = Math.Round(objProducto.Precio, 4);
                objItem.Cantidad = cantidad;
              
                objItem.Importe = Math.Round((1 - ((0) / 100)) * objItem.Precio * objItem.Cantidad, 4);

                objItem.Observacion = txtObservacion.Text;
                objItem.Descuento = 0;
                objItem.FactorUnidadInv = Math.Round(objProducto.FactorUnidadInv, 4);
                objItem.Stock = Math.Round(objProducto.Stock, 4); ;
                objItem.Estado = 1;
                objItem.CostoUnitario = objProducto.CostoUnitario;


                if (Session["lstProductos"] == null)
                {
                    lstProductos.Add(objItem);
                    Session["lstProductos"] = lstProductos;
                }
                else
                {
                    lstProductos = ((List<gsItem_BuscarResult>)Session["lstProductos"]);
                    lstProductos.Add(objItem);
                    Session["lstProductos"] = lstProductos;
                }

                Impuesto_Guardar_Cotizacion(objItem.Codigo, Convert.ToBoolean(Int32.Parse(Request.QueryString["nuevo"])));
            }
            catch(Exception ex)
            {

            }
        }

        private void Impuesto_Guardar_Cotizacion(string idItem, bool nuevo)
        {
            ImpuestoWCFClient objImpuestoWCF;
            List<gsImpuesto_ListarPorItemResult> lstImpuestos;
            try
            {
                if (nuevo)
                {
                    objImpuestoWCF = new ImpuestoWCFClient();
                    lstImpuestos = objImpuestoWCF.Impuesto_ListarPorItem(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                        ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idItem, DateTime.Now.Date).ToList();
                    if (Session["lstImpuestos"] == null)
                        Session["lstImpuestos"] = lstImpuestos;
                    else
                        ((List<gsImpuesto_ListarPorItemResult>)Session["lstImpuestos"]).AddRange(lstImpuestos);
                    lstImpuestos = ((List<gsImpuesto_ListarPorItemResult>)Session["lstImpuestos"]);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (acbCliente.Entries.Count <= 0 || acbCliente.Entries[0].Text.Length <= 0)
                    throw new ArgumentException("Debe seleccionar un cliente valido");

                lblCodigoCliente.Value = acbCliente.Text.Split('-')[0];

                Cliente_ValidarCorreo(lblCodigoCliente.Value); 



            }
            catch (Exception ex)
            {
                rwmPedidoMng.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (!string.IsNullOrEmpty(lblCodigoCliente.Value) && !string.IsNullOrEmpty(acbProducto.Text.Split('-')[0]))
                {
                    Item_Buscar(acbProducto.Text.Split('-')[0], txtRUC.Text);

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowInsertForm('" + acbProducto.Text.Split('-')[0] + "','" +
                        lblCodigoCliente.Value + "',1," + cboAlmacen.SelectedValue + "," + cboMoneda.SelectedValue +  ");", true);
                    acbProducto.Entries.Clear();
                }
                else
                {
                    if (string.IsNullOrEmpty(lblCodigoCliente.Value))
                        throw new ArgumentException("ERROR: Debe buscar y seleccionar un cliente antes de ingresar un producto.");
                    else
                        throw new ArgumentException("ERROR: Debe buscar el nombre del producto antes de agregarlo.");
                }
            }
            catch (Exception ex)
            {
                rwmPedidoMng.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

        protected void grdItem_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem dataItem = e.Item as GridDataItem;
                    if (decimal.Parse(dataItem["Stock"].Text) <= 0)
                        e.Item.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                rwmPedidoMng.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

        protected void cboAlmacen_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                Session["lstProductos"] = null;
                grdItem = null;
            }
            catch (Exception ex)
            {
                rwmPedidoMng.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

        protected void cboFormaPago_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if (cboFormaPago.SelectedItem.Text != "Crédito")
                {
                    DateTime FechaInicio;

                    txtDiasCredito.Text = "0";
                    txtDiasCredito.Value = 0;
                    btnLetras.Visible = false;
                    lblLetras.Visible = false;
                    cboTipoCredito.Visible = false;
                    
                    FechaInicio = dpFechaEmision.SelectedDate.Value;
                    dpFechaVencimiento.SelectedDate = FechaInicio.AddDays(Int32.Parse(txtDiasCredito.Text));

                    List<gsPedidos_FechasLetrasSelectResult> lstFechas = new List<gsPedidos_FechasLetrasSelectResult>();
                    lblLetras.Text = "";

                    Session["lstFechasNew"] = lstFechas;
                    Session["DiasCredito"] = txtDiasCredito.Value;
                    Session["objLetras"] = "";
                    Session["lstFechas"] = Session["lstFechasNew"];
                    rbtFacturas.Checked = true;
                    Credito_Cargar(lblCodigoCliente.Value, int.Parse(cboFormaPago.SelectedValue));
                    CheckFActura();
                }
                else
                {
                    VisibleFormaPago();
                    rbtLetras.Checked = true;
                    Credito_Cargar(lblCodigoCliente.Value, int.Parse(cboFormaPago.SelectedValue));
                    CheckLetra();
                }
            }
            catch (Exception ex)
            {
                rwmPedidoMng.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (string.IsNullOrEmpty(Request.QueryString["fechaInicial"]))
                    Response.Redirect("~/Comercial/Pedido/frmOrdenVenta.aspx");
                else
                    Response.Redirect("~/Comercial/Pedido/frmOrdenVenta.aspx?fechaInicial=" + Request.QueryString["fechaInicial"] +
                        "&fechafinal=" + Request.QueryString["fechafinal"]);
            }
            catch (Exception ex)
            {
                rwmPedidoMng.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (string.IsNullOrEmpty(Request.QueryString["fechaInicial"]))
                    Response.Redirect("~/Comercial/Pedido/frmOrdenVenta.aspx");
                else
                    Response.Redirect("~/Comercial/Pedido/frmOrdenVenta.aspx?fechaInicial=" + Request.QueryString["fechaInicial"] +
                        "&fechaFinal=" + Request.QueryString["fechafinal"]);
            }
            catch (Exception ex)
            {
                rwmPedidoMng.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }
        #endregion
   
        protected void btnAprobar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            string msjError = null;

            try {
                OrdenVentaWCFClient objOrdenVentaWCF = new OrdenVentaWCFClient();
                objOrdenVentaWCF.OV_TransGratuitas_Aprobar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                    int.Parse(Request.QueryString["idOrdenVenta"]), ref msjError);

                if (string.IsNullOrEmpty(Request.QueryString["fechaInicial"]))
                    Response.Redirect("~/Comercial/Pedido/frmOrdenVenta.aspx");
                else
                    Response.Redirect("~/Comercial/Pedido/frmOrdenVenta.aspx?fechaInicial=" + Request.QueryString["fechaInicial"] +
                        "&fechafinal=" + Request.QueryString["fechafinal"]);
            }
            catch (Exception ex) {
                rwmPedidoMng.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

        protected void btnLetras_Click(object sender, EventArgs e)
        {
            DateTime FechaInicio;
            DateTime FechaFin;
            int idOrdenVenta = 0;
            char Cero = Convert.ToChar("0");

            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");
            FechaInicio = dpFechaEmision.SelectedDate.Value;

            int DiasCredito = Convert.ToInt32(ViewState["DiasBase"].ToString());

            FechaFin = dpFechaEmision.SelectedDate.Value.AddDays(double.Parse(DiasCredito.ToString()));

            string strFechaInico = FechaInicio.Year.ToString() + FechaInicio.Month.ToString().PadLeft(2, Cero) + FechaInicio.Day.ToString().PadLeft(2, Cero);
            string strFechaFin = FechaFin.Year.ToString() + FechaFin.Month.ToString().PadLeft(2, Cero) + FechaFin.Day.ToString().PadLeft(2, Cero);


            try
            {
                idOrdenVenta = int.Parse((Request.QueryString["idOrdenVenta"]));

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowInsertFormLetras(" + strFechaInico + "," + strFechaFin + "," + idOrdenVenta + "," + lblCodigoCliente.Value + ");", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }


        // Plan Letras
        private void PedidosFechas_Buscar(int OpOV, int OpDoc)
        {
            try
            {
                List<gsPedidos_FechasLetrasSelectResult> lstFechas = new List<gsPedidos_FechasLetrasSelectResult>();
                PedidoWCFClient objPedidoWCF = new PedidoWCFClient();
                if (OpOV > 0 || OpDoc > 0)
                {
                    lstFechas = objPedidoWCF.PedidoLetras_Detalle(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, OpOV, OpDoc, "Tabla").ToList();
                    Session["lstFechas"] = lstFechas;
                }
                else
                {
                    Session["lstFechas"] = lstFechas;
                }
                PedidosFechas_Letras(lstFechas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PedidosFechas_Letras(List<gsPedidos_FechasLetrasSelectResult> lstFechas)
        {
            DateTime FechaPedido = new DateTime();
            DateTime FechaSelect = new DateTime();
            TimeSpan ts;
            string Letras = "";
            DateTime calendarMinDate = new DateTime();

            int DiasCredito = 0;
            int diferencia = 0;
            int inicial = 0;
            int final = 0;
            int y = 0;

            calendarMinDate = dpFechaEmision.SelectedDate.Value;
            FechaPedido = calendarMinDate;
            lblLetras.Text = "";

            var lstFecha = lstFechas.OrderBy(x => x.Fecha).ToList();
            foreach (var objFecha in lstFecha)
            {
                FechaSelect = Convert.ToDateTime(objFecha.Fecha);
                ts = FechaSelect - FechaPedido;
                diferencia = ts.Days;
                DiasCredito = diferencia;
                if (inicial == 0)
                {
                    Letras = Letras + diferencia;
                    inicial++;
                }
                else
                {
                    Letras = Letras + "-" + diferencia;
                }

                y++;
            }
            Letras = "L" + Letras;

            if (y > 0)
            {
                lblLetras.Text = Letras;
            }

            if (lblLetras.Text.Length > 0)
            {
                rbtFacturas.Checked = false;
                rbtLetras.Checked = true;
                cboTipoCredito.Visible = false;
                cboTipoCredito.SelectedIndex = 0;
                lblLetras.Visible = true;
                btnLetras.Visible = true;
            }
            else
            {
                rbtFacturas.Checked = true;
                rbtLetras.Checked = false;
                cboTipoCredito.Visible = true;
                //cboTipoCredito.SelectedIndex = 0;
                lblLetras.Visible = false;
                btnLetras.Visible = false;
            }

        }


        // Cobro Flete
        private int RegistrarItemFlete(int id_Almacen, string id_cliente, string strMoneda, decimal TC)
        {

            int KardexFlete = 0; 
            try
            {
                decimal? CantidaPeso = 0;
                int? kardex;
                decimal? PrecioSinIGV;
                decimal? PrecioConIGV;
                decimal? PRECIO_FLETE;
                string codigoItem;
                bool EsNuevo; 
 
                ItemWCFClient objItemWCF = new ItemWCFClient();
                spAlmacenesFlete_ListarResult objetoFlete = new spAlmacenesFlete_ListarResult();
                List<spAlmacenesFlete_ListarResult>lstFlete = new List<spAlmacenesFlete_ListarResult>();

                if (id_Almacen > 0)
                {
                    lstFlete = objItemWCF.AlmacenesFlete_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                                                                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, id_Almacen).ToList();
               
                    if (lstFlete.Count()>0)
                    {
                        objetoFlete = lstFlete[0]; 

 
                        kardex = objetoFlete.Kardex;
                        codigoItem = objetoFlete.Codigo;

                        KardexFlete = (int)kardex; 

                        gsItem_BuscarResult objItemF = new gsItem_BuscarResult();
                        objItemF = ((List<gsItem_BuscarResult>)Session["lstProductos"]).Find(x => x.Item_ID.ToString() == objetoFlete.Kardex.ToString() && x.Estado == 1);
                        if(objItemF == null)
                        {
                            EsNuevo = true; 
                        }
                        else
                        {
                            EsNuevo = false; 
                        }
  
                        Item_Buscar_Flete(kardex.ToString(), codigoItem.ToString(), id_cliente, id_Almacen, EsNuevo, strMoneda);

                        PrecioSinIGV = objetoFlete.PrecioS;

                        if (strMoneda == "0")
                        {
                            PRECIO_FLETE = (PrecioSinIGV / TC);
                        }
                        else
                        {
                            PRECIO_FLETE = (PrecioSinIGV);
                        }


                        List<gsItem_BuscarResult> lstProductos = new List<gsItem_BuscarResult>();
                        gsItem_BuscarResult objItem;
                        try
                        {

                            if (!EsNuevo)
                            {
                                objItem = new gsItem_BuscarResult();
                                objItem = ((List<gsItem_BuscarResult>)Session["lstProductos"]).Find(x => x.Item_ID.ToString() == kardex.ToString()  && x.Estado == 1);
                                ((List<gsItem_BuscarResult>)Session["lstProductos"]).Remove(objItem);
                            }

                            decimal PesoItem = 0;
                            decimal PesoPedido = 0; 
                            List<gsItem_BuscarResult> listaPeso = ((List<gsItem_BuscarResult>)Session["lstProductos"]).FindAll(x => x.Item_ID.ToString() != kardex.ToString() && x.Estado == 1).ToList();
                            foreach (gsItem_BuscarResult objeto in listaPeso)
                            {
                                PesoItem = 0; 

                                if(objeto.Peso_Kg2>0)
                                {
                                    PesoItem = (objeto.Cantidad * objeto.Peso_Kg2); 
                                }
                                else if (objeto.Peso_Kg2 == 0)
                                {
                                    PesoItem = (objeto.Cantidad * objeto.Peso2 * objeto.FactorConversion2);
                                }
                                else
                                {
                                    PesoItem = (objeto.Cantidad * objeto.Peso2 * objeto.FactorConversion2);
                                }

                                PesoPedido = PesoPedido + PesoItem; 
                            }

                            CantidaPeso = PesoPedido;

                            if (CantidaPeso <= 0)
                                throw new ArgumentException("La cantidad ingresada debe ser mayor a 0.");

                            if (CantidaPeso <= 0)
                                throw new ArgumentException("El precio ingresado debe ser mayor a 0.");


                            objItem = new gsItem_BuscarResult();
                            objItem = JsonHelper.JsonDeserialize<gsItem_BuscarResult>((string)ViewState["objItemFlete"]);


                            objItem.Precio = Math.Round(decimal.Parse(PRECIO_FLETE.ToString()),4);
                            objItem.Cantidad = Math.Round(decimal.Parse(CantidaPeso.ToString()),4);
                            objItem.Importe = Math.Round(Convert.ToDecimal((PRECIO_FLETE * CantidaPeso)), 4);

                            objItem.Observacion = "Flete"; // txtObservacion.Text;
                            objItem.Descuento = 0;
                            objItem.FactorUnidadInv = objItem.FactorUnidadInv;  //Math.Round(decimal.Parse(txtFactor.Text), 4);
                            objItem.Stock = 1; // Math.Round(decimal.Parse(txtStock.Text), 4); ;
                            objItem.Estado = 1;
                            objItem.CostoUnitario = decimal.Parse(PRECIO_FLETE.ToString());


                            if (Session["lstProductos"] == null)
                            {
                                lstProductos.Add(objItem);
                                Session["lstProductos"] = lstProductos;
                            }
                            else
                            {
                                lstProductos = ((List<gsItem_BuscarResult>)Session["lstProductos"]);
                                lstProductos.Add(objItem);
                                Session["lstProductos"] = lstProductos;
                            }

                            Impuesto_Guardar(objItem.Codigo, EsNuevo);
                        }
                        catch (Exception ex)
                        {

                            lblMensaje.Text = ex.Message;
                            lblMensaje.CssClass = "mensajeError";
                            throw new ArgumentException("Error: " + ex.Message.ToString());
                        }
                    }
                }
 
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return KardexFlete; 
        }

        private void Item_Buscar_Flete(string kardex, string idItem, string idCliente, decimal idAlmacen, bool nuevo, string stridMoneda)
        {
            decimal? stockDisponible = null;
            double? TC_Cambio = null;
            decimal? precio = null;
            decimal? TC = null;
            string idMoneda; 
            try
            {
                ItemWCFClient objItemWCF = new ItemWCFClient();
                gsItem_BuscarResult objProducto = new gsItem_BuscarResult();
                gsItem_BuscarResult objProductoActual = new gsItem_BuscarResult();


                if (nuevo)
                {
                    objProducto = objItemWCF.Item_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                        ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idItem, idCliente, DateTime.Now.Date, 0, null, null, null,
                        idAlmacen, ref stockDisponible, ref TC_Cambio);
                    precio = Math.Round(Convert.ToDecimal(objProducto.Precio.ToString()), 2);
                    //PrecioInicial.Text = precio.ToString();
                    TC = Convert.ToDecimal(objProducto.TC.ToString());
                }
                else
                {
                    List<gsItem_BuscarResult> lista = new List<gsItem_BuscarResult>();
                    lista = ((List<gsItem_BuscarResult>)Session["lstProductos"]); 

                    objProducto = ((List<gsItem_BuscarResult>)Session["lstProductos"]).Find(x => x.Item_ID.ToString() == kardex && x.Estado == 1);

                    objProductoActual = objItemWCF.Item_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                        ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, objProducto.Codigo, idCliente, DateTime.Now.Date,
                        0, null, null, null, idAlmacen, ref stockDisponible, ref TC_Cambio);

                    precio = Math.Round(Convert.ToDecimal(objProductoActual.Precio.ToString()), 2);
                    //PrecioInicial.Text = precio.ToString();
                    TC = Convert.ToDecimal(objProductoActual.TC.ToString());

                }


                idMoneda = objProducto.ID_Moneda.ToString();
                if (stridMoneda != idMoneda)
                {
                    if (stridMoneda == "0")
                    {
                        precio = Convert.ToDecimal(objProducto.Precio.ToString());
                        precio = (precio / TC);
                        precio = Math.Round(Convert.ToDecimal(precio.ToString()), 4);
                        objProducto.Precio = decimal.Parse(precio.ToString());
                        objProducto.ID_Moneda = 0;
                        objProducto.Signo = "US$";
                    }
                    else
                    {
                        precio = Convert.ToDecimal(objProducto.Precio.ToString());
                        precio = (precio * TC);
                        precio = Math.Round(Convert.ToDecimal(precio.ToString()), 4);

                        objProducto.Precio = decimal.Parse(precio.ToString());
                        objProducto.ID_Moneda = 1;
                        objProducto.Signo = "S/.";
                    }
                }
                else
                {

                    if (!nuevo)
                    {
                        //precio = Math.Round(Convert.ToDecimal(objProductoActual.Precio.ToString()), 2);
                        idMoneda = objProductoActual.ID_Moneda.ToString();
                        TC = Convert.ToDecimal(objProductoActual.TC.ToString());

                        if (stridMoneda != idMoneda)
                        {
                            if (stridMoneda == "0")
                            {
                                precio = Convert.ToDecimal(objProductoActual.Precio.ToString());
                                precio = (precio / TC);
                                precio = Math.Round(Convert.ToDecimal(precio.ToString()), 4);
                            }
                            else
                            {
                                precio = Convert.ToDecimal(objProductoActual.Precio.ToString());
                                precio = (precio * TC);
                                precio = Math.Round(Convert.ToDecimal(precio.ToString()), 4);
                            }
                        }
                    }

                }

                ViewState["objItemFlete"] = JsonHelper.JsonSerializer(objProducto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Impuesto_Guardar(string idItem, bool nuevo)
        {
            ImpuestoWCFClient objImpuestoWCF;
            List<gsImpuesto_ListarPorItemResult> lstImpuestos;
            try
            {
                if (nuevo)
                {
                    objImpuestoWCF = new ImpuestoWCFClient();
                    lstImpuestos = objImpuestoWCF.Impuesto_ListarPorItem(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                        ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idItem, DateTime.Now.Date).ToList();
                    if (Session["lstImpuestos"] == null)
                        Session["lstImpuestos"] = lstImpuestos;
                    else
                        ((List<gsImpuesto_ListarPorItemResult>)Session["lstImpuestos"]).AddRange(lstImpuestos);
                    lstImpuestos = ((List<gsImpuesto_ListarPorItemResult>)Session["lstImpuestos"]);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void radioButton1_CheckedChanged1(object sender, EventArgs e)
        {
            CheckFActura();
        }

        public void CheckFActura()
        {
            DateTime FechaInicio = new DateTime();

            txtDiasCredito.Text = cboTipoCredito.SelectedValue == "" ? "0" : cboTipoCredito.SelectedValue.Split(',')[1];
            txtDiasCredito.Value = Convert.ToDouble(ViewState["DiasBase"].ToString() == "" ? "0" : ViewState["DiasBase"].ToString());

            if (cboFormaPago.SelectedItem.Text != "Crédito")
            {
                txtDiasCredito.Text = "0";
                txtDiasCredito.Value = 0;
                btnLetras.Visible = false;
                lblLetras.Visible = false;
                cboTipoCredito.Visible = true;
                rbtLetras.Visible = false;
                rbtFacturas.Visible = true;
            }
            else
            {
                if (rbtFacturas.Checked == true)
                {
                    cboTipoCredito.Visible = true;
                    btnLetras.Visible = false;
                    lblLetras.Visible = false;
                }
                else
                {
                    cboTipoCredito.Visible = false;
                    btnLetras.Visible = true;
                    lblLetras.Visible = true;
                }
            }

            FechaInicio = dpFechaEmision.SelectedDate.Value;
            dpFechaVencimiento.SelectedDate = FechaInicio.AddDays(Int32.Parse(txtDiasCredito.Text));

            List<gsPedidos_FechasLetrasSelectResult> lstFechas = new List<gsPedidos_FechasLetrasSelectResult>();
            lblLetras.Text = "";

            Session["lstFechasNew"] = lstFechas;
            Session["DiasCredito"] = Convert.ToDouble(txtDiasCredito.Text);
            Session["objLetras"] = "";
            Session["lstFechas"] = Session["lstFechasNew"];
        }

        public void CheckLetra()
        {
            DateTime FechaInicio = new DateTime();

            txtDiasCredito.Text = ViewState["DiasBase"].ToString() == "" ? "0" : ViewState["DiasBase"].ToString();
            txtDiasCredito.Value = Convert.ToDouble(ViewState["DiasBase"].ToString() == "" ? "0" : ViewState["DiasBase"].ToString());

            if (cboFormaPago.SelectedItem.Text != "Crédito")
            {
                txtDiasCredito.Text = "0";
                txtDiasCredito.Value = 0;
                btnLetras.Visible = false;
                lblLetras.Visible = false;
                cboTipoCredito.Visible = false;
            }
            else
            {

                if (rbtLetras.Checked == true)
                {
                    cboTipoCredito.Visible = false;
                    btnLetras.Visible = true;
                    lblLetras.Visible = true;
                }
                else
                {
                    cboTipoCredito.Visible = true;
                    btnLetras.Visible = false;
                    lblLetras.Visible = false;
                }
            }

            FechaInicio = dpFechaEmision.SelectedDate.Value;
            dpFechaVencimiento.SelectedDate = FechaInicio.AddDays(Int32.Parse(txtDiasCredito.Text));

            List<gsPedidos_FechasLetrasSelectResult> lstFechas = new List<gsPedidos_FechasLetrasSelectResult>();
            lblLetras.Text = "";

            Session["lstFechasNew"] = lstFechas;
            Session["DiasCredito"] = txtDiasCredito.Value;
            Session["objLetras"] = "";
            Session["lstFechas"] = Session["lstFechasNew"];
        }

        protected void radioButton2_CheckedChanged1(object sender, EventArgs e)
        {
            CheckLetra();
        }
    }
}