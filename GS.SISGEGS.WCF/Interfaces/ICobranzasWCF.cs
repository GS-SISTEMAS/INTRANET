using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ICobranzasWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ICobranzasWCF
    {
        // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ICobranzas" en el código y en el archivo de configuración a la vez.
        [OperationContract]
        List<gsReporteCanceladosWebResult> Reporte_Cancelaciones(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaInicial, DateTime fechaFinal);
        [OperationContract]
        List<gsDocVenta_ReporteVenta_ClienteResult> Reporte_VentasCliente(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaInicial, DateTime fechaFinal, string ESTADO);
        [OperationContract]
        List<gsDocVenta_ReporteVenta_ClienteResumenResult> Reporte_VentasClienteResumen(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaInicial, DateTime fechaFinal, int year);
        [OperationContract]
        List<gsReporteCanceladosWebResumenResult> Reporte_CancelacionesResumen(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, int year);
        [OperationContract]
        List<gsReporteCanceladosWebResumenVendedorResult> Reporte_CancelacionesResumenVendedor(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaInicial, DateTime fechaFinal);
        [OperationContract]
        List<gsReporteCanceladosResumenMes_actualResult> Reporte_CancelacionesResumenActual(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, int year, int mes);

        [OperationContract]
        List<gsReporteCobranzaWeb_DetalleMesResult> Reporte_CancelacionesResumenDetalleMes(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, int year, int mes);
        [OperationContract]
        List<gsReporteCanceladosResumenMes_actualResult> Reporte_CancelacionesResumenActual_v2(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, int year, int mes);
        [OperationContract]
        List<gsReporteCanceladosResumenMes_v3Result> Reporte_CancelacionesResumenActual_v3(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, int year, int mes);
        [OperationContract]
        List<gsProyectadoCobranza_ListarResult> Reporte_Proyectado_Sectorista(int idEmpresa, int codigoUsuario, int id_proyectado, int periodo, string id_sectorista, string id_cliente, int id_zona, string id_vendedor);
        [OperationContract]
        void Insertar_ProyectadoSectorista(int idEmpresa, int codigoUsuario, int id_proyectado, string periodo, string id_sectorista, string id_cliente, decimal montoS1, decimal montoS2, decimal montoS3, decimal montoS4, decimal proyectado, int estado);
        [OperationContract]
        List<gsGestionCobranza_ListarResult> Reporte_Gestion_Sectorista(int idEmpresa, int codigoUsuario, string id_Cliente, int Periodo);

        [OperationContract]
        List<gsEstatus_ListarResult> Estatus_Deuda_Listar(int idEmpresa, int codigoUsuario);

        [OperationContract]
        List<gsReporteCobranzas_Poryectadas_VendedorResult> Reporte_CobranzasProyectadasVendedor(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista);
        [OperationContract]
        List<gsReporteCobranzas_Poryectadas_Vendedor_DetalleResult> Reporte_CobranzasProyectadasVendedorDetalle(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista);

        [OperationContract]
        List<gsReporteCobranzas_Poryectadas_Vendedor_FechaResult> Reporte_CobranzasProyectadasVendedor_Fecha(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista);


        [OperationContract]
        void ProyectadoCobranza_Registrar(int idEmpresa, int codigoUsuario, string codAgenda, string codSectorista, string periodo, int id_zona, decimal montoS1, decimal montoS2, decimal montoS3, decimal montoS4, decimal proyectado);

        [OperationContract]
        void GestionCobranza_Registrar(int idEmpresa, int codigoUsuario, string id_cliente, string periodo, int id_semana, int id_estatus, string observacion, int estado, string TablaOrigen, int opOrigen);


        [OperationContract]
        int ProyectadoCobranza_Verificar(int idEmpresa, int codigoUsuario, string codSectorista, string periodo, int id_zona);
        [OperationContract]
        List<gsVentasCobranzas_ListarResult> Reporte_VentasCobranzasAnual(int idEmpresa, int codigoUsuario, int anho);
        [OperationContract]
        List<gsReporteCancelados_ProyectadoResult> Reporte_CobranzasProyectadas_Sectorista(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista);
        [OperationContract]
        List<Reporte_VentaxCobranzaLegalResult> Reporte_VentaCobranzaLegal(int idEmpresa, int codigoUsuario);
        [OperationContract]
        List<gsReporteFacturasInafectasV1Result> Reporte_FacturasInafecta(int idEmpresa, int codigoUsuario, System.DateTime FechaInicio, System.DateTime FechaFin, int tipodocumento, string Cliente);

        [OperationContract]
        void ProyectarCobranza_Registrar(int idEmpresa, int codigoUsuario, int id_proyectado, string periodo, int id_semana, float Importe, string TablaOrigen, int OpOrigen, int estado);

        [OperationContract]
        List<ProyectadoCobranza_ListarResult> ProyectadoCobranza_Listar(int idEmpresa, int codigoUsuario, int idProyectado,
             string periodo, int id_semana, string tablaOrigen, int opOrigen, int estado);

        //---------------------------------------


        [OperationContract]
        List<ZonasSectorista_ListarResult> ZonasSectorista_Listar(int idEmpresa, int codigoUsuario, string texto, int reporte);
        [OperationContract]
        List<ZonasSectoristaPermiso_ListarResult> ZonasSectoristaPermiso_Listar(int idEmpresa, int codigoUsuario, string id_agenda);
        [OperationContract]
        int PermisosZona_Registrar(int idEmpresa, int codigoUsuario, string id_agenda, int id_zona, int ActivoZona, int ActivoSectorista);
        [OperationContract]
        List<spEstadoCuenta_ProyectadoResult> EstadoCuenta_Proyectado(int idEmpresa, int codigoUsuario, int periodo, string id_sectorista, int id_zona, int anho, int mes);
        [OperationContract]
        List<spEstadoCuenta_Proyectado_ClienteResult> EstadoCuenta_Proyectado_Cliente(int idEmpresa, int codigoUsuario, string id_cliente, int periodo, string id_sectorista, int id_zona, int anho, int mes);
        [OperationContract]
        void ProyectadoCobranza_Eliminar(int idEmpresa, int codigoUsuario, int id_proyectado);
        [OperationContract]
        List<ProyectadoCobranza_DocumentosResult> ProyectadoCobranza_Documentos(int idEmpresa, int codigoUsuario, string id_Cliente, int Periodo);

        [OperationContract]
        List<gsReporteCobranzas_Poryectadas_ClienteResult> Reporte_CobranzasProyectadasCliente(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista);

        [OperationContract]
        List<gsReporteProyectado_Cuadro1Result> Reporte_CobranzasProyectadas_Cuadro1(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista);

        [OperationContract]
        List<gsReporteProyectado_Cuadro2Result> Reporte_CobranzasProyectadas_Cuadro2(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista);

        [OperationContract]
        List<Zonas_Reportes_CobranzaResult> Zonas_Listar_Cobranza(int idEmpresa, int codigoUsuario, string id_sectorista); 
    }
}
