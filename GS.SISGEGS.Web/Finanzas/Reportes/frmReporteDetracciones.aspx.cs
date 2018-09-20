using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.FinanzasWCF;
using GS.SISGEGS.Web.Helpers;
using Telerik.Web.UI;

namespace GS.SISGEGS.Web.Finanzas.Reportes
{
    public partial class frmReporteDetracciones : System.Web.UI.Page
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

                    List<GS_ReporteDetraccionesResult> lst = new List<GS_ReporteDetraccionesResult>();

                    grdDetracciones.DataSource = lst;
                    dpFechaHastaCliente.SelectedDate = DateTime.Now;
                    dpFechaDesdeCliente.SelectedDate = DateTime.Now.AddDays(-1);

                }
            }

            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void Reporte_Cargar()
        {
            try
            {
                DateTime fecha1;
                DateTime fecha2;
                DateTime fecha3;
                DateTime fecha4;
                string Cliente;
                int idEstatus = 0;

                Cliente = "";

                List<GS_ReporteDetraccionesResult> lst;

                lblMensaje.Text = "";
                if (Session["Usuario"] == null)
                    Response.Redirect("~/Security/frmCerrar.aspx");

                try
                {
                    if (Validar_Variables() == 0)
                    {
                        fecha2 = dpFechaHastaCliente.SelectedDate.Value;
                        fecha1 = dpFechaDesdeCliente.SelectedDate.Value;

                        idEstatus = Convert.ToInt32(ddlEstatus.SelectedValue);


                        if (acbCliente == null || acbCliente.Text.Split('-')[0] == "" || acbCliente.Text == "")
                        {
                            Cliente = null;
                        }
                        else { Cliente = acbCliente.Text.Split('-')[0]; }


                        var lstParametros = new List<string> { Cliente, idEstatus.ToString(), fecha1.ToShortDateString(), fecha2.ToShortDateString() };
                        ViewState["lstParametros"] = JsonHelper.JsonSerializer(lstParametros);

                        lst = ListarDetracciones(Cliente,fecha1, fecha2, idEstatus);

                    }

                }
                catch (Exception ex)
                {
                    lblMensaje.Text = ex.Message;
                    lblMensaje.CssClass = "mensajeError";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se encontrarón resultados.";
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private List<GS_ReporteDetraccionesResult> ListarDetracciones(string codAgenda, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, int idEstatus)
        {
            FinanzasWCFClient objFinanzasWCF = new FinanzasWCFClient();

            try
            {
                List<GS_ReporteDetraccionesResult> lstDetracciones = new List<GS_ReporteDetraccionesResult>();

                List<GS_ReporteDetraccionesResult> lst = objFinanzasWCF.ReporteDetracciones(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, fechaEmisionInicial, fechaEmisionFinal, codAgenda,  idEstatus).OrderBy(e => e.Fecha).OrderBy(e => e.Fecha).ToList();
                lstDetracciones = lst;
                

                ViewState["lstDetracciones"] = JsonHelper.JsonSerializer(lstDetracciones);
                grdDetracciones.DataSource = lstDetracciones;
                grdDetracciones.DataBind();
                lblDate.Text = "1";

                lblMensaje.Text = lst.Count().ToString(); 
                lblMensaje.CssClass = "mensajeExito";

                return lstDetracciones;


            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
                throw ex;
               
            }
        }

        public int Validar_Variables()
        {
            int valor = 0;

            if (dpFechaDesdeCliente == null || dpFechaDesdeCliente.SelectedDate.Value.ToString() == "" || dpFechaHastaCliente == null || dpFechaHastaCliente.SelectedDate.Value.ToString() == "")
            {
                valor = 1;
                lblMensaje.Text = lblMensaje.Text + "Seleccionar fecha final de emisión. ";
                lblMensaje.CssClass = "mensajeError";
            }
           
            return valor;
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

                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString, 0);
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

        protected void btnBuscar_OnClick(object sender, EventArgs e)
        {
            Reporte_Cargar();
        }

        protected void grdDetracciones_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Accion")
            {
                var lstDoc = JsonHelper.JsonDeserialize<List<GS_ReporteDetraccionesResult>>((string)ViewState["lstDetracciones"]);
                var detraccion = lstDoc.FirstOrDefault(x => x.Op.ToString() == e.CommandArgument.ToString());
                var serieF = (from tbl in lstDoc
                              where tbl.Op == Convert.ToDecimal(e.CommandArgument)
                              select tbl.Serie).FirstOrDefault();
                var NumeroF= (from tbl in lstDoc
                              where tbl.Op == Convert.ToDecimal(e.CommandArgument)
                              select tbl.Numero).FirstOrDefault();                

                if (detraccion == null) return;
                var op = detraccion.Op;
                string idAgenda = "";
                try {
                    idAgenda = acbCliente.Text.Split('-')[0];
                }
                catch {
                    idAgenda = "";
                }
                
                //var lstParametros = new List<string> { e.CommandArgument.ToString(), nombre };
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowEstadoCuenta(" + op + ",'" + idAgenda + "','" + serieF + "','" + NumeroF  +  "');", true);
            }
        }

        protected void grdDetracciones_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem) {
                GridDataItem item = (GridDataItem)e.Item;
                var estado = DataBinder.Eval(item.DataItem, "NombreEstatus");
                var saldo= DataBinder.Eval(item.DataItem, "saldo");
                var op = DataBinder.Eval(item.DataItem, "op");

                if (estado.ToString() == "Cancelado") {
                    item.ForeColor = System.Drawing.Color.Green;
                }

                ImageButton ibEditar = (ImageButton)item.FindControl("ibEditar");

                if (ibEditar != null && Convert.ToDecimal(saldo) > 0) {
                    ibEditar.Visible = false;
                }

                HyperLink link = new HyperLink();
                //link.NavigateUrl = "DetraccionAccionMng.aspx?op=' +" + 1 + " + '&idAgenda=' + " + 1 + "+ '&Serie=' + " + 1 + " + '&Numero=' + " + 1 + ";";
                link.Text = item["saldo"].Text;

                //int index = e.Item.ItemIndex;
                //link.Attributes.Add("onclick", "window.radopen('DetraccionAccionMng.aspx?op=' +" + 1 + " + '&idAgenda=' + " + 1 + "+ '&Serie=' + " + 1 + " + '&Numero=' + " + 1 + "' , 'rwDocumento');");
                link.Attributes.Add("OnClick", "ShowHistorialVoucher(" + op + ");");
                ////ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowEstadoCuenta(" + op + ",'" + idAgenda + "','" + serieF + "','" + NumeroF  +  "');", true);

                item["saldo"].Controls.Add(link);



            }
        }

        private void prueba()
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowEstadoCuenta(" + 1 + ",'" + 1 + "','" + 1 + "','" + 1 + "');", true);
        }
    }
}