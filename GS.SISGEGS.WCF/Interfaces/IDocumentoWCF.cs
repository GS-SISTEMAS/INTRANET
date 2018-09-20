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
        List<gsDocumento_ListarTipoCompraResult> Documento_ListarDocCompra(int idEmpresa, int codigoUsuario);

        [OperationContract]
        List<VBG01122Result> Documento_ListarEgresosVarios(int idEmpresa, int codigoUsuario);

        //-----------------------------------------
        [OperationContract]
        List<ListarDocumentosResult> ListarDocumentos(int idEmpresa, int codigoUsuario);

        [OperationContract]
        List<ListarDocumentosFamiliaResult> ListarDocumentosFamilia(int idEmpresa, int codigoUsuario, int tipoFamilia);

        [OperationContract]
        void RegistrarDocumentoFamilia(int idEmpresa, int codigoUsuario, int tipoFamilia, decimal idDocumento);

        [OperationContract]
        void EliminarDocumentoFamilia(int idEmpresa, int codigoUsuario, decimal idDocumento);

        [OperationContract]
        List<DetalleOperacionFamiliaResult> ListarDetalleAfiliadas(int idEmpresa, int codigoUsuario, DateTime fechaCorte, char operacion, string id_agendacompara, decimal idmoneda);

        [OperationContract]
        List<DetalleOperacionDocumentoResult> ListarDetalleDocumentos(int idEmpresa, int codigoUsuario, DateTime fechaCorte, char operacion, string id_agenda, int tipoFamiliaDoc, int tipoDoc);

        [OperationContract]
        List<ListarFamiliasResult> ListarFamilias(int idEmpresa, int codigoUsuario);

        [OperationContract]
        List<DetalleOperacionFamiliaAnticuamientoDocumentoResult> DetalleOperacionFamiliaAnticuamientoDocumento(int idEmpresa, int codigoUsuario, DateTime fechaCorte, char operacion, string id_agenda, int tipoFamiliaDoc, int TipoDoc);


    }
}
