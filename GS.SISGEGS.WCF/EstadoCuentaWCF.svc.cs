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


    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "EstadoCuentaWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione EstadoCuentaWCF.svc o EstadoCuentaWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class EstadoCuentaWCF : IEstadoCuentaWCF
    {
        public List<gsReporte_DocumentosPendientesResult> EstadoCuenta_ListarxCliente(int idEmpresa, int codigoUsuario,   string codAgenda, string codigoVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos, int id_zona)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                List<gsReporte_DocumentosPendientesResult> list = new List<gsReporte_DocumentosPendientesResult>() ;

                objEstadoCuentaBL = new EstadoCuentaBL();
                list = objEstadoCuentaBL.EstadoCuenta_ListarxCliente(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos, id_zona);

                return list;
                }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsAgendaCliente_BuscarLimiteCreditoResult> EstadoCuenta_LimiteCreditoxCliente(int idEmpresa, int codigoUsuario, string codAgenda, int Todos)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                List<gsAgendaCliente_BuscarLimiteCreditoResult> list = new List<gsAgendaCliente_BuscarLimiteCreditoResult>();

                objEstadoCuentaBL = new EstadoCuentaBL();
                list = objEstadoCuentaBL.EstadoCuenta_LimiteCreditoxCliente(idEmpresa, codigoUsuario, codAgenda, Todos);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<gsReporte_DocumentosPendientesResumenClienteResult> EstadoCuenta_ListarResumenCliente(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                List<gsReporte_DocumentosPendientesResumenClienteResult> list = new List<gsReporte_DocumentosPendientesResumenClienteResult>();

                objEstadoCuentaBL = new EstadoCuentaBL();
                list = objEstadoCuentaBL.EstadoCuenta_ListarResumenCliente(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<gsReporte_DocumentosPendientesResumenVendedorResult> EstadoCuenta_ListarResumenVendedor(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                List<gsReporte_DocumentosPendientesResumenVendedorResult> list = new List<gsReporte_DocumentosPendientesResumenVendedorResult>();

                objEstadoCuentaBL = new EstadoCuentaBL();
                list = objEstadoCuentaBL.EstadoCuenta_ListarResumenVendedor(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporte_DeudaVencidaTotalResult> EstadoCuenta_ListarResumenTotal(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                List<gsReporte_DeudaVencidaTotalResult> list = new List<gsReporte_DeudaVencidaTotalResult>();

                objEstadoCuentaBL = new EstadoCuentaBL();
                list = objEstadoCuentaBL.EstadoCuenta_ListarResumenTotal(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporteVentasxDiasCobranzaResult> EstadoCuenta_VentasDiasCobranza(int idEmpresa, int codigoUsuario,string id_agenda, string id_vendedor, int anho, int mes)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                List<gsReporteVentasxDiasCobranzaResult> list = new List<gsReporteVentasxDiasCobranzaResult>();

                objEstadoCuentaBL = new EstadoCuentaBL();
                list = objEstadoCuentaBL.EstadoCuenta_VentasDiasCobranza(idEmpresa, codigoUsuario, id_agenda, id_vendedor,  anho, mes);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporteVencidosPorMesResult> EstadoCuenta_VencidosMes(int idEmpresa, int codigoUsuario, DateTime fecha)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                List<gsReporteVencidosPorMesResult> list = new List<gsReporteVencidosPorMesResult>();

                objEstadoCuentaBL = new EstadoCuentaBL();
                list = objEstadoCuentaBL.EstadoCuenta_VencidosMes(idEmpresa, codigoUsuario, fecha);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<gsReporteVencidosPorMesClienteResult> EstadoCuenta_VencidosMesCliente(int idEmpresa, int codigoUsuario, DateTime fecha)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                List<gsReporteVencidosPorMesClienteResult> list = new List<gsReporteVencidosPorMesClienteResult>();

                objEstadoCuentaBL = new EstadoCuentaBL();
                list = objEstadoCuentaBL.EstadoCuenta_VencidosMesCliente(idEmpresa, codigoUsuario, fecha);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporteVencidosPorMesDetalleResult> EstadoCuenta_VencidosMesDetalle(int idEmpresa, int codigoUsuario, string id_agenda, string id_vendedor, DateTime fecha)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                List<gsReporteVencidosPorMesDetalleResult> list = new List<gsReporteVencidosPorMesDetalleResult>();

                objEstadoCuentaBL = new EstadoCuentaBL();
                list = objEstadoCuentaBL.EstadoCuenta_VencidosMesDetalle(idEmpresa, codigoUsuario, id_agenda, id_vendedor, fecha);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporte_DocumentosPendientes_ProyectadoResult> EstadoCuenta_ListarxCliente_Proyectado(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                List<gsReporte_DocumentosPendientes_ProyectadoResult> list = new List<gsReporte_DocumentosPendientes_ProyectadoResult>();

                objEstadoCuentaBL = new EstadoCuentaBL();
                list = objEstadoCuentaBL.EstadoCuenta_ListarxCliente_Proyectado(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsEstadoCuenta_MesBIResult> EstadoCuenta_ListarxBI(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fecha)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                List<gsEstadoCuenta_MesBIResult> list = new List<gsEstadoCuenta_MesBIResult>();

                objEstadoCuentaBL = new EstadoCuentaBL();
                list = objEstadoCuentaBL.EstadoCuenta_ListarxBI(idEmpresa, codigoUsuario, codAgenda, codVendedor, fecha);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporte_DocumentosPendientesBIResult> EstadoCuenta_ListarxClienteBI(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                List<gsReporte_DocumentosPendientesBIResult> list = new List<gsReporte_DocumentosPendientesBIResult>();

                objEstadoCuentaBL = new EstadoCuentaBL();
                list = objEstadoCuentaBL.EstadoCuenta_ListarxClienteBI(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VBG00062_PROVEEDORResult> EstadoCuenta_ReporteProveedor(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                List<VBG00062_PROVEEDORResult> list = new List<VBG00062_PROVEEDORResult>();

                objEstadoCuentaBL = new EstadoCuentaBL();
                list = objEstadoCuentaBL.EstadoCuenta_ReporteProveedor(idEmpresa, codigoUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsEstadoCuenta_TotalResult> EstadoCuenta_ClienteBI(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaFinal)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                List<gsEstadoCuenta_TotalResult> list = new List<gsEstadoCuenta_TotalResult>();

                objEstadoCuentaBL = new EstadoCuentaBL();
                list = objEstadoCuentaBL.EstadoCuenta_ClienteBI(idEmpresa, codigoUsuario, codAgenda, codVendedor, fechaFinal);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // New SWF incloud

        public List<GS_GetCicloLetrasResult> GetCicloLetras(int idEmpresa, int codigoUsuario, decimal opFinan)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                List<GS_GetCicloLetrasResult> list = new List<GS_GetCicloLetrasResult>();

                objEstadoCuentaBL = new EstadoCuentaBL();
                list = objEstadoCuentaBL.GetCicloLetras(idEmpresa, codigoUsuario, opFinan);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GS_LetrasGraficoPieResult> EstadoCuenta_GraficoPie(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                List<GS_LetrasGraficoPieResult> list = new List<GS_LetrasGraficoPieResult>();

                objEstadoCuentaBL = new EstadoCuentaBL();
                list = objEstadoCuentaBL.EstadoCuenta_GraficoPie(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GS_GetLetrasPorEstadoResult> EstadoCuenta_LetrasPorEstados(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                List<GS_GetLetrasPorEstadoResult> list = new List<GS_GetLetrasPorEstadoResult>();

                objEstadoCuentaBL = new EstadoCuentaBL();
                list = objEstadoCuentaBL.EstadoCuenta_LetrasPorEstados(idEmpresa, codigoUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GS_GetLetrasVencPorPlazoResult> EstadoCuenta_LetrasVencPorPlazo(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                List<GS_GetLetrasVencPorPlazoResult> list = new List<GS_GetLetrasVencPorPlazoResult>();

                objEstadoCuentaBL = new EstadoCuentaBL();
                list = objEstadoCuentaBL.EstadoCuenta_LetrasVencPorPlazo(idEmpresa, codigoUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GS_GetFacturasVencPorPlazoResult> EstadoCuenta_FacturasVencPorPlazo(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                List<GS_GetFacturasVencPorPlazoResult> list = new List<GS_GetFacturasVencPorPlazoResult>();

                objEstadoCuentaBL = new EstadoCuentaBL();
                list = objEstadoCuentaBL.EstadoCuenta_FacturasVencPorPlazo(idEmpresa, codigoUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporte_DocumentosPendientes_LegalResult> EstadoCuenta_Listar_Legal(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                List<gsReporte_DocumentosPendientes_LegalResult> list = new List<gsReporte_DocumentosPendientes_LegalResult>();

                objEstadoCuentaBL = new EstadoCuentaBL();
                list = objEstadoCuentaBL.EstadoCuenta_Listar_Legal(idEmpresa, codigoUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporte_DocumentosPendientes_ProvisionResult> EstadoCuenta_Listar_Provision(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                List<gsReporte_DocumentosPendientes_ProvisionResult> list = new List<gsReporte_DocumentosPendientes_ProvisionResult>();

                objEstadoCuentaBL = new EstadoCuentaBL();
                list = objEstadoCuentaBL.EstadoCuenta_Listar_Provision(idEmpresa, codigoUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GS_ReporteEstadoCuentaMngResult> EstadoCuenta_ListarAprobacion(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                List<GS_ReporteEstadoCuentaMngResult> list = new List<GS_ReporteEstadoCuentaMngResult>();

                objEstadoCuentaBL = new EstadoCuentaBL();
                list = objEstadoCuentaBL.EstadoCuenta_ListarAprobacion(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void RiesgoCliente_RegistrarBulkCopy(int idEmpresa, int codigoUsuario, DataTable tabla)
        {
            EstadoCuentaBL objEstadoCuentaBL;
            try
            {
                objEstadoCuentaBL = new EstadoCuentaBL();
                objEstadoCuentaBL.RiesgoCliente_RegistrarBulkCopy(idEmpresa, codigoUsuario, tabla);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
