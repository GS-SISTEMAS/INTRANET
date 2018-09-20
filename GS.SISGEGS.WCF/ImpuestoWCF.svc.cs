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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ImpuestoWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ImpuestoWCF.svc o ImpuestoWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ImpuestoWCF : IImpuestoWCF
    {
        public List<gsImpuesto_BuscarPorPedidoResult> Impuesto_BuscarPorPedido(int idEmpresa, int codigoUsuario, int idPedido)
        {
            ImpuestoBL objImpuestoBL;
            try
            {
                objImpuestoBL = new ImpuestoBL();
                return objImpuestoBL.Impuesto_BuscarPorPedido(idEmpresa, codigoUsuario, idPedido);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsImpuesto_ListarPorItemResult> Impuesto_ListarPorItem(int idEmpresa, int codigoUsuario, string idItem, DateTime fecha)
        {
            ImpuestoBL objImpuestoBL;
            try
            {
                objImpuestoBL = new ImpuestoBL();
                return objImpuestoBL.Impuesto_ListarPorItem(idEmpresa, codigoUsuario, idItem, fecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
