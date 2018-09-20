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
    public partial class frmReporteVenta_Variacion : System.Web.UI.Page
    {


        //string padding = "padding:1px 3px 1px 3px;";
        //string fontHeders = "font-size: 9pt; font-weight:bold;";
        //string fontHeders2 = "text-align:center; font-size: 8pt; font-weight:bold;";
        //string fontHeders3 = "font-size: 7pt; font-weight:bold;";
        //string fontAlignR = "text-align:right;";
        //string fontAlignC = "text-align:center;";
        //string fontAlignL = "text-align:left;";
        //string border = "border:1px solid #000000;";
        //string BackColor1 = "background-color:#F9CFAB;";
        //string BackColor2 = "background-color:#C6C8FC;";
        //string BackColor3 = "background-color:#DCB4F0;";
        //string BackColor4 = "background-color:#A3FF5B;";
        //string BackColor5 = "background-color:#3FE276;";
        //string BackColor6 = "background-color:#F3FF5B;";
        //string BackColorRojo = "background-color:#FF0000;";
        //string fontBlanco = "color:#FFFFFF;";

        string padding = "paddingR";
        string fontHeders = "fontHeders";
        string fontHeders2 = "fontHeders2";
        string fontHeders3 = "fontHeders3";
        string fontAlignR = "fontAlignR";
        string fontAlignC = "fontAlignC";
        string fontAlignL = "fontAlignL";
        string border = "borderR";
        string BackColor1 = "BackColor1";
        string BackColor2 = "BackColor2";
        string BackColor3 = "BackColor3";
        string BackColor4 = "BackColor4";
        string BackColor5 = "BackColor5";
        string BackColor6 = "BackColor6";
        string BackColorRojo = "BackColorRojo";
        string fontBlanco = "fontBlanco";


        string KPI_String = ""; 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try {
                if (!Page.IsPostBack) {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    //Telerik.Web.UI.RadSkinManager.GetCurrent(this.Page).ApplySkin(gsReporteVentas_Familia, "BlackMetroTouch");
                    dpFecInicio.SelectedDate = DateTime.Now.AddMonths(-6);
                    dpFecFinal.SelectedDate = DateTime.Now;
                 
                    //Vendedor_Listar();
                }
            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string Tipo1 = "";
            int countrowTotal = 0;
            int countrowTipo = 0;
            string detalleRows = "";

            decimal Ventas_T = 0;
            decimal VentaPPTO_T = 0;
            decimal VentasAnterior_T = 0;
            decimal KgLto = 0;
            decimal KgLtoPPTO = 0;
            decimal KgLtoAnterior = 0;
            decimal PrecioActual = 0;
            decimal PrecioAnterior = 0;
            decimal PrecioPPTO = 0;
            decimal PrecioVar1 = 0;
            decimal PrecioVar2 = 0;
            decimal Efecto1 = 0;
            decimal Efecto2 = 0;
            decimal Efecto3 = 0;
            decimal Efecto4 = 0;
            ///
            decimal resVentas_T = 0;
            decimal resVentaPPTO_T = 0;
            decimal resVentasAnterior_T = 0;
            decimal resKgLto = 0;
            decimal resKgLtoPPTO = 0;
            decimal resKgLtoAnterior = 0;
            decimal GrandTotal_res = 0;
            decimal GrandTotalKgLt_res = 0;
            decimal calculo = 0;
            decimal cumplimiento = 0;
            decimal GrandTotal = 0;
            decimal GrandTotalKgLt = 0;
            decimal totalcaracteres = 0;
            List<string> listDetalleRows = new List<string>();
 
 
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try {
                //Vendedor_Listar();

                lblMensaje.Text = ""; 


                ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient();
                List<ReporteVentas_ZonasResult> Lista = new List<ReporteVentas_ZonasResult>();

                DateTime fechaInicio = new DateTime(dpFecInicio.SelectedDate.Value.Year, dpFecInicio.SelectedDate.Value.Month, 1);
                DateTime fechaFinal = dpFecFinal.SelectedDate.Value.AddMonths(1);
                fechaFinal = (new DateTime(fechaFinal.Year, fechaFinal.Month, 1)).AddDays(-1);
                Lista = objReporteVentaWCF.ReporteVentas_Zonas(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, fechaInicio, fechaFinal, 0, null).ToList();
 
                DataTable dtTabla = ToDataTable(Lista); 
                
                var listAño = Lista.Select(s => new { s.Año }).Distinct().OrderBy(s => s.Año).ToList();
                var listMeses = Lista.Select(s => new { s.Año, s.Mes }).Distinct().OrderBy(d => d.Mes ).ToList() ;

                var listaTipo = Lista.Select(s => new { s.Tipo }).Distinct().OrderBy(s => s.Tipo).ToList();
                var listaZona = Lista.Select(s => new { s.Tipo, s.Nombre_Zona }).Distinct().OrderBy(s => s.Tipo).ToList(); 

                //DataTable table = new DataTable();
                //table.Columns.Add("Tipo", typeof(string));
                //table.Columns.Add("Zonas", typeof(int));


                foreach (var datoTipo in listaTipo.OrderBy(s => s.Tipo))
                {
                    countrowTipo++;
            
                    detalleRows = detalleRows + "<tr>";
                    detalleRows = detalleRows + "<td  style='height:{3}px;' class ='{0} {1} {2} fix CenterTEXT' >";

                    Tipo1 = datoTipo.Tipo.ToString();
                    detalleRows = detalleRows + Tipo1;
                    detalleRows = detalleRows + "</td>";



                    int count = 0;
                    foreach (var datoZona in listaZona.OrderBy(s => s.Nombre_Zona))
                    {
                        resKgLto = 0;
                        resKgLtoPPTO = 0;
                        resKgLtoAnterior = 0;
                        resVentas_T = 0;
                        resVentaPPTO_T = 0;
                        resVentasAnterior_T = 0;

 
                        if (Tipo1 == datoZona.Tipo)
                        {
                            count = count + 1;
                            countrowTotal++;

                            if(count == 1 )
                            {
                                detalleRows = detalleRows + "<td    class = '{0} {1} {2} fix2' >";
                                detalleRows = detalleRows + datoZona.Nombre_Zona;
                                detalleRows = detalleRows + "</td>";


                                foreach (var datoAño in listAño)
                                {
                                    foreach (var datoMes in listMeses)
                                    {
                                        if (datoMes.Año == datoAño.Año)
                                        {

                                            int intMes = Convert.ToInt32(datoMes.Mes);
                                            int intAñor = Convert.ToInt32(datoMes.Año);
                                            int intZona = 0;


                                            GrandTotal = 0;
                                            GrandTotalKgLt = 0;
                                            foreach (ReporteVentas_ZonasResult zona in Lista)
                                            {
                                                if (zona.Mes == intMes && zona.Año == intAñor)
                                                {
                                                    GrandTotal = GrandTotal + (decimal)zona.SaldoDolares;
                                                    GrandTotalKgLt = GrandTotalKgLt + (decimal)zona.KgLt;
                                                }

                                            }


                                            foreach (ReporteVentas_ZonasResult zona in Lista)
                                            {
                                                if (zona.Tipo == Tipo1 && zona.Nombre_Zona == datoZona.Nombre_Zona && zona.Mes == intMes && zona.Año == intAñor)
                                                {
                                                    intZona++;
                                                    cumplimiento = 0;

                                                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                    detalleRows = detalleRows + string.Format("{0:##,###0}", zona.SaldoDolares);
                                                    detalleRows = detalleRows + "</td>";
                                                    resVentas_T = resVentas_T + (decimal)zona.SaldoDolares;

                                                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                    if (GrandTotal == 0)
                                                    {
                                                        cumplimiento = 0;
                                                    }
                                                    else
                                                    {
                                                        cumplimiento = ((decimal)zona.SaldoDolares / (decimal)GrandTotal) * 100;
                                                    }
                                                    detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                                                    detalleRows = detalleRows + "</td>";

                                                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                    detalleRows = detalleRows + string.Format("{0:##,###0}", zona.VentaPPTO_Dolares);
                                                    detalleRows = detalleRows + "</td>";
                                                    resVentaPPTO_T = resVentaPPTO_T + (decimal)zona.VentaPPTO_Dolares;

                                                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";


                                                    if (zona.VentaPPTO_Dolares == 0)
                                                    {
                                                        cumplimiento = 0;
                                                    }
                                                    else
                                                    {
                                                        cumplimiento = ((decimal)zona.SaldoDolares / (decimal)zona.VentaPPTO_Dolares) * 100;
                                                    }

                                                    detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                                                    detalleRows = detalleRows + "</td>";

                                                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                    detalleRows = detalleRows + string.Format("{0:##,###0}", zona.SaldoAnoMesAnterior_Dolares);
                                                    detalleRows = detalleRows + "</td>";
                                                    resVentasAnterior_T = resVentasAnterior_T + (decimal)zona.SaldoAnoMesAnterior_Dolares;


                                                    cumplimiento = 0;
                                                    if (zona.SaldoAnoMesAnterior_Dolares == 0)
                                                    {
                                                        cumplimiento = 0;
                                                    }
                                                    else
                                                    {
                                                        cumplimiento = (((decimal)zona.SaldoDolares / (decimal)zona.SaldoAnoMesAnterior_Dolares) - 1) * 100;
                                                    }

                                                    if (cumplimiento < 0)
                                                    {
                                                        KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                                        KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                                    }
                                                    else
                                                    {
                                                        KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                                    }
                                                    detalleRows = detalleRows + KPI_String;

                                                    detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                                                    detalleRows = detalleRows + "</td>";



                                                    /// KgLt
                                                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                    detalleRows = detalleRows + String.Format("{0:##,###0}", zona.KgLt);
                                                    detalleRows = detalleRows + "</td>";
                                                    resKgLto = resKgLto + (decimal)zona.KgLt;


                                                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                    if (GrandTotalKgLt == 0)
                                                    {
                                                        cumplimiento = 0;
                                                    }
                                                    else
                                                    {
                                                        cumplimiento = ((decimal)zona.KgLt / (decimal)GrandTotalKgLt) * 100;
                                                    }
                                                    detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                                                    detalleRows = detalleRows + "</td>";

                                                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                    detalleRows = detalleRows + String.Format("{0:##,###0}", zona.KgLtPPTO);
                                                    detalleRows = detalleRows + "</td>";
                                                    resKgLtoPPTO = resKgLtoPPTO + (decimal)zona.KgLtPPTO;

                                                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                    if (zona.KgLtPPTO == 0)
                                                    {
                                                        cumplimiento = 0;
                                                    }
                                                    else
                                                    {
                                                        cumplimiento = ((decimal)zona.KgLt / (decimal)zona.KgLtPPTO) * 100;
                                                    }

                                                    detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                                                    detalleRows = detalleRows + "</td>";

                                                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                    detalleRows = detalleRows + string.Format("{0:##,###0}", zona.KgLt_AnoMesAnterior);
                                                    detalleRows = detalleRows + "</td>";
                                                    resKgLtoAnterior = resKgLtoAnterior + (decimal)zona.KgLt_AnoMesAnterior;

                                                    cumplimiento = 0;
                                                    if (zona.KgLt_AnoMesAnterior == 0)
                                                    {
                                                        cumplimiento = 0;
                                                    }
                                                    else
                                                    {
                                                        cumplimiento = (((decimal)zona.KgLt / (decimal)zona.KgLt_AnoMesAnterior) - 1) * 100;
                                                    }

                                                    if (cumplimiento < 0)
                                                    {
                                                        KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                                        KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                                    }
                                                    else
                                                    {
                                                        KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                                    }
                                                    detalleRows = detalleRows + KPI_String;

                                                    detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                                                    detalleRows = detalleRows + "</td>";


                                                    // Precio

                                                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                    if (zona.KgLt == 0)
                                                    {
                                                        PrecioActual = 0;
                                                    }
                                                    else
                                                    {
                                                        PrecioActual = (((decimal)zona.SaldoDolares / (decimal)zona.KgLt)) * 1000;
                                                    }
                                                    detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioActual);
                                                    detalleRows = detalleRows + "</td>";


                                                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                    if (zona.KgLt_AnoMesAnterior == 0)
                                                    {
                                                        PrecioAnterior = 0;
                                                    }
                                                    else
                                                    {
                                                        PrecioAnterior = (((decimal)zona.SaldoAnoMesAnterior_Dolares / (decimal)zona.KgLt_AnoMesAnterior)) * 1000;
                                                    }
                                                    detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioAnterior);
                                                    detalleRows = detalleRows + "</td>";

                                                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                    if (zona.KgLtPPTO == 0)
                                                    {
                                                        PrecioPPTO = 0;
                                                    }
                                                    else
                                                    {
                                                        PrecioPPTO = (((decimal)zona.VentaPPTO_Dolares / (decimal)zona.KgLtPPTO)) * 1000;
                                                    }
                                                    detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioPPTO);
                                                    detalleRows = detalleRows + "</td>";


                                                    PrecioVar1 = PrecioActual - PrecioAnterior;
                                                    if (PrecioVar1 < 0)
                                                    {
                                                        KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                                        KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                                    }
                                                    else
                                                    {
                                                        KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                                    }
                                                    detalleRows = detalleRows + KPI_String;
                                                    detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioVar1);
                                                    detalleRows = detalleRows + "</td>";


                                                    PrecioVar2 = PrecioActual - PrecioPPTO;
                                                    if (PrecioVar2 < 0)
                                                    {
                                                        KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                                        KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                                    }
                                                    else
                                                    {
                                                        KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                                    }

                                                    detalleRows = detalleRows + KPI_String;
                                                    detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioVar2);
                                                    detalleRows = detalleRows + "</td>";

                                                    //Efecto Precio
                                                    Efecto1 = (((decimal)zona.KgLt_AnoMesAnterior) * PrecioVar1) / 1000;
                                                    if (Efecto1 < 0)
                                                    {
                                                        KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                                        KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                                    }
                                                    else
                                                    {
                                                        KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                                    }

                                                    detalleRows = detalleRows + KPI_String;
                                                    detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto1);
                                                    detalleRows = detalleRows + "</td>";

                                                    Efecto2 = (((decimal)zona.KgLtPPTO) * PrecioVar2) / 1000;
                                                    if (Efecto2 < 0)
                                                    {
                                                        KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                                        KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                                    }
                                                    else
                                                    {
                                                        KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                                    }

                                                    detalleRows = detalleRows + KPI_String;
                                                    detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto2);
                                                    detalleRows = detalleRows + "</td>";

                                                    // Efecto Volumen
                                                    Efecto3 = (((decimal)zona.KgLt - (decimal)zona.KgLt_AnoMesAnterior) * PrecioActual) / 1000;
                                                    if (Efecto3 < 0)
                                                    {
                                                        KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                                        KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                                    }
                                                    else
                                                    {
                                                        KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                                    }

                                                    detalleRows = detalleRows + KPI_String;
                                                    detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto3);
                                                    detalleRows = detalleRows + "</td>";

                                                    Efecto4 = (((decimal)zona.KgLt - (decimal)zona.KgLtPPTO) * PrecioActual) / 1000;
                                                    if (Efecto4 < 0)
                                                    {
                                                        KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                                        KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                                    }
                                                    else
                                                    {
                                                        KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                                    }

                                                    detalleRows = detalleRows + KPI_String;
                                                    detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto4);
                                                    detalleRows = detalleRows + "</td>";
                                                }
                                            }
                                            if (intZona == 0)
                                            {
                                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                                detalleRows = detalleRows + "</td>";
                                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                detalleRows = detalleRows + string.Format("{0:##,###0}%", 0);
                                                detalleRows = detalleRows + "</td>";
                                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                                detalleRows = detalleRows + "</td>";
                                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                detalleRows = detalleRows + string.Format("{0:##,###0}%", 0);
                                                detalleRows = detalleRows + "</td>";
                                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                                detalleRows = detalleRows + "</td>";

                                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                detalleRows = detalleRows + string.Format("{0:##,###0}%", 0);
                                                detalleRows = detalleRows + "</td>";

                                                /// KgLt
                                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                                detalleRows = detalleRows + "</td>";
                                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                detalleRows = detalleRows + string.Format("{0:##,###0}%", 0);
                                                detalleRows = detalleRows + "</td>";
                                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                                detalleRows = detalleRows + "</td>";
                                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                detalleRows = detalleRows + string.Format("{0:##,###0}%", 0);
                                                detalleRows = detalleRows + "</td>";
                                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                                detalleRows = detalleRows + "</td>";

                                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                detalleRows = detalleRows + string.Format("{0:##,###0}%", 0);
                                                detalleRows = detalleRows + "</td>";
                                                // Precio
                                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                                detalleRows = detalleRows + "</td>";
                                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                                detalleRows = detalleRows + "</td>";
                                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                                detalleRows = detalleRows + "</td>";

                                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                                detalleRows = detalleRows + "</td>";

                                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                                detalleRows = detalleRows + "</td>";
                                                //Efecto Precio
                                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                                detalleRows = detalleRows + "</td>";

                                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                                detalleRows = detalleRows + "</td>";

                                                // Efecto Volumen
                                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                                detalleRows = detalleRows + "</td>";

                                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                                detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                                detalleRows = detalleRows + "</td>";
                                            }
                                        }
                                        //------------------------------------

                                    }

                                }

                                // Total de Columnas

                                 GrandTotal_res = 0;
                                 GrandTotalKgLt_res = 0;
                                 calculo = 0; 

                                foreach (ReporteVentas_ZonasResult zona in Lista)
                                {
                                    GrandTotal_res = GrandTotal_res + (decimal)zona.SaldoDolares;
                                    GrandTotalKgLt_res = GrandTotalKgLt_res + (decimal)zona.KgLt;
                                }

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                detalleRows = detalleRows + string.Format("{0:##,###0}", resVentas_T);
                                detalleRows = detalleRows + "</td>";
  
                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                if (GrandTotal_res == 0)
                                {
                                    calculo = 0;
                                }
                                else
                                {
                                    calculo = (resVentas_T/GrandTotal_res) * 100;
                                }

                                detalleRows = detalleRows + string.Format("{0:##,###0}%", calculo);
                                detalleRows = detalleRows + "</td>";

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                detalleRows = detalleRows + string.Format("{0:##,###0}", resVentaPPTO_T);
                                detalleRows = detalleRows + "</td>";
         

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                if (resVentaPPTO_T == 0)
                                {
                                    calculo = 0;
                                }
                                else
                                {
                                    calculo = (resVentas_T/resVentaPPTO_T) * 100;
                                }
                                detalleRows = detalleRows + string.Format("{0:##,###0}%", calculo);
                                detalleRows = detalleRows + "</td>";

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                detalleRows = detalleRows + string.Format("{0:##,###0}", resVentasAnterior_T);
                                detalleRows = detalleRows + "</td>";


                                calculo = 0;
                                if (resVentasAnterior_T == 0)
                                {
                                    calculo = 0;
                                }
                                else
                                {
                                    calculo = ((resVentas_T /resVentasAnterior_T) - 1) * 100;
                                }

                                if (calculo < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                }
                                detalleRows = detalleRows + KPI_String;

                                detalleRows = detalleRows + string.Format("{0:##,###0}%", calculo);
                                detalleRows = detalleRows + "</td>";



                                /// KgLt
                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                detalleRows = detalleRows + String.Format("{0:##,###0}", resKgLto);
                                detalleRows = detalleRows + "</td>";
             
                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                if (GrandTotalKgLt_res == 0)
                                {
                                    calculo = 0;
                                }
                                else
                                {
                                    calculo = (resKgLto /GrandTotalKgLt_res) * 100;
                                }
                                detalleRows = detalleRows + string.Format("{0:##,###0}%", calculo);
                                detalleRows = detalleRows + "</td>";

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                detalleRows = detalleRows + string.Format("{0:##,###0}", resKgLtoPPTO);
                                detalleRows = detalleRows + "</td>";
 
                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                if (resKgLtoPPTO == 0)
                                {
                                    calculo = 0;
                                }
                                else
                                {
                                    calculo = (resKgLto /resKgLtoPPTO) * 100;
                                }

                                detalleRows = detalleRows + string.Format("{0:##,###0}%", calculo);
                                detalleRows = detalleRows + "</td>";

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                detalleRows = detalleRows + string.Format("{0:##,###0}", resKgLtoAnterior);
                                detalleRows = detalleRows + "</td>";
               
                    
                                if (resKgLtoAnterior == 0)
                                {
                                    calculo = 0;
                                }
                                else
                                {
                                    calculo = ((resKgLto / resKgLtoAnterior) - 1) * 100;
                                }

                                if (calculo < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                }
                                detalleRows = detalleRows + KPI_String;

                                detalleRows = detalleRows + string.Format("{0:##,###0}%", calculo);
                                detalleRows = detalleRows + "</td>";


                                // Precio

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                if (resKgLto == 0)
                                {
                                    PrecioActual = 0;
                                }
                                else
                                {
                                    PrecioActual = (( resVentas_T / resKgLto )) * 1000;
                                }
                                detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioActual);
                                detalleRows = detalleRows + "</td>";


                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                if (resKgLtoAnterior == 0)
                                {
                                    PrecioAnterior = 0;
                                }
                                else
                                {
                                    PrecioAnterior = ((resVentasAnterior_T/ resKgLtoAnterior)) * 1000;
                                }
                                detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioAnterior);
                                detalleRows = detalleRows + "</td>";

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                if (resKgLtoPPTO == 0)
                                {
                                    PrecioPPTO = 0;
                                }
                                else
                                {
                                    PrecioPPTO = ((resVentaPPTO_T/ resKgLtoPPTO)) * 1000;
                                }
                                detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioPPTO);
                                detalleRows = detalleRows + "</td>";


                                PrecioVar1 = PrecioActual - PrecioAnterior;
                                if (PrecioVar1 < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                }
                                detalleRows = detalleRows + KPI_String;
                                detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioVar1);
                                detalleRows = detalleRows + "</td>";


                                PrecioVar2 = PrecioActual - PrecioPPTO;
                                if (PrecioVar2 < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                }

                                detalleRows = detalleRows + KPI_String;
                                detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioVar2);
                                detalleRows = detalleRows + "</td>";

                                ////Efecto Precio
                                Efecto1 = ((resKgLtoAnterior) * PrecioVar1) / 1000;
                                if (Efecto1 < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                }

                                detalleRows = detalleRows + KPI_String;
                                detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto1);
                                detalleRows = detalleRows + "</td>";

                                Efecto2 = ((resKgLtoPPTO) * PrecioVar2) / 1000;
                                if (Efecto2 < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                }

                                detalleRows = detalleRows + KPI_String;
                                detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto2);
                                detalleRows = detalleRows + "</td>";

                                // Efecto Volumen
                                Efecto3 = ((resKgLto - resKgLtoAnterior) * PrecioActual) / 1000;
                                if (Efecto3 < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                }

                                detalleRows = detalleRows + KPI_String;
                                detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto3);
                                detalleRows = detalleRows + "</td>";


                                Efecto4 = ((resKgLto - resKgLtoPPTO) * PrecioActual) / 1000;
                                if (Efecto4 < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                }

                                detalleRows = detalleRows + KPI_String;
                                detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto4);
                                detalleRows = detalleRows + "</td>";

                                // Fin sub Columna

                                detalleRows = detalleRows + "</tr>";
                            }
                            else
                            {

 
                                detalleRows = detalleRows + "<tr>";
                                detalleRows = detalleRows + "<td    class = ' {0} {1} {2} fix2' >";
                                detalleRows = detalleRows + datoZona.Nombre_Zona;
                                detalleRows = detalleRows + "</td>";

                                // Detalle 

                                foreach (var dato in listMeses)
                                {
                                    int intMes = Convert.ToInt32(dato.Mes);
                                    int intAñor = Convert.ToInt32(dato.Año);
                                    int intZona = 0;

                                     GrandTotal = 0;
                                     GrandTotalKgLt = 0;
                                    foreach (ReporteVentas_ZonasResult zona in Lista)
                                    {
                                        if (zona.Mes == intMes && zona.Año == intAñor)
                                        {
                                            GrandTotal = GrandTotal + (decimal)zona.SaldoDolares;
                                            GrandTotalKgLt = GrandTotalKgLt + (decimal)zona.KgLt;
                                        }

                                    }


                                    foreach (ReporteVentas_ZonasResult zona in Lista)
                                    {
                                        if (zona.Tipo == Tipo1 && zona.Nombre_Zona == datoZona.Nombre_Zona && zona.Mes == intMes && zona.Año == intAñor)
                                        {
                                            intZona++;
                                            cumplimiento = 0;

                                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                            detalleRows = detalleRows + string.Format("{0:##,###0}", zona.SaldoDolares);
                                            detalleRows = detalleRows + "</td>";
                                            resVentas_T = resVentas_T + (decimal)zona.SaldoDolares;

                                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                            if (GrandTotal == 0)
                                            {
                                                cumplimiento = 0;
                                            }
                                            else
                                            {
                                                cumplimiento = ((decimal)zona.SaldoDolares / (decimal)GrandTotal) * 100;
                                            }
                                            detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                                            detalleRows = detalleRows + "</td>";

                                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                            detalleRows = detalleRows + string.Format("{0:##,###0}", zona.VentaPPTO_Dolares);
                                            detalleRows = detalleRows + "</td>";
                                            resVentaPPTO_T = resVentaPPTO_T + (decimal)zona.VentaPPTO_Dolares;

                                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";


                                            if (zona.VentaPPTO_Dolares == 0)
                                            {
                                                cumplimiento = 0;
                                            }
                                            else
                                            {
                                                cumplimiento = ((decimal)zona.SaldoDolares / (decimal)zona.VentaPPTO_Dolares) * 100;
                                            }

                                            detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                                            detalleRows = detalleRows + "</td>";

                                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                            detalleRows = detalleRows + string.Format("{0:##,###0}", zona.SaldoAnoMesAnterior_Dolares);
                                            detalleRows = detalleRows + "</td>";
                                            resVentasAnterior_T = resVentasAnterior_T + (decimal)zona.SaldoAnoMesAnterior_Dolares;


                                            cumplimiento = 0;
                                            if (zona.SaldoAnoMesAnterior_Dolares == 0)
                                            {
                                                cumplimiento = 0;
                                            }
                                            else
                                            {
                                                cumplimiento = (((decimal)zona.SaldoDolares / (decimal)zona.SaldoAnoMesAnterior_Dolares) - 1) * 100;
                                            }

                                            if (cumplimiento < 0)
                                            {
                                                KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                                KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                            }
                                            else
                                            {
                                                KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                            }
                                            detalleRows = detalleRows + KPI_String;

                                            detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                                            detalleRows = detalleRows + "</td>";



                                            /// KgLt
                                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                            detalleRows = detalleRows + String.Format("{0:##,###0}", zona.KgLt);
                                            detalleRows = detalleRows + "</td>";
                                            resKgLto = resKgLto + (decimal)zona.KgLt;


                                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                            if (GrandTotalKgLt == 0)
                                            {
                                                cumplimiento = 0;
                                            }
                                            else
                                            {
                                                cumplimiento = ((decimal)zona.KgLt / (decimal)GrandTotalKgLt) * 100;
                                            }
                                            detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                                            detalleRows = detalleRows + "</td>";

                                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                            detalleRows = detalleRows + String.Format("{0:##,###0}", zona.KgLtPPTO);
                                            detalleRows = detalleRows + "</td>";
                                            resKgLtoPPTO = resKgLtoPPTO + (decimal)zona.KgLtPPTO;

                                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                            if (zona.KgLtPPTO == 0)
                                            {
                                                cumplimiento = 0;
                                            }
                                            else
                                            {
                                                cumplimiento = ((decimal)zona.KgLt / (decimal)zona.KgLtPPTO) * 100;
                                            }

                                            detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                                            detalleRows = detalleRows + "</td>";

                                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                            detalleRows = detalleRows + string.Format("{0:##,###0}", zona.KgLt_AnoMesAnterior);
                                            detalleRows = detalleRows + "</td>";
                                            resKgLtoAnterior = resKgLtoAnterior + (decimal)zona.KgLt_AnoMesAnterior;

                                            cumplimiento = 0;
                                            if (zona.KgLt_AnoMesAnterior == 0)
                                            {
                                                cumplimiento = 0;
                                            }
                                            else
                                            {
                                                cumplimiento = (((decimal)zona.KgLt / (decimal)zona.KgLt_AnoMesAnterior) - 1) * 100;
                                            }

                                            if (cumplimiento < 0)
                                            {
                                                KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                                KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                            }
                                            else
                                            {
                                                KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                            }
                                            detalleRows = detalleRows + KPI_String;

                                            detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                                            detalleRows = detalleRows + "</td>";


                                            // Precio

                                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                            if (zona.KgLt == 0)
                                            {
                                                PrecioActual = 0;
                                            }
                                            else
                                            {
                                                PrecioActual = (((decimal)zona.SaldoDolares / (decimal)zona.KgLt)) * 1000;
                                            }
                                            detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioActual);
                                            detalleRows = detalleRows + "</td>";


                                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                            if (zona.KgLt_AnoMesAnterior == 0)
                                            {
                                                PrecioAnterior = 0;
                                            }
                                            else
                                            {
                                                PrecioAnterior = (((decimal)zona.SaldoAnoMesAnterior_Dolares / (decimal)zona.KgLt_AnoMesAnterior)) * 1000;
                                            }
                                            detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioAnterior);
                                            detalleRows = detalleRows + "</td>";

                                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                            if (zona.KgLtPPTO == 0)
                                            {
                                                PrecioPPTO = 0;
                                            }
                                            else
                                            {
                                                PrecioPPTO = (((decimal)zona.VentaPPTO_Dolares / (decimal)zona.KgLtPPTO)) * 1000;
                                            }
                                            detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioPPTO);
                                            detalleRows = detalleRows + "</td>";


                                            PrecioVar1 = PrecioActual - PrecioAnterior;
                                            if (PrecioVar1 < 0)
                                            {
                                                KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                                KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                            }
                                            else
                                            {
                                                KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                            }
                                            detalleRows = detalleRows + KPI_String;
                                            detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioVar1);
                                            detalleRows = detalleRows + "</td>";


                                            PrecioVar2 = PrecioActual - PrecioPPTO;
                                            if (PrecioVar2 < 0)
                                            {
                                                KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                                KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                            }
                                            else
                                            {
                                                KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                            }

                                            detalleRows = detalleRows + KPI_String;
                                            detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioVar2);
                                            detalleRows = detalleRows + "</td>";

                                            //Efecto Precio
                                            Efecto1 = (((decimal)zona.KgLt_AnoMesAnterior) * PrecioVar1) / 1000;
                                            if (Efecto1 < 0)
                                            {
                                                KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                                KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                            }
                                            else
                                            {
                                                KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                            }

                                            detalleRows = detalleRows + KPI_String;
                                            detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto1);
                                            detalleRows = detalleRows + "</td>";

                                            Efecto2 = (((decimal)zona.KgLtPPTO) * PrecioVar2) / 1000;
                                            if (Efecto2 < 0)
                                            {
                                                KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                                KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                            }
                                            else
                                            {
                                                KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                            }

                                            detalleRows = detalleRows + KPI_String;
                                            detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto2);
                                            detalleRows = detalleRows + "</td>";

                                            // Efecto Volumen
                                            Efecto3 = (((decimal)zona.KgLt - (decimal)zona.KgLt_AnoMesAnterior) * PrecioActual) / 1000;
                                            if (Efecto3 < 0)
                                            {
                                                KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                                KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                            }
                                            else
                                            {
                                                KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                            }

                                            detalleRows = detalleRows + KPI_String;
                                            detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto3);
                                            detalleRows = detalleRows + "</td>";

                                            Efecto4 = (((decimal)zona.KgLt - (decimal)zona.KgLtPPTO) * PrecioActual) / 1000;
                                            if (Efecto4 < 0)
                                            {
                                                KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                                KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                            }
                                            else
                                            {
                                                KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                            }

                                            detalleRows = detalleRows + KPI_String;
                                            detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto4);
                                            detalleRows = detalleRows + "</td>";
                                        }
                                    }
                                    if (intZona == 0)
                                    {
                                        detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                        detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                        detalleRows = detalleRows + "</td>";
                                        detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                        detalleRows = detalleRows + string.Format("{0:##,###0}%", 0);
                                        detalleRows = detalleRows + "</td>";
                                        detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                        detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                        detalleRows = detalleRows + "</td>";
                                        detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                        detalleRows = detalleRows + string.Format("{0:##,###0}%", 0);
                                        detalleRows = detalleRows + "</td>";
                                        detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                        detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                        detalleRows = detalleRows + "</td>";
                                        detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                        detalleRows = detalleRows + string.Format("{0:##,###0}%", 0);
                                        detalleRows = detalleRows + "</td>";

                                        /// KgLt
                                        detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                        detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                        detalleRows = detalleRows + "</td>";
                                        detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                        detalleRows = detalleRows + string.Format("{0:##,###0}%", 0);
                                        detalleRows = detalleRows + "</td>";
                                        detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                        detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                        detalleRows = detalleRows + "</td>";
                                        detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                        detalleRows = detalleRows + string.Format("{0:##,###0}%", 0);
                                        detalleRows = detalleRows + "</td>";
                                        detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                        detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                        detalleRows = detalleRows + "</td>";

                                        detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                        detalleRows = detalleRows + string.Format("{0:##,###0}%", 0);
                                        detalleRows = detalleRows + "</td>";
                                        // Precio
                                        detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                        detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                        detalleRows = detalleRows + "</td>";
                                        detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                        detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                        detalleRows = detalleRows + "</td>";
                                        detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                        detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                        detalleRows = detalleRows + "</td>";

                                        detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                        detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                        detalleRows = detalleRows + "</td>";

                                        detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                        detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                        detalleRows = detalleRows + "</td>";
                                        //Efecto Precio
                                        detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                        detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                        detalleRows = detalleRows + "</td>";

                                        detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                        detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                        detalleRows = detalleRows + "</td>";

                                        // Efecto Volumen
                                        detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                        detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                        detalleRows = detalleRows + "</td>";

                                        detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                        detalleRows = detalleRows + string.Format("{0:##,###0}", 0);
                                        detalleRows = detalleRows + "</td>";
                                    }
                                }
                                //


                                // Total de Columnas

                                 GrandTotal_res = 0;
                                 GrandTotalKgLt_res = 0;
                                 calculo = 0;

                                foreach (ReporteVentas_ZonasResult zona in Lista)
                                {
                                    GrandTotal_res = GrandTotal_res + (decimal)zona.SaldoDolares;
                                    GrandTotalKgLt_res = GrandTotalKgLt_res + (decimal)zona.KgLt;
                                }

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                detalleRows = detalleRows + string.Format("{0:##,###0}", resVentas_T);
                                detalleRows = detalleRows + "</td>";

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                if (GrandTotal_res == 0)
                                {
                                    calculo = 0;
                                }
                                else
                                {
                                    calculo = (resVentas_T / GrandTotal_res) * 100;
                                }
                                detalleRows = detalleRows + string.Format("{0:##,###0}%", calculo);
                                detalleRows = detalleRows + "</td>";

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                detalleRows = detalleRows + string.Format("{0:##,###0}", resVentaPPTO_T);
                                detalleRows = detalleRows + "</td>";


                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                if (resVentaPPTO_T == 0)
                                {
                                    calculo = 0;
                                }
                                else
                                {
                                    calculo = (resVentas_T / resVentaPPTO_T) * 100;
                                }
                                detalleRows = detalleRows + string.Format("{0:##,###0}%", calculo);
                                detalleRows = detalleRows + "</td>";

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                detalleRows = detalleRows + string.Format("{0:##,###0}", resVentasAnterior_T);
                                detalleRows = detalleRows + "</td>";


                                calculo = 0;
                                if (resVentasAnterior_T == 0)
                                {
                                    calculo = 0;
                                }
                                else
                                {
                                    calculo = ((resVentas_T / resVentasAnterior_T) - 1) * 100;
                                }

                                if (calculo < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                }
                                detalleRows = detalleRows + KPI_String;

                                detalleRows = detalleRows + string.Format("{0:##,###0}%", calculo);
                                detalleRows = detalleRows + "</td>";



                                /// KgLt
                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                detalleRows = detalleRows + String.Format("{0:##,###0}", resKgLto);
                                detalleRows = detalleRows + "</td>";

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                if (GrandTotalKgLt_res == 0)
                                {
                                    calculo = 0;
                                }
                                else
                                {
                                    calculo = (resKgLto / GrandTotalKgLt_res) * 100;
                                }
                                detalleRows = detalleRows + string.Format("{0:##,###0}%", calculo);
                                detalleRows = detalleRows + "</td>";

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                detalleRows = detalleRows + string.Format("{0:##,###0}", resKgLtoPPTO);
                                detalleRows = detalleRows + "</td>";

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                if (resKgLtoPPTO == 0)
                                {
                                    calculo = 0;
                                }
                                else
                                {
                                    calculo = (resKgLto / resKgLtoPPTO) * 100;
                                }

                                detalleRows = detalleRows + string.Format("{0:##,###0}%", calculo);
                                detalleRows = detalleRows + "</td>";

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                detalleRows = detalleRows + string.Format("{0:##,###0}", resKgLtoAnterior);
                                detalleRows = detalleRows + "</td>";


                                if (resKgLtoAnterior == 0)
                                {
                                    calculo = 0;
                                }
                                else
                                {
                                    calculo = ((resKgLto / resKgLtoAnterior) - 1) * 100;
                                }

                                if (calculo < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                }
                                detalleRows = detalleRows + KPI_String;

                                detalleRows = detalleRows + string.Format("{0:##,###0}%", calculo);
                                detalleRows = detalleRows + "</td>";


                                // Precio

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                if (resKgLto == 0)
                                {
                                    PrecioActual = 0;
                                }
                                else
                                {
                                    PrecioActual = ((resVentas_T / resKgLto)) * 1000;
                                }
                                detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioActual);
                                detalleRows = detalleRows + "</td>";


                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                if (resKgLtoAnterior == 0)
                                {
                                    PrecioAnterior = 0;
                                }
                                else
                                {
                                    PrecioAnterior = ((resVentasAnterior_T / resKgLtoAnterior)) * 1000;
                                }
                                detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioAnterior);
                                detalleRows = detalleRows + "</td>";

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} ' >";
                                if (resKgLtoPPTO == 0)
                                {
                                    PrecioPPTO = 0;
                                }
                                else
                                {
                                    PrecioPPTO = ((resVentaPPTO_T / resKgLtoPPTO)) * 1000;
                                }
                                detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioPPTO);
                                detalleRows = detalleRows + "</td>";


                                PrecioVar1 = PrecioActual - PrecioAnterior;
                                if (PrecioVar1 < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                }
                                detalleRows = detalleRows + KPI_String;
                                detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioVar1);
                                detalleRows = detalleRows + "</td>";


                                PrecioVar2 = PrecioActual - PrecioPPTO;
                                if (PrecioVar2 < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                }

                                detalleRows = detalleRows + KPI_String;
                                detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioVar2);
                                detalleRows = detalleRows + "</td>";

                                ////Efecto Precio
                                Efecto1 = ((resKgLtoAnterior) * PrecioVar1) / 1000;
                                if (Efecto1 < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                }

                                detalleRows = detalleRows + KPI_String;
                                detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto1);
                                detalleRows = detalleRows + "</td>";

                                Efecto2 = ((resKgLtoPPTO) * PrecioVar2) / 1000;
                                if (Efecto2 < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                }

                                detalleRows = detalleRows + KPI_String;
                                detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto2);
                                detalleRows = detalleRows + "</td>";

                                // Efecto Volumen
                                Efecto3 = ((resKgLto - resKgLtoAnterior) * PrecioActual) / 1000;
                                if (Efecto3 < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                }

                                detalleRows = detalleRows + KPI_String;
                                detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto3);
                                detalleRows = detalleRows + "</td>";


                                Efecto4 = ((resKgLto - resKgLtoPPTO) * PrecioActual) / 1000;
                                if (Efecto4 < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} ' >";
                                }

                                detalleRows = detalleRows + KPI_String;
                                detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto4);
                                detalleRows = detalleRows + "</td>";

                                // Fin sub Columna

                                // Fin detalle 
                                detalleRows = detalleRows + "</tr>";


                                totalcaracteres = detalleRows.Count();
                                if (totalcaracteres > 700000)
                                {

                                    //detalleRows = string.Format(detalleRows, padding, fontHeders3, border, fontAlignR, fontAlignR, BackColor5);
                                    listDetalleRows.Add(detalleRows);
                                    //detalleRows = "";
                                }

                            }
                        }
                    }

                    detalleRows = detalleRows + "<tr>";
                    detalleRows = detalleRows + "<td colspan='2'    class = 'width:35%; {0} {1} {2}  {5} fix3' >";
                    detalleRows = detalleRows + "TOTAL " + Tipo1; 
                    detalleRows = detalleRows + "</td>";

                    foreach (var datoAño in listAño)
                    {
                        foreach (var datoMes in listMeses)
                        {
                            if (datoMes.Año == datoAño.Año)
                            {
                                Ventas_T = 0;
                                VentaPPTO_T = 0;
                                VentasAnterior_T = 0;

                                KgLto = 0;
                                KgLtoPPTO = 0;
                                KgLtoAnterior = 0;

                                cumplimiento = 0;

                                int intMes = Convert.ToInt32(datoMes.Mes);
                                int intAñor = Convert.ToInt32(datoMes.Año);

                                foreach (ReporteVentas_ZonasResult zona in Lista)
                                {
                                    if (zona.Mes == intMes && zona.Tipo == Tipo1 && zona.Año == intAñor)
                                    {
                                        Ventas_T = Ventas_T + (decimal)zona.SaldoDolares;
                                        VentaPPTO_T = VentaPPTO_T + (decimal)zona.VentaPPTO_Dolares;
                                        VentasAnterior_T = VentasAnterior_T + (decimal)zona.SaldoAnoMesAnterior_Dolares;
                                        KgLto = KgLto + (decimal)zona.KgLt;
                                        KgLtoPPTO = KgLtoPPTO + (decimal)zona.KgLtPPTO;
                                        KgLtoAnterior = KgLtoAnterior + (decimal)zona.KgLt_AnoMesAnterior;
                                    }

                                }


                                GrandTotal = 0;
                                GrandTotalKgLt = 0;
                                foreach (ReporteVentas_ZonasResult zona in Lista)
                                {
                                    if (zona.Mes == intMes && zona.Año == intAñor)
                                    {
                                        GrandTotal = GrandTotal + (decimal)zona.SaldoDolares;
                                        GrandTotalKgLt = GrandTotalKgLt + (decimal)zona.KgLt;
                                    }

                                }

                                //Ventas
                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5} ' >";
                                detalleRows = detalleRows + string.Format("{0:##,###0}", Ventas_T);
                                detalleRows = detalleRows + "</td>";

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5} ' >";
                                if (GrandTotal == 0)
                                {
                                    cumplimiento = 0;
                                }
                                else
                                {
                                    cumplimiento = (Ventas_T / GrandTotal) * 100;
                                }
                                detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                                detalleRows = detalleRows + "</td>";


                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4}  {5}' >";
                                detalleRows = detalleRows + string.Format("{0:##,###0}", VentaPPTO_T);
                                detalleRows = detalleRows + "</td>";

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5} ' >";
                                if (VentaPPTO_T == 0)
                                {
                                    cumplimiento = 0;
                                }
                                else
                                {
                                    cumplimiento = (Ventas_T / VentaPPTO_T) * 100;
                                }

                                detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                                detalleRows = detalleRows + "</td>";

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                                detalleRows = detalleRows + string.Format("{0:##,###0}", VentasAnterior_T);
                                detalleRows = detalleRows + "</td>";


                                cumplimiento = 0;
                                if (VentasAnterior_T == 0)
                                {
                                    cumplimiento = 0;
                                }
                                else
                                {
                                    cumplimiento = ((Ventas_T / VentasAnterior_T) - 1) * 100;
                                }

                                if (cumplimiento < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                                }
                                detalleRows = detalleRows + KPI_String;

                                detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                                detalleRows = detalleRows + "</td>";

                                /// KgLt
                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                                detalleRows = detalleRows + String.Format("{0:##,###0}", KgLto);
                                detalleRows = detalleRows + "</td>";

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                                if (GrandTotalKgLt == 0)
                                {
                                    cumplimiento = 0;
                                }
                                else
                                {
                                    cumplimiento = (KgLto / GrandTotalKgLt) * 100;
                                }
                                detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                                detalleRows = detalleRows + "</td>";

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                                detalleRows = detalleRows + string.Format("{0:##,###0}", KgLtoPPTO);
                                detalleRows = detalleRows + "</td>";


                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5} ' >";
                                if (KgLtoPPTO == 0)
                                {
                                    cumplimiento = 0;
                                }
                                else
                                {
                                    cumplimiento = (KgLto / KgLtoPPTO) * 100;
                                }

                                detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                                detalleRows = detalleRows + "</td>";

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5} ' >";
                                detalleRows = detalleRows + string.Format("{0:##,###0}", KgLtoAnterior);
                                detalleRows = detalleRows + "</td>";



                                cumplimiento = 0;
                                if (KgLtoAnterior == 0)
                                {
                                    cumplimiento = 0;
                                }
                                else
                                {
                                    cumplimiento = ((KgLto / KgLtoAnterior) - 1) * 100;
                                }


                                if (cumplimiento < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                                }

                                detalleRows = detalleRows + KPI_String;
                                detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                                detalleRows = detalleRows + "</td>";

                                //// Precio
                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                                if (KgLto == 0)
                                {
                                    PrecioActual = 0;
                                }
                                else
                                {
                                    PrecioActual = ((Ventas_T / KgLto)) * 1000;
                                }
                                detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioActual);
                                detalleRows = detalleRows + "</td>";


                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                                if (KgLtoAnterior == 0)
                                {
                                    PrecioAnterior = 0;
                                }
                                else
                                {
                                    PrecioAnterior = ((VentasAnterior_T / KgLtoAnterior)) * 1000;
                                }
                                detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioAnterior);
                                detalleRows = detalleRows + "</td>";

                                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                                if (KgLtoPPTO == 0)
                                {
                                    PrecioPPTO = 0;
                                }
                                else
                                {
                                    PrecioPPTO = ((VentaPPTO_T / KgLtoPPTO)) * 1000;
                                }
                                detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioPPTO);
                                detalleRows = detalleRows + "</td>";



                                PrecioVar1 = PrecioActual - PrecioAnterior;
                                if (PrecioVar1 < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                                }
                                detalleRows = detalleRows + KPI_String;
                                detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioVar1);
                                detalleRows = detalleRows + "</td>";


                                PrecioVar2 = PrecioActual - PrecioPPTO;
                                if (PrecioVar2 < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                                }
                                detalleRows = detalleRows + KPI_String;
                                detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioVar2);
                                detalleRows = detalleRows + "</td>";

                                //Efecto Precio

                                Efecto1 = ((KgLtoAnterior) * PrecioVar1) / 1000;
                                if (Efecto1 < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                                }
                                detalleRows = detalleRows + KPI_String;
                                detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto1);
                                detalleRows = detalleRows + "</td>";


                                Efecto2 = ((KgLtoPPTO) * PrecioVar2) / 1000;
                                if (Efecto2 < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                                }
                                detalleRows = detalleRows + KPI_String;
                                detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto2);
                                detalleRows = detalleRows + "</td>";

                                // Efecto Volumen

                                Efecto3 = ((KgLto - KgLtoAnterior) * PrecioActual) / 1000;
                                if (Efecto3 < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                                }
                                detalleRows = detalleRows + KPI_String;
                                detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto3);
                                detalleRows = detalleRows + "</td>";


                                Efecto4 = ((KgLto - KgLtoPPTO) * PrecioActual) / 1000;
                                if (Efecto4 < 0)
                                {
                                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                                }
                                else
                                {
                                    KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                                }
                                detalleRows = detalleRows + KPI_String;
                                detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto4);
                                detalleRows = detalleRows + "</td>";
                            }

                        }
                    }

 

                    // Total de Columnas

                    GrandTotal_res = 0;
                    GrandTotalKgLt_res = 0;
                    calculo = 0;
                    Ventas_T = 0;
                    VentaPPTO_T = 0;
                    VentasAnterior_T = 0;
                    KgLto = 0;
                    KgLtoPPTO = 0;
                    KgLtoAnterior = 0; 


                    foreach (ReporteVentas_ZonasResult zona in Lista)
                    {
                        if (zona.Tipo == Tipo1)
                        {
                            Ventas_T = Ventas_T + (decimal)zona.SaldoDolares;
                            VentaPPTO_T = VentaPPTO_T + (decimal)zona.VentaPPTO_Dolares;
                            VentasAnterior_T = VentasAnterior_T + (decimal)zona.SaldoAnoMesAnterior_Dolares;
                            KgLto = KgLto + (decimal)zona.KgLt;
                            KgLtoPPTO = KgLtoPPTO + (decimal)zona.KgLtPPTO;
                            KgLtoAnterior = KgLtoAnterior + (decimal)zona.KgLt_AnoMesAnterior;
                        }

                    }

                    foreach (ReporteVentas_ZonasResult zona in Lista)
                    {
                        GrandTotal_res = GrandTotal_res + (decimal)zona.SaldoDolares;
                        GrandTotalKgLt_res = GrandTotalKgLt_res + (decimal)zona.KgLt;
                    }


                    //Ventas
                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5} ' >";
                    detalleRows = detalleRows + string.Format("{0:##,###0}", Ventas_T);
                    detalleRows = detalleRows + "</td>";

                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5} ' >";
                    if (GrandTotal_res == 0)
                    {
                        calculo = 0;
                    }
                    else
                    {
                        calculo = (Ventas_T / GrandTotal_res) * 100;
                    }
                    detalleRows = detalleRows + string.Format("{0:##,###0}%", calculo);
                    detalleRows = detalleRows + "</td>";


                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4}  {5}' >";
                    detalleRows = detalleRows + string.Format("{0:##,###0}", VentaPPTO_T);
                    detalleRows = detalleRows + "</td>";

                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5} ' >";
                    if (VentaPPTO_T == 0)
                    {
                        calculo = 0;
                    }
                    else
                    {
                        calculo = (Ventas_T / VentaPPTO_T) * 100;
                    }

                    detalleRows = detalleRows + string.Format("{0:##,###0}%", calculo);
                    detalleRows = detalleRows + "</td>";

                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                    detalleRows = detalleRows + string.Format("{0:##,###0}", VentasAnterior_T);
                    detalleRows = detalleRows + "</td>";

                    
                    calculo = 0;
                    if (VentasAnterior_T == 0)
                    {
                        calculo = 0;
                    }
                    else
                    {
                        calculo = ((Ventas_T / VentasAnterior_T) - 1) * 100;
                    }

                    if (calculo < 0)
                    {
                        KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                        KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                    }
                    else
                    {
                        KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                    }

                    detalleRows = detalleRows + KPI_String; 
                    detalleRows = detalleRows + string.Format("{0:##,###0}%", calculo);
                    detalleRows = detalleRows + "</td>";

                    /// KgLt
                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                    detalleRows = detalleRows + String.Format("{0:##,###0}", KgLto);
                    detalleRows = detalleRows + "</td>";

                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                    if (GrandTotalKgLt_res == 0)
                    {
                        calculo = 0;
                    }
                    else
                    {
                        calculo = (KgLto / GrandTotalKgLt_res) * 100;
                    }
                    detalleRows = detalleRows + string.Format("{0:##,###0}%", calculo);
                    detalleRows = detalleRows + "</td>";

                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                    detalleRows = detalleRows + string.Format("{0:##,###0}", KgLtoPPTO);
                    detalleRows = detalleRows + "</td>";


                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5} ' >";
                    if (KgLtoPPTO == 0)
                    {
                        calculo = 0;
                    }
                    else
                    {
                        calculo = (KgLto / KgLtoPPTO) * 100;
                    }

                    detalleRows = detalleRows + string.Format("{0:##,###0}%", calculo);
                    detalleRows = detalleRows + "</td>";

                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5} ' >";
                    detalleRows = detalleRows + string.Format("{0:##,###0}", KgLtoAnterior);
                    detalleRows = detalleRows + "</td>";


                   

                    calculo = 0;
                    if (KgLtoAnterior == 0)
                    {
                        calculo = 0;
                    }
                    else
                    {
                        calculo = ((KgLto / KgLtoAnterior) - 1) * 100;
                    }
                    if (calculo < 0)
                    {
                        KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                        KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                    }
                    else
                    {
                        KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                    }
                    detalleRows = detalleRows + KPI_String; 
                    detalleRows = detalleRows + string.Format("{0:##,###0}%", calculo);
                    detalleRows = detalleRows + "</td>";

                    //// Precio
                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                    if (KgLto == 0)
                    {
                        PrecioActual = 0;
                    }
                    else
                    {
                        PrecioActual = ((Ventas_T / KgLto)) * 1000;
                    }
                    detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioActual);
                    detalleRows = detalleRows + "</td>";


                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                    if (KgLtoAnterior == 0)
                    {
                        PrecioAnterior = 0;
                    }
                    else
                    {
                        PrecioAnterior = ((VentasAnterior_T / KgLtoAnterior)) * 1000;
                    }
                    detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioAnterior);
                    detalleRows = detalleRows + "</td>";

                    detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                    if (KgLtoPPTO == 0)
                    {
                        PrecioPPTO = 0;
                    }
                    else
                    {
                        PrecioPPTO = ((VentaPPTO_T / KgLtoPPTO)) * 1000;
                    }
                    detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioPPTO);
                    detalleRows = detalleRows + "</td>";


                    /// 
                    
                    PrecioVar1 = PrecioActual - PrecioAnterior;

                    if (PrecioVar1 < 0)
                    {
                        KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                        KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                    }
                    else
                    {
                        KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                    }
                    detalleRows = detalleRows + KPI_String; 
                    detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioVar1);
                    detalleRows = detalleRows + "</td>";

                    
                    PrecioVar2 = PrecioActual - PrecioPPTO;
                    if (PrecioVar2 < 0)
                    {
                        KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                        KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                    }
                    else
                    {
                        KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                    }
                    detalleRows = detalleRows + KPI_String;
                    detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioVar2);
                    detalleRows = detalleRows + "</td>";

                    //Efecto Precio
                    
                    Efecto1 = ((KgLtoAnterior) * PrecioVar1) / 1000;

                    if (Efecto1 < 0)
                    {
                        KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                        KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                    }
                    else
                    {
                        KPI_String = "<td   class = '  {0} {1} {2} {4} {5} ' >";
                    }
                    detalleRows = detalleRows + KPI_String; 
                    detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto1);
                    detalleRows = detalleRows + "</td>";

                    
                    Efecto2 = ((KgLtoPPTO) * PrecioVar2) / 1000;
                    if (Efecto2 < 0)
                    {
                        KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                        KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                    }
                    else
                    {
                        KPI_String = "<td   class = '  {0} {1} {2} {4}  {5}' >";
                    }
                    detalleRows = detalleRows + KPI_String; 
                    detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto2);
                    detalleRows = detalleRows + "</td>";

                    // Efecto Volumen
                   
                    Efecto3 = ((KgLto - KgLtoAnterior) * PrecioActual) / 1000;
                    if (Efecto3 < 0)
                    {
                        KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                        KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                    }
                    else
                    {
                        KPI_String = "<td   class = '  {0} {1} {2} {4}  {5}' >";
                    }
                    detalleRows = detalleRows + KPI_String; 
                    detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto3);
                    detalleRows = detalleRows + "</td>";

                   
                    Efecto4 = ((KgLto - KgLtoPPTO) * PrecioActual) / 1000;
                    if (Efecto4 < 0)
                    {
                        KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                        KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                    }
                    else
                    {
                        KPI_String = "<td   class = '  {0} {1} {2} {4}  {5}' >";
                    }
                    detalleRows = detalleRows + KPI_String; 
                    detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto4);
                    detalleRows = detalleRows + "</td>";

                    // Fin sub Columna

                    detalleRows = detalleRows + "</tr>";

                    int spacio = 0;
                    spacio = count * 17; 
                    detalleRows = string.Format(detalleRows, padding, fontHeders3, border, spacio, fontAlignR, BackColor4);
                    //table.Rows.Add(Tipo1, count);
                 }

                detalleRows = detalleRows + "<tr>";
                detalleRows = detalleRows + "<td colspan='2'    class = 'width:35%; {0} {1} {2} {5} fix3' >";
                detalleRows = detalleRows + "GRAND TOTAL ";
                detalleRows = detalleRows + "</td>";


                foreach(var datoAño in listAño)
                {
                    foreach (var datoMes in listMeses)
                    {
                        if (datoMes.Año == datoAño.Año)
                        {
                            Ventas_T = 0;
                            VentaPPTO_T = 0;
                            VentasAnterior_T = 0;

                            KgLto = 0;
                            KgLtoPPTO = 0;
                            KgLtoAnterior = 0;
                            cumplimiento = 0;

                            int intMes = Convert.ToInt32(datoMes.Mes);
                            int intAñor = Convert.ToInt32(datoMes.Año);

                            foreach (ReporteVentas_ZonasResult zona in Lista)
                            {
                                if (zona.Mes == intMes && zona.Año == intAñor)
                                {
                                    Ventas_T = Ventas_T + (decimal)zona.SaldoDolares;
                                    VentaPPTO_T = VentaPPTO_T + (decimal)zona.VentaPPTO_Dolares;
                                    VentasAnterior_T = VentasAnterior_T + (decimal)zona.SaldoAnoMesAnterior_Dolares;
                                    KgLto = KgLto + (decimal)zona.KgLt;
                                    KgLtoPPTO = KgLtoPPTO + (decimal)zona.KgLtPPTO;
                                    KgLtoAnterior = KgLtoAnterior + (decimal)zona.KgLt_AnoMesAnterior;
                                }

                            }
                            GrandTotal = 0;
                            GrandTotalKgLt = 0;
                            foreach (ReporteVentas_ZonasResult zona in Lista)
                            {
                                if (zona.Mes == intMes && zona.Año == intAñor)
                                {
                                    GrandTotal = GrandTotal + (decimal)zona.SaldoDolares;
                                    GrandTotalKgLt = GrandTotalKgLt + (decimal)zona.KgLt;
                                }

                            }

                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                            detalleRows = detalleRows + string.Format("{0:##,###0}", Ventas_T);
                            detalleRows = detalleRows + "</td>";

                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5} ' >";
                            if (GrandTotal == 0)
                            {
                                cumplimiento = 0;
                            }
                            else
                            {
                                cumplimiento = (Ventas_T / GrandTotal) * 100;
                            }
                            detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                            detalleRows = detalleRows + "</td>";


                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5} ' >";
                            detalleRows = detalleRows + string.Format("{0:##,###0}", VentaPPTO_T);
                            detalleRows = detalleRows + "</td>";

                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4}  {5}' >";
                            if (VentaPPTO_T == 0)
                            {
                                cumplimiento = 0;
                            }
                            else
                            {
                                cumplimiento = (Ventas_T / VentaPPTO_T) * 100;
                            }

                            detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                            detalleRows = detalleRows + "</td>";

                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                            detalleRows = detalleRows + string.Format("{0:##,###0}", VentasAnterior_T);
                            detalleRows = detalleRows + "</td>";


                            cumplimiento = 0;
                            if (VentasAnterior_T == 0)
                            {
                                cumplimiento = 0;
                            }
                            else
                            {
                                cumplimiento = ((Ventas_T / VentasAnterior_T) - 1) * 100;
                            }

                            if (cumplimiento < 0)
                            {
                                KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                            }
                            else
                            {
                                KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                            }
                            detalleRows = detalleRows + KPI_String;
                            detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                            detalleRows = detalleRows + "</td>";

                            /// KgLt
                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5} ' >";
                            detalleRows = detalleRows + String.Format("{0:##,###0}", KgLto);
                            detalleRows = detalleRows + "</td>";

                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4}  {5}' >";
                            if (GrandTotalKgLt == 0)
                            {
                                cumplimiento = 0;
                            }
                            else
                            {
                                cumplimiento = (KgLto / GrandTotalKgLt) * 100;
                            }
                            detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                            detalleRows = detalleRows + "</td>";

                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4}  {5}' >";
                            detalleRows = detalleRows + string.Format("{0:##,###0}", KgLtoPPTO);
                            detalleRows = detalleRows + "</td>";


                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                            if (KgLtoPPTO == 0)
                            {
                                cumplimiento = 0;
                            }
                            else
                            {
                                cumplimiento = (KgLto / KgLtoPPTO) * 100;
                            }

                            detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                            detalleRows = detalleRows + "</td>";

                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                            detalleRows = detalleRows + string.Format("{0:##,###0}", KgLtoAnterior);
                            detalleRows = detalleRows + "</td>";




                            cumplimiento = 0;
                            if (KgLtoAnterior == 0)
                            {
                                cumplimiento = 0;
                            }
                            else
                            {
                                cumplimiento = ((KgLto / KgLtoAnterior) - 1) * 100;
                            }
                            if (cumplimiento < 0)
                            {
                                KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                            }
                            else
                            {
                                KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                            }
                            detalleRows = detalleRows + KPI_String;
                            detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                            detalleRows = detalleRows + "</td>";


                            //// Precio
                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4}  {5}' >";
                            if (KgLto == 0)
                            {
                                PrecioActual = 0;
                            }
                            else
                            {
                                PrecioActual = ((Ventas_T / KgLto)) * 1000;
                            }
                            detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioActual);
                            detalleRows = detalleRows + "</td>";


                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                            if (KgLtoAnterior == 0)
                            {
                                PrecioAnterior = 0;
                            }
                            else
                            {
                                PrecioAnterior = ((VentasAnterior_T / KgLtoAnterior)) * 1000;
                            }
                            detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioAnterior);
                            detalleRows = detalleRows + "</td>";

                            detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                            if (KgLtoPPTO == 0)
                            {
                                PrecioPPTO = 0;
                            }
                            else
                            {
                                PrecioPPTO = ((VentaPPTO_T / KgLtoPPTO)) * 1000;
                            }
                            detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioPPTO);
                            detalleRows = detalleRows + "</td>";




                            PrecioVar1 = PrecioActual - PrecioAnterior;
                            if (PrecioVar1 < 0)
                            {
                                KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                            }
                            else
                            {
                                KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                            }
                            detalleRows = detalleRows + KPI_String;
                            detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioVar1);
                            detalleRows = detalleRows + "</td>";


                            PrecioVar2 = PrecioActual - PrecioPPTO;
                            if (PrecioVar2 < 0)
                            {
                                KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                            }
                            else
                            {
                                KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                            }
                            detalleRows = detalleRows + KPI_String;
                            detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioVar2);
                            detalleRows = detalleRows + "</td>";

                            //Efecto Precio

                            Efecto1 = ((KgLtoAnterior) * PrecioVar1) / 1000;
                            if (Efecto1 < 0)
                            {
                                KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                            }
                            else
                            {
                                KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                            }
                            detalleRows = detalleRows + KPI_String;
                            detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto1);
                            detalleRows = detalleRows + "</td>";


                            Efecto2 = ((KgLtoPPTO) * PrecioVar2) / 1000;
                            if (Efecto2 < 0)
                            {
                                KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                            }
                            else
                            {
                                KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                            }
                            detalleRows = detalleRows + KPI_String;
                            detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto2);
                            detalleRows = detalleRows + "</td>";

                            // Efecto Volumen

                            Efecto3 = ((KgLto - KgLtoAnterior) * PrecioActual) / 1000;
                            if (Efecto3 < 0)
                            {
                                KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                            }
                            else
                            {
                                KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                            }
                            detalleRows = detalleRows + KPI_String;
                            detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto3);
                            detalleRows = detalleRows + "</td>";


                            Efecto4 = ((KgLto - KgLtoPPTO) * PrecioActual) / 1000;
                            if (Efecto4 < 0)
                            {
                                KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                                KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                            }
                            else
                            {
                                KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                            }
                            detalleRows = detalleRows + KPI_String;
                            detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto4);
                            detalleRows = detalleRows + "</td>";

                        }
                    }
                }


 


 

                // Sumatoria total

                Ventas_T = 0;
                VentaPPTO_T = 0;
                VentasAnterior_T = 0;

                KgLto = 0;
                KgLtoPPTO = 0;
                KgLtoAnterior = 0;
                cumplimiento = 0;

       
                foreach (ReporteVentas_ZonasResult zona in Lista)
                {
 
                        Ventas_T = Ventas_T + (decimal)zona.SaldoDolares;
                        VentaPPTO_T = VentaPPTO_T + (decimal)zona.VentaPPTO_Dolares;
                        VentasAnterior_T = VentasAnterior_T + (decimal)zona.SaldoAnoMesAnterior_Dolares;
                        KgLto = KgLto + (decimal)zona.KgLt;
                        KgLtoPPTO = KgLtoPPTO + (decimal)zona.KgLtPPTO;
                        KgLtoAnterior = KgLtoAnterior + (decimal)zona.KgLt_AnoMesAnterior;
 
                }
                 GrandTotal = 0;
                 GrandTotalKgLt = 0;
                foreach (ReporteVentas_ZonasResult zona in Lista)
                {
                        GrandTotal = GrandTotal + (decimal)zona.SaldoDolares;
                        GrandTotalKgLt = GrandTotalKgLt + (decimal)zona.KgLt;
                }

                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                detalleRows = detalleRows + string.Format("{0:##,###0}", Ventas_T);
                detalleRows = detalleRows + "</td>";

                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5} ' >";
                if (GrandTotal == 0)
                {
                    cumplimiento = 0;
                }
                else
                {
                    cumplimiento = (Ventas_T / GrandTotal) * 100;
                }
                detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                detalleRows = detalleRows + "</td>";


                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5} ' >";
                detalleRows = detalleRows + string.Format("{0:##,###0}", VentaPPTO_T);
                detalleRows = detalleRows + "</td>";

                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4}  {5}' >";
                if (VentaPPTO_T == 0)
                {
                    cumplimiento = 0;
                }
                else
                {
                    cumplimiento = (Ventas_T / VentaPPTO_T) * 100;
                }

                detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                detalleRows = detalleRows + "</td>";

                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                detalleRows = detalleRows + string.Format("{0:##,###0}", VentasAnterior_T);
                detalleRows = detalleRows + "</td>";

               
                cumplimiento = 0;
                if (VentasAnterior_T == 0)
                {
                    cumplimiento = 0;
                }
                else
                {
                    cumplimiento = ((Ventas_T / VentasAnterior_T) - 1) * 100;
                }
                if (cumplimiento < 0)
                {
                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                }
                else
                {
                    KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";  
                }
                detalleRows = detalleRows + KPI_String; 
                detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                detalleRows = detalleRows + "</td>";

                /// KgLt
                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5} ' >";
                detalleRows = detalleRows + String.Format("{0:##,###0}", KgLto);
                detalleRows = detalleRows + "</td>";

                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4}  {5}' >";
                if (GrandTotalKgLt == 0)
                {
                    cumplimiento = 0;
                }
                else
                {
                    cumplimiento = (KgLto / GrandTotalKgLt) * 100;
                }
                detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                detalleRows = detalleRows + "</td>";

                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4}  {5}' >";
                detalleRows = detalleRows + string.Format("{0:##,###0}", KgLtoPPTO);
                detalleRows = detalleRows + "</td>";


                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                if (KgLtoPPTO == 0)
                {
                    cumplimiento = 0;
                }
                else
                {
                    cumplimiento = (KgLto / KgLtoPPTO) * 100;
                }

                detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                detalleRows = detalleRows + "</td>";

                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                detalleRows = detalleRows + string.Format("{0:##,###0}", KgLtoAnterior);
                detalleRows = detalleRows + "</td>";


               

                cumplimiento = 0;
                if (KgLtoAnterior == 0)
                {
                    cumplimiento = 0;
                }
                else
                {
                    cumplimiento = ((KgLto / KgLtoAnterior) - 1) * 100;
                }
                if (cumplimiento < 0)
                {
                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                }
                else
                {
                    KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                }
                detalleRows = detalleRows + KPI_String; 
                detalleRows = detalleRows + string.Format("{0:##,###0}%", cumplimiento);
                detalleRows = detalleRows + "</td>";


                //// Precio
                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4}  {5}' >";
                if (KgLto == 0)
                {
                    PrecioActual = 0;
                }
                else
                {
                    PrecioActual = ((Ventas_T / KgLto)) * 1000;
                }
                detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioActual);
                detalleRows = detalleRows + "</td>";


                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                if (KgLtoAnterior == 0)
                {
                    PrecioAnterior = 0;
                }
                else
                {
                    PrecioAnterior = ((VentasAnterior_T / KgLtoAnterior)) * 1000;
                }
                detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioAnterior);
                detalleRows = detalleRows + "</td>";

                detalleRows = detalleRows + "<td   class = '  {0} {1} {2} {4} {5}' >";
                if (KgLtoPPTO == 0)
                {
                    PrecioPPTO = 0;
                }
                else
                {
                    PrecioPPTO = ((VentaPPTO_T / KgLtoPPTO)) * 1000;
                }
                detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioPPTO);
                detalleRows = detalleRows + "</td>";


                
                PrecioVar1 = PrecioActual - PrecioAnterior;
                if (PrecioVar1 < 0)
                {
                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                }
                else
                {
                    KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                }
                detalleRows = detalleRows + KPI_String; 
                detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioVar1);
                detalleRows = detalleRows + "</td>";

                
                PrecioVar2 = PrecioActual - PrecioPPTO;
                if (PrecioVar2 < 0)
                {
                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                }
                else
                {
                    KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                }
                detalleRows = detalleRows + KPI_String; 
                detalleRows = detalleRows + string.Format("{0:##,###0.#}", PrecioVar2);
                detalleRows = detalleRows + "</td>";

                //Efecto Precio
                
                Efecto1 = ((KgLtoAnterior) * PrecioVar1) / 1000;
                if (Efecto1 < 0)
                {
                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                }
                else
                {
                    KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                }
                detalleRows = detalleRows + KPI_String; 
                detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto1);
                detalleRows = detalleRows + "</td>";

               
                Efecto2 = ((KgLtoPPTO) * PrecioVar2) / 1000;
                if (Efecto2 < 0)
                {
                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                }
                else
                {
                    KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                }
                detalleRows = detalleRows + KPI_String; 
                detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto2);
                detalleRows = detalleRows + "</td>";

                // Efecto Volumen
                
                Efecto3 = ((KgLto - KgLtoAnterior) * PrecioActual) / 1000;
                if (Efecto3 < 0)
                {
                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                }
                else
                {
                    KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                }
                detalleRows = detalleRows + KPI_String; 
                detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto3);
                detalleRows = detalleRows + "</td>";

                
                Efecto4 = ((KgLto - KgLtoPPTO) * PrecioActual) / 1000;
                if (Efecto4 < 0)
                {
                    KPI_String = "<td class = '  {0} {1} {2} {3} {4} {5}' >";
                    KPI_String = string.Format(KPI_String, padding, border, fontAlignR, BackColorRojo, fontBlanco, fontHeders3);
                }
                else
                {
                    KPI_String = "<td   class = '  {0} {1} {2} {4} {5}' >";
                }
                detalleRows = detalleRows + KPI_String; 
                detalleRows = detalleRows + string.Format("{0:##,###0}", Efecto4);
                detalleRows = detalleRows + "</td>";


                // Fin sumatoria total
                detalleRows = detalleRows + "</tr>";
                detalleRows = string.Format(detalleRows, padding, fontHeders3, border, fontAlignR, fontAlignR, BackColor5);


                totalcaracteres = detalleRows.Count();
                listDetalleRows.Add(detalleRows); 
                //detalleRows = ""; 

 
                ////---------------------------------------------------------------

                //ViewState["ListaReporteZonas"] = JsonHelper.JsonSerializer(Lista);


                string Cabecera = "";
                string CabeceraMiles = "";
                string CabeceraKlLt = "";
                string CabeceraDetalle = "";

                string Detalle = "";
                string formatDetalle = ""; 
                string mes;
                //int intAño = Convert.ToInt32(listAño[0].Año);

             

                foreach (var datoAño in listAño )
                {
                    Detalle = Cabcera_Detalle((int)datoAño.Año);
                    formatDetalle = string.Format(Detalle, padding, fontHeders3, border);

                    foreach (var datoMes in listMeses)
                    {
                        if (datoMes.Año == datoAño.Año)
                        {
                            int intMes = Convert.ToInt32(datoMes.Mes);
                            int intAñor = Convert.ToInt32(datoMes.Año);

                            Cabecera = Cabecera + "<td colspan='21'  class='  {0} {1} {2} {3}' >";
                            mes = intAñor.ToString() + "-" + intMes.ToString();
                            Cabecera = Cabecera + mes;
                            Cabecera = Cabecera + "</td>";


                            CabeceraMiles = CabeceraMiles + "<td colspan='6' class='  {0} {1} {2} {3}' >";
                            CabeceraMiles = CabeceraMiles + "Miles de Dólares";
                            CabeceraMiles = CabeceraMiles + "</td>";
                            CabeceraMiles = string.Format(CabeceraMiles, padding, fontHeders2, border, BackColor1);

                            CabeceraMiles = CabeceraMiles + "<td colspan='6' class='  {0} {1} {2} {3}' >";
                            CabeceraMiles = CabeceraMiles + "Kilolitros";
                            CabeceraMiles = CabeceraMiles + "</td>";
                            CabeceraMiles = string.Format(CabeceraMiles, padding, fontHeders2, border, BackColor2);

                            CabeceraMiles = CabeceraMiles + "<td colspan='5' class='  {0} {1} {2} {3}' >";
                            CabeceraMiles = CabeceraMiles + "Precio Unitario KL";
                            CabeceraMiles = CabeceraMiles + "</td>";
                            CabeceraMiles = string.Format(CabeceraMiles, padding, fontHeders2, border, BackColor3);

                            CabeceraMiles = CabeceraMiles + "<td colspan='2' class='  {0} {1} {2} {3}' >";
                            CabeceraMiles = CabeceraMiles + "EfectoPrecio";
                            CabeceraMiles = CabeceraMiles + "</td>";
                            CabeceraMiles = string.Format(CabeceraMiles, padding, fontHeders2, border, BackColor3);

                            CabeceraMiles = CabeceraMiles + "<td colspan='2' class='  {0} {1} {2} {3}' >";
                            CabeceraMiles = CabeceraMiles + "EfectoVolumen";
                            CabeceraMiles = CabeceraMiles + "</td>";
                            CabeceraMiles = string.Format(CabeceraMiles, padding, fontHeders2, border, BackColor3);


                            CabeceraDetalle = CabeceraDetalle + formatDetalle;
                        }
                    }
                }

 

                Cabecera = Cabecera + "<td colspan='21'  class='{0} {1} {2} ' >";
                Cabecera = Cabecera + "TOTAL"; 
                Cabecera = Cabecera + "</td>";

                CabeceraMiles = CabeceraMiles + "<td colspan='6' class='{0} {1} {2} {3}' >";
                CabeceraMiles = CabeceraMiles + "Miles de Dólares";
                CabeceraMiles = CabeceraMiles + "</td>";
                CabeceraMiles = string.Format(CabeceraMiles, padding, fontHeders2, border, BackColor1);

                CabeceraMiles = CabeceraMiles + "<td colspan='6' class='{0} {1} {2} {3}' >";
                CabeceraMiles = CabeceraMiles + "Kilolitros";
                CabeceraMiles = CabeceraMiles + "</td>";
                CabeceraMiles = string.Format(CabeceraMiles, padding, fontHeders2, border, BackColor2);

                CabeceraMiles = CabeceraMiles + "<td colspan='5' class='{0} {1} {2} {3}' >";
                CabeceraMiles = CabeceraMiles + "Precio Unitario KL";
                CabeceraMiles = CabeceraMiles + "</td>";
                CabeceraMiles = string.Format(CabeceraMiles, padding, fontHeders2, border, BackColor3);

                CabeceraMiles = CabeceraMiles + "<td colspan='2' class='{0} {1} {2} {3}' >";
                CabeceraMiles = CabeceraMiles + "EfectoPrecio";
                CabeceraMiles = CabeceraMiles + "</td>";
                CabeceraMiles = string.Format(CabeceraMiles, padding, fontHeders2, border, BackColor3);

                CabeceraMiles = CabeceraMiles + "<td colspan='2' class='{0} {1} {2} {3}' >";
                CabeceraMiles = CabeceraMiles + "EfectoVolumen";
                CabeceraMiles = CabeceraMiles + "</td>";
                CabeceraMiles = string.Format(CabeceraMiles, padding, fontHeders2, border, BackColor3);

                CabeceraDetalle = CabeceraDetalle + formatDetalle;



                string formatCabecera = string.Format(Cabecera, padding, fontHeders, border, BackColor6);
                string formatCabeceraMiles = string.Format(CabeceraMiles, padding, fontHeders2, border);          
                string formatCabeceraDetalle= string.Format(CabeceraDetalle, padding, fontHeders3, border);

                    
                string tablaBody = @"
                  <table style='width:100%; ' BorderWidth='1px'>
			        <tr>
				       <td  style='height:60px;' class='fontHeders borderTOP borderLEFT borderRIGHT borderBOTT fix fontAlignC'>
                             TIPO
				        </td>
				        <td  style='height:60px;'  class='fontHeders borderTOP borderLEFT borderRIGHT fontAlignC fix2'>
					        ZONAS 
				        </td>
                        {3}
			        </tr>
                    <tr> 
                        <td   class=' borderLEFT borderRIGHT fix2'>
					          &nbsp;&nbsp;
				        </td> 

                        {4} 
                    </tr>
                    <tr> 
                       
				        <td   class='borderBOTT EspacioH borderLEFT borderRIGHT  fix2'>
					         &nbsp;&nbsp;
				        </td> 
                        {5} 
                    </tr>

                    {6}
                    </table>
                ";
 

                string formatMensaje = string.Format(tablaBody, padding, fontHeders, border, 
                                                     formatCabecera, formatCabeceraMiles, formatCabeceraDetalle,
                                                     detalleRows
                                                     ).Trim();



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

                lblMensaje.Text = "caracteres:" + totalcaracteres.ToString(); 
            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message + " - "+ totalcaracteres.ToString(); ;
                lblMensaje.CssClass = "mensajeError" ;
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
            Detalle = Detalle + "<td class='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + "Real " + intAño.ToString();
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td class='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + "%Venta";
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td class='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + "PPTO " + intAño.ToString();
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td class='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + "%Cumpli.";
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td class='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + "Real " + (intAño - 1).ToString();
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td class='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + "%Creci.";
            Detalle = Detalle + "</td>";
            Detalle = string.Format(Detalle, padding, fontHeders3, border, fontAlignC, BackColor1);

            // KgLt
            Detalle = Detalle + "<td class='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + "Real " + intAño.ToString();
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td class='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + "%Venta";
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td class='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + "PPTO " + intAño.ToString();
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td class='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + "%Cumpli.";
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td class='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + "Real " + (intAño - 1).ToString();
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td class='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + "%Creci.";
            Detalle = Detalle + "</td>";
            Detalle = string.Format(Detalle, padding, fontHeders3, border, fontAlignC, BackColor2);
            // Precios 
            Detalle = Detalle + "<td class='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + intAño.ToString();
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td class='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + (intAño - 1).ToString();
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td class='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + "PPTO " + intAño.ToString();
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td class='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + "Var. Real";
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td class='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + "Var. Ppto";
            Detalle = Detalle + "</td>";

            // Efecto 
            Detalle = Detalle + "<td class='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + intAño.ToString() +"-" + (intAño-1).ToString();
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td class='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + intAño.ToString() + "- PPTO";
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td class='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + "Vol Real";
            Detalle = Detalle + "</td>";

            Detalle = Detalle + "<td class='{3} {4} {0} {1} {2}'>";
            Detalle = Detalle + "Vol PPTO";
            Detalle = Detalle + "</td>";
            Detalle = string.Format(Detalle, padding, fontHeders3, border, fontAlignC, BackColor3);
            return Detalle; 
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


    }
}