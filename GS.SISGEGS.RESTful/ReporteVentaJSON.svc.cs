using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GS.SISGEGS.DM;
using GS.SISGEGS.BL;
using System.ServiceModel.Web;

namespace GS.SISGEGS.RESTful
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ReporteVentaJSON" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ReporteVentaJSON.svc o ReporteVentaJSON.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ReporteVentaJSON : IReporteVentaJSON
    {
        //[WebGet(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ReporteVenta_ListarVendedores/{idEmpresa}/{codigoUsuario}/{mes}/{anho}")]
        public List<gsDocVenta_ReporteVenta_VendedorResult> ReporteVenta_ListarVendedores(string idEmpresa, string codigoUsuario, string ID_Vendedor, string mes, string anho)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL(); 
            try {
                return objDocVentaBL.DocVenta_ReporteVenta_Vendedor(int.Parse(idEmpresa), int.Parse(codigoUsuario), ID_Vendedor, DateTime.Now.AddMonths(-1), DateTime.Now);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
