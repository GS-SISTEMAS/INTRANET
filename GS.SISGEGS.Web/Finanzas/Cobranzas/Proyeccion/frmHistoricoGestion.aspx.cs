using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.CobranzasWCF;
using GS.SISGEGS.Web.UsuarioWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.PerfilWCF;
using System.Web.Services;
using Telerik.Web.UI;

namespace GS.SISGEGS.Web.Finanzas.Cobranzas.Proyeccion
{
    public partial class frmHistoricoGestion : System.Web.UI.Page
    {
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

                    if (Request.QueryString["strCliente"] != "")
                    {
                        Title = "Observación de Gestión";
                        string strCliente = Request.QueryString["strCliente"].ToString();
                        strCliente = Session["strId_Cliente"].ToString(); 

                        string strSectorista = Request.QueryString["strSectorista"].ToString();
                        int id_zona = int.Parse(Request.QueryString["strZona"]) ;

                        if (strCliente == null || strCliente == "")
                        {
                            strCliente = null;
                        }

                        if (strSectorista == null || strSectorista == "")
                        {
                            strSectorista = null;
                        }

                        Estatus_cargar(cboEstado);
                        Semana_cargar(cboSemana);
                        string fecha = Request.QueryString["strfecha"].ToString();
                        DateTime FechaD;
                        string year = fecha.Substring(0, 4);
                        string mes = fecha.Substring(4, 2);
                        FechaD = DateTime.Parse(year + "/" + mes + "/" + "01"); 

                        rmyReporte.SelectedDate = FechaD;

                       
                        Documentos_Proyectado(strCliente, int.Parse(fecha));
                        GestionSectorista(strCliente, int.Parse(fecha));
                        //PeriodoSemana_cargar();
                        //Seleccionar_Documento(); 

                        lblMensaje.Text = "Listo para mostrar gestión sectorista";
                        lblMensaje.CssClass = "mensajeExito";

                        rmyReporte.DateInput.ReadOnly = true;
                        rmyReporte.DatePopupButton.Enabled = false;

                    }

                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        public void LimpiarValores()
        {
            int ToDay = 0;
            ToDay = DateTime.Now.Day;
            try
            {

                if (ToDay < 8)
                {
                    cboSemana.SelectedValue = "1";
                }
                else if (ToDay < 16)
                {
                    cboSemana.SelectedValue = "2";

                }
                else if (ToDay < 22)
                {
                    cboSemana.SelectedValue = "3";
                }
                else if (ToDay < 32)
                {
                    cboSemana.SelectedValue = "4";
                }

                cboEstado.SelectedValue = "0";
            
                txtObservacion.Text = "";
                rmyReporte.DateInput.ReadOnly = true;
                rmyReporte.DatePopupButton.Enabled = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BloquearValores()
        {
            try
            {
                rmyReporte.DateInput.ReadOnly = true;
                rmyReporte.DatePopupButton.Enabled = false;
                cboEstado.Enabled = false;
                cboSemana.Enabled = false;
                txtObservacion.Enabled = false; 
                btnAgregar.Enabled = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            CobranzasWCFClient objCobranzaWCF = new CobranzasWCFClient();
            int id_proyectado = 0, id_semana = 0, id_estatus = 0;
            string observacion = "";
            int year, mes, periodo=0; 

            string strCliente = Request.QueryString["strCliente"].ToString();
            strCliente = Session["strId_Cliente"].ToString();
            string strSectorista = Request.QueryString["strSectorista"].ToString();

            string mensaje = ""; 

            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (ValidarValores(ref mensaje) == false)
                {
                    if (!string.IsNullOrEmpty(txtObservacion.Text) || (txtObservacion.Text != ""))
                    {
                        if ((cboEstado.SelectedValue != "0"))
                        {
                            int count = 0; 
                            foreach (GridItem rowitem in grdDocumentos.MasterTableView.Items)
                            { 

                                GridDataItem dataitem = (GridDataItem)rowitem;
                                TableCell cell = dataitem["CheckColumn"];
                                CheckBox checkBox = (CheckBox)cell.Controls[0].FindControl("Check");

                                if (checkBox.Checked == true && checkBox.Enabled == true)
                                {
                                    count++;

                                    string TablaOrigen = dataitem["TablaOrigen"].Text;
                                    int OpOrigen = int.Parse(dataitem["OpOrigen"].Text);
                                    int Periodo = int.Parse(dataitem["Periodo"].Text);

                                    year = rmyReporte.SelectedDate.Value.Year;
                                    mes = rmyReporte.SelectedDate.Value.Month;
                                    periodo = year * 100 + mes;

                                    id_estatus = int.Parse(cboEstado.SelectedValue);
                                    observacion = txtObservacion.Text;
                                    id_semana = int.Parse(cboSemana.SelectedValue);


                                    objCobranzaWCF.GestionCobranza_Registrar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                                        strCliente, periodo.ToString(), id_semana, id_estatus, observacion, 1, TablaOrigen, OpOrigen);


                                }
                            }
                            if(count>0)
                            {
                                LimpiarValores();
                                GestionSectorista(strCliente, int.Parse(periodo.ToString()));
                                Seleccionar_Documento();
                            }
                            else
                            {
                                lblMensaje.Text = "ERROR: " + "Seleccionar documento.";
                                lblMensaje.CssClass = "mensajeError";
                            }
                        }
                        else
                        {
                            throw new ArgumentException("ERROR: Se debe seleccionar un Estado. ");
                        }
                    }
                    else
                    {
                        throw new ArgumentException("ERROR: Se debe ingresar una Observación. ");
                    }
                }
                else
                {
                    lblMensaje.Text = "ERROR: " + mensaje;
                    lblMensaje.CssClass = "mensajeError";
                }

                
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }


        public void GestionSectorista(string id_cliente, int periodo)
        {
          
            try
            {
 

                CobranzasWCFClient objCobranzaWCF = new CobranzasWCFClient();
                List<gsGestionCobranza_ListarResult> lstGestion = objCobranzaWCF.Reporte_Gestion_Sectorista(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, 
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, id_cliente, periodo).ToList();

              
                grdVencidos.DataSource = lstGestion;
                grdVencidos.DataBind();
                ViewState["lstGestion"] = JsonHelper.JsonSerializer(lstGestion);

                if(lstGestion.Count > 0 )
                {
                    lblGrilla.Value = "1";
                }
                else
                {
                    lblGrilla.Value = "0";
                }
               

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }

        }

        protected void grdVencidos_ItemDataBound1(object sender, GridItemEventArgs e)
        {
            int colum;
            colum = e.Item.RowIndex;

            CobranzasWCFClient objCobranzaWCF = new CobranzasWCFClient();

            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {

                GridEditableItem item = (GridEditableItem)e.Item;
                RadComboBox combo = ((RadComboBox)item.FindControl("RadComboBoxValore"));
                List<gsEstatus_ListarResult> lst = objCobranzaWCF.Estatus_Deuda_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();

                combo.DataSource = lst;
                combo.DataValueField = "id_estatus";
                combo.DataTextField = "NombreEstaus";
                combo.DataBind();
            }
        }

        private void Estatus_cargar( RadComboBox cboBox)
        {
            CobranzasWCFClient objCobranzaWCF = new CobranzasWCFClient();
            gsEstatus_ListarResult objEstatus = new gsEstatus_ListarResult(); 
            try
            {
                objCobranzaWCF = new CobranzasWCFClient();
                List<gsEstatus_ListarResult> lstEstatus = objCobranzaWCF.Estatus_Deuda_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();

                lstEstatus.Insert(0, objEstatus);
                objEstatus.NombreEstaus = "Seleccionar";
                objEstatus.id_estatus = 0;

                cboBox.DataSource = lstEstatus;
                cboBox.DataValueField = "id_estatus";
                cboBox.DataTextField = "NombreEstaus";
                cboBox.DataBind();

                ViewState["lstEstatus"] = JsonHelper.JsonSerializer(lstEstatus);
               

                if (cboBox.Items.Count > 0)
                    cboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Seleccionar_Documento()
        {
            CobranzasWCFClient objCobranzaWCF = new CobranzasWCFClient();
            gsEstatus_ListarResult objEstatus = new gsEstatus_ListarResult();
            try
            {
                List<gsGestionCobranza_ListarResult> lista = new List<gsGestionCobranza_ListarResult>();
                lista = JsonHelper.JsonDeserialize<List<gsGestionCobranza_ListarResult>>((string)ViewState["lstGestion"]);


                foreach (GridItem rowitem in grdDocumentos.MasterTableView.Items)
                {
                    GridDataItem dataitem = (GridDataItem)rowitem;
                    TableCell cell = dataitem["CheckColumn"];
                    CheckBox checkBox = (CheckBox)cell.Controls[0].FindControl("Check");

                    //if (checkBox.Checked == true && checkBox.Enabled == true)
                    //{

                    string TablaOrigen = dataitem["TablaOrigen"].Text;
                    string OpOrigen = dataitem["OpOrigen"].Text;
                    string Periodo = dataitem["Periodo"].Text;

                    foreach (gsGestionCobranza_ListarResult Gestion in lista)
                    {
                        if (Gestion.TablaOrigen == TablaOrigen && Gestion.OpOrigen == OpOrigen && Gestion.periodo == Periodo)
                        {
                            checkBox.Checked = true; 
                        }
                    }
 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Semana_cargar(RadComboBox cboBox)
        {
            int ToDay = 0;
            ToDay = DateTime.Now.Day;

            try
            {
                RadComboBoxItem item;
                int id;
                string strId;

                for (int x = 0; x < 4; x++)
                {
                    id = x + 1;
                    strId = id.ToString();

                    item = new RadComboBoxItem();
                    item.Value = strId;
                    item.Text = strId;
                    cboBox.Items.Add(item);
                }
                if (cboBox.Items.Count > 0)
                    cboBox.SelectedIndex = 0;

                if (ToDay < 8)
                {
                    cboBox.SelectedValue = "1";
                }
                else if (ToDay < 16)
                {
                    cboBox.SelectedValue = "2";

                }
                else if (ToDay < 22)
                {
                    cboBox.SelectedValue = "3";
                }
                else if (ToDay < 32)
                {
                    cboBox.SelectedValue = "4";
                }


                ViewState["id_semana"] = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void PeriodoSemana_cargar()
        //{
        //    string semana;
        //    string periodo;
        //    string strFecha;
        //    int intSemana; 
        //    int year;
        //    int mes;
        //    DateTime fechaS;

        //    List<gsGestionCobranza_ListarResult> lista = new List<gsGestionCobranza_ListarResult>();
        //    try
        //    {
        //        if(lblGrilla.Value == "1")
        //        {
        //            lista = JsonHelper.JsonDeserialize<List<gsGestionCobranza_ListarResult>>((string)ViewState["lstGestion"]);
        //            var ultimaGestion = (from x in lista
        //                              orderby x.periodo descending, x.id_semana descending
        //                              select new
        //                              {
        //                                  x.periodo,
        //                                  x.id_semana
        //                              }).First();

        //            semana = ultimaGestion.id_semana.ToString();
        //            intSemana = int.Parse(semana);
        //            periodo = ultimaGestion.periodo.ToString();

        //            year = int.Parse(periodo.Substring(0, 4));
        //            mes = int.Parse(periodo.Substring(4,2));

        //            if(intSemana == 4)
        //            {
        //                intSemana = 1;
        //                //mes = mes + 1;
        //                if(mes > 12)
        //                {
        //                    year = year + 1;
        //                    mes = 1;
        //                }
        //                BloquearValores();
        //            }
        //            else
        //            {
        //                intSemana = intSemana + 1;
        //            }

        //            strFecha = DateTime.Now.Day.ToString() + "/" + mes.ToString() + "/" + year.ToString();
        //            fechaS = Convert.ToDateTime(strFecha);
        //            rmyReporte.SelectedDate = fechaS;

        //            cboSemana.SelectedValue = intSemana.ToString();
        //        }
        //        else
        //        {
        //            //semana = DateTime.Now.DayOfWeek.ToString();
        //            //rmyReporte.SelectedDate = DateTime.Now;
        //            cboSemana.SelectedValue = 1.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        protected void grdVencidos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (lblGrilla.Value == "1")
                {
                    grdVencidos.DataSource = JsonHelper.JsonDeserialize<List<gsGestionCobranza_ListarResult>>((string)ViewState["lstGestion"]);
                    grdVencidos.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdVencidos_selectedindexchanged(object sender, EventArgs e)
        {
            List<gsEstatus_ListarResult> lstEstatus = new List<gsEstatus_ListarResult>();
            gsEstatus_ListarResult objEstatus = new gsEstatus_ListarResult(); 
            try
            {

                lstEstatus = JsonHelper.JsonDeserialize<List<gsEstatus_ListarResult>>((string)ViewState["lstEstatus"]);

                GridDataItem gridItem = (GridDataItem)grdVencidos.SelectedItems[0];
                string id_semana = gridItem["id_semana"].Text;
                string NombreEstaus = gridItem["NombreEstaus"].Text;
                string observacion = gridItem["observacion"].Text;
                string TablaOrigenR = gridItem["TablaOrigen"].Text;
                string OpOrigenR = gridItem["OpOrigen"].Text;
                string PeriodoR = gridItem["periodo"].Text;


                cboEstado.Enabled = true;
                txtObservacion.Enabled = true;
                btnAgregar.Enabled = true;

                objEstatus = lstEstatus.ToList().FindAll(x => x.NombreEstaus == NombreEstaus).Single(); 

                cboSemana.SelectedValue = id_semana;
                cboEstado.SelectedValue = objEstatus.id_estatus.ToString() ;
                txtObservacion.Text = observacion;

                foreach (GridItem rowitem in grdDocumentos.MasterTableView.Items)
                {
                    GridDataItem dataitem = (GridDataItem)rowitem;
                    TableCell cell = dataitem["CheckColumn"];
                    CheckBox checkBox = (CheckBox)cell.Controls[0].FindControl("Check");

                    //if (checkBox.Checked == true && checkBox.Enabled == true)
                    //{

                    string TablaOrigen = dataitem["TablaOrigen"].Text;
                    string OpOrigen = dataitem["OpOrigen"].Text;
                    string Periodo = dataitem["Periodo"].Text;

                    if (TablaOrigenR == TablaOrigen && OpOrigenR == OpOrigen && PeriodoR == Periodo)
                    {
                        checkBox.Checked = true;
                    }
                    else
                    {
                        checkBox.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + 100 + ");", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        public void Documentos_Proyectado(string id_cliente, int periodo)
        {

            try
            {


                CobranzasWCFClient objCobranzaWCF = new CobranzasWCFClient();
                List<ProyectadoCobranza_DocumentosResult> lstGestion = objCobranzaWCF.ProyectadoCobranza_Documentos(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, id_cliente, periodo).ToList();


                grdDocumentos.DataSource = lstGestion;
                grdDocumentos.DataBind();
                ViewState["lstDocumentos"] = JsonHelper.JsonSerializer(lstGestion);

                if (lstGestion.Count > 0)
                {
                    lblGrilla.Value = "1";
                }
                else
                {
                    lblGrilla.Value = "0";
                }


            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }

        }


        public bool ValidarValores(ref string mensaje)
        {
            bool bloqueo = false;
            int gtSemana = 0;

            int ToDay = 0;
            int ToSemana = 0;

            ToDay = DateTime.Now.Day;
            gtSemana = int.Parse(cboSemana.SelectedValue);

            if (ToDay < 8)
            {
                ToSemana = 1;
            }
            else if (ToDay < 16)
            {
                ToSemana = 2;

            }
            else if (ToDay < 22)
            {
                ToSemana = 3;

            }
            else if (ToDay < 32)
            {
                ToSemana = 4;
            }


            try
            {

                if (gtSemana < ToSemana)
                {
                    mensaje = mensaje + "La semana está cerrada.";
                    bloqueo = true;
                }

                rmyReporte.Enabled = false;


            }
            catch (Exception ex)
            {

                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            return bloqueo;
        }
    }
}