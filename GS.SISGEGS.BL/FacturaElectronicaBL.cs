using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GS.SISGEGS.DM;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface IFacturaElectronicaBL {

        void Item_BuscarPrecioCliente(int idEmpresa, int codigoUsuario, decimal idPrecioCliente, ref decimal? precioEspecial,
            ref DateTime? vigInicio, ref DateTime? vigFinal, ref VBG01134Result objCliente, ref VBG01124Result objProducto, ref string ID_Item);
        List<VBG04694Result> FacturaElectronica_Listar(int idEmpresa, int codigoUsuario, DateTime fechaDesde, DateTime fechaHasta,
            string iD_Cliente, string iD_Vendedor, decimal iD_Moneda, decimal iD_Documento, decimal iD_FormaPago, string serie, decimal numero);
        List<VBG04708_CABECERAResult> DocumentoFactura_Cabecera(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, ref string Archivo);
        List<VBG04708_DETALLEResult> DocumentoFactura_Detalle(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, ref string Archivo);
        List<VBG04709_CABECERAResult> DocumentoNotaCredito_Cabecera(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero, ref string Archivo);
        List<VBG04709_DETALLEResult> DocumentoNotaCredito_Detalle(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero,  ref string Archivo);
        List<VBG04710_CABECERAResult> DocumentoNotaDebito_Cabecera(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero, ref string Archivo);
        List<VBG04710_DETALLEResult> DocumentoNotaDebito_Detalle(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero, ref string Archivo);
        List<VBG04711_CABECERAResult> DocumentoBoletas_Cabecera(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero, ref string Archivo);
        List<VBG04711_DETALLEResult> DocumentoBoletas_Detalle(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero, ref string Archivo);
        void DocumentosElectronicos_Update(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Comentario, int estado);
        List<gsComboDocElectronicoResult> ComboDocElectronico(int idEmpresa, int codigoUsuario);

        List<VBG00946_ElectronicaResult> Retenciones_Electronicas_Listar(int idEmpresa, int codigoUsuario, int ID_Estado, int ID_Documento, string ID_Agenda, DateTime fechaDesde, DateTime fechaHasta);

        List<VBG00946_CABECERAResult> Retenciones_Cabecera_Listar(int idEmpresa, int codigoUsuario, int Op);

        void RetencionElectronica_Update(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Comentario, int estado);

        List<VBG02714_DetraccionResult> Detranccion_Item(int idEmpresa, int codigoUsuario, int Kardex, int Indice); 

    }
    public class FacturaElectronicaBL : IFacturaElectronicaBL
    {
        public void Item_BuscarPrecioCliente(int idEmpresa, int codigoUsuario, decimal idPrecioCliente, ref decimal? precioEspecial,
            ref DateTime? vigInicio, ref DateTime? vigFinal, ref VBG01134Result objCliente, ref VBG01124Result objProducto, ref string ID_Item)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                string ID_Cliente = null;
                int? tipoDscto = null;
                decimal? dsctoAdicional = null, iD_ClienteSucursal = null;
                string nomRelComercial = null;
                bool? existe = null;
                try
                {
                    dcg.VBG01312(idPrecioCliente, ref ID_Cliente, ref ID_Item, ref vigInicio, ref vigFinal, ref precioEspecial, ref tipoDscto, ref dsctoAdicional, ref iD_ClienteSucursal);

                    objCliente = dcg.VBG01134(ID_Cliente, 0, ref nomRelComercial, ref existe).Single();
                    objProducto = dcg.VBG01124(ID_Item, null, null).Single();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede listar los precios de los clientes consultados.");
                }

            }
        }

        public List<VBG04694Result> FacturaElectronica_Listar(int idEmpresa, int codigoUsuario, DateTime fechaDesde, DateTime fechaHasta,
            string iD_Cliente, string iD_Vendedor, decimal iD_Moneda, decimal iD_Documento, decimal iD_FormaPago, string serie, decimal numero )
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<VBG04694Result> listar;
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    listar = new List<VBG04694Result>();
                    listar =  dcg.VBG04694(fechaDesde, fechaHasta, iD_Cliente, iD_Vendedor, null, iD_Documento, null,serie,null).ToList();
                    return listar; 

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puedo buscar las facturas electronicas en Genesys.");
                }

            }
        }
        public List<VBG04708_CABECERAResult> DocumentoFactura_Cabecera(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, ref string Archivo)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<VBG04708_CABECERAResult> listar;
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    listar = new List<VBG04708_CABECERAResult>();
                    listar = dcg.VBG04708_CABECERA(TablaOrigen, Op, Serie, ref Archivo).ToList();
                    return listar;

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puedo buscar las facturas electronicas en Genesys.");
                }

            }
        }
        public List<VBG04708_DETALLEResult> DocumentoFactura_Detalle(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, ref string Archivo)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<VBG04708_DETALLEResult> listar;
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    listar = new List<VBG04708_DETALLEResult>();
                    listar = dcg.VBG04708_DETALLE(TablaOrigen, Op, Serie, ref Archivo).ToList();
                    return listar;

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puedo buscar las facturas electronicas en Genesys.");
                }

            }
        }
        public List<VBG04709_CABECERAResult> DocumentoNotaCredito_Cabecera(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero, ref string Archivo)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<VBG04709_CABECERAResult> listar;
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    listar = new List<VBG04709_CABECERAResult>();
                    listar = dcg.VBG04709_CABECERA(TablaOrigen, Op, Serie, Numero, ref Archivo).ToList();
                    return listar;

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puedo buscar las Notas de Credito electronicas en Genesys.");
                }

            }
        }
        public List<VBG04709_DETALLEResult> DocumentoNotaCredito_Detalle(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero, ref string Archivo)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<VBG04709_DETALLEResult> listar;
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    listar = new List<VBG04709_DETALLEResult>();
                    listar = dcg.VBG04709_DETALLE(TablaOrigen, Op, Serie, Numero, ref Archivo).ToList();
                    return listar;

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puedo buscar las facturas electronicas en Genesys.");
                }

            }
        }
        public List<VBG04710_CABECERAResult> DocumentoNotaDebito_Cabecera(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero, ref string Archivo)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<VBG04710_CABECERAResult> listar;
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    listar = new List<VBG04710_CABECERAResult>();
                    listar = dcg.VBG04710_CABECERA(TablaOrigen, Op, Serie, Numero, ref Archivo).ToList();
                    return listar;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puedo buscar las Notas de Credito electronicas en Genesys.");
                }

            }
        }
        public List<VBG04710_DETALLEResult> DocumentoNotaDebito_Detalle(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero, ref string Archivo)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<VBG04710_DETALLEResult> listar;
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    listar = new List<VBG04710_DETALLEResult>();
                    listar = dcg.VBG04710_DETALLE(TablaOrigen, Op, Serie, Numero, ref Archivo).ToList();
                    return listar;

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puedo buscar las facturas electronicas en Genesys.");
                }

            }
        }
        public List<VBG04711_CABECERAResult> DocumentoBoletas_Cabecera(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero, ref string Archivo)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<VBG04711_CABECERAResult> listar;
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    listar = new List<VBG04711_CABECERAResult>();
                    listar = dcg.VBG04711_CABECERA(TablaOrigen, Op, Serie, ref Archivo).ToList();
                    return listar;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puedo buscar las Notas de Credito electronicas en Genesys.");
                }

            }
        }
        public List<VBG04711_DETALLEResult> DocumentoBoletas_Detalle(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Serie, string Numero, ref string Archivo)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<VBG04711_DETALLEResult> listar;
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    listar = new List<VBG04711_DETALLEResult>();
                    listar = dcg.VBG04711_DETALLE(TablaOrigen, Op, Serie, ref Archivo).ToList();
                    return listar;

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puedo buscar las facturas electronicas en Genesys.");
                }

            }
        }
        public void DocumentosElectronicos_Update(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Comentario, int estado)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                int Respuesta;
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    Respuesta = dcg.VBG04695_ESTADOXML(TablaOrigen, Op, Comentario, estado);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puedo buscar registrar estodo de DocElectronicos en Genesys.");
                }

            }
        }
        public List<gsComboDocElectronicoResult> ComboDocElectronico(int idEmpresa, int codigoUsuario)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<gsComboDocElectronicoResult> listar;
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    listar = new List<gsComboDocElectronicoResult>();
                    listar = dcg.gsComboDocElectronico().ToList();
                    return listar;

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puedo buscar las facturas electronicas en Genesys.");
                }

            }
        }

        public List<VBG00946_ElectronicaResult> Retenciones_Electronicas_Listar(int idEmpresa, int codigoUsuario, int ID_Estado, int ID_Documento, string ID_Agenda, DateTime fechaDesde, DateTime fechaHasta )
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<VBG00946_ElectronicaResult> listar;
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    listar = new List<VBG00946_ElectronicaResult>();
                    listar = dcg.VBG00946_Electronica(ID_Estado,ID_Documento, ID_Agenda, fechaDesde,fechaHasta).ToList();
                    return listar;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puedo buscar las retenciones en Genesys.");
                }

            }
        }

        public List<VBG00946_CABECERAResult> Retenciones_Cabecera_Listar(int idEmpresa, int codigoUsuario, int Op)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<VBG00946_CABECERAResult> listar;
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    listar = new List<VBG00946_CABECERAResult>();
                    listar = dcg.VBG00946_CABECERA(Op).ToList();
                    return listar;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puedo buscar las retenciones en Genesys.");
                }

            }
        }

        public void RetencionElectronica_Update(int idEmpresa, int codigoUsuario, string TablaOrigen, int Op, string Comentario, int estado)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                int Respuesta;
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    Respuesta = dcg.VBG00946_ESTADOXML(TablaOrigen, Op, Comentario, estado);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puedo buscar registrar estodo de DocElectronicos en Genesys.");
                }

            }
        }

        public List<VBG02714_DetraccionResult> Detranccion_Item(int idEmpresa, int codigoUsuario, int Kardex, int Indice)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<VBG02714_DetraccionResult> listar;
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    listar = new List<VBG02714_DetraccionResult>();
                    listar = dcg.VBG02714_Detraccion(Kardex,Indice).ToList();
                    return listar;

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puedo buscar las facturas electronicas en Genesys.");
                }

            }
        }

    }
}
