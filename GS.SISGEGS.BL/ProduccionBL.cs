using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GS.SISGEGS.DM;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface IProduccionBL {
        List<gsProduccion_Listar_PlanProdResult> Produccion_Listar_PlanProd(int idEmpresa, int codigoUsuario, DateTime fechaInicio,
            DateTime fechaFinal, int? kardex);
    }

    public class ProduccionBL : IProduccionBL
    {
        public List<gsProduccion_Listar_PlanProdResult> Produccion_Listar_PlanProd(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, int? kardex)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsProduccion_Listar_PlanProd(0, fechaInicio, fechaFinal, kardex).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede listar los planes de producción.");
                }

            }
        }
    }
}
