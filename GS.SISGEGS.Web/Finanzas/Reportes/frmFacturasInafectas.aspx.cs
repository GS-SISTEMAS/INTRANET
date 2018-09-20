using GS.SISGEGS.DM;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.CobranzasWCF;
using GS.SISGEGS.Web.ItemWCF;
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.EstadoCuentaWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using GS.SISGEGS.Web.Helpers;
using System.IO;
using System.Data.OleDb;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using GS.SISGEGS.Web.LoginWCF;
using System.ComponentModel;


namespace GS.SISGEGS.Web.Finanzas.Financiamientos.LetrasEmitidas
{
    public partial class frmFacturasInafectas : System.Web.UI.Page
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

                    dpInicio.SelectedDate = DateTime.Now;
                    dpFinal.SelectedDate = DateTime.Now;
                    lblDate.Text = "";
                    TipoDocumento_Cargar();
                }
            }

            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        private void TipoDocumento_Cargar()
        {
            try
            {
                //FacturaElectronica2WCFClient objFacturaWCF = new FacturaElectronica2WCFClient();
                //gsComboDocElectronicoResult objTipoDoc = new gsComboDocElectronicoResult();

                //List<gsComboDocElectronicoResult> lst = objFacturaWCF.ComboDocElectronico(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();


                //lst.Insert(0, objTipoDoc);
                //objTipoDoc.Nombre = "Todos";
                //objTipoDoc.ID_Documento = 0;

                //var listDoc = from v in lst
                //              where v.ID_Documento == (101) || v.ID_Documento == (102)
                //              select new
                //              {
                //                  v.ID_Documento,
                //                  v.Nombre
                //              };

                //cboTipoDoc.DataSource = listDoc.ToList();
                //cboTipoDoc.DataTextField = "Nombre";
                //cboTipoDoc.DataValueField = "ID_Documento";
                //cboTipoDoc.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            DateTime fecha1;
            DateTime fecha2;

            fecha1 = dpInicio.SelectedDate.Value;
            fecha2 = dpFinal.SelectedDate.Value;

            string cliente;
            string NombreCliente = null;

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if(Validar_Variables() == 0)
                {                   

                    if (acbCliente == null || acbCliente.Text.Split('-')[0] == "" || acbCliente.Text == "")
                    {
                        cliente = null;
                    }
                    else { cliente = acbCliente.Text.Split('-')[0];
                        NombreCliente = acbCliente.Text.Split('-')[1]; }


                    this.ListarVentasCobranzas(cliente, fecha1, fecha2, cliente, NombreCliente);

                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        private void Cliente_Buscar(string idAgenda)
        {
            AgendaWCFClient objAgendaWCFClient;
            VBG01134Result objAgendaCliente;
            decimal? lineaCredito = null;
            decimal? TC = null;
            DateTime? fechaVecimiento = null;
            try
            {
                objAgendaWCFClient = new AgendaWCFClient();
                objAgendaCliente = new VBG01134Result();

                objAgendaCliente = objAgendaWCFClient.Agenda_BuscarCliente(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idAgenda, ref lineaCredito, ref fechaVecimiento, ref TC);

                String txtClienteObj = objAgendaCliente.Nombre;

                ViewState["LineaCredito"] = lineaCredito;
                ViewState["FechaVencimiento"] = fechaVecimiento;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ListarVentasCobranzas(string codAgenda, DateTime fechaInicial, DateTime fechaFinal, string cliente,string texto)
        {
            int TipoDocumento = Convert.ToInt32(cboTipoDoc.SelectedValue.ToString());
            CobranzasWCFClient objCobranzasWCF = new CobranzasWCFClient();
            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();

            try
            {
                List<gsReporteFacturasInafectasV1Result> lstCobranzas = objCobranzasWCF.Reporte_FacturasInafecta(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, fechaInicial, fechaFinal, TipoDocumento, cliente).ToList();

                if (texto == null)
                {
                    var liq = from lq in lstCobranzas select lq;
                    grdFacturasInafectas.DataSource = liq.ToList(); //lstCobranzas.ToList();
                    grdFacturasInafectas.DataBind();
                }
                else
                {
                    var liq = from lq in lstCobranzas where lq.Cliente == texto select lq;
                    grdFacturasInafectas.DataSource = liq.ToList(); //lstCobranzas.ToList();
                    grdFacturasInafectas.DataBind();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Validar_Variables()
        {
            int valor = 0;

            if ( dpInicio == null || dpInicio.SelectedDate.Value.ToString() == "")
            {
                valor = 1;
                lblMensaje.Text = lblMensaje.Text + "Seleccionar fecha final de emisión. ";
                lblMensaje.CssClass = "mensajeError";
            }

            return valor;
        }

        #region Métodos web
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
        public static AutoCompleteBoxData Agenda_BuscarItem(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                ItemWCFClient objItemWCFClient = new ItemWCFClient();

                gsItem_ListarResult[] lst = objItemWCFClient.Item_Listar(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsItem_ListarResult Item in lst)
                {
                    AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                    childNode.Text = Item.ID_Item + "-" + Item.Nombre;
                    childNode.Value = Item.ID_Item;
                    result.Add(childNode);
                }
                res.Items = result.ToArray();
            }
            return res;
        }
        protected void grdFacturasInafectas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

        }
        protected void grdFacturasInafectas_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }
        #endregion


    }
}