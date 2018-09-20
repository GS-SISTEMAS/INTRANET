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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IAgendaWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IAgendaWCF
    {
        [OperationContract]
        List<gsAgenda_ListarClienteResult> Agenda_ListarCliente(int idEmpresa, int codigoUsuario, string descripcion);
        [OperationContract]
        List<gsAgenda_ListarVendedorResult> Agenda_ListarVendedor(int idEmpresa, int codigoUsuario, string descripcion);
        [OperationContract]
        VBG01134Result Agenda_BuscarCliente(int idEmpresa, int codigoUsuario, string idAgenda, ref decimal? lineaCredito);
        [OperationContract]
        List<VBG00167Result> AgendaAnexo_ListarDireccionCliente(int idEmpresa, int codigoUsuario, string idAgenda);
        //[OperationContract]
        //List<VBG00698Result> Agenda_ListarVendedorTodos(int idEmpresa);
        [OperationContract]
        List<VBG00746Result> AgendaAnexo_ListarAlmacen(int idEmpresa, int codigoUsuario);
        [OperationContract]
        List<VBG03678Result> AgendaAnexo_ListarAlmacenCliente(int idEmpresa, int codigoUsuario, string idAgenda);
        [OperationContract]
        List<VBG02699Result> AgendaAnexoReferencia_ListarPorSucursal(int idEmpresa, int codigoUsuario, int idAgendaAnexo, string idAgenda);
        [OperationContract]
        Agenda_LimiteCreditoBE Agenda_LineaCredito(int idEmpresa, int codigoUsuario, string idAgenda, decimal idMoneda);
        [OperationContract]
        List<gsAgenda_ListarContactoResult> Agenda_ListarContacto(int idEmpresa, int codigoUsuario, string descripcion, int? estado);
        [OperationContract]
        gsAgenda_BucarProveedorResult Agenda_BucarProveedor(int idEmpresa, int codigoUsuario, string idAgenda, ref bool? existe);
        [OperationContract]
        List<gsAgenda_ListarProveedorResult> Agenda_ListarProveedor(int idEmpresa, int codigoUsuario, string descripcion);
        [OperationContract]
        List<gsAgenda_ListarTransportistaResult> Agenda_ListarTransportista(int idEmpresa, int codigoUsuario, string descripcion);
        [OperationContract]
        string Agenda_RegistrarProveedor(int idEmpresa, int codigoUsuario, string nroRUC, string razonSocial);
    }
}
