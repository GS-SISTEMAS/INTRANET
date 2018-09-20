using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
	// NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ISolDevolucionWCF" en el código y en el archivo de configuración a la vez.
	[ServiceContract]
	public interface ISolDevolucionWCF
	{
		[OperationContract]
        List<gsDocVenta_ListarResult> DocVenta_Listar(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, string ID_Vendedor);
        [OperationContract]
        List<gsDevolucionMotivo_ComboBoxResult> DevolucionMotivo_ComboBox(int idEmpresa, int codigoUsuario);
        [OperationContract]
        gsDocVenta_BuscarResult DocVenta_Buscar(int idEmpresa, int codigoUsuario, decimal Op, ref List<gsDocVenta_BuscarDetalleResult> lstDetalle);
        [OperationContract]
        List<gsAgendaAnexo_ListarAlmacenDevolucionResult> AgendaAnexo_ListarAlmacenDevolucion(int idEmpresa, int codigoUsuario);
        [OperationContract]
        void DevolucionSolicitud_Registrar(int idEmpresa, int codigoUsuario, gsDevolucionSolicitud objDevolucionSolicitud, List<gsDevolucionSolicitudDetalle> lstProductos);
        [OperationContract]
        List<gsDevolucionSolicitud_ListarResult> DevolucionSolicitud_Listar(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, string ID_Vendedor, string ID_Cliente);
        [OperationContract]
        gsDevolucionSolicitud_BuscarResult DevolucionSolicitud_Buscar(int idEmpresa, int codigoUsuario, int idDevolucionSolicitud, ref List<gsDevolucionSolicitudDetalle_BuscarResult> lstProductos);
        [OperationContract]
        void DevolucionSolicitud_Aprobar(int idEmpresa, int codigoUsuario, int idDevolucionSolicitud);
        [OperationContract]
        void DevolucionSolicitud_Eliminar(int idEmpresa, int codigoUsuario, int idDevolucionSolicitud);
    }
}
