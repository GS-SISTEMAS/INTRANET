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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "DireccionWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione DireccionWCF.svc o DireccionWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class DireccionWCF : IDireccionWCF
    {
        public List<VBG00209Result> Direccion_ListarCliente(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            DireccionBL objDireccionBL;
            try
            {
                objDireccionBL = new DireccionBL();
                return objDireccionBL.Direccion_ListarCliente(idEmpresa, codigoUsuario, idAgenda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VBG03679Result> Direccion_ListarReferencia(int idEmpresa, int codigoUsuario, string idAgenda, int? idSucursal, int? idReferencia)
        {
            DireccionBL objDireccionBL;
            try
            {
                objDireccionBL = new DireccionBL();
                return objDireccionBL.Direccion_ListarReferencia(idEmpresa, codigoUsuario, idAgenda, idSucursal, idReferencia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
