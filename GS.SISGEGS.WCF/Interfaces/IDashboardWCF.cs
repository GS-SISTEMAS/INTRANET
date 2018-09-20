using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using GS.SISGEGS.DM;
using GS.SISGEGS.BE;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IDashboardWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IDashboardWCF
    {
        [OperationContract]
        List<gsDocVenta_ReporteVenta_MesResult> DocVenta_ReporteVenta_Mes(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal);
        [OperationContract]
        List<Empresa_ListarDashboardResult> Empresa_ListarDashboard();
        [OperationContract]
        void DocVenta_Top5Vendedor(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, ref List<VendedorTop5BE> lstTop5Venta, ref List<VendedorTop5BE> lstTop5Renta);
        [OperationContract]
        void DocVenta_Top5Cliente(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, ref List<VendedorTop5BE> lstTop5Venta, ref List<VendedorTop5BE> lstTop5Renta);
        [OperationContract]
        void DocVenta_Top5Producto(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, ref List<VendedorTop5BE> lstTop5Venta, ref List<VendedorTop5BE> lstTop5Renta);
        [OperationContract]
        void DocVentaDev_ResumenMensual(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, ref List<NotaCreditoKPIBE> lstKPI);
        [OperationContract]
        void DocVentaDev_KPIMotivo(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, ref List<DocVentaDev_KPIMotivoBE> lstMotivo);
        [OperationContract]
        void Noticia_PublicarDashboard(DateTime fecha, ref List<Noticia_PublicarDashboardResult> lstNoticia, ref List<NoticiaFoto_PublicarDashboardResult> lstNoticiaFoto);
        [OperationContract]
        List<Personal_ListarCumpleanhosResult> Personal_ListarCumpleanhos(DateTime fecha);
        [OperationContract]
        List<Aviso_PublicarDashboardResult> Aviso_PublicarDashboard(DateTime fecha);
        [OperationContract]
        void Rentabilidad_Producto(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, ref List<DocVenta_RentaProdBE> lstRentaProd, ref gsConfigIndicadores_RentabilidadResult objConfigIndRenta);
    }
}
