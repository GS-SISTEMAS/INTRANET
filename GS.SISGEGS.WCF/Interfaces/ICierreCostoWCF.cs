using GS.SISGEGS.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GS.SISGEGS.WCF
{
	// NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ICierreCostoWCF" en el código y en el archivo de configuración a la vez.
	[ServiceContract]
	public interface ICierreCostoWCF
	{
		[OperationContract]
        void CierreCosto_Registrar(int idEmpresa, int codigoUsuario, int mes, int anho);
        [OperationContract]
        List<gsCierreCosto_ListarResult> CierreCosto_Listar(int idEmpresa, int codigoUsuario);
        [OperationContract]
        List<gsDocVenta_ControlCosto_MarcaProductoResult> DocVenta_ControlCosto_MarcaProducto(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, int? kardex);
        [OperationContract]
        List<gsCierreCosto_ComboBoxResult> CierreCosto_ComboBox(int idEmpresa, int codigoUsuario);
        [OperationContract]
        List<gsProduccion_Listar_PlanProdResult> Produccion_Listar_PlanProd(int idEmpresa, int codigoUsuario, DateTime fechaInicio,
            DateTime fechaFinal, int? kardex);
    }
}
