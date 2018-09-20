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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "GuiaWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione GuiaWCF.svc o GuiaWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class GuiaWCF : IGuiaWCF
    {
         public List<gsGuiaVenta_ListarXPedidoResult> GuiaVenta_ListarxPedido(int idEmpresa, int codigoUsuario, int idPedido)
        {
            try {
                GuiaBL objGuiaBL = new GuiaBL();
                return objGuiaBL.GuiaVenta_ListarxPedido(idEmpresa, codigoUsuario, idPedido);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

         public List<gsGuiaVentas_listarResult> GuiaVenta_Listar(int idEmpresa, int codigoUsuario, string id_agenda, DateTime fInicio, DateTime fFin, string id_item, int ID_Almacen, int OP)
        {
            List<gsGuiaVentas_listarResult> lista;

            try
            {
                GuiaBL objGuiaBL = new GuiaBL();
                lista = objGuiaBL.GuiaVenta_Listar(idEmpresa, codigoUsuario, id_agenda, fInicio, fFin, id_item, ID_Almacen, OP);
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsGuiaVenta_ListarxOPResult> GuiaVenta_ListarxOP(int idEmpresa, int codigoUsuario, int idOperacion)
        {
            try
            {
                GuiaBL objGuiaBL = new GuiaBL();
                return objGuiaBL.GuiaVenta_ListarxOP(idEmpresa, codigoUsuario, idOperacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuiaVenta_ActualizarTransporte(int idEmpresa, int codigoUsuario, gsGuiaVenta_ListarxOPResult objGuiaVentaLista, decimal idOperacion)
        {
            try
            {
                GuiaBL objGuiaBL = new GuiaBL();
                objGuiaBL.GuiaVenta_ActualizarTransporte(idEmpresa, codigoUsuario, objGuiaVentaLista, idOperacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuiaVenta_FechaInsertar(int idEmpresa, int codigoUsuario, int Op, string transaccion, string id_agenda, DateTime fechaEmision, int id_vehiculo, int estado)
        {
            try
            {
                GuiaBL objGuiaBL = new GuiaBL();
                objGuiaBL.GuiaVenta_FechaInsertar(idEmpresa, codigoUsuario, Op, transaccion, id_agenda, fechaEmision, id_vehiculo, estado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsGuiaVentas_FechaslistarResult> GuiaVenta_FechasListar(int idEmpresa, int codigoUsuario, string id_agenda, DateTime fInicio, DateTime fFin, string id_item, int ID_Almacen, int OP)
        {
            List<gsGuiaVentas_FechaslistarResult> lista;

            try
            {
                GuiaBL objGuiaBL = new GuiaBL();
                lista = objGuiaBL.GuiaVenta_FechasListar(idEmpresa, codigoUsuario, id_agenda, fInicio, fFin, id_item, ID_Almacen, OP);
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsGuiaVentas_TransitolistarResult> GuiaVenta_TransitoListar(int idEmpresa, int codigoUsuario, string id_agenda, DateTime fInicio, DateTime fFin, string id_item, int ID_Almacen, int OP, int id_vehiculo)
        {
            List<gsGuiaVentas_TransitolistarResult> lista;
            try
            {
                GuiaBL objGuiaBL = new GuiaBL();
                lista = objGuiaBL.GuiaVenta_TransitoListar(idEmpresa, codigoUsuario, id_agenda, fInicio, fFin, id_item, ID_Almacen, OP, id_vehiculo);
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuiaVenta_FechaTransporte(int idEmpresa, int codigoUsuario, int Op, string transaccion, string id_agenda, int estado)
        {
            try
            {
                GuiaBL objGuiaBL = new GuiaBL();
                objGuiaBL.GuiaVenta_FechaTransporte(idEmpresa, codigoUsuario, Op, transaccion, id_agenda, estado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsGuiaVentas_FechasGlobalResult> GuiaVenta_ListarGlobal(int idEmpresa, int codigoUsuario, string id_agenda, DateTime fInicio, DateTime fFin, string id_item, int ID_Almacen, int OP)
        {
            List<gsGuiaVentas_FechasGlobalResult> lista;
            try
            {
                GuiaBL objGuiaBL = new GuiaBL();
                lista = objGuiaBL.GuiaVenta_ListarGlobal(idEmpresa, codigoUsuario, id_agenda, fInicio, fFin, id_item, ID_Almacen, OP);
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuiaVenta_Modificar(int idEmpresa, int codigoUsuario, int Op, string transaccion, string id_agenda, int usuarioE, DateTime fechaEmision, int usuarioS, DateTime fechaSeguridad, int usuarioC, DateTime fechaCliente, int estado, int id_vehiculo)
        {
            try
            {
                GuiaBL objGuiaBL = new GuiaBL();
                objGuiaBL.GuiaVenta_Modificar(idEmpresa, codigoUsuario, Op, transaccion, id_agenda, usuarioE, fechaEmision, usuarioS, fechaSeguridad, usuarioC, fechaCliente, estado, id_vehiculo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuiaVenta_Registrar(int idEmpresa, int codigoUsuario, gsGuia_BuscarCabeceraResult objGuiaVentaCabBE,
                gsGuia_BuscarDetalleResult lstProductos, decimal? idOperacion, List<GuiaVenta_LotesItemsResult> lstLotes)
        {
            decimal neto, descuento, impuesto;
            List<gsGuia_BuscarDetalleResult> lst;
            
            try
            {
                //List<gsGuiaVentaDetalle> lstPedidoDet = new List<gsPedidoDetalle>();
                GuiaBL objGuiaVentaBL = new GuiaBL();
                PedidoBL objPedidoBL = new PedidoBL();


                gsPedidoDetalle objPedidoDetalle = new gsPedidoDetalle();

                objPedidoDetalle.Op = objGuiaVentaBL.GuiaVenta_Registrar(idEmpresa, codigoUsuario, objGuiaVentaCabBE, lstProductos, idOperacion, lstLotes);
                
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public gsGuia_BuscarCabeceraResult GuiaVenta_Buscar(int idEmpresa, int codigoUsuario, int idPedido,
        ref List<gsGuia_BuscarDetalleResult> objOrdenVentaDet,  ref bool? bloqueado, ref string mensajeBloqueado)
        {
            try
            {
                GuiaBL objGuiaVentaBL = new GuiaBL();
                return objGuiaVentaBL.GuiaVenta_Buscar(idEmpresa, codigoUsuario, idPedido, ref objOrdenVentaDet,  ref bloqueado, ref mensajeBloqueado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<GuiaVenta_LotesItemsResult> GuiaVenta_LotesItemBuscar(int idEmpresa, int codigoUsuario, int op, int item_id)
        {
            try
            {
                GuiaBL objGuiaVentaBL = new GuiaBL();
                return objGuiaVentaBL.GuiaVenta_LotesItemBuscar(idEmpresa, codigoUsuario, op, item_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<VBG00971Result> GuiaVenta_BuscarLotesxItem(int idEmpresa, int codigoUsuario, int op, int item_id, int ID_AlmacenAnexo, int id_amarre)
        {
            try
            {
                GuiaBL objGuiaVentaBL = new GuiaBL();
                return objGuiaVentaBL.GuiaVenta_BuscarLotesxItem(idEmpresa, codigoUsuario, op, item_id, ID_AlmacenAnexo, id_amarre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USP_SEL_Guia_VentaImpresaQRResult> GuiaVentaQR_SeleccionarDocumentos(int idEmpresa, int codigoUsuario, DateTime fechainicial, DateTime fechafinal, string nombrecliente, Boolean flgimpreso)
        {
            try
            {
                GuiaBL objGuiaVentaBL = new GuiaBL();
                return objGuiaVentaBL.GuiaVentaQR_SeleccionarDocumentos(idEmpresa, codigoUsuario, fechainicial, fechafinal, nombrecliente, flgimpreso);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USP_SEL_IMPRESORASQRResult> GuiaVentaQR_SeleccionarImpresoras(int idEmpresa, int codigoUsuario)
        {
            try
            {
                GuiaBL objGuiaVentaBL = new GuiaBL();
                return objGuiaVentaBL.GuiaVentaQR_SeleccionarImpresoras(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USP_SEL_GUIAS_PENDIENTESIMPRESIONResult> GuiaVentaQR_SeleccionarGuiasPendientesImrpesion(int idEmpresa, int codigoUsuario)
        {
            try
            {
                GuiaBL objGuiaVentaBL = new GuiaBL();
                return objGuiaVentaBL.GuiaVentaQR_SeleccionarGuiasPendientesImrpesion(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GuiaVentaQR_ActualizarFlagImpresion(int idEmpresa, int codigoUsuario, string Empresa, Int32 OpGuia, bool flgimpreso)
        {
            try
            {
                GuiaBL objGuiaVentaBL = new GuiaBL();
                objGuiaVentaBL.GuiaVentaQR_ActualizarFlagImpresion(idEmpresa, codigoUsuario, Empresa, OpGuia, flgimpreso);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GuiaVenta_Registrar_wms(int idEmpresa, int codigoUsuario, gsGuia_BuscarCabeceraResult objGuiaVentaCabBE,
                gsGuia_BuscarDetalleResult lstProductos, decimal? idOperacion, List<GuiaVenta_LotesItemsResult> lstLotes, string EmpresaPT, string PedidoServicio, string pedidolote, string pedidoidamarre)
        {
            decimal neto, descuento, impuesto;
            List<gsGuia_BuscarDetalleResult> lst;

            try
            {
                //List<gsGuiaVentaDetalle> lstPedidoDet = new List<gsPedidoDetalle>();
                GuiaBL objGuiaVentaBL = new GuiaBL();
                PedidoBL objPedidoBL = new PedidoBL();


                gsPedidoDetalle objPedidoDetalle = new gsPedidoDetalle();

                objPedidoDetalle.Op = objGuiaVentaBL.GuiaVenta_Registrar_wms(idEmpresa, codigoUsuario, objGuiaVentaCabBE, lstProductos, idOperacion, lstLotes, EmpresaPT, PedidoServicio, pedidolote, pedidoidamarre);




            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public string Productividad_Almacen_Registrar(string Empresa, int Op)
        {
            GuiaBL objPaBL;
            try
            {
                objPaBL = new GuiaBL();
                return objPaBL.Productividad_Almacen_Registrar(Empresa, Op);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<USP_SEL_Productividad_AlmacenResult> Productividad_Almacen_Listar(string Empresa, DateTime fechaInicio, DateTime fechaFin)
        {
            GuiaBL objPaBL;

            try
            {
                List<USP_SEL_Productividad_AlmacenResult> list = new List<USP_SEL_Productividad_AlmacenResult>();

                objPaBL = new GuiaBL();
                list = objPaBL.Productividad_Almacen_Listar(Empresa, fechaInicio, fechaFin);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USP_SEL_IdTrazabilidadDespachoResult> IdTrazabilidadDespacho_Listar()
        {
            GuiaBL objTrazabilidadDespachoBL;

            try
            {
                List<USP_SEL_IdTrazabilidadDespachoResult> list = new List<USP_SEL_IdTrazabilidadDespachoResult>();

                objTrazabilidadDespachoBL = new GuiaBL();
                list = objTrazabilidadDespachoBL.IdTrazabilidadDespacho_Listar();

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USP_SEL_TrazabilidadDespachoXIDResult> IdTrazabilidadDespacho_Listar_ID(decimal id, int idEmpresa)
        {
            GuiaBL objTrazabilidadDespachoBL;

            try
            {
                List<USP_SEL_TrazabilidadDespachoXIDResult> list = new List<USP_SEL_TrazabilidadDespachoXIDResult>();
                objTrazabilidadDespachoBL = new GuiaBL();
                list = objTrazabilidadDespachoBL.IdTrazabilidadDespacho_Listar_ID(id, idEmpresa);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
