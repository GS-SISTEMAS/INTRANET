using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using GS.SISGEGS.DM;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.DocumentoWCF;
using GS.SISGEGS.Web.FormaPagoWCF;
using GS.SISGEGS.Web.OrdenVentaWCF;
using GS.SISGEGS.Web.LoginWCF;

namespace GS.SISGEGS.Web.Compras.Pedido
{
    public partial class frmOrdenCompra : System.Web.UI.Page
    {
        #region Métodos privados
        //private void FormaPago_ComboBox()
        //{
        //    FormaPagoWCFClient objFormaPagoWCF;
        //    VBG00890Result objFormaPago;
        //    List<VBG00890Result> lstFormaPago;
        //    try
        //    {
        //        objFormaPagoWCF = new FormaPagoWCFClient();
        //        objFormaPago = new VBG00890Result();

        //        objFormaPago.ID = -1;
        //        objFormaPago.Nombre = "Todos";
        //        lstFormaPago = objFormaPagoWCF.FormaPago_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
        //            ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();
        //        lstFormaPago.Insert(0, objFormaPago);

        //        cboFormaPago.DataSource = lstFormaPago;
        //        cboFormaPago.DataValueField = "ID";
        //        cboFormaPago.DataTextField = "Nombre";
        //        cboFormaPago.DataBind();

        //        if (cboFormaPago.Items.Count > 0)
        //            cboFormaPago.SelectedIndex = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private void OrdenVenta_Listar(string ID_Agenda, DateTime fechaInicial, DateTime fechaFinal, string ID_Vendedor)
        {
            try
            {
                OrdenVentaWCFClient objOrdenVentaWCF = new OrdenVentaWCFClient();
                List<gsOV_ListarResult> lst = objOrdenVentaWCF.OrdenVenta_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, ID_Agenda, fechaInicial, fechaFinal, ID_Vendedor,
                    ((Usuario_LoginResult)Session["Usuario"]).modificarPedido).ToList();
                ViewState["lstOrdenVenta"] = JsonHelper.JsonSerializer(lst);
                grdOrdenVenta.DataSource = lst;
                grdOrdenVenta.DataBind();

                ViewState["fechaInicial"] = fechaInicial;
                ViewState["fechaFinal"] = fechaFinal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void TipoDocumento_ComboBox()
        //{
        //    DocumentoWCFClient objDocumentoWCF;
        //    VBG00716Result objDocumento;
        //    List<VBG00716Result> lstDocumentos;
        //    try
        //    {
        //        objDocumentoWCF = new DocumentoWCFClient();
        //        objDocumento = new VBG00716Result();

        //        objDocumento.Nombre = "Todos";
        //        objDocumento.ID = -1;
        //        lstDocumentos = objDocumentoWCF.Documento_ListarDocVenta(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
        //            ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();
        //        lstDocumentos.Insert(0, objDocumento);

        //        cboTipoDocumento.DataSource = lstDocumentos;
        //        cboTipoDocumento.DataTextField = "Nombre";
        //        cboTipoDocumento.DataValueField = "ID";
        //        cboTipoDocumento.DataBind();

        //        if (cboTipoDocumento.Items.Count > 0)
        //            cboTipoDocumento.SelectedIndex = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion

        #region Métodos web
        //[WebMethod]
        //public static AutoCompleteBoxData Agenda_BuscarVendedor(object context)
        //{
        //    AutoCompleteBoxData res = new AutoCompleteBoxData();
        //    string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
        //    if (searchString.Length > 2)
        //    {
        //        AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
        //        gsAgenda_ListarVendedorResult[] lst = objAgendaWCFClient.Agenda_ListarVendedor(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
        //            ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString);
        //        List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

        //        foreach (gsAgenda_ListarVendedorResult agenda in lst)
        //        {
        //            AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
        //            childNode.Text = agenda.ID_Agenda + "-" + agenda.Nombre;
        //            childNode.Value = agenda.ID_Agenda;
        //            result.Add(childNode);
        //        }
        //        res.Items = result.ToArray();
        //    }
        //    return res;
        //}

        [WebMethod]
        public static AutoCompleteBoxData Agenda_BuscarCliente(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarClienteResult[] lst = objAgendaWCFClient.Agenda_ListarCliente(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString, 1);
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
        #endregion

        #region Métodos protegidos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (!Page.IsPostBack)
                {
                    LoginWCFClient objLoginWCF = new LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idUsuario);

                    dpFechaInicio.SelectedDate = DateTime.Now.AddDays(-7);
                    dpFechaFinal.SelectedDate = DateTime.Now;

                    if (Request.QueryString["fechaInicial"] == null)
                        OrdenVenta_Listar(null, dpFechaInicio.SelectedDate.Value, dpFechaFinal.SelectedDate.Value,
                            ((Usuario_LoginResult)Session["Usuario"]).nroDocumento);
                    else {
                        DateTime fechaInicial = DateTime.ParseExact(Request.QueryString["fechaInicial"], "dd/MM/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);
                        DateTime fechaFinal = DateTime.ParseExact(Request.QueryString["fechaFinal"], "dd/MM/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);

                        dpFechaInicio.SelectedDate = fechaInicial;
                        dpFechaFinal.SelectedDate = fechaFinal;
                        OrdenVenta_Listar(null, fechaInicial, fechaFinal, ((Usuario_LoginResult)Session["Usuario"]).nroDocumento);
                    }

                    lblMensaje.Text = "La página cargo correctamente";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            lblMensaje.Text = "";
            try
            {
                Response.Redirect("~/Comercial/Pedido/frmOrdenVentaMng.aspx?idOrdenVenta=0&" + "fechaInicial=" + ((DateTime)ViewState["fechaInicial"]).ToString("dd/MM/yyyy") +
                    "&fechaFinal=" + ((DateTime)ViewState["fechaFinal"]).ToString("dd/MM/yyyy"));
                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowForm(0);", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            lblMensaje.Text = "";
            try
            {
                OrdenVenta_Listar(null, dpFechaInicio.SelectedDate.Value,
                    dpFechaFinal.SelectedDate.Value, ((Usuario_LoginResult)Session["Usuario"]).nroDocumento);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void ramOrdenVenta_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            lblMensaje.Text = "";
            if (e.Argument == "Rebind")
            {
                grdOrdenVenta.MasterTableView.SortExpressions.Clear();
                grdOrdenVenta.MasterTableView.GroupByExpressions.Clear();
                OrdenVenta_Listar(null, dpFechaInicio.SelectedDate.Value, dpFechaFinal.SelectedDate.Value,
                        ((Usuario_LoginResult)Session["Usuario"]).nroDocumento);
                grdOrdenVenta.Rebind();

                Session["lstProductos"] = null;
                Session["lstImpuestos"] = null;

                lblMensaje.Text = "Pedido creado con éxito.";
                lblMensaje.CssClass = "mensajeExito";
            }
        }

        protected void grdOrdenVenta_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                grdOrdenVenta.DataSource = JsonHelper.JsonDeserialize<List<gsOV_ListarResult>>((string)ViewState["lstOrdenVenta"]);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdOrdenVenta_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                OrdenVentaWCFClient objOrdenVenta = new OrdenVentaWCFClient();
                objOrdenVenta.OrdenVenta_Eliminar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, Convert.ToInt32(((GridDataItem)e.Item).GetDataKeyValue("Op")),"");

                List<gsOV_ListarResult> lst = JsonHelper.JsonDeserialize<List<gsOV_ListarResult>>((string)ViewState["lstOrdenVenta"]);
                lst.Remove(lst.Find(x => x.Op == Convert.ToInt32(((GridDataItem)e.Item).GetDataKeyValue("Op"))));
                grdOrdenVenta.DataSource = lst;
                grdOrdenVenta.DataBind();
                ViewState["lstOrdenVenta"] = JsonHelper.JsonSerializer(lst);

                lblMensaje.Text = "Se eliminó el pedido " + ((GridDataItem)e.Item).GetDataKeyValue("Op") + " con éxito.";
                lblMensaje.CssClass = "mensajeExito";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdOrdenVenta_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "AbrirPedido")
                {
                    Response.Redirect("~/Comercial/Pedido/frmOrdenVentaMng.aspx?idOrdenVenta="+ e.CommandArgument.ToString() + "&fechaInicial=" + ((DateTime)ViewState["fechaInicial"]).ToString("dd/MM/yyyy") +
                    "&fechaFinal=" + ((DateTime)ViewState["fechaFinal"]).ToString("dd/MM/yyyy"));
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowForm(" + e.CommandArgument.ToString() + ");", true);
                }

                if (e.CommandName == "Documentos")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowDocuments(" + e.CommandArgument.ToString() + ");", true);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdOrdenVenta_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    if (decimal.Parse(item["Guia"].Text) == 2)
                    {
                        ((Image)e.Item.FindControl("imgGuia")).ImageUrl = "~/Images/Icons/circle-green-16.png";
                    }
                    else
                    {
                        if (decimal.Parse(item["Guia"].Text) == 1)
                        {
                            ((Image)e.Item.FindControl("imgGuia")).ImageUrl = "~/Images/Icons/circle-yellow-16.png";
                        }
                        else
                        {
                            ((Image)e.Item.FindControl("imgGuia")).ImageUrl = "~/Images/Icons/circle-red-16.png";
                        }
                    }

                    if (decimal.Parse(item["Factura"].Text) == 2)
                    {
                        ((Image)e.Item.FindControl("imgFactura")).ImageUrl = "~/Images/Icons/circle-green-16.png";
                    }
                    else
                    {
                        if (decimal.Parse(item["Factura"].Text) == 1)
                        {
                            ((Image)e.Item.FindControl("imgFactura")).ImageUrl = "~/Images/Icons/circle-yellow-16.png";
                        }
                        else
                        {
                            ((Image)e.Item.FindControl("imgFactura")).ImageUrl = "~/Images/Icons/circle-red-16.png";
                        }
                    }

                    if (item["Aprobacion"].Text == "True")
                    {
                        ((Image)e.Item.FindControl("imgAprobacion")).ImageUrl = "~/Images/Icons/sign-check-16.png";
                    }
                    else
                    {
                        ((Image)e.Item.FindControl("imgAprobacion")).ImageUrl = "~/Images/Icons/sign-error-16.png";
                    }
                }
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