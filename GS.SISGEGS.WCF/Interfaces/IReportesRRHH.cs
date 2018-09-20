using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IReportesRRHH" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IReportesRRHH
    {
        [OperationContract]
        List<Ingreso_PersonalResult> Ingreso_Personal(DateTime fecha);
        [OperationContract]
        List<Ingreso_Personal_DetalleResult> Ingreso_PersonalDetalle(DateTime fecha, string ccosto, ref List<Ingreso_Personal_PermisosResult> lstpermiso);
        [OperationContract]
        List<Personal_ListarResult> Personal_Listar(string codEmpresa, string texto);
        [OperationContract]
        void Personal_Registrar(int idPersonal, string nroDocumento, string imageURL, int idUsuarioRegistro);
    }
}
