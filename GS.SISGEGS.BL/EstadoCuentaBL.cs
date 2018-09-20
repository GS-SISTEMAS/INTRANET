using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.SqlClient;
using System.Data;
using GS.SISGEGS.BL;
using GS.SISGEGS.DM;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface IEstadoCuentaBL {

        List<gsReporte_DocumentosPendientesResult> EstadoCuenta_ListarxCliente(int idEmpresa, int codigoUsuario,string codAgenda, string codVendedor, DateTime  fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos, int id_zona);
        List<gsAgendaCliente_BuscarLimiteCreditoResult> EstadoCuenta_LimiteCreditoxCliente(int idEmpresa, int codigoUsuario, string codAgenda, int Todos);
        List<gsReporte_DocumentosPendientesResumenClienteResult> EstadoCuenta_ListarResumenCliente(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos);
        List<gsReporte_DocumentosPendientesResumenVendedorResult> EstadoCuenta_ListarResumenVendedor(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos);
        List<gsReporte_DeudaVencidaTotalResult> EstadoCuenta_ListarResumenTotal(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos);
        List<gsReporteVentasxDiasCobranzaResult> EstadoCuenta_VentasDiasCobranza(int idEmpresa, int codigoUsuario, string id_agenda, string id_vendedor, int anho, int mes);
        List<gsReporteVencidosPorMesResult> EstadoCuenta_VencidosMes(int idEmpresa, int codigoUsuario, DateTime fecha);

        List<gsReporteVencidosPorMesClienteResult> EstadoCuenta_VencidosMesCliente(int idEmpresa, int codigoUsuario, DateTime fecha);

        List<gsReporteVencidosPorMesDetalleResult> EstadoCuenta_VencidosMesDetalle(int idEmpresa, int codigoUsuario, string id_agenda, string id_vendedor, DateTime fecha);

        List<gsReporte_DocumentosPendientes_ProyectadoResult> EstadoCuenta_ListarxCliente_Proyectado(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos);

        List<gsEstadoCuenta_MesBIResult> EstadoCuenta_ListarxBI(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fecha);

        List<gsReporte_DocumentosPendientesBIResult> EstadoCuenta_ListarxClienteBI(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos);

        List<VBG00062_PROVEEDORResult> EstadoCuenta_ReporteProveedor(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal);

        List<gsEstadoCuenta_TotalResult> EstadoCuenta_ClienteBI(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaFinal);

        List<GS_LetrasGraficoPieResult> EstadoCuenta_GraficoPie(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos);

        List<GS_GetLetrasPorEstadoResult> EstadoCuenta_LetrasPorEstados(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos);

        List<GS_GetLetrasVencPorPlazoResult> EstadoCuenta_LetrasVencPorPlazo(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal);

        List<GS_GetFacturasVencPorPlazoResult> EstadoCuenta_FacturasVencPorPlazo(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal);

        List<GS_GetCicloLetrasResult> GetCicloLetras(int idEmpresa, int codigoUsuario, decimal opFinan);

        List<GS_ReporteEstadoCuentaMngResult> EstadoCuenta_ListarAprobacion(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos);

        void RiesgoCliente_RegistrarBulkCopy(int idEmpresa, int codigoUsuario, DataTable tabla);
    }
    public class EstadoCuentaBL : IEstadoCuentaBL
    {   
        public List<gsReporte_DocumentosPendientesResult> EstadoCuenta_ListarxCliente(int idEmpresa, int codigoUsuario,string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos, int id_zona)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                   
                    dcg.CommandTimeout = 90; 
                    List < gsReporte_DocumentosPendientesResult > list = dcg.gsReporte_DocumentosPendientes(null, null, null, null, codAgenda, codVendedor, 0, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos, id_zona).ToList();

                    return list;

                }
                catch (Exception ex)
                {
                    dcg.Connection.Close(); 
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Estado Cuentas en la base de datos.");
                }

            }
        }

        public List<gsAgendaCliente_BuscarLimiteCreditoResult> EstadoCuenta_LimiteCreditoxCliente(int idEmpresa, int codigoUsuario, string codAgenda, int Todos)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<gsAgendaCliente_BuscarLimiteCreditoResult> lstLineaCredito;
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    lstLineaCredito = dcg.gsAgendaCliente_BuscarLimiteCredito(codAgenda, idEmpresa, codigoUsuario, Todos).ToList();

                    return lstLineaCredito;

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar Limite de Credito en la base de datos.");
                }

            }
        }

        public List<gsReporte_DocumentosPendientesResumenClienteResult> EstadoCuenta_ListarResumenCliente(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    dcg.CommandTimeout = 160; 
                    List<gsReporte_DocumentosPendientesResumenClienteResult> list = dcg.gsReporte_DocumentosPendientesResumenCliente(null, null, null, null, codAgenda, codVendedor, null, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Estado Cuentas en la base de datos.");
                }

            }
        }

        public List<gsReporte_DocumentosPendientesResumenVendedorResult> EstadoCuenta_ListarResumenVendedor(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<gsReporte_DocumentosPendientesResumenVendedorResult> list = dcg.gsReporte_DocumentosPendientesResumenVendedor(null, null, null, null, codAgenda, codVendedor, null, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Estado Cuentas en la base de datos.");
                }

            }
        }

        public List<gsReporte_DeudaVencidaTotalResult> EstadoCuenta_ListarResumenTotal(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.CommandTimeout = 90;

                    List<gsReporte_DeudaVencidaTotalResult> list = dcg.gsReporte_DeudaVencidaTotal(null, null, null, null, codAgenda, codVendedor, null, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, 0).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Estado Cuentas en la base de datos.");
                }

            }
        }

        public List<gsReporteVentasxDiasCobranzaResult> EstadoCuenta_VentasDiasCobranza(int idEmpresa, int codigoUsuario, string id_agenda, string id_vendedor, int anho, int mes)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<gsReporteVentasxDiasCobranzaResult> list = dcg.gsReporteVentasxDiasCobranza(id_agenda, id_vendedor,  anho, mes).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Estado Cuentas en la base de datos.");
                }

            }
        }

        public List<gsReporteVencidosPorMesResult> EstadoCuenta_VencidosMes(int idEmpresa, int codigoUsuario, DateTime fecha)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<gsReporteVencidosPorMesResult> list = dcg.gsReporteVencidosPorMes(fecha).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Estado Cuentas en la base de datos.");
                }

            }
        }

        public List<gsReporteVencidosPorMesClienteResult> EstadoCuenta_VencidosMesCliente(int idEmpresa, int codigoUsuario, DateTime fecha)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<gsReporteVencidosPorMesClienteResult> list = dcg.gsReporteVencidosPorMesCliente(fecha).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Estado Cuentas en la base de datos.");
                }

            }
        }

        public List<gsReporteVencidosPorMesDetalleResult> EstadoCuenta_VencidosMesDetalle(int idEmpresa, int codigoUsuario, string id_agenda, string id_vendedor, DateTime fecha)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.CommandTimeout = 90; 
                    List<gsReporteVencidosPorMesDetalleResult> list = dcg.gsReporteVencidosPorMesDetalle(id_agenda, id_vendedor, fecha).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Estado Cuentas en la base de datos.");
                }

            }
        }

        public List<gsReporte_DocumentosPendientes_ProyectadoResult> EstadoCuenta_ListarxCliente_Proyectado(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    List<gsReporte_DocumentosPendientes_ProyectadoResult> list = dcg.gsReporte_DocumentosPendientes_Proyectado(null, null, null, null, codAgenda, codVendedor, null, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos).ToList();

                    return list;

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Estado Cuentas en la base de datos.");
                }

            }
        }

        public List<gsEstadoCuenta_MesBIResult> EstadoCuenta_ListarxBI(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fecha)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    List<gsEstadoCuenta_MesBIResult> list = dcg.gsEstadoCuenta_MesBI(codAgenda, codVendedor, fecha).ToList();

                    return list;

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Estado Cuentas en la base de datos.");
                }

            }
        }

        public List<gsReporte_DocumentosPendientesBIResult> EstadoCuenta_ListarxClienteBI(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.CommandTimeout = 60; 
                    List<gsReporte_DocumentosPendientesBIResult> list = dcg.gsReporte_DocumentosPendientesBI(null, null, null, null, codAgenda, codVendedor, null, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos).ToList();

                    return list;

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Estado Cuentas en la base de datos.");
                }

            }
        }

        public List<VBG00062_PROVEEDORResult> EstadoCuenta_ReporteProveedor(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.CommandTimeout = 90;
                    List<VBG00062_PROVEEDORResult> list = dcg.VBG00062_PROVEEDOR(null, fechaEmisionInicial, fechaEmisionFinal, null, null, null, null, null, null, null, null, null, null).ToList();

                    return list;

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Estado Cuentas en la base de datos.");
                }

            }
        }

        public List<gsEstadoCuenta_TotalResult> EstadoCuenta_ClienteBI(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor,  DateTime fechaFinal)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    dcg.CommandTimeout = 60;
                    List<gsEstadoCuenta_TotalResult> list = dcg.gsEstadoCuenta_Total(codAgenda, codVendedor, fechaFinal).ToList();

                    return list;

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Estado Cuentas en la base de datos.");
                }

            }
        }

        public List<GS_GetCicloLetrasResult> GetCicloLetras(int idEmpresa, int codigoUsuario, decimal opFinan)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    dcg.CommandTimeout = 90;
                    List<GS_GetCicloLetrasResult> list = dcg.GS_GetCicloLetras(opFinan).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Estado Cuentas en la base de datos.");
                }

            }
        }

        public List<GS_LetrasGraficoPieResult> EstadoCuenta_GraficoPie(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {

                    dcg.CommandTimeout = 90;
                    List<GS_LetrasGraficoPieResult> list = dcg.GS_LetrasGraficoPie(null, null, null, null, codAgenda, codVendedor, null, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos).ToList();

                    return list;

                }
                catch (Exception ex)
                {
                    dcg.Connection.Close();
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Estado Cuentas en la base de datos.");
                }

            }
        }

        public List<GS_GetLetrasPorEstadoResult> EstadoCuenta_LetrasPorEstados(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {

                    dcg.CommandTimeout = 90;
                    List<GS_GetLetrasPorEstadoResult> list = dcg.GS_GetLetrasPorEstado(codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos).ToList();

                    return list;

                }
                catch (Exception ex)
                {
                    dcg.Connection.Close();
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Estado Cuentas en la base de datos.");
                }

            }
        }

        public List<GS_GetLetrasVencPorPlazoResult> EstadoCuenta_LetrasVencPorPlazo(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {

                    dcg.CommandTimeout = 90;
                    List<GS_GetLetrasVencPorPlazoResult> list = dcg.GS_GetLetrasVencPorPlazo(codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal).ToList();

                    return list;

                }
                catch (Exception ex)
                {
                    dcg.Connection.Close();
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Estado Cuentas en la base de datos.");
                }

            }
        }

        public List<GS_GetFacturasVencPorPlazoResult> EstadoCuenta_FacturasVencPorPlazo(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {

                    dcg.CommandTimeout = 90;
                    List<GS_GetFacturasVencPorPlazoResult> list = dcg.GS_GetFacturasVencPorPlazo(codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal).ToList();

                    return list;

                }
                catch (Exception ex)
                {
                    dcg.Connection.Close();
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Estado Cuentas en la base de datos.");
                }

            }
        }


        public List<gsReporte_DocumentosPendientes_LegalResult> EstadoCuenta_Listar_Legal(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    dcg.CommandTimeout = 120;
                    List<gsReporte_DocumentosPendientes_LegalResult> list = dcg.gsReporte_DocumentosPendientes_Legal(null, null, null, null, codAgenda, codVendedor, null, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Estado Cuentas en la base de datos.");
                }

            }
        }

        public List<gsReporte_DocumentosPendientes_ProvisionResult> EstadoCuenta_Listar_Provision(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    dcg.CommandTimeout = 120;
                    List<gsReporte_DocumentosPendientes_ProvisionResult> list = dcg.gsReporte_DocumentosPendientes_Provision(null, null, null, null, codAgenda, codVendedor, null, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Estado Cuentas en la base de datos.");
                }

            }
        }


        public List<GS_ReporteEstadoCuentaMngResult> EstadoCuenta_ListarAprobacion(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {

                    dcg.CommandTimeout = 90;
                    List<GS_ReporteEstadoCuentaMngResult> list = dcg.GS_ReporteEstadoCuentaMng(null, null, null, null, codAgenda, codVendedor, null, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos).ToList();

                    return list;

                }
                catch (Exception ex)
                {
                    dcg.Connection.Close();
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Estado Cuentas en la base de datos.");
                }

            }
        }

        public void RiesgoCliente_RegistrarBulkCopy(int idEmpresa, int codigoUsuario, DataTable tabla)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                SqlConnection conn;
                try
                {

                    dci.Connection.Open();
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.Connection.Open();
                    dcg.CommandTimeout = 90;

                    conn = new SqlConnection(dcg.Connection.ConnectionString);

                    using (SqlConnection connection = conn)
                    {
                        connection.Open();
                        string commandText = "delete LimiteCredito_Temp where cod_usuario = @cod_usuario and IdEmpresa = @IdEmpresa;";
                        SqlCommand command = new SqlCommand(commandText, connection);
                        command.Parameters.Add("@cod_usuario", SqlDbType.Int);
                        command.Parameters["@cod_usuario"].Value = codigoUsuario;

                        command.Parameters.Add("@IdEmpresa", SqlDbType.Int);
                        command.Parameters["@IdEmpresa"].Value = idEmpresa;


                        command.ExecuteNonQuery();


                        // Create a table with some rows. 
                        DataTable newReporte = tabla;

                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                        {
                            bulkCopy.DestinationTableName = "dbo.LimiteCredito_Temp";

                            try
                            {
                                bulkCopy.WriteToServer(newReporte);
                            }
                            catch (Exception ex)
                            {
                                string mensaje = ex.Message + " - " + ex.TargetSite.Name;
                                throw new ArgumentException("Error al momento de registrar el pronostico masivo en la base de datos.");
                            }
                        }

                        connection.Close();

                    }

                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de registrar las cobranzas en la base de datos.");
                }

            }
        }




    }
}
