using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using GS.SISGEGS.DM;
using GS.SISGEGS.BL;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ReporteSistemasWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ReporteSistemasWCF.svc o ReporteSistemasWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ReporteSistemasWCF : IReporteSistemasWCF
    {
        public List<AuditoriaMenu_ReporteResult> AuditoriaMenu_Reporte(int idEmpresa, DateTime fechaInicio, DateTime fechaFinal)
        {
            AuditoriaBL objAuditoriaBL = new AuditoriaBL();
            try {
                return objAuditoriaBL.AuditoriaMenu_Reporte(idEmpresa, fechaInicio, fechaFinal);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public List<USP_Sel_ControlFacturasMaximoResult> ListarControlFacturasMaximo(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int procesados)
        {
            OrdenCompraBL objOrdenCompraBL = new OrdenCompraBL();
            try
            {
                return objOrdenCompraBL.ListarControlFacturasMaximo(idEmpresa, codigoUsuario, FechaDesde, FechaHasta, procesados);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    

}
