using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GS.SISGEGS.DM;

using System.ServiceModel;

namespace GS.SISGEGS.WCF
{
    [ServiceContract]
    public interface IMerlinWCF
    {
        [OperationContract]
        List<gsNotificacionesMerlinResult> NotificacionMerlin();
        [OperationContract]
        void ActualizarNotificacionMerlin(int idNotificacion);
    }
}
