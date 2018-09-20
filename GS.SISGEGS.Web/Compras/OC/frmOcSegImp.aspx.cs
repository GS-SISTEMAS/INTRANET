using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using GS.SISGEGS.BE;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.DocumentoWCF;
using GS.SISGEGS.Web.FormaPagoWCF;
using GS.SISGEGS.Web.OrdenVentaWCF;
using GS.SISGEGS.Web.LoginWCF;
using GS.SISGEGS.Web.OrdenCompraWCF;

namespace GS.SISGEGS.Web.Compras.OC
{
    public partial class frmOcSegImp : System.Web.UI.Page
    {
        OrdenCompraWCFClient objOrdenCompraWCF = new OrdenCompraWCFClient();
        int _idSeguimiento = 0;
        #region METODOSWEB
        [WebMethod]
        public static AutoCompleteBoxData Agenda_BuscarAgente(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarClienteAgenteResult[] lst = objAgendaWCFClient.Agenda_ListarClienteAgente(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString, 1);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsAgenda_ListarClienteAgenteResult agenda in lst)
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


        #region PROCEDIMIENTOS
        private void CargarParcialesPendientes()
        {
            objOrdenCompraWCF = new OrdenCompraWCFClient();
            List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult> lst = new List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult>();
            lst = objOrdenCompraWCF.Seleccionar_GenesysOC_ImpParciales(
                ((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                Convert.ToDateTime(dtpfechainicial.SelectedDate), Convert.ToDateTime(dtpfechafinal.SelectedDate),txtproveedor.Text.Trim(),
                0).ToList();

            gvwocparcial.DataSource = lst;
            gvwocparcial.DataBind();
            Session["lstocparcial"] = JsonHelper.JsonSerializer(lst);

            
        }
        private void CargarParcialesConSeguimiento()
        {
            objOrdenCompraWCF = new OrdenCompraWCFClient();
            List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult> lst = new List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult>();
            lst = objOrdenCompraWCF.Seleccionar_GenesysOC_ImpParciales(
                ((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                Convert.ToDateTime(dtpfechainicial.SelectedDate), Convert.ToDateTime(dtpfechafinal.SelectedDate), txtproveedor.Text.Trim(),
                _idSeguimiento).ToList();

            gvwocparcialsel.DataSource = lst;
            gvwocparcialsel.DataBind();
            Session["lstocparcialsel"] = JsonHelper.JsonSerializer(lst);
        }

        private void CargarSeguimientoImportacion()
        {
            objOrdenCompraWCF = new OrdenCompraWCFClient();
            List<USP_Sel_Genesys_Oc_SegImp_IdSegResult> lst = new List<USP_Sel_Genesys_Oc_SegImp_IdSegResult>();
            lst = objOrdenCompraWCF.Seleccionar_GenesysOC_SegImp_IdSeg(
                ((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                _idSeguimiento).ToList();

            cboEstado.SelectedValue = Convert.ToString(lst.Select(x => x.Id_Estado).First());


            AutoCompleteBoxEntry objEntry = new AutoCompleteBoxEntry();
            objEntry.Text = lst.Select(x => (x.Id_Agente + "-" + x.AgenteNombre)).First();
            acbAgente.Entries.Add(objEntry);
            acbAgente.Enabled = true;

            dtpfechaetdaprox.SelectedDate = lst.Select(x => x.FechaETDAprox).First();
            dtpfechaetdconfirmado.SelectedDate = lst.Select(x => x.FechaETD).First();
            dtpfechaeta.SelectedDate = lst.Select(x => x.FechaETA).First();
            txtdiaslibresSe2.Text = lst.Select(x => x.DiasLibresSE).First().ToString();
            if (txtdiaslibresSe2.Text.Trim() == string.Empty || lst.Select(x => x.FechaETA).First() == (DateTime?)null)
                dtpfechadiaslibrese.SelectedDate = (DateTime?)null;
            else
                dtpfechadiaslibrese.SelectedDate = lst.Select(x => x.FechaETA).First() == (DateTime?)null ? (DateTime?)null : Convert.ToDateTime(lst.Select(x => x.FechaETA).First()).AddDays(Convert.ToInt32(txtdiaslibresSe2.Text == string.Empty ? "0" : txtdiaslibresSe2.Text));

            dtpfechaingreso.SelectedDate = lst.Select(x => x.FechaIngresoAlm).First();

            if (dtpfechaingreso.SelectedDate.ToString() != string.Empty)
            {
                if (dtpfechadiaslibrese.SelectedDate != (DateTime?)null)
                {
                    TimeSpan dias = Convert.ToDateTime(dtpfechadiaslibrese.SelectedDate) - Convert.ToDateTime(dtpfechaingreso.SelectedDate);
                    txtdiasSe.Text = dias.Days.ToString();
                    txtestadoSe.Text = Convert.ToInt32(txtdiasSe.Text) <= 0 ? "NO" : "SI";
                }
            }
            txtdiasalmacenaje2.Text = lst.Select(x => x.DiasAlmacenaje).First().ToString();
            cbotipovia.SelectedValue = lst.Select(x => x.Id_TipoVia.ToString()).First();
            txtnrodua.Text = lst.Select(x => x.NumeroDua).First().ToString();
            txtnrobl.Text = lst.Select(x => x.NumeroBL).First();
            txtlinkdua.Text = lst.Select(x => x.LinkDua).First();
            txtnrocontenedores.Text = lst.Select(x => x.CantidadContenedor).First().ToString();

            if (txtdiasalmacenaje2.Text.Trim() == string.Empty || lst.Select(x => x.FechaETA).First() == (DateTime?)null)
                dtpfechaalmacenaje.SelectedDate = (DateTime?)null;
            else
                dtpfechaalmacenaje.SelectedDate = lst.Select(x => x.FechaETA).First() == (DateTime?)null ? (DateTime?)null : Convert.ToDateTime(lst.Select(x => x.FechaETA).First()).AddDays(Convert.ToInt32(txtdiasalmacenaje2.Text == string.Empty ? "0" : txtdiasalmacenaje2.Text));

            if(lst.Select(x=> x.Liquidacion).First()==1)
            {
                btnguardar.Enabled = false;
                btneliminarseg.Enabled = false;
                btnliquidacion.Enabled = false;
            }
            else
            {
                btnguardar.Enabled = true;
                btneliminarseg.Enabled = true;
                btnliquidacion.Enabled = true;
            }
        }

        private void CargarEstados()
        {
            objOrdenCompraWCF = new OrdenCompraWCFClient();
            List<USP_Sel_Genesys_OC_EstadoResult> lst = new List<USP_Sel_Genesys_OC_EstadoResult>();
            lst = objOrdenCompraWCF.Seleccionar_GenesysOC_Estados(
                ((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();

            cboEstado.DataSource = lst;
            cboEstado.DataTextField = "NombreEstado";
            cboEstado.DataValueField = "Id_Estado";
            cboEstado.DataBind();
        }

        private void CargarTipoVia()
        {
            objOrdenCompraWCF = new OrdenCompraWCFClient();
            List<USP_Sel_Genesys_OC_TipoViaResult> lst = new List<USP_Sel_Genesys_OC_TipoViaResult>();
            lst= objOrdenCompraWCF.Seleccionar_GenesysOC_TipoVia(
                ((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();

            cbotipovia.DataSource = lst;
            cbotipovia.DataTextField = "NombreVia";
            cbotipovia.DataValueField = "Id_TipoVia";
            cbotipovia.DataBind();

            cbotipovia.SelectedValue = "1";
            txtnrodua.Text = "235-";
        }

        private void Validar()
        {
            if(dtpfechaetdaprox.IsEmpty)
                throw new ArgumentException("ERROR: El campo fecha ETD Aproximado es obligatorio");
            else if(cbotipovia.SelectedValue.ToString().Trim()==string.Empty)
                throw new ArgumentException("ERROR: El campo Tipo de Via es obligatorio");
            //else if(txtnrodua.Text.Trim()==string.Empty || txtnrobl.Text.Trim()==string.Empty)
            //    throw new ArgumentException("ERROR: El Nro de Dua y Nro de BL es obligatorio");
        }

        private void GuardarSeguimiento()
        {
            objOrdenCompraWCF = new OrdenCompraWCFClient();
            List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult> lstocparcialSel = new List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult>();
            USP_Sel_Genesys_OC_ImpSegEntidadResult eCabecera = new USP_Sel_Genesys_OC_ImpSegEntidadResult();
            List<OrdenCompraSeguimientoBE> lstdetalle = new List<OrdenCompraSeguimientoBE>();
            OrdenCompraSeguimientoBE eoc;

            decimal? id_segimp = _idSeguimiento;

            eCabecera.Id_SegImp = _idSeguimiento;
            eCabecera.Liquidacion = 0;
            eCabecera.FechaLiquidacion = (DateTime?)null;
            eCabecera.UsuarioLiquidacion = 0;
            eCabecera.Id_Estado = Convert.ToInt32(cboEstado.SelectedValue);
            eCabecera.CantidadContenedor = Convert.ToDecimal(txtnrocontenedores.Text.Trim()==string.Empty ? "0" : txtnrocontenedores.Text);
            eCabecera.Id_Agente = acbAgente.Text.Split('-')[0];
            eCabecera.FechaETDAprox = dtpfechaetdaprox.IsEmpty ? (DateTime?)null : dtpfechaetdaprox.SelectedDate;
            eCabecera.FechaETD = dtpfechaetdconfirmado.IsEmpty ? (DateTime?)null : dtpfechaetdconfirmado.SelectedDate;
            eCabecera.FechaETA = dtpfechaeta.IsEmpty ? (DateTime?)null : dtpfechaeta.SelectedDate;
            eCabecera.DiasLibresSE = txtdiaslibresSe2.Text == string.Empty ? (int?)null : Convert.ToInt32(txtdiaslibresSe2.Text);
            eCabecera.FechaIngresoAlm = dtpfechaingreso.IsEmpty ? (DateTime?)null : dtpfechaingreso.SelectedDate;
            eCabecera.Id_TipoVia = Convert.ToInt32(cbotipovia.SelectedValue);
            eCabecera.DiasAlmacenaje= txtdiasalmacenaje2.Text == string.Empty ? (int?)null : Convert.ToInt32(txtdiasalmacenaje2.Text);
            eCabecera.NumeroDua = txtnrodua.Text.Trim();
            eCabecera.NumeroBL = txtnrobl.Text.Trim();
            eCabecera.LinkDua = txtlinkdua.Text.Trim();

            lstocparcialSel= JsonHelper.JsonDeserialize<List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult>>((string)Session["lstocparcialsel"]);

            foreach(USP_Sel_Genesys_OC_Imp_SeleccionarOCResult e in lstocparcialSel)
            {
                eoc = new OrdenCompraSeguimientoBE(Convert.ToInt32(e.Op_OC), e.No_RegistroParcial, Convert.ToInt32(e.Id_SegImp));
                lstdetalle.Add(eoc);
            }


            objOrdenCompraWCF.Registrar_Seguimiento(
                ((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                eCabecera, lstdetalle.ToArray(), ref id_segimp);

            _idSeguimiento = Convert.ToInt32(id_segimp);
            Session["IdSeguimiento"] = _idSeguimiento;

            CargarParcialesConSeguimiento();
            CargarSeguimientoImportacion();


        }

        protected void ShowMessage(string Message, String type)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }

        #endregion
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

                    _idSeguimiento = int.Parse((Request.QueryString["Id_SegImp"]));

                    CargarEstados();
                    CargarTipoVia();
                    lblnroseguimiento.Visible = false;

                    dtpfechainicial.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    dtpfechafinal.SelectedDate = DateTime.Now;

                    Session["lstocparcial"] = null;
                    Session["lstocparcialsel"] = null;
                    Session["IdSeguimiento"] = _idSeguimiento;

                    
                    if(_idSeguimiento!=0)
                    {
                        CargarParcialesConSeguimiento();
                        CargarSeguimientoImportacion();
                        lblnroseguimiento.Visible = true;
                        lblnroseguimiento.Text = " Nro. :" + _idSeguimiento.ToString();
                    }
                    else
                    {
                        lblnroseguimiento.Visible = false;
                        lblnroseguimiento.Text = "";
                        CargarParcialesPendientes();
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

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                
                Session.Remove("lstocparcial");
                Session.Remove("lstocparcialsel");
                Session.Remove("IdSeguimiento");
                
                Response.Redirect("~/Compras/OC/frmOcSegImpLista.aspx");

                //if (string.IsNullOrEmpty(Request.QueryString["Op"]))
                //    Response.Redirect("~/Comercial/Pedido/frmOrdenVenta.aspx");
                //else
                //    Response.Redirect("~/Comercial/Pedido/frmOrdenVenta.aspx?fechaInicial=" + Request.QueryString["fechaInicial"] +
                //        "&fechaFinal=" + Request.QueryString["fechafinal"]);

            }
            catch (Exception ex)
            {
                rwmSeg.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }
        
        protected void gvwocparcial_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                //List<OrdenCompraBE> lstocsel = new List<OrdenCompraBE>();
                List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult> lstocparcial = new List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult>();
                List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult> lstocparcialSel = new List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult>();
                string no_RegistroParcial = string.Empty;
                string op_oc = string.Empty;

                
                lstocparcial = JsonHelper.JsonDeserialize<List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult>>((string)Session["lstocparcial"]);

                if (!string.IsNullOrEmpty(Session["lstocparcialsel"] as string))
                {
                    lstocparcialSel = JsonHelper.JsonDeserialize<List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult>>((string)Session["lstocparcialsel"]);
                }

                

                if (lstocparcial.Any())
                {
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    no_RegistroParcial = commandArgs[0];
                    op_oc = commandArgs[1];
                    //no_RegistroParcial = e.CommandArgument.ToString();

                    if (e.CommandName == "Selparcial")
                    {
                        if (lstocparcialSel.Where(x => x.No_RegistroParcial == no_RegistroParcial && x.Op_OC== Convert.ToDecimal(op_oc)).Any())
                        {
                            lblMensaje.Text = "La OC ya fue seleccionada";
                            return;
                        }
                        else
                        {
                            lstocparcialSel.Add(lstocparcial.Where(x => x.No_RegistroParcial == no_RegistroParcial && x.Op_OC == Convert.ToDecimal(op_oc)).First());

                            //    new OrdenCompraBE(
                            //lstocparcial.Where(x => x.No_RegistroParcial == no_RegistroParcial).Select(x => x.Op_OC).ToList().First().ToString(),
                            //no_RegistroParcial
                            //));

                            gvwocparcialsel.DataSource = lstocparcialSel;
                            gvwocparcialsel.DataBind();

                            //lbocseleccionados.DataSource = lstocsel;
                            //lbocseleccionados.DataTextField = "No_RegistroParcial";
                            //lbocseleccionados.DataValueField = "No_RegistroParcial";
                            //lbocseleccionados.DataBind();

                            Session["lstocparcialsel"] = JsonHelper.JsonSerializer(lstocparcialSel);

                            lstocparcial.Remove(lstocparcial.Where(x => x.No_RegistroParcial == no_RegistroParcial && x.Op_OC == Convert.ToDecimal(op_oc)).First());

                            gvwocparcial.DataSource = lstocparcial;
                            gvwocparcial.DataBind();
                            Session["lstocparcial"] = JsonHelper.JsonSerializer(lstocparcial);
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

        protected void gvwocparcial_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }
        
        protected void gvwseguimiento_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void gvwseguimiento_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }

        protected void btnbuscarocparcial_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                CargarParcialesPendientes();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnseleccionaroc_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            btnseleccionaroc.Enabled = false;
            gvwocparcialsel.Enabled = false;
            gvwocparcial.Enabled = false;
           
        }

        protected void btnagregaritem_Click(object sender, EventArgs e)
        {

        }

        protected void gvwocparcialsel_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                //List<OrdenCompraBE> lstocsel = new List<OrdenCompraBE>();
                //List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult> lstocparcial = new List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult>();
                List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult> lstocparcialSel = new List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult>();
                string no_RegistroParcial = string.Empty;
                string op_oc = string.Empty;

                _idSeguimiento = Convert.ToInt32(Session["IdSeguimiento"]);

                if (!string.IsNullOrEmpty(Session["lstocparcialsel"] as string))
                {
                    lstocparcialSel = JsonHelper.JsonDeserialize<List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult>>((string)Session["lstocparcialsel"]);
                    
                }
                if (lstocparcialSel.Any())
                {
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    no_RegistroParcial = commandArgs[0];
                    op_oc = commandArgs[1];

                    //no_RegistroParcial = e.CommandArgument.ToString();

                    if (e.CommandName == "DelOCSel")
                    {
                        objOrdenCompraWCF = new OrdenCompraWCFClient();
                        objOrdenCompraWCF.Eliminar_OcImp_Seguimiento(
                            ((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, _idSeguimiento, no_RegistroParcial,Convert.ToInt32(op_oc));

                        lstocparcialSel.Remove(lstocparcialSel.Where(x => x.No_RegistroParcial == no_RegistroParcial && x.Op_OC == Convert.ToDecimal(op_oc)).First());

                        gvwocparcialsel.DataSource = lstocparcialSel;
                        gvwocparcialsel.DataBind();

                        Session["lstocparcialsel"] = JsonHelper.JsonSerializer(lstocparcialSel);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void gvwocparcialsel_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void cboEstado_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                _idSeguimiento = Convert.ToInt32(Session["IdSeguimiento"]);
                Validar();
                GuardarSeguimiento();
                if (_idSeguimiento != 0)
                {
                    lblnroseguimiento.Visible = true;
                    lblnroseguimiento.Text = " Nro. :" + _idSeguimiento.ToString();
                    CargarParcialesConSeguimiento();
                    CargarSeguimientoImportacion();
                }
                //ShowMessage("Se Guardó correctamente el seguimiento", "alert-success");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "AvisoOk('Se Guardó correctamente el seguimiento');", true);
                lblMensaje.Text = "Se Guardó correctamente el seguimiento";


            }
            catch (Exception ex)
            {
                lblnroseguimiento.Visible = false;
                lblnroseguimiento.Text = "";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "AvisoError('" + ex.Message + "');", true);
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        

        protected void btneliminarseg_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                //List<OrdenCompraBE> lstocsel = new List<OrdenCompraBE>();
                //List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult> lstocparcial = new List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult>();
                List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult> lstocparcialSel = new List<USP_Sel_Genesys_OC_Imp_SeleccionarOCResult>();
                string no_RegistroParcial = string.Empty;
                string op_oc = string.Empty;

                _idSeguimiento = Convert.ToInt32(Session["IdSeguimiento"]);

                
                objOrdenCompraWCF = new OrdenCompraWCFClient();
                objOrdenCompraWCF.Eliminar_OcImp_Seguimiento(
                    ((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, _idSeguimiento, no_RegistroParcial, 0);


                Session.Remove("lstocparcial");
                Session.Remove("lstocparcialsel");
                Session.Remove("IdSeguimiento");

                Response.Redirect("~/Compras/OC/frmOcSegImpLista.aspx");



            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnliquidacion_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                _idSeguimiento = Convert.ToInt32(Session["IdSeguimiento"]);

                if (_idSeguimiento == 0)
                    throw new Exception("Solo se pueden cambiar a estado liquidacion los seguimientos que esten guardados y con estado en planta");
                objOrdenCompraWCF = new OrdenCompraWCFClient();
                objOrdenCompraWCF.Registrar_OcImpSeg_Liquidacion(
                    ((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, _idSeguimiento);


                //Session.Remove("lstocparcial");
                //Session.Remove("lstocparcialsel");
                //Session.Remove("IdSeguimiento");
                btnguardar.Enabled = false;
                btneliminarseg.Enabled = false;
                btnbuscarocparcial.Enabled = false;
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "AvisoOk('Se cambio a estado LIQUIDADO el seguimiento.');", true);
                //Response.Redirect("~/Compras/OC/frmOcSegImpLista.aspx");



            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void cbotipovia_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if(txtnrodua.Text.Trim()==string.Empty)
                txtnrodua.Text = cbotipovia.SelectedValue.ToString() == "1" ? "235-" : "118-";

            if(txtnrodua.Text.Trim()== "235-" || txtnrodua.Text.Trim() == "118-")
                txtnrodua.Text = cbotipovia.SelectedValue.ToString() == "1" ? "235-" : "118-";

        }

        protected void btnadjuntos_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
               
                _idSeguimiento = Convert.ToInt32(Session["IdSeguimiento"]);

                if (_idSeguimiento == 0)
                    throw new ArgumentException("Tiene que guardar el seguimiento antes de adjuntar archivos.");
                
                //Response.Redirect("~/Compras/OC/frmOCDoc.aspx?Id_SegImp=" + _idSeguimiento.ToString());
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowDocumentsForm(" + _idSeguimiento.ToString() + ");", true);

                //if (e.CommandName == "ElimSeg")
                //{
                //    rwmSeg.RadConfirm("Eliminar?", "confirmCallBackFn", 330, 180, null, "Operacion", string.Empty);
                //}


            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
  

        
    
}