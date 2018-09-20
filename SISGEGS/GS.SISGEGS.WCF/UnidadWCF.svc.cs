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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "UnidadWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione UnidadWCF.svc o UnidadWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class UnidadWCF : IUnidadWCF
    {
        public List<VBG02665Result> UnidadGestion_ListarImputables(int idEmpresa, int codigoUsuario)
        {
            UnidadBL objUnidadBL;
            try {
                objUnidadBL = new UnidadBL();
                return objUnidadBL.UnidadGestion_ListarImputables(idEmpresa, codigoUsuario);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public List<VBG02668Result> UnidadProyecto_ListarImputables(int idEmpresa, int codigoUsuario)
        {
            UnidadBL objUnidadBL;
            try
            {
                objUnidadBL = new UnidadBL();
                return objUnidadBL.UnidadProyecto_ListarImputables(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
