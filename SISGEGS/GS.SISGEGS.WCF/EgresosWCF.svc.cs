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

        public List<gsEgresosVarios_ListarCajaChicaResult> EgresosVarios_ListarCajaChica(int idEmpresa, int codigoUsuario, string idAgenda, DateTime fechaInicio, DateTime fechaFinal)
        {
            EgresosBL objEgresosBL = new EgresosBL(); ;
            try
            {
                return objEgresosBL.EgresosVarios_ListarCajaChica(idEmpresa, codigoUsuario, idAgenda, fechaInicio, fechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EgresosVarios_Registrar(int idEmpresa, int codigoUsuario, gsEgresosVarios_BuscarCabeceraResult objEVCabecera, List<gsEgresosVarios_BuscarDetalleResult> lstEVDetalles)
        {
            EgresosBL objEgresosBL = new EgresosBL(); ;
            try
            {
                objEgresosBL.EgresosVarios_Registrar(idEmpresa, codigoUsuario, objEVCabecera, lstEVDetalles);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
