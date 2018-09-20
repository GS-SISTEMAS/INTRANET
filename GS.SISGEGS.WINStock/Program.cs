using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS.SISGEGS.WINStock.OrdenVentaWCF;
using GS.SISGEGS.WINStock.AgendaWCF;
using GS.SISGEGS.WINStock.ImpuestoWCF;
using GS.SISGEGS.WINStock.ItemWCF;


namespace GS.SISGEGS.WINStock
{
    class Program
    {
        static void Main(string[] args)
        {
            OrdenVentaWCFClient objOrdenVentaWCF = new OrdenVentaWCFClient();
            AgendaWCFClient objAgendaWCF = new AgendaWCFClient();
            int idEmpresa;
            int codUsuario;
            string idOrdenVenta;
            DateTime fechaInicial;
            DateTime fechaFinal;
            int contar = 0 ;

            for (int x = 1; x <= 3; x++)
            {
                contar = 0;
                idEmpresa = x; 
                if(x == 3)
                {
                    idEmpresa = 6;
                }

                Console.WriteLine("Iniciando Empresa: " + x.ToString());

                codUsuario = 1;
                //idOrdenVenta = 20120.ToString();
                fechaFinal = DateTime.Now;
                fechaInicial = fechaFinal.AddDays(-10);

                List<gsOV_Listar_SectoristaResult> ListaPedidos = objOrdenVentaWCF.OrdenVenta_Listar_Sectorista(idEmpresa, codUsuario, null, fechaInicial, 
                                                                  fechaFinal, null, true, null,0,99
                                                                  ).ToList().FindAll(y=> y.Aprobacion1 == false && y.Aprobacion2 == false &&  y.FormaPago != "Contado");

                Console.WriteLine("Total de Op: " + ListaPedidos.Count().ToString());
                foreach (gsOV_Listar_SectoristaResult pedido in ListaPedidos)
                {
                    //if (pedido.Op == 39190)
                    //{
                        contar = contar + 1; 
                        idOrdenVenta = pedido.Op.ToString();
                    try
                    {
                        Pedido_Cargar(idOrdenVenta, idEmpresa, codUsuario);
                        Console.WriteLine("Actualizando Op " + contar.ToString() + ": " + idOrdenVenta.ToString());
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Error Op " + contar.ToString() + ": " + idOrdenVenta.ToString() + " " + ex.Message.ToString());
                    }

                    //}

                }
            }
        }

        static void Pedido_Cargar(string idOrdenVenta, int idEmpresa, int codigoUsuario)
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
            AgendaWCFClient objAgendaWCFClient;
            AgendaWCF.VBG01134Result objAgendaCliente;
            decimal? lineaCredito = null;
            DateTime? fechaVecimiento = null;
            string strLETRAS = "";
            decimal? TC = 0; 

            try
            {
                objAgendaWCFClient = new AgendaWCFClient();
                objAgendaCliente = new AgendaWCF.VBG01134Result();

                objOrdenVentaCab = objOrdenVentaWCF.OrdenVenta_Buscar(idEmpresa, codigoUsuario, int.Parse(idOrdenVenta),
                    ref objOrdenVentaDet, ref lstImpuestos, ref bloqueado, ref mensajeBloqueo);

                objAgendaCliente = objAgendaWCFClient.Agenda_BuscarCliente(idEmpresa, codigoUsuario, objOrdenVentaCab.ID_Agenda, ref lineaCredito, ref fechaVecimiento, ref TC);
                List<GlosaBE> Impuesto_Obtener_R = new List<GlosaBE>();


                gsPedidos_FechasLetrasSelectResult[] lstFechas = objOrdenVentaWCF.PedidoLetras_Detalle(idEmpresa, codigoUsuario, 0, int.Parse(idOrdenVenta));
                strLETRAS = PedidosFechas_Letras(lstFechas.ToList(), (DateTime)objOrdenVentaCab.FechaEmision);



                //-----------------------------------------------------------

                gsOV_BuscarCabeceraResult cabecera = OrdenVenta_ObtenerCabecera(objOrdenVentaCab, idOrdenVenta, lstImpuestos, ref Impuesto_Obtener_R);
                gsOV_BuscarDetalleResult[] detalle = OrdenVenta_ObtenerDetalle(objOrdenVentaCab, objOrdenVentaDet, idEmpresa, codigoUsuario).ToArray();


                int KardexFlete = 0;
                List<gsOV_BuscarDetalleResult> kardex = new List<gsOV_BuscarDetalleResult>();
                gsOV_BuscarDetalleResult objetoFlete = new gsOV_BuscarDetalleResult();
                kardex = detalle.ToList().FindAll(x => x.Item.Contains("Flete") && x.Estado == 1).ToList();

                if(kardex.Count > 0 )
                {
                    KardexFlete = Convert.ToInt32(kardex[0].Item_ID);
                    //-----------------------------------------------------------
                    if (KardexFlete > 0)
                    {
                        objetoFlete = detalle.ToList().Find(x => x.Item_ID == KardexFlete && x.Estado == 1);
                    }
                }
 
                // ----------------------------------------------------------
                objOrdenVentaWCF.OrdenVenta_Registrar(idEmpresa, 1,
                                                      cabecera,
                                                      detalle,
                                                      Impuesto_Obtener_R.ToArray(), decimal.Parse(idOrdenVenta), (decimal)lineaCredito, 
                                                      (DateTime)fechaVecimiento, lstFechas, strLETRAS, KardexFlete, objetoFlete);

                //objOrdenVentaWCF.OrdenVenta_Registrar(codEmpresa, codUsuario, cabecera, detalle, impuesto, idPedido, LineaCredito,
                //                                       dtFechaVencimiento, fechas, PlanLetras, KardexFlete, objetoFlete);

                //string pedido = "Realizado.";

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex.TargetSite.Name + "No se pudo registrar el pedido en la base de datos.");
            }
        }

        static string PedidosFechas_Letras(List<gsPedidos_FechasLetrasSelectResult> lstFechas, DateTime FechaEmision)
        {
            DateTime FechaPedido = new DateTime();
            DateTime FechaSelect = new DateTime();
            TimeSpan ts;
            string Letras = "";
            DateTime calendarMinDate = new DateTime();

            int diferencia = 0;
            int inicial = 0;
            int final = 0;
            int y = 0;

            calendarMinDate = FechaEmision;
            FechaPedido = calendarMinDate;


            var lstFecha = lstFechas.OrderBy(x => x.Fecha).ToList();
            foreach (var objFecha in lstFecha)
            {
                FechaSelect = Convert.ToDateTime(objFecha.Fecha);
                ts = FechaSelect - FechaPedido;
                diferencia = ts.Days;

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

            if (y == 0)
            {
                Letras = "";
            }

            return Letras;

        }

        static List<gsOV_BuscarDetalleResult> OrdenVenta_ObtenerDetalle(gsOV_BuscarCabeceraResult objOrdenVentaCab_R , 
            gsOV_BuscarDetalleResult[] objOrdenVentaDet, int idEmpresa, int codigoUsuario)
        {
            ImpuestoWCFClient objImpuestoWCF = new ImpuestoWCFClient();
            gsOV_BuscarImpuestoResult[] lstImpuestos = null;
            List<gsOV_BuscarDetalleResult> lstPedidoDet;
            List<gsItem_BuscarResult> lstProductos = new List<gsItem_BuscarResult>(); // = (List<gsItem_BuscarResult>)Session["lstProductos"];
            //List<gsItem_BuscarResult> lstProductosR = new List<gsItem_BuscarResult>();
            gsOV_BuscarDetalleResult objProducto;
            List<gsImpuesto_ListarPorItemResult> lstImpuestoItem = new List<gsImpuesto_ListarPorItemResult>();
            DateTime fecha;

            try
            {
                foreach (gsOV_BuscarDetalleResult objDetalle in objOrdenVentaDet)
                {
                    gsItem_BuscarResult objItem = new gsItem_BuscarResult();

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
                    if (objOrdenVentaCab_R.Aprobacion1)
                        objItem.Stock = objDetalle.Stock + objDetalle.Cantidad;
                    else
                        objItem.Stock = objDetalle.Stock;
                    objItem.FactorUnidadInv = objDetalle.FactorUnidadInv;
                    objItem.UnidadPresentacion = objDetalle.UnidadPresentacion;
                    objItem.ID_Amarre = objDetalle.ID_Amarre;
                    objItem.Estado = 1;
                    objItem.CostoUnitario = objDetalle.CostoUnitario;
                    lstImpuestoItem.AddRange(objImpuestoWCF.Impuesto_ListarPorItem(idEmpresa,codigoUsuario, objDetalle.ID_Item, DateTime.Now));
                    //ViewState["fecha"] = objOrdenVentaCab.FechaOrden;
                    fecha = objOrdenVentaCab_R.FechaOrden;
                    lstProductos.Add(objItem);
                }

                //Session["lstProductos"] = lstProductos;
                //Session["lstImpuestos"] = lstImpuestoItem;


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
                    objProducto.Item = producto.Item; 
                    //objProducto.DctoValor = Math.Round(producto.Descuento * producto.Precio / 100, 2);
                    objProducto.DctoValor = Math.Round(((producto.Descuento / 100)) * producto.Precio, 4);
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

        static gsOV_BuscarCabeceraResult OrdenVenta_ObtenerCabecera(gsOV_BuscarCabeceraResult objOrdenVentaCab_R, string idOrdenVenta,
                                                                    gsOV_BuscarImpuestoResult[] lstImpuestos, ref List<GlosaBE> Impuesto_Obtener_R)
        {
            gsOV_BuscarCabeceraResult objOrdenVentaCab;
            decimal impuesto = 0;

            try
            {
                List<GlosaBE> lstGlosaR = new List<GlosaBE>();

                objOrdenVentaCab = new gsOV_BuscarCabeceraResult();
                objOrdenVentaCab.ID_Agenda = objOrdenVentaCab_R.ID_Agenda; // lblCodigoCliente.Value;
                if (!string.IsNullOrEmpty(objOrdenVentaCab_R.NoRegistro))
                { objOrdenVentaCab.NoRegistro = objOrdenVentaCab_R.NoRegistro; } 
                else
                {   objOrdenVentaCab.NoRegistro = "0";}


                if (idOrdenVenta == "0")
                {
                    objOrdenVentaCab.FechaDespacho = DateTime.Now.Date;
                    objOrdenVentaCab.FechaEntrega = DateTime.Now.Date;
                    objOrdenVentaCab.FechaVigencia = DateTime.Now.Date;
                    objOrdenVentaCab.FechaEmision = DateTime.Now.Date;
                }
                else
                {
                    objOrdenVentaCab.FechaDespacho = objOrdenVentaCab_R.FechaOrden;
                    objOrdenVentaCab.FechaEntrega = objOrdenVentaCab_R.FechaOrden;
                    objOrdenVentaCab.FechaVigencia = objOrdenVentaCab_R.FechaOrden;
                    objOrdenVentaCab.FechaEmision = objOrdenVentaCab_R.FechaOrden;
                }

                // OLD
                //objOrdenVentaCab.FechaOrden = dpFechaEmision.SelectedDate.Value;
                objOrdenVentaCab.FechaOrden = (DateTime)objOrdenVentaCab_R.FechaEmision;
                //objOrdenVentaCab.FechaVencimiento = dpFechaVencimiento.SelectedDate.Value;
                objOrdenVentaCab.FechaVencimiento = (DateTime)objOrdenVentaCab_R.FechaVencimiento;
                //objOrdenVentaCab.ID_Envio = int.Parse(cboTipoEnvio.SelectedValue);
                objOrdenVentaCab.ID_Envio = objOrdenVentaCab_R.ID_Envio;
                objOrdenVentaCab.ID_AgendaAnexoReferencia = null;


                //if (cboReferencia.SelectedValue != "-1")
                //    objOrdenVentaCab.ID_AgendaAnexoReferencia = int.Parse(cboReferencia.SelectedValue);
                //else
                //    objOrdenVentaCab.ID_AgendaAnexoReferencia = null;

                //objOrdenVentaCab.ID_Vendedor = acbVendedor.Text.Split('-')[0].Trim();
                objOrdenVentaCab.ID_Vendedor = objOrdenVentaCab_R.ID_Vendedor;

                objOrdenVentaCab.ID_Moneda = objOrdenVentaCab_R.ID_Moneda;


                //-------------------------- Cargar Glosa--------------------------------

                //lstGlosa = JsonHelper.JsonDeserialize<List<GlosaBE>>((string)ViewState["lstGlosa"]);
                lstGlosaR = Cargar_Glosa();
                lstGlosaR.Find(x => x.Descripcion == "Neto").Importe = objOrdenVentaCab_R.Neto;
                lstGlosaR.Find(x => x.Descripcion == "Descuento").Importe = objOrdenVentaCab_R.Dcto;
                lstGlosaR.Find(x => x.Descripcion == "SubTotal").Importe = objOrdenVentaCab_R.SubTotal;
                lstGlosaR.Find(x => x.Descripcion == "Total").Importe = objOrdenVentaCab_R.Total;

                foreach (gsOV_BuscarImpuestoResult objImpuesto in lstImpuestos)
                {
                    GlosaBE objGlosaBE = new GlosaBE();
                    objGlosaBE.IdGlosa = objImpuesto.ID_Impuesto;
                    objGlosaBE.Descripcion = objImpuesto.Abreviacion;
                    objGlosaBE.Importe = objImpuesto.Importe;
                    objGlosaBE.BaseImponible = objImpuesto.BaseImponible;
                    lstGlosaR.Add(objGlosaBE);
                }

                //grdGlosa.DataSource = lstGlosa.OrderBy(x => x.IdGlosa);
                //grdGlosa.DataBind();
                //ViewState["lstGlosa"] = JsonHelper.JsonSerializer(lstGlosa);
                //-------------------------------------------------------------------


                //List<GlosaBE> lstGlosa = JsonHelper.JsonDeserialize<List<GlosaBE>>((string)ViewState["lstGlosa"]);
                List<GlosaBE> lstGlosa = lstGlosaR;

                objOrdenVentaCab.Neto = lstGlosa.Find(x => x.Descripcion == "Neto").Importe;
                objOrdenVentaCab.Dcto = lstGlosa.Find(x => x.Descripcion == "Descuento").Importe;
                objOrdenVentaCab.SubTotal = lstGlosa.Find(x => x.Descripcion == "SubTotal").Importe;
                objOrdenVentaCab.Total = lstGlosa.Find(x => x.Descripcion == "Total").Importe;

                lstGlosa = (lstGlosaR.FindAll(x => x.Descripcion != "Neto" && x.Descripcion != "Descuento" && x.Descripcion != "SubTotal" && x.Descripcion != "Total"));
                foreach (GlosaBE objGlosaBE in lstGlosa)
                {
                    impuesto = impuesto + objGlosaBE.Importe;
                }

                Impuesto_Obtener_R = Impuesto_Obtener(lstGlosaR);

                objOrdenVentaCab.Impuestos = Math.Round(impuesto, 4);
                //objOrdenVentaCab.Observaciones = txtObservacion.Text;
                objOrdenVentaCab.Observaciones = objOrdenVentaCab_R.Observaciones; 

                //objOrdenVentaCab.Prioridad = cboPrioridad.SelectedIndex;
                //Verificar
                objOrdenVentaCab.Prioridad = objOrdenVentaCab_R.Prioridad;

                objOrdenVentaCab.EntregaParcial = false; //Flag para poder hacer entregas parcialmente
                objOrdenVentaCab.Estado = 376; //Cuenta Corriente en StandBy
                objOrdenVentaCab.Id_Pago = objOrdenVentaCab_R.Id_Pago; 

                //if (cboSucursal.SelectedValue != "-1")
                //    objOrdenVentaCab.ID_AgendaAnexo = int.Parse(cboSucursal.SelectedValue);
                //else
                //    objOrdenVentaCab.ID_AgendaAnexo = null;

                objOrdenVentaCab.ID_AgendaAnexo = objOrdenVentaCab_R.ID_AgendaAnexo;


                //objOrdenVentaCab.TEA = decimal.Parse(txtTEA.Text);
                objOrdenVentaCab.TEA = objOrdenVentaCab_R.TEA; 

                //objOrdenVentaCab.ID_AgendaDireccion = int.Parse(cboFacturacion.SelectedValue);
                objOrdenVentaCab.ID_AgendaDireccion = objOrdenVentaCab_R.ID_AgendaDireccion; 

                //objOrdenVentaCab.ID_AgendaDireccion2 = int.Parse(cboDespacho.SelectedValue);
                objOrdenVentaCab.ID_AgendaDireccion2 = objOrdenVentaCab_R.ID_AgendaDireccion2; 

                objOrdenVentaCab.ModoPago = null; //No estoy seguro
                objOrdenVentaCab.NotasDespacho = null;
                //objOrdenVentaCab.ID_CondicionCredito = int.Parse(cboTipoCredito.SelectedValue.Split(',')[0]);
                objOrdenVentaCab.ID_CondicionCredito = objOrdenVentaCab_R.ID_CondicionCredito;


                //if (string.IsNullOrEmpty(txtOrden.Text))
                //    objOrdenVentaCab.NroOrdenCliente = null;
                //else
                //    objOrdenVentaCab.NroOrdenCliente = txtOrden.Text;
                objOrdenVentaCab.NroOrdenCliente = objOrdenVentaCab_R.NroOrdenCliente;


                objOrdenVentaCab.ID_NaturalezaGastoIngreso = null;
                objOrdenVentaCab.ID_AgendaOrigen = null;
                objOrdenVentaCab.DireccionOrigenSucursal = null;
                objOrdenVentaCab.DireccionOrigenReferencia = null;
                objOrdenVentaCab.DireccionOrigenDireccion = null;
                objOrdenVentaCab.ID_AgendaDestino = null;
                objOrdenVentaCab.DireccionDestinoSucursal = null;
                objOrdenVentaCab.DireccionDestinoReferencia = null;
                objOrdenVentaCab.DireccionDestinoDireccion = null;

                //objOrdenVentaCab.ID_TipoDespacho = int.Parse(cboOpDespacho.SelectedValue);
                objOrdenVentaCab.ID_TipoDespacho = objOrdenVentaCab_R.ID_TipoDespacho; 
                //objOrdenVentaCab.ID_TipoPedido = int.Parse(cboOpTipoPedido.SelectedValue);
                objOrdenVentaCab.ID_TipoPedido = objOrdenVentaCab_R.ID_TipoPedido; 
                //objOrdenVentaCab.ID_DocumentoVenta = int.Parse(cboOpDocVenta.SelectedValue);
                objOrdenVentaCab.ID_DocumentoVenta = objOrdenVentaCab_R.ID_DocumentoVenta; 
                //objOrdenVentaCab.ID_Almacen = int.Parse(cboAlmacen.SelectedValue);
                objOrdenVentaCab.ID_Almacen = objOrdenVentaCab_R.ID_Almacen; 


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

                //if (cboSede.SelectedValue != "-1")
                //    objOrdenVentaCab.ID_Sede = int.Parse(cboSede.SelectedValue);
                //else
                //    objOrdenVentaCab.ID_Sede = null;

                objOrdenVentaCab.ID_Sede = objOrdenVentaCab_R.ID_Sede; 

                objOrdenVentaCab.Contacto = null;
                //if (acbContacto.Entries.Count <= 0)
                //    objOrdenVentaCab.Contacto = null;
                //else
                //    objOrdenVentaCab.Contacto = acbContacto.Entries[0].Text.Split('-')[0];

                //if (acbTransporte.Entries.Count <= 0)
                //{
                //    objOrdenVentaCab.ID_Transportista = null;
                //    objOrdenVentaCab.ID_AgendaDestino = null;
                //}
                //else
                //{
                //    objOrdenVentaCab.ID_Transportista = lblTrans.Text;
                //    objOrdenVentaCab.ID_AgendaDestino = acbTransporte.Entries[0].Text.Split('-')[0];
                //}
                objOrdenVentaCab.ID_Transportista = objOrdenVentaCab_R.ID_Transportista;
                objOrdenVentaCab.ID_AgendaDestino = objOrdenVentaCab_R.ID_AgendaDestino; 


                return objOrdenVentaCab;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static List<GlosaBE> Impuesto_Obtener(List<GlosaBE> lstGlosa_R)
        {
            List<GlosaBE> lstGlosa;
            try
            {
                //lstGlosa = JsonHelper.JsonDeserialize<List<GlosaBE>>((string)ViewState["lstGlosa"]);
                lstGlosa = lstGlosa_R;
                lstGlosa = lstGlosa.FindAll(x => x.Descripcion != "Neto" && x.Descripcion != "Descuento" && x.Descripcion != "SubTotal" && x.Descripcion != "Total");
                return lstGlosa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static List<GlosaBE> Cargar_Glosa()
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

                return lstGlosa; 
                //grdGlosa.DataSource = lstGlosa;

                //grdGlosa.DataBind();
                //ViewState["lstGlosa"] = JsonHelper.JsonSerializer<List<GlosaBE>>(lstGlosa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
