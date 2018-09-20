using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Linq;
using GS.SISGEGS.DM;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface IMenuBL {
        List<Menu_CargarInicioResult> Menu_CargarInicio(int idPerfil);
        List<Menu_ListarResult> Menu_Listar();
        void Menu_Registrar(Menu objMenu);
        void Menu_Modificar(Menu objMenu);
    }

    public class MenuBL : IMenuBL
    {
        public List<Menu_CargarInicioResult> Menu_CargarInicio(int idPerfil)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    return dci.Menu_CargarInicio(idPerfil).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pido consultar el Menú por perfil de la base de datos de GrpSilvestre");
                }
            }
        }

        public List<Menu_ListarResult> Menu_Listar()
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    return dci.Menu_Listar().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo consultar el Menú de la base de datos de GrpSilvestre");
                }
            }
        }

        public void Menu_Registrar(Menu objMenu)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    dci.Menu_Registrar(objMenu.idMenu, objMenu.nombreMenu, objMenu.url, objMenu.defecto, objMenu.activo, objMenu.idUsuarioRegistro);
                }
                catch (ChangeConflictException ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException("No se pudo registrar la opción del Menú en la base de datos de GrpSilvestre");
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }

        public void Menu_Modificar(Menu objMenu)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    dci.Menu_Modificar(objMenu.idMenu, objMenu.nombreMenu, objMenu.url, objMenu.defecto, objMenu.activo, objMenu.idUsuarioModifico);
                }
                catch (ChangeConflictException ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException("No se pudo modificar la opción del Menú en la base de datos de GrpSilvestre");
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }
    }
}
