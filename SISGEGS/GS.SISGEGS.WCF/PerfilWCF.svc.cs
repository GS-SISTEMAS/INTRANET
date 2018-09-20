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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "PerfilWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione PerfilWCF.svc o PerfilWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class PerfilWCF : IPerfilWCF
    {
        public List<Perfil_ListarResult> Perfil_Listar(int idEmpresa, string descripcion)
        {
            PerfilBL objPerfilBL = new PerfilBL();
            try {
                return objPerfilBL.Perfil_Listar(idEmpresa, descripcion);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
