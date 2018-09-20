using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GS.SISGEGS.DM;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface IPerfilBL {
        List<Perfil_ListarResult> Perfil_Listar(int idEmpresa, string descripcion);
        Perfil_BuscarResult Perfil_Buscar(int idPerfil);
        void Perfil_Eliminar(int idPerfil, int idUsuarioModificar);
        void Perfil_Registrar(Perfil objPerfil);

    }

    public class PerfilBL : IPerfilBL
    {
        public Perfil_BuscarResult Perfil_Buscar(int idPerfil)
        {
            throw new NotImplementedException();
        }

        public void Perfil_Eliminar(int idPerfil, int idUsuarioModificar)
        {
            throw new NotImplementedException();
        }

        public List<Perfil_ListarResult> Perfil_Listar(int idEmpresa, string descripcion)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    return dci.Perfil_Listar(descripcion, idEmpresa).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
            }
        }

        public void Perfil_Registrar(Perfil objPerfil)
        {
            throw new NotImplementedException();
        }
    }
}
