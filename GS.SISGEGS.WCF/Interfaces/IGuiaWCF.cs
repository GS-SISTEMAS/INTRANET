using GS.SISGEGS.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GS.SISGEGS.BE;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IGuiaWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IGuiaWCF
    {
        [OperationContract]
        List<gsGuiaVenta_ListarXPedidoResult> GuiaVenta_ListarxPedido(int idEmpresa, int codigoUsuario, int idPedido);
        [OperationContract]
        List<gsGuiaVentas_listarResult> GuiaVenta_Listar(int idEmpresa, int codigoUsuario, string id_agenda, DateTime fInicio, DateTime fFin, string id_item, int ID_Almacen, int OP);
        [OperationContract]
        List<gsGuiaVenta_ListarxOPResult> GuiaVenta_ListarxOP(int idEmpresa, int codigoUsuario, int idOperacion);
        [OperationContract]
        void GuiaVenta_ActualizarTransporte(int idEmpresa, int codigoUsuario, gsGuiaVenta_ListarxOPResult objGuiaVentaLista, decimal idOperacion);
        [OperationContract]
        void GuiaVenta_FechaInsertar(int idEmpresa, int codigoUsuario, int Op, string transaccion, string id_agenda, DateTime fechaEmision, int id_vehiculo, int estado);
        [OperationContract]
        List<gsGuiaVentas_FechaslistarResult> GuiaVenta_FechasListar(int idEmpresa, int codigoUsuario, string id_agenda, DateTime fInicio, DateTime fFin, string id_item, int ID_Almacen, int OP);
        [OperationContract]
        List<gsGuiaVentas_TransitolistarResult> GuiaVenta_TransitoListar(int idEmpresa, int codigoUsuario, string id_agenda, DateTime fInicio, DateTime fFin, string id_item, int ID_Almacen, int OP, int id_vehiculo);
        [OperationContract]
        void GuiaVenta_FechaTransporte(int idEmpresa, int codigoUsuario, int Op, string transaccion, string id_agenda, int estado);
        [OperationContract]
        List<gsGuiaVentas_FechasGlobalResult> GuiaVenta_ListarGlobal(int idEmpresa, int codigoUsuario, string id_agenda, DateTime fInicio, DateTime fFin, string id_item, int ID_Almacen, int OP);
        [OperationContract]
        void GuiaVenta_Modificar(int idEmpresa, int codigoUsuario, int Op, string transaccion, string id_agenda, int usuarioE, DateTime fechaEmision, int usuarioS, DateTime fechaSeguridad, int usuarioC, DateTime fechaCliente, int estado, int id_vehiculo);

        [OperationContract]
        void GuiaVenta_Registrar(int idEmpresa, int codigoUsuario, gsGuia_BuscarCabeceraResult objGuiaVentaCabBE,
        gsGuia_BuscarDetalleResult lstProductos, decimal? idOperacion, List<GuiaVenta_LotesItemsResult> lstLotes);

        [OperationContract]
        gsGuia_BuscarCabeceraResult GuiaVenta_Buscar(int idEmpresa, int codigoUsuario, int idPedido,
        ref List<gsGuia_BuscarDetalleResult> objOrdenVentaDet, ref bool? bloqueado, ref string mensajeBloqueado);

        [OperationContract]
        List<GuiaVenta_LotesItemsResult> GuiaVenta_LotesItemBuscar(int idEmpresa, int codigoUsuario, int op, int item_id);
        [OperationContract]
        List<VBG00971Result> GuiaVenta_BuscarLotesxItem(int idEmpresa, int codigoUsuario, int op, int item_id, int ID_AlmacenAnexo, int id_amarre);

        [OperationContract]
        List<USP_SEL_Guia_VentaImpresaQRResult> GuiaVentaQR_SeleccionarDocumentos(int idEmpresa, int codigoUsuario, DateTime fechainicial, DateTime fechafinal, string nombrecliente, Boolean flgimpreso);

        [OperationContract]
        List<USP_SEL_IMPRESORASQRResult> GuiaVentaQR_SeleccionarImpresoras(int idEmpresa, int codigoUsuario);
        [OperationContract]
        List<USP_SEL_GUIAS_PENDIENTESIMPRESIONResult> GuiaVentaQR_SeleccionarGuiasPendientesImrpesion(int idEmpresa, int codigoUsuario);

        [OperationContract]
        void GuiaVentaQR_ActualizarFlagImpresion(int idEmpresa, int codigoUsuario, string Empresa, Int32 OpGuia, bool flgimpreso);

        [OperationContract]
        void GuiaVenta_Registrar_wms(int idEmpresa, int codigoUsuario, gsGuia_BuscarCabeceraResult objGuiaVentaCabBE,
                gsGuia_BuscarDetalleResult lstProductos, decimal? idOperacion, List<GuiaVenta_LotesItemsResult> lstLotes, string EmpresaPT, string PedidoServicio, string pedidolote, string pedidoidamarre);

        [OperationContract]
        string Productividad_Almacen_Registrar(string Empresa, int ID_Letra);

        [OperationContract]
        List<USP_SEL_Productividad_AlmacenResult> Productividad_Almacen_Listar(string Empresa, DateTime fechaInicio, DateTime fechaFin);


        [OperationContract]
        List<USP_SEL_TrazabilidadDespachoXIDResult> IdTrazabilidadDespacho_Listar_ID(decimal id, int idEmpresa);

        [OperationContract]
        List<USP_SEL_IdTrazabilidadDespachoResult> IdTrazabilidadDespacho_Listar();
    }
}
