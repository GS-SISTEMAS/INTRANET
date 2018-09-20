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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IOrdenCompraWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IOrdenCompraWCF
    {
        //[OperationContract]
        //List<gsOV_ListarResult> OrdenVenta_Listar(int idEmpresa, int codigoUsuario, string ID_Agenda,
        //    DateTime fechaDesde, DateTime fechaHasta, string ID_Vendedor, bool modificarPedido);
        //[OperationContract]
        //void OrdenVenta_Eliminar(int idEmpresa, int codigoUsuario, int idOperacion);
        ////[OperationContract]
        ////void OrdenVenta_Registrar(int idEmpresa, int codigoUsuario, gsOV_BuscarCabeceraResult objOrdenVentaCabBE,
        ////    List<gsOV_BuscarDetalleResult> lstProductos, List<GlosaBE> lstImpuestos, decimal? idOperacion, decimal limiteCredito,
        ////    DateTime fechaVecimiento);
        //[OperationContract]
        //gsOV_BuscarCabeceraResult OrdenVenta_Buscar(int idEmpresa, int codigoUsuario, int idPedido,
        //    ref List<gsOV_BuscarDetalleResult> objOrdenVentaDet, ref List<gsOV_BuscarImpuestoResult> objOrdenVentaImp,
        //    ref bool? bloqueado, ref string mensajeBloqueado);
        //[OperationContract]
        //List<VBG03630Result> OrdenVenta_ListarTipo(int idEmpresa, int codigoUsuario);
        //[OperationContract]
        //void OV_TransGratuitas_Aprobar(int idEmpresa, int codigoUsuario, int Op, ref string mensajeError);
        //[OperationContract]
        //List<gsOV_Listar_SectoristaResult> OrdenVenta_Listar_Sectorista(int idEmpresa, int codigoUsuario, string ID_Agenda, DateTime fechaDesde, DateTime fechaHasta, string ID_Vendedor, bool modificarPedido, string id_Sectorista);

        [OperationContract]
        List<VBG00536XResult> OrdenCompraListar(int idEmpresa, int codigoUsuario, string ID_Agenda, DateTime FechaDesde, DateTime FechaHasta,
         int EstadoAprobacion);

        [OperationContract]
        void Anular_OC(int idEmpresa, int codigoUsuario, int OP);


        [OperationContract]
        List<USP_Sel_OCResult> ListarOcImportacion(int idEmpresa, int codigoUsuario, DateTime fechainicial, DateTime fechafinal, string nombreproveedor);

        [OperationContract]
        List<USP_Sel_OC_OpResult> Seleccionar_OC_OP(int idEmpresa, int codigoUsuario, int op);

        [OperationContract]
        List<USP_Sel_OC_OpLineaResult> Seleccionar_OC_OPLinea(int idEmpresa, int codigoUsuario, int op);

        [OperationContract]
        List<USP_Sel_OC_OpParcialResult> Seleccionar_OC_OpParcial(int idEmpresa, int codigoUsuario, int op);

        [OperationContract]
        void Registrar_Oc_Parcial(int idEmpresa, int codigoUsuario, List<USP_Sel_Genesys_OC_ImpResult> CabOcParcial, List<USP_Sel_Genesys_OC_ImpLineaResult> DetOcparcial);

        [OperationContract]
        void Eliminar_Oc_Parcial(int idEmpresa, int codigoUsuario, int op_oc, string No_RegistroParcial);

        [OperationContract]
        void Eliminar_Oc_ParcialLinea(int idEmpresa, int codigoUsuario, int id, int op_oc, string No_RegistroParcial);



        [OperationContract]
        List<USP_Sel_Genesys_Oc_SegImpResult> Seleccionar_GenesysOC_SeguimientoLista(int idEmpresa, int codigoUsuario,
            DateTime fechainicial, DateTime fechafinal, string nombreproveedor, string estado, DateTime? fechaingresoini, DateTime? fechaingresofin);
        [OperationContract]
        List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult> Seleccionar_GenesysOC_ImpParciales(int idEmpresa, int codigoUsuario,
            DateTime fechainicial, DateTime fechafinal, string nombreproveedor, Int32 Id_SegImp);

        [OperationContract]
        List<USP_Sel_Genesys_OC_EstadoResult> Seleccionar_GenesysOC_Estados(int idEmpresa, int codigoUsuario);

        [OperationContract]
        List<USP_Sel_Genesys_OC_TipoViaResult> Seleccionar_GenesysOC_TipoVia(int idEmpresa, int codigoUsuario);

        [OperationContract]
        List<USP_Sel_Genesys_Oc_SegImp_IdSegResult> Seleccionar_GenesysOC_SegImp_IdSeg(int idEmpresa, int codigoUsuario, int idSeg);

        [OperationContract]
        List<USP_Sel_Genesys_OC_Imp_SeleccionarOC_IdSegResult> Seleccionar_GenesysOC_OcImp_IdSeg(int idEmpresa, int codigoUsuario, int idSeg);

        [OperationContract]
        void Registrar_Seguimiento(int idEmpresa, int codigoUsuario, USP_Sel_Genesys_OC_ImpSegEntidadResult CabSeguimiento, List<OrdenCompraSeguimientoBE> DetSeguimiento, ref decimal? Id_SegImp);

        [OperationContract]
        void Eliminar_OcImp_Seguimiento(int idEmpresa, int codigoUsuario, Int32 id_seguimiento, string No_RegistroParcial, Int32 Op_OC);

        [OperationContract]
        void Registrar_OcImpSeg_Liquidacion(int idEmpresa, int codigoUsuario, Int32 id_seguimiento);

        [OperationContract]
        void DocumentosSegImportacion_Registrar(int idEmpresa, int codigoUsuario, Int32 id_seguimiento, string documento, string ruta);
        [OperationContract]
        void DocumentosSegImportacion_Eliminar(int idEmpresa, int codigoUsuario, Int32 id_seguimiento, string documento);
        [OperationContract]
        List<USP_SEL_DocumentosSegImportacionResult> DocumentosSegImportacion_Seleccionar(int idEmpresa, int codigoUsuario, Int32 id_seguimiento);

    }
}
