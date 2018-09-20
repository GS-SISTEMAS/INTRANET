using System;
using System.Collections.Generic;
using System.Linq;
using GS.SISGEGS.DM;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using GS.SISGEGS.BL;
using System.Transactions;

namespace GS.SISGEGS.BL
{

    public interface IComisionBL
    {
        List<gsReporteCanceladosResult> Reporte_Cancelaciones(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaInicial, DateTime fechaFinal);

        List<Personal_ListarTotalResult> Personal_ListarTotal(int idEmpresa, int codigoUsuario, string codigoEmpresa, string codigoCargo, string texto, int reporte, int id_zona);

        List<Personal_BuscarResult> Personal_Buscar(int idEmpresa, int codigoUsuario, string codigoEmpresa, string codigoCargo, string texto);

        void Personal_Registrar(int idPersonal, string nroDocumento, string imageURL, int idUsuarioRegistro, string codigoEmpresa, string codigoCargo, decimal porcentaje, int estado, int reporte); 

        List<ZonaPersonal_ListarResult> ZonaPersonal_Listar(int idEmpresa, int codigoUsuario, string nroDocumento, int id_zona, string texto);

        List<gsZonas_ListarResult> Zonas_Listar(int idEmpresa, int codigoUsuario, string nroDocumento, int id_zona);

        void Zona_Registrar(int idEmpresa, int id_zona, string nroDocumento, int idUsuarioRegistro, string codigoCargo, decimal porcentaje, int estado);

        List<gsZonasComision_ListarResult> ZonasComision_Listar(int idEmpresa, int codigoUsuario);

        void ZonasComision_Insert(int idEmpresa, int id_zona, int idUsuarioRegistro, decimal porcentaje, bool estado);

        List<combo_CargosRHResult> CargoRH_Listar(string idempresa);
        List<gsComisiones_VendedoresResult> gsComisiones_Vendedores(int idEmpresa, int codigoUsuario, int anho, int mes);

        List<gsComisiones_JefaturasResult> gsComisiones_Jefaturas(int idEmpresa, int codigoUsuario, int anho, int mes);

        List<combo_ReporteResult> combo_Reporte();

        List<Reportes_ListarResult> Reportes_Listar(string idempresa);

        List<Promotores_ListarResult> Promotores_Listar(int idEmpresa, int codigoUsuario, int id_zona, int reporte);

        void gsAgenda_UpdateZona(int idEmpresa, int codigoUsuario, string id_agenda, int id_zona);

        List<gsComisiones_PromotoresResult> gsComisiones_Promotores(int idEmpresa, int codigoUsuario, int anho, int mes);

        List<gsComisiones_SemillasResult> gsComisiones_Semillas(int idEmpresa, int codigoUsuario, int anho, int mes); 

    }
    public class ComisionBL : IComisionBL
    {
        public List<gsReporteCanceladosResult> Reporte_Cancelaciones(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaInicial, DateTime fechaFinal)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))

            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<gsReporteCanceladosResult> list = dcg.gsReporteCancelados().ToList();
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

        public List<Personal_ListarTotalResult> Personal_ListarTotal(int idEmpresa, int codigoUsuario, string codigoEmpresa, string codigoCargo, string texto, int reporte, int id_zona)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.Personal_ListarTotal(codigoEmpresa, codigoCargo,texto, reporte, id_zona ).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar en la base de datos. " + ex.Message.ToString());
                }
            }
        }

        public List<Personal_BuscarResult> Personal_Buscar(int idEmpresa, int codigoUsuario, string codigoEmpresa, string codigoCargo, string texto)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<Personal_BuscarResult> list = dcg.Personal_Buscar(codigoEmpresa, codigoCargo, texto).ToList();
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

        public void Personal_Registrar(int idPersonal, string nroDocumento, string imageURL, int idUsuarioRegistro, string codigoEmpresa, string codigoCargo, decimal porcentaje, int estado, int reporte)
        {
            //dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString);
            dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")); 

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    dci.Personal_InsertarComision(idPersonal, nroDocumento, imageURL, idUsuarioRegistro, codigoEmpresa, codigoCargo, porcentaje, estado, reporte);
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

        public List<ZonaPersonal_ListarResult> ZonaPersonal_Listar(int idEmpresa, int codigoUsuario, string nroDocumento, int id_zona, string texto)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.ZonaPersonal_Listar(nroDocumento, id_zona, texto).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar por las referencias de la sucursal en la base de datos.");
                }
            }
        }

        public List<gsZonas_ListarResult> Zonas_Listar(int idEmpresa, int codigoUsuario, string nroDocumento, int id_zona)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsZonas_Listar(nroDocumento, id_zona).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar en la base de datos(BL). " + ex.Message.ToString());
                }
            }
        }

        public void Zona_Registrar(int idEmpresa, int id_zona, string nroDocumento,  int idUsuarioRegistro, string codigoCargo, decimal porcentaje, int estado)
        {
            //dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString);
            dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys"));

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    dci.ZonaPersonal_Insertar(idEmpresa,id_zona , nroDocumento, porcentaje , idUsuarioRegistro, estado);
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

        public List<gsZonasComision_ListarResult> ZonasComision_Listar(int idEmpresa, int codigoUsuario)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsZonasComision_Listar().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar en la base de datos(BL). " + ex.Message.ToString());
                }
            }
        }

        public void ZonasComision_Insert(int idEmpresa, int id_zona, int codigoUsuario, decimal porcentaje, bool estado)
        {
            //dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString);
            dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys"));

            try
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dcg.gsZonasComision_Insert(id_zona.ToString(), porcentaje, codigoUsuario, estado);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                throw new ArgumentException("Error consultar en la base de datos(BL). " + ex.Message.ToString());
            }
          
        }

        public List<combo_CargosRHResult> CargoRH_Listar(string idempresa)
        {
            //dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString);
            dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys"));

            try
            {
                return
                dci.combo_CargosRH(idempresa).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        public List<gsComisiones_VendedoresResult> gsComisiones_Vendedores(int idEmpresa, int codigoUsuario, int anho, int mes)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsComisiones_Vendedores(idEmpresa,anho, mes).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar en la base de datos(BL). " + ex.Message.ToString());
                }
            }
        }

        public List<gsComisiones_JefaturasResult> gsComisiones_Jefaturas(int idEmpresa, int codigoUsuario, int anho, int mes)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsComisiones_Jefaturas(idEmpresa, anho, mes).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar en la base de datos(BL). " + ex.Message.ToString());
                }
            }
        }

        public List<combo_ReporteResult> combo_Reporte()
        {
            //dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString);
            dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys"));

            try
            {
                    return dci.combo_Reporte().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                throw new ArgumentException("Error consultar en la base de datos(BL). " + ex.Message.ToString());
            }
            
        }

        public List<Reportes_ListarResult> Reportes_Listar(string idempresa)
        {
            //dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString);
            dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys"));

            try
            {
                return
                dci.Reportes_Listar().ToList();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error consultar en la base de datos(BL). " + ex.Message.ToString());
            };
        }

        public List<Promotores_ListarResult> Promotores_Listar(int idEmpresa, int codigoUsuario, int id_zona, int reporte)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.Promotores_Listar(idEmpresa, id_zona, reporte).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar en la base de datos(BL). " + ex.Message.ToString());
                }
            }
        }

        public void gsAgenda_UpdateZona(int idEmpresa, int codigoUsuario, string id_agenda, int id_zona)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.gsAgenda_UpdateZona(id_agenda, id_zona, codigoUsuario); 
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar en la base de datos(BL). " + ex.Message.ToString());
                }
            }
        }

        public List<gsComisiones_PromotoresResult> gsComisiones_Promotores(int idEmpresa, int codigoUsuario, int anho, int mes)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.CommandTimeout = 90; 
                    return dcg.gsComisiones_Promotores(idEmpresa, anho, mes).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar en la base de datos(BL). " + ex.Message.ToString());
                }
            }
        }

        public List<gsComisiones_SemillasResult> gsComisiones_Semillas(int idEmpresa, int codigoUsuario, int anho, int mes)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsComisiones_Semillas(idEmpresa, anho, mes).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar en la base de datos(BL). " + ex.Message.ToString());
                }
            }
        }

        public List<gsPeriodoComision_ListarResult> PeriodoComision_Listar(int anho)
        {
            //dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString);
            dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys"));

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    List<gsPeriodoComision_ListarResult> lista = new List<gsPeriodoComision_ListarResult>();
                    lista = dci.gsPeriodoComision_Listar(anho).ToList();
                    dci.SubmitChanges();
                    scope.Complete();

                    return lista; 
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
