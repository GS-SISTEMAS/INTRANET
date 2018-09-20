using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using GS.SISGEGS.BL;
using GS.SISGEGS.DM;
using System.ServiceModel.Web;

namespace GS.SISGEGS.RESTful
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IReporteVentaJSON" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IReporteVentaJSON
    {
        [WebGet(UriTemplate = "ReporteVenta_ListarVendedores/{idEmpresa}/{codigoUsuario}/{mes}/{anho}", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<gsDocVenta_ReporteVenta_VendedorResult> ReporteVenta_ListarVendedores(string idEmpresa, string codigoUsuario, string ID_Vendedor, string mes, string anho);
    }
}
