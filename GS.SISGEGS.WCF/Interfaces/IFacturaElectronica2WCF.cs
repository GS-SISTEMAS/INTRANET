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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IFacturaElectronica2WCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IFacturaElectronica2WCF
    {
        [OperationContract]
        List<VBG04694Result> FacturaElectronica_Listar(int idEmpresa, int codigoUsuario, DateTime fechaDesde, DateTime fechaHasta,
            string iD_Cliente, string iD_Vendedor, decimal iD_Moneda, decimal iD_Documento, decimal iD_FormaPago, string serie, decimal numero);

        [OperationContract]
        List<VBG04708_CABECERAResult> DocumentoFactura_Cabecera(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, ref string Archivo);

        [OperationContract]
        List<VBG04708_DETALLEResult> DocumentoFactura_Detalle(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, ref string Archivo);

        [OperationContract]
        List<VBG04709_CABECERAResult> DocumentoNotaCredito_Cabecera(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero, ref string Archivo);

        [OperationContract]
        List<VBG04709_DETALLEResult> DocumentoNotaCredito_Detalle(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie,string Numero, ref string Archivo);

        [OperationContract]
        List<VBG04710_CABECERAResult> DocumentoNotaDebito_Cabecera(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero, ref string Archivo);
        [OperationContract]
        List<VBG04710_DETALLEResult> DocumentoNotaDebito_Detalle(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero, ref string Archivo);
        [OperationContract]
        List<VBG04711_CABECERAResult> DocumentoBoletas_Cabecera(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero, ref string Archivo);
        [OperationContract]
        List<VBG04711_DETALLEResult> DocumentoBoletas_Detalle(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero, ref string Archivo);
        [OperationContract]
        void DocumentosElectronicos_Update(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Comentario, int estado);
        [OperationContract]
        List<gsComboDocElectronicoResult> ComboDocElectronico(int idEmpresa, int codigoUsuario);

        [OperationContract]
        List<VBG00946_ElectronicaResult> Retenciones_Electronicas_Listar(int idEmpresa, int codigoUsuario, int ID_Estado, int ID_Documento, string ID_Agenda, DateTime fechaDesde, DateTime fechaHasta);

        [OperationContract]
        List<VBG00946_CABECERAResult> Retenciones_Cabecera_Listar(int idEmpresa, int codigoUsuario, int Op);

        [OperationContract]
        void RetencionElectronica_Update(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Comentario, int estado);

        [OperationContract]
        List<VBG02714_DetraccionResult> Detranccion_Item(int idEmpresa, int codigoUsuario, int Kardex, int Indice); 
    }
}
