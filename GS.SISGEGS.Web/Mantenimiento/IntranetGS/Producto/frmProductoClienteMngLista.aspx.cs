using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.BE;
using GS.SISGEGS.Web.ItemWCF;
using GS.SISGEGS.DM;
using Telerik.Web.UI;
using System.Web.Services;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.MonedaWCF;
using GS.SISGEGS.Web.Helpers;


namespace GS.SISGEGS.Web.Mantenimiento.IntranetGS.Producto
{
    public partial class frmProductoClienteMngLista : System.Web.UI.Page
    {
        private List<MantenimientoProductos> lstproductos = new List<MantenimientoProductos>();
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

        private DateTime? ValidarDatos()
        {
            DateTime? vigFinal = null;
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
            return vigFinal;
        }
        private void GuardarRegistro()
        {
            ItemWCFClient objItemWCF = new ItemWCFClient();
            

            if (Session["lstproductos"] != null)
            {
                lstproductos = JsonHelper.JsonDeserialize<List<MantenimientoProductos>>((string)Session["lstproductos"]);
                if(lstproductos.Any())
                {
                    objItemWCF.Item_RegistrarPrecioClienteLista(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                        ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, lstproductos.ToArray());


                    Session["lstproductos"] = null;
                    lstproductos.Clear();
                    gvwparcial.DataSource = lstproductos;
                    gvwparcial.DataBind();
                    txtKardex.Text = string.Empty;
                    txtPrecio.Text = "0";
                    txtPrecEspecial.Text = "0";

                    
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "StringText();", true);

                    rwmProductoMant.RadAlert("Se registraron los productos correctamente", 450, 150, "Mantenimiento de Productos", null, "../../../Images/Icons/sign-check-32.png");
                    acbProducto.Focus();
                    
                    //Response.Redirect("~/Mantenimiento/IntranetGS/Producto/frmProductoCliente.aspx");

                }

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
                    
                    Session["lstproductos"] = null;
                    Moneda_Cargar();
                    dpFechaInicio.SelectedDate = DateTime.Now;
                    dpFechaFinal.SelectedDate = DateTime.Now;
                    //if ((Request.QueryString["id"]) == "0")
                    //{
                    //    Title = "Registrar precio por cliente";
                    //    btnGuardar.Enabled = false;
                    //}
                    Title = "Registrar precio por cliente";
                    btnGuardar.Enabled = false;

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

            
            try
            {
                if (Session["lstproductos"] != null)
                {
                    lstproductos = JsonHelper.JsonDeserialize<List<MantenimientoProductos>>((string)Session["lstproductos"]);
                    if(lstproductos.Any())
                        GuardarRegistro();
                }
                else
                {
                    rwmProductoMant.RadAlert("No hay ningun registro.", 450, 150, "Mantenimiento de Productos", null, "../../../Images/Icons/sign-error-32.png");
                }
                
                
            }
            catch (Exception ex)
            {
                rwmProductoMant.RadAlert(ex.Message, 450, 150, "Mantenimiento de Productos", null, "../../../Images/Icons/sign-error-32.png");
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                lstproductos = new List<MantenimientoProductos>();
                if(Session["lstproductos"]!= null)
                {
                    lstproductos = JsonHelper.JsonDeserialize<List<MantenimientoProductos>>((string)Session["lstproductos"]);
                    
                }

                //JsonHelper.JsonDeserialize<List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult>>((string)Session["lstocparcialsel"]);
                DateTime? fechavigfinal=ValidarDatos();

                MantenimientoProductos producto = new MantenimientoProductos
                {
                    Id_Agenda = acbCliente.Text.Split('-')[0],
                    AgendaNombre =
                                    acbCliente.Text.Substring(
                                    acbCliente.Text.Split('-')[0].Length + 1,
                                    (acbCliente.Text.Length - (acbCliente.Text.Split('-')[0].Length + 1))
                                    ),
                    ItemCodigo = acbProducto.Text.Split('-')[0],
                    Kardex = Convert.ToInt32(txtKardex.Text),
                    NombreKardex = acbProducto.Text.Substring(
                                    acbProducto.Text.Split('-')[0].Length + 1,
                                    (acbProducto.Text.Length - (acbProducto.Text.Split('-')[0].Length + 1))
                                    ),
                    Id_UnidadInv = txtUnidad.Text,
                    Precio = Convert.ToDouble(txtPrecio.Text.Trim()==string.Empty ? "0" : txtPrecio.Text.Trim()),
                    IdMoneda = Convert.ToInt32(cboMoneda.SelectedValue),
                    Moneda = cboMoneda.Text,
                    PrecioEspecial = Convert.ToDouble(txtPrecEspecial.Text.Trim()==string.Empty ? "0" : txtPrecEspecial.Text.Trim()),
                    SinTermino = btnTermino.Checked,
                    FechaVigInicio = Convert.ToDateTime(dpFechaInicio.SelectedDate),
                    FechaVigFin = fechavigfinal
                };

                if (lstproductos.Where(x => x.Id_Agenda ==producto.Id_Agenda && x.ItemCodigo==producto.ItemCodigo && x.Kardex==producto.Kardex).Any())
                {
                    rwmProductoMant.RadAlert("El producto: " + producto.NombreKardex + " , se encuentra registrado con el mismo cliente.", 450,150,"Mantenimiento de Productos",null, "../../../Images/Icons/sign-error-32.png");
                    return;
                }


                lstproductos.Add(producto);
                //lstproductos = JsonHelper.JsonDeserialize<List<Dictionary<string, object>>>((string)Session["lstproductos"]);
                Session["lstproductos"]= JsonHelper.JsonSerializer(lstproductos);
                gvwparcial.DataSource = lstproductos;
                gvwparcial.DataBind();

                txtKardex.Text = string.Empty;
                txtPrecio.Text = "0";
                txtPrecEspecial.Text = "0";

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "StringText();", true);

                
                acbProducto.Focus();

            }
            catch (Exception ex)
            {
                rwmProductoMant.RadAlert(ex.Message, 450, 150, "Mantenimiento de Productos", null, "../../../Images/Icons/sign-error-32.png");
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void gvwparcial_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                string Id_Agenda = string.Empty;
                string ItemCodigo = string.Empty;
                Int32 kardex = 0;
                if (Session["lstproductos"] != null)
                {
                    lstproductos = JsonHelper.JsonDeserialize<List<MantenimientoProductos>>((string)Session["lstproductos"]);
                    if(lstproductos.Any())
                    {
                        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                        Id_Agenda = commandArgs[0];
                        ItemCodigo = commandArgs[1];
                        kardex = Convert.ToInt32(commandArgs[2]);

                        lstproductos.Remove(lstproductos.Where(x => x.Id_Agenda == Id_Agenda && x.ItemCodigo == ItemCodigo && x.Kardex == kardex).First());
                        gvwparcial.DataSource = lstproductos;
                        gvwparcial.DataBind();

                        Session["lstproductos"] = JsonHelper.JsonSerializer(lstproductos);

                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}