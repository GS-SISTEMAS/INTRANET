using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GS.SISGEGS.DM;
using System.Configuration;
using System.Transactions;

namespace GS.SISGEGS.BL
{
    public interface IDevolucionBL {
        List<gsDevolucionMotivo_ComboBoxResult> DevolucionMotivo_ComboBox(int idEmpresa, int codigoUsuario);
        void DevolucionSolicitud_Registrar(int idEmpresa, int codigoUsuario, gsDevolucionSolicitud objDevolucionSolicitud, List<gsDevolucionSolicitudDetalle> lstProductos);
        List<gsDevolucionSolicitud_ListarResult> DevolucionSolicitud_Listar(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, string ID_Vendedor, string ID_Cliente);
        gsDevolucionSolicitud_BuscarResult DevolucionSolicitud_Buscar(int idEmpresa, int codigoUsuario, int idDevolucionSolicitud, ref List<gsDevolucionSolicitudDetalle_BuscarResult> lstProductos);
        void DevolucionSolicitud_Aprobar(int idEmpresa, int codigoUsuario, int idDevolucionSolicitud);
        void DevolucionSolicitud_Eliminar(int idEmpresa, int codigoUsuario, int idDevolucionSolicitud);
    }

    public class DevolucionBL : IDevolucionBL
    {
        public List<gsDevolucionMotivo_ComboBoxResult> DevolucionMotivo_ComboBox(int idEmpresa, int codigoUsuario)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsDevolucionMotivo_ComboBox().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede listar los motivos de la base de datos Genesys.");
                }
            }
        }

        public void DevolucionSolicitud_Aprobar(int idEmpresa, int codigoUsuario, int idDevolucionSolicitud)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        dcg.gsDevolucionSolicitud_Aprobar(idDevolucionSolicitud, codigoUsuario);
                        
                        dcg.SubmitChanges();
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo registrar la solicitud de devolución en el sistema");
                }
            }
        }

        public gsDevolucionSolicitud_BuscarResult DevolucionSolicitud_Buscar(int idEmpresa, int codigoUsuario, int idDevolucionSolicitud, ref List<gsDevolucionSolicitudDetalle_BuscarResult> lstProductos)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    lstProductos = dcg.gsDevolucionSolicitudDetalle_Buscar(idDevolucionSolicitud).ToList();
                    return dcg.gsDevolucionSolicitud_Buscar(idDevolucionSolicitud).Single();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede listar los motivos de la base de datos Genesys.");
                }
            }
        }

        public void DevolucionSolicitud_Eliminar(int idEmpresa, int codigoUsuario, int idDevolucionSolicitud)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        dcg.gsDevolucionSolicitud_Eliminar(idDevolucionSolicitud, codigoUsuario);

                        dcg.SubmitChanges();
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo registrar la solicitud de devolución en el sistema");
                }
            }
        }

        public List<gsDevolucionSolicitud_ListarResult> DevolucionSolicitud_Listar(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, string ID_Vendedor, string ID_Cliente)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<gsDevolucionSolicitud_ListarResult> Lista = new List<gsDevolucionSolicitud_ListarResult>(); 

                    Lista =  dcg.gsDevolucionSolicitud_Listar(fechaInicio, fechaFinal, codigoUsuario, ID_Vendedor, ID_Cliente).ToList();

                    return Lista; 
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los centros de costo en la base de datos.");
                }

            }
        }

        public void DevolucionSolicitud_Registrar(int idEmpresa, int codigoUsuario, gsDevolucionSolicitud objDevolucionSolicitud, List<gsDevolucionSolicitudDetalle> lstProductos)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                int idDevolucionSolicitud;
                ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        idDevolucionSolicitud = dcg.gsDevolucionSolicitud_Registrar(objDevolucionSolicitud.idDevolucionSolicitud, objDevolucionSolicitud.Op, objDevolucionSolicitud.idDevolucionMotivo,
                            objDevolucionSolicitud.ID_Almacen, objDevolucionSolicitud.fechaEnvioDev, objDevolucionSolicitud.guiaCliente, objDevolucionSolicitud.guiaTransportista, objDevolucionSolicitud.ID_Transportista, 
                            objDevolucionSolicitud.fechaEnvio, objDevolucionSolicitud.observacion, objDevolucionSolicitud.CodUsuarioRegistro);

                        foreach (gsDevolucionSolicitudDetalle objProducto in lstProductos)
                        {
                            dcg.gsDevolucionSolicitudDetalle_Registrar(idDevolucionSolicitud, objProducto.ID_Amarre, objProducto.precioUnitario,
                                objProducto.cantidad, objProducto.CodUsuarioRegistro);
                        }

                        dcg.SubmitChanges();
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo registrar la solicitud de devolución en el sistema");
                }
            }
        }
    }
}
