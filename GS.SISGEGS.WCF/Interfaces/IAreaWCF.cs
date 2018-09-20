using System.Collections.Generic;
using System.ServiceModel;
using GS.SISGEGS.DM;
namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IAreaWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IAreaWCF
    {
        [OperationContract]
        List<gsArea_ListarResult> Listar_Areas(int idEmpresa, int codigoUsuario);
    }
}
