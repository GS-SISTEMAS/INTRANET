﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GS.SISGEGS.WINServ.ContratosWCF {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ContratosWCF.IContratosWCF")]
    public interface IContratosWCF {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContratosWCF/ReporteGeneralContratos", ReplyAction="http://tempuri.org/IContratosWCF/ReporteGeneralContratosResponse")]
        GS.SISGEGS.DM.ReporteGeneralContratosResult[] ReporteGeneralContratos(int id_Area, int idMateria, int idTipo, int idProveedor, int idEstado, System.DateTime fechaInicio, System.DateTime fechaFin);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContratosWCF/ReporteGeneralContratos", ReplyAction="http://tempuri.org/IContratosWCF/ReporteGeneralContratosResponse")]
        System.Threading.Tasks.Task<GS.SISGEGS.DM.ReporteGeneralContratosResult[]> ReporteGeneralContratosAsync(int id_Area, int idMateria, int idTipo, int idProveedor, int idEstado, System.DateTime fechaInicio, System.DateTime fechaFin);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContratosWCF/ContratosVencer_Listar", ReplyAction="http://tempuri.org/IContratosWCF/ContratosVencer_ListarResponse")]
        GS.SISGEGS.DM.ContratosVencer_ListarResult[] ContratosVencer_Listar(int id_Area);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContratosWCF/ContratosVencer_Listar", ReplyAction="http://tempuri.org/IContratosWCF/ContratosVencer_ListarResponse")]
        System.Threading.Tasks.Task<GS.SISGEGS.DM.ContratosVencer_ListarResult[]> ContratosVencer_ListarAsync(int id_Area);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IContratosWCFChannel : GS.SISGEGS.WINServ.ContratosWCF.IContratosWCF, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ContratosWCFClient : System.ServiceModel.ClientBase<GS.SISGEGS.WINServ.ContratosWCF.IContratosWCF>, GS.SISGEGS.WINServ.ContratosWCF.IContratosWCF {
        
        public ContratosWCFClient() {
        }
        
        public ContratosWCFClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ContratosWCFClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ContratosWCFClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ContratosWCFClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public GS.SISGEGS.DM.ReporteGeneralContratosResult[] ReporteGeneralContratos(int id_Area, int idMateria, int idTipo, int idProveedor, int idEstado, System.DateTime fechaInicio, System.DateTime fechaFin) {
            return base.Channel.ReporteGeneralContratos(id_Area, idMateria, idTipo, idProveedor, idEstado, fechaInicio, fechaFin);
        }
        
        public System.Threading.Tasks.Task<GS.SISGEGS.DM.ReporteGeneralContratosResult[]> ReporteGeneralContratosAsync(int id_Area, int idMateria, int idTipo, int idProveedor, int idEstado, System.DateTime fechaInicio, System.DateTime fechaFin) {
            return base.Channel.ReporteGeneralContratosAsync(id_Area, idMateria, idTipo, idProveedor, idEstado, fechaInicio, fechaFin);
        }
        
        public GS.SISGEGS.DM.ContratosVencer_ListarResult[] ContratosVencer_Listar(int id_Area) {
            return base.Channel.ContratosVencer_Listar(id_Area);
        }
        
        public System.Threading.Tasks.Task<GS.SISGEGS.DM.ContratosVencer_ListarResult[]> ContratosVencer_ListarAsync(int id_Area) {
            return base.Channel.ContratosVencer_ListarAsync(id_Area);
        }
    }
}
