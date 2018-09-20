using GS.SISGEGS.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IGuiaWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IGuiaWCF
    {
        [OperationContract]
        List<gsGuiaVenta_ListarXPedidoResult> GuiaVenta_ListarxPedido(int idEmpresa, int codigoUsuario, int idPedido);
    }
}
