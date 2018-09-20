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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "VarianteWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione VarianteWCF.svc o VarianteWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class VarianteWCF : IVarianteWCF
    {
        public Variante_BuscarResult Variante_Buscar(int idEmpresa, string codigoVariante)
        {
            try {
                VarianteBL objVarianteBL = new VarianteBL();
                return objVarianteBL.Variante_Buscar(idEmpresa, codigoVariante);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
