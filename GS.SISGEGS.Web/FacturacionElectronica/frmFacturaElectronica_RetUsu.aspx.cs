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
    public partial class frmFacturaElectronica_RetUsu : System.Web.UI.Page
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
                List<VBG04694Result> lista = objFacturaWCF.FacturaElectronica_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, fechaInicial, fechaFinal, codAgenda, null, 0, TipoDoc, 0, null, 0).ToList().FindAll(x => x.TablaOrigen == "tblRetencionIGV");
     
                ViewState["lstRetencionElectronica"] = JsonHelper.JsonSerializer(lista);
                grdFacturaElectronica.DataSource = lista;
                grdFacturaElectronica.DataBind();
                lblDate.Text = "1";
    
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message +  " - No se encontrarón resultados.";
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

                    TipoDoc = Convert.ToInt32(cboTipoDoc.SelectedValue.ToString());

                    //TipoDoc = 7000;
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

                List<gsComboDocElectronicoResult> lst = objFacturaWCF.ComboDocElectronico(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList().FindAll(x => x.Nombre.Contains("Retenci"));


                //lst.Insert(0, objTipoDoc);
                //objTipoDoc.Nombre = "Todos";
                //objTipoDoc.ID_Documento = 0;

                cboTipoDoc.DataSource = lst;
                cboTipoDoc.DataTextField = "Nombre";
                cboTipoDoc.DataValueField = "ID_Documento";
                cboTipoDoc.DataBind();
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

                    CheckBox checkBoxOto = (CheckBox)cell.Controls[0].FindControl("CheckOto");

                    if (chkbx.Checked == true)
                    {
                        checkBox.Enabled = false;
                    }
                    checkBoxOto.Checked = false;

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
                        TableCell cellOto = dataitem["CheckColumnOto"];
                        CheckBox checkBoxOto = (CheckBox)cellOto.Controls[0].FindControl("CheckOto");

                        bool Otorgamiento = false;
                        if (checkBoxOto.Checked)
                        {
                            Otorgamiento = true; 
                        }
    
                        int Op = Convert.ToInt32(dataitem.GetDataKeyValue("OpOrigen").ToString());
                        //string Serie = dataitem.GetDataKeyValue("Serie").ToString(); // dataitem["Serie"].Text;
                        string TablaOrigen = dataitem.GetDataKeyValue("TablaOrigen").ToString();  //dataitem["Origen"].Text;
                        //string Numero = dataitem.GetDataKeyValue("Numero").ToString();  //dataitem["Numero"].Text;

                        EnviarRetencionElectronica(Op, TablaOrigen, Otorgamiento); 

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
 
        void EnviarRetencionElectronica(int Op, string TablaOrigen, bool Otorgamiento)
        {
            RetencionesDevWCF.ens_Respuesta result = new RetencionesDevWCF.ens_Respuesta();
            try
            {
                FacturacionElectronicaOkWCF.ENErrorComunicacion[] ListaError = new FacturacionElectronicaOkWCF.ENErrorComunicacion[0];
                ListaError = null;

                result = LlenarComprobanteRetencion( Op, Otorgamiento );

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
                objCorreo.at_CorreosSecundarios = objCorreoArray;
                objReceptorRetencion.ent_CorreoRetencion = objCorreo;

                //-----------------------------------------------------
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

            workTable.Columns.Add("ImporteTotal", typeof(Decimal));

            workTable.Columns.Add("ImporteSinRetencion", typeof(Decimal));
            workTable.Columns.Add("ImporteRetenido", typeof(Decimal));
            workTable.Columns.Add("MontoTotal", typeof(Decimal));

            workTable.Columns.Add("ImporteSinRetencion_P", typeof(Decimal));
            workTable.Columns.Add("ImporteRetenido_P", typeof(Decimal));
            workTable.Columns.Add("MontoTotal_P", typeof(Decimal));

            workTable.Columns.Add("OK", typeof(int));

            return workTable; 
        }

        en_DatosRetencion LlenarDatosRetencion(List<VBG00946_CABECERAResult> lst0 )
        {
            VBG00946_CABECERAResult Cabecera = new VBG00946_CABECERAResult();
            en_DatosRetencion objDatosRetencion = new en_DatosRetencion();
            decimal at_ImporteTotalPagado = 0;
            decimal at_ImporteTotalRetencion = 0;
            List<VBG00946_CABECERAResult> lst1 = new List<VBG00946_CABECERAResult>();
            List<VBG00946_CABECERAResult> lst2 = new List<VBG00946_CABECERAResult>(); 

            try
            {
                Cabecera = lst0[0];

                //-------------------------------------------------------------------------------
                // Parte 1 
                objDatosRetencion.at_RegimenRetencion = "01";
                objDatosRetencion.at_TasaRetencion = decimal.Round(decimal.Parse(Cabecera.Tasa.ToString()), 2);
                //objDatosRetencion.at_Observaciones = Cabecera.Observaciones.ToString(); // "Observación";

                //-------------------------------------------------------------------------------
                // Parte 2
                DateTime fecha0 = DateTime.Now.AddDays(-5);
                //string fecha1 = fecha0.Year.ToString().PadLeft(4, '0') + "-" + fecha0.Month.ToString().PadLeft(2, '0') + "-" + fecha0.Day.ToString().PadLeft(2, '0'); 
                string fecha1 = Cabecera.FechaEmision.ToString();

                en_CabeceraRetencion CabeceraRetencion = new en_CabeceraRetencion();
                CabeceraRetencion.at_FechaEmision = fecha1; // ; //   Cabecera.FechaEmision.ToString(); //  DateTime.Now.ToShortDateString();
                CabeceraRetencion.at_Serie = Cabecera.SerieCabecera.ToString();
      
                CabeceraRetencion.at_Numero =   int.Parse(Cabecera.NumeroCabecera.ToString()); // 54545;
                //CabeceraRetencion.at_Numero = 12; // int.Parse(Cabecera.NumeroCabecera.ToString()); // 54545;
                objDatosRetencion.ent_CabeceraRetencion = CabeceraRetencion;

                //-------------------------------------------------------------------------------
                // Parte 3
                ArrayOfEn_DatosComprobanteRelacionadoRetencion objDatosArray = new ArrayOfEn_DatosComprobanteRelacionadoRetencion();
                ArrayOfEn_DatosComprobanteRelacionadoRetencion objDatosArrayNC = new ArrayOfEn_DatosComprobanteRelacionadoRetencion();
                ArrayOfEn_DatosComprobanteRelacionadoRetencion objDatosArrayDoc = new ArrayOfEn_DatosComprobanteRelacionadoRetencion();

                int NumeroPago = 1 ;
                int correNC = 0;
                int correDOC = 0;
                int count;
                DataTable dtTablaNC;
                DataTable dtTablaDOC;

                count = lst0.Count;
                int y = 0;

                dtTablaNC = crearTabla();
                dtTablaDOC = crearTabla(); 
                NumeroPago = 1;  /// Observación Revisar 

                lst1 = lst0.OrderByDescending(x => x.ImporteDOC).ToList(); 


                foreach (VBG00946_CABECERAResult objRetencion in lst1)
                {
                   
                    string tipodoc = objRetencion.DocSunatDetalle.ToString();
                    if (tipodoc == "7")
                    {
                        DataRow dr = dtTablaNC.NewRow();
                        correNC = correNC + 1;
                        dr["Id_doc"] = objRetencion.id_doc.ToString();
                        dr["TablaOrigen"] = objRetencion.TablaOrigen.ToString();
                        dr["OpCompra"] = objRetencion.OpCompra.ToString();


                        dr["ImporteTotal"] = decimal.Round(decimal.Parse(objRetencion.ImporteDOC.ToString()), 2);

                        dr["ImporteSinRetencion"] = decimal.Round(decimal.Parse(objRetencion.ImportePago.ToString()), 2);
                        dr["ImporteRetenido"] = decimal.Round(decimal.Parse(objRetencion.ImporteretencionTC.ToString()), 2);
                        dr["MontoTotal"] = decimal.Round(decimal.Parse(objRetencion.ImporteTC.ToString()), 2);

                        dr["ImporteSinRetencion_P"] = decimal.Round(decimal.Parse(objRetencion.ImportePago.ToString()), 2);
                        dr["ImporteRetenido_P"] = decimal.Round(decimal.Parse(objRetencion.ImporteretencionTC.ToString()), 2);
                        dr["MontoTotal_P"] = decimal.Round(decimal.Parse(objRetencion.ImporteTC.ToString()), 2);


                        dr["TipoDoc"] = objRetencion.DocSunatDetalle;
                        dr["OK"] = 0; 
                        dtTablaNC.Rows.Add(dr);

                        at_ImporteTotalRetencion = at_ImporteTotalRetencion - decimal.Round(decimal.Parse(objRetencion.ImporteretencionTC.ToString()), 2);
                        at_ImporteTotalPagado = at_ImporteTotalPagado - decimal.Round(decimal.Parse(objRetencion.ImporteTC.ToString()), 2);
                    }
                    else
                    {
                        DataRow dr = dtTablaDOC.NewRow();
                        correDOC = correDOC + 1;
                        dr["Id_doc"] = objRetencion.id_doc.ToString();
                        dr["TablaOrigen"] = objRetencion.TablaOrigen.ToString();
                        dr["OpCompra"] = objRetencion.OpCompra.ToString();

                        dr["ImporteTotal"] = decimal.Round(decimal.Parse(objRetencion.ImporteDOC.ToString()), 2);

                        dr["ImporteSinRetencion"] = decimal.Round(decimal.Parse(objRetencion.ImportePago.ToString()), 2);
                        dr["ImporteRetenido"] = decimal.Round(decimal.Parse(objRetencion.ImporteretencionTC.ToString()), 2);
                        dr["MontoTotal"] = decimal.Round(decimal.Parse(objRetencion.ImporteTC.ToString()), 2);

                        dr["ImporteSinRetencion_P"] = decimal.Round(decimal.Parse(objRetencion.ImportePago.ToString()), 2);
                        dr["ImporteRetenido_P"] = decimal.Round(decimal.Parse(objRetencion.ImporteretencionTC.ToString()), 2);
                        dr["MontoTotal_P"] = decimal.Round(decimal.Parse(objRetencion.ImporteTC.ToString()), 2);


                        dr["TipoDoc"] = objRetencion.DocSunatDetalle;
                        dr["OK"] = 0;
                        dtTablaDOC.Rows.Add(dr);
                        at_ImporteTotalRetencion = at_ImporteTotalRetencion + decimal.Round(decimal.Parse(objRetencion.ImporteretencionTC.ToString()),2);
                        at_ImporteTotalPagado = at_ImporteTotalPagado + decimal.Round(decimal.Parse(objRetencion.ImporteTC.ToString()),2);
                    }
                }
                //-----------------------------------------------
                //-----------------------------------------------

                if (correNC > 0)
                {
                    DataTable tableNC = dtTablaNC;
                    DataTable tableDOC = dtTablaDOC;

                    foreach (DataRow rowNC in dtTablaNC.Rows)
                    {
                        decimal NC_ImporteSinRetencion_P = Convert.ToDecimal(rowNC["ImporteSinRetencion_P"].ToString());
                        decimal NC_ImporteRetenido_P = Convert.ToDecimal(rowNC["ImporteRetenido_P"].ToString());
                        decimal NC_MontoTotal_P = Convert.ToDecimal(rowNC["MontoTotal_P"].ToString());


                        int x = 0; 
                        while(NC_ImporteSinRetencion_P > 0)
                        {
                            foreach (DataRow rowDOC in dtTablaDOC.Rows)
                            {

                                if (NC_ImporteSinRetencion_P > 0)
                                {
                                    int OKDOC = Convert.ToInt32(rowDOC["OK"].ToString());
                                    decimal DOC_ImporteSinRetencion_P = Convert.ToDecimal(rowDOC["ImporteSinRetencion_P"].ToString());
                                    decimal DOC_ImporteRetenido_P = Convert.ToDecimal(rowDOC["ImporteRetenido_P"].ToString());
                                    decimal DOC_MontoTotal_P = Convert.ToDecimal(rowDOC["MontoTotal_P"].ToString());

                                    if (OKDOC == x)
                                    {
                                        if (DOC_ImporteSinRetencion_P > NC_ImporteSinRetencion_P)
                                        {
                                            rowDOC["ImporteSinRetencion_P"] = DOC_ImporteSinRetencion_P - NC_ImporteSinRetencion_P;
                                            rowDOC["ImporteRetenido_P"] = DOC_ImporteRetenido_P - NC_ImporteRetenido_P;
                                            rowDOC["MontoTotal_P"] = DOC_MontoTotal_P - NC_MontoTotal_P;
                                            rowDOC["OK"] = 1;
                                            NC_ImporteSinRetencion_P = NC_ImporteSinRetencion_P - NC_ImporteSinRetencion_P;
                                            NC_ImporteRetenido_P = NC_ImporteRetenido_P - NC_ImporteRetenido_P;
                                            NC_MontoTotal_P = NC_MontoTotal_P - NC_MontoTotal_P;

                                            break;
                                        }
                                        else
                                        {
                                            if (DOC_ImporteSinRetencion_P > (NC_ImporteSinRetencion_P / 2) )
                                            {
                                                rowDOC["ImporteSinRetencion_P"] = DOC_ImporteSinRetencion_P - (NC_ImporteSinRetencion_P / 2);
                                                rowDOC["ImporteRetenido_P"] = DOC_ImporteRetenido_P - (NC_ImporteRetenido_P / 2);
                                                rowDOC["MontoTotal_P"] = DOC_MontoTotal_P - (NC_MontoTotal_P / 2);
                                                rowDOC["OK"] = 1;
                                                NC_ImporteSinRetencion_P = NC_ImporteSinRetencion_P - (NC_ImporteSinRetencion_P / 2);
                                                NC_ImporteRetenido_P = NC_ImporteRetenido_P - (NC_ImporteRetenido_P / 2);
                                                NC_MontoTotal_P = NC_MontoTotal_P - (NC_MontoTotal_P / 2);
                                            }
                                            else
                                            {
                                                if (DOC_ImporteSinRetencion_P > (NC_ImporteSinRetencion_P / 3) )
                                                {
                                                    rowDOC["ImporteSinRetencion_P"] = DOC_ImporteSinRetencion_P - (NC_ImporteSinRetencion_P / 3);
                                                    rowDOC["ImporteRetenido_P"] = DOC_ImporteRetenido_P - (NC_ImporteRetenido_P / 3);
                                                    rowDOC["MontoTotal_P"] = DOC_MontoTotal_P - (NC_MontoTotal_P / 3);
                                                    rowDOC["OK"] = 1;
                                                    NC_ImporteSinRetencion_P = NC_ImporteSinRetencion_P - (NC_ImporteSinRetencion_P / 3);
                                                    NC_ImporteRetenido_P = NC_ImporteRetenido_P - (NC_ImporteRetenido_P / 3);
                                                    NC_MontoTotal_P = NC_MontoTotal_P - (NC_MontoTotal_P / 3);
                                                }
                                                else
                                                {
                                                    if (DOC_ImporteSinRetencion_P > (NC_ImporteSinRetencion_P / 4))
                                                    {
                                                        rowDOC["ImporteSinRetencion_P"] = DOC_ImporteSinRetencion_P - (NC_ImporteSinRetencion_P / 4);
                                                        rowDOC["ImporteRetenido_P"] = DOC_ImporteRetenido_P - (NC_ImporteRetenido_P / 4);
                                                        rowDOC["MontoTotal_P"] = DOC_MontoTotal_P - (NC_MontoTotal_P / 4);
                                                        rowDOC["OK"] = 1;
                                                        NC_ImporteSinRetencion_P = NC_ImporteSinRetencion_P - (NC_ImporteSinRetencion_P / 4);
                                                        NC_ImporteRetenido_P = NC_ImporteRetenido_P - (NC_ImporteRetenido_P / 4);
                                                        NC_MontoTotal_P = NC_MontoTotal_P - (NC_MontoTotal_P / 4);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    
                                }

                            }

                            x++; 
                        }

                        rowNC["ImporteSinRetencion_P"] = NC_ImporteSinRetencion_P;
                        rowNC["ImporteRetenido_P"] = NC_ImporteRetenido_P;
                        rowNC["MontoTotal_P"] = NC_ImporteRetenido_P;
                        rowNC["OK"] = 1;
                    }


                }


                if (correNC > 0)
                {
                    foreach (DataRow row in dtTablaNC.Rows)
                    {
                        en_DatosComprobanteRelacionadoRetencion objDatosNC = new en_DatosComprobanteRelacionadoRetencion();
                        foreach (VBG00946_CABECERAResult objRetencionNC in lst1)
                        {
                            if (row["Id_doc"].ToString() == objRetencionNC.id_doc.ToString())
                            {
                                objDatosNC = datosArregloNC(objRetencionNC, Cabecera.DocSunatDetalle.ToString(), Cabecera.SerieCabecera, NumeroPago);
                                row["OK"] = 100;
                                objDatosArrayNC.Add(objDatosNC);
                                break; 
                            }
                        }
                    }

                    foreach (DataRow rowDOC in dtTablaDOC.Rows)
                    {
                        en_DatosComprobanteRelacionadoRetencion objDatosDOC = new en_DatosComprobanteRelacionadoRetencion();
                        foreach (VBG00946_CABECERAResult objRetencionDOC in lst1)
                        {
                            if (rowDOC["Id_doc"].ToString() == objRetencionDOC.id_doc.ToString())
                            {
                                decimal DOC_ImporteSinRetencion_P = Convert.ToDecimal(rowDOC["ImporteSinRetencion_P"].ToString());
                                decimal DOC_ImporteRetenido_P = Convert.ToDecimal(rowDOC["ImporteRetenido_P"].ToString());
                                decimal DOC_MontoTotal_P = Convert.ToDecimal(rowDOC["MontoTotal_P"].ToString());

                                objDatosDOC = datosArreglo(objRetencionDOC, Cabecera.DocSunatDetalle.ToString(), Cabecera.SerieCabecera, NumeroPago);

                                objDatosDOC.ent_DatosRetenidos.ent_InformeRetencion.at_ImporteRetenido = decimal.Round(decimal.Parse(DOC_ImporteRetenido_P.ToString()), 2);
                                objDatosDOC.ent_DatosRetenidos.ent_InformeRetencion.at_MontoTotal = decimal.Round(decimal.Parse(DOC_MontoTotal_P.ToString()), 2);
                                objDatosDOC.ent_DatosRetenidos.at_ImporteSinRetencion = decimal.Round(decimal.Parse(DOC_ImporteSinRetencion_P.ToString()), 2);

                                rowDOC["OK"] = 100;
                                objDatosArrayDoc.Add(objDatosDOC);
                                break; 
                            }
                        }
                    }

                    objDatosArray.AddRange(objDatosArrayDoc);
                    objDatosArray.AddRange(objDatosArrayNC);

                }
                else
                {
                    foreach (VBG00946_CABECERAResult objRetencionDOC in lst0)
                    {
                        en_DatosComprobanteRelacionadoRetencion objDatosDOC = new en_DatosComprobanteRelacionadoRetencion();
                        objDatosDOC = datosArreglo(objRetencionDOC, Cabecera.DocSunatDetalle.ToString(), Cabecera.SerieCabecera, NumeroPago);
                        objDatosArray.Add(objDatosDOC);
                    }
                }


                //-----------------------------------------------

                objDatosRetencion.l_DatosComprobanteRelacionado = objDatosArray;

                objDatosRetencion.at_ImporteTotalPagado = decimal.Round(at_ImporteTotalPagado, 2); // 20;
                objDatosRetencion.at_ImporteTotalRetencion = decimal.Round(at_ImporteTotalRetencion, 2); // 566;

               
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message.ToString();
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

                //if(txtNumero.Text.Length > 0)
                //{ objComprobantes.at_Numero = int.Parse(txtNumero.Text.ToString()); }
                //else
                //{
                objComprobantes.at_Numero = int.Parse(Cabecera.NumeroCabecera.ToString());
            //}

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

            en_DatosComprobanteRelacionadoRetencion objDatos = new en_DatosComprobanteRelacionadoRetencion();

            // Parte 1
            objDatos.at_TipoComprobante = objRetencion.DocSunatDetalle.ToString().PadLeft(2, char.Parse("0")); // "1";
            objDatos.at_Serie = objRetencion.SerieDetalle.ToString(); // "544";
            objDatos.at_Numero = objRetencion.NumeroDetalle.ToString(); // 235345.ToString();
            objDatos.at_FechaEmision = objRetencion.FechaDetalle; // DateTime.Now.ToShortDateString();

            // Revisar Cambio 
            objDatos.at_ImporteTotal = decimal.Round(decimal.Parse(objRetencion.ImporteDOC.ToString()), 2); //  365;
            //objDatos.at_ImporteTotal = decimal.Round(decimal.Parse(objRetencion.ImportePago.ToString()), 2); //  365;

            objDatos.at_TipoMoneda = objRetencion.TipoMoneda;

            // Parte 2
            en_DatosRetenidos objRetenidos = new en_DatosRetenidos(); //
            objRetenidos.at_Fecha = objRetencion.FechaEmision.ToString(); // DateTime.Now.ToShortDateString();

            objRetenidos.at_ImporteSinRetencion = decimal.Round(decimal.Parse(objRetencion.ImportePago.ToString()), 2); //  2321;
            objRetenidos.at_TipoMoneda = objRetencion.MonedaRetencion;
            // ---------
                    // Parte 3
                    en_InformeRetencion objInforme = new en_InformeRetencion();
                    objInforme.at_FechaRetencion = objRetencion.FechaEmision.ToString();
                    objInforme.at_ImporteRetenido = decimal.Round(decimal.Parse(objRetencion.ImporteretencionTC.ToString()), 2);
                    objInforme.at_MontoTotal = decimal.Round(decimal.Parse(objRetencion.ImporteTC.ToString()), 2);
                    objRetenidos.ent_InformeRetencion = objInforme;

                    // Parte 4
                    en_TipoCambioRetencion TC = new en_TipoCambioRetencion();
                    TC.at_Fecha = objRetencion.FechaTC;
                    TC.at_MontoCambio = decimal.Parse(objRetencion.TC.ToString());
                    TC.at_TipoMoneda = objRetencion.TipoMoneda;

                    if (objRetencion.MonedaRetencion != "PEN")
                    {
                        objRetenidos.ent_TipoCambio = TC;
                    }
                    // ---------  

            decimal ImporteFactura = 0;
            decimal ImportePago = 0;

            ImportePago = objRetenidos.at_ImporteSinRetencion;
            ImporteFactura = objDatos.at_ImporteTotal;

            //// Parte 2
            //if (ImporteFactura > ImportePago) // Correlativo
            //{ objRetenidos.at_Numero = 2; }
            //else
            //{ objRetenidos.at_Numero = 1; }  // Correlativo

            objRetenidos.at_Numero = 1;
            //---------------------------

            objDatos.ent_DatosRetenidos = objRetenidos;

            return objDatos; 
        }

        en_DatosComprobanteRelacionadoRetencion datosArregloNC(VBG00946_CABECERAResult objRetencion, string DocSunatDetalle, string SerieCabecera, int Correlativo)
        {
            //en_DatosComprobanteRelacionadoRetencion dtcompro = new en_DatosComprobanteRelacionadoRetencion();
            en_DatosComprobanteRelacionadoRetencion objDatos = new en_DatosComprobanteRelacionadoRetencion();
            objDatos.at_FechaEmision = objRetencion.FechaDetalle; // DateTime.Now.ToShortDateString();
            objDatos.at_ImporteTotal = decimal.Round(decimal.Parse(objRetencion.ImporteDOC.ToString()), 2); //  365;
            objDatos.at_Numero = objRetencion.NumeroDetalle.ToString(); // 235345.ToString();

            decimal TipoDoc = decimal.Parse(DocSunatDetalle.ToString());  //decimal.Parse(Cabecera.DocSunatDetalle.ToString());

            objDatos.at_Serie = objRetencion.SerieDetalle.ToString(); // "544";
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

                //objInformacionAdicionales.l_Etiquetas = objEtiquetasArray; 

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objInformacionAdicionales;
        }

        RetencionesDevWCF.ens_Respuesta LlenarComprobanteRetencion(int Op, bool Otorgamiento)
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

                Retencion.at_ControlOtorgamiento = Otorgamiento;
                Retencion.ent_Emisor = Emisor;
                Retencion.ent_Receptor = Receptor;
                Retencion.ent_DatosRetencion = DatosRetencion;
                Retencion.ent_InformacionAdicionales = InformacionAdicional;

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
                
                //if(txtReversa.Text.Length > 0 )
                //{  CabeceraResumenReversion.at_IdentificadorUnico = txtReversa.Text;     // Correlativo.  
                //}
                //else
                //{
                    CabeceraResumenReversion.at_IdentificadorUnico = "RV1001";     // Correlativo. 
                //}

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
                dec = " CON " + decimales.ToString() + "/100 SOLES";
            }
            else
            {
                dec = " CON " + decimales.ToString() + "/100 SOLES";
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