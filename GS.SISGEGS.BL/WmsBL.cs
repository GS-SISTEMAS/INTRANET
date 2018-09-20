using System;
using System.Collections.Generic;
using System.Linq;
using GS.SISGEGS.DM;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface IWmsBL {



        List<gsWMSPendientes_EnvioResult> WmsPendientes_Envio(int idEmpresa, int codigoUsuario);

        List<gsWMSProduccion_PendientesRecibirResult> WmsProduccion_PendientesRecibir(int idEmpresa, int codigoUsuario);

        List<Cargar_Pedidos_ConfirmacionResult> WmsPedidos_Confirmacion_Listar(int idEmpresa, int codigoUsuario, string ruc_empresa);

        List<Cargar_Recepcion_ConfirmacionResult> WmsRecepcion_Confirmacion_Listar(int idEmpresa, int codigoUsuario, string ruc_empresa);

        void WmsPedidosPendientes_Update(int idEmpresa, int codigoUsuario, string Lote, int Id_Amarre, string transferido, string observacion);

        void WmsPedidosPendientes_UpdateEstilos(int idEmpresa, int codigoUsuario, string empresa, string numeroDeAlbaran);

        void Wms_Produccion_UpdateEstilos(int idEmpresa, int codigoUsuario, string empresa, string numeroDeAlbaran); 

        #region guiaCompra
        int WMS_RegistrarGuiaCompra(int idEmpresa, int codigoUsuario, string idAlmacen, string idAgenda);
        int WMS_RegistrarGuiaCompraLinea(int idEmpresa, int codigoUsuario, int Op, int OrdenCompraOp, string item, int unidadesRecibidas, string noLote);
        WMS_ObtenerOrdenCompraResult WMS_ObtenerOrdenCompra(int idEmpresa, int codigoUsuario, string nroRegistro);
        WMS_ObtenerOcLineaResult WMS_ObtenerOCLinea(int idEmpresa, int codigoUsuario, int op, string item);
        WMS_ObtenerLoteResult WMS_ObtenerLote(int idEmpresa, int codigoUsuario, string lote);
        void ProcesarGuiaCompra(int idEmpresa, int codigoUsuario, int op);
        #endregion

        #region Produccion Recibir
        int WMS_Produccion_Recibir(int idEmpresa, int codigoUsuario, string id_almacen, int almacenAnexo);
        int WMS_ProduccionLinea_Recibir(int idEmpresa, int codigoUsuario, int op, int linea, string ID_Item, int kardex, decimal cantidad, string lote, DateTime fechaFabricacion, DateTime fechaVencimiento, string proveedor);
        #endregion
    }
    public class WmsBL : IWmsBL
    {
        #region guiaCompra

        public List<gsWMSPendientes_EnvioResult> WmsPendientes_Envio(int idEmpresa, int codigoUsuario)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.gsWMSPendientes_Envio().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede realizar la consulta de los pendientes");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }


        }

        public List<VBG00518_WMSResult> WmsPedidosPendientes_Envio(int idEmpresa, int codigoUsuario)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    List<VBG00518_WMSResult> lista = new List<VBG00518_WMSResult>();
                    lista = dcg.VBG00518_WMS(null, DateTime.Now, DateTime.Now, null, null, null, null, null, null, null, 0, "").ToList();
                    return lista;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede realizar la consulta de los pendientes");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }


        }

        public void WmsPedidosPendientes_Insertar(int idEmpresa, int codigoUsuario, string NroPedido, string ID_Item, string Lote,
                                                   decimal CantidadPedido, decimal CantidaEntregada,
                                                   decimal CantidadPendiente, string EstadoPedido, int Id_Amarre)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    dcg.Connection.Open();
                    dcg.gsInterfacePedidos_Insertar(int.Parse(NroPedido), ID_Item, Lote, CantidadPedido, CantidaEntregada, CantidadPendiente, EstadoPedido, Id_Amarre);

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede realizar la consulta de los pendientes");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }

        public List<Cargar_Pedidos_ConfirmacionResult> WmsPedidos_Confirmacion_Listar(int idEmpresa, int codigoUsuario, string ruc_empresa)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    List<Cargar_Pedidos_ConfirmacionResult> lista = new List<Cargar_Pedidos_ConfirmacionResult>();
                    lista = dcg.Cargar_Pedidos_Confirmacion(ruc_empresa).ToList();
                    return lista; 
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede realizar la consulta WCF: Cargar_Pedidos_Confirmacion");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }


        }


        public void WmsPedidosPendientes_UpdateEstilos(int idEmpresa, int codigoUsuario, string empresa, string numeroDeAlbaran)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    dcg.Connection.Open();
                    dcg.Cargar_Pedidos_UpdateEstilos(empresa, numeroDeAlbaran);

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede realizar la consultar Cargar_Pedidos_UpdateEstilos");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }


        public List<Cargar_Recepcion_ConfirmacionResult> WmsRecepcion_Confirmacion_Listar(int idEmpresa, int codigoUsuario, string ruc_empresa)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    List<Cargar_Recepcion_ConfirmacionResult> lista = new List<Cargar_Recepcion_ConfirmacionResult>();
                    lista = dcg.Cargar_Recepcion_Confirmacion(ruc_empresa).ToList();
                    return lista;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede realizar la consulta WCF: Cargar_Recepcion_Confirmacion");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }


        }

        public void WmsPedidosPendientes_Update(int idEmpresa, int codigoUsuario,   string Lote,  int Id_Amarre, string transferido,  string observacion)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    dcg.Connection.Open();
                    dcg.gsInterfacePedidos_Update(  Lote, Id_Amarre.ToString(), transferido, observacion);

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede actualizar: gsInterfacePedidos_Update");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }



        #endregion




        #region guiaCompra
        public int WMS_RegistrarGuiaCompra(int idEmpresa, int codigoUsuario, string idAlmacen, string idAgenda) {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                decimal? op = null;
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    dcg.VBG00619(ref op,idAlmacen,DateTime.Now,null,0,null,idAgenda,"WMS",null,null,null,19,1094,1094);
                    return int.Parse(op.ToString());
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo consultar los Egresos Varios en Genesys.");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }

        public int WMS_RegistrarGuiaCompraLinea(int idEmpresa, int codigoUsuario,int Op, int OrdenCompraOp, string item, int unidadesRecibidas, string noLote) {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                decimal? idAmarre = null;
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    var ocLinea =  dcg.WMS_ObtenerOcLinea(OrdenCompraOp, item).FirstOrDefault();
                    var itemG = dcg.WMS_ObtenerItem(int.Parse(ocLinea.Item_ID.ToString())).FirstOrDefault();

                    //REGISTRAR GUIA LINEA 
                    dcg.VBG00616(ref idAmarre, Op, "OC", ocLinea.Linea, item, null, ocLinea.ID_CCosto, ocLinea.ID_UnidadGestion, ocLinea.ID_UnidadProyecto, ocLinea.Item_ID, 
                                unidadesRecibidas, 0, 0, unidadesRecibidas, 0, itemG.Unidad, 1, unidadesRecibidas, itemG.Unidad, unidadesRecibidas, itemG.Unidad, unidadesRecibidas, "WMS");

                    //REGISTRAR GUIA LINEA LOTE 
                    var lote = dcg.WMS_ObtenerLote(noLote).FirstOrDefault();
                    //if (lote == null) {
                        
                    //}

                    decimal? id_lote = lote.ID_Lote;
                    decimal? loteid = null;

                    dcg.VBG01021(ref loteid, idAmarre, ref id_lote, lote.No_Lote, lote.ID_Proveedor, lote.ID_Estado,
                                lote.ID_Calidad, lote.ID_Caracteristica, DateTime.Now, DateTime.Now, unidadesRecibidas,
                                unidadesRecibidas, null, null);

                    return int.Parse(idAmarre.ToString());
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo consultar los Egresos Varios en Genesys.");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }

        public WMS_ObtenerOrdenCompraResult WMS_ObtenerOrdenCompra(int idEmpresa, int codigoUsuario, string nroRegistro) {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.WMS_ObtenerOrdenCompra(nroRegistro).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede realizar la consulta de los pendientes");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }

        public WMS_ObtenerOcLineaResult WMS_ObtenerOCLinea(int idEmpresa, int codigoUsuario, int op, string item) {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.WMS_ObtenerOcLinea(op,item).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede realizar la consulta de los pendientes");
                }
            }
        }

        public WMS_ObtenerLoteResult WMS_ObtenerLote(int idEmpresa, int codigoUsuario, string lote) {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.WMS_ObtenerLote(lote).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede realizar la consulta de los pendientes");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }
        
        public void ProcesarGuiaCompra(int idEmpresa, int codigoUsuario, int op) {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.VBG00871("GuiaCompra", op);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede terminar de procesar la guia de compra ");
                }
            }
        }
        #endregion

        #region Produccion Recibir
        public List<gsWMSProduccion_PendientesRecibirResult> WmsProduccion_PendientesRecibir(int idEmpresa, int codigoUsuario)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                List<gsWMSProduccion_PendientesRecibirResult> lista = new List<gsWMSProduccion_PendientesRecibirResult>();
                try
                {
                    lista = dcg.gsWMSProduccion_PendientesRecibir().ToList();
                    return lista;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede realizar la consulta de los pendientes");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }

        public int WMS_Produccion_Recibir(int idEmpresa, int codigoUsuario, string id_almacen, int almacenAnexo) {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                decimal? op = null;
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    dcg.VBG00599(ref op, id_almacen,almacenAnexo,null,null,DateTime.Now,null,0,0,478,0,null);
                    return int.Parse(op.ToString());
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo consultar los Egresos Varios en Genesys.");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }

        public int WMS_ProduccionLinea_Recibir(int idEmpresa, int codigoUsuario, int op, int linea, string ID_Item, int kardex, decimal cantidad, 
                string lote, DateTime fechaFabricacion, DateTime fechaVencimiento, string proveedor){
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                decimal? idAmarre = null;
                decimal? lote_id = null;
                decimal? id_lote = null;
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    dcg.VBG00597(ref idAmarre,op,"MovProduccion",linea,ID_Item,null,null,"5103","1020",kardex,cantidad,0,0,cantidad,0,"Unidad",1,cantidad,"Unidad",cantidad,"Unidad",cantidad,"");
                    dcg.VBG00969("ParteProduccion", op);
                    dcg.VBG00972("ParteProduccion", op);

                    //GRABAR PARTE PRODUCCION MOVIMIENTO LOTE 

                    dcg.VBG01024(ref lote_id, idAmarre, ref id_lote, lote, proveedor, 1, 1, 1, fechaFabricacion, fechaVencimiento, cantidad, cantidad, "", "");
           
                    //ACTUALIZAR SALDOS STOCK
                    dcg.VBG00871("ParteProduccion", op);

                    return int.Parse(idAmarre.ToString());
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo consultar los Egresos Varios en Genesys.");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }


        public void Wms_Produccion_UpdateEstilos(int idEmpresa, int codigoUsuario, string empresa, string numeroDeAlbaran)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    dcg.Connection.Open();
                    dcg.Cargar_Recepcion_UpdateEstilos(empresa, numeroDeAlbaran);

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede realizar la consultar Cargar_Pedidos_UpdateEstilos");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }


        #endregion
    }
}
 