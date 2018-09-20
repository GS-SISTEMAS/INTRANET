using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GS.SISGEGS.DM;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface IDireccionBL {
        List<VBG00209Result> Direccion_ListarCliente(int idEmpresa, int codigoUsuario, string idAgenda);
        List<VBG03679Result> Direccion_ListarReferencia(int idEmpresa, int codigoUsuario, string idAgenda, int? idSucursal, int? idReferencia);
    }
    public class DireccionBL : IDireccionBL
    {
        public List<VBG00209Result> Direccion_ListarCliente(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.VBG00209("Agenda", idAgenda).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al listar las direcciones del cliente en la fuente de datos.");
                }
            }
        }

        public List<VBG03679Result> Direccion_ListarReferencia(int idEmpresa, int codigoUsuario, string idAgenda, int? idSucursal, int? idReferencia)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.VBG03679(idAgenda, idSucursal, idReferencia).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al listar las direcciones de la referencia en la fuente de datos.");
                }
            }
        }
    }
}
