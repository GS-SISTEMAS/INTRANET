using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.BL
{
    public interface IEstadoContratoBL {
        List<EstadoContrato_ListarResult> EstadoContrato_Listar();
    }
    public class EstadoContratoBL
    {
        public List<EstadoContrato_ListarResult> EstadoContrato_Listar() {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.EstadoContrato_Listar().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo consultar los estado de contrato de la base de datos de GrpSilvestre");
                }
            }
        }
    }
}
