using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GS.SISGEGS.DM;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface ICreditoBL {
        List<gsCredito_ListarCondicionResult> Credito_ListarCondicion(int idEmpresa, int codigoUsuario, string idAgenda);
    }

    public class CreditoBL : ICreditoBL
    {
        public List<gsCredito_ListarCondicionResult> Credito_ListarCondicion(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsCredito_ListarCondicion(idAgenda, 2).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las condiciones de pago en la base de datos.");
                }

            }
        }
    }
}
