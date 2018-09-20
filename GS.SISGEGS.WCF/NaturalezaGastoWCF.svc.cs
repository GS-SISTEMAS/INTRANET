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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "NaturalezaGastoWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione NaturalezaGastoWCF.svc o NaturalezaGastoWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class NaturalezaGastoWCF : INaturalezaGastoWCF
    {
        public List<VBG03096Result> NaturalezaGasto_ListarImputables(int idEmpresa, int codigoUsuario)
        {
            NaturalezaGastoBL objNaturalezaGastoBL;
            try {
                objNaturalezaGastoBL = new NaturalezaGastoBL();
                return objNaturalezaGastoBL.NaturalezaGasto_ListarImputables(idEmpresa, codigoUsuario);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
