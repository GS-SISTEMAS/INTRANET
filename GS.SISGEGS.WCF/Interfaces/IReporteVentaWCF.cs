using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IReporteVentaWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IReporteVentaWCF
    {
        [OperationContract]
        List<gsDocVenta_ReporteVenta_VendedorV2Result> DocVenta_ReporteVenta_VendedorZona(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal);
        [OperationContract]
        List<gsDocVenta_ReporteVenta_VendedorResult> DocVenta_ReporteVenta_Vendedor(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal);
        [OperationContract]
        List<gsDocVenta_ReporteVenta_ClienteResult> DocVenta_ReporteVenta_Cliente(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal, string ESTADO);
        [OperationContract]
        List<gsDocVenta_ReporteVenta_ProductoResult> DocVenta_ReporteVenta_Producto(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal);
        [OperationContract]
        List<gsDocVenta_ReporteVenta_FechaResult> DocVenta_ReporteVenta_Fecha(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal);
        [OperationContract]
        List<gsDocVenta_ReporteVenta_MarcaResult> DocVenta_ReporteVenta_Marca(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal);
        [OperationContract]
        List<gsDocVenta_ReporteVenta_MarcaProductoResult> DocVenta_ReporteVenta_MarcaProducto(int idEmpresa, int codigoUsuario, int? ID_Marca, int? ID_Zona, string ID_Cliente, DateTime fechaInicio, DateTime fechaFinal);
        [OperationContract]
        List<gsDocVenta_ConsultarVentaMarcaResult> DocVenta_ConsultarVentaMarca(int idEmpresa, int codigoUsuario, string ID_Cliente, DateTime fechaInicio, DateTime fechaFinal);
        [OperationContract]
        List<gsDocVenta_ReporteVenta_MarcaClienteResult> DocVenta_ReporteVenta_MarcaCliente(int idEmpresa, int codigoUsuario, int? ID_Marca, DateTime fechaInicio, DateTime fechaFinal);
        [OperationContract]
        List<gsDocVenta_ReporteVenta_MarcaVendedorResult> DocVenta_ReporteVenta_MarcaVendedor(int idEmpresa, int codigoUsuario, int? ID_Marca, DateTime fechaInicio, DateTime fechaFinal);
        [OperationContract]
        List<gsDocVenta_ConsultarVentaProductoResult> DocVenta_ConsultarVentaProducto(int idEmpresa, int codigoUsuario, string ID_Cliente, DateTime fechaInicio, DateTime fechaFinal);
        [OperationContract]
        List<gsReporte_ProyeccionVentasParetoResult> gsReporte_ProyeccionVentasPareto(int idEmpresa, int codigoUsuario, int anhoAnterior, int AnhoActual, int mesInicio, int mesFinal, int id_zona); 

        [OperationContract]
        void ProyectadoVentas_Registrar(int idEmpresa, int codigoUsuario, int ID_Pronostico, DateTime Fecha, int Id_Item, decimal precio, decimal costo, int cantidad, decimal importe, bool aprobacion, int anho, int mes, int Id_Zona, string Id_Vendedor, string Id_Cliente, int moneda);

        [OperationContract]
        List<gsReporteVentaPresupuesto_ProductoResult> gsReporte_PronosticoVentas(int idEmpresa, int codigoUsuario, int anho, int mes, int id_zona);
        [OperationContract]
        List<gsPronostico_Reporte_MarcaResult> Pronostico_Reporte_Marca(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor);
        [OperationContract]
        List<gsPronostico_Reporte_VendedorResult> Pronostico_Reporte_Vendedor(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor);
        [OperationContract]
        void ProyectadoVentas_RegistrarBulkCopy(int idEmpresa, int codigoUsuario, DateTime Fecha, int Id_Zona, string Id_Vendedor, string Id_Cliente, int moneda, DataTable tabla);
        [OperationContract]
        List<gsPronostico_Reporte_Planeamiento_MarcaResult> Pronostico_Planeamiento_Marca(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor);

        [OperationContract]
        List<gsReporte_PresupuestoVentasParetoResult> gsReporte_PresupuestoVentasPareto(int idEmpresa, int codigoUsuario, int anho1, int anho2, int mes1, int mes2, int id_zona, string id_vendedor, bool flag_semillas);

        [OperationContract]
        List<gsReporteVentaPresupuesto_Producto2017Result> gsReporteVentaPresupuesto_Producto2017(int idEmpresa, int codigoUsuario, int anho, int mes, string id_vendedor);

        [OperationContract]
        void PresupuestadoVentas_RegistrarBulkCopy(int idEmpresa, int codigoUsuario, DateTime Fecha, int Id_Zona, string Id_Vendedor, string Id_Cliente, int moneda, DataTable tabla);

        [OperationContract]
        List<gsPronostico_vs_RealResult> gsPronostico_vs_Real(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor);

        [OperationContract]
        List<gsDocVenta_ReporteVenta_Marca_InatecResult> DocVenta_ReporteVenta_MarcaInatec(int idEmpresa, int codigoUsuario, string ID_Vendedor, DateTime fechaInicio, DateTime fechaFinal);
        [OperationContract]
        List<gsDocVenta_ReporteVenta_MarcaProducto_InatecResult> DocVenta_ReporteVenta_MarcaProductoInatec(int idEmpresa, int codigoUsuario, int? ID_Marca, int? ID_Zona, string ID_Cliente, DateTime fechaInicio, DateTime fechaFinal); 

        [OperationContract]
        List<gsPresupuesto_ZonaResult> Presupuesto_Zona(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor);

        [OperationContract]
        List<gsDocVenta_ReporteVenta_MarcaCliente_InatecResult> DocVenta_ReporteVenta_MarcaClienteInatec(int idEmpresa, int codigoUsuario, int? ID_Marca, DateTime fechaInicio, DateTime fechaFinal);
        [OperationContract]
        List<gsDocVenta_ReporteVenta_MarcaVendedor_InatecResult> DocVenta_ReporteVenta_MarcaVendedorInatec(int idEmpresa, int codigoUsuario, int? ID_Marca, DateTime fechaInicio, DateTime fechaFinal);
        [OperationContract]
        List<gsPresupuesto_Reporte_MarcaResult> Presupuesto_Reporte_Marca(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor);

        [OperationContract]
        List<gsVentaPronostico_vs_RealResult> gsVentaPronostico_vs_Real(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor);


        //----------------------------------------
        [OperationContract]
        List<ReporteVentas_FamiliaResult> ReporteVentas_Familia(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor);
        [OperationContract]
        List<ReporteVentas_ItemsResult> ReporteVentas_Items(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor);

        [OperationContract]
        List<ReporteVentas_ZonasResult> ReporteVentas_Zonas(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor);

        [OperationContract]
        List<ReportesIntranet_ListarResult> ReportesIntranet_Lista(int idUsuario);

        [OperationContract]
        List<PermisosUsuarios_ListarResult> PermisosUsuarios_ListarResult(int idEmpresa, int codigoUsuario, string texto, int reporte);

        [OperationContract]
        List<ZonasPermisos_ListarResult> ZonaPersonal_Listar(int idEmpresa, int codigoUsuario, int id_Permiso);

        [OperationContract]
        void Zona_Registrar(int idEmpresa, int codigoUsuario, int idPermiso, int id_zona, int estado);

        [OperationContract]
        List<Zonas_ReportesResult> Zonas_Listar(int idEmpresa, int codigoUsuario, int id_zona);

        [OperationContract]
        List<UsuarioReporte_ListarResult> UsuarioReporte_Listar(int idEmpresa, int codigoUsuario, int idUsuario, string texto);

        [OperationContract]
        List<ZonasReporte_ListarResult> ZonasReporte_Listar(int idEmpresa, int codigoUsuario, int idUsuario, int idReporte);

        [OperationContract]
        int PermisosReportes_Registrar(int idEmpresa, int codigoUsuario, int idUsuario, int idReporte, int Activo);

        [OperationContract]
        List<ReporteVentas_R3_1Result> ReporteVentas_R3_1(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int id_zona, string id_vendedor);

        [OperationContract]
        List<ReporteVentas_EstadoResultadosResult> ReporteVentas_Resultados(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, string Nombre_Zona, string NombreUnidad); 
        [OperationContract]
        List<Listar_Zona_BIResult> Listar_Zona_BI(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, string Nombre_Zona);

        [OperationContract]
        List<Listar_Unidad_BIResult> Listar_Unidad_BI(int idEmpresa, int codigoUsuario); 
    }
}
