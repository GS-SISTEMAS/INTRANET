using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GS.SISGEGS.BL;
using System.Configuration;
using GS.SISGEGS.BE;
using System.Transactions;

namespace GS.SISGEGS.DM
{
    public interface IOrdenCompraBL {
        //List<gsOV_ListarResult> OrdenVenta_Listar(int idEmpresa, int codigoUsuario, string ID_Agenda, 
        //    DateTime fechaDesde, DateTime fechaHasta, string ID_Vendedor, bool modificarPedido);
        //void OrdenVenta_Eliminar(int idEmpresa, int codigoUsuario, int idOperacion);
        //decimal OrdenVenta_Registrar(int idEmpresa, int codigoUsuario, gsOV_BuscarCabeceraResult objOrdenVentaCabBE, 
        //    List<gsOV_BuscarDetalleResult> lstProductos, List<GlosaBE> lstImpuestos, decimal? idOperacion, decimal limiteCredito);
        //gsOV_BuscarCabeceraResult OrdenVenta_Buscar(int idEmpresa, int codigoUsuario, int idPedido,
        //    ref List<gsOV_BuscarDetalleResult> objOrdenVentaDet, ref List<gsOV_BuscarImpuestoResult> objOrdenVentaImp,
        //    ref bool? bloqueado, ref string mensajeBloqueado);
        //void OV_TransGratuitas_Aprobar(int idEmpresa, int codigoUsuario, int Op, ref string mensajeError);

        //List<gsOV_Listar_SectoristaResult> OrdenVenta_Listar_Sectorista(int idEmpresa, int codigoUsuario, string ID_Agenda, DateTime fechaDesde, DateTime fechaHasta, string ID_Vendedor, bool modificarPedido, string id_Sectorista);
        List<VBG00536XResult> OrdenCompraListar(int idEmpresa, int codigoUsuario, string ID_Agenda, DateTime FechaDesde, DateTime FechaHasta,
        int EstadoAprobacion);

        void Anular_OC(int idEmpresa, int codigoUsuario, int OP);

        List<USP_Sel_ControlFacturasMaximoResult> ListarControlFacturasMaximo(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int procesados);
        List<USP_Sel_OCResult> ListarOcImportacion(int idEmpresa, int codigoUsuario, DateTime fechainicial, DateTime fechafinal, string nombreproveedor);

        List<USP_Sel_OC_OpResult> Seleccionar_OC_OP(int idEmpresa, int codigoUsuario, int op);
        List<USP_Sel_OC_OpLineaResult> Seleccionar_OC_OPLinea(int idEmpresa, int codigoUsuario, int op);
        List<USP_Sel_OC_OpParcialResult> Seleccionar_OC_OpParcial(int idEmpresa, int codigoUsuario, int op);
        void Registrar_Oc_Parcial(int idEmpresa, int codigoUsuario, List<USP_Sel_Genesys_OC_ImpResult> CabOcParcial, List<USP_Sel_Genesys_OC_ImpLineaResult> DetOcparcial);
        void Eliminar_Oc_Parcial(int idEmpresa, int codigoUsuario, int op_oc, string No_RegistroParcial);
        void Eliminar_Oc_ParcialLinea(int idEmpresa, int codigoUsuario, int id, int op_oc, string No_RegistroParcial);

        //SEGUNDA PARTE
        List<USP_Sel_Genesys_Oc_SegImpResult> Seleccionar_GenesysOC_SeguimientoLista(int idEmpresa, int codigoUsuario, DateTime fechainicial, DateTime fechafinal, string nombreproveedor, string estado, DateTime? fechaingresoini, DateTime? fechaingresofin);
        List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult> Seleccionar_GenesysOC_ImpParciales(int idEmpresa, int codigoUsuario, DateTime fechainicial, DateTime fechafinal, string nombreproveedor, Int32 Id_SegImp);
        List<USP_Sel_Genesys_OC_EstadoResult> Seleccionar_GenesysOC_Estados(int idEmpresa, int codigoUsuario);
        List<USP_Sel_Genesys_OC_TipoViaResult> Seleccionar_GenesysOC_TipoVia(int idEmpresa, int codigoUsuario);
        List<USP_Sel_Genesys_Oc_SegImp_IdSegResult> Seleccionar_GenesysOC_SegImp_IdSeg(int idEmpresa, int codigoUsuario, int idSeg);
        List<USP_Sel_Genesys_OC_Imp_SeleccionarOC_IdSegResult> Seleccionar_GenesysOC_OcImp_IdSeg(int idEmpresa, int codigoUsuario, int idSeg);
        void Registrar_Seguimiento(int idEmpresa, int codigoUsuario, USP_Sel_Genesys_OC_ImpSegEntidadResult CabSeguimiento, List<OrdenCompraSeguimientoBE> DetSeguimiento, ref decimal? Id_SegImp);
        void Eliminar_OcImp_Seguimiento(int idEmpresa, int codigoUsuario, Int32 id_seguimiento, string No_RegistroParcial, Int32 Op_OC);

        void Registrar_OcImpSeg_Liquidacion(int idEmpresa, int codigoUsuario, Int32 id_seguimiento);


        void DocumentosSegImportacion_Registrar(int idEmpresa, int codigoUsuario, Int32 id_seguimiento, string documento, string ruta);
        void DocumentosSegImportacion_Eliminar(int idEmpresa, int codigoUsuario, Int32 id_seguimiento, string documento);
        List<USP_SEL_DocumentosSegImportacionResult> DocumentosSegImportacion_Seleccionar(int idEmpresa, int codigoUsuario, Int32 id_seguimiento);

    }

    public class OrdenCompraBL : IOrdenCompraBL
    {

        #region OLD

        //    public List<gsOV_ListarResult> OrdenVenta_Listar(int idEmpresa, int codigoUsuario, string ID_Agenda, 
        //        DateTime fechaDesde, DateTime fechaHasta, string ID_Vendedor, bool modificarPedido)
        //    {
        //        //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
        //        {
        //            try
        //            {
        //                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
        //                return dcg.gsOV_Listar(ID_Agenda, fechaDesde, fechaHasta, null, ID_Vendedor, null, null, modificarPedido).ToList();
        //            }
        //            catch (Exception ex)
        //            {
        //                dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
        //                dci.SubmitChanges();
        //                throw new ArgumentException("No se puede listar las ordenes de venta de Genesys");
        //            }
        //        }
        //    }

        //    public void OrdenVenta_Eliminar(int idEmpresa, int codigoUsuario, int idOperacion)
        //    {
        //        //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
        //        {
        //            //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
        //            try
        //            {
        //                if (dcg.OV.ToList().Find(x => x.Op == idOperacion).Aprobacion1)
        //                    dcg.VBG01076("OV", idOperacion, codigoUsuario, "1");
        //                dcg.VBG00516(idOperacion);
        //            }
        //            catch (Exception ex)
        //            {
        //                dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
        //                dci.SubmitChanges();
        //                throw new ArgumentException("Error al eliminar el pedido " + idOperacion.ToString() + " de la base de datos.");
        //            }
        //        }
        //    }


        //    public gsOV_BuscarCabeceraResult OrdenVenta_Buscar(int idEmpresa, int codigoUsuario, int idPedido,
        //        ref List<gsOV_BuscarDetalleResult> objOrdenVentaDet, ref List<gsOV_BuscarImpuestoResult> objOrdenVentaImp,
        //        ref bool? bloqueado, ref string mensajeBloqueado)
        //    {
        //        //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
        //        {
        //            gsOV_BuscarCabeceraResult objOrdenVentaCab;
        //            try
        //            {
        //                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
        //                objOrdenVentaCab = dcg.gsOV_BuscarCabecera(idPedido, ref bloqueado, ref mensajeBloqueado).Single();

        //                objOrdenVentaDet = dcg.gsOV_BuscarDetalle(idPedido).ToList();
        //                foreach (gsOV_BuscarDetalleResult objProducto in objOrdenVentaDet)
        //                {
        //                    List<VBG00939Result> lstStocks = dcg.VBG00939(null, objProducto.Item_ID, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList().FindAll(x => x.ID_Almacen == objOrdenVentaCab.ID_Almacen);
        //                    if (lstStocks.Count == 0)
        //                        objProducto.Stock = 0;
        //                    else
        //                        objProducto.Stock = (decimal)lstStocks[0].StockDisponible;
        //                }

        //                objOrdenVentaImp = dcg.gsOV_BuscarImpuesto(idPedido).ToList();
        //                return objOrdenVentaCab;
        //            }
        //            catch (Exception ex)
        //            {
        //                dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
        //                dci.SubmitChanges();
        //                if ((bool)bloqueado)
        //                    throw ex;
        //                else
        //                    throw new ArgumentException("Error consultar por los pedidos en la base de datos.");
        //            }
        //        }
        //    }

        //    public void OV_TransGratuitas_Aprobar(int idEmpresa, int codigoUsuario, int Op, ref string mensajeError)
        //    {
        //        //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
        //        {
        //            try
        //            {
        //                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
        //                dcg.VBG01076("OV", Op, codigoUsuario, "1");
        //            }
        //            catch (Exception ex)
        //            {
        //                dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
        //                dci.SubmitChanges();
        //                throw new ArgumentException("No se puede listar las transferencias gratuitas de Genesys");
        //            }
        //        }
        //    }

        //    public List<gsOV_Listar_SectoristaResult> OrdenVenta_Listar_Sectorista(int idEmpresa, int codigoUsuario, string ID_Agenda, DateTime fechaDesde, DateTime fechaHasta, string ID_Vendedor, bool modificarPedido, string id_Sectorista)
        //    {
        //        //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
        //        {
        //            try
        //            {
        //                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
        //                return dcg.gsOV_Listar_Sectorista(ID_Agenda, fechaDesde, fechaHasta, null, ID_Vendedor, null, null, modificarPedido,id_Sectorista).ToList();
        //            }
        //            catch (Exception ex)
        //            {
        //                dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
        //                dci.SubmitChanges();
        //                throw new ArgumentException("No se puede listar las ordenes de venta de Genesys");
        //            }
        //        }
        //    }

        //    public decimal OrdenVenta_Registrar(int idEmpresa, int codigoUsuario, gsOV_BuscarCabeceraResult objOrdenVentaCabBE,
        //List<gsOV_BuscarDetalleResult> lstProductos, List<GlosaBE> lstImpuestos, decimal? idOperacion, decimal limiteCredito)
        //    {
        //        //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
        //        {
        //            //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
        //            VBG00004Result objEmpresa;
        //            try
        //            {
        //                objEmpresa = dcg.VBG00004().Single();
        //                using (TransactionScope scope = new TransactionScope())
        //                {
        //                    //Desaorueba para que se pueda editar
        //                    if (idOperacion != null && dcg.OV.ToList().Find(x => x.Op == idOperacion).Aprobacion1 && objOrdenVentaCabBE.Id_Pago != 2 &&
        //                        (limiteCredito >= objOrdenVentaCabBE.Total || !objEmpresa.EvaluaLimCredito))
        //                    {
        //                        dcg.VBG01076("OV", idOperacion, codigoUsuario, "1");
        //                    }

        //                    dcg.VBG00522(ref idOperacion, objOrdenVentaCabBE.ID_Agenda, objOrdenVentaCabBE.NoRegistro, objOrdenVentaCabBE.FechaOrden, objOrdenVentaCabBE.FechaDespacho,
        //                        objOrdenVentaCabBE.FechaEntrega, objOrdenVentaCabBE.FechaVigencia, objOrdenVentaCabBE.FechaEmision, objOrdenVentaCabBE.FechaVencimiento, objOrdenVentaCabBE.ID_Envio,
        //                        objOrdenVentaCabBE.ID_AgendaAnexoReferencia, objOrdenVentaCabBE.ID_Vendedor, objOrdenVentaCabBE.ID_Moneda, objOrdenVentaCabBE.Neto, objOrdenVentaCabBE.Dcto,
        //                        objOrdenVentaCabBE.SubTotal, objOrdenVentaCabBE.Impuestos, objOrdenVentaCabBE.Total, objOrdenVentaCabBE.Observaciones, objOrdenVentaCabBE.Prioridad,
        //                        objOrdenVentaCabBE.EntregaParcial, objOrdenVentaCabBE.Estado, objOrdenVentaCabBE.Id_Pago, objOrdenVentaCabBE.ID_AgendaAnexo, objOrdenVentaCabBE.TEA,
        //                        objOrdenVentaCabBE.ID_AgendaDireccion, objOrdenVentaCabBE.ID_AgendaDireccion2, objOrdenVentaCabBE.ModoPago, objOrdenVentaCabBE.NotasDespacho, objOrdenVentaCabBE.ID_CondicionCredito,
        //                        objOrdenVentaCabBE.NroOrdenCliente, objOrdenVentaCabBE.ID_NaturalezaGastoIngreso, objOrdenVentaCabBE.ID_AgendaOrigen, objOrdenVentaCabBE.DireccionOrigenSucursal,
        //                        objOrdenVentaCabBE.DireccionOrigenReferencia, objOrdenVentaCabBE.DireccionOrigenDireccion, objOrdenVentaCabBE.ID_AgendaDestino, objOrdenVentaCabBE.DireccionDestinoSucursal,
        //                        objOrdenVentaCabBE.DireccionDestinoReferencia, objOrdenVentaCabBE.DireccionDestinoDireccion, objOrdenVentaCabBE.ID_TipoDespacho, objOrdenVentaCabBE.ID_TipoPedido,
        //                        objOrdenVentaCabBE.ID_DocumentoVenta, objOrdenVentaCabBE.ID_Almacen, objOrdenVentaCabBE.ID_Transportista, objOrdenVentaCabBE.ID_Chofer, objOrdenVentaCabBE.ID_Vehiculo1,
        //                        objOrdenVentaCabBE.ID_Vehiculo2, objOrdenVentaCabBE.ID_Vehiculo3, objOrdenVentaCabBE.HoraAtencionOpcion1_Desde, objOrdenVentaCabBE.HoraAtencionOpcion1_Hasta,
        //                        objOrdenVentaCabBE.HoraAtencionOpcion2_Desde, objOrdenVentaCabBE.HoraAtencionOpcion2_Hasta, objOrdenVentaCabBE.HoraAtencionOpcion3_Desde, objOrdenVentaCabBE.HoraAtencionOpcion3_Hasta,
        //                        objOrdenVentaCabBE.ID_Sede, objOrdenVentaCabBE.Contacto);

        //                    if (idOperacion == null)
        //                        throw new ArgumentException("No se pudo registrar el pedido, revisar los campos obligatorios.");

        //                    dcg.VBG00523(idOperacion);

        //                    foreach (gsOV_BuscarDetalleResult objProducto in lstProductos)
        //                    {
        //                        decimal? idAmarre = null;
        //                        if (objProducto.ID_Amarre > 0)
        //                            idAmarre = objProducto.ID_Amarre;
        //                        if (objProducto.Estado != 0)
        //                            dcg.VBG00525(ref idAmarre, idOperacion, objProducto.TablaOrigen, objProducto.Linea, objProducto.ID_Item, objProducto.ID_ItemPedido,
        //                                objProducto.Item_ID, objProducto.Cantidad, objProducto.Precio, objProducto.Dcto, objProducto.DctoValor, objProducto.Importe,
        //                                objProducto.ID_ItemAnexo, objProducto.ID_CCosto, objProducto.ID_UnidadGestion, objProducto.ID_UnidadProyecto, objProducto.ID_UnidadInv,
        //                                objProducto.FactorUnidadInv, objProducto.CantidadUnidadInv, objProducto.ID_UnidadDoc, objProducto.CantidadUnidadDoc, objProducto.Observaciones);
        //                        else
        //                            dcg.VBG00526(idAmarre);
        //                    }

        //                    foreach (GlosaBE objImpuesto in lstImpuestos)
        //                    {
        //                        dcg.VBG00524(idOperacion, objImpuesto.IdGlosa, objImpuesto.BaseImponible, objImpuesto.Importe);
        //                    }
        //                    dcg.VBG04091(idOperacion);

        //                    if (limiteCredito >= objOrdenVentaCabBE.Total || !objEmpresa.EvaluaLimCredito)
        //                    {
        //                        //Aprobar el pedido
        //                        if (objOrdenVentaCabBE.Id_Pago != 2)
        //                            dcg.VBG01076("OV", idOperacion, codigoUsuario, "1");

        //                        string moneda = null;
        //                        decimal? total = null;
        //                        bool? ok = false;

        //                        if (objOrdenVentaCabBE.NoRegistro == "0" || objOrdenVentaCabBE.NoRegistro == null)
        //                        {
        //                            dcg.VBG00045(idOperacion, ref moneda, ref total, ref ok, null);
        //                            //Seleccionar impresora 
        //                            VBG00038Result objImpresora = dcg.VBG00038(52).ToList()[0];
        //                            //Actualizar impresora
        //                            string nombreImpresora = null;
        //                            decimal? idDocumento = null;
        //                            string nombreDocumento = null;
        //                            string serieLetra = null;
        //                            decimal? serieNumero = null;
        //                            decimal? numero = null;
        //                            int? lenLetrasSerie = null;
        //                            int? lenNumeroSerie = null;
        //                            int? lenNumero = null;
        //                            decimal? cantidad = null;
        //                            string archivoRpt = null;
        //                            decimal? largo = null;
        //                            decimal? ancho = null;

        //                            dcg.VBG00037(objImpresora.ID_Impresora, ref nombreImpresora, ref idDocumento, ref nombreDocumento, ref serieLetra, ref serieNumero,
        //                                ref numero, ref lenLetrasSerie, ref lenNumeroSerie, ref lenNumero, ref cantidad, ref archivoRpt, ref largo, ref ancho);

        //                            decimal? idImpresora = objImpresora.ID_Impresora;
        //                            dcg.VBG00036(ref idImpresora, nombreImpresora, idDocumento, serieLetra, serieNumero, numero + 1, lenLetrasSerie, lenNumeroSerie,
        //                                lenNumero, cantidad - 1);

        //                            dcg.VBG00040(4, idOperacion, Math.Round((Math.Pow(10, Convert.ToDouble(lenNumeroSerie)) + Convert.ToDouble(serieNumero)), 0).ToString().Substring(1, Convert.ToInt32(lenNumeroSerie)), numero + 1);
        //                        }

        //                        if (lstProductos.FindAll(x => x.Stock - x.Cantidad < 0 && x.Estado == 1).Count > 0 && objOrdenVentaCabBE.Id_Pago != 2)
        //                            dcg.VBG01076("OV", idOperacion, codigoUsuario, "1");
        //                    }
        //                    dcg.SubmitChanges();
        //                    scope.Complete();
        //                    return (decimal)idOperacion;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
        //                dci.SubmitChanges();
        //                throw new ArgumentException("No se pudo registrar el pedido en la base de datos.");
        //            }
        //        }
        //    }


        //        public decimal OrdenCompra_Registrar(int idEmpresa, int codigoUsuario, gsOV_BuscarCabeceraResult objOrdenVentaCabBE,
        //List<gsOV_BuscarDetalleResult> lstProductos, List<GlosaBE> lstImpuestos, decimal? idOperacion, decimal limiteCredito)
        //        {
        //            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
        //            {
        //                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
        //                VBG00004Result objEmpresa;
        //                try
        //                {
        //                    objEmpresa = dcg.VBG00004().Single();
        //                    using (TransactionScope scope = new TransactionScope())
        //                    {
        //                        //Desaorueba para que se pueda editar
        //                        if (idOperacion != null && dcg.OV.ToList().Find(x => x.Op == idOperacion).Aprobacion1 && objOrdenVentaCabBE.Id_Pago != 2 &&
        //                            (limiteCredito >= objOrdenVentaCabBE.Total || !objEmpresa.EvaluaLimCredito))
        //                        {
        //                            dcg.VBG01076("OV", idOperacion, codigoUsuario, "1");
        //                        }

        //                        //dcg.VBG00522(ref idOperacion, objOrdenVentaCabBE.ID_Agenda, objOrdenVentaCabBE.NoRegistro, objOrdenVentaCabBE.FechaOrden, objOrdenVentaCabBE.FechaDespacho,
        //                        //    objOrdenVentaCabBE.FechaEntrega, objOrdenVentaCabBE.FechaVigencia, objOrdenVentaCabBE.FechaEmision, objOrdenVentaCabBE.FechaVencimiento, objOrdenVentaCabBE.ID_Envio,
        //                        //    objOrdenVentaCabBE.ID_AgendaAnexoReferencia, objOrdenVentaCabBE.ID_Vendedor, objOrdenVentaCabBE.ID_Moneda, objOrdenVentaCabBE.Neto, objOrdenVentaCabBE.Dcto,
        //                        //    objOrdenVentaCabBE.SubTotal, objOrdenVentaCabBE.Impuestos, objOrdenVentaCabBE.Total, objOrdenVentaCabBE.Observaciones, objOrdenVentaCabBE.Prioridad,
        //                        //    objOrdenVentaCabBE.EntregaParcial, objOrdenVentaCabBE.Estado, objOrdenVentaCabBE.Id_Pago, objOrdenVentaCabBE.ID_AgendaAnexo, objOrdenVentaCabBE.TEA,
        //                        //    objOrdenVentaCabBE.ID_AgendaDireccion, objOrdenVentaCabBE.ID_AgendaDireccion2, objOrdenVentaCabBE.ModoPago, objOrdenVentaCabBE.NotasDespacho, objOrdenVentaCabBE.ID_CondicionCredito,
        //                        //    objOrdenVentaCabBE.NroOrdenCliente, objOrdenVentaCabBE.ID_NaturalezaGastoIngreso, objOrdenVentaCabBE.ID_AgendaOrigen, objOrdenVentaCabBE.DireccionOrigenSucursal,
        //                        //    objOrdenVentaCabBE.DireccionOrigenReferencia, objOrdenVentaCabBE.DireccionOrigenDireccion, objOrdenVentaCabBE.ID_AgendaDestino, objOrdenVentaCabBE.DireccionDestinoSucursal,
        //                        //    objOrdenVentaCabBE.DireccionDestinoReferencia, objOrdenVentaCabBE.DireccionDestinoDireccion, objOrdenVentaCabBE.ID_TipoDespacho, objOrdenVentaCabBE.ID_TipoPedido,
        //                        //    objOrdenVentaCabBE.ID_DocumentoVenta, objOrdenVentaCabBE.ID_Almacen, objOrdenVentaCabBE.ID_Transportista, objOrdenVentaCabBE.ID_Chofer, objOrdenVentaCabBE.ID_Vehiculo1,
        //                        //    objOrdenVentaCabBE.ID_Vehiculo2, objOrdenVentaCabBE.ID_Vehiculo3, objOrdenVentaCabBE.HoraAtencionOpcion1_Desde, objOrdenVentaCabBE.HoraAtencionOpcion1_Hasta,
        //                        //    objOrdenVentaCabBE.HoraAtencionOpcion2_Desde, objOrdenVentaCabBE.HoraAtencionOpcion2_Hasta, objOrdenVentaCabBE.HoraAtencionOpcion3_Desde, objOrdenVentaCabBE.HoraAtencionOpcion3_Hasta,
        //                        //    objOrdenVentaCabBE.ID_Sede, objOrdenVentaCabBE.Contacto);

        //                        //dcg.VBG00529(ref idOperacion); 



        //                        if (idOperacion == null)
        //                            throw new ArgumentException("No se pudo registrar el pedido, revisar los campos obligatorios.");

        //                        dcg.VBG00523(idOperacion);

        //                        foreach (gsOV_BuscarDetalleResult objProducto in lstProductos)
        //                        {
        //                            decimal? idAmarre = null;
        //                            if (objProducto.ID_Amarre > 0)
        //                                idAmarre = objProducto.ID_Amarre;
        //                            if (objProducto.Estado != 0)
        //                                dcg.VBG00525(ref idAmarre, idOperacion, objProducto.TablaOrigen, objProducto.Linea, objProducto.ID_Item, objProducto.ID_ItemPedido,
        //                                    objProducto.Item_ID, objProducto.Cantidad, objProducto.Precio, objProducto.Dcto, objProducto.DctoValor, objProducto.Importe,
        //                                    objProducto.ID_ItemAnexo, objProducto.ID_CCosto, objProducto.ID_UnidadGestion, objProducto.ID_UnidadProyecto, objProducto.ID_UnidadInv,
        //                                    objProducto.FactorUnidadInv, objProducto.CantidadUnidadInv, objProducto.ID_UnidadDoc, objProducto.CantidadUnidadDoc, objProducto.Observaciones);
        //                            else
        //                                dcg.VBG00526(idAmarre);
        //                        }

        //                        foreach (GlosaBE objImpuesto in lstImpuestos)
        //                        {
        //                            dcg.VBG00524(idOperacion, objImpuesto.IdGlosa, objImpuesto.BaseImponible, objImpuesto.Importe);
        //                        }
        //                        dcg.VBG04091(idOperacion);

        //                        if (limiteCredito >= objOrdenVentaCabBE.Total || !objEmpresa.EvaluaLimCredito)
        //                        {
        //                            //Aprobar el pedido
        //                            if (objOrdenVentaCabBE.Id_Pago != 2)
        //                                dcg.VBG01076("OV", idOperacion, codigoUsuario, "1");

        //                            string moneda = null;
        //                            decimal? total = null;
        //                            bool? ok = false;

        //                            if (objOrdenVentaCabBE.NoRegistro == "0" || objOrdenVentaCabBE.NoRegistro == null)
        //                            {
        //                                dcg.VBG00045(idOperacion, ref moneda, ref total, ref ok, null);
        //                                //Seleccionar impresora 
        //                                VBG00038Result objImpresora = dcg.VBG00038(52).ToList()[0];
        //                                //Actualizar impresora
        //                                string nombreImpresora = null;
        //                                decimal? idDocumento = null;
        //                                string nombreDocumento = null;
        //                                string serieLetra = null;
        //                                decimal? serieNumero = null;
        //                                decimal? numero = null;
        //                                int? lenLetrasSerie = null;
        //                                int? lenNumeroSerie = null;
        //                                int? lenNumero = null;
        //                                decimal? cantidad = null;
        //                                string archivoRpt = null;
        //                                decimal? largo = null;
        //                                decimal? ancho = null;

        //                                dcg.VBG00037(objImpresora.ID_Impresora, ref nombreImpresora, ref idDocumento, ref nombreDocumento, ref serieLetra, ref serieNumero,
        //                                    ref numero, ref lenLetrasSerie, ref lenNumeroSerie, ref lenNumero, ref cantidad, ref archivoRpt, ref largo, ref ancho);

        //                                decimal? idImpresora = objImpresora.ID_Impresora;
        //                                dcg.VBG00036(ref idImpresora, nombreImpresora, idDocumento, serieLetra, serieNumero, numero + 1, lenLetrasSerie, lenNumeroSerie,
        //                                    lenNumero, cantidad - 1);

        //                                dcg.VBG00040(4, idOperacion, Math.Round((Math.Pow(10, Convert.ToDouble(lenNumeroSerie)) + Convert.ToDouble(serieNumero)), 0).ToString().Substring(1, Convert.ToInt32(lenNumeroSerie)), numero + 1);
        //                            }

        //                            if (lstProductos.FindAll(x => x.Stock - x.Cantidad < 0 && x.Estado == 1).Count > 0 && objOrdenVentaCabBE.Id_Pago != 2)
        //                                dcg.VBG01076("OV", idOperacion, codigoUsuario, "1");
        //                        }
        //                        dcg.SubmitChanges();
        //                        scope.Complete();
        //                        return (decimal)idOperacion;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
        //                    dci.SubmitChanges();
        //                    throw new ArgumentException("No se pudo registrar el pedido en la base de datos.");
        //                }
        //            }
        //        }

        #endregion

        #region NEW
        public void Anular_OC(int idEmpresa, int codigoUsuario, int OP)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    dcg.VBG00466X(Convert.ToDecimal(OP));
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al anular la OP " + OP.ToString() + " de la base de datos.");
                }
            }

        }

        public List<VBG00536XResult> OrdenCompraListar(int idEmpresa, int codigoUsuario, string ID_Agenda, DateTime FechaDesde, DateTime FechaHasta,
            int EstadoAprobacion)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.VBG00536X(ID_Agenda, FechaDesde, FechaHasta, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToDecimal(EstadoAprobacion)).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pueden listar as órdenes de compra");
                }
            }
        }

        public List<USP_Sel_ControlFacturasMaximoResult> ListarControlFacturasMaximo(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int procesados)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dci.USP_Sel_ControlFacturasMaximo(FechaDesde, FechaHasta, procesados).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pueden listar los documentos");
                }
            }
        }


        public List<USP_Sel_OCResult> ListarOcImportacion(int idEmpresa, int codigoUsuario, DateTime fechainicial, DateTime fechafinal, string nombreproveedor)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.USP_Sel_OC(fechainicial, fechafinal, nombreproveedor).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pueden listar las órdenes de compra");
                }
            }
        }

        public List<USP_Sel_OC_OpResult> Seleccionar_OC_OP(int idEmpresa, int codigoUsuario, int op)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.USP_Sel_OC_Op(op).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pueden listar las órdenes de compra");
                }
            }
        }

        public List<USP_Sel_OC_OpLineaResult> Seleccionar_OC_OPLinea(int idEmpresa, int codigoUsuario, int op)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.USP_Sel_OC_OpLinea(op).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pueden listar las órdenes de compra");
                }
            }
        }

        public List<USP_Sel_OC_OpParcialResult> Seleccionar_OC_OpParcial(int idEmpresa, int codigoUsuario, int op)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.USP_Sel_OC_OpParcial(op).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pueden listar las órdenes de compra");
                }
            }
        }
        public void Registrar_Oc_Parcial(int idEmpresa, int codigoUsuario, List<USP_Sel_Genesys_OC_ImpResult> CabOcParcial, List<USP_Sel_Genesys_OC_ImpLineaResult> DetOcparcial)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    Boolean eliminado = false;
                    foreach (USP_Sel_Genesys_OC_ImpResult c in CabOcParcial)
                    {
                        if (eliminado == false)
                        {
                            dcg.USP_Del_OC_OpParcialLinea(0, Convert.ToInt32(c.Op_OC), string.Empty);
                            eliminado = true;
                        }

                        dcg.USP_Ins_Genesys_OC_Imp(c.Op_OC, c.No_Registro, c.OpParcial_OC, c.NroParcial, c.No_RegistroParcial, c.EsParcial,
                        c.Neto, c.Subtotal, c.Impuestos, c.Total, c.Observaciones, c.FechaIngresoAlm, c.FlgOcProcesada,
                        c.Id_SegImp);




                        foreach (USP_Sel_Genesys_OC_ImpLineaResult e in DetOcparcial.Where(x => x.Op_OC == c.Op_OC && x.NroParcial == c.NroParcial))
                        {
                            dcg.USP_Ins_Genesys_OC_ImpLinea(e.Id, e.Op_OC, e.OpParcial_OC, e.NroParcial, e.No_RegistroParcial, e.Id_Amarre, e.Linea, e.Id_Item, e.Item_ID, e.Id_UnidadInv, e.Cantidad,
                                e.Precio, e.Importe, e.Observaciones);
                        }
                    }
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException(ex.Message);
                }
                finally
                {
                    dcg.SubmitChanges();
                    dci.SubmitChanges();
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }

            }
        }

        public void Eliminar_Oc_Parcial(int idEmpresa, int codigoUsuario, int op_oc, string No_RegistroParcial)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    dcg.USP_Del_OC_OpParcial(op_oc, No_RegistroParcial);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException(ex.Message);
                }
                finally
                {
                    dcg.SubmitChanges();
                    dci.SubmitChanges();
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }

            }
        }
        public void Eliminar_Oc_ParcialLinea(int idEmpresa, int codigoUsuario, int id, int op_oc, string No_RegistroParcial)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    dcg.USP_Del_OC_OpParcialLinea(id, op_oc, No_RegistroParcial);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException(ex.Message);
                }
                finally
                {
                    dcg.SubmitChanges();
                    dci.SubmitChanges();
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }

        public List<USP_Sel_Genesys_Oc_SegImpResult> Seleccionar_GenesysOC_SeguimientoLista(int idEmpresa, int codigoUsuario, DateTime fechainicial, DateTime fechafinal, string nombreproveedor, string estado, DateTime? fechaingresoini, DateTime? fechaingresofin)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.USP_Sel_Genesys_Oc_SegImp(fechainicial, fechafinal, nombreproveedor, estado, fechaingresoini, fechaingresofin).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pueden listar los seguimientos de importación");
                }
            }
        }

        public List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult> Seleccionar_GenesysOC_ImpParciales(int idEmpresa, int codigoUsuario, DateTime fechainicial, DateTime fechafinal, string nombreproveedor, Int32 Id_SegImp)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.USP_Sel_Genesys_OC_Imp_SeleccionarOC(fechainicial, fechafinal, nombreproveedor, Id_SegImp).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pueden listar los seguimientos de importación");
                }
            }
        }

        public List<USP_Sel_Genesys_OC_EstadoResult> Seleccionar_GenesysOC_Estados(int idEmpresa, int codigoUsuario)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.USP_Sel_Genesys_OC_Estado().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pueden listar los seguimientos de importación");
                }
            }
        }

        public List<USP_Sel_Genesys_OC_TipoViaResult> Seleccionar_GenesysOC_TipoVia(int idEmpresa, int codigoUsuario)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.USP_Sel_Genesys_OC_TipoVia().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pueden listar las Vias");
                }
            }
        }

        //public List<USP_Sel_Genesys_OC_TipoViaResult> Seleccionar_GenesysOC_TipoVia(int idEmpresa, int codigoUsuario)
        //{
        //    using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
        //    ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
        //    {
        //        dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

        //        try
        //        {
        //            return dcg.USP_Sel_Genesys_OC_TipoVia().ToList();
        //        }
        //        catch (Exception ex)
        //        {
        //            dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
        //            dci.SubmitChanges();
        //            throw new ArgumentException("No se pueden listar las Vias");
        //        }
        //    }
        //}

        public List<USP_Sel_Genesys_Oc_SegImp_IdSegResult> Seleccionar_GenesysOC_SegImp_IdSeg(int idEmpresa, int codigoUsuario, int idSeg)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.USP_Sel_Genesys_Oc_SegImp_IdSeg(idSeg).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pueden listar las Vias");
                }
            }
        }

        public List<USP_Sel_Genesys_OC_Imp_SeleccionarOC_IdSegResult> Seleccionar_GenesysOC_OcImp_IdSeg(int idEmpresa, int codigoUsuario, int idSeg)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.USP_Sel_Genesys_OC_Imp_SeleccionarOC_IdSeg(idSeg).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pueden listar las Vias");
                }
            }
        }

        public void Registrar_Seguimiento(int idEmpresa, int codigoUsuario, USP_Sel_Genesys_OC_ImpSegEntidadResult CabSeguimiento, List<OrdenCompraSeguimientoBE> DetSeguimiento, ref decimal? Id_SegImp)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    Id_SegImp = CabSeguimiento.Id_SegImp;
                    dcg.USP_Ins_Genesys_OC_ImpSeg(ref Id_SegImp, CabSeguimiento.Id_Estado, CabSeguimiento.CantidadContenedor, CabSeguimiento.Id_Agente, CabSeguimiento.FechaETDAprox,
                        CabSeguimiento.FechaETD, CabSeguimiento.FechaETA, CabSeguimiento.DiasLibresSE, CabSeguimiento.FechaIngresoAlm, CabSeguimiento.Id_TipoVia, CabSeguimiento.DiasAlmacenaje, CabSeguimiento.NumeroDua,
                        CabSeguimiento.NumeroBL, CabSeguimiento.LinkDua);

                    foreach (OrdenCompraSeguimientoBE c in DetSeguimiento)
                    {
                        dcg.USP_Ins_Genesys_OC_ImpSeg_Linea(c.Op_OC, c.No_RegistroParcial, Id_SegImp);
                    }

                    //dcg.USP_Upd_Genesys_OC_ImpSeg_Contenedor(Id_SegImp);


                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException(ex.Message);
                }
                finally
                {
                    dcg.SubmitChanges();
                    dci.SubmitChanges();
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }

            }
        }
        public void Eliminar_OcImp_Seguimiento(int idEmpresa, int codigoUsuario, Int32 id_seguimiento, string No_RegistroParcial, Int32 Op_OC)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    dcg.USP_Del_Genesys_OC_ImpSeg_OCImp(id_seguimiento, No_RegistroParcial, Op_OC);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException(ex.Message);
                }
                finally
                {
                    dcg.SubmitChanges();
                    dci.SubmitChanges();
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }

        public void Registrar_OcImpSeg_Liquidacion(int idEmpresa, int codigoUsuario, Int32 id_seguimiento)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    dcg.USP_Upd_Genesys_OC_ImpSegLiq(id_seguimiento, codigoUsuario);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException(ex.Message);
                }
                finally
                {
                    dcg.SubmitChanges();
                    dci.SubmitChanges();
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }

        public void DocumentosSegImportacion_Registrar(int idEmpresa, int codigoUsuario,Int32 id_seguimiento, string documento,string ruta)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    dcg.USP_INS_DocumentosSegImportacion(id_seguimiento, documento, ruta);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException(ex.Message);
                }
                finally
                {
                    dcg.SubmitChanges();
                    dci.SubmitChanges();
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }

        public void DocumentosSegImportacion_Eliminar(int idEmpresa, int codigoUsuario, Int32 id_seguimiento, string documento)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    dcg.USP_DEL_DocumentosSegImportacion(id_seguimiento, documento);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException(ex.Message);
                }
                finally
                {
                    dcg.SubmitChanges();
                    dci.SubmitChanges();
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }

        public List<USP_SEL_DocumentosSegImportacionResult> DocumentosSegImportacion_Seleccionar(int idEmpresa, int codigoUsuario, Int32 id_seguimiento)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.USP_SEL_DocumentosSegImportacion(id_seguimiento).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pueden listar los documentos");
                }
            }
        }
        #endregion

    }
}
