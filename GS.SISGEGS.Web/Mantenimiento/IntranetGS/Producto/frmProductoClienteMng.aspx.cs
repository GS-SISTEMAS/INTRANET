using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.ItemWCF;
using GS.SISGEGS.DM;
using Telerik.Web.UI;
using System.Web.Services;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.MonedaWCF;

namespace GS.SISGEGS.Web.Mantenimiento.IntranetGS.Producto
{
    public partial class frmProductoClienteMng1 : System.Web.UI.Page
    {
        #region Metodos web
        [WebMethod]
        public static AutoCompleteBoxData Agenda_BuscarCliente(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarClienteResult[] lst = objAgendaWCFClient.Agenda_ListarCliente(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString, null);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsAgenda_ListarClienteResult agenda in lst)
                {
                    AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                    childNode.Text = agenda.ID_Agenda + "-" + agenda.Nombre;
                    childNode.Value = agenda.ID_Agenda;
                    result.Add(childNode);
                }
                res.Items = result.ToArray();
            }
            return res;
        }

        [WebMethod]
        public static AutoCompleteBoxData Item_BuscarProducto(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (!string.IsNullOrEmpty(searchString) && searchString.Length >= 4)
            {
                ItemWCFClient objItemWCF = new ItemWCFClient();
                gsItem_ListarProductoResult[] lst = objItemWCF.Item_ListarProducto(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsItem_ListarProductoResult producto in lst)
                {
                    if (result.FindAll(x => x.Text == producto.ID_Item + "-" + producto.Nombre).Count == 0)
                    {
                        AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                        childNode.Text = producto.ID_Item + "-" + producto.Nombre;
                        childNode.Value = producto.ID_Item;
                        result.Add(childNode);
                    }
                }
                res.Items = result.ToArray();
            }
            return res;
        }
        #endregion

        #region Metodos privado
        private void Moneda_Cargar()
        {
            MonedaWCFClient objMonedaWCF;
            try
            {
                objMonedaWCF = new MonedaWCFClient();
                cboMoneda.DataSource = objMonedaWCF.Moneda_Listar(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario);
                cboMoneda.DataTextField = "Nombre";
                cboMoneda.DataValueField = "ID";
                cboMoneda.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Item_Cargar(decimal idPrecioCliente)
        {
            ItemWCFClient objItemWCF = new ItemWCFClient();
            VBG01124Result objProducto = new VBG01124Result();
            VBG01134Result objCliente = new VBG01134Result();
            string ID_Item = null;
            decimal? precioEspecial = null;
            DateTime? vigInicio = null;
            DateTime? vigFinal = null;
            try
            {
                btnBuscarProducto.Visible = false;
                objItemWCF.Item_BuscarPrecioCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idPrecioCliente, ref precioEspecial, ref vigInicio,
                    ref vigFinal, ref objCliente, ref objProducto, ref ID_Item);
                AutoCompleteBoxEntry entry = new AutoCompleteBoxEntry();
                entry.Text = objCliente.ID_Agenda + "-" + objCliente.Nombre;
                acbCliente.Entries.Add(entry);
                acbCliente.Enabled = false;
                entry = new AutoCompleteBoxEntry();
                entry.Text = ID_Item + "-" + objProducto.Nombre;
                acbProducto.Entries.Add(entry);
                acbProducto.Enabled = false;
                txtKardex.Text = objProducto.Item_ID.ToString();
                cboMoneda.SelectedValue = objProducto.ID_Moneda.ToString();
                txtPrecio.Text = objProducto.Precio.ToString();
                txtUnidad.Text = objProducto.UnidadInv;
                txtPrecEspecial.Value = Convert.ToDouble(precioEspecial);
                dpFechaInicio.SelectedDate = vigInicio;
                if (vigFinal != null)
                    dpFechaFinal.SelectedDate = vigFinal;
                else
                {
                    dpFechaFinal.Enabled = false;
                    btnTermino.Checked = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Metodos protegidos
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

                    Moneda_Cargar();
                    dpFechaInicio.SelectedDate = DateTime.Now;
                    dpFechaFinal.SelectedDate = DateTime.Now;
                    if ((Request.QueryString["id"]) == "0")
                    {
                        Title = "Registrar precio por cliente";
                        btnGuardar.Enabled = false;
                    }
                    else
                    {
                        Title = "Modificar precio por cliente";
                        Item_Cargar(int.Parse((Request.QueryString["id"])));
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnTermino_CheckedChanged(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (btnTermino.Checked)
                    dpFechaFinal.Enabled = false;
                else
                    dpFechaFinal.Enabled = true;
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

            ItemWCFClient objItemWCF = new ItemWCFClient();
            DateTime? vigFinal = null;
            decimal? idPrecioCliente = null;
            try
            {
                if (string.IsNullOrEmpty(dpFechaInicio.SelectedDate.ToString()))
                    throw new ArgumentException("Se debe seleccionar la fecha de inicio");
                if (!btnTermino.Checked)
                {
                    if (string.IsNullOrEmpty(dpFechaFinal.SelectedDate.ToString()))
                        throw new ArgumentException("Se debe seleccionar la fecha de finalización");
                    vigFinal = dpFechaFinal.SelectedDate;
                }
                if (string.IsNullOrEmpty(txtPrecEspecial.Text))
                    throw new ArgumentException("Se debe ingresar un precio valido");
                if (txtPrecEspecial.Value <= 0)
                    throw new ArgumentException("Se debe ingresar un precio mayor a 0");
                if (string.IsNullOrEmpty(acbCliente.Text))
                    throw new ArgumentException("Se debe seleccionar un cliente");
                if (string.IsNullOrEmpty(acbProducto.Text))
                    throw new ArgumentException("Se debe seleccionar un producto");
                if (Request.QueryString["id"] != "0")
                    idPrecioCliente = decimal.Parse((Request.QueryString["id"]));

                objItemWCF.Item_RegistrarPrecioCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, ref idPrecioCliente, acbProducto.Text.Split('-')[0],
                    acbCliente.Text.Split('-')[0], dpFechaInicio.SelectedDate, vigFinal, decimal.Parse(txtPrecEspecial.Text));

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind();", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            ItemWCFClient objItemWCF = new ItemWCFClient();
            VBG01124Result objProducto;
            try
            {
                objProducto = objItemWCF.Item_BuscarProducto(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, acbProducto.Text.Split('-')[0]);
                txtPrecio.Text = objProducto.Precio.ToString();
                txtKardex.Text = objProducto.Item_ID.ToString();
                txtUnidad.Text = objProducto.UnidadInv;
                btnGuardar.Enabled = true;

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