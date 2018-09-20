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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMenuWCFsvc" in both code and config file together.
    [ServiceContract]
    public interface IMenuWCF
    {
        [OperationContract]
        List<Menu_CargarInicioResult> Menu_CargarInicio(int idPerfil);
        [OperationContract]
        List<Menu_ListarResult> Menu_Listar();
        [OperationContract]
        void Menu_Registrar(Menu objMenu);
    }
}
