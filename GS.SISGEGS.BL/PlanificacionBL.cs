using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.BL
{
    public interface IPlanificacionBL
    {
        List<GS_GetAllModulosResult> GetAllModulos(int idEmpresa, int codigoUsuario);

        void Modulos_Actualizar(int idEmpresa, int codigoUsuario, int id_Modulo, string detalle, string id_Agenda,
            int id_Estado, string usuarioModificacion);

        List<GS_GetPlanificacionByPeriodoResult> GetPlanificacionByPeriodo(int idEmpresa, int codigoUsuario,
            string periodo);

        List<GS_GetPlanificacionDetalleByIdPlanResult> GetPlanificacionDetalleByIdPlan(int idEmpresa, int codigoUsuario,
            string idPlanificacion);

        List<GS_GetPlanDetalleToInsertResult> GetPlanDetalleToInsert(int idEmpresa, int codigoUsuario);

        List<GS_GetPlanDetalleToEditResult> GetPlanDetalleToEdit(int idEmpresa, int codigoUsuario,
            string idPlanificacion);

        int PlanificacionCabecera_Insertar(int idEmpresa, int codigoUsuario, string periodo, DateTime fechaInicio,
            DateTime fechaFin, string usuarioCreacion);

        void PlanificacionCabecera_Update(int idEmpresa, int codigoUsuario, int idPlanificacion, DateTime fechaInicio,
            DateTime fechaFin, string usuarioCreacion);

        int PlanificacionDetalle_Insertar(int idEmpresa, int codigoUsuario, int id_Modulo, int idPlanificacion,
            DateTime fechaCierre, string detalle, string observacion, int estado, string usuarioCreacion);

        List<GS_GetAgendaListaResult> GetAgendaLista(int idEmpresa, int codigoUsuario);

        void PlanificacionHistorial_Insertar(int idEmpresa, int codigoUsuario, int id_detalle, int id_Modulo,
            string id_Agenda, string detalle, string observacion, DateTime fechaCierre, string usuarioModificacion);

        void PlanificacionDetalle_Actualizar(int idEmpresa, int codigoUsuario, int id_detalle, string detalle,
            string observacion, DateTime fechaCierre, string usuarioModificacion);

        List<GS_GetHistorialCambiosResult> GetHistorialCambios(int idEmpresa, int codigoUsuario, int id_Detalle);

        List<USP_Sel_MetaPresupuestoResult> Obtener_MetaPresupuesto(int idEmpresa, int codigoUsuario, DateTime fechainicial, DateTime fechafinal, string id_vendedor);
        void Registrar_MetaPresupuesto(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor, int id_zona, DateTime fecha, Boolean aprobado, string pcregistro, List<USP_Sel_MetaPresupuestoDetResult> lstdetalle);
        void Obtener_MetaPresupuestoCabDet(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor,
                    ref USP_Sel_MetaPresupuestoCabResult eMetaPreCab, ref List<USP_Sel_MetaPresupuestoDetResult> lstMetaPreDet);

        //void Eliminar_MetaPresupuestoLinea(int idEmpresa, int codigoUsuario, int id);
        void Eliminar_MetaPresupuesto(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor);
        void Aprobar_MetaPresupuesto(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor, bool aprobado, string pcModifica);

        USP_Sel_TipoClienteResult Obtener_TipoCliente(int idEmpresa, int codigoUsuario, string id_cliente);


        void Registrar_MetaPresupuestoPromotor(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor, string id_cliente, string id_promotor, decimal total, bool aprobado);
        List<USP_Sel_MetaPresupuestoPromotorResult> Obtener_MetaPresupuestoPromotor(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor);
        List<USP_SEL_MetaPresupuestoPendienteResult> Obtener_PresupuestoPendiente(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor);
        List<USP_Sel_PromotoresxVendedorResult> Obtener_PromotoresxVendedor(int idEmpresa, int codigoUsuario, string id_vendedor);

        void Eliminar_MetaPresupuestoPromotor(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor, Int32 id);

        List<USP_SEL_VENTA_POR_VENDEDOR2> Obtener_PresupuestoResumen(int idEmpresa, int codigoUsuario, DateTime fechaInicial, DateTime fechaFinal);
    }

    public class PlanificacionBL : IPlanificacionBL
    {
        public List<GS_GetAllModulosResult> GetAllModulos(int idEmpresa, int codigoUsuario)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    return dcg.GS_GetAllModulos().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar los costos de los modulos.");
                }
            }
        }

        public void Modulos_Actualizar(int idEmpresa, int codigoUsuario, int id_Modulo, string detalle, string id_Agenda, int id_Estado, string usuarioModificacion)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    dcg.GS_UpdateModulo(id_Modulo, detalle, id_Agenda, id_Estado, usuarioModificacion);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }

        public List<GS_GetPlanificacionByPeriodoResult> GetPlanificacionByPeriodo(int idEmpresa, int codigoUsuario, string periodo)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.GS_GetPlanificacionByPeriodo(periodo).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar los cierres por periodo. GS_GetPlanificacionByPeriodo");
                }
            }
        }

        public List<GS_GetPlanificacionDetalleByIdPlanResult> GetPlanificacionDetalleByIdPlan(int idEmpresa, int codigoUsuario, string idPlanificacion)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.GS_GetPlanificacionDetalleByIdPlan(idPlanificacion).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar los cierres del Periodo.");
                }
            }
        }

        public List<GS_GetPlanDetalleToInsertResult> GetPlanDetalleToInsert(int idEmpresa, int codigoUsuario)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.GS_GetPlanDetalleToInsert().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar los modulos.");
                }
            }
        }

        public List<GS_GetPlanDetalleToEditResult> GetPlanDetalleToEdit(int idEmpresa, int codigoUsuario, string idPlanificacion)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.GS_GetPlanDetalleToEdit(idPlanificacion).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar los modulos.");
                }
            }
        }


        public int PlanificacionCabecera_Insertar(int idEmpresa, int codigoUsuario, string periodo, DateTime fechaInicio, DateTime fechaFin, string usuarioCreacion)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    int? id = 0;

                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.GS_PlanificacionCabecera_Insert(periodo,fechaInicio,fechaFin,usuarioCreacion,ref id);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }

        public void PlanificacionCabecera_Update(int idEmpresa, int codigoUsuario, int idPlanificacion, DateTime fechaInicio, DateTime fechaFin, string usuarioCreacion)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    int? id = 0;
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.GS_PlanificacionCabecera_Update(idPlanificacion, fechaInicio, fechaFin, fechaFin, usuarioCreacion);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }

        public int PlanificacionDetalle_Insertar(int idEmpresa, int codigoUsuario, int id_Modulo, int idPlanificacion, DateTime fechaCierre, string detalle, string observacion,int estado, string usuarioCreacion)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    int? id = 0;
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.GS_PlanificacionDetalle_Insert(id_Modulo,idPlanificacion,fechaCierre,detalle,observacion,estado,usuarioCreacion);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }

        public List<GS_GetAgendaListaResult> GetAgendaLista(int idEmpresa, int codigoUsuario)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.GS_GetAgendaLista().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar los modulos.");
                }
            }
        }

        public void PlanificacionHistorial_Insertar(int idEmpresa, int codigoUsuario,  int id_detalle, int id_Modulo,string id_Agenda, string detalle, string observacion, DateTime fechaCierre, string usuarioModificacion)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.GS_PlanificacionHistorial_Insert(id_detalle, id_Modulo, id_Agenda, detalle, observacion,fechaCierre, usuarioModificacion);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }

        public void PlanificacionDetalle_Actualizar(int idEmpresa, int codigoUsuario, int id_detalle, string detalle, string observacion, DateTime fechaCierre, string usuarioModificacion)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.GS_PlanificacionDetalle_Update(id_detalle, detalle, observacion, fechaCierre, usuarioModificacion);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }

        public List<GS_GetHistorialCambiosResult> GetHistorialCambios(int idEmpresa, int codigoUsuario, int id_Detalle)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.GS_GetHistorialCambios(id_Detalle).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar los cierres por periodo.");
                }
            }
        }

        public List<USP_Sel_MetaPresupuestoResult> Obtener_MetaPresupuesto(int idEmpresa, int codigoUsuario, DateTime fechainicial,DateTime fechafinal,string id_vendedor)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.USP_Sel_MetaPresupuesto(fechainicial,fechafinal,id_vendedor).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar los presupuestos");
                }
            }
        }

        public void Aprobar_MetaPresupuesto(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor,bool aprobado,string pcModifica)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    
                    dcg.USP_Ins_MetaPresupuesto(anno, mes, id_vendedor, 0, DateTime.Now, aprobado, codigoUsuario,pcModifica);
                    
                    dcg.SubmitChanges();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }
        public void Registrar_MetaPresupuesto(int idEmpresa, int codigoUsuario,int anno,int mes,string id_vendedor,int id_zona,DateTime fecha,Boolean aprobado,
            string pcregistro,List<USP_Sel_MetaPresupuestoDetResult> lstdetalle)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    dcg.USP_Del_MetaPresupuestoLinea(anno, mes, id_vendedor);
                    dcg.USP_Ins_MetaPresupuesto(anno, mes, id_vendedor, id_zona, fecha, aprobado, codigoUsuario, pcregistro);
                    foreach(USP_Sel_MetaPresupuestoDetResult e in lstdetalle)
                    {
                        dcg.USP_Ins_MetaPresupuestoLinea(e.Anno, e.Mes, e.Id_Vendedor, e.Id_Cliente, e.CodigoProducto, e.Kardex, e.Cantidad, e.Precio,e.Id_G5);
                    }

                    dcg.SubmitChanges();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }

        public void Obtener_MetaPresupuestoCabDet(int idEmpresa, int codigoUsuario,int anno,int mes,string id_vendedor,
            ref USP_Sel_MetaPresupuestoCabResult eMetaPreCab,ref List<USP_Sel_MetaPresupuestoDetResult> lstMetaPreDet)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    
                    eMetaPreCab = dcg.USP_Sel_MetaPresupuestoCab(anno, mes, id_vendedor).ToList().First();
                    lstMetaPreDet = dcg.USP_Sel_MetaPresupuestoDet(anno, mes, id_vendedor).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar los presupuestos");
                }
            }
        }

        //public void Eliminar_MetaPresupuestoLinea(int idEmpresa, int codigoUsuario, int id)
        //{
        //    using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
        //    {
        //        dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

        //        try
        //        {
        //            dcg.USP_Del_MetaPresupuestoLinea(id);
        //            dcg.SubmitChanges();
        //        }
        //        catch (Exception ex)
        //        {
        //            dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
        //            dci.SubmitChanges();
        //            throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo registrar la Linea del Presupuesto");
        //        }
        //    }
        //}

        public void Eliminar_MetaPresupuesto(int idEmpresa, int codigoUsuario, int anno,int mes,string id_vendedor)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    dcg.USP_Del_MetaPresupuesto(anno, mes, id_vendedor);
                    dcg.SubmitChanges();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo registrar la Linea del Presupuesto");
                }
            }
        }

        public USP_Sel_TipoClienteResult Obtener_TipoCliente(int idEmpresa, int codigoUsuario,string id_cliente)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.USP_Sel_TipoCliente(id_cliente).ToList().First();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("El cliente seleccionado no tiene definido si es DISTRIBUIDOR O FUNDO");
                }
            }
        }

        public List<USP_Sel_PromotoresxVendedorResult> Obtener_PromotoresxVendedor(int idEmpresa, int codigoUsuario, string id_vendedor)
        {

            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {

                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {

                    return dcg.USP_Sel_PromotoresxVendedor(id_vendedor).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar los promotores");
                }
            }
        }

        public List<USP_SEL_MetaPresupuestoPendienteResult> Obtener_PresupuestoPendiente(int idEmpresa, int codigoUsuario,int anno,int mes, string id_vendedor)
        {

            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {

                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {

                    return dcg.USP_SEL_MetaPresupuestoPendiente(anno, mes, id_vendedor).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar el presupuesto por promotor");
                }
            }
        }

        public List<USP_Sel_MetaPresupuestoPromotorResult> Obtener_MetaPresupuestoPromotor(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor)
        {

            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {

                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {

                    return dcg.USP_Sel_MetaPresupuestoPromotor(anno, mes, id_vendedor).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar el presupuesto por promotor");
                }
            }
        }


        public void Registrar_MetaPresupuestoPromotor(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor,string id_cliente,string id_promotor,decimal total,bool aprobado)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    //dcg.USP_Del_MetaPresupuestoProm(anno, mes, id_vendedor);
                    dcg.USP_INS_MetaPresupuestoProm(anno, mes, id_vendedor, id_cliente, id_promotor, total, aprobado, codigoUsuario);
                    dcg.SubmitChanges();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo registrar el presupuesto");
                }
            }
        }

        public void Eliminar_MetaPresupuestoPromotor(int idEmpresa, int codigoUsuario, int anno, int mes, string id_vendedor,Int32 id)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    dcg.USP_Del_MetaPresupuestoProm(anno, mes, id_vendedor,id);
                    
                    dcg.SubmitChanges();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    
                }
            }
        }

       

        public List<USP_SEL_VENTA_POR_VENDEDOR2> Obtener_PresupuestoResumen(int idEmpresa, int codigoUsuario, DateTime fechaInicial, DateTime fechaFinal)
        {

            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {

                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {

                    return dcg.USP_SEL_VENTA_POR_VENDEDOR(fechaInicial, fechaFinal).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar el presupuesto por promotor");
                }
            }
        }












    }
}
