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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "FacturaElectronica2WCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione FacturaElectronica2WCF.svc o FacturaElectronica2WCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class FacturaElectronica2WCF : IFacturaElectronica2WCF
    {
        public List<VBG04694Result> FacturaElectronica_Listar(int idEmpresa, int codigoUsuario, DateTime fechaDesde, DateTime fechaHasta,
        string iD_Cliente, string iD_Vendedor, decimal iD_Moneda, decimal iD_Documento, decimal iD_FormaPago, string serie, decimal numero)
        {
            List<VBG04694Result> lista;
            try
            {
                lista = new List<VBG04694Result>();
                FacturaElectronicaBL objFactura = new FacturaElectronicaBL();

                lista = objFactura.FacturaElectronica_Listar(idEmpresa, codigoUsuario,fechaDesde,fechaHasta, iD_Cliente, iD_Vendedor, iD_Moneda, iD_Documento, iD_FormaPago, serie, numero);
                return lista; 

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<VBG04708_CABECERAResult> DocumentoFactura_Cabecera(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, ref string Archivo)
        {
            List<VBG04708_CABECERAResult> lista;
            try
            {
                lista = new List<VBG04708_CABECERAResult>();
                FacturaElectronicaBL objFactura = new FacturaElectronicaBL();

                lista = objFactura.DocumentoFactura_Cabecera(idEmpresa, codigoUsuario, TablaOrigen, Op, Serie, ref Archivo );
                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<VBG04708_DETALLEResult> DocumentoFactura_Detalle(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, ref string Archivo)
        {
            List<VBG04708_DETALLEResult> lista;
            try
            {
                lista = new List<VBG04708_DETALLEResult>();
                FacturaElectronicaBL objFactura = new FacturaElectronicaBL();

                lista = objFactura.DocumentoFactura_Detalle(idEmpresa, codigoUsuario, TablaOrigen, Op, Serie, ref Archivo);
                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<VBG04709_CABECERAResult> DocumentoNotaCredito_Cabecera(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero, ref string Archivo)
        {
            List<VBG04709_CABECERAResult> lista;
            try
            {
                lista = new List<VBG04709_CABECERAResult>();
                FacturaElectronicaBL objFactura = new FacturaElectronicaBL();

                lista = objFactura.DocumentoNotaCredito_Cabecera(idEmpresa, codigoUsuario, TablaOrigen, Op, Serie, Numero, ref Archivo);
                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<VBG04709_DETALLEResult> DocumentoNotaCredito_Detalle(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero, ref string Archivo)
        {
            List<VBG04709_DETALLEResult> lista;
            try
            {
                lista = new List<VBG04709_DETALLEResult>();
                FacturaElectronicaBL objFactura = new FacturaElectronicaBL();

                lista = objFactura.DocumentoNotaCredito_Detalle(idEmpresa, codigoUsuario, TablaOrigen, Op, Serie, Numero, ref Archivo);
                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<VBG04710_CABECERAResult> DocumentoNotaDebito_Cabecera(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero, ref string Archivo)
        {
            List<VBG04710_CABECERAResult> lista;
            try
            {
                lista = new List<VBG04710_CABECERAResult>();
                FacturaElectronicaBL objFactura = new FacturaElectronicaBL();

                lista = objFactura.DocumentoNotaDebito_Cabecera(idEmpresa, codigoUsuario, TablaOrigen, Op, Serie, Numero, ref Archivo);
                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<VBG04710_DETALLEResult> DocumentoNotaDebito_Detalle(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero, ref string Archivo)
        {
            List<VBG04710_DETALLEResult> lista;
            try
            {
                lista = new List<VBG04710_DETALLEResult>();
                FacturaElectronicaBL objFactura = new FacturaElectronicaBL();

                lista = objFactura.DocumentoNotaDebito_Detalle(idEmpresa, codigoUsuario, TablaOrigen, Op, Serie, Numero, ref Archivo);
                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<VBG04711_CABECERAResult> DocumentoBoletas_Cabecera(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero, ref string Archivo)
        {
            List<VBG04711_CABECERAResult> lista;
            try
            {
                lista = new List<VBG04711_CABECERAResult>();
                FacturaElectronicaBL objFactura = new FacturaElectronicaBL();

                lista = objFactura.DocumentoBoletas_Cabecera(idEmpresa, codigoUsuario, TablaOrigen, Op, Serie, Numero, ref Archivo);
                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<VBG04711_DETALLEResult> DocumentoBoletas_Detalle(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero, ref string Archivo)
        {
            List<VBG04711_DETALLEResult> lista;
            try
            {
                lista = new List<VBG04711_DETALLEResult>();
                FacturaElectronicaBL objFactura = new FacturaElectronicaBL();

                lista = objFactura.DocumentoBoletas_Detalle(idEmpresa, codigoUsuario, TablaOrigen, Op, Serie, Numero, ref Archivo);
                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DocumentosElectronicos_Update(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Comentario, int estado)
        {
            try
            {
                FacturaElectronicaBL objFactura = new FacturaElectronicaBL();
                objFactura.DocumentosElectronicos_Update(idEmpresa, codigoUsuario, TablaOrigen, Op, Comentario, estado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsComboDocElectronicoResult> ComboDocElectronico(int idEmpresa, int codigoUsuario)
        {
            List<gsComboDocElectronicoResult> lista;
            try
            {
                lista = new List<gsComboDocElectronicoResult>();
                FacturaElectronicaBL objFactura = new FacturaElectronicaBL();

                lista = objFactura.ComboDocElectronico(idEmpresa, codigoUsuario);
                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<VBG00946_ElectronicaResult> Retenciones_Electronicas_Listar(int idEmpresa, int codigoUsuario, int ID_Estado, int ID_Documento, string ID_Agenda, DateTime fechaDesde, DateTime fechaHasta)
        {
            List<VBG00946_ElectronicaResult> lista;
            try
            {
                lista = new List<VBG00946_ElectronicaResult>();
                FacturaElectronicaBL objFactura = new FacturaElectronicaBL();
                lista = objFactura.Retenciones_Electronicas_Listar(idEmpresa, codigoUsuario,ID_Estado,ID_Documento,ID_Agenda, fechaDesde,fechaHasta);
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<VBG00946_CABECERAResult> Retenciones_Cabecera_Listar(int idEmpresa, int codigoUsuario, int Op)
        {
            List<VBG00946_CABECERAResult> lista;
            try
            {
                lista = new List<VBG00946_CABECERAResult>();
                FacturaElectronicaBL objFactura = new FacturaElectronicaBL();
                lista = objFactura.Retenciones_Cabecera_Listar(idEmpresa, codigoUsuario,  Op);
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void RetencionElectronica_Update(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Comentario, int estado)
        {
            try
            {
                FacturaElectronicaBL objFactura = new FacturaElectronicaBL();
                objFactura.RetencionElectronica_Update(idEmpresa, codigoUsuario, TablaOrigen, Op, Comentario, estado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<VBG02714_DetraccionResult> Detranccion_Item(int idEmpresa, int codigoUsuario, int Kardex, int Indice)
        {
            List<VBG02714_DetraccionResult> lista;
            try
            {
                lista = new List<VBG02714_DetraccionResult>();
                FacturaElectronicaBL objFactura = new FacturaElectronicaBL();
                lista = objFactura.Detranccion_Item(idEmpresa, codigoUsuario, Kardex,Indice);
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}
