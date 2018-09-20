using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.CobranzasWCF;
using GS.SISGEGS.Web.UsuarioWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.PerfilWCF;
using System.Web.Services;
using Telerik.Web.UI;

namespace GS.SISGEGS.Web.Finanzas.Cobranzas.Proyeccion
{
    public partial class frmSemanal : System.Web.UI.Page
    {
        float Maximno;
        float Acumulado;
        float Saldo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (!Page.IsPostBack)
                {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    if (Request.QueryString["objDocumento"] != "" || Request.QueryString["objDocumento"] != null)
                    {
                        string obj = Request.QueryString["objDocumento"];

                        char delimiter = '}';
                        string[] arryString = Request.QueryString["objDocumento"].Split(delimiter);
                        arryString[0] = arryString[0] + "}";
                        arryString[1] = arryString[1].Substring(1, 6);

                        gsReporte_EstadoCuenta_BIResult objDocumento = JsonHelper.JsonDeserialize<gsReporte_EstadoCuenta_BIResult>(arryString[0]);

                        Title = "Planificación de Cobranza";

                        lblDocumento.Text = objDocumento.TipoDocumento;
                        lblNumero.Text = objDocumento.NroDocumento;

                        lblImporte.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(objDocumento.ImportePendiente.ToString()));


                        lblMoneda.Text = objDocumento.monedasigno;
                        lblMonedaImporte.Text = objDocumento.monedasigno;

                        Maximno = (float)objDocumento.ImportePendiente;
                        ViewState["Maximno"] = Maximno;

                        Semana_cargar(cboSemana);
                        string fecha = arryString[1];

                        DateTime FechaD;
                        string year = fecha.Substring(0, 4);
                        string mes = fecha.Substring(4, 2);
                        FechaD = DateTime.Parse(year + "/" + mes + "/" + "01");

                        rmyReporte.SelectedDate = FechaD;

                        Proyeccion_Sectorista(0, fecha, 0, objDocumento.Origen, (int)objDocumento.OrigenOp, 1);

                        //PeriodoSemana_cargar();

                        lblMensaje.Text = "Listo para mostrar planificación de sectorista";
                        lblMensaje.CssClass = "mensajeExito";

                        rmyReporte.DateInput.ReadOnly = true;
                        rmyReporte.DatePopupButton.Enabled = false;

                    }

                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            CobranzasWCFClient objCobranzaWCF = new CobranzasWCFClient();
            int id_proyectado = 0, id_semana = 0, id_estatus = 0;
            string periodo = "", mensaje = "", mes;

            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);


            lblMensaje.Text = "";

            try
            {

                if (ValidarValores(ref mensaje) == false)
                {
                    if (!string.IsNullOrEmpty(txtImporte.Text) || (txtImporte.Text != ""))
                    {

                        string semanaMng = ViewState["id_semana"].ToString();
                        string semanaCbo = cboSemana.SelectedValue.ToString();

                        if (semanaMng != semanaCbo)
                        {
                            Maximno = (float)ViewState["Maximno"];
                            Acumulado = (float)ViewState["Acumulado"];
                            Saldo = Maximno - Acumulado;
                            ViewState["Saldo"] = Saldo;
                        }


                        char delimiter = '}';
                        string[] arryString = Request.QueryString["objDocumento"].Split(delimiter);
                        arryString[0] = arryString[0] + "}";
                        arryString[1] = arryString[1].Substring(1, 6);

                        gsReporte_EstadoCuenta_BIResult objDocumento = JsonHelper.JsonDeserialize<gsReporte_EstadoCuenta_BIResult>(arryString[0]);
                        string fecha = arryString[1];

                        id_semana = int.Parse(cboSemana.SelectedValue);
                        float Importe = float.Parse(txtImporte.Text);
                        string TablaOrigen = objDocumento.Origen;
                        int OpOrigen = (int)objDocumento.OrigenOp;

                        periodo = arryString[1];

                        Saldo = (float)ViewState["Saldo"];
                        if (Importe <= Saldo)
                        {
                            objCobranzaWCF.ProyectarCobranza_Registrar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, id_proyectado, periodo, id_semana, Importe, TablaOrigen, OpOrigen, 1);

                            LimpiarValores();
                            Proyeccion_Sectorista(id_proyectado, periodo, 0, objDocumento.Origen, (int)objDocumento.OrigenOp, 1);
                            //PeriodoSemana_cargar();
                            lblMaximo.CssClass = "mensajeExito";
                        }
                        else
                        {
                            lblMensaje.Text = "Ingresar un importe igual o menor a " + ViewState["Saldo"];
                            lblMensaje.CssClass = "mensajeError";
                        }
                    }
                    else
                    {
                        throw new ArgumentException("ERROR: Se debe ingresar una Observación. ");
                    }
                }
                else
                {
                    lblMensaje.Text = "ERROR: " + mensaje;
                    lblMensaje.CssClass = "mensajeError";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                char delimiter = '}';
                string[] arryString = Request.QueryString["objDocumento"].Split(delimiter);
                arryString[0] = arryString[0] + "}";
                arryString[1] = arryString[1].Substring(1, 6);

                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + arryString[1] + ");", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            List<ProyectadoCobranza_ListarResult> lista = new List<ProyectadoCobranza_ListarResult>();
            int id_semana = Convert.ToInt32(cboSemana.SelectedValue);

            if (lblGrilla.Value == "1")
            {
                try
                {
                    ProyectadoCobranza_ListarResult objeto = new ProyectadoCobranza_ListarResult();
                    lista = JsonHelper.JsonDeserialize<List<ProyectadoCobranza_ListarResult>>((string)ViewState["lstProyeccion"]);

                    Maximno = (float)ViewState["Maximno"];
                    Acumulado = (float)ViewState["Acumulado"];
                    Saldo = Maximno - Acumulado;
                    ViewState["Saldo"] = Saldo;

                    objeto = lista.FindAll(x => x.id_Semana == id_semana).Single();

                    if (objeto != null)
                    {
                        float importe = (float)objeto.Importe;
                        Saldo = (float)ViewState["Saldo"];

                        Saldo = Saldo + importe;
                        ViewState["Saldo"] = Saldo;

                        txtImporte.Text = importe.ToString();
                    }
                    else
                    {
                        txtImporte.Text = 0.ToString();
                    }

                    txtImporte.Enabled = true;
                    btnAgregar.Enabled = true;
                    cboSemana.SelectedValue = id_semana.ToString();
                    ViewState["id_semana"] = id_semana;

                    foreach (GridDataItem item in grdCobranza.MasterTableView.Items)
                    {
                        if (item.GetDataKeyValue("id_semana").ToString() == id_semana.ToString())
                        {
                            item.Selected = true;
                        }
                        else
                        {
                            item.Selected = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    txtImporte.Text = 0.ToString();
                    ViewState["id_semana"] = id_semana;

                    foreach (GridDataItem item in grdCobranza.MasterTableView.Items)
                    {
                        item.Selected = false;
                    }


                    lblMensaje.Text = "ERROR: " + ex.Message;
                    lblMensaje.CssClass = "mensajeError";
                }

            }
        }

        public bool ValidarValores(ref string mensaje)
        {  
            bool bloqueo = false;
            int gtSemana = 0; 

            int ToDay = 0;
            int ToSemana = 0;
            
            ToDay = DateTime.Now.Day;
            gtSemana = int.Parse(cboSemana.SelectedValue ) ; 
            
            if(ToDay<8)
            {
                ToSemana = 1; 
            }
            else if(ToDay < 16)
            {
                ToSemana = 2;

            }
            else if (ToDay < 22)
            {
                ToSemana = 3;

            }
            else if (ToDay < 32)
            {
                ToSemana = 4;
            }


            try
            {
                if (cboSemana.SelectedValue == "0")
                {
                    mensaje = "Seleccionar Semana. ";
                    bloqueo = true;
                }
                if (txtImporte.Text == "" || txtImporte.Text.Length == 0)
                {
                    mensaje = mensaje + "Ingresar Importe. ";
                    bloqueo = true;
                }

                if (gtSemana < ToSemana)
                {
                    mensaje = mensaje + "La semana está cerrada.";
                    bloqueo = true;
                }

                rmyReporte.Enabled = false; 


            }
            catch (Exception ex)
            {

                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
            return bloqueo;
        }

        private void Semana_cargar(RadComboBox cboBox)
        {
            int ToDay = 0;
            ToDay = DateTime.Now.Day;

            try
            {
                RadComboBoxItem item;
                int id;
                string strId;

                for (int x = 0; x < 4; x++)
                {
                    id = x + 1;
                    strId = id.ToString();

                    item = new RadComboBoxItem();
                    item.Value = strId;
                    item.Text = strId;
                    cboBox.Items.Add(item);
                }
                if (cboBox.Items.Count > 0)
                    cboBox.SelectedIndex = 0;

                if (ToDay < 8)
                {
                    cboBox.SelectedValue = "1";
                }
                else if (ToDay < 16)
                {
                    cboBox.SelectedValue = "2";

                }
                else if (ToDay < 22)
                {
                    cboBox.SelectedValue = "3";
                }
                else if (ToDay < 32)
                {
                    cboBox.SelectedValue = "4";
                }


                ViewState["id_semana"] = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void PeriodoSemana_cargar()
        //{
        //    string semana;
        //    string periodo;
        //    string strFecha;
        //    int intSemana;
        //    int year;
        //    int mes;
        //    DateTime fechaS;

        //    List<ProyectadoCobranza_ListarResult> lista = new List<ProyectadoCobranza_ListarResult>();
        //    try
        //    {
        //        if (lblGrilla.Value == "1")
        //        {
        //            ProyectadoCobranza_ListarResult objeto = new ProyectadoCobranza_ListarResult();
        //            lista = JsonHelper.JsonDeserialize<List<ProyectadoCobranza_ListarResult>>((string)ViewState["lstProyeccion"]);

        //            var ultimaGestion = (from x in lista
        //                                 orderby x.id_Semana descending
        //                                 select new
        //                                 {
        //                                     x.id_Semana
        //                                 }).First();

        //            semana = ultimaGestion.id_Semana.ToString();
        //            intSemana = int.Parse(semana);

        //            if (intSemana != 4)
        //            {
        //                intSemana = intSemana + 1;
        //            }

        //            cboSemana.SelectedValue = intSemana.ToString();
        //        }
        //        else
        //        {
        //            cboSemana.SelectedValue = 1.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMensaje.Text = "ERROR: " + ex.Message;
        //        lblMensaje.CssClass = "mensajeError";
        //    }
        //}

        public void LimpiarValores()
        {
            int ToDay = 0;
            ToDay = DateTime.Now.Day;
            try
            {
                if (ToDay < 8)
                {
                    cboSemana.SelectedValue = "1";
                }
                else if (ToDay < 16)
                {
                    cboSemana.SelectedValue = "2";

                }
                else if (ToDay < 22)
                {
                    cboSemana.SelectedValue = "3";
                }
                else if (ToDay < 32)
                {
                    cboSemana.SelectedValue = "4";
                }

                txtImporte.Text = "";

            }
            catch (Exception ex)
            {

                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        public void Proyeccion_Sectorista(int idProyectado, string periodo, int id_semana, string tablaOrigen, int opOrigen, int estado)
        {
            try
            {
                Acumulado = 0;

                CobranzasWCFClient objCobranzaWCF = new CobranzasWCFClient();
                List<ProyectadoCobranza_ListarResult> lstProyeccion = new List<ProyectadoCobranza_ListarResult>();

                lstProyeccion = objCobranzaWCF.ProyectadoCobranza_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                                                                            idProyectado, periodo, id_semana, tablaOrigen, opOrigen, estado).ToList();


                foreach (ProyectadoCobranza_ListarResult obj in lstProyeccion)
                {
                    Acumulado = Acumulado + (float)obj.Importe;
                }

                Maximno = (float)ViewState["Maximno"];
                Saldo = Maximno - Acumulado;

                ViewState["Acumulado"] = Acumulado;
                ViewState["Saldo"] = Saldo;

                lblMaximo.Text = "Pendiente de proyectar: " + string.Format("{0:#,##0.##}", Convert.ToDouble(Saldo.ToString()));
                lblMaximo.CssClass = "mensajeExito";

                grdCobranza.DataSource = lstProyeccion;
                grdCobranza.DataBind();
                ViewState["lstProyeccion"] = JsonHelper.JsonSerializer(lstProyeccion);

                if (lstProyeccion.Count > 0)
                {
                    lblGrilla.Value = "1";
                }
                else
                {
                    lblGrilla.Value = "0";
                }

                lblMensaje.Text = "Listo para mostrar planificación de sectorista";
                lblMensaje.CssClass = "mensajeExito";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }

        }

        protected void grdCobranza_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (lblGrilla.Value == "1")
                {
                    grdCobranza.DataSource = JsonHelper.JsonDeserialize<List<ProyectadoCobranza_ListarResult>>((string)ViewState["lstProyeccion"]);
                    grdCobranza.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdCobranza_selectedindexchanged(object sender, EventArgs e)
        {
            float importe = 0;

            try
            {
                Maximno = (float)ViewState["Maximno"];
                Acumulado = (float)ViewState["Acumulado"];
                Saldo = Maximno - Acumulado;
                ViewState["Saldo"] = Saldo;

                GridDataItem gridItem = (GridDataItem)grdCobranza.SelectedItems[0];
                string id_semana = gridItem["id_semana"].Text;
                string strImporte = gridItem["importe"].Text;

                importe = (float)Convert.ToDouble(strImporte);
                Saldo = (float)ViewState["Saldo"];

                Saldo = Saldo + importe;
                ViewState["Saldo"] = Saldo;

                txtImporte.Text = importe.ToString();

                txtImporte.Enabled = true;
                btnAgregar.Enabled = true;

                cboSemana.SelectedValue = id_semana;
                ViewState["id_semana"] = id_semana;

                lblMensaje.Text = "Listo para mostrar planificación de sectorista";
                lblMensaje.CssClass = "mensajeExito";




            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        protected void grdCobranza_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            if (!ConnectionHelpers.CheckForInternetConnection())
                throw new ArgumentException("ERROR: Revisar su conexión a internet.");

            string mensaje = "";
            lblMensaje.Text = ""; 

            try
            {
                if (e.CommandName == "Eliminar")
                {

                    if (ValidarValores(ref mensaje) == false)
                    {

                        int id_proyectado = 0;
                        id_proyectado = int.Parse(e.CommandArgument.ToString());

                        string obj = Request.QueryString["objDocumento"];

                        char delimiter = '}';
                        string[] arryString = Request.QueryString["objDocumento"].Split(delimiter);
                        arryString[0] = arryString[0] + "}";
                        arryString[1] = arryString[1].Substring(1, 6);
                        string fecha = arryString[1];

                        gsReporte_EstadoCuenta_BIResult objDocumento = JsonHelper.JsonDeserialize<gsReporte_EstadoCuenta_BIResult>(arryString[0]);


                        CobranzasWCFClient objCobranzaWCF = new CobranzasWCFClient();
                        objCobranzaWCF.ProyectadoCobranza_Eliminar(
                            ((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, id_proyectado);


                        Proyeccion_Sectorista(0, fecha, 0, objDocumento.Origen, (int)objDocumento.OrigenOp, 1);

                        lblMensaje.Text = "Se eliminó el registro.";
                        lblMensaje.CssClass = "mensajeExito";
                    }
                    else
                    {
                        lblMensaje.Text = mensaje; 
                        lblMensaje.CssClass = "mensajeError";
                    }


                }
            }
            catch (Exception ex)
            {
                
                lblMensaje.Text = ex.Message; 
                lblMensaje.CssClass = "mensajeError";

            }
        }
    }
}