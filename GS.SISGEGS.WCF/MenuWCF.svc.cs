using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using GS.SISGEGS.BL;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
    public class MenuWCFsvc : IMenuWCF
    {
        /// <summary>
        /// Método que lista los perfiles que una opción del menú puede acceder.
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="idMenu"></param>
        /// <returns></returns>
        public List<MenuPerfil_ListarResult> MenuPerfil_Listar(int idEmpresa, int idMenu)
        {
            MenuBL objMenuBL = new MenuBL(); ;
            try
            {
                return objMenuBL.MenuPerfil_Listar(idEmpresa, idMenu);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lista todo el menú registrado, incluyendo los inactivos
        /// </summary>
        /// <returns></returns>
        public List<Menu_ListarResult> Menu_Listar()
        {
            MenuBL objMenuBL;
            try
            {
                objMenuBL = new MenuBL();
                return objMenuBL.Menu_Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Modificar el menu tanto cabecera como detalle
        /// </summary>
        /// <param name="objMenu"></param>
        public void Menu_Modificar(Menu objMenu, int idEmpresa, string lstPerfil)
        {
            MenuBL objMenuBL = new MenuBL(); ;
            try
            {
                objMenuBL.Menu_Modificar(objMenu, idEmpresa, lstPerfil);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Registra una nueva opcion en el menú
        /// </summary>
        /// <param name="objMenu"></param>
        public void Menu_Registrar(Menu objMenu)
        {
            MenuBL objMenuBL = new MenuBL(); ;
            try
            {
                objMenuBL.Menu_Registrar(objMenu);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
