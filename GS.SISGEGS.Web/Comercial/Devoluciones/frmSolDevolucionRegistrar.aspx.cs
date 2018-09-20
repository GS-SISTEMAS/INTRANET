using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.SolDevolucionWCF;
using GS.SISGEGS.Web.Helpers;

namespace GS.SISGEGS.Web.Comercial.Devoluciones
{
    public partial class frmSolDevolucionRegistrar : System.Web.UI.Page
    {
        [WebMethod]
        public static AutoCompleteBoxData Agenda_TransporteBuscar(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarTransportistaResult[] lst = objAgendaWCFClient.Agenda_ListarTransportista(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsAgenda_ListarTransportistaResult agenda in lst)
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

        private List<gsDevolucionSolicitudDetalle> DevolucionSolicitudDetalle_Obtener() {
            List<gsDevolucionSolicitudDetalle> lstProductos = new List<gsDevolucionSolicitudDetalle>();
            gsDevolucionSolicitudDetalle objDevolucionSolicitudDetalle;
            try {

                foreach (GridDataItem row in grdDocVentaDetalle.Items)
                {
                    decimal cantEdit = Convert.ToDecimal(((RadNumericTextBox)row["CantEdit"].FindControl("txtCantidad")).Value);
                    decimal precEdit = Convert.ToDecimal(((RadNumericTextBox)row["PrecioEdit"].FindControl("txtPrecio")).Value);
                    if (0 != cantEdit || decimal.Parse(row["Precio"].Text.Replace("$", string.Empty)) != precEdit)
                    {
                        objDevolucionSolicitudDetalle = new gsDevolucionSolicitudDetalle();
                        objDevolucionSolicitudDetalle.ID_Amarre = int.Parse(row["ID_Amarre"].Text);
                        objDevolucionSolicitudDetalle.cantidad = cantEdit;
                        objDevolucionSolicitudDetalle.precioUnitario = precEdit;
                        objDevolucionSolicitudDetalle.CodUsuarioRegistro = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;

                        lstProductos.Add(objDevolucionSolicitudDetalle);
                    }
                }

                return lstProductos;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private gsDevolucionSolicitud DevolucionSolicitud_Obtener() {
            gsDevolucionSolicitud objDevolucionSolicitud = new gsDevolucionSolicitud();
            try {
                objDevolucionSolicitud.idDevolucionSolicitud = int.Parse(Request.QueryString["idDevolucionSolicitud"]);
                objDevolucionSolicitud.idDevolucionMotivo = int.Parse(cboMotivo.SelectedValue);
                objDevolucionSolicitud.ID_Almacen = decimal.Parse(cboAlmacen.SelectedValue);
                objDevolucionSolicitud.Op = decimal.Parse(Request.QueryString["Op"]);
                objDevolucionSolicitud.fechaEnvioDev = dpFechaDevolucion.SelectedDate.Value;
                objDevolucionSolicitud.guiaCliente = txtNroGuiaCliente.Text;
                objDevolucionSolicitud.guiaTransportista = txtNroGuiaTransportista.Text;
                objDevolucionSolicitud.ID_Transportista = acbTransporte.Text.Split('-')[0];
                objDevolucionSolicitud.fechaEnvio = dpFechaEnvio.SelectedDate.Value;
                objDevolucionSolicitud.observacion = txtObservacion.Text;
                //if (btnCobrarFlete.Checked)
                //    objDevolucionSolicitud.flete = Convert.ToDecimal(txtFlete.Value);
                objDevolucionSolicitud.CodUsuarioRegistro = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;

                return objDevolucionSolicitud;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private void DocVenta_Cargar(decimal Op)
        {
            SolDevolucionWCFClient objSolDevolucionWCF = new SolDevolucionWCFClient();
            gsDocVenta_BuscarResult objDocVenta;
            gsDocVenta_BuscarDetalleResult[] lstProductos = null;
            gsDevolucionSolicitud_BuscarResult objSolDev;
            gsDevolucionSolicitudDetalle_BuscarResult[] lstProdDev = null;
            try
            {
                if (Request.QueryString["idDevolucionSolicitud"] != "0")
                {
                    objSolDev = objSolDevolucionWCF.DevolucionSolicitud_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, int.Parse(Request.QueryString["idDevolucionSolicitud"]), ref lstProdDev);

                    //txtFlete.Value = Convert.ToDouble(objSolDev.flete);
                    //if (objSolDev.flete > 0)
                    //{
                    //    btnCobrarFlete.Checked = true;
                    //    txtFlete.Enabled = true;
                    //}
                    dpFechaDevolucion.SelectedDate = objSolDev.fechaEnvioDev;
                    dpFechaEnvio.SelectedDate = objSolDev.fechaEnvio;

                    AutoCompleteBoxEntry entry = new AutoCompleteBoxEntry();
                    entry.Text = objSolDev.ID_Transportista + "-" + objSolDev.Transportista;
                    acbTransporte.Entries.Add(entry);

                    txtObservacion.Text = objSolDev.observacion;
                    txtNroDocumento.Text = objSolDev.idDevolucionSolicitud.ToString();
                    cboAlmacen.SelectedValue = objSolDev.ID_Almacen.ToString();
                    cboMotivo.SelectedValue = objSolDev.idDevolucionMotivo.ToString();
                    txtNroGuiaCliente.Text = objSolDev.guiaCliente;
                    txtNroGuiaTransportista.Text = objSolDev.guiaTransportista;

                    if ((bool)objSolDev.aprobacion1) {
                        btnGuardar.Enabled = !(bool)objSolDev.aprobacion1;
                        btnAprobar.Text = "Desaprobar";
                        btnAprobar.Icon.PrimaryIconUrl = "../../Images/Icons/sign-error-16.png";
                    }

                    ViewState["lstProductos"] = JsonHelper.JsonSerializer(lstProdDev);
                }

                objDocVenta = objSolDevolucionWCF.DocVenta_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, Op, ref lstProductos);
                txtCliente.Text = objDocVenta.Agenda;
                txtZona.Text = objDocVenta.ZonaVendedor;
                txtVendedor.Text = objDocVenta.Vendedor;
                dpFechaVenta.SelectedDate = objDocVenta.Fecha;
                txtNroFactura.Text = objDocVenta.Transaccion;

                grdDocVentaDetalle.DataSource = lstProductos;
                grdDocVentaDetalle.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Almacen_ComboBox()
        {
            SolDevolucionWCFClient objSolDevolucionWCF = new SolDevolucionWCFClient();
            try
            {
                cboAlmacen.DataSource = objSolDevolucionWCF.AgendaAnexo_ListarAlmacenDevolucion(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario);
                cboAlmacen.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DevolucionMotivo_ComboBox()
        {
            SolDevolucionWCFClient objSolDevolucionWCF = new SolDevolucionWCFClient();
            try
            {
                cboMotivo.DataSource = objSolDevolucionWCF.DevolucionMotivo_ComboBox(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario);
                cboMotivo.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (!Page.IsPostBack)
                {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    DevolucionMotivo_ComboBox();
                    Almacen_ComboBox();
                    dpFechaDevolucion.SelectedDate = DateTime.Now;
                    dpFechaEnvio.SelectedDate = DateTime.Now;
                    if(((Usuario_LoginResult)Session["Usuario"]).aprobarDevolucionSol1 && Request.QueryString["idDevolucionSolicitud"] != "0")
                        btnAprobar.Visible = true;
                    DocVenta_Cargar(decimal.Parse(Request.QueryString["Op"]));

                    if(Request.QueryString["revisar"] != null)
                        if (bool.Parse(Request.QueryString["revisar"])) {
                            btnGuardar.Visible = false;
                            btnAprobar.Visible = false;
                        }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            long codigoCliente; 

            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            SolDevolucionWCFClient objSolDevolucionWCFC = new SolDevolucionWCFClient();

            try
            {
                if (string.IsNullOrEmpty(acbTransporte.Text) && acbTransporte.Text.Split('-').Count() < 2)
                {
                    acbTransporte.Focus();
                    throw new ArgumentException("No se ha seleccionado una empresa de transportes correctamente");
                }
                else
                {
                    try
                    {
                        codigoCliente =  Convert.ToInt64(acbTransporte.Text.Split('-')[0].ToString()) ;
                    }
                    catch
                    {
                        acbTransporte.Focus();
                        throw new ArgumentException("No se ha seleccionado una empresa de transportes correctamente");
                    }
                }


                if (DateTime.Compare(dpFechaEnvio.SelectedDate.Value, dpFechaVenta.SelectedDate.Value) <= 0)
                {
                    dpFechaEnvio.Focus();
                    throw new ArgumentException("La fecha de envio no puede ser antes que la fecha de venta");
                }

                if (DateTime.Compare(dpFechaDevolucion.SelectedDate.Value, dpFechaVenta.SelectedDate.Value) <= 0)
                {
                    dpFechaDevolucion.Focus();
                    throw new ArgumentException("La fecha de devolución no puede ser antes que la fecha de venta");
                }

                if (DateTime.Compare(dpFechaDevolucion.SelectedDate.Value, dpFechaEnvio.SelectedDate.Value) < 0)
                {
                    dpFechaDevolucion.Focus();
                    throw new ArgumentException("La fecha de devolución no puede ser antes que la fecha de envio");
                }

                //if (string.IsNullOrEmpty(txtNroGuiaCliente.Text))
                //{
                //    txtNroGuiaCliente.Focus();
                //    throw new ArgumentException("Se debe completar el número de la guía del cliente");
                //}

                //if (btnCobrarFlete.Checked && txtFlete.Value <= 0)
                //{
                //    txtFlete.Focus();
                //    throw new ArgumentException("El flete debe ser mayor a 0");
                //}

                //if (!btnCobrarFlete.Checked)
                //{
                //    txtFlete.Focus();
                //    txtFlete.Value = 0;
                //}

                objSolDevolucionWCFC.DevolucionSolicitud_Registrar(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, DevolucionSolicitud_Obtener(),
                    DevolucionSolicitudDetalle_Obtener().ToArray());

                Response.Redirect("~/Comercial/Devoluciones/frmSolDevolucionConsultar.aspx");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdDocVentaDetalle_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Item is GridDataItem)
                {
                    RadNumericTextBox txtPrecio = (RadNumericTextBox)e.Item.FindControl("txtPrecio");
                    txtPrecio.Attributes["href"] = "javascript:void(0);";

                    RadNumericTextBox txtCantidad = (RadNumericTextBox)e.Item.FindControl("txtCantidad");
                    txtCantidad.Attributes["href"] = "javascript:void(0);";

                    txtPrecio.Attributes["onkeyup"] = String.Format("return CalcularDevolucion('{0}');", e.Item.ItemIndex);
                    txtCantidad.Attributes["onkeyup"] = String.Format("return CalcularDevolucion('{0}');", e.Item.ItemIndex);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdDocVentaDetalle_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    List<gsDevolucionSolicitudDetalle_BuscarResult> lstProductos = new List<gsDevolucionSolicitudDetalle_BuscarResult>();
                    if(!string.IsNullOrEmpty((string)ViewState["lstProductos"]))
                        lstProductos = JsonHelper.JsonDeserialize<List<gsDevolucionSolicitudDetalle_BuscarResult>>((string)ViewState["lstProductos"]); ;
                    if (Request.QueryString["idDevolucionSolicitud"] != "0" && lstProductos.FindAll(x => e.Item.Cells[2].Text == x.ID_Amarre.ToString()).Count == 1)
                    {
                        Double importe = 0;
                        RadNumericTextBox txt = (RadNumericTextBox)item.FindControl("txtPrecio");
                        txt.Value = Convert.ToDouble(lstProductos.Find(x => e.Item.Cells[2].Text == x.ID_Amarre.ToString()).precioUnitario);
                        importe = (Double)txt.Value;
                        txt = (RadNumericTextBox)item.FindControl("txtCantidad");
                        txt.Value = Convert.ToDouble(lstProductos.Find(x => e.Item.Cells[2].Text == x.ID_Amarre.ToString()).cantidad);
                        importe = importe * (Double)txt.Value;
                        txt = (RadNumericTextBox)item.FindControl("txtImporte");
                        txt.Value = importe;
                    }
                    else {
                        if (Request.QueryString["idDevolucionSolicitud"] != "0")
                        {
                            RadNumericTextBox txt = (RadNumericTextBox)item.FindControl("txtPrecio");
                            txt.Value = 0;
                            txt = (RadNumericTextBox)item.FindControl("txtCantidad");
                            txt.Value = 0;
                            txt = (RadNumericTextBox)item.FindControl("txtImporte");
                            txt.Value = 0;
                        }
                        else {
                            RadNumericTextBox txt = (RadNumericTextBox)item.FindControl("txtPrecio");
                            txt.Value = Convert.ToDouble(e.Item.Cells[8].Text.Replace("$", string.Empty));
                            txt = (RadNumericTextBox)item.FindControl("txtCantidad");
                            //txt.Value = Convert.ToInt32(e.Item.Cells[6].Text.Replace("$", string.Empty));
                            txt.Value = 0;
                            txt = (RadNumericTextBox)item.FindControl("txtImporte");
                            //txt.Value = Convert.ToDouble(e.Item.Cells[9].Text.Replace("$", string.Empty));
                            txt.Value = 0;
                        }
                    }
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
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            SolDevolucionWCFClient objSolDevolucionWCFC = new SolDevolucionWCFClient();

            try
            {
                objSolDevolucionWCFC.DevolucionSolicitud_Aprobar(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, int.Parse(Request.QueryString["idDevolucionSolicitud"]));

                Response.Redirect("~/Comercial/Devoluciones/frmSolDevolucionConsultar.aspx");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}