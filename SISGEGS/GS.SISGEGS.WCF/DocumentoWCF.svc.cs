using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using GS.SISGEGS.BL;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "DocumentoWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione DocumentoWCF.svc o DocumentoWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class DocumentoWCF : IDocumentoWCF
    {
        public List<gsDocumento_ListarTipoCompraResult> Documento_ListarDocCompra(int idEmpresa, int codigoUsuario, string descripcion)
        {
            DocumentoBL objDocumentoBL;
            try
            {
                objDocumentoBL = new DocumentoBL();
                return objDocumentoBL.Documento_ListarDocCompra(idEmpresa, codigoUsuario, descripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VBG01122Result> Documento_ListarEgresosVarios(int idEmpresa, int codigoUsuario)
        {
            DocumentoBL objDocumentoBL;
            try
            {
                objDocumentoBL = new DocumentoBL();
                return objDocumentoBL.Documento_ListarEgresosVarios(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VBG00716Result> Documento_ListarDocVenta(int idEmpresa, int codigoUsuario)
        {
            DocumentoBL objDocumentoBL;
            try
            {
                objDocumentoBL = new DocumentoBL();
                return objDocumentoBL.Documento_ListarDocVenta(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
