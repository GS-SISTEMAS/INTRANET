using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GS.SISGEGS.DM;
using GS.SISGEGS.BE;
using System.Configuration;
using System.Transactions;

namespace GS.SISGEGS.BL
{
    public interface IGuiaBL {
        List<gsGuiaVenta_ListarXPedidoResult> GuiaVenta_ListarxPedido(int idEmpresa, int codigoUsuario, int idPedido);
        List<gsGuiaVentas_listarResult> GuiaVenta_Listar(int idEmpresa, int codigoUsuario, string id_agenda, DateTime fInicio, DateTime fFin, string id_item, int ID_Almacen, int OP);
        List<gsGuiaVenta_ListarxOPResult> GuiaVenta_ListarxOP(int idEmpresa, int codigoUsuario, int idOperacion);
        void GuiaVenta_ActualizarTransporte(int idEmpresa, int codigoUsuario, gsGuiaVenta_ListarxOPResult objGuiaVentaLista, decimal idOperacion);
        void GuiaVenta_FechaInsertar(int idEmpresa, int codigoUsuario, int Op, string transaccion, string id_agenda, DateTime fechaEmision, int id_vehiculo, int estado);
        List<gsGuiaVentas_FechaslistarResult> GuiaVenta_FechasListar(int idEmpresa, int codigoUsuario, string id_agenda, DateTime fInicio, DateTime fFin, string id_item, int ID_Almacen, int OP);
        List<gsGuiaVentas_TransitolistarResult> GuiaVenta_TransitoListar(int idEmpresa, int codigoUsuario, string id_agenda, DateTime fInicio, DateTime fFin, string id_item, int ID_Almacen, int OP, int id_vehiculo);
        void GuiaVenta_FechaTransporte(int idEmpresa, int codigoUsuario, int Op, string transaccion, string id_agenda, int estado);
        List<gsGuiaVentas_FechasGlobalResult> GuiaVenta_ListarGlobal(int idEmpresa, int codigoUsuario, string id_agenda, DateTime fInicio, DateTime fFin, string id_item, int ID_Almacen, int OP);
        void GuiaVenta_Modificar(int idEmpresa, int codigoUsuario, int Op, string transaccion, string id_agenda, int usuarioE, DateTime fechaEmision, int usuarioS, DateTime fechaSeguridad, int usuarioC, DateTime fechaCliente, int estado, int id_vehiculo);

        decimal GuiaVenta_Registrar(int idEmpresa, int codigoUsuario, gsGuia_BuscarCabeceraResult objGuiaVentaCabBE,
           gsGuia_BuscarDetalleResult lstProductos, decimal? idOperacion, List<GuiaVenta_LotesItemsResult> lstLotes);

        gsGuia_BuscarCabeceraResult GuiaVenta_Buscar(int idEmpresa, int codigoUsuario, int idPedido,
        ref List<gsGuia_BuscarDetalleResult> objGuiaVentaDet, ref bool? bloqueado, ref string mensajeBloqueado);

        List<GuiaVenta_LotesItemsResult> GuiaVenta_LotesItemBuscar(int idEmpresa, int codigoUsuario, int op, int item_id);

        List<VBG00971Result> GuiaVenta_BuscarLotesxItem(int idEmpresa, int codigoUsuario, int op, int item_id, int ID_AlmacenAnexo, int id_amarre);

        List<USP_SEL_Guia_VentaImpresaQRResult> GuiaVentaQR_SeleccionarDocumentos(int idEmpresa, int codigoUsuario, DateTime fechainicial, DateTime fechafinal, string nombrecliente, Boolean flgimpreso);
        List<USP_SEL_IMPRESORASQRResult> GuiaVentaQR_SeleccionarImpresoras(int idEmpresa, int codigoUsuario);
        List<USP_SEL_GUIAS_PENDIENTESIMPRESIONResult> GuiaVentaQR_SeleccionarGuiasPendientesImrpesion(int idEmpresa, int codigoUsuario);
        void GuiaVentaQR_ActualizarFlagImpresion(int idEmpresa, int codigoUsuario, string Empresa, Int32 OpGuia, bool flgimpreso);

        decimal GuiaVenta_Registrar_wms(int idEmpresa, int codigoUsuario, gsGuia_BuscarCabeceraResult objGuiaVentaCabBE,
        gsGuia_BuscarDetalleResult lstProductos, decimal? idOperacion, List<GuiaVenta_LotesItemsResult> lstLotes
            , string EmpresaPT, string PedidoServicio, string pedidolote, string pedidoidamarre);

        List<USP_SEL_TrazabilidadDespachoXIDResult> IdTrazabilidadDespacho_Listar_ID(decimal id, int idEmpresa);

        List<USP_SEL_IdTrazabilidadDespachoResult> IdTrazabilidadDespacho_Listar();
    }

    public class GuiaBL : IGuiaBL
    {
        public List<gsGuiaVenta_ListarXPedidoResult> GuiaVenta_ListarxPedido(int idEmpresa, int codigoUsuario, int idPedido)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsGuiaVenta_ListarXPedido(idPedido).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede consultar las guias del pedido Op:"+idPedido.ToString());
                }
            }
        }
        public List<gsGuiaVentas_listarResult> GuiaVenta_Listar(int idEmpresa, int codigoUsuario, string id_agenda, DateTime fInicio, DateTime fFin, string id_item, int  ID_Almacen , int OP)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<gsGuiaVentas_listarResult> lista;
                try
                {
                    lista = new List<gsGuiaVentas_listarResult>();

                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    lista =  dcg.gsGuiaVentas_listar(id_agenda, fInicio, fFin, null, null, ID_Almacen, null, null, null, null, null, null, null, null, null, null).ToList();
                    return lista;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede consultar las guias.");
                }
            }
        }
        public List<gsGuiaVenta_ListarxOPResult> GuiaVenta_ListarxOP(int idEmpresa, int codigoUsuario, int idOperacion)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsGuiaVenta_ListarxOP(idOperacion).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede consultar las guias del pedido Op:" + idOperacion.ToString());
                }
            }
        }
        public void GuiaVenta_ActualizarTransporte(int idEmpresa, int codigoUsuario, gsGuiaVenta_ListarxOPResult  objGuiaVentaLista, decimal idOperacion)
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
                        //Editar

        
                        dcg.gsGuiaVenta_UpdateTransporte(idOperacion, objGuiaVentaLista.id_agenda, objGuiaVentaLista.ID_AgendaDireccion, objGuiaVentaLista.ID_AgendaDireccion,
                            objGuiaVentaLista.DireccionOrigenAgenda, objGuiaVentaLista.DireccionOrigenSucursal, objGuiaVentaLista.DireccionOrigenReferencia, objGuiaVentaLista.DireccionOrigenDireccion,
                            objGuiaVentaLista.DireccionDestinoAgenda, objGuiaVentaLista.DireccionDestinoSucursal, objGuiaVentaLista.DireccionDestinoReferencia, objGuiaVentaLista.DireccionDestinoDireccion,
                            objGuiaVentaLista.ID_Transportista, objGuiaVentaLista.TransportistaRUC, objGuiaVentaLista.ID_Chofer, objGuiaVentaLista.TransportistaChofer,
                            objGuiaVentaLista.TransportistaLicencia, objGuiaVentaLista.ID_Vehiculo, objGuiaVentaLista.TransportistaMarca, objGuiaVentaLista.TransportistaModelo, objGuiaVentaLista.TransportistaCertInscripcion,
                            objGuiaVentaLista.TransportistaPlaca, objGuiaVentaLista.FechaTraslado, objGuiaVentaLista.FechaDespacho, objGuiaVentaLista.FechaEmision);

                        dcg.SubmitChanges();
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo registrar el gsGuiaVenta_UpdateTransporte  en la base de datos.");
                }
            }
        }
        public void GuiaVenta_FechaInsertar(int idEmpresa, int codigoUsuario, int Op, string transaccion, string id_agenda, DateTime fechaEmision, int id_vehiculo, int estado)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    dcg.gsGuiaFechas_insert(Op, transaccion, id_agenda, codigoUsuario, fechaEmision, id_vehiculo, estado);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }
        public List<gsGuiaVentas_FechaslistarResult> GuiaVenta_FechasListar(int idEmpresa, int codigoUsuario, string id_agenda, DateTime fInicio, DateTime fFin, string id_item, int ID_Almacen, int OP)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<gsGuiaVentas_FechaslistarResult> lista;
                try
                {
                    lista = new List<gsGuiaVentas_FechaslistarResult>();

                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    lista = dcg.gsGuiaVentas_Fechaslistar(id_agenda, fInicio, fFin, null, null, ID_Almacen, null, null, null, null, null, null, null, null, null, null).ToList();
                    return lista;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede consultar las guias.");
                }
            }
        }
        public List<gsGuiaVentas_TransitolistarResult> GuiaVenta_TransitoListar(int idEmpresa, int codigoUsuario, string id_agenda, DateTime fInicio, DateTime fFin, string id_item, int ID_Almacen, int OP, int id_vehiculo)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<gsGuiaVentas_TransitolistarResult> lista;
                try
                {
                    lista = new List<gsGuiaVentas_TransitolistarResult>();
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    lista = dcg.gsGuiaVentas_Transitolistar(id_agenda, fInicio, fFin, null, null, null, null, null, null, null, null, null, null, null, null, null, id_vehiculo).ToList();
                    return lista;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede consultar las guias.");
                }
            }
        }
        public void GuiaVenta_FechaTransporte(int idEmpresa, int codigoUsuario, int Op, string transaccion, string id_agenda, int estado)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    dcg.gsGuiaFechas_Transporte(Op, transaccion, id_agenda, codigoUsuario, estado);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }
        public List<gsGuiaVentas_FechasGlobalResult> GuiaVenta_ListarGlobal(int idEmpresa, int codigoUsuario, string id_agenda, DateTime fInicio, DateTime fFin, string id_item, int ID_Almacen, int OP)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<gsGuiaVentas_FechasGlobalResult> lista;
                try
                {
                    lista = new List<gsGuiaVentas_FechasGlobalResult>();
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    lista = dcg.gsGuiaVentas_FechasGlobal(id_agenda, fInicio, fFin, null, null, ID_Almacen, null, null, null, null, null, null, null, null, null, null).ToList();
                    return lista;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede consultar las guias.");
                }
            }
        }
        public void GuiaVenta_Modificar(int idEmpresa, int codigoUsuario, int Op, string transaccion, string id_agenda, int usuarioE, DateTime fechaEmision, int usuarioS, DateTime fechaSeguridad, int usuarioC, DateTime fechaCliente, int estado, int id_vehiculo)
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
                        if(estado == 1)
                        {
                            dcg.gsGuiaFechas_Modificar_S(Op, transaccion, id_agenda, codigoUsuario, usuarioE, usuarioS, fechaEmision, fechaSeguridad, estado, id_vehiculo);
                        }
                        if (estado ==2)
                        {
                            dcg.gsGuiaFechas_Modificar_C(Op, transaccion, id_agenda, codigoUsuario, usuarioE, usuarioS, usuarioC, fechaEmision, fechaSeguridad, fechaCliente, estado, id_vehiculo);
                        }

                        dcg.SubmitChanges();
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo registrar el pedido en la base de datos.");
                }
            }
        }

        public decimal GuiaVenta_Registrar(int idEmpresa, int codigoUsuario, gsGuia_BuscarCabeceraResult objGuiaVentaCabBE,
        gsGuia_BuscarDetalleResult lstProductos, decimal? idOperacion, List<GuiaVenta_LotesItemsResult> lstLotes)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                VBG00004Result objEmpresa;
                List<gsGuia_BuscarDetalleResult> lstProductosUpdate;
                try
                {
                    objEmpresa = dcg.VBG00004().Single();
                    using (TransactionScope scope = new TransactionScope())
                    {

                        dcg.VBG00488(
                            ref idOperacion, objGuiaVentaCabBE.ID_Almacen, 
                            objGuiaVentaCabBE.Fecha, objGuiaVentaCabBE.FechaInicioTraslado,
                            objGuiaVentaCabBE.ID_MotivoTraslado, objGuiaVentaCabBE.Serie, 
                            objGuiaVentaCabBE.Numero, objGuiaVentaCabBE.ID_Agenda, 
                            objGuiaVentaCabBE.ID_Envio, objGuiaVentaCabBE.Observaciones, 

                            objGuiaVentaCabBE.Transportista, objGuiaVentaCabBE.Chofer, 
                            objGuiaVentaCabBE.ID_AgendaAnexo,  objGuiaVentaCabBE.ID_AlmacenAnexo, 
                            objGuiaVentaCabBE.ID_AgendaDireccion, objGuiaVentaCabBE.ID_AgendaDireccion2,
                            objGuiaVentaCabBE.ID_Transportista, objGuiaVentaCabBE.ID_Vehiculo, 
                            objGuiaVentaCabBE.ID_Vehiculo2, objGuiaVentaCabBE.ID_Vehiculo3, 
                            objGuiaVentaCabBE.ID_Chofer, objGuiaVentaCabBE.NotasDespacho, 
                            objGuiaVentaCabBE.ID_CondicionCredito,  objGuiaVentaCabBE.TransportistaRUC,  
                            objGuiaVentaCabBE.TransportistaDomicilio, objGuiaVentaCabBE.TransportistaMarca,  
                            objGuiaVentaCabBE.TransportistaModelo, objGuiaVentaCabBE.TransportistaPlaca, 
                            objGuiaVentaCabBE.TransportistaCertInscripcion, objGuiaVentaCabBE.TransportistaChofer, 
                            objGuiaVentaCabBE.TransportistaLicencia, objGuiaVentaCabBE.CompPagoTipo, 
                            objGuiaVentaCabBE.CompPagoNro, objGuiaVentaCabBE.CompPagoFechaEmision,

                            objGuiaVentaCabBE.ID_AgendaOrigen, objGuiaVentaCabBE.DireccionOrigenSucursal, 
                            objGuiaVentaCabBE.DireccionOrigenReferencia, objGuiaVentaCabBE.DireccionOrigenDireccion,

                            objGuiaVentaCabBE.ID_AgendaDestino, objGuiaVentaCabBE.DireccionDestinoSucursal, 
                            objGuiaVentaCabBE.DireccionDestinoReferencia, objGuiaVentaCabBE.DireccionDestinoDireccion,

                            objGuiaVentaCabBE.HoraAtencionOpcion1_Desde, objGuiaVentaCabBE.HoraAtencionOpcion1_Hasta, 
                            objGuiaVentaCabBE.HoraAtencionOpcion2_Desde, objGuiaVentaCabBE.HoraAtencionOpcion2_Hasta,
                            objGuiaVentaCabBE.HoraAtencionOpcion3_Desde, objGuiaVentaCabBE.HoraAtencionOpcion3_Hasta
                            ) ; 


                        if (idOperacion == null)
                            throw new ArgumentException("No se pudo registrar la guia de venta, revisar los campos obligatorios.");


                        lstProductosUpdate = new List<gsGuia_BuscarDetalleResult>();

                        //foreach (gsGuia_BuscarDetalleResult objProducto in lstProductos)
                        //{
                            gsGuia_BuscarDetalleResult objProducto = lstProductos;

                            decimal? idAmarreOld = null;
                            decimal? idAmarreNew = null;

                            if (objProducto.ID_Amarre > 0)
                            {
                            idAmarreOld = objProducto.ID_Amarre;
                            }

                            if (objProducto.Estado != 0)
                            {
    
                                dcg.VBG00495(
                                    ref idAmarreOld, 
                                    idOperacion, 
                                    objProducto.TablaOrigen, 
                                    objProducto.Linea, 
                                    objProducto.ID_Item, 

                                    objProducto.ID_ItemAnexo,
                                    objProducto.ID_CCosto, 
                                    objProducto.ID_UnidadGestion,
                                    objProducto.ID_UnidadProyecto, 

                                    objProducto.Item_ID, 
                                    
                                    objProducto.CantidadBruta,  // Cantidad
                                    objProducto.Bultos, 
                                    objProducto.Tara, 
                                    objProducto.Cantidad, // Cantidad
                                    objProducto.Ajuste,
                                    objProducto.ID_UnidadInv, 
                                    
                                    objProducto.FactorUnidadInv, 
                                    objProducto.CantidadUnidadInv, // Cantidad
                                    objProducto.ID_UnidadDoc,  
                                    objProducto.CantidadUnidadDoc, // Cantidad
                                    objProducto.ID_UnidadControl, 
                                    objProducto.CantidadUnidadControl,  // Cantidad
                                    objProducto.Observaciones
                                    );

                                objProducto.ID_Amarre = (decimal)idAmarreOld; 
                                lstProductosUpdate.Add(objProducto); 
                            }
                            else
                            {
                                dcg.VBG00490(idAmarreOld);
                            }
                        //}

                        dcg.VBG00969("GuiaVenta", idOperacion);
                        dcg.VBG00972("GuiaVenta", idOperacion);


                        
                        //------------------Lotes---------------------------
                        foreach (gsGuia_BuscarDetalleResult objProductoUp in lstProductosUpdate)
                        {
                            decimal? Lote_ID = null;
                            //decimal? idAmarre = null;
                            decimal ID_Kardex = 0;
                            decimal? ID_Lote = null;
                             

                            if (objProductoUp.ID_Amarre > 0)
                            {
                                idAmarreOld = objProductoUp.ID_Amarre;
                            }

                            ID_Kardex = objProductoUp.Item_ID;


                            foreach(GuiaVenta_LotesItemsResult LoteUp in lstLotes )
                            {
                                if(LoteUp.Item_ID == ID_Kardex)
                                {
                                    idAmarreNew = idAmarreOld;
                                }
                                else
                                {
                                    idAmarreNew = LoteUp.ID_Amarre;
                                }

                                List<VBG00971Result> Lista_LoteVar = dcg.VBG00971(LoteUp.Item_ID, objGuiaVentaCabBE.ID_AlmacenAnexo, "GuiaVenta", 0, "OV", LoteUp.Linea).ToList();

                       
                                int Pendiente = 0;
                                Pendiente = Convert.ToInt32(LoteUp.CantidadUnidadControl);

                                foreach (VBG00971Result Lote in Lista_LoteVar)
                                {
                                    VBG00971Result objLote = new VBG00971Result();
                                    if (Lote.Lote == LoteUp.Lote)
                                    {
                                        int Consumo = 0;
                                        int CantLote = Convert.ToInt32(Lote.CantidadStock);

                                        if (CantLote >= Pendiente)
                                        {
                                            Consumo = Pendiente;
                                            Pendiente = 0;
                                        }
                                        else
                                        {
                                            Consumo = CantLote;
                                            Pendiente = Pendiente - CantLote;
                                        }


                                        objLote = Lote;
                                        ID_Lote = Lote.ID_Lote;
                                        Lote_ID = null; 

                                        dcg.VBG00973(ref Lote_ID, idAmarreNew, ID_Lote, Consumo, false, 0, Consumo, objProductoUp.Observaciones);

                                        if (Pendiente <= 0)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        /////////------------------------------

                        dcg.VBG00871("GuiaVenta", idOperacion);

                        dcg.SubmitChanges();
                        scope.Complete();
                        dcg.Connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    dcg.Connection.Close();
                    throw new ArgumentException(ex.Message.ToString());
                }
                return (decimal)idOperacion;
            }
        }


        public gsGuia_BuscarCabeceraResult GuiaVenta_Buscar(int idEmpresa, int codigoUsuario, int idPedido,
        ref List<gsGuia_BuscarDetalleResult> objGuiaVentaDet, ref bool? bloqueado, ref string mensajeBloqueado)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                gsGuia_BuscarCabeceraResult objGuiaVentaCab;
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    objGuiaVentaCab = dcg.gsGuia_BuscarCabecera(idPedido, ref bloqueado, ref mensajeBloqueado).Single();
                    objGuiaVentaDet = dcg.gsGuia_BuscarDetalle(idPedido, ref bloqueado, ref mensajeBloqueado).ToList();

                    dcg.Connection.Close();

                    return objGuiaVentaCab;
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


        public List<GuiaVenta_LotesItemsResult> GuiaVenta_LotesItemBuscar(int idEmpresa, int codigoUsuario, int op, int item_id )
        {
           using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<GuiaVenta_LotesItemsResult> lstLostesItem = new List<GuiaVenta_LotesItemsResult>(); 
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    lstLostesItem = dcg.GuiaVenta_LotesItems(op, item_id).ToList();
                    dcg.Connection.Close();
                    return lstLostesItem;
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
                }
            }
        }

        public List<VBG00971Result> GuiaVenta_BuscarLotesxItem(int idEmpresa, int codigoUsuario, int op, int item_id, int ID_AlmacenAnexo, int id_amarre )
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<VBG00971Result> lstLostesItem = new List<VBG00971Result>();
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {

                    List<VBG00971Result> Lista_LoteVar = dcg.VBG00971(item_id, ID_AlmacenAnexo, "GuiaVenta", 0, "OV", id_amarre).ToList();

                    dcg.Connection.Close();
                    return Lista_LoteVar;
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
                }
            }
        }


        public List<USP_SEL_Guia_VentaImpresaQRResult> GuiaVentaQR_SeleccionarDocumentos(int idEmpresa, int codigoUsuario, DateTime fechainicial, DateTime fechafinal, string nombrecliente, Boolean flgimpreso)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {

                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {

                    return dci.USP_SEL_Guia_VentaImpresaQR(fechainicial, fechafinal, nombrecliente, flgimpreso).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al consultar las Guia Pendientes.");
                }
            }
        }

        public List<USP_SEL_IMPRESORASQRResult> GuiaVentaQR_SeleccionarImpresoras(int idEmpresa, int codigoUsuario)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {

                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {

                    return dci.USP_SEL_IMPRESORASQR().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al obtener las impresoras.");
                }
            }
        }


        public List<USP_SEL_GUIAS_PENDIENTESIMPRESIONResult> GuiaVentaQR_SeleccionarGuiasPendientesImrpesion(int idEmpresa, int codigoUsuario)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {

                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {

                    return dci.USP_SEL_GUIAS_PENDIENTESIMPRESION().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al obtener las impresoras.");
                }
            }
        }

        public void GuiaVentaQR_ActualizarFlagImpresion(int idEmpresa, int codigoUsuario, string Empresa, Int32 OpGuia, bool flgimpreso)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dci.USP_UPD_GUIAS_PENDIENTESIMPRESION(Empresa, OpGuia, flgimpreso);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al registrar estado de impresion en la Guia");
                }
            }
        }


        public decimal GuiaVenta_Registrar_wms(int idEmpresa, int codigoUsuario, gsGuia_BuscarCabeceraResult objGuiaVentaCabBE,
        gsGuia_BuscarDetalleResult lstProductos, decimal? idOperacion, List<GuiaVenta_LotesItemsResult> lstLotes
            , string EmpresaPT, string PedidoServicio, string pedidolote, string pedidoidamarre)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                VBG00004Result objEmpresa;
                List<gsGuia_BuscarDetalleResult> lstProductosUpdate;
                double error = 0;
                try
                {
                    objEmpresa = dcg.VBG00004().Single();
                    using (TransactionScope scope = new TransactionScope())
                    {

                        dcg.VBG00488(
                            ref idOperacion, objGuiaVentaCabBE.ID_Almacen,
                            objGuiaVentaCabBE.Fecha, objGuiaVentaCabBE.FechaInicioTraslado,
                            objGuiaVentaCabBE.ID_MotivoTraslado, objGuiaVentaCabBE.Serie,
                            objGuiaVentaCabBE.Numero, objGuiaVentaCabBE.ID_Agenda,
                            objGuiaVentaCabBE.ID_Envio, objGuiaVentaCabBE.Observaciones,

                            objGuiaVentaCabBE.Transportista, objGuiaVentaCabBE.Chofer,
                            objGuiaVentaCabBE.ID_AgendaAnexo, objGuiaVentaCabBE.ID_AlmacenAnexo,
                            objGuiaVentaCabBE.ID_AgendaDireccion, objGuiaVentaCabBE.ID_AgendaDireccion2,
                            objGuiaVentaCabBE.ID_Transportista, objGuiaVentaCabBE.ID_Vehiculo,
                            objGuiaVentaCabBE.ID_Vehiculo2, objGuiaVentaCabBE.ID_Vehiculo3,
                            objGuiaVentaCabBE.ID_Chofer, objGuiaVentaCabBE.NotasDespacho,
                            objGuiaVentaCabBE.ID_CondicionCredito, objGuiaVentaCabBE.TransportistaRUC,
                            objGuiaVentaCabBE.TransportistaDomicilio, objGuiaVentaCabBE.TransportistaMarca,
                            objGuiaVentaCabBE.TransportistaModelo, objGuiaVentaCabBE.TransportistaPlaca,
                            objGuiaVentaCabBE.TransportistaCertInscripcion, objGuiaVentaCabBE.TransportistaChofer,
                            objGuiaVentaCabBE.TransportistaLicencia, objGuiaVentaCabBE.CompPagoTipo,
                            objGuiaVentaCabBE.CompPagoNro, objGuiaVentaCabBE.CompPagoFechaEmision,

                            objGuiaVentaCabBE.ID_AgendaOrigen, objGuiaVentaCabBE.DireccionOrigenSucursal,
                            objGuiaVentaCabBE.DireccionOrigenReferencia, objGuiaVentaCabBE.DireccionOrigenDireccion,

                            objGuiaVentaCabBE.ID_AgendaDestino, objGuiaVentaCabBE.DireccionDestinoSucursal,
                            objGuiaVentaCabBE.DireccionDestinoReferencia, objGuiaVentaCabBE.DireccionDestinoDireccion,

                            objGuiaVentaCabBE.HoraAtencionOpcion1_Desde, objGuiaVentaCabBE.HoraAtencionOpcion1_Hasta,
                            objGuiaVentaCabBE.HoraAtencionOpcion2_Desde, objGuiaVentaCabBE.HoraAtencionOpcion2_Hasta,
                            objGuiaVentaCabBE.HoraAtencionOpcion3_Desde, objGuiaVentaCabBE.HoraAtencionOpcion3_Hasta
                            );


                        if (idOperacion == null)
                            throw new ArgumentException("No se pudo registrar la guia de venta, revisar los campos obligatorios.");


                        lstProductosUpdate = new List<gsGuia_BuscarDetalleResult>();

                        //foreach (gsGuia_BuscarDetalleResult objProducto in lstProductos)
                        //{
                        gsGuia_BuscarDetalleResult objProducto = lstProductos;

                        decimal? idAmarreOld = null;
                        decimal? idAmarreNew = null;

                        if (objProducto.ID_Amarre > 0)
                        {
                            idAmarreOld = objProducto.ID_Amarre;
                        }

                        if (objProducto.Estado != 0)
                        {

                            dcg.VBG00495(
                                ref idAmarreOld,
                                idOperacion,
                                objProducto.TablaOrigen,
                                objProducto.Linea,
                                objProducto.ID_Item,

                                objProducto.ID_ItemAnexo,
                                objProducto.ID_CCosto,
                                objProducto.ID_UnidadGestion,
                                objProducto.ID_UnidadProyecto,

                                objProducto.Item_ID,

                                objProducto.CantidadBruta,  // Cantidad
                                objProducto.Bultos,
                                objProducto.Tara,
                                objProducto.Cantidad, // Cantidad
                                objProducto.Ajuste,
                                objProducto.ID_UnidadInv,

                                objProducto.FactorUnidadInv,
                                objProducto.CantidadUnidadInv, // Cantidad
                                objProducto.ID_UnidadDoc,
                                objProducto.CantidadUnidadDoc, // Cantidad
                                objProducto.ID_UnidadControl,
                                objProducto.CantidadUnidadControl,  // Cantidad
                                objProducto.Observaciones
                                );

                            objProducto.ID_Amarre = (decimal)idAmarreOld;
                            lstProductosUpdate.Add(objProducto);
                        }
                        else
                        {
                            dcg.VBG00490(idAmarreOld);
                        }
                        //}

                        dcg.VBG00969("GuiaVenta", idOperacion);
                        dcg.VBG00972("GuiaVenta", idOperacion);



                        //------------------Lotes---------------------------
                        foreach (gsGuia_BuscarDetalleResult objProductoUp in lstProductosUpdate)
                        {
                            decimal? Lote_ID = null;
                            //decimal? idAmarre = null;
                            decimal ID_Kardex = 0;
                            decimal? ID_Lote = null;


                            if (objProductoUp.ID_Amarre > 0)
                            {
                                idAmarreOld = objProductoUp.ID_Amarre;
                            }

                            ID_Kardex = objProductoUp.Item_ID;


                            foreach (GuiaVenta_LotesItemsResult LoteUp in lstLotes)
                            {
                                if (LoteUp.Item_ID == ID_Kardex)
                                {
                                    idAmarreNew = idAmarreOld;
                                }
                                else
                                {
                                    idAmarreNew = LoteUp.ID_Amarre;
                                }

                                List<VBG00971_WMSResult> Lista_LoteVar = dcg.VBG00971_WMS(LoteUp.Item_ID, objGuiaVentaCabBE.ID_AlmacenAnexo, "GuiaVenta", 0, "OV", LoteUp.Linea, LoteUp.Lote).ToList();


                                int Pendiente = 0;
                                Pendiente = Convert.ToInt32(LoteUp.CantidadUnidadControl);

                                foreach (VBG00971_WMSResult Lote in Lista_LoteVar)
                                {
                                    VBG00971_WMSResult objLote = new VBG00971_WMSResult();
                                    if (Lote.Lote == LoteUp.Lote)
                                    {
                                        int Consumo = 0;
                                        int CantLote = Convert.ToInt32(Lote.CantidadStock);

                                        if (CantLote >= Pendiente)
                                        {
                                            Consumo = Pendiente;
                                            Pendiente = 0;
                                        }
                                        else
                                        {
                                            Consumo = CantLote;
                                            Pendiente = Pendiente - CantLote;
                                        }


                                        objLote = Lote;
                                        ID_Lote = Lote.ID_Lote;
                                        Lote_ID = null;

                                        dcg.VBG00973(ref Lote_ID, idAmarreNew, ID_Lote, Consumo, false, 0, Consumo, objProductoUp.Observaciones);

                                        if (Pendiente <= 0)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        /////////------------------------------

                        dcg.VBG00871("GuiaVenta", idOperacion);

                        //Cargar_Pedidos_UpdateEstilos
                        dcg.Cargar_Pedidos_UpdateEstilos(EmpresaPT, PedidoServicio);
                        dcg.gsInterfacePedidos_Update(pedidolote, pedidoidamarre == "" ? "0" : pedidoidamarre, "S", "Se registro correctamente");


                        dcg.SubmitChanges();
                        scope.Complete();
                        dcg.Connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dcg.gsInterfacePedidos_Update(pedidolote, pedidoidamarre == "" ? "0" : pedidoidamarre, "F", "Error: al registrar Guía");
                    dci.SubmitChanges();
                    dcg.Connection.Close();
                    throw new ArgumentException(ex.Message.ToString());
                }
                return (decimal)idOperacion;
            }
        }


        public string Productividad_Almacen_Registrar(string Empresa, int Op)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.USP_INS_Productividad_Almacen(Empresa, Op).ToList()[0].RESULT.ToString();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }

        public List<USP_SEL_Productividad_AlmacenResult> Productividad_Almacen_Listar(string Empresa, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
                using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
                {

                    List<USP_SEL_Productividad_AlmacenResult> listPA = new List<USP_SEL_Productividad_AlmacenResult>();
                    try
                    {
                        listPA = dci.USP_SEL_Productividad_Almacen(Empresa, fechaInicio, fechaFin).ToList();
                        return listPA;
                    }
                    catch (Exception ex)
                    {
                        dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                        dci.SubmitChanges();
                        throw new ArgumentException("Error al consultar Letras");
                    }
                    finally
                    {
                        dci.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USP_SEL_IdTrazabilidadDespachoResult> IdTrazabilidadDespacho_Listar()
        {
            try
            {
                ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
                using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
                {

                    List<USP_SEL_IdTrazabilidadDespachoResult> listPA = new List<USP_SEL_IdTrazabilidadDespachoResult>();
                    try
                    {
                        listPA = dci.USP_SEL_IdTrazabilidadDespacho().ToList();
                        return listPA;
                    }
                    catch (Exception ex)
                    {
                        dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                        dci.SubmitChanges();
                        throw new ArgumentException("Error al consultar Letras");
                    }
                    finally
                    {
                        dci.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<USP_SEL_TrazabilidadDespachoXIDResult> IdTrazabilidadDespacho_Listar_ID(decimal id, int idEmpresa)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + 398).ToString().Substring(1, 4)));
                    List<USP_SEL_TrazabilidadDespachoXIDResult> list = dcg.USP_SEL_TrazabilidadDespachoXID(id).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al consultar.");
                }
                finally
                {

                }
            }
        }

    }
}
