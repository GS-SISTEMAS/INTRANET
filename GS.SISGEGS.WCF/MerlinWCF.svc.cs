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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "MerlinWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione MerlinWCF.svc o MerlinWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class MerlinWCF : IMerlinWCF
    {
        public List<gsNotificacionesMerlinResult> NotificacionMerlin() {
            MerlinBL objMerlinBL = new MerlinBL(); ;
            try
            {
                return objMerlinBL.NotificacionMerlin();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarNotificacionMerlin(int idNotificacion) {
            MerlinBL objMerlinBL = new MerlinBL(); ;
            try
            {
                 objMerlinBL.ActualizarNotificacionMerlin(idNotificacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
