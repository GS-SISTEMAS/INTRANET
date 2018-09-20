using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using GS.SISGEGS.DM;
namespace GS.SISGEGS.BL
{
    public interface IResponsableContratoBL {
        List<ResponsablesContrato_ListarResult> ResponsablesContrato_Listar();
    }
    public class ResponsableContratoBL:IResponsableContratoBL
    {
        public List<ResponsablesContrato_ListarResult> ResponsablesContrato_Listar() {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.ResponsablesContrato_Listar().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo consultar los responsables de contrato de la base de datos de GrpSilvestre");
                }
            }
        }
    }
}
