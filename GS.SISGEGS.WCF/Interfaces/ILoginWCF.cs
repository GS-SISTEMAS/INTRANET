using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ILoginWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ILoginWCF
    {
        [OperationContract]
        Usuario_AutenticarResult Usuario_Autenticar(int idEmpresa, string codigo, string contrasena);
        [OperationContract]
        Usuario_LoginResult Usuario_Login(int idUsuario);
        [OperationContract]
        void Usuario_CambiarContrasena(int idEmpresa, int codigoUsuario, int idUsuario, string contrasena, bool cambiarAmbos);
        [OperationContract]
        List<Menu_CargarInicioResult> Menu_CargarInicio(int idEmpresa, int codigoUsuario, int idPerfil, ref VBG00004Result objEmpresa);
        [OperationContract]
        void AuditoriaMenu_Registrar(string url, string nombreDispositivo, int idUsuario);
    }
}
