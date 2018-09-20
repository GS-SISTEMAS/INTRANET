using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GS.SISGEGS.DM;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface ICentroCostoBL {
        List<VBG00786Result> CentroCosto_ListarImputables(int idEmpresa, int codigoUsuario);

        List<VBG00786Result> CentroCosto_Listar(int idEmpresa, int codigoUsuario, string nombre);

        List<gsBuscarCentroCosto_IntranetResult> BuscarCentroCosto_Intranet(int idEmpresa, int codigoUsuario, bool flagRendGasto);
    }

    public class CentroCostoBL : ICentroCostoBL
    {
        public List<VBG00786Result> CentroCosto_ListarImputables(int idEmpresa, int codigoUsuario)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<VBG00786Result> Lista = new List<VBG00786Result>(); 
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    Lista = dcg.VBG00786(null, null, null, null, null, null).ToList();
                    // dcg.VBG00786("%", null, null, null, null).ToList().FindAll(x => x.Imputable == true).OrderBy(x=>x.CentroCostos).ToList();

                    return Lista.FindAll(x => x.Imputable == 1).OrderBy(x => x.CentroCostos).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los centros de costo en la base de datos.");
                }

            }
        }

        public List<VBG00786Result> CentroCosto_Listar(int idEmpresa, int codigoUsuario, string nombre)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.VBG00786(null, nombre + "%", null, null, null, null).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los centros de costo en la base de datos.");
                }

            }
        }

        public List<gsBuscarCentroCosto_IntranetResult> BuscarCentroCosto_Intranet(int idEmpresa, int codigoUsuario, bool flagRendGasto) {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<gsBuscarCentroCosto_IntranetResult> Lista = new List<gsBuscarCentroCosto_IntranetResult>();
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    Lista = dcg.gsBuscarCentroCosto_Intranet(null, null, null, null, null, null, flagRendGasto).ToList();

                    return Lista.FindAll(x => x.Imputable == 1).OrderBy(x => x.CentroCostos).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los centros de costo en la base de datos.");
                }

            }
        }
    }
}
