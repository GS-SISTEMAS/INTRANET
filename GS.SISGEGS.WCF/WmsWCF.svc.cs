using System;
using System.Collections.Generic;
using GS.SISGEGS.DM;
using GS.SISGEGS.BL;

namespace GS.SISGEGS.WCF
{
    public class WmsWCF : IWmsWCF
    {
        public List<gsWMSPendientes_EnvioResult> WmsPendientes_Envio(int idEmpresa, int codigoUsuario)
        {
            WmsBL objWmsBL;
            try
            {
                objWmsBL = new WmsBL();
                return objWmsBL.WmsPendientes_Envio(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<gsWMSProduccion_PendientesRecibirResult> WmsProduccion_PendientesRecibir(int idEmpresa, int codigoUsuario) {
            WmsBL objWmsBL;
            try
            {
                objWmsBL = new WmsBL();
                return objWmsBL.WmsProduccion_PendientesRecibir(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region guiaCompra
        public int WMS_RegistrarGuiaCompra(int idEmpresa, int codigoUsuario, string idAlmacen, string idAgenda) {
            WmsBL objWmsBL;
            try
            {
                objWmsBL = new WmsBL();
                return objWmsBL.WMS_RegistrarGuiaCompra(idEmpresa, codigoUsuario, idAlmacen, idAgenda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WMS_ObtenerOrdenCompraResult WMS_ObtenerOrdenCompra(int idEmpresa, int codigoUsuario, string nroRegistro) {
            WmsBL objWmsBL;
            try
            {
                objWmsBL = new WmsBL();
                return objWmsBL.WMS_ObtenerOrdenCompra(idEmpresa, codigoUsuario, nroRegistro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int WMS_RegistrarGuiaCompraLinea(int idEmpresa, int codigoUsuario, int Op, int OrdenCompraOp, string item, int unidadesRecibidas, string noLote)
        {
            WmsBL objWmsBL;
            try
            {
                objWmsBL = new WmsBL();
                return objWmsBL.WMS_RegistrarGuiaCompraLinea(idEmpresa, codigoUsuario, Op, OrdenCompraOp, item,unidadesRecibidas, noLote);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ProcesarGuiaCompra(int idEmpresa, int codigoUsuario, int op) {
            WmsBL objWmsBL;
            try
            {
                objWmsBL = new WmsBL();
                objWmsBL.ProcesarGuiaCompra(idEmpresa, codigoUsuario, op);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Produccion Recibir

        public int WMS_Produccion_Recibir(int idEmpresa, int codigoUsuario, string id_almacen, int almacenAnexo) {
            WmsBL objWmsBL;
            try
            {
                objWmsBL = new WmsBL();
                return objWmsBL.WMS_Produccion_Recibir(idEmpresa, codigoUsuario, id_almacen, almacenAnexo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public int WMS_ProduccionLinea_Recibir(int idEmpresa, int codigoUsuario, int op, int linea, string ID_Item, int kardex, decimal cantidad, string lote, DateTime fechaFabricacion, DateTime fechaVencimiento, string proveedor)
        {
            WmsBL objWmsBL;
            try
            {
                objWmsBL = new WmsBL();
                return objWmsBL.WMS_ProduccionLinea_Recibir(idEmpresa, codigoUsuario, op, linea, ID_Item, kardex, cantidad,lote,fechaFabricacion,fechaVencimiento,proveedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        public List<VBG00518_WMSResult> WmsPedidosPendientes_Envio(int idEmpresa, int codigoUsuario)
        {
            WmsBL objWmsBL;
            try
            {
                objWmsBL = new WmsBL();
                return objWmsBL.WmsPedidosPendientes_Envio(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void WmsPedidosPendientes_Insertar(int idEmpresa, int codigoUsuario, string NroPedido, string ID_Item, string Lote, decimal CantidadPedido, decimal CantidaEntregada, decimal CantidadPendiente, string EstadoPedido, int Id_Amarre)
        {
            WmsBL objWmsBL;
            try
            {
                objWmsBL = new WmsBL();
                objWmsBL.WmsPedidosPendientes_Insertar(idEmpresa, codigoUsuario, NroPedido, ID_Item, Lote, CantidadPedido, CantidaEntregada, CantidadPendiente, EstadoPedido, Id_Amarre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Cargar_Pedidos_ConfirmacionResult> WmsPedidos_Confirmacion_Listar(int idEmpresa, int codigoUsuario, string ruc_empresa)
        {
            WmsBL objWmsBL;
            try
            {
                objWmsBL = new WmsBL();
                return objWmsBL.WmsPedidos_Confirmacion_Listar(idEmpresa, codigoUsuario, ruc_empresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void WmsPedidosPendientes_UpdateEstilos(int idEmpresa, int codigoUsuario, string empresa, string numeroDeAlbaran)
        {
            WmsBL objWmsBL;
            try
            {
                objWmsBL = new WmsBL();
                objWmsBL.WmsPedidosPendientes_UpdateEstilos(idEmpresa, codigoUsuario, empresa, numeroDeAlbaran);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Cargar_Recepcion_ConfirmacionResult> WmsRecepcion_Confirmacion_Listar(int idEmpresa, int codigoUsuario, string ruc_empresa)
        {
            WmsBL objWmsBL;
            try
            {
                objWmsBL = new WmsBL();
                List<Cargar_Recepcion_ConfirmacionResult> lista = new List<Cargar_Recepcion_ConfirmacionResult>();

                lista = objWmsBL.WmsRecepcion_Confirmacion_Listar(idEmpresa, codigoUsuario, ruc_empresa);
                return lista; 

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void WmsPedidosPendientes_Update(int idEmpresa, int codigoUsuario, string Lote, int Id_Amarre, string transferido, string observacion)
        {
            WmsBL objWmsBL;
            try
            {
                objWmsBL = new WmsBL();
                objWmsBL.WmsPedidosPendientes_Update(idEmpresa, codigoUsuario,  Lote, Id_Amarre, transferido, observacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Wms_Produccion_UpdateEstilos(int idEmpresa, int codigoUsuario, string empresa, string numeroDeAlbaran)
        {
            WmsBL objWmsBL;
            try
            {
                objWmsBL = new WmsBL();
                objWmsBL.Wms_Produccion_UpdateEstilos(idEmpresa, codigoUsuario, empresa, numeroDeAlbaran);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}