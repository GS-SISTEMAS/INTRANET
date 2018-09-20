using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.EgresosWCF;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.Helpers;
using Telerik.Web.UI;

namespace GS.SISGEGS.Web.Commercial.Gastos
{
    public partial class frmGastos : System.Web.UI.Page
    {
        private void EgresosVarios_Listar(string idAgenda, DateTime fechaInicio, DateTime fechaFinal) {
            EgresosWCFClient objEgresosWCF;
            try {
                objEgresosWCF = new EgresosWCFClient();
                grdEgresosVarios.DataSource = objEgresosWCF.EgresosVarios_ListarCajaChica(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda, fechaInicio, fechaFinal);
                grdEgresosVarios.DataBind();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            if (!ConnectionHelpers.CheckForInternetConnection())
                throw new ArgumentException("Revisar su conexión a internet.");

            try {
                if (!Page.IsPostBack) {
                    dpFechaInicio.SelectedDate = DateTime.Now.AddMonths(-1);
                    dpFechaFinal.SelectedDate = DateTime.Now;
                    EgresosVarios_Listar(((Usuario_LoginResult)Session["Usuario"]).nroDocumento, dpFechaInicio.SelectedDate.Value, dpFechaFinal.SelectedDate.Value);

                    lblMensaje.Text = "Se encontraron " + grdEgresosVarios.Items.Count.ToString() + " registros.";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if ((dpFechaFinal.SelectedDate.Value - dpFechaInicio.SelectedDate.Value).TotalDays < 0)
                    throw new ArgumentException("ERROR: Las fecha de inicio debe ser menor a la fecha final.");

                EgresosVarios_Listar(((Usuario_LoginResult)Session["Usuario"]).nroDocumento, dpFechaInicio.SelectedDate.Value, dpFechaFinal.SelectedDate.Value);

                lblMensaje.Text = "Se encontraron " + grdEgresosVarios.Items.Count.ToString() + " registros.";
                lblMensaje.CssClass = "mensajeExito";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void ramEgresosVarios_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Argument == "Rebind")
                {
                    grdEgresosVarios.MasterTableView.SortExpressions.Clear();
                    grdEgresosVarios.MasterTableView.GroupByExpressions.Clear();
                    EgresosVarios_Listar(((Usuario_LoginResult)Session["Usuario"]).nroDocumento, dpFechaInicio.SelectedDate.Value, dpFechaFinal.SelectedDate.Value);
                    grdEgresosVarios.DataBind();

                    lblMensaje.Text = "Se agregó el gasto al sistema.";
                    lblMensaje.CssClass = "mensajeExito";
                }

                if (e.Argument.Split('(')[0].Trim() == "RebindAndNavigate")
                {
                    //grdRecibos.MasterTableView.SortExpressions.Clear();
                    //grdRecibos.MasterTableView.GroupByExpressions.Clear();
                    //List<gsEgresosVarios_BuscarDetalleResult> lst = JsonHelper.JsonDeserialize<List<gsEgresosVarios_BuscarDetalleResult>>((string)ViewState["lstEVDetalle"]);
                    //string strEVDetalle = "{" + e.Argument.Split('{')[1];
                    //gsEgresosVarios_BuscarDetalleResult objEVDetalle = JsonHelper.JsonDeserialize<gsEgresosVarios_BuscarDetalleResult>(strEVDetalle.Substring(0, strEVDetalle.Length - 1));
                    //if (objEVDetalle.ID_Amarre == 0)
                    //    objEVDetalle.ID_Amarre = (lst.FindAll(x => x.ID_Amarre <= 0).Count + 1) * -1;
                    //lst.Remove(lst.Find(x => x.ID_Amarre == objEVDetalle.ID_Amarre));
                    //lst.Add(objEVDetalle);

                    //grdRecibos.DataSource = lst.OrderBy(x => x.ID_Amarre);
                    //grdRecibos.DataBind();

                    //ViewState["lstEVDetalle"] = JsonHelper.JsonSerializer(lst);
                    //if (objEVDetalle.ID_Amarre > 0)
                    //    lblMensaje.Text = "Se modificó el gastos con código " + objEVDetalle.ID_Amarre.ToString();
                    //else
                    //    lblMensaje.Text = "Se registró el gastos con código " + objEVDetalle.ID_Amarre.ToString();
                    //lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
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

        protected void grdEgresosVarios_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    if (item["Ok1"].Text == "False")
                    {
                        ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/Images/Icons/sign-error-16.png";
                        ((Image)e.Item.FindControl("imgEstado")).ToolTip = "Por aprobar";
                    }
                    else
                    {
                        ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/Images/Icons/sign-check-16.png";
                        ((Image)e.Item.FindControl("imgEstado")).ToolTip = "Aprobado";
                        item["Elim"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdEgresosVarios_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            EgresosWCFClient objEgresosWCF = new EgresosWCFClient();
            try
            {
                objEgresosWCF.EgresosVarios_Eliminar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, Convert.ToInt32(((GridDataItem)e.Item).GetDataKeyValue("Op")));
                EgresosVarios_Listar(((Usuario_LoginResult)Session["Usuario"]).nroDocumento, dpFechaInicio.SelectedDate.Value, dpFechaFinal.SelectedDate.Value);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}