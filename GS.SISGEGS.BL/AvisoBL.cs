using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GS.SISGEGS.DM;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface IAvisoBL {
        List<Aviso_PublicarDashboardResult> Aviso_PublicarDashboard(DateTime fecha);
    }

    public class AvisoBL : IAvisoBL
    {
        public List<Aviso_PublicarDashboardResult> Aviso_PublicarDashboard(DateTime fecha)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.Aviso_PublicarDashboard(fecha).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
            }
        }
    }
}
