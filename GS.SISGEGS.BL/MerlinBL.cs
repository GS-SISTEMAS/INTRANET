using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GS.SISGEGS.DM;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface IMerlinBL
    {
        List<gsNotificacionesMerlinResult> NotificacionMerlin();
        void ActualizarNotificacionMerlin(int idNotificacion);
    }
    public class MerlinBL : IMerlinBL
    {
        public List<gsNotificacionesMerlinResult> NotificacionMerlin() {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.gsNotificacionesMerlin().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo consultar las notificaciones pendientes de envio de Merlin");
                }
            }
        }

        public void ActualizarNotificacionMerlin(int idNotificacion) {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dci.gsNotificacionMerlin_Enviado(idNotificacion);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo marcar como enviada la notificación de Merlin");
                }
            }
        }
    }
}
