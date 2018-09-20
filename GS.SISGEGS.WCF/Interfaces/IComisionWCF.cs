using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IComisionWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IComisionWCF
    {
        [OperationContract]
        List<gsReporteCanceladosResult> Reporte_Cancelaciones(int idEmpresa, int codigoUsuario, string codAgenda, string codVendedor, DateTime fechaInicial, DateTime fechaFinal);
        [OperationContract]
        List<Personal_ListarTotalResult> Personal_ListarTotal(int idEmpresa, int codigoUsuario, string codigoEmpresa, string codigoCargo, string texto, int reporte, int id_zona);
        [OperationContract]
        List<Personal_BuscarResult> Personal_Buscar(int idEmpresa, int codigoUsuario, string codigoEmpresa, string codigoCargo, string texto);
        [OperationContract]
        void Personal_Registrar(int idPersonal, string nroDocumento, string imageURL, int idUsuarioRegistro, string codigoEmpresa, string codigoCargo, decimal porcentaje, int estado, int reporte); 
        [OperationContract]
        List<ZonaPersonal_ListarResult> ZonaPersonal_Listar(int idEmpresa, int codigoUsuario, string nroDocumento, int id_zona, string texto);

        [OperationContract]
        List<gsZonas_ListarResult> Zonas_Listar(int idEmpresa, int codigoUsuario, string nroDocumento, int id_zona);

        [OperationContract]
        void Zona_Registrar(int idEmpresa, int id_zona, string nroDocumento, int idUsuarioRegistro, string codigoCargo, decimal porcentaje, int estado);
        [OperationContract]
        List<gsZonasComision_ListarResult> ZonasComision_Listar(int idEmpresa, int codigoUsuario);
        [OperationContract]
        void ZonasComision_Insert(int idEmpresa, int id_zona, int idUsuarioRegistro, decimal porcentaje, bool estado);
        [OperationContract]
        List<combo_CargosRHResult> CargoRH_Listar(string idempresa);
        [OperationContract]
        List<gsComisiones_VendedoresResult> gsComisiones_Vendedores(int idEmpresa, int codigoUsuario, int anho, int mes);
        [OperationContract]
        List<gsComisiones_JefaturasResult> gsComisiones_Jefaturas(int idEmpresa, int codigoUsuario, int anho, int mes);
        [OperationContract]
        List<combo_ReporteResult> combo_Reporte();
        [OperationContract]
        List<Reportes_ListarResult> Reportes_Listar(string idempresa);

        [OperationContract]
        List<Promotores_ListarResult> Promotores_Listar(int idEmpresa, int codigoUsuario, int id_zona, int reporte);

        [OperationContract]
        void gsAgenda_UpdateZona(int idEmpresa, int codigoUsuario, string id_agenda, int id_zona);
        [OperationContract]
        List<gsComisiones_PromotoresResult> gsComisiones_Promotores(int idEmpresa, int codigoUsuario, int anho, int mes);
        [OperationContract]
        List<gsComisiones_SemillasResult> gsComisiones_Semillas(int idEmpresa, int codigoUsuario, int anho, int mes);

        [OperationContract]
        List<gsPeriodoComision_ListarResult> PeriodoComision_Listar(int anho);
    }
}
