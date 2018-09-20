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
    public partial class frmMetaPresupuestoPromAdd : System.Web.UI.Page
    {
        PlanificacionWCFClient objPlanificacion = new PlanificacionWCFClient();
        string strId_Vendedor, strAnno, strMes;
        List<USP_Sel_MetaPresupuestoPromotorResult> _lstpresupuestoProm = new List<USP_Sel_MetaPresupuestoPromotorResult>();

        #region MetodosWeb
        //[WebMethod]
        //public static AutoCompleteBoxData CargarClientes(object context)
        //{
        //    AutoCompleteBoxData res = new AutoCompleteBoxData();
        //    string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
        //    if (searchString.Length > 2)
        //    {
        //        AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
        //        gsAgenda_ListarVendedorResult[] lst = objAgendaWCFClient.Agenda_ListarVendedor(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
        //            ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString);
        //        List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

        //        foreach (gsAgenda_ListarVendedorResult agenda in lst)
        //        {
        //            AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
        //            childNode.Text = agenda.ID_Agenda + "-" + agenda.Nombre;
        //            childNode.Value = agenda.ID_Agenda;
        //            result.Add(childNode);
        //        }
        //        res.Items = result.ToArray();
        //    }
        //    return res;
        //}

        #endregion

        #region Procedimientos
        private void CargarPresupuesto()
        {
            strId_Vendedor = Request.QueryString["Id_Vendedor"];
            strMes = Request.QueryString["Mes"];
            strAnno = Request.QueryString["Anno"];
            _lstpresupuestoProm = objPlanificacion.Obtener_MetaPresupuestoPromotor(
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario,
                Convert.ToInt32(strAnno),
                Convert.ToInt32(strMes),
                strId_Vendedor).ToList();
            gvwProductos.DataSource = _lstpresupuestoProm;
            gvwProductos.DataBind();

            Session["PresupuestoPromotor"] = JsonHelper.JsonSerializer(_lstpresupuestoProm);

            


        }

        private void CargarClientes()
        {
            List<USP_SEL_MetaPresupuestoPendienteResult> lst = new List<USP_SEL_MetaPresupuestoPendienteResult>();
            strId_Vendedor = Request.QueryString["Id_Vendedor"];
            strMes = Request.QueryString["Mes"];
            strAnno = Request.QueryString["Anno"];
            lst = objPlanificacion.Obtener_PresupuestoPendiente(
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario,
                Convert.ToInt32(strAnno),
                Convert.ToInt32(strMes),
                strId_Vendedor).ToList();

            var lstclientes = lst.Select(x => new
            {
                x.Id_Cliente,
                x.Cliente
            }).Distinct();


            cbcliente.DataSource = lstclientes;
            cbcliente.DataTextField = "Cliente";
            cbcliente.DataValueField = "Id_Cliente";
            cbcliente.DataBind();

            lblDiferencia.Text = Math.Round(Convert.ToDouble(lst.Sum(x => x.Total)),2).ToString();
        }

        private void CargarPromotores()
        {
            objPlanificacion = new PlanificacionWCFClient();
            string id_vendedor = Request.QueryString["Id_Vendedor"];
            List<USP_Sel_PromotoresxVendedorResult> lst = new List<USP_Sel_PromotoresxVendedorResult>();
            lst = objPlanificacion.Obtener_PromotoresxVendedor(
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario,
                id_vendedor
                ).ToList();

            cbpromotor.DataSource = lst;
            cbpromotor.DataTextField = "NombrePromotor";
            cbpromotor.DataValueField = "Id_Promotor";
            cbpromotor.DataBind();
        }

        private Boolean ValidarInformacion()
        {
            
            if(cbcliente.SelectedValue.ToString()==String.Empty)
            {
               
                rwmPre.RadAlert("Tiene que seleccionar un cliente", 400, null, "Validación de Información", null);
                return false;
            }
            else if(cbpromotor.SelectedValue.ToString()==string.Empty)
            {
                rwmPre.RadAlert("Tiene que seleccionar un promotor", 400, null, "Validación de Información", null);
                return false;
            }
            else if(txtmonto.Text.Trim()==string.Empty || Convert.ToDouble(txtmonto.Text==string.Empty ? "0" : txtmonto.Text)<=0)
            {
                rwmPre.RadAlert("El monto a ingresar debe ser mayor a cero", 400, null, "Validación de Información", null);
                return false;
            }
            return true;
            
        }

        private void AgregarMontoPromotor()
        {
            objPlanificacion = new PlanificacionWCFClient();
            //List<USP_Sel_MetaPresupuestoPromotorResult> lstprom = new List<USP_Sel_MetaPresupuestoPromotorResult>();
            List<USP_SEL_MetaPresupuestoPendienteResult> lstpendiente = new List<USP_SEL_MetaPresupuestoPendienteResult>();

            _lstpresupuestoProm = JsonHelper.JsonDeserialize<List<USP_Sel_MetaPresupuestoPromotorResult>>((string)Session["PresupuestoPromotor"]);

            strId_Vendedor = Request.QueryString["Id_Vendedor"];
            strMes = Request.QueryString["Mes"];
            strAnno = Request.QueryString["Anno"];
            lstpendiente = objPlanificacion.Obtener_PresupuestoPendiente(
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario,
                Convert.ToInt32(strAnno),
                Convert.ToInt32(strMes),
                strId_Vendedor).ToList();

            double TotalPromotor = Math.Round(Convert.ToDouble(_lstpresupuestoProm.Sum(x => x.Total)),2);
            double TotalPendiente= Math.Round(Convert.ToDouble(lstpendiente.Sum(x => x.Total)),2);


            if(TotalPendiente<(TotalPromotor)+Convert.ToDouble(txtmonto.Text))
            {
                rwmPre.RadAlert("El monto total asignado a los promotores es superior al monto total del presupuesto", 400, null, "Validación de Información", null);
            }
            else if(_lstpresupuestoProm.Where(x=> x.Id_Cliente== cbcliente.SelectedValue.ToString() && x.Id_Promotor== cbpromotor.SelectedValue.ToString()).Count()>=1)
            {
                rwmPre.RadAlert("El promotor seleccionado ya tiene registrado un presupuesto al cliente seleccionado", 400, null, "Validación de Información", null);
            }
            else
            {
                lblDiferencia.Text = Math.Round(TotalPendiente - ((TotalPromotor) + Convert.ToDouble(txtmonto.Text)), 2).ToString();
                //_lstpresupuestoProm.Add(new USP_Sel_MetaPresupuestoPromotorResult
                //{
                //    Id=0,
                //    Anno=Convert.ToInt32(strAnno),
                //    Mes=Convert.ToInt32(strMes),
                //    Id_Vendedor=strId_Vendedor,
                //    Id_Cliente=cbcliente.SelectedValue.ToString(),
                //    NombreCliente=cbcliente.Text.Trim(),
                //    Id_Promotor=cbpromotor.SelectedValue.ToString(),
                //    NombrePromotor=cbpromotor.Text.Trim(),
                //    Aprobado=false,
                //    Total=Convert.ToDecimal(txtmonto.Text)
                //});

                objPlanificacion.Registrar_MetaPresupuestoPromotor(
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario,
                    Convert.ToInt32(strAnno), Convert.ToInt32(strMes), strId_Vendedor, cbcliente.SelectedValue.ToString(), cbpromotor.SelectedValue.ToString(), Convert.ToDecimal(txtmonto.Text), false);


                    
            }
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
                    Session["PresupuestoPromotor"] = null;
                    CargarClientes();
                    CargarPromotores();
                    CargarPresupuesto();
                    if (_lstpresupuestoProm.Any())
                        lblDiferencia.Text =
                            (Convert.ToDouble(lblDiferencia.Text == string.Empty ? "0" : lblDiferencia.Text) -
                            Convert.ToDouble(_lstpresupuestoProm.Sum(x => x.Total))).ToString();

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

        protected void gvwProductos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {

                string id;
                string Aprobado = string.Empty;



                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                id = commandArgs[0].ToString();
                Aprobado= commandArgs[1].ToString();




                if (e.CommandName == "EliminarProm")
                {
                    
                    if (!Convert.ToBoolean(Aprobado))
                    {
                        strId_Vendedor = Request.QueryString["Id_Vendedor"];
                        strMes = Request.QueryString["Mes"];
                        strAnno = Request.QueryString["Anno"];

                        objPlanificacion = new PlanificacionWCFClient();
                        objPlanificacion.Eliminar_MetaPresupuestoPromotor(
                            ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                            ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario,
                            Convert.ToInt32(strAnno), Convert.ToInt32(strMes), strId_Vendedor, Convert.ToInt32(id)
                            );
                        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ClearText();", true);
                        CargarPresupuesto();

                        List<USP_SEL_MetaPresupuestoPendienteResult> lst = new List<USP_SEL_MetaPresupuestoPendienteResult>();
                        strId_Vendedor = Request.QueryString["Id_Vendedor"];
                        strMes = Request.QueryString["Mes"];
                        strAnno = Request.QueryString["Anno"];
                        lst = objPlanificacion.Obtener_PresupuestoPendiente(
                            ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                            ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario,
                            Convert.ToInt32(strAnno),
                            Convert.ToInt32(strMes),
                            strId_Vendedor).ToList();

                        lblDiferencia.Text =(Convert.ToDouble(lst.Sum(x=> x.Total)) - Convert.ToDouble(_lstpresupuestoProm.Sum(x => x.Total))).ToString();
                        
                        
                    }
                    else
                    {
                        rwmPre.RadAlert("El presupuesto pro promotor esta aprobado, imposible realizar cambios.", 400, null, "Mensaje de error", null);
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void gvwProductos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            
        }

        protected void btncerrar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(1);", true);
                
            }
            catch (Exception ex)
            {
                rwmPre.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

        protected void btnagregar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (ValidarInformacion())
                {
                    AgregarMontoPromotor();
                    CargarPresupuesto();
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ClearText();", true);
                    txtmonto.Text = "0";
                }
            }
            catch (Exception ex)
            {
                rwmPre.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

        
    }
}