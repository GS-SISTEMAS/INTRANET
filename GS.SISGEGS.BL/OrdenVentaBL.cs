using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GS.SISGEGS.BL;
using System.Configuration;
using GS.SISGEGS.BE;
using System.Transactions;
using System.Data;


namespace GS.SISGEGS.DM
{
    public interface IOrdenVentaBL {
        List<gsOV_ListarResult> OrdenVenta_Listar(int idEmpresa, int codigoUsuario, string ID_Agenda, 
            DateTime fechaDesde, DateTime fechaHasta, string ID_Vendedor, bool modificarPedido);
        void OrdenVenta_Eliminar(int idEmpresa, int codigoUsuario, int idOperacion, string Comentario);
        decimal OrdenVenta_Registrar(int idEmpresa, int codigoUsuario, gsOV_BuscarCabeceraResult objOrdenVentaCabBE,
           List<gsOV_BuscarDetalleResult> lstProductos, List<GlosaBE> lstImpuestos, decimal? idOperacion, decimal limiteCredito,
           List<gsPedidos_FechasLetrasSelectResult> ListaFechas);

        gsOV_BuscarCabeceraResult OrdenVenta_Buscar(int idEmpresa, int codigoUsuario, int idPedido,
            ref List<gsOV_BuscarDetalleResult> objOrdenVentaDet, ref List<gsOV_BuscarImpuestoResult> objOrdenVentaImp,
            ref bool? bloqueado, ref string mensajeBloqueado);
        void OV_TransGratuitas_Aprobar(int idEmpresa, int codigoUsuario, int Op, ref string mensajeError);

        List<gsOV_Listar_SectoristaResult> OrdenVenta_Listar_Sectorista(int idEmpresa, int codigoUsuario, string ID_Agenda, DateTime fechaDesde, DateTime fechaHasta, string ID_Vendedor, bool modificarPedido, string id_Sectorista, int Estado, int FormaPago);
        void OrdenVenta_Deasaprobar(int idEmpresa, int codigoUsuario, int idOperacion, string Comentario);

        gsOV_BuscarCabeceraResult OrdenVenta_Buscar_Guia(int idEmpresa, int codigoUsuario, int idPedido,
        ref List<gsOV_BuscarDetalleResult> objOrdenVentaDet, ref List<gsOV_BuscarImpuestoResult> objOrdenVentaImp,
        ref bool? bloqueado, ref string mensajeBloqueado);

        List<gsPedidos_FechasLetrasSelectResult> PedidoLetras_Detalle(int idEmpresa, int codigoUsuario, int OpOV, int OpDOC, string Tabla);

        gsPedido_EliminarOP_WMSResult Pedido_Apto_Modificacion(int idEmpresa, int codigoUsuario, int idPedido, int Op);

        void OrdenVenta_Registrar_Fechas(int idEmpresa, int codigoUsuario, DataTable TablaDocs, List<gsPedidos_FechasLetrasSelectResult> ListaFechas);
    }

    public class OrdenVentaBL : IOrdenVentaBL
    {
        public List<gsOV_ListarResult> OrdenVenta_Listar(int idEmpresa, int codigoUsuario, string ID_Agenda, 
            DateTime fechaDesde, DateTime fechaHasta, string ID_Vendedor, bool modificarPedido)
        {
            //////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                     return dcg.gsOV_Listar(ID_Agenda, fechaDesde, fechaHasta, null, ID_Vendedor, null, null, modificarPedido).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede listar las ordenes de venta de Genesys");
                }
                finally
                {
                    dcg.Connection.Close();
                }
            }
        }

        public void OrdenVenta_Eliminar(int idEmpresa, int codigoUsuario, int idOperacion, string Comentario)
        {
            //////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))

            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    if (idOperacion > 0)
                    {
                        //dcg.VBG01076("OV", idOperacion, codigoUsuario, "1");
                        dcg.VBG01076_OV("OV", idOperacion, codigoUsuario, "1", Comentario); //Desaprueba
                        dcg.VBG00516(idOperacion);
                    }
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al eliminar el pedido " + idOperacion.ToString() + " de la base de datos.");
                }
                finally
                {
                    dcg.Connection.Close();
                }
            }
        }


        public decimal OrdenVenta_Registrar(int idEmpresa, int codigoUsuario, gsOV_BuscarCabeceraResult objOrdenVentaCabBE,
  List<gsOV_BuscarDetalleResult> lstProductos, List<GlosaBE> lstImpuestos, decimal? idOperacion, decimal limiteCredito,
  List<gsPedidos_FechasLetrasSelectResult> ListaFechas)


        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))

            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                VBG00004Result objEmpresa;
                try
                {
                    objEmpresa = dcg.VBG00004().Single();
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //Desaorueba para que se pueda editar
                        if (idOperacion != null && dcg.OV.ToList().Find(x => x.Op == idOperacion).Aprobacion1 && objOrdenVentaCabBE.Id_Pago != 2 &&
                            (limiteCredito >= objOrdenVentaCabBE.Total || !objEmpresa.EvaluaLimCredito))
                        {
                            dcg.VBG01076("OV", idOperacion, 1, "1");
                        }

                        if (objOrdenVentaCabBE.ID_CondicionCredito == 0 && objOrdenVentaCabBE.Id_Pago == 1)
                            throw new ArgumentException("No se puede registrar el pedido, se está procesando un pedido con Tipo de Venta: Crédito y Tipo Credito: Contado, presione F5 para refrescar el formulario");

                        dcg.VBG00522(ref idOperacion, objOrdenVentaCabBE.ID_Agenda, objOrdenVentaCabBE.NoRegistro, objOrdenVentaCabBE.FechaOrden, objOrdenVentaCabBE.FechaDespacho,
                            objOrdenVentaCabBE.FechaEntrega, objOrdenVentaCabBE.FechaVigencia, objOrdenVentaCabBE.FechaEmision, objOrdenVentaCabBE.FechaVencimiento, objOrdenVentaCabBE.ID_Envio,
                            objOrdenVentaCabBE.ID_AgendaAnexoReferencia, objOrdenVentaCabBE.ID_Vendedor, objOrdenVentaCabBE.ID_Moneda, objOrdenVentaCabBE.Neto, objOrdenVentaCabBE.Dcto,
                            objOrdenVentaCabBE.SubTotal, objOrdenVentaCabBE.Impuestos, objOrdenVentaCabBE.Total, objOrdenVentaCabBE.Observaciones, objOrdenVentaCabBE.Prioridad,
                            objOrdenVentaCabBE.EntregaParcial, objOrdenVentaCabBE.Estado, objOrdenVentaCabBE.Id_Pago, objOrdenVentaCabBE.ID_AgendaAnexo, objOrdenVentaCabBE.TEA,
                            objOrdenVentaCabBE.ID_AgendaDireccion, objOrdenVentaCabBE.ID_AgendaDireccion2, objOrdenVentaCabBE.ModoPago, objOrdenVentaCabBE.NotasDespacho, objOrdenVentaCabBE.ID_CondicionCredito,
                            objOrdenVentaCabBE.NroOrdenCliente, objOrdenVentaCabBE.ID_NaturalezaGastoIngreso, objOrdenVentaCabBE.ID_AgendaOrigen, objOrdenVentaCabBE.DireccionOrigenSucursal,
                            objOrdenVentaCabBE.DireccionOrigenReferencia, objOrdenVentaCabBE.DireccionOrigenDireccion, objOrdenVentaCabBE.ID_AgendaDestino, objOrdenVentaCabBE.DireccionDestinoSucursal,
                            objOrdenVentaCabBE.DireccionDestinoReferencia, objOrdenVentaCabBE.DireccionDestinoDireccion, objOrdenVentaCabBE.ID_TipoDespacho, objOrdenVentaCabBE.ID_TipoPedido,
                            objOrdenVentaCabBE.ID_DocumentoVenta, objOrdenVentaCabBE.ID_Almacen, objOrdenVentaCabBE.ID_Transportista, objOrdenVentaCabBE.ID_Chofer, objOrdenVentaCabBE.ID_Vehiculo1,
                            objOrdenVentaCabBE.ID_Vehiculo2, objOrdenVentaCabBE.ID_Vehiculo3, objOrdenVentaCabBE.HoraAtencionOpcion1_Desde, objOrdenVentaCabBE.HoraAtencionOpcion1_Hasta,
                            objOrdenVentaCabBE.HoraAtencionOpcion2_Desde, objOrdenVentaCabBE.HoraAtencionOpcion2_Hasta, objOrdenVentaCabBE.HoraAtencionOpcion3_Desde, objOrdenVentaCabBE.HoraAtencionOpcion3_Hasta,
                            objOrdenVentaCabBE.ID_Sede, objOrdenVentaCabBE.Contacto);

                        if (idOperacion == null)
                            throw new ArgumentException("No se pudo registrar el pedido, revisar los campos obligatorios.");

                        dcg.VBG00523(idOperacion);

                        foreach (gsOV_BuscarDetalleResult objProducto in lstProductos)
                        {
                            decimal? idAmarre = null;
                            if (objProducto.ID_Amarre > 0)
                            {
                                idAmarre = objProducto.ID_Amarre;
                            }

                            if (objProducto.Estado != 0)
                            {
                                dcg.VBG00525(ref idAmarre, idOperacion, objProducto.TablaOrigen, objProducto.Linea, objProducto.ID_Item, objProducto.ID_ItemPedido,
                                    objProducto.Item_ID, objProducto.Cantidad, objProducto.Precio, objProducto.Dcto, objProducto.DctoValor, objProducto.Importe,
                                    objProducto.ID_ItemAnexo, objProducto.ID_CCosto, objProducto.ID_UnidadGestion, objProducto.ID_UnidadProyecto, objProducto.ID_UnidadInv,
                                    objProducto.FactorUnidadInv, objProducto.CantidadUnidadInv, objProducto.ID_UnidadDoc, objProducto.CantidadUnidadDoc, objProducto.Observaciones);
                            }
                            else
                            {
                                if (objProducto.ID_Amarre > 0)
                                {
                                    dcg.VBG00526(idAmarre);
                                }
                            }
                        }

                        foreach (GlosaBE objImpuesto in lstImpuestos)
                        {
                            dcg.VBG00524(idOperacion, objImpuesto.IdGlosa, objImpuesto.BaseImponible, objImpuesto.Importe);
                        }

                        dcg.VBG04091(idOperacion);

                        if (limiteCredito >= objOrdenVentaCabBE.Total || !objEmpresa.EvaluaLimCredito)
                        {
                            //Aprobar el pedido
                            if (objOrdenVentaCabBE.Id_Pago != 2)
                                dcg.VBG01076("OV", idOperacion, 1, "1");

                            string moneda = null;
                            decimal? total = null;
                            bool? ok = false;

                            if (objOrdenVentaCabBE.NoRegistro == "0" || objOrdenVentaCabBE.NoRegistro == null)
                            {
                                dcg.VBG00045(idOperacion, ref moneda, ref total, ref ok, null);
                                //Seleccionar impresora 
                                VBG00038Result objImpresora = dcg.VBG00038(52).ToList()[0];
                                //Actualizar impresora
                                string nombreImpresora = null;
                                decimal? idDocumento = null;
                                string nombreDocumento = null;
                                string serieLetra = null;
                                decimal? serieNumero = null;
                                decimal? numero = null;
                                int? lenLetrasSerie = null;
                                int? lenNumeroSerie = null;
                                int? lenNumero = null;
                                decimal? cantidad = null;
                                string archivoRpt = null;
                                decimal? largo = null;
                                decimal? ancho = null;

                                dcg.VBG00037(objImpresora.ID_Impresora, ref nombreImpresora, ref idDocumento, ref nombreDocumento, ref serieLetra, ref serieNumero,
                                    ref numero, ref lenLetrasSerie, ref lenNumeroSerie, ref lenNumero, ref cantidad, ref archivoRpt, ref largo, ref ancho);

                                decimal? idImpresora = objImpresora.ID_Impresora;
                                dcg.VBG00036(ref idImpresora, nombreImpresora, idDocumento, serieLetra, serieNumero, numero + 1, lenLetrasSerie, lenNumeroSerie,
                                    lenNumero, cantidad - 1);

                                dcg.VBG00040(4, idOperacion, Math.Round((Math.Pow(10, Convert.ToDouble(lenNumeroSerie)) + Convert.ToDouble(serieNumero)), 0).ToString().Substring(1, Convert.ToInt32(lenNumeroSerie)), numero + 1);
                            }

                            //if (lstProductos.FindAll(x => x.Stock - x.Cantidad < 0 && x.Estado == 1 && x.Item_ID != KardexFlete).Count > 0 && objOrdenVentaCabBE.Id_Pago != 2)
                            //{
                            //    dcg.VBG01076("OV", idOperacion, 1, "1");
                            //}

                            //--------------Actualizar Fecha Letras 

                            dcg.gsProcesoLetras_EliminarTotal((int)idOperacion, codigoUsuario);
                            gsProcesoLetras_RegistrarResult objProceso = new gsProcesoLetras_RegistrarResult();
                            gsProcesoLetras_Registrar_OLDResult objProcesoOLD = new gsProcesoLetras_Registrar_OLDResult();


                            if (ListaFechas != null)
                            {
                                if (ListaFechas.Count() > 0)
                                {
                                    int ID_PROCESO = 0;
                                    int ID_PROCESO_OLD = 0;
                                    objProceso = dcg.gsProcesoLetras_Registrar(0, codigoUsuario).Single();
                                    objProcesoOLD = dcg.gsProcesoLetras_Registrar_OLD(0, codigoUsuario).Single();

                                    ID_PROCESO = (int)objProceso.Column1;
                                    ID_PROCESO_OLD = (int)objProcesoOLD.Column1;

                                    dcg.gsProcesoLetrasDetalle_Registrar(ID_PROCESO, (int)idOperacion, 0);
                                    dcg.gsProcesoLetrasDetalle_Registrar_OLD(ID_PROCESO_OLD, (int)idOperacion, 0);

                                    foreach (gsPedidos_FechasLetrasSelectResult objFecha in ListaFechas.OrderBy(x => x.Fecha))
                                    {
                                        dcg.gsPedidos_FechasLetrasInsert(ID_PROCESO_OLD, objFecha.Fecha, codigoUsuario);
                                        dcg.gsFactura_FechasLetrasInsert(ID_PROCESO, objFecha.Fecha, 0, codigoUsuario);
                                    }

                                }
                            }

                            //-------------------------------------------------------
                        }
                        dcg.SubmitChanges();
                        scope.Complete();
                        dcg.Connection.Close();

                        return (decimal)idOperacion;
                    }
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo registrar el pedido en la base de datos.");
                }
                finally
                {
                    dcg.Connection.Close();
                }

            }
        }

        public gsOV_BuscarCabeceraResult OrdenVenta_Buscar(int idEmpresa, int codigoUsuario, int idPedido,
            ref List<gsOV_BuscarDetalleResult> objOrdenVentaDet, ref List<gsOV_BuscarImpuestoResult> objOrdenVentaImp,
            ref bool? bloqueado, ref string mensajeBloqueado)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                gsOV_BuscarCabeceraResult objOrdenVentaCab;
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                     objOrdenVentaCab = dcg.gsOV_BuscarCabecera(idPedido, ref bloqueado, ref mensajeBloqueado).Single();

                    objOrdenVentaDet = dcg.gsOV_BuscarDetalle(idPedido).ToList();

                    foreach (gsOV_BuscarDetalleResult objProducto in objOrdenVentaDet)
                    {
                        List<VBG00939Result> lstStocks = dcg.VBG00939(null, objProducto.Item_ID, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList().FindAll(x => x.ID_Almacen == objOrdenVentaCab.ID_Almacen);
                        if (lstStocks.Count == 0)
                            objProducto.Stock = 0;
                        else
                            objProducto.Stock = (decimal)lstStocks[0].StockDisponible;
                    }

                    objOrdenVentaImp = dcg.gsOV_BuscarImpuesto(idPedido).ToList();
                    dcg.Connection.Close(); 

                    return objOrdenVentaCab;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    if ((bool)bloqueado)
                        throw ex;
                    else
                        throw new ArgumentException("Error consultar por los pedidos en la base de datos.");
                }
                finally
                {
                    dcg.Connection.Close();
                }
            }
        }


        public void OV_TransGratuitas_Aprobar(int idEmpresa, int codigoUsuario, int Op, ref string mensajeError)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                  
                    dcg.VBG01076("OV", Op, codigoUsuario, "1");
                    dcg.Connection.Close();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede listar las transferencias gratuitas de Genesys");
                }
                finally
                {
                    dcg.Connection.Close();
                }
            }
        }

        public List<gsOV_Listar_SectoristaResult> OrdenVenta_Listar_Sectorista(int idEmpresa, int codigoUsuario, string ID_Agenda, DateTime fechaDesde, DateTime fechaHasta, string ID_Vendedor, bool modificarPedido, string id_Sectorista, int Estado, int FormaPago)
        {
            List<gsOV_Listar_SectoristaResult> lista = new List<gsOV_Listar_SectoristaResult>(); 

            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    lista = dcg.gsOV_Listar_Sectorista(ID_Agenda, fechaDesde, fechaHasta, null, ID_Vendedor, FormaPago, Estado, modificarPedido,id_Sectorista).ToList();
                    return lista; 

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede listar las ordenes de venta de Genesys");
                }
                finally
                {
                    dcg.Connection.Close();
                }
            }
        }

        public void OrdenVenta_Deasaprobar(int idEmpresa, int codigoUsuario, int idOperacion, string Comentario)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {

                    if (idOperacion > 0)
                    {
                        //dcg.VBG01076("OV", idOperacion, codigoUsuario, "1");
                        dcg.VBG01076_OV("OV", idOperacion, codigoUsuario , "1", Comentario); //Desaprueba
                    }
                    ///dcg.VBG00516(idOperacion);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al deasprobar el pedido " + idOperacion.ToString() + " de la base de datos.");
                }
                finally
                {
                    dcg.Connection.Close();
                }
            }
        }


        public gsOV_BuscarCabeceraResult OrdenVenta_Buscar_Guia(int idEmpresa, int codigoUsuario, int idPedido,
    ref List<gsOV_BuscarDetalleResult> objOrdenVentaDet, ref List<gsOV_BuscarImpuestoResult> objOrdenVentaImp,
    ref bool? bloqueado, ref string mensajeBloqueado)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                gsOV_BuscarCabeceraResult objOrdenVentaCab;
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    objOrdenVentaCab = dcg.gsOV_BuscarCabecera(idPedido, ref bloqueado, ref mensajeBloqueado).Single();
                    objOrdenVentaDet = dcg.gsOV_BuscarDetalle(idPedido).ToList();

                    foreach (gsOV_BuscarDetalleResult objProducto in objOrdenVentaDet)
                    {
                        //List<VBG00939Result> lstStocks = dcg.VBG00939(null, objProducto.Item_ID, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList().FindAll(x => x.ID_Almacen == objOrdenVentaCab.ID_Almacen);
                        List<VBG00939_WMSResult> lstStocks = dcg.VBG00939_WMS(null, objProducto.Item_ID, null, null, null, objOrdenVentaCab.ID_Almacen, null, null, null, null, null, null, null, null, null).ToList();
                        if (lstStocks.Count == 0)
                            objProducto.Stock = 0;
                        else
                            objProducto.Stock = (decimal)lstStocks[0].StockDisponible;
                    }

                    objOrdenVentaImp = dcg.gsOV_BuscarImpuesto(idPedido).ToList();
                    dcg.Connection.Close();

                    return objOrdenVentaCab;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    if ((bool)bloqueado)
                        throw ex;
                    else
                        throw new ArgumentException("Error consultar por los pedidos en la base de datos.");
                }
                finally
                {
                    dcg.Connection.Close();
                }
            }
        }

        public List<gsPedidos_FechasLetrasSelectResult> PedidoLetras_Detalle(int idEmpresa, int codigoUsuario, int OpOV, int OpDOC, string Tabla)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    List<gsPedidos_FechasLetrasSelectResult> lstFechas = dcg.gsPedidos_FechasLetrasSelect(OpOV, OpDOC, Tabla).ToList();
                    return lstFechas;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar por el detalle del pedido en la base de datos.");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }


        public gsPedido_EliminarOP_WMSResult Pedido_Apto_Modificacion(int idEmpresa, int codigoUsuario, int idPedido, int Op)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))

            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                     gsPedido_EliminarOP_WMSResult  Respuesta = dcg.gsPedido_EliminarOP_WMS(Op).Single();
                    return Respuesta;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar por el detalle del pedido en la base de datos.");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }


        public void OrdenVenta_Registrar_Fechas(int idEmpresa, int codigoUsuario, DataTable TablaDocs, List<gsPedidos_FechasLetrasSelectResult> ListaFechas)

        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {

                    using (TransactionScope scope = new TransactionScope())
                    {

                        //--------------Actualizar Fecha Letras 

                        foreach (DataRow row in TablaDocs.Rows)
                        {
                            int OP_DOC = Convert.ToInt32(row["OP_OV"]);
                            dcg.gsRE_ProcesoLetras_EliminarTotal((int)OP_DOC, codigoUsuario);
                        }

                        gsProcesoLetras_RegistrarResult objProceso = new gsProcesoLetras_RegistrarResult();


                        if (ListaFechas != null)
                        {
                            if (ListaFechas.Count() > 0)
                            {
                                int ID_PROCESO = 0;
                                objProceso = dcg.gsProcesoLetras_Registrar(0, codigoUsuario).Single();
                                ID_PROCESO = (int)objProceso.Column1;

                                foreach (DataRow row in TablaDocs.Rows)
                                {
                                    int OP_DOC = Convert.ToInt32(row["OP_OV"]);
                                    dcg.gsProcesoLetrasDetalle_Registrar(ID_PROCESO, OP_DOC, 0);
                                }

                                foreach (gsPedidos_FechasLetrasSelectResult objFecha in ListaFechas.OrderBy(x => x.Fecha))
                                {
                                    //dcg.gsPedidos_FechasLetrasInsert(ID_PROCESO, objFecha.Fecha, codigoUsuario);
                                    dcg.gsFactura_FechasLetrasInsert(ID_PROCESO, objFecha.Fecha, 0, codigoUsuario);
                                }

                            }
                        }
                        //--------------------------------------

                        dcg.SubmitChanges();
                        scope.Complete();
                        dcg.Connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo registrar el pedido en la base de datos.");
                }
                finally
                {
                    dcg.Connection.Close();
                }

            }
        }


    }
}
