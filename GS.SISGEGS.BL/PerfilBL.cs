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
        void Perfil_Registrar(int idPerfil, string nombrePerfil, int idEmpresa, int idUsuarioRegistro, bool activo, 
            bool aprobarPlanilla0, bool aprobarPlanilla1, bool modificarPedido);
    }

    public class PerfilBL : IPerfilBL
    {
        public Perfil_BuscarResult Perfil_Buscar(int idPerfil)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.Perfil_Buscar(idPerfil).Single();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
            }
        }

        public void Perfil_Eliminar(int idPerfil, int idUsuarioModificar)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dci.Perfil_Eliminar(idPerfil, idUsuarioModificar);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
                finally {
                    dci.SubmitChanges();
                }
            }
        }

        public List<Perfil_ListarResult> Perfil_Listar(int idEmpresa, string descripcion)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.Perfil_Listar(idEmpresa, descripcion).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
            }
        }

        public void Perfil_Registrar(int idPerfil, string nombrePerfil, int idEmpresa, int idUsuarioRegistro, bool activo, 
            bool aprobarPlanilla0, bool aprobarPlanilla1, bool modificarPedido)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dci.Perfil_Registrar(idPerfil, nombrePerfil, idEmpresa, idUsuarioRegistro, activo, aprobarPlanilla0, aprobarPlanilla1, 
                        modificarPedido);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
                finally {
                    dci.SubmitChanges();
                }
            }
        }
    }
}
