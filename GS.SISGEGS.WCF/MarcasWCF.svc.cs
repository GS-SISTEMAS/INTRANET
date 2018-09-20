using System;
using System.Collections.Generic;
using GS.SISGEGS.DM;
using GS.SISGEGS.BL;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "MarcasWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione MarcasWCF.svc o MarcasWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class MarcasWCF : IMarcasWCF
    {
        public List<TitularMarca_ListarResult> TitularMarca_Listar() {
            MarcasBL objMarcaBL;
            try
            {
                objMarcaBL = new MarcasBL();
                return objMarcaBL.TitularMarca_Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RegistroMarca_Listar_v2Result> RegistroMarca_Listar(int idEmpresa, string marca, int idTipo, int idPais, int idTitular, DateTime fechaInicio, DateTime fechaFin, string clase, int todo)
        {
            MarcasBL objMarcaBL;
            try
            {
                objMarcaBL = new MarcasBL();
                return objMarcaBL.RegistroMarca_Listar(idEmpresa,marca,idTipo,idPais,idTitular,fechaInicio,fechaFin,clase,todo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoMarca_ListarResult> TipoMarca_Listar() {
            MarcasBL objMarcaBL;
            try
            {
                objMarcaBL = new MarcasBL();
                return objMarcaBL.TipoMarca_Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Pais_ListarResult> Pais_Listar() {
            MarcasBL objMarcaBL;
            try
            {
                objMarcaBL = new MarcasBL();
                return objMarcaBL.Pais_Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstadoMarca_ListarResult> EstadoMarca_Listar() {
            MarcasBL objMarcaBL;
            try
            {
                objMarcaBL = new MarcasBL();
                return objMarcaBL.EstadoMarca_Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Marca_ListarResult> Marca_Listar(string nombreMarca) {
            MarcasBL objMarcaBL;
            try
            {
                objMarcaBL = new MarcasBL();
                return objMarcaBL.Marca_Listar(nombreMarca);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ResponsablesRegistros_ListarResult> ResponsablesRegistros_Listar() {
            MarcasBL objMarcaBL;
            try
            {
                objMarcaBL = new MarcasBL();
                return objMarcaBL.ResponsablesRegistros_Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RegistroMarca_NotificacionResult> RegistroMarca_Notificacion() {
            MarcasBL objMarcaBL;
            try
            {
                objMarcaBL = new MarcasBL();
                return objMarcaBL.RegistroMarca_Notificacion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RegistroMarca_ObtenerResult RegistroMarca_Obtener(int idRegistroMarca){
            MarcasBL objMarcaBL;
            try
            {
                objMarcaBL = new MarcasBL();
                return objMarcaBL.RegistroMarca_Obtener(idRegistroMarca);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RegistroMarca_Registrar(int idMarca, int idEmpresa, string marca, int idPais, int idTipo, int idTitular, string clase, string certificado,
                                    DateTime fechaVencimiento, int idEstadoMarca, string observacion, int usuario)
        {
            MarcasBL objMarcaBL;
            try
            {
                objMarcaBL = new MarcasBL();
                objMarcaBL.RegistroMarca_Registrar(idMarca, idEmpresa, marca, idPais, idTipo, idTitular, clase, certificado, fechaVencimiento, idEstadoMarca, observacion, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RegistroMarcaHistorico_ListarResult> HistoricoMarca_Listar(int idMarca, DateTime FechanVencimientoIni, DateTime FechaVencimientoFin) {
            MarcasBL objMarcaBL;
            try
            {
                objMarcaBL = new MarcasBL();
                return objMarcaBL.HistoricoMarca_Listar(idMarca,FechanVencimientoIni,FechaVencimientoFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DocumentoMarca_Registrar(int idRegistroMarca, int idTipoDocumento, string documento, string ruta, int usuario) {
            MarcasBL objMarcaBL;
            try
            {
                objMarcaBL = new MarcasBL();
                objMarcaBL.DocumentoMarca_Registrar(idRegistroMarca, idTipoDocumento, documento, ruta, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentosMarca_ListarResult> DocumentoMarca_Listar(int idRegistroMarca, int idTipoDocumentoMarca) {
            MarcasBL objMarcaBL;
            try
            {
                objMarcaBL = new MarcasBL();
                return objMarcaBL.DocumentoMarca_Listar(idRegistroMarca, idTipoDocumentoMarca);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DocumentoMarca_Eliminar(int idDocumento, int idRegistroMarca) {
            MarcasBL objMarcaBL;
            try
            {
                objMarcaBL = new MarcasBL();
                objMarcaBL.DocumentoMarca_Eliminar(idDocumento, idRegistroMarca);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoDocumentoMarca_ListarResult> TipoDocumentoMarca_Listar() {
            MarcasBL objMarcaBL;
            try
            {
                objMarcaBL = new MarcasBL();
                return objMarcaBL.TipoDocumentoMarca_Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ClaseMarca_ListarResult> ClaseMarca_Listar()
        {
            MarcasBL objMarcaBL;
            try
            {
                objMarcaBL = new MarcasBL();
                return objMarcaBL.ClaseMarca_Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
