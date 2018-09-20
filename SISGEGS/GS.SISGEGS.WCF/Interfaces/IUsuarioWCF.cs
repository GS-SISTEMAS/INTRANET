using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUsuarioWCF" in both code and config file together.
    [ServiceContract]
    public interface IUsuarioWCF
    {
        [OperationContract]
        Usuario_AutenticarResult Usuario_Autenticar(int idEmpresa, string codigo, string contrasena);
        [OperationContract]
        Usuario_LoginResult Usuario_Login(int idUsuario);
        [OperationContract]
        void Usuario_CambiarContrasena(int idEmpresa, int codigoUsuario, int idUsuario, string contrasena);
    }
}
