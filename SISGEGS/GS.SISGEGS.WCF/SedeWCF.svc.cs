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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "SedeWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione SedeWCF.svc o SedeWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class SedeWCF : ISedeWCF
    {
        public List<VBG02689Result> RRHHSede_Listar(int idEmpresa, int codigoUsuario)
        {
            SedeBL objSedeBL;
            try
            {
                objSedeBL = new SedeBL();
                return objSedeBL.RRHHSede_Listar(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
