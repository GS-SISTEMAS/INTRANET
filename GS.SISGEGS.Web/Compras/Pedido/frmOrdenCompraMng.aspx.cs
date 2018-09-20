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
using GS.SISGEGS.Web.OrdenCompraWCF;
using GS.SISGEGS.Web.DespachoWCF;
using GS.SISGEGS.Web.MonedaWCF;
using GS.SISGEGS.Web.FormaPagoWCF;
using GS.SISGEGS.Web.ItemWCF;
using GS.SISGEGS.Web.ImpuestoWCF;
using GS.SISGEGS.Web.VarianteWCF;
using GS.SISGEGS.Web.PedidoWCF;
using GS.SISGEGS.Web.LoginWCF;

namespace GS.SISGEGS.Web.Compras.Pedido
{
    public partial class frmOrdenCompraMng : System.Web.UI.Page
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
                //cboAlmacen.SelectedValue = cboAlmacen.Items.FindItem(x => x.Text.ToUpper() == objVariante.parametro1.ToUpper()).Value;
                //cboOpDocCompra.SelectedValue = cboOpDocCompra.Items.FindItem(x => x.Text.ToUpper() == objVariante.parametro2.ToUpper()).Value;
                cboFormaPago.SelectedValue = cboFormaPago.Items.FindItem(x => x.Text.ToUpper() == objVariante.parametro3.ToUpper()).Value;
                //cboSede.SelectedValue = cboSede.Items.FindItem(x => x.Text.ToUpper() == objVariante.parametro4.ToUpper()).Value;
                //lblTrans.Text = objVariante.parametro5;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void Pedido_Cargar(int idPedido)
        //{
        //    OrdenCompraWCFClient objOrdenCompraWCF = new OrdenCompraWCFClient(); ;
        //    gsOV_BuscarCabeceraResult objOrdenCompraCab;
        //    List<GlosaBE> lstGlosa = new List<GlosaBE>();
        //    ImpuestoWCFClient objImpuestoWCF = new ImpuestoWCFClient();
        //    gsOV_BuscarImpuestoResult[] lstImpuestos = null;
        //    List<gsImpuesto_ListarPorItemResult> lstImpuestoItem = new List<gsImpuesto_ListarPorItemResult>();
        //    gsOV_BuscarDetalleResult[] objOrdenCompraDet = null;
        //    List<gsItem_BuscarResult> lstProductos = new List<gsItem_BuscarResult>();
        //    bool? bloqueado = false;
        //    string mensajeBloqueo = null;
        //    try
        //    {
        //        objOrdenCompraCab = objOrdenCompraWCF.OrdenVenta_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
        //            ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idPedido,
        //            ref objOrdenCompraDet, ref lstImpuestos, ref bloqueado, ref mensajeBloqueo);

        //        lblCodigoProveedor.Value = objOrdenCompraCab.ID_Agenda;
        //        Proveedor_Buscar(objOrdenCompraCab.ID_Agenda);
        //        Direccion_Cargar(objOrdenCompraCab.ID_Agenda);
        //        Sucursal_Cargar(objOrdenCompraCab.ID_Agenda);
        //        //Credito_Cargar(objOrdenCompraCab.ID_Agenda, objOrdenCompraCab.Id_Pago);
        //        Proveedor_Buscar(objOrdenCompraCab.ID_Agenda);
        //        Cargar_Glosa();

        //        //if (!string.IsNullOrEmpty(cboSucursal.SelectedValue))
        //        //    Referencia_Cargar(lblCodigoCliente.Value, int.Parse(cboSucursal.SelectedValue));

        //        AutoCompleteBoxEntry objEntry = new AutoCompleteBoxEntry();
        //        objEntry.Text = objOrdenCompraCab.ID_Vendedor.ToString() + "-" + objOrdenCompraCab.Vendedor;
        //        //acbVendedor.Entries.Add(objEntry);
        //        objEntry = new AutoCompleteBoxEntry();
        //        objEntry.Text = objOrdenCompraCab.ID_Agenda.ToString() + "-" + txtProveedor.Text;
        //        acbProveedor.Entries.Add(objEntry);
        //        acbProveedor.Enabled = false;
        //        btnBuscarProveedor.Enabled = false;
        //        cboSucursal.SelectedValue = objOrdenCompraCab.ID_AgendaAnexo.ToString();
        //        cboFacturacion.SelectedValue = objOrdenCompraCab.ID_AgendaDireccion.ToString();
        //        cboDespacho.SelectedValue = objOrdenCompraCab.ID_AgendaDireccion2.ToString();
        //        //cboTipoEnvio.SelectedValue = objOrdenCompraCab.ID_Envio.ToString();
        //        cboPrioridad.SelectedValue = (objOrdenCompraCab.Prioridad + 1).ToString();
        //        cboMoneda.SelectedValue = objOrdenCompraCab.ID_Moneda.ToString();
        //        cboFormaPago.SelectedValue = objOrdenCompraCab.Id_Pago.ToString();
        //        //acbVendedor.Enabled = false;
        //        //txtOrden.Text = objOrdenCompraCab.NroOrdenCliente;
        //        dpFechaEmision.SelectedDate = objOrdenCompraCab.FechaEmision;
        //        dpFechaVencimiento.SelectedDate = objOrdenCompraCab.FechaVencimiento;
        //        //txtModoPago.Value = ((DateTime)objOrdenCompraCab.FechaVencimiento - (DateTime)objOrdenCompraCab.FechaEmision).TotalDays;

        //        //try
        //        //{
        //        //    cboTipoCredito.SelectedValue = cboTipoCredito.FindItem(x => x.Value.Split(',')[0] == objOrdenCompraCab.ID_CondicionCredito.ToString()).Value;

        //        //}
        //        //catch (Exception ex)
        //        //{
        //        //    string mensaje = "No cuenta con tipos de crédito, revisar “Tipo de Compra” o  comunicarse con créditos y cobranzas.";
        //        //    rwmPedidoMng.RadAlert(mensaje, 400, null, "Mensaje de error", null);
        //        //}

        //        txtObservacion.Text = objOrdenCompraCab.Observaciones;

        //        //if (string.IsNullOrEmpty(objOrdenCompraCab.ID_Sede.ToString().Trim()))
        //        //    cboSede.SelectedIndex = 0;
        //        //else
        //        //    cboSede.SelectedValue = objOrdenCompraCab.ID_Sede.ToString();
        //        //cboReferencia.SelectedValue = objOrdenCompraCab.ID_AgendaAnexoReferencia.ToString();
        //        //cboOpDespacho.SelectedValue = objOrdenCompraCab.ID_TipoDespacho.ToString();
        //        //cboOpTipoPedido.SelectedValue = objOrdenCompraCab.ID_TipoPedido.ToString();
        //        //cboOpDocCompra.SelectedValue = objOrdenCompraCab.ID_DocumentoCompra.ToString();
        //        cboAlmacen.SelectedValue = objOrdenCompraCab.ID_Almacen.ToString();
        //        Session["idAlmacen"] = objOrdenCompraCab.ID_Almacen;
        //        cboAlmacen.Enabled = false;
        //        txtNroRegistro.Text = objOrdenCompraCab.NoRegistro;

        //        //if (!string.IsNullOrEmpty(objOrdenCompraCab.ID_AgendaDestino))
        //        //{
        //        //    objEntry = new AutoCompleteBoxEntry();
        //        //    objEntry.Text = objOrdenCompraCab.ID_AgendaDestino.ToString() + "-" + objOrdenCompraCab.Transporte;
        //        //    //acbTransporte.Entries.Add(objEntry);
        //        //}

        //        //if (!string.IsNullOrEmpty(objOrdenCompraCab.ID_ReferenciaAgenda))
        //        //{
        //        //    objEntry = new AutoCompleteBoxEntry();
        //        //    objEntry.Text = objOrdenCompraCab.ID_ReferenciaAgenda.ToString() + "-" + objOrdenCompraCab.Contacto;
        //        //    acbContacto.Entries.Add(objEntry);
        //        //}

        //        lstGlosa = JsonHelper.JsonDeserialize<List<GlosaBE>>((string)ViewState["lstGlosa"]);
        //        lstGlosa.Find(x => x.Descripcion == "Neto").Importe = objOrdenCompraCab.Neto;
        //        lstGlosa.Find(x => x.Descripcion == "Descuento").Importe = objOrdenCompraCab.Dcto;
        //        lstGlosa.Find(x => x.Descripcion == "SubTotal").Importe = objOrdenCompraCab.SubTotal;
        //        lstGlosa.Find(x => x.Descripcion == "Total").Importe = objOrdenCompraCab.Total;

        //        foreach (gsOV_BuscarImpuestoResult objImpuesto in lstImpuestos)
        //        {
        //            GlosaBE objGlosaBE = new GlosaBE();
        //            objGlosaBE.IdGlosa = objImpuesto.ID_Impuesto;
        //            objGlosaBE.Descripcion = objImpuesto.Abreviacion;
        //            objGlosaBE.Importe = objImpuesto.Importe;
        //            objGlosaBE.BaseImponible = objImpuesto.BaseImponible;
        //            lstGlosa.Add(objGlosaBE);
        //        }
        //        grdGlosa.DataSource = lstGlosa.OrderBy(x => x.IdGlosa);
        //        grdGlosa.DataBind();
        //        ViewState["lstGlosa"] = JsonHelper.JsonSerializer(lstGlosa);

        //        foreach (gsOV_BuscarDetalleResult objDetalle in objOrdenCompraDet)
        //        {
        //            gsItem_BuscarResult objItem = new gsItem_BuscarResult();

        //            objItem.Codigo = objDetalle.ID_Item;
        //            objItem.Cantidad = Convert.ToInt32(objDetalle.Cantidad);
        //            objItem.DctoMax = objDetalle.DctoMax;
        //            objItem.Descuento = objDetalle.Dcto;
        //            objItem.ID_Moneda = objDetalle.ID_Moneda;
        //            objItem.ID_UnidadControl = objDetalle.ID_UnidadDoc;
        //            objItem.ID_UnidadInv = objDetalle.ID_UnidadInv;
        //            objItem.Importe = objDetalle.Importe;
        //            objItem.Item = objDetalle.Item;
        //            objItem.Item_ID = objDetalle.Item_ID;
        //            objItem.NombreMoneda = objDetalle.NombreMoneda;
        //            objItem.Observacion = objDetalle.Observaciones;
        //            objItem.Precio = objDetalle.Precio;
        //            objItem.PrecioInicial = objDetalle.PrecioMinimo;
        //            objItem.Signo = objDetalle.Signo;
        //            if (objOrdenCompraCab.Aprobacion1)
        //                objItem.Stock = objDetalle.Stock + objDetalle.Cantidad;
        //            else
        //                objItem.Stock = objDetalle.Stock;
        //            objItem.FactorUnidadInv = objDetalle.FactorUnidadInv;
        //            objItem.UnidadPresentacion = objDetalle.UnidadPresentacion;
        //            objItem.ID_Amarre = objDetalle.ID_Amarre;
        //            objItem.Estado = 1;
        //            objItem.CostoUnitario = objDetalle.CostoUnitario;
        //            lstImpuestoItem.AddRange(objImpuestoWCF.Impuesto_ListarPorItem(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
        //                ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, objDetalle.ID_Item, DateTime.Now));
        //            ViewState["fecha"] = objOrdenCompraCab.FechaOrden;

        //            lstProductos.Add(objItem);
        //        }

        //        Session["lstProductos"] = lstProductos;
        //        Session["lstImpuestos"] = lstImpuestoItem;
        //        grdItem.DataSource = lstProductos;
        //        grdItem.DataBind();

        //        lblRentabilidad.Text = "Rentabilidad: " + Math.Round((double)((lstProductos.Sum(x => x.Precio) - 
        //            lstProductos.Sum(x => x.CostoUnitario)) * 100) / (double)lstProductos.Sum(x => x.Precio), 0).ToString() + "%";

        //        if ((bool)bloqueado)
        //        {
        //            btnGuardar.Enabled = false;
        //            throw new ArgumentException(mensajeBloqueo);
        //        }

        //        if (((Usuario_LoginResult)Session["Usuario"]).aprobarTransfGratuita && objOrdenCompraCab.Id_Pago == 2)
        //            btnAprobar.Visible = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

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

        private List<gsOV_BuscarDetalleResult> OrdenCompra_ObtenerDetalle()
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
                    objProducto.ID_Amarre = producto.ID_Amarre;
                    objProducto.TablaOrigen = "OV";
                    objProducto.ID_Item = producto.Codigo;
                    objProducto.ID_ItemPedido = null;
                    objProducto.Item_ID = producto.Item_ID;
                    objProducto.Cantidad = producto.Cantidad;
                    objProducto.Precio = producto.Precio;
                    objProducto.Dcto = producto.Descuento;
                    objProducto.DctoValor = Math.Round(producto.Descuento * producto.Precio / 100, 2);
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

                    lstPedidoDet.Add(objProducto);
                }
                return lstPedidoDet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private gsOV_BuscarCabeceraResult OrdenCompra_ObtenerCabecera()
        {
            gsOV_BuscarCabeceraResult objOrdenCompraCab;
            decimal impuesto = 0;
            try
            {
                objOrdenCompraCab = new gsOV_BuscarCabeceraResult();
                objOrdenCompraCab.ID_Agenda = lblCodigoProveedor.Value;
                if (!string.IsNullOrEmpty(txtNroRegistro.Text))
                    objOrdenCompraCab.NoRegistro = txtNroRegistro.Text;
                else
                    objOrdenCompraCab.NoRegistro = "0"; ;

                if ((Request.QueryString["idOrdenCompra"]) == "0")
                {
                    objOrdenCompraCab.FechaDespacho = DateTime.Now.Date;
                    objOrdenCompraCab.FechaEntrega = DateTime.Now.Date;
                    objOrdenCompraCab.FechaVigencia = DateTime.Now.Date;
                    objOrdenCompraCab.FechaEmision = DateTime.Now.Date;
                }
                else
                {
                    objOrdenCompraCab.FechaDespacho = (DateTime)ViewState["fecha"];
                    objOrdenCompraCab.FechaEntrega = (DateTime)ViewState["fecha"];
                    objOrdenCompraCab.FechaVigencia = (DateTime)ViewState["fecha"];
                    objOrdenCompraCab.FechaEmision = (DateTime)ViewState["fecha"];
                }

                objOrdenCompraCab.FechaOrden = dpFechaEmision.SelectedDate.Value;
                objOrdenCompraCab.FechaVencimiento = dpFechaVencimiento.SelectedDate.Value;
                //objOrdenCompraCab.ID_Envio = int.Parse(cboTipoEnvio.SelectedValue);
                objOrdenCompraCab.ID_AgendaAnexoReferencia = null;
                //if (cboReferencia.SelectedValue != "-1")
                //    objOrdenCompraCab.ID_AgendaAnexoReferencia = int.Parse(cboReferencia.SelectedValue);
                //else
                //    objOrdenCompraCab.ID_AgendaAnexoReferencia = null;
                //objOrdenCompraCab.ID_Vendedor = acbVendedor.Text.Split('-')[0].Trim();
                objOrdenCompraCab.ID_Moneda = int.Parse(cboMoneda.SelectedValue);
                List<GlosaBE> lstGlosa = JsonHelper.JsonDeserialize<List<GlosaBE>>((string)ViewState["lstGlosa"]);
                objOrdenCompraCab.Neto = lstGlosa.Find(x => x.Descripcion == "Neto").Importe;
                objOrdenCompraCab.Dcto = lstGlosa.Find(x => x.Descripcion == "Descuento").Importe;
                objOrdenCompraCab.SubTotal = lstGlosa.Find(x => x.Descripcion == "SubTotal").Importe;
                objOrdenCompraCab.Total = lstGlosa.Find(x => x.Descripcion == "Total").Importe;
                lstGlosa = (JsonHelper.JsonDeserialize<List<GlosaBE>>((string)ViewState["lstGlosa"])).FindAll(x => x.Descripcion != "Neto" && x.Descripcion != "Descuento" && x.Descripcion != "SubTotal" && x.Descripcion != "Total");
                foreach (GlosaBE objGlosaBE in lstGlosa)
                {
                    impuesto = impuesto + objGlosaBE.Importe;
                }
                objOrdenCompraCab.Impuestos = Math.Round(impuesto, 2);
                objOrdenCompraCab.Observaciones = txtObservacion.Text;
                objOrdenCompraCab.Prioridad = cboPrioridad.SelectedIndex;
                objOrdenCompraCab.EntregaParcial = false; //Flag para poder hacer entregas parcialmente
                objOrdenCompraCab.Estado = 376; //Cuenta Corriente en StandBy
                objOrdenCompraCab.Id_Pago = int.Parse(cboFormaPago.SelectedValue);
                if (cboSucursal.SelectedValue != "-1")
                    objOrdenCompraCab.ID_AgendaAnexo = int.Parse(cboSucursal.SelectedValue);
                else
                    objOrdenCompraCab.ID_AgendaAnexo = null;
                objOrdenCompraCab.TEA = decimal.Parse(txtTEA.Text);
                objOrdenCompraCab.ID_AgendaDireccion = int.Parse(cboFacturacion.SelectedValue);
                objOrdenCompraCab.ID_AgendaDireccion2 = int.Parse(cboDespacho.SelectedValue);
                objOrdenCompraCab.ModoPago = null; //No estoy seguro
                objOrdenCompraCab.NotasDespacho = null;

                //objOrdenCompraCab.ID_CondicionCredito = int.Parse(cboTipoCredito.SelectedValue.Split(',')[0]);

                //if (string.IsNullOrEmpty(txtOrden.Text))
                //    objOrdenCompraCab.NroOrdenCliente = null;
                //else
                //    objOrdenCompraCab.NroOrdenCliente = txtOrden.Text; 

                objOrdenCompraCab.ID_NaturalezaGastoIngreso = null;
                objOrdenCompraCab.ID_AgendaOrigen = null;
                objOrdenCompraCab.DireccionOrigenSucursal = null;
                objOrdenCompraCab.DireccionOrigenReferencia = null;
                objOrdenCompraCab.DireccionOrigenDireccion = null;
                objOrdenCompraCab.ID_AgendaDestino = null;
                objOrdenCompraCab.DireccionDestinoSucursal = null;
                objOrdenCompraCab.DireccionDestinoReferencia = null;
                objOrdenCompraCab.DireccionDestinoDireccion = null;
                //objOrdenCompraCab.ID_TipoDespacho = int.Parse(cboOpDespacho.SelectedValue);
                //objOrdenCompraCab.ID_TipoPedido = int.Parse(cboOpTipoPedido.SelectedValue);
                //objOrdenCompraCab.ID_DocumentoCompra = int.Parse(cboOpDocCompra.SelectedValue);
                objOrdenCompraCab.ID_Almacen = int.Parse(cboAlmacen.SelectedValue);
                objOrdenCompraCab.ID_Transportista = null;
                objOrdenCompraCab.ID_Chofer = null;
                objOrdenCompraCab.ID_Vehiculo1 = null;
                objOrdenCompraCab.ID_Vehiculo2 = null;
                objOrdenCompraCab.ID_Vehiculo3 = null;
                objOrdenCompraCab.HoraAtencionOpcion1_Desde = 0;
                objOrdenCompraCab.HoraAtencionOpcion1_Hasta = 0;
                objOrdenCompraCab.HoraAtencionOpcion2_Desde = 0;
                objOrdenCompraCab.HoraAtencionOpcion2_Hasta = 0;
                objOrdenCompraCab.HoraAtencionOpcion3_Desde = 0;
                objOrdenCompraCab.HoraAtencionOpcion3_Hasta = 0;
                //if (cboSede.SelectedValue != "-1")
                //    objOrdenCompraCab.ID_Sede = int.Parse(cboSede.SelectedValue);
                //else
                //    objOrdenCompraCab.ID_Sede = null;

                objOrdenCompraCab.Contacto = null;
                //if (acbContacto.Entries.Count <= 0)
                //    objOrdenCompraCab.Contacto = null;
                //else
                //    objOrdenCompraCab.Contacto = acbContacto.Entries[0].Text.Split('-')[0];

                //if (acbTransporte.Entries.Count <= 0)
                //{
                //    objOrdenCompraCab.ID_Transportista = null;
                //    objOrdenCompraCab.ID_AgendaDestino = null;
                //}
                //else
                //{
                //    objOrdenCompraCab.ID_Transportista = lblTrans.Text;
                //    objOrdenCompraCab.ID_AgendaDestino = acbTransporte.Entries[0].Text.Split('-')[0];
                //}

                return objOrdenCompraCab;
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
                lstGlosa.Find(x => x.Descripcion == "Neto").Importe = Math.Round(neto, 2);
                lstGlosa.Find(x => x.Descripcion == "Descuento").Importe = Math.Round(descuento, 2);
                lstGlosa.Find(x => x.Descripcion == "SubTotal").Importe = Math.Round(subtotal, 2);

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
                        objGlosaBE.Importe = Math.Round(((decimal)objImpuesto.Valor / 100) * (objProducto.Importe * (1 - (objProducto.Descuento / 100))), 2);
                    }
                    else
                    {
                        objGlosaBE = lstGlosa.Find(x => x.IdGlosa == objImpuesto.ID_Impuesto);
                        lstGlosa.Remove(objGlosaBE);
                        if (objImpuesto.Valor > 0)
                            objGlosaBE.BaseImponible = objGlosaBE.BaseImponible + objProducto.Importe;
                        objGlosaBE.Importe = Math.Round(objGlosaBE.Importe + ((decimal)objImpuesto.Valor / 100) * (objProducto.Importe * (1 - (objProducto.Descuento / 100))), 2);
                    }
                    lstGlosa.Add(objGlosaBE);
                }

                lstGlosaImpuestos.Clear();
                lstGlosaImpuestos = lstGlosa.FindAll(x => x.Descripcion != "Neto" && x.Descripcion != "Descuento" && x.Descripcion != "SubTotal" && x.Descripcion != "Total").ToList();
                foreach (GlosaBE objGlosaImpuestos in lstGlosaImpuestos)
                {
                    impuestos = impuestos + objGlosaImpuestos.Importe;
                }
                lstGlosa.Find(x => x.Descripcion == "Total").Importe = Math.Round(subtotal + impuestos, 2);

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

        //private void Referencia_Cargar(string idAgenda, int idSucursal)
        //{
        //    AgendaWCFClient objAgendaWCF;
        //    VBG02699Result objReferencia;
        //    List<VBG02699Result> lstReferencias;
        //    try
        //    {
        //        objAgendaWCF = new AgendaWCFClient();
        //        objReferencia = new VBG02699Result();
        //        objReferencia.ID = -1;
        //        objReferencia.Nombre = "Ninguno";

        //        lstReferencias = objAgendaWCF.AgendaAnexoReferencia_ListarPorSucursal(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
        //            ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idSucursal, idAgenda).ToList();
        //        lstReferencias.Insert(0, objReferencia);

        //        cboReferencia.DataSource = lstReferencias;
        //        cboReferencia.DataTextField = "Nombre";
        //        cboReferencia.DataValueField = "ID";
        //        cboReferencia.DataBind();

        //        if (cboReferencia.Items.Count > 0)
        //            cboReferencia.SelectedIndex = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

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

        //private void Pedido_CargarTipo()
        //{
        //    PedidoWCFClient objPedidoWCF;
        //    try
        //    {
        //        objPedidoWCF = new PedidoWCFClient();
        //        cboOpTipoPedido.DataSource = objPedidoWCF.Pedido_ListarTipo(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
        //            ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario);
        //        cboOpTipoPedido.DataTextField = "Nombre";
        //        cboOpTipoPedido.DataValueField = "ID";
        //        cboOpTipoPedido.DataBind();

        //        if (cboOpTipoPedido.Items.Count > 0)
        //            cboOpTipoPedido.SelectedIndex = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void Documento_CagarTipoCompra()
        //{
        //    DocumentoWCFClient objDocumentoWCF;
        //    try
        //    {
        //        objDocumentoWCF = new DocumentoWCFClient();
        //        cboOpDocCompra.DataSource = objDocumentoWCF.Documento_ListarDocCompra(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
        //            ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario);
        //        cboOpDocCompra.DataTextField = "Nombre";
        //        cboOpDocCompra.DataValueField = "ID";
        //        cboOpDocCompra.DataBind();

        //        if (cboOpDocCompra.Items.Count > 0)
        //            cboOpDocCompra.SelectedIndex = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private void Almacen_Cargar()
        {
            AgendaWCFClient objAgendaWCF;
            VBG00746Result objAlamcen = new VBG00746Result(); 
            try
            {
                objAgendaWCF = new AgendaWCFClient();
                List<VBG00746Result> lstAlamcenes = new List<VBG00746Result>();

                lstAlamcenes = objAgendaWCF.AgendaAnexo_ListarAlmacen(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario).ToList();

                objAlamcen.AlmacenAnexo = "Todos";
                objAlamcen.ID_AlmacenAnexo = 0;

                lstAlamcenes.Insert(0, objAlamcen);

                cboAlmacen.DataSource = lstAlamcenes; 
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

        //private void Credito_Cargar(string idAgenda, int ID_Pago)
        //{
        //    CreditoWCFClient objCreditoWCF;
        //    try
        //    {
        //        objCreditoWCF = new CreditoWCFClient();
        //        List < gsCredito_ListarCondicionResult > lst = objCreditoWCF.Credito_ListarCondicion(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
        //            ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda).ToList().FindAll(x => x.ID_Pago == ID_Pago);

        //        var datasource = from x in lst
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

                if (cboDespacho.Items.Count > 0)
                    cboDespacho.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Proveedor_Buscar(string idAgenda)
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

                txtProveedor.Text = objAgendaCliente.Nombre;

                if (!string.IsNullOrEmpty(objAgendaCliente.RUC))
                    txtRUC.Text = objAgendaCliente.RUC;
                else
                {
                    txtRUC.Text = idAgenda;
                    txtRUC.Label = "RUC";
                }

                txtTEA.Text = objAgendaCliente.TEA.ToString();
                //txtModoPagoSTR.Text = objAgendaCliente.DiasCredito.ToString();
                cboMoneda.SelectedValue = objAgendaCliente.ID_MonedaCompra.ToString();

                ViewState["LineaCredito"] = lineaCredito;
                ViewState["FechaVencimiento"] = fechaVecimiento;

                //if (lineaCredito <= 0)
                //{
                //    lblLineaCredito.Text = "Linea de crédito insuficiente $." + Math.Round(Convert.ToDouble(lineaCredito.ToString()), 3).ToString();
                //    lblLineaCredito.CssClass = "mensajeError";
                //}
                //else
                //{
                //    lblLineaCredito.Text = "Linea de crédito disponible $." + Math.Round(Convert.ToDouble(lineaCredito.ToString()), 3).ToString();
                //    lblLineaCredito.CssClass = "mensajeExito";
                //}

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
        public static AutoCompleteBoxData Agenda_BuscarProveedor(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarProveedorResult[] lst = objAgendaWCFClient.Agenda_ListarProveedor(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsAgenda_ListarProveedorResult agenda in lst)
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
            int idCategoria; 

            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (!string.IsNullOrEmpty(searchString) && searchString.Length >= 4)
            {

                idCategoria = Convert.ToInt32(HttpContext.Current.Session["idCategoria"].ToString());

                    ItemWCFClient objItemWCF = new ItemWCFClient();
                List<VBG00321Result> lst = objItemWCF.Item_Listar_ProductosCompras(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString, idCategoria).ToList();


                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (VBG00321Result producto in lst)
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
                    LoginWCFClient objLoginWCF = new LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)Session["Usuario"]).idUsuario);

                    Session["idAlmacen"] = null;
                    if (Session["lstProductos"] != null)
                    {
                        Session["lstProductos"] = null;
                        Session["lstImpuestos"] = null;
                    }

                    Cargar_Glosa();
                    //Sede_Cagar();
                    //Envio_Cargar();
                    Almacen_Cargar();
                    //Documento_CagarTipoCompra();
                    //Pedido_CargarTipo();
                    //Despacho_CargarTipo();
                    Moneda_Cargar();
                    FormaPago_Cargar();

                    cboCategoria.SelectedValue = "0";
                    Session["idCategoria"] = cboCategoria.SelectedValue;

                    if ((Request.QueryString["idOrdenCompra"]) == "0")
                    {
                        Title = "Registrar una pedido";
                        lblMensaje.Text = "Listo para registrar una orden.";

                        dpFechaEmision.SelectedDate = DateTime.Now;
                        dpFechaEntrega.SelectedDate = DateTime.Now;
                        dpFechaVencimiento.SelectedDate = DateTime.Now.AddDays(180);
                        Variante_Cargar();
                    }
                    else
                    {
                        Title = "Modificar una orden";
                        //Pedido_Cargar(int.Parse((Request.QueryString["idOrdenCompra"])));
                        lblMensaje.Text = "Listo para modificar la orden " + (Request.QueryString["idOrdenCompra"]);
                    }

                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                rwmPedidoMng.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

        protected void cboCategoria_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            if (!ConnectionHelpers.CheckForInternetConnection())
                throw new ArgumentException("ERROR: Revisar su conexión a internet.");

            try
            {
                Session["idCategoria"] = cboCategoria.SelectedValue;
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
                if (!string.IsNullOrEmpty(lblCodigoProveedor.Value) && !string.IsNullOrEmpty(acbProducto.Text.Split('-')[0]))
                {
                    Item_Buscar(acbProducto.Text.Split('-')[0], txtRUC.Text);
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowInsertForm('" + acbProducto.Text.Split('-')[0] + "','" +
                        lblCodigoProveedor.Value + "',1," + 0 + "," + cboMoneda.SelectedValue + ");", true);
                    acbProducto.Entries.Clear();
                }
                else
                {
                    if (string.IsNullOrEmpty(lblCodigoProveedor.Value))
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
                    grdItem.DataSource = ((List<gsItem_BuscarResult>)Session["lstProductos"]).FindAll(x => x.Estado == 1);
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
                rwmPedidoMng.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            OrdenCompraWCFClient objOrdenCompraWCF = new OrdenCompraWCFClient(); ;
            try
            {
                //if(cboTipoCredito.Items.Count == 0  || cboTipoCredito.SelectedItem.Text.Trim() == "" || string.IsNullOrEmpty(cboTipoCredito.SelectedItem.Text.Trim()))
                //{
                    stripPedido.Tabs[0].Selected = true;
                    pagesPedido.PageViews[0].Selected = true;
                    //cboTipoCredito.Focus();
                    throw new ArgumentException("No cuenta con tipos de crédito, revisar “Tipo de Compra” o  comunicarse con créditos y cobranzas.");
                //}

                //if (string.IsNullOrEmpty(acbTransporte.Text))
                //{
                //    stripPedido.Tabs[0].Selected = true;
                //    pagesPedido.PageViews[0].Selected = true;
                //    acbTransporte.Focus();
                //    throw new ArgumentException("No se pudo guardar el pedido porque debe  seleccionar una empresa de transporte.");
                //}

                //if (string.IsNullOrEmpty(acbVendedor.Text))
                //{
                //    stripPedido.Tabs[0].Selected = true;
                //    pagesPedido.PageViews[0].Selected = true;
                //    acbVendedor.Focus();
                //    throw new ArgumentException("No se pudo guardar el pedido porque debe seleccionar un vendedor.");
                //}

                if (grdItem.Items.Count <= 0)
                {
                    stripPedido.Tabs[1].Selected = true;
                    pagesPedido.PageViews[1].Selected = true;
                    acbProducto.Focus();
                    throw new ArgumentException("No se pudo guardar el pedido porque debe ingresar por lo menos un producto.");
                }

                int? idPedido = null;
                if ((Request.QueryString["idOrdenCompra"]) != "0")
                    idPedido = int.Parse((Request.QueryString["idOrdenCompra"]));
                List<GlosaBE> lstGlosa = JsonHelper.JsonDeserialize<List<GlosaBE>>((string)ViewState["lstGlosa"]);


                //objOrdenCompraWCF.OrdenVenta_Registrar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                //    OrdenCompra_ObtenerCabecera(), OrdenCompra_ObtenerDetalle().ToArray(), Impuesto_Obtener().ToArray(), idPedido, (decimal)ViewState["LineaCredito"],
                //    (DateTime)ViewState["FechaVencimiento"]);



                if ((Request.QueryString["idOrdenCompra"]) == "0")
                    Response.Redirect("~/Comercial/Pedido/frmOrdenCompra.aspx");
                else
                    Response.Redirect("~/Comercial/Pedido/frmOrdenCompra.aspx?fechaInicial=" + Request.QueryString["fechaInicial"] +
                        "&fechafinal=" + Request.QueryString["fechafinal"]);
            }
            catch (Exception ex)
            {
                rwmPedidoMng.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

        protected void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (acbProveedor.Entries.Count <= 0 || acbProveedor.Entries[0].Text.Length <= 0)
                    throw new ArgumentException("Debe seleccionar un cliente valido");

                lblCodigoProveedor.Value = acbProveedor.Text.Split('-')[0];

                Direccion_Cargar(lblCodigoProveedor.Value);
                Sucursal_Cargar(lblCodigoProveedor.Value);
                //Credito_Cargar(lblCodigoProveedor.Value, int.Parse(cboFormaPago.SelectedValue));
                Proveedor_Buscar(lblCodigoProveedor.Value);

                //if (!string.IsNullOrEmpty(cboSucursal.SelectedValue))
                //    Referencia_Cargar(lblCodigoCliente.Value, Int32.Parse(cboSucursal.SelectedValue));

                acbProveedor.Entries.Clear();
                acbProveedor.Enabled = false;
                cboAlmacen.Enabled = false;
                btnBuscarProveedor.Enabled = false;
                acbProveedor.Focus();
                Session["idAlmacen"] = cboAlmacen.SelectedValue;

                lblMensaje.Text = "Los datos del cliente fueron cargados.";
                lblMensaje.CssClass = "mensajeExito";
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
                //Credito_Cargar(lblCodigoCliente.Value, int.Parse(cboFormaPago.SelectedValue));
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
                    Response.Redirect("~/Comercial/Pedido/frmOrdenCompra.aspx");
                else
                    Response.Redirect("~/Comercial/Pedido/frmOrdenCompra.aspx?fechaInicial=" + Request.QueryString["fechaInicial"] +
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
                    Response.Redirect("~/Comercial/Pedido/frmOrdenCompra.aspx");
                else
                    Response.Redirect("~/Comercial/Pedido/frmOrdenCompra.aspx?fechaInicial=" + Request.QueryString["fechaInicial"] +
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
                //OrdenCompraWCFClient objOrdenCompraWCF = new OrdenCompraWCFClient();
                //objOrdenCompraWCF.OV_TransGratuitas_Aprobar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                //    int.Parse(Request.QueryString["idOrdenCompra"]), ref msjError);

                //if (string.IsNullOrEmpty(Request.QueryString["fechaInicial"]))
                //    Response.Redirect("~/Comercial/Pedido/frmOrdenCompra.aspx");
                //else
                //    Response.Redirect("~/Comercial/Pedido/frmOrdenCompra.aspx?fechaInicial=" + Request.QueryString["fechaInicial"] +
                //        "&fechafinal=" + Request.QueryString["fechafinal"]);
            }
            catch (Exception ex) {
                rwmPedidoMng.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }
    }
}