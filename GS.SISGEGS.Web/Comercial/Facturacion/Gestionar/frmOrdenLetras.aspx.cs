using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.PedidoWCF;
using GS.SISGEGS.Web.LoginWCF;
using System.Web.Script.Serialization;
using GS.Helpdesk.entities.Commons;

namespace GS.SISGEGS.Web.Comercial.Facturacion.Gestionar
{
    public partial class frmOrdenLetras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PedidosFechas_Buscar(int.Parse(Request.QueryString["idOrdenVenta"]),0);

            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (!Page.IsPostBack)
                {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    Session["lstFechasNew"] = Session["lstFechas"]; 
                    if ((Request.QueryString["idOrdenVenta"]) == "0")
                    {
                        Title = "Registrar Planificación";
                        lblMensaje.Text = "Listo para registrar Planificación.";
                        List<gsPedidos_FechasLetrasSelectResult> lstFechas = new List<gsPedidos_FechasLetrasSelectResult>();
                        Session["lstFechas"] = lstFechas;
                    }
                    else
                    {
                        Title = "Modificar Planificación";
                        lblMensaje.Text = "Listo para modificar Planificación.";
                        List<gsPedidos_FechasLetrasSelectResult> lstFechas = new List<gsPedidos_FechasLetrasSelectResult>();
                        PedidosFechas_Buscar(0, int.Parse(Request.QueryString["idOrdenVenta"])); 
                    }

                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            string Letras;

            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            lblMensaje.Text = "";
            Letras = txtLetras.Text;
            string LetrasTXT;

            try
            {
                LetrasTXT = txtLetras.Text;
                Session["objLetras"] = LetrasTXT;
                Session["lstFechas"] = Session["lstFechasNew"];

                //---------------Validar 45 días -------------

                List<gsPedidos_FechasLetrasSelectResult> lstFechas = new List<gsPedidos_FechasLetrasSelectResult>();

                lstFechas = ((List<gsPedidos_FechasLetrasSelectResult>)Session["lstFechasNew"]);
                DateTime calendarMinDate = new DateTime();
                DateTime primeraLetra = new DateTime();

                int countDias = 0;
                TimeSpan difDayMinimo; 
            

                calendarMinDate = cdLetras.RangeMinDate; 

                if (lstFechas != null)
                {
                    if (lstFechas.Count > 0)
                    {
                        foreach (gsPedidos_FechasLetrasSelectResult objFecha in lstFechas.OrderBy(y => y.Fecha).ToList())
                        {
                            if(countDias==0)
                            {
                                primeraLetra = Convert.ToDateTime(objFecha.Fecha); 
                                countDias++;
                            }
                        }
                    }
                }

                //---------------  -------------
                difDayMinimo = primeraLetra.Subtract(calendarMinDate);
                    
                if (!string.IsNullOrEmpty(difDayMinimo.Days.ToString()))
                {
                    List<object> parametros = new List<object>();
                    parametros.Add(((Usuario_LoginResult)Session["Usuario"]).idEmpresa);
                    parametros.Add(Request.QueryString["id_agenda"].ToString());
                    string responseFromServer = GSbase.POSTResult("ListarMasterTen", 24, parametros);
                    MasterTenCollection collection = new JavaScriptSerializer().Deserialize<MasterTenCollection>(responseFromServer);
                    if (collection.Rows.Count() > 0)
                    {
                        if (difDayMinimo.Days > Convert.ToInt32(collection.Rows[0].v02)) //v02 = DiasPrimeraLetra
                        {
                            string Message = "La fecha de la primera letra debe estar dentro de los primeros " + collection.Rows[0].v02 + " días desde la fecha de emisión de la factura. Favor de consultar con el Sectorista de su Zona";
                            rwmPedidoMng.RadAlert(Message, 400, null, "Mensaje de error", null);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + 1 + ");", true);
                        }
                    }
                    else
                    {
                        //if (difDayMinimo.Days > 45 && lstFechas.Count > 1)
                        if (difDayMinimo.Days > 45)
                        {
                            //string Message = "La fecha de la primera letra debe estar dentro de los primeros 45 días desde la fecha de emisión de la factura.";
                            string Message = "La fecha de la primera letra debe estar dentro de los primeros 45 días desde la fecha de emisión de la factura. Favor de consultar con el Sectorista de su Zona";
                            rwmPedidoMng.RadAlert(Message, 400, null, "Mensaje de error", null);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + 1 + ");", true);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            string Letras;

            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            lblMensaje.Text = "";
            Letras = txtLetras.Text;
            string LetrasTXT;

            try
            {
                List<gsPedidos_FechasLetrasSelectResult> lstFechas = new List<gsPedidos_FechasLetrasSelectResult>();
                cdLetras.SelectedDates.Clear();
                txtLetras.Text = "";

                Session["lstFechasNew"] = lstFechas;
                Session["DiasCredito"] = 0;
                Session["objLetras"] = "";
                Session["lstFechas"] = Session["lstFechasNew"];

                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + 1 + ");", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            DateTime calendarMinDate = new DateTime();
            DateTime calendarMaxDate = new DateTime();
            string strFechaInico = Request.QueryString["FechaInicio"];
            string strFechaFin = Request.QueryString["FechaFin"];
            List<gsPedidos_FechasLetrasSelectResult> lstFechas = new List<gsPedidos_FechasLetrasSelectResult>();

            calendarMinDate = Convert.ToDateTime(strFechaInico.Substring(0,4) +"/" + strFechaInico.Substring(4,2) + "/" + strFechaInico.Substring(6,2));
            calendarMaxDate = Convert.ToDateTime(strFechaFin.Substring(0, 4) + "/" + strFechaFin.Substring(4, 2) + "/" + strFechaFin.Substring(6, 2));

            cdLetras.RangeMinDate = calendarMinDate;
            cdLetras.RangeMaxDate = calendarMaxDate;
            
            

            lstFechas = ((List<gsPedidos_FechasLetrasSelectResult>)Session["lstFechasNew"]);

            if(lstFechas != null)
            {
                if (lstFechas.Count > 0)
                {
                    //System.Collections.ArrayList ListaDate = new System.Collections.ArrayList();
                    //DateTime Fecha;
                    foreach (gsPedidos_FechasLetrasSelectResult objFecha in lstFechas.OrderBy(y => y.Fecha).ToList())
                    {
                        Telerik.Web.UI.RadDate objRadFecha = new Telerik.Web.UI.RadDate();
                        objRadFecha.Date = (DateTime)objFecha.Fecha;
                        cdLetras.SelectedDates.Add(objRadFecha); 
                    }
                    //SelectedDatesCollection objDates = new SelectedDatesCollection(ListaDate);
                    //cdLetras.SelectedDates.Add(objDates);
                    PedidosFechas_Letras(lstFechas);
                }
            }

          

            if (cdLetras.RangeSelectionStartDate != calendarMinDate && cdLetras.RangeSelectionEndDate != calendarMaxDate)
            {
                //RadDatePicker1.SelectedDate = RadCalendar2.RangeSelectionStartDate;
                //RadDatePicker2.SelectedDate = RadCalendar2.RangeSelectionEndDate;
            }
            else
            {
                //RadDatePicker1.Clear();
                //RadDatePicker2.Clear();
            }

        }

        protected void cdLetras_SelectionChanged(object sender, Telerik.Web.UI.Calendar.SelectedDatesEventArgs e)
        {
            List<gsPedidos_FechasLetrasSelectResult> lstFechas = new List<gsPedidos_FechasLetrasSelectResult>(); 
            int y = 0;
            int idOrdenVenta = int.Parse(Request.QueryString["idOrdenVenta"]);

            for (int i = 0; i < e.SelectedDates.Count; i++)
            {
                gsPedidos_FechasLetrasSelectResult objFecha = new gsPedidos_FechasLetrasSelectResult();
 
                objFecha.ID_Proceso = idOrdenVenta;  
                objFecha.Fecha = (e.SelectedDates[i]).Date;  
                lstFechas.Add(objFecha);  
            }

            Session["lstFechasNew"] = lstFechas; 
            PedidosFechas_Letras(lstFechas); 

        }

        private void PedidosFechas_Letras(List<gsPedidos_FechasLetrasSelectResult> lstFechas)
        {
            DateTime FechaPedido = new DateTime();
            DateTime FechaSelect = new DateTime();
            TimeSpan ts;
            string Letras = "";
            DateTime calendarMinDate = new DateTime();
            string strFechaInico = Request.QueryString["FechaInicio"];
            int diferencia = 0;
            int inicial = 0;
            int final = 0;
            int y = 0;

            calendarMinDate = Convert.ToDateTime(strFechaInico.Substring(0, 4) + "/" + strFechaInico.Substring(4, 2) + "/" + strFechaInico.Substring(6, 2));
            FechaPedido = calendarMinDate;
            txtLetras.Text = "";

            var lstFecha = lstFechas.OrderBy(x => x.Fecha).ToList();
            foreach (var objFecha in lstFecha)
            {
                FechaSelect = Convert.ToDateTime(objFecha.Fecha);
                ts = FechaSelect - FechaPedido;
                diferencia = ts.Days;
                Session["DiasCredito"] = diferencia;

                if (inicial == 0)
                {
                    Letras = Letras + diferencia;
                    inicial++;
                }
                else
                {
                    Letras = Letras + "-" + diferencia;
                }

                y++;
            }
            Letras = "L" + Letras;

            if (y > 0)
            {
                txtLetras.Text = Letras;
            }
        }

        // Plan Letras
        private void PedidosFechas_Buscar(int OpOV, int OpDoc)
        {
            try
            {
                List<gsPedidos_FechasLetrasSelectResult> lstFechas = new List<gsPedidos_FechasLetrasSelectResult>();
                PedidoWCFClient objPedidoWCF = new PedidoWCFClient();
                if(OpOV > 0 || OpDoc > 0)
                {
                    lstFechas = objPedidoWCF.PedidoLetras_Detalle(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, OpOV, OpDoc, "Tabla").ToList();
                    Session["lstFechas"] = lstFechas;
                }
                else
                {
                    Session["lstFechas"] = lstFechas;
                }

                PedidosFechas_Letras(lstFechas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}