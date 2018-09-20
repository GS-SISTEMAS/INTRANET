using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GS.SISGEGS.DM;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface IConfigIndicadoresBL {
        gsConfigIndicadores_RentabilidadResult ConfigIndicadores_Rentabilidad(int idEmpresa, int codigoUsuario, DateTime fecha);
    }

    public class ConfigIndicadoresBL : IConfigIndicadoresBL
    {
        public gsConfigIndicadores_RentabilidadResult ConfigIndicadores_Rentabilidad(int idEmpresa, int codigoUsuario, DateTime fecha)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    using (dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4))))
                    {
                        return dcg.gsConfigIndicadores_Rentabilidad(fecha).Single();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
