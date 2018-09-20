using System;
using System.Collections.Generic;
using System.ServiceModel;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IPlanificacionWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IPlanificacionWCF
    {
        [OperationContract]
        List<GS_GetAllModulosResult> Perfil_Listar(int idEmpresa, int codigoUsuario);

        [OperationContract]
        void Modulos_Actualizar(int idEmpresa, int codigoUsuario, int id_Modulo, string detalle, string id_Agenda,
            int id_Estado, string usuarioModificacion);

        [OperationContract]
        List<GS_GetPlanificacionByPeriodoResult> GetPlanificacionByPeriodo(int idEmpresa, int codigoUsuario,
            string periodo);

        [OperationContract]
        List<GS_GetPlanificacionDetalleByIdPlanResult> GetPlanificacionDetalleByIdPlan(int idEmpresa, int codigoUsuario,
            string idPlanificacion);

        [OperationContract]
        List<GS_GetPlanDetalleToInsertResult> GetPlanDetalleToInsert(int idEmpresa, int codigoUsuario);

        [OperationContract]
        List<GS_GetPlanDetalleToEditResult> GetPlanDetalleToEdit(int idEmpresa, int codigoUsuario,
            string idPlanificacion);

        [OperationContract]
        int PlanificacionCabecera_Insertar(int idEmpresa, int codigoUsuario, string periodo, DateTime fechaInicio,
            DateTime fechaFin, string usuarioCreacion);

        [OperationContract]
        void PlanificacionCabecera_Update(int idEmpresa, int codigoUsuario, int idPlanificacion, DateTime fechaInicio,
            DateTime fechaFin, string usuarioCreacion);

        [OperationContract]
        int PlanificacionDetalle_Insertar(int idEmpresa, int codigoUsuario, int id_Modulo, int idPlanificacion,
            DateTime fechaCierre, string detalle, string observacion, int estado, string usuarioCreacion);

        [OperationContract]
        List<GS_GetAgendaListaResult> GetAgendaLista(int idEmpresa, int codigoUsuario);

        [OperationContract]
        void PlanificacionHistorial_Insertar(int idEmpresa, int codigoUsuario, int id_detalle, int id_Modulo,
            string id_Agenda, string detalle, string observacion, DateTime fechaCierre, string usuarioModificacion);

        [OperationContract]
        void PlanificacionDetalle_Actualizar(int idEmpresa, int codigoUsuario, int id_detalle, string detalle,
            string observacion, DateTime fechaCierre, string usuarioModificacion);

        [OperationContract]
        List<GS_GetHistorialCambiosResult> GetHistorialCambios(int idEmpresa, int codigoUsuario, int id_Detalle);

        [OperationContract]
        List<USP_Sel_MetaPresupuestoResult> Obtener_MetaPresupuesto(int idEmpresa, int codigoUsuario, DateTime fechainicial, DateTime fechafinal, string id_vendedor);

        [OperationContract]
        void Registrar_MetaPresupuesto(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor, int id_zona, DateTime fecha, Boolean aprobado, string pcregistro, List<USP_Sel_MetaPresupuestoDetResult> lstdetalle);

        [OperationContract]
        void Obtener_MetaPresupuestoCabDet(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor,
            ref USP_Sel_MetaPresupuestoCabResult eMetaPreCab, ref List<USP_Sel_MetaPresupuestoDetResult> lstMetaPreDet);

        //[OperationContract]
        //void Eliminar_MetaPresupuestoLinea(int idEmpresa, int codigoUsuario, int id);

        [OperationContract]
        void Eliminar_MetaPresupuesto(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor);

        [OperationContract]
        void Aprobar_MetaPresupuesto(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor, bool aprobado, string pcModifica);

        [OperationContract]
        USP_Sel_TipoClienteResult Obtener_TipoCliente(int idEmpresa, int codigoUsuario, string id_cliente);


        [OperationContract]
        void Registrar_MetaPresupuestoPromotor(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor, string id_cliente, string id_promotor, decimal total, bool aprobado);

        [OperationContract]
        List<USP_Sel_MetaPresupuestoPromotorResult> Obtener_MetaPresupuestoPromotor(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor);
        
        [OperationContract]
        List<USP_SEL_MetaPresupuestoPendienteResult> Obtener_PresupuestoPendiente(int idEmpresa, int codigoUsuario,int anno, int mes, string id_vendedor);
        
        [OperationContract]
        List<USP_Sel_PromotoresxVendedorResult> Obtener_PromotoresxVendedor(int idEmpresa, int codigoUsuario, string id_vendedor);

        [OperationContract]
        void Eliminar_MetaPresupuestoPromotor(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor, Int32 id);

        [OperationContract]
        List<USP_SEL_VENTA_POR_VENDEDOR2> Obtener_PresupuestoResumen(int idEmpresa, int codigoUsuario, DateTime fechaInicial, DateTime fechaFinal);
    }
}
