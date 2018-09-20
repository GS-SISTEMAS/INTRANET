using System;
using GS.SISGEGS.Web.ReporteContabilidadWCF;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.Helpers;
using Telerik.Web.UI;


namespace GS.SISGEGS.Web.Contabilidad.Reporte
{
    public partial class frmCostoProdCompa : System.Web.UI.Page
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

                    dpFecInicio.SelectedDate = DateTime.Now.AddMonths(-1);
                    dpFecFinal.SelectedDate = DateTime.Now;
                    dpFecVariacion.SelectedDate = DateTime.Now;

                    Reporte_Cargar();

                    int cantidad = Convert.ToInt32(Session["cantidad"]);
                    string mensaje = Convert.ToString(Session["mensaje"]);

                    if (cantidad == 0)
                    {
                        lblMensaje.Text = mensaje;
                        lblMensaje.CssClass = "mensajeError";
                    }

                    else {
                        lblMensaje.Text = mensaje;
                        lblMensaje.CssClass = "mensajeExito";
                    }
                }
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }
        }

        private void Reporte_Cargar()
        {
            List<Reporte_CostoProduccionResult> lstDocumentos;
            try
            {
                ReporteContabilidadWCFClient objReporteContabilidadWCF = new ReporteContabilidadWCFClient();
                DateTime fechaInicio = new DateTime(dpFecInicio.SelectedDate.Value.Year, dpFecInicio.SelectedDate.Value.Month, 1);
                DateTime fechaFinal = new DateTime(dpFecFinal.SelectedDate.Value.Year, dpFecFinal.SelectedDate.Value.Month, 1).AddMonths(1).AddDays(-1);

                DateTime mesEvaluado = new DateTime(dpFecVariacion.SelectedDate.Value.Year, dpFecVariacion.SelectedDate.Value.Month, 1);

                if (fechaFinal < fechaInicio)
                {
                    rwmReporte.RadAlert("Ud. debe ingresar un periodo final mayor o igual al periodo inicial", 500, 100, "Validación de fechas", "");

                }

                else if (mesEvaluado < fechaInicio)
                {

                    rwmReporte.RadAlert("Ud. debe ingresar un mes evaluado que pertenezca al rango del Periodo Inicio y Final", 500, 100, "Validación de fechas", "");

                }

                else if (mesEvaluado > fechaFinal)
                {

                    rwmReporte.RadAlert("Ud. debe ingresar un mes evaluado que pertenezca al rango del Periodo Inicio y Final", 500, 100, "Validación de fechas", "");

                }

                else
                {

                    lstDocumentos = objReporteContabilidadWCF.ReporteCostoProduccion(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                    fechaInicio, fechaFinal).ToList();

                    var dtLstDocumento = new DataTable();
                    DataTable dtDoc = ToDataTable(lstDocumentos, "dtLstDoc");
                    dtLstDocumento = dtDoc;
                    if (lstDocumentos.Any())
                    {
                        PivotTable objPivotTable = new PivotTable();

                        var moneda = ddlMoneda.SelectedValue;
                        var lstRows = new List<string>();
                        lstRows.Add("PT_Kardex");
                        lstRows.Add("PT_Descripcion");
                        lstRows.Add("cantidadTotal");
                        if (moneda == "0")
                        {
                            lstRows.Add("costoTotalSol");
                            lstRows.Add("hhSol");
                            lstRows.Add("hhMaqSol");
                            lstRows.Add("costoDistribuidoSol");
                            lstRows.Add("costoProdTerminadoSol");
                        }
                        else
                        {
                            lstRows.Add("costoTotalDol");
                            lstRows.Add("hhDol");
                            lstRows.Add("hhMaqDol");
                            lstRows.Add("costoDistribuidoDol");
                            lstRows.Add("costoProdTerminadoDol");
                        }
                        dtLstDocumento = objPivotTable.Generate(dtDoc, lstRows, "Mes", "cuSol");

                        string mesVariacion = "c/u " + dpFecVariacion.SelectedDate.Value.Year.ToString() + " - " +
                                              dpFecVariacion.SelectedDate.Value.Month.ToString();

                        if (dtLstDocumento.Columns.Contains(mesVariacion))
                        {
                            
                            foreach (DataRow row in dtLstDocumento.Select())
                            {
                                if (moneda == "0")
                                {
                                    string cuMesEvaluado = string.IsNullOrEmpty(row[mesVariacion].ToString()) ? "0" : row[mesVariacion].ToString();
                                    row["Variacion"] = 1 - Math.Round((Convert.ToDecimal(cuMesEvaluado) / (Convert.ToDecimal(row["Variacion"]) / dtLstDocumento.Columns.Count - 8)), 2);
                                    row["cantidadTotal"] = row["cantidadTotal"] == null ? 0m : Math.Round(Convert.ToDecimal(row["cantidadTotal"]), 2);
                                    row["hhSol"] = row["hhSol"] == null ? 0m : Math.Round(Convert.ToDecimal(row["hhSol"]), 2);
                                    row["hhMaqSol"] = row["hhMaqSol"] == null ? 0m : Math.Round(Convert.ToDecimal(row["hhMaqSol"]), 2);
                                    row["costoTotalSol"] = row["costoTotalSol"] == null ? 0m : Math.Round(Convert.ToDecimal(row["costoTotalSol"]), 2);
                                }

                                else {
                                    string cuMesEvaluado = string.IsNullOrEmpty(row[mesVariacion].ToString()) ? "0" : row[mesVariacion].ToString();
                                    row["Variacion"] = 1 - Math.Round((Convert.ToDecimal(cuMesEvaluado) / (Convert.ToDecimal(row["Variacion"]) / dtLstDocumento.Columns.Count - 8)), 2);
                                    row["cantidadTotal"] = row["cantidadTotal"] == null ? 0m : Math.Round(Convert.ToDecimal(row["cantidadTotal"]), 2);
                                    row["hhDol"] = row["hhDol"] == null ? 0m : Math.Round(Convert.ToDecimal(row["hhDol"]), 2);
                                    row["hhMaqDol"] = row["hhMaqDol"] == null ? 0m : Math.Round(Convert.ToDecimal(row["hhMaqDol"]), 2);
                                    row["costoTotalDol"] = row["costoTotalDol"] == null ? 0m : Math.Round(Convert.ToDecimal(row["costoTotalDol"]), 2);                                
                                }
                            }

                        }
                        else
                        {
                            foreach (DataRow row in dtLstDocumento.Select())
                            {
                                if (moneda == "0")
                                {
                                    row["Variacion"] = 0; 
                                    row["cantidadTotal"] = row["cantidadTotal"]==null ? 0m : Math.Round(Convert.ToDecimal(row["cantidadTotal"]), 2);
                                    row["hhSol"] = row["hhSol"] == null ? 0m : Math.Round(Convert.ToDecimal(row["hhSol"]), 2);
                                    row["hhMaqSol"] = row["hhMaqSol"] == null ? 0m : Math.Round(Convert.ToDecimal(row["hhMaqSol"]), 2);
                                    row["costoTotalSol"] = row["costoTotalSol"] == null ? 0m : Math.Round(Convert.ToDecimal(row["costoTotalSol"]), 2);                                     
                                }

                                else {
                                    row["Variacion"] = 0;
                                    row["cantidadTotal"] = row["cantidadTotal"] == null ? 0m : Math.Round(Convert.ToDecimal(row["cantidadTotal"]), 2);
                                    row["hhDol"] = row["hhDol"] == null ? 0m : Math.Round(Convert.ToDecimal(row["hhDol"]), 2);
                                    row["hhMaqDol"] = row["hhMaqDol"] == null ? 0m : Math.Round(Convert.ToDecimal(row["hhMaqDol"]), 2);
                                    row["costoTotalDol"] = row["costoTotalDol"] == null ? 0m : Math.Round(Convert.ToDecimal(row["costoTotalDol"]), 2);
                                }
                                
                            }
                        }

                        dtLstDocumento.Columns["PT_Kardex"].ColumnName = "Kardex";
                        dtLstDocumento.Columns["PT_Descripcion"].ColumnName = "Descripcion";
                        dtLstDocumento.Columns["cantidadTotal"].ColumnName = "Cantidad de Produccion";
                        dtLstDocumento.Columns["Variacion"].ColumnName = "% Variacion " + mesVariacion;

                        if (moneda == "0")
                        {
                            dtLstDocumento.Columns["costoTotalSol"].ColumnName = "Costo de Material Consumido (S/)";
                            dtLstDocumento.Columns["hhSol"].ColumnName = "Costo Inductor H/H (S/)";
                            dtLstDocumento.Columns["hhMaqSol"].ColumnName = "Costo Inductor H/MAQ (S/)";
                            dtLstDocumento.Columns["costoDistribuidoSol"].ColumnName = "Costo Gasto Distribuido (S/)";
                            dtLstDocumento.Columns["costoProdTerminadoSol"].ColumnName = "Costo de Produccion Producto Terminado (S/)";
                        }
                        else
                        {
                            dtLstDocumento.Columns["costoTotalDol"].ColumnName = "Costo de Material Consumido ($)";
                            dtLstDocumento.Columns["hhDol"].ColumnName = "Costo Inductor H/H ($)";
                            dtLstDocumento.Columns["hhMaqDol"].ColumnName = "Costo Inductor H/MAQ ($)";
                            dtLstDocumento.Columns["costoDistribuidoDol"].ColumnName = "Costo Gasto Distribuido ($)";
                            dtLstDocumento.Columns["costoProdTerminadoDol"].ColumnName = "Costo de Produccion Producto Terminado ($)";
                        }
                    }

                    List<DataRow> rows = dtLstDocumento.Rows.Cast<DataRow>().ToList();

                    foreach (var row in rows)
                    {
                        foreach (var column in row.Table.Columns)
                        {
                            string HeaderName = column.ToString();

                            if (HeaderName.Contains("201") && !HeaderName.Contains("Variaci"))
                            {
                                if (string.IsNullOrEmpty(row[HeaderName].ToString()))
                                    row[HeaderName] = 0;
                                else row[HeaderName] = Math.Round(Convert.ToDecimal(row[HeaderName]), 2);
                            }
                            
                        }
                    }

                    grdDocumentos01.DataSource = rows;
                    string mensaje = "Se han encontrado " + rows.Count.ToString() + " registros.";
                    //lblMensaje.Text = "Se han encontrado " + rows.Count.ToString() + " registros.";
                    //lblMensaje.CssClass = "mensajeExito";
                    grdDocumentos01.DataBind();
                    
                    ViewState["lstDocumentos"] = JsonHelper.JsonSerializer(lstDocumentos);
                    HttpContext.Current.Session["rows"] = rows;
                    Session["mensaje"] = mensaje;
                    Session["cantidad"] = rows.Count;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                Reporte_Cargar();
                string mensaje = Convert.ToString(Session["mensaje"]);
                int cantidad = Convert.ToInt32(Session["cantidad"]);

                if (cantidad == 0)
                {
                    lblMensaje.Text = mensaje;
                    lblMensaje.CssClass = "mensajeError";
                }
                else {
                    lblMensaje.Text = mensaje;
                    lblMensaje.CssClass = "mensajeExito";
                }
                
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                grdDocumentos01.ExportSettings.FileName = "CostProdComparativo_" + DateTime.Now.ToString("yyyyMMddHmm");
                grdDocumentos01.ExportSettings.ExportOnlyData = true;
                grdDocumentos01.ExportSettings.IgnorePaging = true;
                grdDocumentos01.ExportSettings.OpenInNewWindow = true;
                grdDocumentos01.MasterTableView.ExportToExcel();
                Page.Response.ClearHeaders();
                Page.Response.ClearContent();
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }
        }

        protected void btnGraph_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");
            try
            {
                if (grdDocumentos01.SelectedIndexes.Count > 0)
                {
                    var rowIndex = Convert.ToInt32(grdDocumentos01.SelectedIndexes[0]);
                    var rows = (List<DataRow>)HttpContext.Current.Session["rows"];
                    var list = new List<string>();
                    foreach (var column in rows[rowIndex].Table.Columns)
                    //GrdMarkBook is Data Grid name
                    {
                        string HeaderName = column.ToString();

                        //  This line Used for find any Column Have Name With Exam
                        if (HeaderName.Contains("201") && !HeaderName.Contains("Variaci"))
                        {
                            list.Add(HeaderName);
                        }
                    }

                    var source = new List<MesCosto>();

                    foreach (string name in list)
                    {
                        var objCast = string.IsNullOrEmpty(rows[rowIndex][name].ToString())
                            ? "0"
                            : rows[rowIndex][name].ToString();
                        var elemento = new MesCosto
                        {
                            mes = name.ToString(),
                            costoUnitario = Convert.ToDecimal(objCast)
                        };

                        source.Add(elemento);
                    }

                    RadHtmlChart1.Visible = true;
                    grdDocumentos01.Visible = false;
                    PanelGraph.Visible = true;
                    PanelGrid.Visible = false;
                    btnBack.Visible = true;
                    btnGraph.Visible = false;
                    RadHtmlChart1.DataSource = source;
                    RadHtmlChart1.DataBind();
                }

                else {
                    rwmReporte.RadAlert("Ud. Debe seleccionar una fila", 500, 100, "Error al mostrar imagen", "");
                }

            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al mostrar imagen", "");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                RadHtmlChart1.Visible = false;
                grdDocumentos01.Visible = true;
                PanelGraph.Visible = false;
                PanelGrid.Visible = true;
                btnBack.Visible = false;
                btnGraph.Visible = true;
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al regresar", "");
            }
        }

        

        //protected void grdDocumentos_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        //{
        //    if (Session["Usuario"] == null)
        //        Response.Redirect("~/Security/frmCerrar.aspx");

        //    try
        //    {
        //        grdDocumentos01.DataSource = JsonHelper.JsonDeserialize<List<Reporte_CostoProduccionResult>>((string)ViewState["lstDocumentos"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        rwmReporte.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
        //    }
        //}

        protected void grdDocumentos01_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {

                //var auxdt = (DataTable)ViewState["dtLstDocumento"]; HttpContext.Current.Session["rows"] = rows;
               // grdDocumentos01.DataSource = JsonHelper.JsonDeserialize<List<Reporte_CostoProduccionResult>>((string)ViewState["lstDocumentos"]);
                grdDocumentos01.DataSource = (List<DataRow>)HttpContext.Current.Session["rows"];
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error recargar la informacion en el formulario.", "");
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static DataTable ToDataTable<T>(IEnumerable<T> collection, string tableName)
        {
            DataTable tbl = ToDataTable(collection);
            tbl.TableName = tableName;
            return tbl;
        }

        public static DataTable ToDataTable<T>(IEnumerable<T> collection)
        {
            DataTable dt = new DataTable();
            Type t = typeof(T);
            PropertyInfo[] pia = t.GetProperties();
            object temp;
            DataRow dr;

            for (int i = 0; i < pia.Length; i++)
            {
                dt.Columns.Add(pia[i].Name, Nullable.GetUnderlyingType(pia[i].PropertyType) ?? pia[i].PropertyType);
                dt.Columns[i].AllowDBNull = true;
            }

            //Populate the table
            foreach (T item in collection)
            {
                dr = dt.NewRow();
                dr.BeginEdit();

                for (int i = 0; i < pia.Length; i++)
                {
                    temp = pia[i].GetValue(item, null);
                    if (temp == null || (temp.GetType().Name == "Char" && ((char)temp).Equals('\0')))
                    {
                        dr[pia[i].Name] = (object)DBNull.Value;
                    }
                    else
                    {
                        dr[pia[i].Name] = temp;
                    }
                }

                dr.EndEdit();
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }

    public class MesCosto
    {
        public string mes { get; set; }
        public decimal costoUnitario { get; set; }
    }
}