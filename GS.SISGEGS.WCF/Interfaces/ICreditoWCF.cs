using GS.SISGEGS.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ICreditoWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ICreditoWCF
    {
        [OperationContract]
        List<gsCredito_ListarCondicionResult> Credito_ListarCondicion(int idEmpresa, int codigoUsuario, string idAgenda);

        [OperationContract]
        List<USP_SEL_LetrasSinCanjeV2Result> ObtenerLetrasSinCanje(int idEmpresa, int codigoUsuario, int? estado, DateTime FechaEmisionDesde, DateTime FechaEmisionHasta, int dias, string zona);

        [OperationContract]
        void ObtenerVerificacionAprobacion2(int idEmpresa, int codigoUsuario, string id_agenda, ref List<USP_SEL_Verificacion_DeudaVencidaResult> lstdeuda, ref List<USP_SEL_Verificacion_LetrasxAceptarResult> lstletras);

        [OperationContract]
        void Enviar_Notificacion_Aprobacion2(int idEmpresa, int codigoUsuario, string id_agenda, string NombreAgenda, string OpPedido,
            decimal TotalLetrasxAceptarS, decimal TotalDeudaVencidaS, decimal TotalLetrasxAceptarN, decimal TotalDeudaVencidaN,
            decimal TotalLetrasxAceptarI, decimal TotalDeudaVencidaI, string UsuarioAprobacion, string comentarios);

        [OperationContract]
        void RegistrarLog_AprobacionDeudaVencida(int idEmpresa, int codigoUsuario, string id_agenda, string NombreAgenda, string Op,
            decimal TotalLetrasxAceptarSil, decimal TotalDeudaVencidaSil, decimal TotalLetrasxAceptarNeo, decimal TotalDeudaVencidaNeo, 
            decimal TotalLetrasxAceptarIna, decimal TotalDeudaVencidaIna,string comentarios);
    }
}
