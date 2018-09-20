using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.ItemWCF;
using GS.SISGEGS.Web.ImpuestoWCF;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.Web.Commercial.Pedido
{
    public partial class frmPedidoItem : System.Web.UI.Page
    {
        #region Métodos privados
        private void Impuesto_Guardar(string idItem, bool nuevo)
        {
            ImpuestoWCFClient objImpuestoWCF;
            List<gsImpuesto_ListarPorItemResult> lstImpuestos;
            try
            {
                if (nuevo)
                {
                    objImpuestoWCF = new ImpuestoWCFClient();
                    lstImpuestos = objImpuestoWCF.Impuesto_ListarPorItem(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, 
                        ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idItem, DateTime.Now.Date).ToList();
                    if (Session["lstImpuestos"] == null)
                        Session["lstImpuestos"] = lstImpuestos;
                    else
                        ((List<gsImpuesto_ListarPorItemResult>)Session["lstImpuestos"]).AddRange(lstImpuestos);
                    lstImpuestos = ((List<gsImpuesto_ListarPorItemResult>)Session["lstImpuestos"]);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Item_Buscar(string idItem, string idCliente, decimal idAlmacen, bool nuevo)
        {
            decimal? stockDisponible = null;
            try
            {
                ItemWCFClient objItemWCF = new ItemWCFClient();
                gsItem_BuscarResult objProducto = new gsItem_BuscarResult();
                gsItem_BuscarResult objProductoActual = new gsItem_BuscarResult();

                if (nuevo)
                {
                    objProducto = objItemWCF.Item_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, 
                        ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idItem, idCliente, DateTime.Now.Date, 0, null, null, null, 
                        idAlmacen, ref stockDisponible);
                    PrecioInicial.Text = objProducto.Precio.ToString();
                }
                else
                {
                    objProducto = ((List<gsItem_BuscarResult>)Session["lstProductos"]).Find(x => x.Item_ID.ToString() == idItem);
                    //((List<gsItem_BuscarResult>)Session["lstProductos"]).Remove(objProducto);

                    objProductoActual = objItemWCF.Item_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, 
                        ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, objProducto.Codigo, idCliente, DateTime.Now.Date, 0, null, null, null,
                        idAlmacen, ref stockDisponible);
                    PrecioInicial.Text = objProductoActual.Precio.ToString();
                }

                txtCodigo.Text = objProducto.Codigo;
                txtNombre.Text = objProducto.Item;
                txtKardex.Text = objProducto.Item_ID.ToString();
                txtPrecio.Text = objProducto.Precio.ToString();
                idMoneda.Text = objProducto.ID_Moneda.ToString();
                lblMonedaPrecio.Text = objProducto.Signo;
                txtFactor.Text = objProducto.FactorUnidadInv.ToString();
                lblUnidadFactor.Text = objProducto.ID_UnidadInv;
                txtStock.Text = stockDisponible.ToString();
                //txtStock.Text = objProducto.Stock.ToString();
                lblUnidadStock.Text = objProducto.UnidadPresentacion;
                lblUnidadCantidad.Text = objProducto.UnidadPresentacion;
                lblMonedaImporte.Text = objProducto.Signo;
                txtImporte.Text = objProducto.Importe.ToString();
                txtCantidad.Text = objProducto.Cantidad.ToString();
                txtDescuento.Text = objProducto.DctoMax.ToString();
                DescuentoInicial.Text = objProducto.DctoMax.ToString();
                txtObservacion.Text = objProducto.Observacion;

                ViewState["objItem"] = JsonHelper.JsonSerializer(objProducto);
                if (stockDisponible == 0)
                    throw new ArgumentException("El producto no se encuentra disponible en el almacen");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Métodos protegidos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            if (!ConnectionHelpers.CheckForInternetConnection())
                throw new ArgumentException("ERROR: Revisar su conexión a internet.");
            try
            {
                if (!Page.IsPostBack)
                {
                    Title = "Registrar producto";
                    Item_Buscar(Request.QueryString["idItem"], Request.QueryString["idCliente"], decimal.Parse(Request.QueryString["idAlmacen"]),
                        Convert.ToBoolean(Int32.Parse(Request.QueryString["nuevo"])));
                }
                lblMensaje.Text = "Datos del producto se cargo correctamente.";
                lblMensaje.CssClass = "mensajeExito";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            if (!ConnectionHelpers.CheckForInternetConnection())
                throw new ArgumentException("ERROR: Revisar su conexión a internet.");

            gsItem_BuscarResult objItem;
            try
            {
                if (txtCantidad.Value <= 0)
                    throw new ArgumentException("ERROR: La cantidad ingresada debe ser mayor a 0.");

                if (!Convert.ToBoolean(Int32.Parse(Request.QueryString["nuevo"]))){
                    objItem = new gsItem_BuscarResult();
                    objItem = ((List<gsItem_BuscarResult>)Session["lstProductos"]).Find(x => x.Item_ID.ToString() == Request.QueryString["idItem"]);
                    ((List<gsItem_BuscarResult>)Session["lstProductos"]).Remove(objItem);
                }

                objItem = new gsItem_BuscarResult();
                objItem = JsonHelper.JsonDeserialize<gsItem_BuscarResult>((string)ViewState["objItem"]);
                objItem.Precio = Math.Round(decimal.Parse(txtPrecio.Text), 4);
                objItem.Cantidad = Int32.Parse(txtCantidad.Text);
                objItem.Importe = Math.Round(objItem.Precio * Int32.Parse(txtCantidad.Text), 4);
                objItem.Observacion = txtObservacion.Text;
                objItem.Descuento = Math.Round(decimal.Parse(txtDescuento.Text), 2);
                objItem.FactorUnidadInv = Math.Round(decimal.Parse(txtFactor.Text), 2);
                objItem.Stock = Math.Round(decimal.Parse(txtStock.Text), 0); ;
                objItem.Estado = 1;
                if (Session["lstProductos"] == null)
                {
                    List<gsItem_BuscarResult> lstProductos = new List<gsItem_BuscarResult>();
                    Session["lstProductos"] = lstProductos;
                }

                ((List<gsItem_BuscarResult>)Session["lstProductos"]).Add(objItem);

                Impuesto_Guardar(objItem.Codigo, Convert.ToBoolean(Int32.Parse(Request.QueryString["nuevo"])));

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + objItem.Item_ID + ");", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        #endregion
    }
}