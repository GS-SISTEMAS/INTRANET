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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "CierreCostoWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione CierreCostoWCF.svc o CierreCostoWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class CierreCostoWCF : ICierreCostoWCF
    {
        public List<gsCierreCosto_ComboBoxResult> CierreCosto_ComboBox(int idEmpresa, int codigoUsuario)
        {
            CierreCostoBL objCierreCostoBL = new CierreCostoBL();
            try
            {
                return objCierreCostoBL.CierreCosto_ComboBox(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsCierreCosto_ListarResult> CierreCosto_Listar(int idEmpresa, int codigoUsuario)
        {
            CierreCostoBL objCierreCostoBL = new CierreCostoBL();
            try {
                return objCierreCostoBL.CierreCosto_Listar(idEmpresa, codigoUsuario);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public void CierreCosto_Registrar(int idEmpresa, int codigoUsuario, int mes, int anho)
        {
            CierreCostoBL objCierreCostoBL = new CierreCostoBL();
            try {
                objCierreCostoBL.CierreCosto_Registrar(idEmpresa, codigoUsuario, mes, anho);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public List<gsDocVenta_ControlCosto_MarcaProductoResult> DocVenta_ControlCosto_MarcaProducto(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, int? kardex)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.DocVenta_ControlCosto_MarcaProducto(idEmpresa, codigoUsuario, fechaInicio, fechaFinal, kardex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsProduccion_Listar_PlanProdResult> Produccion_Listar_PlanProd(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, int? kardex)
        {
            ProduccionBL objProduccionBL = new ProduccionBL();
            try
            {
                return objProduccionBL.Produccion_Listar_PlanProd(idEmpresa, codigoUsuario, fechaInicio, fechaFinal, kardex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
