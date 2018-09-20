using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{

    [ServiceContract]
    public interface INoticiasWCF
    {
        [OperationContract]
        List<Noticia_ListarResult> Noticia_Listar(int idEmpresa, string descripcion, DateTime fecha);
        [OperationContract]
        Noticia_BuscarResult Noticia_Buscar(int idNoticia, ref List<NoticiaFoto_ListarResult> lstNoticiaFoto);
        [OperationContract]
        void Noticia_Eliminar(int idNoticia, int idUsuarioModificar);
        [OperationContract]
        void Noticia_Registrar(int idNoticia, string titulo, string texto, DateTime fechaPublicacion, DateTime fechaVencimiento, int idEmpresa, int idUsuarioRegistro, bool activo, List<NoticiaFoto_ListarResult> lstNoticiaFoto);
    }
}
