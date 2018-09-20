using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GS.SISGEGS.DM;
using System.Configuration;
using System.Transactions;

namespace GS.SISGEGS.BL
{
    public interface IEgresoBL {
        List<gsEgresosVarios_ListarCajaChicaResult> EgresosVarios_ListarCajaChica(int idEmpresa, int codigoUsuario, string idAgenda, DateTime fechaInicio, DateTime fechaFinal, bool revisarGastos, string id_proveedor, string serie, decimal  numero);
        gsEgresosVarios_BuscarCabeceraResult EgresosVarios_Buscar(int idEmpresa, int codigoUsuario, int idOperacion, ref bool? bloqueado, ref string mensajeBloqueado, ref List<gsEgresosVarios_BuscarDetalleResult> lstEgresosVarios);
        void EgresosVarios_Registrar(int idEmpresa, int codigoUsuario, gsEgresosVarios_BuscarCabeceraResult objEVCabecera, List<gsEgresosVarios_BuscarDetalleResult> lstEVDetalles, DateTime fechaInicio);
        void EgresosVarios_Eliminar(int idEmpresa, int codigoUsuario, int idOperacion);
        gsEgresosVariosUsuario_BuscarResult EgresosVariosUsuario_Buscar(int idEmpresa, int codigoUsuario, string ID_Agenda);
        void EgresosVariosAprobar_Registrar(int idEmpresa, int codigoUsuario, int Op);
        void EgresosVarios_Aprobar(int idEmpresa, int codigoUsuario, decimal ID_Doc);


        /// <summary>
        /// EGRESOS VARIOS INTRANET 
        /// </summary>
        int EgresosVarios_RegistrarInt(int idEmpresa, int codigoUsuario, gsEgresosVariosInt_BuscarCabeceraResult objEVCabecera, List<gsEgresosVariosDInt_BuscarDetalleResult> lstEVDetalles, DateTime fechaInicio); //
        List<gsEgresosVariosInt_ListarCajaChicaResult> EgresosVariosInt_ListarCajaChica(int idEmpresa, int codigoUsuario, string idAgenda, DateTime fechaInicio, DateTime fechaFinal, bool revisarGastos, string id_proveedor);
        gsEgresosVariosInt_BuscarCabeceraResult EgresosVariosInt_Buscar(int idEmpresa, int codigoUsuario, int idOperacion, ref bool? bloqueado, ref string mensajeBloqueado, ref List<gsEgresosVariosDInt_BuscarDetalleResult> lstEgresosVarios);
        gsFlujoPermisoEditarResult FlujoPermisoEditar(int idEmpresa, int codigoUsuario, int idPerfil, string idCcosto);
        void AprobarEgresoVariosFlujo(int idEmpresa, int codigoUsuario, int idPerfil, string idCcosto, int Id, char Ok, string Observacion);
        List<gsEgresoCajaFlujoResult> EgresoCajaFlujo(int idEmpresa, int codigoUsuario, int id);
        void EgresosVariosInt_Eliminar(int idEmpresa, int codigoUsuario, int id);
        List<gsIndicadorEnvioResult> IndicadorEnvio(int idEmpresa, int codigoUsuario);

        void EgresosVarios_RegistrarGenesys(int idEmpresa, int codigoUsuario, gsEgresosVarios_BuscarCabeceraResult objEVCabecera, List<gsEgresosVarios_BuscarDetalleResult> lstEVDetalles, DateTime fechaInicio, int id);

        void ActualizarOP_EgresosVariosInt(int idEmpresa, int codigoUsuario, int id, int op);
        DateTime EgresosVariosFechaInicio_Obtener(int idEmpresa, int codigoUsuario, int id);

        List<gsEgresosVarios_MaxObservacionesResult> ListarObservaciones_Usuario(int idEmpresa, int codigoUsuario);
        List<gsEgresosVarios_DetalleObservacionesResult> ListarDetalleObservaciones_Usuario(int idEmpresa, int codigoUsuario);
        List<gsIndicadorNoEnvioResult> ListarIndicadorNoEnvio(int idEmpresa, int codigoUsuario);
    }

    public class EgresosBL : IEgresoBL
    {
        public void EgresosVariosAprobar_Registrar(int idEmpresa, int codigoUsuario, int Op)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    dcg.gsEgresosVariosAprobar_Registrar(Op, codigoUsuario);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException("No se pudo registrar la aprobacion los Egresos Varios en Genesys.");
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }

        public gsEgresosVariosUsuario_BuscarResult EgresosVariosUsuario_Buscar(int idEmpresa, int codigoUsuario, string ID_Agenda)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    List<gsEgresosVariosUsuario_BuscarResult> lst = dcg.gsEgresosVariosUsuario_Buscar(ID_Agenda).ToList();
                    if (lst.Count == 0)
                        return null;
                    else
                        return lst[0];
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw ex;
                }
            }
        }

        public void EgresosVarios_Aprobar(int idEmpresa, int codigoUsuario, decimal ID_Doc)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    dcg.VBG01076("CtasCtes", ID_Doc, codigoUsuario, "1");
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException("No se pudo registrar la aprobacion los Egresos Varios en Genesys.");
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }

        public gsEgresosVarios_BuscarCabeceraResult EgresosVarios_Buscar(int idEmpresa, int codigoUsuario, int idOperacion, ref bool? bloqueado, ref string mensajeBloqueado, ref List<gsEgresosVarios_BuscarDetalleResult> lstEgresosVarios)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    lstEgresosVarios = dcg.gsEgresosVarios_BuscarDetalle(idOperacion).ToList();
                    return dcg.gsEgresosVarios_BuscarCabecera(idOperacion, ref bloqueado, ref mensajeBloqueado).Single();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo obtener los egresos de la operación " + idOperacion + " en la base de datos.");
                }
            }
        }

        public void EgresosVarios_Eliminar(int idEmpresa, int codigoUsuario, int idOperacion)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    dcg.VBG00832(idOperacion);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException("No se pudo eliminar los Egresos Varios en Genesys.");
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }

        public List<gsEgresosVarios_ListarCajaChicaResult> EgresosVarios_ListarCajaChica(int idEmpresa,int codigoUsuario, string idAgenda, DateTime fechaInicio, DateTime fechaFinal, bool revisarGastos, string id_proveedor, string serie, decimal numero)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    return dcg.gsEgresosVarios_ListarCajaChica(idAgenda, fechaInicio, fechaFinal, dci.Empresa.Single(x => x.idEmpresa == idEmpresa).idDocCajaChica, revisarGastos, id_proveedor, serie, numero).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo consultar los Egresos Varios en Genesys.");
                }
            }
        }

        public void EgresosVarios_Registrar(int idEmpresa, int codigoUsuario, gsEgresosVarios_BuscarCabeceraResult objEVCabecera, List<gsEgresosVarios_BuscarDetalleResult> lstEVDetalles, DateTime fechaInicio)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                decimal? op = null;
                decimal? numero = null;
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    using (TransactionScope scope = new TransactionScope()) {
                        if (objEVCabecera.Op > 0)
                        {
                            op = objEVCabecera.Op;
                            numero = objEVCabecera.Numero;
                        }

                        dcg.VBG00417(ref op, objEVCabecera.ID_Agenda, null, objEVCabecera.Concepto, objEVCabecera.ID_Moneda,
                                    objEVCabecera.Importe, dci.Empresa.Single(x => x.idEmpresa == idEmpresa).idDocCajaChica,
                                    objEVCabecera.ID_CCosto, objEVCabecera.ID_UnidadGestion, objEVCabecera.ID_UnidadProyecto, 479,
                                    objEVCabecera.Fecha, objEVCabecera.Vcmto, objEVCabecera.Serie, ref numero, 0, objEVCabecera.ID_NaturalezaGastoIngreso, null);

                        dcg.gsEgresosVarios_Registrar(op, fechaInicio);

                        foreach (gsEgresosVarios_BuscarDetalleResult objEVDetalle in lstEVDetalles)
                        {
                            decimal? idAmarre = null;
                            if (objEVDetalle.ID_Amarre > 0)
                                idAmarre = objEVDetalle.ID_Amarre;

                            if (objEVDetalle.Estado == 1)
                                dcg.VBG00418(ref idAmarre, op, objEVDetalle.ID_Agenda, null, objEVDetalle.ID_Item, null, objEVCabecera.ID_CCosto, 
                                    objEVCabecera.ID_UnidadGestion, objEVCabecera.ID_UnidadProyecto, objEVDetalle.Importe, objEVDetalle.ID_Documento, 
                                    objEVDetalle.Serie, objEVDetalle.Numero, objEVDetalle.FechaEmision, objEVDetalle.Observaciones, 
                                    objEVDetalle.ImporteBaseIGV, objEVDetalle.ImporteIGV, objEVDetalle.ImporteInafecto);
                            else
                            {
                                if (objEVDetalle.ID_Amarre > 0)
                                    dcg.VBG00419(objEVDetalle.ID_Amarre);
                            }
                        }
                        dcg.SubmitChanges();
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo registrar los Egresos Varios en Genesys.");
                }
            }
        }

        /// <summary>
        /// EGRESOS VARIOS INTRANET 
        /// </summary>
    
        public int EgresosVarios_RegistrarInt(int idEmpresa, int codigoUsuario, gsEgresosVariosInt_BuscarCabeceraResult objEVCabecera, List<gsEgresosVariosDInt_BuscarDetalleResult> lstEVDetalles,DateTime fechaInicio) 
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                decimal? id = null;
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (objEVCabecera.Id > 0)
                        {
                            id = objEVCabecera.Id;
                          
                        }

                        
                        dcg.gsRegistrarEgresosVarios(ref id, objEVCabecera.ID_Agenda, null, objEVCabecera.Concepto, objEVCabecera.ID_Moneda,
                                    objEVCabecera.Importe, dci.Empresa.Single(x => x.idEmpresa == idEmpresa).idDocCajaChica,
                                    objEVCabecera.ID_CCosto, objEVCabecera.ID_UnidadGestion, objEVCabecera.ID_UnidadProyecto, 479,
                                    objEVCabecera.Fecha, objEVCabecera.Vcmto, 0, objEVCabecera.ID_NaturalezaGastoIngreso, null);

                        dcg.gsEgresosVariosInicioInt_Registrar(int.Parse(id.ToString()), fechaInicio);

                        foreach (gsEgresosVariosDInt_BuscarDetalleResult objEVDetalle in lstEVDetalles)
                        {
                            decimal? idAmarre = null;
                            if (objEVDetalle.ID_Amarre > 0)
                                idAmarre = objEVDetalle.ID_Amarre;

                            if (objEVDetalle.Estado == 1)
                                dcg.gsRegistrarEgresosVariosDetalle_Int(ref idAmarre, id, objEVDetalle.ID_Agenda, null, objEVDetalle.ID_Item, null, objEVCabecera.ID_CCosto,
                                    objEVCabecera.ID_UnidadGestion, objEVCabecera.ID_UnidadProyecto, objEVDetalle.Importe, objEVDetalle.ID_Documento,
                                    objEVDetalle.Serie, objEVDetalle.Numero, objEVDetalle.FechaEmision, objEVDetalle.Observaciones,
                                    objEVDetalle.ImporteBaseIGV, objEVDetalle.ImporteIGV, objEVDetalle.ImporteInafecto);
                            else
                                dcg.gsEliminarEgresosVariosDetalle_Int(objEVDetalle.ID_Amarre);
                            
                        }
                        dcg.SubmitChanges();
                        scope.Complete();

                        return int.Parse(id.ToString());
                    }
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo registrar los Egresos Varios en Genesys.");
                }
            }
        }

        public List<gsEgresosVariosInt_ListarCajaChicaResult> EgresosVariosInt_ListarCajaChica(int idEmpresa, int codigoUsuario, string idAgenda, DateTime fechaInicio, DateTime fechaFinal, bool revisarGastos, string id_proveedor)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    return dcg.gsEgresosVariosInt_ListarCajaChica(idAgenda, fechaInicio, fechaFinal, dci.Empresa.Single(x => x.idEmpresa == idEmpresa).idDocCajaChica, revisarGastos, id_proveedor).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo consultar los Egresos Varios en Genesys.");
                }
            }
        }

        public gsEgresosVariosInt_BuscarCabeceraResult EgresosVariosInt_Buscar(int idEmpresa, int codigoUsuario, int idOperacion, ref bool? bloqueado, ref string mensajeBloqueado, ref List<gsEgresosVariosDInt_BuscarDetalleResult> lstEgresosVarios)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    lstEgresosVarios = dcg.gsEgresosVariosDInt_BuscarDetalle(idOperacion).ToList();
                    return dcg.gsEgresosVariosInt_BuscarCabecera(idOperacion).Single();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo obtener los egresos de la operación " + idOperacion + " en la base de datos.");
                }
            }
        }

        public gsFlujoPermisoEditarResult FlujoPermisoEditar(int idEmpresa, int codigoUsuario, int idPerfil, string idCcosto) {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    List<gsFlujoPermisoEditarResult> lst = dcg.gsFlujoPermisoEditar(idPerfil,idCcosto).ToList();
                    if (lst.Count == 0)
                        return null;
                    else
                        return lst[0];
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw ex;
                }
            }
        }

        public void AprobarEgresoVariosFlujo(int idEmpresa, int codigoUsuario, int idPerfil, string idCcosto, int Id, char Ok, string Observacion) {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    dcg.gsAprobarEgresoVarioFlujo(idPerfil,idCcosto,Id,Ok,Observacion);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException("No se pudo registrar la aprobacion los Egresos Varios en Genesys.");
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }

        public List<gsEgresoCajaFlujoResult> EgresoCajaFlujo(int idEmpresa, int codigoUsuario, int id) {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    return dcg.gsEgresoCajaFlujo(id).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo consultar los flujos de los Egresos Varios en Genesys.");
                }
            }
        }

        public void EgresosVariosInt_Eliminar(int idEmpresa, int codigoUsuario, int id) {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    dcg.gsEgresosVariosInt_Eliminar(id);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException("No se pudo eliminar los Egresos Varios en Intranet.");
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }

        public List<gsIndicadorEnvioResult> IndicadorEnvio(int idEmpresa, int codigoUsuario) {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    return dcg.gsIndicadorEnvio(idEmpresa).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo consultar el indicador de envio de Egresos Varios en Genesys.");
                }
            }
        }

        public void EgresosVarios_RegistrarGenesys(int idEmpresa, int codigoUsuario, gsEgresosVarios_BuscarCabeceraResult objEVCabecera, List<gsEgresosVarios_BuscarDetalleResult> lstEVDetalles, DateTime fechaInicio, int id) {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                decimal? op = null;
                decimal? numero = null;
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (objEVCabecera.Op > 0)
                        {
                            op = objEVCabecera.Op;
                            numero = objEVCabecera.Numero;
                        }

                        dcg.VBG00417(ref op, objEVCabecera.ID_Agenda, null, objEVCabecera.Concepto, objEVCabecera.ID_Moneda,
                                    objEVCabecera.Importe, dci.Empresa.Single(x => x.idEmpresa == idEmpresa).idDocCajaChica,
                                    objEVCabecera.ID_CCosto, objEVCabecera.ID_UnidadGestion, objEVCabecera.ID_UnidadProyecto, 479,
                                    objEVCabecera.Fecha, objEVCabecera.Vcmto, objEVCabecera.Serie, ref numero, 0, objEVCabecera.ID_NaturalezaGastoIngreso, null);

                        dcg.gsEgresosVarios_Registrar(op, fechaInicio);
                        dcg.gsActualizarOp_EgresosInt(id,int.Parse(op.ToString()));

                        foreach (gsEgresosVarios_BuscarDetalleResult objEVDetalle in lstEVDetalles)
                        {
                            decimal? idAmarre = null;
                            if (objEVDetalle.ID_Amarre > 0)
                                idAmarre = objEVDetalle.ID_Amarre;

                            if (objEVDetalle.Estado == 1)
                                dcg.VBG00418(ref idAmarre, op, objEVDetalle.ID_Agenda, null, objEVDetalle.ID_Item, null, objEVCabecera.ID_CCosto,
                                    objEVCabecera.ID_UnidadGestion, objEVCabecera.ID_UnidadProyecto, objEVDetalle.Importe, objEVDetalle.ID_Documento,
                                    objEVDetalle.Serie, objEVDetalle.Numero, objEVDetalle.FechaEmision, objEVDetalle.Observaciones,
                                    objEVDetalle.ImporteBaseIGV, objEVDetalle.ImporteIGV, objEVDetalle.ImporteInafecto);
                            else
                            {
                                if (objEVDetalle.ID_Amarre > 0)
                                    dcg.VBG00419(objEVDetalle.ID_Amarre);
                            }
                        }
                        dcg.SubmitChanges();
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo registrar los Egresos Varios en Genesys.");
                }
            }
        }

        public void ActualizarOP_EgresosVariosInt(int idEmpresa, int codigoUsuario, int id, int op) {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    dcg.gsActualizarOp_EgresosInt(id,op);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException("No se actualizar la op en el egreso vario.");
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }

        public DateTime EgresosVariosFechaInicio_Obtener(int idEmpresa, int codigoUsuario, int id) {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    return  dcg.gsEgresosVariosInicioInt_Obtener(id).FirstOrDefault().fechaInicio.Value;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException("No se pudo obtener la fecha de inicio.");
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }

        public List<gsEgresosVarios_MaxObservacionesResult> ListarObservaciones_Usuario(int idEmpresa, int codigoUsuario)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    return dcg.gsEgresosVarios_MaxObservaciones().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo consultar las observaciones de los Egresos Varios en Genesys.");
                }
            }
        }

        public List<gsEgresosVarios_DetalleObservacionesResult> ListarDetalleObservaciones_Usuario(int idEmpresa, int codigoUsuario) {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    return dcg.gsEgresosVarios_DetalleObservaciones(codigoUsuario).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo consultar el detalle de las observaciones de los Egresos Varios en Genesys.");
                }
            }
        }

        public List<gsIndicadorNoEnvioResult> ListarIndicadorNoEnvio(int idEmpresa, int codigoUsuario) {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    return dcg.gsIndicadorNoEnvio(idEmpresa).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo consultar el indicador de no envío de Egresos Varios en Genesys.");
                }
            }
        }
    }
}
