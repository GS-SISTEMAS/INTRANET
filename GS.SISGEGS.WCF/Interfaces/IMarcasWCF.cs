using System;
using System.Collections.Generic;
using System.ServiceModel;
using GS.SISGEGS.DM;


namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IMarcasWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IMarcasWCF
    {
        [OperationContract]
        List<TitularMarca_ListarResult> TitularMarca_Listar();
        [OperationContract]
        List<RegistroMarca_Listar_v2Result> RegistroMarca_Listar(int idEmpresa,string marca, int idTipo, int idPais, int idTitular, DateTime fechaInicio, DateTime fechaFin, string clase, int todo);
        [OperationContract]
        List<TipoMarca_ListarResult> TipoMarca_Listar();
        [OperationContract]
        List<Pais_ListarResult> Pais_Listar();
        [OperationContract]
        List<Marca_ListarResult> Marca_Listar(string nombreMarca);
        [OperationContract]
        List<ResponsablesRegistros_ListarResult> ResponsablesRegistros_Listar();
        [OperationContract]
        List<RegistroMarca_NotificacionResult> RegistroMarca_Notificacion();
        [OperationContract]
        List<EstadoMarca_ListarResult> EstadoMarca_Listar();
        [OperationContract]
        RegistroMarca_ObtenerResult RegistroMarca_Obtener(int idRegistroMarca);
        [OperationContract]
        void RegistroMarca_Registrar(int idMarca, int idEmpresa, string marca, int idPais, int idTipo, int idTitular, string clase, string certificado,
                                    DateTime fechaVencimiento, int idEstadoMarca, string observacion, int usuario);
        [OperationContract]
        List<RegistroMarcaHistorico_ListarResult> HistoricoMarca_Listar(int idMarca, DateTime FechanVencimientoIni, DateTime FechaVencimientoFin);

        [OperationContract]
        void DocumentoMarca_Registrar(int idRegistroMarca, int idTipoDocumento, string documento, string ruta, int usuario);
        [OperationContract]
        List<DocumentosMarca_ListarResult> DocumentoMarca_Listar(int idRegistroMarca, int idTipoDocumentoMarca);
        [OperationContract]
        void DocumentoMarca_Eliminar(int idDocumento, int idRegistroMarca);
        [OperationContract]
        List<TipoDocumentoMarca_ListarResult> TipoDocumentoMarca_Listar();

        [OperationContract]
        List<ClaseMarca_ListarResult> ClaseMarca_Listar();
    }
}
