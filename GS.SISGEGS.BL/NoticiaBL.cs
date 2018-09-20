using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GS.SISGEGS.DM;
using System.Configuration;
using System.Transactions;

namespace GS.SISGEGS.BL
{

    public interface INoticiaBL
    {
        List<Noticia_PublicarDashboardResult> Noticia_PublicarDashboard(DateTime fecha, ref List<NoticiaFoto_PublicarDashboardResult> lstNoticiaFoto);
        List<Noticia_ListarResult> Noticia_Listar(int idEmpresa, string descripcion, DateTime fecha);
        Noticia_BuscarResult Noticia_Buscar(int idNoticia, ref List<NoticiaFoto_ListarResult> lstNoticiaFoto);
        void Noticia_Eliminar(int idNoticia, int idUsuarioModificar);
        void Noticia_Registrar(int idNoticia, string titulo, string texto, DateTime fechaPublicacion, DateTime fechaVencimiento, int idEmpresa, int idUsuarioRegistro, bool activo, List<NoticiaFoto_ListarResult> lstNoticiaFoto);
    }
    public class NoticiaBL : INoticiaBL
    {
        public Noticia_BuscarResult Noticia_Buscar(int idNoticia, ref List<NoticiaFoto_ListarResult> lstNoticiaFoto)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    lstNoticiaFoto = dci.NoticiaFoto_Listar(idNoticia).ToList();
                    return dci.Noticia_Buscar(idNoticia).Single();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
            }
        }

        public void Noticia_Eliminar(int idNoticia, int idUsuarioModificar)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dci.Noticia_Eliminar(idNoticia, idUsuarioModificar);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }

        public List<Noticia_ListarResult> Noticia_Listar(int idEmpresa, string descripcion, DateTime fecha)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.Noticia_Listar(idEmpresa, descripcion, fecha).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
            }
        }

        public void Noticia_Registrar(int idNoticia, string titulo, string texto,  DateTime fechaPublicacion, DateTime fechaVencimiento, int idEmpresa, int idUsuarioRegistro, bool activo, List<NoticiaFoto_ListarResult> lstNoticiaFoto)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    List<NoticiaFoto_ListarResult> lstImagenes;
                    try
                    {
                        idNoticia = dci.Noticia_Registrar(idNoticia, titulo, texto, fechaPublicacion, fechaVencimiento, idEmpresa, idUsuarioRegistro, activo);
                        lstImagenes = lstNoticiaFoto.FindAll(x => !x.elimino);
                        foreach (NoticiaFoto_ListarResult foto in lstImagenes)
                        {
                            dci.NoticiaFoto_Registrar(foto.idNoticiaFoto, idNoticia, foto.descripcion, foto.urlImagen, 
                                idUsuarioRegistro, foto.activo, foto.horizontal, foto.altura, foto.anchura);
                        }
                        lstImagenes = lstNoticiaFoto.FindAll(x => x.elimino && x.idNoticiaFoto != 0);
                        foreach (NoticiaFoto_ListarResult foto in lstImagenes)
                        {
                            dci.NoticiaFoto_Eliminar(foto.idNoticiaFoto, idUsuarioRegistro);
                        }
                        dci.SubmitChanges();
                        scope.Complete();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public List<Noticia_PublicarDashboardResult> Noticia_PublicarDashboard(DateTime fecha, ref List<NoticiaFoto_PublicarDashboardResult> lstNoticiaFoto)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    lstNoticiaFoto = dci.NoticiaFoto_PublicarDashboard(fecha).ToList();
                    return dci.Noticia_PublicarDashboard(fecha).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR: No se pudo conectarse con la base de datos.");
                }
            }
        }
    }
}
