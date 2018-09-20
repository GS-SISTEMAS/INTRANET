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

        public List<Usuario_BuscarResult> Usuario_Buscar(int idEmpresa, int idPerfil, string Descripcion)
        {
            UsuarioBL objUsuarioBL;
            List<Usuario_BuscarResult> ListUsuario = new List<Usuario_BuscarResult>();
            try
            {
                objUsuarioBL = new UsuarioBL();
                ListUsuario = objUsuarioBL.Usuario_Buscar(idEmpresa, idPerfil,  Descripcion);
                return ListUsuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsUsuario_BuscarResult> Usuario_BuscarGenesys(int idEmpresa, int codigoUsuario, int idUsuario, string descripcion)
        {
            UsuarioBL objUsuarioBL;
            try
            {
                objUsuarioBL = new UsuarioBL();
                return objUsuarioBL.UsuarioBuscar_Genesys(idEmpresa, codigoUsuario, idUsuario,  descripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Usuario_Registrar(int idEmpresa, int codigoUsuario, string password, string nombreUsuario, string LoginUsuario, int idPerfil, string correo, string nroDocumento, bool cambioPassword, bool cambioPasswordAmbos, int idUsuarioRegistro, bool activo)
        {
            UsuarioBL objUsuarioBL;
            try
            {
                objUsuarioBL = new UsuarioBL();
                return objUsuarioBL.Usuario_Registrar(idEmpresa, codigoUsuario, password, nombreUsuario, LoginUsuario, idPerfil, correo, nroDocumento, cambioPassword, cambioPasswordAmbos, idUsuarioRegistro, activo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Usuario_Actualizar(int idEmpresa, int idUsuario, int codigoUsuario, string password, string nombreUsuario, string LoginUsuario, int idPerfil, string correo, string nroDocumento, bool cambioPassword, bool cambioAmbos,  int idUsuarioRegistro, bool activo)
        {
            UsuarioBL objUsuarioBL;
            try
            {
                objUsuarioBL = new UsuarioBL();
                return objUsuarioBL.Usuario_Update(idEmpresa, idUsuario, codigoUsuario, password, nombreUsuario, LoginUsuario, idPerfil, correo, nroDocumento, cambioPassword, cambioAmbos, idUsuarioRegistro, activo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Usuario_CambiarContrasena(int idEmpresa, int idUsuario, int codigoUsuario, string password, bool cambiarAmbos)
        {
            UsuarioBL objUsuarioBL;
            try {
                objUsuarioBL = new UsuarioBL();
                objUsuarioBL.Usuario_CambiarContrasena(idEmpresa, codigoUsuario, idUsuario, password, cambiarAmbos);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public List<usp_SelCantidadAccesosPorMenuResult> Usuario_ListarMenusPorUsuario(int idempresa, DateTime fechainicio, DateTime fechafin)
        {
            UsuarioBL objUsuarioBL;
            try
            {
                objUsuarioBL = new UsuarioBL();
                return objUsuarioBL.ListarMenusPorUsuario(idempresa, fechainicio, fechafin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<usp_Sel_MenusNoAccedidosResult> Usuario_ListarMenusNoAccedidos(int idempresa, DateTime fechainicio, DateTime fechafin)
        {
            UsuarioBL objUsuarioBL;
            try
            {
                objUsuarioBL = new UsuarioBL();
                return objUsuarioBL.ListarMenusNoAccedidos(idempresa, fechainicio, fechafin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<USP_Sel_Usuarios_GeneralResult> Usuario_Listar_Usuarios(string loginUsuario)
        {
            UsuarioBL objUsuarioBL;
            List<USP_Sel_Usuarios_GeneralResult> ListUsuario = new List<USP_Sel_Usuarios_GeneralResult>();
            try
            {
                objUsuarioBL = new UsuarioBL();
                ListUsuario = objUsuarioBL.Usuario_Listar_Usuarios(loginUsuario);
                return ListUsuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Actualizar_Estado_Usuarios_General(string loginUsuario, bool Estado_General)
        {
            UsuarioBL objUsuarioBL;
            try
            {
                objUsuarioBL = new UsuarioBL();
                return objUsuarioBL.Actualizar_Estado_Usuarios_General(loginUsuario, Estado_General);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Actualizar_Estado_Usuarios_Empresa(string loginUsuario, bool Estado_Silvestre, bool Estado_NeoAgrum, bool Estado_Inatec, bool Estado_Intranet, bool Estado_Ticket)
        {
            UsuarioBL objUsuarioBL;
            try
            {
                objUsuarioBL = new UsuarioBL();
                return objUsuarioBL.Actualizar_Estado_Usuarios_Empresa(loginUsuario, Estado_Silvestre, Estado_NeoAgrum, Estado_Inatec, Estado_Intranet, Estado_Ticket);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
