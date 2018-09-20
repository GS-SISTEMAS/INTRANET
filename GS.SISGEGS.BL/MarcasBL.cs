using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GS.SISGEGS.DM;
using System.Configuration;
namespace GS.SISGEGS.BL
{
    public interface IMarcasBL {
        List<TitularMarca_ListarResult> TitularMarca_Listar();
        List<RegistroMarca_ListarResult> RegistroMarca_Listar(int idEmpresa, string marca, int idTipo, int idPais, int idTitular, DateTime fechaInicio, DateTime fechaFin, string clase, int todo);
        List<TipoMarca_ListarResult> TipoMarca_Listar();
        List<Pais_ListarResult> Pais_Listar();
        List<Marca_ListarResult> Marca_Listar(string nombreMarca);
        List<ResponsablesRegistros_ListarResult> ResponsablesRegistros_Listar();
        List<RegistroMarca_NotificacionResult> RegistroMarca_Notificacion();
        List<EstadoMarca_ListarResult> EstadoMarca_Listar();
        RegistroMarca_ObtenerResult RegistroMarca_Obtener(int idRegistroMarca);
        void RegistroMarca_Registrar(int idMarca, int idEmpresa, string marca, int idPais, int idTipo, int idTitular, string clase, string certificado, 
                                    DateTime fechaVencimiento, int idEstadoMarca, string observacion, int usuario);

        List<RegistroMarcaHistorico_ListarResult> HistoricoMarca_Listar(int idMarca, DateTime FechanVencimientoIni, DateTime FechaVencimientoFin);

        void DocumentoMarca_Registrar(int idRegistroMarca, int idTipoDocumento, string documento, string ruta, int usuario);

        List<DocumentosMarca_ListarResult> DocumentoMarca_Listar(int idRegistroMarca, int idTipoDocumentoMarca);

        void DocumentoMarca_Eliminar (int idDocumento, int idRegistroMarca);

        List<TipoDocumentoMarca_ListarResult> TipoDocumentoMarca_Listar();
        List<ClaseMarca_ListarResult> ClaseMarca_Listar();

    }
    public class MarcasBL
    {
        public List<TitularMarca_ListarResult> TitularMarca_Listar() {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.TitularMarca_Listar().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar titulares del registro de marcas en la base de datos.");
                }
            }
        }

        public List<RegistroMarca_Listar_v2Result> RegistroMarca_Listar(int idEmpresa, string marca, int idTipo, int idPais, int idTitular, DateTime fechaInicio, DateTime fechaFin, string clase, int todos)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.RegistroMarca_Listar_v2(idEmpresa,marca,idTipo,idPais,idTitular,fechaInicio,fechaFin,clase,todos ).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte general de registro de marcas en la base de datos.");
                }
            }
        }

        public List<TipoMarca_ListarResult> TipoMarca_Listar() {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.TipoMarca_Listar().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar los tipos de marca en la base de datos.");
                }
            }
        }

        public List<Pais_ListarResult> Pais_Listar() {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.Pais_Listar().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar los paises en la base de datos.");
                }
            }
        }

        public List<Marca_ListarResult> Marca_Listar(string nombreMarca) {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.Marca_Listar(nombreMarca).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar las marcas en la base de datos.");
                }
            }
        }

        public List<ResponsablesRegistros_ListarResult> ResponsablesRegistros_Listar() {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.ResponsablesRegistros_Listar().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar los responsables de registros en la base de datos.");
                }
            }
        }

        public List<RegistroMarca_NotificacionResult> RegistroMarca_Notificacion() {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.RegistroMarca_Notificacion().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar los registros de marcas en la base de datos.");
                }
            }
        }

        public List<EstadoMarca_ListarResult> EstadoMarca_Listar() {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.EstadoMarca_Listar().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar los estados de las marcas en la base de datos.");
                }
            }
        }

        public RegistroMarca_ObtenerResult RegistroMarca_Obtener(int idRegistroMarca) {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.RegistroMarca_Obtener(idRegistroMarca).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error, no se pudo obtener los datos de la marca en la base de datos.");
                }
            }
        }

        public void RegistroMarca_Registrar(int idMarca, int idEmpresa, string marca, int idPais, int idTipo, int idTitular, string clase, string certificado,
                                    DateTime fechaVencimiento, int idEstadoMarca, string observacion, int usuario)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dci.RegistroMarca_Registrar(idMarca,idEmpresa,marca,idPais,idTipo,idTitular,clase,certificado,fechaVencimiento,idEstadoMarca,observacion,usuario);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al registrar/actualizar la marca en la base de datos.");
                }
            }
        }

        public List<RegistroMarcaHistorico_ListarResult> HistoricoMarca_Listar(int idMarca, DateTime FechanVencimientoIni, DateTime FechaVencimientoFin) {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.RegistroMarcaHistorico_Listar(idMarca, FechanVencimientoIni, FechaVencimientoFin).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el historico de las marcas en la base de datos.");
                }
            }
        }

        public void DocumentoMarca_Registrar(int idRegistroMarca, int idTipoDocumento, string documento, string ruta, int usuario) {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dci.DocumentosMarca_Registrar(idRegistroMarca,idTipoDocumento,documento,ruta , usuario);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al registrar el documento de la marca en la base de datos.");
                }
            }

        }

        public List<DocumentosMarca_ListarResult> DocumentoMarca_Listar(int idRegistroMarca, int idTipoDocumentoMarca)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.DocumentosMarca_Listar(idRegistroMarca, idTipoDocumentoMarca).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar los documentos de las marcas en la base de datos.");
                }
            }
        }

        public void DocumentoMarca_Eliminar(int idDocumento, int idRegistroMarca)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dci.DocumentosMarca_Eliminar(idDocumento,idRegistroMarca);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al eliminar el documento de la marca en la base de datos.");
                }
            }

        }

        public List<TipoDocumentoMarca_ListarResult> TipoDocumentoMarca_Listar()
        {

            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.TipoDocumentoMarca_Listar().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar los tipos de documentos de las marcas en la base de datos.");
                }
            }

        }

        public List<ClaseMarca_ListarResult> ClaseMarca_Listar() {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.ClaseMarca_Listar().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar las clases de las marcas en la base de datos.");
                }
            }
        }
    }
}
