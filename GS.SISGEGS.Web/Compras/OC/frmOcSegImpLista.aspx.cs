using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

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
    public partial class frmOcSegImpLista : System.Web.UI.Page
    {
        OrdenCompraWCFClient objOrdenCompraWCF = new OrdenCompraWCFClient();
        #region PROCESOS
        private void CargarEstados()
        {
            List<USP_Sel_Genesys_OC_EstadoResult> lst = new List<USP_Sel_Genesys_OC_EstadoResult>();
            lst = objOrdenCompraWCF.Seleccionar_GenesysOC_Estados(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();

            
            cbestado.DataSource = lst;
            cbestado.DataTextField = "NombreEstado";
            cbestado.DataValueField = "Id_Estado";
            cbestado.DataBind();

            foreach (RadComboBoxItem itm in cbestado.Items)
                itm.Checked = true;

            
        }
        private void CargarSeguimiento()
        {
            DateTime fechainicial = Convert.ToDateTime(dpFechaInicio.SelectedDate);
            DateTime fechafinal = Convert.ToDateTime(dpFechaFinal.SelectedDate);
            DateTime? fechaingresoini = dpFechaIngresoIni.IsEmpty ? (DateTime?)null : dpFechaIngresoIni.SelectedDate;
            DateTime? fechaingresofin = dpFechaIngresoIni.IsEmpty ? (DateTime?)null : dpFechaIngresoFin.IsEmpty ? (DateTime?)null : dpFechaIngresoFin.SelectedDate;

            if(fechaingresoini ==(DateTime?)null || fechaingresofin==(DateTime?)null)
            {
                fechaingresoini = (DateTime?)null;
                fechaingresofin = (DateTime?)null;
            }
            string agendaNombre = txtproveedor.Text;
            string strestados = string.Empty;
            var collection = cbestado.CheckedItems;
            foreach(var x in collection)
                strestados = strestados + x.Value.ToString() + ",";
            
            strestados = strestados.Trim()==string.Empty ? string.Empty : strestados.Substring(0, strestados.Length - 1);
            
            objOrdenCompraWCF = new OrdenCompraWCFClient();
            
            
            List<USP_Sel_Genesys_Oc_SegImpResult> lst = new List<USP_Sel_Genesys_Oc_SegImpResult>();
            lst = objOrdenCompraWCF.Seleccionar_GenesysOC_SeguimientoLista(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                fechainicial, fechafinal, agendaNombre,strestados,fechaingresoini,fechaingresofin).ToList();
            gvwSeguimiento.DataSource = lst;
            gvwSeguimiento.DataBind();
            Session["lstseguimiento"] = JsonHelper.JsonSerializer(lst);
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

                    //dpFechaInicio.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    dpFechaInicio.SelectedDate = new DateTime(DateTime.Now.Year,DateTime.Now.Month, 1);
                    dpFechaFinal.SelectedDate = DateTime.Now;

                    Session["lstseguimiento"] = null;
                    CargarEstados();
                    CargarSeguimiento();

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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                    CargarSeguimiento();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void gvwSeguimiento_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (Session["lstseguimiento"] != null)
                {

                    gvwSeguimiento.DataSource = JsonHelper.JsonDeserialize<List<USP_Sel_Genesys_Oc_SegImpResult>>((string)Session["lstseguimiento"]);
                    //gvwSeguimiento.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void gvwSeguimiento_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                string Id_SegImp = string.Empty;
                Id_SegImp = e.CommandArgument.ToString();

                //ElimSeg
                if (e.CommandName == "EditarSeg")
                {
                    Response.Redirect("~/Compras/OC/frmOcSegImp.aspx?Id_SegImp=" + Id_SegImp);
                }
                if (e.CommandName == "ElimSeg")
                {
                   rwmSeg.RadConfirm("Eliminar?", "confirmCallBackFn", 330, 180, null, "Operacion",string.Empty);
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
                Response.Redirect("~/Compras/OC/frmOcSegImp.aspx?Id_SegImp=0");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            
        }

        protected void gvwSeguimiento_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    
                    Int32? dias = Convert.ToInt32(item["DiasLibresSE"].Text.Trim().Replace("&nbsp;", "")==string.Empty ? "-1" : item["DiasLibresSE"].Text);
                    
                    if (dias>=0)
                    {
                        //Int32 dias = Convert.ToInt32(item["DiasLibresSE"].Text == string.Empty ? "0" : item["DiasLibresSE"].Text);

                        
                        if (dias>=0 && dias <=10)
                        {
                            ((Image)e.Item.FindControl("imgSE")).ImageUrl = "~/Images/Icons/circle-red-16.png";
                        }
                        else if (dias >= 11 && dias <= 15)
                        {
                            ((Image)e.Item.FindControl("imgSE")).ImageUrl = "~/Images/Icons/circle-green-16.png";
                        }
                        else if (dias > 15)
                        {
                            ((Image)e.Item.FindControl("imgSE")).ImageUrl = "~/Images/Icons/circle-yellow-16.png";
                        }
                    }

                    Int32? diasAlm= Convert.ToInt32(item["DiasAlmacenaje"].Text.Trim().Replace("&nbsp;", "") == string.Empty ? "-1" : item["DiasAlmacenaje"].Text);
                    if (diasAlm >=0)
                    {
                        if (diasAlm >= 0 && diasAlm <= 10)
                        {
                            ((Image)e.Item.FindControl("imgALM")).ImageUrl = "~/Images/Icons/circle-red-16.png";
                        }
                        else if (diasAlm >= 11 && diasAlm <= 15)
                        {
                            ((Image)e.Item.FindControl("imgALM")).ImageUrl = "~/Images/Icons/circle-green-16.png";
                        }
                        else if (diasAlm > 15)
                        {
                            ((Image)e.Item.FindControl("imgALM")).ImageUrl = "~/Images/Icons/circle-yellow-16.png";
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

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {


                
                gvwSeguimiento.ExportSettings.FileName ="Seguimiento_" + DateTime.Now.ToString("ddMMyyyyhhmm");
                gvwSeguimiento.ExportSettings.ExportOnlyData = true;
                gvwSeguimiento.ExportSettings.IgnorePaging = true;
                gvwSeguimiento.ExportSettings.OpenInNewWindow = true;
                gvwSeguimiento.MasterTableView.ExportToExcel();
                
                Page.Response.ClearHeaders();
                Page.Response.ClearContent();
            }
            catch (Exception ex)
            {
                //ra.RadAl

            }
        }

        protected void gvwSeguimiento_PreRender(object sender, EventArgs e)
        {
            
            
            for (int rowIndex = gvwSeguimiento.Items.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridDataItem row = gvwSeguimiento.Items[rowIndex];
                GridDataItem previousRow = gvwSeguimiento.Items[rowIndex + 1];

                if (row["Id_SegImp"].Text == previousRow["Id_SegImp"].Text)
                {
                    row["Id_SegImp"].RowSpan = previousRow["Id_SegImp"].RowSpan < 2 ? 2 : previousRow["Id_SegImp"].RowSpan + 1;
                    previousRow["Id_SegImp"].Visible = false;
                    row["Id_SegImp"].CssClass = "border-left-style: solid;";

                    if (row["NombreEstado"].Text == previousRow["NombreEstado"].Text)
                    {

                        row["NombreEstado"].RowSpan = previousRow["NombreEstado"].RowSpan < 2 ? 2 : previousRow["NombreEstado"].RowSpan + 1;
                        previousRow["NombreEstado"].Visible = false;
                        row["NombreEstado"].CssClass = "border-left-style: solid;";
                    }

                    if (row["NombreProveedor"].Text == previousRow["NombreProveedor"].Text)
                    {
                        row["NombreProveedor"].RowSpan = previousRow["NombreProveedor"].RowSpan < 2 ? 2 : previousRow["NombreProveedor"].RowSpan + 1;
                        previousRow["NombreProveedor"].Visible = false;
                        row["NombreEstado"].CssClass = "border-left-style: solid;";
                    }

                    if (row["NombreAgente"].Text == previousRow["NombreAgente"].Text)
                    {
                        row["NombreAgente"].RowSpan = previousRow["NombreAgente"].RowSpan < 2 ? 2 : previousRow["NombreAgente"].RowSpan + 1;
                        previousRow["NombreAgente"].Visible = false;
                        row["NombreAgente"].CssClass = "border-left-style: solid;";
                    }

                    if (row["FechaETDAprox"].Text == previousRow["FechaETDAprox"].Text)
                    {
                        row["FechaETDAprox"].RowSpan = previousRow["FechaETDAprox"].RowSpan < 2 ? 2 : previousRow["FechaETDAprox"].RowSpan + 1;
                        previousRow["FechaETDAprox"].Visible = false;
                        row["FechaETDAprox"].CssClass = "border-left-style: solid;";
                    }

                    if (row["FechaETD"].Text == previousRow["FechaETD"].Text)
                    {
                        row["FechaETD"].RowSpan = previousRow["FechaETD"].RowSpan < 2 ? 2 : previousRow["FechaETD"].RowSpan + 1;
                        previousRow["FechaETD"].Visible = false;
                        row["FechaETD"].CssClass = "border-left-style: solid;";
                    }

                    if (row["FechaETA"].Text == previousRow["FechaETA"].Text)
                    {
                        row["FechaETA"].RowSpan = previousRow["FechaETA"].RowSpan < 2 ? 2 : previousRow["FechaETA"].RowSpan + 1;
                        previousRow["FechaETA"].Visible = false;
                        row["FechaETA"].CssClass = "border-left-style: solid;";
                    }

                    if (row["DiasLibresSE"].Text == previousRow["DiasLibresSE"].Text)
                    {
                        row["DiasLibresSE"].RowSpan = previousRow["DiasLibresSE"].RowSpan < 2 ? 2 : previousRow["DiasLibresSE"].RowSpan + 1;
                        previousRow["DiasLibresSE"].Visible = false;
                        row["DiasLibresSE"].CssClass = "border-left-style: solid;";
                    }

                    if (row["FechaSobreEstadia"].Text == previousRow["FechaSobreEstadia"].Text)
                    {
                        row["FechaSobreEstadia"].RowSpan = previousRow["FechaSobreEstadia"].RowSpan < 2 ? 2 : previousRow["FechaSobreEstadia"].RowSpan + 1;
                        previousRow["FechaSobreEstadia"].Visible = false;
                        row["FechaSobreEstadia"].CssClass = "border-left-style: solid;";
                    }

                    if (row["FechaIngresoAlm"].Text == previousRow["FechaIngresoAlm"].Text)
                    {
                        row["FechaIngresoAlm"].RowSpan = previousRow["FechaIngresoAlm"].RowSpan < 2 ? 2 : previousRow["FechaIngresoAlm"].RowSpan + 1;
                        previousRow["FechaIngresoAlm"].Visible = false;
                        row["FechaIngresoAlm"].CssClass = "border-left-style: solid;";
                    }

                    if (row["DiasSobreEstadia"].Text == previousRow["DiasSobreEstadia"].Text)
                    {
                        row["DiasSobreEstadia"].RowSpan = previousRow["DiasSobreEstadia"].RowSpan < 2 ? 2 : previousRow["DiasSobreEstadia"].RowSpan + 1;
                        previousRow["DiasSobreEstadia"].Visible = false;
                        row["DiasSobreEstadia"].CssClass = "border-left-style: solid;";
                    }

                    if (row["EstadoSobreEstadia"].Text == previousRow["EstadoSobreEstadia"].Text)
                    {
                        row["EstadoSobreEstadia"].RowSpan = previousRow["EstadoSobreEstadia"].RowSpan < 2 ? 2 : previousRow["EstadoSobreEstadia"].RowSpan + 1;
                        previousRow["EstadoSobreEstadia"].Visible = false;
                        row["EstadoSobreEstadia"].CssClass = "border-left-style: solid;";
                    }

                    if (row["NombreVia"].Text == previousRow["NombreVia"].Text)
                    {
                        row["NombreVia"].RowSpan = previousRow["NombreVia"].RowSpan < 2 ? 2 : previousRow["NombreVia"].RowSpan + 1;
                        previousRow["NombreVia"].Visible = false;
                        row["NombreVia"].CssClass = "border-left-style: solid;";
                    }

                    if (row["DiasAlmacenaje"].Text == previousRow["DiasAlmacenaje"].Text)
                    {
                        row["DiasAlmacenaje"].RowSpan = previousRow["DiasAlmacenaje"].RowSpan < 2 ? 2 : previousRow["DiasAlmacenaje"].RowSpan + 1;
                        previousRow["DiasAlmacenaje"].Visible = false;
                        row["DiasAlmacenaje"].CssClass = "border-left-style: solid;";
                    }

                    if (row["NumeroDua"].Text == previousRow["NumeroDua"].Text)
                    {
                        row["NumeroDua"].RowSpan = previousRow["NumeroDua"].RowSpan < 2 ? 2 : previousRow["NumeroDua"].RowSpan + 1;
                        previousRow["NumeroDua"].Visible = false;
                        row["NumeroDua"].CssClass = "border-left-style: solid;";
                    }

                    if (row["NumeroBL"].Text == previousRow["NumeroBL"].Text)
                    {
                        row["NumeroBL"].RowSpan = previousRow["NumeroBL"].RowSpan < 2 ? 2 : previousRow["NumeroBL"].RowSpan + 1;
                        previousRow["NumeroBL"].Visible = false;
                        row["NumeroBL"].CssClass = "border-left-style: solid;";
                    }

                    if (row["LinkDua"].Text == previousRow["LinkDua"].Text)
                    {
                        row["LinkDua"].RowSpan = previousRow["LinkDua"].RowSpan < 2 ? 2 : previousRow["LinkDua"].RowSpan + 1;
                        previousRow["LinkDua"].Visible = false;
                        row["LinkDua"].CssClass = "border-left-style: solid;";
                    }

                    if (row["imgALM"].Text == previousRow["imgALM"].Text)
                    {
                        row["imgALM"].RowSpan = previousRow["imgALM"].RowSpan < 2 ? 2 : previousRow["imgALM"].RowSpan + 1;
                        previousRow["imgALM"].Visible = false;
                        row["imgALM"].CssClass = "border-left-style: solid;";
                    }

                    if (row["imgSE"].Text == previousRow["imgSE"].Text)
                    {
                        row["imgSE"].RowSpan = previousRow["imgSE"].RowSpan < 2 ? 2 : previousRow["imgSE"].RowSpan + 1;
                        previousRow["imgSE"].Visible = false;
                        row["imgSE"].CssClass = "border-left-style: solid;";
                    }

                    if (row["ibEditar"].Text == previousRow["ibEditar"].Text)
                    {
                        row["ibEditar"].RowSpan = previousRow["ibEditar"].RowSpan < 2 ? 2 : previousRow["ibEditar"].RowSpan + 1;
                        previousRow["ibEditar"].Visible = false;
                        row["ibEditar"].CssClass = "border-left-style: solid;";
                    }

                    if (row["Liquidacion"].Text == previousRow["Liquidacion"].Text)
                    {
                        row["Liquidacion"].RowSpan = previousRow["Liquidacion"].RowSpan < 2 ? 2 : previousRow["Liquidacion"].RowSpan + 1;
                        previousRow["Liquidacion"].Visible = false;
                        row["Liquidacion"].CssClass = "border-left-style: solid;";
                    }

                    if (row["FechaLiquidacion"].Text == previousRow["FechaLiquidacion"].Text)
                    {
                        row["FechaLiquidacion"].RowSpan = previousRow["FechaLiquidacion"].RowSpan < 2 ? 2 : previousRow["FechaLiquidacion"].RowSpan + 1;
                        previousRow["FechaLiquidacion"].Visible = false;
                        row["FechaLiquidacion"].CssClass = "border-left-style: solid;";
                    }

                    if (row["CantidadContenedor"].Text == previousRow["CantidadContenedor"].Text)
                    {
                        row["CantidadContenedor"].RowSpan = previousRow["CantidadContenedor"].RowSpan < 2 ? 2 : previousRow["CantidadContenedor"].RowSpan + 1;
                        previousRow["CantidadContenedor"].Visible = false;
                        row["CantidadContenedor"].CssClass = "border-left-style: solid;";
                    }
                    if (row["Anno"].Text == previousRow["Anno"].Text)
                    {
                        row["Anno"].RowSpan = previousRow["Anno"].RowSpan < 2 ? 2 : previousRow["Anno"].RowSpan + 1;
                        previousRow["Anno"].Visible = false;
                        row["Anno"].CssClass = "border-left-style: solid;";
                    }
                }
            }
        }
    }
}