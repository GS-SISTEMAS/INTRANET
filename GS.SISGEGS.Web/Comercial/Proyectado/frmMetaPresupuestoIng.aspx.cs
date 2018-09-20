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
using System.Drawing;

namespace GS.SISGEGS.Web.Comercial.Proyectado
{
    public partial class frmMetaPresupuestoIng : System.Web.UI.Page
    {
        List<USP_Sel_MetaPresupuestoDetResult> _lstdetalle = new List<USP_Sel_MetaPresupuestoDetResult>();
        PlanificacionWCFClient objPlanificacion = new PlanificacionWCFClient();
        List<USP_Sel_MetaPresupuestoResult> _lstbusquedaPre = new List<USP_Sel_MetaPresupuestoResult>();

        #region CargarProcedimientos
        private void CargarVendedorZona()
        {
            AgendaWCFClient objAgendaWCF = new AgendaWCFClient();
            
            List<gsVendedorZona_ListarResult> lstVendedor;

            lstVendedor = objAgendaWCF.Agenda_VendedorZonaListar(
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, 0,"0").ToList();


            if (lstVendedor.Where(x => x.ID_Agenda.Trim() == ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).nroDocumento).Any())
            {
                txt_idvendedor.Value= lstVendedor.Where(x => x.ID_Agenda.Trim() == ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).nroDocumento).Select(x => x.ID_Agenda).First();
                txtvendedor.Text = lstVendedor.Where(x => x.ID_Agenda.Trim() == ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).nroDocumento).Select(x => x.AgendaNombre).First();
                txtzona.Text= lstVendedor.Where(x => x.ID_Agenda.Trim() == ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).nroDocumento).Select(x => x.Zona).First();
                txt_idzona.Value= lstVendedor.Where(x => x.ID_Agenda.Trim() == ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).nroDocumento).Select(x => x.ID_Zona.ToString()).First();
            }
        }

        private bool ValidarInformacion()
        {
            Boolean estado = true;
            if(txtCantidad2.Text.Trim()==string.Empty)
            {
                rwmPre.RadAlert("Debe llenar el campo cantidad", 400, null, "Mensaje de error", null);
                estado = false;
            }
            else if(txtprecio2.Text.Trim()==string.Empty)
            {
                rwmPre.RadAlert("Debe llenar el campo precio", 400, null, "Mensaje de error", null);
                estado = false;
            }
            else if(Convert.ToDouble(txtCantidad2.Text)<=0)
            {
                rwmPre.RadAlert("La cantidad debe ser mayor a 0", 400, null, "Mensaje de error", null);
                estado = false;
            }
            else if (Convert.ToDouble(txtprecio2.Text) <= 0)
            {
                rwmPre.RadAlert("El precio debe ser mayor a 0", 400, null, "Mensaje de error", null);
                estado = false;
            }
            else if(abcProducto.Text.Trim()==String.Empty)
            {
                rwmPre.RadAlert("Debe ingresar un producto", 400, null, "Mensaje de error", null);
                estado = false;
            }
            else if(acbCliente.Text.Trim()==string.Empty)
            {
                rwmPre.RadAlert("Debe ingresar un cliente", 400, null, "Mensaje de error", null);
                estado = false;
            }

            return estado;
        }

        private void AgregarItemPresupuesto()
        {
            objPlanificacion = new PlanificacionWCFClient();
            _lstdetalle = new List<USP_Sel_MetaPresupuestoDetResult>();


            USP_Sel_MetaPresupuestoDetResult edetalle = new USP_Sel_MetaPresupuestoDetResult();

            if(Session["lstdetalle"] != null)
                _lstdetalle=JsonHelper.JsonDeserialize<List<USP_Sel_MetaPresupuestoDetResult>>((string)Session["lstdetalle"]);
            edetalle.Id = 0;
            
            edetalle.Anno = rmyPre.SelectedDate.Value.Year;
            edetalle.Mes = rmyPre.SelectedDate.Value.Month;
            edetalle.Id_Vendedor = txt_idvendedor.Value.ToString();
            edetalle.Id_Cliente=acbCliente.Entries[0].Text.Split('-')[0];
            edetalle.TipoCliente = objPlanificacion.Obtener_TipoCliente(
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, edetalle.Id_Cliente).Propiedad.ToString();
            edetalle.Cliente = acbCliente.Entries[0].Text.Split('-')[1];
            edetalle.CodigoProducto = string.Empty;
            edetalle.Id_G5= Convert.ToInt32(abcProducto.Entries[0].Text.Split('-')[0]);
            edetalle.Kardex = 0;
            edetalle.NombreKardex= abcProducto.Entries[0].Text.Split('-')[1];
            edetalle.Cantidad = Convert.ToDecimal(txtCantidad2.Text == string.Empty ? "0" : txtCantidad2.Text);
            edetalle.NombreG5= abcProducto.Entries[0].Text.Split('-')[1];
            edetalle.Precio = Convert.ToDecimal(txtprecio2.Text == string.Empty ? "0" : txtprecio2.Text);
            edetalle.Total = Convert.ToDecimal(Math.Round((Convert.ToDecimal(edetalle.Cantidad) * Convert.ToDecimal(edetalle.Precio)), 3));

            if(_lstdetalle.Where(x=> x.Id_Cliente== edetalle.Id_Cliente && x.Id_G5== edetalle.Id_G5).ToList().Any())
            {
                rwmPre.RadAlert("El codigo de producto para el cliente " + edetalle.Cliente + " ya fue ingresado", 400, null, "Mensaje de error", null);
                return;
            }
            _lstdetalle.Add(edetalle);
            rmyPre.Enabled = false;
            gvwItems.DataSource = _lstdetalle;
            gvwItems.DataBind();
            Session["lstdetalle"] = JsonHelper.JsonSerializer(_lstdetalle);
            txtCantidad2.Text = "0";
            txtprecio2.Text = "0";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "StringText();", true);

        }

        private void AprobarPresupuesto()
        {
            objPlanificacion = new PlanificacionWCFClient();
            objPlanificacion.Aprobar_MetaPresupuesto(
                 ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario,
                rmyPre.SelectedDate.Value.Year,
                rmyPre.SelectedDate.Value.Month,
                txt_idvendedor.Value.ToString(), true, Dns.GetHostName()
                );
            CargarPresupuesto(rmyPre.SelectedDate.Value.Year,
                rmyPre.SelectedDate.Value.Month,
                txt_idvendedor.Value.ToString());


        }
        private Boolean GuardarPresupuesto()
        {
            if (_lstdetalle == null)
            {
                rwmPre.RadAlert("No hay Items para guardar", 400, null, "Mensaje de error", null);
                return false;
            }
            if (!_lstdetalle.Any())
            {
                rwmPre.RadAlert("No hay Items para guardar", 400, null, "Mensaje de error", null);
                return false;
            }
            if (txt_idzona.Value.ToString() == string.Empty || txt_idzona.Value.ToString() == "0")
            {
                rwmPre.RadAlert("No tiene una Zona asignada, consulte con su sectorista", 400, null, "Mensaje de error", null);
                return false;
            }
            if(txtvendedor.Text.Trim()==string.Empty)
            {
                rwmPre.RadAlert("No tiene una Zona asignada, consulte con su sectorista", 400, null, "Mensaje de error", null);
                return false;
            }


            

            //_lstdetalle = JsonHelper.JsonDeserialize<List<USP_Sel_MetaPresupuestoDetResult>>((string)Session["lstdetalle"]);

            objPlanificacion.Registrar_MetaPresupuesto(
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario,
                rmyPre.SelectedDate.Value.Year,
                rmyPre.SelectedDate.Value.Month,
                txt_idvendedor.Value.ToString(),
                Convert.ToInt32(txt_idzona.Value.ToString()),
                new DateTime(rmyPre.SelectedDate.Value.Year, rmyPre.SelectedDate.Value.Month, 1),
                false, Dns.GetHostName(), _lstdetalle.ToArray());

            lblMensaje.Text = "Se registro correctamente";
            lblMensaje.CssClass = "mensajeExito";

            return true;

        }

        private void CargarPresupuesto(int anno,int mes,string id_vendedor)
        {
            USP_Sel_MetaPresupuestoCabResult eCab = new USP_Sel_MetaPresupuestoCabResult();
            _lstdetalle = new List<USP_Sel_MetaPresupuestoDetResult>();
            USP_Sel_MetaPresupuestoDetResult[] lstdet = null;

             objPlanificacion = new PlanificacionWCFClient();
            objPlanificacion.Obtener_MetaPresupuestoCabDet(
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, anno, mes, id_vendedor, ref eCab, ref lstdet);
            _lstdetalle = lstdet.ToList();
            CargarVendedorZona();
            rmyPre.SelectedDate = new DateTime(eCab.Anno, eCab.Mes,1);
            lblestado.Text = eCab.Aprobado == true ? "Aprobado" : "Registrado";
            
            rmyPre.Enabled= eCab.Aprobado == true ? false : ((_lstdetalle.Any()) ? false : true);
            btnguardar.Enabled = eCab.Aprobado==true ? false : true;
            btnagregar.Enabled= eCab.Aprobado == true ? false : true;

            gvwItems.DataSource = _lstdetalle;
            gvwItems.DataBind();
            Session["lstdetalle"] = JsonHelper.JsonSerializer(_lstdetalle);
        }

        private void CargarBusquedaPresupuesto(DateTime fechai,DateTime fechaf,string vendedor)
        {
            objPlanificacion = new PlanificacionWCFClient();
            _lstbusquedaPre = objPlanificacion.Obtener_MetaPresupuesto(
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, fechai, fechaf, vendedor
                ).ToList();

            Session["lstBusquedaPre"] = JsonHelper.JsonSerializer(_lstbusquedaPre);
            gvwSeguimiento.DataSource = _lstbusquedaPre;
            gvwSeguimiento.DataBind();


        }




        #endregion

        #region Métodos Web

        [WebMethod]
        public static AutoCompleteBoxData Agenda_BuscarVendedor(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarVendedorResult[] lst = objAgendaWCFClient.Agenda_ListarVendedor(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsAgenda_ListarVendedorResult agenda in lst)
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

        [WebMethod]
        public static AutoCompleteBoxData Agenda_BuscarCliente(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarClienteResult[] lst = objAgendaWCFClient.Agenda_ListarCliente(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString, 1);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsAgenda_ListarClienteResult agenda in lst)
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

        [WebMethod]
        public static AutoCompleteBoxData Item_BuscarProducto(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (!string.IsNullOrEmpty(searchString) && searchString.Length >= 4)
            {
                string idalmacen;

                //idalmacen = HttpContext.Current.Session["idAlmacen"].ToString();

                ItemWCFClient objItemWCF = new ItemWCFClient();
                List<gsItem_ListarProductoPresupuestoResult> lst = objItemWCF.Item_ListarProductoPresupuesto(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString)
                    .ToList();


                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsItem_ListarProductoPresupuestoResult producto in lst)
                {
                    AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                    childNode.Text = producto.ItemCodigo + "-" + producto.Nombre;
                    childNode.Value = producto.Kardex.ToString();

                    result.Add(childNode);
                }
                res.Items = result.ToArray();
            }
            return res;
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
                    CargarVendedorZona();
                    rmyPre.SelectedDate = DateTime.Now;
                    Session["lstdetalle"] = null;
                    Session["lstBusquedaPre"] = null;
                    //dpFechaInicio.SelectedDate=DateTime.
                    dpFechaInicio.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    dpFechaFinal.SelectedDate = DateTime.Now;

                    //txtestado.Text = "Registrado";

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

        protected void btnagregar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if(ValidarInformacion())
                    AgregarItemPresupuesto();
                
            }
            catch (Exception ex)
            {
                rwmPre.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

        protected void gvwItems_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                
                string id_cliente = string.Empty;
                string codigoproducto = string.Empty;

                _lstdetalle = JsonHelper.JsonDeserialize<List<USP_Sel_MetaPresupuestoDetResult>>((string)Session["lstdetalle"]);
                if (_lstdetalle.Any())
                {
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    id_cliente = commandArgs[0].ToString();
                    codigoproducto = commandArgs[1].ToString();
                    


                    if (e.CommandName == "EliminarItem")
                    {

                       if(lblestado.Text.Trim().ToUpper()=="APROBADO")
                        {
                            rwmPre.RadAlert("El presupuesto esta aprobado, imposible realizar cambios.", 400, null, "Mensaje de error", null);
                            return;
                        }

                        _lstdetalle.Remove(_lstdetalle.Where(x => x.Id_Cliente == id_cliente && x.CodigoProducto == codigoproducto).First());
                        gvwItems.DataSource = _lstdetalle;
                        gvwItems.DataBind();

                        Session["lstdetalle"] = JsonHelper.JsonSerializer(_lstdetalle);
                        if(!_lstdetalle.Any())
                        {
                            rmyPre.Enabled = true;
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

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            try
            {
                _lstdetalle = JsonHelper.JsonDeserialize<List<USP_Sel_MetaPresupuestoDetResult>>((string)Session["lstdetalle"]);

                if (_lstdetalle.Any())
                {
                    if(GuardarPresupuesto())
                        CargarPresupuesto(Convert.ToInt32(rmyPre.SelectedDate.Value.Year),Convert.ToInt32(rmyPre.SelectedDate.Value.Month),txt_idvendedor.Value.ToString());
                }

            }
            catch (Exception ex)
            {
                rwmPre.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
                
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void gvwSeguimiento_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {

                string Anno,mes,id_vendedor = string.Empty;
                string Aprobado = string.Empty;
                

                
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                Anno = commandArgs[0].ToString();
                mes = commandArgs[1].ToString();
                id_vendedor= commandArgs[2].ToString();


                if (e.CommandName == "EditarSeg")
                {
                    CargarPresupuesto(Convert.ToInt32(Anno), Convert.ToInt32(mes), id_vendedor);
                    //stripPre.SelectedIndex = 1;
                    stripPre.Tabs[1].Selected = true;
                    radmultipage.SelectedIndex = 1;
                }
                if(e.CommandName=="AgregarPromotor")
                {
                    string[] commandArgs2 = e.CommandArgument.ToString().Split(new char[] { ',' });
                    Aprobado = commandArgs2[3].ToString();
                    if(Convert.ToBoolean(Aprobado))
                        Response.Redirect("~/Comercial/Proyectado/frmMetaPresupuestoProm.aspx?Id_Vendedor=" + id_vendedor + "&Anno="+Anno + "&Mes=" + mes);
                    else
                    {
                        rwmPre.RadAlert("El presupuesto tiene que estar Aprobado para distribuir entre promotores", 400, null, "Mensaje de error", null);
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                CargarBusquedaPresupuesto(
                    Convert.ToDateTime(dpFechaInicio.SelectedDate),
                    Convert.ToDateTime(dpFechaFinal.SelectedDate),
                    //abcJefeZona.Entries[0].Text.Split('-')[0]== txt_idvendedor.Value ? txt_idvendedor.Value.ToString() : abcJefeZona.Entries[0].Text.Split('-')[0]
                    txt_idvendedor.Value.ToString().Trim()==string.Empty ? abcJefeZona.Entries[0].Text.Split('-')[0] : txt_idvendedor.Value.ToString().Trim()
                    );
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            
        }

        protected void btnaprobar_Click(object sender, EventArgs e)
        {
            try
            {
                if(Session["lstdetalle"]!= null)
                {
                    _lstdetalle = JsonHelper.JsonDeserialize<List<USP_Sel_MetaPresupuestoDetResult>>((string)Session["lstdetalle"]);
                    if (_lstdetalle.Where(x=> x.Id==0).Any())
                    {
                        rwmPre.RadAlert("Primero debe guardar el presupuesto y luego aprobarlo.", 400, null, "Mensaje de error", null);
                    }
                    else
                    {
                        AprobarPresupuesto();
                    }
                        
                }
                
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnnuevo_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["lstdetalle"] != null)
                {
                    _lstdetalle = new List<USP_Sel_MetaPresupuestoDetResult>();
                    Session["lstdetalle"] = null;

                    CargarVendedorZona();
                    rmyPre.Enabled = true;
                    btnguardar.Enabled = true;
                    btnagregar.Enabled = true;
                    gvwItems.DataSource = _lstdetalle;
                    gvwItems.DataBind();
                    lblestado.Text = "Nuevo";

                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void gvwItems_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {

                //if (e.Item is GridDataItem)
                //{
                //    GridDataItem item = (GridDataItem)e.Item;
                    
                //    var Id = item["Id"].Text;
                   
                //    GridDataItem dataBoundItem = e.Item as GridDataItem;
                //    if (Id == "0")
                //    {
                //        dataBoundItem.ForeColor = Color.Yellow; /// Change the row Color
                //    }

                   
                //}
            }
            catch (Exception ex)
            {
                rwmPre.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {

                gvwSeguimiento.ExportSettings.FileName = "Seguimiento_" + DateTime.Now.ToString("ddMMyyyyhhmm");
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

        protected void gvwSeguimiento_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                gvwSeguimiento.DataSource = JsonHelper.JsonDeserialize<List<USP_Sel_MetaPresupuestoResult>>((string)Session["lstBusquedaPre"]);
                
            }
            catch (Exception ex)
            {
                //ra.RadAl

            }
        }
    }
}