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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "EgresosWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione EgresosWCF.svc o EgresosWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class EgresosWCF : IEgresosWCF
    {
        public void EgresosVariosAprobar_Registrar(int idEmpresa, int codigoUsuario, int Op)
        {
            EgresosBL objEgresosBL = new EgresosBL(); ;
            try
            {
                objEgresosBL.EgresosVariosAprobar_Registrar(idEmpresa, codigoUsuario, Op);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public gsEgresosVariosUsuario_BuscarResult EgresosVariosUsuario_Buscar(int idEmpresa, int codigoUsuario, string ID_Agenda)
        {
            EgresosBL objEgresosBL = new EgresosBL(); ;
            try
            {
                return objEgresosBL.EgresosVariosUsuario_Buscar(idEmpresa, codigoUsuario, ID_Agenda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EgresosVarios_Aprobar(int idEmpresa, int codigoUsuario, decimal ID_Doc)
        {
            EgresosBL objEgresosBL = new EgresosBL(); ;
            try
            {
                objEgresosBL.EgresosVarios_Aprobar(idEmpresa, codigoUsuario, ID_Doc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public gsEgresosVarios_BuscarCabeceraResult EgresosVarios_Buscar(int idEmpresa, int codigoUsuario, int idOperacion, ref bool? bloqueado, ref string mensajeBloqueado, ref List<gsEgresosVarios_BuscarDetalleResult> lstEgresosVarios)
        {
            EgresosBL objEgresosBL = new EgresosBL(); ;
            try
            {
                return objEgresosBL.EgresosVarios_Buscar(idEmpresa, codigoUsuario, idOperacion, ref bloqueado, ref mensajeBloqueado, ref lstEgresosVarios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EgresosVarios_Eliminar(int idEmpresa, int codigoUsuario, int idOperacion)
        {
            EgresosBL objEgresosBL = new EgresosBL(); ;
            try
            {
                objEgresosBL.EgresosVarios_Eliminar(idEmpresa, codigoUsuario, idOperacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsEgresosVarios_ListarCajaChicaResult> EgresosVarios_ListarCajaChica(int idEmpresa, int codigoUsuario, string idAgenda, 
            DateTime fechaInicio, DateTime fechaFinal, bool revisarGastos, string id_proveedor, string serie, decimal numero)
        {
            EgresosBL objEgresosBL = new EgresosBL();
            List<gsEgresosVarios_ListarCajaChicaResult> Lista = new List<gsEgresosVarios_ListarCajaChicaResult>(); 
            try
            {
                Lista = objEgresosBL.EgresosVarios_ListarCajaChica(idEmpresa, codigoUsuario, idAgenda, fechaInicio, fechaFinal, revisarGastos, id_proveedor, serie, numero);

                return Lista; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EgresosVarios_Registrar(int idEmpresa, int codigoUsuario, gsEgresosVarios_BuscarCabeceraResult objEVCabecera, List<gsEgresosVarios_BuscarDetalleResult> lstEVDetalles, DateTime fechaInicio)
        {
            EgresosBL objEgresosBL = new EgresosBL(); ;
            try
            {
                objEgresosBL.EgresosVarios_Registrar(idEmpresa, codigoUsuario, objEVCabecera, lstEVDetalles, fechaInicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VBG03096Result> NaturalezaGasto_ListarImputables(int idEmpresa, int codigoUsuario)
        {
            NaturalezaGastoBL objNaturalezaGastoBL;
            try
            {
                objNaturalezaGastoBL = new NaturalezaGastoBL();
                return objNaturalezaGastoBL.NaturalezaGasto_ListarImputables(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VBG02665Result> UnidadGestion_ListarImputables(int idEmpresa, int codigoUsuario)
        {
            UnidadBL objUnidadBL;
            try
            {
                objUnidadBL = new UnidadBL();
                return objUnidadBL.UnidadGestion_ListarImputables(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VBG02668Result> UnidadProyecto_ListarImputables(int idEmpresa, int codigoUsuario)
        {
            UnidadBL objUnidadBL;
            try
            {
                objUnidadBL = new UnidadBL();
                return objUnidadBL.UnidadProyecto_ListarImputables(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VBG00786Result> CentroCosto_ListarImputables(int idEmpresa, int codigoUsuario)
        {
            CentroCostoBL objCentroCostoBL;
            try
            {
                objCentroCostoBL = new CentroCostoBL();
                return objCentroCostoBL.CentroCosto_ListarImputables(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsDocumento_ListarTipoCompraResult> Documento_ListarDocCompra(int idEmpresa, int codigoUsuario)
        {
            DocumentoBL objDocumentoBL;
            try
            {
                objDocumentoBL = new DocumentoBL();
                return objDocumentoBL.Documento_ListarDocCompra(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VBG01122Result> Documento_ListarEgresosVarios(int idEmpresa, int codigoUsuario)
        {
            DocumentoBL objDocumentoBL;
            try
            {
                objDocumentoBL = new DocumentoBL();
                return objDocumentoBL.Documento_ListarEgresosVarios(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int RegistrarEgresosVarios(int idEmpresa, int codigoUsuario, gsEgresosVariosInt_BuscarCabeceraResult objEVCabecera, List<gsEgresosVariosDInt_BuscarDetalleResult> lstEVDetalles,DateTime fechaInicio) {
            EgresosBL objEgresosBL = new EgresosBL(); ;
            try
            {
                objEVCabecera.Importe = lstEVDetalles.ToList().FindAll(x => x.Estado == 1).AsEnumerable().Sum(x => x.Importe);
                return objEgresosBL.EgresosVarios_RegistrarInt(idEmpresa, codigoUsuario, objEVCabecera, lstEVDetalles, fechaInicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsEgresosVariosInt_ListarCajaChicaResult> EgresosVariosInt_ListarCajaChica(int idEmpresa, int codigoUsuario, string idAgenda,
            DateTime fechaInicio, DateTime fechaFinal, bool revisarGastos, string id_proveedor)
        {
            EgresosBL objEgresosBL = new EgresosBL();
            List<gsEgresosVariosInt_ListarCajaChicaResult> Lista = new List<gsEgresosVariosInt_ListarCajaChicaResult>();
            try
            {
                Lista = objEgresosBL.EgresosVariosInt_ListarCajaChica(idEmpresa, codigoUsuario, idAgenda, fechaInicio, fechaFinal, revisarGastos, id_proveedor);

                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public gsEgresosVariosInt_BuscarCabeceraResult EgresosVariosInt_Buscar(int idEmpresa, int codigoUsuario, int idOperacion, ref bool? bloqueado, ref string mensajeBloqueado, ref List<gsEgresosVariosDInt_BuscarDetalleResult> lstEgresosVarios)
        {
            EgresosBL objEgresosBL = new EgresosBL(); ;
            try
            {
                return objEgresosBL.EgresosVariosInt_Buscar(idEmpresa, codigoUsuario, idOperacion, ref bloqueado, ref mensajeBloqueado, ref lstEgresosVarios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public gsFlujoPermisoEditarResult FlujoPermisoEditar(int idEmpresa, int codigoUsuario, int idPerfil, string idCcosto) {
            EgresosBL objEgresosBL = new EgresosBL(); ;
            try
            {
                return objEgresosBL.FlujoPermisoEditar(idEmpresa, codigoUsuario, idPerfil, idCcosto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AprobarEgresoVariosFlujo(int idEmpresa, int codigoUsuario, int idPerfil, string idCcosto, int Id, char Ok, string Observacion){
            EgresosBL objEgresosBL = new EgresosBL(); ;
            try
            {
                 objEgresosBL.AprobarEgresoVariosFlujo(idEmpresa, codigoUsuario, idPerfil, idCcosto,Id,Ok,Observacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsEgresoCajaFlujoResult> EgresoCajaFlujo(int idEmpresa, int codigoUsuario, int id) {
            EgresosBL objEgresosBL = new EgresosBL();
            List<gsEgresoCajaFlujoResult> Lista = new List<gsEgresoCajaFlujoResult>();
            try
            {
                Lista = objEgresosBL.EgresoCajaFlujo(idEmpresa,codigoUsuario,id); 

                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EgresosVariosInt_Eliminar(int idEmpresa, int codigoUsuario, int id) {
            EgresosBL objEgresosBL = new EgresosBL(); ;
            try
            {
                objEgresosBL.EgresosVariosInt_Eliminar(idEmpresa, codigoUsuario, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsIndicadorEnvioResult> IndicadorEnvio(int idEmpresa, int codigoUsuario) {
            EgresosBL objEgresosBL = new EgresosBL();
            List<gsIndicadorEnvioResult> Lista = new List<gsIndicadorEnvioResult>();
            try
            {
                Lista = objEgresosBL.IndicadorEnvio(idEmpresa, codigoUsuario);

                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EgresosVarios_RegistrarGenesys(int idEmpresa, int codigoUsuario, gsEgresosVarios_BuscarCabeceraResult objEVCabecera, List<gsEgresosVarios_BuscarDetalleResult> lstEVDetalles, DateTime fechaInicio, int id) {
            EgresosBL objEgresosBL = new EgresosBL(); ;
            try
            {
                objEgresosBL.EgresosVarios_RegistrarGenesys(idEmpresa, codigoUsuario, objEVCabecera, lstEVDetalles, fechaInicio,id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DateTime EgresosVariosFechaInicio_Obtener(int idEmpresa, int codigoUsuario, int id)
        {
            EgresosBL objEgresosBL = new EgresosBL(); ;
            try
            {
               return objEgresosBL.EgresosVariosFechaInicio_Obtener(idEmpresa, codigoUsuario, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsEgresosVarios_MaxObservacionesResult> ListarObservaciones_Usuario(int idEmpresa, int codigoUsuario) {
            EgresosBL objEgresosBL = new EgresosBL(); ;
            List<gsEgresosVarios_MaxObservacionesResult> Lista = new List<gsEgresosVarios_MaxObservacionesResult>();
            try
            {
                Lista = objEgresosBL.ListarObservaciones_Usuario(idEmpresa, codigoUsuario);
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsEgresosVarios_DetalleObservacionesResult> ListarDetalleObservaciones_Usuario(int idEmpresa, int codigoUsuario) {
            EgresosBL objEgresosBL = new EgresosBL(); ;
            List<gsEgresosVarios_DetalleObservacionesResult> Lista = new List<gsEgresosVarios_DetalleObservacionesResult>();
            try
            {
                Lista = objEgresosBL.ListarDetalleObservaciones_Usuario(idEmpresa, codigoUsuario);
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsIndicadorNoEnvioResult> ListarIndicadorNoEnvio(int idEmpresa, int codigoUsuario) {
            EgresosBL objEgresosBL = new EgresosBL(); ;
            List<gsIndicadorNoEnvioResult> Lista = new List<gsIndicadorNoEnvioResult>();
            try
            {
                Lista = objEgresosBL.ListarIndicadorNoEnvio(idEmpresa, codigoUsuario);
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
