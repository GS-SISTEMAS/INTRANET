using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IContratosWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IContratosWCF
    {
        [OperationContract]
        List<ReporteGeneralContratosResult> ReporteGeneralContratos(int idAreaResponsable, int idMateria, int idTipo, int idProveedor, int idEstado, DateTime fechaInicio, DateTime fechaFin,DateTime fechaVencIni, DateTime fechaVencFin);
        [OperationContract]
        List<ContratosVencer_ListarResult> ContratosVencer_Listar(int id_Area);
        [OperationContract]
        List<AreaResponsable_ListarResult> AreaResponsable_Listar();
        [OperationContract]
        List<ProveedorContrato_ListarResult> ProveedorContrato_Listar();
        [OperationContract]
        void Contrato_Registrar(int Codigo, int materia, int tipo, int areaResponsable, string renovar, int proveedor, string contratante,
                string fechaSuscripcion, string fechaVencimiento, string objeto, string renovacion, string monto, int estado, int idUsuario);
        [OperationContract]
        void Contrato_Eliminar(int idContrato, int idUsuario);
        [OperationContract]
        Contrato_ObtenerResult Contrato_Obtener(int idContrato);
        [OperationContract]
        void Contrato_Actualizar(int idContrato,int Codigo, int materia, int tipo, int areaResponsable, string renovar, int proveedor, string contratante,
                string fechaSuscripcion, string fechaVencimiento, string objeto, string renovacion, string monto, int estado, int idUsuario);

    }
}
