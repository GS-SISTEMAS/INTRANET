using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.MonedaWCF;
using GS.SISGEGS.Web.EgresosWCF;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.DM;
using Telerik.Web.UI;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.CentroCostoWCF;
using GS.SISGEGS.Web.UnidadWCF;
using GS.SISGEGS.Web.NaturalezaGastoWCF;

namespace GS.SISGEGS.Web.Commercial.Gastos
{
    public partial class frmGastosMng : System.Web.UI.Page
    {
        private void NaturalezaGasto_Cargar()
        {
            NaturalezaGastoWCFClient objNaturalezaGastoWCF;
            VBG03096Result objNatGasto;
            List<VBG03096Result> lst;
            try
            {
                objNaturalezaGastoWCF = new NaturalezaGastoWCFClient();
                objNatGasto = new VBG03096Result();
                objNatGasto.CentroCostos = "Ninguna";
                lst = objNaturalezaGastoWCF.NaturalezaGasto_ListarImputables(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();
                lst.Insert(0, objNatGasto);

                cboNatGasto.DataSource = lst;
                cboNatGasto.DataTextField = "CentroCostos";
                cboNatGasto.DataValueField = "ID_NaturalezaGastoIngreso";
                cboNatGasto.DataBind();

                cboNatGasto.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UnidadProyecto_Cargar()
        {
            UnidadWCFClient objUnidadWCF;
            VBG02668Result objUnidProyecto;
            List<VBG02668Result> lst;
            try
            {
                objUnidadWCF = new UnidadWCFClient();
                objUnidProyecto = new VBG02668Result();
                objUnidProyecto.UnidadProyecto = "Ninguno";
                lst = objUnidadWCF.UnidadProyecto_ListarImputables(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();
                lst.Insert(0, objUnidProyecto);

                cboUnidProy.DataSource = lst;
                cboUnidProy.DataTextField = "UnidadProyecto";
                cboUnidProy.DataValueField = "ID_UnidadProyecto";
                cboUnidProy.DataBind();

                cboUnidProy.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UnidadGestion_Cargar() {
            UnidadWCFClient objUnidadWCF;
            VBG02665Result objUnidGestion;
            List<VBG02665Result> lstUnidGestion;
             
            try {
                objUnidadWCF = new UnidadWCFClient();
                objUnidGestion = new VBG02665Result();
                objUnidGestion.UnidadGestion = "Ninguno";

                lstUnidGestion = objUnidadWCF.UnidadGestion_ListarImputables(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();
                lstUnidGestion.Insert(0, objUnidGestion);

                cboUnidGestion.DataSource = lstUnidGestion;
                cboUnidGestion.DataTextField = "UnidadGestion";
                cboUnidGestion.DataValueField = "ID_UnidadGestion";
                cboUnidGestion.DataBind();

                cboUnidGestion.SelectedIndex = 0;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private void CentroCosto_Cargar() {
            CentroCostoWCFClient objCentroCostoWCF;
            VBG00786Result objCentroCosto;
            List<VBG00786Result> lstCentroCosto;
            try
            {
                objCentroCostoWCF = new CentroCostoWCFClient();
                objCentroCosto = new VBG00786Result();
                objCentroCosto.CentroCostos = "Ninguno";

                lstCentroCosto = objCentroCostoWCF.CentroCosto_ListarImputables(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();
                lstCentroCosto.Insert(0, objCentroCosto);

                cboCentroCosto.DataSource = lstCentroCosto;
                cboCentroCosto.DataValueField = "ID_CentroCostos";
                cboCentroCosto.DataTextField = "CentroCostos";
                cboCentroCosto.DataBind();

                cboCentroCosto.SelectedIndex = 0;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private gsEgresosVarios_BuscarCabeceraResult EgresosVarios_Obtener() {
            gsEgresosVarios_BuscarCabeceraResult objEVCabecera;
            try {
                objEVCabecera = JsonHelper.JsonDeserialize<gsEgresosVarios_BuscarCabeceraResult>((string)ViewState["objEVCabecera"]);
               
                objEVCabecera.Concepto = txtConcepto.Text;
                objEVCabecera.ID_Moneda = decimal.Parse(cboMoneda.SelectedValue);
                objEVCabecera.Fecha = dpFecRegistro.SelectedDate.Value;
                objEVCabecera.Vcmto = dpFecVencimiento.SelectedDate.Value;
                objEVCabecera.NroDocumento = txtNroDoc.Text;
                objEVCabecera.AgendaNombre = txtNombre.Text;
                objEVCabecera.Importe = JsonHelper.JsonDeserialize<List<gsEgresosVarios_BuscarDetalleResult>>((string)ViewState["lstEVDetalle"]).FindAll(x => x.Estado == 1).AsEnumerable().Sum(x => x.Importe);
                if (!string.IsNullOrEmpty(cboCentroCosto.SelectedValue))
                    objEVCabecera.ID_CCosto = cboCentroCosto.SelectedValue;
                if (!string.IsNullOrEmpty(cboUnidGestion.SelectedValue))
                    objEVCabecera.ID_UnidadGestion = cboUnidGestion.SelectedValue;
                if (!string.IsNullOrEmpty(cboUnidProy.SelectedValue))
                    objEVCabecera.ID_UnidadProyecto = cboUnidProy.SelectedValue;
                if (!string.IsNullOrEmpty(cboNatGasto.SelectedValue))
                    objEVCabecera.ID_NaturalezaGastoIngreso = cboNatGasto.SelectedValue;

                return objEVCabecera;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private void EgresosVarios_Cargar(int idOperacion) {
            EgresosWCFClient objEgresoWCF = new EgresosWCFClient();
            bool? bloqueado = null;
            string mensajeBloqueado = null;
            gsEgresosVarios_BuscarDetalleResult[] lstEgresosVarios_Detalle = null;
            gsEgresosVarios_BuscarCabeceraResult objEgresosVarios;
            try {
                if (idOperacion != 0)
                {
                    objEgresosVarios = objEgresoWCF.EgresosVarios_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idOperacion, ref bloqueado, ref mensajeBloqueado,
                    ref lstEgresosVarios_Detalle);
                    if ((bool)bloqueado)
                        throw new ArithmeticException(mensajeBloqueado);

                    lblIDAgenda.Text = objEgresosVarios.ID_Agenda;
                    txtNroDoc.Text = objEgresosVarios.NroDocumento;
                    txtNombre.Text = objEgresosVarios.AgendaNombre;
                    dpFecRegistro.SelectedDate = objEgresosVarios.Fecha;
                    dpFecVencimiento.SelectedDate = objEgresosVarios.Vcmto;
                    cboMoneda.SelectedValue = objEgresosVarios.ID_Moneda.ToString();
                    txtConcepto.Text = objEgresosVarios.Concepto;
                    txtSerie.Text = objEgresosVarios.Serie;
                    txtNumero.Text = objEgresosVarios.Numero.ToString();
                    cboCentroCosto.SelectedValue = objEgresosVarios.ID_CCosto;
                    cboUnidGestion.SelectedValue = objEgresosVarios.ID_UnidadGestion;
                    cboUnidProy.SelectedValue = objEgresosVarios.ID_UnidadProyecto;
                    cboNatGasto.SelectedValue = objEgresosVarios.ID_NaturalezaGastoIngreso;

                    ViewState["objEVCabecera"] = JsonHelper.JsonSerializer(objEgresosVarios);
                    ViewState["lstEVDetalle"] = JsonHelper.JsonSerializer(lstEgresosVarios_Detalle.ToList());

                    lblTotal.Text = "Total: " + lstEgresosVarios_Detalle.ToList().AsEnumerable().Sum(x => x.Importe).ToString();

                    grdRecibos.DataSource = lstEgresosVarios_Detalle.ToList().FindAll(x => x.Estado == 1); ;
                    grdRecibos.DataBind();
                }
                else {
                    txtNroDoc.Text = ((Usuario_LoginResult)Session["Usuario"]).nroDocumento;
                    txtNombre.Text = ((Usuario_LoginResult)Session["Usuario"]).nombres.ToUpper();

                    objEgresosVarios = new gsEgresosVarios_BuscarCabeceraResult();
                    objEgresosVarios.Op = idOperacion;
                    objEgresosVarios.ID_Agenda = ((Usuario_LoginResult)Session["Usuario"]).nroDocumento;
                    objEgresosVarios.AgendaNombre = ((Usuario_LoginResult)Session["Usuario"]).nombres;
                    ViewState["objEVCabecera"] = JsonHelper.JsonSerializer(objEgresosVarios);
                    ViewState["lstEVDetalle"] = JsonHelper.JsonSerializer(new List<gsEgresosVarios_BuscarDetalleResult>());

                    lblTotal.Text = "Total: 0.00";

                    grdRecibos.DataSource = lstEgresosVarios_Detalle;
                    grdRecibos.DataBind();
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private void Moneda_CargarComboBox() {
            MonedaWCFClient objMonedaWCF = new MonedaWCFClient();
            try {
                objMonedaWCF = new MonedaWCFClient();
                cboMoneda.DataSource = objMonedaWCF.Moneda_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, 
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario);
                cboMoneda.DataTextField = "Nombre";
                cboMoneda.DataValueField = "ID";
                cboMoneda.DataBind();

                cboMoneda.SelectedValue = "0";
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            if (!ConnectionHelpers.CheckForInternetConnection())
                throw new ArgumentException("ERROR: Revisar su conexión a internet.");

            try
            {
                if (!Page.IsPostBack)
                {
                    dpFecRegistro.SelectedDate = DateTime.Now;
                    dpFecVencimiento.SelectedDate = DateTime.Now;
                    Moneda_CargarComboBox();
                    CentroCosto_Cargar();
                    UnidadGestion_Cargar();
                    UnidadProyecto_Cargar();
                    NaturalezaGasto_Cargar();
                    EgresosVarios_Cargar(int.Parse((Request.QueryString["idOperacion"])));
                    if ((Request.QueryString["idOperacion"]) == "0")
                    {
                        Page.Title = "Registrar planilla de gasto";
                        lblMensaje.Text = "Listo para registrar el gasto.";
                    }
                    else
                    {
                        Page.Title = "Modificar planilla de gasto";
                        lblMensaje.Text = "Listo para modificar el gasto " + (Request.QueryString["idOperacion"]);
                    }

                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdRecibos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try {
                grdRecibos.DataSource = JsonHelper.JsonDeserialize<List<gsEgresosVarios_BuscarDetalleResult>>((string)ViewState["lstEVDetalle"]);
                //grdRecibos.DataBind();
            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdRecibos_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            if (!ConnectionHelpers.CheckForInternetConnection())
                throw new ArgumentException("ERROR: Revisar su conexión a internet.");

            try
            {
                List<gsEgresosVarios_BuscarDetalleResult> lst = JsonHelper.JsonDeserialize<List<gsEgresosVarios_BuscarDetalleResult>>((string)ViewState["lstEVDetalle"]);
                lst.Find(x => x.ID_Amarre == (decimal)((GridDataItem)e.Item).GetDataKeyValue("ID_Amarre")).Estado = 0;
                ViewState["lstEVDetalle"] = JsonHelper.JsonSerializer(lst);
                lblTotal.Text = "Total: " + lst.ToList().AsEnumerable().Sum(x => x.Importe).ToString();

                grdRecibos.DataSource = lst.FindAll(x => x.Estado == 1);
                grdRecibos.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdRecibos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (e.CommandName == "Editar")
                {
                    gsEgresosVarios_BuscarDetalleResult objEVDetalle = JsonHelper.JsonDeserialize<List<gsEgresosVarios_BuscarDetalleResult>>((string)ViewState["lstEVDetalle"]).Find(x => x.ID_Amarre.ToString() == e.CommandArgument.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowInsertForm(" + JsonHelper.JsonSerializer(objEVDetalle) + ");", true);
                }
            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void ramGastosMng_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            if (!ConnectionHelpers.CheckForInternetConnection())
                throw new ArgumentException("ERROR: Revisar su conexión a internet.");

            try
            {
                if (e.Argument == "Rebind")
                {
                    //grdItem.MasterTableView.SortExpressions.Clear();
                    //grdItem.MasterTableView.GroupByExpressions.Clear();
                    //grdItem.DataSource = (List<gsItem_BuscarResult>)Session["lstProductos"];
                    //grdItem.DataBind();
                    ////Calcular_Glosa();

                    //lblMensaje.Text = "Se agregó el producto al pedido.";
                    //lblMensaje.CssClass = "mensajeExito";

                    //acbProducto.Entries.Clear();
                    //acbProducto.Focus();
                }

                if (e.Argument.Split('(')[0].Trim() == "RebindAndNavigate")
                {
                    grdRecibos.MasterTableView.SortExpressions.Clear();
                    grdRecibos.MasterTableView.GroupByExpressions.Clear();
                    List<gsEgresosVarios_BuscarDetalleResult> lst = JsonHelper.JsonDeserialize<List<gsEgresosVarios_BuscarDetalleResult>>((string)ViewState["lstEVDetalle"]);
                    string strEVDetalle = "{" + e.Argument.Split('{')[1];
                    gsEgresosVarios_BuscarDetalleResult objEVDetalle = JsonHelper.JsonDeserialize<gsEgresosVarios_BuscarDetalleResult>(strEVDetalle.Substring(0, strEVDetalle.Length - 1));
                    if (objEVDetalle.ID_Amarre == 0)
                        objEVDetalle.ID_Amarre = (lst.FindAll(x => x.ID_Amarre <= 0).Count + 1) * -1;
                    lst.Remove(lst.Find(x => x.ID_Amarre == objEVDetalle.ID_Amarre));
                    lst.Add(objEVDetalle);

                    lblTotal.Text = "Total: " + lst.ToList().AsEnumerable().Sum(x => x.Importe).ToString();

                    grdRecibos.DataSource = lst.OrderBy(x => x.ID_Amarre);
                    grdRecibos.DataBind();

                    ViewState["lstEVDetalle"] = JsonHelper.JsonSerializer(lst);
                    if (objEVDetalle.ID_Amarre > 0)
                        lblMensaje.Text = "Se modificó el gastos con código " + objEVDetalle.ID_Amarre.ToString();
                    else
                        lblMensaje.Text = "Se registró el gastos con código " + objEVDetalle.ID_Amarre.ToString();
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            if (!ConnectionHelpers.CheckForInternetConnection())
                throw new ArgumentException("Revisar su conexión a internet.");

            try {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowInsertForm(0);", true);
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

            try
            {
                EgresosWCFClient objEgresoWCF = new EgresosWCFClient();
                if (cboMoneda.SelectedIndex < 0)
                    throw new ArgumentException("Se debe seleccionar un tipo de moneda.");

                if (txtConcepto.Text.Length <= 0)
                    throw new ArgumentException("Se debe especificar el concepto general de los gastos.");

                if (grdRecibos.Items.Count <= 0)
                    throw new ArgumentException("Se debe ingresar un documento de egreso.");

                objEgresoWCF.EgresosVarios_Registrar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, EgresosVarios_Obtener(),
                    JsonHelper.JsonDeserialize<List<gsEgresosVarios_BuscarDetalleResult>>((string)ViewState["lstEVDetalle"]).ToArray());

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind();", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}