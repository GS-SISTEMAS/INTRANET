using System;
using System.Collections.Generic;
using System.Linq;
using GS.SISGEGS.DM;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using GS.SISGEGS.BL;



namespace GS.SISGEGS.BL
{
    public interface ICobranzasBL
    {
        List<gsReporteCanceladosWebResult> Reporte_Cancelaciones(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaInicial, DateTime fechaFinal);

        List<gsDocVenta_ReporteVenta_ClienteResult> Reporte_VentasCliente(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaInicial, DateTime fechaFinal, string ESTADO);
        List<gsDocVenta_ReporteVenta_ClienteResumenResult> Reporte_VentasClienteResumen(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaInicial, DateTime fechaFinal, int year);
        List<gsReporteCanceladosWebResumenResult> Reporte_CancelacionesResumen(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, int year);
        List<gsReporteCanceladosWebResumenVendedorResult> Reporte_CancelacionesResumenVendedor(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaInicial, DateTime fechaFinal);
        List<gsReporteCanceladosResumenMes_actualResult> Reporte_CancelacionesResumenActual(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, int year, int mes);
        List<gsReporteCobranzaWeb_DetalleMesResult> Reporte_CancelacionesResumenDetalleMes(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, int year, int mes);
        List<gsReporteCanceladosResumenMes_actualResult> Reporte_CancelacionesResumenActual_v2(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, int year, int mes);
        List<gsReporteCanceladosResumenMes_v3Result> Reporte_CancelacionesResumenv3(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, int year, int mes);
        List<gsProyectadoCobranza_ListarResult> Reporte_Proyectado_Sectorista(int idEmpresa, int codigoUsuario, int id_proyectado, int periodo, string id_sectorista, string id_Cliente, int id_zona, string id_vendedor);
        void Insertar_ProyectadoSectorista(int idEmpresa, int codigoUsuario, int id_proyectado, string periodo, string id_sectorista, string id_cliente, decimal montoS1, decimal montoS2, decimal montoS3, decimal montoS4, decimal proyectado, int estado);

        List<gsGestionCobranza_ListarResult> Reporte_Gestion_Sectorista(int idEmpresa, int codigoUsuario, string id_Cliente, int Periodo);

        List<gsEstatus_ListarResult> Estatus_Deuda_Listar(int idEmpresa, int codigoUsuario);


        List<gsReporteCobranzas_Poryectadas_VendedorResult> Reporte_CobranzasProyectadasVendedor(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista);
        List<gsReporteCobranzas_Poryectadas_Vendedor_DetalleResult> Reporte_CobranzasProyectadasVendedorDetalle(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista);
        List<gsReporteCobranzas_Poryectadas_Vendedor_FechaResult> Reporte_CobranzasProyectadasVendedor_Fecha(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista);


        void ProyectadoCobranza_Registrar(int idEmpresa, int codigoUsuario, string codAgenda, string codSectorista, string periodo, int id_zona, decimal montoS1, decimal montoS2, decimal montoS3, decimal montoS4, decimal proyectado);
        void GestionCobranza_Registrar(int idEmpresa, int codigoUsuario, string id_cliente, string periodo, int id_semana, int id_estatus, string observacion, int estado, string TablaOrigen, int opOrigen);

        int ProyectadoCobranza_Verificar(int idEmpresa, int codigoUsuario, string codSectorista, string periodo, int id_zona);

        List<gsVentasCobranzas_ListarResult> Reporte_VentasCobranzasAnual(int idEmpresa, int codigoUsuario, int anho);

        List<gsReporteCancelados_ProyectadoResult> Reporte_CobranzasProyectadas_Sectorista(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista);

        List<Reporte_VentaxCobranzaLegalResult> Reporte_VentaCobranzaLegal(int idEmpresa, int codigoUsuario);

        // 10/11/2016 Percy Santiago 
        List<gsReporteFacturasInafectasV1Result> Reporte_FacturasInafecta(int idEmpresa, int codigoUsuario, System.DateTime FechaInicio, DateTime FechaFin, int tipodocumento, string Cliente);
        // fin 10/11/2016

        void ProyectarCobranza_Registrar(int idEmpresa, int codigoUsuario, int id_proyectado, string periodo, int id_semana, float Importe, string TablaOrigen, int OpOrigen, int estado);

        List<ProyectadoCobranza_ListarResult> ProyectadoCobranza_Listar(int idEmpresa, int codigoUsuario, int idProyectado,
            string periodo, int id_semana, string tablaOrigen, int opOrigen, int estado);

        //----------------------------------------------------


        List<ZonasSectorista_ListarResult> ZonasSectorista_Listar(int idEmpresa, int codigoUsuario, string texto, int reporte);

        List<ZonasSectoristaPermiso_ListarResult> ZonasSectoristaPermiso_Listar(int idEmpresa, int codigoUsuario, string id_agenda);
        int PermisosZona_Registrar(int idEmpresa, int codigoUsuario, string id_agenda, int id_zona, int ActivoZona, int ActivoSectorista);

        List<spEstadoCuenta_ProyectadoResult> EstadoCuenta_Proyectado(int idEmpresa, int codigoUsuario, int periodo, string id_sectorista, int id_zona, int anho, int mes);

        List<spEstadoCuenta_Proyectado_ClienteResult> EstadoCuenta_Proyectado_Cliente(int idEmpresa, int codigoUsuario, string id_cliente, int periodo, string id_sectorista, int id_zona, int anho, int mes);

        void ProyectadoCobranza_Eliminar(int idEmpresa, int codigoUsuario, int id_proyectado);

        List<ProyectadoCobranza_DocumentosResult> ProyectadoCobranza_Documentos(int idEmpresa, int codigoUsuario, string id_Cliente, int Periodo);

        List<gsReporteCobranzas_Poryectadas_ClienteResult> Reporte_CobranzasProyectadasCliente(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista);

        List<gsReporteProyectado_Cuadro1Result> Reporte_CobranzasProyectadas_Cuadro1(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista);

        List<gsReporteProyectado_Cuadro2Result> Reporte_CobranzasProyectadas_Cuadro2(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista);

        List<Zonas_Reportes_CobranzaResult> Zonas_Listar_Cobranza(int idEmpresa, int codigoUsuario, string id_sectorista); 

    }
    public class CobranzasBL : ICobranzasBL
    {
        public List<gsReporteCanceladosWebResult> Reporte_Cancelaciones(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaInicial, DateTime fechaFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<gsReporteCanceladosWebResult> list = dcg.gsReporteCanceladosWeb(codAgenda, codVendedor, fechaInicial, fechaFinal).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las cobranzas en la base de datos.");
                }
            }
        }


        public List<gsDocVenta_ReporteVenta_ClienteResult> Reporte_VentasCliente(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaInicial, DateTime fechaFinal, string ESTADO)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsDocVenta_ReporteVenta_Cliente(fechaInicial, fechaFinal, null, codAgenda, null, codVendedor, null, 0, null, null, null, null, null, null, null, null, null, null, null, true, true, true, true, true, true, true, true, true, ESTADO).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta por cliente en la base de datos.");
                }
            }
        }
        public List<gsDocVenta_ReporteVenta_ClienteResumenResult> Reporte_VentasClienteResumen(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaInicial, DateTime fechaFinal, int year)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsDocVenta_ReporteVenta_ClienteResumen(year, fechaInicial, fechaFinal, null, codAgenda, null, codVendedor, null, 0, null, null, null, null, null, null, null, null, null, null, null, true, true, true, true, true, true, true, true, true).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta por cliente en la base de datos.");
                }
            }
        }
        public List<gsReporteCanceladosWebResumenResult> Reporte_CancelacionesResumen(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, int year)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    List<gsReporteCanceladosWebResumenResult> list1 = dcg.gsReporteCanceladosWebResumen(codAgenda, codVendedor , year ).ToList();
                    return list1;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las cobranzas en la base de datos.");
                }
            }
        }
        public List<gsReporteCanceladosWebResumenVendedorResult> Reporte_CancelacionesResumenVendedor(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaInicial, DateTime fechaFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                 
                    List<gsReporteCanceladosWebResumenVendedorResult> list = dcg.gsReporteCanceladosWebResumenVendedor(codAgenda, codVendedor, fechaInicial, fechaFinal).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las cobranzas en la base de datos.");
                }
            }
        }
        public List<gsReporteCanceladosResumenMes_actualResult> Reporte_CancelacionesResumenActual(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, int year, int mes)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    dcg.CommandTimeout = 300; 
                    List<gsReporteCanceladosResumenMes_actualResult> list = dcg.gsReporteCanceladosResumenMes_actual(codAgenda, codVendedor, year, mes).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las cobranzas en la base de datos.");
                }
            }
        }

        public List<gsReporteCanceladosResumenMes_v3Result> Reporte_CancelacionesResumenv3(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, int year, int mes)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    dcg.CommandTimeout = 300;
                    List<gsReporteCanceladosResumenMes_v3Result> list = dcg.gsReporteCanceladosResumenMes_v3(codAgenda, codVendedor, year, mes).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las cobranzas en la base de datos.");
                }
            }
        }
        public List<gsReporteCobranzaWeb_DetalleMesResult> Reporte_CancelacionesResumenDetalleMes(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, int year, int mes)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    dcg.CommandTimeout = 120; 
                    List<gsReporteCobranzaWeb_DetalleMesResult> list = dcg.gsReporteCobranzaWeb_DetalleMes(codAgenda, codVendedor, year, mes).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las cobranzas en la base de datos.");
                }
            }
        }
        public List<gsReporteCanceladosResumenMes_actualResult> Reporte_CancelacionesResumenActual_v2(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, int year, int mes)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    dcg.CommandTimeout = 300;
                    List<gsReporteCanceladosResumenMes_actualResult> list = dcg.gsReporteCanceladosResumenMes_actual(codAgenda, codVendedor, year, mes).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las cobranzas en la base de datos.");
                }
            }
            /////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            //{
            //    string consString;
            //    SqlDataReader rdr = null;
            //    string textSql;


            //    gsReporteCanceladosResumenMes_actualResult resumen;

            //    try
            //    {
            //        //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
            //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));


            //        consString = string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4));
            //        SqlConnection con = new SqlConnection(consString);

            //        SqlCommand cmd = new SqlCommand();
            //        cmd.Connection = con;
            //        cmd.CommandTimeout = 200;

            //        textSql = Properties.Resources.SelectCobrandoMes;

            //        //cmd.CommandText = "gsReporteCanceladosResumenMes_actual";
            //        cmd.CommandText = textSql;

            //        if (codAgenda == null)
            //        {
            //            cmd.Parameters.Add("@ID_Agenda", SqlDbType.VarChar, 20).Value = DBNull.Value;
            //        }
            //        else
            //        {
            //            cmd.Parameters.Add("@ID_Agenda", SqlDbType.VarChar, 20).Value = codAgenda;
            //        }
            //        if (codVendedor == null)
            //        {
            //            cmd.Parameters.Add("@ID_Vendedor", SqlDbType.VarChar).Value = DBNull.Value;
            //        }
            //        else
            //        {
            //            cmd.Parameters.Add("@ID_Vendedor", SqlDbType.VarChar).Value = codVendedor;
            //        }


            //        cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;
            //        cmd.Parameters.Add("@Mes", SqlDbType.Int).Value = mes;

            //        con.Open();
            //        rdr = cmd.ExecuteReader();
            //        //DataTable dt = new DataTable();
            //        //dt.Load(rdr);

            //        resumen = new gsReporteCanceladosResumenMes_actualResult();
            //        if (rdr.HasRows)
            //        {
            //            while (rdr.Read())
            //            {
            //                resumen.ClaseCliente = rdr.GetString(0);
            //                resumen.periodoYearE = rdr.GetInt32(1);
            //                resumen.periodoMesE = rdr.GetInt32(2);
            //                resumen.TotalVentaMes = rdr.GetDecimal(3);
            //                resumen.totalMes = rdr.GetDouble(4);
            //            }
            //        }
            //        else
            //        {
            //            resumen.ClaseCliente = null;
            //            resumen.periodoYearE = null;
            //            resumen.periodoMesE = null;
            //            resumen.TotalVentaMes = null;
            //            resumen.totalMes = null;
            //        }

            //        List<gsReporteCanceladosResumenMes_actualResult> list = new List<gsReporteCanceladosResumenMes_actualResult>();
            //        list.Add(resumen);

            //        rdr.Close();
            //        con.Close();

            //        //List<gsReporteCanceladosResumenMes_actualResult> list = dcg.gsReporteCanceladosResumenMes_actual(codAgenda, codVendedor, year, mes).ToList();
            //        return list;
            //    }
            //    catch (Exception ex)
            //    {
            //        dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
            //        dci.SubmitChanges();
            //        throw new ArgumentException("Error al momento de consultar las cobranzas en la base de datos.");
            //    }
            //    finally
            //    {

            //    }
            //}
        }
        public List<gsProyectadoCobranza_ListarResult> Reporte_Proyectado_Sectorista(int idEmpresa, int codigoUsuario, int id_proyectado, int periodo,  string id_sectorista, string id_cliente, int id_zona, string id_vendedor)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                string proyectado;
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<gsProyectadoCobranza_ListarResult> list = new List<gsProyectadoCobranza_ListarResult>();

                    list = dcg.gsProyectadoCobranza_Listar(id_proyectado, periodo, id_sectorista, id_cliente, id_zona, id_vendedor).ToList();
                
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las proyeciones en la base de datos.");
                }
            }
        }
        public void Insertar_ProyectadoSectorista(int idEmpresa, int codigoUsuario, int id_proyectado, string periodo, string id_sectorista, string id_cliente, decimal montoS1, decimal montoS2, decimal montoS3, decimal montoS4, decimal proyectado, int estado )
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.gsProyectadoCobranza_create(id_proyectado, periodo, id_sectorista, id_cliente, montoS1, montoS2, montoS3, montoS4, proyectado, codigoUsuario, estado);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las proyeciones en la base de datos.");
                }
            }
        }
        public List<gsGestionCobranza_ListarResult> Reporte_Gestion_Sectorista(int idEmpresa, int codigoUsuario, string id_Cliente, int Periodo)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                   List<gsGestionCobranza_ListarResult> list = new List<gsGestionCobranza_ListarResult>();
                    list = dcg.gsGestionCobranza_Listar(id_Cliente, Periodo).ToList();

                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las proyeciones en la base de datos.");
                }
            }
        }

        public List<gsEstatus_ListarResult> Estatus_Deuda_Listar(int idEmpresa, int codigoUsuario)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<gsEstatus_ListarResult> list = new List<gsEstatus_ListarResult>();
                    list = dcg.gsEstatus_Listar().ToList();

                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las proyeciones en la base de datos.");
                }
            }
        }

        public List<gsReporteCobranzas_Poryectadas_VendedorResult> Reporte_CobranzasProyectadasVendedor(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
        
                    List<gsReporteCobranzas_Poryectadas_VendedorResult> list = dcg.gsReporteCobranzas_Poryectadas_Vendedor(mes, año, periodo, id_zona, id_sectorista).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las cobranzas en la base de datos.");
                }
            }
        }

        public List<gsReporteCobranzas_Poryectadas_Vendedor_DetalleResult> Reporte_CobranzasProyectadasVendedorDetalle(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
 
                try
                {
                  List<gsReporteCobranzas_Poryectadas_Vendedor_DetalleResult> list = dcg.gsReporteCobranzas_Poryectadas_Vendedor_Detalle(mes, año, periodo, id_zona, id_sectorista).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las cobranzas en la base de datos.");
                }
            }
        }

        public List<gsReporteCobranzas_Poryectadas_Vendedor_FechaResult> Reporte_CobranzasProyectadasVendedor_Fecha(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

  
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<gsReporteCobranzas_Poryectadas_Vendedor_FechaResult> list = dcg.gsReporteCobranzas_Poryectadas_Vendedor_Fecha(mes, año, periodo, id_zona, id_sectorista).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las cobranzas en la base de datos.");
                }
            }
        }

        public void ProyectadoCobranza_Registrar(int idEmpresa, int codigoUsuario, string codAgenda,  string codSectorista, string periodo, int id_zona, decimal montoS1, decimal montoS2, decimal montoS3, decimal montoS4, decimal proyectado  )
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
 
                try
                {
        
                    dcg.gsProyectadoCobranza_Registrar(codSectorista, codAgenda, periodo, id_zona, montoS1,montoS2,montoS3,montoS4, proyectado, codigoUsuario, 1);

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de registrar las cobranzas en la base de datos.");
                }
            }
        }

        public void GestionCobranza_Registrar(int idEmpresa, int codigoUsuario, string id_cliente, string periodo, int id_semana,
            int id_estatus, string observacion, int estado, string TablaOrigen, int opOrigen)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
 
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.gsGestionCobranza_insert(id_cliente, periodo, id_semana, id_estatus, observacion, codigoUsuario.ToString(), estado, TablaOrigen, opOrigen);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de registrar las cobranzas en la base de datos.");
                }
            }
        }


        public int ProyectadoCobranza_Verificar(int idEmpresa, int codigoUsuario, string codSectorista, string periodo, int id_zona)
        {
            int respuesta = 0;


            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    gsProyectado_verificarResult obj = new gsProyectado_verificarResult();

                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<gsProyectado_verificarResult> list = dcg.gsProyectado_verificar(codSectorista,periodo,id_zona).ToList();
                    obj = list[0];
                    respuesta = int.Parse(obj.valor.ToString()); 
                    return respuesta;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las cobranzas en la base de datos.");
                }
            }
        }

        public List<gsVentasCobranzas_ListarResult> Reporte_VentasCobranzasAnual(int idEmpresa, int codigoUsuario, int anho)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<gsVentasCobranzas_ListarResult> list = dcg.gsVentasCobranzas_Listar(anho).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las cobranzas en la base de datos.");
                }
            }
        }

        public List<gsReporteCancelados_ProyectadoResult> Reporte_CobranzasProyectadas_Sectorista(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
           
                    List<gsReporteCancelados_ProyectadoResult> list = dcg.gsReporteCancelados_Proyectado(mes, año, periodo, id_zona, id_sectorista).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las cobranzas en la base de datos.");
                }
            }
        }


        public List<Reporte_VentaxCobranzaLegalResult> Reporte_VentaCobranzaLegal(int idEmpresa, int codigoUsuario)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<Reporte_VentaxCobranzaLegalResult> list = dcg.Reporte_VentaxCobranzaLegal().ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las cobranzas en la base de datos.");
                }
            }
        }
        //Percy Santiago 10/11/2016
        public List<gsReporteFacturasInafectasV1Result> Reporte_FacturasInafecta(int idEmpresa, int codigoUsuario, System.DateTime FechaInicio, System.DateTime FechaFin, int TipoDocumento, string Cliente)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                  List<gsReporteFacturasInafectasV1Result> list = dcg.gsReporteFacturasInafectasV1(FechaInicio, FechaFin, TipoDocumento, Cliente).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las cobranzas en la base de datos.");
                }
            }
        }
        // Fin 10/11/2016


        public void ProyectarCobranza_Registrar(int idEmpresa, int codigoUsuario, int id_proyectado, string periodo, int id_semana, float Importe, string TablaOrigen, int OpOrigen, int estado)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    dcg.ProyectadoCobranza_Insertar(id_proyectado, periodo, id_semana, Importe, TablaOrigen, OpOrigen, codigoUsuario, estado);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de registrar las cobranzas en la base de datos.");
                }
            }
        }

        public List<ProyectadoCobranza_ListarResult> ProyectadoCobranza_Listar(int idEmpresa, int codigoUsuario, int idProyectado,
            string periodo, int id_semana, string tablaOrigen, int opOrigen, int estado)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                   List<ProyectadoCobranza_ListarResult> list = new List<ProyectadoCobranza_ListarResult>();
                    list = dcg.ProyectadoCobranza_Listar(idProyectado, periodo, id_semana, tablaOrigen, opOrigen, estado).ToList();

                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las proyeciones en la base de datos.");
                }
            }
        }

        //--------------------------------------------



        public List<ZonasSectorista_ListarResult> ZonasSectorista_Listar(int idEmpresa, int codigoUsuario, string texto, int reporte)
        {

            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    List<ZonasSectorista_ListarResult> lista = new List<ZonasSectorista_ListarResult>();
                    lista = dcg.ZonasSectorista_Listar(reporte).ToList();
                    return lista;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar en la base de datos. " + ex.Message.ToString());
                }
            }
        }

        public List<ZonasSectoristaPermiso_ListarResult> ZonasSectoristaPermiso_Listar(int idEmpresa, int codigoUsuario, string id_agenda)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                
                    List<ZonasSectoristaPermiso_ListarResult> lista = new List<ZonasSectoristaPermiso_ListarResult>();

                    lista = dcg.ZonasSectoristaPermiso_Listar(id_agenda).ToList();
                    return lista;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        public int PermisosZona_Registrar(int idEmpresa, int codigoUsuario, string id_agenda, int id_zona, int ActivoZona, int ActivoSectorista)
        {
            int idPermiso = 0;

            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    dcg.gsActualizar_Categoria(id_agenda, ActivoSectorista);
                }
                catch (Exception ex)
                {

                    throw new ArgumentException("No se puede registrar el NroDocumento:" + id_agenda);
                }
                finally
                {
                    dcg.SubmitChanges();
                }

                try
                {
                    PermisosZona_RegistrarResult lstidPermiso = new PermisosZona_RegistrarResult();
                    lstidPermiso = dci.PermisosZona_Registrar(id_agenda, id_zona, codigoUsuario, ActivoZona, idEmpresa).Single();
                    idPermiso = int.Parse(lstidPermiso.Column1.ToString());
                    return idPermiso;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException("No se puede registrar: dci.PermisosZona_Registrar ");
                }
                finally
                {
                    dci.SubmitChanges();
                }

            }
        }

        public List<spEstadoCuenta_ProyectadoResult> EstadoCuenta_Proyectado(int idEmpresa, int codigoUsuario, int periodo, string id_sectorista, int id_zona, int anho, int mes)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                string proyectado;
                try
                {
                   List<spEstadoCuenta_ProyectadoResult> list = new List<spEstadoCuenta_ProyectadoResult>();

                    list = dcg.spEstadoCuenta_Proyectado(periodo, anho, mes, id_sectorista, id_zona).ToList();

                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar spEstadoCuenta_Proyectado");
                }
            }
        }

        public List<spEstadoCuenta_Proyectado_ClienteResult> EstadoCuenta_Proyectado_Cliente(int idEmpresa, int codigoUsuario, string id_cliente, int periodo, string id_sectorista, int id_zona, int anho, int mes)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                string proyectado;
                try
                {

                    List<spEstadoCuenta_Proyectado_ClienteResult> list = new List<spEstadoCuenta_Proyectado_ClienteResult>();

                    list = dcg.spEstadoCuenta_Proyectado_Cliente(id_cliente, periodo, anho, mes, id_sectorista, id_zona).ToList();

                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar spEstadoCuenta_Proyectado_Cliente");
                }
            }
        }

        public void ProyectadoCobranza_Eliminar(int idEmpresa, int codigoUsuario, int id_proyectado)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                   
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.ProyectadoCobranza_Eliminar(id_proyectado);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de dcg.ProyectadoCobranza_Eliminar.");
                }
            }
        }


        public List<ProyectadoCobranza_DocumentosResult> ProyectadoCobranza_Documentos(int idEmpresa, int codigoUsuario, string id_Cliente, int Periodo)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                   //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<ProyectadoCobranza_DocumentosResult> list = new List<ProyectadoCobranza_DocumentosResult>();
                    list = dcg.ProyectadoCobranza_Documentos(Periodo, id_Cliente).ToList();

                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las proyeciones. dcg.ProyectadoCobranza_Documentos");
                }
            }
        }

        public List<gsReporteCobranzas_Poryectadas_ClienteResult> Reporte_CobranzasProyectadasCliente(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                  //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<gsReporteCobranzas_Poryectadas_ClienteResult> list = dcg.gsReporteCobranzas_Poryectadas_Cliente(mes, año, periodo, id_zona, id_sectorista).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las cobranzas en la base de datos.");
                }
            }
        }

        public List<gsReporteProyectado_Cuadro1Result> Reporte_CobranzasProyectadas_Cuadro1(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {

                    List<gsReporteProyectado_Cuadro1Result> list = new List<gsReporteProyectado_Cuadro1Result>();

                    list = dcg.gsReporteProyectado_Cuadro1(mes, año, periodo, id_zona, id_sectorista).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar WCF: gsReporteProyectado_Cuadro1");
                }
            }
        }

        public List<gsReporteProyectado_Cuadro2Result> Reporte_CobranzasProyectadas_Cuadro2(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    List<gsReporteProyectado_Cuadro2Result> list = new List<gsReporteProyectado_Cuadro2Result>();

                    list = dcg.gsReporteProyectado_Cuadro2(mes, año, periodo, id_zona, id_sectorista).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar WCF: gsReporteProyectado_Cuadro2");
                }
            }
        }

        public List<Zonas_Reportes_CobranzaResult> Zonas_Listar_Cobranza(int idEmpresa, int codigoUsuario, string id_sectorista)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                   return dcg.Zonas_Reportes_Cobranza(id_sectorista).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar en la base de datos(BL). " + ex.Message.ToString());
                }
            }
        }


    }
}
