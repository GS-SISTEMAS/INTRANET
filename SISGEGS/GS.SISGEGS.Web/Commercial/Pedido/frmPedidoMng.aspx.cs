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
using GS.SISGEGS.Web.PedidoWCF;
using GS.SISGEGS.Web.DespachoWCF;
using GS.SISGEGS.Web.MonedaWCF;
using GS.SISGEGS.Web.FormaPagoWCF;
using GS.SISGEGS.Web.ItemWCF;
using GS.SISGEGS.Web.ImpuestoWCF;
using GS.SISGEGS.Web.VarianteWCF;


namespace GS.SISGEGS.Web.Commercial.Pedido
{
    public partial class frmPedidoMng : System.Web.UI.Page
    {
        #region Métodos privados
        private void Variante_Cargar() {
            VarianteWCFClient objVarianteWCF;
            Variante_BuscarResult objVariante;
            try {
                objVarianteWCF = new VarianteWCFClient();
                objVariante = objVarianteWCF.Variante_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, "PEDMNG");
                cboAlmacen.SelectedValue = cboAlmacen.Items.FindItem(x => x.Text.ToUpper() == objVariante.parametro1.ToUpper()).Value;
                cboOpDocVenta.SelectedValue = cboOpDocVenta.Items.FindItem(x => x.Text.ToUpper() == objVariante.parametro2.ToUpper()).Value;
                cboFormaPago.SelectedValue = cboFormaPago.Items.FindItem(x => x.Text.ToUpper() == objVariante.parametro3.ToUpper()).Value;
                cboSede.SelectedValue = cboSede.Items.FindItem(x => x.Text.ToUpper() == objVariante.parametro4.ToUpper()).Value;
                lblTrans.Text = objVariante.parametro5;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private void Pedido_Cargar(int idPedido)
        {
            PedidoWCFClient objPedidoWCF = new PedidoWCFClient(); ;
            gsPedido_BuscarCabeceraResult objPedido;
            List<GlosaBE> lstGlosa = new List<GlosaBE>();
            ImpuestoWCFClient objImpuestoWCF = new ImpuestoWCFClient();
            List<gsImpuesto_BuscarPorPedidoResult> lstImpuestos;
            List<gsImpuesto_ListarPorItemResult> lstImpuestoItem = new List<gsImpuesto_ListarPorItemResult>();
            List<gsPedido_BuscarDetalleResult> lstPedidoDetalle;
            List<gsItem_BuscarResult> lstProductos = new List<gsItem_BuscarResult>();
            try
            {
                objPedido = objPedidoWCF.Pedido_BuscarCabecera(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idPedido);
                lstImpuestos = objImpuestoWCF.Impuesto_BuscarPorPedido(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idPedido).ToList();
                lstPedidoDetalle = objPedidoWCF.Pedido_BuscarDetalle(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, 
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idPedido, objPedido.ID_Almacen).ToList();

                acbCliente.Visible = false;
                btnBuscarCliente.Visible = false;

                lblCodigoCliente.Value = objPedido.ID_Agenda;
                Cliente_Buscar(objPedido.ID_Agenda);
                Direccion_Cargar(objPedido.ID_Agenda);
                Sucursal_Cargar(objPedido.ID_Agenda);
                Credito_Cargar(objPedido.ID_Agenda);
                Cliente_Buscar(objPedido.ID_Agenda);
                Cargar_Glosa();

                if (!string.IsNullOrEmpty(cboSucursal.SelectedValue))
                    Referencia_Cargar(lblCodigoCliente.Value, int.Parse(cboSucursal.SelectedValue));

                AutoCompleteBoxEntry objEntry = new AutoCompleteBoxEntry();
                objEntry.Text = objPedido.ID_Vendedor.ToString() + "-" + objPedido.Vendedor;
                acbVendedor.Entries.Add(objEntry);
                cboSucursal.SelectedValue = objPedido.ID_AgendaAnexo.ToString();
                cboFacturacion.SelectedValue = objPedido.ID_AgendaDireccion.ToString();
                cboDespacho.SelectedValue = objPedido.ID_AgendaDireccion2.ToString();
                cboTipoEnvio.SelectedValue = objPedido.ID_Envio.ToString();
                cboPrioridad.SelectedValue = (objPedido.Prioridad + 1).ToString();
                cboMoneda.SelectedValue = objPedido.ID_Moneda.ToString();
                cboFormaPago.SelectedValue = objPedido.Id_Pago.ToString();
                acbVendedor.Enabled = false;
                txtOrden.Text = objPedido.NroOrdenCliente;
                dpFechaEmision.SelectedDate = objPedido.FechaEmision;
                dpFechaVencimiento.SelectedDate = objPedido.FechaVencimiento;
                txtDiasCredito.Value = ((DateTime)objPedido.FechaVencimiento - (DateTime)objPedido.FechaEmision).TotalDays;
                cboTipoCredito.SelectedValue = cboTipoCredito.FindItem(x => x.Value.Split(',')[0] == objPedido.ID_CondicionCredito.ToString()).Value;
                txtObservacion.Text = objPedido.Observaciones;
                if (string.IsNullOrEmpty(objPedido.ID_Sede.ToString().Trim()))
                    cboSede.SelectedIndex = 0;
                else
                    cboSede.SelectedValue = objPedido.ID_Sede.ToString();
                cboReferencia.SelectedValue = objPedido.ID_AgendaAnexoReferencia.ToString();
                cboOpDespacho.SelectedValue = objPedido.ID_TipoDespacho.ToString();
                cboOpTipoPedido.SelectedValue = objPedido.ID_TipoPedido.ToString();
                cboOpDocVenta.SelectedValue = objPedido.ID_DocumentoVenta.ToString();
                cboAlmacen.SelectedValue = objPedido.ID_Almacen.ToString();
                txtNroRegistro.Text = objPedido.NoRegistro;
                if (!string.IsNullOrEmpty(objPedido.ID_AgendaDestino))
                {
                    objEntry = new AutoCompleteBoxEntry();
                    objEntry.Text = objPedido.ID_AgendaDestino.ToString() + "-" + objPedido.Transporte;
                    acbTransporte.Entries.Add(objEntry);
                }

                if (!string.IsNullOrEmpty(objPedido.ID_ReferenciaAgenda)) {
                    objEntry = new AutoCompleteBoxEntry();
                    objEntry.Text = objPedido.ID_ReferenciaAgenda.ToString() + "-" + objPedido.Contacto;
                    acbContacto.Entries.Add(objEntry);
                }

                lstGlosa = JsonHelper.JsonDeserialize<List<GlosaBE>>((string)ViewState["lstGlosa"]);
                lstGlosa.Find(x => x.Descripcion == "Neto").Importe = objPedido.Neto;
                lstGlosa.Find(x => x.Descripcion == "Descuento").Importe = objPedido.Dcto;
                lstGlosa.Find(x => x.Descripcion == "SubTotal").Importe = objPedido.SubTotal;
                lstGlosa.Find(x => x.Descripcion == "Total").Importe = objPedido.Total;

                foreach (gsImpuesto_BuscarPorPedidoResult objImpuesto in lstImpuestos)
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

                foreach (gsPedido_BuscarDetalleResult objPedidoDetalle in lstPedidoDetalle)
                {
                    gsItem_BuscarResult objItem = new gsItem_BuscarResult();

                    objItem.Codigo = objPedidoDetalle.ID_Item;
                    objItem.Cantidad = Convert.ToInt32(objPedidoDetalle.Cantidad);
                    objItem.DctoMax = objPedidoDetalle.DctoMax;
                    objItem.Descuento = objPedidoDetalle.Dcto;
                    objItem.ID_Moneda = objPedidoDetalle.ID_Moneda;
                    objItem.ID_UnidadControl = objPedidoDetalle.ID_UnidadDoc;
                    objItem.ID_UnidadInv = objPedidoDetalle.ID_UnidadInv;
                    objItem.Importe = objPedidoDetalle.Importe;
                    objItem.Item = objPedidoDetalle.Item;
                    objItem.Item_ID = objPedidoDetalle.Item_ID;
                    objItem.NombreMoneda = objPedidoDetalle.NombreMoneda;
                    objItem.Observacion = objPedidoDetalle.Observaciones;
                    objItem.Precio = objPedidoDetalle.Precio;
                    objItem.PrecioInicial = objPedidoDetalle.PrecioMinimo;
                    objItem.Signo = objPedidoDetalle.Signo;
                    objItem.Stock = objPedidoDetalle.Stock;
                    objItem.FactorUnidadInv = objPedidoDetalle.FactorUnidadInv;
                    objItem.UnidadPresentacion = objPedidoDetalle.UnidadPresentacion;
                    objItem.ID_Amarre = objPedidoDetalle.ID_Amarre;
                    objItem.Estado = 1;
                    lstImpuestoItem.AddRange(objImpuestoWCF.Impuesto_ListarPorItem(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                        ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, objPedidoDetalle.ID_Item, DateTime.Now));
                    ViewState["fecha"] = objPedido.FechaOrden;

                    lstProductos.Add(objItem);
                }

                Session["lstProductos"] = lstProductos;
                Session["lstImpuestos"] = lstImpuestoItem;
                grdItem.DataSource = lstProductos;
                grdItem.DataBind();
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

        private List<PedidoDetBE> PedidoDetalle_Obtener()
        {
            List<PedidoDetBE> lstPedidoDetBE;
            List<gsItem_BuscarResult> lstProductos = (List<gsItem_BuscarResult>)Session["lstProductos"];
            PedidoDetBE objProducto;
            try
            {
                lstPedidoDetBE = new List<PedidoDetBE>();
                foreach (gsItem_BuscarResult producto in lstProductos)
                {
                    objProducto = new PedidoDetBE();
                    objProducto.IdAmarre = producto.ID_Amarre;
                    objProducto.TablaOrigen = "OV";
                    objProducto.ID_Item = producto.Codigo;
                    objProducto.ID_ItemPedido = null;
                    objProducto.Item_ID = producto.Item_ID;
                    objProducto.Cantidad = producto.Cantidad;
                    objProducto.Precio = producto.Precio;
                    objProducto.Dcto = producto.Descuento;
                    objProducto.DctoValor = producto.Descuento * producto.Precio / 100;
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
                    objProducto.Estado = Convert.ToBoolean(producto.Estado);

                    lstPedidoDetBE.Add(objProducto);
                }
                return lstPedidoDetBE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private PedidoCabBE PedidoCabecera_Obtener()
        {
            PedidoCabBE objPedidoCabBE;
            decimal impuesto = 0;
            try
            {
                objPedidoCabBE = new PedidoCabBE();
                objPedidoCabBE.IdAgenda = lblCodigoCliente.Value;
                if (!string.IsNullOrEmpty(txtNroRegistro.Text))
                    objPedidoCabBE.NroRegistro = txtNroRegistro.Text;
                else
                    objPedidoCabBE.NroRegistro = "0"; ;

                if ((Request.QueryString["idPedido"]) == "0")
                {
                    objPedidoCabBE.FechaDespacho = DateTime.Now.Date;
                    objPedidoCabBE.FechaEntrega = DateTime.Now.Date;
                    objPedidoCabBE.FechaVigencia = DateTime.Now.Date;
                    objPedidoCabBE.Fecha = DateTime.Now.Date;
                }
                else
                {
                    objPedidoCabBE.FechaDespacho = (DateTime)ViewState["fecha"];
                    objPedidoCabBE.FechaEntrega = (DateTime)ViewState["fecha"];
                    objPedidoCabBE.FechaVigencia = (DateTime)ViewState["fecha"];
                    objPedidoCabBE.Fecha = (DateTime)ViewState["fecha"];
                }

                objPedidoCabBE.FechaOrden = dpFechaEmision.SelectedDate.Value;
                objPedidoCabBE.FechaVencimiento = dpFechaVencimiento.SelectedDate.Value;
                objPedidoCabBE.IdEnvio = int.Parse(cboTipoEnvio.SelectedValue);
                if (cboReferencia.SelectedValue != "-1")
                    objPedidoCabBE.IdAgenciaAnexoReferencia = int.Parse(cboReferencia.SelectedValue);
                else
                    objPedidoCabBE.IdAgenciaAnexoReferencia = null;
                objPedidoCabBE.IdVendedor = acbVendedor.Text.Split('-')[0].Trim();
                objPedidoCabBE.IdMoneda = int.Parse(cboMoneda.SelectedValue);
                List<GlosaBE> lstGlosa = JsonHelper.JsonDeserialize<List<GlosaBE>>((string)ViewState["lstGlosa"]);
                objPedidoCabBE.Neto = lstGlosa.Find(x => x.Descripcion == "Neto").Importe;
                objPedidoCabBE.Descuento = lstGlosa.Find(x => x.Descripcion == "Descuento").Importe;
                objPedidoCabBE.Subtotal = lstGlosa.Find(x => x.Descripcion == "SubTotal").Importe;
                objPedidoCabBE.Total = lstGlosa.Find(x => x.Descripcion == "Total").Importe;
                lstGlosa = (JsonHelper.JsonDeserialize<List<GlosaBE>>((string)ViewState["lstGlosa"])).FindAll(x => x.Descripcion != "Neto" && x.Descripcion != "Descuento" && x.Descripcion != "SubTotal" && x.Descripcion != "Total");
                foreach (GlosaBE objGlosaBE in lstGlosa)
                {
                    impuesto = impuesto + objGlosaBE.Importe;
                }
                objPedidoCabBE.Impuestos = impuesto;
                objPedidoCabBE.Observaciones = txtObservacion.Text;
                objPedidoCabBE.Prioridad = cboPrioridad.SelectedIndex;
                objPedidoCabBE.EntregaParcial = true; //Flag para poder hacer entregas parcialmente
                objPedidoCabBE.Estado = 376; //Cuenta Corriente en StandBy
                objPedidoCabBE.IdPago = int.Parse(cboFormaPago.SelectedValue);
                if (cboSucursal.SelectedValue != "-1")
                    objPedidoCabBE.IdAgenciaAnexo = int.Parse(cboSucursal.SelectedValue);
                else
                    objPedidoCabBE.IdAgenciaAnexo = null;
                objPedidoCabBE.TEA = decimal.Parse(txtTEA.Text);
                objPedidoCabBE.IdAgenciaDireccion1 = int.Parse(cboFacturacion.SelectedValue);
                objPedidoCabBE.IdAgenciaDireccion2 = int.Parse(cboDespacho.SelectedValue);
                objPedidoCabBE.ModoPago = null; //No estoy seguro
                objPedidoCabBE.NotasDespacho = null;
                objPedidoCabBE.IdCondicionCredito = int.Parse(cboTipoCredito.SelectedValue.Split(',')[0]);
                if (string.IsNullOrEmpty(txtOrden.Text))
                    objPedidoCabBE.NroOrdenCliente = null;
                else
                    objPedidoCabBE.NroOrdenCliente = txtOrden.Text;
                objPedidoCabBE.IdNaturalezaGasto = null;
                objPedidoCabBE.IdAgendaOrigen = null;
                objPedidoCabBE.IdSucursalOrigen = null;
                objPedidoCabBE.IdReferenciaOrigen = null;
                objPedidoCabBE.IdDireccionOrigen = null;
                objPedidoCabBE.IdAgendaDestino = null;
                objPedidoCabBE.IdSucursalDestino = null;
                objPedidoCabBE.IdReferenciaDestino = null;
                objPedidoCabBE.IdDireccionDestino = null;
                objPedidoCabBE.IdTipoDespacho = int.Parse(cboOpDespacho.SelectedValue);
                objPedidoCabBE.IdTipoPedido = int.Parse(cboOpTipoPedido.SelectedValue);
                objPedidoCabBE.IdDocumentoVenta = int.Parse(cboOpDocVenta.SelectedValue);
                objPedidoCabBE.IdAlmacen = int.Parse(cboAlmacen.SelectedValue);
                objPedidoCabBE.IdTransportista = null;
                objPedidoCabBE.IdChofer = null;
                objPedidoCabBE.IdVehiculo1 = null;
                objPedidoCabBE.IdVehiculo2 = null;
                objPedidoCabBE.IdVehiculo3 = null;
                objPedidoCabBE.HoraAtencionOpcional1_Desde = null;
                objPedidoCabBE.HoraAtencionOpcional1_Hasta = null;
                objPedidoCabBE.HoraAtencionOpcional2_Desde = null;
                objPedidoCabBE.HoraAtencionOpcional2_Hasta = null;
                objPedidoCabBE.HoraAtencionOpcional2_Desde = null;
                objPedidoCabBE.HoraAtencionOpcional2_Hasta = null;
                if (cboSede.SelectedValue != "-1")
                    objPedidoCabBE.IdSede = int.Parse(cboSede.SelectedValue);
                else
                    objPedidoCabBE.IdSede = null;

                if (acbContacto.Entries.Count <= 0)
                    objPedidoCabBE.IdContacto = null;
                else
                    objPedidoCabBE.IdContacto = acbContacto.Entries[0].Text.Split('-')[0];

                if (acbTransporte.Entries.Count <= 0)
                {
                    objPedidoCabBE.IdTransportista = null;
                    objPedidoCabBE.IdAgendaDestino = null;
                }
                else
                {
                    objPedidoCabBE.IdTransportista = lblTrans.Text;
                    objPedidoCabBE.IdAgendaDestino = acbTransporte.Entries[0].Text.Split('-')[0];
                }

                return objPedidoCabBE;
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
                    neto = neto + objProducto.Importe;
                    descuento = descuento + (objProducto.Descuento / 100) * objProducto.Importe;
                }
                subtotal = neto - descuento;
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
                    gsItem_BuscarResult objProducto = lstProductos.Find(x => x.Item_ID == objImpuesto.ID_Item);
                    if (lstGlosa.FindAll(x => x.IdGlosa == objImpuesto.ID_Impuesto).Count <= 0)
                    {
                        objGlosaBE.IdGlosa = objImpuesto.ID_Impuesto;
                        objGlosaBE.Descripcion = objImpuesto.Abreviacion;
                        if(objImpuesto.Valor > 0)
                            objGlosaBE.BaseImponible = objProducto.Importe;
                        else
                            objGlosaBE.BaseImponible = 0;
                        objGlosaBE.Importe = Math.Round(((decimal)objImpuesto.Valor / 100) * (objProducto.Importe * (1 - (objProducto.Descuento / 100))), 4);
                    }
                    else
                    {
                        objGlosaBE = lstGlosa.Find(x => x.IdGlosa == objImpuesto.ID_Impuesto);
                        lstGlosa.Remove(objGlosaBE);
                        if (objImpuesto.Valor > 0)
                            objGlosaBE.BaseImponible = objGlosaBE.BaseImponible + objProducto.Importe;
                        objGlosaBE.Importe = Math.Round(objGlosaBE.Importe + ((decimal)objImpuesto.Valor / 100) * (objProducto.Importe * (1 - (objProducto.Descuento / 100))), 4);
                    }
                    lstGlosa.Add(objGlosaBE);
                }

                lstGlosaImpuestos.Clear();
                lstGlosaImpuestos = lstGlosa.FindAll(x => x.Descripcion != "Neto" && x.Descripcion != "Descuento" && x.Descripcion != "SubTotal" && x.Descripcion != "Total").ToList();
                foreach (GlosaBE objGlosaImpuestos in lstGlosaImpuestos)
                {
                    impuestos = impuestos + objGlosaImpuestos.Importe;
                }
                lstGlosa.Find(x => x.Descripcion == "Total").Importe = Math.Round(subtotal + impuestos, 3);

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
                        throw new ArgumentException("El producto ya ha sido seleccionado.");
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

        private void Referencia_Cargar(string idAgenda, int idSucursal)
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

                cboReferencia.DataSource = lstReferencias;
                cboReferencia.DataTextField = "Nombre";
                cboReferencia.DataValueField = "ID";
                cboReferencia.DataBind();

                if (cboReferencia.Items.Count > 0)
                    cboReferencia.SelectedIndex = 0;
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
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario);
                cboOpDocVenta.DataTextField = "Nombre";
                cboOpDocVenta.DataValueField = "ID";
                cboOpDocVenta.DataBind();

                if (cboOpDocVenta.Items.Count > 0)
                    cboOpDocVenta.SelectedIndex = 0;
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

        private void Credito_Cargar(string idAgenda)
        {
            CreditoWCFClient objCreditoWCF;
            try
            {
                objCreditoWCF = new CreditoWCFClient();

                var datasource = from x in objCreditoWCF.Credito_ListarCondicion(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda)
                                 select new
                                 {
                                     ValueField = String.Format("{0},{1}", x.ID_CondicionCredito, x.DiasCredito),
                                     TextField = String.Format("{0}", x.Nombre)
                                 };

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
                                 ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda).ToList().FindAll(x=>x.TipoDireccion == 104)
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

                if (cboDespacho.Items.Count > 0)
                    cboDespacho.SelectedIndex = 0;
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
            try
            {
                objAgendaWCFClient = new AgendaWCFClient();
                objAgendaCliente = new VBG01134Result();

                objAgendaCliente = objAgendaWCFClient.Agenda_BuscarCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, 
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda, ref lineaCredito);
                
                txtCliente.Text = objAgendaCliente.Nombre;
                txtRUC.Text = objAgendaCliente.RUC;
                txtTEA.Text = objAgendaCliente.TEA.ToString();
                txtDiasCredito.Text = objAgendaCliente.DiasCredito.ToString();
                cboMoneda.SelectedValue = objAgendaCliente.ID_MonedaCompra.ToString();
                ViewState["LineaCredito"] = lineaCredito;
                if (lineaCredito <= 0)
                {
                    lblLineaCredito.Text = "Linea de crédito insuficiente $." + Math.Round(Convert.ToDouble(lineaCredito.ToString()), 3).ToString();
                    lblLineaCredito.CssClass = "mensajeError";
                }
                else {
                    lblLineaCredito.Text = "Linea de crédito disponible $." + Math.Round(Convert.ToDouble(lineaCredito.ToString()), 3).ToString();
                    lblLineaCredito.CssClass = "mensajeExito";
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
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString);
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
                ItemWCFClient objItemWCF = new ItemWCFClient();
                gsItem_ListarProductoResult[] lst = objItemWCF.Item_ListarProducto(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString);
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
                    if ((Request.QueryString["idPedido"]) == "0")
                    {
                        Title = "Registrar un pedido";
                        lblMensaje.Text = "Listo para registrar un pedido.";

                        dpFechaEmision.SelectedDate = DateTime.Now;
                        dpFechaVencimiento.SelectedDate = DateTime.Now.AddDays(180);
                        Variante_Cargar();
                    }
                    else
                    {
                        Title = "Modificar un pedido";
                        Pedido_Cargar(int.Parse((Request.QueryString["idPedido"])));
                        lblMensaje.Text = "Listo para modificar el pedido " + (Request.QueryString["idPedido"]);
                    }

                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
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
                txtDiasCredito.Text = cboTipoCredito.SelectedValue.Split(',')[1];
                dpFechaEmision.SelectedDate = DateTime.Now;
                dpFechaVencimiento.SelectedDate = DateTime.Now.AddDays(Int32.Parse(txtDiasCredito.Text));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            if (!ConnectionHelpers.CheckForInternetConnection())
                throw new ArgumentException("ERROR: Revisar su conexión a internet.");

            try
            {
                if (!string.IsNullOrEmpty(lblCodigoCliente.Value) && !string.IsNullOrEmpty(acbProducto.Text.Split('-')[0]))
                {
                    Item_Buscar(acbProducto.Text.Split('-')[0], txtRUC.Text);
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowInsertForm(" + acbProducto.Text.Split('-')[0] + ",'" + 
                        lblCodigoCliente.Value + "',1,"+cboAlmacen.SelectedValue+");", true);
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
                lblMensaje.Text = ex.Message;
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

                    Calcular_Glosa();

                    grdItem.DataSource = ((List<gsItem_BuscarResult>)Session["lstProductos"]).FindAll(x => x.Estado == 1).OrderBy(x => x.Item);
                    grdItem.DataBind();

                    lblMensaje.Text = "Se eliminó el producto del pedido con código " + e.CommandArgument.ToString() + " del pedido.";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
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
                    //Calcular_Glosa();

                    lblMensaje.Text = "Se agregó el producto al pedido.";
                    lblMensaje.CssClass = "mensajeExito";

                    acbProducto.Entries.Clear();
                    acbProducto.Focus();
                }

                if (e.Argument.Split('(')[0].Trim() == "RebindAndNavigate")
                {
                    //grdItem.MasterTableView.SortExpressions.Clear();
                    //grdItem.MasterTableView.GroupByExpressions.Clear();
                    //grdItem.MasterTableView.CurrentPageIndex = grdItem.MasterTableView.PageCount - 1;
                    //grdItem.Rebind();
                    grdItem.MasterTableView.SortExpressions.Clear();
                    grdItem.MasterTableView.GroupByExpressions.Clear();
                    grdItem.DataSource = ((List<gsItem_BuscarResult>)Session["lstProductos"]).FindAll(x => x.Estado == 1).OrderBy(x => x.Item);
                    grdItem.DataBind();

                    Calcular_Glosa();

                    lblMensaje.Text = "Se agregó el producto con nro. kardex " + e.Argument.Split('(')[1].Trim().Split(')')[0] + " al pedido.";
                    lblMensaje.CssClass = "mensajeExito";

                    acbProducto.Entries.Clear();
                    acbProducto.Focus();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            if (!ConnectionHelpers.CheckForInternetConnection())
                throw new ArgumentException("ERROR: Revisar su conexión a internet.");

            PedidoWCFClient objPedidoWCF = new PedidoWCFClient(); ;
            AgendaWCFClient objAgendaWCF = new AgendaWCFClient();
            Agenda_LimiteCreditoBE objLimiteCreditoBE;
            try
            {
                if (string.IsNullOrEmpty(acbVendedor.Text))
                {
                    stripPedido.Tabs[0].Selected = true;
                    pagesPedido.PageViews[0].Selected = true;
                    acbVendedor.Focus();
                    throw new ArgumentException("ERROR: No se pudo guardar el pedido porque debe seleccionar un vendedor.");
                }

                if (grdItem.Items.Count <= 0)
                {
                    stripPedido.Tabs[1].Selected = true;
                    pagesPedido.PageViews[1].Selected = true;
                    acbProducto.Focus();
                    throw new ArgumentException("ERROR: No se pudo guardar el pedido porque debe ingresar por lo menos un producto.");
                }

                int? idPedido = null;
                if ((Request.QueryString["idPedido"]) != "0")
                    idPedido = int.Parse((Request.QueryString["idPedido"]));
                //objLimiteCreditoBE = objAgendaWCF.Agenda_LineaCredito(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                //    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, lblCodigoCliente.Value,  decimal.Parse(cboMoneda.SelectedValue));
                List<GlosaBE> lstGlosa = JsonHelper.JsonDeserialize<List<GlosaBE>>((string)ViewState["lstGlosa"]);
                //if (objLimiteCreditoBE.CreditoDisponibleMonedaSol < lstGlosa.Find(x => x.Descripcion == "Total").Importe)
                //    throw new ArgumentException("Limite de crédito insuficiente para guardar este pedido. La linea de credito actual es de "
                //        + objLimiteCreditoBE.CreditoDisponibleMonedaSol + " " + objLimiteCreditoBE.MonedaNombre);

                objPedidoWCF.Pedido_Registrar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                    PedidoCabecera_Obtener(), PedidoDetalle_Obtener().ToArray(), Impuesto_Obtener().ToArray(),
                    idPedido, ((Usuario_LoginResult)Session["Usuario"]).password, (decimal)ViewState["LineaCredito"]);
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind();", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
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

                Direccion_Cargar(lblCodigoCliente.Value);
                Sucursal_Cargar(lblCodigoCliente.Value);
                Credito_Cargar(lblCodigoCliente.Value);
                Cliente_Buscar(lblCodigoCliente.Value);

                if (!string.IsNullOrEmpty(cboSucursal.SelectedValue))
                    Referencia_Cargar(lblCodigoCliente.Value, Int32.Parse(cboSucursal.SelectedValue));

                acbCliente.Entries.Clear();
                acbVendedor.Focus();

                lblMensaje.Text = "Los datos del cliente fueron cargados.";
                lblMensaje.CssClass = "mensajeExito";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
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
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        #endregion
    }
}