using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.Web.MateriaContratoWCF;
using GS.SISGEGS.Web.TipoContratoWCF;
using GS.SISGEGS.Web.EstadoContratoWCF;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.ContratosWCF;
using Telerik.Web.UI;
using GS.SISGEGS.Web.Helpers;

namespace GS.SISGEGS.Web.Contratos.Reportes
{
    public partial class frmGeneralContratos : System.Web.UI.Page
    {
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

                    Area_Cargar();
                    MateriaContrato_Cargar();
                    TipoContrato_Cargar();
                    EstadoContrato_Cargar();
                    
                    dpFechaDesde.SelectedDate = DateTime.Now.AddMonths(-1).Date;
                    dpFechaHasta.SelectedDate = DateTime.Now.Date;
                    dpFechanVencDesde.SelectedDate = DateTime.Now.Date;
                    dpFechanVencHasta.SelectedDate = DateTime.Now.AddMonths(1).Date;
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void Area_Cargar()
        {
            try
            {
                ContratosWCFClient objContratoWCF = new ContratosWCFClient();
                AreaResponsable_ListarResult objArea = new AreaResponsable_ListarResult();
                List<AreaResponsable_ListarResult> lstArea;

                lstArea = objContratoWCF.AreaResponsable_Listar().ToList();

                lstArea.Insert(0, objArea);
                objArea.nombreAreaResponsable = "Todos";


                var lstAreas = from x in lstArea
                               select new
                               {
                                   x.IdAreaResponsable,
                                   DisplayID = String.Format("{0}", x.IdAreaResponsable),
                                   DisplayField = String.Format("{0}", x.nombreAreaResponsable)
                                   //DisplayField = String.Format("{0} {1} {2} {3}", x.Placa, x.Modelo, x.Marca, x.Agenda)
                               };

                cboArea.DataSource = lstAreas;
                cboArea.DataTextField = "DisplayField";
                cboArea.DataValueField = "DisplayID";
                cboArea.DataBind();

            }
            catch (Exception ex)
            {
            }

        }

        private void MateriaContrato_Cargar() {
            try
            {
                MateriaContratoWCFClient objMateriaWCF = new MateriaContratoWCFClient();
                MateriaContrato_ListarResult objMateria = new MateriaContrato_ListarResult();
                List<MateriaContrato_ListarResult> lstMateria;

                lstMateria = objMateriaWCF.MateriaContrato_Listar().ToList();

                lstMateria.Insert(0, objMateria);
                objMateria.nombreMateria= "Todos";
                objMateria.idMateriaContrato = 0;

                var lstMaterias = from x in lstMateria
                               select new
                               {
                                   x.idMateriaContrato,
                                   DisplayID = String.Format("{0}", x.idMateriaContrato),
                                   DisplayField = String.Format("{0}", x.nombreMateria)
                                   //DisplayField = String.Format("{0} {1} {2} {3}", x.Placa, x.Modelo, x.Marca, x.Agenda)
                               };

                cboMateriaContrato.DataSource = lstMaterias;
                cboMateriaContrato.DataTextField = "DisplayField";
                cboMateriaContrato.DataValueField = "DisplayID";
                cboMateriaContrato.DataBind();

            }
            catch (Exception ex)
            {
            }
        }

        private void TipoContrato_Cargar()
        {
            try
            {
                TipoContratoWCFClient objTipoWCF = new TipoContratoWCFClient();
                TipoContrato_ListarResult objTipo = new TipoContrato_ListarResult();
                List<TipoContrato_ListarResult> lstTipo;

                lstTipo = objTipoWCF.TipoContrato_Listar().ToList();

                lstTipo.Insert(0, objTipo);
                objTipo.nombreTipo = "Todos";
                objTipo.idTipoContrato = 0;

                var lstTipos = from x in lstTipo
                                  select new
                                  {
                                      x.idTipoContrato,
                                      DisplayID = String.Format("{0}", x.idTipoContrato),
                                      DisplayField = String.Format("{0}", x.nombreTipo)
                                      //DisplayField = String.Format("{0} {1} {2} {3}", x.Placa, x.Modelo, x.Marca, x.Agenda)
                                  };

                cboTipoContrato.DataSource = lstTipos;
                cboTipoContrato.DataTextField = "DisplayField";
                cboTipoContrato.DataValueField = "DisplayID";
                cboTipoContrato.DataBind();

            }
            catch (Exception ex)
            {
            }
        }

        private void EstadoContrato_Cargar() {
            try
            {
                EstadoContratoWCFClient objEstadoWCF = new EstadoContratoWCFClient();
                EstadoContrato_ListarResult objEstado = new EstadoContrato_ListarResult();
                List<EstadoContrato_ListarResult> lstEstado;

                lstEstado = objEstadoWCF.EstadoContrato_Listar().ToList();

                lstEstado.Insert(0, objEstado);
                objEstado.nombreEstado= "Todos";
                objEstado.idEstadoContrato = 0;

                var lstEstados = from x in lstEstado
                               select new
                               {
                                   x.idEstadoContrato ,
                                   DisplayID = String.Format("{0}", x.idEstadoContrato),
                                   DisplayField = String.Format("{0}", x.nombreEstado)
                                   //DisplayField = String.Format("{0} {1} {2} {3}", x.Placa, x.Modelo, x.Marca, x.Agenda)
                               };

                cboEstado.DataSource = lstEstados;
                cboEstado.DataTextField = "DisplayField";
                cboEstado.DataValueField = "DisplayID";
                cboEstado.DataBind();

            }
            catch (Exception ex)
            {
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                ReporteGeneral_Contratos();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void ReporteGeneral_Contratos() {
            ContratosWCFClient objContratosWCF = new ContratosWCFClient();
            
            try
            {
                var areaResponsable = int.Parse(cboArea.SelectedValue);
                var idMateria = int.Parse(cboMateriaContrato.SelectedValue);
                var idTipo = int.Parse(cboTipoContrato.SelectedValue);
                var idProveedor = 0;
                var idEstado = int.Parse(cboEstado.SelectedValue);
                var fechaInicio = dpFechaDesde.SelectedDate.Value;
                var fechaFin = dpFechaHasta.SelectedDate.Value;
                var fechaVencIni = dpFechanVencDesde.SelectedDate.Value;
                var fechaVencFin = dpFechanVencHasta.SelectedDate.Value;

                List<ReporteGeneralContratosResult> lstContratos = objContratosWCF.ReporteGeneralContratos(areaResponsable, idMateria, idTipo, idProveedor, idEstado, fechaInicio, fechaFin, fechaVencIni, fechaVencFin).ToList();
                grdGeneralContratos.DataSource = lstContratos;
                grdGeneralContratos.DataBind();

                ViewState["lstContratos"] = JsonHelper.JsonSerializer(lstContratos);
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        protected void ramRepContrato_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
        
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                grdGeneralContratos.ExportSettings.FileName = "GeneralContratos_" + DateTime.Now.ToString("yyyyMMddHmm");
                grdGeneralContratos.ExportSettings.ExportOnlyData = true;
                grdGeneralContratos.ExportSettings.IgnorePaging = true;
                grdGeneralContratos.ExportSettings.OpenInNewWindow = true;
                grdGeneralContratos.MasterTableView.ExportToExcel();

                Page.Response.ClearHeaders();
                Page.Response.ClearContent();
            }
            catch (Exception ex)
            {
                //ra.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

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

        protected void ramContratos_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Argument == "Rebind")
                {
                    grdGeneralContratos.MasterTableView.SortExpressions.Clear();
                    grdGeneralContratos.MasterTableView.GroupByExpressions.Clear();
                   
                    ReporteGeneral_Contratos();


                    grdGeneralContratos.DataBind();

                    lblMensaje.Text = "Se agregó el contrato al sistema.";
                    lblMensaje.CssClass = "mensajeExito";
                }

                if (e.Argument.Split('(')[0].Trim() == "RebindAndNavigate")
                {
                    
                }

                if (e.Argument.Split(',')[0] == "ChangePageSize")
                {
                    grdGeneralContratos.Height = new Unit(e.Argument.Split(',')[1] + "px");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdGeneralContratos_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            ContratosWCFClient objContratoWCF = new ContratosWCFClient();
            List<ReporteGeneralContratosResult> lstContratos;
            try
            {
                int idContrato = Convert.ToInt32(((GridDataItem)e.Item).GetDataKeyValue("idContrato"));
                var usuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;

                objContratoWCF.Contrato_Eliminar(idContrato, usuario);
                lstContratos = JsonHelper.JsonDeserialize<List<ReporteGeneralContratosResult>>((string)ViewState["lstContratos"]);
                lstContratos.Remove(lstContratos.Find(x => x.idContrato == idContrato));
                ViewState["lstContratos"] = JsonHelper.JsonSerializer(lstContratos);
                grdGeneralContratos.DataSource = lstContratos;
                grdGeneralContratos.DataBind();

                lblMensaje.Text = "Se eliminó el contrato del sistema.";
                lblMensaje.CssClass = "mensajeExito";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdGeneralContratos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if (e.CommandName == "Editar")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowInsertForm(" + e.CommandArgument + ");", true);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}