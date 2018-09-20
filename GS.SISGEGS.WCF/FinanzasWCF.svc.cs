using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GS.SISGEGS.BL;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "FinanzasWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione FinanzasWCF.svc o FinanzasWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class FinanzasWCF : IFinanzasWCF
    {
        public List<GS_ReporteDetraccionesResult> ReporteDetracciones(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, string idAgenda, int estatus)
        {
            ReportesBL objReportesBL;
            try
            {
                List<GS_ReporteDetraccionesResult> list = new List<GS_ReporteDetraccionesResult>();

                objReportesBL = new ReportesBL();
                list = objReportesBL.ReporteDetracciones(idEmpresa, codigoUsuario, fechaInicio, fechaFinal, idAgenda, estatus);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Accion_Registrar(int idEmpresa, int codigoUsuario, GS_DetraccionAccionGetResult objAccionInsert)
        {
            ReportesBL objReportesBL;
            try
            {
                objReportesBL = new ReportesBL();
                objReportesBL.Accion_Registrar(idEmpresa, codigoUsuario, objAccionInsert);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GS_DetraccionAccionGetResult GetDetraccionAccion(int idEmpresa, int codigoUsuario, int op)
        {
            ReportesBL objReportesBL;
            try
            {
                List<GS_DetraccionAccionGetResult> list = new List<GS_DetraccionAccionGetResult>();

                objReportesBL = new ReportesBL();
                list = objReportesBL.GetDetraccionAccion(idEmpresa, codigoUsuario, op);
                var result = new GS_DetraccionAccionGetResult();
                if (list.Any())
                    result = list.FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GS_GetVoucherDetraccionesResult> GetVoucherDetraccione(int idEmpresa, int codigoUsuario, int op)
        {
            ReportesBL objReportesBL;
            try
            {
                List<GS_GetVoucherDetraccionesResult> list = new List<GS_GetVoucherDetraccionesResult>();

                objReportesBL = new ReportesBL();
                list = objReportesBL.GetVoucherDetraccione(idEmpresa, codigoUsuario, op);

                return list;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
