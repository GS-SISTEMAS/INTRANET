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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ResponsableContratoWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ResponsableContratoWCF.svc o ResponsableContratoWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ResponsableContratoWCF : IResponsableContratoWCF
    {
        public List<ResponsablesContrato_ListarResult> ResponsablesContrato_Listar() {

            ResponsableContratoBL objResponsableBL;
            try
            {
                objResponsableBL = new ResponsableContratoBL();
                return objResponsableBL.ResponsablesContrato_Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
