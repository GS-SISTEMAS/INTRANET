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
using GS.SISGEGS.Web.ItemWCF;
using GS.SISGEGS.Web.DocumentoWCF;
using GS.SISGEGS.Web.FormaPagoWCF;
using GS.SISGEGS.Web.OrdenVentaWCF;
using GS.SISGEGS.Web.LoginWCF;
using GS.SISGEGS.Web.OrdenCompraWCF;
using GS.SISGEGS.Web.PlanificacionWCF;
using System.Net;

namespace GS.SISGEGS.Web.Comercial.Proyectado
{
    public partial class frmMetaPresupuestoProm : System.Web.UI.Page
    {
        PlanificacionWCFClient objPlanificacion = new PlanificacionWCFClient();
        List<USP_SEL_MetaPresupuestoPendienteResult> _lstPrePendiente = new List<USP_SEL_MetaPresupuestoPendienteResult>();
        List<USP_Sel_MetaPresupuestoPromotorResult> _lstPrePromotor = new List<USP_Sel_MetaPresupuestoPromotorResult>();
        string strAnno, strMes, strId_Vendedor;
        private void CargarPresupuestoPendiente()
        {
            objPlanificacion = new PlanificacionWCFClient();
            strId_Vendedor = Request.QueryString["Id_Vendedor"];
            strMes = Request.QueryString["Mes"];
            strAnno = Request.QueryString["Anno"];

            _lstPrePendiente = objPlanificacion.Obtener_PresupuestoPendiente(
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario,Convert.ToInt32(strAnno),Convert.ToInt32(strMes), strId_Vendedor).ToList();

            txtzona.Text = _lstPrePendiente.Select(x => x.Zona).First().ToUpper();
            txtjefezona.Text = _lstPrePendiente.Select(x => x.Vendedor).First().ToUpper();
            txtperiodo.Text = (RetornaMes(Convert.ToInt32(strMes)) + " - " + strAnno).ToUpper();
            Session["lstpendientes"] = JsonHelper.JsonSerializer(_lstPrePendiente);
            gvwProductos.DataSource = _lstPrePendiente;
            gvwProductos.DataBind();

        }

        private void CargarPresupuestoPromotor()
        {
            strId_Vendedor = Request.QueryString["Id_Vendedor"];
            strMes = Request.QueryString["Mes"];
            strAnno = Request.QueryString["Anno"];
            objPlanificacion = new PlanificacionWCFClient();
            _lstPrePromotor = objPlanificacion.Obtener_MetaPresupuestoPromotor(
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, Convert.ToInt32(strAnno), Convert.ToInt32(strMes), strId_Vendedor).ToList();
            Session["lstpromotor"] = JsonHelper.JsonSerializer(_lstPrePromotor);

            rpgResumenZona.DataSource= _lstPrePromotor;
            rpgResumenZona.DataBind();

            if(_lstPrePromotor.Where(x=> x.Aprobado==true).Any())
            {
                btnagregar.Enabled = false;
                btnAprobar.Enabled = false;
                lblestado.Text = "Aprobado";
            }
            else
            {
                lblestado.Text = "Nuevo";
            }
        }

        protected void btnagregar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                strId_Vendedor = Request.QueryString["Id_Vendedor"];
                strMes = Request.QueryString["Mes"];
                strAnno = Request.QueryString["Anno"];

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowInsertForm('" + strId_Vendedor + "'," +
                                   strAnno.ToString() + "," + strMes.ToString() + ");", true);

                //Response.Redirect("~/Comercial/Proyectado/frmMetaPresupuestoPromAdd.aspx?Id_Vendedor=" + strId_Vendedor + "&Anno=" + strAnno + "&Mes=" + strMes);

            }
            catch (Exception ex)
            {
                rwmPre.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

        protected void ramPresupuesto_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (e.Argument[0].ToString().Trim() != string.Empty)
                {
                    //grdPersonal.MasterTableView.SortExpressions.Clear();
                    //grdPersonal.MasterTableView.GroupByExpressions.Clear();
                    //gvwProductos.Rebind();
                    rpgResumenZona.Rebind();
                    //CargarPresupuestoPendiente();
                    CargarPresupuestoPromotor();

                    lblMensaje.Text = "Se realizo el registro del Personal";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void rpgResumenZona_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            //if (Session["Usuario"] == null)
            //    Response.Redirect("~/Security/frmCerrar.aspx");

            //try
            //{


            //    //CargarPresupuestoPendiente();
            //    //CargarPresupuestoPromotor();
            //    rpgResumenZona.DataSource = JsonHelper.JsonDeserialize<List<USP_Sel_MetaPresupuestoPromotorResult>>((string)Session["lstpromotor"]);

               
                
            //}
            //catch (Exception ex)
            //{
            //    lblMensaje.Text = ex.Message;
            //    lblMensaje.CssClass = "mensajeError";
            //}
        }

        protected void btnregresar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                Response.Redirect("~/Comercial/Proyectado/frmMetaPresupuestoIng.aspx");
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

            try
            {
                strId_Vendedor = Request.QueryString["Id_Vendedor"];
                strMes = Request.QueryString["Mes"];
                strAnno = Request.QueryString["Anno"];
                objPlanificacion = new PlanificacionWCFClient();
                objPlanificacion.Registrar_MetaPresupuestoPromotor(
                     ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, Convert.ToInt32(strAnno), Convert.ToInt32(strMes), strId_Vendedor, string.Empty, string.Empty, 0, true
                    );
                rwmPre.RadAlert("Se aprobó correctamente el presupuesto.", 400, null, "Mensaje de Informacion", null);
                btnagregar.Enabled = false;
                btnAprobar.Enabled = false;
                lblestado.Text = "Aprobado";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private string RetornaMes(int mes)
        {
            string mes2 = string.Empty;
            switch(mes)
            {
                case 1:
                    mes2="Enero";
                    break;
                case 2:
                    mes2="Feberero";
                    break;
                case 3:
                    mes2 = "Marzo";
                    break;
                case 4:
                    mes2 = "Abril";
                    break;
                case 5:
                    mes2 = "Mayo";
                    break;
                case 6:
                    mes2 = "Junio";
                    break;
                case 7:
                    mes2 = "Julio";
                    break;
                case 8:
                    mes2 = "Agosto";
                    break;
                case 9:
                    mes2 = "Setiembre";
                    break;
                case 10:
                    mes2 = "Octubre";
                    break;
                case 11:
                    mes2 = "Noviembre";
                    break;
                case 12:
                    mes2 = "Diciembre";
                    break;
                    
            }
            return mes2;
        }
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

                    Session["lstpendientes"] = null;
                    Session["lstpromotor"] = null;

                    CargarPresupuestoPendiente();
                    CargarPresupuestoPromotor();

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
    }
}