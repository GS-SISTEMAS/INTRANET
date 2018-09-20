using GS.SISGEGS.DM;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.FacturaElectronica2WCF;
using GS.SISGEGS.Web.ReversionWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.EmpresaWCF;
using System.Drawing;
using xi = Telerik.Web.UI.ExportInfrastructure;
using Telerik.Web.UI.GridExcelBuilder;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Web.UI.HtmlControls;
using GS.SISGEGS.Web.FacturacionElectronicaOkWCF;

using GS.SISGEGS.Web.RetencionesDevWCF;

namespace GS.SISGEGS.Web.FacturacionElectronica 
{
    public partial class frmFacturaElectronica_Ret : System.Web.UI.Page
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

                    dpInicio.SelectedDate = DateTime.Now;
                    dpFinal.SelectedDate = DateTime.Now;
                    lblDate.Text = "";
                    dpInicio.SelectedDate = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
                    dpFinal.SelectedDate = DateTime.Now;
                    TipoDocumento_Cargar();
                }
            }

            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        private void ListarFacturaElectronica(string codAgenda,  DateTime fechaInicial, DateTime fechaFinal, int TipoDoc)
        {
            FacturaElectronica2WCFClient objFacturaWCF = new FacturaElectronica2WCFClient();

            try
            {
                //List<VBG00946_ElectronicaResult> lst = objFacturaWCF.Retenciones_Electronicas_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, 0, TipoDoc, codAgenda, fechaInicial, fechaFinal).ToList();
                List<VBG04694Result> lst = objFacturaWCF.FacturaElectronica_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, fechaInicial, fechaFinal, codAgenda, null, 0, TipoDoc, 0, null, 0).ToList().FindAll(x => x.TablaOrigen == "tblRetencionIGV");
                ViewState["lstRetencionElectronica"] = JsonHelper.JsonSerializer(lst);
                grdFacturaElectronica.DataSource = lst;
                grdFacturaElectronica.DataBind();
                lblDate.Text = "1";
    
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se encontrarón resultados.";
                lblMensaje.CssClass = "mensajeError";
            }
        }
        private void BuscarFechas_ListarFacturaElectronica()
        {
            DateTime fecha1;
            DateTime fecha2;
            string cliente;
            int TipoDoc;
            string item;

            item = "";
            cliente = "";
            TipoDoc = 0;

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (Validar_Variables() == 0)
                {
                    fecha1 = dpInicio.SelectedDate.Value;
                    fecha2 = dpFinal.SelectedDate.Value;
                    //fecha2 = dpInicio.SelectedDate.Value;

                    if (acbCliente == null || acbCliente.Text.Split('-')[0] == "" || acbCliente.Text == "")
                    {
                        cliente = null;
                    }
                    else { cliente = acbCliente.Text.Split('-')[0]; }

                    //if (cboTipoDoc == null || cboTipoDoc.SelectedValue == "" || cboTipoDoc.SelectedIndex == 0)
                    //{
                    //    TipoDoc = 0;
                    //}
                    //else { TipoDoc = Convert.ToInt32(cboTipoDoc.SelectedValue.ToString()); }

                    TipoDoc = 0;
                    ListarFacturaElectronica(cliente, fecha1, fecha2, TipoDoc);
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }

        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensajeFecha.Text = "";
                BuscarFechas_ListarFacturaElectronica();
            }

            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        public int Validar_Variables()
        {
            int dias;
            DateTime inicial = new DateTime();
            DateTime final = new DateTime() ;

            int valor = 0;

            if ( dpInicio == null || dpInicio.SelectedDate.Value.ToString() == "")
            {
                valor = 1;
                lblMensajeFecha.Text = lblMensaje.Text + "Seleccionar fecha final de emisión. ";
                lblMensajeFecha.CssClass = "mensajeError";
              
            }
            else
            { inicial = dpInicio.SelectedDate.Value; }
            if (dpFinal == null || dpFinal.SelectedDate.Value.ToString() == "")
            {
                valor = 1;
                lblMensajeFecha.Text = lblMensaje.Text + "Seleccionar fecha final de emisión. ";
                lblMensajeFecha.CssClass = "mensajeError";
                
            }
            else
            { final = dpFinal.SelectedDate.Value; }
            
           if(valor == 0)
            {
                dias = final.Subtract(inicial).Days;
                if( dias < 0)
                {
                    valor = 1;
                    lblMensajeFecha.Text = lblMensaje.Text + "Fecha final debe ser igual o mayor que fecha inicial. ";
                    lblMensajeFecha.CssClass = "mensajeError";
                  
                }

                if (dias > 30)
                {
                    valor = 1;
                    lblMensajeFecha.Text = lblMensaje.Text + "El rango de fechas, no puede superar los 30 días. ";
                    lblMensajeFecha.CssClass = "mensajeError";
                   
                }
            }

            //if (cboTipoDoc == null || cboTipoDoc.SelectedValue == "" || cboTipoDoc.SelectedIndex == 0)
            //{
            //    valor = 1;
            //    lblMensajeFecha.Text = lblMensaje.Text + "Debe seleccionar un Tipo de documento. ";
            //    lblMensajeFecha.CssClass = "mensajeError";
              
            //}

            return valor;
        }

        #region Métodos web
        [WebMethod]
        public static AutoCompleteBoxData Agenda_BuscarCliente(object context)
        {
            AutoCompleteBoxData res = new AutoCompleteBoxData();
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            if (searchString.Length > 2)
            {
                AgendaWCFClient objAgendaWCFClient = new AgendaWCFClient();
                gsAgenda_ListarClienteResult[] lst = objAgendaWCFClient.Agenda_ListarCliente(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, searchString, 1);
                List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

                foreach (gsAgenda_ListarClienteResult agenda in lst)
                {
                    AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                    childNode.Text = agenda.ID_Agenda + "-" + agenda.Nombre;
                    childNode.Value = agenda.ID_Agenda;
                    result.Add(childNode);
                }
                res.Items = result.ToArray();
            }
            return res;
        }
        #endregion

        protected void grdFacturaElectronica_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (lblDate.Text == "1")
                {
                    List<VBG04694Result> lst = JsonHelper.JsonDeserialize<List<VBG04694Result>>((string)ViewState["lstRetencionElectronica"]);
                    grdFacturaElectronica.DataSource = lst;
                    //grdFacturaElectronica.Rebind();

                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message.ToString() + " Need,  No se encontrarón resultados.";
                lblMensaje.CssClass = "mensajeError";
            }
        }
        private void TipoDocumento_Cargar()
        {
            try
            {
                FacturaElectronica2WCFClient objFacturaWCF = new FacturaElectronica2WCFClient();
                gsComboDocElectronicoResult objTipoDoc = new gsComboDocElectronicoResult();

                List<gsComboDocElectronicoResult> lst = objFacturaWCF.ComboDocElectronico(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();


                lst.Insert(0, objTipoDoc);
                objTipoDoc.Nombre = "Todos";
                objTipoDoc.ID_Documento = 0;

                //cboTipoDoc.DataSource = lst;
                //cboTipoDoc.DataTextField = "Nombre";
                //cboTipoDoc.DataValueField = "ID_Documento";
                //cboTipoDoc.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void grdFacturaElectronica_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    GridDataItem dataitem = (GridDataItem)e.Item;

                    CheckBox chkbx = (CheckBox)item["Ok_XML"].Controls[0];
                    TableCell cell = dataitem["CheckColumn"];
                    CheckBox checkBox = (CheckBox)cell.Controls[0].FindControl("Check");

                    if (chkbx.Checked == true)
                    {
                        checkBox.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        protected void grdFacturaElectronica_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if (e.CommandName == "Editar")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowInsertForm(" + e.CommandArgument + ");", true);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;

            }
        }
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx"); 
            try
            {
                foreach (GridItem rowitem in grdFacturaElectronica.MasterTableView.Items)
                {
                    GridDataItem dataitem = (GridDataItem)rowitem;
                    TableCell cell = dataitem["CheckColumn"];
                    CheckBox checkBox = (CheckBox)cell.Controls[0].FindControl("Check");
                    if (checkBox.Checked)
                    {
                        int Op = Convert.ToInt32(dataitem.GetDataKeyValue("Op").ToString());
                        //string Serie = dataitem.GetDataKeyValue("Serie").ToString(); // dataitem["Serie"].Text;
                        string TablaOrigen = dataitem.GetDataKeyValue("Origen").ToString();  //dataitem["Origen"].Text;
                        //string Numero = dataitem.GetDataKeyValue("Numero").ToString();  //dataitem["Numero"].Text;

                        EnviarRetencionElectronica(Op, TablaOrigen); 

                    }   
                }
                BuscarFechas_ListarFacturaElectronica();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnReversar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");
            try
            {
                foreach (GridItem rowitem in grdFacturaElectronica.MasterTableView.Items)
                {
                    GridDataItem dataitem = (GridDataItem)rowitem;
                    TableCell cell = dataitem["CheckColumn"];
                    CheckBox checkBox = (CheckBox)cell.Controls[0].FindControl("Check");
                    if (checkBox.Checked)
                    {
                        int Op = Convert.ToInt32(dataitem.GetDataKeyValue("Op").ToString());
                        //string Serie = dataitem.GetDataKeyValue("Serie").ToString(); // dataitem["Serie"].Text;
                        string TablaOrigen = dataitem.GetDataKeyValue("Origen").ToString();  //dataitem["Origen"].Text;
                        //string Numero = dataitem.GetDataKeyValue("Numero").ToString();  //dataitem["Numero"].Text;

                        EnviarReversaRetencion(Op, TablaOrigen);

                    }
                }
                BuscarFechas_ListarFacturaElectronica();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        void RegistrarEstado( RetencionesDevWCF.ens_Respuesta result, string TablaOrigen, int Op)
        {
            FacturaElectronica2WCFClient objElectronicoWCF = new FacturaElectronica2WCFClient();

            try
            {
                if (result.at_NivelResultado == true)
                {

                    objElectronicoWCF.RetencionElectronica_Update(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, TablaOrigen, Op, result.at_MensajeResultado, 0);
                    lblMensaje.Text = "Comprobante Generado Correctamente - " + result.ToString();
                    lblMensaje.CssClass = "mensajeExito";
                }
                else
                {
                    objElectronicoWCF.RetencionElectronica_Update(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, TablaOrigen, Op, result.at_MensajeResultado, 1);
                    lblMensaje.Text = result.ToString();
                    lblMensaje.CssClass = "mensajeError";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        string ConvertirSerie(int id_tipoDocRef, string Serie)
        {
            string SerieConv = "";


            if (id_tipoDocRef == 1)
            {
                if (Serie.Length > 3)
                {
                    SerieConv = "F" + Serie.Substring(1, 3);

                }
                else
                {
                    SerieConv = "F" + Serie.ToString();
                }
            }
            else if (id_tipoDocRef == 3)
            {
                if (Serie.Length > 3)
                {
                    SerieConv = "B" + Serie.Substring(1, 3);
                }
                else
                {
                    SerieConv = "B" + Serie.ToString();
                }
            }

            return SerieConv;
        }

        void EnviarRetencionElectronica(int Op, string TablaOrigen)
        {
            RetencionesDevWCF.ens_Respuesta result = new RetencionesDevWCF.ens_Respuesta();
            try
            {
                FacturacionElectronicaOkWCF.ENErrorComunicacion[] ListaError = new FacturacionElectronicaOkWCF.ENErrorComunicacion[0];
                ListaError = null;

                result = LlenarComprobanteRetencion( Op );

                RegistrarEstado(result, TablaOrigen, Op);

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }


        void EnviarReversaRetencion(int Op, string TablaOrigen)
        {
            ReversionWCF.ens_Respuesta result = new ReversionWCF.ens_Respuesta();
            try
            {
                FacturacionElectronicaOkWCF.ENErrorComunicacion[] ListaError = new FacturacionElectronicaOkWCF.ENErrorComunicacion[0];
                ListaError = null;

                result = LlenarComprobanteReversion(Op);

                //RegistrarEstado(result, TablaOrigen, Op);

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }


        en_EmisorRetencion LlenarEmisor()
        {
            en_EmisorRetencion objEmisor = new en_EmisorRetencion(); 

            EmpresaWCFClient objEmpresaWCFC = new EmpresaWCFClient();
            AgendaWCFClient objAgendaWCF = new AgendaWCFClient();
            Agenda_BuscarEmpresaResult objEmpresaGenesys = new Agenda_BuscarEmpresaResult();
            List<Agenda_BuscarEmpresaResult> lstEmpresaGenesys = new List<Agenda_BuscarEmpresaResult>();
            List<Empresa_ComboBoxResult> lstComboGenesys = new List<Empresa_ComboBoxResult>();
            int idEmpresa;
            string id_agenda = "";
            try
            {
                idEmpresa = ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa;
                lstComboGenesys = objEmpresaWCFC.Empresa_ComboBox().ToList();

                var query_Empresa = from c in lstComboGenesys
                                    where c.idEmpresa == idEmpresa
                                    select new
                                    {
                                        c.RUC
                                    };
                var QueryRUC = from cust in query_Empresa
                               select cust.RUC;

                foreach (string s in QueryRUC)
                {
                    id_agenda = s;
                }

                lstEmpresaGenesys = objAgendaWCF.Agenda_BuscarEmpresa(idEmpresa, ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, id_agenda).ToList();
                objEmpresaGenesys = lstEmpresaGenesys[0];

                // Llenar WS 

                objEmisor.at_RazonSocial = objEmpresaGenesys.AgendaNombre.ToString();
                objEmisor.at_NombreComercial = objEmpresaGenesys.AgendaNombre.ToString();
                objEmisor.at_NumeroDocumentoIdentidad = objEmpresaGenesys.RUC.ToString();
                objEmisor.at_CorreoContacto = objEmpresaGenesys.Cliente_Correo;
                objEmisor.at_Telefono = "6173300";
                objEmisor.at_SitioWeb = "www.silvestre.com.pe";

                en_DireccionFiscalEmisorRetencion direccion = new en_DireccionFiscalEmisorRetencion();
                direccion.at_CodigoPais = "PE";
                direccion.at_Departamento = objEmpresaGenesys.DepartamentoNombre.ToString();
                direccion.at_Provincia = objEmpresaGenesys.ProvinciaNombre.ToString();
                direccion.at_Distrito = objEmpresaGenesys.DistritoNombre.ToString();
                //direccion.at_Urbanizacion = "";
                direccion.at_DireccionDetallada = objEmpresaGenesys.TipoCalleNombre.ToString() + " " + objEmpresaGenesys.Direccion.ToString();
                direccion.at_Ubigeo = objEmpresaGenesys.CodigoSunat.ToString();

                objEmisor.ent_DireccionFiscal = direccion;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objEmisor;
        }

        en_Emisor LlenarEmisorRev()
        {
            en_Emisor objEmisor = new en_Emisor();

            EmpresaWCFClient objEmpresaWCFC = new EmpresaWCFClient();
            AgendaWCFClient objAgendaWCF = new AgendaWCFClient();
            Agenda_BuscarEmpresaResult objEmpresaGenesys = new Agenda_BuscarEmpresaResult();
            List<Agenda_BuscarEmpresaResult> lstEmpresaGenesys = new List<Agenda_BuscarEmpresaResult>();
            List<Empresa_ComboBoxResult> lstComboGenesys = new List<Empresa_ComboBoxResult>();
            int idEmpresa;
            string id_agenda = "";
            try
            {
                idEmpresa = ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa;
                lstComboGenesys = objEmpresaWCFC.Empresa_ComboBox().ToList();

                var query_Empresa = from c in lstComboGenesys
                                    where c.idEmpresa == idEmpresa
                                    select new
                                    {
                                        c.RUC
                                    };
                var QueryRUC = from cust in query_Empresa
                               select cust.RUC;

                foreach (string s in QueryRUC)
                {
                    id_agenda = s;
                }

                lstEmpresaGenesys = objAgendaWCF.Agenda_BuscarEmpresa(idEmpresa, ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario, id_agenda).ToList();
                objEmpresaGenesys = lstEmpresaGenesys[0];

                // Llenar WS 

                objEmisor.at_RazonSocial = objEmpresaGenesys.AgendaNombre.ToString();
                objEmisor.at_NumeroDocumentoIdentidad = objEmpresaGenesys.RUC.ToString();

            

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objEmisor;
        }


        en_ReceptorRetencion LlenarReceptor(VBG00946_CABECERAResult objRetencionCabecera)
        {
            en_ReceptorRetencion objReceptorRetencion = new en_ReceptorRetencion();

            try
            {
                objReceptorRetencion.at_NombreComercial = objRetencionCabecera.Cliente_RazonSocial.ToString(); //  "Empresa SAC";
                objReceptorRetencion.at_NumeroDocumentoIdentidad = objRetencionCabecera.Cliente_NumeroDocIdentidad.ToString(); // "5435435";
                objReceptorRetencion.at_RazonSocial = objRetencionCabecera.Cliente_RazonSocial.ToString(); // "Empresa Razón";
                objReceptorRetencion.at_TipoDocumentoIdentidad = "6";

                en_CorreoRetencion objCorreo = new en_CorreoRetencion();
                objCorreo.at_CorreoPrincipal = objRetencionCabecera.Cliente_Correo.ToString(); // "CorreoPrincipal@gmail.com";
                //-----------------------------------------------------
                ArrayOfString objCorreoArray = new ArrayOfString();
              
                string Correo = objRetencionCabecera.Cliente_Correo.ToString(); //  "CorreoSecundario@gmail.com";
                objCorreoArray.Add(Correo);
                //-----------------------------------------------------
                objCorreo.at_CorreosSecundarios = objCorreoArray;

                objReceptorRetencion.ent_CorreoRetencion = objCorreo;

                en_DireccionFiscalReceptorRetencion DireccionReceptor = new en_DireccionFiscalReceptorRetencion();
                DireccionReceptor.at_CodigoPais = "PE";
                DireccionReceptor.at_Departamento = objRetencionCabecera.Cliente_Departamento.ToString(); // "";
                DireccionReceptor.at_Provincia = objRetencionCabecera.Cliente_Provincia.ToString(); // "";
                DireccionReceptor.at_Distrito = objRetencionCabecera.Cliente_Distrito.ToString(); // "";
                //DireccionReceptor.at_Urbanizacion = objRetencionCabecera.Cliente_Urbanizacion.ToString(); // "";
                DireccionReceptor.at_DireccionDetallada = objRetencionCabecera.Cliente_Direccion.ToString(); // "";
                DireccionReceptor.at_Ubigeo = objRetencionCabecera.Cliente_Ubigeo.ToString(); // "54616";

                objReceptorRetencion.ent_DireccionFiscal = DireccionReceptor;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objReceptorRetencion;
        }

        DataTable crearTabla()
        {
            DataTable workTable = new DataTable();
            workTable.Columns.Add("Id_doc", typeof(String));
            workTable.Columns.Add("TipoDoc", typeof(String));
            workTable.Columns.Add("TablaOrigen", typeof(String));
            workTable.Columns.Add("OpCompra", typeof(string));
            workTable.Columns.Add("ImporteRetenido", typeof(Decimal));
            workTable.Columns.Add("MontoTotal", typeof(Decimal));

            workTable.Columns.Add("OK", typeof(int));

            return workTable; 
        }

        en_DatosRetencion LlenarDatosRetencion(List<VBG00946_CABECERAResult> lst )
        {
            VBG00946_CABECERAResult Cabecera = new VBG00946_CABECERAResult();
            en_DatosRetencion objDatosRetencion = new en_DatosRetencion();
            decimal at_ImporteTotalPagado = 0;
            decimal at_ImporteTotalRetencion = 0; 
            try
            {
                Cabecera = lst[0];

             
                objDatosRetencion.at_Observaciones = Cabecera.Observaciones.ToString(); // "Observación";
                objDatosRetencion.at_RegimenRetencion = "01";
                objDatosRetencion.at_TasaRetencion = decimal.Round(decimal.Parse(Cabecera.Tasa.ToString()),2);
            
                en_CabeceraRetencion CabeceraRetencion = new en_CabeceraRetencion();
                CabeceraRetencion.at_FechaEmision = Cabecera.FechaEmision.ToString(); //  DateTime.Now.ToShortDateString();

                if(txtNumero.Text.Length > 0 )
                {
                    CabeceraRetencion.at_Numero = int.Parse(txtNumero.Text.ToString()); // 54545;
                }
                else
                {
                    CabeceraRetencion.at_Numero = int.Parse(Cabecera.NumeroCabecera.ToString()); // 54545;
                }
                

                string SerieCab = "";
                string SerieC = Cabecera.SerieCabecera.ToString();

                if (SerieC.Length > 3)
                {
                    SerieCab = "R" + SerieC.Substring(1, SerieC.Length - 1 );
                }
                else
                {
                    SerieCab = "R" + SerieC.ToString().PadLeft(3, char.Parse("0"));
                }

                CabeceraRetencion.at_Serie = SerieCab;  // "0054";

                objDatosRetencion.ent_CabeceraRetencion = CabeceraRetencion;

                ArrayOfEn_DatosComprobanteRelacionadoRetencion objDatosArray = new ArrayOfEn_DatosComprobanteRelacionadoRetencion();
                ArrayOfEn_DatosComprobanteRelacionadoRetencion objDatosArrayNC = new ArrayOfEn_DatosComprobanteRelacionadoRetencion();
                ArrayOfEn_DatosComprobanteRelacionadoRetencion objDatosArrayDoc = new ArrayOfEn_DatosComprobanteRelacionadoRetencion();

                int NumeroPago = 1 ;
                int correNC = 0;
                int correDOC = 0;
                int count;
                DataTable dtTablaNC;
                DataTable dtTablaDOC;

                count = lst.Count;
                int y = 0;

                dtTablaNC = crearTabla();
                dtTablaDOC = crearTabla(); 

                if(txtReversa.Text.Length > 0)
                { NumeroPago = int.Parse(txtReversa.Text); }
                else
                { NumeroPago = 1; }


                foreach (VBG00946_CABECERAResult objRetencion in lst)
                {
                   
                    string tipodoc = objRetencion.DocSunatDetalle.ToString();
                    if (tipodoc == "7")
                    {
                        DataRow dr = dtTablaNC.NewRow();
                        correNC = correNC + 1;
                        dr["Id_doc"] = objRetencion.id_doc.ToString();
                        dr["TablaOrigen"] = objRetencion.TablaOrigen.ToString();
                        dr["OpCompra"] = objRetencion.OpCompra.ToString();
                        dr["ImporteRetenido"] = decimal.Round(decimal.Parse(objRetencion.ImporteretencionTC.ToString()), 2);
                        dr["MontoTotal"] = decimal.Round(decimal.Parse(objRetencion.ImporteTC.ToString()), 2);
                        dr["TipoDoc"] = objRetencion.DocSunatDetalle;
                        dr["OK"] = 0; 
                        dtTablaNC.Rows.Add(dr);
                        at_ImporteTotalRetencion = at_ImporteTotalRetencion - decimal.Parse(objRetencion.ImporteretencionTC.ToString());
                        at_ImporteTotalPagado = at_ImporteTotalPagado - decimal.Parse(objRetencion.ImporteTC.ToString());
                    }
                    else
                    {
                        DataRow dr = dtTablaDOC.NewRow();
                        correDOC = correDOC + 1;
                        dr["Id_doc"] = objRetencion.id_doc.ToString();
                        dr["TablaOrigen"] = objRetencion.TablaOrigen.ToString();
                        dr["OpCompra"] = objRetencion.OpCompra.ToString();
                        dr["ImporteRetenido"] = decimal.Round(decimal.Parse(objRetencion.ImporteretencionTC.ToString()), 2);
                        dr["MontoTotal"] = decimal.Round(decimal.Parse(objRetencion.ImporteTC.ToString()), 2);
                        dr["TipoDoc"] = objRetencion.DocSunatDetalle;
                        dr["OK"] = 0;
                        dtTablaDOC.Rows.Add(dr);
                        at_ImporteTotalRetencion = at_ImporteTotalRetencion + decimal.Parse(objRetencion.ImporteretencionTC.ToString());
                        at_ImporteTotalPagado = at_ImporteTotalPagado + decimal.Parse(objRetencion.ImporteTC.ToString());
                    }
                    

                }

                if (correNC > 0 )
                {
                    foreach (DataRow row in dtTablaNC.Rows)
                    {
                        
                        foreach (VBG00946_CABECERAResult objRetencionNC in lst)
                        {
                            en_DatosComprobanteRelacionadoRetencion objDatosNC = new en_DatosComprobanteRelacionadoRetencion();

                            if (row["Id_doc"].ToString() == objRetencionNC.id_doc.ToString())
                            {
                                int contNCDOC = 0;
                                foreach (DataRow rowDOC in dtTablaDOC.Rows)
                                {
                                    string OKDOC = rowDOC["OK"].ToString(); 

                                    if( OKDOC == "0")
                                    {
                                        foreach (VBG00946_CABECERAResult objRetencionDOC in lst)
                                        {
                                            en_DatosComprobanteRelacionadoRetencion objDatosDOC = new en_DatosComprobanteRelacionadoRetencion();
                                            if (rowDOC["Id_doc"].ToString() == objRetencionDOC.id_doc.ToString())
                                            {
                                                decimal montoNC = Convert.ToDecimal(row["MontoTotal"].ToString());
                                                decimal montoDOC = Convert.ToDecimal(rowDOC["MontoTotal"].ToString());
                                                string tipoSunat = objRetencionDOC.DocSunatDetalle.ToString();

                                                if ((tipoSunat == "1" && contNCDOC == 0) && (montoNC < montoDOC))
                                                {
                                                    decimal ImporteFactura = 0;
                                                    decimal ImportePago = 0; 

                                                    objDatosDOC = datosArreglo(objRetencionDOC, Cabecera.DocSunatDetalle.ToString(), Cabecera.SerieCabecera, NumeroPago);
                                                    objDatosDOC.ent_DatosRetenidos.ent_InformeRetencion.at_ImporteRetenido = decimal.Round(decimal.Parse(objRetencionDOC.ImporteretencionTC.ToString()), 2) - decimal.Round(decimal.Parse(objRetencionNC.ImporteretencionTC.ToString()), 2);
                                                    objDatosDOC.ent_DatosRetenidos.ent_InformeRetencion.at_MontoTotal = decimal.Round(decimal.Parse(objRetencionDOC.ImporteTC.ToString()), 2) - decimal.Round(decimal.Parse(objRetencionNC.ImporteTC.ToString()), 2);
                                                    objDatosDOC.ent_DatosRetenidos.at_ImporteSinRetencion = decimal.Round(decimal.Parse(objRetencionDOC.ImportePago.ToString()), 2) - decimal.Round(decimal.Parse(objRetencionNC.ImportePago.ToString()), 2);

                                                    //ImportePago = decimal.Round(decimal.Parse(objRetencionDOC.ImportePago.ToString()), 2); 
                                                    //ImporteFactura = objDatosDOC.at_ImporteTotal;

                                                    //if (ImporteFactura > ImportePago)
                                                    //{ objDatosDOC.at_Numero = "2";  }
                                                    //else
                                                    //{ objDatosDOC.at_Numero = "1"; }

                                                    contNCDOC = contNCDOC + 1;
                                                    rowDOC["OK"] = 1;
                                                    objDatosArray.Add(objDatosDOC);
                                                }
                                            }
                                        }
                                    }


                                 }


                                objDatosNC = datosArregloNC(objRetencionNC, Cabecera.DocSunatDetalle.ToString(), Cabecera.SerieCabecera, NumeroPago);
                                row["OK"] = 1;
                                objDatosArray.Add(objDatosNC);
                            }
                        }
                       
                    }

                    foreach (DataRow row in dtTablaDOC.Rows)
                    {
                        foreach (VBG00946_CABECERAResult objRetencionDOC in lst)
                        {
                            en_DatosComprobanteRelacionadoRetencion objDatosDOC = new en_DatosComprobanteRelacionadoRetencion();
                            string OKDoc = row["OK"].ToString(); 

                            if (row["Id_doc"].ToString() == objRetencionDOC.id_doc.ToString() && OKDoc == "0")
                            {
                              
                                objDatosDOC = datosArreglo(objRetencionDOC, Cabecera.DocSunatDetalle.ToString(), Cabecera.SerieCabecera, NumeroPago);
                                objDatosArray.Add(objDatosDOC);
                            }

                        }
                     }



                }
                else
                {

                    foreach (VBG00946_CABECERAResult objRetencionDOC in lst)
                    {

                        en_DatosComprobanteRelacionadoRetencion objDatosDOC = new en_DatosComprobanteRelacionadoRetencion();
                        objDatosDOC = datosArreglo(objRetencionDOC, Cabecera.DocSunatDetalle.ToString(), Cabecera.SerieCabecera, NumeroPago);
                        objDatosArray.Add(objDatosDOC);
                    }
                }

                objDatosRetencion.l_DatosComprobanteRelacionado = objDatosArray;
                objDatosRetencion.at_ImporteTotalPagado = decimal.Round(at_ImporteTotalPagado, 2); // 20;
                objDatosRetencion.at_ImporteTotalRetencion = decimal.Round(at_ImporteTotalRetencion, 2); // 566;


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objDatosRetencion;
        }

        en_ComprobantesRevertidos LlenarDatosReversion(List<VBG00946_CABECERAResult> lst)
        {
            VBG00946_CABECERAResult Cabecera = new VBG00946_CABECERAResult();
            en_ComprobantesRevertidos objComprobantes = new en_ComprobantesRevertidos();

            try
            {
                Cabecera = lst[0];

                if(txtNumero.Text.Length > 0)
                { objComprobantes.at_Numero = int.Parse(txtNumero.Text.ToString()); }
                else
                { objComprobantes.at_Numero = int.Parse(Cabecera.NumeroCabecera.ToString()); }

                string SerieCab = "";
                string SerieC = Cabecera.SerieCabecera.ToString();

                if (SerieC.Length > 3)
                { SerieCab = "R" + SerieC.Substring(1, SerieC.Length - 1); }
                else
                { SerieCab = "R" + SerieC.ToString().PadLeft(3, char.Parse("0")); }

                objComprobantes.at_Serie = SerieCab;
                objComprobantes.at_TipoComprobante = "20";
                objComprobantes.at_MotivoReversion = Cabecera.Observaciones.ToUpper(); 

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objComprobantes;
        }

        en_DatosComprobanteRelacionadoRetencion datosArreglo(VBG00946_CABECERAResult objRetencion, string DocSunatDetalle, string SerieCabecera , int Correlativo)
        {
                //en_DatosComprobanteRelacionadoRetencion dtcompro = new en_DatosComprobanteRelacionadoRetencion();

                en_DatosComprobanteRelacionadoRetencion objDatos = new en_DatosComprobanteRelacionadoRetencion();
                objDatos.at_FechaEmision = objRetencion.FechaDetalle; // DateTime.Now.ToShortDateString();

            // Revisar Cambio 
            objDatos.at_ImporteTotal = decimal.Round(decimal.Parse(objRetencion.ImporteDOC.ToString()), 2); //  365;

            //objDatos.at_ImporteTotal = decimal.Round(decimal.Parse(objRetencion.ImportePago.ToString()), 2); //  365;



            objDatos.at_Numero = objRetencion.NumeroDetalle.ToString(); // 235345.ToString();

                decimal TipoDoc = decimal.Parse(DocSunatDetalle.ToString());  //decimal.Parse(Cabecera.DocSunatDetalle.ToString());
                string Letra = "F";
                string SerieF = "";
                string Serie = SerieCabecera.ToString(); // Cabecera.SerieCabecera.ToString();


                if (Serie.Length > 3)
                {
                    SerieF = Letra + Serie.Substring(1, Serie.Length - 1);
                }
                else
                {
                    SerieF = Serie.ToString().PadLeft(4, char.Parse("0"));
                }

                objDatos.at_Serie = SerieF;  // objRetencion.SerieDetalle.ToString(); // "544";
                objDatos.at_TipoComprobante = objRetencion.DocSunatDetalle.ToString().PadLeft(2, char.Parse("0")); // "1";
                objDatos.at_TipoMoneda = objRetencion.TipoMoneda;


                en_DatosRetenidos objRetenidos = new en_DatosRetenidos();
                objRetenidos.at_Fecha = objRetencion.FechaEmision.ToString(); // DateTime.Now.ToShortDateString();
                objRetenidos.at_ImporteSinRetencion = decimal.Round(decimal.Parse(objRetencion.ImportePago.ToString()), 2); //  2321;


              
                objRetenidos.at_TipoMoneda = objRetencion.MonedaRetencion;

                en_TipoCambioRetencion TC = new en_TipoCambioRetencion();
                TC.at_Fecha = objRetencion.FechaTC;
                TC.at_MontoCambio = decimal.Parse(objRetencion.TC.ToString());
                TC.at_TipoMoneda = objRetencion.TipoMoneda;

                if (objRetencion.MonedaRetencion != "PEN")
                {
                    objRetenidos.ent_TipoCambio = TC;
                }

                en_InformeRetencion objInforme = new en_InformeRetencion();
                objInforme.at_ImporteRetenido = decimal.Round(decimal.Parse(objRetencion.ImporteretencionTC.ToString()), 2);
                objInforme.at_MontoTotal = decimal.Round(decimal.Parse(objRetencion.ImporteTC.ToString()), 2);
                objInforme.at_FechaRetencion = objRetencion.FechaEmision.ToString();

            decimal ImporteFactura = 0;
            decimal ImportePago = 0;

            ImportePago = objRetenidos.at_ImporteSinRetencion;
            ImporteFactura = objDatos.at_ImporteTotal;

            if (ImporteFactura > ImportePago)
            { objRetenidos.at_Numero = 2; }
            else
            { objRetenidos.at_Numero = 1; }
            //objRetenidos.at_Numero = Correlativo;

            objRetenidos.ent_InformeRetencion = objInforme;

                objDatos.ent_DatosRetenidos = objRetenidos;
                //objDatosArray.Add(objDatos);

            return objDatos; 
        }

        en_DatosComprobanteRelacionadoRetencion datosArregloNC(VBG00946_CABECERAResult objRetencion, string DocSunatDetalle, string SerieCabecera, int Correlativo)
        {
            //en_DatosComprobanteRelacionadoRetencion dtcompro = new en_DatosComprobanteRelacionadoRetencion();

            en_DatosComprobanteRelacionadoRetencion objDatos = new en_DatosComprobanteRelacionadoRetencion();
            objDatos.at_FechaEmision = objRetencion.FechaDetalle; // DateTime.Now.ToShortDateString();
            objDatos.at_ImporteTotal = decimal.Round(decimal.Parse(objRetencion.ImportePago.ToString()), 2); //  365;
            objDatos.at_Numero = objRetencion.NumeroDetalle.ToString(); // 235345.ToString();

            decimal TipoDoc = decimal.Parse(DocSunatDetalle.ToString());  //decimal.Parse(Cabecera.DocSunatDetalle.ToString());
            string Letra = "F";
            string SerieF = "";
            string Serie = SerieCabecera.ToString(); // Cabecera.SerieCabecera.ToString();


            if (Serie.Length > 3)
            {
                SerieF = Letra + Serie.Substring(1, Serie.Length - 1);
            }
            else
            {
                SerieF = Serie.ToString().PadLeft(4, char.Parse("0"));
            }

            objDatos.at_Serie = SerieF;  // objRetencion.SerieDetalle.ToString(); // "544";
            objDatos.at_TipoComprobante = objRetencion.DocSunatDetalle.ToString().PadLeft(2, char.Parse("0")); // "1";
            objDatos.at_TipoMoneda = objRetencion.TipoMoneda;


            en_DatosRetenidos objRetenidos = new en_DatosRetenidos();
            objRetenidos.at_Fecha = objRetencion.FechaEmision.ToString(); // DateTime.Now.ToShortDateString();
            objRetenidos.at_ImporteSinRetencion = decimal.Round(decimal.Parse(objRetencion.ImportePago.ToString()), 2); //  2321;


            objRetenidos.at_Numero = Correlativo;
            objRetenidos.at_TipoMoneda = objRetencion.MonedaRetencion;

            en_TipoCambioRetencion TC = new en_TipoCambioRetencion();
            TC.at_Fecha = objRetencion.FechaTC;
            TC.at_MontoCambio = decimal.Parse(objRetencion.TC.ToString());
            TC.at_TipoMoneda = objRetencion.TipoMoneda;

            if (objRetencion.MonedaRetencion != "PEN")
            {
                objRetenidos.ent_TipoCambio = TC;
            }

            en_InformeRetencion objInforme = new en_InformeRetencion();
            //objInforme.at_ImporteRetenido = decimal.Round(decimal.Parse(objRetencion.ImporteretencionTC.ToString()), 2);
            objInforme.at_MontoTotal = decimal.Round(decimal.Parse(objRetencion.ImporteTC.ToString()), 2);
            //objInforme.at_FechaRetencion = objRetencion.FechaEmision.ToString();


            objRetenidos.ent_InformeRetencion = objInforme;

            //objDatos.ent_DatosRetenidos = objRetenidos;
            //objDatosArray.Add(objDatos);

            return objDatos;
        }

        en_InformacionAdicionalesRetencion LlenarInformacionAdicionales(VBG00946_CABECERAResult objRetencion, decimal TotalRetencion)
        {
            en_InformacionAdicionalesRetencion objInformacionAdicionales = new en_InformacionAdicionalesRetencion();
            string strLetrasRetencion = ""; 

            try
            {
                strLetrasRetencion = enletras(TotalRetencion.ToString()); 

                objInformacionAdicionales.at_ImporteTotalRetencionLetras = objRetencion.Leyenda_Descripcion.ToString(); // "65656";
                objInformacionAdicionales.at_ImporteTotalRetencionLetras = strLetrasRetencion; // 

                //objInformacionAdicionales.at_LogoRepresentacionImpresa = "Logo";

                ArrayOfEn_EtiquetasRetencion objEtiquetasArray = new ArrayOfEn_EtiquetasRetencion();

                en_EtiquetasRetencion objEtiquetas = new en_EtiquetasRetencion();
                objEtiquetas.at_Etiqueta = "Cliente_Codigo";  // "";
                objEtiquetas.at_Valor = objRetencion.Cliente_Codigo.ToString(); // "555";
                objEtiquetasArray.Add(objEtiquetas); 

                objInformacionAdicionales.l_Etiquetas = objEtiquetasArray; 

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objInformacionAdicionales;
        }

        RetencionesDevWCF.ens_Respuesta LlenarComprobanteRetencion(int Op)
        {
            FacturaElectronica2WCFClient objFacturaWCF = new FacturaElectronica2WCFClient();
            VBG00946_CABECERAResult objRetencionCabecera = new VBG00946_CABECERAResult();

            ServicioRetencionClient oServicioRet = new ServicioRetencionClient();
            ene_Retencion Retencion = new ene_Retencion();
            en_EmisorRetencion Emisor = new en_EmisorRetencion();
            en_DatosRetencion DatosRetencion = new en_DatosRetencion();
            en_InformacionAdicionalesRetencion InformacionAdicional = new en_InformacionAdicionalesRetencion();
            en_ReceptorRetencion Receptor = new en_ReceptorRetencion();

            ene_ConfirmarRespuesta Respuesta = new ene_ConfirmarRespuesta();

            RetencionesDevWCF.ens_Respuesta result = new RetencionesDevWCF.ens_Respuesta() ;
            try
            {
                List<VBG00946_CABECERAResult> lst = objFacturaWCF.Retenciones_Cabecera_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, Op).ToList();
                objRetencionCabecera = lst[0];

                Emisor = LlenarEmisor();
                Receptor = LlenarReceptor(objRetencionCabecera);
                DatosRetencion = LlenarDatosRetencion(lst);
                InformacionAdicional = LlenarInformacionAdicionales(objRetencionCabecera, DatosRetencion.at_ImporteTotalRetencion);

                Retencion.ent_DatosRetencion = DatosRetencion;
                Retencion.ent_Emisor = Emisor;
                Retencion.ent_InformacionAdicionales = InformacionAdicional;
                Retencion.ent_Receptor = Receptor;

                Retencion.at_ControlOtorgamiento = true;

                result =  oServicioRet.RegistrarComprobanteRetencion(Retencion);
             
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result; 
        }

        ReversionWCF.ens_Respuesta LlenarComprobanteReversion(int Op)
        {
            FacturaElectronica2WCFClient objFacturaWCF = new FacturaElectronica2WCFClient();
            VBG00946_CABECERAResult objRetencionCabecera = new VBG00946_CABECERAResult();
            ServicioReversionesClient oServicioReversion = new ServicioReversionesClient();
            ene_ResumenReversion ResumenRev = new ene_ResumenReversion(); 

            //ene_Retencion Retencion = new ene_Retencion();
            ReversionWCF.en_Emisor Emisor = new en_Emisor();

            en_DatoResumenReversion DatosRevercion = new en_DatoResumenReversion();
            en_CabeceraResumenReversion CabeceraResumenReversion = new en_CabeceraResumenReversion();
            ArrayOfEn_ComprobantesRevertidos lstComprobantes = new ArrayOfEn_ComprobantesRevertidos();
            en_ComprobantesRevertidos oComprobantes = new en_ComprobantesRevertidos(); 


            ReversionWCF.ens_Respuesta result = new ReversionWCF.ens_Respuesta();
            try
            {
                List<VBG00946_CABECERAResult> lst = objFacturaWCF.Retenciones_Cabecera_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, Op).ToList();
                objRetencionCabecera = lst[0];

                Emisor = LlenarEmisorRev();
                ResumenRev.ent_Emisor = Emisor;

                CabeceraResumenReversion.at_FechaComprobante = objRetencionCabecera.FechaEmision;
                CabeceraResumenReversion.at_FechaGeneracion = string.Concat(DateTime.Now.Year, "-" , DateTime.Now.Month,"-" , DateTime.Now.Day );  
                
                if(txtReversa.Text.Length > 0 )
                {  CabeceraResumenReversion.at_IdentificadorUnico = txtReversa.Text;     // Correlativo.  
                }
                else
                {  CabeceraResumenReversion.at_IdentificadorUnico = "RV1001";     // Correlativo. 
                }

                oComprobantes = LlenarDatosReversion(lst);
                lstComprobantes.Add(oComprobantes); 

                CabeceraResumenReversion.l_ComprobantesRevertidos = lstComprobantes; 

                DatosRevercion.ent_CabeceraResumenReversion = CabeceraResumenReversion;
 
                ResumenRev.ent_DatoResumenReversion = DatosRevercion;

                result = oServicioReversion.RegistrarResumenReversion(ResumenRev);
            

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }


        public string enletras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try

            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "/100 NUEVOS SOLES";
            }

            res = toText(Convert.ToDouble(entero)) + dec;
            return res;
        }

        private string toText(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;

        }

    }
}