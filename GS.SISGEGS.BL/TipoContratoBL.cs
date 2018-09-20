using System;
using System.Collections.Generic;
using System.Linq;
using GS.SISGEGS.DM;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface ITipoContratoBL {
        List<TipoContrato_ListarResult> TipoContrato_Listar();
    }
    public class TipoContratoBL:ITipoContratoBL
    {
        public List<TipoContrato_ListarResult> TipoContrato_Listar() {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.TipoContrato_Listar().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo consultar el tipo de contrato de la base de datos de GrpSilvestre");
                }
            }
        }
    }
}
