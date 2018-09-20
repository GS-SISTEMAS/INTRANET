using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using GS.SISGEGS.Web.EstadoCuentaWCF;
using GS.SISGEGS.Web.CobranzasWCF;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.DM;
using System.Globalization;
using Telerik.Web.UI;
using GS.SISGEGS.Web.LoginWCF;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.ComponentModel;

namespace GS.SISGEGS.Web.Finanzas.Cobranzas
{
    public partial class frmProyectado2 : System.Web.UI.Page
    {
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
                    rmyReporte.SelectedDate = DateTime.Now;
                    Sectorista_Cargar();
                    Zona_Cargar("0");

                    cboZona.Enabled = true;
                    btnBuscar.Enabled = true; 

                    //Empresa_Cargar();
                    //cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();
                    //Perfil_Cargar(int.Parse(cboEmpresa.SelectedValue), "");
                    ////Usuario_Cargar(int.Parse(cboEmpresa.SelectedValue), int.Parse(cboPerfil.SelectedValue),  "");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        protected void Buscar_ListarProyectado()
        {
  
            int periodo, id_zona;
            int year;
            int mes;
            string id_sectorista;

            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                year = rmyReporte.SelectedDate.Value.Year;
                mes = rmyReporte.SelectedDate.Value.Month;

                periodo = (year * 100) + mes;

                id_sectorista = cboSectorista.SelectedValue.ToString();
                id_zona = int.Parse(cboZona.SelectedValue);

                Proyectado_Listar(periodo, id_sectorista, id_zona, year, mes);
                ViewState["fechaInicial"] = rmyReporte.SelectedDate.Value;

                cboZona.SelectedValue = id_zona.ToString();
                ViewState["id_Sectorista"] = id_sectorista;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
 

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar_ListarProyectado();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreate('');", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void Proyectado_Listar(int periodo, string id_sectorista, int id_zona, int anho, int mes)
        {
            CobranzasWCFClient objProyectadoWCF = new CobranzasWCFClient();
            List<spEstadoCuenta_ProyectadoResult> lstProyectado = new List<spEstadoCuenta_ProyectadoResult>();
            try
            {
                lstProyectado = objProyectadoWCF.EstadoCuenta_Proyectado(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                     ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, periodo, id_sectorista, id_zona, anho, mes).ToList().OrderBy(x=> x.ClienteNombre).ToList();

                ViewState["lstProyectado"] = JsonHelper.JsonSerializer(lstProyectado);

                lstProyectado = lstProyectado.FindAll(x=> x.ImportePendiente > 0).ToList().OrderBy(x=> x.Nom_Zona).ToList(); 


                grdClientes.DataSource = lstProyectado;
                grdClientes.DataBind();

 
                lblMensaje.Text = "Se cargo con exitó";
                lblMensaje.CssClass = "mensajeExito";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }


        protected void grdClientes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (lblGrilla.Value == "1")
                {
                    grdClientes.DataSource = JsonHelper.JsonDeserialize<List<spEstadoCuenta_ProyectadoResult>>((string)ViewState["lstProyectado"]);
                    grdClientes.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdClientes_ItemCommand(object sender, GridCommandEventArgs e)
        {
            string id_Sectorista;
            string stringPeriodo;
            string strId_Cliente;

            string year;
            string mes;
            string day;

            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if (e.CommandName == "Gestion")
                {
                    year = rmyReporte.SelectedDate.Value.Year.ToString();
                    mes = rmyReporte.SelectedDate.Value.Month.ToString();

                    if (mes.Length == 1)
                    { mes = "0" + mes; }

                    GridDataItem dataitem = (GridDataItem)e.Item;
                    string ID_Zona = dataitem.GetDataKeyValue("Id_Zona").ToString();

                    strId_Cliente = e.CommandArgument.ToString();
                    Session["strId_Cliente"] = strId_Cliente;


                    if (ID_Zona != "0")
                    {
                        stringPeriodo = year + "" + mes;
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreateViewGestion(" + strId_Cliente + "," + ViewState["id_Sectorista"] + "," + ID_Zona + "," + stringPeriodo + ");", true);
                    }
                    else
                    {
                        lblMensaje.Text = "No se realizó la proyección del cliente para el periodo seleccionado.!!";
                        lblMensaje.CssClass = "mensajeError";

                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "buscarError();", true);
                    }

                }

                if (e.CommandName == "PeriodoDeuda")
                {
                    year = rmyReporte.SelectedDate.Value.Year.ToString();
                    mes = rmyReporte.SelectedDate.Value.Month.ToString();

                    if (mes.Length == 1)
                    {
                        mes = "0" + mes;
                    }

                    stringPeriodo = year + "" + mes;

                    GridDataItem dataitem = (GridDataItem)e.Item;
                    string id_Cliente = dataitem.GetDataKeyValue("id_Cliente").ToString();

                    id_Cliente = "1" + id_Cliente;
                    string id_sectorista = ViewState["id_Sectorista"].ToString();

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreateViewDeuda(" + id_Cliente + "," + id_sectorista + ");", true);

                }
                if (e.CommandName == "EstadoCuenta")
                {
                    GridDataItem dataitem = (GridDataItem)e.Item;
                  
                    string ID_zona = dataitem.GetDataKeyValue("Id_Zona").ToString();
                    string ID_Cliente = "1" + e.CommandArgument.ToString();

                    id_Sectorista = "1" + ViewState["id_Sectorista"].ToString();
                    string strFecha;
                    year = rmyReporte.SelectedDate.Value.Year.ToString();
                    mes = rmyReporte.SelectedDate.Value.Month.ToString();

                    if (int.Parse(mes) < 10)
                    {
                        mes = "0" + mes;
                    }

                    strFecha = year + "" + mes;

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreateViewEstadoCuenta(" + ID_Cliente + "," + strFecha + "," + id_Sectorista + "," + ID_zona + ");", true);

                    //Response.Redirect("~/Finanzas/Cobranzas/Proyeccion/frmEstadoCuenta.aspx?id_cliente=" + e.CommandArgument.ToString() + "&fechaInicial=" + ((DateTime)ViewState["fechaInicial"]).ToString("dd/MM/yyyy") + "&ID_Vendedor=" + ID_Vendedor + "&ID_Sectorista=" + id_Sectorista + "&ID_zona=" + ID_zona);
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void ramUsuario_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            int periodo, id_zona;
            int year;
            int mes;
            string id_sectorista;
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (e.Argument.Split(',')[0] == "Registro")
                {
                    grdClientes.MasterTableView.SortExpressions.Clear();
                    grdClientes.MasterTableView.GroupByExpressions.Clear();

                    year = rmyReporte.SelectedDate.Value.Year;
                    mes = rmyReporte.SelectedDate.Value.Month;

                    periodo = (year * 100) + mes;

                    id_sectorista = cboSectorista.SelectedValue.ToString();
                    id_zona = int.Parse(cboZona.SelectedValue);

                    Proyectado_Listar(periodo, id_sectorista, id_zona, year, mes);


                    lblMensaje.Text = "Se realizo el registro del usuario";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void Zona_Cargar(string id_sectorista)
        {
            try
            {
                CobranzasWCFClient objCorbanza = new CobranzasWCFClient();
                List<ZonasSectoristaPermiso_ListarResult> lstdetalle = new List<ZonasSectoristaPermiso_ListarResult>();

                ZonasSectoristaPermiso_ListarResult objZona = new ZonasSectoristaPermiso_ListarResult();
                List<ZonasSectoristaPermiso_ListarResult> lstZona;

                //lstZona = objAgendaWCF.Agenda_ListarZonaSectorista(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                //    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, id_sectorista).ToList();
                lstZona = objCorbanza.ZonasSectoristaPermiso_Listar(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, id_sectorista).ToList();


                lstZona.Insert(0, objZona);
                objZona.Zona = "TODO";
                objZona.ID_Zona = 0;

                var lstZonas = from x in lstZona
                               select new
                               {
                                   x.ID_Zona,
                                   DisplayID = String.Format("{0}", x.ID_Zona),
                                   DisplayField = String.Format("{0}", x.Zona)
                                   //DisplayField = String.Format("{0} {1} {2} {3}", x.Placa, x.Modelo, x.Marca, x.Agenda)
                               };

                cboZona.DataSource = lstZonas;
                cboZona.DataTextField = "DisplayField";
                cboZona.DataValueField = "DisplayID";
                cboZona.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void Sectorista_Cargar()
        {
            try
            {
                AgendaWCFClient objAgendaWCF = new AgendaWCFClient();
                gsUsuario_SectoristaResult objSecotrista = new gsUsuario_SectoristaResult();
                List<gsUsuario_SectoristaResult> lstSectoirista = new List<gsUsuario_SectoristaResult>();

                lstSectoirista = objAgendaWCF.Agenda_ListarSectorista(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, null, 1).ToList();



                lstSectoirista.Insert(0, objSecotrista);
                objSecotrista.AgendaNombre = "TODO";
                objSecotrista.ID_Agenda = "0";

                var lstSectoiristas = from x in lstSectoirista
                                      select new
                                      {
                                          x.ID_Agenda,
                                          DisplayID = String.Format("{0}", x.ID_Agenda),
                                          DisplayField = String.Format("{0}", x.AgendaNombre)
                                          //DisplayField = String.Format("{0} {1} {2} {3}", x.Placa, x.Modelo, x.Marca, x.Agenda)
                                      };

                cboSectorista.DataSource = lstSectoiristas;
                cboSectorista.DataTextField = "DisplayField";
                cboSectorista.DataValueField = "DisplayID";
                cboSectorista.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void cboZonas_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Ocultar_Botones();
                List<gsProyectadoCobranza_ListarResult> lstProyectado = new List<gsProyectadoCobranza_ListarResult>();
                grdClientes.DataSource = lstProyectado;
                grdClientes.DataBind();

                if (cboZona == null || cboZona.SelectedValue == "" || cboZona.SelectedValue == "0")
                {
                    Ocultar_Botones();
                    btnBuscar.Enabled = true;
                    cboZona.Enabled = true;
                }
                else
                {
                    cboZona.Enabled = true;
                    btnBuscar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }

        }

        protected void cboSectorista_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Ocultar_Botones();
                Zona_Cargar(cboSectorista.SelectedValue);
                List<gsProyectadoCobranza_ListarResult> lstProyectado = new List<gsProyectadoCobranza_ListarResult>();
                grdClientes.DataSource = lstProyectado;
                grdClientes.DataBind();

                if (cboSectorista == null || cboSectorista.SelectedValue == "" || cboSectorista.SelectedValue == "0")
                {
                    Ocultar_Botones();
                    btnBuscar.Enabled = true;
                    cboZona.Enabled = true;
                }
                else
                {
                    btnBuscar.Enabled = true; 
                    cboZona.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void Ocultar_Botones()
        {
            try
            {
                btnBuscar.Enabled = false;
                cboZona.Enabled = false;
                //btnCargaMasiva.Enabled = false;
                //btnExcel.Enabled = false;
                List<gsProyectadoCobranza_ListarResult> lstProyectado = new List<gsProyectadoCobranza_ListarResult>();
                grdClientes.DataSource = lstProyectado;
                grdClientes.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void ramProyectado_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if (e.Argument == "Rebind")
                {
                    grdClientes.MasterTableView.SortExpressions.Clear();
                    grdClientes.MasterTableView.GroupByExpressions.Clear();
                    Buscar_ListarProyectado();
                    grdClientes.Rebind();

                    lblMensaje.Text = "Proyección cargada con éxito.";
                    lblMensaje.CssClass = "mensajeExito";
                }


                if (e.Argument.Split('(')[0].Trim() == "RebindAndNavigateBuscar")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "buscar();", true);

                }

                if (e.Argument.Split('(')[0].Trim() == "RebindAndNavigate")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "buscarHistorico();", true);
                }

            }
            catch (Exception ex)
            {
                rwmUsuario.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }
    }
}