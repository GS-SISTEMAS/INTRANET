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
        List<gsEgresosVarios_ListarCajaChicaResult> EgresosVarios_ListarCajaChica(int idEmpresa, int codigoUsuario, string idAgenda, DateTime fechaInicio, DateTime fechaFinal);
        gsEgresosVarios_BuscarCabeceraResult EgresosVarios_Buscar(int idEmpresa, int codigoUsuario, int idOperacion, ref bool? bloqueado, ref string mensajeBloqueado, ref List<gsEgresosVarios_BuscarDetalleResult> lstEgresosVarios);
        void EgresosVarios_Registrar(int idEmpresa, int codigoUsuario, gsEgresosVarios_BuscarCabeceraResult objEVCabecera, List<gsEgresosVarios_BuscarDetalleResult> lstEVDetalles);
        void EgresosVarios_Eliminar(int idEmpresa, int codigoUsuario, int idOperacion);
    }

    public class EgresosBL : IEgresoBL
    {
        public gsEgresosVarios_BuscarCabeceraResult EgresosVarios_Buscar(int idEmpresa, int codigoUsuario, int idOperacion, ref bool? bloqueado, ref string mensajeBloqueado, ref List<gsEgresosVarios_BuscarDetalleResult> lstEgresosVarios)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
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
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
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

        public List<gsEgresosVarios_ListarCajaChicaResult> EgresosVarios_ListarCajaChica(int idEmpresa,int codigoUsuario, string idAgenda, DateTime fechaInicio, DateTime fechaFinal)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    return dcg.gsEgresosVarios_ListarCajaChica(idAgenda, fechaInicio, fechaFinal, dci.Empresa.Single(x => x.idEmpresa == idEmpresa).idDocCajaChica).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo consultar los Egresos Varios en Genesys.");
                }
            }
        }

        public void EgresosVarios_Registrar(int idEmpresa, int codigoUsuario, gsEgresosVarios_BuscarCabeceraResult objEVCabecera, List<gsEgresosVarios_BuscarDetalleResult> lstEVDetalles)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                decimal? op = null;
                decimal? numero = null;
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
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
    }
}
