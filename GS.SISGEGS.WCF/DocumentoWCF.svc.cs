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
        public List<gsDocumento_ListarTipoCompraResult> Documento_ListarDocCompra(int idEmpresa, int codigoUsuario)
        {
            DocumentoBL objDocumentoBL;
            try
            {
                objDocumentoBL = new DocumentoBL();
                return objDocumentoBL.Documento_ListarDocCompra(idEmpresa, codigoUsuario);
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

        //------------------------------------------------

        public List<ListarFamiliasResult> ListarFamilias(int idEmpresa, int codigoUsuario)
        {
            DocumentoBL objDocumentoBL;
            try
            {
                objDocumentoBL = new DocumentoBL();
                return objDocumentoBL.ListarFamilias(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ListarDocumentosResult> ListarDocumentos(int idEmpresa, int codigoUsuario)
        {
            DocumentoBL objDocumentoBL;
            try
            {
                objDocumentoBL = new DocumentoBL();
                return objDocumentoBL.ListarDocumentos(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ListarDocumentosFamiliaResult> ListarDocumentosFamilia(int idEmpresa, int codigoUsuario, int tipoFamilia)
        {
            DocumentoBL objDocumentoBL;
            try
            {
                objDocumentoBL = new DocumentoBL();
                return objDocumentoBL.ListarDocumentosFamilia(idEmpresa, codigoUsuario, tipoFamilia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DetalleOperacionDocumentoResult> ListarDetalleDocumentos(int idEmpresa, int codigoUsuario, DateTime fechaCorte, char operacion, string id_agenda, int tipoFamiliaDoc, int tipoDoc)
        {
            DocumentoBL objDocumentoBL;
            try
            {
                objDocumentoBL = new DocumentoBL();
                return objDocumentoBL.ListarDetalleDocumentos(idEmpresa, codigoUsuario, fechaCorte, operacion, id_agenda, tipoFamiliaDoc, tipoDoc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DetalleOperacionFamiliaAnticuamientoDocumentoResult> DetalleOperacionFamiliaAnticuamientoDocumento(int idEmpresa, int codigoUsuario, DateTime fechaCorte, char operacion, string id_agenda, int tipoFamiliaDoc, int TipoDoc)
        {
            DocumentoBL objDocumentoBL;
            try
            {
                objDocumentoBL = new DocumentoBL();
                return objDocumentoBL.DetalleOperacionFamiliaAnticuamientoDocumento(idEmpresa, codigoUsuario, fechaCorte, operacion, id_agenda, tipoFamiliaDoc, TipoDoc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DetalleOperacionFamiliaResult> ListarDetalleAfiliadas(int idEmpresa, int codigoUsuario, DateTime fechaCorte, char operacion, string id_agendacompara, decimal idmoneda)
        {
            DocumentoBL objDocumentoBL;
            try
            {
                objDocumentoBL = new DocumentoBL();
                return objDocumentoBL.ListarDetalleAfiliadas(idEmpresa, codigoUsuario, fechaCorte, operacion, id_agendacompara, idmoneda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RegistrarDocumentoFamilia(int idEmpresa, int codigoUsuario, int tipoFamilia, decimal idDocumento)
        {
            DocumentoBL objDocumentoBL;
            try
            {
                objDocumentoBL = new DocumentoBL();
                objDocumentoBL.RegistrarDocumentoFamilia(idEmpresa, codigoUsuario, tipoFamilia, idDocumento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarDocumentoFamilia(int idEmpresa, int codigoUsuario, decimal idDocumento)
        {
            DocumentoBL objDocumentoBL;
            try
            {
                objDocumentoBL = new DocumentoBL();
                objDocumentoBL.EliminarDocumentoFamilia(idEmpresa, codigoUsuario, idDocumento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
