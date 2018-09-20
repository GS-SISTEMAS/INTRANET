using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
	// NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IEmpresaWCF" en el código y en el archivo de configuración a la vez.
	[ServiceContract]
	public interface IEmpresaWCF
	{
		[OperationContract]
		List<Empresa_ComboBoxResult> Empresa_ComboBox();
        [OperationContract]
        List<Empresa_BuscarDetalleResult> Empresa_BuscarDetalle(int idEmmpresa);

        [OperationContract]
        List<Empresa_ListarResult> Empresa_Listar(int idEmmpresa, string detalle);
        [OperationContract]
        void Empresa_Registrar(int id_empresa, decimal provision, int comision); 

    }
}
