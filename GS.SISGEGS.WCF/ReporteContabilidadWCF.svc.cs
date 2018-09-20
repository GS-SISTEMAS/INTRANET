using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GS.SISGEGS.BL;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ReporteContabilidadWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ReporteContabilidadWCF.svc o ReporteContabilidadWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ReporteContabilidadWCF : IReporteContabilidadWCF
    {
        public List<gsFileDistGtos_ListarFechasResult> DistGtos_ListarFechas(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal)
        {
            DistribGastoBL objDistribGastoBL = new DistribGastoBL();
            try
            {
                return objDistribGastoBL.DistGtos_ListarFechas(idEmpresa, codigoUsuario, fechaInicio, fechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsDocVenta_ListarTransGratuitasResult> DocVenta_ListarTransGratuitas(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.DocVenta_ListarTransGratuitas(idEmpresa, codigoUsuario, fechaInicio, fechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsVoucher_fechContaVSfechaAplicResult> Voucher_FechContaVSFechaAplic(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal)
        {
            VoucherBL objVoucherBL = new VoucherBL();
            try
            {
                return objVoucherBL.Voucher_FechContaVSFechaAplic(idEmpresa, codigoUsuario, fechaInicio, fechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------- SWF - inclouds

        public List<Reporte_CostoProduccionResult> ReporteCostoProduccion(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal)
        {
            ReportesBL objReportesBL = new ReportesBL();
            List<Reporte_CostoProduccionResult> lista = new List<Reporte_CostoProduccionResult>();
            try
            {
                lista = objReportesBL.ReporteCostoProduccion(idEmpresa, codigoUsuario, fechaInicio, fechaFinal);
                return lista; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Reporte_CostoVentaResult> ReporteCostoVenta(int idEmpresa, int codigoUsuario, int idMoneda, DateTime fechaInicio, DateTime fechaFinal, int mesEvaluar, int kardex, int id_forma)
        {
            ReportesBL objReportesBL = new ReportesBL();
            try
            {
                return objReportesBL.ReporteCostoVenta(idEmpresa, codigoUsuario, idMoneda, fechaInicio, fechaFinal, mesEvaluar, kardex, id_forma);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Reporte_Venta_TransfResult> ReporteVenta(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, int formaPago, int idMoneda, int? kardex)
        {
            ReportesBL objReportesBL = new ReportesBL();
            try
            {
                return objReportesBL.ReporteVenta(idEmpresa, codigoUsuario, fechaInicio, fechaFinal, formaPago, idMoneda, kardex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReporteAfiliadasResult> ResumenAfiliadas(int idEmpresa, int codigoUsuario, string operacion, DateTime fechaCorte, decimal moneda)
        {
            ReportesBL objReportesBL = new ReportesBL();
            try
            {
                return objReportesBL.ResumenAfiliadas(idEmpresa, codigoUsuario, operacion, fechaCorte, moneda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DetalleOperacionFamiliaResult> DetalleOperacionFamilia(int idEmpresa, int codigoUsuario, DateTime fechaCorte, string operacion, string id_agenda, decimal moneda)
        {
            ReportesBL objReportesBL = new ReportesBL();
            try
            {
                return objReportesBL.DetalleOperacionFamilia(idEmpresa, codigoUsuario, fechaCorte, operacion, id_agenda, moneda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DetalleOperacionFamiliaAnticuamientoResult> DetalleOperacionFamiliaAnticuamiento(int idEmpresa, int codigoUsuario, DateTime fechaCorte, string operacion, string id_agenda, decimal moneda)
        {
            ReportesBL objReportesBL = new ReportesBL();
            try
            {
                return objReportesBL.DetalleOperacionFamiliaAnticuamiento(idEmpresa, codigoUsuario, fechaCorte, operacion, id_agenda, moneda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
