﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GS.SISGEGS.Web.IndicadoresWCF {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="IndicadoresWCF.IIndicadoresWCF")]
    public interface IIndicadoresWCF {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IIndicadoresWCF/Indicadores_CreditosCobranzas", ReplyAction="http://tempuri.org/IIndicadoresWCF/Indicadores_CreditosCobranzasResponse")]
        GS.SISGEGS.DM.gsReporte_IndicadoresCreditosResult[] Indicadores_CreditosCobranzas(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, System.DateTime fechaEmisionInicial, System.DateTime fechaEmisionFinal, System.DateTime fechaVencimientoInicial, System.DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IIndicadoresWCF/Indicadores_CreditosCobranzas", ReplyAction="http://tempuri.org/IIndicadoresWCF/Indicadores_CreditosCobranzasResponse")]
        System.Threading.Tasks.Task<GS.SISGEGS.DM.gsReporte_IndicadoresCreditosResult[]> Indicadores_CreditosCobranzasAsync(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, System.DateTime fechaEmisionInicial, System.DateTime fechaEmisionFinal, System.DateTime fechaVencimientoInicial, System.DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IIndicadoresWCF/Indicadores_LetrasRenyRef", ReplyAction="http://tempuri.org/IIndicadoresWCF/Indicadores_LetrasRenyRefResponse")]
        GS.SISGEGS.DM.GS_ReporteLtasREF_RENOVResult[] Indicadores_LetrasRenyRef(int idEmpresa, int codigoUsuario, System.DateTime fechaCorte);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IIndicadoresWCF/Indicadores_LetrasRenyRef", ReplyAction="http://tempuri.org/IIndicadoresWCF/Indicadores_LetrasRenyRefResponse")]
        System.Threading.Tasks.Task<GS.SISGEGS.DM.GS_ReporteLtasREF_RENOVResult[]> Indicadores_LetrasRenyRefAsync(int idEmpresa, int codigoUsuario, System.DateTime fechaCorte);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IIndicadoresWCF/Indicadores_LetrasProtestadas", ReplyAction="http://tempuri.org/IIndicadoresWCF/Indicadores_LetrasProtestadasResponse")]
        GS.SISGEGS.DM.GS_ReporteIndicadorLetrasProtestadasResult[] Indicadores_LetrasProtestadas(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, System.DateTime fechaEmisionInicial, System.DateTime fechaEmisionFinal, System.DateTime fechaVencimientoInicial, System.DateTime fechaVencimientoFinal, int vencidos);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IIndicadoresWCF/Indicadores_LetrasProtestadas", ReplyAction="http://tempuri.org/IIndicadoresWCF/Indicadores_LetrasProtestadasResponse")]
        System.Threading.Tasks.Task<GS.SISGEGS.DM.GS_ReporteIndicadorLetrasProtestadasResult[]> Indicadores_LetrasProtestadasAsync(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, System.DateTime fechaEmisionInicial, System.DateTime fechaEmisionFinal, System.DateTime fechaVencimientoInicial, System.DateTime fechaVencimientoFinal, int vencidos);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IIndicadoresWCF/Indicadores_DeudaVencida", ReplyAction="http://tempuri.org/IIndicadoresWCF/Indicadores_DeudaVencidaResponse")]
        GS.SISGEGS.DM.gsReporte_IndicadoresDeudaVencidaResult[] Indicadores_DeudaVencida(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, System.DateTime fechaEmisionInicial, System.DateTime fechaEmisionFinal, System.DateTime fechaVencimientoInicial, System.DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IIndicadoresWCF/Indicadores_DeudaVencida", ReplyAction="http://tempuri.org/IIndicadoresWCF/Indicadores_DeudaVencidaResponse")]
        System.Threading.Tasks.Task<GS.SISGEGS.DM.gsReporte_IndicadoresDeudaVencidaResult[]> Indicadores_DeudaVencidaAsync(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, System.DateTime fechaEmisionInicial, System.DateTime fechaEmisionFinal, System.DateTime fechaVencimientoInicial, System.DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IIndicadoresWCF/Indicadores_DeudaVencidaCreditoAct", ReplyAction="http://tempuri.org/IIndicadoresWCF/Indicadores_DeudaVencidaCreditoActResponse")]
        GS.SISGEGS.DM.gsReporte_IndicadoresDeudaVencCreditoActResult[] Indicadores_DeudaVencidaCreditoAct(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, System.DateTime fechaEmisionInicial, System.DateTime fechaEmisionFinal, System.DateTime fechaVencimientoInicial, System.DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IIndicadoresWCF/Indicadores_DeudaVencidaCreditoAct", ReplyAction="http://tempuri.org/IIndicadoresWCF/Indicadores_DeudaVencidaCreditoActResponse")]
        System.Threading.Tasks.Task<GS.SISGEGS.DM.gsReporte_IndicadoresDeudaVencCreditoActResult[]> Indicadores_DeudaVencidaCreditoActAsync(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, System.DateTime fechaEmisionInicial, System.DateTime fechaEmisionFinal, System.DateTime fechaVencimientoInicial, System.DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IIndicadoresWCFChannel : GS.SISGEGS.Web.IndicadoresWCF.IIndicadoresWCF, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class IndicadoresWCFClient : System.ServiceModel.ClientBase<GS.SISGEGS.Web.IndicadoresWCF.IIndicadoresWCF>, GS.SISGEGS.Web.IndicadoresWCF.IIndicadoresWCF {
        
        public IndicadoresWCFClient() {
        }
        
        public IndicadoresWCFClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public IndicadoresWCFClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IndicadoresWCFClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IndicadoresWCFClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public GS.SISGEGS.DM.gsReporte_IndicadoresCreditosResult[] Indicadores_CreditosCobranzas(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, System.DateTime fechaEmisionInicial, System.DateTime fechaEmisionFinal, System.DateTime fechaVencimientoInicial, System.DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera) {
            return base.Channel.Indicadores_CreditosCobranzas(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos, divisor, verTodo, verCartera);
        }
        
        public System.Threading.Tasks.Task<GS.SISGEGS.DM.gsReporte_IndicadoresCreditosResult[]> Indicadores_CreditosCobranzasAsync(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, System.DateTime fechaEmisionInicial, System.DateTime fechaEmisionFinal, System.DateTime fechaVencimientoInicial, System.DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera) {
            return base.Channel.Indicadores_CreditosCobranzasAsync(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos, divisor, verTodo, verCartera);
        }
        
        public GS.SISGEGS.DM.GS_ReporteLtasREF_RENOVResult[] Indicadores_LetrasRenyRef(int idEmpresa, int codigoUsuario, System.DateTime fechaCorte) {
            return base.Channel.Indicadores_LetrasRenyRef(idEmpresa, codigoUsuario, fechaCorte);
        }
        
        public System.Threading.Tasks.Task<GS.SISGEGS.DM.GS_ReporteLtasREF_RENOVResult[]> Indicadores_LetrasRenyRefAsync(int idEmpresa, int codigoUsuario, System.DateTime fechaCorte) {
            return base.Channel.Indicadores_LetrasRenyRefAsync(idEmpresa, codigoUsuario, fechaCorte);
        }
        
        public GS.SISGEGS.DM.GS_ReporteIndicadorLetrasProtestadasResult[] Indicadores_LetrasProtestadas(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, System.DateTime fechaEmisionInicial, System.DateTime fechaEmisionFinal, System.DateTime fechaVencimientoInicial, System.DateTime fechaVencimientoFinal, int vencidos) {
            return base.Channel.Indicadores_LetrasProtestadas(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos);
        }
        
        public System.Threading.Tasks.Task<GS.SISGEGS.DM.GS_ReporteIndicadorLetrasProtestadasResult[]> Indicadores_LetrasProtestadasAsync(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, System.DateTime fechaEmisionInicial, System.DateTime fechaEmisionFinal, System.DateTime fechaVencimientoInicial, System.DateTime fechaVencimientoFinal, int vencidos) {
            return base.Channel.Indicadores_LetrasProtestadasAsync(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos);
        }
        
        public GS.SISGEGS.DM.gsReporte_IndicadoresDeudaVencidaResult[] Indicadores_DeudaVencida(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, System.DateTime fechaEmisionInicial, System.DateTime fechaEmisionFinal, System.DateTime fechaVencimientoInicial, System.DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera) {
            return base.Channel.Indicadores_DeudaVencida(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos, divisor, verTodo, verCartera);
        }
        
        public System.Threading.Tasks.Task<GS.SISGEGS.DM.gsReporte_IndicadoresDeudaVencidaResult[]> Indicadores_DeudaVencidaAsync(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, System.DateTime fechaEmisionInicial, System.DateTime fechaEmisionFinal, System.DateTime fechaVencimientoInicial, System.DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera) {
            return base.Channel.Indicadores_DeudaVencidaAsync(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos, divisor, verTodo, verCartera);
        }
        
        public GS.SISGEGS.DM.gsReporte_IndicadoresDeudaVencCreditoActResult[] Indicadores_DeudaVencidaCreditoAct(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, System.DateTime fechaEmisionInicial, System.DateTime fechaEmisionFinal, System.DateTime fechaVencimientoInicial, System.DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera) {
            return base.Channel.Indicadores_DeudaVencidaCreditoAct(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos, divisor, verTodo, verCartera);
        }
        
        public System.Threading.Tasks.Task<GS.SISGEGS.DM.gsReporte_IndicadoresDeudaVencCreditoActResult[]> Indicadores_DeudaVencidaCreditoActAsync(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, System.DateTime fechaEmisionInicial, System.DateTime fechaEmisionFinal, System.DateTime fechaVencimientoInicial, System.DateTime fechaVencimientoFinal, int vencidos, int divisor, int verTodo, int verCartera) {
            return base.Channel.Indicadores_DeudaVencidaCreditoActAsync(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos, divisor, verTodo, verCartera);
        }
    }
}