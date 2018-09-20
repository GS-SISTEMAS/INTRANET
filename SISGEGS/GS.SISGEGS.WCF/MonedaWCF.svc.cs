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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "MonedaWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione MonedaWCF.svc o MonedaWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class MonedaWCF : IMonedaWCF
    {
        public List<VBG00187Result> Moneda_Listar(int idEmpresa, int codigoUsuario)
        {
            MonedaBL objMonedaBL;
            try {
                objMonedaBL = new MonedaBL();
                return objMonedaBL.Moneda_Listar(idEmpresa, codigoUsuario);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
