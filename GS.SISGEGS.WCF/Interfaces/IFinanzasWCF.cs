using System;
using System.Collections.Generic;
using System.ServiceModel;
using GS.SISGEGS.DM;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IFinanzasWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IFinanzasWCF
    {
        [OperationContract]
        List<GS_ReporteDetraccionesResult> ReporteDetracciones(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, string idAgenda, int estatus);

        [OperationContract]
        void Accion_Registrar(int idEmpresa, int codigoUsuario, GS_DetraccionAccionGetResult objAccionInsert);

        [OperationContract]
        GS_DetraccionAccionGetResult GetDetraccionAccion(int idEmpresa, int codigoUsuario, int op);
        [OperationContract]
        List<GS_GetVoucherDetraccionesResult> GetVoucherDetraccione(int idEmpresa, int codigoUsuario, int op); 
    }
}
