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
        List<gsAgenda_ListarClienteResult> Agenda_ListarCliente(int idEmpresa, int codigoUsuario, string descripcion); 
        List<gsAgenda_ListarVendedorResult> Agenda_ListarVendedor(int idEmpresa, int codigoUsuario, string descripcion);
        VBG01134Result Agenda_BuscarCliente(int idEmpresa, int codigoUsuario, string idAgenda, ref decimal? lineaCredito);
        List<VBG00167Result> AgendaAnexo_ListarDireccionCliente(int idEmpresa, int codigoUsuario, string idAgenda);
        //List<VBG00698Result> Agenda_ListarVendedorTodos(int idEmpresa);
        List<VBG00746Result> AgendaAnexo_ListarAlmacen(int idEmpresa, int codigoUsuario);
        List<VBG03678Result> AgendaAnexo_ListarAlmacenCliente(int idEmpresa, int codigoUsuario, string idAgenda);
        List<VBG02699Result> AgendaAnexoReferencia_ListarPorSucursal(int idEmpresa, int codigoUsuario, int idAgendaAnexo, string idAgenda);
        Agenda_LimiteCreditoBE Agenda_LineaCredito(int idEmpresa, int codigoUsuario, string idAgenda, decimal? idMoneda);
        List<gsAgenda_ListarContactoResult> Agenda_ListarContacto(int idEmpresa, int codigoUsuario, string descripcion, int? estado);
        gsAgenda_BucarProveedorResult Agenda_BucarProveedor(int idEmpresa, int codigoUsuario, string idAgenda, ref bool? existe);
        List<gsAgenda_ListarProveedorResult> Agenda_ListarProveedor(int idEmpresa, int codigoUsuario, string descripcion);
        List<gsAgenda_ListarTransportistaResult> Agenda_ListarTransportista(int idEmpresa, int codigoUsuario, string descripcion);
        string Agenda_RegistrarProveedor(int idEmpresa, int codigoUsuario, string nroRUC, string razonSocial);
    }

    public class AgendaBL : IAgendaBL
    {
        public List<VBG02699Result> AgendaAnexoReferencia_ListarPorSucursal(int idEmpresa, int codigoUsuario, int idAgendaAnexo, string idAgenda)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.VBG02699(idAgendaAnexo, idAgenda).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar por las referencias de la sucursal en la base de datos.");
                }
            }
        }

        public List<VBG00746Result> AgendaAnexo_ListarAlmacen(int idEmpresa, int codigoUsuario)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.VBG00746(codigoUsuario).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar por los almacenes en la base de datos.");
                }
            }
        }

        public List<VBG03678Result> AgendaAnexo_ListarAlmacenCliente(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.VBG03678(idAgenda).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar por los almacenes en la base de datos.");
                }
            }
        }

        public List<VBG00167Result> AgendaAnexo_ListarDireccionCliente(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.VBG00167(idAgenda, 1, codigoUsuario).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al listar las sucursales de los clientes.");
                }
            }
        }

        public gsAgenda_BucarProveedorResult Agenda_BucarProveedor(int idEmpresa, int codigoUsuario, string idAgenda, ref bool? existe)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsAgenda_BucarProveedor(idAgenda, ref existe).Single();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede buscar el proveedor en la base de datos de Genesys.");
                }
            }
        }

        public VBG01134Result Agenda_BuscarCliente(int idEmpresa, int codigoUsuario, string idAgenda, ref decimal? lineaCredito)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                string nombreRelacionComercial = null;
                bool? existeCliente = null;
                VBG01134Result objCliente;
                List<gsAgenda_BuscarLimiteCreditoResult> lstLineaCredito;
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    objCliente = dcg.VBG01134(idAgenda, 0, ref nombreRelacionComercial, ref existeCliente).Single();
                    lstLineaCredito = dcg.gsAgenda_BuscarLimiteCredito(idAgenda).ToList();
                    if (lstLineaCredito.Count <= 0)
                        lineaCredito = 0;
                    else
                        lineaCredito = lstLineaCredito[0].CreditoDisponible;
                    if (existeCliente != true)
                        throw new ArgumentException("El cliente solicitado no existe.");
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

        public Agenda_LimiteCreditoBE Agenda_LineaCredito(int idEmpresa, int codigoUsuario, string idAgenda, decimal? idMoneda)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
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
                }
            }
        }

        public List<gsAgenda_ListarClienteResult> Agenda_ListarCliente(int idEmpresa, int codigoUsuario, string descripcion)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsAgenda_ListarCliente(descripcion, 1).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede listar los clientes de la base de datos de Genesys.");
                }
            }
        }

        public List<gsAgenda_ListarContactoResult> Agenda_ListarContacto(int idEmpresa, int codigoUsuario, string descripcion, int? estado)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsAgenda_ListarContacto(descripcion, estado).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al listar los clientes de la agenda.");
                }
            }
        }

        public List<gsAgenda_ListarProveedorResult> Agenda_ListarProveedor(int idEmpresa, int codigoUsuario, string descripcion)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
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
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
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
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
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
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    string idAgenda = null;
                    decimal? idRelComercial = null;
                    string nombreRelComercial = null;
                    using (dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)))) {
                        try
                        {
                            if (dcg.Agenda.ToList().FindAll(x => x.ID_Agenda == nroRUC).Count > 0)
                                throw new ArgumentException("El proveedor ya existe en el sistema Genesys.");

                            if (nroRUC.Substring(0, 2) == "10")
                                idAgenda = nroRUC.Substring(2, 8);
                            else
                                idAgenda = nroRUC;

                            dcg.VBG00162(idAgenda, 116, 6, nroRUC, razonSocial, null, null, null, null, null, nroRUC, null, 0, 0, 0, 300, 0, 0,
                               null, null, 0, 0, 0, 1, null, null);
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
    }
}
