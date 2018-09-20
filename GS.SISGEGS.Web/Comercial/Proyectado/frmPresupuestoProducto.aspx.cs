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
using System.Configuration;

namespace GS.SISGEGS.Web.Comercial.Proyectado
{
    public partial class frmPresupuestoProducto : System.Web.UI.Page
    {
        private void Proyectado_Listar(int anho1, int anho2, int mes1, int mes2, int id_zona, string id_vendedor, bool flagSemilla)
        {
            //CobranzasWCFClient objProyectadoWCF = new CobranzasWCFClient();
            ReporteVentaWCFClient objProyectadoWCF = new ReporteVentaWCFClient();

            List<gsReporte_PresupuestoVentasParetoResult> lstProyectado = new List<gsReporte_PresupuestoVentasParetoResult>();
            List<gsReporte_PresupuestoVentasParetoResult> lstProyectadoNew = new List<gsReporte_PresupuestoVentasParetoResult>();
            List<gsReporteVentaPresupuesto_Producto2017Result> objPresupuesto = new List<gsReporteVentaPresupuesto_Producto2017Result>();

            decimal TotalVenta1;

            try
            {

                lstProyectado = objProyectadoWCF.gsReporte_PresupuestoVentasPareto(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, anho1, anho2, mes1, mes2, id_zona, id_vendedor, flagSemilla).ToList();

                List<gsReporte_PresupuestoVentasParetoResult> lstProyectadov = new List<gsReporte_PresupuestoVentasParetoResult>();
                lstProyectadov = lstProyectado.FindAll(x => x.SKU_Nombre.Contains("CURT"));

                objPresupuesto = ListarPresupuesto(id_vendedor);
                ViewState["lstPresupuesto"] = JsonHelper.JsonSerializer(objPresupuesto);

                var query_Venta = from c in lstProyectado
                                      //where c.EstadoCliente != "AFILIADA"
                                  orderby c.CantidadReal2 descending
                                  select new
                                  {
                                      c.Kardex,
                                      c.CantidadPronostico1,
                                      c.CantidadPronostico2,
                                      c.CantidadReal1,
                                      c.CantidadReal2,
                                      cumplimiento1 = (c.cumplimiento1 * 100),
                                      cumplimiento2 = (c.cumplimiento2 * 100),
                                      c.Mes1,
                                      c.Mes2,
                                      c.Mes3,
                                      c.Mes4,
                                      c.Mes5,
                                      c.Mes6,
                                      c.Mes7,
                                      c.Mes8,
                                      c.Mes9,
                                      c.Mes10,
                                      c.Mes11,
                                      c.Mes12,
                                      c.TotalProyectado,
                                      c.SKU,
                                      c.SKU_Nombre,
                                      c.Precio,
                                      c.PrecioVend,
                                      c.Clase1
                                  };

                if (query_Venta != null)
                {
                    var sumImport = query_Venta.ToList().Select(c => c.CantidadReal2).Sum();
                    TotalVenta1 = Convert.ToDecimal(sumImport);
                }
                else
                {
                    TotalVenta1 = 1;
                }

                if (TotalVenta1 == 0) TotalVenta1 = 1;

                decimal sumTotal = 0;
                decimal acumulado = 0;
                int correlativo = 1;

                foreach (var Objeto in query_Venta)
                {

                    gsReporte_PresupuestoVentasParetoResult proyectado = new gsReporte_PresupuestoVentasParetoResult();
                    proyectado.Correlativo = correlativo;
                    proyectado.CantidadPronostico1 = Objeto.CantidadPronostico1;
                    proyectado.CantidadPronostico2 = Objeto.CantidadPronostico2;
                    proyectado.CantidadReal1 = Objeto.CantidadReal1;
                    proyectado.CantidadReal2 = Objeto.CantidadReal2;
                    proyectado.cumplimiento1 = Objeto.cumplimiento1;
                    proyectado.cumplimiento2 = Objeto.cumplimiento2;
                    proyectado.Kardex = Objeto.Kardex;
                    proyectado.SKU = Objeto.SKU;
                    proyectado.SKU_Nombre = Objeto.SKU_Nombre;
                    proyectado.Precio = Objeto.Precio;
                    proyectado.PrecioVend = Objeto.PrecioVend;
                    proyectado.Clase1 = Objeto.Clase1;
                    proyectado.Mes1 = Objeto.Mes1;
                    proyectado.Mes2 = Objeto.Mes2;
                    proyectado.Mes3 = Objeto.Mes3;
                    proyectado.Mes4 = Objeto.Mes4;
                    proyectado.Mes5 = Objeto.Mes5;
                    proyectado.Mes6 = Objeto.Mes6;
                    proyectado.Mes7 = Objeto.Mes7;
                    proyectado.Mes8 = Objeto.Mes8;
                    proyectado.Mes9 = Objeto.Mes9;
                    proyectado.Mes10 = Objeto.Mes10;
                    proyectado.Mes11 = Objeto.Mes11;
                    proyectado.Mes12 = Objeto.Mes12;
                    proyectado.TotalProyectado = Objeto.TotalProyectado;

                    sumTotal = sumTotal + proyectado.CantidadReal2;
                    acumulado = (sumTotal / TotalVenta1) * 100;

                    if (acumulado < 82)
                    { proyectado._80_20 = 1.ToString(); }
                    else
                    { proyectado._80_20 = 0.ToString(); }

                    if (acumulado < 82)
                    {
                        lstProyectadoNew.Add(proyectado);
                        correlativo = correlativo + 1;
                    }
                }

                sumTotal = 0;
                acumulado = 0;

                foreach (var Objeto in query_Venta)
                {

                    gsReporte_PresupuestoVentasParetoResult proyectado = new gsReporte_PresupuestoVentasParetoResult();
                    proyectado.Correlativo = correlativo;
                    proyectado.CantidadPronostico1 = Objeto.CantidadPronostico1;
                    proyectado.CantidadPronostico2 = Objeto.CantidadPronostico2;
                    proyectado.CantidadReal1 = Objeto.CantidadReal1;
                    proyectado.CantidadReal2 = Objeto.CantidadReal2;
                    proyectado.cumplimiento1 = Objeto.cumplimiento1;
                    proyectado.cumplimiento2 = Objeto.cumplimiento2;
                    proyectado.Kardex = Objeto.Kardex;
                    proyectado.SKU = Objeto.SKU;
                    proyectado.SKU_Nombre = Objeto.SKU_Nombre;
                    proyectado.Precio = Objeto.Precio;
                    proyectado.PrecioVend = Objeto.PrecioVend;
                    proyectado.Clase1 = Objeto.Clase1;
                    proyectado.Mes1 = Objeto.Mes1;
                    proyectado.Mes2 = Objeto.Mes2;
                    proyectado.Mes3 = Objeto.Mes3;
                    proyectado.Mes4 = Objeto.Mes4;
                    proyectado.Mes5 = Objeto.Mes5;
                    proyectado.Mes6 = Objeto.Mes6;
                    proyectado.Mes7 = Objeto.Mes7;
                    proyectado.Mes8 = Objeto.Mes8;
                    proyectado.Mes9 = Objeto.Mes9;
                    proyectado.Mes10 = Objeto.Mes10;
                    proyectado.Mes11 = Objeto.Mes11;
                    proyectado.Mes12 = Objeto.Mes12;
                    proyectado.TotalProyectado = Objeto.TotalProyectado;

                    sumTotal = sumTotal + proyectado.CantidadReal2;
                    acumulado = (sumTotal / TotalVenta1) * 100;

                    if (acumulado < 82)
                    { proyectado._80_20 = 1.ToString(); }
                    else
                    { proyectado._80_20 = 0.ToString(); }

                    if (proyectado.Clase1 == "PREMIUM" && acumulado >= 82)
                    {
                        lstProyectadoNew.Add(proyectado);
                        correlativo = correlativo + 1;
                    }

                }


                sumTotal = 0;
                acumulado = 0;
                foreach (var Objeto in query_Venta)
                {

                    gsReporte_PresupuestoVentasParetoResult proyectado = new gsReporte_PresupuestoVentasParetoResult();
                    proyectado.Correlativo = correlativo;
                    proyectado.CantidadPronostico1 = Objeto.CantidadPronostico1;
                    proyectado.CantidadPronostico2 = Objeto.CantidadPronostico2;
                    proyectado.CantidadReal1 = Objeto.CantidadReal1;
                    proyectado.CantidadReal2 = Objeto.CantidadReal2;
                    proyectado.cumplimiento1 = Objeto.cumplimiento1;
                    proyectado.cumplimiento2 = Objeto.cumplimiento2;
                    proyectado.Kardex = Objeto.Kardex;
                    proyectado.SKU = Objeto.SKU;
                    proyectado.SKU_Nombre = Objeto.SKU_Nombre;
                    proyectado.Precio = Objeto.Precio;
                    proyectado.PrecioVend = Objeto.PrecioVend;
                    proyectado.Clase1 = Objeto.Clase1;
                    proyectado.Mes1 = Objeto.Mes1;
                    proyectado.Mes2 = Objeto.Mes2;
                    proyectado.Mes3 = Objeto.Mes3;
                    proyectado.Mes4 = Objeto.Mes4;
                    proyectado.Mes5 = Objeto.Mes5;
                    proyectado.Mes6 = Objeto.Mes6;
                    proyectado.Mes7 = Objeto.Mes7;
                    proyectado.Mes8 = Objeto.Mes8;
                    proyectado.Mes9 = Objeto.Mes9;
                    proyectado.Mes10 = Objeto.Mes10;
                    proyectado.Mes11 = Objeto.Mes11;
                    proyectado.Mes12 = Objeto.Mes12;
                    proyectado.TotalProyectado = Objeto.TotalProyectado;

                    sumTotal = sumTotal + proyectado.CantidadReal2;
                    acumulado = (sumTotal / TotalVenta1) * 100;

                    if (acumulado < 82)
                    { proyectado._80_20 = 1.ToString(); }
                    else
                    { proyectado._80_20 = 0.ToString(); }

                    if (proyectado.Clase1 == "VIP" && acumulado >= 82)
                    {
                        lstProyectadoNew.Add(proyectado);
                        correlativo = correlativo + 1;
                    }
                }


                sumTotal = 0;
                acumulado = 0;
                foreach (var Objeto in query_Venta)
                {

                    gsReporte_PresupuestoVentasParetoResult proyectado = new gsReporte_PresupuestoVentasParetoResult();
                    proyectado.Correlativo = correlativo;
                    proyectado.CantidadPronostico1 = Objeto.CantidadPronostico1;
                    proyectado.CantidadPronostico2 = Objeto.CantidadPronostico2;
                    proyectado.CantidadReal1 = Objeto.CantidadReal1;
                    proyectado.CantidadReal2 = Objeto.CantidadReal2;
                    proyectado.cumplimiento1 = Objeto.cumplimiento1;
                    proyectado.cumplimiento2 = Objeto.cumplimiento2;
                    proyectado.Kardex = Objeto.Kardex;
                    proyectado.SKU = Objeto.SKU;
                    proyectado.SKU_Nombre = Objeto.SKU_Nombre;
                    proyectado.Precio = Objeto.Precio;
                    proyectado.PrecioVend = Objeto.PrecioVend;
                    proyectado.Clase1 = Objeto.Clase1;
                    proyectado.Mes1 = Objeto.Mes1;
                    proyectado.Mes2 = Objeto.Mes2;
                    proyectado.Mes3 = Objeto.Mes3;
                    proyectado.Mes4 = Objeto.Mes4;
                    proyectado.Mes5 = Objeto.Mes5;
                    proyectado.Mes6 = Objeto.Mes6;
                    proyectado.Mes7 = Objeto.Mes7;
                    proyectado.Mes8 = Objeto.Mes8;
                    proyectado.Mes9 = Objeto.Mes9;
                    proyectado.Mes10 = Objeto.Mes10;
                    proyectado.Mes11 = Objeto.Mes11;
                    proyectado.Mes12 = Objeto.Mes12;
                    proyectado.TotalProyectado = Objeto.TotalProyectado;

                    sumTotal = sumTotal + proyectado.CantidadReal2;
                    acumulado = (sumTotal / TotalVenta1) * 100;

                    if (acumulado < 82)
                    { proyectado._80_20 = 1.ToString(); }
                    else
                    { proyectado._80_20 = 0.ToString(); }

                    if (proyectado.Clase1 != "PREMIUM" && proyectado.Clase1 != "VIP" && acumulado >= 82)
                    {
                        lstProyectadoNew.Add(proyectado);
                        correlativo = correlativo + 1;
                    }
                }

                grdDocVenta.DataSource = lstProyectadoNew;
                grdDocVenta.DataBind();

                ViewState["lstProyectado"] = JsonHelper.JsonSerializer(lstProyectadoNew);
                lblGrilla.Value = "1";
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
                        int mes;
                        
                        mes = DateTime.Now.Month;

                        var menos = int.Parse(ConfigurationManager.AppSettings.Get("PresupuestoMenosYear").ToString());


                        rmyReporte.SelectedDate = DateTime.Now.AddMonths(12 - mes).AddYears(-menos);
                        rmyReporte0.SelectedDate = DateTime.Now.AddMonths(-mes + 1).AddYears(-menos);

                        Zona_Cargar(0);
                        btnBuscar.Enabled = false;
                        //btnBajarPlantilla.Enabled = false;
                        btnCargaMasiva.Enabled = false;

                        empresa = ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa;
                        cboVendedor.Enabled = true;

                        Carga_Vendedores(0, null);
                    }
                    else
                    {
                        IFormatProvider culture = new System.Globalization.CultureInfo("es-PE", true);
                        rmyReporte.SelectedDate = DateTime.Parse(Request.QueryString["fechaInicio"], culture, System.Globalization.DateTimeStyles.AssumeLocal);
                        string year;
                        string mes;
                        string id_sectorista, id_vendedor;
                        string stringPeriodo;
                        int periodo, id_zona;
                        string id_zonasec;

                        year = rmyReporte.SelectedDate.Value.Year.ToString();
                        mes = rmyReporte.SelectedDate.Value.Month.ToString();

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

                        //Proyectado_Listar(0, periodo, id_sectorista, null, id_zona, id_vendedor);

                        //BuscarSectorista(id_sectorista); 
                        //lblCodigoSectorista.Value = id_sectorista; 
                        //Zona_Cargar(id_sectorista);
                        //cboZonasVendedor.SelectedValue = id_zonasec;

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

        private void Zona_Cargar(int id_zona)
        {
            try
            {
                AgendaWCFClient objAgendaWCF = new AgendaWCFClient();
                gsZona_ListarResult objZona = new gsZona_ListarResult();
                List<gsZona_ListarResult> lstZona;

                lstZona = objAgendaWCF.Agenda_ListarZona(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, id_zona).ToList();

                lstZona.Insert(0, objZona);
                objZona.Zona = "Todos";
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

        protected void btnCargaMasiva_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreate('');", true);
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
            int periodo, id_zona = 0;
            int year1;
            int mes1, mes2, mesDefoult;
            int year2, year3;
            string id_vendedor;
            string strVendedor = "";
            bool flagSemilla = false;

            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                year2 = rmyReporte.SelectedDate.Value.Year;
                mes2 = rmyReporte.SelectedDate.Value.Month;
                year1 = year2 - 1;
                mes1 = 1;

                id_zona = int.Parse(cboZona.SelectedValue);
                id_vendedor = cboVendedor.SelectedValue;

                mesDefoult = mes2;
                for (int i = 0; i < 12; i++)
                {
                    mesDefoult = mesDefoult + i;
                    if (mesDefoult > 12)
                    {
                        mesDefoult = 1;
                        year3 = year2 + 1;
                    }
                }

                if (chkSemilla.Checked == true) flagSemilla = true;

                Proyectado_Listar(year1, year2, mes1, mes2, id_zona, id_vendedor, flagSemilla);

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        public List<gsReporteVentaPresupuesto_Producto2017Result> ListarPresupuesto(string id_vendedor)
        {
            string stringPeriodo;
            int year1;
            int mes1, mes2, mesDefoult;
            int year2, year3;
            ReporteVentaWCFClient objProyectadoWCF = new ReporteVentaWCFClient();
            List<gsReporteVentaPresupuesto_Producto2017Result> lstPronostico = new List<gsReporteVentaPresupuesto_Producto2017Result>();
            gsReporteVentaPresupuesto_Producto2017Result lstPronosticoMes = new gsReporteVentaPresupuesto_Producto2017Result();
            try
            {
                year2 = rmyReporte.SelectedDate.Value.Year;
                mes2 = rmyReporte.SelectedDate.Value.Month;
                year1 = year2 - 1;
                mes1 = 1;

                mesDefoult = mes1;
                year3 = year2;

                for (int i = 0; i < 12; i++)
                {
                    if (mesDefoult > 12)
                    {
                        mesDefoult = 1;
                        year3 = year2 + 1;
                        stringPeriodo = year3 + "_" + mesDefoult;
                    }
                    else
                    {
                        stringPeriodo = year3 + "_" + mesDefoult;
                    }

                    lstPronosticoMes = objProyectadoWCF.gsReporteVentaPresupuesto_Producto2017(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, year3, mesDefoult, id_vendedor).ToList()[0];
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

        protected void grdDocVenta_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (lblGrilla.Value == "1")
                {
                    grdDocVenta.DataSource = JsonHelper.JsonDeserialize<List<gsReporte_ProyeccionVentasParetoResult>>((string)ViewState["lstProyectado"]);
                    grdDocVenta.DataBind();
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
            string stringPeriodo;
            int year1;
            int mes1, mes2, mesDefoult;
            int year2, year3;

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
                            if (StrClase == "PREMIUM")
                            {
                                dataItem.BackColor = System.Drawing.Color.Yellow;
                                dataItem.Font.Bold = true;
                            }
                            else if (StrClase == "VIP")
                            {
                                dataItem.BackColor = System.Drawing.Color.GreenYellow;
                                dataItem.Font.Bold = true;
                            }

                        }

                    }
                }

                if (e.Item is GridHeaderItem)
                {
                    year2 = rmyReporte.SelectedDate.Value.Year;
                    mes2 = rmyReporte.SelectedDate.Value.Month;
                    //year1 = year2 - 1;
                    mes1 = 1;

                    mesDefoult = mes1;
                    year3 = year2;

                    for (int i = 0; i < 12; i++)
                    {
                        if (mesDefoult > 12)
                        {
                            mesDefoult = 1;
                            year3 = year2 + 1;
                            stringPeriodo = year3 + "_" + mesDefoult;
                        }
                        else
                        {
                            stringPeriodo = year3 + "_" + mesDefoult;
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
            string Id_Vendedor;
            string Vendedor;
            string stringPeriodo;
            int year;
            int mes;

            string ArchivoExcel = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                year = rmyReporte.SelectedDate.Value.Year;
                mes = rmyReporte.SelectedDate.Value.Month;
                string strMes = mes.ToString();
                if (strMes.Length == 1)
                {
                    strMes = "0" + strMes;
                }

                stringPeriodo = year.ToString() + "" + strMes;
                Id_Zona = int.Parse(cboZona.SelectedValue);
                Zona = cboZona.SelectedItem.Text.ToString();
                Id_Vendedor = cboVendedor.SelectedValue.ToString();
                Vendedor = cboVendedor.SelectedItem.Text.Split('-')[1];

                //string alternateText = (sender as ImageButton).AlternateText;
                ArchivoExcel = "Proyectado_" + Id_Zona + "_" + cboVendedor.SelectedValue.ToString() + "_" + stringPeriodo + "_" + DateTime.Now.Millisecond.ToString();

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

                cboVendedor.DataSource = lstVendedores;
                cboVendedor.DataTextField = "DisplayField";
                cboVendedor.DataValueField = "DisplayID";
                cboVendedor.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grdDocVenta_ItemCommand(object sender, GridCommandEventArgs e)
        {
            string id_Sectorista;
            string stringPeriodo;
            string year;
            string mes;

            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                //if (e.CommandName == "Gestion")
                //{
                //    year = rmyReporte.SelectedDate.Value.Year.ToString();
                //    mes = rmyReporte.SelectedDate.Value.Month.ToString();

                //    if (mes.Length == 1)
                //    {mes = "0" + mes;}

                //    GridDataItem dataitem = (GridDataItem)e.Item;
                //    string id_proyectado = dataitem.GetDataKeyValue("id_proyectado").ToString();


                //    stringPeriodo = year + "" + mes;
                //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreateViewGestion(" + e.CommandArgument.ToString() + "," + ViewState["id_Sectorista"] +  "," + id_proyectado + "," + stringPeriodo + ");", true);

                //}

                //if (e.CommandName == "PeriodoDeuda")
                //{
                //    year = rmyReporte.SelectedDate.Value.Year.ToString();
                //    mes = rmyReporte.SelectedDate.Value.Month.ToString();

                //    if (mes.Length == 1)
                //    {
                //        mes = "0" + mes;
                //    }

                //    stringPeriodo = year + "" + mes;

                //    GridDataItem dataitem = (GridDataItem)e.Item;
                //    string id_Cliente = dataitem.GetDataKeyValue("id_Cliente").ToString();

                //    id_Cliente = "1" + id_Cliente;
                //    string id_sectorista = ViewState["id_Sectorista"].ToString(); 

                //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreateViewDeuda(" + id_Cliente + "," + id_sectorista + ");", true);
                //}
                //if (e.CommandName == "EstadoCuenta")
                //{
                //    GridDataItem dataitem = (GridDataItem)e.Item;
                //    string ID_Vendedor = dataitem.GetDataKeyValue("ID_Vendedor").ToString();
                //    string ID_zona = dataitem.GetDataKeyValue("ID_Zona").ToString();
                //    id_Sectorista = ViewState["id_Sectorista"].ToString(); 

                //    Response.Redirect("~/Finanzas/Cobranzas/frmEstadoCuenta.aspx?id_cliente=" + e.CommandArgument.ToString() + "&fechaInicial=" + ((DateTime)ViewState["fechaInicial"]).ToString("dd/MM/yyyy") + "&ID_Vendedor=" + ID_Vendedor + "&ID_Sectorista=" + id_Sectorista + "&ID_zona=" + ID_zona);
                //}

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
                        dato = row.Cells[i].Text.Replace(" ", "");
                        //if (i == 3 || i == 6)
                        //{
                        //    dato = "'" + dato;
                        //}
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
            string stringPeriodo;
            int year1;
            int mes1, mes2, mesDefoult;
            int year2, year3;

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


                int y = 0;
                year2 = rmyReporte.SelectedDate.Value.Year;
                mes2 = rmyReporte.SelectedDate.Value.Month;
                year1 = year2 - 1;
                mes1 = 1;
                mesDefoult = mes2;
                year3 = year2;

                int countColumn = table.Columns.Count - 1;

                foreach (DataColumn col in table.Columns)
                {//write in new col


                    if (y <= 11)
                    {
                        HttpContext.Current.Response.Write("<Td BGCOLOR=" + "#66FF66" + " border='1' >");
                        HttpContext.Current.Response.Write("<B>");
                        HttpContext.Current.Response.Write(col.ColumnName.ToString());
                        HttpContext.Current.Response.Write("</B>");
                        HttpContext.Current.Response.Write("</Td>");
                    }
                    else
                    {
                        if (mesDefoult > 12)
                        {
                            mesDefoult = 1;
                            year3 = year2 + 1;
                            stringPeriodo = year3 + "_" + mesDefoult;
                        }
                        else
                        {
                            stringPeriodo = year3 + "_" + mesDefoult;
                        }

                        if (y < countColumn)
                        {
                            HttpContext.Current.Response.Write("<Td   style=" + "color:#efffef" + "  BGCOLOR=" + "#003f00" + " border='1' >");
                            HttpContext.Current.Response.Write("<B>");
                            HttpContext.Current.Response.Write(stringPeriodo);
                            //HttpContext.Current.Response.Write(stringPeriodo);
                            HttpContext.Current.Response.Write("</B>");
                            HttpContext.Current.Response.Write("</Td>");
                        }

                        mesDefoult = mesDefoult + 1;
                    }
                    y = y + 1;
                }
                HttpContext.Current.Response.Write("</TR>");


                foreach (DataRow row in table.Rows)
                {//write in new row
                    HttpContext.Current.Response.Write("<TR>");

                    string pareto = row[countColumn].ToString();

                    for (int i = 0; i < table.Columns.Count; i++)
                    {

                        if (i < countColumn)
                        {
                            if (pareto == "1")
                            {
                                HttpContext.Current.Response.Write("<Td BGCOLOR=" + "#ffff66" + " border='1'>");
                            }
                            else
                            {
                                HttpContext.Current.Response.Write("<Td border='1'>");
                            }

                            HttpContext.Current.Response.Write(row[i].ToString());
                            HttpContext.Current.Response.Write("</Td>");
                        }
                    }

                    HttpContext.Current.Response.Write("</TR>");
                }

                HttpContext.Current.Response.Write("</Table>");
                HttpContext.Current.Response.Write("</font>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
                //    HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                //    HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                //    HttpContext.Current.ApplicationInstance.CompleteRequest();

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }

        }

        private void ExporttoExcel_V2(GridView GridView1, string Archivo, string id_vendedor, int id_zona)
        {
            string stringPeriodo;
            int year1;
            int mes1, mes2, mesDefoult;
            int year2, year3;
            string Origen;
            string Destino;

            try
            {
                DataTable table = new DataTable();
                table = funConvertGVToDatatable(GridView1);
                string strFecha = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

                //Origen = "C:\\Users\\cesar.coronel\\Desktop\\Plantillas\\Origen\\Plantilla_pronostico.xls";
                Origen = "C:\\inetpub\\www\\IntranetGS\\Comercial\\Proyectado\\Plantilla\\Plantilla_presupuesto.xls";

                //Destino = "C:\\Users\\cesar.coronel\\Desktop\\Plantillas\\Destino\\ReportePronostico_" + id_vendedor.ToString() + "_" + strFecha + ".xls";
                Destino = "C:\\temp\\uploads\\ReportePresupuesto_" + id_vendedor.ToString() + "_" + strFecha + ".xls";

                File.Copy(Origen, Destino, true);

                ESCRIBIR_EXCEL_GENERAL(Destino, "Pronostico", table, "MANTENIMIENTO", "VENTAS");

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

        public void ESCRIBIR_EXCEL_GENERAL(string Directorio, string Nombre_Hoja, System.Data.DataTable dtDatos, string stTipo, string stSubTipo)
        {
            try
            {
                if (stTipo == "MANTENIMIENTO")
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
            int year1;
            int mes1, mes2, mesDefoult;
            int year2, year3;

            string quote = "\"";
            string strConnnectionOle = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directorio + ";Extended Properties=" + quote + "Excel 8.0;HDR=NO" + quote + "";
            //string strConnnectionOle = "Microsoft.ACE.OLEDB.12.0;Data Source=" + Directorio + ";Extended Properties=Excel 8.0;HDR=NO";

            OleDbConnection oleConn = new OleDbConnection(strConnnectionOle);
            try
            {
                oleConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = oleConn;

                int countColumn = Tabla.Columns.Count - 1;
                int y = 0;
                year2 = rmyReporte.SelectedDate.Value.Year;
                mes2 = rmyReporte.SelectedDate.Value.Month;
                year1 = year2 - 1;
                mes1 = 1;
                mesDefoult = 13 ;
                year3 = year2;
                char a;
                int cod;
                string Letra = "P";
                a = Convert.ToChar(Letra);
                cod = (int)a;

                List<gsReporteVentaPresupuesto_ProductoResult> lstPresupuesto = JsonHelper.JsonDeserialize<List<gsReporteVentaPresupuesto_ProductoResult>>((string)ViewState["lstPresupuesto"]);
                decimal TotalVenta;

                foreach (DataColumn col in Tabla.Columns)
                {//write in new col

                    if (y > 14)
                    {
                        if (mesDefoult > 12)
                        {
                            mesDefoult = 1;
                            year3 = year2 + 1;
                            stringPeriodo = year3 + "_" + mesDefoult;
                        }
                        else if (mesDefoult == 12) {
                            stringPeriodo = year3 + "_" + mesDefoult;
                            Letra = "AA";
                        }
                        else
                        {
                            stringPeriodo = year3 + "_" + mesDefoult;
                        }
                        if (y < countColumn)
                        {
                            var query_Venta = from c in lstPresupuesto
                                              where c.Año == year3-1 && c.Mes == mesDefoult
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

                            cmd.CommandText = "UPDATE [" + NombreArchivo + "$" + Letra + "4:" + Letra + "4] SET F1=" + "'" + stringPeriodo + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE [" + NombreArchivo + "$" + "AZ" + "4:" + "AZ" + "4] SET F1=" + "'" + "PARETO" + "'";
                            cmd.ExecuteNonQuery();

                            cod = cod + 1;
                            a = (char)cod;
                            Letra = Convert.ToString(a);

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


                    Texto = "UPDATE [" + NombreArchivo + "$" + "A" + fila.ToString() + ":" + "AA" + fila.ToString() + "]   ";
                    Texto = Texto + " SET F1=" + "'" + row[0].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F2=" + "'" + row[1].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F3=" + "'" + row[2].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F4=" + "'" + row[3].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F5=" + "'" + row[4].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F6=" + "'" + row[5].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F7=" + "'" + row[6].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F8=" + "'" + row[7].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F9=" + "'" + row[8].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F10=" + "'" + row[9].ToString().Replace("&nbsp;", "0") + "', ";
                    Texto = Texto + "  F11=" + "'" + row[10].ToString().Replace("&nbsp;", "0") + "', ";
                    Texto = Texto + "  F12=" + "'" + row[11].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F13=" + "'" + row[12].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F14=" + "'" + row[13].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F15=" + "'" + row[14].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F16=" + "'" + row[15].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F17=" + "'" + row[16].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F18=" + "'" + row[17].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F19=" + "'" + row[18].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F20=" + "'" + row[19].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F21=" + "'" + row[20].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F22=" + "'" + row[21].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F23=" + "'" + row[22].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F24=" + "'" + row[23].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F25=" + "'" + row[24].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F26=" + "'" + row[25].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F27=" + "'" + row[26].ToString().Replace("&nbsp;", "") + "'";
                    //Texto = Texto + "  F26=" + "'" + row[25].ToString() + "'";

                    cmd.CommandText = Texto;
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "UPDATE [" + NombreArchivo + "$" + "AZ" + fila.ToString() + ":" + "AZ" + fila.ToString() + "] SET F1=" + "'" + row[27].ToString() + "'";
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
                int countColumn = Tabla.Columns.Count;
                string Celda;

                foreach (DataRow row in Tabla.Rows)
                {//write in new row
                    string pareto = row[countColumn - 1].ToString();
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

                //objBook.Save(); 
                objXls.Application.Visible = true;
                //objXls.Workbooks.Close();

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }
        }

        protected void cboVendedor_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cboVendedor == null || cboVendedor.SelectedValue == "" || cboVendedor.SelectedValue == "0")
            {
                //cboCliente.Enabled = false;
                btnBuscar.Enabled = false;
                btnCargaMasiva.Enabled = false;
                btnExpDetalle.Enabled = false;
            }
            else
            {
                btnBuscar.Enabled = true;
                btnCargaMasiva.Enabled = true;
                btnExpDetalle.Enabled = true;

                string idVendedor = cboVendedor.SelectedValue;
                //CargarClientes(idVendedor);
            }
        }

        protected void cboZona_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cboZona == null || cboZona.SelectedValue == "" || cboZona.SelectedValue == "0")
            {
                cboVendedor.Enabled = false;
                btnBuscar.Enabled = false;
                btnCargaMasiva.Enabled = false;
                btnExpDetalle.Enabled = false;
            }
            else
            {
                //btnBuscar.Enabled = true;
                //btnCargaMasiva.Enabled = true;

                //cboCliente.Enabled = true;
                int idZona = int.Parse(cboZona.SelectedValue);
                Carga_Vendedores(idZona, null);

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
            string Id_Vendedor;
            string Vendedor;
            string stringPeriodo;
            int year;
            int mes;
            string ArchivoExcel = "";

            decimal TotalVenta1;

            try
            {
                if (lblGrilla.Value == "1")
                {
                    year = rmyReporte.SelectedDate.Value.Year;
                    mes = rmyReporte.SelectedDate.Value.Month;

                    string strMes = mes.ToString();
                    if (strMes.Length == 1)
                    {
                        strMes = "0" + strMes;
                    }

                    stringPeriodo = year.ToString() + "" + strMes;
                    Id_Zona = int.Parse(cboZona.SelectedValue);
                    Zona = cboZona.SelectedItem.Text.ToString();
                    Id_Vendedor = cboVendedor.SelectedValue.ToString();
                    Vendedor = cboVendedor.SelectedItem.Text.Split('-')[1];

                    ArchivoExcel = "Proyectado_" + Id_Zona + "_" + cboVendedor.SelectedValue.ToString() + "_" + stringPeriodo + "_" + DateTime.Now.Millisecond.ToString();

                    List<gsReporte_PresupuestoVentasParetoResult> lstProyectado = JsonHelper.JsonDeserialize<List<gsReporte_PresupuestoVentasParetoResult>>((string)ViewState["lstProyectado"]);//.FindAll(x => x.SKU_Nombre.Contains("curti"));
        

                    var query_Venta2 = from c in lstProyectado
                                       select new
                                       {
                                           Periodo = stringPeriodo,
                                           Id_Zona,
                                           Zona,
                                           Id_Vendedor,
                                           Vendedor,
                                           Categoria = c.Clase1,
                                           c.Kardex,
                                           c.SKU,
                                           c.SKU_Nombre,
                                           c.Precio,
                                           c.PrecioVend,
                                           Q_Vent2015 = c.CantidadReal1,
                                           //Q_Pres2015 = c.CantidadPronostico1,
                                           //Avc2015 = (c.cumplimiento1 * 100),

                                           Q_Vent2016 = c.CantidadReal2,
                                           Q_Pres2016 = c.CantidadPronostico2,
                                           Avc2016 = (c.cumplimiento2 / 100),

                                           c.Mes1,
                                           c.Mes2,
                                           c.Mes3,
                                           c.Mes4,
                                           c.Mes5,
                                           c.Mes6,
                                           c.Mes7,
                                           c.Mes8,
                                           c.Mes9,
                                           c.Mes10,
                                           c.Mes11,
                                           c.Mes12,

                                           c._80_20
                                       };

                    GridView GridView1 = new GridView();

                    GridView1.AllowPaging = false;
                    GridView1.DataSource = query_Venta2;
                    GridView1.DataBind();
                    //ExportGridToExcel_Detalle();

                    // ExporttoExcel(GridView1, ArchivoExcel);   // Vale 

                    ExporttoExcel_V2(GridView1, ArchivoExcel, Id_Vendedor, Id_Zona);

                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }

        }

        protected void btnExpDetalle_Click(object sender, ImageClickEventArgs e)
        {
            ExportarExcel1();
        }

    }
}