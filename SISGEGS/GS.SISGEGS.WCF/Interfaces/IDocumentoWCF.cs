using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IDocumentoWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IDocumentoWCF
    {
        [OperationContract]
        List<VBG00716Result> Documento_ListarDocVenta(int idEmpresa, int codigoUsuario);
        [OperationContract]
        List<gsDocumento_ListarTipoCompraResult> Documento_ListarDocCompra(int idEmpresa, int codigoUsuario, string descripcion);
        [OperationContract]
        List<VBG01122Result> Documento_ListarEgresosVarios(int idEmpresa, int codigoUsuario);
    }
}
