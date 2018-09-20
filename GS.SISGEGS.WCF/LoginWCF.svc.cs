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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "LoginWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione LoginWCF.svc o LoginWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class LoginWCF : ILoginWCF
    {
        /// <summary>
        /// Metodo que registra los usuario y las paginas a las que entra.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="idUsuario"></param>
        public void AuditoriaMenu_Registrar(string url, string nombreDispositivo, int idUsuario)
        {
            AuditoriaBL objAuditoriaBL;
            try
            {
                objAuditoriaBL = new AuditoriaBL();
                objAuditoriaBL.AuditoriaMenu_Registrar(url, nombreDispositivo, idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta las opciones del menu que tiene cierto perfil
        /// </summary>
        /// <param name="idPerfil"></param>
        /// <returns></returns>
        public List<Menu_CargarInicioResult> Menu_CargarInicio(int idEmpresa, int codigoUsuario, int idPerfil, ref VBG00004Result objEmpresa)
        {
            MenuBL objMenuBL;
            try
            {
                objMenuBL = new MenuBL();
                return objMenuBL.Menu_CargarInicio(idEmpresa, codigoUsuario, idPerfil, ref objEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario_AutenticarResult Usuario_Autenticar(int idEmpresa, string codigo, string contrasena)
        {
            UsuarioBL objUsuarioBL;
            try
            {
                objUsuarioBL = new UsuarioBL();
                return objUsuarioBL.Usuario_Autenticar(idEmpresa, codigo, contrasena);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Usuario_CambiarContrasena(int idEmpresa, int codigoUsuario, int idUsuario, string contrasena, bool cambiarAmbos)
        {
            UsuarioBL objUsuarioBL;
            try
            {
                objUsuarioBL = new UsuarioBL();
                objUsuarioBL.Usuario_CambiarContrasena(idEmpresa, codigoUsuario, idUsuario, contrasena, cambiarAmbos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario_LoginResult Usuario_Login(int idUsuario)
        {
            UsuarioBL objUsuarioBL;
            try
            {
                objUsuarioBL = new UsuarioBL();
                return objUsuarioBL.Usuario_Login(idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
