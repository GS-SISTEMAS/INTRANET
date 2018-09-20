using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GS.SISGEGS.DM;
using GS.SISGEGS.BE;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface IAgendaBL {
        List<gsAgenda_ListarClienteResult> Agenda_ListarCliente(int idEmpresa, int codigoUsuario, string descripcion, int? estado); 
        List<gsAgenda_ListarVendedorResult> Agenda_ListarVendedor(int idEmpresa, int codigoUsuario, string descripcion);
        VBG01134Result Agenda_BuscarCliente(int idEmpresa, int codigoUsuario, string idAgenda, ref decimal? lineaCredito, ref DateTime? fechaVencimiento, ref decimal? TC );
        List<VBG00167Result> AgendaAnexo_ListarDireccionCliente(int idEmpresa, int codigoUsuario, string idAgenda);
        List<VBG00746Result> AgendaAnexo_ListarAlmacen(int idEmpresa, int codigoUsuario);
        List<VBG03678Result> AgendaAnexo_ListarAlmacenCliente(int idEmpresa, int codigoUsuario, string idAgenda);
        List<VBG02699Result> AgendaAnexoReferencia_ListarPorSucursal(int idEmpresa, int codigoUsuario, int idAgendaAnexo, string idAgenda);
        Agenda_LimiteCreditoBE Agenda_LineaCredito(int idEmpresa, int codigoUsuario, string idAgenda, decimal? idMoneda);
        List<gsAgenda_ListarContactoResult> Agenda_ListarContacto(int idEmpresa, int codigoUsuario, string descripcion, int? estado);
        gsAgenda_BucarProveedorResult Agenda_BucarProveedor(int idEmpresa, int codigoUsuario, string idAgenda, ref bool? existe);
        List<gsAgenda_ListarProveedorResult> Agenda_ListarProveedor(int idEmpresa, int codigoUsuario, string descripcion);
        List<gsAgenda_ListarTransportistaResult> Agenda_ListarTransportista(int idEmpresa, int codigoUsuario, string descripcion);
        string Agenda_RegistrarProveedor(int idEmpresa, int codigoUsuario, string nroRUC, string razonSocial);
        List<gsChofer_ListarResult> Agenda_ListarChofer(int idEmpresa, int codigoUsuario, string Id_Transportista, string Id_Chofer, string descripcion);
        List<gsAgendaAnexo_ListarAlmacenDevolucionResult> AgendaAnexo_ListarAlmacenDevolucion(int idEmpresa, int codigoUsuario);
        List<gsPlaca_ListarResult> Agenda_ListarPlaca(int idEmpresa, int codigoUsuario, string Id_Transportista, string Id_Vehiculo, string descripcion);
        List<gsPlaca_DespachoResult> Agenda_ListarPlaca_Despacho(int idEmpresa, int codigoUsuario, string Id_Transportista, string Id_Vehiculo, string descripcion, string despacho);

        List<gsZonaSectorista_ListarResult> Agenda_ListarZonaSectorista(int idEmpresa, int codigoUsuario, string id_sectorista);

        List<Agenda_BuscarEmpresaResult> Agenda_BuscarEmpresa(int idEmpresa, int codigoUsuario, string idAgenda);

        List<gsUsuario_SectoristaResult> Agenda_ListarSectorista(int idEmpresa, int codigoUsuario, string descripcion, int? estado);

        List<gsZona_ListarResult> Agenda_ListarZona(int idEmpresa, int codigoUsuario, int id_zona);

        List<gsVendedor_ListarResult> Agenda_ListarVendedorProyectado(int idEmpresa, int codigoUsuario, int id_zona, string id_vendedor);

        List<gsClientesCorreo_EnvioResult> Agenda_ListarCorreos(int idEmpresa, int codigoUsuario, int id_zona, string id_vendedor);

        // INICIO PSF 5/12/2016
        List<gsAgenda_ListarGarantiaResult> Agenda_ListarGarantia(int idEmpresa, int codigoUsuario, int id_zona, string id_vendedor);

        VBG01134_validarCorreoResult Agenda_ValidarCorreo(int idEmpresa, int codigoUsuario, string idAgenda, ref bool? existeCliente, ref bool? existeCorreo);

        void Agenda_RegistrarCorreo(int idEmpresa, int codigoUsuario, string idAgenda, string Correo, ref int? Correlativo);

        VBG01134Result Agenda_BuscarCliente_Contado(int idEmpresa, int codigoUsuario, string idAgenda, ref decimal? lineaCredito, ref DateTime? fechaVencimiento, int idMoneda);

        RPT00015Result Agenda_TipoCambio(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int ID_Moneda);

        List<gsListarObservacionesAgendaResult> ListarObservacionesAgenda(int idEmpresa, int codigoUsuario, string idAgenda);

        List<gsListarLogLineaCreditoResult> ListarLogLineaCredito(int idEmpresa, int codigoUsuario, string idAgenda);

        List<GS_RecuperaCorreoAgendaResult> RecuperaCorreoAgenda(int idEmpresa, int codigoUsuario, string idAgenda);

        List<gsAgenda_ContactoResult> Agenda_ListarContacto_Estado(int idEmpresa, int codigoUsuario, string idAgenda);

        List<gsAgenda_ListarClienteAgenteResult> Agenda_ListarClienteAgente(int idEmpresa, int codigoUsuario, string descripcion, int? estado);

        List<gsVendedorZona_ListarResult> Agenda_VendedorZonaListar(int idEmpresa, int codigoUsuario, int id_zona, string id_vendedor);
    }

    public class AgendaBL : IAgendaBL
    {
        public List<VBG02699Result> AgendaAnexoReferencia_ListarPorSucursal(int idEmpresa, int codigoUsuario, int idAgendaAnexo, string idAgenda)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {   ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                     return dcg.VBG02699(idAgendaAnexo, idAgenda).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar por las referencias de la sucursal en la base de datos.");
                }
                finally
                {
                    dci.Connection.Close();
                    dcg.Connection.Close();
                }
            }
        }

        public List<VBG00746Result> AgendaAnexo_ListarAlmacen(int idEmpresa, int codigoUsuario)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                   return dcg.VBG00746(codigoUsuario).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar por los almacenes en la base de datos.");
                }
                finally
                {
                    dci.Connection.Close();
                    dcg.Connection.Close();
                }
            }
        }

        public List<VBG03678Result> AgendaAnexo_ListarAlmacenCliente(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            { 
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                     return dcg.VBG03678(idAgenda).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar por los almacenes en la base de datos.");
                }
                finally
                {
                    dci.Connection.Close();
                    dcg.Connection.Close();
                }
            }
        }

        public List<VBG00167Result> AgendaAnexo_ListarDireccionCliente(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                     return dcg.VBG00167(idAgenda, 1, codigoUsuario).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al listar las sucursales de los clientes.");
                }
                finally
                {
                    dci.Connection.Close();
                    dcg.Connection.Close();
                }
            }
        }

        public gsAgenda_BucarProveedorResult Agenda_BucarProveedor(int idEmpresa, int codigoUsuario, string idAgenda, ref bool? existe)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.gsAgenda_BucarProveedor(idAgenda, ref existe).Single();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede buscar el proveedor en la base de datos de Genesys.");
                }
                finally
                {
                    dci.Connection.Close();
                    dcg.Connection.Close();
                }
            }
        }

        public VBG01134Result Agenda_BuscarCliente(int idEmpresa, int codigoUsuario, string idAgenda, ref decimal? lineaCredito, ref DateTime? fechaVencimiento, ref decimal? TC)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                string nombreRelacionComercial = null;
                bool? existeCliente = null;
                DateTime fechaNow; 
                VBG01134Result objCliente;
                List<gsAgenda_BuscarLimiteCreditoResult> lstLineaCredito;
                List<gsAgenda_BuscarDocPendientesResult> lstDocsPendientes;
                List<gsReporte_DocumentosPendientes_top1Result> list;
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    objCliente = dcg.VBG01134(idAgenda, 0, ref nombreRelacionComercial, ref existeCliente).Single();

                    lstLineaCredito = dcg.gsAgenda_BuscarLimiteCredito(idAgenda).ToList();

                    if (lstLineaCredito.Count <= 0)
                    {
                        lineaCredito = 0;
                        TC = 0;
                    }
                    else
                    {
                        TC = lstLineaCredito[0].TC;
                        lineaCredito = lstLineaCredito[0].CreditoDisponible;
                    }


                    if (existeCliente != true)
                    { throw new ArgumentException("El cliente solicitado no existe."); }


                    fechaNow = DateTime.Now; 
                    lstDocsPendientes = dcg.gsAgenda_BuscarDocPendientes(idAgenda, DateTime.Now.Date).ToList();
                    dcg.CommandTimeout = 360;
                    list = dcg.gsReporte_DocumentosPendientes_top1(null, null, null, null, idAgenda, null, null, fechaNow.AddYears(-20), fechaNow, fechaNow.AddYears(-20), fechaNow.AddYears(20), 0).ToList();

                    if(list.Count <=0)
                    {
                        fechaVencimiento = DateTime.Now;

                        //if (lstDocsPendientes.Count <= 0)
                        //{ fechaVencimiento = DateTime.Now; }
                        //else
                        //{ fechaVencimiento = lstDocsPendientes[0].FechaVencimiento; }
                    }
                    else
                    {
                        fechaVencimiento = list[0].FechaVencimiento;
                    }

     
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw ex;
                }
                finally
                {
                    dci.Connection.Close();
                    dcg.Connection.Close();
                }

                return objCliente;
            }
        }

        public Agenda_LimiteCreditoBE Agenda_LineaCredito(int idEmpresa, int codigoUsuario, string idAgenda, decimal? idMoneda)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    Agenda_LimiteCreditoBE objAgenda_LimiteCreditoBE = new Agenda_LimiteCreditoBE();

                    string AgendaNombre = null;
                    string MonedaNombre = null;
                    decimal? LimCreditoMonedaVta = null;
                    decimal? LimCreditoMonedaSol = null;
                    decimal? SaldoCtaCteMonedaSol = null;
                    decimal? CreditoDisponibleMonedaSol = null;
                    bool? EvaluarLimCredito = null;

                    dcg.VBG00521(idAgenda, idMoneda, DateTime.Now.Date, ref AgendaNombre, ref MonedaNombre, ref LimCreditoMonedaVta,
                        ref LimCreditoMonedaSol, ref SaldoCtaCteMonedaSol, ref CreditoDisponibleMonedaSol, ref EvaluarLimCredito, null);

                    objAgenda_LimiteCreditoBE.AgendaNombre = AgendaNombre;
                    objAgenda_LimiteCreditoBE.MonedaNombre = MonedaNombre;
                    objAgenda_LimiteCreditoBE.LimCreditoMonedaVta = LimCreditoMonedaVta;
                    objAgenda_LimiteCreditoBE.LimCreditoMonedaSol = LimCreditoMonedaSol;
                    objAgenda_LimiteCreditoBE.SaldoCtaCteMonedaSol = SaldoCtaCteMonedaSol;
                    objAgenda_LimiteCreditoBE.CreditoDisponibleMonedaSol = CreditoDisponibleMonedaSol;
                    objAgenda_LimiteCreditoBE.EvaluarLimCredito = EvaluarLimCredito;

                    return objAgenda_LimiteCreditoBE;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException("No se pudo buscar el cliente en la base de datos de Genesys.");
                }
                finally
                {
                    dcg.SubmitChanges();
                    dci.SubmitChanges();
                    dci.Connection.Close();
                    dcg.Connection.Close();
                }
          
            }
        }

        public List<gsAgenda_ListarClienteResult> Agenda_ListarCliente(int idEmpresa, int codigoUsuario, string descripcion, int? estado)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                 return dcg.gsAgenda_ListarCliente(descripcion, estado).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede listar los clientes de la base de datos de Genesys.");
                }
                finally
                {
                    dcg.SubmitChanges();
                    dci.SubmitChanges();
                    dci.Connection.Close();
                    dcg.Connection.Close();
                }
            }
        }

        public List<gsAgenda_ListarContactoResult> Agenda_ListarContacto(int idEmpresa, int codigoUsuario, string descripcion, int? estado)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                   return dcg.gsAgenda_ListarContacto(descripcion, estado).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al listar los clientes de la agenda.");
                }
                finally
                {
                    dcg.SubmitChanges();
                    dci.SubmitChanges();
                    dci.Connection.Close();
                    dcg.Connection.Close();
                }
            }
        }

        public List<gsAgenda_ListarProveedorResult> Agenda_ListarProveedor(int idEmpresa, int codigoUsuario, string descripcion)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsAgenda_ListarProveedor(descripcion).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al listar los proveedores de la agenda.");
                }
            }
        }

        public List<gsAgenda_ListarTransportistaResult> Agenda_ListarTransportista(int idEmpresa, int codigoUsuario, string descripcion)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsAgenda_ListarTransportista(descripcion).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR: No se pudo listar los transportistas de la base de datos.");
                }
            }
        }

        public List<gsAgenda_ListarVendedorResult> Agenda_ListarVendedor(int idEmpresa, int codigoUsuario, string descripcion)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsAgenda_ListarVendedor(descripcion, 1).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al buscar el cliente en la agenda.");
                }
            }
        }

        public string Agenda_RegistrarProveedor(int idEmpresa, int codigoUsuario, string nroRUC, string razonSocial)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    string idAgenda = null;
                    decimal? idRelComercial = null;
                    string nombreRelComercial = null; 
                    using (dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)))) {
                        try
                        {
                            if (dcg.Agenda.ToList().FindAll(x => x.ID_Agenda == nroRUC).Count > 0)
                                throw new ArgumentException("El proveedor ya existe en el sistema Genesys.");

                            if (nroRUC.Substring(0, 2) == "10")
                                idAgenda = nroRUC.Substring(2, 8);
                            else
                                idAgenda = nroRUC;

                            dcg.VBG00162(idAgenda, 116, 6, nroRUC, razonSocial, null, null, null, null, null, nroRUC, null, 0, 0, 0, 300, 0, 0,
                               null, null, 0, 0, 0, 1, null, null, null);

                            if (dcg.AgendaRel.ToList().FindAll(x => x.ID_Agenda == nroRUC && x.Res == 2).Count == 0)
                                dcg.VBG01321(ref idRelComercial, idAgenda, ref nombreRelComercial, 2);

                            return idAgenda;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally {
                            dcg.SubmitChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
                finally {
                    dci.SubmitChanges();
                }
            }
        }

        public List<gsChofer_ListarResult> Agenda_ListarChofer(int idEmpresa, int codigoUsuario, string Id_Transportista, string Id_Chofer, string descripcion)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsChofer_Listar( Id_Chofer,Id_Transportista, descripcion ).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR: No se pudo listar los Choferes de la base de datos.");
                }
            }
        }

        public List<gsPlaca_ListarResult> Agenda_ListarPlaca(int idEmpresa, int codigoUsuario, string Id_Transportista, string Id_Vehiculo, string descripcion)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsPlaca_Listar(Id_Vehiculo , Id_Transportista, descripcion).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR: No se pudo listar los transportistas de la base de datos.");
                }
            }
        }

        public List<gsAgenda_BuscarClienteDetalleResult> Agenda_BuscarClienteDetalle(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                List<gsAgenda_BuscarClienteDetalleResult> lstobjCliente;
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    lstobjCliente = dcg.gsAgenda_BuscarClienteDetalle(idAgenda, 0).ToList();
                    return lstobjCliente;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw ex;
                }
            }
        }

        public List<gsAgendaAnexo_ListarAlmacenDevolucionResult> AgendaAnexo_ListarAlmacenDevolucion(int idEmpresa, int codigoUsuario)
        {
         
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsAgendaAnexo_ListarAlmacenDevolucion().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw ex;
                }
            }
        }

        public List<gsPlaca_DespachoResult> Agenda_ListarPlaca_Despacho(int idEmpresa, int codigoUsuario, string Id_Transportista, string Id_Vehiculo, string descripcion, string despacho)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsPlaca_Despacho(Id_Vehiculo, Id_Transportista, descripcion, despacho).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("ERROR: No se pudo listar las placas de transporte de la base de datos.");
                }
            }
        }

        public List<gsZonaSectorista_ListarResult> Agenda_ListarZonaSectorista(int idEmpresa, int codigoUsuario, string id_sectorista)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsZonaSectorista_Listar(id_sectorista).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar por los almacenes en la base de datos.");
                }
            }
        }

        public List<gsVendedores_ListarResult> Agenda_ListarVendedores(int idEmpresa, int codigoUsuario)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsVendedores_Listar().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar por los almacenes en la base de datos.");
                }
            }
        }

        public List<gsClientesXVendedor_ListarResult> Agenda_ListarClientes(int idEmpresa, int codigoUsuario, string idVendedor)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsClientesXVendedor_Listar(idVendedor).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar por los almacenes en la base de datos.");
                }
            }
        }

        public List<Agenda_BuscarEmpresaResult> Agenda_BuscarEmpresa(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                int TotalCliente;
                List<Agenda_BuscarEmpresaResult> lstCliente;
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    lstCliente = dcg.Agenda_BuscarEmpresa(idAgenda).ToList();

                    if (lstCliente.Count <= 0)
                        TotalCliente = 0;
                    else
                        TotalCliente = 1;

                    if (TotalCliente == 0)
                        throw new ArgumentException("El cliente solicitado no existe.");

                    return lstCliente;

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw ex;
                }
            }
        }

        public List<gsUsuario_SectoristaResult> Agenda_ListarSectorista(int idEmpresa, int codigoUsuario, string descripcion, int? estado)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                     return dcg.gsUsuario_Sectorista(descripcion, estado).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede listar los Sectorista de la base de datos de Genesys.");
                }
                finally
                {
                    dcg.SubmitChanges();
                    dci.SubmitChanges();
                    dci.Connection.Close();
                    dcg.Connection.Close();
                }
            }
        }

        public List<gsZona_ListarResult> Agenda_ListarZona(int idEmpresa, int codigoUsuario, int id_zona)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.gsZona_Listar(id_zona).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar por los almacenes en la base de datos.");
                }
                finally
                {
                    dcg.SubmitChanges();
                    dci.SubmitChanges();
                    dci.Connection.Close();
                    dcg.Connection.Close();
                }
            }
        }

        public List<gsVendedor_ListarResult> Agenda_ListarVendedorProyectado(int idEmpresa, int codigoUsuario, int id_zona, string id_vendedor)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                   return dcg.gsVendedor_Listar(id_zona, id_vendedor).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar por los almacenes en la base de datos.");
                }
                finally
                {
                    dcg.SubmitChanges();
                    dci.SubmitChanges();
                    dci.Connection.Close();
                    dcg.Connection.Close();
                }
            }
        }

        public List<gsClientesCorreo_EnvioResult> Agenda_ListarCorreos(int idEmpresa, int codigoUsuario, int id_zona, string id_vendedor)
        {
            List<gsClientesCorreo_EnvioResult> lista;

            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            { ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    lista =  dcg.gsClientesCorreo_Envio(null).ToList();
                    
                    return lista; 
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar por los almacenes en la base de datos.");
                }
                finally
                {
                    dci.Connection.Close();
                    dcg.Connection.Close();
                }
            }
        }

        /* INICIO PSF 5/12/2016 */
        public List<gsAgenda_ListarGarantiaResult> Agenda_ListarGarantia(int idEmpresa, int codigoUsuario, int id_zona, string id_vendedor)
        {
            List<gsAgenda_ListarGarantiaResult> lista;

            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    lista = dcg.gsAgenda_ListarGarantia(id_vendedor).ToList();

                    return lista;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar por los almacenes en la base de datos.");
                }
            }
        }

        /* FIN PSF 5/12/2016*/

        public VBG01134_validarCorreoResult Agenda_ValidarCorreo(int idEmpresa, int codigoUsuario, string idAgenda, ref bool? existeCliente, ref bool? existeCorreo)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                string nombreRelacionComercial = null;
 
                VBG01134_validarCorreoResult objCliente;
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    objCliente = dcg.VBG01134_validarCorreo(idAgenda, 0, ref nombreRelacionComercial, ref existeCliente, ref existeCorreo).Single();
                    return objCliente;

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw ex;
                }
            }
        }


        public void Agenda_RegistrarCorreo(int idEmpresa, int codigoUsuario, string idAgenda, string Correo, ref int? Correlativo)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.Insertar_Email(idAgenda, Correo, ref Correlativo);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw ex;
                }
            }
        }

        public VBG01134Result Agenda_BuscarCliente_Contado(int idEmpresa, int codigoUsuario, string idAgenda, ref decimal? lineaCredito, ref DateTime? fechaVencimiento, int idMoneda)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                string nombreRelacionComercial = null;
                bool? existeCliente = null;
                decimal? AdelantoCliente = null;

                DateTime fechaNow;
                VBG01134Result objCliente;
                List<gsAgenda_BuscarLimiteCreditoResult> lstLineaCredito;
                List<gsAgenda_BuscarDocPendientesResult> lstDocsPendientes;
                List<gsReporte_DocumentosPendientes_top1Result> list;
                gsReporte_DocumentosPendientes_AfavorResult objContado;
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    fechaNow = DateTime.Now;

                  objCliente = dcg.VBG01134(idAgenda, 0, ref nombreRelacionComercial, ref existeCliente).Single();

                    if (existeCliente != true)
                    { throw new ArgumentException("El cliente solicitado no existe."); }

                    lstLineaCredito = dcg.gsAgenda_BuscarLimiteCredito(idAgenda).ToList();
                    objContado = dcg.gsReporte_DocumentosPendientes_Afavor(null, null, null, null, idAgenda, null, null, fechaNow.AddYears(-20), fechaNow, fechaNow.AddYears(-20), fechaNow.AddYears(20), 0).Single(); 



                    if(idMoneda == 0)
                    {
                        AdelantoCliente = Math.Abs((decimal)objContado.PendienteDolares); 
                    }
                    else if (idMoneda == 1)
                    {
                        AdelantoCliente = Math.Abs((decimal)objContado.PendienteSoles);
                    }
                    else  
                    {
                        AdelantoCliente = 0;
                    }

                    //if (lstLineaCredito.Count <= 0)
                    //{ lineaCredito = AdelantoCliente; }
                    //else
                    //{
                    //    lineaCredito = (lstLineaCredito[0].LineaCredito + AdelantoCliente - lstLineaCredito[0].TotalRiesgo);
                    //}

                    lineaCredito = AdelantoCliente;


                    lstDocsPendientes = dcg.gsAgenda_BuscarDocPendientes(idAgenda, DateTime.Now.Date).ToList() ;
                    list = dcg.gsReporte_DocumentosPendientes_top1(null, null, null, null, idAgenda, null, null, fechaNow.AddYears(-20), fechaNow, fechaNow.AddYears(-20), fechaNow.AddYears(20), 0).ToList() ;

                    if (list.Count <= 0)
                    {
                        //if (lstDocsPendientes.Count <= 0)
                        //{
                            fechaVencimiento = DateTime.Now;
                        //}
                        //else
                        //{ fechaVencimiento = lstDocsPendientes[0].FechaVencimiento; }
                    }
                    else
                    {
                        fechaVencimiento = list[0].FechaVencimiento;
                    }

                    return objCliente;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw ex;
                }
                finally
                {
                    dci.Connection.Close();
                    dcg.Connection.Close();
                }
            }
        }

        public RPT00015Result Agenda_TipoCambio(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int ID_Moneda)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                RPT00015Result objTipoCambio = new RPT00015Result();
                try
                {
                    objTipoCambio = dcg.RPT00015(FechaDesde, FechaHasta, ID_Moneda).Single();
                    dci.Connection.Close();
                    dcg.Connection.Close();
                   
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw ex;
                }
                finally
                {
                    dci.Connection.Close();
                    dcg.Connection.Close();
                }

                return objTipoCambio;
            }
          
        }


        public List<gsListarLogLineaCreditoResult> ListarLogLineaCredito(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));


                try
                {
                   
                    return dcg.gsListarLogLineaCredito(idAgenda, idEmpresa).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el historial de línea de crédito del cliente.");
                }
            }
        }

        public List<gsListarObservacionesAgendaResult> ListarObservacionesAgenda(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.gsListarObservacionesAgenda(idAgenda).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar por las observaciones del cliente.");
                }
            }
        }

        public List<GS_RecuperaCorreoAgendaResult> RecuperaCorreoAgenda(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                   return dcg.GS_RecuperaCorreoAgenda(idAgenda).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el correo electrónico del cliente");
                }
            }
        }

        public List<gsAgenda_ContactoResult> Agenda_ListarContacto_Estado(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.gsAgenda_Contacto(idAgenda).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el contacto del cliente");
                }
            }
        }

        public List<gsAgenda_ListarClienteAgenteResult> Agenda_ListarClienteAgente(int idEmpresa, int codigoUsuario, string descripcion, int? estado)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.gsAgenda_ListarClienteAgente(descripcion, estado).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede listar los clientes de la base de datos de Genesys.");
                }
                finally
                {
                    dcg.SubmitChanges();
                    dci.SubmitChanges();
                    dci.Connection.Close();
                    dcg.Connection.Close();
                }
            }
        }


        public List<gsVendedorZona_ListarResult> Agenda_VendedorZonaListar(int idEmpresa, int codigoUsuario, int id_zona, string id_vendedor)
        {
            
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {  
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                try
                {
                    return dcg.gsVendedorZona_Listar(id_zona, id_vendedor).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar por los almacenes en la base de datos.");
                }
                finally
                {
                    dcg.SubmitChanges();
                    dci.SubmitChanges();
                    dci.Connection.Close();
                    dcg.Connection.Close();
                }
            }
        }

    }

}
