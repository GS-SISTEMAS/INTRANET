using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using GS.SISGEGS.DM;
using GS.SISGEGS.BL;
using GS.SISGEGS.BE;

namespace GS.SISGEGS.WCF
{
    public class AgendaWCF : IAgendaWCF
    {
        public List<VBG02699Result> AgendaAnexoReferencia_ListarPorSucursal(int idEmpresa, int codigoUsuario, int idAgendaAnexo, string idAgenda)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.AgendaAnexoReferencia_ListarPorSucursal(idEmpresa, codigoUsuario, idAgendaAnexo, idAgenda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VBG00746Result> AgendaAnexo_ListarAlmacen(int idEmpresa, int codigoUsuario)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.AgendaAnexo_ListarAlmacen(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VBG03678Result> AgendaAnexo_ListarAlmacenCliente(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.AgendaAnexo_ListarAlmacenCliente(idEmpresa, codigoUsuario, idAgenda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VBG00167Result> AgendaAnexo_ListarDireccionCliente(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.AgendaAnexo_ListarDireccionCliente(idEmpresa, codigoUsuario, idAgenda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public gsAgenda_BucarProveedorResult Agenda_BucarProveedor(int idEmpresa, int codigoUsuario, string idAgenda, ref bool? existe)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_BucarProveedor(idEmpresa, codigoUsuario, idAgenda, ref existe);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VBG01134Result Agenda_BuscarCliente(int idEmpresa, int codigoUsuario, string idAgenda, ref decimal? lineaCredito, 
            ref DateTime? fechaVencimiento, ref decimal? TC)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_BuscarCliente(idEmpresa, codigoUsuario, idAgenda, ref lineaCredito, ref fechaVencimiento, ref TC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Agenda_LimiteCreditoBE Agenda_LineaCredito(int idEmpresa, int codigoUsuario, string idAgenda, decimal idMoneda)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_LineaCredito(idEmpresa, codigoUsuario, idAgenda, idMoneda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsAgenda_ListarClienteResult> Agenda_ListarCliente(int idEmpresa, int codigoUsuario, string descripcion, int? estado)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_ListarCliente(idEmpresa, codigoUsuario, descripcion, estado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsAgenda_ListarContactoResult> Agenda_ListarContacto(int idEmpresa, int codigoUsuario, string descripcion, int? estado)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_ListarContacto(idEmpresa, codigoUsuario, descripcion, estado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsAgenda_ListarProveedorResult> Agenda_ListarProveedor(int idEmpresa, int codigoUsuario, string descripcion)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_ListarProveedor(idEmpresa, codigoUsuario, descripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsAgenda_ListarTransportistaResult> Agenda_ListarTransportista(int idEmpresa, int codigoUsuario, string descripcion)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_ListarTransportista(idEmpresa, codigoUsuario, descripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsAgenda_ListarVendedorResult> Agenda_ListarVendedor(int idEmpresa, int codigoUsuario, string descripcion)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_ListarVendedor(idEmpresa, codigoUsuario, descripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Agenda_RegistrarProveedor(int idEmpresa, int codigoUsuario, string nroRUC, string razonSocial)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_RegistrarProveedor(idEmpresa, codigoUsuario, nroRUC, razonSocial);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsChofer_ListarResult> Agenda_ListarChofer(int idEmpresa, int codigoUsuario, string Id_Transportista, string Id_Chofer, string descripcion)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_ListarChofer(idEmpresa, codigoUsuario, Id_Transportista, Id_Chofer, descripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsPlaca_ListarResult> Agenda_ListarPlaca(int idEmpresa, int codigoUsuario, string Id_Transportista, string Id_Vehiculo, string descripcion)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_ListarPlaca(idEmpresa, codigoUsuario, Id_Transportista, Id_Vehiculo, descripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsAgenda_BuscarClienteDetalleResult> Agenda_BuscarClienteDetalle(int idEmpresa, int codigoUsuario, string Id_Agenda)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_BuscarClienteDetalle(idEmpresa, codigoUsuario, Id_Agenda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsPlaca_DespachoResult> Agenda_ListarPlaca_Despacho(int idEmpresa, int codigoUsuario, string Id_Transportista, string Id_Vehiculo, string descripcion, string despacho)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_ListarPlaca_Despacho(idEmpresa, codigoUsuario, Id_Transportista, Id_Vehiculo, descripcion, despacho);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsZonaSectorista_ListarResult> Agenda_ListarZonaSectorista(int idEmpresa, int codigoUsuario, string id_sectorista)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_ListarZonaSectorista(idEmpresa, codigoUsuario, id_sectorista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsVendedores_ListarResult> Agenda_ListarVendedores(int idEmpresa, int codigoUsuario)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_ListarVendedores(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsClientesXVendedor_ListarResult> Agenda_ListarClientes(int idEmpresa, int codigoUsuario, string idVendedor)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_ListarClientes(idEmpresa, codigoUsuario, idVendedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Agenda_BuscarEmpresaResult> Agenda_BuscarEmpresa(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_BuscarEmpresa(idEmpresa, codigoUsuario, idAgenda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsUsuario_SectoristaResult> Agenda_ListarSectorista(int idEmpresa, int codigoUsuario, string descripcion, int? estado)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_ListarSectorista(idEmpresa, codigoUsuario, descripcion, estado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<gsZona_ListarResult> Agenda_ListarZona(int idEmpresa, int codigoUsuario, int id_zona)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_ListarZona(idEmpresa, codigoUsuario, id_zona);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsVendedor_ListarResult> Agenda_ListarVendedorProyectado(int idEmpresa, int codigoUsuario, int id_zona, string id_vendedor)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_ListarVendedorProyectado(idEmpresa, codigoUsuario, id_zona, id_vendedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<gsClientesCorreo_EnvioResult> Agenda_ListarCorreos(int idEmpresa, int codigoUsuario, int id_zona, string id_vendedor)
        {
            List<gsClientesCorreo_EnvioResult> lista; 

            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                lista = objAgendaBL.Agenda_ListarCorreos(idEmpresa, codigoUsuario, id_zona, id_vendedor);
                return lista; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<gsAgenda_ListarGarantiaResult> Agenda_ListarGarantia(int idEmpresa, int codigoUsuario, int id_zona, string id_vendedor)
        {
            List<gsAgenda_ListarGarantiaResult> lista;

            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                lista = objAgendaBL.Agenda_ListarGarantia(idEmpresa, codigoUsuario, id_zona, id_vendedor);
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public VBG01134_validarCorreoResult Agenda_ValidarCorreo(int idEmpresa, int codigoUsuario, string idAgenda, ref bool? existeCliente, ref bool? existeCorreo)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_ValidarCorreo(idEmpresa, codigoUsuario, idAgenda, ref existeCliente, ref existeCorreo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Agenda_RegistrarCorreo(int idEmpresa, int codigoUsuario, string idAgenda, string Correo, ref int? Correlativo)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                objAgendaBL.Agenda_RegistrarCorreo(idEmpresa, codigoUsuario, idAgenda, Correo, ref Correlativo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VBG01134Result Agenda_BuscarCliente_Contado(int idEmpresa, int codigoUsuario, string idAgenda, ref decimal? lineaCredito,
    ref DateTime? fechaVencimiento, int idMoneda)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_BuscarCliente_Contado(idEmpresa, codigoUsuario, idAgenda, ref lineaCredito, ref fechaVencimiento, idMoneda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RPT00015Result Agenda_TipoCambio(int idEmpresa, int codigoUsuario, DateTime FechaDesde, DateTime FechaHasta, int ID_Moneda)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_TipoCambio(idEmpresa, codigoUsuario, FechaDesde, FechaHasta, ID_Moneda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsListarLogLineaCreditoResult> ListarLogLineaCredito(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.ListarLogLineaCredito(idEmpresa, codigoUsuario, idAgenda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsListarObservacionesAgendaResult> ListarObservacionesAgenda(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.ListarObservacionesAgenda(idEmpresa, codigoUsuario, idAgenda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GS_RecuperaCorreoAgendaResult> RecuperaCorreoAgenda(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.RecuperaCorreoAgenda(idEmpresa, codigoUsuario, idAgenda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsAgenda_ContactoResult> Agenda_ListarContacto_Estado(int idEmpresa, int codigoUsuario, string idAgenda)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_ListarContacto_Estado(idEmpresa, codigoUsuario, idAgenda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsAgenda_ListarClienteAgenteResult> Agenda_ListarClienteAgente(int idEmpresa, int codigoUsuario, string descripcion, int? estado)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_ListarClienteAgente(idEmpresa, codigoUsuario, descripcion, estado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsVendedorZona_ListarResult> Agenda_VendedorZonaListar(int idEmpresa, int codigoUsuario, int id_zona, string id_vendedor)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_VendedorZonaListar (idEmpresa, codigoUsuario, id_zona, id_vendedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
