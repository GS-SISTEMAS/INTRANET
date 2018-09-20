using System;
using System.Collections.Generic;
using GS.SISGEGS.DM;
using GS.SISGEGS.BL;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "EstadoContratoWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione EstadoContratoWCF.svc o EstadoContratoWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class EstadoContratoWCF : IEstadoContratoWCF
    {
        public List<EstadoContrato_ListarResult> EstadoContrato_Listar()
        {
            EstadoContratoBL objEstadoContratoBL;
            try
            {
                objEstadoContratoBL = new EstadoContratoBL();
                return objEstadoContratoBL.EstadoContrato_Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
