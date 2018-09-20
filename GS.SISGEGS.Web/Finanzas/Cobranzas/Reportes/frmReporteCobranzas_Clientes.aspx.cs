using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.DM;
using GS.SISGEGS.Web.CobranzasWCF;
using Telerik.Web.UI;
using System.Globalization;
using GS.SISGEGS.Web.LoginWCF;
using GS.SISGEGS.Web.Helpers;

namespace GS.SISGEGS.Web.Finanzas.Cobranzas.ReporteCobranza
{
    public partial class frmReporteCobranzas_Clientes : System.Web.UI.Page
    {
        private void ReporteVenta_Cliente(int periodo, int year, int mes, int id_zona, string id_sectorista) {

            int Periodo;
            int TotalDias = 0;
            decimal EsperadoDia = 0;
            decimal EsperadoAcumulado = 0;


            DateTime fechaInico;
            DateTime fechaFinal;

            fechaInico = Convert.ToDateTime(year + "-" + mes + "-" + "1");
            fechaFinal = fechaInico.AddMonths(1).AddDays(-1);
            TimeSpan ts = fechaFinal - fechaInico;
            TotalDias = ts.Days + 1;

            CobranzasWCFClient objCobranzasWCF = new CobranzasWCFClient();
            //rhcParticipacion.PlotArea.Series[0].Items.Clear();
            try
            {

                #region Venta al 80% y 20%
                List<gsReporteCobranzas_Poryectadas_Vendedor_DetalleResult> lst = objCobranzasWCF.Reporte_CobranzasProyectadasVendedorDetalle(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, mes, year, periodo, id_zona, id_sectorista).ToList().OrderByDescending(x => x.ImporteCobrado).ToList();

                decimal sum = (decimal)lst.Sum(i => i.ImporteCobrado);

                decimal acumulado = 0;
                bool paso80 = false;

                foreach (var cliente in lst)
                {
                    SeriesItem item = new SeriesItem();
                    if (sum > 0)
                        if (((int)((decimal.Parse(cliente.ImporteCobrado.ToString()) / sum) * 100)) > 0)
                        {
                            item.YValue = ((int)((decimal.Parse(cliente.ImporteCobrado.ToString()) / sum) * 100));
                            item.Name = string.Format("{0}<br/>Valor Cobrado:${1}K<br/>Proyectado:${2}K<br/>Avance:{3}%<br/>Participación:{4}%",
                                cliente.Cliente + " - " + cliente.ImporteCobrado.ToString().Replace("'", string.Empty),
                                Math.Round(decimal.Parse(cliente.ImporteCobrado.ToString()) / 1000, 2),
                                Math.Round((decimal)cliente.ImporteProyectado / 1000, 2),
                                cliente.Avance,
                                item.YValue);
                            //rhcParticipacion.PlotArea.Series[0].Items.Add(item);
                        }

                    if (cliente.ImporteCobrado > 0)
                    {
                        acumulado = acumulado + (decimal)cliente.ImporteCobrado;
                        if (!paso80)
                        {
                            if (cliente.Cliente.Length > 30)
                                rhcCliente80.PlotArea.XAxis.Items.Add(cliente.Cliente.Substring(0, 30).Replace("'", string.Empty));
                            else
                                rhcCliente80.PlotArea.XAxis.Items.Add(cliente.Cliente.Replace("'", string.Empty));

                            item = new SeriesItem();
                            item.Name = cliente.Id_Cliente.Replace("'", string.Empty);
                            item.YValue = Math.Round((decimal)cliente.ImporteCobrado / 1000, 2);
                            this.rhcCliente80.PlotArea.Series[0].Items.Add(item);

                            item = new SeriesItem();
                            item.Name = cliente.Cliente.Replace("'", string.Empty);
                            item.YValue = Math.Round((decimal)cliente.ImporteProyectado / 1000, 2);
                            this.rhcCliente80.PlotArea.Series[1].Items.Add(item);
                        }
                        else
                        {
                            if (cliente.Cliente.Length > 30)
                                rhcCliente20.PlotArea.XAxis.Items.Add(cliente.Cliente.Substring(0, 30).Replace("'", string.Empty));
                            else
                                rhcCliente20.PlotArea.XAxis.Items.Add(cliente.Cliente.Replace("'", string.Empty));

                            item = new SeriesItem();
                            item.Name = cliente.Cliente.Replace("'", string.Empty);
                            item.YValue = Math.Round((decimal)(cliente.ImporteCobrado) / 1000, 2);
                            this.rhcCliente20.PlotArea.Series[0].Items.Add(item);

                            item = new SeriesItem();
                            item.Name = cliente.Cliente.Replace("'", string.Empty);
                            item.YValue = Math.Round((decimal)cliente.ImporteProyectado / 1000, 2);
                            this.rhcCliente20.PlotArea.Series[1].Items.Add(item);
                        }

                        if (acumulado / sum * 100 >= 80)
                            paso80 = true;
                    }
                }
                #endregion

                #region Datos principales
                //Datos principales de la venta
                gsReporteCobranzas_Poryectadas_VendedorResult[] rvP = objCobranzasWCF.Reporte_CobranzasProyectadasVendedor(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                   ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, mes, year, periodo, id_zona, null);
                gsReporteCobranzas_Poryectadas_VendedorResult rv = new gsReporteCobranzas_Poryectadas_VendedorResult();

                if (rvP.Count() > 0)
                {
                    rv = rvP.ToList().Single();

                    rrgAvanceReal.Pointer.Value = decimal.Parse(rv.AvanceCobrado.ToString());
                    lblAvanceReal.Text = "Avance Real: " + Math.Round((decimal)rv.AvanceCobrado, 0).ToString() + "%";

                    rrgAvanceEsperado.Pointer.Value = decimal.Parse(rv.AvanceEsperado.ToString());
                    lblAvanceEsperado.Text = "Avance Esperado: " + Math.Round((decimal)rv.AvanceEsperado, 0).ToString() + "%";



                    //- Cuadro de periodos
                    //lblPronostico.Text = string.Format("${0}", Math.Round((decimal)rv.ImporteProyectado, 0).ToString("#,#", CultureInfo.InvariantCulture));
                    //lblValorVenta.Text = string.Format("${0}", Math.Round((decimal)rv.ImporteCobrado, 0).ToString("#,#", CultureInfo.InvariantCulture));
                    //lblDiferencia.Text = string.Format("${0}", Math.Round((decimal)rv.Diferencia, 0).ToString("#,#", CultureInfo.InvariantCulture));
                    //lblCantTotal.Text = lst.Count.ToString();
                    //lblCantVenta.Text = lst.FindAll(x => x.ImporteCobrado > 0).Count.ToString();
                    //lblCantNoVenta.Text = lst.FindAll(x => x.ImporteCobrado <= 0).Count.ToString();
                }



                gsReporteCancelados_ProyectadoResult[] rvCobradoP = objCobranzasWCF.Reporte_CobranzasProyectadas_Sectorista(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                            ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, mes, year, periodo, id_zona, id_sectorista);

                gsReporteCancelados_ProyectadoResult rvCobrado = new gsReporteCancelados_ProyectadoResult();
                if (rvCobradoP.Count() > 0)
                {
                    rvCobrado = rvCobradoP.ToList().Single();

                    //lblVencido01a30.Text = string.Format("${0}", Math.Round((decimal)rvCobrado.Importe_01a30, 0).ToString("#,#", CultureInfo.InvariantCulture));
                    //lblVencido31a60.Text = string.Format("${0}", Math.Round((decimal)rvCobrado.Importe_31a60, 0).ToString("#,#", CultureInfo.InvariantCulture));
                    //lblVencido61a120.Text = string.Format("${0}", Math.Round((decimal)rvCobrado.Importe_61a120, 0).ToString("#,#", CultureInfo.InvariantCulture));
                    //lblVencido121a360.Text = string.Format("${0}", Math.Round((decimal)rvCobrado.Importe_121a360, 0).ToString("#,#", CultureInfo.InvariantCulture));
                    //lblVencido361aMas.Text = string.Format("${0}", Math.Round((decimal)(rvCobrado.Importe_361a720 + rvCobrado.Importe_721amas), 0).ToString("#,#", CultureInfo.InvariantCulture));
                    //lblNoVencido.Text = string.Format("${0}", Math.Round((decimal)rvCobrado.Importe_NoVencido, 0).ToString("#,#", CultureInfo.InvariantCulture));

                }
                else
                {
                    //lblVencido01a30.Text = string.Format("${0}", Math.Round((decimal)0, 0).ToString("#,#", CultureInfo.InvariantCulture));
                    //lblVencido31a60.Text = string.Format("${0}", Math.Round((decimal)0, 0).ToString("#,#", CultureInfo.InvariantCulture));
                    //lblVencido61a120.Text = string.Format("${0}", Math.Round((decimal)0, 0).ToString("#,#", CultureInfo.InvariantCulture));
                    //lblVencido121a360.Text = string.Format("${0}", Math.Round((decimal)0, 0).ToString("#,#", CultureInfo.InvariantCulture));
                    //lblVencido361aMas.Text = string.Format("${0}", Math.Round((decimal)(0), 0).ToString("#,#", CultureInfo.InvariantCulture));
                    //lblNoVencido.Text = string.Format("${0}", Math.Round((decimal)0, 0).ToString("#,#", CultureInfo.InvariantCulture));

                }


                #endregion

                #region Cobro diario
                List<gsReporteCobranzas_Poryectadas_Vendedor_FechaResult> lstFecha = objCobranzasWCF.Reporte_CobranzasProyectadasVendedor_Fecha(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, mes, year, periodo, id_zona, null).ToList();

                acumulado = 0;


                EsperadoDia = (decimal)rv.ImporteProyectado / TotalDias;
                EsperadoAcumulado = 0;


                for (DateTime day = fechaInico; DateTime.Compare(day, fechaFinal) <= 0; day = day.AddDays(1))
                {
                    AxisItem xitem = new AxisItem();
                    xitem.LabelText = day.ToString("dd/MM/yyyy");
                    rhcDiario.PlotArea.XAxis.Items.Add(xitem);

                    gsReporteCobranzas_Poryectadas_Vendedor_FechaResult objVFecha = lstFecha.Find(x => x.FechaCobranza == day);

                    //Completar linea 1
                    SeriesItem serie1 = new SeriesItem();
                    if (objVFecha != null)
                    {
                        acumulado = acumulado + (decimal)objVFecha.ImporteCobrado;
                        serie1.YValue = Math.Round((decimal)objVFecha.ImporteCobrado / 1000, 2);
                    }
                    else
                    {
                        serie1.YValue = 0;
                    }
                    rhcDiario.PlotArea.Series[0].Items.Add(serie1);

                    //Completar linea 2
                    SeriesItem serie2 = new SeriesItem();
                    if (rv.ImporteProyectado == 0)
                    {
                        serie2.YValue = Math.Round(acumulado / 100 * 100, 0);
                    }
                    else
                    {
                        serie2.YValue = Math.Round(acumulado / (decimal)rv.ImporteProyectado * 100, 0);
                    }
                    rhcDiario.PlotArea.Series[1].Items.Add(serie2);

                    //Completar linea 3
                    SeriesItem serie3 = new SeriesItem();
                    EsperadoAcumulado = EsperadoAcumulado + EsperadoDia;
                    //serie3.YValue = Math.Round((decimal)EsperadoAcumulado / 1000, 2);

                    if (rv.ImporteProyectado == 0)
                    {
                        serie2.YValue = Math.Round((decimal)EsperadoAcumulado / 100 * 100, 0);
                    }
                    else
                    {
                        serie3.YValue = Math.Round((decimal)EsperadoAcumulado / (decimal)rv.ImporteProyectado * 100, 0);
                    }
                    rhcDiario.PlotArea.Series[2].Items.Add(serie3);

                }

                rhcDiario.PlotArea.Series[1].AxisName = "AdditionalAxis";
                rhcDiario.PlotArea.Series[2].AxisName = "AdditionalAxis";
                #endregion

                lblTitulo.Text = "Reporte " + year.ToString() + "-" + mes.ToString() + " de " + rv.Zona_nombre;
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

                    int year = int.Parse(Request.QueryString["anho"].ToString());
                    int mes = int.Parse(Request.QueryString["mes"].ToString());
                    int periodo = year * 100 + mes;
                    int id_zona = int.Parse(Request.QueryString["id_zona"].ToString());
                    //string id_sectorista = Request.QueryString["id_sectorista"].ToString();


                    ReporteVenta_Cliente(periodo, year, mes, id_zona, null);
                    ReporteCobranza_Cuadro1(mes, year, id_zona);
                    ReporteCobranza_Cuadro2(mes, year, id_zona);

                    lblMensaje.Text = "El reporte del vendedor ha sido cargado.";
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try {
                int year = int.Parse(Request.QueryString["anho"].ToString());
                int mes = int.Parse(Request.QueryString["mes"].ToString());
                int id_zona = int.Parse(Request.QueryString["id_zona"].ToString());

                Response.Redirect("~/Finanzas/Cobranzas/Reportes/frmReporteCobranzas.aspx?id_zona=" + id_zona + "&anho=" + year + "&mes=" + mes);

                //Response.Redirect("~/Finanzas/Cobranzas/ReporteCobranza/frmReporteCobranzas.aspx?fechaInicio=" +
                //    Request.QueryString["fechaInicio"] + "&fechaFinal=" + Request.QueryString["fechaFinal"]);
            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeExito";
            }
        }

        protected void ramRepCliente_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Argument.Split(',')[0] == "ChangePageSize")
                {
                    rmpRepCliente.Height = new Unit(e.Argument.Split(',')[1] + "px");
                    decimal altura = 470 + decimal.Parse(e.Argument.Split(',')[1]) - 531;
                    rhcDiario.Height = new Unit(altura.ToString() + "px");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdCuadro1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem dataItem = e.Item as GridDataItem;
                    if (dataItem["Periodo"].Text == "Avance")
                    {
                        e.Item.ForeColor = System.Drawing.Color.GreenYellow;

                        dataItem["ImportePendiente"].Text = dataItem["ImportePendiente"].Text + "%";
                        dataItem["Importe_NoVencidoMenor30"].Text = dataItem["Importe_NoVencidoMenor30"].Text + "%";
                        dataItem["Importe_NoVencido30a0"].Text = dataItem["Importe_NoVencido30a0"].Text + "%";
                        dataItem["Importe_01a15"].Text = dataItem["Importe_01a15"].Text + "%";
                        dataItem["Importe_16a30"].Text = dataItem["Importe_16a30"].Text + "%";
                        dataItem["Importe_31a60"].Text = dataItem["Importe_31a60"].Text + "%";
                        dataItem["Importe_61a90"].Text = dataItem["Importe_61a90"].Text + "%";
                        dataItem["Importe_91a120"].Text = dataItem["Importe_91a120"].Text + "%";
                        dataItem["Importe_121a240"].Text = dataItem["Importe_121a240"].Text + "%";
                        dataItem["Importe_240aMas"].Text = dataItem["Importe_240aMas"].Text + "%";

                    }
                        
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdCuadro1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (lblGrilla.Value == "1")
                {
                    grdCuadro1.DataSource = JsonHelper.JsonDeserialize<List<gsReporteProyectado_Cuadro1Result>>((string)ViewState["lstCuadro1"]);
                    grdCuadro1.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdCuadro2_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (lblGrilla2.Value == "1")
                {
                    grdCuadro2.DataSource = JsonHelper.JsonDeserialize<List<gsReporteProyectado_Cuadro2Result>>((string)ViewState["lstCuadro2"]);
                    grdCuadro2.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }


        protected void ibExcel_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                string alternateText = (sender as ImageButton).AlternateText;

                //grdProducto.ExportSettings.FileName = "ReporteVenta_Cliente_Producto_" + DateTime.Now.ToString("yyyyMMddhhmm");
                //grdProducto.ExportSettings.ExportOnlyData = true;
                //grdProducto.ExportSettings.IgnorePaging = true;
                //grdProducto.ExportSettings.OpenInNewWindow = true;
                //grdProducto.MasterTableView.ExportToExcel();

                Page.Response.ClearHeaders();
                Page.Response.ClearContent();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }
        }

        private void ReporteCobranza_Cuadro1(int mes, int year, int id_zona)
        {
            try
            {

                int periodo;
                periodo = year * 100 + mes;


                CobranzasWCFClient objCobranzasWCF = new CobranzasWCFClient();
                List<gsReporteProyectado_Cuadro1Result> Lista;
                List<gsReporteProyectado_Cuadro1Result> ListaNew = new List<gsReporteProyectado_Cuadro1Result>();

                Lista = objCobranzasWCF.Reporte_CobranzasProyectadas_Cuadro1(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                                        ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario,
                                         mes, year, periodo, id_zona, null).ToList();

                gsReporteProyectado_Cuadro1Result objNew0 = new gsReporteProyectado_Cuadro1Result();
                gsReporteProyectado_Cuadro1Result objNew1 = new gsReporteProyectado_Cuadro1Result();
                gsReporteProyectado_Cuadro1Result objNew2 = new gsReporteProyectado_Cuadro1Result();
                gsReporteProyectado_Cuadro1Result objNew3 = new gsReporteProyectado_Cuadro1Result();
                gsReporteProyectado_Cuadro1Result objNew4 = new gsReporteProyectado_Cuadro1Result();
                gsReporteProyectado_Cuadro1Result objNew5 = new gsReporteProyectado_Cuadro1Result();

                objNew0 = Lista.FindAll(x => x.Periodo.Contains("Deuda")).Single();
                ListaNew.Add(objNew0); 

                if (Lista.FindAll(x => x.Periodo.Contains("Pronostico")).Count() > 0)
                {
                    objNew1 = Lista.FindAll(x => x.Periodo.Contains("Pronostico")).Single();
                }
                else
                {
                    objNew1.Periodo = "Pronostico";
                    objNew1.ImportePendiente = 0;
                    objNew1.Importe_NoVencidoMenor30 = 0;
                    objNew1.Importe_NoVencido30a0 = 0;
                    objNew1.Importe_01a15 = 0;
                    objNew1.Importe_16a30 = 0;
                    objNew1.Importe_31a60 = 0;
                    objNew1.Importe_61a90 = 0;
                    objNew1.Importe_91a120 = 0;
                    objNew1.Importe_121a240 = 0;
                    objNew1.Importe_240aMas = 0;
                }
                ListaNew.Add(objNew1);

                if (Lista.FindAll(x => x.Periodo.Contains("Cobrado")).Count() > 0)
                {
                    objNew2 = Lista.FindAll(x => x.Periodo.Contains("Cobrado")).Single();
                }
                else
                {
                    objNew2.Periodo = "Cobrado";
                    objNew2.ImportePendiente = 0;
                    objNew2.Importe_NoVencidoMenor30 = 0;
                    objNew2.Importe_NoVencido30a0 = 0;
                    objNew2.Importe_01a15 = 0;
                    objNew2.Importe_16a30 = 0;
                    objNew2.Importe_31a60 = 0;
                    objNew2.Importe_61a90 = 0;
                    objNew2.Importe_91a120 = 0;
                    objNew2.Importe_121a240 = 0;
                    objNew2.Importe_240aMas = 0;
                }


                ListaNew.Add(objNew2);

                objNew3.Periodo = "Avance";
                objNew3.ImportePendiente = (double)Valor_Avance((decimal)objNew1.ImportePendiente, (decimal)objNew2.ImportePendiente);
                objNew3.Importe_NoVencidoMenor30 = (double)Valor_Avance((decimal)objNew1.Importe_NoVencidoMenor30, (decimal)objNew2.Importe_NoVencidoMenor30);
                objNew3.Importe_NoVencido30a0 = (double)Valor_Avance((decimal)objNew1.Importe_NoVencido30a0, (decimal)objNew2.Importe_NoVencido30a0);
                objNew3.Importe_01a15 = (double)Valor_Avance((decimal)objNew1.Importe_01a15, (decimal)objNew2.Importe_01a15);
                objNew3.Importe_16a30 = (double)Valor_Avance((decimal)objNew1.Importe_16a30, (decimal)objNew2.Importe_16a30);
                objNew3.Importe_31a60 = (double)Valor_Avance((decimal)objNew1.Importe_31a60, (decimal)objNew2.Importe_31a60);
                objNew3.Importe_61a90 = (double)Valor_Avance((decimal)objNew1.Importe_61a90, (decimal)objNew2.Importe_61a90);
                objNew3.Importe_91a120 = (double)Valor_Avance((decimal)objNew1.Importe_91a120, (decimal)objNew2.Importe_91a120);
                objNew3.Importe_121a240 = (double)Valor_Avance((decimal)objNew1.Importe_121a240, (decimal)objNew2.Importe_121a240);
                objNew3.Importe_240aMas = (double)Valor_Avance((decimal)objNew1.Importe_240aMas, (decimal)objNew2.Importe_240aMas);

                ListaNew.Add(objNew3);


                if (Lista.FindAll(x => x.Periodo.Contains("Descuento")).Count() > 0)
                {
                    objNew4 = Lista.FindAll(x => x.Periodo.Contains("Descuento")).Single();
                }
                else
                {
                    objNew4.Periodo = "Descuento";
                    objNew4.ImportePendiente = 0;
                    objNew4.Importe_NoVencidoMenor30 = 0;
                    objNew4.Importe_NoVencido30a0 = 0;
                    objNew4.Importe_01a15 = 0;
                    objNew4.Importe_16a30 = 0;
                    objNew4.Importe_31a60 = 0;
                    objNew4.Importe_61a90 = 0;
                    objNew4.Importe_91a120 = 0;
                    objNew4.Importe_121a240 = 0;
                    objNew4.Importe_240aMas = 0;
                }
                ListaNew.Add(objNew4);


                if (Lista.FindAll(x => x.Periodo.Contains("OtrosIngresos")).Count() > 0)
                {
                    objNew5 = Lista.FindAll(x => x.Periodo.Contains("OtrosIngresos")).Single();
                }
                else
                {
                    objNew5.Periodo = "OtrosIngresos";
                    objNew5.ImportePendiente = 0;
                    objNew5.Importe_NoVencidoMenor30 = 0;
                    objNew5.Importe_NoVencido30a0 = 0;
                    objNew5.Importe_01a15 = 0;
                    objNew5.Importe_16a30 = 0;
                    objNew5.Importe_31a60 = 0;
                    objNew5.Importe_61a90 = 0;
                    objNew5.Importe_91a120 = 0;
                    objNew5.Importe_121a240 = 0;
                    objNew5.Importe_240aMas = 0;
                }
                ListaNew.Add(objNew5);

                grdCuadro1.DataSource = ListaNew;
                grdCuadro1.DataBind();

                ViewState["lstCuadro1"] = JsonHelper.JsonSerializer(ListaNew);

                if (Lista.Count > 0)
                {
                    lblGrilla.Value = "1";
                }
                else
                {
                    lblGrilla.Value = "0";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ReporteCobranza_Cuadro2(int mes, int year, int id_zona)
        {
            try
            {

                int periodo;
                periodo = year * 100 + mes;


                CobranzasWCFClient objCobranzasWCF = new CobranzasWCFClient();
                List<gsReporteProyectado_Cuadro2Result> Lista;
                List<gsReporteProyectado_Cuadro2Result> ListaNew = new List<gsReporteProyectado_Cuadro2Result>();

                Lista = objCobranzasWCF.Reporte_CobranzasProyectadas_Cuadro2(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                                        ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario,
                                         mes, year, periodo, id_zona, null).ToList();

                gsReporteProyectado_Cuadro2Result objNew0 = new gsReporteProyectado_Cuadro2Result();
                gsReporteProyectado_Cuadro2Result objNew1 = new gsReporteProyectado_Cuadro2Result();
                gsReporteProyectado_Cuadro2Result objNew2 = new gsReporteProyectado_Cuadro2Result();

                objNew0 = Lista.FindAll(x => x.Periodo.Contains("Deuda")).Single();
                ListaNew.Add(objNew0);

                if (Lista.FindAll(x => x.Periodo.Contains("Proyectado")).Count() > 0)
                {
                    objNew1 = Lista.FindAll(x => x.Periodo.Contains("Proyectado")).Single();
                }
                else
                {
                    objNew1.Periodo = "Proyectado";
                    objNew1.ClientesPendiente = 0;
                    objNew1.ClientesNoVencido = 0;
                    objNew1.Clientes01a70 = 0;
                    objNew1.Clientes71aMas = 0;
                }
                ListaNew.Add(objNew1);

                if (Lista.FindAll(x => x.Periodo.Contains("Cobrado")).Count() > 0)
                {
                    objNew2 = Lista.FindAll(x => x.Periodo.Contains("Cobrado")).Single();
                }
                else
                {
                    objNew2.Periodo = "Cobrado";
                    objNew2.ClientesPendiente = 0;
                    objNew2.ClientesNoVencido = 0;
                    objNew2.Clientes01a70 = 0;
                    objNew2.Clientes71aMas = 0;
                }

                ListaNew.Add(objNew2);


                grdCuadro2.DataSource = ListaNew;
                grdCuadro2.DataBind();

                ViewState["lstCuadro2"] = JsonHelper.JsonSerializer(ListaNew);

                if (Lista.Count > 0)
                {
                    lblGrilla2.Value = "1";
                }
                else
                {
                    lblGrilla2.Value = "0";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private decimal Valor_Avance(decimal ValorProyectado, decimal ValorCobrado)
        {
            decimal ValorAvance = 0; 
            try
            {

                if (ValorProyectado == 0)
                {
                    if (ValorCobrado == 0)
                    {
                        ValorAvance = 0;
                    }
                    else
                    {
                        ValorAvance = 100;
                    }
                }
                else
                {
                    ValorAvance = (ValorCobrado / ValorProyectado) * 100;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ValorAvance; 
        }




    }
}