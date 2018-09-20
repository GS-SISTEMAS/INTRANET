using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.ImpuestoWCF;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.ItemWCF;
using GS.SISGEGS.Web.Helpers;


namespace GS.SISGEGS.Web.Comercial.Pedido
{
    public partial class frmOrdenVentaItem : System.Web.UI.Page
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

        private void Item_Buscar(string idItem, string idCliente, decimal idAlmacen, bool nuevo, string stridMoneda)
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
                        ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idItem, idCliente, DateTime.Now.Date, 0, null, null, null,
                        idAlmacen, ref stockDisponible, ref TC_Cambio);
                    precio = Math.Round(Convert.ToDecimal(objProducto.Precio.ToString()), 2);
                    PrecioInicial.Text = precio.ToString();
                    TC = Convert.ToDecimal(objProducto.TC.ToString());
                }
                else
                {
                    objProducto = ((List<gsItem_BuscarResult>)Session["lstProductos"]).Find(x => x.Item_ID.ToString() == idItem && x.Estado == 1 );

                    objProductoActual = objItemWCF.Item_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                        ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, objProducto.Codigo, idCliente, DateTime.Now.Date,
                        0, null, null, null, idAlmacen, ref stockDisponible, ref TC_Cambio);

                    precio = Math.Round(Convert.ToDecimal(objProductoActual.Precio.ToString()), 2);
                    PrecioInicial.Text = precio.ToString();
                    TC = Convert.ToDecimal(objProductoActual.TC.ToString());

                }

                txtCodigo.Text = objProducto.Codigo;
                txtNombre.Text = objProducto.Item;
                txtKardex.Text = objProducto.Item_ID.ToString();
                

                idMoneda.Text = objProducto.ID_Moneda.ToString();
                if (stridMoneda != idMoneda.Text)
                {
                    if (stridMoneda == "0")
                    {
                        idMoneda.Text = "0";
                        precio = Convert.ToDecimal(objProducto.Precio.ToString());
                        precio = (precio / TC);
                        precio = Math.Round(Convert.ToDecimal(precio.ToString()), 4);

                        PrecioInicial.Text = precio.ToString();
                        txtPrecio.Text = precio.ToString();
                        objProducto.Precio = decimal.Parse(precio.ToString()); 

                        lblMonedaPrecio.Text = "US$";
                        lblMonedaImporte.Text = "US$";
                        objProducto.ID_Moneda = 0;
                        objProducto.Signo = "US$";
                    }
                    else
                    {
                        idMoneda.Text = "1";
                        precio = Convert.ToDecimal(objProducto.Precio.ToString());
                        precio = (precio * TC);
                        precio = Math.Round(Convert.ToDecimal(precio.ToString()), 4);

                        PrecioInicial.Text = precio.ToString();
                        txtPrecio.Text = precio.ToString();
                        objProducto.Precio = decimal.Parse(precio.ToString());

                        lblMonedaPrecio.Text =  "S/.";
                        lblMonedaImporte.Text = "S/.";
                        objProducto.ID_Moneda = 1;
                        objProducto.Signo = "S/.";
                    }
                }
                else
                {
                    precio = Math.Round(Convert.ToDecimal(objProducto.Precio.ToString()), 4);
                    txtPrecio.Text = precio.ToString();
                    lblMonedaPrecio.Text = objProducto.Signo;
                    lblMonedaImporte.Text = objProducto.Signo;

                    if (!nuevo)
                    {
                        //precio = Math.Round(Convert.ToDecimal(objProductoActual.Precio.ToString()), 2);

                        PrecioInicial.Text = objProductoActual.Precio.ToString(); 
                        idMoneda.Text = objProductoActual.ID_Moneda.ToString();
                        TC = Convert.ToDecimal(objProductoActual.TC.ToString());

                        if (stridMoneda != idMoneda.Text)
                        {
                            if (stridMoneda == "0")
                            {
                                idMoneda.Text = "0";
                                precio = Convert.ToDecimal(objProductoActual.Precio.ToString());
                                precio = (precio / TC);
                                precio = Math.Round(Convert.ToDecimal(precio.ToString()), 4);
                                PrecioInicial.Text = precio.ToString();
                            }
                            else
                            {
                                idMoneda.Text = "1";
                                precio = Convert.ToDecimal(objProductoActual.Precio.ToString());
                                precio = (precio * TC);
                                precio = Math.Round(Convert.ToDecimal(precio.ToString()), 4);
                                PrecioInicial.Text = precio.ToString();
                            }
                        }
                    }
                   
                }


                txtFactor.Text = objProducto.FactorUnidadInv.ToString();
                lblUnidadFactor.Text = objProducto.ID_UnidadInv;
                txtStock.Text = stockDisponible.ToString();
                lblUnidadStock.Text = objProducto.UnidadPresentacion;
                lblUnidadCantidad.Text = objProducto.UnidadPresentacion;
                
                txtImporte.Text = objProducto.Importe.ToString();
                txtCantidad.Text = objProducto.Cantidad.ToString();

                txtDescuentoMax.Text = objProducto.DctoMax.ToString();
                txtDescuentoFinal.Text = objProducto.Descuento.ToString();

                DescuentoFinal.Text = objProducto.Descuento.ToString();
                DescuentoInicial.Text = objProducto.Descuento.ToString();
                DescuentoMaximo.Text = objProducto.DctoMax.ToString();

                txtObservacion.Text = objProducto.Observacion;

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

                string Impuesto = Session["Impuestos"].ToString();
                string ItemImpuesto = ""; 

                if (objProducto.TieneImpuesto > 0)
                {
                    ItemImpuesto = "IGV";
                }
                else
                {
                    ItemImpuesto = "SINIGV";
                }

                if (Impuesto != ItemImpuesto)
                {
                    if(Impuesto == "")
                    {
                        Session["Impuestos"] = ItemImpuesto;
                        if (stockDisponible <= 0)
                        {
                            lblMensaje.CssClass = "mensajeError";
                            throw new ArgumentException("El producto no se encuentra disponible en el almacen");
                        }
                    }
                    else
                    {
                        btnGuardar.Enabled = false;
                        if (ItemImpuesto == "IGV")
                        {
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "Afecto();", true);
                            lblMensaje.CssClass = "mensajeError";
                            lblMensaje.Text = "El producto es Afecto al IGV, por favor, registrarlo en otro pedido.";
                            throw new ArgumentException("El producto es Afecto al IGV, por favor, registrarlo en otro pedido.");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "Inafecto();", true);
                            lblMensaje.CssClass = "mensajeError";
                            lblMensaje.Text = "El producto es Inafecto al IGV, por favor, registrarlo en otro pedido.";
                            throw new ArgumentException("El producto es Inafecto al IGV, por favor, registrarlo en otro pedido.");
                        }
                    }
                }
                else
                {
                    if (stockDisponible <= 0)
                    {
                        lblMensaje.CssClass = "mensajeError";
                        throw new ArgumentException("El producto no se encuentra disponible en el almacen");
                    }  
                }
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

                    txtDescuentoFinal.Text = "0";
                    DescuentoInicial.Text= "0";
                    DescuentoFinal.Text = "0";

                    Title = "Registrar producto";
                    Item_Buscar(Request.QueryString["idItem"], Request.QueryString["idCliente"], decimal.Parse(Request.QueryString["idAlmacen"]),
                        Convert.ToBoolean(Int32.Parse(Request.QueryString["nuevo"])), Request.QueryString["id_moneda"]);
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
                    objItem = ((List<gsItem_BuscarResult>)Session["lstProductos"]).Find(x => x.Item_ID.ToString() == Request.QueryString["idItem"] && x.Estado == 1);

                    ((List<gsItem_BuscarResult>)Session["lstProductos"]).Remove(objItem);
                }

                objItem = new gsItem_BuscarResult();
                objItem = JsonHelper.JsonDeserialize<gsItem_BuscarResult>((string)ViewState["objItem"]);

                objItem.Precio = Math.Round(decimal.Parse(txtPrecio.Text.ToString()),4);
                objItem.Cantidad =  Int32.Parse(txtCantidad.Text);

                decimal desc = decimal.Parse(txtDescuentoFinal.Text);

                objItem.Importe = Math.Round( ( 1 -  ((desc)/100) ) *  objItem.Precio * Int32.Parse(txtCantidad.Text), 4);

                objItem.Observacion = txtObservacion.Text;
                objItem.Descuento = Math.Round(decimal.Parse(txtDescuentoFinal.Text), 4);
                objItem.FactorUnidadInv = Math.Round(decimal.Parse(txtFactor.Text), 4);
                objItem.Stock = Math.Round(decimal.Parse(txtStock.Text), 4); ;
                objItem.Estado = 1;
                objItem.CostoUnitario = decimal.Parse(hfCostoUnitario.Value);

            
                if (Session["lstProductos"] == null)
                {
                    lstProductos.Add(objItem);
                    Session["lstProductos"] = lstProductos;
                }
                else
                {
                    lstProductos = ((List<gsItem_BuscarResult>)Session["lstProductos"]);
                    lstProductos.Add(objItem);
                    Session["lstProductos"] = lstProductos;
                }

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