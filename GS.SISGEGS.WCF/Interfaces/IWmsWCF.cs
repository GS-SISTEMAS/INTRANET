using System.Collections.Generic;
using GS.SISGEGS.DM;
using System.ServiceModel;
using System;

namespace GS.SISGEGS.WCF
{
    [ServiceContract]
    public interface IWmsWCF
    {
        [OperationContract]
        List<gsWMSPendientes_EnvioResult> WmsPendientes_Envio(int idEmpresa, int codigoUsuario);

        [OperationContract]
        List<gsWMSProduccion_PendientesRecibirResult> WmsProduccion_PendientesRecibir(int idEmpresa, int codigoUsuario);
        [OperationContract]
        void WmsPedidosPendientes_UpdateEstilos(int idEmpresa, int codigoUsuario, string empresa, string numeroDeAlbaran);

        #region guiaCompra
        [OperationContract]
        int WMS_RegistrarGuiaCompra(int idEmpresa, int codigoUsuario, string idAlmacen, string idAgenda);
        [OperationContract]
        WMS_ObtenerOrdenCompraResult WMS_ObtenerOrdenCompra(int idEmpresa, int codigoUsuario, string nroRegistro);
        [OperationContract]
        int WMS_RegistrarGuiaCompraLinea(int idEmpresa, int codigoUsuario, int Op, int OrdenCompraOp, string item, int unidadesRecibidas, string noLote);
        [OperationContract]
        void ProcesarGuiaCompra(int idEmpresa, int codigoUsuario, int op);
        #endregion

        #region Produccion Recibir
        [OperationContract]
        int WMS_Produccion_Recibir(int idEmpresa, int codigoUsuario, string id_almacen, int almacenAnexo);
        [OperationContract]
        int WMS_ProduccionLinea_Recibir(int idEmpresa, int codigoUsuario, int op, int linea, string ID_Item, int kardex, decimal cantidad, string lote, DateTime fechaFabricacion, DateTime fechaVencimiento, string proveedor);
        #endregion

        [OperationContract]
        List<VBG00518_WMSResult> WmsPedidosPendientes_Envio(int idEmpresa, int codigoUsuario);

        [OperationContract]
        void WmsPedidosPendientes_Insertar(int idEmpresa, int codigoUsuario, string NroPedido, string ID_Item, string Lote, decimal CantidadPedido, decimal CantidaEntregada, decimal CantidadPendiente, string EstadoPedido, int Id_Amarre);

        [OperationContract]
        List<Cargar_Pedidos_ConfirmacionResult> WmsPedidos_Confirmacion_Listar(int idEmpresa, int codigoUsuario, string ruc_empresa);
        [OperationContract]
        List<Cargar_Recepcion_ConfirmacionResult> WmsRecepcion_Confirmacion_Listar(int idEmpresa, int codigoUsuario, string ruc_empresa);

        [OperationContract]
        void WmsPedidosPendientes_Update(int idEmpresa, int codigoUsuario, string Lote, int Id_Amarre, string transferido, string observacion);

        [OperationContract]
        void Wms_Produccion_UpdateEstilos(int idEmpresa, int codigoUsuario, string empresa, string numeroDeAlbaran); 

    }
}
