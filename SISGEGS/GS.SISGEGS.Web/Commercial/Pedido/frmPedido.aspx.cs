using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.DocumentoWCF;
using GS.SISGEGS.Web.FormaPagoWCF;
using GS.SISGEGS.Web.PedidoWCF;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.DM;
using System.Web.Services;
using Telerik.Web.UI;

namespace GS.SISGEGS.Web.Commercial.Pedido
{
    public partial class frmPedido : System.Web.UI.Page
    {
        #region Métodos privados
        private void FormaPago_ComboBox()
        {
            FormaPagoWCFClient objFormaPagoWCF;
            VBG00890Result objFormaPago;
            List<VBG00890Result> lstFormaPago;
            try
            {
                objFormaPagoWCF = new FormaPagoWCFClient();
                objFormaPago = new VBG00890Result();

                objFormaPago.ID = -1;
                objFormaPago.Nombre = "Todos";
                lstFormaPago = objFormaPagoWCF.FormaPago_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();
                lstFormaPago.Insert(0, objFormaPago);

                cboFormaPago.DataSource = lstFormaPago;
                cboFormaPago.DataValueField = "ID";
                cboFormaPago.DataTextField = "Nombre";
                cboFormaPago.DataBind();

                if (cboFormaPago.Items.Count > 0)
                    cboFormaPago.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Pedido_Listar(string idAgenda, DateTime fechaInicial, DateTime fechaFinal, int? idDocumento, string idVendedor, int? idFormaPago, decimal? idEstadoAprobacion)
        {
            PedidoWCFClient objPedidoWCF;
            bool superUsuario = false;
            try
            {
                objPedidoWCF = new PedidoWCFClient();
                grdPedido.DataSource = objPedidoWCF.Pedido_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda, fechaInicial, fechaFinal, idDocumento,
                    idVendedor, idFormaPago, idEstadoAprobacion, ref superUsuario);
                grdPedido.DataBind();
                if (!superUsuario)
                {
                    lblVendedor.Visible = false;
                    acbVendedor.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void TipoDocumento_ComboBox()
        {
            DocumentoWCFClient objDocumentoWCF;
            VBG00716Result objDocumento;
            List<VBG00716Result> lstDocumentos;
            try
            {
                objDocumentoWCF = new DocumentoWCFClient();
                objDocumento = new VBG00716Result();

                objDocumento.Nombre = "Todos";
                objDocumento.ID = -1;
                lstDocumentos = objDocumentoWCF.Documento_ListarDocVenta(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();
                lstDocumentos.Insert(0, objDocumento);

                cboTipoDocumento.DataSource = lstDocumentos;
                cboTipoDocumento.DataTextField = "Nombre";
                cboTipoDocumento.DataValueField = "ID";
                cboTipoDocumento.DataBind();

                if (cboTipoDocumento.Items.Count > 0)
                    cboTipoDocumento.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Métodos web
        [WebMethod]
        public static AutoCompleteBoxData Agenda_BuscarVendedor(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarVendedorResult[] lst = objAgendaWCFClient.Agenda_ListarVendedor(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsAgenda_ListarVendedorResult agenda in lst)
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
        public static AutoCompleteBoxData Agenda_BuscarCliente(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarClienteResult[] lst = objAgendaWCFClient.Agenda_ListarCliente(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString);
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
                    dpFechaInicio.SelectedDate = DateTime.Now.AddDays(-7);
                    dpFechaFinal.SelectedDate = DateTime.Now;
                    TipoDocumento_ComboBox();
                    FormaPago_ComboBox();

                    Pedido_Listar(acbCliente.Text.Split('-')[0], dpFechaInicio.SelectedDate.Value, dpFechaFinal.SelectedDate.Value, Int32.Parse(cboTipoDocumento.SelectedValue),
                        ((Usuario_LoginResult)Session["Usuario"]).nroDocumento, int.Parse(cboFormaPago.SelectedValue), decimal.Parse(cboEstado.SelectedValue));

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
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowInsertForm(0);", true);
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
                if ((dpFechaFinal.SelectedDate.Value - dpFechaInicio.SelectedDate.Value).TotalDays < 0)
                    throw new ArgumentException("Error: La fecha inicio no debe ser mayor a la fecha final.");

                if (acbVendedor.Visible)
                    Pedido_Listar(acbCliente.Text.Split('-')[0], dpFechaInicio.SelectedDate.Value, dpFechaFinal.SelectedDate.Value,
                        int.Parse(cboTipoDocumento.SelectedValue), acbVendedor.Text.Split('-')[0], int.Parse(cboFormaPago.SelectedValue),
                        int.Parse(cboEstado.SelectedValue));
                else
                    Pedido_Listar(acbCliente.Text.Split('-')[0], dpFechaInicio.SelectedDate.Value, dpFechaFinal.SelectedDate.Value,
                        int.Parse(cboTipoDocumento.SelectedValue), ((Usuario_LoginResult)Session["Usuario"]).nroDocumento,
                        int.Parse(cboFormaPago.SelectedValue), int.Parse(cboEstado.SelectedValue));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void ramPedido_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            lblMensaje.Text = "";
            if (e.Argument == "Rebind")
            {
                grdPedido.MasterTableView.SortExpressions.Clear();
                grdPedido.MasterTableView.GroupByExpressions.Clear();
                Pedido_Listar(acbCliente.Text.Split('-')[0], dpFechaInicio.SelectedDate.Value, dpFechaFinal.SelectedDate.Value, int.Parse(cboTipoDocumento.SelectedValue),
                        ((Usuario_LoginResult)Session["Usuario"]).nroDocumento, int.Parse(cboFormaPago.SelectedValue), int.Parse(cboEstado.SelectedValue));
                grdPedido.Rebind();

                Session["lstProductos"] = null;
                Session["lstImpuestos"] = null;

                lblMensaje.Text = "Pedido creado con éxito.";
                lblMensaje.CssClass = "mensajeExito";
            }

            if (e.Argument == "RebindAndNavigate")
            {
                grdPedido.MasterTableView.SortExpressions.Clear();
                grdPedido.MasterTableView.GroupByExpressions.Clear();
                grdPedido.MasterTableView.CurrentPageIndex = grdPedido.MasterTableView.PageCount - 1;
                grdPedido.Rebind();
            }
        }

        protected void grdPedido_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    string rpt = ((Label)e.Item.FindControl("Aprobacion1")).Text;

                    if (((Label)e.Item.FindControl("Aprobacion1")).Text == "False")
                    {
                        ((Image)e.Item.FindControl("imgAprobacion")).ImageUrl = "~/Images/Icons/sign-error-16.png";
                        ((Image)e.Item.FindControl("imgAprobacion")).ToolTip = "Por aprobar";
                    }
                    else
                    {
                        ((Image)e.Item.FindControl("imgAprobacion")).ImageUrl = "~/Images/Icons/sign-check-16.png";
                        ((Image)e.Item.FindControl("imgAprobacion")).ToolTip = "Aprobado";
                    }

                    if (decimal.Parse(item["CantGuia"].Text) == 0)
                    {
                        ((Image)e.Item.FindControl("imgEstado")).Visible = true;
                        ((ImageButton)e.Item.FindControl("ibEstado")).Visible = false;
                    }
                    else
                    {
                        if (decimal.Parse(item["CantGuia"].Text) == decimal.Parse(item["CantProd"].Text))
                        {
                            ((Image)e.Item.FindControl("ibEstado")).ToolTip = "Con guía";
                            ((Image)e.Item.FindControl("ibEstado")).ImageUrl = "~/Images/Icons/circle-green-16.png";
                        }
                        else {
                            ((Image)e.Item.FindControl("ibEstado")).ToolTip = "Parcialmente guíado";
                            ((Image)e.Item.FindControl("ibEstado")).ImageUrl = "~/Images/Icons/circle-yellow-16.png";
                        }
                        ((ImageButton)e.Item.FindControl("ibEstado")).Visible = true;
                        ((Image)e.Item.FindControl("imgEstado")).Visible = false;
                        ((ImageButton)e.Item.FindControl("ibEliminar")).Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdPedido_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    PedidoWCFClient objPedidoWCF = new PedidoWCFClient();
                    objPedidoWCF.Pedido_Eliminar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, int.Parse(e.CommandArgument.ToString()),
                        ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, ((Usuario_LoginResult)Session["Usuario"]).password);

                    Pedido_Listar(acbCliente.Text.Split('-')[0], dpFechaInicio.SelectedDate.Value, dpFechaFinal.SelectedDate.Value, int.Parse(cboTipoDocumento.SelectedValue),
                    acbVendedor.Text.Split('-')[0], int.Parse(cboFormaPago.SelectedValue), int.Parse(cboEstado.SelectedValue));

                    lblMensaje.Text = "Se eliminó el pedido " + e.CommandArgument.ToString() + " con éxito.";
                    lblMensaje.CssClass = "mensajeExito";
                }

                if (e.CommandName == "VerGuia")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "DocumentsViewer("+ e.CommandArgument.ToString() + ");", true);
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