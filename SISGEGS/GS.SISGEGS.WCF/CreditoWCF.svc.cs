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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "CreditoWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione CreditoWCF.svc o CreditoWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class CreditoWCF : ICreditoWCF
    {
        public List<gsCredito_ListarCondicionResult> Credito_ListarCondicion(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            CreditoBL objCreditoBL;
            try
            {
                objCreditoBL = new CreditoBL();
                return objCreditoBL.Credito_ListarCondicion(idEmpresa, codigoUsuario, idAgenda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
