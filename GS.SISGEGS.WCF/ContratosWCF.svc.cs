using System;
using System.Collections.Generic;
using GS.SISGEGS.DM;
using GS.SISGEGS.BL;
namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ContratosWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ContratosWCF.svc o ContratosWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ContratosWCF : IContratosWCF
    {
        public List<ReporteGeneralContratosResult> ReporteGeneralContratos(int idAreaResponsable, int idMateria, int idTipo, int idProveedor, int idEstado, DateTime fechaInicio, DateTime fechaFin, DateTime fechaVencIni, DateTime fechaVencFin) {

            ContratosBL objContratoBL;
            try
            {
                objContratoBL = new ContratosBL();
                return objContratoBL.ReporteGeneralContratos(idAreaResponsable, idMateria,idTipo,idProveedor,idEstado,fechaInicio,fechaFin, fechaVencIni, fechaVencFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ContratosVencer_ListarResult> ContratosVencer_Listar(int id_Area) {
            ContratosBL objContratoBL;
            try
            {
                objContratoBL = new ContratosBL();
                return objContratoBL.ContratosVencer_Listar(id_Area);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public List<AreaResponsable_ListarResult> AreaResponsable_Listar() {
            ContratosBL objContratoBL;
            try
            {
                objContratoBL = new ContratosBL();
                return objContratoBL.AreaResponsable_Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProveedorContrato_ListarResult> ProveedorContrato_Listar()
        {
            ContratosBL objContratoBL;
            try
            {
                objContratoBL = new ContratosBL();
                return objContratoBL.ProveedorContrato_Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Contrato_Registrar(int Codigo, int materia, int tipo, int areaResponsable, string renovar, int proveedor, string contratante,
                string fechaSuscripcion, string fechaVencimiento, string objeto, string renovacion, string monto, int estado, int idUsuario)
        {

            ContratosBL objContratoBL;
            try
            {
                objContratoBL = new ContratosBL();
                 objContratoBL.Contrato_Registrar(Codigo, materia, tipo, areaResponsable, renovar, proveedor, contratante, fechaSuscripcion,
                                            fechaVencimiento, objeto, renovacion, monto, estado, idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Contrato_Eliminar(int idContrato, int idUsuario) {
            ContratosBL objContratoBL;
            try
            {
                objContratoBL = new ContratosBL();
                objContratoBL.Contrato_Eliminar(idContrato, idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Contrato_ObtenerResult Contrato_Obtener(int idContrato) {
            ContratosBL objContratoBL;
            try
            {
                objContratoBL = new ContratosBL();
               return objContratoBL.Contrato_Obtener(idContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Contrato_Actualizar(int idContrato, int Codigo, int materia, int tipo, int areaResponsable, string renovar, int proveedor, string contratante,
                string fechaSuscripcion, string fechaVencimiento, string objeto, string renovacion, string monto, int estado, int idUsuario)
        {
            ContratosBL objContratoBL;
            try
            {
                objContratoBL = new ContratosBL();
                objContratoBL.Contrato_Actualizar(idContrato,Codigo, materia, tipo, areaResponsable, renovar, proveedor, contratante, fechaSuscripcion,
                                           fechaVencimiento, objeto, renovacion, monto, estado, idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
