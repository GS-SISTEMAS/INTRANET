using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GS.SISGEGS.DM;
using GS.SISGEGS.BL;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "CobranzasWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione CobranzasWCF.svc o CobranzasWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class CobranzasWCF : ICobranzasWCF
    {
        public List<gsReporteCanceladosWebResult> Reporte_Cancelaciones(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaInicial, DateTime fechaFinal)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<gsReporteCanceladosWebResult> list = new List<gsReporteCanceladosWebResult>();

                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.Reporte_Cancelaciones(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaInicial, fechaFinal);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsDocVenta_ReporteVenta_ClienteResult> Reporte_VentasCliente(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaInicial, DateTime fechaFinal, string ESTADO)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<gsDocVenta_ReporteVenta_ClienteResult> list = new List<gsDocVenta_ReporteVenta_ClienteResult>();

                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.Reporte_VentasCliente(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaInicial, fechaFinal, ESTADO);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsDocVenta_ReporteVenta_ClienteResumenResult> Reporte_VentasClienteResumen(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaInicial, DateTime fechaFinal, int year)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<gsDocVenta_ReporteVenta_ClienteResumenResult> list = new List<gsDocVenta_ReporteVenta_ClienteResumenResult>();

                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.Reporte_VentasClienteResumen(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaInicial, fechaFinal, year);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporteCanceladosWebResumenResult> Reporte_CancelacionesResumen(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, int year)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<gsReporteCanceladosWebResumenResult> list = new List<gsReporteCanceladosWebResumenResult>();

                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.Reporte_CancelacionesResumen(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, year);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporteCanceladosWebResumenVendedorResult> Reporte_CancelacionesResumenVendedor(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, DateTime fechaInicial, DateTime fechaFinal)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<gsReporteCanceladosWebResumenVendedorResult> list = new List<gsReporteCanceladosWebResumenVendedorResult>();

                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.Reporte_CancelacionesResumenVendedor(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, fechaInicial, fechaFinal);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<gsReporteCanceladosResumenMes_actualResult> Reporte_CancelacionesResumenActual(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, int year, int mes)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<gsReporteCanceladosResumenMes_actualResult> list = new List<gsReporteCanceladosResumenMes_actualResult>();

                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.Reporte_CancelacionesResumenActual(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, year, mes);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporteCanceladosResumenMes_v3Result> Reporte_CancelacionesResumenActual_v3(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, int year, int mes)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<gsReporteCanceladosResumenMes_v3Result> list = new List<gsReporteCanceladosResumenMes_v3Result>();

                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.Reporte_CancelacionesResumenv3(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, year, mes);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporteCobranzaWeb_DetalleMesResult> Reporte_CancelacionesResumenDetalleMes(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, int year, int mes)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<gsReporteCobranzaWeb_DetalleMesResult> list = new List<gsReporteCobranzaWeb_DetalleMesResult>();

                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.Reporte_CancelacionesResumenDetalleMes(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, year, mes);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporteCanceladosResumenMes_actualResult> Reporte_CancelacionesResumenActual_v2(int idEmpresa, int codigoUsuario, string codAgenda, string codigoVendedor, int year, int mes)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<gsReporteCanceladosResumenMes_actualResult> list = new List<gsReporteCanceladosResumenMes_actualResult>();

                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.Reporte_CancelacionesResumenActual_v2(idEmpresa, codigoUsuario, codAgenda, codigoVendedor, year, mes);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsProyectadoCobranza_ListarResult> Reporte_Proyectado_Sectorista(int idEmpresa, int codigoUsuario, int id_proyectado, int periodo, string id_sectorista, string id_cliente, int id_zona, string id_vendedor)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<gsProyectadoCobranza_ListarResult> list = new List<gsProyectadoCobranza_ListarResult>();

                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.Reporte_Proyectado_Sectorista(idEmpresa, codigoUsuario,id_proyectado, periodo, id_sectorista, id_cliente, id_zona, id_vendedor );

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar_ProyectadoSectorista(int idEmpresa, int codigoUsuario, int id_proyectado, string periodo, string id_sectorista, string id_cliente, decimal montoS1, decimal montoS2, decimal montoS3, decimal montoS4, decimal proyectado, int estado)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                objCobranzasBL = new CobranzasBL();
                objCobranzasBL.Insertar_ProyectadoSectorista(idEmpresa, codigoUsuario, id_proyectado, periodo, id_sectorista, id_cliente,montoS1, montoS2, montoS3, montoS4, proyectado, estado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsGestionCobranza_ListarResult> Reporte_Gestion_Sectorista(int idEmpresa, int codigoUsuario, string id_Cliente, int Periodo)

        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<gsGestionCobranza_ListarResult> list = new List<gsGestionCobranza_ListarResult>();

                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.Reporte_Gestion_Sectorista(idEmpresa, codigoUsuario, id_Cliente, Periodo);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsEstatus_ListarResult> Estatus_Deuda_Listar(int idEmpresa, int codigoUsuario)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<gsEstatus_ListarResult> list = new List<gsEstatus_ListarResult>();

                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.Estatus_Deuda_Listar(idEmpresa, codigoUsuario);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporteCobranzas_Poryectadas_VendedorResult> Reporte_CobranzasProyectadasVendedor(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<gsReporteCobranzas_Poryectadas_VendedorResult> list = new List<gsReporteCobranzas_Poryectadas_VendedorResult>();
                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.Reporte_CobranzasProyectadasVendedor(idEmpresa, codigoUsuario, mes, año, periodo, id_zona, id_sectorista);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporteCobranzas_Poryectadas_Vendedor_DetalleResult> Reporte_CobranzasProyectadasVendedorDetalle(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<gsReporteCobranzas_Poryectadas_Vendedor_DetalleResult> list = new List<gsReporteCobranzas_Poryectadas_Vendedor_DetalleResult>();
                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.Reporte_CobranzasProyectadasVendedorDetalle(idEmpresa, codigoUsuario, mes, año, periodo, id_zona, id_sectorista);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporteCobranzas_Poryectadas_Vendedor_FechaResult> Reporte_CobranzasProyectadasVendedor_Fecha(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<gsReporteCobranzas_Poryectadas_Vendedor_FechaResult> list = new List<gsReporteCobranzas_Poryectadas_Vendedor_FechaResult>();
                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.Reporte_CobranzasProyectadasVendedor_Fecha(idEmpresa, codigoUsuario, mes, año, periodo, id_zona, id_sectorista);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ProyectadoCobranza_Registrar(int idEmpresa, int codigoUsuario, string codAgenda, string codSectorista, string periodo, int id_zona, decimal montoS1, decimal montoS2, decimal montoS3, decimal montoS4, decimal proyectado)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                objCobranzasBL = new CobranzasBL();
                objCobranzasBL.ProyectadoCobranza_Registrar(idEmpresa, codigoUsuario, codAgenda, codSectorista, periodo, id_zona, montoS1, montoS2, montoS3, montoS4, proyectado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GestionCobranza_Registrar(int idEmpresa, int codigoUsuario, string id_cliente, string periodo, int id_semana,
            int id_estatus, string observacion, int estado, string TablaOrigen, int opOrigen)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                objCobranzasBL = new CobranzasBL();
                objCobranzasBL.GestionCobranza_Registrar(idEmpresa, codigoUsuario, id_cliente, periodo, id_semana, id_estatus, observacion, estado, TablaOrigen, opOrigen);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ProyectadoCobranza_Verificar(int idEmpresa, int codigoUsuario, string codSectorista, string periodo, int id_zona)
        {
            CobranzasBL objCobranzasBL;
            int respuesta; 
            try
            {
                objCobranzasBL = new CobranzasBL();
                respuesta = objCobranzasBL.ProyectadoCobranza_Verificar(idEmpresa, codigoUsuario, codSectorista, periodo, id_zona);
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsVentasCobranzas_ListarResult> Reporte_VentasCobranzasAnual(int idEmpresa, int codigoUsuario, int anho)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<gsVentasCobranzas_ListarResult> list = new List<gsVentasCobranzas_ListarResult>();

                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.Reporte_VentasCobranzasAnual(idEmpresa, codigoUsuario, anho);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporteCancelados_ProyectadoResult> Reporte_CobranzasProyectadas_Sectorista(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<gsReporteCancelados_ProyectadoResult> list = new List<gsReporteCancelados_ProyectadoResult>();
                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.Reporte_CobranzasProyectadas_Sectorista(idEmpresa, codigoUsuario, mes, año, periodo, id_zona, id_sectorista);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Reporte_VentaxCobranzaLegalResult> Reporte_VentaCobranzaLegal(int idEmpresa, int codigoUsuario)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<Reporte_VentaxCobranzaLegalResult> list = new List<Reporte_VentaxCobranzaLegalResult>();
                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.Reporte_VentaCobranzaLegal(idEmpresa, codigoUsuario);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // Agregado por Percy Santiago 10/11/2016
        public List<gsReporteFacturasInafectasV1Result> Reporte_FacturasInafecta(int idEmpresa, int codigoUsuario, System.DateTime dtpFechaInicio, System.DateTime dtpFechaFin, int tipodocumento, string Cliente)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<gsReporteFacturasInafectasV1Result> list = new List<gsReporteFacturasInafectasV1Result>();

                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.Reporte_FacturasInafecta(idEmpresa, codigoUsuario, dtpFechaInicio, dtpFechaFin, tipodocumento, Cliente);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // Fin Percy Santiago 10/11/2016

        public void ProyectarCobranza_Registrar(int idEmpresa, int codigoUsuario, int id_proyectado, string periodo, int id_semana, float Importe, string TablaOrigen, int OpOrigen, int estado)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                objCobranzasBL = new CobranzasBL();
                objCobranzasBL.ProyectarCobranza_Registrar(idEmpresa, codigoUsuario, id_proyectado, periodo, id_semana, Importe, TablaOrigen, OpOrigen, estado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProyectadoCobranza_ListarResult> ProyectadoCobranza_Listar(int idEmpresa, int codigoUsuario, int idProyectado,
             string periodo, int id_semana, string tablaOrigen, int opOrigen, int estado)
        {
            CobranzasBL objCobranzasBL;
            List<ProyectadoCobranza_ListarResult> lista = new List<ProyectadoCobranza_ListarResult>();

            try
            {
                objCobranzasBL = new CobranzasBL();
                lista = objCobranzasBL.ProyectadoCobranza_Listar(idEmpresa, codigoUsuario, idProyectado, periodo, id_semana, tablaOrigen, opOrigen, estado);

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //----------------------------------

        public List<ZonasSectorista_ListarResult> ZonasSectorista_Listar(int idEmpresa, int codigoUsuario, string texto, int reporte)
        {
            CobranzasBL objCobranzasBL;
            List<ZonasSectorista_ListarResult> lista = new List<ZonasSectorista_ListarResult>();

            try
            {
                objCobranzasBL = new CobranzasBL();
                lista = objCobranzasBL.ZonasSectorista_Listar(idEmpresa, codigoUsuario, texto, reporte);

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ZonasSectoristaPermiso_ListarResult> ZonasSectoristaPermiso_Listar(int idEmpresa, int codigoUsuario, string id_agenda)
        {
            CobranzasBL objCobranzasBL = new CobranzasBL();
            List<ZonasSectoristaPermiso_ListarResult> lista = new List<ZonasSectoristaPermiso_ListarResult>();

            try
            {
                lista = objCobranzasBL.ZonasSectoristaPermiso_Listar(idEmpresa, codigoUsuario, id_agenda);

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int PermisosZona_Registrar(int idEmpresa, int codigoUsuario, string id_agenda, int id_zona, int ActivoZona, int ActivoSectorista)
        {
            CobranzasBL objCobranzasBL = new CobranzasBL();
            int Registro = 0;

            try
            {
                Registro = objCobranzasBL.PermisosZona_Registrar(idEmpresa, codigoUsuario, id_agenda, id_zona, ActivoZona, ActivoSectorista);

                return Registro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<spEstadoCuenta_ProyectadoResult> EstadoCuenta_Proyectado(int idEmpresa, int codigoUsuario, int periodo, string id_sectorista, int id_zona, int anho, int mes)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<spEstadoCuenta_ProyectadoResult> list = new List<spEstadoCuenta_ProyectadoResult>();

                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.EstadoCuenta_Proyectado(idEmpresa, codigoUsuario, periodo, id_sectorista, id_zona, anho, mes);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<spEstadoCuenta_Proyectado_ClienteResult> EstadoCuenta_Proyectado_Cliente(int idEmpresa, int codigoUsuario, string id_cliente, int periodo, string id_sectorista, int id_zona, int anho, int mes)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<spEstadoCuenta_Proyectado_ClienteResult> list = new List<spEstadoCuenta_Proyectado_ClienteResult>();

                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.EstadoCuenta_Proyectado_Cliente(idEmpresa, codigoUsuario, id_cliente, periodo, id_sectorista, id_zona, anho, mes);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ProyectadoCobranza_Eliminar(int idEmpresa, int codigoUsuario, int id_proyectado)
        {
            CobranzasBL objCobranzasBL = new CobranzasBL();

            try
            {
                objCobranzasBL.ProyectadoCobranza_Eliminar(idEmpresa, codigoUsuario, id_proyectado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProyectadoCobranza_DocumentosResult> ProyectadoCobranza_Documentos(int idEmpresa, int codigoUsuario, string id_Cliente, int Periodo)
        {
            CobranzasBL objCobranzasBL = new CobranzasBL();
            List<ProyectadoCobranza_DocumentosResult> lista = new List<ProyectadoCobranza_DocumentosResult>();

            try
            {
                lista = objCobranzasBL.ProyectadoCobranza_Documentos(idEmpresa, codigoUsuario, id_Cliente, Periodo);
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<gsReporteCobranzas_Poryectadas_ClienteResult> Reporte_CobranzasProyectadasCliente(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<gsReporteCobranzas_Poryectadas_ClienteResult> list = new List<gsReporteCobranzas_Poryectadas_ClienteResult>();
                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.Reporte_CobranzasProyectadasCliente(idEmpresa, codigoUsuario, mes, año, periodo, id_zona, id_sectorista);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporteProyectado_Cuadro1Result> Reporte_CobranzasProyectadas_Cuadro1(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<gsReporteProyectado_Cuadro1Result> list = new List<gsReporteProyectado_Cuadro1Result>();
                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.Reporte_CobranzasProyectadas_Cuadro1(idEmpresa, codigoUsuario, mes, año, periodo, id_zona, id_sectorista);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsReporteProyectado_Cuadro2Result> Reporte_CobranzasProyectadas_Cuadro2(int idEmpresa, int codigoUsuario, int mes, int año, int periodo, int id_zona, string id_sectorista)
        {
            CobranzasBL objCobranzasBL;
            try
            {
                List<gsReporteProyectado_Cuadro2Result> list = new List<gsReporteProyectado_Cuadro2Result>();
                objCobranzasBL = new CobranzasBL();
                list = objCobranzasBL.Reporte_CobranzasProyectadas_Cuadro2(idEmpresa, codigoUsuario, mes, año, periodo, id_zona, id_sectorista);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Zonas_Reportes_CobranzaResult> Zonas_Listar_Cobranza(int idEmpresa, int codigoUsuario, string id_sectorista)
        {
            CobranzasBL objCobranzasBL;

            try
            {
                List<Zonas_Reportes_CobranzaResult> lstZonas = new List<Zonas_Reportes_CobranzaResult>();
                objCobranzasBL = new CobranzasBL();

                lstZonas = objCobranzasBL.Zonas_Listar_Cobranza(idEmpresa, codigoUsuario, id_sectorista);

                return lstZonas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
