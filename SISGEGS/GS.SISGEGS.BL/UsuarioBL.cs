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
        Usuario_AutenticarResult Usuario_Autenticar(int idEmpresa, string loginUsuario, string contrasena);
        Usuario_LoginResult Usuario_Login(int idUsuario);
        void Usuario_CambiarContrasena(int idEmpresa, int codigoUsuario, int idUsuario, string contrasena);
    }

    public class UsuarioBL : IUsuarioBL
    {
        public Usuario_LoginResult Usuario_Login(int idUsuario)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString)) {
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
                using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
                {
                    try
                    {
                        Usuario_AutenticarResult objUsuario = dci.Usuario_Autenticar(loginUsuario, idEmpresa, EncryptHelper.Encode(contrasena)).Single();
                        dmGenesysDataContext dc = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + objUsuario.codigoUsuario).ToString().Substring(1, 4)));
                        if (!dc.gsUsuario_Autenticar(loginUsuario, contrasena).Single().EsActivo)
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

        public void Usuario_CambiarContrasena(int idEmpresa, int codigoUsuario, int idUsuario, string contrasena)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    dci.Usuario_CambiarPassword(idUsuario, Helpers.EncryptHelper.Encode(contrasena));
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
    }
}
