using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using GS.SISGEGS.Web.EstadoCuentaWCF;
using GS.SISGEGS.Web.ReporteVentaWCF;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.DM;
using System.Globalization;
using Telerik.Web.UI;
using GS.SISGEGS.Web.LoginWCF;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;
using Microsoft.CSharp;
 
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.LetrasEmitidasWCF;
using GS.SISGEGS.Web.PerfilWCF;
 

namespace GS.SISGEGS.Web.Contabilidad.Informacion
{
    public partial class frmNumerosUnicosMng : System.Web.UI.Page
    {
        private void Procesos_Listar(int anho, int mes, string descripcion)
        {
            LetrasEmitidasWCFClient objProcesoWCF = new LetrasEmitidasWCFClient();

            List<gsProcesoLetras_ListarResult> lstPronostico = new List<gsProcesoLetras_ListarResult>();
   
 
            try
            {

                lstPronostico = objProcesoWCF.ProcesoLetras_NumerosUnicos(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, anho, mes, descripcion).ToList().OrderByDescending(x=> x.id).ToList();
 
   
                grdDocVenta.DataSource = lstPronostico;
                grdDocVenta.DataBind();

                ViewState["lstPronostico"] = JsonHelper.JsonSerializer(lstPronostico);
             
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int empresa;

            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (!Page.IsPostBack)
                {
                    LoginWCFClient objLoginWCF = new LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idUsuario);

                    if (string.IsNullOrEmpty(Request.QueryString["fechaInicio"]))
                    {

                        Empresa_Cargar();
                        cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();
 
                        rmyReporte0.SelectedDate = DateTime.Now;
    
                        empresa = ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa;
                   

                        Carga_Vendedores(0, null);
                    }
                    else
                    {
                        IFormatProvider culture = new System.Globalization.CultureInfo("es-PE", true);
                        rmyReporte0.SelectedDate = DateTime.Parse(Request.QueryString["fechaInicio"], culture, System.Globalization.DateTimeStyles.AssumeLocal);
                        string year;
                        string mes;
                        string id_sectorista, id_vendedor;
                        string stringPeriodo;
                        int periodo, id_zona;
                        string id_zonasec;

                        year = rmyReporte0.SelectedDate.Value.Year.ToString();
                        mes = rmyReporte0.SelectedDate.Value.Month.ToString();

                        if (mes.Length == 1)
                        {
                            mes = "0" + mes;
                        }


                        stringPeriodo = year + "" + mes;
                        periodo = int.Parse(stringPeriodo);
                        id_sectorista = Request.QueryString["ID_Sectorista"];
                        id_zona = int.Parse(Request.QueryString["id_zona"]);
                        id_vendedor = Request.QueryString["id_vendedor"];
                        id_zonasec = id_zona.ToString() + "_" + id_vendedor.ToString();

                        ViewState["id_Sectorista"] = id_sectorista.ToString();
 

                        Buscar_ListarProyectado();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void Empresa_Cargar()
        {
            EmpresaWCFClient objEmpresaWCF = new EmpresaWCFClient();
            try
            {
                cboEmpresa.DataSource = objEmpresaWCF.Empresa_ComboBox();
                cboEmpresa.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void btnCargaMasiva_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                int Periodo;
                Periodo = int.Parse(rmyReporte0.SelectedDate.Value.Year.ToString()) * 100;
                Periodo = (Periodo + int.Parse(rmyReporte0.SelectedDate.Value.Month.ToString())) * 100;
                Periodo = (Periodo + int.Parse(rmyReporte0.SelectedDate.Value.Day.ToString())); 

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreate('"+ Periodo + "');", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void Buscar_ListarProyectado()
        {
            string stringPeriodo;
            int periodo ;
            int anho = 0, mes = 0;
            string descripcion = ""; 

 

            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
 
                descripcion = txtBuscar.Text;
                anho = rmyReporte0.SelectedDate.Value.Year;
                mes = rmyReporte0.SelectedDate.Value.Month;

                Procesos_Listar(anho, mes, descripcion );

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        public List<gsReporteVentaPresupuesto_ProductoResult> ListarPresupuesto(int id_zona)
        {
            string stringPeriodo;
            int yearAnterior;
            int mesInicial, mesFinal, mesDefoult;
            int yearActual, yearDefoult;
            ReporteVentaWCFClient objProyectadoWCF = new ReporteVentaWCFClient();
            List<gsReporteVentaPresupuesto_ProductoResult> lstPronostico = new List<gsReporteVentaPresupuesto_ProductoResult>();
            gsReporteVentaPresupuesto_ProductoResult lstPronosticoMes = new gsReporteVentaPresupuesto_ProductoResult();
            try
            {
                yearActual = rmyReporte0.SelectedDate.Value.Year;
                mesInicial = rmyReporte0.SelectedDate.Value.Month;
            
                yearAnterior = yearActual - 1;
  
                mesDefoult = mesInicial;
                yearDefoult = yearActual;

                for (int i = 0; i < 12; i++)
                {
                    if (mesDefoult > 12)
                    {
                        mesDefoult = 1;
                        yearDefoult = yearDefoult + 1;
                        stringPeriodo = yearDefoult + "_" + mesDefoult;
                    }
                    else
                    {
                        stringPeriodo = yearDefoult + "_" + mesDefoult;
                    }

                    lstPronosticoMes = objProyectadoWCF.gsReporte_PronosticoVentas(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, yearDefoult ,mesDefoult, id_zona).ToList()[0];
                    lstPronostico.Add(lstPronosticoMes); 

                    mesDefoult = mesDefoult + 1;
                }
            }         
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }
            return lstPronostico; 
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar_ListarProyectado();
        }

      

        protected void grdDocVenta_ItemDataBound(object sender, GridItemEventArgs e)
        {
            int yearAnterior;
            int mesInicial, mesFinal, mesDefoult, yearDefoult;
            int yearActual;
            string stringPeriodo = ""; 
            try
            { 

                if ((e.Item is GridEditableItem) || (e.Item.IsDataBound))
            {
                //GridDataItem dataitem = (GridDataItem)e.Item;
                if (e.Item is GridDataItem)
                {
                    GridDataItem dataItem = (GridDataItem)e.Item;
                    string strID = dataItem.GetDataKeyValue("_80_20").ToString();
                    string StrClase = dataItem.GetDataKeyValue("Clase1").ToString();
                    if (strID == "1")
                    {
                        dataItem.BackColor = System.Drawing.Color.Orange;
                        //dataItem.ForeColor = System.Drawing.Color.Orange;
                        dataItem.Font.Bold = true;
                    }
                    else
                    {
                        if(StrClase == "PREMIUM")
                        {
                            dataItem.BackColor = System.Drawing.Color.Yellow;
                            dataItem.Font.Bold = true;
                        }
                        else if(StrClase == "VIP")
                        {
                            dataItem.BackColor = System.Drawing.Color.GreenYellow;
                            dataItem.Font.Bold = true;
                        }

                    }

                }
            }

            if (e.Item is GridHeaderItem)
            {
                    yearActual = rmyReporte0.SelectedDate.Value.Year;
                    mesInicial = rmyReporte0.SelectedDate.Value.Month;
                
                    yearAnterior = yearActual - 1;
                    yearDefoult = yearActual; 
                    mesDefoult = mesInicial;

                    GridHeaderItem headerItem0 = (GridHeaderItem)e.Item;
                    headerItem0.Cells[7].Text = "Q.Venta" + yearAnterior;

                    GridHeaderItem headerItem1 = (GridHeaderItem)e.Item;
                    headerItem1.Cells[8].Text = "Q.Venta" + yearActual;

                    GridHeaderItem headerItem2 = (GridHeaderItem)e.Item;
                    headerItem2.Cells[9].Text = "Q.PPTO" + yearActual;



                for (int i = 0; i < 12; i++)
                {
                    if (mesDefoult > 12)
                    {
                        mesDefoult = 1;
                        yearDefoult = yearActual + 1;
                        stringPeriodo = yearDefoult + "_" + mesDefoult;
                    }
                    else
                    {
                        stringPeriodo = yearActual + "_" + mesDefoult;
                    }

                    GridHeaderItem headerItem = (GridHeaderItem)e.Item;
                    headerItem.Cells[11 + i].Text = stringPeriodo;
                    mesDefoult = mesDefoult + 1;
                }
            }

        }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
}

        protected void ibExcel_Click(object sender, EventArgs e)
        {
            int Id_Zona;
            string Zona;
            //string Id_Vendedor;
            //string Vendedor;
            string stringPeriodo;
            int year;
            int mes;

            string ArchivoExcel = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                year = rmyReporte0.SelectedDate.Value.Year;
                mes = rmyReporte0.SelectedDate.Value.Month;
                string strMes = mes.ToString();
                if (strMes.Length == 1)
                {
                    strMes = "0" + strMes;
                }

                stringPeriodo = year.ToString() + "" + strMes;
                //Id_Zona = int.Parse(cboZona.SelectedItem.Text.Substring(5));
                
                ArchivoExcel = "Proyectado_" + "" +  "_" + stringPeriodo + "_" + DateTime.Now.Millisecond.ToString();

                grdDocVenta.ExportSettings.FileName = ArchivoExcel;

                grdDocVenta.ExportSettings.ExportOnlyData = true;
                grdDocVenta.ExportSettings.IgnorePaging = true;
                grdDocVenta.ExportSettings.OpenInNewWindow = true;
                grdDocVenta.MasterTableView.ExportToExcel();

                Page.Response.ClearHeaders();
                Page.Response.ClearContent();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }
        }

        private void Carga_Vendedores(int id_zona, string id_vendedor)
        {
            try
            {
                AgendaWCFClient objAgendaWCF = new AgendaWCFClient();
                gsVendedor_ListarResult objVendedor = new gsVendedor_ListarResult();
                List<gsVendedor_ListarResult> lstVendedor;

                lstVendedor = objAgendaWCF.Agenda_ListarVendedorProyectado(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, id_zona, id_vendedor).ToList();

                lstVendedor.Insert(0, objVendedor);
                objVendedor.ID_Agenda = "0";
                objVendedor.AgendaNombre = "Todos";

                var lstVendedores = from x in lstVendedor
                                    select new
                                    {
                                        x.ID_Agenda,
                                        DisplayID = x.ID_Agenda.ToString(),
                                        DisplayField = String.Format("{0} - {1}", x.ID_Agenda, x.AgendaNombre)
                                    };
 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grdDocVenta_ItemCommand(object sender, GridCommandEventArgs e)
        {
          
            string year;
            string mes;

            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if (e.CommandName == "Editar")
                {
                    int id_proceso = 0;
                    id_proceso = int.Parse(e.CommandArgument.ToString());

                    LetrasEmitidasWCFClient objLetrasWCF = new LetrasEmitidasWCFClient();
                    List<gsNumerosUnicos_ListarExportarResult> lstNumetosUnicos = new List<gsNumerosUnicos_ListarExportarResult>();


                    try
                    {

                        lstNumetosUnicos = objLetrasWCF.ProcesoLetras_NumerosUnicos_Listar(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                            ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario,  id_proceso).ToList();

                        GridView grdTabla = new GridView();

                        grdTabla.DataSource = lstNumetosUnicos.OrderBy(x=> x.Correlativo).ToList();
                        grdTabla.DataBind();

                        ExporttoExcel(grdTabla, "Reporte_NumetosUnicos_"+ id_proceso.ToString()); 


                    }
                    catch (Exception ex)
                    {
                        lblMensaje.Text = ex.Message;
                        lblMensaje.CssClass = "mensajeError";
                    }



                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;

            }
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
                        string dato;
                        dato = row.Cells[i].Text.ToString(); 

                        dr[i] = dato;
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
        private void ExporttoExcel(GridView GridView1, string Archivo)
        {

            try
            {
                DataTable table = new DataTable();
                table = funConvertGVToDatatable(GridView1);

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Archivo + ".xls");

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



                foreach (DataColumn col in table.Columns)
                {//write in new col

                        HttpContext.Current.Response.Write("<Td BGCOLOR=" + "#66FF66" + " border='1' >");
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
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }

        }

        private void ExporttoExcel_V2( string Archivo )
        {
            string stringPeriodo;
            int year1;
            int mes1, mes2, mesDefoult;
            int year2, year3;
            string Origen;
            string Destino;

            try
            {

                //C: \Users\cesar.coronel\Desktop\Pruebas_TXT\Plantillas\Origen
                 Origen = "C:\\Users\\cesar.coronel\\Desktop\\Pruebas_TXT\\Plantillas\\Origen\\Plantilla_NumerosUnicos.xls";
                 Origen = "C:\\inetpub\\www\\IntranetGS\\Contabilidad\\Informacion\\Plantilla\\Plantilla_NumerosUnicos.xls";

                //Destino = "C:\\Users\\cesar.coronel\\Desktop\\Pruebas_TXT\\Plantillas\\Destino\\ReportePronostico_" + id_zona.ToString() + "_" + strFecha + ".xls";
                Destino = "C:\\temp\\uploads\\" + Archivo + ".xls";

                File.Copy(Origen, Destino, true);

                //ESCRIBIR_EXCEL_GENERAL(Destino, "Pronostico", table, "MANTENIMIENTO", "VENTAS");

                System.IO.FileInfo toDownload = new System.IO.FileInfo(Destino);
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
                Response.AddHeader("Content-Length", toDownload.Length.ToString());
                Response.ContentType = "application/xls";
                string tab = string.Empty;

                Response.WriteFile(Destino);
                Response.End();

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }

        }

        public void ESCRIBIR_EXCEL_GENERAL (string Directorio, string Nombre_Hoja , System.Data.DataTable dtDatos , string stTipo , string stSubTipo)
        {
            try
            {
                if(stTipo == "MANTENIMIENTO" )
                {
                    if (stSubTipo == "VENTAS")
                    {
                        Escribir_Excel_Proyectado(Directorio, Nombre_Hoja, dtDatos);
                        //PintarFormatoPareto(Directorio, Nombre_Hoja, dtDatos); 
                    }
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }
         }

        public void Escribir_Excel_Proyectado(string Directorio, string NombreArchivo, DataTable Tabla)
        {
            string stringPeriodo;
            int yearAnterior;
            int mesInicial, mesFinal, mesDefoult;
            int yearActual, yearDefoult;

            string quote = "\"";
            string strConnnectionOle = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directorio + ";Extended Properties="+ quote + "Excel 8.0;HDR=NO" + quote + "";
            //string strConnnectionOle = "Microsoft.ACE.OLEDB.12.0;Data Source=" + Directorio + ";Extended Properties=Excel 8.0;HDR=NO";

            OleDbConnection oleConn = new OleDbConnection(strConnnectionOle);
            try
            {
                oleConn.Open();
                OleDbCommand cmd = new OleDbCommand() ;
                cmd.Connection = oleConn;

                int countColumn = Tabla.Columns.Count - 1;
                int y = 0;
                yearActual = rmyReporte0.SelectedDate.Value.Year;
        
                yearAnterior = yearActual - 1;
                mesInicial = rmyReporte0.SelectedDate.Value.Month;
                mesDefoult = mesInicial;
                yearDefoult = yearActual;

                cmd.CommandText = "UPDATE [" + NombreArchivo + "$" + "I" + "4:" + "I" + "4] SET F1=" + "'" + "Q_Vent" + yearActual.ToString() + "'";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "UPDATE [" + NombreArchivo + "$" + "J" + "4:" + "J" + "4] SET F1=" + "'" + "Q_PPTO" + yearActual.ToString() + "'";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "UPDATE [" + NombreArchivo + "$" + "K" + "4:" + "K" + "4] SET F1=" + "'" + "Avance%" + "'";
                cmd.ExecuteNonQuery();

                char a;
                int cod;
                string Letra = "L";
                a = Convert.ToChar(Letra);
                cod = (int)a;

                List<gsReporteVentaPresupuesto_ProductoResult> lstPresupuesto = JsonHelper.JsonDeserialize<List<gsReporteVentaPresupuesto_ProductoResult>>((string)ViewState["lstPresupuesto"]);

                decimal TotalVenta;

                foreach (DataColumn col in Tabla.Columns)
                {//write in new col

                    if (y > 11)
                    {
                        if (mesDefoult > 12)
                        {
                            mesDefoult = 1;
                            yearDefoult = yearDefoult + 1;
                            stringPeriodo = yearDefoult + "_" + mesDefoult;
                        }
                        else
                        {
                            stringPeriodo = yearDefoult + "_" + mesDefoult;
                        }


                        if (y < countColumn-1)
                        {
                            var query_Venta = from c in lstPresupuesto
                                              where c.Año == yearDefoult &&  c.Mes == mesDefoult 
                                              //orderby c.CantidadReal2 descending
                                              select new
                                              {
                                                  c.Id_Zona,
                                                  c.Año,
                                                  c.Mes,
                                                  c.Cantidad,
                                                  c.Importe
                                               };

                            if (query_Venta != null)
                            {
                                var sumImport = query_Venta.ToList().Select(c => c.Importe).Sum();
                                TotalVenta = Convert.ToDecimal(sumImport);
                            }
                            else
                            {
                                TotalVenta = 0;
                            }

             


                            cmd.CommandText = "UPDATE [" + NombreArchivo + "$" + Letra + "2:" + Letra + "2] SET F1=" + "'" + TotalVenta + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE [" + NombreArchivo + "$"+ Letra + "4:" + Letra + "4] SET F1=" + "'" + stringPeriodo + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE [" + NombreArchivo + "$" + "AZ" + "4:" + "AZ" + "4] SET F1=" + "'" + "PARETO" + "'";
                            cmd.ExecuteNonQuery();

                            cod = cod + 1 ;
                            a = (char)cod;

                            Letra = Convert.ToString(a);
                            if (cod == 91) Letra = "X";

                        }

                        mesDefoult = mesDefoult + 1;
                    }
                    y = y + 1;
                }

                int fila = 5;

                foreach (DataRow row in Tabla.Rows)
                {//write in new row
                    string pareto = row[countColumn].ToString();
                    Letra = "A";
                    a = Convert.ToChar(Letra);
                    cod = (int)a;
                    string Texto = "";


                    Texto = "UPDATE [" + NombreArchivo + "$" + "A" + fila.ToString() + ":" + "W" + fila.ToString() + "]   ";
                    Texto = Texto + " SET F1=" + "'" + row[0].ToString().Replace("&nbsp;","") + "', ";
                    Texto = Texto + "  F2=" + "'" + row[1].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F3=" + "'" + row[2].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F4=" + "'" + row[3].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F5=" + "'" + row[4].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F6=" + "'" + row[5].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F7=" + "'" + row[6].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F8=" + "'" + row[7].ToString().Replace("&nbsp;", "") + "', ";

                    ////Texto = Texto + "  F9=" + "'" + row[8].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F9=" + "'" + row[10].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F10=" + "'" + row[11].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F11=" + "'" + row[12].ToString().Replace("&nbsp;", "") + "', ";

                    Texto = Texto + "  F12=" + "'" + row[13].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F13=" + "'" + row[14].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F14=" + "'" + row[15].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F15=" + "'" + row[16].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F16=" + "'" + row[17].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F17=" + "'" + row[18].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F18=" + "'" + row[19].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F19=" + "'" + row[20].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F20=" + "'" + row[21].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F21=" + "'" + row[22].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F22=" + "'" + row[23].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F23=" + "'" + row[24].ToString().Replace("&nbsp;", "") + "' ";
                    //  Texto = Texto + "  F25=" + "'" + row[24].ToString().Replace("&nbsp;", "") + "' ";

                    //Texto = Texto + "  F26=" + "'" + row[25].ToString().Replace("&nbsp;", "") + "' ";

                    //Texto = Texto + "  F27=" + "'" + row[26].ToString().Replace("&nbsp;", "") + "'";
                    //Texto = Texto + "  F26=" + "'" + row[25].ToString() + "'";

                    cmd.CommandText = Texto; 
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "UPDATE [" + NombreArchivo + "$" + "AZ" + fila.ToString() + ":" + "AZ" + fila.ToString() + "] SET F1=" + "'" + row[25].ToString() + "'";
                    cmd.ExecuteNonQuery();

                    //for (int i = 0; i < Tabla.Columns.Count; i++)
                    //{
                    //    if (i < countColumn)
                    //    {
                    //        cmd.CommandText = "UPDATE [" + NombreArchivo + "$" + Letra + fila.ToString() + ":" + Letra + fila.ToString() + "] SET F1=" + "'" + row[i].ToString() + "'";


                    //        cmd.ExecuteNonQuery();
                    //    }
                    //    else
                    //    {
                    //        cmd.CommandText = "UPDATE [" + NombreArchivo + "$" + "AY" + fila.ToString() + ":" + "AY" + fila.ToString() + "] SET F1=" + "'" + row[i].ToString() + "'";
                    //        cmd.ExecuteNonQuery();
                    //    }

                    //    cod = cod + 1;
                    //    a = (char)cod;
                    //    Letra = Convert.ToString(a);
                    //}
                    fila = fila + 1; 
                }


                oleConn.Close();
            }
            catch (Exception ex)
            {
                oleConn.Close(); 
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }

        }

        public void PintarFormatoPareto(string Directorio, string NombreArchivo, DataTable Tabla)
        {
            try
            {
                char a;
                int cod;
                string Letra = "M";
                a = Convert.ToChar(Letra);
                cod = (int)a;

                Excel.Application objXls = new Excel.Application();
                Excel.Workbook objBook;
                Excel.Sheets sheets;
                Excel.Worksheet worksheet;

                objBook = objXls.Workbooks.Open(Directorio);

                objXls.Sheets["Pronostico"].select();

                int fila = 5;
                int countColumn = Tabla.Columns.Count ;
                string Celda;

                foreach (DataRow row in Tabla.Rows)
                {//write in new row
                    string pareto = row[countColumn - 1 ].ToString();
                    string StrClase = row[5].ToString();
                    Letra = "A";
                    a = Convert.ToChar(Letra);
                    cod = (int)a;

                            if (pareto == "1")
                            {
                                Celda = "" + "A" + fila.ToString() + ":" + "AA" + fila.ToString() + "";
                                objXls.Range[Celda].Select();
                                objXls.Selection.interior.color = Color.Orange;
                            }
                            else
                            {
                                if (StrClase == "PREMIUM")
                                {
                                    Celda = "" + "A" + fila.ToString() + ":" + "AA" + fila.ToString() + "";
                                    objXls.Range[Celda].Select();
                                    objXls.Selection.interior.color = Color.Yellow;
                                }
                                else if (StrClase == "VIP")
                                {
                                    Celda = "" + "A" + fila.ToString() + ":" + "AA" + fila.ToString() + "";
                                    objXls.Range[Celda].Select();
                                    objXls.Selection.interior.color = Color.GreenYellow;
                                }

                            }
                  
                    fila = fila + 1;
                }

                Celda = "" + "A" + 5.ToString() + ":" + "AA" + 5.ToString() + "";
                objXls.Range[Celda].Select();
                //objXls.Range["A8"].Value = "REPORTE GERENCIAL DE GASTOS TOTAL";
                //objXls.Range["A8"].Select();
                ////objXls.Selection.MergeCells = true;   // combina celdas
                //objXls.Selection.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                //objXls.Selection.interior.color = Color.LightGoldenrodYellow;
                //objXls.Selection.BORDERS.Color = Color.Silver;

                MemoryStream ms = new MemoryStream();
                objBook.Save();  
                objXls.Application.Visible = true;
                //objXls.Workbooks.Close();

             }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }
        }
 
 

        protected void btnBajarPlantilla_Click(object sender, EventArgs e)
        {
            ExportarExcel1();
        }

        public void ExportarExcel2()
        {
            try
            {
            grdDocVenta.MasterTableView.ExportToExcel();
            Page.Response.ClearHeaders();
            Page.Response.ClearContent();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }
        }

        public void ExportarExcel1()
        {
            int Id_Zona;
            string Zona;
            //string Id_Vendedor;
            //string Vendedor;
            string stringPeriodo;
            int year;
            int mes;
            string ArchivoExcel = "";

            decimal TotalVenta1;

            try
            {


                ArchivoExcel = "Plantilla_NumerosUnicos"; 
 
                ExporttoExcel_V2( ArchivoExcel);

             
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }

        }


        protected void btnExcel_Click(object sender, EventArgs e)
        {
            ExportarExcel1();
        }


    }
}