using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.DM;
using System.Web.Services;
using Telerik.Web.UI;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.DocumentoWCF;
using GS.SISGEGS.Web.ItemWCF;

namespace GS.SISGEGS.Web.Commercial.Gastos
{
    public partial class frmGastosEdt : System.Web.UI.Page
    {
        private void TipoGasto_Cargar() {
            try {
                ItemWCFClient objItemWCF = new ItemWCFClient();
                cboTipoGasto.DataSource = objItemWCF.Item_ListarTipoGastoCC(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario);
                cboTipoGasto.DataTextField = "Nombre";
                cboTipoGasto.DataValueField = "Codigo";
                cboTipoGasto.DataBind();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        [WebMethod]
        public static AutoCompleteBoxData Agenda_ListarProveedor(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarProveedorResult[] lst = objAgendaWCFClient.Agenda_ListarProveedor(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsAgenda_ListarProveedorResult agenda in lst)
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
        public static AutoCompleteBoxData Documento_ListarTipoCompra(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                DocumentoWCFClient objDocumentoWCF = new DocumentoWCFClient();
                gsDocumento_ListarTipoCompraResult[] lst = objDocumentoWCF.Documento_ListarDocCompra(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa, 
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsDocumento_ListarTipoCompraResult agenda in lst)
                {
                    AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                    childNode.Text = agenda.ID.ToString() + "-" + agenda.Nombre;
                    childNode.Value = agenda.ID.ToString();
                    result.Add(childNode);
                }
                res.Items = result.ToArray();
            }
            return res;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            if (!ConnectionHelpers.CheckForInternetConnection())
                throw new ArgumentException("ERROR: Revisar su conexión a internet.");

            AutoCompleteBoxEntry entry;

            try {
                if (!Page.IsPostBack) {
                    dpFecEmision.SelectedDate = DateTime.Now;
                    TipoGasto_Cargar();
                    if (Request.QueryString["objRecibo"] != "0")
                    {
                        Title = "Modificar documnento";

                        gsEgresosVarios_BuscarDetalleResult objEVDetalle = JsonHelper.JsonDeserialize<gsEgresosVarios_BuscarDetalleResult>(Request.QueryString["objRecibo"]);

                        txtComentario.Text = objEVDetalle.Observaciones;

                        if (!string.IsNullOrEmpty(objEVDetalle.ID_Agenda))
                        {
                            entry = new AutoCompleteBoxEntry();
                            entry.Text = objEVDetalle.ID_Agenda + "-" + objEVDetalle.Agenda;
                            acbProveedor.Entries.Add(entry);
                        }

                        if (!string.IsNullOrEmpty(objEVDetalle.ID_Item))
                        {
                            cboTipoGasto.SelectedValue = objEVDetalle.ID_Item;
                        }

                        if (!string.IsNullOrEmpty(objEVDetalle.ID_Documento.ToString()))
                        {
                            entry = new AutoCompleteBoxEntry();
                            entry.Text = objEVDetalle.ID_Documento + "-" + objEVDetalle.NombreDocumento;
                            acbTipoDocumento.Entries.Add(entry);
                        }

                        txtSerie.Text = objEVDetalle.Serie;
                        txtNumero.Text = objEVDetalle.Numero.ToString();
                        txtImporte.Value = (double)objEVDetalle.Importe;
                        if(objEVDetalle.FechaEmision != null)
                            dpFecEmision.SelectedDate = objEVDetalle.FechaEmision;

                        lblMensaje.Text = "Datos del gasto " + objEVDetalle.ID_Amarre.ToString() + " cargados con éxito";
                    }
                    else {
                        Title = "Registrar documento";
                        lblMensaje.Text = "Datos iniciales cargados con éxito";
                    }

                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex) {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            gsEgresosVarios_BuscarDetalleResult objEVDetalle;
            string idAgenda = "";
            try {
                if (!btnAgregar.Visible) {
                    AgendaWCFClient objAgendaWCF = new AgendaWCFClient();
                    if (txtNroRUC.Text.Length != 11)
                        throw new ArgumentException("El número RUC ingresado no es correcto");
                    if (string.IsNullOrEmpty(txtRazonSocial.Text))
                        throw new ArgumentException("Se debe ingresar la razón social del proveedor.");
                    idAgenda = objAgendaWCF.Agenda_RegistrarProveedor(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                                ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, txtNroRUC.Text, txtRazonSocial.Text.ToUpper());
                }

                objEVDetalle = new gsEgresosVarios_BuscarDetalleResult();
                if (Request.QueryString["objRecibo"] != "0")
                    objEVDetalle = JsonHelper.JsonDeserialize<gsEgresosVarios_BuscarDetalleResult>(Request.QueryString["objRecibo"]);
                else 
                    objEVDetalle = new gsEgresosVarios_BuscarDetalleResult();

                if (string.IsNullOrEmpty(txtImporte.Text) || txtImporte.Value <= 0)
                    throw new ArgumentException("Se debe ingresar un importe mayor 0 soles.");
                objEVDetalle.Importe = (decimal)txtImporte.Value;
                if (cboTipoGasto.SelectedIndex < 0)
                    throw new ArgumentException("Se debe seleccionar un tipo de gasto valido.");
                objEVDetalle.ID_Item = cboTipoGasto.SelectedValue;
                objEVDetalle.Item = cboTipoGasto.Text;
                if (acbTipoDocumento.Entries.Count <= 0)
                    throw new ArgumentException("Se debe ingresar un tipo de documento valido.");
                objEVDetalle.ID_Documento = decimal.Parse(acbTipoDocumento.Text.Split('-')[0]);
                objEVDetalle.NombreDocumento = acbTipoDocumento.Text.Split('-')[1];
                if (acbProveedor.Entries.Count <= 0 && btnAgregar.Visible)
                    throw new ArgumentException("Se debe ingresar un proveedor valido.");
                if (btnAgregar.Visible)
                {
                    objEVDetalle.ID_Agenda = acbProveedor.Text.Split('-')[0];
                    objEVDetalle.Agenda = acbProveedor.Text.Split('-')[1];
                }
                else {
                    objEVDetalle.ID_Agenda = idAgenda;
                    objEVDetalle.Agenda = txtRazonSocial.Text;
                }
                if (string.IsNullOrEmpty(txtSerie.Text))
                    throw new ArgumentException("Se debe ingresar una serie valida.");
                objEVDetalle.Serie = txtSerie.Text;
                if (string.IsNullOrEmpty(txtNumero.Text))
                    throw new ArgumentException("Se debe ingresar un número valido.");
                objEVDetalle.Numero = (decimal)txtNumero.Value;
                objEVDetalle.Observaciones = txtComentario.Text;
                objEVDetalle.FechaEmision = dpFecEmision.SelectedDate.Value;
                objEVDetalle.ImporteBaseIGV = Convert.ToDecimal(txtImpBase.Value);
                objEVDetalle.ImporteIGV = Convert.ToDecimal(txtIGV.Value);
                objEVDetalle.ImporteInafecto = Convert.ToDecimal(txtInafecto.Value);
                objEVDetalle.Estado = 1;

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + JsonHelper.JsonSerializer(objEVDetalle) + ");", true);
            }
            catch (Exception ex) {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                acbProveedor.Visible = false;
                btnAgregar.Visible = false;
                txtNroRUC.Visible = true;
                txtRazonSocial.Visible = true;
                btnCancelar.Visible = true;

                acbProveedor.Entries.Clear();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                acbProveedor.Visible = true;
                btnAgregar.Visible = true;
                txtNroRUC.Visible = false;
                txtRazonSocial.Visible = false;
                btnCancelar.Visible = false;

                txtNroRUC.Text = "";
                txtRazonSocial.Text = "";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}