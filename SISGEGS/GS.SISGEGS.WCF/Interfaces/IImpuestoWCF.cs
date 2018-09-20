using GS.SISGEGS.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IImpuestoWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IImpuestoWCF
    {
        [OperationContract]
        List<gsImpuesto_ListarPorItemResult> Impuesto_ListarPorItem(int idEmpresa, int codigoUsuario, string idItem, DateTime fecha);
        [OperationContract]
        List<gsImpuesto_BuscarPorPedidoResult> Impuesto_BuscarPorPedido(int idEmpresa, int codigoUsuario, int idPedido);
    }
}
