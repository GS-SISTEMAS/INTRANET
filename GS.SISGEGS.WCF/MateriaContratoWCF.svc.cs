using System;
using System.Collections.Generic;
using GS.SISGEGS.DM;
using GS.SISGEGS.BL;
namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "MateriaContratoWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione MateriaContratoWCF.svc o MateriaContratoWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class MateriaContratoWCF : IMateriaContratoWCF
    {
        public List<MateriaContrato_ListarResult> MateriaContrato_Listar() {
            MateriaContratoBL objMateriaContratoBL;
            try
            {
                objMateriaContratoBL = new MateriaContratoBL();
                return objMateriaContratoBL.MateriaContrato_Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
