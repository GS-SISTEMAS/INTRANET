using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IReporteSistemasWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IReporteSistemasWCF
    {
        [OperationContract]
        List<AuditoriaMenu_ReporteResult> AuditoriaMenu_Reporte(int idEmpresa, DateTime fechaInicio, DateTime fechaFinal);
        [OperationContract]
        List<USP_Sel_ControlFacturasMaximoResult> ListarControlFacturasMaximo(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int procesados);
    }
}
