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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "EnvioWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione EnvioWCF.svc o EnvioWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class EnvioWCF : IEnvioWCF
    {
        public List<VBG00700Result> Envio_ListarTipo(int idEmpresa, int codigoUsuario)
        {
            EnvioBL objEnvioBL;
            try
            {
                objEnvioBL = new EnvioBL();
                return objEnvioBL.Envio_ListarTipo(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
