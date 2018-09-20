using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.DM;
using GS.SISGEGS.Web.ReporteVentaWCF;
using System.Web.Services;
using Telerik.Web.UI;
using GS.SISGEGS.Web.AgendaWCF;

namespace GS.SISGEGS.Web.Comercial.Reportes.ReporteGerencial
{
    public partial class frmProductoKardex : System.Web.UI.Page
    {
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

        private void Vendedor_Listar()
        {
            ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient();
            try
            {
                DateTime fechaInicio = new DateTime(dpFecInicio.SelectedDate.Value.Year, dpFecInicio.SelectedDate.Value.Month, 1);
                DateTime fechaFinal = dpFecFinal.SelectedDate.Value.AddMonths(1);
                fechaFinal = (new DateTime(fechaFinal.Year, fechaFinal.Month, 1)).AddDays(-1);
                string idVendedor = null;
                if (!string.IsNullOrEmpty(acbVendedor.Text))
                    idVendedor = acbVendedor.Text.Split('-')[0].Trim();

                grdProducto.DataSource = objReporteVentaWCF.DocVenta_ReporteVenta_Marca(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, idVendedor, fechaInicio, fechaFinal);
                grdProducto.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

                    dpFecInicio.SelectedDate = DateTime.Now.AddMonths(-2);
                    dpFecFinal.SelectedDate = DateTime.Now;

                    Vendedor_Listar();
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
                if (acbVendedor.Text.Count() > 0)
                {
                    if (acbVendedor.Entries.Count <= 0 && acbVendedor.Text.Split('-').ToList().Count != 2)
                        throw new ArgumentException("Se debe seleccionar un vendedor valido");
                }

                Vendedor_Listar();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}