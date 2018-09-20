using System;
using System.Collections.Generic;
using System.ServiceModel;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IResponsableContratoWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IResponsableContratoWCF
    {
        [OperationContract]
        List<ResponsablesContrato_ListarResult> ResponsablesContrato_Listar();
    }
}
