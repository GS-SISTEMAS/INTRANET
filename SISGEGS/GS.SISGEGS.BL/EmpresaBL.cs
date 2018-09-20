using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GS.SISGEGS.DM;
using System.Data.Linq;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface IEmpresaBL {
        List<Empresa_ComboBoxResult> Empresa_ComboBox();
    }

    public class EmpresaBL : IEmpresaBL
    {
        public List<Empresa_ComboBoxResult> Empresa_ComboBox()
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    return dci.Empresa_ComboBox().ToList();
                }
                catch (ChangeConflictException ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al obtener la lista de empresas.");
                }
            }
        }
    }
}
