using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GS.SISGEGS.DM;
using GS.SISGEGS.BE;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface IIndicadoresBL
    {
        List<gsReporte_IndicadoresCreditosResult> Indicadores_CreditosCobranzas(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera);
        List<GS_ReporteLtasREF_RENOVResult> Indicadores_LetrasRenyRef(int idEmpresa, int codigoUsuario, DateTime fechaCorte);
        List<GS_ReporteIndicadorLetrasProtestadasResult> Indicadores_LetrasProtestadas(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos);
        List<gsReporte_IndicadoresDeudaVencidaResult> Indicadores_DeudaVencida(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera);
        List<gsReporte_IndicadoresDeudaVencCreditoActResult> Indicadores_DeudaVencidaCreditoAct(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera);
    }

    public class IndicadoresBL : IIndicadoresBL
    {
        public List<gsReporte_IndicadoresCreditosResult> Indicadores_CreditosCobranzas(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {   ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    dcg.CommandTimeout = 900;
                    List<gsReporte_IndicadoresCreditosResult> list = dcg.gsReporte_IndicadoresCreditos(null, null, null, null, codAgenda, codVendedor, null, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos, divisor, verTodo, verCartera).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Indicadores en la base de datos.");
                }

            }
        }

        public List<GS_ReporteLtasREF_RENOVResult> Indicadores_LetrasRenyRef(int idEmpresa, int codigoUsuario, DateTime fechaCorte)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {   ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    dcg.CommandTimeout = 300;
                    List<GS_ReporteLtasREF_RENOVResult> lista = dcg.GS_ReporteLtasREF_RENOV(fechaCorte).ToList();
                    return lista;
                }
                catch (Exception ex) {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al emitir el indicador");
                }
            }
        }

        public List<GS_ReporteIndicadorLetrasProtestadasResult> Indicadores_LetrasProtestadas(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {   ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    dcg.CommandTimeout = 900;
                    List<GS_ReporteIndicadorLetrasProtestadasResult> list = dcg.GS_ReporteIndicadorLetrasProtestadas(null, null, null, null, codAgenda, codVendedor, null, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Indicadores en la base de datos.");
                }

            }
        }

        public List<gsReporte_IndicadoresDeudaVencidaResult> Indicadores_DeudaVencida(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {   ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    dcg.CommandTimeout = 900;
                    List<gsReporte_IndicadoresDeudaVencidaResult> list = dcg.gsReporte_IndicadoresDeudaVencida(null, null, null, null, codAgenda, codVendedor, null, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos, divisor, verTodo, verCartera).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Indicadores en la base de datos.");
                }

            }
        }

        public List<gsReporte_IndicadoresDeudaVencCreditoActResult> Indicadores_DeudaVencidaCreditoAct(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {   ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    dcg.CommandTimeout = 900;
                    List<gsReporte_IndicadoresDeudaVencCreditoActResult> list = dcg.gsReporte_IndicadoresDeudaVencCreditoAct(null, null, null, null, codAgenda, codVendedor, null, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos, divisor, verTodo, verCartera).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Indicadores en la base de datos.");
                }

            }
        }
    }
}
