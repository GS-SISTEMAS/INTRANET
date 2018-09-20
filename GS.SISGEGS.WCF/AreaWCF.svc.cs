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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "AreaWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione AreaWCF.svc o AreaWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class AreaWCF : IAreaWCF
    {
        public List<gsArea_ListarResult> Listar_Areas(int idEmpresa, int codigoUsuario)
        {
            AreaBL objAreaBL;
            try
            {
                objAreaBL = new AreaBL();
                return objAreaBL.Listar_Areas(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }   
    }
}
 