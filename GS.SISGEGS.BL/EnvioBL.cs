using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GS.SISGEGS.DM;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface IEnvioBL {
        List<VBG00700Result> Envio_ListarTipo(int idEmpresa, int codigoUsuario);
    }

    public class EnvioBL : IEnvioBL
    {
        public List<VBG00700Result> Envio_ListarTipo(int idEmpresa, int codigoUsuario)
        {
            //using (dmIntranetDataContext dcgs = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))

            {
                //dmGenesysDataContext dc = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dcgs.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {

                    return dcg.VBG00700().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los tipos de envio en la base de datos.");
                }

            }
        }
    }
}
