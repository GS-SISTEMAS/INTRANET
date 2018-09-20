using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.DM;
using GS.SISGEGS.Web.ReporteVentaWCF;
using System.Drawing;
using Telerik.Web.UI;
using GS.SISGEGS.Web.Helpers;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;
using System.Data;
using System.Reflection; 

namespace GS.SISGEGS.Web.Comercial.Reportes.ReporteBI
{
    public partial class frmReporteVenta_Resultados : System.Web.UI.Page
    {
     

        string padding = "padding-top:1px; padding-left:3px; padding-right:3px; padding-bottom:1px;";
        string fontHeders = "font-size: 9pt; font-weight:bold;";
        string fontHeders2 = "font-size: 8pt; font-weight:bold;";
        string fontHeders3 = "font-size: 7pt; font-weight:bold;";
        string fontAlignR = "text-align:right;";
        string fontAlignC = "text-align:center;";
        string fontAlignL = "text-align:left;";
        string border = "border:1px solid #000000;";
        string BackColor1 = "background-color:#F9CFAB;";
        string BackColor2 = "background-color:#C6C8FC;";
        string BackColor3 = "background-color:#DCB4F0;";
        string BackColor4 = "background-color:#A3FF5B;";
        string BackColor5 = "background-color:#A6FFBE;";
        string BackColor6 = "background-color:#F3FF5B;";
        string BackColorRojo = "background-color:#FF0000;";
        string fontBlanco = "color:#FFFFFF;";
        string KPI_String = "";

        private void Zona_Cargar()
        {
            try
            {
                ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient();

                List<Listar_Zona_BIResult> lista = new List<Listar_Zona_BIResult>();

                DateTime fechaInicio = new DateTime(dpFecInicio.SelectedDate.Value.Year, dpFecInicio.SelectedDate.Value.Month, 1);
                DateTime fechaFinal = dpFecFinal.SelectedDate.Value.AddMonths(1);
                fechaFinal = (new DateTime(fechaFinal.Year, fechaFinal.Month, 1)).AddDays(-1);

                lista = objReporteVentaWCF.Listar_Zona_BI(
                           ((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, fechaInicio, fechaFinal, "Lima").ToList();



                var lstSect = from x in lista
                              select new
                              {

                                  DisplayField = x.Nombre_Zona
                                  //DisplayField = String.Format("{0} {1} {2} {3}", x.Placa, x.Modelo, x.Marca, x.Agenda)
                              };

                cboZona.DataSource = lstSect;
                cboZona.DataTextField = "DisplayField";
                cboZona.DataValueField = "DisplayField";
                cboZona.DataBind();

                cboZona.Items.Insert(0, new RadComboBoxItem("TODOS", string.Empty));

                cboZona.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Unidad_Cargar()
        {
            try
            {
                ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient();

                List<Listar_Unidad_BIResult> lista = new List<Listar_Unidad_BIResult>();

                DateTime fechaInicio = new DateTime(dpFecInicio.SelectedDate.Value.Year, dpFecInicio.SelectedDate.Value.Month, 1);
                DateTime fechaFinal = dpFecFinal.SelectedDate.Value.AddMonths(1);
                fechaFinal = (new DateTime(fechaFinal.Year, fechaFinal.Month, 1)).AddDays(-1);

                lista = objReporteVentaWCF.Listar_Unidad_BI(
                           ((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();


                var lstSect = from x in lista
                              select new
                              {
                                  DisplayField = x.NombrePCNiv03
                              };


                cboUnidad.DataSource = lstSect;
                cboUnidad.DataTextField = "DisplayField";
                cboUnidad.DataValueField = "DisplayField";
                cboUnidad.DataBind();

                cboUnidad.Items.Insert(0, new RadComboBoxItem("TODOS", string.Empty));

                cboUnidad.SelectedIndex = 0;
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

            try {
                if (!Page.IsPostBack) {
                    //Telerik.Web.UI.RadSkinManager.GetCurrent(this.Page).ApplySkin(gsReporteVentas_Familia, "BlackMetroTouch");
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    dpFecInicio.SelectedDate = DateTime.Now.AddMonths(-6);
                    dpFecFinal.SelectedDate = DateTime.Now;
                    Zona_Cargar();
                    Unidad_Cargar(); 


                }
            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            string detalleRows = "";

            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            lblMensaje.Text = ""; 

            try {

                tblReporte.CellPadding = 10;
                ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient();
                List<ReporteVentas_EstadoResultadosResult> Lista = new List<ReporteVentas_EstadoResultadosResult>();
                string nombre_zona = "";
                string nombre_unidad = "";


                if(cboUnidad.SelectedItem.Text == "TODOS")
                {
                    nombre_unidad = null; 
                }
                else
                {
                    nombre_unidad = cboUnidad.SelectedItem.Text;
                }
                if (cboZona.SelectedItem.Text == "TODOS")
                {
                    nombre_zona = null;
                }
                else
                {
                    nombre_zona = cboZona.SelectedItem.Text;
                }

                DateTime fechaInicio = new DateTime(dpFecInicio.SelectedDate.Value.Year, dpFecInicio.SelectedDate.Value.Month, 1);
                DateTime fechaFinal = dpFecFinal.SelectedDate.Value.AddMonths(1);
                fechaFinal = (new DateTime(fechaFinal.Year, fechaFinal.Month, 1)).AddDays(-1);
                Lista = objReporteVentaWCF.ReporteVentas_Resultados(
                    ((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, fechaInicio, fechaFinal, nombre_zona, nombre_unidad).ToList();

                //Lista = Lista.FindAll(n => n.Nombre_Zona == nombre_zona).ToList(); 

                DataTable dtTabla = ToDataTable(Lista);

                int intAño = fechaFinal.Year ;
                int intMes = fechaFinal.Month;
                int intAñoAnterior = intAño - 1; 


                //var listaTipo = Lista.Select(s => new { s.Tipo }).Distinct().OrderBy(s => s.Tipo).ToList();
                //var listaZona = Lista.Select(s => new { s.Nombre_Zona }).Distinct().OrderBy(s => s.Nombre_Zona).ToList(); 


                string Cabecera1 = "";
                string Cabecera2 = "";
                string Cabecera3 = "";
                string Cabecera4 = "";
                string detalleReporte = "";

                string Detalle = ""; 
                string Periodo;

                Detalle = Cabcera_Detalle(intAño);

                string formatDetalle = string.Format(Detalle, padding, fontHeders3, border);


                string separador = "";
                separador = separador + "<td ROWSPAN='21' >";
                separador = separador + "&nbsp;&nbsp;&nbsp;&nbsp;";
                separador = separador + "</td>";

                decimal Actual =0;
                decimal PPTO =0;
                decimal Anterior = 0;
                decimal PPTO_Real = 0;
                decimal Real_Anterior = 0;
                decimal Porcentaje1 = 0;
                decimal Porcentaje2 = 0;
                decimal Porcentaje3 = 0;
                decimal Porcentaje4 = 0;
                decimal Porcentaje5 = 0;

                for (int y = 1; y < 18; y++)
                {
                    detalleReporte = detalleReporte + "<tr>";
                    string datoString = ""; 
                    string strConcepto = destalle_Concepto(y, ref datoString);

                    detalleReporte = detalleReporte + strConcepto; 

                    for (int x = 0; x < 2; x++)
                    {
                        Actual = 0;
                        PPTO = 0;
                        Anterior = 0;
                        PPTO_Real = 0;
                        Real_Anterior = 0;
                        Porcentaje1 = 0;
                        Porcentaje2 = 0;
                        Porcentaje3 = 0;
                        Porcentaje4 = 0;
                        Porcentaje5 = 0;


                        if (datoString.Trim().Length > 0)
                        {
                            destalle_Porcentajes(Lista, intAño, intMes, intAñoAnterior, x, y, ref Porcentaje1, ref Porcentaje2, ref Porcentaje3, ref Porcentaje4, ref Porcentaje5, ref Actual, ref PPTO, ref Anterior, ref PPTO_Real, ref Real_Anterior);

                            // 1
                            detalleReporte = detalleReporte + "<td style=' {0} {1} {2}  {3}' >";
                            detalleReporte = detalleReporte + string.Format("{0:##,###0}", Actual);
                            detalleReporte = detalleReporte + "</td>";

                            detalleReporte = detalleReporte + "<td style=' {0} {1} {2}  {3}' >";
                            detalleReporte = detalleReporte + string.Format("{0:##,###0}%", Porcentaje1);
                            detalleReporte = detalleReporte + "</td>";

                            // 2
                            detalleReporte = detalleReporte + "<td style=' {0} {1} {2}  {3}' >";
                            detalleReporte = detalleReporte + string.Format("{0:##,###0}", PPTO);
                            detalleReporte = detalleReporte + "</td>";

                            detalleReporte = detalleReporte + "<td style=' {0} {1} {2} {3}' >";
                            detalleReporte = detalleReporte + string.Format("{0:##,###0}%", Porcentaje2);
                            detalleReporte = detalleReporte + "</td>";

                            //3
                            detalleReporte = detalleReporte + "<td style=' {0} {1} {2}  {3}' >";
                            detalleReporte = detalleReporte + string.Format("{0:##,###0}", Anterior);
                            detalleReporte = detalleReporte + "</td>";

                            detalleReporte = detalleReporte + "<td style=' {0} {1} {2}  {3}' >";
                            detalleReporte = detalleReporte + string.Format("{0:##,###0}%", Porcentaje3);
                            detalleReporte = detalleReporte + "</td>";

                            //4
                            detalleReporte = detalleReporte + "<td style=' {0} {1} {2}  {3}' >";
                            detalleReporte = detalleReporte + string.Format("{0:##,###0}", PPTO_Real);
                            detalleReporte = detalleReporte + "</td>";

                            detalleReporte = detalleReporte + "<td style=' {0} {1} {2}  {3}' >";
                            detalleReporte = detalleReporte + string.Format("{0:##,###0}%", Porcentaje4);
                            detalleReporte = detalleReporte + "</td>";

                            //5
                            detalleReporte = detalleReporte + "<td style=' {0} {1} {2}  {3}' >";
                            detalleReporte = detalleReporte + string.Format("{0:##,###0}", Real_Anterior);
                            detalleReporte = detalleReporte + "</td>";

                            detalleReporte = detalleReporte + "<td style=' {0} {1} {2}  {3}' >";
                            detalleReporte = detalleReporte + string.Format("{0:##,###0}%", Porcentaje5);
                            detalleReporte = detalleReporte + "</td>";

                            detalleReporte = string.Format(detalleReporte, padding, fontHeders3, border, fontAlignR);
                        }
                        else
                        {
                            // 1
                            detalleReporte = detalleReporte + "<td style=' {0} {1} {2}  {3}  {4}' >&nbsp;&nbsp;";
                            detalleReporte = detalleReporte + "</td>";
                            detalleReporte = detalleReporte + "<td style=' {0} {1} {2}  {3} {4}' >&nbsp;&nbsp;";
                            detalleReporte = detalleReporte + "</td>";
                            // 2
                            detalleReporte = detalleReporte + "<td style=' {0} {1} {2}  {3} {4}' >&nbsp;&nbsp;";
                            detalleReporte = detalleReporte + "</td>";
                            detalleReporte = detalleReporte + "<td style=' {0} {1} {2}  {3} {4}' >&nbsp;&nbsp;";
                            detalleReporte = detalleReporte + "</td>";
                            //3
                            detalleReporte = detalleReporte + "<td style=' {0} {1} {2}  {3} {4}' >&nbsp;&nbsp;";
                            detalleReporte = detalleReporte + "</td>";
                            detalleReporte = detalleReporte + "<td style=' {0} {1} {2}  {3} {4}' >&nbsp;&nbsp;";
                            detalleReporte = detalleReporte + "</td>";
                            //4
                            detalleReporte = detalleReporte + "<td style=' {0} {1} {2}  {3} {4}' >&nbsp;&nbsp;";
                            detalleReporte = detalleReporte + "</td>";
                            detalleReporte = detalleReporte + "<td style=' {0} {1} {2}  {3} {4}' >&nbsp;&nbsp;";
                            detalleReporte = detalleReporte + "</td>";
                            //5
                            detalleReporte = detalleReporte + "<td style=' {0} {1} {2}  {3} {4}' >&nbsp;&nbsp;";
                            detalleReporte = detalleReporte + "</td>";
                            detalleReporte = detalleReporte + "<td style=' {0} {1} {2}  {3} {4}' >&nbsp;&nbsp;";
                            detalleReporte = detalleReporte + "</td>";
                            detalleReporte = string.Format(detalleReporte, padding, fontHeders3, border, fontAlignR, BackColor5);
                        }
                    }

                    detalleReporte = detalleReporte + "</tr>";
                }


                string variable = ""; 
                for (int y = 0; y<2; y++)
                {
                    if(y>0)
                    {
                        variable = "Acumulado " + intAño.ToString() ; 
                    }
                    else
                    {
                        string nombreMes = Nombre_Mes(intMes); 
                        variable = "Mes " + nombreMes;
                    }

                    Cabecera1 = Cabecera1 + "<td colspan='10'   style='width:450px; text-align:center;  {0} {1} {2} {3}' >";
                    Cabecera1 = Cabecera1 + variable;
                    Cabecera1 = Cabecera1 + "</td>";
                    Cabecera1 = Cabecera1 + separador; 

                    Cabecera2 = Cabecera2 + "<td ROWSPAN='2' colspan='2' style='text-align:center;  {0} {1} {2}' >";
                    Periodo = intAño.ToString() + "-" + intMes.ToString();
                    Cabecera2 = Cabecera2 + Periodo;
                    Cabecera2 = Cabecera2 + "</td>";
                    Cabecera2 = string.Format(Cabecera2, padding, fontHeders2, border);

                    Cabecera2 = Cabecera2 + "<td ROWSPAN='2' colspan='2' style='text-align:center;  {0} {1} {2}' >";
                    Cabecera2 = Cabecera2 + "PPTO " + Periodo;
                    Cabecera2 = Cabecera2 + "</td>";
                    Cabecera2 = string.Format(Cabecera2, padding, fontHeders2, border);

                    Cabecera2 = Cabecera2 + "<td ROWSPAN='2' colspan='2' style='text-align:center; {0} {1} {2}' >";
                    Periodo = (intAño - 1).ToString() + "-" + intMes.ToString();
                    Cabecera2 = Cabecera2 + Periodo;
                    Cabecera2 = Cabecera2 + "</td>";
                    Cabecera2 = string.Format(Cabecera2, padding, fontHeders2, border);

                    Cabecera2 = Cabecera2 + "<td colspan='4' style='text-align:center; {0} {1} {2}' >";
                    Cabecera2 = Cabecera2 + "Variaciones";
                    Cabecera2 = Cabecera2 + "</td>";
                    Cabecera2 = string.Format(Cabecera2, padding, fontHeders2, border);


                    Cabecera3 = Cabecera3 + "<td colspan='2' style='text-align:center; {0} {1} {2}' >";
                    Cabecera3 = Cabecera3 + "PPTOvsReal" + intAño.ToString();
                    Cabecera3 = Cabecera3 + "</td>";
                    Cabecera3 = string.Format(Cabecera3, padding, fontHeders2, border);

                    Cabecera3 = Cabecera3 + "<td colspan='2' style='text-align:center; {0} {1} {2}' >";
                    Cabecera3 = Cabecera3 + intAño.ToString() + "vs" + (intAño - 1).ToString();
                    Cabecera3 = Cabecera3 + "</td>";
                    Cabecera3 = string.Format(Cabecera3, padding, fontHeders2, border);

                    Cabecera4 = Cabecera4 + formatDetalle;

                }
                string formatCabecera1 = string.Format(Cabecera1, padding, fontHeders, border, BackColor6);
                string formatCabecera2 = string.Format(Cabecera2, padding, fontHeders2, border);
                string formatCabecera3 = string.Format(Cabecera3, padding, fontHeders2, border);
                string formatCabecera4 = string.Format(Cabecera4, padding, fontHeders2, border);


                string Descripcion = "Reporte " + cboZona.SelectedItem.Text + "<br>" +  "Unidad: " + cboUnidad.SelectedItem.Text; 
                
                string tablaBody = @"
                  <table style='width:100%; ' BorderWidth='1px'>
			        <tr>
				        <td ROWSPAN='4'   style='width:300px; {0} {1} {2} text-align:center; '>
					        {8}
				        </td>
                        {3}
			        </tr>
                    <tr>  {4} </tr>
                    <tr>  {5} </tr>
                    <tr>  {6} </tr>
                    {7}

	              </table>
                ";

                string formatMensaje = string.Format(tablaBody, 
                                                     padding, fontHeders, border,
                                                     formatCabecera1, formatCabecera2, formatCabecera3, 
                                                     formatCabecera4, detalleReporte, Descripcion
                                                     );

                Session["ReproteVariacion"] = formatMensaje; 

                int rowCtr = 1;
                int rowCnt = 1;
                int cellCtr = 1;
                int cellCnt = 1;
  
                for (rowCtr = 1; rowCtr <= rowCnt; rowCtr++)
                {
                    // Create new row and add it to the table.
                    TableRow tRow = new TableRow();
                    tblReporte.Rows.Add(tRow);
                    for (cellCtr = 1; cellCtr <= cellCnt; cellCtr++)
                    {
                        // Create a new cell and add it to the row.
                        TableCell tCell = new TableCell();
                        tCell.Text = formatMensaje; 
                        tRow.Cells.Add(tCell);
                    }
                }


            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        protected string Cabcera_Detalle(int intAño)
        {
            string Detalle = "";

            // Miles
            Detalle = Detalle + "<td style='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + "$"; 
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td style='{3} {4}  {0} {1} {2}'>";
            Detalle = Detalle + "%";
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td style='{3} {4}  {0} {1} {2}'>";
            Detalle = Detalle + "$";
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td style='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + "%";
            Detalle = Detalle + "</td>";
            Detalle = Detalle + "<td style='{3} {4}  {0} {1} {2}'>";
            Detalle = Detalle + "$";
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td style='{3} {4}  {0} {1} {2}'>";
            Detalle = Detalle + "%";
            Detalle = Detalle + "</td>";
            Detalle = Detalle + "<td style='{3} {4}  {0} {1} {2}'>";
            Detalle = Detalle + "$";
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td style='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + "%";
            Detalle = Detalle + "</td>";
            Detalle = Detalle + "<td style='{3} {4}  {0} {1} {2}'>";
            Detalle = Detalle + "$";
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td style='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + "%";
            Detalle = Detalle + "</td>";

            Detalle = string.Format(Detalle, padding, fontHeders3, border, fontAlignC, fontAlignC);
   
 
            return Detalle; 
        }

        protected string destalle_Concepto(int intConcepto, ref string datoString)
        {
            string Detalle = "";
            string Concepto = ""; 

            int c = intConcepto; 
            switch (c)
            {
                case 1:
                    Concepto = "Ingresos de Explotación"; 
                    break;
                case 2:
                    Concepto = "Costos de Explotación";
                    break;
                case 3:
                    Concepto = "Utilidad Bruta / Margen Bruto %";
                    break;
                case 4:
                    Concepto = " ";
                    break;
                case 5:
                    Concepto = "Gastos de Ventas";
                    break;
                case 6:
                    Concepto = "Gasto de Ventas Directas";
                    break;
                case 7:
                    Concepto = "Gasto de Ventas indirectas";
                    break;
                case 8:
                    Concepto = "Gastos de Administración";
                    break;
                case 9:
                    Concepto = "Utilidad Operacional / Margen Operacional";
                    break;
                case 10:
                    Concepto = " ";
                    break;
                case 11:
                    Concepto = "Neto de Ingresos y Gastos Financieros";
                    break;
                case 12:
                    Concepto = "Resultado no Operacional";
                    break;
                case 13:
                    Concepto = " ";
                    break;
                case 14:
                    Concepto = "Resultado antes de Impuestos";
                    break;
                case 15:
                    Concepto = "Participación a Trabajadores";
                    break;
                case 16:
                    Concepto = "Impuesto a la Renta";
                    break;
                case 17:
                    Concepto = "Utilidad del Ejercicio / Margen Utilidad %";
                    break;
                default:
                    Concepto = " ";
                    break;
            }

            // Miles
            Detalle = Detalle + "<td style='{3} {0} {1} {2}'>";
            Detalle = Detalle + Concepto;
            Detalle = Detalle + "</td>";

            Detalle = string.Format(Detalle, padding, fontHeders2, border, fontAlignL);
            datoString = Concepto; 
            return Detalle;
        }

        protected void destalle_Porcentajes(List<ReporteVentas_EstadoResultadosResult> Lista, int intAño, int intMes, int intAñoAnterior, int x, int intConcepto, 
            ref decimal Porcentaje1, ref decimal Porcentaje2, ref decimal Porcentaje3, ref decimal Porcentaje4, ref decimal Porcentaje5,
            ref decimal Actual, ref decimal PPTO, ref decimal Anterior, ref decimal PPTO_Real, ref decimal Real_Anterior
            )
        {
            string Concepto = ""; 
            int c = intConcepto;
            decimal VentaActual = 0 ;
            decimal VentaPPTO = 0;
            decimal VentaAnterior = 0;
            Actual = 0;
            PPTO = 0;
            Anterior = 0;
            PPTO_Real = 0;
            Real_Anterior = 0; 

            switch (c)
            {
                case 1:
                    Concepto = "Ventas";

                    if (x == 0)
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto1 = row;
                                Actual = Actual + (decimal)objeto1.SaldoDolares;
                                PPTO = PPTO + (decimal)objeto1.VentaPPTO_Dolares;
                            }
                            if (row.Año == intAñoAnterior && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto2 = row;
                                Anterior = Anterior + (decimal)objeto2.SaldoDolares;
                            }
                        }
                    }
                    else
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño)
                            {
                                Actual = Actual + (decimal)row.SaldoDolares;
                                PPTO = PPTO + (decimal)row.VentaPPTO_Dolares;

                            }
                            else if (row.Año == intAñoAnterior)
                            {
                                Anterior = Anterior + (decimal)row.SaldoDolares;
                            }
                        }

                    }

                    PPTO_Real = Actual - PPTO;
                    Real_Anterior = Actual - Anterior;

                    if (Actual == 0)
                    { Porcentaje1 = 0; }
                    else
                    { Porcentaje1 = (Actual / Actual) * 100; }

                    if (PPTO == 0)
                    { Porcentaje2 = 0; }
                    else
                    { Porcentaje2 = (PPTO / PPTO) * 100; }

                    if (Anterior == 0)
                    { Porcentaje3 = 0; }
                    else
                    { Porcentaje3 = (Anterior / Anterior) * 100; }

                    if (PPTO == 0)
                    { Porcentaje4 = 0; }
                    else
                    { Porcentaje4 = (Actual / PPTO) * 100; }

                    if (Anterior == 0)
                    { Porcentaje5 = 0; }
                    else
                    { Porcentaje5 = ((Actual / Anterior) - 1) * 100; }

                    break;

                case 2:
                    Concepto = "Costos de Explotación";

                    if (x == 0)
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto1 = row;
                                VentaActual = VentaActual+ (decimal)objeto1.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)objeto1.VentaPPTO_Dolares;
                                Actual = Actual + (decimal)objeto1.CostoDolares;
                                PPTO = PPTO + (decimal)objeto1.CostoPPTO_Dolares;
                            }
                            if (row.Año == intAñoAnterior && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto2 = row;
                                VentaAnterior = VentaAnterior + (decimal)objeto2.SaldoDolares;
                                Anterior = Anterior + (decimal)objeto2.CostoDolares;
                            }
                        }
                    }
                    else
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño)
                            {
                                VentaActual = VentaActual + (decimal)row.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)row.VentaPPTO_Dolares;

                                Actual = Actual + (decimal)row.CostoDolares;
                                PPTO = PPTO + (decimal)row.CostoPPTO_Dolares;

                            }
                            else if (row.Año == intAñoAnterior)
                            {
                                VentaAnterior = VentaAnterior + (decimal)row.SaldoDolares;
                                Anterior = Anterior + (decimal)row.CostoDolares;
                            }
                        }
                    }

                    PPTO_Real = Actual - PPTO;
                    Real_Anterior = Actual - Anterior;

                    if (VentaActual == 0)
                    { Porcentaje1 = 0; }
                    else
                    { Porcentaje1 = (Actual/VentaActual) * 100;}

                    if (VentaPPTO == 0)
                    { Porcentaje2 = 0; }
                    else
                    { Porcentaje2 = (PPTO /VentaPPTO) * 100; }

                    if (VentaAnterior == 0)
                    { Porcentaje3 = 0; }
                    else
                    { Porcentaje3 = (Anterior/VentaAnterior) * 100; }


                    if (PPTO == 0)
                    { Porcentaje4 = 0; }
                    else
                    { Porcentaje4 = (Actual / PPTO) * 100; }

                    if (Anterior == 0)
                    { Porcentaje5 = 0; }
                    else
                    { Porcentaje5 = ((Actual / Anterior) - 1) * 100; }


                    break;
                case 3:
                    Concepto = "Utilidad Bruta / Margen Bruto %";

                    if (x == 0)
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto1 = row;
                                VentaActual = VentaActual + (decimal)objeto1.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)objeto1.VentaPPTO_Dolares;
                                Actual = Actual + (decimal)objeto1.Margen;
                                PPTO = PPTO + (decimal)objeto1.MargenPPTO;
                            }
                            if (row.Año == intAñoAnterior && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto2 = row;
                                VentaAnterior = (decimal)objeto2.SaldoDolares;
                                Anterior = (decimal)objeto2.Margen;
                            }
                        }
                    }
                    else
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño)
                            {
                                VentaActual = VentaActual + (decimal)row.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)row.VentaPPTO_Dolares;

                                Actual = Actual + (decimal)row.Margen;
                                PPTO = PPTO + (decimal)row.MargenPPTO;

                            }
                            else if (row.Año == intAñoAnterior)
                            {
                                VentaAnterior = VentaAnterior + (decimal)row.SaldoDolares;
                                Anterior = Anterior + (decimal)row.Margen;
                            }
                        }
                    }

                    PPTO_Real = Actual - PPTO;
                    Real_Anterior = Actual - Anterior;

                    if (VentaActual == 0)
                    { Porcentaje1 = 0; }
                    else
                    { Porcentaje1 = (Actual / VentaActual) * 100; }

                    if (VentaPPTO == 0)
                    { Porcentaje2 = 0; }
                    else
                    { Porcentaje2 = (PPTO / VentaPPTO) * 100; }

                    if (VentaAnterior == 0)
                    { Porcentaje3 = 0; }
                    else
                    { Porcentaje3 = (Anterior / VentaAnterior) * 100; }


                    if (PPTO == 0)
                    { Porcentaje4 = 0; }
                    else
                    { Porcentaje4 = (Actual / PPTO) * 100; }

                    if (Anterior == 0)
                    { Porcentaje5 = 0; }
                    else
                    { Porcentaje5 = ((Actual / Anterior) - 1) * 100; }

                    break;
                case 4:
                    Concepto = "";
                    break;
                case 5:
                    Concepto = "Gastos de Ventas";

                    if (x == 0)
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto1 = row;
                                VentaActual = VentaActual + (decimal)objeto1.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)objeto1.VentaPPTO_Dolares;
                                Actual = Actual + (decimal)objeto1.GastoVentaDolares;
                                PPTO = PPTO + (decimal)objeto1.GastoVentaDolaresPPTO;
                            }
                            if (row.Año == intAñoAnterior && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto2 = row;
                                VentaAnterior = VentaAnterior + (decimal)objeto2.SaldoDolares;
                                Anterior = Anterior + (decimal)objeto2.GastoVentaDolares;
                            }
                        }
                    }
                    else
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño)
                            {
                                VentaActual = VentaActual + (decimal)row.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)row.VentaPPTO_Dolares;

                                Actual = Actual + (decimal)row.GastoVentaDolares;
                                PPTO =     PPTO + (decimal)row.GastoVentaDolaresPPTO;

                            }
                            else if (row.Año == intAñoAnterior)
                            {
                                VentaAnterior = VentaAnterior + (decimal)row.SaldoDolares;
                                Anterior = Anterior + (decimal)row.GastoVentaDolares;
                            }
                        }
                    }

                    PPTO_Real = Actual - PPTO;
                    Real_Anterior = Actual - Anterior;

                    if (VentaActual == 0)
                    { Porcentaje1 = 0; }
                    else
                    { Porcentaje1 = (Actual / VentaActual) * 100; }

                    if (VentaPPTO == 0)
                    { Porcentaje2 = 0; }
                    else
                    { Porcentaje2 = (PPTO / VentaPPTO) * 100; }

                    if (VentaAnterior == 0)
                    { Porcentaje3 = 0; }
                    else
                    { Porcentaje3 = (Anterior / VentaAnterior) * 100; }


                    if (PPTO == 0)
                    { Porcentaje4 = 0; }
                    else
                    { Porcentaje4 = (Actual / PPTO) * 100; }

                    if (Anterior == 0)
                    { Porcentaje5 = 0; }
                    else
                    { Porcentaje5 = ((Actual / Anterior) - 1) * 100; }


                    break;
                case 6:
                    Concepto = "Gasto de Ventas Directas";

                    if (x == 0)
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto1 = row;
                                VentaActual = VentaActual + (decimal)objeto1.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)objeto1.VentaPPTO_Dolares;
                                Actual = Actual + (decimal)objeto1.GastoVentaDirecto;
                                PPTO = PPTO + (decimal)objeto1.GastoVentaDirectoPPTO;
                            }
                            if (row.Año == intAñoAnterior && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto2 = row;
                                VentaAnterior = VentaAnterior + (decimal)objeto2.SaldoDolares;
                                Anterior = Anterior + (decimal)objeto2.GastoVentaDirecto;
                            }
                        }
                    }
                    else
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño)
                            {
                                VentaActual = VentaActual + (decimal)row.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)row.VentaPPTO_Dolares;

                                Actual = Actual + (decimal)row.GastoVentaDirecto;
                                PPTO = PPTO + (decimal)row.GastoVentaDirectoPPTO;

                            }
                            else if (row.Año == intAñoAnterior)
                            {
                                VentaAnterior = VentaAnterior + (decimal)row.SaldoDolares;
                                Anterior = Anterior + (decimal)row.GastoVentaDirecto;
                            }
                        }
                    }

                    PPTO_Real = Actual - PPTO;
                    Real_Anterior = Actual - Anterior;

                    if (VentaActual == 0)
                    { Porcentaje1 = 0; }
                    else
                    { Porcentaje1 = (Actual / VentaActual) * 100; }

                    if (VentaPPTO == 0)
                    { Porcentaje2 = 0; }
                    else
                    { Porcentaje2 = (PPTO / VentaPPTO) * 100; }

                    if (VentaAnterior == 0)
                    { Porcentaje3 = 0; }
                    else
                    { Porcentaje3 = (Anterior / VentaAnterior) * 100; }


                    if (PPTO == 0)
                    { Porcentaje4 = 0; }
                    else
                    { Porcentaje4 = (Actual / PPTO) * 100; }

                    if (Anterior == 0)
                    { Porcentaje5 = 0; }
                    else
                    { Porcentaje5 = ((Actual / Anterior) - 1) * 100; }


                    break;
                case 7:
                    Concepto = "Gasto de Ventas indirectas";

                    if (x == 0)
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto1 = row;
                                VentaActual = VentaActual + (decimal)objeto1.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)objeto1.VentaPPTO_Dolares;
                                Actual = Actual + (decimal)objeto1.GastoVentaIndirecto;
                                PPTO = PPTO + (decimal)objeto1.GastoVentaIndirectoPPTO;
                            }
                            if (row.Año == intAñoAnterior && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto2 = row;
                                VentaAnterior = VentaAnterior + (decimal)objeto2.SaldoDolares;
                                Anterior = Anterior + (decimal)objeto2.GastoVentaIndirecto;
                            }
                        }
                    }
                    else
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño)
                            {
                                VentaActual = VentaActual + (decimal)row.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)row.VentaPPTO_Dolares;

                                Actual = Actual + (decimal)row.GastoVentaIndirecto;
                                PPTO = PPTO + (decimal)row.GastoVentaIndirectoPPTO;

                            }
                            else if (row.Año == intAñoAnterior)
                            {
                                VentaAnterior = VentaAnterior + (decimal)row.SaldoDolares;
                                Anterior = Anterior + (decimal)row.GastoVentaIndirecto;
                            }
                        }
                    }

                    PPTO_Real = Actual - PPTO;
                    Real_Anterior = Actual - Anterior;

                    if (VentaActual == 0)
                    { Porcentaje1 = 0; }
                    else
                    { Porcentaje1 = (Actual / VentaActual) * 100; }

                    if (VentaPPTO == 0)
                    { Porcentaje2 = 0; }
                    else
                    { Porcentaje2 = (PPTO / VentaPPTO) * 100; }

                    if (VentaAnterior == 0)
                    { Porcentaje3 = 0; }
                    else
                    { Porcentaje3 = (Anterior / VentaAnterior) * 100; }


                    if (PPTO == 0)
                    { Porcentaje4 = 0; }
                    else
                    { Porcentaje4 = (Actual / PPTO) * 100; }

                    if (Anterior == 0)
                    { Porcentaje5 = 0; }
                    else
                    { Porcentaje5 = ((Actual / Anterior) - 1) * 100; }


                    break;
                case 8:
                    Concepto = "Gastos de Administración";

                    if (x == 0)
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto1 = row;
                                VentaActual = VentaActual + (decimal)objeto1.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)objeto1.VentaPPTO_Dolares;
                                Actual = Actual + (decimal)objeto1.GastoAdminDolaresD;
                                PPTO = PPTO + (decimal)objeto1.GastoAdministrativoD_PPTO;
                            }
                            if (row.Año == intAñoAnterior && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto2 = row;
                                VentaAnterior = VentaAnterior + (decimal)objeto2.SaldoDolares;
                                Anterior = Anterior + (decimal)objeto2.GastoAdminDolaresD;
                            }
                        }
                    }
                    else
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño)
                            {
                                VentaActual = VentaActual + (decimal)row.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)row.VentaPPTO_Dolares;

                                Actual = Actual + (decimal)row.GastoAdminDolaresD;
                                PPTO = PPTO + (decimal)row.GastoAdministrativoD_PPTO;

                            }
                            else if (row.Año == intAñoAnterior)
                            {
                                VentaAnterior = VentaAnterior + (decimal)row.SaldoDolares;
                                Anterior = Anterior + (decimal)row.GastoAdminDolaresD;
                            }
                        }
                    }

                    PPTO_Real = Actual - PPTO;
                    Real_Anterior = Actual - Anterior;

                    if (VentaActual == 0)
                    { Porcentaje1 = 0; }
                    else
                    { Porcentaje1 = (Actual / VentaActual) * 100; }

                    if (VentaPPTO == 0)
                    { Porcentaje2 = 0; }
                    else
                    { Porcentaje2 = (PPTO / VentaPPTO) * 100; }

                    if (VentaAnterior == 0)
                    { Porcentaje3 = 0; }
                    else
                    { Porcentaje3 = (Anterior / VentaAnterior) * 100; }


                    if (PPTO == 0)
                    { Porcentaje4 = 0; }
                    else
                    { Porcentaje4 = (Actual / PPTO) * 100; }

                    if (Anterior == 0)
                    { Porcentaje5 = 0; }
                    else
                    { Porcentaje5 = ((Actual / Anterior) - 1) * 100; }


                    break;
                case 9:
                    Concepto = "Utilidad Operacional / Margen Operacional";

                    if (x == 0)
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto1 = row;
                                VentaActual = VentaActual + (decimal)objeto1.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)objeto1.VentaPPTO_Dolares;
                                Actual = Actual + (decimal)objeto1.utilidadGA;
                                PPTO = PPTO + (decimal)objeto1.UtilidadGA_PPTO;
                            }
                            if (row.Año == intAñoAnterior && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto2 = row;
                                VentaAnterior = (decimal)objeto2.SaldoDolares;
                                Anterior = (decimal)objeto2.utilidadGA;
                            }
                        }
                    }
                    else
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño)
                            {
                                VentaActual = VentaActual + (decimal)row.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)row.VentaPPTO_Dolares;

                                Actual = Actual + (decimal)row.utilidadGA;
                                PPTO = PPTO + (decimal)row.UtilidadGA_PPTO;

                            }
                            else if (row.Año == intAñoAnterior)
                            {
                                VentaAnterior = VentaAnterior + (decimal)row.SaldoDolares;
                                Anterior = Anterior + (decimal)row.utilidadGA;
                            }
                        }
                    }

                    PPTO_Real = Actual - PPTO;
                    Real_Anterior = Actual - Anterior;

                    if (VentaActual == 0)
                    { Porcentaje1 = 0; }
                    else
                    { Porcentaje1 = (Actual / VentaActual) * 100; }

                    if (VentaPPTO == 0)
                    { Porcentaje2 = 0; }
                    else
                    { Porcentaje2 = (PPTO / VentaPPTO) * 100; }

                    if (VentaAnterior == 0)
                    { Porcentaje3 = 0; }
                    else
                    { Porcentaje3 = (Anterior / VentaAnterior) * 100; }


                    if (PPTO == 0)
                    { Porcentaje4 = 0; }
                    else
                    { Porcentaje4 = (Actual / PPTO) * 100; }

                    if (Anterior == 0)
                    { Porcentaje5 = 0; }
                    else
                    { Porcentaje5 = ((Actual / Anterior) - 1) * 100; }


                    break;
                case 10:
                    Concepto = " ";
                    break;
                case 11:
                    Concepto = "Neto de Ingresos y Gastos Financieros";

                    if (x == 0)
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto1 = row;
                                VentaActual = VentaActual + (decimal)objeto1.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)objeto1.VentaPPTO_Dolares;
                                Actual = Actual + (decimal)objeto1.GastoFinanDolares + (decimal)objeto1.GastoOtrosDolares;
                                PPTO = PPTO + (decimal)objeto1.GastoFinanDolaresPPTO + (decimal)objeto1.GastoOtrosDolaresPPTO;
                            }
                            if (row.Año == intAñoAnterior && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto2 = row;
                                VentaAnterior = VentaAnterior + (decimal)objeto2.SaldoDolares;
                                Anterior = Anterior + (decimal)objeto2.GastoFinanDolares + (decimal)objeto2.GastoOtrosDolares;
                            }
                        }
                    }
                    else
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño)
                            {
                                VentaActual = VentaActual + (decimal)row.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)row.VentaPPTO_Dolares;

                                Actual = Actual + (decimal)row.GastoFinanDolares + (decimal)row.GastoOtrosDolares;
                                PPTO = PPTO + (decimal)row.GastoFinanDolaresPPTO + (decimal)row.GastoOtrosDolaresPPTO;

                            }
                            else if (row.Año == intAñoAnterior)
                            {
                                VentaAnterior = VentaAnterior + (decimal)row.SaldoDolares;
                                Anterior = Anterior + (decimal)row.GastoFinanDolares + (decimal)row.GastoOtrosDolares;
                            }
                        }
                    }

                    PPTO_Real = Actual - PPTO;
                    Real_Anterior = Actual - Anterior;

                    if (VentaActual == 0)
                    { Porcentaje1 = 0; }
                    else
                    { Porcentaje1 = (Actual / VentaActual) * 100; }

                    if (VentaPPTO == 0)
                    { Porcentaje2 = 0; }
                    else
                    { Porcentaje2 = (PPTO / VentaPPTO) * 100; }

                    if (VentaAnterior == 0)
                    { Porcentaje3 = 0; }
                    else
                    { Porcentaje3 = (Anterior / VentaAnterior) * 100; }


                    if (PPTO == 0)
                    { Porcentaje4 = 0; }
                    else
                    { Porcentaje4 = (Actual / PPTO) * 100; }

                    if (Anterior == 0)
                    { Porcentaje5 = 0; }
                    else
                    { Porcentaje5 = ((Actual / Anterior) - 1) * 100; }


                    break;
                case 12:
                    Concepto = "Resultado no Operacional";

                    if (x == 0)
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto1 = row;
                                VentaActual = VentaActual + (decimal)objeto1.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)objeto1.VentaPPTO_Dolares;
                                Actual = Actual  + (decimal)objeto1.GastoFinanDolares + (decimal)objeto1.GastoOtrosDolares;
                                PPTO = PPTO + (decimal)objeto1.GastoFinanDolaresPPTO + (decimal)objeto1.GastoOtrosDolaresPPTO;
                            }
                            if (row.Año == intAñoAnterior && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto2 = row;
                                VentaAnterior = VentaAnterior + (decimal)objeto2.SaldoDolares;
                                Anterior = Anterior + (decimal)objeto2.GastoFinanDolares + (decimal)objeto2.GastoOtrosDolares;
                            }
                        }
                    }
                    else
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño)
                            {
                                VentaActual = VentaActual + (decimal)row.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)row.VentaPPTO_Dolares;

                                Actual = Actual + (decimal)row.GastoFinanDolares + (decimal)row.GastoOtrosDolares;
                                PPTO = PPTO + (decimal)row.GastoFinanDolaresPPTO + (decimal)row.GastoOtrosDolaresPPTO;

                            }
                            else if (row.Año == intAñoAnterior)
                            {
                                VentaAnterior = VentaAnterior + (decimal)row.SaldoDolares;
                                Anterior = Anterior + (decimal)row.GastoFinanDolares + (decimal)row.GastoOtrosDolares;
                            }
                        }
                    }

                    PPTO_Real = Actual - PPTO;
                    Real_Anterior = Actual - Anterior;

                    if (VentaActual == 0)
                    { Porcentaje1 = 0; }
                    else
                    { Porcentaje1 = (Actual / VentaActual) * 100; }

                    if (VentaPPTO == 0)
                    { Porcentaje2 = 0; }
                    else
                    { Porcentaje2 = (PPTO / VentaPPTO) * 100; }

                    if (VentaAnterior == 0)
                    { Porcentaje3 = 0; }
                    else
                    { Porcentaje3 = (Anterior / VentaAnterior) * 100; }


                    if (PPTO == 0)
                    { Porcentaje4 = 0; }
                    else
                    { Porcentaje4 = (Actual / PPTO) * 100; }

                    if (Anterior == 0)
                    { Porcentaje5 = 0; }
                    else
                    { Porcentaje5 = ((Actual / Anterior) - 1) * 100; }

                    break;
                case 13:
                    Concepto = " ";
                    break;
                case 14:
                    Concepto = "Resultado antes de Impuestos";

                    if (x == 0)
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto1 = row;
                                VentaActual = VentaActual + (decimal)objeto1.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)objeto1.VentaPPTO_Dolares;
                                Actual = Actual + (decimal)objeto1.UtilidadGF;
                                PPTO = PPTO + (decimal)objeto1.UtilidadGF_PPTO;
                            }
                            if (row.Año == intAñoAnterior && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto2 = row;
                                VentaAnterior = VentaAnterior + (decimal)objeto2.SaldoDolares;
                                Anterior = Anterior + (decimal)objeto2.UtilidadGF;
                            }
                        }
                    }
                    else
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño)
                            {
                                VentaActual = VentaActual + (decimal)row.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)row.VentaPPTO_Dolares;

                                Actual = Actual +  (decimal)row.UtilidadGF;
                                PPTO = PPTO +  (decimal)row.UtilidadGF_PPTO;

                            }
                            else if (row.Año == intAñoAnterior)
                            {
                                VentaAnterior = VentaAnterior + (decimal)row.SaldoDolares;
                                Anterior = Anterior +  (decimal)row.UtilidadGF;
                            }
                        }
                    }

                    PPTO_Real = Actual - PPTO;
                    Real_Anterior = Actual - Anterior;

                    if (VentaActual == 0)
                    { Porcentaje1 = 0; }
                    else
                    { Porcentaje1 = (Actual / VentaActual) * 100; }

                    if (VentaPPTO == 0)
                    { Porcentaje2 = 0; }
                    else
                    { Porcentaje2 = (PPTO / VentaPPTO) * 100; }

                    if (VentaAnterior == 0)
                    { Porcentaje3 = 0; }
                    else
                    { Porcentaje3 = (Anterior / VentaAnterior) * 100; }


                    if (PPTO == 0)
                    { Porcentaje4 = 0; }
                    else
                    { Porcentaje4 = (Actual / PPTO) * 100; }

                    if (Anterior == 0)
                    { Porcentaje5 = 0; }
                    else
                    { Porcentaje5 = ((Actual / Anterior) - 1) * 100; }

                    break;
                case 15:
                    Concepto = "Participación a Trabajadores";

                    if (x == 0)
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto1 = row;
                                VentaActual = VentaActual + (decimal)objeto1.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)objeto1.VentaPPTO_Dolares;
                                Actual = Actual + (decimal)objeto1.Participacion;
                                PPTO = PPTO + (decimal)objeto1.ParticipacionPPTO;
                            }
                            if (row.Año == intAñoAnterior && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto2 = row;
                                VentaAnterior = VentaAnterior + (decimal)objeto2.SaldoDolares;
                                Anterior = Anterior + (decimal)objeto2.Participacion;
                            }
                        }
                    }
                    else
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño)
                            {
                                VentaActual = VentaActual + (decimal)row.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)row.VentaPPTO_Dolares;

                                Actual = Actual + (decimal)row.Participacion;
                                PPTO = PPTO + (decimal)row.ParticipacionPPTO;

                            }
                            else if (row.Año == intAñoAnterior)
                            {
                                VentaAnterior = VentaAnterior + (decimal)row.SaldoDolares;
                                Anterior = Anterior + (decimal)row.Participacion;
                            }
                        }
                    }

                    PPTO_Real = Actual - PPTO;
                    Real_Anterior = Actual - Anterior;

                    if (VentaActual == 0)
                    { Porcentaje1 = 0; }
                    else
                    { Porcentaje1 = (Actual / VentaActual) * 100; }

                    if (VentaPPTO == 0)
                    { Porcentaje2 = 0; }
                    else
                    { Porcentaje2 = (PPTO / VentaPPTO) * 100; }

                    if (VentaAnterior == 0)
                    { Porcentaje3 = 0; }
                    else
                    { Porcentaje3 = (Anterior / VentaAnterior) * 100; }


                    if (PPTO == 0)
                    { Porcentaje4 = 0; }
                    else
                    { Porcentaje4 = (Actual / PPTO) * 100; }

                    if (Anterior == 0)
                    { Porcentaje5 = 0; }
                    else
                    { Porcentaje5 = ((Actual / Anterior) - 1) * 100; }

                    break;
                case 16:
                    Concepto = "Impuesto a la Renta";

                    if (x == 0)
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto1 = row;
                                VentaActual = VentaActual + (decimal)objeto1.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)objeto1.VentaPPTO_Dolares;
                                Actual = Actual + (decimal)objeto1.Impuestos;
                                PPTO = PPTO + (decimal)objeto1.ImpuestosPPTO;
                            }
                            if (row.Año == intAñoAnterior && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto2 = row;
                                VentaAnterior = VentaAnterior + (decimal)objeto2.SaldoDolares;
                                Anterior = Anterior + (decimal)objeto2.Impuestos;
                            }
                        }
                    }
                    else
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño)
                            {
                                VentaActual = VentaActual + (decimal)row.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)row.VentaPPTO_Dolares;

                                Actual = Actual + (decimal)row.Impuestos;
                                PPTO = PPTO + (decimal)row.ImpuestosPPTO;

                            }
                            else if (row.Año == intAñoAnterior)
                            {
                                VentaAnterior = VentaAnterior + (decimal)row.SaldoDolares;
                                Anterior = Anterior + (decimal)row.Impuestos;
                            }
                        }
                    }

                    PPTO_Real = Actual - PPTO;
                    Real_Anterior = Actual - Anterior;

                    if (VentaActual == 0)
                    { Porcentaje1 = 0; }
                    else
                    { Porcentaje1 = (Actual / VentaActual) * 100; }

                    if (VentaPPTO == 0)
                    { Porcentaje2 = 0; }
                    else
                    { Porcentaje2 = (PPTO / VentaPPTO) * 100; }

                    if (VentaAnterior == 0)
                    { Porcentaje3 = 0; }
                    else
                    { Porcentaje3 = (Anterior / VentaAnterior) * 100; }


                    if (PPTO == 0)
                    { Porcentaje4 = 0; }
                    else
                    { Porcentaje4 = (Actual / PPTO) * 100; }

                    if (Anterior == 0)
                    { Porcentaje5 = 0; }
                    else
                    { Porcentaje5 = ((Actual / Anterior) - 1) * 100; }


                    break;
                case 17:
                    Concepto = "Utilidad del Ejercicio / Margen Utilidad %";

                    if (x == 0)
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto1 = row;
                                VentaActual = VentaActual + (decimal)objeto1.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)objeto1.VentaPPTO_Dolares;
                                Actual = Actual + (decimal)objeto1.UtilidadNeta;
                                PPTO = PPTO + (decimal)objeto1.UtilidadNetaPPTO;
                            }
                            if (row.Año == intAñoAnterior && row.Mes == intMes)
                            {
                                ReporteVentas_EstadoResultadosResult objeto2 = row;
                                VentaAnterior = VentaAnterior + (decimal)objeto2.SaldoDolares;
                                Anterior = Anterior  + (decimal)objeto2.UtilidadNeta;
                            }
                        }
                    }
                    else
                    {
                        foreach (ReporteVentas_EstadoResultadosResult row in Lista)
                        {
                            if (row.Año == intAño)
                            {
                                VentaActual = VentaActual + (decimal)row.SaldoDolares;
                                VentaPPTO = VentaPPTO + (decimal)row.VentaPPTO_Dolares;

                                Actual = Actual + (decimal)row.UtilidadNeta;
                                PPTO = PPTO + (decimal)row.UtilidadNetaPPTO;

                            }
                            else if (row.Año == intAñoAnterior)
                            {
                                VentaAnterior = VentaAnterior + (decimal)row.SaldoDolares;
                                Anterior = Anterior + (decimal)row.UtilidadNeta;
                            }
                        }
                    }

                    PPTO_Real = Actual - PPTO;
                    Real_Anterior = Actual - Anterior;

                    if (VentaActual == 0)
                    { Porcentaje1 = 0; }
                    else
                    { Porcentaje1 = (Actual / VentaActual) * 100; }

                    if (VentaPPTO == 0)
                    { Porcentaje2 = 0; }
                    else
                    { Porcentaje2 = (PPTO / VentaPPTO) * 100; }

                    if (VentaAnterior == 0)
                    { Porcentaje3 = 0; }
                    else
                    { Porcentaje3 = (Anterior / VentaAnterior) * 100; }


                    if (PPTO == 0)
                    { Porcentaje4 = 0; }
                    else
                    { Porcentaje4 = (Actual / PPTO) * 100; }

                    if (Anterior == 0)
                    { Porcentaje5 = 0; }
                    else
                    { Porcentaje5 = ((Actual / Anterior) - 1) * 100; }

                    break;
                default:
                    Concepto = " ";
                    break;
            }

 
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
 
            try
            {
                string Mensaje = (string)Session["ReproteVariacion"]; 
                ExporttoExcel_Moneda(Mensaje, "ReproteVariacion");
            }
            catch(Exception ex)
            {

            }
        }

        private void ExporttoExcel_Moneda(string mensaje, string TipoReporte)
        {
            string NombreReporte;

            NombreReporte = TipoReporte; 

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
            HttpContext.Current.Response.Write(mensaje);
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        private string Nombre_Mes(int Mes)

        {
            int intMes = Mes; 
            string mes = "";

            switch (intMes)
            {
                case 1:
                    mes = "Enero";
                    break;
                case 2:
                    mes ="Febrero";
                    break;
                case 3:
                    mes = "Marzo";
                    break;
                case 4:
                    mes ="Abril";
                    break;
                case 5:
                    mes ="Mayo";
                    break;
                case 6:
                    mes ="Junio";
                    break;
                case 7:
                    mes = "Julio";
                    break;
                case 8:
                    mes = "Agosto";
                    break;
                case 9:
                    mes ="Septiembre";
                    break;
                case 10:
                    mes = "Octubre";
                    break;
                case 11:
                    mes ="Noviembre";
                    break;
                case 12:
                    mes = "Diciembre";
                    break;
            }
            return mes;
        }
    }
}