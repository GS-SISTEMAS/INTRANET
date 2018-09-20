using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GS.SISGEGS.DM;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface INaturalezaGastoBL {
        List<VBG03096Result> NaturalezaGasto_ListarImputables(int idEmpresa, int codigoUsuario);
    }

    public class NaturalezaGastoBL : INaturalezaGastoBL
    {
        public List<VBG03096Result> NaturalezaGasto_ListarImputables(int idEmpresa, int codigoUsuario)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.VBG03096("%", null).ToList().FindAll(x => x.Imputable == true).OrderBy(x => x.CentroCostos).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las naturaleza del gasto en la base de datos.");
                }

            }
        }
    }
}
