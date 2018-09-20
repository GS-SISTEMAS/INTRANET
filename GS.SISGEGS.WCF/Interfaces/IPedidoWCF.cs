using GS.SISGEGS.BE;
using GS.SISGEGS.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GS.SISGEGS.WCF
{
	// NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IPedidoWCF" en el código y en el archivo de configuración a la vez.
	[ServiceContract]
	public interface IPedidoWCF
	{
        [OperationContract]
        List<VBG03630Result> Pedido_ListarTipo(int idEmpresa, int codigoUsuario);
        [OperationContract]
        List<gsPedido_ListarResult> Pedido_Listar(int? idEmpresa, int codigoUsuario, string idAgenda, DateTime fechaInicio, DateTime fechaFinal, int? idDocumento, string idVendedor, int? idFormaPago, decimal? estadoAprobacion, ref bool superUsuario);
        [OperationContract]
        void Pedido_Registrar(int idEmpresa, int codigoUsuario, PedidoCabBE objPedidoCabBE, List<PedidoDetBE> lstProductos, 
            List<GlosaBE> lstImpuestos, decimal? idOperacion, string password, decimal limiteCredito);
        [OperationContract]
        gsPedido_BuscarCabeceraResult Pedido_BuscarCabecera(int idEmpresa, int codigoUsuario, int idPedido);
        [OperationContract]
        List<gsPedido_BuscarDetalleResult> Pedido_BuscarDetalle(int idEmpresa, int codigoUsuario, int idPedido, decimal? idAlmacen);
        [OperationContract]
        void Pedido_Eliminar(int idEmpresa, int codigoUsuario, int idOperacion, string password);
        [OperationContract]
        void Pedido_Aprobar(int idEmpresa, int codigoUsuario, int idPedido, int op, string idSectorista, bool aproCred, string motivo);
        [OperationContract]
        void Pedido_DesAprobar(int idEmpresa, int codigoUsuario, int idPedido, int op, string idSectorista, bool aproCred);
        [OperationContract]
        void gsDocVentaAprobacion_Registrar(int idEmpresa, int idPedido, int op, string id_agenda, int codigoUsuario);
        [OperationContract]
        VerificarExisteDocVentaResult VerificarExisteDocVenta(int idEmpresa, int codigoUsuario, string idAgenda);

        [OperationContract]
        List<gsPedidos_FechasLetrasSelectResult> PedidoLetras_Detalle(int idEmpresa, int codigoUsuario, int OpOV, int OpDOC, string Tabla);
    }
}
