﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GS.SISGEGS.Web.CierreCostoWCF {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CierreCostoWCF.ICierreCostoWCF")]
    public interface ICierreCostoWCF {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICierreCostoWCF/CierreCosto_Registrar", ReplyAction="http://tempuri.org/ICierreCostoWCF/CierreCosto_RegistrarResponse")]
        void CierreCosto_Registrar(int idEmpresa, int codigoUsuario, int mes, int anho);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICierreCostoWCF/CierreCosto_Registrar", ReplyAction="http://tempuri.org/ICierreCostoWCF/CierreCosto_RegistrarResponse")]
        System.Threading.Tasks.Task CierreCosto_RegistrarAsync(int idEmpresa, int codigoUsuario, int mes, int anho);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICierreCostoWCF/CierreCosto_Listar", ReplyAction="http://tempuri.org/ICierreCostoWCF/CierreCosto_ListarResponse")]
        GS.SISGEGS.DM.gsCierreCosto_ListarResult[] CierreCosto_Listar(int idEmpresa, int codigoUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICierreCostoWCF/CierreCosto_Listar", ReplyAction="http://tempuri.org/ICierreCostoWCF/CierreCosto_ListarResponse")]
        System.Threading.Tasks.Task<GS.SISGEGS.DM.gsCierreCosto_ListarResult[]> CierreCosto_ListarAsync(int idEmpresa, int codigoUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICierreCostoWCF/DocVenta_ControlCosto_MarcaProducto", ReplyAction="http://tempuri.org/ICierreCostoWCF/DocVenta_ControlCosto_MarcaProductoResponse")]
        GS.SISGEGS.DM.gsDocVenta_ControlCosto_MarcaProductoResult[] DocVenta_ControlCosto_MarcaProducto(int idEmpresa, int codigoUsuario, System.DateTime fechaInicio, System.DateTime fechaFinal, System.Nullable<int> kardex);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICierreCostoWCF/DocVenta_ControlCosto_MarcaProducto", ReplyAction="http://tempuri.org/ICierreCostoWCF/DocVenta_ControlCosto_MarcaProductoResponse")]
        System.Threading.Tasks.Task<GS.SISGEGS.DM.gsDocVenta_ControlCosto_MarcaProductoResult[]> DocVenta_ControlCosto_MarcaProductoAsync(int idEmpresa, int codigoUsuario, System.DateTime fechaInicio, System.DateTime fechaFinal, System.Nullable<int> kardex);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICierreCostoWCF/CierreCosto_ComboBox", ReplyAction="http://tempuri.org/ICierreCostoWCF/CierreCosto_ComboBoxResponse")]
        GS.SISGEGS.DM.gsCierreCosto_ComboBoxResult[] CierreCosto_ComboBox(int idEmpresa, int codigoUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICierreCostoWCF/CierreCosto_ComboBox", ReplyAction="http://tempuri.org/ICierreCostoWCF/CierreCosto_ComboBoxResponse")]
        System.Threading.Tasks.Task<GS.SISGEGS.DM.gsCierreCosto_ComboBoxResult[]> CierreCosto_ComboBoxAsync(int idEmpresa, int codigoUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICierreCostoWCF/Produccion_Listar_PlanProd", ReplyAction="http://tempuri.org/ICierreCostoWCF/Produccion_Listar_PlanProdResponse")]
        GS.SISGEGS.DM.gsProduccion_Listar_PlanProdResult[] Produccion_Listar_PlanProd(int idEmpresa, int codigoUsuario, System.DateTime fechaInicio, System.DateTime fechaFinal, System.Nullable<int> kardex);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICierreCostoWCF/Produccion_Listar_PlanProd", ReplyAction="http://tempuri.org/ICierreCostoWCF/Produccion_Listar_PlanProdResponse")]
        System.Threading.Tasks.Task<GS.SISGEGS.DM.gsProduccion_Listar_PlanProdResult[]> Produccion_Listar_PlanProdAsync(int idEmpresa, int codigoUsuario, System.DateTime fechaInicio, System.DateTime fechaFinal, System.Nullable<int> kardex);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICierreCostoWCFChannel : GS.SISGEGS.Web.CierreCostoWCF.ICierreCostoWCF, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CierreCostoWCFClient : System.ServiceModel.ClientBase<GS.SISGEGS.Web.CierreCostoWCF.ICierreCostoWCF>, GS.SISGEGS.Web.CierreCostoWCF.ICierreCostoWCF {
        
        public CierreCostoWCFClient() {
        }
        
        public CierreCostoWCFClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CierreCostoWCFClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CierreCostoWCFClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CierreCostoWCFClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void CierreCosto_Registrar(int idEmpresa, int codigoUsuario, int mes, int anho) {
            base.Channel.CierreCosto_Registrar(idEmpresa, codigoUsuario, mes, anho);
        }
        
        public System.Threading.Tasks.Task CierreCosto_RegistrarAsync(int idEmpresa, int codigoUsuario, int mes, int anho) {
            return base.Channel.CierreCosto_RegistrarAsync(idEmpresa, codigoUsuario, mes, anho);
        }
        
        public GS.SISGEGS.DM.gsCierreCosto_ListarResult[] CierreCosto_Listar(int idEmpresa, int codigoUsuario) {
            return base.Channel.CierreCosto_Listar(idEmpresa, codigoUsuario);
        }
        
        public System.Threading.Tasks.Task<GS.SISGEGS.DM.gsCierreCosto_ListarResult[]> CierreCosto_ListarAsync(int idEmpresa, int codigoUsuario) {
            return base.Channel.CierreCosto_ListarAsync(idEmpresa, codigoUsuario);
        }
        
        public GS.SISGEGS.DM.gsDocVenta_ControlCosto_MarcaProductoResult[] DocVenta_ControlCosto_MarcaProducto(int idEmpresa, int codigoUsuario, System.DateTime fechaInicio, System.DateTime fechaFinal, System.Nullable<int> kardex) {
            return base.Channel.DocVenta_ControlCosto_MarcaProducto(idEmpresa, codigoUsuario, fechaInicio, fechaFinal, kardex);
        }
        
        public System.Threading.Tasks.Task<GS.SISGEGS.DM.gsDocVenta_ControlCosto_MarcaProductoResult[]> DocVenta_ControlCosto_MarcaProductoAsync(int idEmpresa, int codigoUsuario, System.DateTime fechaInicio, System.DateTime fechaFinal, System.Nullable<int> kardex) {
            return base.Channel.DocVenta_ControlCosto_MarcaProductoAsync(idEmpresa, codigoUsuario, fechaInicio, fechaFinal, kardex);
        }
        
        public GS.SISGEGS.DM.gsCierreCosto_ComboBoxResult[] CierreCosto_ComboBox(int idEmpresa, int codigoUsuario) {
            return base.Channel.CierreCosto_ComboBox(idEmpresa, codigoUsuario);
        }
        
        public System.Threading.Tasks.Task<GS.SISGEGS.DM.gsCierreCosto_ComboBoxResult[]> CierreCosto_ComboBoxAsync(int idEmpresa, int codigoUsuario) {
            return base.Channel.CierreCosto_ComboBoxAsync(idEmpresa, codigoUsuario);
        }
        
        public GS.SISGEGS.DM.gsProduccion_Listar_PlanProdResult[] Produccion_Listar_PlanProd(int idEmpresa, int codigoUsuario, System.DateTime fechaInicio, System.DateTime fechaFinal, System.Nullable<int> kardex) {
            return base.Channel.Produccion_Listar_PlanProd(idEmpresa, codigoUsuario, fechaInicio, fechaFinal, kardex);
        }
        
        public System.Threading.Tasks.Task<GS.SISGEGS.DM.gsProduccion_Listar_PlanProdResult[]> Produccion_Listar_PlanProdAsync(int idEmpresa, int codigoUsuario, System.DateTime fechaInicio, System.DateTime fechaFinal, System.Nullable<int> kardex) {
            return base.Channel.Produccion_Listar_PlanProdAsync(idEmpresa, codigoUsuario, fechaInicio, fechaFinal, kardex);
        }
    }
}
