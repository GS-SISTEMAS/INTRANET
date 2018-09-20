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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "CentroCostoWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione CentroCostoWCF.svc o CentroCostoWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class CentroCostoWCF : ICentroCostoWCF
    {
        public List<VBG00786Result> CentroCosto_ListarImputables(int idEmpresa, int codigoUsuario)
        {
            CentroCostoBL objCentroCostoBL;
            try {
                objCentroCostoBL = new CentroCostoBL();
                return objCentroCostoBL.CentroCosto_ListarImputables(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
