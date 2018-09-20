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

        public VBG01134Result Agenda_BuscarCliente(int idEmpresa, int codigoUsuario, string idAgenda, ref decimal? lineaCredito)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_BuscarCliente(idEmpresa, codigoUsuario, idAgenda, ref lineaCredito);
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

        public List<gsAgenda_ListarClienteResult> Agenda_ListarCliente(int idEmpresa, int codigoUsuario, string descripcion)
        {
            AgendaBL objAgendaBL;
            try
            {
                objAgendaBL = new AgendaBL();
                return objAgendaBL.Agenda_ListarCliente(idEmpresa, codigoUsuario, descripcion);
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
    }
}
