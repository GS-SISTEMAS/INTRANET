using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GS.SISGEGS.BL;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "IndicadoresWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione IndicadoresWCF.svc o IndicadoresWCF.svc.cs en el Explorador de soluciones e inicie la depuración.

    public class IndicadoresWCF : IIndicadoresWCF
    {
        public List<gsReporte_IndicadoresCreditosResult> Indicadores_CreditosCobranzas(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera)
        {
            IndicadoresBL objIndicadoresBL;
            try
            {
                List<gsReporte_IndicadoresCreditosResult> list = new List<gsReporte_IndicadoresCreditosResult>();

                objIndicadoresBL = new IndicadoresBL();
                list = objIndicadoresBL.Indicadores_CreditosCobranzas(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos, divisor, verTodo, verCartera);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GS_ReporteLtasREF_RENOVResult> Indicadores_LetrasRenyRef(int idEmpresa, int codigoUsuario, DateTime fechaCorte) {
            IndicadoresBL objIndicadoresBL;
            try
            {
                List<GS_ReporteLtasREF_RENOVResult> list = new List<GS_ReporteLtasREF_RENOVResult>();

                objIndicadoresBL = new IndicadoresBL();
                list = objIndicadoresBL.Indicadores_LetrasRenyRef(idEmpresa, codigoUsuario, fechaCorte);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GS_ReporteIndicadorLetrasProtestadasResult> Indicadores_LetrasProtestadas(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            IndicadoresBL objIndicadoresBL;
            try
            {
                List<GS_ReporteIndicadorLetrasProtestadasResult> list = new List<GS_ReporteIndicadorLetrasProtestadasResult>();

                objIndicadoresBL = new IndicadoresBL();
                list = objIndicadoresBL.Indicadores_LetrasProtestadas(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporte_IndicadoresDeudaVencidaResult> Indicadores_DeudaVencida(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera)
        {
            IndicadoresBL objIndicadoresBL;
            try
            {
                List<gsReporte_IndicadoresDeudaVencidaResult> list = new List<gsReporte_IndicadoresDeudaVencidaResult>();

                objIndicadoresBL = new IndicadoresBL();
                list = objIndicadoresBL.Indicadores_DeudaVencida(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos, divisor, verTodo, verCartera);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporte_IndicadoresDeudaVencCreditoActResult> Indicadores_DeudaVencidaCreditoAct(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera)
        {
            IndicadoresBL objIndicadoresBL;
            try
            {
                List<gsReporte_IndicadoresDeudaVencCreditoActResult> list = new List<gsReporte_IndicadoresDeudaVencCreditoActResult>();

                objIndicadoresBL = new IndicadoresBL();
                list = objIndicadoresBL.Indicadores_DeudaVencidaCreditoAct(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos, divisor, verTodo, verCartera);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
