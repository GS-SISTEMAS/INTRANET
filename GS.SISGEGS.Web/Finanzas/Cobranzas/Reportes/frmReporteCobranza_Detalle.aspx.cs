using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.DM;
using GS.SISGEGS.Web.ReporteVentaWCF;
using Telerik.Web.UI;
using System.Globalization;
using GS.SISGEGS.Web.LoginWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.CobranzasWCF;
using GS.SISGEGS.Web.AgendaWCF;
using System.Data; 

namespace GS.SISGEGS.Web.finzanzas.cobranzas.Reportes
{
    public partial class frmReporteCobranza_Detalle : System.Web.UI.Page
    {
        private void ReporteVenta_Cliente(int mes, int year, int id_zona, string id_sectorista)
        {
            ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient();
            try
            {

                int periodo;
                periodo = year * 100 + mes; 


                CobranzasWCFClient objCobranzasWCF = new CobranzasWCFClient();
                List<gsReporteCobranzas_Poryectadas_ClienteResult> Lista;

                Lista = objCobranzasWCF.Reporte_CobranzasProyectadasCliente(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                                        ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario,
                                         mes, year, periodo, id_zona, id_sectorista).ToList();

                Lista = Lista.FindAll(x => x.ImportePendiente > 0).ToList();
                Lista = Lista.OrderByDescending(x => x.Avance).ToList();

                ViewState["lstReporte"] = JsonHelper.JsonSerializer(Lista);

                grdCliente.DataSource = Lista; 
                grdCliente.DataBind();
                //lblTitulo.Text = "Reporte " + fechaInico.Year.ToString() + "-" + fechaInico.Month.ToString() + " de " + rv.Vendedor;
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
                    //EstadoCliente_Cargar(); 

                    LoginWCFClient objLoginWCF = new LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idUsuario);

                    Sectorista_Cargar();
                    Zona_Cargar(Convert.ToString(cboSectorista.SelectedValue.ToString())); 
                 
                    rmpPeriodo.SelectedDate = DateTime.Now;
                    //ReporteVenta_Cliente(rmpPeriodo.SelectedDate.Value.Month, rmpPeriodo.SelectedDate.Value.Year, int.Parse(cboZona.SelectedValue) );
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void ramRepCliente_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            //if (Session["Usuario"] == null)
            //    Response.Redirect("~/Security/frmCerrar.aspx");

            //try
            //{
            //    if (e.Argument.Split(',')[0] == "ChangePageSize")
            //    {
            //        rmpRepCliente.Height = new Unit(e.Argument.Split(',')[1] + "px");
            //        decimal altura = 470 + decimal.Parse(e.Argument.Split(',')[1]) - 531;
            //        rhcDiario.Height = new Unit(altura.ToString() + "px");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    lblMensaje.Text = ex.Message;
            //    lblMensaje.CssClass = "mensajeError";
            //}
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {

            List<gsReporteCobranzas_Poryectadas_ClienteResult> Lista = new List<gsReporteCobranzas_Poryectadas_ClienteResult>();

            Lista = JsonHelper.JsonDeserialize<List<gsReporteCobranzas_Poryectadas_ClienteResult>>((string)ViewState["lstReporte"]);

            if (Lista.Count() >  0)
            {

                var query_Detalle = from c in Lista
                                    orderby c.Cliente
                                    select new
                                    {
                                        Anho = c.Año,
                                        Mes = c.Mes,
                                        ClienteNombre = c.Cliente,

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
                                        AvanceCobrado = string.Format("{0:#,##0.00}", c.Avance),

                                    };

                GridView GridView1 = new GridView();
                GridView1.AllowPaging = false;
                GridView1.DataSource = query_Detalle;
                GridView1.DataBind();
                //ExportGridToExcel_Detalle();
                ExporttoExcel_Moneda(GridView1, "Clientes");
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
 
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            string mensaje = "";
            lblMensaje.Text = ""; 
            try
            {
                if (ValidarValores(ref mensaje) == false)
                {
                    ReporteVenta_Cliente(rmpPeriodo.SelectedDate.Value.Month, rmpPeriodo.SelectedDate.Value.Year, int.Parse(cboZona.SelectedValue), cboSectorista.SelectedValue.ToString());

                    lblMensaje.Text = "Se cargo la consulta. ";
                    lblMensaje.CssClass = "mensajeExito";
                }
                else
                {
                    lblMensaje.Text = mensaje;
                    lblMensaje.CssClass = "mensajeError";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }


        public bool ValidarValores(ref string mensaje)
        {
            bool bloqueo = false;

            try
            {
                if (cboZona.SelectedValue == "0")
                {
                    mensaje = "Seleccionar Zona. ";
                    bloqueo = true;
                }
                if (cboSectorista.SelectedValue == "0")
                {
                    mensaje = "Seleccionar Sectorista. ";
                    bloqueo = true;
                }
            }
            catch (Exception ex)
            {

                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            return bloqueo;
        }


        private void Zona_Cargar(string id_sectorista)
        {
            try
            {
                CobranzasWCFClient objCorbanza = new CobranzasWCFClient();
                List<ZonasSectoristaPermiso_ListarResult> lstdetalle = new List<ZonasSectoristaPermiso_ListarResult>();

                ZonasSectoristaPermiso_ListarResult objZona = new ZonasSectoristaPermiso_ListarResult();
                List<ZonasSectoristaPermiso_ListarResult> lstZona;

                //lstZona = objAgendaWCF.Agenda_ListarZonaSectorista(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                //    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, id_sectorista).ToList();
                lstZona = objCorbanza.ZonasSectoristaPermiso_Listar(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, id_sectorista).ToList();


                lstZona.Insert(0, objZona);
                objZona.Zona = "SELECCIONAR";
                objZona.ID_Zona = 0;

                var lstZonas = from x in lstZona
                               select new
                               {
                                   x.ID_Zona,
                                   DisplayID = String.Format("{0}", x.ID_Zona),
                                   DisplayField = String.Format("{0}", x.Zona)
                                   //DisplayField = String.Format("{0} {1} {2} {3}", x.Placa, x.Modelo, x.Marca, x.Agenda)
                               };

                cboZona.DataSource = lstZonas;
                cboZona.DataTextField = "DisplayField";
                cboZona.DataValueField = "DisplayID";
                cboZona.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void Sectorista_Cargar()
        {
            try
            {
                AgendaWCFClient objAgendaWCF = new AgendaWCFClient();
                gsUsuario_SectoristaResult objSecotrista = new gsUsuario_SectoristaResult();
                List<gsUsuario_SectoristaResult> lstSectoirista = new List<gsUsuario_SectoristaResult>();

                lstSectoirista = objAgendaWCF.Agenda_ListarSectorista(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, null, 1).ToList();



                lstSectoirista.Insert(0, objSecotrista);
                objSecotrista.AgendaNombre = "SELECCIONAR";
                objSecotrista.ID_Agenda = "0";

                var lstSectoiristas = from x in lstSectoirista
                                      select new
                                      {
                                          x.ID_Agenda,
                                          DisplayID = String.Format("{0}", x.ID_Agenda),
                                          DisplayField = String.Format("{0}", x.AgendaNombre)
                                          //DisplayField = String.Format("{0} {1} {2} {3}", x.Placa, x.Modelo, x.Marca, x.Agenda)
                                      };

                cboSectorista.DataSource = lstSectoiristas;
                cboSectorista.DataTextField = "DisplayField";
                cboSectorista.DataValueField = "DisplayID";
                cboSectorista.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void cboSectorista_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                List<gsReporteCobranzas_Poryectadas_ClienteResult> Lista; ; 
                //Ocultar_Botones();
                Zona_Cargar(cboSectorista.SelectedValue);
                List<gsReporteCobranzas_Poryectadas_ClienteResult> lstProyectado = new List<gsReporteCobranzas_Poryectadas_ClienteResult>();
                grdCliente.DataSource = lstProyectado;
                grdCliente.DataBind();

                if (cboSectorista == null || cboSectorista.SelectedValue == "" || cboSectorista.SelectedValue == "0")
                {
                    //Ocultar_Botones();
                    cboZona.Enabled = false;
                }
                else
                {
                    cboZona.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void cboZonas_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                //Ocultar_Botones();
                List<gsProyectadoCobranza_ListarResult> lstProyectado = new List<gsProyectadoCobranza_ListarResult>();
                grdCliente.DataSource = lstProyectado;
                grdCliente.DataBind();

                if (cboZona == null || cboZona.SelectedValue == "" || cboZona.SelectedValue == "0")
                {
                    /*Ocultar_Botones()*/;
                    btnBuscar.Enabled = false;
                    cboZona.Enabled = true;
                }
                else
                {
                    cboZona.Enabled = true;
                    btnBuscar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }

        }

    }
}