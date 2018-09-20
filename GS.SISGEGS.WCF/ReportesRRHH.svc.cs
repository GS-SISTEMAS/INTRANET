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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ReportesRRHH" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ReportesRRHH.svc o ReportesRRHH.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ReportesRRHH : IReportesRRHH
    {
        public List<Ingreso_PersonalResult> Ingreso_Personal(DateTime fecha)
        {
            RRHHBL objRRHHBL = new RRHHBL();
            try
            {
                return objRRHHBL.Ingreso_Personal(fecha);
            }
            catch (Exception ex) { throw ex; }
        }

        public List<Ingreso_Personal_DetalleResult> Ingreso_PersonalDetalle(DateTime fecha, string ccosto, ref List<Ingreso_Personal_PermisosResult> lstpermiso)
        {
            RRHHBL objRRHHBL = new RRHHBL();
            try
            {
                lstpermiso = objRRHHBL.Ingreso_PersonalPermisos(fecha, ccosto);
                return objRRHHBL.Ingreso_PersonalDetalle(fecha, ccosto);
            }
            catch (Exception ex) { throw ex; }
        }

        public List<Personal_ListarResult> Personal_Listar(string codEmpresa, string texto)
        {
            RRHHBL objRRHHBL = new RRHHBL();
            try
            {
                return objRRHHBL.Personal_Listar(codEmpresa, texto);
            }
            catch (Exception ex) { throw ex; }
        }

        public void Personal_Registrar(int idPersonal, string nroDocumento, string imageURL, int idUsuarioRegistro)
        {
            RRHHBL objRRHHBL = new RRHHBL();
            try
            {
                objRRHHBL.Personal_Registrar(idPersonal, nroDocumento, imageURL, idUsuarioRegistro);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
