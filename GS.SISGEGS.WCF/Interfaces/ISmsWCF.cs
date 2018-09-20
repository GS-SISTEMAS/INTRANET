using System;
using System.Collections.Generic;
using System.ServiceModel;
using GS.SISGEGS.DM;


namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ISmsWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ISmsWCF
    {
        [OperationContract]
        List<SP_PerfilesEmpresaResult> Lista_PerfilesEmpresa(int idEmpresa);

        [OperationContract]
        void Registro_SMS( string text, string id_perfil);

    }
}
