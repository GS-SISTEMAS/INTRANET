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
        List<Menu_CargarInicioResult> Menu_CargarInicio(int idEmpresa, int codigoUsuario, int idPerfil, ref VBG00004Result objEmpresa);
        List<Menu_ListarResult> Menu_Listar();
        void Menu_Registrar(Menu objMenu);
        void Menu_Modificar(Menu objMenu, int idEmpresa, string lstPerfil);
        List<MenuPerfil_ListarResult> MenuPerfil_Listar(int idEmpresa, int idMenu);
    }

    public class MenuBL : IMenuBL
    {
        public List<Menu_CargarInicioResult> Menu_CargarInicio(int idEmpresa, int codigoUsuario, int idPerfil, ref VBG00004Result objEmpresa)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    objEmpresa = dcg.VBG00004().Single();
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
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
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
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
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

        public void Menu_Modificar(Menu objMenu, int idEmpresa, string lstPerfil)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dci.Menu_Modificar(objMenu.idMenu, objMenu.nombreMenu, objMenu.url, objMenu.defecto, objMenu.activo, objMenu.idUsuarioModifico);
                    dci.MenuPerfil_Registrar(lstPerfil, objMenu.idMenu, idEmpresa, objMenu.idUsuarioModifico);
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

        public List<MenuPerfil_ListarResult> MenuPerfil_Listar(int idEmpresa, int idMenu)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.MenuPerfil_Listar(idEmpresa, idMenu).ToList();
                }
                catch (ChangeConflictException ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException("No se puede listar los perfiles del habilitados para el Menú");
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }
    }
}
