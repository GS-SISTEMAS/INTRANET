using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.PlanificacionWCF;
using Telerik.Web.UI;

namespace GS.SISGEGS.Web.Contabilidad.Planificacion
{
    public partial class frmCierreContableMng : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (!Page.IsPostBack)
                {

                    if (string.IsNullOrEmpty(Request.QueryString["objCierreContable"]))
                    {
                        LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                        objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                            ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                        Title = "Registrar Cierre Contable";

                        var lstCierreByPlan = CargarGridInsert();
                        grdCierreMng.DataSource = lstCierreByPlan;
                        grdCierreMng.DataBind();

                        HttpContext.Current.Session["lstCierreByPlan"] = lstCierreByPlan;
                        ViewState["idPlanificacion"] = string.Empty;

                        lblMensaje.Text = "Listo para registrar Cierre Contable";
                        lblMensaje.CssClass = "mensajeExito";
                    }
                    else
                    {
                        Title = "Modificar perfil";
                        string obj = Request.QueryString["objPobjCierreContableerfil"];
                        GS_GetPlanDetalleToInsertResult objCierreContable = JsonHelper.JsonDeserialize<GS_GetPlanDetalleToInsertResult>(Request.QueryString["objCierreContable"]);
                        ViewState["idPlanificacion"] = objCierreContable.idPlanificacion;
                        CargarGridEdit();
                        dpPeriodo.SelectedDate = DateTime.Now;
                        lblMensaje.Text = "Listo para modificar Cierre Contable";
                        lblMensaje.CssClass = "mensajeExito";
                    }
                }
                else
                {
                    if (ViewState["txtInicio"]!=null)
                        RadTextBox1.Text = ViewState["txtInicio"].ToString();
                    if (ViewState["txtFin"] != null) RadTextBox2.Text = ViewState["txtFin"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        public List<GS_GetPlanDetalleToInsertResult> CargarGridInsert()
        {
            List<GS_GetPlanDetalleToInsertResult> lstCierreByPlan;
            PlanificacionWCFClient objCierreByPlanWCF = new PlanificacionWCFClient();
            try
            {
                var idEmpresa = ((Usuario_LoginResult) Session["Usuario"]).idEmpresa;
                var codigoUsuario = ((Usuario_LoginResult) Session["Usuario"]).codigoUsuario;

                lstCierreByPlan = objCierreByPlanWCF.GetPlanDetalleToInsert(idEmpresa, codigoUsuario).ToList();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstCierreByPlan;
        }

        public List<GS_GetPlanDetalleToEditResult> CargarGridEdit()
        {
            List<GS_GetPlanDetalleToEditResult> lstCierreByPlan;
            PlanificacionWCFClient objCierreByPlanWCF = new PlanificacionWCFClient();
            try
            {
                var idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                var codigoUsuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
                var idPlanificacion = ViewState["idPlanificacion"].ToString();

                lstCierreByPlan = objCierreByPlanWCF.GetPlanDetalleToEdit(idEmpresa, codigoUsuario, idPlanificacion).ToList();
                HttpContext.Current.Session["lstCierreByPlan"] = lstCierreByPlan;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstCierreByPlan;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            PlanificacionWCFClient objCierreByPlanWCF = new PlanificacionWCFClient();
            int idPlanificacion = 0;
            try
            {
                //if (Request.QueryString["objCierreContable"] != "")
                //    idPlanificacion = (int)ViewState["idPlanificacion"];

                if (VerificaInsertOrUpdate())
                {
                    List<GS_GetPlanDetalleToInsertResult> lstCierreByPlan =
                        (List<GS_GetPlanDetalleToInsertResult>) HttpContext.Current.Session["lstCierreByPlan"];
                    var periodo = dpPeriodo.SelectedDate.Value.Month.ToString("D2") + "/" + dpPeriodo.SelectedDate.Value.Year;
                    var fechaInicial = lstCierreByPlan.Min(x => x.FechaCierre);
                    var fechaFinal = lstCierreByPlan.Max(x => x.FechaCierre);

                    var idEmpresa = ((Usuario_LoginResult) Session["Usuario"]).idEmpresa;
                    var codigoUsuario = ((Usuario_LoginResult) Session["Usuario"]).codigoUsuario;
                    var nroDocumento = ((Usuario_LoginResult)Session["Usuario"]).nroDocumento;

                    idPlanificacion = objCierreByPlanWCF.PlanificacionCabecera_Insertar(idEmpresa, codigoUsuario, periodo, fechaInicial.Value, fechaFinal.Value, nroDocumento);

                    var lstToDb = lstCierreByPlan.Where(x => x.idPlanificacion == 0 && x.FechaCierre.HasValue);

                    if(lstCierreByPlan.Exists(x => !x.FechaCierre.HasValue))
                        rwmCierre.RadAlert("No se ha ingresado una fecha", 500, 100, "Validación de fechas", "");

                    foreach (var item in lstToDb)
                    {
                        objCierreByPlanWCF.PlanificacionDetalle_Insertar(idEmpresa, codigoUsuario, item.id_Modulo,
                            idPlanificacion, item.FechaCierre.Value, item.Detalle, item.Observacion, item.Estado,
                            codigoUsuario.ToString());
                    }
                }

                else

                {
                    List<GS_GetPlanDetalleToEditResult> lstCierreByPlan =
                        (List<GS_GetPlanDetalleToEditResult>) HttpContext.Current.Session["lstCierreByPlan"];

                    var periodo = dpPeriodo.SelectedDate.Value.Month.ToString("D2") + "/" + dpPeriodo.SelectedDate.Value.Year;
                    var fechaInicial = lstCierreByPlan.Min(x => x.FechaCierre);
                    var fechaFinal = lstCierreByPlan.Max(x => x.FechaCierre);

                    var idEmpresa = ((Usuario_LoginResult) Session["Usuario"]).idEmpresa;
                    var codigoUsuario = ((Usuario_LoginResult) Session["Usuario"]).codigoUsuario;
                    var nroDocumento = ((Usuario_LoginResult)Session["Usuario"]).nroDocumento;

                    idPlanificacion = Convert.ToInt32(ViewState["idPlanificacion"].ToString());

                    objCierreByPlanWCF.PlanificacionCabecera_Update(idEmpresa, codigoUsuario, idPlanificacion, fechaInicial.Value, fechaFinal.Value, nroDocumento);

                    var lstToDb = lstCierreByPlan.Where(x => x.idPlanificacion == 0 && x.FechaCierre.HasValue);
                    if (lstCierreByPlan.Exists(x => !x.FechaCierre.HasValue))
                        rwmCierre.RadAlert("No se ha ingresado una fecha", 500, 100, "Validación de fechas", "");

                    foreach (var item in lstToDb)
                    {
                        objCierreByPlanWCF.PlanificacionDetalle_Insertar(idEmpresa, codigoUsuario, item.id_Modulo,
                            idPlanificacion, item.FechaCierre.Value, item.Detalle, item.Observacion, item.Estado.Value,
                            codigoUsuario.ToString());
                    }
                }
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind();", true);

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdCierreMng_ItemDataBound(object sender, GridItemEventArgs e)
        {
           
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem) e.Item;
                Label txtProcessStatus = e.Item.FindControl("Detalle") as Label;
                bool flag;
                DateTime? date;
                if (VerificaInsertOrUpdate())
                {
                    
                    flag = ((GS_GetPlanDetalleToInsertResult) (e.Item.DataItem)).flag.Value;
                    
                    date = ((GS_GetPlanDetalleToInsertResult)(e.Item.DataItem)).FechaCierre;
                }
                else
                {
                    flag = ((GS_GetPlanDetalleToEditResult)(e.Item.DataItem)).flag.Value;
                    date = ((GS_GetPlanDetalleToEditResult)(e.Item.DataItem)).FechaCierre;
                }
                if (flag && date!=null)
                {
                    LinkButton img = (LinkButton)item["EditCommandColumn"].Controls[0]; //Accessing EditCommandColumn
                    img.Visible = false;
                }
            }
        }

        private List<GS_GetPlanificacionDetalleByIdPlanResult> CierreContableByPlan_Cargar(int idEmpresa, int codigoUsuario, string idPlanificacion)
        {
            PlanificacionWCFClient objCierreByPlanWCF = new PlanificacionWCFClient();
            try
            {
                List<GS_GetPlanificacionDetalleByIdPlanResult> lstCierreByPlan = objCierreByPlanWCF.GetPlanificacionDetalleByIdPlan(idEmpresa, codigoUsuario, idPlanificacion).ToList();

                ViewState["lstCierreByPlan"] = JsonHelper.JsonSerializer(lstCierreByPlan);
                return lstCierreByPlan;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void grdCierreMng_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (VerificaInsertOrUpdate())
                {
                    List<GS_GetPlanDetalleToInsertResult> lstCierreByPlan =
                        (List<GS_GetPlanDetalleToInsertResult>) HttpContext.Current.Session["lstCierreByPlan"];
                    grdCierreMng.DataSource = lstCierreByPlan;

                    var fechaInicial = lstCierreByPlan.Min(x => x.FechaCierre);
                    var fechaFinal = lstCierreByPlan.Max(x => x.FechaCierre);

                    if (fechaInicial != null) RadTextBox1.Text = fechaInicial.Value.ToShortDateString();
                    if (fechaFinal != null) RadTextBox2.Text = fechaFinal.Value.ToShortDateString();
                }
                else
                {
                    List<GS_GetPlanDetalleToEditResult> lstCierreByPlan =
                        (List<GS_GetPlanDetalleToEditResult>)HttpContext.Current.Session["lstCierreByPlan"];
                    grdCierreMng.DataSource = lstCierreByPlan;

                    var fechaInicial = lstCierreByPlan.Min(x => x.FechaCierre);
                    var fechaFinal = lstCierreByPlan.Max(x => x.FechaCierre);

                    if (fechaInicial != null) RadTextBox1.Text = fechaInicial.Value.ToShortDateString();
                    if (fechaFinal != null) RadTextBox2.Text = fechaFinal.Value.ToShortDateString();
                }


                //TextBox1.Enabled = false;
                //TextBox2.Enabled = false;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }

        private bool VerificaInsertOrUpdate()
        {
            if (ViewState["idPlanificacion"] == null) return true;
            var idPlanificacion = ViewState["idPlanificacion"].ToString();
            return string.IsNullOrEmpty(idPlanificacion);
        }

        protected void RadGrid1_BatchEditCommand(object sender, Telerik.Web.UI.GridBatchEditingEventArgs e)
        {
            //SavedChangesList.Visible = true;
            var id = e.Commands.Select(x => x.NewValues["id_Modulo"]);

        }

        protected void RadGrid1_ItemUpdated(object source, Telerik.Web.UI.GridUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.KeepInEditMode = true;
                e.ExceptionHandled = true;
                DisplayMessage(true, "Customer " + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CustomerID"] + " cannot be updated due to invalid data.");
            }
            else
            {
                DisplayMessage(false, "Customer " + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CustomerID"] + " updated");
            }
        }

        private void DisplayMessage(bool isError, string text)
        {
            //if (isError)
            //{
            //    this.Label1.Text = text;
            //    this.Label3.Text = text;
            //}
            //else
            //{
            //    this.Label2.Text = text;
            //    this.Label4.Text = text;
            //}
        }

       protected void RadGrid1_ItemDeleted(object source, GridDeletedEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.Item;
            String id = dataItem.GetDataKeyValue("ProductID").ToString();
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;
                NotifyUser("Product with ID " + id + " cannot be deleted. Reason: " + e.Exception.Message);
            }
            else
            {
                NotifyUser("Product with ID " + id + " is deleted!");
            }

        }

       protected void grdCierreMng_PreRender(object sender, EventArgs e)
        {
            //RadNumericTextBox unitsNumericTextBox = (grdCierreMng.MasterTableView.GetBatchColumnEditor("UnitsInStock") as GridNumericColumnEditor).NumericTextBox;
            //unitsNumericTextBox.Width = Unit.Pixel(60);
            if (!IsPostBack)
            {
                foreach (GridItem item in grdCierreMng.MasterTableView.Items)
                {
                    if (item is GridEditableItem)
                    {
                        GridEditableItem editableItem = item as GridDataItem;
                        TableCell cell = (TableCell)editableItem["NombreEstado"];
                        if (!string.IsNullOrEmpty(cell.Text))
                        {
                            editableItem.Edit = false;
                        }
                    }
                }
                grdCierreMng.Rebind();
            }
        }

        private void NotifyUser(string message)
        {
            RadListBoxItem commandListItem = new RadListBoxItem();
            commandListItem.Text = message;
            //SavedChangesList.Items.Add(commandListItem);
        }

        protected void grdCierreMng_OnEditCommand(object sender, GridCommandEventArgs e)
        {
            var aux = e;
        }

        protected void grdCierreMng_ItemCreated(object sender, GridItemEventArgs e)
        {
            //(this.grdCierreMng.MasterTableView.AutoGeneratedColumns[0] as GridBoundColumn).MaxLength = 5;
            GridEditableItem item = e.Item as GridEditableItem;

            //NotifyUser("Product with ID " + id + " is deleted!");
            if (item != null && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {
                (item.EditManager.GetColumnEditor("id_Modulo").ContainerControl.Controls[0] as TextBox).Enabled = false;
                (item.EditManager.GetColumnEditor("Modulo").ContainerControl.Controls[0] as TextBox).Enabled = false;
                (item.EditManager.GetColumnEditor("NombreEstado").ContainerControl.Controls[0] as TextBox).Enabled = false;
                (item.EditManager.GetColumnEditor("Responsable").ContainerControl.Controls[0] as TextBox).Enabled = false;
                (item.EditManager.GetColumnEditor("flag").ContainerControl.Controls[0] as CheckBox).Checked = true;

            }

        }

        protected void grdCierreMng_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var editableItem = ((GridEditableItem)e.Item);
            //create new entity
            var product = new GS_GetPlanDetalleToInsertResult();
            //populate its properties
            Hashtable values = new Hashtable();
            editableItem.ExtractValues(values);
            product.Observacion = (string)values["Observacion"];
            if (values["Detalle"] != null)
            {
                product.Detalle = (string)(values["Detalle"].ToString());
            }
            if (values["FechaCierre"] != null)
            {
                product.FechaCierre = DateTime.Parse(values["UnitPrice"].ToString());
            }
        }

       

        protected void grdCierreMng_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editableItem = ((GridEditableItem)e.Item);
            var productId = (int)editableItem.GetDataKeyValue("id_Modulo");

            if (VerificaInsertOrUpdate())
            {
                List<GS_GetPlanDetalleToInsertResult> lstCierreByPlan =
                    (List<GS_GetPlanDetalleToInsertResult>) HttpContext.Current.Session["lstCierreByPlan"];
                var index = lstCierreByPlan.FindIndex(n => n.id_Modulo == productId);

                //retrive entity form the Db
                var product = lstCierreByPlan.FirstOrDefault(n => n.id_Modulo == productId);
                if (product != null)
                {
                    //update entity's state
                    editableItem.UpdateValues(product);
                    lstCierreByPlan[index] = product;
                    HttpContext.Current.Session["lstCierreByPlan"] = lstCierreByPlan;
                    var fechaInicial = lstCierreByPlan.Min(x => x.FechaCierre);
                    var fechaFinal = lstCierreByPlan.Max(x => x.FechaCierre);

                    if (fechaInicial != null)
                    {
                        ViewState["txtInicio"] = fechaInicial.Value.ToShortDateString();
                        RadTextBox1.Text = fechaInicial.Value.ToShortDateString();
                        
                    }
                    if (fechaFinal != null)
                    {
                        ViewState["txtFin"] = fechaFinal.Value.ToShortDateString();
                        RadTextBox2.Text = fechaFinal.Value.ToShortDateString();
                    }


                    //TextBox1.Enabled = false;
                    //TextBox2.Enabled = false;

                }
            }
            else
            {
                
                    List<GS_GetPlanDetalleToEditResult> lstCierreByPlan =
                        (List<GS_GetPlanDetalleToEditResult>)HttpContext.Current.Session["lstCierreByPlan"];
                    var index = lstCierreByPlan.FindIndex(n => n.id_Modulo == productId);

                    //retrive entity form the Db
                    var product = lstCierreByPlan.FirstOrDefault(n => n.id_Modulo == productId);
                    if (product != null)
                    {
                        //update entity's state
                        editableItem.UpdateValues(product);
                        lstCierreByPlan[index] = product;
                        HttpContext.Current.Session["lstCierreByPlan"] = lstCierreByPlan;
                        var fechaInicial = lstCierreByPlan.Min(x => x.FechaCierre);
                        var fechaFinal = lstCierreByPlan.Max(x => x.FechaCierre);

                        if (fechaInicial != null) RadTextBox1.Text = fechaInicial.Value.ToShortDateString();
                        if (fechaFinal != null) RadTextBox2.Text = fechaFinal.Value.ToShortDateString();


                        RadTextBox1.Enabled = false;
                        RadTextBox2.Enabled = false;

                    }
                
            }
        }
    }
}