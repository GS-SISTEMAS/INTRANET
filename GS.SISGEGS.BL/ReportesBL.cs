using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.BL
{
    public interface IReportesBL
    {
        List<Reporte_CostoProduccionResult> ReporteCostoProduccion(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal);
        List<Reporte_CostoVentaResult> ReporteCostoVenta(int idEmpresa, int codigoUsuario, int idMoneda, DateTime fechaInicio, DateTime fechaFinal, int mesEvaluar, int kardex, int id_forma);
        List<Reporte_Venta_TransfResult> ReporteVenta(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, int formaPago, int idMoneda, int? kardex);

        //-------------------------------------------
        List<ReporteAfiliadasResult> ResumenAfiliadas(int idEmpresa, int codigoUsuario, string operacion, DateTime fechaCorte, decimal moneda);
        List<DetalleOperacionFamiliaResult> DetalleOperacionFamilia(int idEmpresa, int codigoUsuario, DateTime fechaCorte, string operacion, string id_agenda, decimal moneda);
        List<DetalleOperacionFamiliaAnticuamientoResult> DetalleOperacionFamiliaAnticuamiento(int idEmpresa, int codigoUsuario, DateTime fechaCorte, string operacion, string id_agenda, decimal moneda);
        //-------------------------------------------
        List<GS_ReporteDetraccionesResult> ReporteDetracciones(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, string idAgenda, int estatus);
        void Accion_Registrar(int idEmpresa, int codigoUsuario, GS_DetraccionAccionGetResult objAccionInsert);
        List<GS_DetraccionAccionGetResult> GetDetraccionAccion(int idEmpresa, int codigoUsuario, int op);

        List<GS_GetVoucherDetraccionesResult> GetVoucherDetraccione(int idEmpresa, int codigoUsuario, int op); 
    }

    public class ReportesBL : IReportesBL
    {
        public List<Reporte_CostoProduccionResult> ReporteCostoProduccion(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                List<Reporte_CostoProduccionResult> lista = new List<Reporte_CostoProduccionResult>(); 
                try
                {
                    lista = dcg.Reporte_CostoProduccion(fechaInicio,fechaFinal).ToList();
                    return lista; 
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar los costos de los productos.");
                }
            }
        }

        public List<Reporte_CostoVentaResult> ReporteCostoVenta(int idEmpresa, int codigoUsuario,int idMoneda, DateTime fechaInicio, DateTime fechaFinal, int mesEvaluar, int kardex, int id_forma)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                  
                    return dcg.Reporte_CostoVenta(idMoneda, fechaInicio, fechaFinal, mesEvaluar, kardex, id_forma).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar las ventas de los productos.");
                }
            }
        }

        public List<Reporte_Venta_TransfResult> ReporteVenta(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, int formaPago, int idMoneda, int? kardex)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
               
                    return dcg.Reporte_Venta_Transf(fechaInicio, fechaFinal, formaPago, idMoneda, kardex).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar las ventas de los productos.");
                }
            }
        }

        //----------------------------------------

        public List<ReporteAfiliadasResult> ResumenAfiliadas(int idEmpresa, int codigoUsuario, string operacion, DateTime fechaCorte, decimal moneda)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                  
                    dcg.CommandTimeout = 1000000;
                    return dcg.ReporteAfiliadas(Convert.ToChar(operacion), fechaCorte, moneda).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar el resumen de afiliacion");
                }
            }
        }

        public List<DetalleOperacionFamiliaResult> DetalleOperacionFamilia(int idEmpresa, int codigoUsuario, DateTime fechaCorte, string operacion, string id_agenda, decimal moneda)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                      return dcg.DetalleOperacionFamilia(fechaCorte, Convert.ToChar(operacion), id_agenda, moneda).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar el detalle del resumen de afiliacion");
                }
            }
        }

        public List<DetalleOperacionFamiliaAnticuamientoResult> DetalleOperacionFamiliaAnticuamiento(int idEmpresa, int codigoUsuario, DateTime fechaCorte, string operacion, string id_agenda, decimal moneda)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.DetalleOperacionFamiliaAnticuamiento(fechaCorte, Convert.ToChar(operacion), id_agenda, moneda).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar el detalle del resumen de afiliacion");
                }
            }
        }

        //----------------------------------------
        public List<GS_ReporteDetraccionesResult> ReporteDetracciones(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, string idAgenda, int estatus)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                   return dcg.GS_ReporteDetracciones(fechaInicio, fechaFinal, idAgenda, estatus).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar las ventas de los productos.");
                }
            }
        }

        public void Accion_Registrar(int idEmpresa, int codigoUsuario, GS_DetraccionAccionGetResult objAccionInsert)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    dcg.GS_DetraccionAccionInsert(objAccionInsert.NroConstancia, objAccionInsert.FechaPago, objAccionInsert.Op);

                    dcg.SubmitChanges();
                    dcg.Connection.Close();

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo registrar la Accion en la base de datos de Genesys.");
                }
                finally
                {
                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
            }
        }

        public List<GS_DetraccionAccionGetResult> GetDetraccionAccion(int idEmpresa, int codigoUsuario, int op)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                   return dcg.GS_DetraccionAccionGet(op).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar la accion.");
                }
            }
        }


        public List<GS_GetVoucherDetraccionesResult> GetVoucherDetraccione(int idEmpresa, int codigoUsuario, int op)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    return dcg.GS_GetVoucherDetracciones(op).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar los vouchers.");
                }
            }
        }

    }
}
