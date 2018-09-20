using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using GS.SISGEGS.DM;
namespace GS.SISGEGS.BL
{
    public interface IContratosBL {
        List<ReporteGeneralContratosResult> ReporteGeneralContratos(int idAreaResponsable,int idMateria, int idTipo, int idProveedor,int idEstado, DateTime fechaInicio, DateTime fechaFin, DateTime fechaVencIni, DateTime fechaVencFin);
        List<ContratosVencer_ListarResult> ContratosVencer_Listar(int id_Area);
        List<AreaResponsable_ListarResult> AreaResponsable_Listar();
        List<ProveedorContrato_ListarResult> ProveedorContrato_Listar();
        void Contrato_Registrar(int Codigo, int materia, int tipo, int areaResponsable, string renovar, int proveedor, string contratante,
                string fechaSuscripcion, string fechaVencimiento, string objeto, string renovacion, string monto, int estado, int idUsuario);

        void Contrato_Eliminar(int idContrato, int idUsuario);
        Contrato_ObtenerResult Contrato_Obtener(int idContrato);
        void Contrato_Actualizar(int idContrato, int Codigo, int materia, int tipo, int areaResponsable, string renovar, int proveedor, string contratante,
                string fechaSuscripcion, string fechaVencimiento, string objeto, string renovacion, string monto, int estado, int idUsuario);
    }
    public class ContratosBL: IContratosBL
    {
        public List<ReporteGeneralContratosResult> ReporteGeneralContratos(int idAreaResponsable, int idMateria, int idTipo, int idProveedor, int idEstado, DateTime fechaInicio, DateTime fechaFin, DateTime fechaVencIni, DateTime fechaVencFin) {

            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                   
                    return dci.ReporteGeneralContratos(idAreaResponsable, idMateria,idTipo,idProveedor,idEstado,fechaInicio,fechaFin,fechaVencIni, fechaVencFin).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte general de contratos en la base de datos.");
                }
            }
            
        }
        public List<ContratosVencer_ListarResult> ContratosVencer_Listar(int id_Area) {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.ContratosVencer_Listar(id_Area).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte general de contratos por vencer en la base de datos.");
                }
            }
        }
        public List<AreaResponsable_ListarResult> AreaResponsable_Listar() {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.AreaResponsable_Listar().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar las areas responsables de contratos por vencer en la base de datos.");
                }
            }
        }
        public List<ProveedorContrato_ListarResult> ProveedorContrato_Listar() {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.ProveedorContrato_Listar().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar los clientes/proveedores de contratos por vencer en la base de datos.");
                }
            }
        }
        public void Contrato_Registrar(int Codigo,int materia, int tipo, int areaResponsable, string renovar, int proveedor, string contratante,
                string fechaSuscripcion, string fechaVencimiento, string objeto,string renovacion,string monto, int estado,int idUsuario) {

            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                     dci.Contrato_Registrar(Codigo,materia,tipo,areaResponsable,renovar,proveedor,contratante,fechaSuscripcion,
                                            fechaVencimiento,objeto,renovacion,monto,estado,idUsuario);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error, no se pudo registrar el contrato en la base de datos.");
                }
            }

        }
        public void Contrato_Eliminar(int idContrato, int idUsuario) {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dci.Contrato_Eliminar(idContrato, idUsuario);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error, no se pudo eliminar el contrato en la base de datos.");
                }
            }
        }

        public Contrato_ObtenerResult Contrato_Obtener(int idContrato) {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                   return dci.Contrato_Obtener(idContrato).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error, no se pudo eliminar el contrato en la base de datos.");
                }
            }
        }

        public void Contrato_Actualizar(int idContrato, int Codigo, int materia, int tipo, int areaResponsable, string renovar, int proveedor, string contratante,
                string fechaSuscripcion, string fechaVencimiento, string objeto, string renovacion, string monto, int estado, int idUsuario)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dci.Contrato_Actualizar(idContrato,Codigo, materia, tipo, areaResponsable, renovar, proveedor, contratante, fechaSuscripcion,
                                           fechaVencimiento, objeto, renovacion, monto, estado, idUsuario);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error, no se pudo actualizar el contrato en la base de datos.");
                }
            }
        }
    }
}
