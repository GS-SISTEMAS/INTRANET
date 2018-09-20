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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "PlanificacionWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione PlanificacionWCF.svc o PlanificacionWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class PlanificacionWCF : IPlanificacionWCF
    {
        public List<GS_GetAllModulosResult> Perfil_Listar(int idEmpresa, int codigoUsuario)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                return objPlanificacion.GetAllModulos(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modulos_Actualizar(int idEmpresa, int codigoUsuario, int id_Modulo, string detalle, string id_Agenda, int id_Estado, string usuarioModificacion)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                objPlanificacion.Modulos_Actualizar(idEmpresa, codigoUsuario, id_Modulo, detalle, id_Agenda, id_Estado, usuarioModificacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GS_GetPlanificacionByPeriodoResult> GetPlanificacionByPeriodo(int idEmpresa, int codigoUsuario, string periodo)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                return objPlanificacion.GetPlanificacionByPeriodo(idEmpresa, codigoUsuario, String.Concat((object) periodo));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GS_GetPlanificacionDetalleByIdPlanResult> GetPlanificacionDetalleByIdPlan(int idEmpresa, int codigoUsuario, string idPlanificacion)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                return objPlanificacion.GetPlanificacionDetalleByIdPlan(idEmpresa, codigoUsuario, idPlanificacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GS_GetPlanDetalleToInsertResult> GetPlanDetalleToInsert(int idEmpresa, int codigoUsuario)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                return objPlanificacion.GetPlanDetalleToInsert(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GS_GetPlanDetalleToEditResult> GetPlanDetalleToEdit(int idEmpresa, int codigoUsuario, string idPlanificacion)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                return objPlanificacion.GetPlanDetalleToEdit(idEmpresa, codigoUsuario, idPlanificacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int PlanificacionCabecera_Insertar(int idEmpresa, int codigoUsuario, string periodo, DateTime fechaInicio, DateTime fechaFin, string usuarioCreacion)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                return objPlanificacion.PlanificacionCabecera_Insertar(idEmpresa, codigoUsuario, periodo, fechaInicio, fechaFin, usuarioCreacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PlanificacionCabecera_Update(int idEmpresa, int codigoUsuario, int idPlanificacion, DateTime fechaInicio, DateTime fechaFin, string usuarioCreacion)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                objPlanificacion.PlanificacionCabecera_Update(idEmpresa, codigoUsuario, idPlanificacion, fechaInicio, fechaFin, usuarioCreacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int PlanificacionDetalle_Insertar(int idEmpresa, int codigoUsuario, int id_Modulo, int idPlanificacion, DateTime fechaCierre, string detalle, string observacion, int estado, string usuarioCreacion)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                return objPlanificacion.PlanificacionDetalle_Insertar(idEmpresa, codigoUsuario, id_Modulo, idPlanificacion, fechaCierre, detalle, observacion, estado, usuarioCreacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GS_GetAgendaListaResult> GetAgendaLista(int idEmpresa, int codigoUsuario)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                return objPlanificacion.GetAgendaLista(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PlanificacionHistorial_Insertar(int idEmpresa, int codigoUsuario, int id_detalle, int id_Modulo,
            string id_Agenda, string detalle, string observacion, DateTime fechaCierre, string usuarioModificacion)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                objPlanificacion.PlanificacionHistorial_Insertar(idEmpresa, codigoUsuario, id_detalle, id_Modulo, id_Agenda, detalle, observacion, fechaCierre, usuarioModificacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PlanificacionDetalle_Actualizar(int idEmpresa, int codigoUsuario, int id_detalle, string detalle,
            string observacion, DateTime fechaCierre, string usuarioModificacion)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                objPlanificacion.PlanificacionDetalle_Actualizar(idEmpresa, codigoUsuario, id_detalle, detalle, observacion, fechaCierre, usuarioModificacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GS_GetHistorialCambiosResult> GetHistorialCambios(int idEmpresa, int codigoUsuario, int id_Detalle)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                return objPlanificacion.GetHistorialCambios(idEmpresa, codigoUsuario, id_Detalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<USP_Sel_MetaPresupuestoResult> Obtener_MetaPresupuesto(int idEmpresa, int codigoUsuario, DateTime fechainicial, DateTime fechafinal, string id_vendedor)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                return objPlanificacion.Obtener_MetaPresupuesto(idEmpresa, codigoUsuario, fechainicial, fechafinal, id_vendedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Registrar_MetaPresupuesto(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor, int id_zona, DateTime fecha, Boolean aprobado, string pcregistro, List<USP_Sel_MetaPresupuestoDetResult> lstdetalle)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                objPlanificacion.Registrar_MetaPresupuesto(idEmpresa, codigoUsuario, anno, mes, id_vendedor, id_zona, fecha, aprobado, pcregistro, lstdetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Obtener_MetaPresupuestoCabDet(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor,
            ref USP_Sel_MetaPresupuestoCabResult eMetaPreCab, ref List<USP_Sel_MetaPresupuestoDetResult> lstMetaPreDet)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                objPlanificacion.Obtener_MetaPresupuestoCabDet(idEmpresa, codigoUsuario, anno, mes, id_vendedor,
                    ref eMetaPreCab, ref lstMetaPreDet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void Eliminar_MetaPresupuestoLinea(int idEmpresa, int codigoUsuario, int id)
        //{
        //    PlanificacionBL objPlanificacion = new PlanificacionBL();
        //    try
        //    {
        //        objPlanificacion.Eliminar_MetaPresupuestoLinea(idEmpresa, codigoUsuario, id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void Eliminar_MetaPresupuesto(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                objPlanificacion.Eliminar_MetaPresupuesto(idEmpresa, codigoUsuario, anno,mes,id_vendedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Aprobar_MetaPresupuesto(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor, bool aprobado, string pcModifica)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                objPlanificacion.Aprobar_MetaPresupuesto(idEmpresa, codigoUsuario, anno, mes, id_vendedor,aprobado,pcModifica);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public USP_Sel_TipoClienteResult Obtener_TipoCliente(int idEmpresa, int codigoUsuario, string id_cliente)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                return objPlanificacion.Obtener_TipoCliente(idEmpresa, codigoUsuario, id_cliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Registrar_MetaPresupuestoPromotor(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor, string id_cliente, string id_promotor, decimal total, bool aprobado)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                objPlanificacion.Registrar_MetaPresupuestoPromotor(idEmpresa, codigoUsuario, anno, mes, id_vendedor, id_cliente, id_promotor, total, aprobado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USP_Sel_MetaPresupuestoPromotorResult> Obtener_MetaPresupuestoPromotor(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                return objPlanificacion.Obtener_MetaPresupuestoPromotor(idEmpresa, codigoUsuario, anno, mes, id_vendedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USP_SEL_MetaPresupuestoPendienteResult> Obtener_PresupuestoPendiente(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                return objPlanificacion.Obtener_PresupuestoPendiente(idEmpresa, codigoUsuario, anno, mes, id_vendedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USP_Sel_PromotoresxVendedorResult> Obtener_PromotoresxVendedor(int idEmpresa, int codigoUsuario, string id_vendedor)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                return objPlanificacion.Obtener_PromotoresxVendedor(idEmpresa, codigoUsuario, id_vendedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar_MetaPresupuestoPromotor(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor, Int32 id)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                objPlanificacion.Eliminar_MetaPresupuestoPromotor(idEmpresa, codigoUsuario, anno, mes, id_vendedor, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USP_SEL_VENTA_POR_VENDEDOR2> Obtener_PresupuestoResumen(int idEmpresa, int codigoUsuario, DateTime fechaInicial, DateTime fechaFinal)
        {
            PlanificacionBL objPlanificacion = new PlanificacionBL();
            try
            {
                return objPlanificacion.Obtener_PresupuestoResumen(idEmpresa, codigoUsuario, fechaInicial, fechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
