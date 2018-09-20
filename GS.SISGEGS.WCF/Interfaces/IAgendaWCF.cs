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
        List<gsAgenda_ListarClienteResult> Agenda_ListarCliente(int idEmpresa, int codigoUsuario, string descripcion, int? estado);
        [OperationContract]
        List<gsAgenda_ListarVendedorResult> Agenda_ListarVendedor(int idEmpresa, int codigoUsuario, string descripcion);
        [OperationContract]
        List<gsVendedores_ListarResult> Agenda_ListarVendedores(int idEmpresa, int codigoUsuario);
        [OperationContract]
        List<gsClientesXVendedor_ListarResult> Agenda_ListarClientes(int idEmpresa, int codigoUsuario, string idVendedor);
        [OperationContract]
        VBG01134Result Agenda_BuscarCliente(int idEmpresa, int codigoUsuario, string idAgenda, ref decimal? lineaCredito,
            ref DateTime? fechaVencimiento, ref decimal? TC);
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

        [OperationContract]
        List<gsChofer_ListarResult> Agenda_ListarChofer(int idEmpresa, int codigoUsuario, string Id_Transportista, string Id_Chofer, string descripcion);

        [OperationContract]
        List<gsPlaca_ListarResult> Agenda_ListarPlaca(int idEmpresa, int codigoUsuario, string Id_Transportista, string Id_Vehiculo, string descripcion);

        [OperationContract]
        List<gsAgenda_BuscarClienteDetalleResult> Agenda_BuscarClienteDetalle(int idEmpresa, int codigoUsuario, string Id_Agenda);

        [OperationContract]
        List<gsPlaca_DespachoResult> Agenda_ListarPlaca_Despacho(int idEmpresa, int codigoUsuario, string Id_Transportista, string Id_Vehiculo, string descripcion, string despacho);
        [OperationContract]
        List<gsZonaSectorista_ListarResult> Agenda_ListarZonaSectorista(int idEmpresa, int codigoUsuario, string id_sectorista);
        [OperationContract]
        List<Agenda_BuscarEmpresaResult> Agenda_BuscarEmpresa(int idEmpresa, int codigoUsuario, string idAgenda);
        [OperationContract]

        List<gsUsuario_SectoristaResult> Agenda_ListarSectorista(int idEmpresa, int codigoUsuario, string descripcion, int? estado);
        [OperationContract]
        List<gsZona_ListarResult> Agenda_ListarZona(int idEmpresa, int codigoUsuario, int id_zona);

        [OperationContract]
        List<gsVendedor_ListarResult> Agenda_ListarVendedorProyectado(int idEmpresa, int codigoUsuario, int id_zona, string id_vendedor);

        [OperationContract]
        List<gsClientesCorreo_EnvioResult> Agenda_ListarCorreos(int idEmpresa, int codigoUsuario, int id_zona, string id_vendedor);

        // PSF 07/12/2016
        [OperationContract]
        List<gsAgenda_ListarGarantiaResult> Agenda_ListarGarantia(int idEmpresa, int codigoUsuario, int id_zona, string id_vendedor);

        [OperationContract]
        VBG01134_validarCorreoResult Agenda_ValidarCorreo(int idEmpresa, int codigoUsuario, string idAgenda, ref bool? existeCliente, ref bool? existeCorreo);
        [OperationContract]
        void Agenda_RegistrarCorreo(int idEmpresa, int codigoUsuario, string idAgenda, string Correo, ref int? Correlativo);

        [OperationContract]
        VBG01134Result Agenda_BuscarCliente_Contado(int idEmpresa, int codigoUsuario, string idAgenda, ref decimal? lineaCredito,
    ref DateTime? fechaVencimiento, int idMoneda);

        [OperationContract]
        RPT00015Result Agenda_TipoCambio(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int ID_Moneda);

        [OperationContract]
        List<gsListarObservacionesAgendaResult> ListarObservacionesAgenda(int idEmpresa, int codigoUsuario, string idAgenda);

        [OperationContract]
        List<gsListarLogLineaCreditoResult> ListarLogLineaCredito(int idEmpresa, int codigoUsuario, string idAgenda);

        [OperationContract]
        List<GS_RecuperaCorreoAgendaResult> RecuperaCorreoAgenda(int idEmpresa, int codigoUsuario, string idAgenda);

        [OperationContract]
        List<gsAgenda_ContactoResult> Agenda_ListarContacto_Estado(int idEmpresa, int codigoUsuario, string idAgenda);

        [OperationContract]
        List<gsAgenda_ListarClienteAgenteResult> Agenda_ListarClienteAgente(int idEmpresa, int codigoUsuario, string descripcion, int? estado);

        [OperationContract]
        List<gsVendedorZona_ListarResult> Agenda_VendedorZonaListar(int idEmpresa, int codigoUsuario, int id_zona, string id_vendedor);
    }
}
