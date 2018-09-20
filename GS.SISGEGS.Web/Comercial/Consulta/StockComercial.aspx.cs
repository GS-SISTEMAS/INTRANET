using GS.SISGEGS.DM;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.ItemWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using GS.SISGEGS.BE;
using GS.SISGEGS.Web.DireccionWCF;
using GS.SISGEGS.Web.SedeWCF;
using GS.SISGEGS.Web.EnvioWCF;
using GS.SISGEGS.Web.CreditoWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.DocumentoWCF;
using GS.SISGEGS.Web.OrdenVentaWCF;
using GS.SISGEGS.Web.DespachoWCF;
using GS.SISGEGS.Web.MonedaWCF;
using GS.SISGEGS.Web.FormaPagoWCF;
 
using GS.SISGEGS.Web.VarianteWCF;
using GS.SISGEGS.Web.PedidoWCF;

namespace GS.SISGEGS.Web.Comercial.Consulta
{
    public partial class StockComercial : System.Web.UI.Page
    {
        private void Almacen_Cargar()
        {
            try
            {
                AgendaWCFClient objAgendaWCF = new AgendaWCFClient();
                VBG00746Result objAlmacen = new VBG00746Result();
                List<VBG00746Result> lstAlmacen;

                lstAlmacen = objAgendaWCF.AgendaAnexo_ListarAlmacen(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa, 
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario).ToList();

                lstAlmacen.Insert(0, objAlmacen);
                objAlmacen.AlmacenAnexo = "Todos";
                objAlmacen.ID_Almacen = "";

                cboAlmacen.DataSource = lstAlmacen;
                cboAlmacen.DataTextField = "AlmacenAnexo";
                cboAlmacen.DataValueField = "ID_AlmacenAnexo";
                cboAlmacen.DataBind();

                cboAlmacen.SelectedValue = "0"; 

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void TipoProducto_Cargar()
        {
            try
            {
                ItemWCFClient objItemsWCF = new ItemWCFClient();
                VBG04054Result objItem = new VBG04054Result();
                List<VBG04054Result> lstItems;

                lstItems = objItemsWCF.Item_CategoriasGxOpciones(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, 2).ToList();

                lstItems = lstItems.FindAll(x=> x.Nombre.Contains("Prima") || x.Nombre.Contains("Producto")).ToList(); 

                //lstItems.Insert(0, objItem);
                //objItem.ID_ItemEstructuraOpcion = 0;
                //objItem.Nombre = "Todos";

                cboTipoMaterial.DataSource = lstItems;
                cboTipoMaterial.DataTextField = "Nombre";
                cboTipoMaterial.DataValueField = "ID_ItemEstructuraOpcion";
                cboTipoMaterial.DataBind();

                cboTipoMaterial.SelectedIndex =0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void MarcaProducto_Cargar()
        {
            try
            {
                ItemWCFClient objItemsWCF = new ItemWCFClient();
                VBG04054Result objItem = new VBG04054Result();
                List<VBG04054Result> lstItems;

                lstItems = objItemsWCF.Item_CategoriasGxOpciones(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, 5).ToList();

                lstItems.Insert(0, objItem);
                objItem.ID_ItemEstructuraOpcion = 0;
                objItem.Nombre = "Todos";

                cboMarca.DataSource = lstItems;
                cboMarca.DataTextField = "Nombre";
                cboMarca.DataValueField = "ID_ItemEstructuraOpcion";
                cboMarca.DataBind();

                cboMarca.SelectedValue = "0"; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Item_ListarStock(string nombre, decimal? ID_AnexoAlmacen, int Codigo_Tipo, int Codigo_Marca) {
            ItemWCFClient objItemWCF = new ItemWCFClient();
            try {

                if (ID_AnexoAlmacen == 0)
                    ID_AnexoAlmacen = null;

                if (Codigo_Tipo == 0)
                    Codigo_Tipo = 0;

                if (Codigo_Marca == 0)
                    Codigo_Marca = 0;

                if (string.IsNullOrEmpty(nombre))
                    nombre = null;

                List<gsItem_ListarStock_ComercialResult> Lista = new List<gsItem_ListarStock_ComercialResult>();
                Lista = objItemWCF.Item_ListarStock_Comercial(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, 
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, nombre, ID_AnexoAlmacen, Codigo_Tipo, Codigo_Marca).ToList();
                grdStock.DataSource = Lista; 
                grdStock.DataBind();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (!Page.IsPostBack) {

                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    Session["idAlmacen"] = null;
                    Session["idAlmacen"] = cboAlmacen.SelectedValue;
                    cboTipoMaterial.Filter = (RadComboBoxFilter)Convert.ToInt32(1);
                    cboAlmacen.Filter = (RadComboBoxFilter)Convert.ToInt32(1);
                    cboMarca.Filter = (RadComboBoxFilter)Convert.ToInt32(1);

                    Almacen_Cargar();
                    TipoProducto_Cargar();
                    MarcaProducto_Cargar();

                    int verStock = (int)((Usuario_LoginResult)Session["Usuario"]).verStock;
                    Ocultar_Columnas(verStock); 

                }
            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string Codigo_Item = "";
            string Nombre_Item = "";
            string Codigo_Almacen = "";

            string Codigo_Tipo = "";
            string Codigo_Marca = "";

            lblMensaje.Text = ""; 

            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                Session["idAlmacen"] = cboAlmacen.SelectedValue;

               
                Codigo_Almacen = cboAlmacen.SelectedValue;
                Codigo_Tipo = cboTipoMaterial.SelectedValue;
                Codigo_Marca = cboMarca.SelectedValue;
                Codigo_Item = acbProducto.Text.Split('-')[0].Trim();

                if (Codigo_Almacen.Length <= 0)
                    Codigo_Almacen = "0";

                if (Codigo_Tipo.Length <= 0)
                {
                    Codigo_Tipo = "0";
                }
                if (Codigo_Marca.Length <= 0)
                {
                    Codigo_Marca = "0";
                }
                if (Codigo_Item.Length <= 0)
                {
                    Codigo_Item = "";
                }

                int bloquear = 0;
                bloquear = ValidarConsulta(Codigo_Almacen, Codigo_Tipo, Codigo_Marca, Codigo_Item); 

                if (bloquear == 0)
                {
                    Item_ListarStock(Codigo_Item, decimal.Parse(Codigo_Almacen), int.Parse(Codigo_Tipo), int.Parse(Codigo_Marca));
                }
                else
                {
                    string strMns = ""; 
                    if (bloquear ==1)
                    {
                        strMns = "Por favor, seleccionar Almacén, Marca o Producto.";

                        string script = string.Format("AlertaSeleccion('{0}');", strMns);

                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", script, true);
                        lblMensaje.Text = strMns;
                        lblMensaje.CssClass = "mensajeError";
                    }
                    if (bloquear ==2)
                    {
                        strMns = "Por favor, seleccionar Marca o Producto.";
                        string script = string.Format("AlertaSeleccion('{0}');", strMns);
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", script, true);
                        lblMensaje.Text = strMns;
                        lblMensaje.CssClass = "mensajeError";
                    }
                }


            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void Ocultar_Columnas(int id_verStock)
        {

            lblMensaje.Text = "";

            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
              
                if(id_verStock == 1)
                {
                    grdStock.MasterTableView.GetColumn("TransitoOC").Display = true;
                    grdStock.MasterTableView.GetColumn("TransitoGestion").Display = true;
                    grdStock.MasterTableView.GetColumn("StockTotal_KgLt").Display = true;
                    grdStock.MasterTableView.GetColumn("MesesStock_Disponible").Display = true;
                   
                }
                else
                {
                    grdStock.MasterTableView.GetColumn("TransitoOC").Display = false;
                    grdStock.MasterTableView.GetColumn("TransitoGestion").Display = false;
                    grdStock.MasterTableView.GetColumn("StockTotal_KgLt").Display = false;
                    grdStock.MasterTableView.GetColumn("MesesStock_Disponible").Display = false;
                  
                }
 
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }


        [WebMethod]
        public static AutoCompleteBoxData Item_BuscarProducto(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (!string.IsNullOrEmpty(searchString) && searchString.Length >= 4)
            {

                ItemWCFClient objItemWCF = new ItemWCFClient();
                List<gsItem_ListarProducto_StockResult> lst = objItemWCF.Item_ListarProducto_Stock(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString).ToList();

                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsItem_ListarProducto_StockResult producto in lst)
                {
                    AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                    childNode.Text = producto.ID_Item + "-" + producto.Nombre;
                    childNode.Value = producto.ID_Item;

                    result.Add(childNode);
                }
                res.Items = result.ToArray();
            }
            return res;
        }


        protected void cboAlmacen_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Session["idAlmacen"] = cboAlmacen.SelectedValue;
        }

        protected void cboTipoMaterial_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboMarca.SelectedIndex = 0;

            int count = acbProducto.Entries.Count;
            if (count > 0)
            {
                acbProducto.Entries.RemoveAt(count - 1);
            }
            
        }

        protected void cboMarca_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            int count = acbProducto.Entries.Count;
            if (count > 0)
            {
                acbProducto.Entries.RemoveAt(count - 1);
            }
            
        }


        private int ValidarConsulta(string Codigo_Almacen, string Codigo_Tipo, string Codigo_Marca, string Codigo_Item)
        {
            int bloquear = 0; 
            try
            {
                if (Codigo_Almacen=="0")
                {
                    if (Codigo_Marca == "0")
                    {
                        if (string.IsNullOrEmpty(Codigo_Item))
                        {
                            bloquear = 1; 
                        }
                    }
                }
                else
                {
                    if (Codigo_Marca == "0")
                    {
                        if (string.IsNullOrEmpty(Codigo_Item))
                        {
                            bloquear = 2;
                        }
                    }
                }
 

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bloquear; 
        }

        protected void grdStock_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
    
                var TransitoGestion = DataBinder.Eval(item.DataItem, "TransitoGestion");
                var Kardex = DataBinder.Eval(item.DataItem, "Kardex");
                var ID_Almacen = DataBinder.Eval(item.DataItem, "ID_Almacen");

                HyperLink link = new HyperLink();
 
                link.Text = item["TransitoGestion"].Text;
                link.Attributes.Add("OnClick", "ShowRegistrarGestion(" + Kardex + "," + ID_Almacen + ");");
                link.ForeColor = System.Drawing.Color.Blue; 
  
                item["TransitoGestion"].Controls.Add(link);

            }
        }


        protected void ramProyectado_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if (e.Argument == "Rebind")
                {
                    grdStock.MasterTableView.SortExpressions.Clear();
                    grdStock.MasterTableView.GroupByExpressions.Clear();
                   
                    grdStock.Rebind();

                    lblMensaje.Text = "Proyección cargada con éxito.";
                    lblMensaje.CssClass = "mensajeExito";
                }


                if (e.Argument.Split('(')[0].Trim() == "RebindAndNavigateBuscar")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "buscar();", true);

                }

                if (e.Argument.Split('(')[0].Trim() == "RebindAndNavigate")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "buscarHistorico();", true);
                }

            }
            catch (Exception ex)
            {
                rwmVidaLey.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }


    }
}