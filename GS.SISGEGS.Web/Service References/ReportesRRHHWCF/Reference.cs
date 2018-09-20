﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GS.SISGEGS.Web.ReportesRRHHWCF {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ReportesRRHHWCF.IReportesRRHH")]
    public interface IReportesRRHH {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportesRRHH/Ingreso_Personal", ReplyAction="http://tempuri.org/IReportesRRHH/Ingreso_PersonalResponse")]
        GS.SISGEGS.DM.Ingreso_PersonalResult[] Ingreso_Personal(System.DateTime fecha);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportesRRHH/Ingreso_Personal", ReplyAction="http://tempuri.org/IReportesRRHH/Ingreso_PersonalResponse")]
        System.Threading.Tasks.Task<GS.SISGEGS.DM.Ingreso_PersonalResult[]> Ingreso_PersonalAsync(System.DateTime fecha);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportesRRHH/Ingreso_PersonalDetalle", ReplyAction="http://tempuri.org/IReportesRRHH/Ingreso_PersonalDetalleResponse")]
        GS.SISGEGS.Web.ReportesRRHHWCF.Ingreso_PersonalDetalleResponse Ingreso_PersonalDetalle(GS.SISGEGS.Web.ReportesRRHHWCF.Ingreso_PersonalDetalleRequest request);
        
        // CODEGEN: Generando contrato de mensaje, ya que la operación tiene múltiples valores de devolución.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportesRRHH/Ingreso_PersonalDetalle", ReplyAction="http://tempuri.org/IReportesRRHH/Ingreso_PersonalDetalleResponse")]
        System.Threading.Tasks.Task<GS.SISGEGS.Web.ReportesRRHHWCF.Ingreso_PersonalDetalleResponse> Ingreso_PersonalDetalleAsync(GS.SISGEGS.Web.ReportesRRHHWCF.Ingreso_PersonalDetalleRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportesRRHH/Personal_Listar", ReplyAction="http://tempuri.org/IReportesRRHH/Personal_ListarResponse")]
        GS.SISGEGS.DM.Personal_ListarResult[] Personal_Listar(string codEmpresa, string texto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportesRRHH/Personal_Listar", ReplyAction="http://tempuri.org/IReportesRRHH/Personal_ListarResponse")]
        System.Threading.Tasks.Task<GS.SISGEGS.DM.Personal_ListarResult[]> Personal_ListarAsync(string codEmpresa, string texto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportesRRHH/Personal_Registrar", ReplyAction="http://tempuri.org/IReportesRRHH/Personal_RegistrarResponse")]
        void Personal_Registrar(int idPersonal, string nroDocumento, string imageURL, int idUsuarioRegistro);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportesRRHH/Personal_Registrar", ReplyAction="http://tempuri.org/IReportesRRHH/Personal_RegistrarResponse")]
        System.Threading.Tasks.Task Personal_RegistrarAsync(int idPersonal, string nroDocumento, string imageURL, int idUsuarioRegistro);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Ingreso_PersonalDetalle", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Ingreso_PersonalDetalleRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.DateTime fecha;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string ccosto;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public GS.SISGEGS.DM.Ingreso_Personal_PermisosResult[] lstpermiso;
        
        public Ingreso_PersonalDetalleRequest() {
        }
        
        public Ingreso_PersonalDetalleRequest(System.DateTime fecha, string ccosto, GS.SISGEGS.DM.Ingreso_Personal_PermisosResult[] lstpermiso) {
            this.fecha = fecha;
            this.ccosto = ccosto;
            this.lstpermiso = lstpermiso;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Ingreso_PersonalDetalleResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Ingreso_PersonalDetalleResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public GS.SISGEGS.DM.Ingreso_Personal_DetalleResult[] Ingreso_PersonalDetalleResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public GS.SISGEGS.DM.Ingreso_Personal_PermisosResult[] lstpermiso;
        
        public Ingreso_PersonalDetalleResponse() {
        }
        
        public Ingreso_PersonalDetalleResponse(GS.SISGEGS.DM.Ingreso_Personal_DetalleResult[] Ingreso_PersonalDetalleResult, GS.SISGEGS.DM.Ingreso_Personal_PermisosResult[] lstpermiso) {
            this.Ingreso_PersonalDetalleResult = Ingreso_PersonalDetalleResult;
            this.lstpermiso = lstpermiso;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IReportesRRHHChannel : GS.SISGEGS.Web.ReportesRRHHWCF.IReportesRRHH, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ReportesRRHHClient : System.ServiceModel.ClientBase<GS.SISGEGS.Web.ReportesRRHHWCF.IReportesRRHH>, GS.SISGEGS.Web.ReportesRRHHWCF.IReportesRRHH {
        
        public ReportesRRHHClient() {
        }
        
        public ReportesRRHHClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ReportesRRHHClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ReportesRRHHClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ReportesRRHHClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public GS.SISGEGS.DM.Ingreso_PersonalResult[] Ingreso_Personal(System.DateTime fecha) {
            return base.Channel.Ingreso_Personal(fecha);
        }
        
        public System.Threading.Tasks.Task<GS.SISGEGS.DM.Ingreso_PersonalResult[]> Ingreso_PersonalAsync(System.DateTime fecha) {
            return base.Channel.Ingreso_PersonalAsync(fecha);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        GS.SISGEGS.Web.ReportesRRHHWCF.Ingreso_PersonalDetalleResponse GS.SISGEGS.Web.ReportesRRHHWCF.IReportesRRHH.Ingreso_PersonalDetalle(GS.SISGEGS.Web.ReportesRRHHWCF.Ingreso_PersonalDetalleRequest request) {
            return base.Channel.Ingreso_PersonalDetalle(request);
        }
        
        public GS.SISGEGS.DM.Ingreso_Personal_DetalleResult[] Ingreso_PersonalDetalle(System.DateTime fecha, string ccosto, ref GS.SISGEGS.DM.Ingreso_Personal_PermisosResult[] lstpermiso) {
            GS.SISGEGS.Web.ReportesRRHHWCF.Ingreso_PersonalDetalleRequest inValue = new GS.SISGEGS.Web.ReportesRRHHWCF.Ingreso_PersonalDetalleRequest();
            inValue.fecha = fecha;
            inValue.ccosto = ccosto;
            inValue.lstpermiso = lstpermiso;
            GS.SISGEGS.Web.ReportesRRHHWCF.Ingreso_PersonalDetalleResponse retVal = ((GS.SISGEGS.Web.ReportesRRHHWCF.IReportesRRHH)(this)).Ingreso_PersonalDetalle(inValue);
            lstpermiso = retVal.lstpermiso;
            return retVal.Ingreso_PersonalDetalleResult;
        }
        
        public System.Threading.Tasks.Task<GS.SISGEGS.Web.ReportesRRHHWCF.Ingreso_PersonalDetalleResponse> Ingreso_PersonalDetalleAsync(GS.SISGEGS.Web.ReportesRRHHWCF.Ingreso_PersonalDetalleRequest request) {
            return base.Channel.Ingreso_PersonalDetalleAsync(request);
        }
        
        public GS.SISGEGS.DM.Personal_ListarResult[] Personal_Listar(string codEmpresa, string texto) {
            return base.Channel.Personal_Listar(codEmpresa, texto);
        }
        
        public System.Threading.Tasks.Task<GS.SISGEGS.DM.Personal_ListarResult[]> Personal_ListarAsync(string codEmpresa, string texto) {
            return base.Channel.Personal_ListarAsync(codEmpresa, texto);
        }
        
        public void Personal_Registrar(int idPersonal, string nroDocumento, string imageURL, int idUsuarioRegistro) {
            base.Channel.Personal_Registrar(idPersonal, nroDocumento, imageURL, idUsuarioRegistro);
        }
        
        public System.Threading.Tasks.Task Personal_RegistrarAsync(int idPersonal, string nroDocumento, string imageURL, int idUsuarioRegistro) {
            return base.Channel.Personal_RegistrarAsync(idPersonal, nroDocumento, imageURL, idUsuarioRegistro);
        }
    }
}