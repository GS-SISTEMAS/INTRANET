using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GS.SISGEGS.DM;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface IAuditoriaBL {
        void AuditoriaMenu_Registrar(string url, string nombreDispositivo, int idUsuario);
        List<AuditoriaMenu_ReporteResult> AuditoriaMenu_Reporte(int idEmpresa, DateTime fechaInicio, DateTime fechaFinal);
    }

    public class AuditoriaBL : IAuditoriaBL
    {
        public void AuditoriaMenu_Registrar(string url, string nombreDispositivo, int idUsuario)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dci.AuditoriaMenu_Registrar(url, nombreDispositivo, idUsuario);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR: No se pudo conectarse con la base de datos.");
                }
            }
        }

        public List<AuditoriaMenu_ReporteResult> AuditoriaMenu_Reporte(int idEmpresa, DateTime fechaInicio, DateTime fechaFinal)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.AuditoriaMenu_Reporte(idEmpresa, fechaInicio, fechaFinal).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo consultar el reporte de auditoriía de páginas.");
                }
            }
        }
    }
}
