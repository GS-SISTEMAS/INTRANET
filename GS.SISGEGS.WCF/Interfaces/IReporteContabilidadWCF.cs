using GS.SISGEGS.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IReporteContabilidadWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IReporteContabilidadWCF
    {
        [OperationContract]
        List<gsVoucher_fechContaVSfechaAplicResult> Voucher_FechContaVSFechaAplic(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal);
        [OperationContract]
        List<gsFileDistGtos_ListarFechasResult> DistGtos_ListarFechas(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal);
        [OperationContract]
        List<gsDocVenta_ListarTransGratuitasResult> DocVenta_ListarTransGratuitas(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal);

        [OperationContract]
        List<Reporte_CostoProduccionResult> ReporteCostoProduccion(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal);

        [OperationContract]
        List<Reporte_CostoVentaResult> ReporteCostoVenta(int idEmpresa, int codigoUsuario, int idMoneda, DateTime fechaInicio, DateTime fechaFinal, int mesEvaluar, int kardex, int id_forma); 

        [OperationContract]
        List<Reporte_Venta_TransfResult> ReporteVenta(int idEmpresa, int codigoUsuario, DateTime fechaInicio,
            DateTime fechaFinal, int formaPago, int idMoneda, int? kardex);
        [OperationContract]
        List<ReporteAfiliadasResult> ResumenAfiliadas(int idEmpresa, int codigoUsuario, string operacion, DateTime fechaCorte, decimal moneda);
        [OperationContract]
        List<DetalleOperacionFamiliaResult> DetalleOperacionFamilia(int idEmpresa, int codigoUsuario, DateTime fechaCorte, string operacion, string id_agenda, decimal moneda);
        [OperationContract]
        List<DetalleOperacionFamiliaAnticuamientoResult> DetalleOperacionFamiliaAnticuamiento(int idEmpresa, int codigoUsuario, DateTime fechaCorte, string operacion, string id_agenda, decimal moneda);

    }
}
