using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using GS.SISGEGS.BL;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ReporteVentaWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ReporteVentaWCF.svc o ReporteVentaWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ReporteVentaWCF : IReporteVentaWCF
    {
        public List<gsDocVenta_ReporteVenta_VendedorV2Result> DocVenta_ReporteVenta_VendedorZona(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try {
                return objDocVentaBL.DocVenta_ReporteVenta_VendedorZona(idEmpresa, codigoUsuario, ID_Vendedor, fechaInicio, fechaFinal);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public List<gsDocVenta_ReporteVenta_VendedorResult> DocVenta_ReporteVenta_Vendedor(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.DocVenta_ReporteVenta_Vendedor(idEmpresa, codigoUsuario, ID_Vendedor, fechaInicio, fechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsDocVenta_ReporteVenta_ClienteResult> DocVenta_ReporteVenta_Cliente(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal, string ESTADO)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.DocVenta_ReporteVenta_Cliente(idEmpresa, codigoUsuario, ID_Vendedor, fechaInicio, fechaFinal, ESTADO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsDocVenta_ReporteVenta_ProductoResult> DocVenta_ReporteVenta_Producto(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.DocVenta_ReporteVenta_Producto(idEmpresa, codigoUsuario, ID_Vendedor, fechaInicio, fechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsDocVenta_ReporteVenta_FechaResult> DocVenta_ReporteVenta_Fecha(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.DocVenta_ReporteVenta_Fecha(idEmpresa, codigoUsuario, ID_Vendedor, fechaInicio, fechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsDocVenta_ReporteVenta_MarcaResult> DocVenta_ReporteVenta_Marca(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.DocVenta_ReporteVenta_Marca(idEmpresa, codigoUsuario, ID_Vendedor, fechaInicio, fechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsDocVenta_ReporteVenta_MarcaProductoResult> DocVenta_ReporteVenta_MarcaProducto(int idEmpresa, int codigoUsuario, int? ID_Marca, int? ID_Zona, string ID_Cliente, DateTime fechaInicio, DateTime fechaFinal)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.DocVenta_ReporteVenta_MarcaProducto(idEmpresa, codigoUsuario, ID_Marca, ID_Zona, ID_Cliente, fechaInicio, fechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsDocVenta_ConsultarVentaMarcaResult> DocVenta_ConsultarVentaMarca(int idEmpresa, int codigoUsuario, string ID_Cliente, DateTime fechaInicio, DateTime fechaFinal)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.DocVenta_ConsultarVentaMarca(idEmpresa, codigoUsuario, ID_Cliente, fechaInicio, fechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsDocVenta_ReporteVenta_MarcaClienteResult> DocVenta_ReporteVenta_MarcaCliente(int idEmpresa, int codigoUsuario, int? ID_Marca, DateTime fechaInicio, DateTime fechaFinal)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.DocVenta_ReporteVenta_MarcaCliente(idEmpresa, codigoUsuario, ID_Marca, fechaInicio, fechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsDocVenta_ReporteVenta_MarcaVendedorResult> DocVenta_ReporteVenta_MarcaVendedor(int idEmpresa, int codigoUsuario, int? ID_Marca, DateTime fechaInicio, DateTime fechaFinal)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.DocVenta_ReporteVenta_MarcaVendedor(idEmpresa, codigoUsuario, ID_Marca, fechaInicio, fechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsDocVenta_ConsultarVentaProductoResult> DocVenta_ConsultarVentaProducto(int idEmpresa, int codigoUsuario, string ID_Cliente, DateTime fechaInicio, DateTime fechaFinal)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.DocVenta_ConsultarVentaProducto(idEmpresa, codigoUsuario, ID_Cliente, fechaInicio, fechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporte_ProyeccionVentasParetoResult> gsReporte_ProyeccionVentasPareto(int idEmpresa, int codigoUsuario, int anhoAnterior, int AnhoActual, int mesInicio, int mesFinal, int id_zona)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.gsReporte_ProyeccionVentasPareto(idEmpresa, codigoUsuario, anhoAnterior, AnhoActual, mesInicio, mesFinal, id_zona);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ProyectadoVentas_Registrar(int idEmpresa, int codigoUsuario, int ID_Pronostico, DateTime Fecha, int Id_Item, decimal precio, decimal costo, int cantidad, decimal importe, bool aprobacion, int anho, int mes, int Id_Zona, string Id_Vendedor, string Id_Cliente, int moneda)
        {
            DocVentaBL objDocVentaBL; 
            try
            {
                objDocVentaBL = new DocVentaBL();
                objDocVentaBL.ProyectadoVentas_Registrar(idEmpresa, codigoUsuario, ID_Pronostico, Fecha, Id_Item, precio, costo, cantidad, importe, true, anho, mes, Id_Zona, Id_Vendedor, Id_Cliente, moneda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporteVentaPresupuesto_ProductoResult> gsReporte_PronosticoVentas(int idEmpresa, int codigoUsuario, int anho, int mes, int id_zona)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.gsReporte_PronosticoVentas(idEmpresa, codigoUsuario, anho, mes, id_zona);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsPronostico_Reporte_MarcaResult> Pronostico_Reporte_Marca(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.Pronostico_Reporte_Marca(idEmpresa, codigoUsuario,FechaDesde, FechaHasta, id_zona, id_vendedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsPronostico_Reporte_VendedorResult> Pronostico_Reporte_Vendedor(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.Pronostico_Reporte_Vendedor(idEmpresa, codigoUsuario, FechaDesde, FechaHasta, id_zona, id_vendedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ProyectadoVentas_RegistrarBulkCopy(int idEmpresa, int codigoUsuario, DateTime Fecha, int Id_Zona, string Id_Vendedor, string Id_Cliente, int moneda, DataTable tabla)
        {
            DocVentaBL objDocVentaBL;
            try
            {
                objDocVentaBL = new DocVentaBL();
                objDocVentaBL.ProyectadoVentas_RegistrarBulkCopy(idEmpresa, codigoUsuario, Fecha,Id_Zona, Id_Vendedor, Id_Cliente, moneda, tabla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PresupuestadoVentas_RegistrarBulkCopy(int idEmpresa, int codigoUsuario, DateTime Fecha, int Id_Zona, string Id_Vendedor, string Id_Cliente, int moneda, DataTable tabla)
        {
            DocVentaBL objDocVentaBL;
            try
            {
                objDocVentaBL = new DocVentaBL();
                objDocVentaBL.PresupuestoVentas_RegistrarBulkCopy(idEmpresa, codigoUsuario, Fecha, Id_Zona, Id_Vendedor, Id_Cliente, moneda, tabla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsPronostico_Reporte_Planeamiento_MarcaResult> Pronostico_Planeamiento_Marca(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor) {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.Pronostico_Planeamiento_Marca(idEmpresa, codigoUsuario, FechaDesde, FechaHasta, id_zona, id_vendedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporte_PresupuestoVentasParetoResult> gsReporte_PresupuestoVentasPareto(int idEmpresa, int codigoUsuario, int anho1, int anho2, int mes1, int mes2, int id_zona, string id_vendedor, bool flag_Semillas)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.gsReporte_PresupuestoVentasPareto(idEmpresa, codigoUsuario, anho1, anho2, mes1, mes2, id_zona, id_vendedor, flag_Semillas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporteVentaPresupuesto_Producto2017Result> gsReporteVentaPresupuesto_Producto2017(int idEmpresa, int codigoUsuario, int anho, int mes, string id_vendedor)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.gsReporteVentaPresupuesto_Producto2017(idEmpresa, codigoUsuario, anho, mes, id_vendedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsPronostico_vs_RealResult> gsPronostico_vs_Real(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor) {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.gsPronostico_vs_Real(idEmpresa, codigoUsuario, FechaDesde, FechaHasta, id_zona, id_vendedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsDocVenta_ReporteVenta_Marca_InatecResult> DocVenta_ReporteVenta_MarcaInatec(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.DocVenta_ReporteVenta_MarcaInatec(idEmpresa, codigoUsuario, ID_Vendedor, fechaInicio, fechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsDocVenta_ReporteVenta_MarcaProducto_InatecResult> DocVenta_ReporteVenta_MarcaProductoInatec(int idEmpresa, int codigoUsuario, int? ID_Marca, int? ID_Zona, string ID_Cliente, DateTime fechaInicio, DateTime fechaFinal)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.DocVenta_ReporteVenta_MarcaProductoInatec(idEmpresa, codigoUsuario, ID_Marca, ID_Zona, ID_Cliente, fechaInicio, fechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsPresupuesto_ZonaResult> Presupuesto_Zona(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            {
                try
                {
                    return objDocVentaBL.Presupuesto_Zona(idEmpresa, codigoUsuario, FechaDesde, FechaHasta, id_zona, id_vendedor);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<gsDocVenta_ReporteVenta_MarcaCliente_InatecResult> DocVenta_ReporteVenta_MarcaClienteInatec(int idEmpresa, int codigoUsuario, int? ID_Marca, DateTime fechaInicio, DateTime fechaFinal)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.DocVenta_ReporteVenta_MarcaClienteInatec(idEmpresa, codigoUsuario, ID_Marca, fechaInicio, fechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsDocVenta_ReporteVenta_MarcaVendedor_InatecResult> DocVenta_ReporteVenta_MarcaVendedorInatec(int idEmpresa, int codigoUsuario, int? ID_Marca, DateTime fechaInicio, DateTime fechaFinal)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.DocVenta_ReporteVenta_MarcaVendedorInatec(idEmpresa, codigoUsuario, ID_Marca, fechaInicio, fechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<gsPresupuesto_Reporte_MarcaResult> Presupuesto_Reporte_Marca(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            {
                try
                {
                    return objDocVentaBL.Presupuesto_Reporte_Marca(idEmpresa, codigoUsuario, FechaDesde, FechaHasta, id_zona, id_vendedor);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<gsVentaPronostico_vs_RealResult> gsVentaPronostico_vs_Real(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.gsVentaPronostico_vs_Real(idEmpresa, codigoUsuario, FechaDesde, FechaHasta, id_zona, id_vendedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------------------------------------------------

        public List<ReporteVentas_FamiliaResult> ReporteVentas_Familia(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.ReporteVentas_Familia(idEmpresa, codigoUsuario, FechaDesde, FechaHasta, id_zona, id_vendedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReporteVentas_ItemsResult> ReporteVentas_Items(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.ReporteVentas_Items(idEmpresa, codigoUsuario, FechaDesde, FechaHasta, id_zona, id_vendedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReporteVentas_ZonasResult> ReporteVentas_Zonas(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.ReporteVentas_Zonas(idEmpresa, codigoUsuario, FechaDesde, FechaHasta, id_zona, id_vendedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReportesIntranet_ListarResult> ReportesIntranet_Lista(int idUsuario)
        {
            DocVentaBL objDocVentaBL;

            try
            {
                List<ReportesIntranet_ListarResult> list = new List<ReportesIntranet_ListarResult>();
                objDocVentaBL = new DocVentaBL();
                list = objDocVentaBL.ReportesIntranet_Lista(idUsuario);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PermisosUsuarios_ListarResult> PermisosUsuarios_ListarResult(int idEmpresa, int codigoUsuario, string texto, int reporte)
        {
            DocVentaBL objDocVentaBL;

            try
            {
                List<PermisosUsuarios_ListarResult> list = new List<PermisosUsuarios_ListarResult>();

                objDocVentaBL = new DocVentaBL();
                list = objDocVentaBL.PermisosUsuarios_ListarResult(idEmpresa, codigoUsuario, texto, reporte);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ZonasPermisos_ListarResult> ZonaPersonal_Listar(int idEmpresa, int codigoUsuario, int id_Permiso)
        {
            DocVentaBL objDocVentaBL;

            try
            {
                List<ZonasPermisos_ListarResult> list = new List<ZonasPermisos_ListarResult>();

                objDocVentaBL = new DocVentaBL();
                list = objDocVentaBL.ZonaPersonal_Listar(idEmpresa, codigoUsuario, id_Permiso);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Zona_Registrar(int idEmpresa, int codigoUsuario, int idPermiso, int id_zona, int estado)
        {
            DocVentaBL objDocVentaBL;

            try
            {
                objDocVentaBL = new DocVentaBL();
                objDocVentaBL.Zona_Registrar(idEmpresa, codigoUsuario, idPermiso, id_zona, estado);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Zonas_ReportesResult> Zonas_Listar(int idEmpresa, int codigoUsuario, int id_zona)
        {
            DocVentaBL objDocVentaBL;

            try
            {
                List<Zonas_ReportesResult> lstZonas = new List<Zonas_ReportesResult>();
                objDocVentaBL = new DocVentaBL();
                lstZonas = objDocVentaBL.Zonas_Listar(idEmpresa, codigoUsuario, id_zona);

                return lstZonas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UsuarioReporte_ListarResult> UsuarioReporte_Listar(int idEmpresa, int codigoUsuario, int idUsuario, string texto)
        {
            DocVentaBL objDocVentaBL;

            try
            {
                List<UsuarioReporte_ListarResult> lstZonas = new List<UsuarioReporte_ListarResult>();
                objDocVentaBL = new DocVentaBL();
                lstZonas = objDocVentaBL.UsuarioReporte_Listar(idEmpresa, codigoUsuario, idUsuario, texto);

                return lstZonas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ZonasReporte_ListarResult> ZonasReporte_Listar(int idEmpresa, int codigoUsuario, int idUsuario, int idReporte)
        {
            DocVentaBL objDocVentaBL;

            try
            {
                List<ZonasReporte_ListarResult> lstZonas = new List<ZonasReporte_ListarResult>();
                objDocVentaBL = new DocVentaBL();
                lstZonas = objDocVentaBL.ZonasReporte_Listar(idEmpresa, codigoUsuario, idUsuario, idReporte);

                return lstZonas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int PermisosReportes_Registrar(int idEmpresa, int codigoUsuario, int idUsuario, int idReporte, int Activo)
        {
            DocVentaBL objDocVentaBL;
            int idPermiso = 0;
            try
            {
                objDocVentaBL = new DocVentaBL();
                idPermiso = objDocVentaBL.PermisosReportes_Registrar(idEmpresa, codigoUsuario, idUsuario, idReporte, Activo);

                return idPermiso;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReporteVentas_R3_1Result> ReporteVentas_R3_1(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.ReporteVentas_R3_1(idEmpresa, codigoUsuario, FechaDesde, FechaHasta, id_zona, id_vendedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReporteVentas_EstadoResultadosResult> ReporteVentas_Resultados(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, string Nombre_Zona, string NombreUnidad)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                List<ReporteVentas_EstadoResultadosResult> lista = new List<ReporteVentas_EstadoResultadosResult>();

                lista = objDocVentaBL.ReporteVentas_Resultados(idEmpresa, codigoUsuario, FechaDesde, FechaHasta, Nombre_Zona, NombreUnidad);

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Listar_Zona_BIResult> Listar_Zona_BI(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, string Nombre_Zona)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                List<Listar_Zona_BIResult> lista = new List<Listar_Zona_BIResult>();

                lista = objDocVentaBL.Listar_Zona_BI(idEmpresa, codigoUsuario, FechaDesde, FechaHasta, Nombre_Zona);

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Listar_Unidad_BIResult> Listar_Unidad_BI(int idEmpresa, int codigoUsuario)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                List<Listar_Unidad_BIResult> lista = new List<Listar_Unidad_BIResult>();

                lista = objDocVentaBL.Listar_Unidad_BI(idEmpresa, codigoUsuario);

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
