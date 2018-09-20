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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "TipoContratoWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione TipoContratoWCF.svc o TipoContratoWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class TipoContratoWCF : ITipoContratoWCF
    {
        public List<TipoContrato_ListarResult> TipoContrato_Listar() {
            TipoContratoBL objTipoContratoBL;
            try
            {
                objTipoContratoBL  = new TipoContratoBL();
                return objTipoContratoBL.TipoContrato_Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
