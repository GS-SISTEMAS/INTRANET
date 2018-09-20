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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ItemWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ItemWCF.svc o ItemWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ItemWCF : IItemWCF
    {
        public gsItem_BuscarResult Item_Buscar(int idEmpresa, int codigoUsuario, string idProducto, string idCliente, 
            DateTime? fecha, decimal cantidad, int? idTipoEnlaceContable, decimal? idDireccionOrigen, decimal? idDireccionDestino,
            decimal idAlmacen, ref decimal? StockDisponible, ref double? TC_Cambio)
        {
            ItemBL objItemBL;
            try
            {
                objItemBL = new ItemBL();
                return objItemBL.Item_Buscar(idEmpresa, codigoUsuario, idProducto, idCliente, fecha, cantidad, idTipoEnlaceContable, 
                    idDireccionOrigen, idDireccionDestino, idAlmacen, ref StockDisponible,ref TC_Cambio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsItem_ListarTipoGastoCCResult> Item_ListarTipoGastoCC(int idEmpresa, int codigoUsuario)
        {
            ItemBL objItemBL;
            try
            {
                objItemBL = new ItemBL();
                return objItemBL.Item_ListarTipoGastoCC(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsItem_ListarProductoResult> Item_ListarProducto(int idEmpresa, int codigoUsuario, string nombre)
        {
            ItemBL objItemBL;
            try
            {
                objItemBL = new ItemBL();
                return objItemBL.Item_ListarProducto(idEmpresa, codigoUsuario, nombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsItem_ListarStockResult> Item_ListarStock(int idEmpresa, int codigoUsuario, string nombre, decimal? ID_AlmacenAnexo)
        {
            ItemBL objItemBL;
            try
            {
                objItemBL = new ItemBL();
                return objItemBL.Item_ListarStock(idEmpresa, codigoUsuario, nombre, ID_AlmacenAnexo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsItem_ListarPrecioClienteResult> Item_ListarPrecioCliente(int idEmpresa, int codigoUsuario, string ID_Agenda, string descripcion)
        {
            ItemBL objItemBL;
            try
            {
                objItemBL = new ItemBL();
                return objItemBL.Item_ListarPrecioCliente(idEmpresa, codigoUsuario, ID_Agenda, descripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Item_BuscarPrecioCliente(int idEmpresa, int codigoUsuario, decimal idPrecioCliente, ref decimal? precioEspecial, 
            ref DateTime? vigInicio, ref DateTime? vigFinal, ref VBG01134Result objCliente, ref VBG01124Result objProducto, ref string ID_Item)
        {
            ItemBL objItemBL;
            try
            {
                objItemBL = new ItemBL();
                objItemBL.Item_BuscarPrecioCliente(idEmpresa, codigoUsuario, idPrecioCliente,  ref precioEspecial, ref vigInicio, 
                    ref vigFinal,  ref objCliente, ref objProducto, ref ID_Item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Item_RegistrarPrecioClienteLista(int idEmpresa, int codigoUsuario, List<MantenimientoProductos> lstproductos)
        {
            try
            {
                ItemBL objItemBL = new ItemBL();
                objItemBL.Item_RegistrarPrecioClienteLista(idEmpresa, codigoUsuario, lstproductos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Item_RegistrarPrecioCliente(int idEmpresa, int codigoUsuario, ref decimal? idClienteProd, string ID_Item, string ID_Cliente, 
            DateTime? vigInicio, DateTime? vigFinal, decimal? precioEspecial)
        {
            try
            {
                ItemBL objItemBL = new ItemBL();
                objItemBL.Item_RegistrarPrecioCliente(idEmpresa, codigoUsuario, ref idClienteProd, ID_Item, ID_Cliente,
                    vigInicio, vigFinal, precioEspecial);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VBG01124Result Item_BuscarProducto(int idEmpresa, int codigoUsuario, string ID_Item)
        {
            try
            {
                ItemBL objItemBL = new ItemBL();
                return objItemBL.Item_BuscarProducto(idEmpresa, codigoUsuario, ID_Item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Item_EliminarProductoCliente(int idEmpresa, int codigoUsuario, decimal ID_ItemCliente)
        {
            try
            {
                ItemBL objItemBL = new ItemBL();
                objItemBL.Item_EliminarProductoCliente(idEmpresa, codigoUsuario, ID_ItemCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsItem_ListarResult> Item_Listar(int idEmpresa, int codigoUsuario, string Item)
        {
            try
            {
                ItemBL objItemBL = new ItemBL();
                return objItemBL.Item_Listar(idEmpresa, codigoUsuario, Item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VBG00321Result> Item_Listar_ProductosCompras(int idEmpresa, int codigoUsuario, string Nombre, int CategoriaItem)
        {
            List<VBG00321Result> lista = new List<VBG00321Result>();
            try
            {
                ItemBL objItemBL = new ItemBL();

                lista = objItemBL.Item_Listar_ProductosCompras(idEmpresa, codigoUsuario, Nombre, CategoriaItem);
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VBG04054Result> Item_CategoriasGxOpciones(int idEmpresa, int codigoUsuario, int intTipo)
        {
            List<VBG04054Result> lista = new List<VBG04054Result>();
            try
            {
                ItemBL objItemBL = new ItemBL();

                lista = objItemBL.Item_CategoriasGxOpciones(idEmpresa, codigoUsuario, intTipo);
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsItem_ListarProducto_StockResult> Item_ListarProducto_Stock(int idEmpresa, int codigoUsuario, string nombre)
        {
            ItemBL objItemBL;
            try
            {
                objItemBL = new ItemBL();
                return objItemBL.Item_ListarProducto_Stock(idEmpresa, codigoUsuario, nombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsItem_ListarStock_ComercialResult> Item_ListarStock_Comercial(int idEmpresa, int codigoUsuario, string nombre, decimal? ID_AlmacenAnexo, int G2, int G5)
        {
            ItemBL objItemBL;
            try
            {
                objItemBL = new ItemBL();
                return objItemBL.Item_ListarStock_Comercial(idEmpresa, codigoUsuario, nombre, ID_AlmacenAnexo, G2, G5);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<sp_GestionStock_ListarResult> Item_Listar_GestionStock(int idEmpresa, int codigoUsuario, int ID, int id_agendanexo, int id_item, float cantidad, string Observacion, int Operacion)
        {
            ItemBL objItemBL;
            try
            {
                objItemBL = new ItemBL();
                return objItemBL.Item_Listar_GestionStock(idEmpresa, codigoUsuario, ID, id_agendanexo, id_item, cantidad, Observacion, Operacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Item_Mantenimiento_GestionStock(int idEmpresa, int codigoUsuario, int ID, int id_agendanexo, int id_item, float cantidad, string Observacion, int Operacion)
        {
            ItemBL objItemBL;
            try
            {
                objItemBL = new ItemBL();
                objItemBL.Item_Mantenimiento_GestionStock(idEmpresa, codigoUsuario, ID, id_agendanexo, id_item, cantidad, Observacion, Operacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<spAlmacenesFlete_ListarResult> AlmacenesFlete_Listar(int idEmpresa, int codigoUsuario, int id_agendanexo)
        {
            ItemBL objItemBL;
            try
            {
                objItemBL = new ItemBL();
                return objItemBL.AlmacenesFlete_Listar(idEmpresa, codigoUsuario, id_agendanexo );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsItem_ListarProductoPresupuestoResult> Item_ListarProductoPresupuesto(int idEmpresa, int codigoUsuario, string nombre)
        {
            ItemBL objItemBL;
            try
            {
                objItemBL = new ItemBL();
                return objItemBL.Item_ListarProductoPresupuesto(idEmpresa, codigoUsuario, nombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
