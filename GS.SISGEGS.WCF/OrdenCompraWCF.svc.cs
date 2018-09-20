using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GS.SISGEGS.DM;
using GS.SISGEGS.BL;
using GS.SISGEGS.BE;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "OrdenCompraWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione OrdenCompraWCF.svc o OrdenCompraWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class OrdenCompraWCF : IOrdenCompraWCF

    {
        #region OLD
        //public gsOV_BuscarCabeceraResult OrdenVenta_Buscar(int idEmpresa, int codigoUsuario, int idPedido,
        //    ref List<gsOV_BuscarDetalleResult> objOrdenVentaDet, ref List<gsOV_BuscarImpuestoResult> objOrdenVentaImp,
        //    ref bool? bloqueado, ref string mensajeBloqueado)
        //{
        //    try
        //    {
        //        OrdenVentaBL objOrdenVentaBL = new OrdenVentaBL();
        //        return objOrdenVentaBL.OrdenVenta_Buscar(idEmpresa, codigoUsuario, idPedido, ref objOrdenVentaDet, ref objOrdenVentaImp,
        //            ref bloqueado, ref mensajeBloqueado);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void OrdenVenta_Eliminar(int idEmpresa, int codigoUsuario, int idOperacion)
        //{
        //    try
        //    {
        //        OrdenVentaBL objOrdenVentaBL = new OrdenVentaBL();
        //        //objOrdenVentaBL.OrdenVenta_Eliminar(idEmpresa, codigoUsuario, idOperacion);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<gsOV_ListarResult> OrdenVenta_Listar(int idEmpresa, int codigoUsuario, string ID_Agenda,
        //    DateTime fechaDesde, DateTime fechaHasta, string ID_Vendedor, bool modificarPedido)
        //{
        //    try
        //    {
        //        OrdenVentaBL objOrdenVentaBL = new OrdenVentaBL();
        //        return objOrdenVentaBL.OrdenVenta_Listar(idEmpresa, codigoUsuario, ID_Agenda, fechaDesde,
        //            fechaHasta, ID_Vendedor, modificarPedido);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ////public void OrdenVenta_Registrar(int idEmpresa, int codigoUsuario, gsOV_BuscarCabeceraResult objOrdenVentaCabBE,
        ////    List<gsOV_BuscarDetalleResult> lstProductos, List<GlosaBE> lstImpuestos, decimal? idOperacion, decimal limiteCredito,
        ////    DateTime fechaVencimiento)
        ////{
        ////    decimal neto, descuento, impuesto;
        ////    List<gsOV_BuscarDetalleResult> lst;
        ////    List<gsImpuesto_ListarPorItemResult> lstImp;
        ////    List<GlosaBE> lstGlosa;

        ////    try
        ////    {
        ////        List<gsPedidoDetalle> lstPedidoDet = new List<gsPedidoDetalle>();
        ////        OrdenVentaBL objOrdenVentaBL = new OrdenVentaBL();
        ////        PedidoBL objPedidoBL = new PedidoBL();
        ////        if (idOperacion == null && lstProductos.FindAll(x => x.Stock - x.Cantidad < 0).Count > 0
        ////            && lstProductos.FindAll(x => x.Stock - x.Cantidad >= 0).Count > 0)
        ////        {
        ////            ImpuestoBL objImpuestoBL = new ImpuestoBL();
        ////            if (lstProductos.FindAll(x => x.Stock - x.Cantidad >= 0).Count > 0)
        ////            {
        ////                neto = 0;
        ////                descuento = 0;
        ////                impuesto = 0;
        ////                lst = lstProductos.FindAll(x => x.Stock - x.Cantidad >= 0);
        ////                lstGlosa = new List<GlosaBE>();
        ////                foreach (gsOV_BuscarDetalleResult objProducto in lst)
        ////                {
        ////                    if (objProducto.Estado == 1)
        ////                    {
        ////                        neto = neto + objProducto.Importe;
        ////                        descuento = descuento + objProducto.DctoValor;

        ////                        lstImp = objImpuestoBL.Impuesto_ListarPorItem(idEmpresa, codigoUsuario, objProducto.ID_Item, DateTime.Now.Date);
        ////                        foreach (gsImpuesto_ListarPorItemResult objImpuesto in lstImp)
        ////                        {
        ////                            GlosaBE objGlosaBE = new GlosaBE();
        ////                            if (lstGlosa.FindAll(x => x.IdGlosa == objImpuesto.ID_Impuesto).Count <= 0)
        ////                            {
        ////                                objGlosaBE.IdGlosa = objImpuesto.ID_Impuesto;
        ////                                objGlosaBE.Descripcion = objImpuesto.Abreviacion;
        ////                                if (objImpuesto.Valor > 0)
        ////                                    objGlosaBE.BaseImponible = objProducto.Importe;
        ////                                else
        ////                                    objGlosaBE.BaseImponible = 0;
        ////                                objGlosaBE.Importe = Math.Round(((decimal)objImpuesto.Valor / 100) * (objProducto.Importe * (1 - (objProducto.DctoValor / 100))), 4);
        ////                            }
        ////                            else
        ////                            {
        ////                                objGlosaBE = lstGlosa.Find(x => x.IdGlosa == objImpuesto.ID_Impuesto);
        ////                                lstGlosa.Remove(objGlosaBE);
        ////                                if (objImpuesto.Valor > 0)
        ////                                    objGlosaBE.BaseImponible = objGlosaBE.BaseImponible + objProducto.Importe;
        ////                                objGlosaBE.Importe = Math.Round(objGlosaBE.Importe + ((decimal)objImpuesto.Valor / 100) * (objProducto.Importe * (1 - (objProducto.DctoValor / 100))), 4);
        ////                            }
        ////                            lstGlosa.Add(objGlosaBE);
        ////                        }
        ////                    }
        ////                }

        ////                foreach (GlosaBE objGlosaBE in lstGlosa)
        ////                {
        ////                    impuesto = impuesto + objGlosaBE.Importe;
        ////                }

        ////                objOrdenVentaCabBE.Neto = neto;
        ////                objOrdenVentaCabBE.Dcto = descuento;
        ////                objOrdenVentaCabBE.SubTotal = neto - descuento;
        ////                objOrdenVentaCabBE.Impuestos = impuesto;
        ////                objOrdenVentaCabBE.Total = neto - descuento + impuesto;

        ////                gsPedidoDetalle objPedidoDetalle = new gsPedidoDetalle();
        ////                objPedidoDetalle.Op = objOrdenVentaBL.OrdenVenta_Registrar(idEmpresa, codigoUsuario, objOrdenVentaCabBE,
        ////                lstProductos.FindAll(x => x.Stock - x.Cantidad >= 0), lstGlosa, idOperacion, limiteCredito);

        ////                if (objOrdenVentaCabBE.Total > limiteCredito)
        ////                {
        ////                    objPedidoDetalle.Motivo = "Se debe aumentar la linea de crédito en " +
        ////                        Math.Round((objOrdenVentaCabBE.Total - limiteCredito), 2).ToString() + ".";
        ////                    objPedidoDetalle.limiteCredito = true;
        ////                }

        ////                if (DateTime.Compare(DateTime.Now.Date, fechaVencimiento) > 0)
        ////                {
        ////                    objPedidoDetalle.Motivo = objPedidoDetalle.Motivo + " Documentos pendientes de pago, aumentar los días de bloqueo en " +
        ////                        (DateTime.Now.Date - fechaVencimiento).Days.ToString() + ".";
        ////                    objPedidoDetalle.documentoPendiente = true;
        ////                }

        ////                if (lstProductos.FindAll(x => x.Stock - x.Cantidad >= 0).Count > 0)
        ////                {
        ////                    objPedidoDetalle.Motivo = objPedidoDetalle.Motivo + " El pedido se ha guardado con stock.";
        ////                    objPedidoDetalle.sinStock = false;
        ////                }

        ////                objPedidoDetalle.Motivo = objPedidoDetalle.Motivo.Trim();
        ////                limiteCredito = limiteCredito - objOrdenVentaCabBE.Total;
        ////                lstPedidoDet.Add(objPedidoDetalle);
        ////            }
        ////            if (lstProductos.FindAll(x => x.Stock - x.Cantidad < 0).Count > 0)
        ////            {
        ////                neto = 0;
        ////                descuento = 0;
        ////                impuesto = 0;
        ////                lst = lstProductos.FindAll(x => x.Stock - x.Cantidad < 0);
        ////                lstGlosa = new List<GlosaBE>();
        ////                foreach (gsOV_BuscarDetalleResult objProducto in lst)
        ////                {
        ////                    if (objProducto.Estado == 1)
        ////                    {
        ////                        neto = neto + objProducto.Importe;
        ////                        descuento = descuento + objProducto.DctoValor;

        ////                        lstImp = objImpuestoBL.Impuesto_ListarPorItem(idEmpresa, codigoUsuario, objProducto.ID_Item, DateTime.Now.Date);
        ////                        foreach (gsImpuesto_ListarPorItemResult objImpuesto in lstImp)
        ////                        {
        ////                            GlosaBE objGlosaBE = new GlosaBE();
        ////                            if (lstGlosa.FindAll(x => x.IdGlosa == objImpuesto.ID_Impuesto).Count <= 0)
        ////                            {
        ////                                objGlosaBE.IdGlosa = objImpuesto.ID_Impuesto;
        ////                                objGlosaBE.Descripcion = objImpuesto.Abreviacion;
        ////                                if (objImpuesto.Valor > 0)
        ////                                    objGlosaBE.BaseImponible = objProducto.Importe;
        ////                                else
        ////                                    objGlosaBE.BaseImponible = 0;
        ////                                objGlosaBE.Importe = Math.Round(((decimal)objImpuesto.Valor / 100) * (objProducto.Importe * (1 - (objProducto.DctoValor / 100))), 4);
        ////                            }
        ////                            else
        ////                            {
        ////                                objGlosaBE = lstGlosa.Find(x => x.IdGlosa == objImpuesto.ID_Impuesto);
        ////                                lstGlosa.Remove(objGlosaBE);
        ////                                if (objImpuesto.Valor > 0)
        ////                                    objGlosaBE.BaseImponible = objGlosaBE.BaseImponible + objProducto.Importe;
        ////                                objGlosaBE.Importe = Math.Round(objGlosaBE.Importe + ((decimal)objImpuesto.Valor / 100) * (objProducto.Importe * (1 - (objProducto.DctoValor / 100))), 4);
        ////                            }
        ////                            lstGlosa.Add(objGlosaBE);
        ////                        }
        ////                    }
        ////                }

        ////                foreach (GlosaBE objGlosaBE in lstGlosa)
        ////                {
        ////                    impuesto = impuesto + objGlosaBE.Importe;
        ////                }

        ////                objOrdenVentaCabBE.Neto = neto;
        ////                objOrdenVentaCabBE.Dcto = descuento;
        ////                objOrdenVentaCabBE.SubTotal = neto - descuento;
        ////                objOrdenVentaCabBE.Impuestos = impuesto;
        ////                objOrdenVentaCabBE.Total = neto - descuento + impuesto;

        ////                gsPedidoDetalle objPedidoDetalle = new gsPedidoDetalle();
        ////                objPedidoDetalle.Op = objOrdenVentaBL.OrdenVenta_Registrar(idEmpresa, codigoUsuario, objOrdenVentaCabBE,
        ////                lstProductos.FindAll(x => x.Stock - x.Cantidad < 0), lstGlosa, idOperacion, limiteCredito);

        ////                if (objOrdenVentaCabBE.Total > limiteCredito)
        ////                {
        ////                    objPedidoDetalle.Motivo = "Se debe aumentar la linea de crédito en " +
        ////                        Math.Round((objOrdenVentaCabBE.Total - limiteCredito), 2).ToString() + ".";
        ////                    objPedidoDetalle.limiteCredito = true;
        ////                }

        ////                if (DateTime.Compare(DateTime.Now.Date, fechaVencimiento) > 0)
        ////                {
        ////                    objPedidoDetalle.Motivo = objPedidoDetalle.Motivo + " Documentos pendientes de pago, aumentar los días de bloqueo en " +
        ////                        (DateTime.Now.Date - fechaVencimiento).Days.ToString() + ".";
        ////                    objPedidoDetalle.documentoPendiente = true;
        ////                }

        ////                if (lstProductos.FindAll(x => x.Stock - x.Cantidad < 0).Count > 0)
        ////                {
        ////                    objPedidoDetalle.Motivo = objPedidoDetalle.Motivo + " El pedido se ha guardado sin stock.";
        ////                    objPedidoDetalle.sinStock = true;
        ////                }

        ////                lstPedidoDet.Add(objPedidoDetalle);
        ////            }
        ////            objPedidoBL.Pedido_RegistrarAmarre(idEmpresa, codigoUsuario, lstPedidoDet);
        ////        }
        ////        else {
        ////            gsPedidoDetalle objPedidoDetalle = new gsPedidoDetalle();
        ////            objPedidoDetalle.Op = objOrdenVentaBL.OrdenVenta_Registrar(idEmpresa, codigoUsuario, objOrdenVentaCabBE,
        ////                lstProductos, lstImpuestos, idOperacion, limiteCredito);

        ////            if (idOperacion == null)
        ////            {
        ////                objPedidoDetalle.Motivo = "";
        ////                if (objOrdenVentaCabBE.Total > limiteCredito)
        ////                {
        ////                    objPedidoDetalle.Motivo = "Se debe aumentar la linea de crédito en " +
        ////                        Math.Round((objOrdenVentaCabBE.Total - limiteCredito), 2).ToString() + ".";
        ////                    objPedidoDetalle.limiteCredito = true;
        ////                }

        ////                if (DateTime.Compare(DateTime.Now.Date, fechaVencimiento) > 0)
        ////                {
        ////                    objPedidoDetalle.Motivo = objPedidoDetalle.Motivo + " Documentos pendientes de pago, aumentar los días de bloqueo en " +
        ////                        (DateTime.Now.Date - fechaVencimiento).Days.ToString() + ".";
        ////                    objPedidoDetalle.documentoPendiente = true;
        ////                }

        ////                if (lstProductos.FindAll(x => x.Stock - x.Cantidad < 0).Count > 0)
        ////                {
        ////                    objPedidoDetalle.Motivo = objPedidoDetalle.Motivo + " El pedido se ha guardado sin stock.";
        ////                    objPedidoDetalle.sinStock = true;
        ////                }

        ////                objPedidoDetalle.Motivo = objPedidoDetalle.Motivo.Trim();
        ////                lstPedidoDet.Add(objPedidoDetalle);
        ////                objPedidoBL.Pedido_RegistrarAmarre(idEmpresa, codigoUsuario, lstPedidoDet);
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }
        ////}

        //public List<VBG03630Result> OrdenVenta_ListarTipo(int idEmpresa, int codigoUsuario)
        //{
        //    PedidoBL objPedidoBL;
        //    try
        //    {
        //        objPedidoBL = new PedidoBL();
        //        return objPedidoBL.Pedido_ListarTipo(idEmpresa, codigoUsuario);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void OV_TransGratuitas_Aprobar(int idEmpresa, int codigoUsuario, int Op, ref string mensajeError)
        //{
        //    try
        //    {
        //        OrdenVentaBL objOrdenVentaBL = new OrdenVentaBL();
        //        objOrdenVentaBL.OV_TransGratuitas_Aprobar(idEmpresa, codigoUsuario, Op, ref mensajeError);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<gsOV_Listar_SectoristaResult> OrdenVenta_Listar_Sectorista(int idEmpresa, int codigoUsuario, string ID_Agenda, DateTime fechaDesde, DateTime fechaHasta, string ID_Vendedor, bool modificarPedido, string id_Sectorista)
        //{
        //    List<gsOV_Listar_SectoristaResult> lista = new List<gsOV_Listar_SectoristaResult>(); 
        //    try
        //    {
        //        OrdenVentaBL objOrdenVentaBL = new OrdenVentaBL();
        //        //return objOrdenVentaBL.OrdenVenta_Listar_Sectorista(idEmpresa, codigoUsuario, ID_Agenda, fechaDesde, fechaHasta, ID_Vendedor, modificarPedido, id_Sectorista);

        //        return lista; 
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //----------------------------------------------
        #endregion

        #region NEW
        public void Anular_OC(int idEmpresa, int codigoUsuario, int OP)
        {
            try
            {
                OrdenCompraBL objBL = new OrdenCompraBL();
                objBL.Anular_OC(idEmpresa, codigoUsuario, OP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VBG00536XResult> OrdenCompraListar(int idEmpresa, int codigoUsuario, string ID_Agenda, DateTime FechaDesde, DateTime FechaHasta,
            int EstadoAprobacion)
        {
            try
            {
                OrdenCompraBL objOrdenCompraBL = new OrdenCompraBL();
                return objOrdenCompraBL.OrdenCompraListar(idEmpresa, codigoUsuario, ID_Agenda, FechaDesde, FechaHasta, EstadoAprobacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<USP_Sel_OCResult> ListarOcImportacion(int idEmpresa, int codigoUsuario, DateTime fechainicial, DateTime fechafinal, string nombreproveedor)
        {
            try
            {
                OrdenCompraBL objOrdenCompraBL = new OrdenCompraBL();
                return objOrdenCompraBL.ListarOcImportacion(idEmpresa, codigoUsuario, fechainicial, fechafinal, nombreproveedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USP_Sel_OC_OpResult> Seleccionar_OC_OP(int idEmpresa, int codigoUsuario, int op)
        {
            try
            {
                OrdenCompraBL objOrdenCompraBL = new OrdenCompraBL();
                return objOrdenCompraBL.Seleccionar_OC_OP(idEmpresa, codigoUsuario, op).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USP_Sel_OC_OpLineaResult> Seleccionar_OC_OPLinea(int idEmpresa, int codigoUsuario, int op)
        {
            try
            {
                OrdenCompraBL objOrdenCompraBL = new OrdenCompraBL();
                return objOrdenCompraBL.Seleccionar_OC_OPLinea(idEmpresa, codigoUsuario, op).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USP_Sel_OC_OpParcialResult> Seleccionar_OC_OpParcial(int idEmpresa, int codigoUsuario, int op)
        {
            try
            {
                OrdenCompraBL objOrdenCompraBL = new OrdenCompraBL();
                return objOrdenCompraBL.Seleccionar_OC_OpParcial(idEmpresa, codigoUsuario, op).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Registrar_Oc_Parcial(int idEmpresa, int codigoUsuario, List<USP_Sel_Genesys_OC_ImpResult> CabOcParcial, List<USP_Sel_Genesys_OC_ImpLineaResult> DetOcparcial)
        {
            try
            {
                OrdenCompraBL objOrdenCompraBL = new OrdenCompraBL();
                objOrdenCompraBL.Registrar_Oc_Parcial(idEmpresa, codigoUsuario, CabOcParcial, DetOcparcial);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Eliminar_Oc_Parcial(int idEmpresa, int codigoUsuario, int op_oc, string No_RegistroParcial)
        {
            try
            {
                OrdenCompraBL objOrdenCompraBL = new OrdenCompraBL();
                objOrdenCompraBL.Eliminar_Oc_Parcial(idEmpresa, codigoUsuario, op_oc, No_RegistroParcial);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar_Oc_ParcialLinea(int idEmpresa, int codigoUsuario, int id, int op_oc, string No_RegistroParcial)
        {
            try
            {
                OrdenCompraBL objOrdenCompraBL = new OrdenCompraBL();
                objOrdenCompraBL.Eliminar_Oc_ParcialLinea(idEmpresa, codigoUsuario, id, op_oc, No_RegistroParcial);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //SEG IMPORTACION SEGUNDA PARTE
        public List<USP_Sel_Genesys_Oc_SegImpResult> Seleccionar_GenesysOC_SeguimientoLista(int idEmpresa, int codigoUsuario,
            DateTime fechainicial, DateTime fechafinal, string nombreproveedor, string estado, DateTime? fechaingresoini, DateTime? fechaingresofin)
        {
            try
            {
                OrdenCompraBL objOrdenCompraBL = new OrdenCompraBL();
                return objOrdenCompraBL.Seleccionar_GenesysOC_SeguimientoLista(idEmpresa, codigoUsuario, fechainicial, fechafinal, nombreproveedor, estado, fechaingresoini, fechaingresofin).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult> Seleccionar_GenesysOC_ImpParciales(int idEmpresa, int codigoUsuario,
            DateTime fechainicial, DateTime fechafinal, string nombreproveedor, Int32 Id_SegImp)
        {
            try
            {
                OrdenCompraBL objOrdenCompraBL = new OrdenCompraBL();
                return objOrdenCompraBL.Seleccionar_GenesysOC_ImpParciales(idEmpresa, codigoUsuario,
                    fechainicial, fechafinal, nombreproveedor, Id_SegImp).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USP_Sel_Genesys_OC_EstadoResult> Seleccionar_GenesysOC_Estados(int idEmpresa, int codigoUsuario)
        {
            try
            {
                OrdenCompraBL objOrdenCompraBL = new OrdenCompraBL();
                return objOrdenCompraBL.Seleccionar_GenesysOC_Estados(idEmpresa, codigoUsuario).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USP_Sel_Genesys_OC_TipoViaResult> Seleccionar_GenesysOC_TipoVia(int idEmpresa, int codigoUsuario)
        {
            try
            {
                OrdenCompraBL objOrdenCompraBL = new OrdenCompraBL();
                return objOrdenCompraBL.Seleccionar_GenesysOC_TipoVia(idEmpresa, codigoUsuario).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USP_Sel_Genesys_Oc_SegImp_IdSegResult> Seleccionar_GenesysOC_SegImp_IdSeg(int idEmpresa, int codigoUsuario, int idSeg)
        {
            try
            {
                OrdenCompraBL objOrdenCompraBL = new OrdenCompraBL();
                return objOrdenCompraBL.Seleccionar_GenesysOC_SegImp_IdSeg(idEmpresa, codigoUsuario, idSeg).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USP_Sel_Genesys_OC_Imp_SeleccionarOC_IdSegResult> Seleccionar_GenesysOC_OcImp_IdSeg(int idEmpresa, int codigoUsuario, int idSeg)
        {
            try
            {
                OrdenCompraBL objOrdenCompraBL = new OrdenCompraBL();
                return objOrdenCompraBL.Seleccionar_GenesysOC_OcImp_IdSeg(idEmpresa, codigoUsuario, idSeg).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //TERCERA PARTE
        public void Registrar_Seguimiento(int idEmpresa, int codigoUsuario, USP_Sel_Genesys_OC_ImpSegEntidadResult CabSeguimiento, List<OrdenCompraSeguimientoBE> DetSeguimiento, ref decimal? Id_SegImp)
        {
            try
            {
                OrdenCompraBL objOrdenCompraBL = new OrdenCompraBL();
                objOrdenCompraBL.Registrar_Seguimiento(idEmpresa, codigoUsuario, CabSeguimiento, DetSeguimiento, ref Id_SegImp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar_OcImp_Seguimiento(int idEmpresa, int codigoUsuario, Int32 id_seguimiento, string No_RegistroParcial, Int32 Op_OC)
        {
            try
            {
                OrdenCompraBL objOrdenCompraBL = new OrdenCompraBL();
                objOrdenCompraBL.Eliminar_OcImp_Seguimiento(idEmpresa, codigoUsuario, id_seguimiento, No_RegistroParcial, Op_OC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Registrar_OcImpSeg_Liquidacion(int idEmpresa, int codigoUsuario, Int32 id_seguimiento)
        {
            try
            {
                OrdenCompraBL objOrdenCompraBL = new OrdenCompraBL();
                objOrdenCompraBL.Registrar_OcImpSeg_Liquidacion(idEmpresa, codigoUsuario, id_seguimiento);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DocumentosSegImportacion_Registrar(int idEmpresa, int codigoUsuario, Int32 id_seguimiento, string documento, string ruta)
        {
            try
            {
                OrdenCompraBL objOrdenCompraBL = new OrdenCompraBL();
                objOrdenCompraBL.DocumentosSegImportacion_Registrar(idEmpresa, codigoUsuario, id_seguimiento,documento,ruta);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DocumentosSegImportacion_Eliminar(int idEmpresa, int codigoUsuario, Int32 id_seguimiento, string documento)
        {
            try
            {
                OrdenCompraBL objOrdenCompraBL = new OrdenCompraBL();
                objOrdenCompraBL.DocumentosSegImportacion_Eliminar(idEmpresa, codigoUsuario, id_seguimiento, documento);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<USP_SEL_DocumentosSegImportacionResult> DocumentosSegImportacion_Seleccionar(int idEmpresa, int codigoUsuario, Int32 id_seguimiento)
        {
            try
            {
                OrdenCompraBL objOrdenCompraBL = new OrdenCompraBL();
                return objOrdenCompraBL.DocumentosSegImportacion_Seleccionar(idEmpresa, codigoUsuario, id_seguimiento).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
