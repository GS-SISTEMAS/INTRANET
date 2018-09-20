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

namespace GS.SISGEGS.Web.Comercial.Gastos
{
    public partial class frmGastosEdt : System.Web.UI.Page
    {
        private void Documento_ListarTipoCompra() {
            DocumentoWCFClient objDocumentoWCF;
            try {
                objDocumentoWCF = new DocumentoWCFClient();
                cboTipoDocumento.DataSource = objDocumentoWCF.Documento_ListarDocCompra(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario);
                cboTipoDocumento.DataTextField = "Nombre";
                cboTipoDocumento.DataValueField = "ID";
                cboTipoDocumento.DataBind();

                cboTipoDocumento.SelectedValue = "1";
            }
            catch (Exception ex) {
                throw ex;
            }
        }

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
            if (searchString.Length > 3)
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            if (!ConnectionHelpers.CheckForInternetConnection())
                throw new ArgumentException("ERROR: Revisar su conexión a internet.");

            AutoCompleteBoxEntry entry;

            try {
                if (!Page.IsPostBack) {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    dpFecEmision.SelectedDate = DateTime.Now;
                    txtIGV.Value = 0;
                    txtImpBase.Value = 0;
                    txtInafecto.Value = 0;
                    txtImporte.Value = 0;
                    TipoGasto_Cargar();
                    Documento_ListarTipoCompra();
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
                            //entry = new AutoCompleteBoxEntry();
                            //entry.Text = objEVDetalle.ID_Documento + "-" + objEVDetalle.NombreDocumento;
                            //acbTipoDocumento.Entries.Add(entry);
                            cboTipoDocumento.SelectedValue = objEVDetalle.ID_Documento.ToString();
                        }

                        txtSerie.Text = objEVDetalle.Serie;
                        txtNumero.Text = objEVDetalle.Numero.ToString();
                        txtImporte.Value = (double)objEVDetalle.Importe;
                        txtIGV.Value = (double)objEVDetalle.ImporteIGV;
                        txtImpBase.Value = (double)objEVDetalle.ImporteBaseIGV;
                        txtInafecto.Value = (double)objEVDetalle.ImporteInafecto;
                        if (objEVDetalle.FechaEmision != null)
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
            //string idAgenda = "";
            try {
                //if (!btnAgregar.Visible) {
                //    AgendaWCFClient objAgendaWCF = new AgendaWCFClient();
                //    if (txtNroRUC.Text.Length != 11)
                //        throw new ArgumentException("El número RUC ingresado no es correcto");
                //    if (string.IsNullOrEmpty(txtRazonSocial.Text))
                //        throw new ArgumentException("Se debe ingresar la razón social del proveedor.");
                //    idAgenda = objAgendaWCF.Agenda_RegistrarProveedor(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                //                ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, txtNroRUC.Text, txtRazonSocial.Text.ToUpper());
                //}

                objEVDetalle = new gsEgresosVarios_BuscarDetalleResult();
                if (Request.QueryString["objRecibo"] != "0")
                {
                    objEVDetalle = JsonHelper.JsonDeserialize<gsEgresosVarios_BuscarDetalleResult>(Request.QueryString["objRecibo"]);
                    objEVDetalle.Tipo = "M";
                }
                else
                {
                    objEVDetalle = new gsEgresosVarios_BuscarDetalleResult();
                    objEVDetalle.Tipo = "R";
                }
                    

                if (string.IsNullOrEmpty(txtImporte.Text) || txtImporte.Value <= 0)
                    throw new ArgumentException("Se debe ingresar un importe mayor 0 soles.");
                objEVDetalle.Importe = (decimal)txtImporte.Value;
                if (cboTipoGasto.SelectedIndex < 0)
                    throw new ArgumentException("Se debe seleccionar un tipo de gasto valido.");
                objEVDetalle.ID_Item = cboTipoGasto.SelectedValue;
                objEVDetalle.Item = cboTipoGasto.Text;
                //if (acbTipoDocumento.Entries.Count <= 0)
                //    throw new ArgumentException("Se debe ingresar un tipo de documento valido.");
                objEVDetalle.ID_Documento = decimal.Parse(cboTipoDocumento.SelectedValue);
                objEVDetalle.NombreDocumento = cboTipoDocumento.SelectedItem.Text;
                if (acbProveedor.Entries.Count <= 0 || !acbProveedor.Text.Contains("-"))
                    throw new ArgumentException("Se debe ingresar un proveedor valido.");
                objEVDetalle.ID_Agenda = acbProveedor.Text.Split('-')[0];
                objEVDetalle.Agenda = acbProveedor.Text.Split('-')[1];


                if (string.IsNullOrEmpty(txtSerie.Text))
                    throw new ArgumentException("Se debe ingresar una serie valida.");
                if (string.IsNullOrEmpty(txtNumero.Text))
                    throw new ArgumentException("Se debe ingresar un número valido.");

                if (txtNumero.Text == string.Empty)
                { throw new ArgumentException("Se debe ingresar un número valido."); }
                else
                {
                    if (txtSerie.Text == string.Empty)
                    { throw new ArgumentException("Se debe ingresar una serie valida."); }
                    else
                    {
                        objEVDetalle.Serie = txtSerie.Text.PadLeft(4,'0');
                        objEVDetalle.Numero = (decimal)txtNumero.Value;
                        objEVDetalle.Observaciones = txtComentario.Text;
                        objEVDetalle.FechaEmision = dpFecEmision.SelectedDate.Value;
                        objEVDetalle.ImporteBaseIGV = Convert.ToDecimal(txtImpBase.Value);
                        objEVDetalle.ImporteIGV = Convert.ToDecimal(txtIGV.Value);
                        objEVDetalle.ImporteInafecto = Convert.ToDecimal(txtInafecto.Value);
                        objEVDetalle.Estado = 1;
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + JsonHelper.JsonSerializer(objEVDetalle) + ");", true);

                    }
                }


            }
            catch (Exception ex) {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void cboTipoDocumento_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (cboTipoDocumento.SelectedValue == "1" || cboTipoDocumento.SelectedValue == "12")
                {
                    txtImpBase.Value = txtImporte.Value / 1.18;
                    txtIGV.Value = txtImpBase.Value * 0.18;
                }
                else
                {
                    txtImpBase.Value = 0;
                    txtIGV.Value = 0;
                }
                txtInafecto.Value = txtImporte.Value - txtImpBase.Value - txtIGV.Value;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}