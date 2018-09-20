using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GS.SISGEGS.DM;
using GS.SISGEGS.BL;
using System.Data; 

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IEstadoCuentaWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IEstadoCuentaWCF
    {

        [OperationContract]
        List<gsReporte_DocumentosPendientesResult> EstadoCuenta_ListarxCliente(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos, int id_Zona);

        [OperationContract]
        List<gsAgendaCliente_BuscarLimiteCreditoResult> EstadoCuenta_LimiteCreditoxCliente(int idEmpresa, int codigoUsuario, string codAgenda, int Todos);

        [OperationContract]
        List<gsReporte_DocumentosPendientesResumenClienteResult> EstadoCuenta_ListarResumenCliente(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos);

        [OperationContract]
        List<gsReporte_DocumentosPendientesResumenVendedorResult> EstadoCuenta_ListarResumenVendedor(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos);
        [OperationContract]
        List<gsReporte_DeudaVencidaTotalResult> EstadoCuenta_ListarResumenTotal(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos);
        [OperationContract]
        List<gsReporteVentasxDiasCobranzaResult> EstadoCuenta_VentasDiasCobranza(int idEmpresa, int codigoUsuario, string id_agenda, string id_vendedor, int anho, int mes);
        [OperationContract]
        List<gsReporteVencidosPorMesResult> EstadoCuenta_VencidosMes(int idEmpresa, int codigoUsuario, DateTime fecha);
        [OperationContract]
        List<gsReporteVencidosPorMesClienteResult> EstadoCuenta_VencidosMesCliente(int idEmpresa, int codigoUsuario, DateTime fecha);
        [OperationContract]
        List<gsReporteVencidosPorMesDetalleResult> EstadoCuenta_VencidosMesDetalle(int idEmpresa, int codigoUsuario, string id_agenda, string id_vendedor, DateTime fecha);
        [OperationContract]
        List<gsReporte_DocumentosPendientes_ProyectadoResult> EstadoCuenta_ListarxCliente_Proyectado(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos);
        [OperationContract]
        List<gsEstadoCuenta_MesBIResult> EstadoCuenta_ListarxBI(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fecha);
        [OperationContract]
        List<gsReporte_DocumentosPendientesBIResult> EstadoCuenta_ListarxClienteBI(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos);
        [OperationContract]
        List<VBG00062_PROVEEDORResult> EstadoCuenta_ReporteProveedor(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal);
        [OperationContract]
        List<gsEstadoCuenta_TotalResult> EstadoCuenta_ClienteBI(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaFinal);

        [OperationContract]
        List<GS_GetCicloLetrasResult> GetCicloLetras(int idEmpresa, int codigoUsuario, decimal opFinan);

        [OperationContract]
        List<GS_LetrasGraficoPieResult> EstadoCuenta_GraficoPie(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos);

        [OperationContract]
        List<GS_GetLetrasPorEstadoResult> EstadoCuenta_LetrasPorEstados(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos);

        [OperationContract]
        List<GS_GetLetrasVencPorPlazoResult> EstadoCuenta_LetrasVencPorPlazo(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal);

        [OperationContract]
        List<GS_GetFacturasVencPorPlazoResult> EstadoCuenta_FacturasVencPorPlazo(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal);

        [OperationContract]
        List<gsReporte_DocumentosPendientes_LegalResult> EstadoCuenta_Listar_Legal(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos);
        [OperationContract]
        List<gsReporte_DocumentosPendientes_ProvisionResult> EstadoCuenta_Listar_Provision(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos);

        [OperationContract]
        List<GS_ReporteEstadoCuentaMngResult> EstadoCuenta_ListarAprobacion(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos);

        [OperationContract]
        void RiesgoCliente_RegistrarBulkCopy(int idEmpresa, int codigoUsuario, DataTable tabla);
    }
}
