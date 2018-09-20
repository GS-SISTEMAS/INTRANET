using GS.SISGEGS.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GS.SISGEGS.BE;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IItemWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IItemWCF
    {
        [OperationContract]
        List<gsItem_ListarProductoResult> Item_ListarProducto(int idEmpresa, int codigoUsuario, string nombre);

        [OperationContract]
        gsItem_BuscarResult Item_Buscar(int idEmpresa, int codigoUsuario, string idProducto, string idCliente, 
            DateTime? fecha, decimal cantidad, int? idTipoEnlaceContable, decimal? idDireccionOrigen, decimal? idDireccionDestino, decimal idAlmacen,
            ref decimal? StockDisponible, ref double? TC_Cambio);

        [OperationContract]
        List<gsItem_ListarTipoGastoCCResult> Item_ListarTipoGastoCC(int idEmpresa, int codigoUsuario);

        [OperationContract]
        List<gsItem_ListarStockResult> Item_ListarStock(int idEmpresa, int codigoUsuario, string nombre, decimal? ID_AlmacenAnexo);

        [OperationContract]
        List<gsItem_ListarPrecioClienteResult> Item_ListarPrecioCliente(int idEmpresa, int codigoUsuario, string ID_Agenda, string descripcion);

        [OperationContract]
        void Item_BuscarPrecioCliente(int idEmpresa, int codigoUsuario, decimal idPrecioCliente, ref decimal? precioEspecial, ref DateTime? 
            vigInicio, ref DateTime? vigFinal, ref VBG01134Result objCliente, ref VBG01124Result objProducto, ref string ID_Item);

        [OperationContract]
        void Item_RegistrarPrecioCliente(int idEmpresa, int codigoUsuario, ref decimal? idClienteProd, string ID_Item, string ID_Cliente, DateTime? vigInicio,
            DateTime? vigFinal, decimal? precioEspecial);

        [OperationContract]
        VBG01124Result Item_BuscarProducto(int idEmpresa, int codigoUsuario, string ID_Item);

        [OperationContract]
        void Item_EliminarProductoCliente(int idEmpresa, int codigoUsuario, decimal ID_ItemCliente);

        [OperationContract]
        List<gsItem_ListarResult> Item_Listar(int idEmpresa, int codigoUsuario, string Item);

        [OperationContract]
        List<VBG00321Result> Item_Listar_ProductosCompras(int idEmpresa, int codigoUsuario, string Nombre, int CategoriaItem);

        [OperationContract]
        List<VBG04054Result> Item_CategoriasGxOpciones(int idEmpresa, int codigoUsuario, int intTipo);

        [OperationContract]
        List<gsItem_ListarProducto_StockResult> Item_ListarProducto_Stock(int idEmpresa, int codigoUsuario, string nombre);

        [OperationContract]
        List<gsItem_ListarStock_ComercialResult> Item_ListarStock_Comercial(int idEmpresa, int codigoUsuario, string nombre, decimal? ID_AlmacenAnexo, int G2, int G5);

        [OperationContract]
        List<sp_GestionStock_ListarResult> Item_Listar_GestionStock(int idEmpresa, int codigoUsuario, int ID, int id_agendanexo, int id_item, float cantidad, string Observacion, int Operacion);

        [OperationContract]
        void Item_Mantenimiento_GestionStock(int idEmpresa, int codigoUsuario, int ID, int id_agendanexo, int id_item, float cantidad, string Observacion, int Operacion);

        [OperationContract]
        List<spAlmacenesFlete_ListarResult> AlmacenesFlete_Listar(int idEmpresa, int codigoUsuario, int id_agendanexo);

        [OperationContract]
        void Item_RegistrarPrecioClienteLista(int idEmpresa, int codigoUsuario, List<MantenimientoProductos> lstproductos);

        [OperationContract]
        List<gsItem_ListarProductoPresupuestoResult> Item_ListarProductoPresupuesto(int idEmpresa, int codigoUsuario, string nombre);
    }
}
