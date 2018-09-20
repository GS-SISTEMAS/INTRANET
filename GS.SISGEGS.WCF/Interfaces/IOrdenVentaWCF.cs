using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using GS.SISGEGS.DM;
using GS.SISGEGS.BE;


namespace GS.SISGEGS.WCF
{
    [ServiceContract]
    public interface IOrdenVentaWCF
    {
        [OperationContract]
        List<gsOV_ListarResult> OrdenVenta_Listar(int idEmpresa, int codigoUsuario, string ID_Agenda,
            DateTime fechaDesde, DateTime fechaHasta, string ID_Vendedor, bool modificarPedido);
        [OperationContract]
        void OrdenVenta_Eliminar(int idEmpresa, int codigoUsuario, int idOperacion, string Comentario);
        [OperationContract]
        void OrdenVenta_Registrar(int idEmpresa, int codigoUsuario, gsOV_BuscarCabeceraResult objOrdenVentaCabBE,
            List<gsOV_BuscarDetalleResult> lstProductos, List<GlosaBE> lstImpuestos, decimal? idOperacion, decimal limiteCredito,
            DateTime fechaVencimiento, List<gsPedidos_FechasLetrasSelectResult> ListaFechas, string Letras, int KardexFlete, gsOV_BuscarDetalleResult objetoFlete, int DiasCredito); 
        [OperationContract]
        gsOV_BuscarCabeceraResult OrdenVenta_Buscar(int idEmpresa, int codigoUsuario, int idPedido,
            ref List<gsOV_BuscarDetalleResult> objOrdenVentaDet, ref List<gsOV_BuscarImpuestoResult> objOrdenVentaImp,
            ref bool? bloqueado, ref string mensajeBloqueado);
        [OperationContract]
        List<VBG03630Result> OrdenVenta_ListarTipo(int idEmpresa, int codigoUsuario);
        [OperationContract]
        void OV_TransGratuitas_Aprobar(int idEmpresa, int codigoUsuario, int Op, ref string mensajeError);
        [OperationContract]
        List<gsOV_Listar_SectoristaResult> OrdenVenta_Listar_Sectorista(int idEmpresa, int codigoUsuario, string ID_Agenda, DateTime fechaDesde, DateTime fechaHasta, string ID_Vendedor, bool modificarPedido, string id_Sectorista, int Estado, int FormaPago); 
        [OperationContract]
        void OrdenVenta_Deasaprobar(int idEmpresa, int codigoUsuario, int idOperacion, string Comentario);

        [OperationContract]
        void OrdenVenta_Registrar_Contado(int idEmpresa, int codigoUsuario, gsOV_BuscarCabeceraResult objOrdenVentaCabBE,
        List<gsOV_BuscarDetalleResult> lstProductos, List<GlosaBE> lstImpuestos, decimal? idOperacion, decimal limiteCredito,
        DateTime fechaVencimiento, List<gsPedidos_FechasLetrasSelectResult> ListaFechas, string Letras, int KardexFlete, gsOV_BuscarDetalleResult objetoFlete, int DiasCredito);

        [OperationContract]
        gsOV_BuscarCabeceraResult OrdenVenta_Buscar_Guia(int idEmpresa, int codigoUsuario, int idPedido,
    ref List<gsOV_BuscarDetalleResult> objOrdenVentaDet, ref List<gsOV_BuscarImpuestoResult> objOrdenVentaImp,
    ref bool? bloqueado, ref string mensajeBloqueado);

        [OperationContract]
        List<gsPedidos_FechasLetrasSelectResult> PedidoLetras_Detalle(int idEmpresa, int codigoUsuario, int OpOV, int OpDOC, string Tabla);


        [OperationContract]
        gsPedido_EliminarOP_WMSResult Pedido_Apto_Modificacion(int idEmpresa, int codigoUsuario, int idPedido, int Op);

        [OperationContract]
        void OrdenVenta_Registrar_Fechas(int idEmpresa, int codigoUsuario, DataTable TablaDocs, List<gsPedidos_FechasLetrasSelectResult> ListaFechas);
    }
}
