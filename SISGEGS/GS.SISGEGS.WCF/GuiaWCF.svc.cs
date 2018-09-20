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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "GuiaWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione GuiaWCF.svc o GuiaWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class GuiaWCF : IGuiaWCF
    {
        public List<gsGuiaVenta_ListarXPedidoResult> GuiaVenta_ListarxPedido(int idEmpresa, int codigoUsuario, int idPedido)
        {
            try {
                GuiaBL objGuiaBL = new GuiaBL();
                return objGuiaBL.GuiaVenta_ListarxPedido(idEmpresa, codigoUsuario, idPedido);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
