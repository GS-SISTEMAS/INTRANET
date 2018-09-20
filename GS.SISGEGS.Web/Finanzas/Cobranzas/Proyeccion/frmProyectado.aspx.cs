using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using GS.SISGEGS.Web.EstadoCuentaWCF;
using GS.SISGEGS.Web.CobranzasWCF;
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
using System.ComponentModel; 


namespace GS.SISGEGS.Web.Finanzas.Cobranzas
{
    public partial class frmProyectado : System.Web.UI.Page
    {
        private DataTable ListaToDataTable<T>(List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection =
                TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);


                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[propertyDescriptorCollection.Count];
            foreach (T iListItem in iList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        private void Proyectado_Listar( int periodo, string id_sectorista, int id_zona, int anho, int mes ) {
            CobranzasWCFClient objProyectadoWCF = new CobranzasWCFClient();
            List<spEstadoCuenta_ProyectadoResult> lstProyectado = new List<spEstadoCuenta_ProyectadoResult>();

            try
            {
                lstProyectado = objProyectadoWCF.EstadoCuenta_Proyectado(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                   
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, periodo, id_sectorista, id_zona, anho, mes).ToList();

                var query_Detalle = from c in lstProyectado
                                    orderby c.id_Cliente
                                    select new
                                    {
                                        c.Proyectado,
                                        c.ClienteNombre,
                                        c.estado,
                                        c.id_Cliente,
                                        c.id_estado,
  
                                        c.Id_Zona,
                                        c.MontoS1,
                                        c.MontoS2,
                                        c.MontoS3,
                                        c.MontoS4,
                                        c.obsercacion,
                                        c.Periodo,
  
                                        c.Proyectado01a30,
                                        c.Proyectado31a60,
                                        c.Proyectado61a120,
                                        c.Proyectado121a360,
                                        c.Proyectado360aMas,
                                        c.ImportePendiente,
                                        c.ImportePendienteNoVencido,
                                        c.ImportePendienteVencido
                                         
                                    };

                List<spEstadoCuenta_ProyectadoResult> lstProyectadoT = new List<spEstadoCuenta_ProyectadoResult>();
                int y = 0; 
                foreach (var linea in query_Detalle)
                {
                    spEstadoCuenta_ProyectadoResult lineaP = new spEstadoCuenta_ProyectadoResult();
                    lineaP.ClienteNombre = linea.ClienteNombre;
                    lineaP.estado = linea.estado;
                    lineaP.id_Cliente = linea.id_Cliente;
                    lineaP.id_estado = linea.id_estado;
         
                    lineaP.Id_Zona = linea.Id_Zona;
                    lineaP.MontoS1 = linea.MontoS1;
                    lineaP.MontoS2 = linea.MontoS2;
                    lineaP.MontoS3 = linea.MontoS3;
                    lineaP.MontoS4 = linea.MontoS4;
                    lineaP.obsercacion = linea.obsercacion;
                    lineaP.Periodo = linea.Periodo;
                    lineaP.Proyectado = linea.Proyectado;
 
                    lineaP.Proyectado01a30 = linea.Proyectado01a30;
                    lineaP.Proyectado31a60 = linea.Proyectado31a60;
                    lineaP.Proyectado61a120 = linea.Proyectado61a120;
                    lineaP.Proyectado121a360 = linea.Proyectado121a360;
                    lineaP.Proyectado360aMas = linea.Proyectado360aMas;
                    lineaP.ImportePendiente = linea.ImportePendiente;
                    lineaP.ImportePendienteNoVencido = linea.ImportePendienteNoVencido;
                    lineaP.ImportePendienteVencido = linea.ImportePendienteVencido;

                    lstProyectadoT.Add(lineaP);
                    y++;
                }

                string strminimo = "0.5"; 
                decimal minimo = decimal.Parse(strminimo); 
                var ListarDetalle = query_Detalle.ToList().FindAll(x => x.ImportePendiente > minimo).ToList(); 

                grdDocVenta.DataSource = ListarDetalle;
                grdDocVenta.DataBind();

                ViewState["lstProyectado"] = lstProyectadoT;

                if(lstProyectado.Count > 0)
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
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void Proyectado_Verificar(string id_sectorista, string periodo, int id_zona)
        {
            CobranzasWCFClient objProyectadoWCF = new CobranzasWCFClient();
            int respuesta; 

            try
            {
                respuesta = objProyectadoWCF.ProyectadoCobranza_Verificar(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, id_sectorista, periodo, id_zona) ;

                //if(lblGrilla.Value == "0")
                //{
                //    btnCargaMasiva.Enabled = false;
                //    btnExcel.Enabled = false; 
                //}
                //else
                //{
                //    btnCargaMasiva.Enabled = true;
                //    btnExcel.Enabled = true; 
                //}               
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscarSectorista_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (cboSectorista.SelectedValue != "0" )
                    throw new ArgumentException("Debe seleccionar un sectorista valido");

                lblCodigoSectorista.Value = cboSectorista.SelectedValue.ToString();
                Zona_Cargar(lblCodigoSectorista.Value);
                cboZona.Enabled = true; 
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
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

                    Sectorista_Cargar();

                    if (string.IsNullOrEmpty(Request.QueryString["fechaInicio"]))
                    {
                        rmyReporte.SelectedDate = DateTime.Now;
                        Zona_Cargar("0");
                        empresa = ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa;

                        btnBuscar.Enabled = false;
                        cboZona.Enabled = false;
                        //btnCargaMasiva.Enabled = false;
                        //btnExcel.Enabled = false; 
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

                        Sectorista_Cargar(); 
                        lblCodigoSectorista.Value = id_sectorista; 
                        Zona_Cargar(id_sectorista);
                        cboZona.SelectedValue = id_zonasec;

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
            int periodo, id_zona;
            int year;
            int mes;
            string id_sectorista;
 
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                year = rmyReporte.SelectedDate.Value.Year ;
                mes = rmyReporte.SelectedDate.Value.Month ;
 
                periodo = (year * 100) + mes; 

                id_sectorista = cboSectorista.SelectedValue.ToString();
                id_zona = int.Parse(cboZona.SelectedValue) ;

                Proyectado_Listar(periodo, id_sectorista,id_zona, year,mes);
                ViewState["fechaInicial"] = rmyReporte.SelectedDate.Value;

                cboZona.SelectedValue = id_zona.ToString();
                ViewState["id_Sectorista"] = id_sectorista;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
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
                if(lblGrilla.Value == "1" )
                {
                    grdDocVenta.DataSource = JsonHelper.JsonDeserialize<List<spEstadoCuenta_ProyectadoResult>>((string)ViewState["lstProyectado"]);
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
            //if ((e.Item.IsDataBound))
            //{
            //    GridDataItem dataitem = (GridDataItem)e.Item;
            //    string cliente = dataitem.GetDataKeyValue("id_Cliente").ToString();
            //    string vendedor = dataitem.GetDataKeyValue("ID_Vendedor").ToString();

            //    int anho = rmyReporte.SelectedDate.Value.Year;
            //    int mes = rmyReporte.SelectedDate.Value.Month;
            //    decimal deuda = DeudaVencidaMes(cliente, vendedor, anho, mes);

            //    GridDataItem dataItem = (GridDataItem)e.Item;
            //    RadTextBox txtCountry = ((RadTextBox)dataItem.FindControl("txtCountry"));

            //    string format = string.Format("{0: #,##0.00}", deuda);
            //    txtCountry.Text = format;
            //    txtCountry.ReadOnly = true; 
            
            //}
        }

        public decimal DeudaVencidaMes(string codAgenda, string codVendedor, int anho, int mes)
        {
            decimal deudaVencidaMes = 0;
            DateTime fecha;

            if (mes == DateTime.Now.Month)
            {
                fecha = DateTime.Now;
            }
            else
            {
                DateTime firstOfNextMonth = new DateTime(anho, mes, 1).AddMonths(1);
                DateTime lastOfThisMonth = firstOfNextMonth.AddDays(-1);
                fecha = lastOfThisMonth;
            }

            try
            {
                List<gsEstadoCuenta_TotalResult> lstPendientes = BuscarDocumentosPendientes(codAgenda, codVendedor, fecha);
                var query_Detalle = from c in lstPendientes
                                    orderby c.ID_Agenda
                                    select new
                                    {
                                        c.DeudaTotal
                                    };
                var sumPendiente = query_Detalle.ToList().Select(c => c.DeudaTotal).Sum();
                decimal DeudaTotal;

                DeudaTotal = Convert.ToDecimal(sumPendiente);
                deudaVencidaMes = DeudaTotal;
            }
            catch (Exception ex)
            {
                deudaVencidaMes = 0; 
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }

            return deudaVencidaMes;
        }

        public List<gsEstadoCuenta_TotalResult> BuscarDocumentosPendientes(string idCliente, string idVendedor, DateTime fechaForm2)
        {
            DateTime fecha1;
            DateTime fecha2;
            DateTime fecha3;
            DateTime fecha4;
            string Cliente;
            string Vendedor;

            Cliente = null;
            Vendedor = null;
            List<gsEstadoCuenta_TotalResult> lst = new List<gsEstadoCuenta_TotalResult>();

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (Validar_Variables() == 0)
                {
                    fecha2 = fechaForm2;
                    fecha1 = fecha2.AddYears(-50);
                    fecha3 = fecha2.AddYears(-50);
                    fecha4 = fecha2.AddYears(50);

                    Cliente = idCliente;
                    Vendedor = idVendedor;

                    lst = ListarEstadoCuenta(Cliente, Vendedor, fecha2);
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            return lst;
        }

        private List<gsEstadoCuenta_TotalResult> ListarEstadoCuenta(string codAgenda, string codVendedor, DateTime FechaHasta)
        {
            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();
            List<gsEstadoCuenta_TotalResult> lst = new List<gsEstadoCuenta_TotalResult>();

            try
            {
                lst = objEstadoCuentaWCF.EstadoCuenta_ClienteBI(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, codAgenda, codVendedor, FechaHasta).ToList();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            return lst;
        }
        public int Validar_Variables()
        {
            int valor = 0;
            return valor;
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

                var lstZonas= from x in lstZona
                                select new
                                {
                                    x.ID_Zona,
                                    DisplayID = String.Format("{0}", x.ID_Zona),
                                    DisplayField = String.Format("{0}", x.Zona)
                                    //DisplayField = String.Format("{0} {1} {2} {3}", x.Placa, x.Modelo, x.Marca, x.Agenda)
                                } ;

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

        protected void grdDocVenta_ItemCommand(object sender, GridCommandEventArgs e)
        {
            string id_Sectorista;
            string stringPeriodo;
            string strId_Cliente; 

            string year;
            string mes;
            string day; 

            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if (e.CommandName == "Gestion")
                {
                    year = rmyReporte.SelectedDate.Value.Year.ToString();
                    mes = rmyReporte.SelectedDate.Value.Month.ToString();
 
                    if (mes.Length == 1)
                    {mes = "0" + mes;}

                    GridDataItem dataitem = (GridDataItem)e.Item;
                    string ID_Zona = dataitem.GetDataKeyValue("ID_Zona").ToString();

                    strId_Cliente = e.CommandArgument.ToString();
                    Session["strId_Cliente"] = strId_Cliente; 


                    if (ID_Zona != "0")
                    {
                        stringPeriodo = year + "" + mes;
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreateViewGestion(" + strId_Cliente + "," + ViewState["id_Sectorista"] + "," + ID_Zona + "," + stringPeriodo + ");", true);
                    }
                    else
                    {
                        lblMensaje.Text = "No se realizó la proyección del cliente para el periodo seleccionado.!!";
                        lblMensaje.CssClass = "mensajeError";

                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "buscarError();", true);
                    }

                }

                if (e.CommandName == "PeriodoDeuda")
                {
                    year = rmyReporte.SelectedDate.Value.Year.ToString();
                    mes = rmyReporte.SelectedDate.Value.Month.ToString();

                    if (mes.Length == 1)
                    {
                        mes = "0" + mes;
                    }

                    stringPeriodo = year + "" + mes;

                    GridDataItem dataitem = (GridDataItem)e.Item;
                    string id_Cliente = dataitem.GetDataKeyValue("id_Cliente").ToString();
          
                    id_Cliente = "1" + id_Cliente;
                    string id_sectorista = ViewState["id_Sectorista"].ToString(); 

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreateViewDeuda(" + id_Cliente + "," + id_sectorista + ");", true);

                }
                if (e.CommandName == "EstadoCuenta")
                {
                    GridDataItem dataitem = (GridDataItem)e.Item;
                    string ID_Vendedor = "1" + dataitem.GetDataKeyValue("ID_Vendedor").ToString();
                    string ID_zona = dataitem.GetDataKeyValue("ID_Zona").ToString();
                    string ID_Cliente = "1" + e.CommandArgument.ToString();

                    id_Sectorista = "1" + ViewState["id_Sectorista"].ToString();
                    string strFecha;
                    year = rmyReporte.SelectedDate.Value.Year.ToString();
                    mes = rmyReporte.SelectedDate.Value.Month.ToString();

                    if(int.Parse(mes) < 10)
                    {
                        mes = "0" + mes; 
                    }

                    strFecha = year + "" + mes;

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowCreateViewEstadoCuenta(" + ID_Cliente + "," + strFecha + "," + ID_Vendedor + "," + id_Sectorista  + "," + ID_zona + ");", true);

                    //Response.Redirect("~/Finanzas/Cobranzas/Proyeccion/frmEstadoCuenta.aspx?id_cliente=" + e.CommandArgument.ToString() + "&fechaInicial=" + ((DateTime)ViewState["fechaInicial"]).ToString("dd/MM/yyyy") + "&ID_Vendedor=" + ID_Vendedor + "&ID_Sectorista=" + id_Sectorista + "&ID_zona=" + ID_zona);
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private DataTable funConvertGVToDatatable(GridView dtgrv)
        {
            DataTable dt = new DataTable();
            try
            {
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
                        string str = "'";
                        string dato;

                        //HttpUtility.HtmlDecode(Trim(GridViewFiltro.Rows(xx).Cells(0).Text))

                        dato = HttpUtility.HtmlDecode(row.Cells[i].Text).ToString();
                        dato = dato.Replace(" ", "");

                        if ( i == 3 || i == 0)
                        { dato = str + dato; }

                        dr[i] = dato;
                    }
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            return dt;
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
            catch(Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }

        }
        protected void cboZonas_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Ocultar_Botones();
                List<gsProyectadoCobranza_ListarResult> lstProyectado = new List<gsProyectadoCobranza_ListarResult>();
                grdDocVenta.DataSource = lstProyectado;
                grdDocVenta.DataBind();

                if (cboZona == null || cboZona.SelectedValue == "" || cboZona.SelectedValue == "0")
                {
                    Ocultar_Botones();
                    btnBuscar.Enabled = false;
                    cboZona.Enabled = true;
                }
                else
                {
                    cboZona.Enabled = true;
                    btnBuscar.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }

        }

        protected void cboSectorista_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Ocultar_Botones();
                Zona_Cargar(cboSectorista.SelectedValue);
                List<gsProyectadoCobranza_ListarResult> lstProyectado = new List<gsProyectadoCobranza_ListarResult>();
                grdDocVenta.DataSource = lstProyectado;
                grdDocVenta.DataBind();

                if (cboSectorista == null || cboSectorista.SelectedValue == "" || cboSectorista.SelectedValue == "0")
                {
                    Ocultar_Botones();
                    cboZona.Enabled = false;
                }
                else
                {
                    cboZona.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void Ocultar_Botones()
        {
            try
            {
                btnBuscar.Enabled = false;
                cboZona.Enabled = false;
                //btnCargaMasiva.Enabled = false;
                //btnExcel.Enabled = false;
                List<gsProyectadoCobranza_ListarResult> lstProyectado = new List<gsProyectadoCobranza_ListarResult>();
                grdDocVenta.DataSource = lstProyectado;
                grdDocVenta.DataBind();
            }
            catch(Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void ExporttoExcel_V2(GridView GridView1, string Archivo, string id_Sectorista)
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

                //Origen = "C:\\Users\\cesar.coronel\\Desktop\\Pruebas_TXT\\Plantillas\\Origen\\Plantilla_Cobranza_Proyectado.xls";
                Origen = "C:\\inetpub\\www\\IntranetGS\\Comercial\\Proyectado\\Plantilla\\Plantilla_Cobranza_Proyectado.xls";

                //Destino = "C:\\Users\\cesar.coronel\\Desktop\\Pruebas_TXT\\Plantillas\\Destino\\" + Archivo + ".xls";
                Destino = "C:\\temp\\uploads\\ReporteCobranza_Proyectado_" + id_Sectorista.ToString() + "_" + strFecha + ".xls";

                File.Copy(Origen, HttpUtility.HtmlDecode(Destino), true);

                ESCRIBIR_EXCEL_GENERAL(HttpUtility.HtmlDecode(Destino), "Proyectado", table);

                System.IO.FileInfo toDownload = new System.IO.FileInfo(HttpUtility.HtmlDecode(Destino));
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
                Response.AddHeader("Content-Length", toDownload.Length.ToString());
                Response.ContentType = "application/xls";
                string tab = string.Empty;

                Response.WriteFile(HttpUtility.HtmlDecode(Destino));
                Response.End();

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "MensajeError";
            }

        }

        public void ESCRIBIR_EXCEL_GENERAL(string Directorio, string Nombre_Hoja, System.Data.DataTable dtDatos)
        {
            try
            {
                Escribir_Excel_Proyectado(Directorio, Nombre_Hoja, dtDatos);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        public void Escribir_Excel_Proyectado(string Directorio, string NombreArchivo, DataTable Tabla)
        {
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
               
                char a;
                int cod;
                string Letra = "A";
                a = Convert.ToChar(Letra);
                cod = (int)a;

                //List<gsReporteVentaPresupuesto_ProductoResult> lstPresupuesto = JsonHelper.JsonDeserialize<List<gsReporteVentaPresupuesto_ProductoResult>>((string)ViewState["lstProyectado"]);

                int fila = 2;

                foreach (DataRow row in Tabla.Rows)
                {//write in new row
                    string pareto = row[countColumn].ToString();
                    Letra = "A";
                    a = Convert.ToChar(Letra);
                    cod = (int)a;
                    string Texto = "";


                    Texto = "UPDATE [" + NombreArchivo + "$" + "A" + fila.ToString() + ":" + "K" + fila.ToString() + "]   ";
                    Texto = Texto + " SET F1=" + "'" + row[0].ToString().Replace("'","").Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F2=" + "'" + row[1].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F3=" + "'" + row[2].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F4=" + "'" + row[3].ToString().Replace("'", "").Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F5=" + "'" + row[4].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F6=" + "'" + row[5].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F7=" + "'" + row[6].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F8=" + "'" + row[7].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F9=" + "'" + row[8].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F10=" + "'" + row[9].ToString().Replace("&nbsp;", "") + "', ";
                    Texto = Texto + "  F11=" + "'" + row[10].ToString().Replace("&nbsp;", "") + "' ";
                    

                    cmd.CommandText = Texto;
                    cmd.ExecuteNonQuery();

                   

                   
                    fila = fila + 1;
                }


                oleConn.Close();
            }
            catch (Exception ex)
            {
                oleConn.Close();
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string id_Sectorista;
            string stringPeriodo;
            int year;
            int mes;
            string Fecha;
            string ArchivoExcel;
            string nombreZona; 
            Fecha = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
            //Fecha = Fecha + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            char text =Convert.ToChar("|");

            nombreZona = HttpUtility.HtmlDecode(cboZona.SelectedItem.Text.Split(text)[0].ToString().Trim().ToString()); 

            try
            {
                if (lblGrilla.Value == "1")
                {
                    id_Sectorista = ViewState["id_Sectorista"].ToString();
                    year = rmyReporte.SelectedDate.Value.Year;
                    mes = rmyReporte.SelectedDate.Value.Month;


                    ArchivoExcel = "Cobranza_" + id_Sectorista + "_" + nombreZona  + "_" + Fecha;

                    string strMes = mes.ToString();
                    if (strMes.Length == 1)
                    {
                        strMes = "0" + strMes;
                    }

                    stringPeriodo = year + "" + strMes;

                    List<gsProyectadoCobranza_ListarResult> lst = ((List<gsProyectadoCobranza_ListarResult>)ViewState["lstProyectado"]);

                    var query_Detalle = from c in lst
                                        select new
                                        {
                                            codSectorista = id_Sectorista.ToString(),
                                            codZona = c.ID_Zona.ToString(),
                                            Zona = c.Zona.ToString(),
                                            codCliente = c.id_Cliente.ToString(),
                                            Cliente = c.Cliente.ToString(),
                                            Periodo = stringPeriodo.ToString(),
                                            Deuda = string.Format("{0:#,##0.00}", c.Deuda),
                                            Semana1 = string.Format("{0:#,##0.00}", c.MontoS1),
                                            Semana2 = string.Format("{0:#,##0.00}", c.MontoS2),
                                            Semana3 = string.Format("{0:#,##0.00}", c.MontoS3),
                                            Semana4 = string.Format("{0:#,##0.00}", c.MontoS4)
                                        };

                    GridView GridView1 = new GridView();
                    GridView1.AllowPaging = false;
                    GridView1.DataSource = query_Detalle;
                    GridView1.DataBind();

                    //DataTable dtLista = ListaToDataTable(lst);
                    ExporttoExcel_V2(GridView1, ArchivoExcel, id_Sectorista);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }


        }

        protected void ramProyectado_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if (e.Argument == "Rebind")
                {
                    grdDocVenta.MasterTableView.SortExpressions.Clear();
                    grdDocVenta.MasterTableView.GroupByExpressions.Clear();
                    Buscar_ListarProyectado();
                    grdDocVenta.Rebind();

                    lblMensaje.Text = "Proyección cargada con éxito.";
                    lblMensaje.CssClass = "mensajeExito";
                }


                if (e.Argument.Split('(')[0].Trim() == "RebindAndNavigateBuscar")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "buscar();", true);
                
                }

                if (e.Argument.Split('(')[0].Trim() == "RebindAndNavigate")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "buscarHistorico();", true);
                }

            }
            catch (Exception ex)
            {
                rwmProyectado.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }



    }
}