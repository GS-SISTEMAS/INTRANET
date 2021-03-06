﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GS.SISGEGS.Web.LoginWCF {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="LoginWCF.ILoginWCF")]
    public interface ILoginWCF {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginWCF/Usuario_Autenticar", ReplyAction="http://tempuri.org/ILoginWCF/Usuario_AutenticarResponse")]
        GS.SISGEGS.DM.Usuario_AutenticarResult Usuario_Autenticar(int idEmpresa, string codigo, string contrasena);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginWCF/Usuario_Autenticar", ReplyAction="http://tempuri.org/ILoginWCF/Usuario_AutenticarResponse")]
        System.Threading.Tasks.Task<GS.SISGEGS.DM.Usuario_AutenticarResult> Usuario_AutenticarAsync(int idEmpresa, string codigo, string contrasena);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginWCF/Usuario_Login", ReplyAction="http://tempuri.org/ILoginWCF/Usuario_LoginResponse")]
        GS.SISGEGS.DM.Usuario_LoginResult Usuario_Login(int idUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginWCF/Usuario_Login", ReplyAction="http://tempuri.org/ILoginWCF/Usuario_LoginResponse")]
        System.Threading.Tasks.Task<GS.SISGEGS.DM.Usuario_LoginResult> Usuario_LoginAsync(int idUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginWCF/Usuario_CambiarContrasena", ReplyAction="http://tempuri.org/ILoginWCF/Usuario_CambiarContrasenaResponse")]
        void Usuario_CambiarContrasena(int idEmpresa, int codigoUsuario, int idUsuario, string contrasena, bool cambiarAmbos);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginWCF/Usuario_CambiarContrasena", ReplyAction="http://tempuri.org/ILoginWCF/Usuario_CambiarContrasenaResponse")]
        System.Threading.Tasks.Task Usuario_CambiarContrasenaAsync(int idEmpresa, int codigoUsuario, int idUsuario, string contrasena, bool cambiarAmbos);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginWCF/Menu_CargarInicio", ReplyAction="http://tempuri.org/ILoginWCF/Menu_CargarInicioResponse")]
        GS.SISGEGS.Web.LoginWCF.Menu_CargarInicioResponse Menu_CargarInicio(GS.SISGEGS.Web.LoginWCF.Menu_CargarInicioRequest request);
        
        // CODEGEN: Generando contrato de mensaje, ya que la operación tiene múltiples valores de devolución.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginWCF/Menu_CargarInicio", ReplyAction="http://tempuri.org/ILoginWCF/Menu_CargarInicioResponse")]
        System.Threading.Tasks.Task<GS.SISGEGS.Web.LoginWCF.Menu_CargarInicioResponse> Menu_CargarInicioAsync(GS.SISGEGS.Web.LoginWCF.Menu_CargarInicioRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginWCF/AuditoriaMenu_Registrar", ReplyAction="http://tempuri.org/ILoginWCF/AuditoriaMenu_RegistrarResponse")]
        void AuditoriaMenu_Registrar(string url, string nombreDispositivo, int idUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginWCF/AuditoriaMenu_Registrar", ReplyAction="http://tempuri.org/ILoginWCF/AuditoriaMenu_RegistrarResponse")]
        System.Threading.Tasks.Task AuditoriaMenu_RegistrarAsync(string url, string nombreDispositivo, int idUsuario);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Menu_CargarInicio", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Menu_CargarInicioRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public int idEmpresa;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public int codigoUsuario;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public int idPerfil;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=3)]
        public GS.SISGEGS.DM.VBG00004Result objEmpresa;
        
        public Menu_CargarInicioRequest() {
        }
        
        public Menu_CargarInicioRequest(int idEmpresa, int codigoUsuario, int idPerfil, GS.SISGEGS.DM.VBG00004Result objEmpresa) {
            this.idEmpresa = idEmpresa;
            this.codigoUsuario = codigoUsuario;
            this.idPerfil = idPerfil;
            this.objEmpresa = objEmpresa;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Menu_CargarInicioResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Menu_CargarInicioResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public GS.SISGEGS.DM.Menu_CargarInicioResult[] Menu_CargarInicioResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public GS.SISGEGS.DM.VBG00004Result objEmpresa;
        
        public Menu_CargarInicioResponse() {
        }
        
        public Menu_CargarInicioResponse(GS.SISGEGS.DM.Menu_CargarInicioResult[] Menu_CargarInicioResult, GS.SISGEGS.DM.VBG00004Result objEmpresa) {
            this.Menu_CargarInicioResult = Menu_CargarInicioResult;
            this.objEmpresa = objEmpresa;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILoginWCFChannel : GS.SISGEGS.Web.LoginWCF.ILoginWCF, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LoginWCFClient : System.ServiceModel.ClientBase<GS.SISGEGS.Web.LoginWCF.ILoginWCF>, GS.SISGEGS.Web.LoginWCF.ILoginWCF {
        
        public LoginWCFClient() {
        }
        
        public LoginWCFClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public LoginWCFClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LoginWCFClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LoginWCFClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public GS.SISGEGS.DM.Usuario_AutenticarResult Usuario_Autenticar(int idEmpresa, string codigo, string contrasena) {
            return base.Channel.Usuario_Autenticar(idEmpresa, codigo, contrasena);
        }
        
        public System.Threading.Tasks.Task<GS.SISGEGS.DM.Usuario_AutenticarResult> Usuario_AutenticarAsync(int idEmpresa, string codigo, string contrasena) {
            return base.Channel.Usuario_AutenticarAsync(idEmpresa, codigo, contrasena);
        }
        
        public GS.SISGEGS.DM.Usuario_LoginResult Usuario_Login(int idUsuario) {
            return base.Channel.Usuario_Login(idUsuario);
        }
        
        public System.Threading.Tasks.Task<GS.SISGEGS.DM.Usuario_LoginResult> Usuario_LoginAsync(int idUsuario) {
            return base.Channel.Usuario_LoginAsync(idUsuario);
        }
        
        public void Usuario_CambiarContrasena(int idEmpresa, int codigoUsuario, int idUsuario, string contrasena, bool cambiarAmbos) {
            base.Channel.Usuario_CambiarContrasena(idEmpresa, codigoUsuario, idUsuario, contrasena, cambiarAmbos);
        }
        
        public System.Threading.Tasks.Task Usuario_CambiarContrasenaAsync(int idEmpresa, int codigoUsuario, int idUsuario, string contrasena, bool cambiarAmbos) {
            return base.Channel.Usuario_CambiarContrasenaAsync(idEmpresa, codigoUsuario, idUsuario, contrasena, cambiarAmbos);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        GS.SISGEGS.Web.LoginWCF.Menu_CargarInicioResponse GS.SISGEGS.Web.LoginWCF.ILoginWCF.Menu_CargarInicio(GS.SISGEGS.Web.LoginWCF.Menu_CargarInicioRequest request) {
            return base.Channel.Menu_CargarInicio(request);
        }
        
        public GS.SISGEGS.DM.Menu_CargarInicioResult[] Menu_CargarInicio(int idEmpresa, int codigoUsuario, int idPerfil, ref GS.SISGEGS.DM.VBG00004Result objEmpresa) {
            GS.SISGEGS.Web.LoginWCF.Menu_CargarInicioRequest inValue = new GS.SISGEGS.Web.LoginWCF.Menu_CargarInicioRequest();
            inValue.idEmpresa = idEmpresa;
            inValue.codigoUsuario = codigoUsuario;
            inValue.idPerfil = idPerfil;
            inValue.objEmpresa = objEmpresa;
            GS.SISGEGS.Web.LoginWCF.Menu_CargarInicioResponse retVal = ((GS.SISGEGS.Web.LoginWCF.ILoginWCF)(this)).Menu_CargarInicio(inValue);
            objEmpresa = retVal.objEmpresa;
            return retVal.Menu_CargarInicioResult;
        }
        
        public System.Threading.Tasks.Task<GS.SISGEGS.Web.LoginWCF.Menu_CargarInicioResponse> Menu_CargarInicioAsync(GS.SISGEGS.Web.LoginWCF.Menu_CargarInicioRequest request) {
            return base.Channel.Menu_CargarInicioAsync(request);
        }
        
        public void AuditoriaMenu_Registrar(string url, string nombreDispositivo, int idUsuario) {
            base.Channel.AuditoriaMenu_Registrar(url, nombreDispositivo, idUsuario);
        }
        
        public System.Threading.Tasks.Task AuditoriaMenu_RegistrarAsync(string url, string nombreDispositivo, int idUsuario) {
            return base.Channel.AuditoriaMenu_RegistrarAsync(url, nombreDispositivo, idUsuario);
        }
    }
}
