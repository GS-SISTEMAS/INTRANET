using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using GS.SISGEGS.DM;
using System.Configuration;
using System.Transactions;
using System.Data.Linq;

namespace GS.SISGEGS.BL
{
    public interface IDocVentaBL
    {
        List<gsDocVenta_ReporteVenta_VendedorV2Result> DocVenta_ReporteVenta_VendedorZona(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal);
        List<gsDocVenta_ReporteVenta_VendedorResult> DocVenta_ReporteVenta_Vendedor(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal);
        List<gsDocVenta_ReporteVenta_ClienteResult> DocVenta_ReporteVenta_Cliente(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal, string ESTADO);
        List<gsDocVenta_ReporteVenta_ProductoResult> DocVenta_ReporteVenta_Producto(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal);
        List<gsDocVenta_ReporteVenta_FechaResult> DocVenta_ReporteVenta_Fecha(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal);
        List<gsDocVenta_ReporteVenta_MarcaResult> DocVenta_ReporteVenta_Marca(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal);
        List<gsDocVenta_ReporteVenta_MarcaProductoResult> DocVenta_ReporteVenta_MarcaProducto(int idEmpresa, int codigoUsuario, int? ID_Marca, int? ID_Zona, string ID_Cliente, DateTime fechaInicio, DateTime fechaFinal);
        List<gsDocVenta_ConsultarVentaMarcaResult> DocVenta_ConsultarVentaMarca(int idEmpresa, int codigoUsuario, string ID_Cliente, DateTime fechaInicio, DateTime fechaFinal);
        List<gsDocVenta_ReporteVenta_MarcaClienteResult> DocVenta_ReporteVenta_MarcaCliente(int idEmpresa, int codigoUsuario, int? ID_Marca, DateTime fechaInicio, DateTime fechaFinal);
        List<gsDocVenta_ReporteVenta_MarcaVendedorResult> DocVenta_ReporteVenta_MarcaVendedor(int idEmpresa, int codigoUsuario, int? ID_Marca, DateTime fechaInicio, DateTime fechaFinal);
        List<gsDocVenta_ListarResult> DocVenta_Listar(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, string ID_Vendedor);
        gsDocVenta_BuscarResult DocVenta_Buscar(int idEmpresa, int codigoUsuario, decimal Op, ref List<gsDocVenta_BuscarDetalleResult> lstDetalle);
        List<gsDocVenta_ReporteVenta_MesResult> DocVenta_ReporteVenta_Mes(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal);
        List<gsDocVenta_ReporteVenta_ClienteTotalResult> DocVenta_ReporteVenta_ClienteTotal(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal);
        List<gsDocVenta_ReporteVenta_ProductoTotalResult> DocVenta_ReporteVenta_ProductoTotal(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal);
        List<gsDocVentaDev_ReporteDevMesResult> DocVentaDev_ReporteDevMes(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal);
        List<gsDocVentaDev_ReporteDevMotivoResult> DocVentaDev_ReporteDevMotivo(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal);
        List<gsDocVenta_ControlCosto_MarcaProductoResult> DocVenta_ControlCosto_MarcaProducto(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, int? kardex);
        List<gsDocVenta_ListarTransGratuitasResult> DocVenta_ListarTransGratuitas(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal);
        List<gsDocVenta_ConsultarVentaProductoResult> DocVenta_ConsultarVentaProducto(int idEmpresa, int codigoUsuario, string ID_Cliente, DateTime fechaInicio, DateTime fechaFinal);
        List<gsReporte_ProyeccionVentasParetoResult> gsReporte_ProyeccionVentasPareto(int idEmpresa, int codigoUsuario, int anhoAnterior, int AnhoActual, int mesInicio, int mesFinal, int id_zona);
        void ProyectadoVentas_Registrar(int idEmpresa, int codigoUsuario, int ID_Pronostico, DateTime Fecha, int Id_Item, decimal precio, decimal costo, int cantidad, decimal importe, bool aprobacion, int anho, int mes, int Id_Zona, string Id_Vendedor, string Id_Cliente, int moneda);
        List<gsReporteVentaPresupuesto_ProductoResult> gsReporte_PronosticoVentas(int idEmpresa, int codigoUsuario, int anho, int mes, int id_zona);
        List<gsPronostico_Reporte_MarcaResult> Pronostico_Reporte_Marca(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor);

        List<gsPronostico_Reporte_VendedorResult> Pronostico_Reporte_Vendedor(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor);

        void ProyectadoVentas_RegistrarBulkCopy(int idEmpresa, int codigoUsuario, DateTime Fecha, int Id_Zona, string Id_Vendedor, string Id_Cliente, int moneda, DataTable tabla);

        void PresupuestoVentas_RegistrarBulkCopy(int idEmpresa, int codigoUsuario, DateTime Fecha, int Id_Zona, string Id_Vendedor, string Id_Cliente, int moneda, DataTable tabla);

        List<gsPronostico_Reporte_Planeamiento_MarcaResult> Pronostico_Planeamiento_Marca(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor);

        List<gsReporte_PresupuestoVentasParetoResult> gsReporte_PresupuestoVentasPareto(int idEmpresa, int codigoUsuario, int anho1, int anho2, int mes1, int mes2, int id_zona, string id_vendedor, bool flag_semillas);
        List<gsReporteVentaPresupuesto_Producto2017Result> gsReporteVentaPresupuesto_Producto2017(int idEmpresa, int codigoUsuario, int anho, int mes, string id_vendedor);

        List<gsPronostico_vs_RealResult> gsPronostico_vs_Real(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor);

        List<gsDocVenta_ReporteVenta_Marca_InatecResult> DocVenta_ReporteVenta_MarcaInatec(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal);

        List<gsDocVenta_ReporteVenta_MarcaProducto_InatecResult> DocVenta_ReporteVenta_MarcaProductoInatec(int idEmpresa, int codigoUsuario, int? ID_Marca, int? ID_Zona, string ID_Cliente, DateTime fechaInicio, DateTime fechaFinal);

        List<gsPresupuesto_ZonaResult> Presupuesto_Zona(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor);

        List<gsDocVenta_ReporteVenta_MarcaCliente_InatecResult> DocVenta_ReporteVenta_MarcaClienteInatec(int idEmpresa, int codigoUsuario, int? ID_Marca, DateTime fechaInicio, DateTime fechaFinal);

        List<gsDocVenta_ReporteVenta_MarcaVendedor_InatecResult> DocVenta_ReporteVenta_MarcaVendedorInatec(int idEmpresa, int codigoUsuario, int? ID_Marca, DateTime fechaInicio, DateTime fechaFinal);

        List<gsPresupuesto_Reporte_MarcaResult> Presupuesto_Reporte_Marca(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor);

        List<gsVentaPronostico_vs_RealResult> gsVentaPronostico_vs_Real(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor);

        //--------------------------------------
        List<ReporteVentas_FamiliaResult> ReporteVentas_Familia(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor);

        List<ReporteVentas_ItemsResult> ReporteVentas_Items(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor);

        List<ReporteVentas_ZonasResult> ReporteVentas_Zonas(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor);

        List<ReportesIntranet_ListarResult> ReportesIntranet_Lista(int idUsuario);
        List<PermisosUsuarios_ListarResult> PermisosUsuarios_ListarResult(int idEmpresa, int codigoUsuario, string texto, int reporte);

        List<ZonasPermisos_ListarResult> ZonaPersonal_Listar(int idEmpresa, int codigoUsuario, int id_Permiso);

        List<Zonas_ReportesResult> Zonas_Listar(int idEmpresa, int codigoUsuario, int id_zona);

        List<UsuarioReporte_ListarResult> UsuarioReporte_Listar(int idEmpresa, int codigoUsuario, int idUsuario, string texto);

        List<ZonasReporte_ListarResult> ZonasReporte_Listar(int idEmpresa, int codigoUsuario, int idUsuario, int idReporte);

        int PermisosReportes_Registrar(int idEmpresa, int codigoUsuario, int idUsuario, int idReporte, int Activo);

        List<ReporteVentas_R3_1Result> ReporteVentas_R3_1(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor);

        List<ReporteVentas_EstadoResultadosResult> ReporteVentas_Resultados(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, string Nombre_Zona, string NombreUnidad);

        List<Listar_Zona_BIResult> Listar_Zona_BI(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, string Nombre_Zona);

        List<Listar_Unidad_BIResult> Listar_Unidad_BI(int idEmpresa, int codigoUsuario);

    }

    public class DocVentaBL : IDocVentaBL
    {
        public List<gsDocVenta_ReporteVenta_VendedorResult> DocVenta_ReporteVenta_Vendedor(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {

                List<gsDocVenta_ReporteVenta_VendedorResult> Lista = new List<gsDocVenta_ReporteVenta_VendedorResult>();
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    dcg.CommandTimeout = 500;
                    Lista = dcg.gsDocVenta_ReporteVenta_Vendedor(fechaInicio, fechaFinal, null, null, null, ID_Vendedor, null, 0, null, null, null, null, null, null, null, null, null,
                        null, null, false, false, true, false, false, false, false, true, true).ToList();

                    dcg.Connection.Close();
                    return Lista;

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta base de datos.");
                }
            }
        }

        public List<gsDocVenta_ReporteVenta_VendedorV2Result> DocVenta_ReporteVenta_VendedorZona(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {

                List<gsDocVenta_ReporteVenta_VendedorV2Result> Lista = new List<gsDocVenta_ReporteVenta_VendedorV2Result>();
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    dcg.CommandTimeout = 500;
                    Lista = dcg.gsDocVenta_ReporteVenta_VendedorV2(fechaInicio, fechaFinal, null, null, null, ID_Vendedor, null, 0, null, null, null, null, null, null, null, null, null,
                        null, null, false, false, true, false, false, false, false, true, true).ToList();

                    dcg.Connection.Close();
                    return Lista;

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta base de datos.");
                }
            }
        }


        public List<gsDocVenta_ReporteVenta_ClienteResult> DocVenta_ReporteVenta_Cliente(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal, string ESTADO)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsDocVenta_ReporteVenta_Cliente(fechaInicio, fechaFinal, null, null, null, ID_Vendedor, null, 0, null, null, null, null, null, null, null, null, null,
                        null, null, true, true, true, true, true, true, true, true, true, ESTADO).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta por cliente en la base de datos.");
                }
            }
        }
        public List<gsDocVenta_ReporteVenta_FechaResult> DocVenta_ReporteVenta_Fecha(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsDocVenta_ReporteVenta_Fecha(fechaInicio, fechaFinal, null, null, null, ID_Vendedor, null, 0, null, null, null, null, null, null, null, null, null,
                        null, null, true, true, true, true, true, true, true, true, true).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta por cliente en la base de datos.");
                }
            }
        }
        public List<gsDocVenta_ReporteVenta_ProductoResult> DocVenta_ReporteVenta_Producto(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsDocVenta_ReporteVenta_Producto(fechaInicio, fechaFinal, null, null, null, ID_Vendedor, null, 0, null, null, null, null, null, null, null, null, null,
                        null, null, true, true, true, true, true, true, true, true, true).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta por cliente en la base de datos.");
                }
            }
        }
        public List<gsDocVenta_ReporteVenta_MarcaResult> DocVenta_ReporteVenta_Marca(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsDocVenta_ReporteVenta_Marca(fechaInicio, fechaFinal, null, null, null, ID_Vendedor, null, 0, null, null, null, null, null, null, null, null, null,
                        null, null, true, true, true, true, true, true, true, true, true).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta por cliente en la base de datos.");
                }
            }
        }

        public List<gsDocVenta_ReporteVenta_MarcaProductoResult> DocVenta_ReporteVenta_MarcaProducto(int idEmpresa, int codigoUsuario, int? ID_Marca, int? ID_Zona, string ID_Cliente, DateTime fechaInicio, DateTime fechaFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsDocVenta_ReporteVenta_MarcaProducto(fechaInicio, fechaFinal, null, ID_Cliente, null, null, null, 0, null, null, null, null, null, null, null, null, null,
                        null, null, true, true, true, true, true, true, true, true, true, ID_Marca, ID_Zona).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta por cliente en la base de datos.");
                }
            }
        }

        public List<gsDocVenta_ConsultarVentaMarcaResult> DocVenta_ConsultarVentaMarca(int idEmpresa, int codigoUsuario, string ID_Cliente, DateTime fechaInicio, DateTime fechaFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsDocVenta_ConsultarVentaMarca(fechaInicio, fechaFinal, null, ID_Cliente, null, null, null, 0, null, null, null, null, null, null, null, null, null,
                        null, null, true, true, true, true, true, true, true, true, true, null).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta por cliente en la base de datos.");
                }
            }
        }

        public List<gsDocVenta_ReporteVenta_MarcaClienteResult> DocVenta_ReporteVenta_MarcaCliente(int idEmpresa, int codigoUsuario, int? ID_Marca, DateTime fechaInicio, DateTime fechaFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsDocVenta_ReporteVenta_MarcaCliente(fechaInicio, fechaFinal, null, null, null, null, null, 0, null, null, null, null, null, null, null, null, null,
                        null, null, true, true, true, true, true, true, true, true, true, ID_Marca).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta por cliente en la base de datos.");
                }
            }
        }

        public List<gsDocVenta_ReporteVenta_MarcaVendedorResult> DocVenta_ReporteVenta_MarcaVendedor(int idEmpresa, int codigoUsuario, int? ID_Marca, DateTime fechaInicio, DateTime fechaFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsDocVenta_ReporteVenta_MarcaVendedor(fechaInicio, fechaFinal, null, null, null, null, null, 0, null, null, null, null, null, null, null, null, null,
                        null, null, true, true, true, true, true, true, true, true, true, ID_Marca).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta por cliente en la base de datos.");
                }
            }
        }

        public List<gsDocVenta_ListarResult> DocVenta_Listar(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, string ID_Vendedor)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsDocVenta_Listar(null, fechaInicio, fechaFinal, null, ID_Vendedor, null, null, null, null, null).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar los documentos de venta.");
                }
            }
        }

        public gsDocVenta_BuscarResult DocVenta_Buscar(int idEmpresa, int codigoUsuario, decimal Op, ref List<gsDocVenta_BuscarDetalleResult> lstDetalle)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                decimal? estado = null;
                bool? bloqueado = null;
                string mensajeBloqueo = null;
                DateTime? fechaDocumento = null;
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    lstDetalle = dcg.gsDocVenta_BuscarDetalle(Op, ref fechaDocumento, ref estado, ref bloqueado, ref mensajeBloqueo).ToList();
                    return dcg.gsDocVenta_Buscar(Op).Single();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar los documentos de venta.");
                }
            }
        }

        public List<gsDocVenta_ReporteVenta_MesResult> DocVenta_ReporteVenta_Mes(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.CommandTimeout = 500;
                    return dcg.gsDocVenta_ReporteVenta_Mes(fechaInicio, fechaFinal, null, null, null, null, null, 0, null, null, null, null, null, null, null, null, null,
                        null, null, true, true, true, true, true, true, true, true, true).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta por cliente en la base de datos.");
                }
            }
        }

        public List<gsDocVenta_ReporteVenta_ClienteTotalResult> DocVenta_ReporteVenta_ClienteTotal(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsDocVenta_ReporteVenta_ClienteTotal(fechaInicio, fechaFinal, null, null, null, ID_Vendedor, null, 0, null, null, null, null, null, null, null, null, null,
                        null, null, true, true, true, true, true, true, true, true, true).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta por cliente en la base de datos.");
                }
            }
        }

        public List<gsDocVenta_ReporteVenta_ProductoTotalResult> DocVenta_ReporteVenta_ProductoTotal(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsDocVenta_ReporteVenta_ProductoTotal(fechaInicio, fechaFinal, null, null, null, ID_Vendedor, null, 0, null, null, null, null, null, null, null, null, null,
                        null, null, true, true, true, true, true, true, true, true, true).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta por cliente en la base de datos.");
                }
            }
        }

        public List<gsDocVentaDev_ReporteDevMesResult> DocVentaDev_ReporteDevMes(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsDocVentaDev_ReporteDevMes(null, fechaInicio, fechaFinal, null, ID_Vendedor, null, 0, null, null, null, null, null, null, null, null, null,
                        null, null).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta por cliente en la base de datos.");
                }
            }
        }

        public List<gsDocVentaDev_ReporteDevMotivoResult> DocVentaDev_ReporteDevMotivo(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsDocVentaDev_ReporteDevMotivo(null, fechaInicio, fechaFinal, null, ID_Vendedor, null, 0, null, null, null, null, null, null, null, null, null,
                        null, null).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta por cliente en la base de datos.");
                }
            }
        }

        public List<gsDocVenta_ControlCosto_MarcaProductoResult> DocVenta_ControlCosto_MarcaProducto(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, int? kardex)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsDocVenta_ControlCosto_MarcaProducto(fechaInicio, fechaFinal, null, null, null, null, null, 0, kardex, null, null, null, null, null, null, null, null, null,
                        null, true, true, true, true, true, true, true, true, true, null, null).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR EN EL SERVICIO: No se pudo listar los costos de los productos.");
                }
            }
        }

        public List<gsDocVenta_ListarTransGratuitasResult> DocVenta_ListarTransGratuitas(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsDocVenta_ListarTransGratuitas(fechaInicio, fechaFinal).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede listar las transferencias gratuitas");
                }
            }
        }

        public List<gsDocVenta_ConsultarVentaProductoResult> DocVenta_ConsultarVentaProducto(int idEmpresa, int codigoUsuario, string ID_Cliente, DateTime fechaInicio, DateTime fechaFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsDocVenta_ConsultarVentaProducto(fechaInicio, fechaFinal, null, ID_Cliente, null, null, null, 0, null, null, null, null, null, null, null, null, null,
                        null, null, true, true, true, true, true, true, true, true, true, null).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta por cliente en la base de datos.");
                }
            }
        }

        public List<gsReporte_ProyeccionVentasParetoResult> gsReporte_ProyeccionVentasPareto(int idEmpresa, int codigoUsuario, int anhoAnterior, int AnhoActual, int mesInicio, int mesFinal, int id_zona)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsReporte_ProyeccionVentasPareto(anhoAnterior, AnhoActual, mesInicio, mesFinal, id_zona).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de proyeción en la BD: " + ex.Message);
                }
            }
        }

        public void ProyectadoVentas_Registrar(int idEmpresa, int codigoUsuario, int ID_Pronostico, DateTime Fecha, int Id_Item, decimal precio, decimal costo, int cantidad, decimal importe, bool aprobacion, int anho, int mes, int Id_Zona, string Id_Vendedor, string Id_Cliente, int moneda)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dci.Connection.Open();
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.Connection.Open();
                    dcg.CommandTimeout = 60;
                    dcg.gsProyectadoVentas_Registrar(ID_Pronostico, Fecha, Id_Item, precio, costo, cantidad, importe, codigoUsuario, true, anho, mes, Id_Zona, Id_Vendedor, Id_Cliente, moneda);
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

        public List<gsReporteVentaPresupuesto_ProductoResult> gsReporte_PronosticoVentas(int idEmpresa, int codigoUsuario, int anho, int mes, int id_zona)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsReporteVentaPresupuesto_Producto(anho, mes, id_zona).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de proyeción de ventas en la base de datos.");
                }
            }
        }


        public List<gsPronostico_Reporte_MarcaResult> Pronostico_Reporte_Marca(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.CommandTimeout = 60;
                    return dcg.gsPronostico_Reporte_Marca(FechaDesde, FechaHasta, id_zona, id_vendedor).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de proyeción de ventas en la base de datos.");
                }
            }
        }

        public List<gsPronostico_Reporte_VendedorResult> Pronostico_Reporte_Vendedor(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.CommandTimeout = 60;
                    return dcg.gsPronostico_Reporte_Vendedor(FechaDesde, FechaHasta, id_zona, id_vendedor).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de proyeción de ventas en la base de datos.");
                }
            }
        }

        public void ProyectadoVentas_RegistrarBulkCopy(int idEmpresa, int codigoUsuario, DateTime Fecha, int Id_Zona, string Id_Vendedor, string Id_Cliente, int moneda, DataTable tabla)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                SqlConnection conn;
                try
                {
                    int Anho = 0;
                    Anho = Fecha.Year;

                    dci.Connection.Open();
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.Connection.Open();
                    dcg.CommandTimeout = 60;

                    conn = new SqlConnection(dcg.Connection.ConnectionString);

                    using (SqlConnection connection = conn)
                    {
                        connection.Open();
                        string commandText = "delete Doc_VentaProyectadoTEMP where ID_Zona = @ID_zona;";
                        SqlCommand command = new SqlCommand(commandText, connection);
                        command.Parameters.Add("@ID_zona", SqlDbType.Int);
                        command.Parameters["@ID_zona"].Value = Id_Zona;

                        command.ExecuteNonQuery();


                        // Create a table with some rows. 
                        DataTable newReporte = tabla;

                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                        {
                            bulkCopy.DestinationTableName = "dbo.Doc_VentaProyectadoTEMP";

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


                        // Cargar Tabla Pronostico
                        string commandText2 = "delete Doc_VentaProyectado where Año = @Anho and ID_Zona = @ID_Zona; ";
                        SqlCommand command2 = new SqlCommand(commandText2, connection);
                        command2.Parameters.Add("@ID_zona", SqlDbType.Int);
                        command2.Parameters["@ID_zona"].Value = Id_Zona;
                        command2.Parameters.Add("@Anho", SqlDbType.Int);
                        command2.Parameters["@Anho"].Value = Anho;
                        command2.ExecuteNonQuery();


                        // Cargar Tabla Pronostico
                        string commandText3 = "insert Doc_VentaProyectado "
                                              + " select Fecha, Item_ID, Precio, Costo, Cantidad, Importe, Aprobacion1, Aprobacion2, Aprobacion1Fecha, Aprobacion2Fecha,  "
                                               + "Aprobacion1CodUsuario, Aprobacion2CodUsuario, UsuarioAcceso, UsuarioAccesoFecha, Año, Trimestre, Mes, Semana, Dia, ID_Zona, ID_Vendedor, "
                                               + "ID_Cliente, ID_Moneda  from Doc_VentaProyectadoTEMP "
                                               + "where Año = @Anho and ID_Zona = @ID_Zona "
                                               + "; ";

                        SqlCommand command3 = new SqlCommand(commandText3, connection);
                        command3.Parameters.Add("@ID_zona", SqlDbType.Int);
                        command3.Parameters["@ID_zona"].Value = Id_Zona;
                        command3.Parameters.Add("@Anho", SqlDbType.Int);
                        command3.Parameters["@Anho"].Value = Anho;
                        command3.ExecuteNonQuery();



                        // string commandTextUpdate = "update Doc_VentaProyectado set Precio		= IsNull(p2.Precio,0), "
                        //                          + "Costo = IsNull(p2.Costo,0),  Cantidad	= IsNull(p2.Cantidad,0), "
                        //                          + "Importe		= IsNull(p2.Importe,0), Aprobacion2Fecha = GETDATE(), "
                        //                          + "UsuarioAccesoFecha = GETDATE()  from Doc_VentaProyectadoTEMP p2  "
                        //                          + "inner join Doc_VentaProyectado p1 on ( p2.Año = p1.Año and p2.Mes = p1.Mes and "
                        //                          + " p2.Item_ID = p1.Item_ID and p2.ID_Zona = p1.ID_Zona ) ";


                        //SqlCommand commandUpdate = new SqlCommand(commandTextUpdate, connection);
                        //int rowsAffectedUpdate = commandUpdate.ExecuteNonQuery();

                        // string commandTextInsert = "insert Doc_VentaProyectado  "
                        //  + "select temp.Fecha,temp.Item_ID,  IsNull(temp.Precio,0),IsNull(temp.Costo,0),IsNull(temp.Cantidad,0),IsNull(temp.Importe,0),  "
                        //  + "temp.Aprobacion1,temp.Aprobacion2,getdate(), getdate(),temp.Aprobacion1CodUsuario,temp.Aprobacion2CodUsuario,temp.UsuarioAcceso,getdate(),   "
                        //  + "year(temp.Fecha),Datepart(quarter,temp.Fecha),month(temp.Fecha),Datepart(Week,temp.Fecha),Day(temp.Fecha),   "
                        //  + "temp.ID_Zona,temp.ID_Vendedor,temp.ID_Cliente,IsNull(temp.ID_Moneda,0) from Doc_VentaProyectadoTEMP temp   "
                        //  + "left join Doc_VentaProyectado p on temp.Item_ID = p.Item_ID and temp.Año = p.Año and temp.Mes = p.Mes and temp.ID_Zona = p.ID_Zona "

                        //  + "where p.Item_ID is null and p.Año is null and p.Mes is null and p.ID_Zona is null "; 

                        // SqlCommand commandInsert = new SqlCommand(commandTextInsert, connection);
                        // int rowsAffectedInsert = commandInsert.ExecuteNonQuery();

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

        public void PresupuestoVentas_RegistrarBulkCopy(int idEmpresa, int codigoUsuario, DateTime Fecha, int Id_Zona, string Id_Vendedor, string Id_Cliente, int moneda, DataTable tabla)
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
                    dcg.CommandTimeout = 60;

                    conn = new SqlConnection(dcg.Connection.ConnectionString);

                    using (SqlConnection connection = conn)
                    {
                        connection.Open();
                        string commandText = "delete Doc_VentaPronosticoTemp where ID_Zona = @ID_zona and ID_Vendedor = @ID_vendedor;";
                        SqlCommand command = new SqlCommand(commandText, connection);
                        command.Parameters.Add("@ID_zona", SqlDbType.Int);
                        command.Parameters["@ID_zona"].Value = Id_Zona;

                        command.Parameters.Add("@ID_vendedor", SqlDbType.VarChar);
                        command.Parameters["@ID_vendedor"].Value = Id_Vendedor;

                        command.ExecuteNonQuery();


                        // Create a table with some rows. 
                        DataTable newReporte = tabla;

                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                        {
                            bulkCopy.DestinationTableName = "dbo.Doc_VentaPronosticoTemp";

                            try
                            {
                                bulkCopy.WriteToServer(newReporte);
                            }
                            catch (Exception ex)
                            {
                                string mensaje = ex.Message + " - " + ex.TargetSite.Name;
                                throw new ArgumentException("Error al momento de registrar el presupuesto masivo en la base de datos.");
                            }
                        }


                        string commandTextUpdate = "update Doc_VentaPronostico set Precio = IsNull(p2.Precio,0), "
                                                 + "Cantidad	= IsNull(p2.Cantidad,0), "
                                                 + "Importe		= IsNull(p2.Importe,0), Aprobacion2Fecha = GETDATE(),  "
                                                 + "UsuarioAccesoFecha = GETDATE()  from Doc_VentaPronosticoTEMP p2    "
                                                 + "inner join Doc_VentaPronostico p1 on ( p2.Año = p1.Año and p2.Mes = p1.Mes and "
                                                 + "p2.Item_ID = p1.Item_ID and p2.ID_Zona = p1.ID_Zona and p2.ID_Vendedor = p1.ID_Vendedor ) ";


                        SqlCommand commandUpdate = new SqlCommand(commandTextUpdate, connection);
                        int rowsAffectedUpdate = commandUpdate.ExecuteNonQuery();

                        string commandTextInsert = "insert Doc_VentaPronostico  "
                         + "select temp.Fecha,temp.Item_ID,  IsNull(temp.Cantidad, 0),IsNull(temp.Importe, 0), "
                         + "temp.Aprobacion1,temp.Aprobacion2,getdate(), getdate(),temp.Aprobacion1CodUsuario,temp.Aprobacion2CodUsuario,temp.UsuarioAcceso,getdate(),   "
                         + "year(temp.Fecha),Datepart(quarter, temp.Fecha),month(temp.Fecha),Datepart(Week, temp.Fecha),Day(temp.Fecha),   "
                         + " temp.ID_Vendedor,temp.ID_Cliente,IsNull(temp.ID_Moneda, 0), IsNull(temp.Precio, 0),  temp.ID_Zona "
                         + " from Doc_VentaPronosticoTEMP temp "
                         + " left join Doc_VentaPronostico p on temp.Item_ID = p.Item_ID and temp.Año = p.Año and temp.Mes = p.Mes and temp.ID_Zona = p.ID_Zona  "
                         + " and temp.ID_Vendedor = p.ID_Vendedor "
                         + " where p.Item_ID is null and p.Año is null and p.Mes is null and p.ID_Zona is null and p.ID_Vendedor is null ";

                        SqlCommand commandInsert = new SqlCommand(commandTextInsert, connection);
                        int rowsAffectedInsert = commandInsert.ExecuteNonQuery();

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

        public List<gsPronostico_Reporte_Planeamiento_MarcaResult> Pronostico_Planeamiento_Marca(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor)
        {

            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.CommandTimeout = 60;
                    return dcg.gsPronostico_Reporte_Planeamiento_Marca(FechaDesde, FechaHasta, id_zona, id_vendedor).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de pronostico de planeamiento de marcas en la base de datos.");
                }
            }
        }

        public List<gsReporte_PresupuestoVentasParetoResult> gsReporte_PresupuestoVentasPareto(int idEmpresa, int codigoUsuario, int anho1, int anho2, int mes1, int mes2, int id_zona, string id_vendedor, bool flag_semilla)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsReporte_PresupuestoVentasPareto(anho1, anho2, mes1, mes2, id_zona, id_vendedor, flag_semilla).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de pronostico de ventas en la base de datos.");
                }
            }
        }
        public List<gsReporteVentaPresupuesto_Producto2017Result> gsReporteVentaPresupuesto_Producto2017(int idEmpresa, int codigoUsuario, int anho, int mes, string id_vendedor)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsReporteVentaPresupuesto_Producto2017(anho, mes, id_vendedor).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de proyeción de ventas en la base de datos.");
                }
            }
        }

        public List<gsPronostico_vs_RealResult> gsPronostico_vs_Real(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.CommandTimeout = 60;
                    return dcg.gsPronostico_vs_Real(FechaDesde, FechaHasta, id_zona, id_vendedor).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de pronostico vs real en la base de datos.");
                }
            }
        }

        public List<gsDocVenta_ReporteVenta_Marca_InatecResult> DocVenta_ReporteVenta_MarcaInatec(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal)
        {
            List<gsDocVenta_ReporteVenta_Marca_InatecResult> lstReturn = new List<gsDocVenta_ReporteVenta_Marca_InatecResult>();

            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    lstReturn = dcg.gsDocVenta_ReporteVenta_Marca_Inatec(fechaInicio, fechaFinal, null, null, null, ID_Vendedor, null, 0, null, null, null, null, null, null, null, null, null,
                        null, null, true, true, true, true, true, true, true, true, true).ToList();

                    return lstReturn;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta por cliente en la base de datos.");
                }
            }
        }


        public List<gsDocVenta_ReporteVenta_MarcaProducto_InatecResult> DocVenta_ReporteVenta_MarcaProductoInatec(int idEmpresa, int codigoUsuario, int? ID_Marca, int? ID_Zona, string ID_Cliente, DateTime fechaInicio, DateTime fechaFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsDocVenta_ReporteVenta_MarcaProducto_Inatec(fechaInicio, fechaFinal, null, ID_Cliente, null, null, null, 0, null, null, null, null, null, null, null, null, null,
                        null, null, true, true, true, true, true, true, true, true, true, ID_Marca, ID_Zona).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta por cliente en la base de datos.");
                }
            }
        }

        public List<gsPresupuesto_ZonaResult> Presupuesto_Zona(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.CommandTimeout = 60;
                    return dcg.gsPresupuesto_Zona(FechaDesde, FechaHasta, id_zona, id_vendedor).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de presupuesto por zona en la base de datos.");
                }
            }
        }


        public List<gsDocVenta_ReporteVenta_MarcaCliente_InatecResult> DocVenta_ReporteVenta_MarcaClienteInatec(int idEmpresa, int codigoUsuario, int? ID_Marca, DateTime fechaInicio, DateTime fechaFinal)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsDocVenta_ReporteVenta_MarcaCliente_Inatec(fechaInicio, fechaFinal, null, null, null, null, null, 0, null, null, null, null, null, null, null, null, null,
                        null, null, true, true, true, true, true, true, true, true, true, ID_Marca).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta por cliente en la base de datos.");
                }
            }
        }

        public List<gsDocVenta_ReporteVenta_MarcaVendedor_InatecResult> DocVenta_ReporteVenta_MarcaVendedorInatec(int idEmpresa, int codigoUsuario, int? ID_Marca, DateTime fechaInicio, DateTime fechaFinal)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsDocVenta_ReporteVenta_MarcaVendedor_Inatec(fechaInicio, fechaFinal, null, null, null, null, null, 0, null, null, null, null, null, null, null, null, null,
                        null, null, true, true, true, true, true, true, true, true, true, ID_Marca).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta por cliente en la base de datos.");
                }
            }
        }

        public List<gsPresupuesto_Reporte_MarcaResult> Presupuesto_Reporte_Marca(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.CommandTimeout = 60;
                    return dcg.gsPresupuesto_Reporte_Marca(FechaDesde, FechaHasta, id_zona, id_vendedor).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de presupuesto por marca en la base de datos.");
                }
            }
        }

        public List<gsVentaPronostico_vs_RealResult> gsVentaPronostico_vs_Real(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.CommandTimeout = 60;
                    return dcg.gsVentaPronostico_vs_Real(FechaDesde, FechaHasta, id_zona, id_vendedor).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta pronostico vs real en la base de datos.");
                }
            }
        }


        //-------------------------------------------------------------

        public List<ReporteVentas_FamiliaResult> ReporteVentas_Familia(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    dcg.CommandTimeout = 60;
                    return dcg.ReporteVentas_Familia(FechaDesde, FechaHasta, codigoUsuario).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta pronostico vs real en la base de datos.");
                }
            }
        }

        public List<ReporteVentas_ItemsResult> ReporteVentas_Items(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));


                    dcg.CommandTimeout = 60;
                    return dcg.ReporteVentas_Items(FechaDesde, FechaHasta, codigoUsuario).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta pronostico vs real en la base de datos.");
                }
            }
        }

        public List<ReporteVentas_ZonasResult> ReporteVentas_Zonas(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.CommandTimeout = 60;
                    return dcg.ReporteVentas_Zonas(FechaDesde, FechaHasta, codigoUsuario).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta pronostico vs real en la base de datos.");
                }
            }
        }

        public List<ReportesIntranet_ListarResult> ReportesIntranet_Lista(int idUsuario)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    return dci.ReportesIntranet_Listar(idUsuario).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar en la base de datos(BL). " + ex.Message.ToString());
                }

        }

        public List<PermisosUsuarios_ListarResult> PermisosUsuarios_ListarResult(int idEmpresa, int codigoUsuario, string texto, int reporte)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dci.PermisosUsuarios_Listar(texto, reporte).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar en la base de datos. " + ex.Message.ToString());
                }
            }
        }

        public List<ZonasPermisos_ListarResult> ZonaPersonal_Listar(int idEmpresa, int codigoUsuario, int id_Permiso)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.ZonasPermisos_Listar(id_Permiso).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar por las referencias de la sucursal en la base de datos.");
                }
            }
        }


        public void Zona_Registrar(int idEmpresa, int codigoUsuario, int idPermiso, int id_zona, int estado)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            using (TransactionScope scope = new TransactionScope())
            {

                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    dci.ZonaPermisos_Insertar(idPermiso, id_zona, codigoUsuario, estado);
                    dci.SubmitChanges();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar en la base de datos(BL). " + ex.Message.ToString());
                }
            }
        }

        public List<Zonas_ReportesResult> Zonas_Listar(int idEmpresa, int codigoUsuario, int id_zona)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.Zonas_Reportes().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar en la base de datos(BL). " + ex.Message.ToString());
                }
            }
        }

        public List<UsuarioReporte_ListarResult> UsuarioReporte_Listar(int idEmpresa, int codigoUsuario, int idUsuario, string texto)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<UsuarioReporte_ListarResult> list = dci.UsuarioReporte_Listar(texto, idUsuario, idEmpresa).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar en la base de datos(BL). " + ex.Message.ToString());
                }
            }
        }

        public List<ZonasReporte_ListarResult> ZonasReporte_Listar(int idEmpresa, int codigoUsuario, int idUsuario, int idReporte)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<ZonasReporte_ListarResult> list = dcg.ZonasReporte_Listar(idEmpresa, idUsuario, idReporte).ToList();
                    return list;
                }
                catch (ChangeConflictException ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException("No se puede listar los perfiles del habilitados para el dcg.ZonasReporte_Listar");
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }

        public int PermisosReportes_Registrar(int idEmpresa, int codigoUsuario, int idUsuario, int idReporte, int Activo)
        {
            int idPermiso = 0;

            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    PermisosReportes_RegistrarResult lstidPermiso = new PermisosReportes_RegistrarResult();

                    lstidPermiso = dci.PermisosReportes_Registrar(idUsuario, idReporte, codigoUsuario, Activo).Single();

                    idPermiso = int.Parse(lstidPermiso.Column1.ToString());

                    return idPermiso;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException("No se puede listar los perfiles del habilitados para el dcg.ZonasReporte_Listar");
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }

        public List<ReporteVentas_R3_1Result> ReporteVentas_R3_1(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    dcg.CommandTimeout = 60;
                    return dcg.ReporteVentas_R3_1(FechaDesde, FechaHasta, codigoUsuario).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte de venta pronostico vs real en la base de datos ReporteVentas_R3_1.");
                }
            }
        }

        public List<ReporteVentas_EstadoResultadosResult> ReporteVentas_Resultados(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, string Nombre_Zona, string NombreUnidad)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));


                    dcg.CommandTimeout = 60;
                    return dcg.ReporteVentas_EstadoResultados(FechaDesde, FechaHasta, Nombre_Zona, NombreUnidad).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte en la base de datos. Reporte_R1");
                }
            }
        }

        public List<Listar_Zona_BIResult> Listar_Zona_BI(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, string Nombre_Zona)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    dcg.CommandTimeout = 60;
                    return dcg.Listar_Zona_BI().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte en la base de datos. Reporte_R1");
                }
            }
        }

        public List<Listar_Unidad_BIResult> Listar_Unidad_BI(int idEmpresa, int codigoUsuario)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    dcg.CommandTimeout = 60;
                    return dcg.Listar_Unidad_BI().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte en la base de datos. Reporte_R1");
                }
            }
        }
    }
}
