using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using GS.SISGEGS.DM;
using GS.SISGEGS.BL;

namespace GS.SISGEGS.WCF
{
    public class EmpresaWCF : IEmpresaWCF
    {
        public List<Empresa_ComboBoxResult> Empresa_ComboBox()
        {
            EmpresaBL objEmpresaBL;
            try {
                objEmpresaBL = new EmpresaBL();
                return objEmpresaBL.Empresa_ComboBox();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
