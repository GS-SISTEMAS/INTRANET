using System;
using System.Collections.Generic;
using System.ServiceModel;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IIndicadoresWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IIndicadoresWCF
    {
        [OperationContract]
        List<gsReporte_IndicadoresCreditosResult> Indicadores_CreditosCobranzas(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera);

        [OperationContract]
        List<GS_ReporteLtasREF_RENOVResult> Indicadores_LetrasRenyRef(int idEmpresa, int codigoUsuario, DateTime fechaCorte);

        [OperationContract]
        List<GS_ReporteIndicadorLetrasProtestadasResult> Indicadores_LetrasProtestadas(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos);
       
        [OperationContract]
        List<gsReporte_IndicadoresDeudaVencidaResult> Indicadores_DeudaVencida(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera);

        [OperationContract]
        List<gsReporte_IndicadoresDeudaVencCreditoActResult> Indicadores_DeudaVencidaCreditoAct(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera);

    }
}
