using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Web.Services;

using GS.SISGEGS.Web.ImpuestoWCF;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.ItemWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.CentroCostoWCF;



namespace GS.SISGEGS.Web.Compras.Pedido
{
    public partial class frmOrdenCompraItem : System.Web.UI.Page
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

        private void Impuesto_GuardarParcial(string idItem, bool nuevo)
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
                    if (Session["lstImpuestosParcial"] == null)
                        Session["lstImpuestosParcial"] = lstImpuestos;
                    else
                        ((List<gsImpuesto_ListarPorItemResult>)Session["lstImpuestosParcial"]).AddRange(lstImpuestos);
                    lstImpuestos = ((List<gsImpuesto_ListarPorItemResult>)Session["lstImpuestosParcial"]);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void Item_Buscar(string idItem, string idProveedor, decimal idAlmacen, bool nuevo, string stridMoneda)
        {
            decimal? stockDisponible = null;
            double? TC_Cambio = null;
            decimal? precio = null;
            decimal? TC = null;
            try
            {
                ItemWCFClient objItemWCF = new ItemWCFClient();
                gsItem_BuscarResult objProducto = new gsItem_BuscarResult();
                gsItem_BuscarResult objProductoActual = new gsItem_BuscarResult();

                if (nuevo)
                {
                    objProducto = objItemWCF.Item_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                        ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idItem, idProveedor, DateTime.Now.Date, 0, 1001, null, null,
                        idAlmacen, ref stockDisponible, ref TC_Cambio);
                    PrecioInicial.Text = objProducto.Precio.ToString();
                }
                else
                {
                    objProducto = ((List<gsItem_BuscarResult>)Session["lstProductos"]).Find(x => x.Item_ID.ToString() == idItem);

                    objProductoActual = objItemWCF.Item_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                        ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, objProducto.Codigo, idProveedor, DateTime.Now.Date,
                        0, 1001, null, null, idAlmacen, ref stockDisponible, ref TC_Cambio);
                    PrecioInicial.Text = objProductoActual.Precio.ToString();
                }

                idMoneda.Text = objProducto.ID_Moneda.ToString();
                txtPrecio.Text = objProducto.Precio.ToString();
                lblMonedaPrecio.Text = objProducto.Signo;
                lblMonedaImporte.Text = objProducto.Signo;
                TC = Convert.ToDecimal( objProducto.TC.ToString());

                if (stridMoneda != idMoneda.Text)
                {
                    if(stridMoneda == "0")
                    {
                        idMoneda.Text = "0";
                        precio = Convert.ToDecimal(objProducto.Precio.ToString());
                        precio = (precio / TC);
                        PrecioInicial.Text = precio.ToString();
                        txtPrecio.Text = precio.ToString();
                        lblMonedaPrecio.Text = "US$";
                        lblMonedaImporte.Text = "US$";
                    }
                    else
                    {
                        idMoneda.Text = "1";
                        precio = Convert.ToDecimal(objProducto.Precio.ToString());
                        precio = (precio * TC);
                        PrecioInicial.Text = precio.ToString();
                        txtPrecio.Text = precio.ToString();
                        lblMonedaImporte.Text = "S/.";
                    }

                }

                txtCodigo.Text = objProducto.Codigo;
                txtNombre.Text = objProducto.Item;
                txtKardex.Text = objProducto.Item_ID.ToString();
                txtFactor.Text = objProducto.FactorUnidadInv.ToString();
                lblUnidadFactor.Text = objProducto.ID_UnidadInv;
                txtStock.Text = objProducto.Stock.ToString(); //stockDisponible.ToString();
                lblUnidadStock.Text = objProducto.UnidadPresentacion;
                lblUnidadCantidad.Text = objProducto.UnidadPresentacion;
        
                txtImporte.Text = objProducto.Importe.ToString();
                txtCantidad.Text = objProducto.Cantidad.ToString();
                txtDescuento.Text = objProducto.DctoMax.ToString();
                DescuentoInicial.Text = objProducto.DctoMax.ToString();
                txtObservacion.Text = objProducto.Observacion;

                txtTC.Text = objProducto.TC.ToString();

                if (objProducto.CostoUnitario == 0 || objProducto.CostoUnitario == null || objProducto.PrecioInicial == 0)
                {
                    txtRentabilidad.Value = 0;
                    hfCostoUnitario.Value = "0";
                }
                else {
                    txtRentabilidad.Value = (double)((objProducto.PrecioInicial - (decimal)objProducto.CostoUnitario) * 100 / objProducto.PrecioInicial);
                    hfCostoUnitario.Value = objProducto.CostoUnitario.ToString();
                }

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
            try
            {
                if (!Page.IsPostBack)
                {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    Title = "Registrar producto";
                    Item_Buscar(Request.QueryString["idItem"], Request.QueryString["idProveedor"], decimal.Parse(Request.QueryString["idAlmacen"]),
                        Convert.ToBoolean(Int32.Parse(Request.QueryString["nuevo"])), Request.QueryString["idMoneda"]);
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

            List<gsItem_BuscarResult> lstProductos = new List<gsItem_BuscarResult>();
            gsItem_BuscarResult objItem;
            try
            {
                if (txtCantidad.Value <= 0)
                    throw new ArgumentException("La cantidad ingresada debe ser mayor a 0.");

                if (txtPrecio.Value <= 0)
                    throw new ArgumentException("El precio ingresado debe ser mayor a 0.");

                if (!Convert.ToBoolean(Int32.Parse(Request.QueryString["nuevo"])))
                {
                    objItem = new gsItem_BuscarResult();
                    objItem = ((List<gsItem_BuscarResult>)Session["lstProductos"]).Find(x => x.Item_ID.ToString() == Request.QueryString["idItem"]);
                    ((List<gsItem_BuscarResult>)Session["lstProductos"]).Remove(objItem);
                }

                objItem = new gsItem_BuscarResult();
                objItem = JsonHelper.JsonDeserialize<gsItem_BuscarResult>((string)ViewState["objItem"]);
                objItem.Precio = Math.Round(decimal.Parse(txtPrecio.Text), 2);
                objItem.Cantidad = Int32.Parse(txtCantidad.Text);
                objItem.Importe = Math.Round(objItem.Precio * Int32.Parse(txtCantidad.Text), 2);
                objItem.Observacion = txtObservacion.Text;
                objItem.Descuento = Math.Round(decimal.Parse(txtDescuento.Text), 2);
                objItem.FactorUnidadInv = Math.Round(decimal.Parse(txtFactor.Text), 2);
                objItem.Stock = Math.Round(decimal.Parse(txtStock.Text), 0); ;
                objItem.Estado = 1;
                objItem.CostoUnitario = decimal.Parse(hfCostoUnitario.Value);

                objItem.idCCosto = acbCCosto.Entries[0].Text.Split('-')[0];
                objItem.CCosto = acbCCosto.Entries[0].Text.Split('-')[1];

                if (Session["lstProductos"] == null)
                    Session["lstProductos"] = lstProductos;
                else
                    lstProductos = ((List<gsItem_BuscarResult>)Session["lstProductos"]);

                lstProductos.Add(objItem);
                Session["lstProductos"] = lstProductos;

                Impuesto_Guardar(objItem.Codigo, Convert.ToBoolean(Int32.Parse(Request.QueryString["nuevo"])));

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + objItem.Item_ID + ");", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            int correlativo;
            correlativo = Convert.ToInt32(txtCorrelativo.Text);
            correlativo = correlativo + 1;
            txtCorrelativo.Text = correlativo.ToString(); 

            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            List<gsItem_BuscarResult> lstProductosParcial = new List<gsItem_BuscarResult>();
            gsItem_BuscarResult objItem;
            try
            {
                if (txtCantidadParcial.Value <= 0)
                    throw new ArgumentException("La cantidad ingresada debe ser mayor a 0.");

                if (txtPrecio.Value <= 0)
                    throw new ArgumentException("El precio ingresado debe ser mayor a 0.");

                if (!Convert.ToBoolean(Int32.Parse(Request.QueryString["nuevo"])))
                {
                    objItem = new gsItem_BuscarResult();
                    objItem = ((List<gsItem_BuscarResult>)Session["lstProductosParcial"]).Find(x => x.Item_ID.ToString() == Request.QueryString["idItem"]);
                    ((List<gsItem_BuscarResult>)Session["lstProductosParcial"]).Remove(objItem);
                }

                objItem = new gsItem_BuscarResult();
                objItem = JsonHelper.JsonDeserialize<gsItem_BuscarResult>((string)ViewState["objItem"]);
                objItem.Precio = Math.Round(decimal.Parse(txtPrecio.Text), 2);
                objItem.Cantidad = Int32.Parse(txtCantidadParcial.Text);
                objItem.Importe = Math.Round(objItem.Precio * Int32.Parse(txtCantidadParcial.Text), 2);
                objItem.Observacion = txtObservacion.Text;

                objItem.Descuento = 0;
                objItem.FactorUnidadInv = Math.Round(decimal.Parse(txtFactor.Text), 2);
                objItem.Stock = Math.Round(decimal.Parse(txtStock.Text), 0); ;
                objItem.Estado = 1;
                objItem.CostoUnitario = decimal.Parse(hfCostoUnitario.Value);

                objItem.idCCosto = acbCCosto.Entries[0].Text.Split('-')[0];
                objItem.CCosto = acbCCosto.Entries[0].Text.Split('-')[1];
                objItem.Fecha = dpFechaParcial.SelectedDate.Value;
                objItem.correlativo = correlativo;

                

                if (Session["lstProductosParcial"] == null)
                    Session["lstProductosParcial"] = lstProductosParcial;
                else
                    lstProductosParcial = ((List<gsItem_BuscarResult>)Session["lstProductosParcial"]);

                lstProductosParcial.Add(objItem);
                Session["lstProductosParcial"] = lstProductosParcial;

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

        [WebMethod]
        public static AutoCompleteBoxData Agenda_BuscarCCosto(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                CentroCostoWCFClient objCCortosWCFClient = new CentroCostoWCFClient();
                VBG00786Result[] lst = objCCortosWCFClient.CentroCosto_Listar(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (VBG00786Result ccosto in lst)
                {
                    AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                    childNode.Text = ccosto.ID_CentroCostos + "-" + ccosto.CentroCostos;
                    childNode.Value = ccosto.ID_CentroCostos;
                    result.Add(childNode);
                }
                res.Items = result.ToArray();
            }
            return res;
        }
    }
}