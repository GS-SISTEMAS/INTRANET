using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ICorreoWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ICorreoWCF
    {
        [OperationContract]
        void EnviarCorreo(string to, string toCorreo, string cc, string ccCorreo, string asunto, string mensaje);
        [OperationContract]
        void MerlinEnviarCorreo(string to, string toCorreo, string cc, string ccCorreo, string asunto, string mensaje);
        [OperationContract]
        void MerlinEnvioCorreoAdjunto(string to, string toCorreo, string cc, string ccCorreo, string asunto, string mensaje, string FilePath); 
    }
}
