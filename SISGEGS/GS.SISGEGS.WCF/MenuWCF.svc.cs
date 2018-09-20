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
        /// Consulta las opciones del menu que tiene cierto perfil
        /// </summary>
        /// <param name="idPerfil"></param>
        /// <returns></returns>
        public List<Menu_CargarInicioResult> Menu_CargarInicio(int idPerfil)
        {
            MenuBL objMenuBL;
            try {
                objMenuBL = new MenuBL();
                return objMenuBL.Menu_CargarInicio(idPerfil);
            }
            catch (Exception ex) {
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
        /// Registra una nueva opcion en el menú
        /// </summary>
        /// <param name="objMenu"></param>
        public void Menu_Registrar(Menu objMenu)
        {
            MenuBL objMenuBL;
            try
            {
                objMenuBL = new MenuBL();
                objMenuBL.Menu_Registrar(objMenu);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
