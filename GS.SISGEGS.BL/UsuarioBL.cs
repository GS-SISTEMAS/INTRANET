using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Linq;
using GS.SISGEGS.DM;
using GS.SISGEGS.BL.Helpers;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface IUsuarioBL {
        Usuario_LoginResult Usuario_Login(int idUsuario);
        Usuario_AutenticarResult Usuario_Autenticar(int idEmpresa, string loginUsuario, string contrasena);

        void Usuario_CambiarContrasena(int idEmpresa, int codigoUsuario, int idUsuario, string contrasena, bool cambiarAmbos);
        List<Usuario_BuscarResult> Usuario_Buscar(int idEmpresa, int idPerfil, string Descripcion);
        List<gsUsuario_BuscarResult> UsuarioBuscar_Genesys(int idEmpresa, int codigoUsuario, int idUsuario, string Descripcion);
        int Usuario_Registrar(int idEmpresa, int codigoUsuario, string password, string nombreUsuario, string LoginUsuario, int idPerfil, string correo, string nroDocumento, bool cambioPassword, bool cambioPasswordAmbos, int idUsuarioRegistro, bool activo);

        int Usuario_Update(int idEmpresa, int idUsuario, int codigoUsuario, string password, string nombreUsuario, string LoginUsuario, int idPerfil, string correo, string nroDocumento, bool cambioPassword, bool cambioAmbos, int idUsuarioRegistro, bool activo);
        List<usp_SelCantidadAccesosPorMenuResult> ListarMenusPorUsuario(int idempresa, DateTime fechainicio, DateTime fechafin);
        List<usp_Sel_MenusNoAccedidosResult> ListarMenusNoAccedidos(int idempresa, DateTime fechainicio, DateTime fechafin);
    }

    public class UsuarioBL : IUsuarioBL
    {
        public Usuario_LoginResult Usuario_Login(int idUsuario)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.Usuario_Login(idUsuario).Single();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR: No se pudo conectarse con la base de datos.");
                }
            }
        }

        public Usuario_AutenticarResult Usuario_Autenticar(int idEmpresa, string loginUsuario, string contrasena)
        {
            try {
                ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
                using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
                {
                    try
                    {
                        string cadena; 
                        string password = EncryptHelper.Encode(contrasena);
                        Usuario_AutenticarResult objUsuario = dci.Usuario_Autenticar(loginUsuario, idEmpresa, EncryptHelper.Encode(contrasena)).Single();

                        //string con = ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString; 
                        //string usu = "usrGEN" + (10000 + objUsuario.codigoUsuario).ToString().Substring(1, 4); 


                        dmGenesysDataContext dc = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + objUsuario.codigoUsuario).ToString().Substring(1, 4)));
                        if (!dc.gsUsuario_Autenticar(objUsuario.codigoUsuario, EncryptHelper.Decode(objUsuario.passwordGenesys)).Single().EsActivo)
                            throw new ArgumentException("Usuario no esta activo en Genesys.");
                        return objUsuario;
                    }
                    catch (ChangeConflictException ex)
                    {
                        dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                        throw ex;
                    }
                    finally {
                        dci.SubmitChanges();
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public void Usuario_CambiarContrasena(int idEmpresa, int codigoUsuario, int idUsuario, string contrasena, bool cambiarAmbos)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    dci.Usuario_CambiarPassword(idUsuario, Helpers.EncryptHelper.Encode(contrasena), cambiarAmbos);
                    if(cambiarAmbos)
                    dcg.gsUsuario_CambiarPassword(codigoUsuario, contrasena);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    dcg.SubmitChanges();
                    throw ex;
                }
            }
        }

        public List<Usuario_BuscarResult> Usuario_Buscar(int idEmpresa, int idPerfil, string Descripcion)
        {
            try
            {
                ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
                using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
                {
                    List<Usuario_BuscarResult> listUsuario = new List<Usuario_BuscarResult>();
                    List<Usuario_BuscarResult> listUsuarioNew = new List<Usuario_BuscarResult>();
                    try
                    {
                        listUsuario = dci.Usuario_Buscar(idEmpresa, idPerfil,  Descripcion).ToList();

                        foreach(Usuario_BuscarResult objUsu in listUsuario )
                        {
                            Usuario_BuscarResult objUsuNew = new Usuario_BuscarResult();
                            objUsuNew = objUsu;
                            string password = null;
                            string passwordG = null;

                            if (objUsuNew.password == "" ||  objUsuNew.password == null || String.IsNullOrEmpty(objUsuNew.password))
                            { password = ""; }
                            else
                            { password = EncryptHelper.Decode(objUsuNew.password); }

                            if (objUsuNew.passwordGenesys == "" || objUsuNew.passwordGenesys == null || String.IsNullOrEmpty(objUsuNew.passwordGenesys))
                            { passwordG = ""; }
                            else
                            { passwordG = EncryptHelper.Decode(objUsuNew.passwordGenesys); }

                            objUsuNew.password = password;
                            objUsuNew.passwordGenesys = passwordG; 

                            listUsuarioNew.Add(objUsuNew); 
                        }

                        return listUsuarioNew;
                    }
                    catch (ChangeConflictException ex)
                    {
                        dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                        throw ex;
                    }
                    finally
                    {
                        dci.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsUsuario_BuscarResult> UsuarioBuscar_Genesys(int idEmpresa, int codigoUsuario, int idUsuario, string descripcion)
        {

            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //string cadena;
                    //--cadena = ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString;
                    //--cadena = GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos);
                    //cadena = cadena + "{1}";

                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    List<gsUsuario_BuscarResult> list = dcg.gsUsuario_Buscar(idUsuario, descripcion).ToList();

                    return list;

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los usuarios en la base de datos.");
                }

            }
        }

        public int Usuario_Registrar(int idEmpresa, int codigoUsuario, string clave, string nombreUsuario, string LoginUsuario, int idPerfil, string correo, string nroDocumento, bool cambioPassword, bool cambioPasswordAmbos, int idUsuarioRegistro,  bool activo)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    string password = EncryptHelper.Encode(clave);
                    return dci.Usuario_RegistrarNew(idEmpresa, codigoUsuario, password, nombreUsuario, LoginUsuario, idPerfil, correo, nroDocumento, cambioPassword, cambioPasswordAmbos, idUsuarioRegistro, activo);
                }
                catch (Exception ex)
                {
                     dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }

        public int Usuario_Update(int idEmpresa, int idUsuario, int codigoUsuario, string password, string nombreUsuario, string LoginUsuario, int idPerfil, string correo, string nroDocumento, bool cambioPassword, bool cambioAmbos,  int idUsuarioRegistro, bool activo)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    Usuario_CambiarContrasena(idEmpresa, codigoUsuario, idUsuario, password, cambioAmbos);
                    return dci.Usuario_UpdateNew(idEmpresa, idUsuario, codigoUsuario, password, nombreUsuario, LoginUsuario, idPerfil, correo, nroDocumento, cambioPassword, cambioAmbos, idUsuarioRegistro, activo);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
            }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }
        public List<usp_SelCantidadAccesosPorMenuResult> ListarMenusPorUsuario(int idempresa, DateTime fechainicio, DateTime fechafin)
        {
            {
                using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
                {
                    try
                    {
                        List<usp_SelCantidadAccesosPorMenuResult> listamenus = new List<usp_SelCantidadAccesosPorMenuResult>();
                        listamenus = dci.usp_SelCantidadAccesosPorMenu(fechainicio, fechafin, idempresa).ToList();
                        return listamenus;
                    }
                    catch (Exception ex)
                    {
                        dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                        dci.SubmitChanges();
                        throw new ArgumentException("Error al consultar el reporte de Seguimiento de Accesos por Menus");
                    }
                    finally
                    {
                        dci.SubmitChanges();
                    }
                }
            }
        }
        public List<usp_Sel_MenusNoAccedidosResult> ListarMenusNoAccedidos(int idempresa, DateTime fechainicio, DateTime fechafin)
        {
            {
                using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
                {
                    try
                    {
                        List<usp_Sel_MenusNoAccedidosResult> listamenus = new List<usp_Sel_MenusNoAccedidosResult>();
                        listamenus = dci.usp_Sel_MenusNoAccedidos(fechainicio, fechafin, idempresa).ToList();
                        return listamenus;
                    }
                    catch (Exception ex)
                    {
                        dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                        dci.SubmitChanges();
                        throw new ArgumentException("Error al consultar el reporte de Seguimiento de Accesos por Menus No accedidos");
                    }
                    finally
                    {
                        dci.SubmitChanges();
                    }
                }
            }
        }



        public List<USP_Sel_Usuarios_GeneralResult> Usuario_Listar_Usuarios(string loginUsuario)
        {
            try
            {
                ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
                using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
                {

                    List<USP_Sel_Usuarios_GeneralResult> listUsuario = new List<USP_Sel_Usuarios_GeneralResult>();
                    try
                    {
                        listUsuario = dci.USP_Sel_Usuarios_General(loginUsuario).ToList();
                        return listUsuario;
                    }
                    catch (ChangeConflictException ex)
                    {
                        dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                        dci.SubmitChanges();
                        throw new ArgumentException("Error al consultar Usuarios");
                    }
                    finally
                    {
                        dci.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Actualizar_Estado_Usuarios_General(string loginUsuario, bool Estado_General)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.USP_UPD_EstadoUsuarios_General(loginUsuario, Estado_General);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }

        public int Actualizar_Estado_Usuarios_Empresa(string loginUsuario, bool Estado_Silvestre, bool Estado_NeoAgrum, bool Estado_Inatec, bool Estado_Intranet, bool Estado_Ticket)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.USP_UPD_EstadoUsuariosXEmpresa(loginUsuario, Estado_Silvestre, Estado_NeoAgrum, Estado_Inatec, Estado_Intranet, Estado_Ticket);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }


    }
}
