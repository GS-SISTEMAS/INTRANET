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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "NoticiasWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione NoticiasWCF.svc o NoticiasWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class NoticiasWCF : INoticiasWCF
    {

            public Noticia_BuscarResult Noticia_Buscar(int idNoticia, ref List<NoticiaFoto_ListarResult> lstNoticiaFoto)
            {
                NoticiaBL objNoticiaBL = new NoticiaBL();
                try
                {
                    return objNoticiaBL.Noticia_Buscar(idNoticia, ref lstNoticiaFoto);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public void Noticia_Eliminar(int idNoticia, int idUsuarioModificar)
            {
            NoticiaBL objNoticiaBL = new NoticiaBL();
            try
                {
                objNoticiaBL.Noticia_Eliminar(idNoticia, idUsuarioModificar);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public List<Noticia_ListarResult> Noticia_Listar(int idEmpresa, string descripcion, DateTime fecha)
            {

                NoticiaBL objNoticiaBL = new NoticiaBL();

                try
                {
                    return objNoticiaBL.Noticia_Listar(idEmpresa, descripcion, fecha);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public void Noticia_Registrar(int idNoticia, string titulo, string texto, DateTime fechaPublicacion, DateTime fechaVencimiento, int idEmpresa, int idUsuarioRegistro, bool activo, List<NoticiaFoto_ListarResult> lstNoticiaFoto)
        {
            NoticiaBL objNoticiaBL = new NoticiaBL();
            try
                {
                objNoticiaBL.Noticia_Registrar(idNoticia, titulo, texto, fechaPublicacion, fechaVencimiento, idEmpresa, idUsuarioRegistro, activo, lstNoticiaFoto);
            }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
   }
    
}
