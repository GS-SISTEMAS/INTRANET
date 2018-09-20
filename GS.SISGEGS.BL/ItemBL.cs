using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GS.SISGEGS.BE;
using GS.SISGEGS.DM;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface IItemBL {
        List<gsItem_ListarProductoResult> Item_ListarProducto(int idEmpresa, int codigoUsuario, string nombre);
        gsItem_BuscarResult Item_Buscar(int idEmpresa, int codigoUsuario, string idProducto, string idCliente, DateTime? fecha, 
            decimal cantidad, int? idTipoEnlaceContable, decimal? idDireccionOrigen, decimal? idDireccionDestino, decimal idAlmacen,
            ref decimal? StockDisponible, ref double? TC_Cambio);
        VBG00878Result Item_BuscarUnidad(int idEmpresa, int codigoUsuario, string idItem);
        List<gsItem_ListarTipoGastoCCResult> Item_ListarTipoGastoCC(int idEmpresa, int codigoUsuario);
        List<gsItem_ListarStockResult> Item_ListarStock(int idEmpresa, int codigoUsuario, string nombre, decimal? ID_AlmacenAnexo);
        List<gsItem_ListarPrecioClienteResult> Item_ListarPrecioCliente(int idEmpresa, int codigoUsuario, string ID_Agenda, string descripcion);
        void Item_BuscarPrecioCliente(int idEmpresa, int codigoUsuario, decimal idPrecioCliente, ref decimal? precioEspecial,
            ref DateTime? vigInicio, ref DateTime? vigFinal, ref VBG01134Result objCliente, ref VBG01124Result objProducto, ref string ID_Item);
        void Item_RegistrarPrecioCliente(int idEmpresa, int codigoUsuario, ref decimal? idClienteProd, string ID_Item, string ID_Cliente, DateTime? vigInicio,
            DateTime? vigFinal, decimal? precioEspecial);
        VBG01124Result Item_BuscarProducto(int idEmpresa, int codigoUsuario, string ID_Item);
        void Item_EliminarProductoCliente(int idEmpresa, int codigoUsuario, decimal ID_ItemCliente);
        List<gsItem_ListarResult> Item_Listar(int idEmpresa, int codigoUsuario, string Item);

        List<VBG00321Result> Item_Listar_ProductosCompras(int idEmpresa, int codigoUsuario, string Nombre, int CategoriaItem);

        List<VBG04054Result> Item_CategoriasGxOpciones(int idEmpresa, int codigoUsuario, int intTipo);

        List<gsItem_ListarProducto_StockResult> Item_ListarProducto_Stock(int idEmpresa, int codigoUsuario, string nombre);

        List<gsItem_ListarStock_ComercialResult> Item_ListarStock_Comercial(int idEmpresa, int codigoUsuario, string nombre, decimal? ID_AlmacenAnexo, int G2, int G5);

        List<sp_GestionStock_ListarResult> Item_Listar_GestionStock(int idEmpresa, int codigoUsuario, int ID, int id_agendanexo, int id_item, float cantidad, string Observacion, int Operacion);

        void Item_Mantenimiento_GestionStock(int idEmpresa, int codigoUsuario, int ID, int id_agendanexo, int id_item, float cantidad, string Observacion, int Operacion);

        List<spAlmacenesFlete_ListarResult> AlmacenesFlete_Listar(int idEmpresa, int codigoUsuario, int id_agendanexo);
        void Item_RegistrarPrecioClienteLista(int idEmpresa, int codigoUsuario, List<MantenimientoProductos> lstproductos);
        List<gsItem_ListarProductoPresupuestoResult> Item_ListarProductoPresupuesto(int idEmpresa, int codigoUsuario, string nombre);

    }
    public class ItemBL : IItemBL
    {
        public gsItem_BuscarResult Item_Buscar(int idEmpresa, int codigoUsuario, string idProducto, string idCliente, DateTime? fecha, 
            decimal cantidad, int? idTipoEnlaceContable, decimal? idDireccionOrigen, decimal? idDireccionDestino, decimal idAlmacen, 
            ref decimal? StockDisponible, ref double? TC_Cambio)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                gsItem_BuscarResult objItem = new gsItem_BuscarResult();
                List<gsItem_BuscarResult> listItem;
                List<VBG00939Result> objStock;
                string Dolares = "";
                string Soles = "";
                double? TC_Dolares = 0;
                double? TC_Soles = 0; 

                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    listItem = dcg.gsItem_Buscar(idProducto, idCliente, fecha, cantidad, idTipoEnlaceContable, idDireccionOrigen, idDireccionDestino).ToList();
                    objItem = listItem.Single(); 

                    objStock = dcg.VBG00939(null, objItem.Item_ID, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList().FindAll(x=>x.ID_Almacen == idAlmacen);
                    dcg.VBG02485(0, 1, ref Dolares, ref Soles, fecha, 480, ref TC_Dolares, ref TC_Soles);
                    TC_Cambio = TC_Soles; 

                    if (objStock.Count == 0)
                        StockDisponible = 0;
                    else
                        StockDisponible = objStock[0].StockDisponible;
                    return objItem;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los items en la base de datos.");
                }

            }
        }

        public VBG00878Result Item_BuscarUnidad(int idEmpresa, int codigoUsuario, string idItem)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.VBG00878(idItem).Single();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las unidades del item " + idItem + " en la base de datos.");
                }

            }
        }

        public List<gsItem_ListarTipoGastoCCResult> Item_ListarTipoGastoCC(int idEmpresa, int codigoUsuario)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsItem_ListarTipoGastoCC().ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los gastos en la base de datos.");
                }

            }
        }

        public List<gsItem_ListarProductoResult> Item_ListarProducto(int idEmpresa, int codigoUsuario, string nombre)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    List<gsItem_ListarProductoResult> lista = new List<gsItem_ListarProductoResult>();
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    lista = dcg.gsItem_ListarProducto(nombre).ToList();

                    return lista;


                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los items en la base de datos.");
                }
            }
        }

        public List<gsItem_ListarStockResult> Item_ListarStock(int idEmpresa, int codigoUsuario, string nombre, decimal? ID_AlmacenAnexo)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsItem_ListarStock(null, null, nombre, null, null, ID_AlmacenAnexo, null, codigoUsuario).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los items en la base de datos.");
                }

            }
        }

        public List<gsItem_ListarPrecioClienteResult> Item_ListarPrecioCliente(int idEmpresa, int codigoUsuario, string ID_Agenda, string descripcion)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsItem_ListarPrecioCliente(ID_Agenda, descripcion).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede listar los precios de los clientes consultados.");
                }

            }
        }

        public void Item_BuscarPrecioCliente(int idEmpresa, int codigoUsuario, decimal idPrecioCliente, ref decimal? precioEspecial,
            ref DateTime? vigInicio, ref DateTime? vigFinal, ref VBG01134Result objCliente, ref VBG01124Result objProducto, ref string ID_Item)
        {
            //VBG01124_v1Result producto = new VBG01124_v1Result(); 

            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                string ID_Cliente = null;
                int? tipoDscto = null ;
                decimal? dsctoAdicional = null , iD_ClienteSucursal = null;
                string nomRelComercial = null;
                bool? existe = null;
                try
                {
                    dcg.VBG01312(idPrecioCliente, ref ID_Cliente, ref ID_Item, ref vigInicio, ref vigFinal, ref precioEspecial, ref tipoDscto, ref dsctoAdicional, ref iD_ClienteSucursal );
                    objCliente = dcg.VBG01134(ID_Cliente, 0, ref nomRelComercial, ref existe).Single();
                    objProducto = dcg.VBG01124(ID_Item, null, null).Single();
                    //objProducto = producto; 
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede listar los precios de los clientes consultados.");
                }

            }
        }

        public void Item_RegistrarPrecioClienteLista(int idEmpresa, int codigoUsuario, List<MantenimientoProductos> lstproductos)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                string descripcion = null, moneda = null;
                decimal? precio = null, descuentoEspecial = null, precioEspecial = null, porcentajeDctoAdicional = 0;
                decimal? idClienteProd = null;
                try
                {
                    foreach (MantenimientoProductos ep in lstproductos)
                    {
                        dcg.VBG01313(ref idClienteProd, ep.ItemCodigo, ep.Id_Agenda, ref descripcion, ref moneda, ep.FechaVigInicio, ep.FechaVigFin,
                            ref precio, ref descuentoEspecial, ref precioEspecial,Convert.ToDecimal(ep.PrecioEspecial), 3, ref porcentajeDctoAdicional, null);

                        idClienteProd = null;
                        descripcion = null;
                        moneda = null;
                        precio = null;
                        descuentoEspecial = null;
                        precioEspecial = null;
                        porcentajeDctoAdicional = 0;
                    }
                }
                catch (Exception ex)
                {
                    bool duplicado = false;

                    string mensaje = ex.Message;
                    duplicado = mensaje.Contains("duplicada");

                    if (duplicado == true)
                    {
                        mensaje = "Ya existe un registro de precio igual. ";
                    }
                    else
                    {
                        mensaje = "";
                    }

                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    dcg.SubmitChanges();
                    throw new ArgumentException(mensaje + "No se registró el precio del cliente en Genesys. ");
                }

            }
        }

        public void Item_RegistrarPrecioCliente(int idEmpresa, int codigoUsuario, 
            ref decimal? idClienteProd, string ID_Item, string ID_Cliente, DateTime? vigInicio, 
            DateTime? vigFinal, decimal? precioCliente)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                string descripcion = null, moneda = null;
                decimal? precio = null, descuentoEspecial = null, precioEspecial = null, porcentajeDctoAdicional = 0;
                try
                {
                    dcg.VBG01313(ref idClienteProd, ID_Item, ID_Cliente, ref descripcion, ref moneda, vigInicio, vigFinal, ref precio,
                        ref descuentoEspecial, ref precioEspecial, precioCliente, 3, ref porcentajeDctoAdicional, null);
                }
                catch (Exception ex)
                {
                    bool duplicado = false; 

                    string mensaje = ex.Message;
                    duplicado = mensaje.Contains("duplicada");  

                    if(duplicado==true)
                    {
                        mensaje = "Ya existe un registro de precio igual. "; 
                    }
                    else
                    {
                        mensaje = ""; 
                    }

                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    dcg.SubmitChanges();
                    throw new ArgumentException(mensaje + "No se registró el precio del cliente en Genesys. ");
                }

            }
        }

        public VBG01124Result Item_BuscarProducto(int idEmpresa, int codigoUsuario, string ID_Item)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    return dcg.VBG01124(ID_Item, null, null).Single();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puedo buscar el item en Genesys.");
                }

            }
        }

        public void Item_EliminarProductoCliente(int idEmpresa, int codigoUsuario, decimal ID_ItemCliente)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    dcg.VBG01314(ID_ItemCliente);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException("No se puedo eliminar el item en Genesys.");
                }
                finally {
                    dci.SubmitChanges();
                    dcg.SubmitChanges();
                }
            }
        }

        public List<gsItem_ListarResult> Item_Listar(int idEmpresa, int codigoUsuario, string Item)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    return dcg.gsItem_Listar(Item).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puedo buscar el item en Genesys.");
                }

            }
        }

        public List<VBG00321Result> Item_Listar_ProductosCompras(int idEmpresa, int codigoUsuario, string Nombre, int CategoriaItem)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    List<VBG00321Result> lista = new List<VBG00321Result>();
                    lista = dcg.VBG00321(null, Nombre, null, null, null, CategoriaItem, null, 0, 0, 0, 0, 0, 0, 0, 0, 0).ToList();

                    return lista; 

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puedo buscar el item en Genesys.");
                }

            }
        }

        public List<VBG04054Result> Item_CategoriasGxOpciones(int idEmpresa, int codigoUsuario,  int intTipo)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    List<VBG04054Result> lista = new List<VBG04054Result>();
                    lista = dcg.VBG04054(intTipo, null, null, null, null, null, null, null, null, null).ToList(); 
                    return lista;

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puedo buscar el item en Genesys, VBG04054.");
                }

            }
        }

        public List<gsItem_ListarProducto_StockResult> Item_ListarProducto_Stock(int idEmpresa, int codigoUsuario, string nombre)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    List<gsItem_ListarProducto_StockResult> lista = new List<gsItem_ListarProducto_StockResult>();
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    lista = dcg.gsItem_ListarProducto_Stock(nombre).ToList();

                    return lista;


                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los items en la base de datos.");
                }
            }
        }

        public List<gsItem_ListarStock_ComercialResult> Item_ListarStock_Comercial(int idEmpresa, int codigoUsuario, string nombre, decimal? ID_AlmacenAnexo, int G2, int G5 )
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    List<gsItem_ListarStock_ComercialResult> lista = new List<gsItem_ListarStock_ComercialResult>();

                    dcg.CommandTimeout = 120; 
                    lista = dcg.gsItem_ListarStock_Comercial(null, null, nombre, null, null, ID_AlmacenAnexo, null, G2, G5).ToList();

                    return lista; 
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los items en la base de datos.");
                }

            }
        }

        public List<sp_GestionStock_ListarResult> Item_Listar_GestionStock(int idEmpresa, int codigoUsuario, int ID, int id_agendanexo, int id_item, float cantidad, string Observacion, int Operacion)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    List<sp_GestionStock_ListarResult> lista = new List<sp_GestionStock_ListarResult>();

                    dcg.CommandTimeout = 120;
                    lista = dcg.sp_GestionStock_Listar(ID, id_agendanexo, id_item, cantidad, Observacion, 1, Operacion).ToList();

                    return lista;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los items en la base de datos.");
                }

            }
        }

        public void Item_Mantenimiento_GestionStock(int idEmpresa, int codigoUsuario, int ID, int id_agendanexo, int id_item, float cantidad, string Observacion, int Operacion)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                try
                {
                    dcg.sp_GestionStock_Mantenimiento(ID, id_agendanexo, id_item, cantidad, Observacion, 1, Operacion);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw new ArgumentException("No se puedo eliminar el item en Genesys, sp_GestionStock_Listar.");
                }
                finally
                {
                    dci.SubmitChanges();
                    dcg.SubmitChanges();
                }
            }
        }

        // Cobro Flete 
        public List<spAlmacenesFlete_ListarResult> AlmacenesFlete_Listar(int idEmpresa, int codigoUsuario, int id_agendanexo )
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    List<spAlmacenesFlete_ListarResult> lista = new List<spAlmacenesFlete_ListarResult>();

                    dcg.CommandTimeout = 120;
                    lista = dcg.spAlmacenesFlete_Listar( id_agendanexo).ToList();

                    return lista;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar spAlmacenesFlete_Listar" );
                }

            }
        }

        public List<gsItem_ListarProductoPresupuestoResult> Item_ListarProductoPresupuesto(int idEmpresa, int codigoUsuario, string nombre)
        {
            
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    List<gsItem_ListarProductoPresupuestoResult> lista = new List<gsItem_ListarProductoPresupuestoResult>();
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    lista = dcg.gsItem_ListarProductoPresupuesto(nombre).ToList();

                    return lista;


                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los items en la base de datos.");
                }
            }
        }

    }
}
