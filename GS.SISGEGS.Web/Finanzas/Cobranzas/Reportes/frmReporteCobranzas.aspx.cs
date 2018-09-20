using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.CobranzasWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.DM;
using System.Globalization;
using Telerik.Web.UI;
using GS.SISGEGS.Web.LoginWCF;
using System.Data; 

namespace GS.SISGEGS.Web.Finanzas.Cobranzas.ReporteCobranza
{
    public partial class frmReporteCobranzas : System.Web.UI.Page
    {
        private void ReporteCobranza_Listar(string codAgenda, string codigoVendedor) {

            int periodo;
            int year, mes;

            CobranzasWCFClient objCobranzasWCF = new CobranzasWCFClient();
            List<gsReporteCobranzas_Poryectadas_VendedorResult> lst;

            try {
                DateTime firstDayOfMonth = new DateTime(rmyReporte.SelectedDate.Value.Year, rmyReporte.SelectedDate.Value.Month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                year = firstDayOfMonth.Year;
                mes = firstDayOfMonth.Month;
                periodo = year * 100 + mes;


                lst = objCobranzasWCF.Reporte_CobranzasProyectadasVendedor(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario,
                     mes,year,periodo,0,null).ToList();

                lst = lst.OrderByDescending(x => x.AvanceCobrado).ToList().OrderBy(x=> x.Sectorista_Nombre).ToList(); 

                ViewState["cantidad"] = lst.Count;

                grdDocVenta.DataSource = lst;
                grdDocVenta.DataBind();

                lblTitulo.Text = "Reporte de Pronostico de Cobranza  (" + rmyReporte.SelectedDate.Value.Year.ToString() + "-" + rmyReporte.SelectedDate.Value.Month.ToString() + ")";


                ViewState["lstDocVenta"] = JsonHelper.JsonSerializer(lst);

                ViewState["fechaInicio"] = firstDayOfMonth.ToString("dd/MM/yyyy");
                ViewState["fechaFinal"] = lastDayOfMonth.ToString("dd/MM/yyyy");
                ViewState["lstReporte"] = JsonHelper.JsonSerializer(lst);

                lblMensaje.Text = "Se cargo con éxito.";
                lblMensaje.CssClass = "mensajeExito";

            }
            catch (Exception ex) {
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
                    LoginWCFClient objLoginWCF = new LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idUsuario);

                    if (string.IsNullOrEmpty(Request.QueryString["anho"]))
                    {
                        rmyReporte.SelectedDate = DateTime.Now;
                    }
                    else {

                        string Anho = Request.QueryString["anho"].ToString();
                        string Mes = Request.QueryString["mes"].ToString();
                        string Dia = "1";
                        string Fecha = Anho + "-" + Mes + "-" + Dia; 

                        DateTime fecha = Convert.ToDateTime(Fecha);
                        rmyReporte.SelectedDate = fecha; 
                    }
                    ReporteCobranza_Listar(null, null);
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
                ReporteCobranza_Listar(null,null);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void ramReporteVenta_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Argument.Split(',')[0] == "ChangePageSize")
                {
                    grdDocVenta.Height = new Unit(e.Argument.Split(',')[1] + "px");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdDocVenta_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            int cantidad; 
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                cantidad = (int)ViewState["cantidad"];
                if(cantidad > 0 )
                {
                    grdDocVenta.DataSource = JsonHelper.JsonDeserialize<List<gsDocVenta_ReporteVenta_VendedorResult>>((string)ViewState["lstReporte"]);
                    grdDocVenta.DataBind();
                }
              
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdDocVenta_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.CommandName == "Grafico")
                {

                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string id_zona = commandArgs[0];
                    string año = commandArgs[1];
                    string mes = commandArgs[2];

                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreateViewEstadoCuenta(" + ID_Cliente + "," + strFecha + "," + id_Sectorista + "," + ID_zona + ");", true);

                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowReportbySalesman('" + id_Vendedor + "','" + (string)ViewState["fechaInicio"] + "','" + (string)ViewState["fechaFinal"] + "');", true);

                    Response.Redirect("~/Finanzas/Cobranzas/Reportes/frmReporteCobranzas_Clientes.aspx?id_zona=" + id_zona + "&anho=" + año
                        + "&mes=" + mes );
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdDocVenta_ItemDataBound(object sender, GridItemEventArgs e)
        {
            int cantidad; 
            cantidad = (int)ViewState["cantidad"];
            if (cantidad > 0)
            {
                if (e.Item is GridFooterItem)
                {
                    //GridFooterItem footerItem = e.Item as GridFooterItem;
                    //string strValorVenta = footerItem["ImporteCobrado"].Text;
                    //strValorVenta = strValorVenta.Substring(1, strValorVenta.Length - 1);
                    //strValorVenta = strValorVenta.Replace(",", string.Empty);
                    //string strValorPlanificado = footerItem["ImporteProyectado"].Text;
                    //strValorPlanificado = strValorPlanificado.Substring(1, strValorPlanificado.Length - 1);
                    //strValorPlanificado = strValorPlanificado.Replace(",", string.Empty);

                    //if (decimal.Parse(strValorPlanificado) > 0)
                    //    footerItem["Avance"].Text = Math.Round((decimal.Parse(strValorVenta) / decimal.Parse(strValorPlanificado) * 100), 0).ToString() + "%";
                    //else
                    //    footerItem["Avance"].Text = "100%";
                }
            }
           
        }


        protected void btnExcel_Click(object sender, EventArgs e)
        {

            List<gsReporteCobranzas_Poryectadas_VendedorResult> Lista = new List<gsReporteCobranzas_Poryectadas_VendedorResult>();

            Lista = JsonHelper.JsonDeserialize<List<gsReporteCobranzas_Poryectadas_VendedorResult>>((string)ViewState["lstReporte"]);

            if (Lista.Count() > 0)
            {
                var query_Detalle = from c in Lista
                                    orderby c.Zona_nombre
                                    select new
                                    {
                                        Anho = c.Año,
                                        Mes = c.Mes,
                                        ZonaNombre = c.Zona_nombre,

                                        PendienteNoVencido = string.Format("{0:#,##0.00}", c.ImportePendienteNoVencido),
                                        PendienteVencido = string.Format("{0:#,##0.00}", c.ImportePendienteVencido),
                                        Pendiente = string.Format("{0:#,##0.00}", c.ImportePendiente),
                                        ProyectadoNoVencido = string.Format("{0:#,##0.00}", c.ImporteProyectadoNoVencido),
                                        ProyectadoVencido = string.Format("{0:#,##0.00}", c.ImporteProyectadoVencido),
                                        Proyectado = string.Format("{0:#,##0.00}", c.ImporteProyectado),
                                        CobradoNoVencido = string.Format("{0:#,##0.00}", c.ImporteCobradoNoVencido),
                                        CobradoVencido = string.Format("{0:#,##0.00}", c.ImporteCobradoVencido),
                                        Cobrado = string.Format("{0:#,##0.00}", c.ImporteCobrado),

                                        Diferencia = string.Format("{0:#,##0.00}", c.Diferencia),
                                        AvanceCobrado = string.Format("{0:#,##0.00}", c.AvanceCobrado),

                                    };


                GridView GridView1 = new GridView();
                GridView1.AllowPaging = false;
                GridView1.DataSource = query_Detalle;
                GridView1.DataBind();
                //ExportGridToExcel_Detalle();
                ExporttoExcel_Moneda(GridView1, "Zonas");
            }
        }

        private void ExporttoExcel_Moneda(GridView GridView1, string TipoReporte)
        {
            string Fecha;
            string NombreReporte;

            Fecha = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString();
            DataTable table = new DataTable();
            table = funConvertGVToDatatable(GridView1);
            NombreReporte = "Reporte_" + TipoReporte + "_" + Fecha;

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + NombreReporte + ".xls");

            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height

            int columnscount = GridView1.Columns.Count;

            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'>");
            HttpContext.Current.Response.Write("<TR>");
            HttpContext.Current.Response.Write("</TR>");


            HttpContext.Current.Response.Write("<TR>");
            HttpContext.Current.Response.Write("<Td style='border - width: 1px; border: solid; border - color:RED;'  colspan='" + columnscount.ToString() + "' >");
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write("Reporte " + TipoReporte);
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");

            HttpContext.Current.Response.Write("</TR>");

            HttpContext.Current.Response.Write("<TR>");
            HttpContext.Current.Response.Write("<Td colspan='" + columnscount.ToString() + "'>");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("</TR>");


            HttpContext.Current.Response.Write("<TR>");
            foreach (DataColumn col in table.Columns)
            {//write in new col
                HttpContext.Current.Response.Write("<Td BGCOLOR=" + "#66FF66" + " border='1' >");
                //Get column headers  and make it as bold in excel columns
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(col.ColumnName.ToString());
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");


            foreach (DataRow row in table.Rows)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td border='1'>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        private DataTable funConvertGVToDatatable(GridView dtgrv)
        {
            try
            {
                DataTable dt = new DataTable();

                if (dtgrv.HeaderRow != null)
                {
                    for (int i = 0; i < dtgrv.HeaderRow.Cells.Count; i++)
                    {
                        dt.Columns.Add(dtgrv.HeaderRow.Cells[i].Text);
                    }
                }

                foreach (GridViewRow row in dtgrv.Rows)
                {
                    DataRow dr;
                    dr = dt.NewRow();

                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        dr[i] = row.Cells[i].Text.Replace(" ", "");
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}