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
    public class UsuarioWCF : IUsuarioWCF
    {
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

        public void Usuario_CambiarContrasena(int idEmpresa, int codigoUsuario, int idUsuario, string contrasena)
        {
            UsuarioBL objUsuarioBL;
            try
            {
                objUsuarioBL = new UsuarioBL();
                objUsuarioBL.Usuario_CambiarContrasena(idEmpresa, codigoUsuario, idUsuario, contrasena);
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
