using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GS.SISGEGS.DM;
using GS.SISGEGS.BE;
using System.Configuration;
using System.Transactions;

namespace GS.SISGEGS.BL
{
    public interface IPedidoBL {
        List<VBG03630Result> Pedido_ListarTipo(int idEmpresa, int codigoUsuario);
        List<gsPedido_ListarResult> Pedido_Listar(int? idEmpresa, int codigoUsuario, string idAgenda, DateTime fechaInicio, 
            DateTime fechaFinal, int? idDocumento, string idVendedor, int? idFormaPago, decimal? estadoAprobacion, ref bool superUsuario);
        void Pedido_Registrar(int idEmpresa, int codigoUsuario, PedidoCabBE objPedidoCabBE, List<PedidoDetBE> lstProductos, 
            List<GlosaBE> lstImpuestos, decimal? idOperacion, string password, decimal limiteCredito);
        gsPedido_BuscarCabeceraResult Pedido_BuscarCabecera(int idEmpresa, int codigoUsuario, int idPedido);
        List<gsPedido_BuscarDetalleResult> Pedido_BuscarDetalle(int idEmpresa, int codigoUsuario, int idPedido, decimal? idAlmacen);
        void Pedido_Eliminar(int idEmpresa, int codigoUsuario, int idOperacion, string password);
        void Pedido_RegistrarAmarre(int idEmpresa, int codigoUsuario, List<gsPedidoDetalle> lstPedidoDetalle, int diasCredito); 

        void Pedido_Aprobar(int idEmpresa, int codigoUsuario, int idPedido, int op, string idSectorista, bool aproCred, string motivo);
        void Pedido_DesAprobar(int idEmpresa, int codigoUsuario, int idPedido, int op, string idSectorista, bool aproCred);
        void gsDocVentaAprobacion_Registrar(int idEmpresa, int idPedido, int op, string id_Agenda, int codigoUsuario);
        VerificarExisteDocVentaResult VerificarExisteDocVenta(int idEmpresa, int codigoUsuario, string idAgenda);

        List<gsPedidos_FechasLetrasSelectResult> PedidoLetras_Detalle(int idEmpresa, int codigoUsuario, int OpOV, int OpDOC, string Tabla);

    }

    public class PedidoBL : IPedidoBL
    {
        public gsPedido_BuscarCabeceraResult Pedido_BuscarCabecera(int idEmpresa, int codigoUsuario, int idPedido)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                bool? bloqueado = false;
                string mensajeBloqueado = "";
                gsPedido_BuscarCabeceraResult objPedido;
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                   objPedido = dcg.gsPedido_BuscarCabecera(idPedido, ref bloqueado, ref mensajeBloqueado).Single();
                    if ((bool)bloqueado)
                        throw new ArgumentException("Error: El cliente se encuentra bloqueado.");
                    return objPedido;
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
                    dci.Connection.Close();
                }
            }
        }

        public List<gsPedido_BuscarDetalleResult> Pedido_BuscarDetalle(int idEmpresa, int codigoUsuario, int idPedido, decimal? idAlmacen)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    List<gsPedido_BuscarDetalleResult> lstProductos = dcg.gsPedido_BuscarDetalle(idPedido).ToList();
                    foreach (gsPedido_BuscarDetalleResult objProducto in lstProductos) {
                        List<VBG00939Result> lstStocks = dcg.VBG00939(null, objProducto.Item_ID, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList().FindAll(x => x.ID_Almacen == idAlmacen);
                        if (lstStocks.Count == 0)
                            objProducto.Stock = 0;
                        else
                            objProducto.Stock = (decimal)lstStocks[0].StockDisponible;
                    }
                    return lstProductos;
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

        public void Pedido_Eliminar(int idEmpresa, int idOperacion, int codigoUsuario, string password)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    if (dcg.OV.ToList().Find(x => x.Op == idOperacion).Aprobacion1)
                        dcg.VBG01076("OV", idOperacion, codigoUsuario, "1");
                    dcg.VBG00516(idOperacion);
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
                    dci.Connection.Close();
                }
            }
        }

        public List<gsPedido_ListarResult> Pedido_Listar(int? idEmpresa, int codigoUsuario, string idAgenda, DateTime fechaInicio, DateTime fechaFinal, int? idDocumento, string idVendedor, int? idFormaPago, decimal? estadoAprobacion, ref bool superUsuario)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    if (dcg.gsUsuarioPedido.ToList().FindAll(x => x.idUsuario == codigoUsuario).Count > 0)
                        superUsuario = true;
                    else
                        superUsuario = false;
                    return dcg.gsPedido_Listar(idAgenda, fechaInicio, fechaFinal, idDocumento, idVendedor, idFormaPago, estadoAprobacion).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar por los pedidos en la base de datos.");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }

        public List<VBG03630Result> Pedido_ListarTipo(int idEmpresa, int codigoUsuario)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.VBG03630().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar por los tipos de pedido en la base de datos.");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }

        public void Pedido_Registrar(int idEmpresa, int codigoUsuario, PedidoCabBE objPedidoCabBE, List<PedidoDetBE> lstProductos, 
            List<GlosaBE> lstImpuestos, decimal? idOperacion, string password, decimal limiteCredito)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    //Desaorueba para que se pueda editar
                    if (idOperacion != null && dcg.OV.ToList().Find(x => x.Op == idOperacion).Aprobacion1 && objPedidoCabBE.IdPago != 2 && limiteCredito >= objPedidoCabBE.Total)
                    {
                        dcg.VBG01076("OV", idOperacion, codigoUsuario, "1");
                    }

                    dcg.VBG00522(ref idOperacion, objPedidoCabBE.IdAgenda, objPedidoCabBE.NroRegistro, objPedidoCabBE.FechaOrden, objPedidoCabBE.FechaDespacho,
                        objPedidoCabBE.FechaEntrega, objPedidoCabBE.FechaVigencia, objPedidoCabBE.Fecha, objPedidoCabBE.FechaVencimiento, objPedidoCabBE.IdEnvio,
                        objPedidoCabBE.IdAgenciaAnexoReferencia, objPedidoCabBE.IdVendedor, objPedidoCabBE.IdMoneda, objPedidoCabBE.Neto, objPedidoCabBE.Descuento,
                        objPedidoCabBE.Subtotal, objPedidoCabBE.Impuestos, objPedidoCabBE.Total, objPedidoCabBE.Observaciones, objPedidoCabBE.Prioridad,
                        objPedidoCabBE.EntregaParcial, objPedidoCabBE.Estado, objPedidoCabBE.IdPago, objPedidoCabBE.IdAgenciaAnexo, objPedidoCabBE.TEA,
                        objPedidoCabBE.IdAgenciaDireccion1, objPedidoCabBE.IdAgenciaDireccion2, objPedidoCabBE.ModoPago, objPedidoCabBE.NotasDespacho, objPedidoCabBE.IdCondicionCredito,
                        objPedidoCabBE.NroOrdenCliente, objPedidoCabBE.IdNaturalezaGasto, objPedidoCabBE.IdAgendaOrigen, objPedidoCabBE.IdSucursalOrigen,
                        objPedidoCabBE.IdReferenciaOrigen, objPedidoCabBE.IdDireccionOrigen, objPedidoCabBE.IdAgendaDestino, objPedidoCabBE.IdSucursalDestino,
                        objPedidoCabBE.IdReferenciaDestino, objPedidoCabBE.IdDireccionDestino, objPedidoCabBE.IdTipoDespacho, objPedidoCabBE.IdTipoPedido,
                        objPedidoCabBE.IdDocumentoVenta, objPedidoCabBE.IdAlmacen, objPedidoCabBE.IdTransportista, objPedidoCabBE.IdChofer, objPedidoCabBE.IdVehiculo1,
                        objPedidoCabBE.IdVehiculo2, objPedidoCabBE.IdVehiculo3, objPedidoCabBE.HoraAtencionOpcional1_Desde, objPedidoCabBE.HoraAtencionOpcional1_Hasta,
                        objPedidoCabBE.HoraAtencionOpcional2_Desde, objPedidoCabBE.HoraAtencionOpcional2_Hasta, objPedidoCabBE.HoraAtencionOpcional3_Desde, objPedidoCabBE.HoraAtencionOpcional3_Hasta,
                        objPedidoCabBE.IdSede, objPedidoCabBE.IdContacto);

                    if (idOperacion == null)
                        throw new ArgumentException("No se pudo registrar el pedido, revisar los campos obligatorios.");

                    dcg.VBG00523(idOperacion);

                    foreach (PedidoDetBE objPedidoDetBE in lstProductos)
                    {
                        decimal? idAmarre = null;
                        if (objPedidoDetBE.IdAmarre > 0)
                            idAmarre = objPedidoDetBE.IdAmarre;
                        if (objPedidoDetBE.Estado)
                            dcg.VBG00525(ref idAmarre, idOperacion, objPedidoDetBE.TablaOrigen, objPedidoDetBE.Linea, objPedidoDetBE.ID_Item, objPedidoDetBE.ID_ItemPedido,
                                objPedidoDetBE.Item_ID, objPedidoDetBE.Cantidad, objPedidoDetBE.Precio, objPedidoDetBE.Dcto, objPedidoDetBE.DctoValor, objPedidoDetBE.Importe,
                                objPedidoDetBE.ID_ItemAnexo, objPedidoDetBE.ID_CCosto, objPedidoDetBE.ID_UnidadGestion, objPedidoDetBE.ID_UnidadProyecto, objPedidoDetBE.ID_UnidadInv,
                                objPedidoDetBE.FactorUnidadInv, objPedidoDetBE.CantidadUnidadInv, objPedidoDetBE.ID_UnidadDoc, objPedidoDetBE.CantidadUnidadDoc, objPedidoDetBE.Observaciones);
                        else
                            dcg.VBG00526(idAmarre);
                    }

                    foreach (GlosaBE objImpuesto in lstImpuestos)
                    {
                        dcg.VBG00524(idOperacion, objImpuesto.IdGlosa, objImpuesto.BaseImponible, objImpuesto.Importe);
                    }
                    dcg.VBG04091(idOperacion);

                    if (limiteCredito >= objPedidoCabBE.Total) {
                        //Aprobar el pedido
                        if (objPedidoCabBE.IdPago != 2)
                            dcg.VBG01076("OV", idOperacion, codigoUsuario, "1");

                        //
                        string moneda = null;
                        decimal? total = null;
                        bool? ok = false;

                        if (objPedidoCabBE.NroRegistro == "0" || objPedidoCabBE.NroRegistro == null)
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
                            decimal? longitud = null;
                            decimal? ancho = null;

                            dcg.VBG00037(objImpresora.ID_Impresora, ref nombreImpresora, ref idDocumento, ref nombreDocumento, ref serieLetra, ref serieNumero,
                                ref numero, ref lenLetrasSerie, ref lenNumeroSerie, ref lenNumero, ref cantidad, ref archivoRpt, ref longitud, ref ancho);

                            decimal? idImpresora = objImpresora.ID_Impresora;
                            dcg.VBG00036(ref idImpresora, nombreImpresora, idDocumento, serieLetra, serieNumero, numero + 1, lenLetrasSerie, lenNumeroSerie,
                                lenNumero, cantidad - 1);

                            dcg.VBG00040(4, idOperacion, Math.Round((Math.Pow(10, Convert.ToDouble(lenNumeroSerie)) + Convert.ToDouble(serieNumero)), 0).ToString().Substring(1, Convert.ToInt32(lenNumeroSerie)), numero + 1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException("No se pudo registrar el pedido en la base de datos.");
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

        public void Pedido_RegistrarAmarre(int idEmpresa, int codigoUsuario, List<gsPedidoDetalle> lstPedidoDetalle, int diasCredito)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                int idPedido;
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (dcg.gsPedidoDetalle.ToList().FindAll(x => x.Op == lstPedidoDetalle[0].Op).Count == 0)
                        {
                            idPedido = dcg.gsPedidoCabecera_Registrar(codigoUsuario);
                        }
                        else
                        {
                            idPedido = dcg.gsPedidoDetalle.ToList().Find(x => x.Op == lstPedidoDetalle[0].Op).idPedido;
                        }

                        foreach (gsPedidoDetalle objPedidoDetalle in lstPedidoDetalle)
                        {
                            if (diasCredito != 0)
                            {
                                // Registrar CondicionCredito
                                VBG02967_CondicionCreditoResult Objeto = new VBG02967_CondicionCreditoResult();
                                int ID_CondicionCredito = 0;
                                Objeto = dcg.VBG02967_CondicionCredito(ID_CondicionCredito, objPedidoDetalle.PlanLetras, diasCredito, 2, 1).Single();
                                ID_CondicionCredito = (int)Objeto.Column1;

                                // Actualizar OV 
                                dcg.gsOV_CondicionCredito((int)objPedidoDetalle.Op, ID_CondicionCredito);
                            }

                            // Seguimiento OV 
                            dcg.gsPedidoDetalle_Registrar(idPedido, objPedidoDetalle.Op, objPedidoDetalle.Motivo, objPedidoDetalle.documentoPendiente, objPedidoDetalle.limiteCredito, objPedidoDetalle.sinStock, objPedidoDetalle.PlanLetras);

                        }


                        dcg.SubmitChanges();
                        scope.Complete();
                        dcg.Connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo registrar el pedido en la base de datos de Genesys.");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }

        public void Pedido_Aprobar(int idEmpresa, int codigoUsuario, int idPedido, int op, string idSectorista, bool aproCred, string motivo)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (dcg.gsPedidoDetalle.ToList().FindAll(x => x.Op == op).Count == 0)
                            idPedido = dcg.gsPedidoCabecera_Registrar(codigoUsuario);
                        else
                            idPedido = dcg.gsPedidoDetalle.ToList().Find(x => x.Op == op).idPedido;

                        dcg.gsPedidoCabecera_Aprobar(idPedido, op, idSectorista, aproCred, codigoUsuario, motivo);

                        dcg.SubmitChanges();
                        scope.Complete();
                        dcg.Connection.Close();
                    }
                    
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo registrar el pedido en la base de datos de Genesys.");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close(); 
                }

            }
        }

        public void Pedido_DesAprobar(int idEmpresa, int codigoUsuario, int idPedido, int op, string idSectorista, bool aproCred)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    dcg.gsPedidoCabecera_DesAprobar(idPedido, op, idSectorista, aproCred, codigoUsuario);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo registrar el pedido en la base de datos de Genesys.");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }

        public void gsDocVentaAprobacion_Registrar(int idEmpresa, int idPedido, int op, string id_Agenda, int codigoUsuario)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    dcg.gsDocVentaAprobacion_Registrar(idPedido,op,id_Agenda,codigoUsuario);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo registrar la aprobaciòn del documento en la base de datos de Genesys.");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }

        public VerificarExisteDocVentaResult VerificarExisteDocVenta(int idEmpresa, int codigoUsuario, string idAgenda) {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))

            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.VerificarExisteDocVenta(idAgenda).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo verificar la existencia del documento de venta en la base de datos de Genesys.");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }

        public List<gsPedidos_FechasLetrasSelectResult> PedidoLetras_Detalle(int idEmpresa, int codigoUsuario, int OpOV, int OpDOC, string Tabla)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
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


    }
}
