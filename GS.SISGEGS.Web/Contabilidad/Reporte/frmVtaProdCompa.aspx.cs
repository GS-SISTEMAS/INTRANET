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
using GS.SISGEGS.Web.FormaPagoWCF;

namespace GS.SISGEGS.Web.Contabilidad.Reporte
{
    public partial class frmVtaProdCompa : System.Web.UI.Page
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

                    FormaPago_Cargar(); 
                    dpFecInicio.SelectedDate = DateTime.Now.AddMonths(-1);
                    dpFecFinal.SelectedDate = DateTime.Now;
                    dpFecVariacion.SelectedDate = DateTime.Now;
                    grdDocumentos01.Width = Unit.Pixel(2800);

                    Reporte_Cargar();
                }
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }
        }

        private void FormaPago_Cargar()
        {
            FormaPagoWCFClient objFormaPagoWCF;
            try
            {
                objFormaPagoWCF = new FormaPagoWCFClient();
                cboTipoPago.DataSource = objFormaPagoWCF.FormaPago_Listar(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario).ToList();
                cboTipoPago.DataTextField = "Nombre";
                cboTipoPago.DataValueField = "ID";
                cboTipoPago.DataBind();
                cboTipoPago.Items.Insert(0, new RadComboBoxItem("Todos", "99"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Reporte_Cargar()
        {
            List<Reporte_CostoVentaResult> lstDocumentos;
            try
            {
                ReporteContabilidadWCFClient objReporteContabilidadWCF = new ReporteContabilidadWCFClient();
                grdDocumentos01.Columns.Clear();
                //valores seleccionados
                DateTime fechaInicio = new DateTime(dpFecInicio.SelectedDate.Value.Year, dpFecInicio.SelectedDate.Value.Month, 1);
                DateTime fechaFinal = new DateTime(dpFecFinal.SelectedDate.Value.Year, dpFecFinal.SelectedDate.Value.Month, 1).AddMonths(1).AddDays(-1);
                int idMoneda = ddlMoneda.SelectedIndex == -1 ? 1 : Convert.ToInt32(ddlMoneda.SelectedValue);
                int mesEvaluar = this.dpFecVariacion.SelectedDate.Value.Month;
                int kardex = string.IsNullOrEmpty(this.txtKardex.Text.Trim()) ? 0 : Convert.ToInt32(this.txtKardex.Text.Trim());

                int mesInicio = dpFecInicio.SelectedDate.Value.Month;
                int mesFin = dpFecFinal.SelectedDate.Value.Month;

                int difMesesFecha = 12*(fechaFinal.Year - fechaInicio.Year) +  (mesFin - mesInicio) + 1;

                if (fechaInicio > fechaFinal) {
                    throw new System.ArgumentException("Ud. debe ingresar un periodo final mayor o igual al periodo inicial");
                }

                if ((mesInicio < mesFin && mesEvaluar < mesInicio) || (mesInicio < mesFin && mesEvaluar > mesFin) || (mesInicio > mesFin && mesEvaluar < mesInicio && mesEvaluar > mesFin))
                {
                    throw new System.ArgumentException("Ud. debe ingresar un mes evaluado que pertenezca al rango del Periodo Inicio y Final");
                }

                if (difMesesFecha > 12)
                {
                    throw new System.ArgumentException("El rango no debe de superar los 12 meses");
                }

                if (this.txtKardex.Text.Trim().Length > 0 && this.txtKardex.Text.Trim().Length < 4) {
                    throw new System.ArgumentException("Ud. debe ingresar mínimo 4 dígitos, para iniciar la búsqueda");
                }

                if (mesInicio > mesFin)
                {
                    mesFin = mesFin + 12;
                }

                int difMeses = (mesFin - mesInicio) + 1;

                int id_forma = int.Parse(cboTipoPago.SelectedValue); 

                lstDocumentos = objReporteContabilidadWCF.ReporteCostoVenta(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                 idMoneda, fechaInicio, fechaFinal, mesEvaluar, kardex, id_forma).ToList();

                Dictionary<int, string> MesesAnio = new Dictionary<int, string>();
                MesesAnio.Add(1, "Enero");
                MesesAnio.Add(2, "Febrero");
                MesesAnio.Add(3, "Marzo");
                MesesAnio.Add(4, "Abril");
                MesesAnio.Add(5, "Mayo");
                MesesAnio.Add(6, "Junio");
                MesesAnio.Add(7, "Julio");
                MesesAnio.Add(8, "Agosto");
                MesesAnio.Add(9, "Setiembre");
                MesesAnio.Add(10, "Octubre");
                MesesAnio.Add(11, "Noviembre");
                MesesAnio.Add(12, "Diciembre");

                GridBoundColumn columnaGrid;

                columnaGrid = new GridBoundColumn();
                grdDocumentos01.Columns.Add(columnaGrid);
                columnaGrid.DataField = "Marca";
                columnaGrid.HeaderText = "Marca";
                columnaGrid.HeaderStyle.Width = Unit.Pixel(100);

                columnaGrid = new GridBoundColumn();
                grdDocumentos01.Columns.Add(columnaGrid);
                columnaGrid.DataField = "NombreFKNiv01";
                columnaGrid.HeaderText = "NombreFKNiv01";
                columnaGrid.HeaderStyle.Width = Unit.Pixel(100);

                columnaGrid = new GridBoundColumn();
                grdDocumentos01.Columns.Add(columnaGrid);
                columnaGrid.DataField = "NombreFKNiv02";
                columnaGrid.HeaderText = "NombreFKNiv02";
                columnaGrid.HeaderStyle.Width = Unit.Pixel(100);


                columnaGrid = new GridBoundColumn();
                grdDocumentos01.Columns.Add(columnaGrid);
                columnaGrid.DataField = "kardex";
                columnaGrid.HeaderText = "Nro. Kardex";
                columnaGrid.HeaderStyle.Width = Unit.Pixel(45);

 
                columnaGrid = new GridBoundColumn();
                grdDocumentos01.Columns.Add(columnaGrid);
                columnaGrid.DataField = "descripcion";
                columnaGrid.HeaderText = "Descripción";
                columnaGrid.HeaderStyle.Width = Unit.Pixel(300);

                columnaGrid = new GridBoundColumn();
                grdDocumentos01.Columns.Add(columnaGrid);
                columnaGrid.DataField = "cantidad_ME";
                columnaGrid.HeaderText = "Cantidad " + MesesAnio[mesEvaluar];
                columnaGrid.DataFormatString = "{0:#,#0.00}";
                columnaGrid.HeaderStyle.Width = Unit.Pixel(80);

                columnaGrid = new GridBoundColumn();
                grdDocumentos01.Columns.Add(columnaGrid);
                columnaGrid.DataField = "formapago";
                columnaGrid.HeaderText = "Forma de Pago";
                columnaGrid.HeaderStyle.Width = Unit.Pixel(150);

                columnaGrid = new GridBoundColumn();
                grdDocumentos01.Columns.Add(columnaGrid);
                columnaGrid.DataField = "moneda";
                columnaGrid.HeaderText = "Moneda";
                columnaGrid.HeaderStyle.Width = Unit.Pixel(150);

                columnaGrid = new GridBoundColumn();
                grdDocumentos01.Columns.Add(columnaGrid);
                columnaGrid.DataField = "precioventa_me";
                columnaGrid.HeaderText = "Precio Total - " + MesesAnio[mesEvaluar];
                columnaGrid.DataFormatString = "{0:#,#0.00}";
                columnaGrid.HeaderStyle.Width = Unit.Pixel(80);

                columnaGrid = new GridBoundColumn();
                grdDocumentos01.Columns.Add(columnaGrid);
                columnaGrid.DataField = "costoventa_me";
                columnaGrid.HeaderText = "Costo Total - " + MesesAnio[mesEvaluar];
                columnaGrid.DataFormatString = "{0:#,#0.00}";
                columnaGrid.HeaderStyle.Width = Unit.Pixel(80);

                columnaGrid = new GridBoundColumn();
                grdDocumentos01.Columns.Add(columnaGrid);
                columnaGrid.DataField = "margen_me";
                columnaGrid.HeaderText = "Margen Total - " + MesesAnio[mesEvaluar];
                columnaGrid.DataFormatString = "{0:#,#0.00}";
                columnaGrid.HeaderStyle.Width = Unit.Pixel(80);

                

                for (int i = 0; i <= difMeses - 1; i++)
                {

                    string cadena_mes = "";
                    string cadena_anio = "";

                    if ((mesInicio + i) > 12)
                    {
                        cadena_mes = MesesAnio[(mesInicio + i) - 12];
                        cadena_anio = fechaFinal.Year.ToString();
                    }
                    else
                    {
                        cadena_mes = MesesAnio[mesInicio + i];
                        cadena_anio = fechaInicio.Year.ToString();
                    }

                    columnaGrid = new GridBoundColumn();
                    grdDocumentos01.Columns.Add(columnaGrid);
                    columnaGrid.DataField = "Q_" + cadena_mes;
                    columnaGrid.HeaderText = "Cantidad " + cadena_mes.ToUpper().Substring(0, 3) + " - " + cadena_anio;
                    columnaGrid.DataFormatString = "{0:#,#0}";
                    columnaGrid.HeaderStyle.Width = Unit.Pixel(80);

                    columnaGrid = new GridBoundColumn();
                    grdDocumentos01.Columns.Add(columnaGrid);
                    columnaGrid.DataField = "pv_" + cadena_mes;
                    columnaGrid.HeaderText = "Precio Vta Uni " + cadena_mes.ToUpper().Substring(0, 3) + " - " + cadena_anio;
                    columnaGrid.DataFormatString = "{0:#,#0.00}";
                    columnaGrid.HeaderStyle.Width = Unit.Pixel(80);

                    columnaGrid = new GridBoundColumn();
                    grdDocumentos01.Columns.Add(columnaGrid);
                    columnaGrid.DataField = "ct_" + cadena_mes;
                    columnaGrid.HeaderText = "Costo Uni - " + cadena_mes.ToUpper().Substring(0, 3) + " - " + cadena_anio;
                    columnaGrid.DataFormatString = "{0:#,#0.00}";
                    columnaGrid.HeaderStyle.Width = Unit.Pixel(80);

                    columnaGrid = new GridBoundColumn();
                    grdDocumentos01.Columns.Add(columnaGrid);
                    columnaGrid.DataField = "mg_" + cadena_mes;
                    columnaGrid.HeaderText = "Margen Uni - " + cadena_mes.ToUpper().Substring(0, 3) + " - " + cadena_anio;
                    columnaGrid.DataFormatString = "{0:#,#0.00}";
                    columnaGrid.HeaderStyle.Width = Unit.Pixel(80);

                }

                columnaGrid = new GridBoundColumn();
                grdDocumentos01.Columns.Add(columnaGrid);
                columnaGrid.DataField = "variacion";
                columnaGrid.HeaderText = "% de Variación";
                columnaGrid.DataFormatString = "{0:#,#0.00}";
                columnaGrid.HeaderStyle.Width = Unit.Pixel(80);

                grdDocumentos01.DataSource = lstDocumentos;
                grdDocumentos01.DataBind();

                lblRegistros.Text = "Se han encontrado " + lstDocumentos.Count.ToString() + " registros";
                lblRegistros.CssClass = "mensajeExito";


                ViewState["lstDocumentos"] = JsonHelper.JsonSerializer(lstDocumentos);
                HttpContext.Current.Session["rows"] = lstDocumentos;
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
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 300, 120,"Mensaje", "");                
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (grdDocumentos01.Items.Count == 0)
                {
                    throw new System.ArgumentException("No se puede descargar el reporte porque no hay registros en el listado");
                }

                grdDocumentos01.ExportSettings.FileName = "CostVtaComparativo_" + DateTime.Now.ToString("yyyyMMddHmm");
                grdDocumentos01.ExportSettings.ExportOnlyData = true;
                grdDocumentos01.ExportSettings.IgnorePaging = true;
                grdDocumentos01.ExportSettings.OpenInNewWindow = true;
                grdDocumentos01.MasterTableView.ExportToExcel();

                Page.Response.ClearHeaders();
                Page.Response.ClearContent();
            }
            catch (ArgumentException ex1)
            {
                rwmReporte.RadAlert(ex1.Message, 300, 120, "Mensaje", "");  
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
                    //var rows = (List<DataRow>)HttpContext.Current.Session["rows"];
                    List<Reporte_CostoVentaResult> listaSource = (List<Reporte_CostoVentaResult>)HttpContext.Current.Session["rows"];
                    int? kardex = listaSource[rowIndex].Kardex;

                    int mesInicio = dpFecInicio.SelectedDate.Value.Month;
                    int mesFin = dpFecFinal.SelectedDate.Value.Month;

                    var listaFiltro = (from tbl in listaSource
                                       where tbl.Kardex == kardex
                                       group tbl by new { tbl.Kardex, tbl.Descripcion } into KardexAgrupado
                                       select new
                                       {
                                           nroKardex = KardexAgrupado.Key.Kardex,
                                           Descripcion = KardexAgrupado.Key.Descripcion,
                                           Enero = KardexAgrupado.Sum(x => x.MG_Enero),
                                           Febrero = KardexAgrupado.Sum(x => x.MG_Febrero),
                                           Marzo = KardexAgrupado.Sum(x => x.MG_Marzo),
                                           Abril = KardexAgrupado.Sum(x => x.MG_Abril),
                                           Mayo = KardexAgrupado.Sum(x => x.MG_Mayo),
                                           Junio = KardexAgrupado.Sum(x => x.MG_Junio),
                                           Julio = KardexAgrupado.Sum(x => x.MG_Julio),
                                           Agosto = KardexAgrupado.Sum(x => x.MG_Agosto),
                                           Setiembre = KardexAgrupado.Sum(x => x.MG_Setiembre),
                                           Octubre = KardexAgrupado.Sum(x => x.MG_Octubre),
                                           Noviembre = KardexAgrupado.Sum(x => x.MG_Noviembre),
                                           Diciembre = KardexAgrupado.Sum(x => x.MG_Diciembre)
                                       }).ToList();

                    if (listaFiltro.Count == 1)
                    {
                        List<MesCosto> source = new List<MesCosto>();

                        Dictionary<int, string> MesesAnio = new Dictionary<int, string>();
                        MesesAnio.Add(1, "Enero");
                        MesesAnio.Add(2, "Febrero");
                        MesesAnio.Add(3, "Marzo");
                        MesesAnio.Add(4, "Abril");
                        MesesAnio.Add(5, "Mayo");
                        MesesAnio.Add(6, "Junio");
                        MesesAnio.Add(7, "Julio");
                        MesesAnio.Add(8, "Agosto");
                        MesesAnio.Add(9, "Setiembre");
                        MesesAnio.Add(10, "Octubre");
                        MesesAnio.Add(11, "Noviembre");
                        MesesAnio.Add(12, "Diciembre");


                        if (mesInicio > mesFin)
                        {
                            mesFin = mesFin + 12;
                        }

                        int difMeses = (mesFin - mesInicio) + 1;

                        for (int i = 0; i <= difMeses - 1; i++)
                        {

                            string cadena_mes = "";
                            //string cadena_anio = "";

                            if ((mesInicio + i) > 12)
                            {
                                cadena_mes = MesesAnio[(mesInicio + i) - 12];
                                //cadena_anio = fechaFinal.Year.ToString();
                            }
                            else
                            {
                                cadena_mes = MesesAnio[mesInicio + i];
                                //cadena_anio = fechaInicio.Year.ToString();
                            }

                            MesCosto itemMes = new MesCosto();
                            itemMes.mes = cadena_mes;

                            switch (cadena_mes)
                            {
                                case "Enero":
                                    itemMes.costoUnitario = Convert.ToDecimal(listaFiltro[0].Enero);
                                    break;
                                case "Febrero":
                                    itemMes.costoUnitario = Convert.ToDecimal(listaFiltro[0].Febrero);
                                    break;
                                case "Marzo":
                                    itemMes.costoUnitario = Convert.ToDecimal(listaFiltro[0].Marzo);
                                    break;
                                case "Abril":
                                    itemMes.costoUnitario = Convert.ToDecimal(listaFiltro[0].Abril);
                                    break;
                                case "Mayo":
                                    itemMes.costoUnitario = Convert.ToDecimal(listaFiltro[0].Mayo);
                                    break;
                                case "Junio":
                                    itemMes.costoUnitario = Convert.ToDecimal(listaFiltro[0].Junio);
                                    break;
                                case "Julio":
                                    itemMes.costoUnitario = Convert.ToDecimal(listaFiltro[0].Julio);
                                    break;
                                case "Agosto":
                                    itemMes.costoUnitario = Convert.ToDecimal(listaFiltro[0].Agosto);
                                    break;
                                case "Setiembre":
                                    itemMes.costoUnitario = Convert.ToDecimal(listaFiltro[0].Setiembre);
                                    break;
                                case "Octubre":
                                    itemMes.costoUnitario = Convert.ToDecimal(listaFiltro[0].Octubre);
                                    break;
                                case "Noviembre":
                                    itemMes.costoUnitario = Convert.ToDecimal(listaFiltro[0].Noviembre);
                                    break;
                                case "Diciembre":
                                    itemMes.costoUnitario = Convert.ToDecimal(listaFiltro[0].Diciembre);
                                    break;
                                default:
                                    itemMes.costoUnitario = 0;
                                    break;
                            }

                            //itemMes.costoUnitario = listaFiltro[0]. 


                            source.Add(itemMes);

                        }

                        lblTitulo.Text = "Costo de ventas por producto " + dpFecInicio.SelectedDate.Value.Month.ToString("00") + "-" + dpFecInicio.SelectedDate.Value.Year.ToString() +
                            " hasta " + dpFecFinal.SelectedDate.Value.Month.ToString("00") + "-" + dpFecFinal.SelectedDate.Value.Year.ToString();

                        RadHtmlChart1.ChartTitle.Text = "Fluctuación del % Margen Unitario por Kardex " + kardex.ToString().Trim()
                            + " : " + listaFiltro[0].Descripcion;

                        RadHtmlChart1.Visible = true;
                        grdDocumentos01.Visible = false;
                        PanelGraph.Visible = true;
                        PanelGrid.Visible = false;
                        btnBack.Visible = true;
                        btnGraph.Visible = false;
                        RadHtmlChart1.DataSource = source;
                        RadHtmlChart1.DataBind();
                    }
                }
                else
                {
                    throw new System.ArgumentException("Por favor seleccione un registro de la lista de Costos de Ventas");
                }
            }
            catch (ArgumentException ex1) {
                rwmReporte.RadAlert(ex1.Message, 300, 120, "Mensaje", "");  
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
                grdDocumentos01.DataSource = (List<gsReporteCostoVenta>)HttpContext.Current.Session["rows"];
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error recargar la informacion en el formulario.", "Mensaje");
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
    /*    public class MesCosto
        {
            public string mes { get; set; }
            public decimal costoUnitario { get; set; }
        }
     * */

}