using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IEgresosWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IEgresosWCF
    {
        [OperationContract]
        List<gsEgresosVarios_ListarCajaChicaResult> EgresosVarios_ListarCajaChica(int idEmpresa, int codigoUsuario, string idAgenda, 
            DateTime fechaInicio, DateTime fechaFinal, bool revisarGastos, string id_proveedor, string serie, decimal numero);
        [OperationContract]
        gsEgresosVarios_BuscarCabeceraResult EgresosVarios_Buscar(int idEmpresa, int codigoUsuario, int idOperacion, ref bool? bloqueado, ref string mensajeBloqueado, ref List<gsEgresosVarios_BuscarDetalleResult> lstEgresosVarios);
        [OperationContract]
        void EgresosVarios_Registrar(int idEmpresa, int codigoUsuario, gsEgresosVarios_BuscarCabeceraResult objEVCabecera, List<gsEgresosVarios_BuscarDetalleResult> lstEVDetalles, DateTime fechaInicio);
        [OperationContract]
        void EgresosVarios_Eliminar(int idEmpresa, int codigoUsuario, int idOperacion);
        [OperationContract]
        gsEgresosVariosUsuario_BuscarResult EgresosVariosUsuario_Buscar(int idEmpresa, int codigoUsuario, string ID_Agenda);
        [OperationContract]
        void EgresosVariosAprobar_Registrar(int idEmpresa, int codigoUsuario, int Op);
        [OperationContract]
        void EgresosVarios_Aprobar(int idEmpresa, int codigoUsuario, decimal ID_Doc);
        [OperationContract]
        List<VBG03096Result> NaturalezaGasto_ListarImputables(int idEmpresa, int codigoUsuario);
        [OperationContract]
        List<VBG02665Result> UnidadGestion_ListarImputables(int idEmpresa, int codigoUsuario); 
        [OperationContract]
        List<VBG02668Result> UnidadProyecto_ListarImputables(int idEmpresa, int codigoUsuario);
        [OperationContract]
        List<VBG00786Result> CentroCosto_ListarImputables(int idEmpresa, int codigoUsuario);
        [OperationContract]
        List<gsDocumento_ListarTipoCompraResult> Documento_ListarDocCompra(int idEmpresa, int codigoUsuario);
        [OperationContract]        
        List<VBG01122Result> Documento_ListarEgresosVarios(int idEmpresa, int codigoUsuario);
        [OperationContract]
        int RegistrarEgresosVarios(int idEmpresa, int codigoUsuario, gsEgresosVariosInt_BuscarCabeceraResult objEVCabecera, List<gsEgresosVariosDInt_BuscarDetalleResult> lstEVDetalles, DateTime fechaInicio); //
        [OperationContract]
        List<gsEgresosVariosInt_ListarCajaChicaResult> EgresosVariosInt_ListarCajaChica(int idEmpresa, int codigoUsuario, string idAgenda,
           DateTime fechaInicio, DateTime fechaFinal, bool revisarGastos, string id_proveedor);

        [OperationContract]
        gsEgresosVariosInt_BuscarCabeceraResult EgresosVariosInt_Buscar(int idEmpresa, int codigoUsuario, int idOperacion, ref bool? bloqueado, ref string mensajeBloqueado, ref List<gsEgresosVariosDInt_BuscarDetalleResult> lstEgresosVarios);

        [OperationContract]
        gsFlujoPermisoEditarResult FlujoPermisoEditar(int idEmpresa, int codigoUsuario, int idPerfil, string idCcosto);
        [OperationContract]
        void AprobarEgresoVariosFlujo(int idEmpresa, int codigoUsuario, int idPerfil, string idCcosto, int Id, char Ok, string Observacion);
        [OperationContract]
        List<gsEgresoCajaFlujoResult> EgresoCajaFlujo(int idEmpresa, int codigoUsuario, int id);
        [OperationContract]
        void EgresosVariosInt_Eliminar(int idEmpresa, int codigoUsuario, int id);
        [OperationContract]
        List<gsIndicadorEnvioResult> IndicadorEnvio(int idEmpresa, int codigoUsuario);
        [OperationContract]
        void EgresosVarios_RegistrarGenesys(int idEmpresa, int codigoUsuario, gsEgresosVarios_BuscarCabeceraResult objEVCabecera, List<gsEgresosVarios_BuscarDetalleResult> lstEVDetalles, DateTime fechaInicio, int id);
        [OperationContract]
        DateTime EgresosVariosFechaInicio_Obtener(int idEmpresa, int codigoUsuario, int id);
        [OperationContract]
        List<gsEgresosVarios_MaxObservacionesResult> ListarObservaciones_Usuario(int idEmpresa, int codigoUsuario);
        [OperationContract]
        List<gsEgresosVarios_DetalleObservacionesResult> ListarDetalleObservaciones_Usuario(int idEmpresa, int codigoUsuario);
        [OperationContract]
        List<gsIndicadorNoEnvioResult> ListarIndicadorNoEnvio(int idEmpresa, int codigoUsuario);
    }
}
