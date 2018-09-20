using GS.SISGEGS.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IUnidadWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IUnidadWCF
    {
        [OperationContract]
        List<VBG02665Result> UnidadGestion_ListarImputables(int idEmpresa, int codigoUsuario);
        [OperationContract]
        List<VBG02668Result> UnidadProyecto_ListarImputables(int idEmpresa, int codigoUsuario);
    }
}
