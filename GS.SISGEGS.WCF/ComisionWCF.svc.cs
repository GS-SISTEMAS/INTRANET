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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ComisionWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ComisionWCF.svc o ComisionWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ComisionWCF : IComisionWCF
    {
        public List<gsReporteCanceladosResult> Reporte_Cancelaciones(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaInicial, DateTime fechaFinal)
        {
            ComisionBL objComisionBL;

            try
            {
                List<gsReporteCanceladosResult> list = new List<gsReporteCanceladosResult>();

                objComisionBL = new ComisionBL();
                list = objComisionBL.Reporte_Cancelaciones(idEmpresa, codigoUsuario, codAgenda, codVendedor, fechaInicial, fechaFinal);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Personal_ListarTotalResult> Personal_ListarTotal(int idEmpresa, int codigoUsuario, string codigoEmpresa, string codigoCargo, string texto, int reporte, int id_zona)
        {
            ComisionBL objComisionBL;

            try
            {
                List<Personal_ListarTotalResult> list = new List<Personal_ListarTotalResult>();

                objComisionBL = new ComisionBL();
                list = objComisionBL.Personal_ListarTotal(idEmpresa, codigoUsuario, codigoEmpresa, codigoCargo, texto, reporte, id_zona);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Personal_BuscarResult> Personal_Buscar(int idEmpresa, int codigoUsuario, string codigoEmpresa, string codigoCargo, string texto)
        {
            ComisionBL objComisionBL;

            try
            {
                List<Personal_BuscarResult> list = new List<Personal_BuscarResult>();

                objComisionBL = new ComisionBL();
                list = objComisionBL.Personal_Buscar(idEmpresa, codigoUsuario, codigoEmpresa, codigoCargo, texto);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Personal_Registrar(int idPersonal, string nroDocumento, string imageURL, int idUsuarioRegistro, string codigoEmpresa, string codigoCargo, decimal porcentaje, int estado, int reporte)
        {
            ComisionBL objComisionBL;
            try
            {
                objComisionBL = new ComisionBL();
                objComisionBL.Personal_Registrar(idPersonal, nroDocumento, imageURL, idUsuarioRegistro, codigoEmpresa, codigoCargo, porcentaje, estado, reporte);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ZonaPersonal_ListarResult> ZonaPersonal_Listar(int idEmpresa, int codigoUsuario, string nroDocumento, int id_zona, string texto)
        {
            ComisionBL objComisionBL;

            try
            {
                List<ZonaPersonal_ListarResult> list = new List<ZonaPersonal_ListarResult>();

                objComisionBL = new ComisionBL();
                list = objComisionBL.ZonaPersonal_Listar(idEmpresa, codigoUsuario, nroDocumento,id_zona,texto);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsZonas_ListarResult> Zonas_Listar(int idEmpresa, int codigoUsuario, string nroDocumento, int id_zona)
        {
            ComisionBL objComisionBL;

            try
            {
                List<gsZonas_ListarResult> list = new List<gsZonas_ListarResult>();

                objComisionBL = new ComisionBL();
                list = objComisionBL.Zonas_Listar(idEmpresa, codigoUsuario, nroDocumento, id_zona);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Zona_Registrar(int idEmpresa, int id_zona, string nroDocumento, int idUsuarioRegistro, string codigoCargo, decimal porcentaje, int estado)
        {
            ComisionBL objComisionBL;
            try
            {
                objComisionBL = new ComisionBL();
                objComisionBL.Zona_Registrar(idEmpresa, id_zona, nroDocumento, idUsuarioRegistro, codigoCargo, porcentaje, estado);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsZonasComision_ListarResult> ZonasComision_Listar(int idEmpresa, int codigoUsuario)
        {
            ComisionBL objComisionBL;

            try
            {
                List<gsZonasComision_ListarResult> list = new List<gsZonasComision_ListarResult>();

                objComisionBL = new ComisionBL();
                list = objComisionBL.ZonasComision_Listar(idEmpresa, codigoUsuario);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ZonasComision_Insert(int idEmpresa, int id_zona, int idUsuarioRegistro, decimal porcentaje, bool estado)
        {
            ComisionBL objComisionBL;
            try
            {
                objComisionBL = new ComisionBL();
                objComisionBL.ZonasComision_Insert(idEmpresa, id_zona, idUsuarioRegistro, porcentaje, estado);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<combo_CargosRHResult> CargoRH_Listar(string idempresa)
        {
            ComisionBL objComisionBL;

            try
            {
                List<combo_CargosRHResult> list = new List<combo_CargosRHResult>();

                objComisionBL = new ComisionBL();
                list = objComisionBL.CargoRH_Listar(idempresa);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsComisiones_VendedoresResult> gsComisiones_Vendedores(int idEmpresa, int codigoUsuario, int anho, int mes)
        {
            ComisionBL objComisionBL;
            try
            {
                List<gsComisiones_VendedoresResult> list = new List<gsComisiones_VendedoresResult>();
                objComisionBL = new ComisionBL();
                list = objComisionBL.gsComisiones_Vendedores(idEmpresa, codigoUsuario, anho, mes);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsComisiones_JefaturasResult> gsComisiones_Jefaturas(int idEmpresa, int codigoUsuario, int anho, int mes)
        {
            ComisionBL objComisionBL;
            try
            {
                List<gsComisiones_JefaturasResult> list = new List<gsComisiones_JefaturasResult>();
                objComisionBL = new ComisionBL();
                list = objComisionBL.gsComisiones_Jefaturas(idEmpresa, codigoUsuario, anho, mes);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<combo_ReporteResult> combo_Reporte()
        {
            ComisionBL objComisionBL;

            try
            {
                List<combo_ReporteResult> list = new List<combo_ReporteResult>();

                objComisionBL = new ComisionBL();
                list = objComisionBL.combo_Reporte();

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Reportes_ListarResult> Reportes_Listar(string idempresa)
        {
            ComisionBL objComisionBL;
            try
            {
                List<Reportes_ListarResult> list = new List<Reportes_ListarResult>();
                objComisionBL = new ComisionBL();
                list = objComisionBL.Reportes_Listar(idempresa);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Promotores_ListarResult> Promotores_Listar(int idEmpresa, int codigoUsuario, int id_zona, int reporte)
        {
            ComisionBL objComisionBL;
            try
            {
                List<Promotores_ListarResult> list = new List<Promotores_ListarResult>();
                objComisionBL = new ComisionBL();
                list = objComisionBL.Promotores_Listar(idEmpresa, codigoUsuario, id_zona, reporte);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void gsAgenda_UpdateZona(int idEmpresa, int codigoUsuario, string id_agenda, int id_zona)
        {
            ComisionBL objComisionBL;

            try
            {
                objComisionBL = new ComisionBL();
                objComisionBL.gsAgenda_UpdateZona(idEmpresa, codigoUsuario, id_agenda, id_zona);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<gsComisiones_PromotoresResult> gsComisiones_Promotores(int idEmpresa, int codigoUsuario, int anho, int mes)
        {
            ComisionBL objComisionBL;
            try
            {
                List<gsComisiones_PromotoresResult> list = new List<gsComisiones_PromotoresResult>();
                objComisionBL = new ComisionBL();
                list = objComisionBL.gsComisiones_Promotores(idEmpresa, codigoUsuario, anho, mes);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsComisiones_SemillasResult> gsComisiones_Semillas(int idEmpresa, int codigoUsuario, int anho, int mes)
        {
            ComisionBL objComisionBL;
            try
            {
                List<gsComisiones_SemillasResult> list = new List<gsComisiones_SemillasResult>();
                objComisionBL = new ComisionBL();
                list = objComisionBL.gsComisiones_Semillas(idEmpresa, codigoUsuario, anho, mes);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsPeriodoComision_ListarResult> PeriodoComision_Listar(int anho)
        {
            ComisionBL objComisionBL;
            try
            {
                List<gsPeriodoComision_ListarResult> list = new List<gsPeriodoComision_ListarResult>();
                objComisionBL = new ComisionBL();
                list = objComisionBL.PeriodoComision_Listar(anho);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
