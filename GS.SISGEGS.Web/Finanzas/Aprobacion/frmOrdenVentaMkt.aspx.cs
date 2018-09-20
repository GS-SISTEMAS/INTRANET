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
using GS.SISGEGS.Web.PedidoWCF;
using GS.SISGEGS.Web.EstadoCuentaWCF;


namespace GS.SISGEGS.Web.Finanzas.Aprobacion
{
    public partial class frmOrdenVentaMkt : System.Web.UI.Page
    {
        #region Métodos privados
        private void Sectorista_Cargar()
        {
            try
            {
                AgendaWCFClient objAgendaWCF = new AgendaWCFClient();
                gsUsuario_SectoristaResult objSectorista = new gsUsuario_SectoristaResult();
                List<gsUsuario_SectoristaResult> lstSectorista;

                lstSectorista = objAgendaWCF.Agenda_ListarSectorista(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, null , 1).ToList();

                var lstSect = from x in lstSectorista
                                select new
                                {
                                    x.ID_Agenda,
                                    DisplayField =  x.AgendaNombre
                                    //DisplayField = String.Format("{0} {1} {2} {3}", x.Placa, x.Modelo, x.Marca, x.Agenda)
                                };

                //cbSectorista.DataSource = lstSect;
                //cbSectorista.DataTextField = "DisplayField";
                //cbSectorista.DataValueField = "ID_Agenda";
                //cbSectorista.DataBind();

                //cbSectorista.SelectedIndex = 0; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FormaPago_Cargar()
        {
            FormaPagoWCFClient objFormaPagoWCF;
            try
            {
                objFormaPagoWCF = new FormaPagoWCFClient();
                cboFormaPago.DataSource = objFormaPagoWCF.FormaPago_Listar(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario).ToList().FindAll(x=> x.ID == 2).ToList(); ;
                cboFormaPago.DataTextField = "Nombre";
                cboFormaPago.DataValueField = "ID";
                cboFormaPago.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Estado_Cargar()
        {
            try
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Value = "0";
                item.Text = "Todos";
                cboEstado.Items.Add(item);
                item = new RadComboBoxItem();
                item.Value = "1";
                item.Text = "Aprobados";
                cboEstado.Items.Add(item);
                item = new RadComboBoxItem();
                item.Value = "2";
                item.Text = "Desaprobados";
                cboEstado.Items.Add(item);

                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void OrdenVenta_Listar(string ID_Agenda, DateTime fechaInicial, DateTime fechaFinal, string ID_Vendedor, string Id_sectorista, int Estado, int FormaPago)
        {
            try
            {
                OrdenVentaWCFClient objOrdenVentaWCF = new OrdenVentaWCFClient();

                List<gsOV_Listar_SectoristaResult> lst = objOrdenVentaWCF.OrdenVenta_Listar_Sectorista(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, ID_Agenda, fechaInicial, fechaFinal, ID_Vendedor,
                    ((Usuario_LoginResult)Session["Usuario"]).modificarPedido, Id_sectorista, Estado, FormaPago).ToList().FindAll(x => x.Id_Pago == 2).ToList() ;


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

        #endregion

        #region Métodos web


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
                    Sectorista_Cargar();
                    FormaPago_Cargar();
                    Estado_Cargar();
                    cboEstado.SelectedValue = "2";

                    if (Request.QueryString["fechaInicial"] == null)
                    {
                        OrdenVenta_Listar(null, dpFechaInicio.SelectedDate.Value, dpFechaFinal.SelectedDate.Value, null, null, int.Parse(cboEstado.SelectedValue), int.Parse(cboFormaPago.SelectedValue));
                    }
                    else
                    {
                        DateTime fechaInicial = DateTime.ParseExact(Request.QueryString["fechaInicial"], "dd/MM/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);
                        DateTime fechaFinal = DateTime.ParseExact(Request.QueryString["fechaFinal"], "dd/MM/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);

                        dpFechaInicio.SelectedDate = fechaInicial;
                        dpFechaFinal.SelectedDate = fechaFinal;
                        OrdenVenta_Listar(null, fechaInicial, fechaFinal, null, null, int.Parse(cboEstado.SelectedValue), int.Parse(cboFormaPago.SelectedValue));
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
        protected void btnAprobar_Click(object sender, EventArgs e)
        {

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            PedidoWCFClient objPedidoWCF = new PedidoWCFClient(); 

            try
            {
                foreach (GridItem rowitem in grdOrdenVenta.MasterTableView.Items)
                {
                    GridDataItem dataitem = (GridDataItem)rowitem;
                    TableCell cell = dataitem["CheckColumn"];
                    CheckBox checkBox = (CheckBox)cell.Controls[0].FindControl("Check");
                    if (checkBox.Checked == true && checkBox.Enabled == true )
                    {
                        int Op = Convert.ToInt32(dataitem.GetDataKeyValue("Op").ToString());
                        string IdAgenda = dataitem["ID_Agenda"].Text;
                        int IdPedido =  int.Parse( dataitem["ID"].Text) ;
                        decimal ValorVenta = decimal.Parse(dataitem["Total"].Text);
                        string Perfil = ((Usuario_LoginResult)Session["Usuario"]).nombrePerfil;
                        string usuario = ((Usuario_LoginResult)Session["Usuario"]).nombres;
                        int Id_moneda = int.Parse(dataitem["Id_moneda"].Text);

                        //string idSectorista = cbSectorista.SelectedValue;
                        string comentario = "Aprobado por "+ Perfil + " ("+ usuario +")."; 

                        var idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                        var idUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;


                        var opDocVenta = objPedidoWCF.VerificarExisteDocVenta(idEmpresa,idUsuario, IdAgenda).OpDocVenta;
                        //rwmOrdenVenta.RadConfirm("¿Esta seguro que desea Aprobar el pedido " + Op + "?", null, 400, 200,null, "Aprobar Pedido"); 
                        if (opDocVenta != 0)
                            Registrar_Aprobacion(IdAgenda, IdPedido, Op, null, comentario, ValorVenta, Id_moneda);
                        else
                            rwmOrdenVenta.RadAlert("Existe otro pedido que ha sido aprobado y aun no se ha generado su documento de venta.",400,200,"Alerta Verificar Pedido","");

                    }
                }
                OrdenVenta_Listar(null, dpFechaInicio.SelectedDate.Value, dpFechaFinal.SelectedDate.Value,null, null, int.Parse(cboEstado.SelectedValue), int.Parse(cboFormaPago.SelectedValue));

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void Registrar_Aprobacion( string idAgenda,  int IdPedido, int Op, string idSectorista, string comentario, decimal ValorVenta, int id_moneda)
        {
            PedidoWCFClient objPedidoWCF = new PedidoWCFClient();
            gsAgendaCliente_BuscarLimiteCreditoResult objLimite = new gsAgendaCliente_BuscarLimiteCreditoResult(); 

            decimal porcentaje = 0;
            decimal ValorPedido= 0;
            decimal LineaMaximo = 0;
            decimal LineaCredito = 0;
            decimal Deuda = 0;
            decimal DeudaIncluida = 0;
            decimal TC = 0;
            var idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
            var idUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;

            try
            {
                if (((Usuario_LoginResult)Session["Usuario"]).aprobarTransfGratuita == true)
                {
                    ValorPedido = ValorVenta; 
                    porcentaje =  (decimal.Parse(((Usuario_LoginResult)Session["Usuario"]).aprobarPorcentaje.ToString()) / 100 );
                    objLimite = ListarClientesResumen(idAgenda);

                    if( !string.IsNullOrEmpty(objLimite.ID_Agenda))
                    {
                        //LineaCredito = decimal.Parse(objLimite.LineaCredito.ToString());
                        //LineaMaximo = LineaCredito + (LineaCredito * porcentaje);
                        //Deuda = decimal.Parse(objLimite.TotalRiesgo.ToString());
                        //DeudaIncluida = Deuda + ValorPedido;

                        //-------------------------------------
                        TC = (decimal)objLimite.TC;
                        LineaCredito = decimal.Parse(objLimite.LineaCredito.ToString());
                        if (id_moneda == 1)
                        {
                            LineaCredito = LineaCredito * TC;
                        }
                        LineaMaximo = LineaCredito + (LineaCredito * porcentaje);
                        Deuda = decimal.Parse(objLimite.TotalRiesgo.ToString());

                        if (id_moneda == 1)
                        {
                            Deuda = Deuda * TC;
                        }
                        DeudaIncluida = Deuda + ValorPedido;

                        //-------------------------------------
                        if (DeudaIncluida <= LineaMaximo)
                        {
                            objPedidoWCF.Pedido_Aprobar(idEmpresa, idUsuario, IdPedido, Op, idSectorista, true, comentario);
                            objPedidoWCF.gsDocVentaAprobacion_Registrar(idEmpresa, IdPedido, Op, idAgenda, idUsuario);
                        }
                        else
                        {
                            rwmOrdenVenta.RadAlert("El porcentaje de sobregiro asignado no es suficiente. Solicitar la aprobación de su Superior. ", 400, null, "Mensaje de error", null);
                        }
                    }
                    else
                    {
                        rwmOrdenVenta.RadAlert("El cliente no tiene linea de crédito, solicitar a C&C. ", 400, null, "Mensaje de error", null);
                    }
                }
                else
                {
                    rwmOrdenVenta.RadAlert("No tiene autorización para aprobar pedidos. ", 400, null, "Mensaje de error", null);
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }

        }

        private gsAgendaCliente_BuscarLimiteCreditoResult ListarClientesResumen(string IdAgenda)
        {
            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();
            gsAgendaCliente_BuscarLimiteCreditoResult Limite = new gsAgendaCliente_BuscarLimiteCreditoResult(); 
            try
            {
                List<gsAgendaCliente_BuscarLimiteCreditoResult> LimiteCreditoAgenda = objEstadoCuentaWCF.EstadoCuenta_LimiteCreditoxCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, IdAgenda, 0).ToList();

                if(LimiteCreditoAgenda.Count>0)
                {
                    Limite = LimiteCreditoAgenda[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Limite; 
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            lblMensaje.Text = "";
            try
            {
                OrdenVenta_Listar(null, dpFechaInicio.SelectedDate.Value, dpFechaFinal.SelectedDate.Value, null, null, int.Parse(cboEstado.SelectedValue), int.Parse(cboFormaPago.SelectedValue));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdOrdenVenta_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                grdOrdenVenta.DataSource = JsonHelper.JsonDeserialize<List<gsOV_Listar_SectoristaResult>>((string)ViewState["lstOrdenVenta"]);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdOrdenVenta_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            PedidoWCFClient objPedidoWCF = new PedidoWCFClient();
            OrdenVentaWCFClient objOrdenVenta = new OrdenVentaWCFClient();

            GridDataItem dataitem = (GridDataItem)e.Item;

            try
            {
                int Op = Convert.ToInt32(((GridDataItem)e.Item).GetDataKeyValue("Op"));
                string IdAgenda = dataitem["ID_Agenda"].Text;
                int IdPedido = int.Parse(dataitem["ID"].Text);
                decimal ValorVenta = decimal.Parse(dataitem["Total"].Text);

                string Perfil = ((Usuario_LoginResult)Session["Usuario"]).nombrePerfil;
                string usuario = ((Usuario_LoginResult)Session["Usuario"]).nombres;
                string Comentario = "Desaprobado por " + Perfil + " (" + usuario + ").";


                objPedidoWCF.Pedido_DesAprobar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, IdPedido, Op,null, false );
                objOrdenVenta.OrdenVenta_Deasaprobar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, Op, Comentario);

                OrdenVenta_Listar(null, dpFechaInicio.SelectedDate.Value, dpFechaFinal.SelectedDate.Value, null, null, int.Parse(cboEstado.SelectedValue), int.Parse(cboFormaPago.SelectedValue));

                lblMensaje.Text = "Se desaprobo el pedido " + ((GridDataItem)e.Item).GetDataKeyValue("Op") + " con éxito.";
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
                //if (e.CommandName == "AbrirPedido")
                //{
                //    Response.Redirect("~/Comercial/Pedido/frmOrdenVentaMng.aspx?idOrdenVenta="+ e.CommandArgument.ToString() + "&fechaInicial=" + ((DateTime)ViewState["fechaInicial"]).ToString("dd/MM/yyyy") +
                //    "&fechaFinal=" + ((DateTime)ViewState["fechaFinal"]).ToString("dd/MM/yyyy"));
                //    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowForm(" + e.CommandArgument.ToString() + ");", true);
                //}

                //if (e.CommandName == "Documentos")
                //{
                //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowDocuments(" + e.CommandArgument.ToString() + ");", true);
                //}
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

                    if (item["Creditos"].Text == "True")
                    {
                        TableCell cell = item["CheckColumn"];
                        CheckBox checkBox = (CheckBox)cell.Controls[0].FindControl("Check");
                        checkBox.Checked = true;
                        checkBox.Enabled = false;

                        item["Elim"].Controls[0].Visible = true;
                    }
                    else
                    {
                        item["Elim"].Controls[0].Visible = false;
                    }


                    if (decimal.Parse(item["Guia"].Text) == 2)
                    {
                        ((Image)e.Item.FindControl("imgGuia")).ImageUrl = "~/Images/Icons/circle-green-16.png";
                        item["Elim"].Controls[0].Visible = false;
                    }
                    else
                    {
                        if (decimal.Parse(item["Guia"].Text) == 1)
                        {
                            ((Image)e.Item.FindControl("imgGuia")).ImageUrl = "~/Images/Icons/circle-yellow-16.png";
                            item["Elim"].Controls[0].Visible = false;
                        }
                        else
                        {
                            ((Image)e.Item.FindControl("imgGuia")).ImageUrl = "~/Images/Icons/circle-red-16.png";
                        }
                    }

                    if (decimal.Parse(item["Factura"].Text) == 2)
                    {
                        ((Image)e.Item.FindControl("imgFactura")).ImageUrl = "~/Images/Icons/circle-green-16.png";
                        item["Elim"].Controls[0].Visible = false;

                        TableCell cell = item["CheckColumn"];
                        CheckBox checkBox = (CheckBox)cell.Controls[0].FindControl("Check");
                        checkBox.Enabled = false;

                    }
                    else
                    {
                        if (decimal.Parse(item["Factura"].Text) == 1)
                        {
                            ((Image)e.Item.FindControl("imgFactura")).ImageUrl = "~/Images/Icons/circle-yellow-16.png";
                            item["Elim"].Controls[0].Visible = false;
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

                    if (item["BloqueadoEstado"].Text == "True")
                    {
                        ((Label)e.Item.FindControl("lblEstado")).Text = "Bloqueado";
                        e.Item.ForeColor = System.Drawing.Color.Red;

                        TableCell cell = item["CheckColumn"];
                        CheckBox checkBox = (CheckBox)cell.Controls[0].FindControl("Check");
                        checkBox.Checked = false;
                        checkBox.Enabled = false;

                    }
                    else
                    {
                        ((Label)e.Item.FindControl("lblEstado")).Text = "Habilitado";
                       
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