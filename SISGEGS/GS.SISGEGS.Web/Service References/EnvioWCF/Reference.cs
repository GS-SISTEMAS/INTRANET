﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GS.SISGEGS.Web.EnvioWCF {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="EnvioWCF.IEnvioWCF")]
    public interface IEnvioWCF {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEnvioWCF/Envio_ListarTipo", ReplyAction="http://tempuri.org/IEnvioWCF/Envio_ListarTipoResponse")]
        GS.SISGEGS.DM.VBG00700Result[] Envio_ListarTipo(int idEmpresa, int codigoUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEnvioWCF/Envio_ListarTipo", ReplyAction="http://tempuri.org/IEnvioWCF/Envio_ListarTipoResponse")]
        System.Threading.Tasks.Task<GS.SISGEGS.DM.VBG00700Result[]> Envio_ListarTipoAsync(int idEmpresa, int codigoUsuario);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IEnvioWCFChannel : GS.SISGEGS.Web.EnvioWCF.IEnvioWCF, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class EnvioWCFClient : System.ServiceModel.ClientBase<GS.SISGEGS.Web.EnvioWCF.IEnvioWCF>, GS.SISGEGS.Web.EnvioWCF.IEnvioWCF {
        
        public EnvioWCFClient() {
        }
        
        public EnvioWCFClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public EnvioWCFClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EnvioWCFClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EnvioWCFClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public GS.SISGEGS.DM.VBG00700Result[] Envio_ListarTipo(int idEmpresa, int codigoUsuario) {
            return base.Channel.Envio_ListarTipo(idEmpresa, codigoUsuario);
        }
        
        public System.Threading.Tasks.Task<GS.SISGEGS.DM.VBG00700Result[]> Envio_ListarTipoAsync(int idEmpresa, int codigoUsuario) {
            return base.Channel.Envio_ListarTipoAsync(idEmpresa, codigoUsuario);
        }
    }
}