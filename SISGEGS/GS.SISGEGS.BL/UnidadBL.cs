using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GS.SISGEGS.DM;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface IUnidadBL
    {
        List<VBG02665Result> UnidadGestion_ListarImputables(int idEmpresa, int codigoUsuario);
        List<VBG02668Result> UnidadProyecto_ListarImputables(int idEmpresa, int codigoUsuario);
    }

    public class UnidadBL : IUnidadBL
    {
        public List<VBG02665Result> UnidadGestion_ListarImputables(int idEmpresa, int codigoUsuario)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.VBG02665("%", null, null, null, null).ToList().FindAll(x => x.Imputable == true).OrderBy(x => x.UnidadGestion).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las unidades de gestión en la base de datos.");
                }

            }
        }

        public List<VBG02668Result> UnidadProyecto_ListarImputables(int idEmpresa, int codigoUsuario)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.VBG02668("%", null, null, null, null).ToList().FindAll(x => x.Imputable == true).OrderBy(x => x.UnidadProyecto).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las unidades de gestión en la base de datos.");
                }

            }
        }
    }
}
