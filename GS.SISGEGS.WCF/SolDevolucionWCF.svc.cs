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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "SolDevolucionWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione SolDevolucionWCF.svc o SolDevolucionWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class SolDevolucionWCF : ISolDevolucionWCF
    {
        public List<gsAgendaAnexo_ListarAlmacenDevolucionResult> AgendaAnexo_ListarAlmacenDevolucion(int idEmpresa, int codigoUsuario)
        {
            AgendaBL objAgendaBL = new AgendaBL();
            try
            {
                return objAgendaBL.AgendaAnexo_ListarAlmacenDevolucion(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsDevolucionMotivo_ComboBoxResult> DevolucionMotivo_ComboBox(int idEmpresa, int codigoUsuario)
        {
            DevolucionBL objDevolucionBL = new DevolucionBL();
            try
            {
                return objDevolucionBL.DevolucionMotivo_ComboBox(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DevolucionSolicitud_Aprobar(int idEmpresa, int codigoUsuario, int idDevolucionSolicitud)
        {
            DevolucionBL objDevolucionBL = new DevolucionBL();
            try
            {
                objDevolucionBL.DevolucionSolicitud_Aprobar(idEmpresa, codigoUsuario, idDevolucionSolicitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public gsDevolucionSolicitud_BuscarResult DevolucionSolicitud_Buscar(int idEmpresa, int codigoUsuario, int idDevolucionSolicitud, ref List<gsDevolucionSolicitudDetalle_BuscarResult> lstProductos)
        {
            DevolucionBL objDevolucionBL = new DevolucionBL();
            try
            {
                return objDevolucionBL.DevolucionSolicitud_Buscar(idEmpresa, codigoUsuario, idDevolucionSolicitud, ref lstProductos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DevolucionSolicitud_Eliminar(int idEmpresa, int codigoUsuario, int idDevolucionSolicitud)
        {
            DevolucionBL objDevolucionBL = new DevolucionBL();
            try
            {
                objDevolucionBL.DevolucionSolicitud_Eliminar(idEmpresa, codigoUsuario, idDevolucionSolicitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsDevolucionSolicitud_ListarResult> DevolucionSolicitud_Listar(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, string ID_Vendedor, string ID_Cliente)
        {
            DevolucionBL objDevolucionBL = new DevolucionBL();
            try
            {
                return objDevolucionBL.DevolucionSolicitud_Listar(idEmpresa, codigoUsuario, fechaInicio, fechaFinal, ID_Vendedor, ID_Cliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DevolucionSolicitud_Registrar(int idEmpresa, int codigoUsuario, gsDevolucionSolicitud objDevolucionSolicitud, List<gsDevolucionSolicitudDetalle> lstProductos)
        {
            DevolucionBL objDevolucionBL = new DevolucionBL();
            try
            {
                objDevolucionBL.DevolucionSolicitud_Registrar(idEmpresa, codigoUsuario, objDevolucionSolicitud, lstProductos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public gsDocVenta_BuscarResult DocVenta_Buscar(int idEmpresa, int codigoUsuario, decimal Op, ref List<gsDocVenta_BuscarDetalleResult> lstDetalle)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.DocVenta_Buscar(idEmpresa, codigoUsuario, Op, ref lstDetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsDocVenta_ListarResult> DocVenta_Listar(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, string ID_Vendedor)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.DocVenta_Listar(idEmpresa, codigoUsuario, fechaInicio, fechaFinal, ID_Vendedor);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
