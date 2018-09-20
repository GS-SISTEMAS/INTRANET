﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using GS.SISGEGS.Web.MonedaWCF;
using GS.SISGEGS.Web.EgresosWCF;
using GS.SISGEGS.Web.VarianteWCF;
using GS.SISGEGS.DM;
using Telerik.Web.UI;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.CentroCostoWCF;
using GS.SISGEGS.Web.UnidadWCF;
using GS.SISGEGS.Web.NaturalezaGastoWCF;
using GS.SISGEGS.Web.LoginWCF;
using iTextSharp.text;
using iTextSharp.text.pdf;
using GS.SISGEGS.Web.EmpresaWCF;

namespace GS.SISGEGS.Web.Comercial.Gastos
{
    public partial class frmGastosMng : System.Web.UI.Page
    {
        #region Metodos privados
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

        private void UnidadGestion_Cargar()
        {
            UnidadWCFClient objUnidadWCF;
            VBG02665Result objUnidGestion;
            List<VBG02665Result> lstUnidGestion;

            try
            {
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CentroCosto_Cargar()
        {
            CentroCostoWCFClient objCentroCostoWCF;
            gsBuscarCentroCosto_IntranetResult objCentroCosto;
            List<gsBuscarCentroCosto_IntranetResult> lstCentroCosto;
            try
            {
                objCentroCostoWCF = new CentroCostoWCFClient();
                objCentroCosto = new gsBuscarCentroCosto_IntranetResult();
                objCentroCosto.CentroCostos = "Ninguno";

                lstCentroCosto = objCentroCostoWCF.BuscarCentroCosto_Intranet(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, false).ToList();
                lstCentroCosto.Insert(0, objCentroCosto);

                cboCentroCosto.DataSource = lstCentroCosto;
                cboCentroCosto.DataValueField = "ID_CentroCostos";
                cboCentroCosto.DataTextField = "CentroCostos";
                cboCentroCosto.DataBind();

                cboCentroCosto.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private gsEgresosVarios_BuscarCabeceraResult EgresosVarios_Obtener()
        {
            gsEgresosVarios_BuscarCabeceraResult objEVCabecera;
            try
            {
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void EgresosVarios_Cargar(int idOperacion)
        {
            EgresosWCFClient objEgresoWCF = new EgresosWCFClient();
            bool? bloqueado = null;
            string mensajeBloqueado = null;
            gsEgresosVarios_BuscarDetalleResult[] lstEgresosVarios_Detalle = null;
            gsEgresosVarios_BuscarCabeceraResult objEgresosVarios;
            try
            {
                if (idOperacion != 0)
                {
                    objEgresosVarios = objEgresoWCF.EgresosVarios_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idOperacion, ref bloqueado, ref mensajeBloqueado,
                    ref lstEgresosVarios_Detalle);

                    lblIDAgenda.Text = objEgresosVarios.ID_Agenda;
                    txtNroDoc.Text = objEgresosVarios.NroDocumento;
                    txtNombre.Text = objEgresosVarios.AgendaNombre;
                    dpFecRegistro.SelectedDate = objEgresosVarios.Fecha;
                    dpFechaInicio.SelectedDate = objEgresosVarios.FechaInicio;
                    dpFecVencimiento.SelectedDate = objEgresosVarios.Vcmto;
                    cboMoneda.SelectedValue = objEgresosVarios.ID_Moneda.ToString();
                    txtConcepto.Text = objEgresosVarios.Concepto;
                    txtSerie.Text = objEgresosVarios.Serie;
                    txtNumero.Text = objEgresosVarios.Numero.ToString();
                    cboCentroCosto.SelectedValue = objEgresosVarios.ID_CCosto;
                    cboUnidGestion.SelectedValue = objEgresosVarios.ID_UnidadGestion;
                    cboUnidProy.SelectedValue = objEgresosVarios.ID_UnidadProyecto;
                    cboNatGasto.SelectedValue = objEgresosVarios.ID_NaturalezaGastoIngreso;
                    ViewState["ID_Doc"] = objEgresosVarios.ID_Doc;
                    if (!((Usuario_LoginResult)Session["Usuario"]).aprobarPlanilla1  && !((Usuario_LoginResult)Session["Usuario"]).aprobarPlanilla0)
                    {
                        if (objEgresosVarios.Ok0 || objEgresosVarios.Ok1)
                            btnGuardar.Enabled = false;
                    }
                    else
                    {
                        if(((Usuario_LoginResult)Session["Usuario"]).aprobarPlanilla0 && objEgresosVarios.Ok1)
                            btnGuardar.Enabled = false;
                    }
                            

                    btnAprobJI.Enabled = !objEgresosVarios.Ok1;
                    if (objEgresosVarios.Ok0)
                    {
                        btnAprobJI.Text = "Desaprob.J.I.";
                        btnAprobJI.Icon.PrimaryIconUrl = "~/Images/Icons/sign-error-16.png";
                    }

                    if (objEgresosVarios.Ok1)
                    {
                        btnAprobConta.Text = "Desaprob.Conta.";
                        btnAprobConta.Icon.PrimaryIconUrl = "~/Images/Icons/sign-error-16.png";
                    }

                    ViewState["objEVCabecera"] = JsonHelper.JsonSerializer(objEgresosVarios);
                    ViewState["lstEVDetalle"] = JsonHelper.JsonSerializer(lstEgresosVarios_Detalle.ToList());

                    lblTotal.Text = "Total: " + lstEgresosVarios_Detalle.ToList().AsEnumerable().Sum(x => x.Importe).ToString();

                    grdRecibos.DataSource = lstEgresosVarios_Detalle.ToList().FindAll(x => x.Estado == 1); ;
                    grdRecibos.DataBind();

                    if ((bool)bloqueado)
                    {
                        lblMensaje.Text = mensajeBloqueado;
                        lblMensaje.CssClass = "mensajeError";
                        btnGuardar.Enabled = false;
                    }
                    else {
                        lblMensaje.Text = "Listo para modificar el gasto " + (Request.QueryString["idOperacion"]);
                        lblMensaje.CssClass = "mensajeExito";
                    }
                }
                else
                {
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Moneda_CargarComboBox()
        {
            MonedaWCFClient objMonedaWCF = new MonedaWCFClient();
            try
            {
                objMonedaWCF = new MonedaWCFClient();
                cboMoneda.DataSource = objMonedaWCF.Moneda_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario);
                cboMoneda.DataTextField = "Nombre";
                cboMoneda.DataValueField = "ID";
                cboMoneda.DataBind();

                cboMoneda.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Metodos Protegidos
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
                    LoginWCFClient objLoginWCF = new LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idUsuario);

                    dpFecRegistro.SelectedDate = DateTime.Now;
                    dpFechaInicio.SelectedDate = DateTime.Now;
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

                        VarianteWCFClient objVarianteWCF = new VarianteWCFClient();
                        EgresosWCFClient objEgresoWCF = new EgresosWCFClient();

                        Variante_BuscarResult objVariante = objVarianteWCF.Variante_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, "GASMNG");
                        gsEgresosVariosUsuario_BuscarResult objEVUsuario = objEgresoWCF.EgresosVariosUsuario_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                            ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, ((Usuario_LoginResult)Session["Usuario"]).nroDocumento);
                        if (objEVUsuario != null)
                        {
                            cboCentroCosto.SelectedValue = objEVUsuario.Id_CentroCosto;
                            cboUnidGestion.SelectedValue = objEVUsuario.Id_UnidadGestion;
                            cboUnidProy.SelectedValue = objEVUsuario.Id_UnidadProyecto;
                        }

                        cboMoneda.SelectedValue = objVariante.parametro1;
                        lblMensaje.CssClass = "mensajeExito";
                    }
                    else
                    {
                        Page.Title = "Modificar planilla de gasto";
                        if (((Usuario_LoginResult)Session["Usuario"]).aprobarPlanilla0) 
                            btnAprobJI.Visible = true;

                        if (((Usuario_LoginResult)Session["Usuario"]).aprobarPlanilla1)
                            btnAprobConta.Visible = true;
                    }
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

            try
            {
                grdRecibos.DataSource = JsonHelper.JsonDeserialize<List<gsEgresosVarios_BuscarDetalleResult>>((string)ViewState["lstEVDetalle"]);
                //grdRecibos.DataBind();
            }
            catch (Exception ex)
            {
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
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowInsertForm(" + JsonHelper.JsonSerializer(objEVDetalle).Replace('&','Y') + ");", true);
                }
            }
            catch (Exception ex)
            {
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
            catch (Exception ex)
            {
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

            try
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowInsertForm(0);", true);
            }
            catch (Exception ex)
            {
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

                if (cboCentroCosto.SelectedIndex <= 0)
                {
                    cboCentroCosto.Focus();
                    throw new ArgumentException("Se debe seleccionar un centro de costo.");
                }

                if (cboUnidProy.SelectedIndex <= 0)
                {
                    cboUnidProy.Focus();
                    throw new ArgumentException("Se debe seleccionar una unidad de proyecto.");
                }

                if (cboNatGasto.SelectedIndex <= 0)
                {
                    cboNatGasto.Focus();
                    throw new ArgumentException("Se debe seleccionar la naturaleza del gasto.");
                }

                if (cboUnidGestion.SelectedIndex <= 0)
                {
                    cboUnidGestion.Focus();
                    throw new ArgumentException("Se debe seleccionar una Unidad de gestión.");
                }

                if (DateTime.Compare(dpFechaInicio.SelectedDate.Value, dpFecVencimiento.SelectedDate.Value) > 0)
                    throw new ArithmeticException("La fecha de inicio no debe ser mayor a la fecha de final.");

                List<gsEgresosVarios_BuscarDetalleResult> lst = JsonHelper.JsonDeserialize<List<gsEgresosVarios_BuscarDetalleResult>>((string)ViewState["lstEVDetalle"]);
                if (lst.FindAll(x => DateTime.Compare(dpFecVencimiento.SelectedDate.Value, (DateTime)x.FechaEmision) < 0 || DateTime.Compare(dpFechaInicio.SelectedDate.Value, (DateTime)x.FechaEmision) > 0).Count > 0)
                    throw new ArgumentException("Existen documentos con fechas de emisión mayores a la fecha de registro.");

                objEgresoWCF.EgresosVarios_Registrar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                             ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, EgresosVarios_Obtener(), lst.ToArray(), dpFechaInicio.SelectedDate.Value);

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind();", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnAprobJI_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            EgresosWCFClient objEgresosWCF = new EgresosWCFClient();
            try
            {
                objEgresosWCF.EgresosVariosAprobar_Registrar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, int.Parse((Request.QueryString["idOperacion"])));

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind();", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnAprobConta_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            EgresosWCFClient objEgresosWCF = new EgresosWCFClient();
            try
            {
                objEgresosWCF.EgresosVarios_Aprobar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, (decimal)ViewState["ID_Doc"]);

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind();", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnPDFDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                string Op =  Request.QueryString["idOperacion"].ToString() ; 

                ShowPdf(CreatePDF(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, DateTime.Now.ToShortDateString(), Op), Op ); 

            }
            catch
            {
                lblMensaje.Text = "Error";
            }
        }

        private byte[] CreatePDF(int idEmpresa, string fechaHasta, string Op)
        {
            Document doc = new Document(PageSize.LETTER, 50, 50, 50, 50);

            using (MemoryStream output = new MemoryStream())
            {
                PdfPTable tableLayout = new PdfPTable(11);

                doc = new Document();
                doc = new Document(PageSize.LETTER, 20, 20, 20, 20);
                MemoryStream memoryStream = new MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(doc, output);
                doc.Open();
                doc.Add(Add_Content_To_PDF(tableLayout, fechaHasta, idEmpresa, Op));

                writer.CloseStream = false;
                doc.Close();
                memoryStream.Position = 0;

                return output.ToArray();
            }

        }

        private void ShowPdf(byte[] strS, string Op)
        {

            try
            {
                string fecha = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString(); 
                string fileName = "Planilla_OP" + Op + "_F" + fecha +  ".pdf";
                //if (!File.Exists(rutaArchivo)) return;

                var file = strS;
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ClearHeaders();
                System.Web.HttpContext.Current.Response.ContentType = "application/file";
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");
                System.Web.HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString(System.Globalization.CultureInfo.InvariantCulture));
                System.Web.HttpContext.Current.Response.OutputStream.Write(file, 0, file.Length);

                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                Response.BufferOutput = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest(); //
                Response.Close();

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }


        }

        private PdfPTable Add_Content_To_PDF(PdfPTable tableLayout, string fechaHasta, int idEmpresa, string Op)
        {
            string urlImagen;
            decimal OpInt = 0;
            OpInt = Convert.ToDecimal(Op);

            EmpresaWCFClient objEmpresaWCF = new EmpresaWCFClient();

            Empresa_BuscarDetalleResult objEmpresa;
            Empresa_BuscarDetalleResult[] lst = objEmpresaWCF.Empresa_BuscarDetalle(idEmpresa);
            objEmpresa = lst[0];
            urlImagen = objEmpresa.logotipo.ToString();

            float[] values = new float[11];
            values[0] = 120;
            values[1] = 100;
            values[2] = 100;
            values[3] = 100;
            values[4] = 100;
            values[5] = 100;
            values[6] = 100;
            values[7] = 100;
            values[8] = 100;
            values[9] = 100;
            values[10] = 100;

            tableLayout.SetWidths(values);        //Set the pdf headers
            tableLayout.WidthPercentage = 100;       //Set the PDF File witdh percentage

            //Add Title to the PDF file at the top
            //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath(urlImagen));

            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/Logos/grupo.png"));

            logo.ScaleAbsolute(205, 90);
            PdfPCell imageCell = new PdfPCell(logo);
            imageCell.Colspan = 2; // either 1 if you need to insert one cell
            imageCell.Border = 0;
            imageCell.HorizontalAlignment = Element.ALIGN_LEFT;

            tableLayout = EgresosVarios_PDF(tableLayout, Convert.ToInt32(OpInt), objEmpresa.razonSocial);

            return tableLayout;
        }

        private PdfPTable EgresosVarios_PDF(PdfPTable PDFCreator, int idOperacion, string NombreEmpresa)
        {
            PdfPTable tableLayout;
            tableLayout = PDFCreator;
            EgresosWCFClient objEgresoWCF = new EgresosWCFClient();
            bool? bloqueado = null;
            string mensajeBloqueado = null;
            gsEgresosVarios_BuscarDetalleResult[] lstEgresosVarios_Detalle = null;
            gsEgresosVarios_BuscarDetalleResult[] lstEgresosVarios_DetalleGasto = null;

            gsEgresosVarios_BuscarCabeceraResult objEgresosVarios;
            try
            {
                if (idOperacion != 0)
                {
                    objEgresosVarios = objEgresoWCF.EgresosVarios_Buscar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idOperacion, ref bloqueado, ref mensajeBloqueado,
                    ref lstEgresosVarios_Detalle);

                    //tableLayout.AddCell(imageCell);
                    tableLayout.AddCell(new PdfPCell(new Phrase(NombreEmpresa, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 4, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_LEFT });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 4, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableLayout.AddCell(new PdfPCell(new Phrase(DateTime.Now.ToShortDateString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 5, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 2, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableLayout.AddCell(new PdfPCell(new Phrase(DateTime.Now.ToShortTimeString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 5, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 1, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Egreso de Caja", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, 1, iTextSharp.text.BaseColor.DARK_GRAY))) { Colspan = 11, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 11, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 11, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 11, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 11, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                    //Add Cliente
                    int cont = 0;

                    tableLayout.AddCell(new PdfPCell(new Phrase("Fecha : ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase(objEgresosVarios.Fecha.ToShortDateString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 9, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Documento : ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase(objEgresosVarios.NombreDocumento, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Egreso Nro : ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase(objEgresosVarios.Serie + " - " + objEgresosVarios.Numero + " (OP:" + objEgresosVarios.Op + ")", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Moneda : ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                    if (objEgresosVarios.ID_Moneda == 0)
                    {
                        tableLayout.AddCell(new PdfPCell(new Phrase("Dolares", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    }
                    else
                    {
                        tableLayout.AddCell(new PdfPCell(new Phrase("Soles", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    }
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 9, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });


                    tableLayout.AddCell(new PdfPCell(new Phrase("Orden de : ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase(objEgresosVarios.AgendaNombre, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });


                    tableLayout.AddCell(new PdfPCell(new Phrase("Codigo : ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase(objEgresosVarios.ID_Agenda, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 9, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Concepto : ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase(objEgresosVarios.Concepto, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 11, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 11, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });


                    PdfPCell cellGastos = new PdfPCell(new Phrase("  Gastos", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 6, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, Border = 1, BorderColor = iTextSharp.text.BaseColor.BLACK, BackgroundColor = iTextSharp.text.BaseColor.WHITE };
                    cellGastos.Border = Rectangle.BOTTOM_BORDER; // | Rectangle.TOP_BORDER;
                    cellGastos.BorderWidthBottom = 2f;
                    cellGastos.BorderWidthTop = 0f;
                    cellGastos.PaddingBottom = 3f;
                    cellGastos.PaddingLeft = 0f;
                    cellGastos.PaddingTop = 0f;
                    tableLayout.AddCell(cellGastos);

                    PdfPCell cellImporte = new PdfPCell(new Phrase("  Importe  ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 1, BorderColor = iTextSharp.text.BaseColor.BLACK, BackgroundColor = iTextSharp.text.BaseColor.WHITE };
                    cellImporte.Border = Rectangle.BOTTOM_BORDER; // | Rectangle.TOP_BORDER;
                    cellImporte.BorderWidthBottom = 2f;
                    cellImporte.BorderWidthTop = 0f;
                    cellImporte.PaddingBottom = 3f;
                    cellImporte.PaddingLeft = 0f;
                    cellImporte.PaddingTop = 0f;
                    tableLayout.AddCell(cellImporte);

                    tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 11, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                    lstEgresosVarios_Detalle.OrderByDescending(x => x.ID_Item);

                    var ListaGasto = lstEgresosVarios_Detalle.Select(x => new { x.ID_Item, x.Item }).Distinct().ToList();


                    decimal Total = 0; 
                    foreach (var lineaGasto in ListaGasto)
                    {
                        tableLayout.AddCell(new PdfPCell(new Phrase(lineaGasto.ID_Item.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                        tableLayout.AddCell(new PdfPCell(new Phrase(lineaGasto.Item.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 8, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });

                        decimal sumaTotal = 0; 
                        foreach (gsEgresosVarios_BuscarDetalleResult lineaDetalle in lstEgresosVarios_Detalle)
                        {
                            if(lineaDetalle.ID_Item == lineaGasto.ID_Item)
                            {
                                tableLayout.AddCell(new PdfPCell(new Phrase(lineaDetalle.ID_Agenda, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 0, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                                tableLayout.AddCell(new PdfPCell(new Phrase(lineaDetalle.Agenda, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 0, iTextSharp.text.BaseColor.BLACK))) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                                tableLayout.AddCell(new PdfPCell(new Phrase(lineaDetalle.NombreDocumento, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 0, iTextSharp.text.BaseColor.BLACK))) { Colspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                                tableLayout.AddCell(new PdfPCell(new Phrase(lineaDetalle.Serie + "-" + lineaDetalle.Numero, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 0, iTextSharp.text.BaseColor.BLACK))) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                                tableLayout.AddCell(new PdfPCell(new Phrase(decimal.Round(lineaDetalle.Importe, 2).ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 0, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                                sumaTotal = sumaTotal + decimal.Round(lineaDetalle.Importe, 2);
                                Total = Total + decimal.Round(lineaDetalle.Importe, 2);
                            }
                        }

                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, 0, iTextSharp.text.BaseColor.BLACK))) { Colspan = 9, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
 
                        cellImporte = new PdfPCell(new Phrase("S/.", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 1, BorderColor = iTextSharp.text.BaseColor.BLACK, BackgroundColor = iTextSharp.text.BaseColor.WHITE };
                        cellImporte.Border = Rectangle.BOTTOM_BORDER; // | Rectangle.TOP_BORDER;
                        cellImporte.BorderWidthBottom = 0f;
                        cellImporte.BorderWidthTop = 2f;
                        cellImporte.PaddingBottom = 3f;
                        cellImporte.PaddingLeft = 0f;
                        cellImporte.PaddingTop = 0f;
                        tableLayout.AddCell(cellImporte);

                        cellImporte = new PdfPCell(new Phrase(decimal.Round(sumaTotal, 2).ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 1, BorderColor = iTextSharp.text.BaseColor.BLACK, BackgroundColor = iTextSharp.text.BaseColor.WHITE };
                        cellImporte.Border = Rectangle.BOTTOM_BORDER; // | Rectangle.TOP_BORDER;
                        cellImporte.BorderWidthBottom = 0f;
                        cellImporte.BorderWidthTop = 2f;
                        cellImporte.PaddingBottom = 3f;
                        cellImporte.PaddingLeft = 0f;
                        cellImporte.PaddingTop = 0f;
                        tableLayout.AddCell(cellImporte);

                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 11, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
                    }
                    cellImporte = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 9, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 1, BorderColor = iTextSharp.text.BaseColor.BLACK, BackgroundColor = iTextSharp.text.BaseColor.WHITE };
                    cellImporte.Border = Rectangle.BOTTOM_BORDER; // | Rectangle.TOP_BORDER;
                    cellImporte.BorderWidthBottom = 0f;
                    cellImporte.BorderWidthTop = 2f;
                    cellImporte.PaddingBottom = 3f;
                    cellImporte.PaddingLeft = 0f;
                    cellImporte.PaddingTop = 0f;
                    tableLayout.AddCell(cellImporte);

                    cellImporte = new PdfPCell(new Phrase("S/.", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 1, BorderColor = iTextSharp.text.BaseColor.BLACK, BackgroundColor = iTextSharp.text.BaseColor.WHITE };
                    cellImporte.Border = Rectangle.BOTTOM_BORDER; // | Rectangle.TOP_BORDER;
                    cellImporte.BorderWidthBottom = 0f;
                    cellImporte.BorderWidthTop = 2f;
                    cellImporte.PaddingBottom = 3f;
                    cellImporte.PaddingLeft = 0f;
                    cellImporte.PaddingTop = 0f;
                    tableLayout.AddCell(cellImporte);

                    cellImporte = new PdfPCell(new Phrase(decimal.Round(Total, 2).ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, 1, iTextSharp.text.BaseColor.BLACK))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 1, BorderColor = iTextSharp.text.BaseColor.BLACK, BackgroundColor = iTextSharp.text.BaseColor.WHITE };
                    cellImporte.Border = Rectangle.BOTTOM_BORDER; // | Rectangle.TOP_BORDER;
                    cellImporte.BorderWidthBottom = 0f;
                    cellImporte.BorderWidthTop = 2f;
                    cellImporte.PaddingBottom = 3f;
                    cellImporte.PaddingLeft = 0f;
                    cellImporte.PaddingTop = 0f;
                    tableLayout.AddCell(cellImporte);



                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tableLayout;
        }

        #endregion
    }
}