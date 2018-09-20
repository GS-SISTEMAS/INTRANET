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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "CreditoWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione CreditoWCF.svc o CreditoWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class CreditoWCF : ICreditoWCF
    {
        public List<gsCredito_ListarCondicionResult> Credito_ListarCondicion(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            CreditoBL objCreditoBL;
            try
            {
                objCreditoBL = new CreditoBL();
                return objCreditoBL.Credito_ListarCondicion(idEmpresa, codigoUsuario, idAgenda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USP_SEL_LetrasSinCanjeV2Result> ObtenerLetrasSinCanje(int idEmpresa, int codigoUsuario, int? estado, DateTime FechaEmisionDesde, DateTime FechaEmisionHasta, int dias, string zona)
        {
            CreditoBL objCreditoBL;
            try
            {
                objCreditoBL = new CreditoBL();
                return objCreditoBL.ObtenerLetrasSinCanje(idEmpresa, codigoUsuario, estado, FechaEmisionDesde, FechaEmisionHasta, dias,zona);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ObtenerVerificacionAprobacion2(int idEmpresa, int codigoUsuario, string id_agenda, ref List<USP_SEL_Verificacion_DeudaVencidaResult> lstdeuda, ref List<USP_SEL_Verificacion_LetrasxAceptarResult> lstletras)
        {
            CreditoBL objCreditoBL;
            try
            {
                objCreditoBL = new CreditoBL();
                objCreditoBL.ObtenerVerificacionAprobacion2(idEmpresa, codigoUsuario, id_agenda, ref lstdeuda,ref lstletras);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Enviar_Notificacion_Aprobacion2(int idEmpresa, int codigoUsuario, string id_agenda, string NombreAgenda, string OpPedido,
            decimal TotalLetrasxAceptarS, decimal TotalDeudaVencidaS, decimal TotalLetrasxAceptarN, decimal TotalDeudaVencidaN,
            decimal TotalLetrasxAceptarI, decimal TotalDeudaVencidaI, string UsuarioAprobacion, string comentarios)
        {
            CreditoBL objCreditoBL;
            try
            {
                objCreditoBL = new CreditoBL();
                objCreditoBL.Enviar_Notificacion_Aprobacion2(idEmpresa, codigoUsuario, id_agenda, NombreAgenda, OpPedido, TotalLetrasxAceptarS, TotalDeudaVencidaS,
                    TotalLetrasxAceptarN, TotalDeudaVencidaN, TotalLetrasxAceptarI, TotalDeudaVencidaI,UsuarioAprobacion,comentarios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RegistrarLog_AprobacionDeudaVencida(int idEmpresa, int codigoUsuario, string id_agenda, string NombreAgenda, string Op,
            decimal TotalLetrasxAceptarSil, decimal TotalDeudaVencidaSil, decimal TotalLetrasxAceptarNeo, decimal TotalDeudaVencidaNeo, 
            decimal TotalLetrasxAceptarIna, decimal TotalDeudaVencidaIna, string comentarios)
        {
            CreditoBL objCreditoBL;
            try
            {
                objCreditoBL = new CreditoBL();
                objCreditoBL.RegistrarLog_AprobacionDeudaVencida(
                    idEmpresa,
                    codigoUsuario,
                        id_agenda,
                        NombreAgenda,
                        Op,
                         TotalLetrasxAceptarSil, TotalDeudaVencidaSil,
                        TotalLetrasxAceptarNeo,TotalDeudaVencidaNeo,
                        TotalLetrasxAceptarIna,TotalDeudaVencidaIna, comentarios
                        );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
