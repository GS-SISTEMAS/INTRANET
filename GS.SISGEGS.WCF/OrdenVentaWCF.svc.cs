using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GS.SISGEGS.DM;
using GS.SISGEGS.BL;
using GS.SISGEGS.BE;
using System.Data;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "OrdenVentaWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione OrdenVentaWCF.svc o OrdenVentaWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class OrdenVentaWCF : IOrdenVentaWCF
    {
        public gsOV_BuscarCabeceraResult OrdenVenta_Buscar(int idEmpresa, int codigoUsuario, int idPedido,
            ref List<gsOV_BuscarDetalleResult> objOrdenVentaDet, ref List<gsOV_BuscarImpuestoResult> objOrdenVentaImp,
            ref bool? bloqueado, ref string mensajeBloqueado)
        {
            try
            {
                OrdenVentaBL objOrdenVentaBL = new OrdenVentaBL();
                return objOrdenVentaBL.OrdenVenta_Buscar(idEmpresa, codigoUsuario, idPedido, ref objOrdenVentaDet, ref objOrdenVentaImp,
                    ref bloqueado, ref mensajeBloqueado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void OrdenVenta_Eliminar(int idEmpresa, int codigoUsuario, int idOperacion, string Comentario)
        {
            try
            {
                OrdenVentaBL objOrdenVentaBL = new OrdenVentaBL();
                objOrdenVentaBL.OrdenVenta_Eliminar(idEmpresa, codigoUsuario, idOperacion, Comentario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsOV_ListarResult> OrdenVenta_Listar(int idEmpresa, int codigoUsuario, string ID_Agenda,
            DateTime fechaDesde, DateTime fechaHasta, string ID_Vendedor, bool modificarPedido)
        {
            try {
                OrdenVentaBL objOrdenVentaBL = new OrdenVentaBL();
                return objOrdenVentaBL.OrdenVenta_Listar(idEmpresa, codigoUsuario, ID_Agenda, fechaDesde,
                    fechaHasta, ID_Vendedor, modificarPedido);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public void OrdenVenta_Registrar(int idEmpresa, int codigoUsuario, gsOV_BuscarCabeceraResult objOrdenVentaCabBE,
            List<gsOV_BuscarDetalleResult> lstProductos, List<GlosaBE> lstImpuestos, decimal? idOperacion, decimal limiteCredito,
            DateTime fechaVencimiento, List<gsPedidos_FechasLetrasSelectResult> ListaFechas, string Letras, int KardexFlete, gsOV_BuscarDetalleResult objetoFlete, int DiasCredito)
        {
            decimal neto, descuento, impuesto;
            List<gsOV_BuscarDetalleResult> lst;
            List<gsImpuesto_ListarPorItemResult> lstImp;
            List<GlosaBE> lstGlosa;
            gsOV_BuscarDetalleResult newItemFlete = new gsOV_BuscarDetalleResult();
 

            try
            {
                List<gsPedidoDetalle> lstPedidoDet = new List<gsPedidoDetalle>();
                OrdenVentaBL objOrdenVentaBL = new OrdenVentaBL();
                PedidoBL objPedidoBL = new PedidoBL();

                if (idOperacion == null && lstProductos.FindAll(x => x.Stock - x.Cantidad < 0 && x.Estado == 1 && x.Item_ID != KardexFlete).Count > 0
                    && lstProductos.FindAll(x => x.Stock - x.Cantidad >= 0 && x.Estado == 1 && x.Item_ID != KardexFlete).Count > 0)
                {
                    List<gsOV_BuscarDetalleResult> lstProductosBack = new List<gsOV_BuscarDetalleResult>();
                    // Guardar Pedido 
                    for (int x = 0; x < lstProductos.Count(); x++)
                    {
                        lstProductosBack.Add(lstProductos[x]);
                    }


                    //----------------------------------------------------
                    // Pedidos con Stock 
                    ImpuestoBL objImpuestoBL = new ImpuestoBL();
                    if (lstProductos.FindAll(x => x.Stock - x.Cantidad >= 0 && x.Estado > 0).Count > 0)
                    {

                        newItemFlete = new gsOV_BuscarDetalleResult();

                        List<gsOV_BuscarDetalleResult> lstProductosConStock = new List<gsOV_BuscarDetalleResult>();
                        foreach (gsOV_BuscarDetalleResult pedidoItem in lstProductosBack)
                        {
                            gsOV_BuscarDetalleResult newItem = new gsOV_BuscarDetalleResult();
                      

                            newItem = pedidoItem;
                            if ((newItem.Stock - newItem.Cantidad) >= 0)
                            {
                                if (newItem.Item_ID == KardexFlete)
                                {
                                    newItem.Estado = 0;
                                    newItemFlete = objetoFlete; 
                                }
                                else
                                {
                                    lstProductosConStock.Add(newItem);
                                }
                            }
                            else
                            {
                                if (newItem.Item_ID == KardexFlete)
                                {
                                    newItem.Estado = 0;
                                    newItemFlete = objetoFlete; 
                                }
                            }

                        }

                        neto = 0;
                        descuento = 0;
                        impuesto = 0;
                        lst = lstProductosConStock.FindAll(x => x.Stock - x.Cantidad >= 0 && x.Estado > 0 && x.Item_ID != KardexFlete);

                        if (lst.Count() > 0)
                        {
                            // Agregar Item Flete Con Stock
                            if (  !string.IsNullOrEmpty(newItemFlete.Item))
                            {
                                if ( newItemFlete.Item_ID != 0)
                                {
                                    decimal PesoItem = 0;
                                    decimal PesoPedido = 0;

                                    foreach (gsOV_BuscarDetalleResult objF in lst)
                                    {
                                        PesoItem = 0;

                                        if (objF.Peso_Kg2 > 0)
                                        {
                                            PesoItem = (objF.Cantidad * objF.Peso_Kg2);
                                        }
                                        else if (objF.Peso_Kg2 == 0)
                                        {
                                            PesoItem = (objF.Cantidad * objF.Peso2 * objF.FactorConversion2);
                                        }
                                        else
                                        {
                                            PesoItem = (objF.Cantidad * objF.Peso2 * objF.FactorConversion2);
                                        }

                                        PesoPedido = PesoPedido + PesoItem;
                                    }

                                    newItemFlete.ID_Amarre = 0;
                                    newItemFlete.Cantidad = PesoPedido;
                                    newItemFlete.CantidadMaxima = 0;
                                    newItemFlete.CantidadUnidadDoc = PesoPedido;
                                    newItemFlete.CantidadUnidadInv = PesoPedido;
                                    newItemFlete.Importe = (PesoPedido * newItemFlete.Precio);
                                    newItemFlete.Stock = 1;
                                    newItemFlete.Estado = 1; 

                                    lst.Add(newItemFlete);
                                    lstProductosConStock.Add(newItemFlete);
                                }
                                //-----------------------------
                            }
                            //-----------------------------



                        lstGlosa = new List<GlosaBE>();
                                foreach (gsOV_BuscarDetalleResult objProducto in lst)
                                {
                                    if (objProducto.Estado == 1)
                                    {
                                        neto = neto + objProducto.Importe + ((objProducto.Dcto / 100 * objProducto.Precio) * objProducto.Cantidad);
                                        descuento = descuento + ((objProducto.Dcto / 100 * objProducto.Precio) * objProducto.Cantidad);

                                        lstImp = objImpuestoBL.Impuesto_ListarPorItem(idEmpresa, codigoUsuario, objProducto.ID_Item, DateTime.Now.Date);
                                        foreach (gsImpuesto_ListarPorItemResult objImpuesto in lstImp)
                                        {
                                            GlosaBE objGlosaBE = new GlosaBE();
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
                                    }
                                }

                                foreach (GlosaBE objGlosaBE in lstGlosa)
                                {
                                    impuesto = impuesto + objGlosaBE.Importe;
                                }

                                objOrdenVentaCabBE.Neto = neto;
                                objOrdenVentaCabBE.Dcto = descuento;
                                objOrdenVentaCabBE.SubTotal = neto - descuento;
                                objOrdenVentaCabBE.Impuestos = impuesto;
                                objOrdenVentaCabBE.Total = neto - descuento + impuesto;

                                gsPedidoDetalle objPedidoDetalle = new gsPedidoDetalle();
                                objPedidoDetalle.Op = objOrdenVentaBL.OrdenVenta_Registrar(idEmpresa, codigoUsuario, objOrdenVentaCabBE,
                                lstProductosConStock , lstGlosa, idOperacion, limiteCredito, ListaFechas);

                                objPedidoDetalle.PlanLetras = Letras;

                                if (objOrdenVentaCabBE.Total > limiteCredito)
                                {
                                    objPedidoDetalle.Motivo = "Se debe aumentar la linea de crédito en " +
                                        Math.Round((objOrdenVentaCabBE.Total - limiteCredito), 2).ToString() + ".";
                                    objPedidoDetalle.limiteCredito = true;
                                    objOrdenVentaBL.OrdenVenta_Deasaprobar(idEmpresa, codigoUsuario, int.Parse(objPedidoDetalle.Op.ToString()), "");
                                }

                                if (DateTime.Compare(DateTime.Now.Date, fechaVencimiento) > 0)
                                {
                                    objPedidoDetalle.Motivo = objPedidoDetalle.Motivo + " Documentos pendientes de pago, aumentar los días de bloqueo en " +
                                        (DateTime.Now.Date - fechaVencimiento).Days.ToString() + ".";
                                    objPedidoDetalle.documentoPendiente = true;
                                    objOrdenVentaBL.OrdenVenta_Deasaprobar(idEmpresa, codigoUsuario, int.Parse(objPedidoDetalle.Op.ToString()), "");
                                }

                                if (lstProductos.FindAll(x => x.Stock - x.Cantidad >= 0 && x.Item_ID != KardexFlete).Count > 0)
                                {
                                    objPedidoDetalle.Motivo = objPedidoDetalle.Motivo + " El pedido se ha guardado con stock.";
                                    objPedidoDetalle.sinStock = false;
                                }

                                objPedidoDetalle.Motivo = objPedidoDetalle.Motivo.Trim();
                                limiteCredito = limiteCredito - objOrdenVentaCabBE.Total;
                                lstPedidoDet.Add(objPedidoDetalle);
                        }
                        //-----------------------------

                    }
                    if (lstProductos.FindAll(x => x.Stock - x.Cantidad < 0 && x.Estado > 0).Count > 0)
                    {

                        // Insertar productos Sin Stock 
                        lstProductosBack = new List<gsOV_BuscarDetalleResult>();
                        // Guardar Pedido 
                        for (int x = 0; x < lstProductos.Count(); x++)
                        {
                            lstProductosBack.Add(lstProductos[x]);
                        }
                        //----------------------------------------------------------
                        List<gsOV_BuscarDetalleResult> lstProductosSinStock = new List<gsOV_BuscarDetalleResult>();
                        newItemFlete = new gsOV_BuscarDetalleResult(); 
            
                        foreach (gsOV_BuscarDetalleResult pedidoItem in lstProductosBack)
                        {
                            gsOV_BuscarDetalleResult newItem = new gsOV_BuscarDetalleResult();
                            newItem = pedidoItem;

                            if ((newItem.Stock - newItem.Cantidad) < 0)
                            {
                                newItem.Estado = 1;
                                newItem.ID_Amarre = 0;

                                if (newItem.Item_ID == KardexFlete)
                                {
                                    newItem.Estado = 0;
                                    newItemFlete = objetoFlete; 
                                }
                                else
                                {
                                    lstProductosSinStock.Add(newItem);
                                }
                                
                            }
                            else
                            {
                                if (newItem.Item_ID == KardexFlete)
                                {
                                    newItem.Estado = 0;
                                    newItemFlete = objetoFlete; 
                                }
                            }
                        }
 
                        //------------------------------------------------------------
                        neto = 0;
                        descuento = 0;
                        impuesto = 0;

                        lst = lstProductosSinStock.FindAll(x => x.Stock - x.Cantidad < 0 && x.Estado > 0  && x.Item_ID != KardexFlete);


                        // Agregar Item Flete Sin Stock
                        if(lst.Count() > 0)
                        {
                            if (!string.IsNullOrEmpty(newItemFlete.Item))
                            {
                                if (newItemFlete.Item_ID != 0)
                                {
                                    decimal PesoItem = 0;
                                    decimal PesoPedido = 0;

                                    foreach (gsOV_BuscarDetalleResult objF in lst)
                                    {
                                        PesoItem = 0;

                                        if (objF.Peso_Kg2 > 0)
                                        {
                                            PesoItem = (objF.Cantidad * objF.Peso_Kg2);
                                        }
                                        else if (objF.Peso_Kg2 == 0)
                                        {
                                            PesoItem = (objF.Cantidad * objF.Peso2 * objF.FactorConversion2);
                                        }
                                        else
                                        {
                                            PesoItem = (objF.Cantidad * objF.Peso2 * objF.FactorConversion2);
                                        }

                                        PesoPedido = PesoPedido + PesoItem;
                                    }

                                    newItemFlete.ID_Amarre = 0;
                                    newItemFlete.Cantidad = PesoPedido;
                                    newItemFlete.CantidadMaxima = 0;
                                    newItemFlete.CantidadUnidadDoc = PesoPedido;
                                    newItemFlete.CantidadUnidadInv = PesoPedido;
                                    newItemFlete.Importe = (PesoPedido * newItemFlete.Precio);
                                    newItemFlete.Stock = 1;
                                    newItemFlete.Estado = 1;

                                    lst.Add(newItemFlete);
                                    lstProductosSinStock.Add(newItemFlete);
                                }
                            }
                            //-----------------------------
     

                            lstGlosa = new List<GlosaBE>();
                            foreach (gsOV_BuscarDetalleResult objProducto in lst)
                            {
                                if (objProducto.Estado == 1)
                                {
                                    neto = neto + objProducto.Importe + ((objProducto.Dcto / 100 * objProducto.Precio) * objProducto.Cantidad);
                                    descuento = descuento + ((objProducto.Dcto / 100 * objProducto.Precio) * objProducto.Cantidad);

                                    lstImp = objImpuestoBL.Impuesto_ListarPorItem(idEmpresa, codigoUsuario, objProducto.ID_Item, DateTime.Now.Date);
                                    foreach (gsImpuesto_ListarPorItemResult objImpuesto in lstImp)
                                    {
                                        GlosaBE objGlosaBE = new GlosaBE();
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
                                }
                            }

                            foreach (GlosaBE objGlosaBE in lstGlosa)
                            {
                                impuesto = impuesto + objGlosaBE.Importe;
                            }

                            objOrdenVentaCabBE.Neto = neto;
                            objOrdenVentaCabBE.Dcto = descuento;
                            objOrdenVentaCabBE.SubTotal = neto - descuento;
                            objOrdenVentaCabBE.Impuestos = impuesto;
                            objOrdenVentaCabBE.Total = neto - descuento + impuesto;

                            gsPedidoDetalle objPedidoDetalle = new gsPedidoDetalle();
                            objPedidoDetalle.Op = objOrdenVentaBL.OrdenVenta_Registrar(idEmpresa, codigoUsuario, objOrdenVentaCabBE,
                            lstProductosSinStock, lstGlosa, idOperacion, limiteCredito, ListaFechas);

                            objPedidoDetalle.PlanLetras = Letras;

                            if (objOrdenVentaCabBE.Total > limiteCredito)
                            {
                                objPedidoDetalle.Motivo = "Se debe aumentar la linea de crédito en " +
                                    Math.Round((objOrdenVentaCabBE.Total - limiteCredito), 2).ToString() + ".";
                                objPedidoDetalle.limiteCredito = true;
                                objOrdenVentaBL.OrdenVenta_Deasaprobar(idEmpresa, codigoUsuario, int.Parse(objPedidoDetalle.Op.ToString()), "");
                            }

                            if (DateTime.Compare(DateTime.Now.Date, fechaVencimiento) > 0)
                            {
                                objPedidoDetalle.Motivo = objPedidoDetalle.Motivo + " Documentos pendientes de pago, aumentar los días de bloqueo en " +
                                    (DateTime.Now.Date - fechaVencimiento).Days.ToString() + ".";
                                objPedidoDetalle.documentoPendiente = true;
                                objOrdenVentaBL.OrdenVenta_Deasaprobar(idEmpresa, codigoUsuario, int.Parse(objPedidoDetalle.Op.ToString()), "");
                            }

                            if (lstProductos.FindAll(x => x.Stock - x.Cantidad < 0 && x.Item_ID != KardexFlete).Count > 0)
                            {
                                objPedidoDetalle.Motivo = objPedidoDetalle.Motivo + " El pedido se ha guardado sin stock.";
                                objPedidoDetalle.sinStock = true;
                                objOrdenVentaBL.OrdenVenta_Deasaprobar(idEmpresa, codigoUsuario, int.Parse(objPedidoDetalle.Op.ToString()), "");
                            }

                            lstPedidoDet.Add(objPedidoDetalle);
                        }
                        //-----------------------------

                    }
                    objPedidoBL.Pedido_RegistrarAmarre(idEmpresa, codigoUsuario, lstPedidoDet, DiasCredito);
                }
                else
                {
                    List<gsOV_BuscarDetalleResult> lstProductosBack = new List<gsOV_BuscarDetalleResult>();
                    string[,] ItemsBack = new string[lstProductos.Count(), 2];

                    for (int x = 0; x < lstProductos.Count(); x++)
                    {
                        ItemsBack[x, 0] = lstProductos[x].Item_ID.ToString();
                        ItemsBack[x, 1] = lstProductos[x].Estado.ToString();
                    }


                    if (idOperacion != null && lstProductos.FindAll(x => x.Stock - x.Cantidad < 0 && x.Estado == 1 && x.Item_ID != KardexFlete).Count > 0 && 
                        lstProductos.FindAll(x => x.Stock - x.Cantidad >= 0 && x.Estado == 1 && x.Item_ID != KardexFlete ).Count > 0)
                    {
                        ImpuestoBL objImpuestoBL = new ImpuestoBL();

                        for (int x = 0; x < lstProductos.Count(); x++)
                        {
                            lstProductosBack.Add(lstProductos[x]);
                        }

                        //Insertar Pedidos con Stock
                        if (lstProductos.FindAll(x => x.Stock - x.Cantidad >= 0).Count > 0)
                        {

                            newItemFlete = new gsOV_BuscarDetalleResult();

                            List<gsOV_BuscarDetalleResult> lstProductosConStock = new List<gsOV_BuscarDetalleResult>();
                            foreach (gsOV_BuscarDetalleResult pedidoItem in lstProductosBack)
                            {
                                gsOV_BuscarDetalleResult newItem = new gsOV_BuscarDetalleResult();
                        

                                newItem = pedidoItem;
                                if ((newItem.Stock - newItem.Cantidad) >= 0)
                                {
                                    if (newItem.Item_ID == KardexFlete)
                                    {
                                        newItem.Estado = 0;
                                        newItemFlete = objetoFlete; 
                                    }
                                }
                                else
                                {
                                    if (newItem.Item_ID == KardexFlete)
                                    {
                                        newItem.Estado = 0;
                                        newItemFlete = objetoFlete; 
                                    }
                                    newItem.Estado = 0;
                                }

                                lstProductosConStock.Add(newItem);
                            }

                            neto = 0;
                            descuento = 0;
                            impuesto = 0;

                            lst = lstProductosConStock.FindAll(x => (x.Stock - x.Cantidad) >= 0 && x.Estado>0 && x.Item_ID != KardexFlete);

                            if (lst.Count() > 0)
                            {
                                // Agregar Item Flete Con Stock
                                if (!string.IsNullOrEmpty(newItemFlete.Item))
                                {
                                    if (newItemFlete.Item_ID != 0)
                                    {
                                        decimal PesoItem = 0;
                                        decimal PesoPedido = 0;

                                        foreach (gsOV_BuscarDetalleResult objF in lst)
                                        {
                                            PesoItem = 0;

                                            if (objF.Peso_Kg2 > 0)
                                            {
                                                PesoItem = (objF.Cantidad * objF.Peso_Kg2);
                                            }
                                            else if (objF.Peso_Kg2 == 0)
                                            {
                                                PesoItem = (objF.Cantidad * objF.Peso2 * objF.FactorConversion2);
                                            }
                                            else
                                            {
                                                PesoItem = (objF.Cantidad * objF.Peso2 * objF.FactorConversion2);
                                            }

                                            PesoPedido = PesoPedido + PesoItem;
                                        }

                                        newItemFlete.ID_Amarre = 0;
                                        newItemFlete.Cantidad = PesoPedido;
                                        newItemFlete.CantidadMaxima = 0;
                                        newItemFlete.CantidadUnidadDoc = PesoPedido;
                                        newItemFlete.CantidadUnidadInv = PesoPedido;
                                        newItemFlete.Importe = (PesoPedido * newItemFlete.Precio);
                                        newItemFlete.Stock = 1;
                                        newItemFlete.Estado = 1;

                                        lst.Add(newItemFlete);
                                        lstProductosConStock.Add(newItemFlete);

                                    }
                                }
                                    //-----------------------------




                                    lstGlosa = new List<GlosaBE>();
                                    foreach (gsOV_BuscarDetalleResult objProducto in lst)
                                    {
                                        if (objProducto.Estado == 1)
                                        {
                                            neto = neto + objProducto.Importe + ((objProducto.Dcto / 100 * objProducto.Precio) * objProducto.Cantidad);
                                            descuento = descuento + ((objProducto.Dcto / 100 * objProducto.Precio) * objProducto.Cantidad);

                                            lstImp = objImpuestoBL.Impuesto_ListarPorItem(idEmpresa, codigoUsuario, objProducto.ID_Item, DateTime.Now.Date);
                                            foreach (gsImpuesto_ListarPorItemResult objImpuesto in lstImp)
                                            {
                                                GlosaBE objGlosaBE = new GlosaBE();
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
                                        }
                                    }

                                    foreach (GlosaBE objGlosaBE in lstGlosa)
                                    {
                                        impuesto = impuesto + objGlosaBE.Importe;
                                    }

                                    objOrdenVentaCabBE.Neto = neto;
                                    objOrdenVentaCabBE.Dcto = descuento;
                                    objOrdenVentaCabBE.SubTotal = neto - descuento;
                                    objOrdenVentaCabBE.Impuestos = impuesto;
                                    objOrdenVentaCabBE.Total = neto - descuento + impuesto;

                                    gsPedidoDetalle objPedidoDetalle = new gsPedidoDetalle();
                                    objPedidoDetalle.Op = objOrdenVentaBL.OrdenVenta_Registrar(idEmpresa, codigoUsuario, objOrdenVentaCabBE,
                                                                                               lstProductosConStock, lstGlosa, idOperacion, limiteCredito, ListaFechas);

                                    objPedidoDetalle.PlanLetras = Letras;

                                    if (objOrdenVentaCabBE.Total > limiteCredito)
                                    {
                                        objPedidoDetalle.Motivo = "Se debe aumentar la linea de crédito en " +
                                            Math.Round((objOrdenVentaCabBE.Total - limiteCredito), 2).ToString() + ".";
                                        objPedidoDetalle.limiteCredito = true;
                                        objOrdenVentaBL.OrdenVenta_Deasaprobar(idEmpresa, codigoUsuario, int.Parse(objPedidoDetalle.Op.ToString()), "");
                                    }

                                    if (DateTime.Compare(DateTime.Now.Date, fechaVencimiento) > 0)
                                    {
                                        objPedidoDetalle.Motivo = objPedidoDetalle.Motivo + " Documentos pendientes de pago, aumentar los días de bloqueo en " +
                                            (DateTime.Now.Date - fechaVencimiento).Days.ToString() + ".";
                                        objPedidoDetalle.documentoPendiente = true;
                                        objOrdenVentaBL.OrdenVenta_Deasaprobar(idEmpresa, codigoUsuario, int.Parse(objPedidoDetalle.Op.ToString()), "");
                                    }

                                    if (lstProductos.FindAll(x => x.Stock - x.Cantidad >= 0 && x.Item_ID != KardexFlete).Count > 0)
                                    {
                                        objPedidoDetalle.Motivo = objPedidoDetalle.Motivo + " El pedido se ha guardado con stock.";
                                        objPedidoDetalle.sinStock = false;
                                    }

                                    objPedidoDetalle.Motivo = objPedidoDetalle.Motivo.Trim();
                                    limiteCredito = limiteCredito - objOrdenVentaCabBE.Total;
                                    lstPedidoDet.Add(objPedidoDetalle);

                            }
                            ////-----------------------------
                        }

                        //----------------------------------------------------------------------------
                        //lstProductosBack = lstProductos;
                        //Insertar Pedidos Sin Stock

                        idOperacion = null;
                        newItemFlete = new gsOV_BuscarDetalleResult(); 

                        lstProductosBack = new List<gsOV_BuscarDetalleResult>();
                        for (int x = 0; x < lstProductos.Count(); x++)
                        {
                            lstProductosBack.Add(lstProductos[x]);
                        }

                        if (lstProductos.FindAll(x => x.Stock - x.Cantidad < 0).Count > 0)
                        {
                            List<gsOV_BuscarDetalleResult> lstProductosSinStock = new List<gsOV_BuscarDetalleResult>();

                            foreach (gsOV_BuscarDetalleResult pedidoItem in lstProductosBack)
                            {
                                gsOV_BuscarDetalleResult newItem = new gsOV_BuscarDetalleResult();
                                newItem = pedidoItem;

                                if ((newItem.Stock - newItem.Cantidad) < 0)
                                {
                                    newItem.Estado = 1;
                                    newItem.ID_Amarre = 0;

                                    for (int x = 0; x < lstProductos.Count(); x++)
                                    {
                                        if (ItemsBack[x, 0] == newItem.Item_ID.ToString())
                                        {
                                            newItem.Estado = int.Parse(ItemsBack[x, 1]);
                                        }
                                    }

                                    if (newItem.Item_ID == KardexFlete)
                                    {
                                        newItem.Estado = 0;
                                        newItemFlete = objetoFlete; 
                                    }

                                    lstProductosSinStock.Add(newItem);
                                }
                            }

                            neto = 0;
                            descuento = 0;
                            impuesto = 0;
                            lst = lstProductosSinStock.FindAll(x => x.Stock - x.Cantidad < 0 && x.Estado > 0 && x.Item_ID != KardexFlete );


                            if (lst.Count() > 0)
                            {
                                // Agregar Item Flete Sin Stock
                                if (!string.IsNullOrEmpty(newItemFlete.Item))
                                {
                                    if (newItemFlete.Item_ID != 0)
                                    {
                                        decimal PesoItem = 0;
                                        decimal PesoPedido = 0;

                                        foreach (gsOV_BuscarDetalleResult objF in lst)
                                        {
                                            PesoItem = 0;

                                            if (objF.Peso_Kg2 > 0)
                                            {
                                                PesoItem = (objF.Cantidad * objF.Peso_Kg2);
                                            }
                                            else if (objF.Peso_Kg2 == 0)
                                            {
                                                PesoItem = (objF.Cantidad * objF.Peso2 * objF.FactorConversion2);
                                            }
                                            else
                                            {
                                                PesoItem = (objF.Cantidad * objF.Peso2 * objF.FactorConversion2);
                                            }

                                            PesoPedido = PesoPedido + PesoItem;
                                        }

                                        newItemFlete.ID_Amarre = 0;
                                        newItemFlete.Cantidad = PesoPedido;
                                        newItemFlete.CantidadMaxima = 0;
                                        newItemFlete.CantidadUnidadDoc = PesoPedido;
                                        newItemFlete.CantidadUnidadInv = PesoPedido;
                                        newItemFlete.Importe = (PesoPedido * newItemFlete.Precio);
                                        newItemFlete.Stock = 1;

                                        lst.Add(newItemFlete);
                                        lstProductosSinStock.Add(newItemFlete);
                                    }
                                }
                                //-----------------------------




                                    lstGlosa = new List<GlosaBE>();
                                    foreach (gsOV_BuscarDetalleResult objProducto in lst)
                                    {
                                        if (objProducto.Estado == 1)
                                        {
                                            neto = neto + objProducto.Importe + ((objProducto.Dcto / 100 * objProducto.Precio) * objProducto.Cantidad);
                                            descuento = descuento + ((objProducto.Dcto / 100 * objProducto.Precio) * objProducto.Cantidad);

                                            lstImp = objImpuestoBL.Impuesto_ListarPorItem(idEmpresa, codigoUsuario, objProducto.ID_Item, DateTime.Now.Date);
                                            foreach (gsImpuesto_ListarPorItemResult objImpuesto in lstImp)
                                            {
                                                GlosaBE objGlosaBE = new GlosaBE();
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
                                        }
                                    }

                                    foreach (GlosaBE objGlosaBE in lstGlosa)
                                    {
                                        impuesto = impuesto + objGlosaBE.Importe;
                                    }

                                    objOrdenVentaCabBE.Neto = neto;
                                    objOrdenVentaCabBE.Dcto = descuento;
                                    objOrdenVentaCabBE.SubTotal = neto - descuento;
                                    objOrdenVentaCabBE.Impuestos = impuesto;
                                    objOrdenVentaCabBE.Total = neto - descuento + impuesto;

                                    gsPedidoDetalle objPedidoDetalle = new gsPedidoDetalle();
                                    objPedidoDetalle.Op = objOrdenVentaBL.OrdenVenta_Registrar(idEmpresa, codigoUsuario, objOrdenVentaCabBE,
                                                                                               lstProductosSinStock, lstGlosa, idOperacion, limiteCredito, ListaFechas);
                                    objPedidoDetalle.PlanLetras = Letras;
                                    if (objOrdenVentaCabBE.Total > limiteCredito)
                                    {
                                        objPedidoDetalle.Motivo = "Se debe aumentar la linea de crédito en " +
                                            Math.Round((objOrdenVentaCabBE.Total - limiteCredito), 2).ToString() + ".";
                                        objPedidoDetalle.limiteCredito = true;
                                        objOrdenVentaBL.OrdenVenta_Deasaprobar(idEmpresa, codigoUsuario, int.Parse(objPedidoDetalle.Op.ToString()), "");
                                    }

                                    if (DateTime.Compare(DateTime.Now.Date, fechaVencimiento) > 0)
                                    {
                                        objPedidoDetalle.Motivo = objPedidoDetalle.Motivo + " Documentos pendientes de pago, aumentar los días de bloqueo en " +
                                            (DateTime.Now.Date - fechaVencimiento).Days.ToString() + ".";
                                        objPedidoDetalle.documentoPendiente = true;
                                        objOrdenVentaBL.OrdenVenta_Deasaprobar(idEmpresa, codigoUsuario, int.Parse(objPedidoDetalle.Op.ToString()), "");
                                    }

                                    if (lstProductos.FindAll(x => x.Stock - x.Cantidad < 0 && x.Item_ID != KardexFlete).Count > 0)
                                    {
                                        objPedidoDetalle.Motivo = objPedidoDetalle.Motivo + " El pedido se ha guardado sin stock.";
                                        objPedidoDetalle.sinStock = true;
                                        objOrdenVentaBL.OrdenVenta_Deasaprobar(idEmpresa, codigoUsuario, int.Parse(objPedidoDetalle.Op.ToString()), "");
                                    }

                                    lstPedidoDet.Add(objPedidoDetalle);

                            }
                            ////-----------------------------
                        }

                        //Actualizar detalle de estados 
                        objPedidoBL.Pedido_RegistrarAmarre(idEmpresa, codigoUsuario, lstPedidoDet, DiasCredito);
                    }
                    else
                    {
                        ImpuestoBL objImpuestoBL = new ImpuestoBL();

                        neto = 0;
                        descuento = 0;
                        impuesto = 0;
                        lst = lstProductos;
                        lstGlosa = new List<GlosaBE>();

                        foreach (gsOV_BuscarDetalleResult objProducto in lst)
                        {
                            if (objProducto.Estado == 1)
                            {
                                neto = neto + objProducto.Importe + ((objProducto.Dcto/100 * objProducto.Precio) * objProducto.Cantidad);
                                descuento = descuento + ((objProducto.Dcto / 100 * objProducto.Precio) * objProducto.Cantidad);

                                lstImp = objImpuestoBL.Impuesto_ListarPorItem(idEmpresa, codigoUsuario, objProducto.ID_Item, DateTime.Now.Date);
                                foreach (gsImpuesto_ListarPorItemResult objImpuesto in lstImp)
                                {
                                    GlosaBE objGlosaBE = new GlosaBE();
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
                            }
                        }

                        foreach (GlosaBE objGlosaBE in lstGlosa)
                        {
                            impuesto = impuesto + objGlosaBE.Importe;
                        }

                        objOrdenVentaCabBE.Neto = neto;
                        objOrdenVentaCabBE.Dcto = descuento;
                        objOrdenVentaCabBE.SubTotal = neto - descuento;
                        objOrdenVentaCabBE.Impuestos = impuesto;
                        objOrdenVentaCabBE.Total = neto - descuento + impuesto;

                        //--------------------------------------------------------------
                        gsPedidoDetalle objPedidoDetalle = new gsPedidoDetalle();
                        objPedidoDetalle.Op = objOrdenVentaBL.OrdenVenta_Registrar(idEmpresa, codigoUsuario, objOrdenVentaCabBE, lstProductos, lstImpuestos, idOperacion, limiteCredito, ListaFechas);

                        objPedidoDetalle.PlanLetras = Letras;
                        objPedidoDetalle.Motivo = "";
                        if (objOrdenVentaCabBE.Total > limiteCredito)
                        {
                            objPedidoDetalle.Motivo = "Se debe aumentar la linea de crédito en " +
                                Math.Round((objOrdenVentaCabBE.Total - limiteCredito), 2).ToString() + ".";
                            objPedidoDetalle.limiteCredito = true;
                            objOrdenVentaBL.OrdenVenta_Deasaprobar(idEmpresa, codigoUsuario, int.Parse(objPedidoDetalle.Op.ToString()), "");
                        }

                        if (DateTime.Compare(DateTime.Now.Date, fechaVencimiento) > 0)
                        {
                            objPedidoDetalle.Motivo = objPedidoDetalle.Motivo + " Documentos pendientes de pago, aumentar los días de bloqueo en " +
                                (DateTime.Now.Date - fechaVencimiento).Days.ToString() + ".";
                            objPedidoDetalle.documentoPendiente = true;
                            objOrdenVentaBL.OrdenVenta_Deasaprobar(idEmpresa, codigoUsuario, int.Parse(objPedidoDetalle.Op.ToString()), "");
                        }

                        if (lstProductos.FindAll(x => x.Stock - x.Cantidad < 0 && x.Item_ID != KardexFlete ).Count > 0)
                        {
                            objPedidoDetalle.Motivo = objPedidoDetalle.Motivo + " El pedido se ha guardado sin stock.";
                            objPedidoDetalle.sinStock = true;
                            objOrdenVentaBL.OrdenVenta_Deasaprobar(idEmpresa, codigoUsuario, int.Parse(objPedidoDetalle.Op.ToString()), "");
                        }

                        objPedidoDetalle.Motivo = objPedidoDetalle.Motivo.Trim();
                        lstPedidoDet.Add(objPedidoDetalle);
                        objPedidoBL.Pedido_RegistrarAmarre(idEmpresa, codigoUsuario, lstPedidoDet, DiasCredito);

                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error WCF: " + ex.Message.ToString());
            }
        }

        public List<VBG03630Result> OrdenVenta_ListarTipo(int idEmpresa, int codigoUsuario)
        {
            PedidoBL objPedidoBL;
            try
            {
                objPedidoBL = new PedidoBL();
                return objPedidoBL.Pedido_ListarTipo(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error WCF: " + ex.Message.ToString());
            }
        }

        public void OV_TransGratuitas_Aprobar(int idEmpresa, int codigoUsuario, int Op, ref string mensajeError)
        {
            try
            {
                OrdenVentaBL objOrdenVentaBL = new OrdenVentaBL();
                objOrdenVentaBL.OV_TransGratuitas_Aprobar(idEmpresa, codigoUsuario, Op, ref mensajeError);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error WCF: " + ex.Message.ToString());
            }
        }

        public List<gsOV_Listar_SectoristaResult> OrdenVenta_Listar_Sectorista(int idEmpresa, int codigoUsuario, string ID_Agenda, DateTime fechaDesde, DateTime fechaHasta, string ID_Vendedor, bool modificarPedido, string id_Sectorista, int Estado, int FormaPago)
        {
            try
            {
                List<gsOV_Listar_SectoristaResult> lista = new List<gsOV_Listar_SectoristaResult>(); 
                OrdenVentaBL objOrdenVentaBL = new OrdenVentaBL();

                lista = objOrdenVentaBL.OrdenVenta_Listar_Sectorista(idEmpresa, codigoUsuario, ID_Agenda, fechaDesde, fechaHasta, ID_Vendedor, modificarPedido, id_Sectorista, Estado, FormaPago);
                return lista; 
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error WCF: " + ex.Message.ToString());
            }
        }

        public void OrdenVenta_Deasaprobar(int idEmpresa, int codigoUsuario, int idOperacion, string Comentario)
        {
            try
            {
                OrdenVentaBL objOrdenVentaBL = new OrdenVentaBL();
                objOrdenVentaBL.OrdenVenta_Deasaprobar(idEmpresa, codigoUsuario, idOperacion, Comentario);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error WCF: " + ex.Message.ToString());
            }
        }

        public void OrdenVenta_Registrar_Contado(int idEmpresa, int codigoUsuario, gsOV_BuscarCabeceraResult objOrdenVentaCabBE,
        List<gsOV_BuscarDetalleResult> lstProductos, List<GlosaBE> lstImpuestos, decimal? idOperacion, decimal limiteCredito,
        DateTime fechaVencimiento, List<gsPedidos_FechasLetrasSelectResult> ListaFechas, string Letras, int KardexFlete, gsOV_BuscarDetalleResult objetoFlete, int DiasCredito)
        {
            decimal neto, descuento, impuesto;
            List<gsOV_BuscarDetalleResult> lst;
            List<gsImpuesto_ListarPorItemResult> lstImp;
            List<GlosaBE> lstGlosa;
            string motivo;
            try
            {
                List<gsPedidoDetalle> lstPedidoDet = new List<gsPedidoDetalle>();
                OrdenVentaBL objOrdenVentaBL = new OrdenVentaBL();
                PedidoBL objPedidoBL = new PedidoBL();


                List<gsOV_BuscarDetalleResult> lstProductosBack = new List<gsOV_BuscarDetalleResult>();

                gsPedidoDetalle objPedidoDetalle = new gsPedidoDetalle();
                objPedidoDetalle.Op = objOrdenVentaBL.OrdenVenta_Registrar(idEmpresa, codigoUsuario, objOrdenVentaCabBE, lstProductos, lstImpuestos, idOperacion, limiteCredito, ListaFechas);

                objPedidoDetalle.PlanLetras = Letras;

                objPedidoDetalle.Motivo = "";

                decimal dTotal = Math.Round(objOrdenVentaCabBE.Total, 4);
                decimal dLimite = Math.Round(limiteCredito, 4);


                if (dTotal > dLimite)
                {


                    objPedidoDetalle.Motivo = "Se debe ingresar un Pago Anticipado igual o mayor a " +
                    Math.Round((objOrdenVentaCabBE.Total - limiteCredito), 4).ToString() + ".";
                    objPedidoDetalle.limiteCredito = true;
                    objOrdenVentaBL.OrdenVenta_Deasaprobar(idEmpresa, codigoUsuario, int.Parse(objPedidoDetalle.Op.ToString()), "");

                    if (DateTime.Compare(DateTime.Now.Date, fechaVencimiento) > 0)
                    {
                        objPedidoDetalle.Motivo = objPedidoDetalle.Motivo + " Documentos pendientes de pago, aumentar los días de bloqueo en " +
                            (DateTime.Now.Date - fechaVencimiento).Days.ToString() + ".";
                        objPedidoDetalle.documentoPendiente = true;
                        objOrdenVentaBL.OrdenVenta_Deasaprobar(idEmpresa, codigoUsuario, int.Parse(objPedidoDetalle.Op.ToString()), "");
                    }

                }
                else
                {

                    if (DateTime.Compare(DateTime.Now.Date, fechaVencimiento) > 0)
                    {

                        objPedidoDetalle.Motivo = objPedidoDetalle.Motivo + " Documentos pendientes de pago, aumentar los días de bloqueo en " +
                            (DateTime.Now.Date - fechaVencimiento).Days.ToString() + ".";
                        objPedidoDetalle.documentoPendiente = true;
                        objOrdenVentaBL.OrdenVenta_Deasaprobar(idEmpresa, codigoUsuario, int.Parse(objPedidoDetalle.Op.ToString()), "");
                    }
                    else
                    {
                        string Moneda = "";
                        if (objOrdenVentaCabBE.ID_Moneda == 0)
                        {
                            Moneda = "$";
                        }
                        else
                        {
                            Moneda = "S/";
                        }
                        objPedidoDetalle.Motivo = "Se aprobó el pedido por tener un Pago Adelanto y/o NC de: " + Moneda + Math.Round((limiteCredito), 2).ToString() + ".";
                        objPedidoBL.Pedido_Aprobar(idEmpresa, codigoUsuario, objPedidoDetalle.idPedido, int.Parse(objPedidoDetalle.Op.ToString()),
                                                   codigoUsuario.ToString(), true, objPedidoDetalle.Motivo);
                    }

                }

                objPedidoDetalle.Motivo = objPedidoDetalle.Motivo.Trim();
                lstPedidoDet.Add(objPedidoDetalle);
                objPedidoBL.Pedido_RegistrarAmarre(idEmpresa, codigoUsuario, lstPedidoDet, DiasCredito);

                //}

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error WCF: " + ex.Message.ToString());
            }
        }

        public gsOV_BuscarCabeceraResult OrdenVenta_Buscar_Guia(int idEmpresa, int codigoUsuario, int idPedido,
        ref List<gsOV_BuscarDetalleResult> objOrdenVentaDet, ref List<gsOV_BuscarImpuestoResult> objOrdenVentaImp,
        ref bool? bloqueado, ref string mensajeBloqueado)
        {
            try
            {
                OrdenVentaBL objOrdenVentaBL = new OrdenVentaBL();
                return objOrdenVentaBL.OrdenVenta_Buscar_Guia(idEmpresa, codigoUsuario, idPedido, ref objOrdenVentaDet, ref objOrdenVentaImp,
                    ref bloqueado, ref mensajeBloqueado);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error WCF: " + ex.Message.ToString());
            }
        }

        public List<gsPedidos_FechasLetrasSelectResult> PedidoLetras_Detalle(int idEmpresa, int codigoUsuario, int OpOV, int OpDOC, string Tabla)
        {
            OrdenVentaBL objOrdenVentaBL = new OrdenVentaBL();
            try
            {
                objOrdenVentaBL = new OrdenVentaBL();
                return objOrdenVentaBL.PedidoLetras_Detalle(idEmpresa, codigoUsuario, OpOV, OpDOC, Tabla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public gsPedido_EliminarOP_WMSResult Pedido_Apto_Modificacion(int idEmpresa, int codigoUsuario, int idPedido, int Op)
        {
            OrdenVentaBL objOrdenVentaBL = new OrdenVentaBL();
            try
            {
                objOrdenVentaBL = new OrdenVentaBL();
                return objOrdenVentaBL.Pedido_Apto_Modificacion(idEmpresa, codigoUsuario, idPedido, Op);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void OrdenVenta_Registrar_Fechas(int idEmpresa, int codigoUsuario, DataTable TablaDocs, List<gsPedidos_FechasLetrasSelectResult> ListaFechas)
        {
            OrdenVentaBL objOrdenVentaBL = new OrdenVentaBL();
            try
            {
                objOrdenVentaBL = new OrdenVentaBL();
                objOrdenVentaBL.OrdenVenta_Registrar_Fechas(idEmpresa, codigoUsuario, TablaDocs, ListaFechas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
