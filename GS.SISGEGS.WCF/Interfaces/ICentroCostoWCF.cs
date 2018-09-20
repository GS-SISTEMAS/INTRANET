using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ICentroCostoWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ICentroCostoWCF
    {
        [OperationContract]
        List<VBG00786Result> CentroCosto_ListarImputables(int idEmpresa, int codigoUsuario);
        [OperationContract]
        List<VBG00786Result> CentroCosto_Listar(int idEmpresa, int codigoUsuario, string nombre);
        [OperationContract]
        List<gsBuscarCentroCosto_IntranetResult> BuscarCentroCosto_Intranet(int idEmpresa, int codigoUsuario, bool flagRendGasto);
    }
}
