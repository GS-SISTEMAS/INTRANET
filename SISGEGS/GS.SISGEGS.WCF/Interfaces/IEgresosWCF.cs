using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IEgresosWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IEgresosWCF
    {
        [OperationContract]
        List<gsEgresosVarios_ListarCajaChicaResult> EgresosVarios_ListarCajaChica(int idEmpresa, int codigoUsuario, string idAgenda, DateTime fechaInicio, DateTime fechaFinal);
        [OperationContract]
        gsEgresosVarios_BuscarCabeceraResult EgresosVarios_Buscar(int idEmpresa, int codigoUsuario, int idOperacion, ref bool? bloqueado, ref string mensajeBloqueado, ref List<gsEgresosVarios_BuscarDetalleResult> lstEgresosVarios);
        [OperationContract]
        void EgresosVarios_Registrar(int idEmpresa, int codigoUsuario, gsEgresosVarios_BuscarCabeceraResult objEVCabecera, List<gsEgresosVarios_BuscarDetalleResult> lstEVDetalles);
        [OperationContract]
        void EgresosVarios_Eliminar(int idEmpresa, int codigoUsuario, int idOperacion);
    }
}
