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
        List<Usuario_BuscarResult> Usuario_Buscar(int idEmpresa, int idPerfil, string Descripcion);

        [OperationContract]
        List<gsUsuario_BuscarResult> Usuario_BuscarGenesys(int idEmpresa, int codigoUsuario, int idUsuario, string descripcion);

        [OperationContract]
        int Usuario_Registrar(int idEmpresa, int codigoUsuario, string password, string nombreUsuario, string LoginUsuario, int idPerfil, string correo, string nroDocumento, bool cambioPassword, bool cambioPasswordAmbos, int idUsuarioRegistro, bool activo);

        [OperationContract]
        int Usuario_Actualizar(int idEmpresa, int idUsuario, int codigoUsuario, string password, string nombreUsuario, string LoginUsuario, int idPerfil, string correo, string nroDocumento, bool cambioPassword, bool cambioAmbos, int idUsuarioRegistro, bool activo);

        [OperationContract]
        void Usuario_CambiarContrasena(int idEmpresa, int idUsuario, int codigoUsuario, string password, bool cambiarAmbos);

        [OperationContract]
        List<usp_SelCantidadAccesosPorMenuResult> Usuario_ListarMenusPorUsuario(int idempresa, DateTime fechainicio, DateTime fechafin);

        [OperationContract]
        List<usp_Sel_MenusNoAccedidosResult> Usuario_ListarMenusNoAccedidos(int idempresa, DateTime fechainicio, DateTime fechafin);

        [OperationContract]
        List<USP_Sel_Usuarios_GeneralResult> Usuario_Listar_Usuarios(string loginUsuario);

        [OperationContract]
        int Actualizar_Estado_Usuarios_General(string loginUsuario, bool Estado_General);
        [OperationContract]
        int Actualizar_Estado_Usuarios_Empresa(string loginUsuario, bool Estado_Silvestre, bool Estado_NeoAgrum, bool Estado_Inatec, bool Estado_Intranet, bool Estado_Ticket);

    }
}
