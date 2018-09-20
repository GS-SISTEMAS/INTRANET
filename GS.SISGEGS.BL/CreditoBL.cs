using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GS.SISGEGS.DM;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface ICreditoBL {
        List<gsCredito_ListarCondicionResult> Credito_ListarCondicion(int idEmpresa, int codigoUsuario, string idAgenda);
        List<USP_SEL_LetrasSinCanjeV2Result> ObtenerLetrasSinCanje(int idEmpresa, int codigoUsuario, int? estado, DateTime FechaEmisionDesde, DateTime FechaEmisionHasta, int dias, string zona);
        void ObtenerVerificacionAprobacion2(int idEmpresa, int codigoUsuario, string id_agenda, ref List<USP_SEL_Verificacion_DeudaVencidaResult> lstdeuda, ref List<USP_SEL_Verificacion_LetrasxAceptarResult> lstletras);
        void Enviar_Notificacion_Aprobacion2(int idEmpresa, int codigoUsuario, string id_agenda, string NombreAgenda, string OpPedido,
            decimal TotalLetrasxAceptarS, decimal TotalDeudaVencidaS, decimal TotalLetrasxAceptarN, decimal TotalDeudaVencidaN,
            decimal TotalLetrasxAceptarI, decimal TotalDeudaVencidaI, string UsuarioAprobacion, string comentarios);
        void RegistrarLog_AprobacionDeudaVencida(int idEmpresa, int codigoUsuario, string id_agenda, string NombreAgenda, string Op,
            decimal TotalLetrasxAceptarSil, decimal TotalDeudaVencidaSil, decimal TotalLetrasxAceptarNeo, decimal TotalDeudaVencidaNeo, 
            decimal TotalLetrasxAceptarIna, decimal TotalDeudaVencidaIna, string comentarios);
    }

    public class CreditoBL : ICreditoBL
    {
        public List<gsCredito_ListarCondicionResult> Credito_ListarCondicion(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<gsCredito_ListarCondicionResult> lista = new List<gsCredito_ListarCondicionResult>(); 
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    lista = dcg.gsCredito_ListarCondicion(idAgenda, 2).ToList();
                    return lista; 
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las condiciones de pago en la base de datos.");
                }

            }
        }

        public List<USP_SEL_LetrasSinCanjeV2Result> ObtenerLetrasSinCanje(int idEmpresa, int codigoUsuario, int? estado,DateTime FechaEmisionDesde,DateTime FechaEmisionHasta,int dias,string zona)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<gsCredito_ListarCondicionResult> lista = new List<gsCredito_ListarCondicionResult>();
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.USP_SEL_LetrasSinCanjeV2(estado, FechaEmisionDesde, FechaEmisionHasta, dias,zona.Trim()).ToList();
                    
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al obtener las letras sin canje.");
                }
            }
        }

        public void ObtenerVerificacionAprobacion2(int idEmpresa, int codigoUsuario,string id_agenda,ref List<USP_SEL_Verificacion_DeudaVencidaResult> lstdeuda,ref List<USP_SEL_Verificacion_LetrasxAceptarResult> lstletras)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    lstdeuda = dci.USP_SEL_Verificacion_DeudaVencida(id_agenda).ToList();
                    lstletras = dci.USP_SEL_Verificacion_LetrasxAceptar(id_agenda).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al obtener las letras sin canje.");
                }
            }
        }

        public void Enviar_Notificacion_Aprobacion2(int idEmpresa, int codigoUsuario,string id_agenda,string NombreAgenda,string OpPedido,
            decimal TotalLetrasxAceptarS,decimal TotalDeudaVencidaS, decimal TotalLetrasxAceptarN, decimal TotalDeudaVencidaN,
            decimal TotalLetrasxAceptarI, decimal TotalDeudaVencidaI,string UsuarioAprobacion,string comentarios)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dci.USP_NOTIFICACION_APROBACION2_V2(id_agenda, NombreAgenda, OpPedido, TotalLetrasxAceptarS, TotalDeudaVencidaS, TotalLetrasxAceptarN, TotalDeudaVencidaN,
                        TotalLetrasxAceptarI, TotalDeudaVencidaI,UsuarioAprobacion, comentarios);
                    dci.SubmitChanges();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al obtener las letras sin canje.");
                }
            }
        }

        public void RegistrarLog_AprobacionDeudaVencida(int idEmpresa, int codigoUsuario, string id_agenda, string NombreAgenda, string Op, 
            decimal TotalLetrasxAceptarSil, decimal TotalDeudaVencidaSil, decimal TotalLetrasxAceptarNeo, decimal TotalDeudaVencidaNeo, 
            decimal TotalLetrasxAceptarIna, decimal TotalDeudaVencidaIna,string comentarios)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dci.USP_INS_LOG_DEUDASVENCIDAS_APROBACION2(
                        idEmpresa,
                        id_agenda, 
                        NombreAgenda, 
                        Convert.ToInt32(Op), 
                        TotalDeudaVencidaSil, TotalLetrasxAceptarSil,
                        TotalDeudaVencidaNeo, TotalLetrasxAceptarNeo,
                        TotalDeudaVencidaIna, TotalLetrasxAceptarIna,
                        codigoUsuario, comentarios
                        );
                    dci.SubmitChanges();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al obtener las letras sin canje.");
                }
            }
        }
    }
}
