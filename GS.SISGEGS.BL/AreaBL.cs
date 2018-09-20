using System;
using System.Collections.Generic;
using System.Linq;
using GS.SISGEGS.DM;
using System.Configuration;
namespace GS.SISGEGS.BL
{
    public interface IAreaBL {
        List<gsArea_ListarResult> Listar_Areas(int idEmpresa, int codigoUsuario);
    }
    public class AreaBL: IAreaBL 
    {
        public List<gsArea_ListarResult> Listar_Areas(int idEmpresa, int codigoUsuario) {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))

            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    return dcg.gsArea_Listar().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar por las referencias de la sucursal en la base de datos.");
                }
            }
            
        }
    }
}
