using GS.SISGEGS.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GS.SISGEGS.WCF
{
	// NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IDireccionWCF" en el código y en el archivo de configuración a la vez.
	[ServiceContract]
	public interface IDireccionWCF
	{
        [OperationContract]
        List<VBG00209Result> Direccion_ListarCliente(int idEmpresa, int codigoUsuario, string idAgenda);
        [OperationContract]
        List<VBG03679Result> Direccion_ListarReferencia(int idEmpresa, int codigoUsuario, string idAgenda, int? idSucursal, int? idReferencia);
    }
}
