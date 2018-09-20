using GS.SISGEGS.DM;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.FacturaElectronica2WCF;
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
using System.ComponentModel;


namespace GS.SISGEGS.Web.FacturacionElectronica 
{
    public partial class frmFacturaElectronica : System.Web.UI.Page
    {
        FacturacionElectronicaOkWCF.WSComprobanteSoapClient oServicio = new FacturacionElectronicaOkWCF.WSComprobanteSoapClient();
        FacturacionElectronicaOkWCF.General oGeneral = new FacturacionElectronicaOkWCF.General();
        FacturacionElectronicaOkWCF.ENEmpresa objEmpresa = new FacturacionElectronicaOkWCF.ENEmpresa();
        FacturacionElectronicaOkWCF.ENComprobante objComprobante = new FacturacionElectronicaOkWCF.ENComprobante();

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
                List<VBG04694Result> lst = objFacturaWCF.FacturaElectronica_Listar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, fechaInicial, fechaFinal, codAgenda, null, 0, TipoDoc, 0, null, 0).ToList();
       
                ViewState["lstFacturaElectronica"] = JsonHelper.JsonSerializer(lst);
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

                    if (cboTipoDoc == null || cboTipoDoc.SelectedValue == "" || cboTipoDoc.SelectedIndex == 0)
                    {
                        TipoDoc = 0;
                    }
                    else { TipoDoc = Convert.ToInt32(cboTipoDoc.SelectedValue.ToString()); }


                    ListarFacturaElectronica(cliente, fecha1, fecha2, TipoDoc);
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }

        }
        private void Actualizar_FacturaElectronica()
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

                    if (cboTipoDoc == null || cboTipoDoc.SelectedValue == "" || cboTipoDoc.SelectedIndex == 0)
                    {
                        TipoDoc = 0;
                    }
                    else { TipoDoc = Convert.ToInt32(cboTipoDoc.SelectedValue.ToString()); }


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

        protected void btnActualizarEstados_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");
            try
            {

                ActualizarEstado(txtIdCliente.Text, txtNumeroREF.Text, txtSerieREF.Text);
                //foreach (GridItem rowitem in grdFacturaElectronica.MasterTableView.Items)
                //{
                //    GridDataItem dataitem = (GridDataItem)rowitem;
                //    TableCell cell = dataitem["CheckColumn"];
                //    CheckBox checkBox = (CheckBox)cell.Controls[0].FindControl("Check");
                //    if (checkBox.Checked)
                //    {

                       

                //    }
                //}
                //BuscarFechas_ListarFacturaElectronica();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscarRegistros_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");
            try
            {

                BuscarResumen(txtIdCliente.Text, txtNumeroREF.Text, txtSerieREF.Text);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnEnviarResumen_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");
            try
            {
                RegistrarResumenDiario(txtIdCliente.Text, txtNumeroREF.Text, txtSerieREF.Text);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnDarBaja_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");
            try
            {

                RegistrarDarBajaComprobante(txtIdCliente.Text, txtNumeroREF.Text, txtSerieREF.Text);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }

            return table;

        }


        protected void btnTipoCambio_Click(object sender, EventArgs e)
        {
            FacturacionElectronicaOkWCF.WSComprobanteSoapClient oServicioOK2 = new FacturacionElectronicaOkWCF.WSComprobanteSoapClient();

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");
            try
            {

                //Opcional  RegistrarTipoCambio   ----------------
                FacturacionElectronicaOkWCF.ENEnvioTipoCambio oTipoCambio = new FacturacionElectronicaOkWCF.ENEnvioTipoCambio();

                oTipoCambio.CodigoMoneda = "USD";
                oTipoCambio.FechaTipoCambio = dpFinal.SelectedDate.Value;
                oTipoCambio.Ruc = txtIdCliente.Text;
                oTipoCambio.TipoCambio = Convert.ToDecimal(txtTipoCambio.Text);

                string cadena = "";
                bool Exito = oServicioOK2.RegistrarTipoCambio(oTipoCambio, ref cadena);
                Exito = Exito;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }



        void ActualizarEstado( string ruc, string numero, string serie )
        {

            FacturacionElectronicaOkWCF.WSComprobanteSoapClient oServicioOK2 = new FacturacionElectronicaOkWCF.WSComprobanteSoapClient();

            //FacturacionElectronicaOkWCF.General oGeneral = new FacturacionElectronicaOkWCF.General();
            //FacturacionElectronicaOkWCF.ENEmpresa objEmpresa = new FacturacionElectronicaOkWCF.ENEmpresa();
            //FacturacionElectronicaOkWCF.ENComprobante objComprobante = new FacturacionElectronicaOkWCF.ENComprobante();

            try
            {

                // ConsultarRespuestaComprobante ----------------
                FacturacionElectronicaOkWCF.ENSConsultarRespuestaComprobante oConsultaPeticion = new FacturacionElectronicaOkWCF.ENSConsultarRespuestaComprobante();
                FacturacionElectronicaOkWCF.ENRConsultarRespuestaComprobante oRespuestaConsulta = new FacturacionElectronicaOkWCF.ENRConsultarRespuestaComprobante();

                oConsultaPeticion.RucEmisor = ruc;
                oConsultaPeticion.CantidadComprobante = Convert.ToInt32(txtCorrelativo.Text);
                
                oRespuestaConsulta = oServicioOK2.ConsultarRespuestaComprobante(oConsultaPeticion);



                // ConfirmarRespuestaComprobante ----------------
                FacturacionElectronicaOkWCF.ENSConfirmarRespuestaComprobante oConfirmarPeticion = new FacturacionElectronicaOkWCF.ENSConfirmarRespuestaComprobante();
                FacturacionElectronicaOkWCF.ENRConfirmarRespuestaComprobante oRespuestaConfirmar = new FacturacionElectronicaOkWCF.ENRConfirmarRespuestaComprobante();
                FacturacionElectronicaOkWCF.ENDetalleComprobante[] oListaDetalleComprobante = new FacturacionElectronicaOkWCF.ENDetalleComprobante[1];
                FacturacionElectronicaOkWCF.ENDetalleComprobante oDetalleComprobante = new FacturacionElectronicaOkWCF.ENDetalleComprobante();

                oDetalleComprobante.Numero = Convert.ToInt32(numero);
                oDetalleComprobante.Serie = serie;
                oDetalleComprobante.TipoComprobante = "01";
                oListaDetalleComprobante[0] = oDetalleComprobante; 

                oConfirmarPeticion.RucEmisor = ruc;
                oConfirmarPeticion.DetalleComprobante = oListaDetalleComprobante;
        
                oRespuestaConfirmar = oServicioOK2.ConfirmarRespuestaComprobante(oConfirmarPeticion);

                // ConsultarComprobanteIndividual ----------------
                FacturacionElectronicaOkWCF.ENComprobanteConsulta oComprobanteConsulta = new FacturacionElectronicaOkWCF.ENComprobanteConsulta();
                FacturacionElectronicaOkWCF.ENRespuesta oRespuestaComprobante = new FacturacionElectronicaOkWCF.ENRespuesta();
                FacturacionElectronicaOkWCF.ENComprobanteN oComprobanteN = new FacturacionElectronicaOkWCF.ENComprobanteN();
                FacturacionElectronicaOkWCF.ENComprobanteResumenDiario oComprobanteResumendiario = new FacturacionElectronicaOkWCF.ENComprobanteResumenDiario();
                FacturacionElectronicaOkWCF.ENComprobanteComunicadoBaja oComprobanteComunicadoBaja = new FacturacionElectronicaOkWCF.ENComprobanteComunicadoBaja();

                oComprobanteN.Numero = numero;
                oComprobanteN.Serie = serie;
                oComprobanteN.TipoComprobante = "01" ;  // Factura 

                oComprobanteConsulta.RucEmpresa = ruc;
                oComprobanteConsulta.NComprobante = oComprobanteN;
                oComprobanteConsulta.TipoConsulta = 1; // 1 :Por comprobante (factura, nota de crédito y nota de debito que hacen referencia a una factura) • 2 : Por resumen diario • 3 : Por comunicación de baja

                oComprobanteConsulta.NResumenDiario = oComprobanteResumendiario;
                oComprobanteConsulta.NComunicacionBaja = oComprobanteComunicadoBaja; 
                string cadena;
                cadena = ""; 

                oRespuestaComprobante = oServicioOK2.ConsultarComprobanteIndividual(oComprobanteConsulta, ref cadena );


                // Opcional ConsultarEstadoComprobante ----------------

                FacturacionElectronicaOkWCF.ENPeticionEstadoComprobante oPeticionEstado = new FacturacionElectronicaOkWCF.ENPeticionEstadoComprobante();
                FacturacionElectronicaOkWCF.ENRespuestaEstadoComprobante oRespuestaEstado = new FacturacionElectronicaOkWCF.ENRespuestaEstadoComprobante();

                oPeticionEstado.RucEmpresa = ruc;
                oPeticionEstado.CantidadComprobantes =Convert.ToInt32( txtCorrelativo.Text);

                oRespuestaEstado = oServicioOK2.ConsultarEstadoComprobante(oPeticionEstado);

                DataTable dtTablaCruce;

                dtTablaCruce = ConvertToDataTable(oRespuestaEstado.ListaEstadoComprobante.ToList());

                //Opcional ConfirmarEstadoComprobante ----------------
                FacturacionElectronicaOkWCF.ENPeticionConfirmacion oPeticion = new FacturacionElectronicaOkWCF.ENPeticionConfirmacion();
                FacturacionElectronicaOkWCF.ENRespuestaConfirmacion oRespuesta = new FacturacionElectronicaOkWCF.ENRespuestaConfirmacion();
                FacturacionElectronicaOkWCF.ENListaConfirmacionComprobante[] oListaComprovante = new FacturacionElectronicaOkWCF.ENListaConfirmacionComprobante[1]; 
                FacturacionElectronicaOkWCF.ENListaConfirmacionComprobante oComprovante = new FacturacionElectronicaOkWCF.ENListaConfirmacionComprobante();

                oComprovante.Numero = Convert.ToInt32(numero) ;
                oComprovante.Serie = serie;
                oListaComprovante[0] = oComprovante;

                oPeticion.RucEmpresa = ruc;
                oPeticion.ListaConfirmacionComprobante = oListaComprovante; 

                oRespuesta = oServicioOK2.ConfirmarEstadoComprobante(oPeticion);

                //Opcional  ConsultarInformacionComprobante   ----------------
                FacturacionElectronicaOkWCF.ENPeticionInformacionComprobante oPeticionInformacion = new FacturacionElectronicaOkWCF.ENPeticionInformacionComprobante();
                FacturacionElectronicaOkWCF.ENRespuestaInformacionComprobante oRespuestaInformacion = new FacturacionElectronicaOkWCF.ENRespuestaInformacionComprobante();

                oPeticionInformacion.numero = numero;
                oPeticionInformacion.serie = serie;
                oPeticionInformacion.RucEmisor = ruc;
                oPeticionInformacion.TipoComprobante = "1";
               
                oRespuestaInformacion = oServicioOK2.ConsultarInformacionComprobante(oPeticionInformacion);


                //Opcional  ConsultarXMLComprobante   ----------------
                FacturacionElectronicaOkWCF.ENSConsultarXMLComprobante oPeticionXML = new FacturacionElectronicaOkWCF.ENSConsultarXMLComprobante();
                FacturacionElectronicaOkWCF.ENRConsultarXMLComprobante oRespuestaXML = new FacturacionElectronicaOkWCF.ENRConsultarXMLComprobante();

                oPeticionXML.Ruc = ruc;
                oPeticionXML.Serie = serie;
                oPeticionXML.TipoComprobante = "01";
                oPeticionXML.Numero = Convert.ToInt32(numero);
                oPeticionXML.TipoConsulta = "RCE";  // RCE: Xml del CDR de sunat ;  XCE : Xml del comprobante electronico

                oRespuestaXML = oServicioOK2.ConsultarXMLComprobante(oPeticionXML);

                // --------------------------------------------

                //Opcional  Obtener_PDF   ----------------
                FacturacionElectronicaOkWCF.ENPeticion oPeticionPDF = new FacturacionElectronicaOkWCF.ENPeticion();
                FacturacionElectronicaOkWCF.ENRespuestaPDF oRespuestaPDF = new FacturacionElectronicaOkWCF.ENRespuestaPDF();

                oPeticionPDF.Numero = numero;
                oPeticionPDF.Serie = serie;
                oPeticionPDF.TipoComprobante = "01";
                oPeticionPDF.Ruc = ruc;
                oPeticionPDF.IndicadorComprobante = 1;
               
                cadena = "";

                oRespuestaPDF = oServicioOK2.Obtener_PDF(oPeticionPDF, ref cadena);

                FileStream fst;
                BinaryWriter bwt;
                string destino = "C:\\Users\\cesar.coronel\\Desktop\\Pruebas_TXT\\TCI\\";

                fst = new FileStream(destino + oRespuestaPDF.NombrePDF, FileMode.Create, FileAccess.ReadWrite );
                bwt = new BinaryWriter(fst);
                bwt.Write(oRespuestaPDF.ArchivoPDF);
                bwt.Close();


                //Opcional  Obtener_XML   ----------------
                FacturacionElectronicaOkWCF.ENPeticion objPeticionXML = new FacturacionElectronicaOkWCF.ENPeticion();
                FacturacionElectronicaOkWCF.ENRespuestaXML objRespuestaXML = new FacturacionElectronicaOkWCF.ENRespuestaXML();

                objPeticionXML.Numero = numero;
                objPeticionXML.Serie = serie;
                objPeticionXML.TipoComprobante = "01";
                objPeticionXML.Ruc = ruc;
                objPeticionXML.IndicadorComprobante = 1;

                cadena = "";
                objRespuestaXML = oServicioOK2.Obtener_XML(objPeticionXML, ref cadena);

                string strCadenaDesencriptada;
                Stream s = new MemoryStream(objRespuestaXML.ArchivoXML);
                StreamReader objReader = new StreamReader(s);
                string contenidoXML = objReader.ReadToEnd();

                char oldChar = Convert.ToChar(0);
                char newChar = Convert.ToChar(" "); 

                string cadenaEnviar = contenidoXML.Replace(oldChar, newChar).Trim();
                strCadenaDesencriptada = cadenaEnviar;
                // initiateWebBrowser(strCadenaDesencriptada)


                //Opcional  RegistrarTipoCambio   ----------------
                FacturacionElectronicaOkWCF.ENEnvioTipoCambio oTipoCambio = new FacturacionElectronicaOkWCF.ENEnvioTipoCambio();

                oTipoCambio.CodigoMoneda = "USD";
                oTipoCambio.FechaTipoCambio = dpFechaFinal.SelectedDate.Value;
                oTipoCambio.Ruc = ruc;
                oTipoCambio.TipoCambio = Convert.ToDecimal("3.5") ;
                
                cadena = "";
                bool Exito = oServicioOK2.RegistrarTipoCambio(oTipoCambio, ref cadena);
                Exito = Exito; 

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        void RegistrarDarBajaComprobante(string ruc, string numero, string serie)
        {
            FacturacionElectronicaOkWCF.WSComprobanteSoapClient oServicioOK2 = new FacturacionElectronicaOkWCF.WSComprobanteSoapClient();

            try
            {
                // // RegistrarComunicacionBaja----------------
                bool Exito;
                string cadena = "";

                int idComunicacionBaja;

                FacturacionElectronicaOkWCF.General oGeneral = new FacturacionElectronicaOkWCF.General();
                FacturacionElectronicaOkWCF.ENNumeradosNoEmitidosCab objENNumeradosNoEmitidosCab = new FacturacionElectronicaOkWCF.ENNumeradosNoEmitidosCab();
                FacturacionElectronicaOkWCF.ENNumeradosNoEmitidos[] lstaENNumeradosNoEmitidosDet = new FacturacionElectronicaOkWCF.ENNumeradosNoEmitidos[1];
                FacturacionElectronicaOkWCF.ENNumeradosNoEmitidos objENNumeradosNoEmitidosDet = new FacturacionElectronicaOkWCF.ENNumeradosNoEmitidos();
                FacturacionElectronicaOkWCF.ENEmpresa oEmpresa = new FacturacionElectronicaOkWCF.ENEmpresa();
                FacturacionElectronicaOkWCF.ENErrorComunicacion[] oRespuestaError = new FacturacionElectronicaOkWCF.ENErrorComunicacion[1];

                LlenarEmpresa();
                oEmpresa = objEmpresa;
                oGeneral.oENEmpresa = oEmpresa;
                objENNumeradosNoEmitidosCab.FechaEmision = "2016-07-29";
                objENNumeradosNoEmitidosCab.FechaGeneracion = "2016-07-29";
                idComunicacionBaja = Convert.ToInt32( txtCorrelativo.Text) ;

                objENNumeradosNoEmitidosDet.Item = 1;
                objENNumeradosNoEmitidosDet.CodigoTipoDocumento = "01";
                objENNumeradosNoEmitidosDet.NumeroDocumento = numero;
                objENNumeradosNoEmitidosDet.SerieDocumento = serie;
                objENNumeradosNoEmitidosDet.MotivoBaja = "Prueba integral, dar de baja";

                lstaENNumeradosNoEmitidosDet[0] = objENNumeradosNoEmitidosDet;

                objENNumeradosNoEmitidosCab.NumeradosNoEmitidos = lstaENNumeradosNoEmitidosDet;

                oGeneral.oENNumeradosNoEmitidosCab = objENNumeradosNoEmitidosCab;

                bool ExitoR; 
                Exito = oServicioOK2.RegistrarComunicacionBaja(oGeneral, cadena, ref oRespuestaError,idComunicacionBaja);
                ExitoR = Exito; 
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        void RegistrarResumenDiario(string ruc, string numero, string serie)
        {
            FacturacionElectronicaOkWCF.WSComprobanteSoapClient oServicioOK2 = new FacturacionElectronicaOkWCF.WSComprobanteSoapClient();
  
            try
            {
                // // RegistrarResumen----------------
                bool Exito;
                string cadena = "";
                int IdResumenCliente; 

                DataTable dtEmpresa;
                DataTable dtCabcera;
                DataTable dtDetalle;
                int idComunicacionBaja;

                FacturacionElectronicaOkWCF.ENResumen oResumen = new FacturacionElectronicaOkWCF.ENResumen();
                FacturacionElectronicaOkWCF.ENEmpresa oEmpresa = new FacturacionElectronicaOkWCF.ENEmpresa();

                FacturacionElectronicaOkWCF.ENResumenDetalle[] oListaResumenDetalle = new FacturacionElectronicaOkWCF.ENResumenDetalle[1];

                FacturacionElectronicaOkWCF.ENResumenDetalle oResumenDetalle = new FacturacionElectronicaOkWCF.ENResumenDetalle();
                LlenarEmpresa();

                oEmpresa = objEmpresa;
                oResumen.RucEmpresa = oEmpresa.Ruc;
                oResumen.Correlativo = 10;
                oResumen.FechaEmision = DateTime.Now;
                oResumen.FechaGeneracion = DateTime.Now;
                oResumen.TipoDocumento = "RC";

            
                oResumenDetalle.NumeroInicial = "2679";
                oResumenDetalle.NumeroFinal = "2680";
                oResumenDetalle.TipoDocumento = "03";
                oResumenDetalle.Serie = "B002";
                oResumenDetalle.Total = 2000;
                oResumenDetalle.TotalCargos = 0;
                oResumenDetalle.TotalGravadas = 2000;
                oResumenDetalle.TotalExonerada = 0;
                oResumenDetalle.TotalInafectas = 0;
                oResumenDetalle.TotalExportacion = 0;
                oResumenDetalle.Moneda = "PEN";

                FacturacionElectronicaOkWCF.ENResumenDetalleImpuesto[] oListaResumenDetalleImpuesto = new FacturacionElectronicaOkWCF.ENResumenDetalleImpuesto[2];
                FacturacionElectronicaOkWCF.ENResumenDetalleImpuesto oResumenDetalleImpuesto = new FacturacionElectronicaOkWCF.ENResumenDetalleImpuesto();

                oResumenDetalleImpuesto = new FacturacionElectronicaOkWCF.ENResumenDetalleImpuesto();
                oResumenDetalleImpuesto.CodigoTipoImpuesto = "1000";
                oResumenDetalleImpuesto.ImporteExplicito = 100;
                oResumenDetalleImpuesto.ImporteTotal = 100;
                oListaResumenDetalleImpuesto[0] = oResumenDetalleImpuesto;

                oResumenDetalleImpuesto = new FacturacionElectronicaOkWCF.ENResumenDetalleImpuesto();
                oResumenDetalleImpuesto.CodigoTipoImpuesto = "2000";
                oResumenDetalleImpuesto.ImporteExplicito = 0;
                oResumenDetalleImpuesto.ImporteTotal = 0;
                oListaResumenDetalleImpuesto[1] = oResumenDetalleImpuesto;

                oResumenDetalle.ResumenDetalleImpuesto = oListaResumenDetalleImpuesto;

                oListaResumenDetalle[0] = oResumenDetalle; 

                oResumen.ResumenDetalle = oListaResumenDetalle;

                bool ExitoR;
                IdResumenCliente = Convert.ToInt32(txtCorrelativo.Text); 

                Exito = oServicioOK2.RegistrarResumen(oResumen,ref cadena, IdResumenCliente);
                ExitoR = Exito;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        void BuscarResumen(string ruc, string numero, string serie)
        {
            DataTable dtTablaCruce = new DataTable();
            FacturacionElectronicaOkWCF.WSComprobanteSoapClient oServicioOK2 = new FacturacionElectronicaOkWCF.WSComprobanteSoapClient();
  
            try
            {

                // ConsultarRespuestaResumen ----------------

                FacturacionElectronicaOkWCF.ENSConsultarRespuestaResumen oPeticionResumen = new FacturacionElectronicaOkWCF.ENSConsultarRespuestaResumen();
                FacturacionElectronicaOkWCF.ENRConsultarRespuestaResumen oRespuestaResumen = new FacturacionElectronicaOkWCF.ENRConsultarRespuestaResumen();

                oPeticionResumen.RucEmisor = ruc;
                oPeticionResumen.CantidadResumen = Convert.ToInt32(txtCorrelativo.Text) ;

                oRespuestaResumen = oServicioOK2.ConsultarRespuestaResumen(oPeticionResumen);

                
                dtTablaCruce = ConvertToDataTable(oRespuestaResumen.ResumenRespuesta.ToList());

                // ConfirmarRespuestaResumen ----------------

                FacturacionElectronicaOkWCF.ENSConfirmarRespuestaResumen oPeticionConfirmarResumen = new FacturacionElectronicaOkWCF.ENSConfirmarRespuestaResumen();
                FacturacionElectronicaOkWCF.ENRConfirmarRespuestaResumen oRespuestaConfirmarResumen = new FacturacionElectronicaOkWCF.ENRConfirmarRespuestaResumen();

                FacturacionElectronicaOkWCF.ENDetalleResumen[] oListaResumen = new FacturacionElectronicaOkWCF.ENDetalleResumen[1];
                FacturacionElectronicaOkWCF.ENDetalleResumen oResumen = new FacturacionElectronicaOkWCF.ENDetalleResumen();

                oResumen.IdResumenCliente = 402;
                oResumen.NombreResumen = "20191503482-RA-20160729-1";
                oResumen.TipoResumen = "RA"; //RA resumen de baja,  RC resumen diario

                oListaResumen[0] = oResumen;

                oPeticionConfirmarResumen.RucEmisor = ruc;
                oPeticionConfirmarResumen.DetalleResumen = oListaResumen;


                oRespuestaConfirmarResumen = oServicioOK2.ConfirmarRespuestaResumen(oPeticionConfirmarResumen);

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

            if (cboTipoDoc == null || cboTipoDoc.SelectedValue == "" || cboTipoDoc.SelectedIndex == 0)
            {
                valor = 1;
                lblMensajeFecha.Text = lblMensaje.Text + "Debe seleccionar un Tipo de documento. ";
                lblMensajeFecha.CssClass = "mensajeError";
              
            }

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
                    List<VBG04694Result> lst = JsonHelper.JsonDeserialize<List<VBG04694Result>>((string)ViewState["lstFacturaElectronica"]);
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

        private void CargarFecha_Facturacion()
        {
            try
            {
                FacturaElectronica2WCFClient objFacturaWCF = new FacturaElectronica2WCFClient();
                gsComboDocElectronicoResult objTipoDoc = new gsComboDocElectronicoResult();

                List<gsComboDocElectronicoResult> lst = objFacturaWCF.ComboDocElectronico(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();


                lst.Insert(0, objTipoDoc);
                objTipoDoc.Nombre = "Todos";
                objTipoDoc.ID_Documento = 0;

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
                        int Op = Convert.ToInt32(dataitem.GetDataKeyValue("OpOrigen").ToString());
                        int DocSunat = Convert.ToInt32(dataitem.GetDataKeyValue("DocSunat").ToString());
                        string Serie = dataitem["Serie"].Text;
                        string TablaOrigen = dataitem["TablaOrigen"].Text;
                        string Numero = dataitem["Numero"].Text;

                        if (txtNumero.Text.Length > 0)
                        {
                            Numero = txtNumero.Text;
                        }
                        if (txtSerie.Text.Length > 0)
                        {
                            Serie = txtSerie.Text;
                        }


                        if (DocSunat == 1) // Factura (Venta) - Factura (Venta Relacionadas)
                        {
                            EnviarFacturaElectronica(Op, TablaOrigen, Serie, Numero);
                        }
                        else if (DocSunat == 3) // Boleta (Venta)
                        {
                            EnviarBoletaElectronica(Op, TablaOrigen, Serie, Numero);
                        }
                        else if (DocSunat == 8) // Nota Debito
                        {
                            string ReferenciaDocumentoTipo = dataitem["ReferenciaDocumentoTipo"].Text;
                            string ReferenciaDocumentoNombre = dataitem["ReferenciaDocumentoNombre"].Text;
                            string ReferenciaDocumentoSerie = dataitem["ReferenciaDocumentoSerie"].Text;
                            string ReferenciaDocumentoNumero = dataitem["ReferenciaDocumentoNumero"].Text;
                            string ReferenciaDocumentoFecha = dataitem["ReferenciaDocumentoFecha"].Text;

                            if (txtSerieREF.Text.Length > 0)
                            {
                                ReferenciaDocumentoSerie = txtSerieREF.Text;
                            }

                            if (txtNumeroREF.Text.Length > 0)
                            {
                                ReferenciaDocumentoNumero = txtNumeroREF.Text;
                            }

                            EnviarNotaDebitoElectronica(Op, TablaOrigen, Serie, Numero, ReferenciaDocumentoTipo, ReferenciaDocumentoNombre, ReferenciaDocumentoSerie, ReferenciaDocumentoNumero, ReferenciaDocumentoFecha);
                        }
                        else if (DocSunat == 7) // Nota Credito (Venta)
                        {
                            string ReferenciaDocumentoTipo = dataitem["ReferenciaDocumentoTipo"].Text;
                            string ReferenciaDocumentoNombre = dataitem["ReferenciaDocumentoNombre"].Text;
                            string ReferenciaDocumentoSerie = dataitem["ReferenciaDocumentoSerie"].Text;
                            string ReferenciaDocumentoNumero = dataitem["ReferenciaDocumentoNumero"].Text;
                            string ReferenciaDocumentoFecha = dataitem["ReferenciaDocumentoFecha"].Text;

                            if (txtSerieREF.Text.Length > 0)
                            {
                                ReferenciaDocumentoSerie = txtSerieREF.Text;
                            }
                            if (txtNumeroREF.Text.Length > 0)
                            {
                                ReferenciaDocumentoNumero = txtNumeroREF.Text;
                            }

                            if (dpFechaReferencia.SelectedDate != null  )
                            {
                                ReferenciaDocumentoFecha = dpFechaReferencia.SelectedDate.Value.ToShortDateString();
                            }




                            EnviarNotaCreditoElectronica(Op, TablaOrigen, Serie, Numero, ReferenciaDocumentoTipo, ReferenciaDocumentoNombre, ReferenciaDocumentoSerie, ReferenciaDocumentoNumero, ReferenciaDocumentoFecha);

                        }
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
        void RegistrarEstado(bool result, string TablaOrigen, int Op, string cadena)
        {
            FacturaElectronica2WCFClient objElectronicoWCF = new FacturaElectronica2WCFClient();
            try
            {
                if (result == false)
                {
                    objElectronicoWCF.DocumentosElectronicos_Update(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, TablaOrigen, Op, cadena, 1);
                    lblMensaje.Text = cadena.ToString();
                    lblMensaje.CssClass = "mensajeError";
                }
                else
                {
                    objElectronicoWCF.DocumentosElectronicos_Update(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, TablaOrigen, Op, "Registro exitoso en TCI.", 0);
                    lblMensaje.Text = "Comprobante Generado Correctamente - " + result.ToString();
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        string ConvertirSerieNC(int id_tipoDocRef, string Serie)
        {
            string SerieConv = "";
            string letra;
            letra = Serie.Substring(1, 1);

            if(letra=="F" || letra=="B")
            {
                SerieConv = Serie; 
            }
            else
            {
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
            }
           


            return SerieConv;
        }
        string ConvertirSerieND(int id_tipoDocRef, string Serie)
        {
            string SerieConv = "";
            string letra;
            letra = Serie.Substring(1, 1);

            if (letra == "F" || letra == "B")
            {
                SerieConv = Serie;
            }
            else
            {
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
            }



            return SerieConv;
        }


        void EnviarFacturaElectronica(int Op, string TablaOrigen, string Serie, string Numero)
        {
            try
            {
                string SerieF =""; 

                if(Serie.Length>3)
                {
                    SerieF  = "F" + Serie.Substring(1, 3);
                }
                else
                {
                    SerieF = "F" + Serie.ToString();
                }

                string cadena = "";
                int tipoCodigo = 1;
                byte[] codigoBarra = null;
                string codigoHash = "";
                FacturacionElectronicaOkWCF.ENErrorComunicacion[] ListaError = new FacturacionElectronicaOkWCF.ENErrorComunicacion[0];
                ListaError = null;
                bool result = false;
                string Archivo = null;

                LlenarEmpresa(); //Lleno Datos de la Empresa Emisora
                LlenarComprobanteFactura(Op, TablaOrigen, SerieF, Numero, ref Archivo); //Lleno los Datos del Comprobante

                oGeneral.oENEmpresa = objEmpresa;
                oGeneral.oENComprobante = objComprobante;

                if (objComprobante.TipoComprobante == "01")
                {
                    result = oServicio.Registrar(oGeneral, FacturacionElectronicaOkWCF.TipoDocumento.Factura, ref cadena, tipoCodigo, ref codigoBarra, ref codigoHash, ref ListaError, 0, "1");
                }
                else if (objComprobante.TipoComprobante == "03")
                {
                    result = oServicio.Registrar(oGeneral, FacturacionElectronicaOkWCF.TipoDocumento.Boleta, ref cadena, tipoCodigo, ref codigoBarra, ref codigoHash, ref ListaError, 0, "1");
                }
                else if (objComprobante.TipoComprobante == "07")
                {
                    result = oServicio.Registrar(oGeneral, FacturacionElectronicaOkWCF.TipoDocumento.NotaCredito, ref cadena, tipoCodigo, ref codigoBarra, ref codigoHash, ref ListaError, 0, "1");
                }
                else if (objComprobante.TipoComprobante == "08")
                {
                    result = oServicio.Registrar(oGeneral, FacturacionElectronicaOkWCF.TipoDocumento.NotaDebito, ref cadena, tipoCodigo, ref codigoBarra, ref codigoHash, ref ListaError, 0, "1");
                }

                RegistrarEstado(result, TablaOrigen, Op, cadena );

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        void EnviarBoletaElectronica(int Op, string TablaOrigen, string Serie, string Numero)
        {
            try
            {
                string SerieF = "";

                if (Serie.Length > 3)
                {
                    SerieF = "B" + Serie.Substring(1, 3);
                }
                else
                {
                    SerieF = "B" + Serie.ToString();
                }

                string cadena = "";
                int tipoCodigo = 1;
                byte[] codigoBarra = null;
                string codigoHash = "";
                FacturacionElectronicaOkWCF.ENErrorComunicacion[] ListaError = new FacturacionElectronicaOkWCF.ENErrorComunicacion[0];
                ListaError = null;
                bool result = false;
                string Archivo = null;

                LlenarEmpresa(); //Lleno Datos de la Empresa Emisora
                LlenarComprobanteBoleta(Op, TablaOrigen, SerieF, Numero, ref Archivo); //Lleno los Datos del Comprobante

                oGeneral.oENEmpresa = objEmpresa;
                oGeneral.oENComprobante = objComprobante;

                if (objComprobante.TipoComprobante == "01")
                {
                    result = oServicio.Registrar(oGeneral, FacturacionElectronicaOkWCF.TipoDocumento.Factura, ref cadena, tipoCodigo, ref codigoBarra, ref codigoHash, ref ListaError, 0, "1");
                }
                else if (objComprobante.TipoComprobante == "03")
                {
                    result = oServicio.Registrar(oGeneral, FacturacionElectronicaOkWCF.TipoDocumento.Boleta, ref cadena, tipoCodigo, ref codigoBarra, ref codigoHash, ref ListaError, 0, "1");
                }
                else if (objComprobante.TipoComprobante == "07")
                {
                    result = oServicio.Registrar(oGeneral, FacturacionElectronicaOkWCF.TipoDocumento.NotaCredito, ref cadena, tipoCodigo, ref codigoBarra, ref codigoHash, ref ListaError, 0, "1");
                }
                else if (objComprobante.TipoComprobante == "08")
                {
                    result = oServicio.Registrar(oGeneral, FacturacionElectronicaOkWCF.TipoDocumento.NotaDebito, ref cadena, tipoCodigo, ref codigoBarra, ref codigoHash, ref ListaError, 0, "1");
                }

                RegistrarEstado(result, TablaOrigen, Op, cadena);

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        void EnviarNotaCreditoElectronica(int Op, string TablaOrigen, string Serie, string Numero, string ReferenciaDocumentoTipo, string ReferenciaDocumentoNombre, string ReferenciaDocumentoSerie, string ReferenciaDocumentoNumero, string ReferenciaDocumentoFecha)
        {
            string SerieNC = "";
            string SerieRef = "";
            int id_tipoDocRef;
            try
            {
                id_tipoDocRef = int.Parse(ReferenciaDocumentoTipo);
                SerieNC = ConvertirSerieNC(id_tipoDocRef, Serie);
                SerieRef = ReferenciaDocumentoSerie;


                string cadena = "";
                int tipoCodigo = 1;
                byte[] codigoBarra = null;
                string codigoHash = "";
                FacturacionElectronicaOkWCF.ENErrorComunicacion[] ListaError = new FacturacionElectronicaOkWCF.ENErrorComunicacion[0];
                ListaError = null;
                bool result = false;
                string Archivo = null;

                LlenarEmpresa(); //Lleno Datos de la Empresa Emisora
                LlenarComprobanteNC(Op, TablaOrigen, SerieNC, Numero, ref Archivo, ReferenciaDocumentoTipo, ReferenciaDocumentoNombre, SerieRef, ReferenciaDocumentoNumero, ReferenciaDocumentoFecha);

                oGeneral.oENEmpresa = objEmpresa;
                oGeneral.oENComprobante = objComprobante;

                if (objComprobante.TipoComprobante == "01")
                {
                    result = oServicio.Registrar(oGeneral, FacturacionElectronicaOkWCF.TipoDocumento.Factura, ref cadena, tipoCodigo, ref codigoBarra, ref codigoHash, ref ListaError, 0, "1");
                }
                else if (objComprobante.TipoComprobante == "03")
                {
                    result = oServicio.Registrar(oGeneral, FacturacionElectronicaOkWCF.TipoDocumento.Boleta, ref cadena, tipoCodigo, ref codigoBarra, ref codigoHash, ref ListaError, 0, "1");
                }
                else if (objComprobante.TipoComprobante == "07")
                {
                    result = oServicio.Registrar(oGeneral, FacturacionElectronicaOkWCF.TipoDocumento.NotaCredito, ref cadena, tipoCodigo, ref codigoBarra, ref codigoHash, ref ListaError, 0, "1");
                }
                else if (objComprobante.TipoComprobante == "08")
                {
                    result = oServicio.Registrar(oGeneral, FacturacionElectronicaOkWCF.TipoDocumento.NotaDebito, ref cadena, tipoCodigo, ref codigoBarra, ref codigoHash, ref ListaError, 0, "1");
                }

                RegistrarEstado(result, TablaOrigen, Op, cadena);

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        void EnviarNotaDebitoElectronica(int Op, string TablaOrigen, string Serie, string Numero, string ReferenciaDocumentoTipo,string ReferenciaDocumentoNombre, string ReferenciaDocumentoSerie, string ReferenciaDocumentoNumero, string ReferenciaDocumentoFecha)
        {
            string SerieND = "";
            string SerieRef = "";
            string Letra = ""; 
            int id_tipoDocRef;
            try
            {
                id_tipoDocRef = int.Parse(ReferenciaDocumentoTipo);
                SerieND = ConvertirSerieND(id_tipoDocRef, Serie);

                SerieRef = ReferenciaDocumentoSerie;


                string cadena = "";
                int tipoCodigo = 1;
                byte[] codigoBarra = null;
                string codigoHash = "";
                FacturacionElectronicaOkWCF.ENErrorComunicacion[] ListaError = new FacturacionElectronicaOkWCF.ENErrorComunicacion[0];
                ListaError = null;
                bool result = false;
                string Archivo = null;


                LlenarEmpresa(); //Lleno Datos de la Empresa Emisora
                LlenarComprobanteND(Op, TablaOrigen, SerieND, Numero, ref Archivo, ReferenciaDocumentoTipo, ReferenciaDocumentoNombre, SerieRef, ReferenciaDocumentoNumero, ReferenciaDocumentoFecha);

                oGeneral.oENEmpresa = objEmpresa;
                oGeneral.oENComprobante = objComprobante;

                if (objComprobante.TipoComprobante == "01")
                {
                    result = oServicio.Registrar(oGeneral, FacturacionElectronicaOkWCF.TipoDocumento.Factura, ref cadena, tipoCodigo, ref codigoBarra, ref codigoHash, ref ListaError, 0, "1");
                }
                else if (objComprobante.TipoComprobante == "03")
                {
                    result = oServicio.Registrar(oGeneral, FacturacionElectronicaOkWCF.TipoDocumento.Boleta, ref cadena, tipoCodigo, ref codigoBarra, ref codigoHash, ref ListaError, 0, "1");
                }
                else if (objComprobante.TipoComprobante == "07")
                {
                    result = oServicio.Registrar(oGeneral, FacturacionElectronicaOkWCF.TipoDocumento.NotaCredito, ref cadena, tipoCodigo, ref codigoBarra, ref codigoHash, ref ListaError, 0, "1");
                }
                else if (objComprobante.TipoComprobante == "08")
                {
                    result = oServicio.Registrar(oGeneral, FacturacionElectronicaOkWCF.TipoDocumento.NotaDebito, ref cadena, tipoCodigo, ref codigoBarra, ref codigoHash, ref ListaError, 0, "1");
                }

                RegistrarEstado(result, TablaOrigen, Op, cadena);

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }


        void LlenarEmpresa()
        {
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
                objEmpresa.Ruc = objEmpresaGenesys.RUC.ToString(); // "20191503482"; //"20112811096"; //
                objEmpresa.RazonSocial = objEmpresaGenesys.AgendaNombre.ToString(); //   "SILVESTRE PERU S.A.C.";  //""; // \"TCI\" TRANSPORTE CONFIDENCIAL DE INFORMACION
                objEmpresa.CodigoTipoDocumento = "6";
                objEmpresa.CodDistrito = objEmpresaGenesys.CodigoSunat.ToString(); // "150131";
                objEmpresa.Correo = objEmpresaGenesys.Cliente_Correo;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        void LlenarComprobanteFactura(int Op, string TablaOrigen, string serie, string numero, ref string Archivo)
        {
            string CASO = ""; 

            FacturaElectronica2WCFClient objFacturaWCF = new FacturaElectronica2WCFClient();
            VBG04708_CABECERAResult objFacturaCabecera = new VBG04708_CABECERAResult();
            List<VBG04708_CABECERAResult> lstCabcera = new List<VBG04708_CABECERAResult>(); 
            List<VBG04708_DETALLEResult> lstDetalle = new List<VBG04708_DETALLEResult>();

            try
            {
                lstCabcera = objFacturaWCF.DocumentoFactura_Cabecera(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, TablaOrigen, Op, serie, ref Archivo).ToList();
                objFacturaCabecera = lstCabcera[0];
                lstDetalle = objFacturaWCF.DocumentoFactura_Detalle(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, TablaOrigen, Op, serie, ref Archivo).ToList();

                List<string> Multiglosa2 = new List<string>(); // Observ. de la factura

                Multiglosa2.Add(objFacturaCabecera.Leyenda_Descripcion);

                objComprobante.Serie = serie;
                objComprobante.Numero = numero; // objFacturaCabecera.Numero.ToString();

                objComprobante.TipoComprobante = "01";
                objComprobante.Moneda = objFacturaCabecera.TipoMoneda.ToString(); //"1"
                objComprobante.TipoDocumentoIdentidad = objFacturaCabecera.Cliente_TipoDocIdentidad;  //  "6";

                objComprobante.RazonSocial = objFacturaCabecera.Cliente_RazonSocial.ToString();
                objComprobante.Ruc = objFacturaCabecera.Cliente_NumeroDocIdentidad.ToString();
                objComprobante.FechaEmision = Convert.ToDateTime(objFacturaCabecera.FechaEmision); //DateTime.Now;
                objComprobante.ImporteTotal = decimal.Parse(objFacturaCabecera.ImporteTotalVenta.ToString());
                objComprobante.CorreoElectronico = objFacturaCabecera.Cliente_Correo.ToString(); //"jean.dedios@gruposilvestre.com.pe";

                FacturacionElectronicaOkWCF.ENVendedor[] Vendedorlst = new FacturacionElectronicaOkWCF.ENVendedor[1];
                FacturacionElectronicaOkWCF.ENVendedor Vendedor = new FacturacionElectronicaOkWCF.ENVendedor();
                Vendedor.Nombre = objFacturaCabecera.Vendedor.ToString();
                Vendedorlst[0] = Vendedor;
                objComprobante.Vendedor = Vendedorlst;


                // Direccion 
                FacturacionElectronicaOkWCF.ENReceptor[] DireccionLst = new FacturacionElectronicaOkWCF.ENReceptor[1];
                FacturacionElectronicaOkWCF.ENReceptor Direccion = new FacturacionElectronicaOkWCF.ENReceptor();

                Direccion.Calle = objFacturaCabecera.Cliente_Direccion;
                Direccion.Urbanizacion = objFacturaCabecera.Cliente_Urbanizacion;
                Direccion.Departamento = objFacturaCabecera.Cliente_Departamento;
                Direccion.Distrito = objFacturaCabecera.Cliente_Distrito;
                Direccion.Provincia = objFacturaCabecera.Cliente_Provincia;
                DireccionLst[0] = Direccion; 
                objComprobante.Receptor = DireccionLst; 

                FacturacionElectronicaOkWCF.ENTexto[] Textlst = new FacturacionElectronicaOkWCF.ENTexto[1];
                FacturacionElectronicaOkWCF.ENTexto Texto = new FacturacionElectronicaOkWCF.ENTexto();

                // Tipo Venta
                Texto.Texto1 = objFacturaCabecera.Tipo_Venta;
                // Glosa 
                Texto.Texto2 = objFacturaCabecera.Observaciones;

                Textlst[0] = Texto;

                objComprobante.Texto = Textlst;

                // Forma de pago
                FacturacionElectronicaOkWCF.ENFormaPago[] FormaLst = new FacturacionElectronicaOkWCF.ENFormaPago[1];
                FacturacionElectronicaOkWCF.ENFormaPago Forma = new FacturacionElectronicaOkWCF.ENFormaPago();

                Forma.NotaInstruccion = objFacturaCabecera.Forma_Pago;
                Forma.CodigoFormaPago = objFacturaCabecera.ID_Forma_Pago.ToString();
                FormaLst[0] = Forma;
                objComprobante.FormaPago = FormaLst;

                //  N° O/C
                objComprobante.NroOrdenCompra = objFacturaCabecera.SerieNumeroOV;

                //  N° Guia
                FacturacionElectronicaOkWCF.ENComprobanteGuia[] GuiaLst = new FacturacionElectronicaOkWCF.ENComprobanteGuia[1];
                FacturacionElectronicaOkWCF.ENComprobanteGuia Guia = new FacturacionElectronicaOkWCF.ENComprobanteGuia();

                Guia.Numero = objFacturaCabecera.NumeroGuia;
                Guia.Serie = objFacturaCabecera.SerieGuia;
                Guia.TipoDocReferencia = objFacturaCabecera.Guia_TipoDocumento;

                GuiaLst[0] = Guia;
                objComprobante.ComprobanteGuia = GuiaLst;



                CASO = ObtenerCasoFactura(objFacturaCabecera, lstDetalle); 

                objComprobante.ComprobanteDetalle = LlenarDetallesFac(Op, TablaOrigen, serie, ref Archivo, CASO, lstDetalle).ToArray();

                if ( CASO == "GINAFECTA" || CASO == "GGRAVADA" || CASO == "GEXONERADA")
                {
                    //Llenamos Impuestos a nivel cabecera
                    objComprobante.ComprobanteImpuestos = LlenarImpuestos(decimal.Parse(0.ToString()), decimal.Parse(0.ToString()),objFacturaCabecera.SumIGV_CodigoTributo, objFacturaCabecera.SumIGV_NombreTributoCodigo, objFacturaCabecera.SumIGV_CodigoInternacional).ToArray();

                    //Llenamos lso Montos Adicionales Obligatorios
                    objComprobante.ComprobanteMontosAdicionalesObligatorios = LlenarMontosAdicionalesObligatoriosFactura(objComprobante.ComprobanteDetalle.ToList()).ToArray();

                    objComprobante.ComprobanteMontosAdicionalesOtros = LlenarMontosAdicionalesOtrosFac( lstDetalle).ToArray();
                    objComprobante.ComprobantePropiedadesAdicionales = LlenarPropiedadesAdicionales().ToArray();
                }
                else
                {
                    //Llenamos Impuestos a nivel cabecera
                    objComprobante.ComprobanteImpuestos = LlenarImpuestos(decimal.Parse(objFacturaCabecera.SumIGV_Importe.ToString()), decimal.Parse(objFacturaCabecera.SumIGV_Importe.ToString()),objFacturaCabecera.SumIGV_CodigoTributo, objFacturaCabecera.SumIGV_NombreTributoCodigo, objFacturaCabecera.SumIGV_CodigoInternacional).ToArray();

                    //Llenamos lso Montos Adicionales Obligatorios
                    objComprobante.ComprobanteMontosAdicionalesObligatorios = LlenarMontosAdicionalesObligatoriosFactura(objComprobante.ComprobanteDetalle.ToList()).ToArray();

                }

                //objComprobante.GlosaDescuentos = GlosaDescuentos;

                //Llenamos MultiGlosa
                objComprobante.Multiglosa = Multiglosa2.ToArray();

                //Llenamos la placa del vehiculo
                FacturacionElectronicaOkWCF.ENReceptor objReceptor = new FacturacionElectronicaOkWCF.ENReceptor();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void LlenarComprobanteBoleta(int Op, string TablaOrigen, string serie, string numero, ref string Archivo)
        {
            string CASO = "";

            FacturaElectronica2WCFClient objBoletaWCF = new FacturaElectronica2WCFClient();
            VBG04711_CABECERAResult objBoletaCabecera = new VBG04711_CABECERAResult();
            List<VBG04711_CABECERAResult> lstCabcera = new List<VBG04711_CABECERAResult>();
            List<VBG04711_DETALLEResult> lstDetalle = new List<VBG04711_DETALLEResult>();

            try
            {
                lstCabcera = objBoletaWCF.DocumentoBoletas_Cabecera(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, TablaOrigen, Op, serie, "0", ref Archivo).ToList();
                objBoletaCabecera = lstCabcera[0];

                lstDetalle = objBoletaWCF.DocumentoBoletas_Detalle(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, TablaOrigen, Op, serie, "", ref Archivo).ToList();

                List<string> Multiglosa2 = new List<string>(); // Observ. de la factura

                Multiglosa2.Add(objBoletaCabecera.Leyenda_Descripcion);

                objComprobante.Serie = serie;
                objComprobante.Numero = numero; // objFacturaCabecera.Numero.ToString();

                objComprobante.TipoComprobante = "03";

                objComprobante.Moneda = objBoletaCabecera.TipoMoneda.ToString(); //"1"
                objComprobante.TipoDocumentoIdentidad = objBoletaCabecera.Cliente_TipoDocIdentidad;  //  "6";

                objComprobante.RazonSocial = objBoletaCabecera.Cliente_RazonSocial.ToString();
                objComprobante.Ruc = objBoletaCabecera.Cliente_NumeroDocIdentidad.ToString();
                objComprobante.FechaEmision = Convert.ToDateTime(objBoletaCabecera.FechaEmision); //DateTime.Now;
                objComprobante.ImporteTotal = decimal.Parse(objBoletaCabecera.ImporteTotalVenta.ToString());
                objComprobante.CorreoElectronico = objBoletaCabecera.Cliente_Correo.ToString(); //"jean.dedios@gruposilvestre.com.pe";


                // Adicionales 
                objComprobante.ClienteDireccion = objBoletaCabecera.Cliente_Direccion.ToString();

                FacturacionElectronicaOkWCF.ENVendedor[] Vendedorlst = new FacturacionElectronicaOkWCF.ENVendedor[1];
                FacturacionElectronicaOkWCF.ENVendedor Vendedor = new FacturacionElectronicaOkWCF.ENVendedor();
                Vendedor.Nombre = objBoletaCabecera.Vendedor.ToString();
                Vendedorlst[0] = Vendedor;
                objComprobante.Vendedor = Vendedorlst;

                // Direccion 
                FacturacionElectronicaOkWCF.ENReceptor[] DireccionLst = new FacturacionElectronicaOkWCF.ENReceptor[1];
                FacturacionElectronicaOkWCF.ENReceptor Direccion = new FacturacionElectronicaOkWCF.ENReceptor();

                Direccion.Calle = objBoletaCabecera.Cliente_Direccion;
                Direccion.Urbanizacion = objBoletaCabecera.Cliente_Urbanizacion;
                Direccion.Departamento = objBoletaCabecera.Cliente_Departamento;
                Direccion.Distrito = objBoletaCabecera.Cliente_Distrito;
                Direccion.Provincia = objBoletaCabecera.Cliente_Provincia;
                DireccionLst[0] = Direccion;
                objComprobante.Receptor = DireccionLst;

                FacturacionElectronicaOkWCF.ENTexto[] Textlst = new FacturacionElectronicaOkWCF.ENTexto[1];
                FacturacionElectronicaOkWCF.ENTexto Texto = new FacturacionElectronicaOkWCF.ENTexto();

                // Tipo Venta
                Texto.Texto1 = objBoletaCabecera.Tipo_Venta;
                // Glosa 
                Texto.Texto2 = objBoletaCabecera.Observaciones;


                Textlst[0] = Texto;
                objComprobante.Texto = Textlst;

                // Forma de pago
                FacturacionElectronicaOkWCF.ENFormaPago[] FormaLst = new FacturacionElectronicaOkWCF.ENFormaPago[1];
                FacturacionElectronicaOkWCF.ENFormaPago Forma = new FacturacionElectronicaOkWCF.ENFormaPago();

                Forma.NotaInstruccion = objBoletaCabecera.Forma_Pago;
                Forma.CodigoFormaPago = objBoletaCabecera.ID_Forma_Pago.ToString(); 
                FormaLst[0] = Forma;
                objComprobante.FormaPago = FormaLst;

                //  N° O/C
                objComprobante.NroOrdenCompra = objBoletaCabecera.SerieNumeroOV;

                //  N° Guia
                FacturacionElectronicaOkWCF.ENComprobanteGuia[] GuiaLst = new FacturacionElectronicaOkWCF.ENComprobanteGuia[1];
                FacturacionElectronicaOkWCF.ENComprobanteGuia Guia = new FacturacionElectronicaOkWCF.ENComprobanteGuia();

                Guia.Numero = objBoletaCabecera.NumeroGuia;
                Guia.Serie = objBoletaCabecera.SerieGuia;
                Guia.TipoDocReferencia = objBoletaCabecera.Guia_TipoDocumento;

                GuiaLst[0] = Guia;
                objComprobante.ComprobanteGuia = GuiaLst;


                //---------------------------------------------------


                CASO = ObtenerCasoBoleta(objBoletaCabecera, lstDetalle);

                objComprobante.ComprobanteDetalle = LlenarDetallesBol(Op, TablaOrigen, serie, ref Archivo, CASO, lstDetalle).ToArray();

                if (CASO == "GINAFECTA" || CASO == "GGRAVADA" || CASO == "GEXONERADA")
                {
                    //Llenamos Impuestos a nivel cabecera
                    objComprobante.ComprobanteImpuestos = LlenarImpuestos(decimal.Parse(0.ToString()), decimal.Parse(0.ToString()), objBoletaCabecera.SumIGV_CodigoTributo, objBoletaCabecera.SumIGV_NombreTributoCodigo, objBoletaCabecera.SumIGV_CodigoInternacional).ToArray();

                    //Llenamos lso Montos Adicionales Obligatorios
                    objComprobante.ComprobanteMontosAdicionalesObligatorios = LlenarMontosAdicionalesObligatoriosBoleta(objComprobante.ComprobanteDetalle.ToList()).ToArray();

                    objComprobante.ComprobanteMontosAdicionalesOtros = LlenarMontosAdicionalesOtrosBol(lstDetalle).ToArray();
                    objComprobante.ComprobantePropiedadesAdicionales = LlenarPropiedadesAdicionales().ToArray();
                }
                else
                {
                    //Llenamos Impuestos a nivel cabecera
                    objComprobante.ComprobanteImpuestos = LlenarImpuestos(decimal.Parse(objBoletaCabecera.SumIGV_Importe.ToString()), decimal.Parse(objBoletaCabecera.SumIGV_Importe.ToString()), objBoletaCabecera.SumIGV_CodigoTributo, objBoletaCabecera.SumIGV_NombreTributoCodigo, objBoletaCabecera.SumIGV_CodigoInternacional).ToArray();
                    //Llenamos lso Montos Adicionales Obligatorios
                    objComprobante.ComprobanteMontosAdicionalesObligatorios = LlenarMontosAdicionalesObligatoriosBoleta(objComprobante.ComprobanteDetalle.ToList()).ToArray();

                }

                //objComprobante.GlosaDescuentos = GlosaDescuentos;

                //Llenamos MultiGlosa
                objComprobante.Multiglosa = Multiglosa2.ToArray();

                //Llenamos la placa del vehiculo
                FacturacionElectronicaOkWCF.ENReceptor objReceptor = new FacturacionElectronicaOkWCF.ENReceptor();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void LlenarComprobanteNC(int Op, string TablaOrigen, string serie, string numero, ref string Archivo, string ReferenciaDocumentoTipo,string ReferenciaDocumentoNombre, string ReferenciaDocumentoSerie, string ReferenciaDocumentoNumero, string ReferenciaDocumentoFecha)
        {
            string CASO;

            FacturaElectronica2WCFClient objNotaCreditoWCF = new FacturaElectronica2WCFClient();
            VBG04709_CABECERAResult objNotaCreditoCabecera = new VBG04709_CABECERAResult();
            List<VBG04709_DETALLEResult> lstDetalle = new List<VBG04709_DETALLEResult>();

            try
            {
                List<VBG04709_CABECERAResult> lst = objNotaCreditoWCF.DocumentoNotaCredito_Cabecera(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, TablaOrigen, Op, serie, numero, ref Archivo).ToList();
                objNotaCreditoCabecera = lst[0];

                lstDetalle = objNotaCreditoWCF.DocumentoNotaCredito_Detalle(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, TablaOrigen, Op, serie, numero, ref Archivo).ToList();

                objComprobante.Serie = serie;


                objComprobante.Numero = numero; // Prueba borrar NC
                objComprobante.TipoComprobante = "07";
                objComprobante.Moneda = objNotaCreditoCabecera.TipoMoneda.ToString(); //"2"
                objComprobante.TipoDocumentoIdentidad = objNotaCreditoCabecera.Cliente_TipoDocIdentidad;  // "6";

                objComprobante.RazonSocial = objNotaCreditoCabecera.Cliente_RazonSocial.ToString();
                objComprobante.Ruc = objNotaCreditoCabecera.Cliente_NumeroDocIdentidad.ToString();

                if (txtIdCliente.Text.Length > 0)
                {
                    objComprobante.Ruc = txtIdCliente.Text;
                }
                if (txtCliente.Text.Length > 0)
                {
                    objComprobante.RazonSocial = txtCliente.Text;
                }

                objComprobante.FechaEmision = Convert.ToDateTime(objNotaCreditoCabecera.FechaEmision); //DateTime.Now;
                objComprobante.ImporteTotal = decimal.Parse(objNotaCreditoCabecera.ImporteTotalVenta); // 0
                objComprobante.CorreoElectronico = objNotaCreditoCabecera.Cliente_Correo.ToString(); // "jespinoza@tci.net.pe";

                // Adicionales 
                objComprobante.ClienteDireccion = objNotaCreditoCabecera.Cliente_Direccion.ToString();

                FacturacionElectronicaOkWCF.ENVendedor[] Vendedorlst = new FacturacionElectronicaOkWCF.ENVendedor[1];
                FacturacionElectronicaOkWCF.ENVendedor Vendedor = new FacturacionElectronicaOkWCF.ENVendedor();
                Vendedor.Nombre = objNotaCreditoCabecera.Vendedor.ToString();
                Vendedorlst[0] = Vendedor;
                objComprobante.Vendedor = Vendedorlst;

                //---------------------------------------------------
                // Direccion 
                FacturacionElectronicaOkWCF.ENReceptor[] DireccionLst = new FacturacionElectronicaOkWCF.ENReceptor[1];
                FacturacionElectronicaOkWCF.ENReceptor Direccion = new FacturacionElectronicaOkWCF.ENReceptor();

                Direccion.Calle = objNotaCreditoCabecera.Cliente_Direccion;
                Direccion.Urbanizacion = objNotaCreditoCabecera.Cliente_Urbanizacion;
                Direccion.Departamento = objNotaCreditoCabecera.Cliente_Departamento;
                Direccion.Distrito = objNotaCreditoCabecera.Cliente_Distrito;
                Direccion.Provincia = objNotaCreditoCabecera.Cliente_Provincia;
                DireccionLst[0] = Direccion;
                objComprobante.Receptor = DireccionLst;

                FacturacionElectronicaOkWCF.ENTexto[] Textlst = new FacturacionElectronicaOkWCF.ENTexto[1];
                FacturacionElectronicaOkWCF.ENTexto Texto = new FacturacionElectronicaOkWCF.ENTexto();

                // Tipo Venta
                //Texto.Texto1 = objNotaCreditoCabecera.Tipo_Venta;
                //// Glosa 
                //Texto.Texto2 = objNotaCreditoCabecera.Observaciones;

                //Textlst[0] = Texto;

                //objComprobante.Texto = Textlst;

                // Forma de pago
                FacturacionElectronicaOkWCF.ENFormaPago[] FormaLst = new FacturacionElectronicaOkWCF.ENFormaPago[1];
                FacturacionElectronicaOkWCF.ENFormaPago Forma = new FacturacionElectronicaOkWCF.ENFormaPago();

                //Forma.NotaInstruccion = objNotaCreditoCabecera.Forma_Pago;
                //FormaLst[0] = Forma;
                ////objComprobante.FormaPago = FormaLst;

                ////  N° O/C
                //objComprobante.NroOrdenCompra = objNotaCreditoCabecera.SerieNumeroOV;

                //  N° Guia
                FacturacionElectronicaOkWCF.ENComprobanteGuia[] GuiaLst = new FacturacionElectronicaOkWCF.ENComprobanteGuia[1];
                FacturacionElectronicaOkWCF.ENComprobanteGuia Guia = new FacturacionElectronicaOkWCF.ENComprobanteGuia();

                //Guia.Numero = objNotaCreditoCabecera.NumeroGuia;
                //Guia.Serie = objNotaCreditoCabecera.SerieGuia;
                Guia.TipoDocReferencia = objNotaCreditoCabecera.Guia_TipoDocumento;

                GuiaLst[0] = Guia;
                //objComprobante.ComprobanteGuia = GuiaLst;

                //---------------------------------------------------
                //Llenamos el Detalle()
                CASO = ObtenerCasoNC(objNotaCreditoCabecera, lstDetalle);
                lblCaso.Text = CASO;

                //Llenamos el Detalle()

                //objComprobante.ComprobanteDetalle = LlenarDetalles().ToArray();
                objComprobante.ComprobanteDetalle = LlenarDetallesNC(Op, TablaOrigen, serie, numero, ref Archivo, CASO, lstDetalle).ToArray();

                //Llenamos lso Montos Adicionales Obligatorios
                //objComprobante.ComprobanteMontosAdicionalesObligatorios = LlenarMontosAdicionalesObligatorios().ToArray();
                objComprobante.ComprobanteMontosAdicionalesObligatorios = LlenarMontosAdicionalesObligatorios(objComprobante.ComprobanteDetalle.ToList()).ToArray();

                //Llenamos Impuestos a nivel cabecera
                //objComprobante.ComprobanteImpuestos = LlenarImpuestos().ToArray();
                objComprobante.ComprobanteImpuestos = LlenarImpuestos(decimal.Parse(objNotaCreditoCabecera.SumIGV_Importe), decimal.Parse(objNotaCreditoCabecera.SumIGV_Importe),
                objNotaCreditoCabecera.SumIGV_CodigoTributo, objNotaCreditoCabecera.SumIGV_NombreTributoCodigo, objNotaCreditoCabecera.SumIGV_CodigoInternacional).ToArray();

                //Llenamos el Motivo
           
                objComprobante.ComprobanteMotivosDocumentos = LlenarMotivoDocumentoNC(objNotaCreditoCabecera.ID_TipoNC.ToString(), ReferenciaDocumentoSerie, ReferenciaDocumentoNumero, objNotaCreditoCabecera.Sustento).ToArray();

                //if (objComprobante.ComprobanteMotivosDocumentos[0].CodigoMotivoEmision == "10")
                //{
                //    objComprobante.ComprobanteOtrosDocumentos = LlenarOtrosDocumentos(ReferenciaDocumentoSerie, ReferenciaDocumentoNumero, ReferenciaDocumentoTipo, ReferenciaDocumentoFecha).ToArray();
                //    objComprobante.ComprobanteNotaCreditoDocRef = LlenarNotaDocref(ReferenciaDocumentoSerie, ReferenciaDocumentoNumero, ReferenciaDocumentoTipo, ReferenciaDocumentoFecha).ToArray();
                //}
                //else
                //{
                    objComprobante.ComprobanteNotaCreditoDocRef = LlenarNotaDocref(ReferenciaDocumentoSerie, ReferenciaDocumentoNumero, ReferenciaDocumentoTipo, ReferenciaDocumentoFecha).ToArray();
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void LlenarComprobanteND(int Op, string TablaOrigen, string serie, string numero, ref string Archivo, string ReferenciaDocumentoTipo,string ReferenciaDocumentoNombre, string ReferenciaDocumentoSerie, string ReferenciaDocumentoNumero, string ReferenciaDocumentoFecha)
        {
            string CASO;

            FacturaElectronica2WCFClient objNotaDebitoWCF = new FacturaElectronica2WCFClient();
            VBG04710_CABECERAResult objNotaDebitoCabecera = new VBG04710_CABECERAResult();
            List<VBG04710_DETALLEResult> lstDetalle = new List<VBG04710_DETALLEResult>();

            try
            {
                List<VBG04710_CABECERAResult> lstCabecera = objNotaDebitoWCF.DocumentoNotaDebito_Cabecera(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, TablaOrigen, Op, serie, numero, ref Archivo).ToList();
                objNotaDebitoCabecera = lstCabecera[0];

                lstDetalle = objNotaDebitoWCF.DocumentoNotaDebito_Detalle(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, TablaOrigen, Op, serie, numero, ref Archivo).ToList();

                objComprobante.Serie = serie;
                //objComprobante.Numero = objNotaDebitoCabecera.Numero.ToString();
                objComprobante.Numero = numero; // Prueba borrar NC
                objComprobante.TipoComprobante = "08";
                objComprobante.Moneda = objNotaDebitoCabecera.TipoMoneda.ToString(); //"2"
                objComprobante.TipoDocumentoIdentidad = objNotaDebitoCabecera.Cliente_TipoDocIdentidad; // "6";

                objComprobante.RazonSocial = objNotaDebitoCabecera.Cliente_RazonSocial.ToString();
                objComprobante.Ruc = objNotaDebitoCabecera.Cliente_NumeroDocIdentidad.ToString();

                //// Adicionales 
                objComprobante.ClienteDireccion = objNotaDebitoCabecera.Cliente_Direccion.ToString();

                FacturacionElectronicaOkWCF.ENVendedor[] Vendedorlst = new FacturacionElectronicaOkWCF.ENVendedor[1];
                FacturacionElectronicaOkWCF.ENVendedor Vendedor = new FacturacionElectronicaOkWCF.ENVendedor();
                Vendedor.Nombre = objNotaDebitoCabecera.Vendedor.ToString();
                Vendedorlst[0] = Vendedor;
                objComprobante.Vendedor = Vendedorlst;

                //FacturacionElectronicaOkWCF.ENTexto[] Textlst = new FacturacionElectronicaOkWCF.ENTexto[1];
                //FacturacionElectronicaOkWCF.ENTexto Texto = new FacturacionElectronicaOkWCF.ENTexto();

                //Texto.Texto1 = objNotaDebitoCabecera.Tipo_Venta;
                //Textlst[0] = Texto;
                //objComprobante.Texto = Textlst;

                //----------------------------------------------------

                // Direccion 
                FacturacionElectronicaOkWCF.ENReceptor[] DireccionLst = new FacturacionElectronicaOkWCF.ENReceptor[1];
                FacturacionElectronicaOkWCF.ENReceptor Direccion = new FacturacionElectronicaOkWCF.ENReceptor();

                Direccion.Calle = objNotaDebitoCabecera.Cliente_Direccion;
                Direccion.Urbanizacion = objNotaDebitoCabecera.Cliente_Urbanizacion;
                Direccion.Departamento = objNotaDebitoCabecera.Cliente_Departamento;
                Direccion.Distrito = objNotaDebitoCabecera.Cliente_Distrito;
                Direccion.Provincia = objNotaDebitoCabecera.Cliente_Provincia;
                DireccionLst[0] = Direccion;
                objComprobante.Receptor = DireccionLst;


                // --------
                FacturacionElectronicaOkWCF.ENTexto[] Textlst = new FacturacionElectronicaOkWCF.ENTexto[1];
                FacturacionElectronicaOkWCF.ENTexto Texto = new FacturacionElectronicaOkWCF.ENTexto();

                // Tipo Venta
                Texto.Texto1 = objNotaDebitoCabecera.Tipo_Venta;
                // Glosa 
                //Texto.Texto2 = objNotaDebitoCabecera.Observaciones;

                Textlst[0] = Texto;

                objComprobante.Texto = Textlst;

                // Forma de pago
                FacturacionElectronicaOkWCF.ENFormaPago[] FormaLst = new FacturacionElectronicaOkWCF.ENFormaPago[1];
                FacturacionElectronicaOkWCF.ENFormaPago Forma = new FacturacionElectronicaOkWCF.ENFormaPago();

                Forma.NotaInstruccion = objNotaDebitoCabecera.Forma_Pago;
                FormaLst[0] = Forma;
                //objComprobante.FormaPago = FormaLst;

                //  N° O/C
                //objComprobante.NroOrdenCompra = objNotaDebitoCabecera.SerieNumeroOV;

                //  N° Guia
                FacturacionElectronicaOkWCF.ENComprobanteGuia[] GuiaLst = new FacturacionElectronicaOkWCF.ENComprobanteGuia[1];
                FacturacionElectronicaOkWCF.ENComprobanteGuia Guia = new FacturacionElectronicaOkWCF.ENComprobanteGuia();

                //Guia.Numero = objNotaDebitoCabecera.NumeroGuia;
                //Guia.Serie = objNotaDebitoCabecera.SerieGuia;
                //Guia.TipoDocReferencia = objNotaDebitoCabecera.Guia_TipoDocumento;

                //GuiaLst[0] = Guia;
                //objComprobante.ComprobanteGuia = GuiaLst;


                ////---------------------------------------------------

                if (txtIdCliente.Text.Length > 0)
                {
                    objComprobante.Ruc = txtIdCliente.Text;
                }
                if (txtCliente.Text.Length > 0)
                {
                    objComprobante.RazonSocial = txtCliente.Text;
                }


                objComprobante.FechaEmision = Convert.ToDateTime(objNotaDebitoCabecera.FechaEmision); //DateTime.Now;
                objComprobante.ImporteTotal = decimal.Parse(objNotaDebitoCabecera.ImporteTotalVenta); // 0
                objComprobante.CorreoElectronico = objNotaDebitoCabecera.Cliente_Correo.ToString(); // "jespinoza@tci.net.pe";

                //Llenamos el Detalle()
                CASO = ObtenerCasoND(objNotaDebitoCabecera, lstDetalle);
                lblCaso.Text = CASO; 

                //objComprobante.ComprobanteDetalle = LlenarDetalles().ToArray();
                objComprobante.ComprobanteDetalle = LlenarDetallesND(Op, TablaOrigen, serie, numero, ref Archivo, CASO, lstDetalle).ToArray();

                //Llenamos lso Montos Adicionales Obligatorios
                //objComprobante.ComprobanteMontosAdicionalesObligatorios = LlenarMontosAdicionalesObligatorios().ToArray();
                objComprobante.ComprobanteMontosAdicionalesObligatorios = LlenarMontosAdicionalesObligatorios(objComprobante.ComprobanteDetalle.ToList()).ToArray();

                //Llenamos Impuestos a nivel cabecera
                //objComprobante.ComprobanteImpuestos = LlenarImpuestos().ToArray();
                objComprobante.ComprobanteImpuestos = LlenarImpuestos(decimal.Parse(objNotaDebitoCabecera.SumIGV_Importe), decimal.Parse(objNotaDebitoCabecera.SumIGV_Importe),
                    objNotaDebitoCabecera.SumIGV_CodigoTributo, objNotaDebitoCabecera.SumIGV_NombreTributoCodigo, objNotaDebitoCabecera.SumIGV_CodigoInternacional).ToArray();

                //Llenamos el Motivo

                objComprobante.ComprobanteMotivosDocumentos = LlenarMotivoDocumentoND(objNotaDebitoCabecera.ID_TipoND.ToString(), ReferenciaDocumentoSerie, ReferenciaDocumentoNumero, objNotaDebitoCabecera.Sustento).ToArray();


                if (objComprobante.ComprobanteMotivosDocumentos[0].CodigoMotivoEmision != "03")
                {
                    objComprobante.ComprobanteNotaCreditoDocRef = LlenarNotaDocref(ReferenciaDocumentoSerie, ReferenciaDocumentoNumero, ReferenciaDocumentoTipo, ReferenciaDocumentoFecha).ToArray();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        string ObtenerCasoFactura(VBG04708_CABECERAResult objFacturaCabecera, List<VBG04708_DETALLEResult> lstDetalle)
        {
            string CASO = "";
            try
            {
                if (objFacturaCabecera.Leyenda_Codigo == "1002" & decimal.Parse(objFacturaCabecera.SumIGV_Importe.ToString()) > 0)
                {
                    CASO = "GGRAVADA"; // CON IGV
                }
                else if (objFacturaCabecera.Leyenda_Codigo == "1002" & decimal.Parse(objFacturaCabecera.SumIGV_Importe.ToString()) == 0)
                {
                    foreach (VBG04708_DETALLEResult obj in lstDetalle)
                    {
                        if (obj.TieneImpuesto == 0)
                        {
                            CASO = "GINAFECTA";  // SIN IGV
                        }
                        else
                        {
                            CASO = "GEXONERADA";  // CON IGV 
                        }
                    }
                }
                else
                {
                    if (decimal.Parse(objFacturaCabecera.SumIGV_Importe.ToString()) > 0)
                    {
                        CASO = "GRAVADA"; // CON IGV
                    }
                    else
                    {
                        foreach (VBG04708_DETALLEResult obj in lstDetalle)
                        {
                            if (obj.TieneImpuesto == 0)
                            {
                                CASO = "INAFECTA";  // SIN IGV
                            }
                            else
                            {
                                CASO = "EXONERADA";   // CON IGV 
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CASO;
        }
        string ObtenerCasoBoleta(VBG04711_CABECERAResult objFacturaCabecera, List<VBG04711_DETALLEResult> lstDetalle)
        {
            string CASO = "";
            try
            {
                if (objFacturaCabecera.Leyenda_Codigo == "1002" & decimal.Parse(objFacturaCabecera.SumIGV_Importe.ToString()) > 0)
                {
                    CASO = "GGRAVADA"; // CON IGV
                }
                else if (objFacturaCabecera.Leyenda_Codigo == "1002" & decimal.Parse(objFacturaCabecera.SumIGV_Importe.ToString()) == 0)
                {
                    foreach (VBG04711_DETALLEResult obj in lstDetalle)
                    {
                        if (obj.TieneImpuesto == 0)
                        {
                            
                            CASO = "GINAFECTA";  // SIN IGV
                        }
                        else
                        {
                            CASO = "GEXONERADA";   // CON IGV 
                        }

                    }
                }
                else
                {
                    if (decimal.Parse(objFacturaCabecera.SumIGV_Importe.ToString()) > 0)
                    {
                        CASO = "GRAVADA"; // CON IGV
                    }
                    else
                    {
                        foreach (VBG04711_DETALLEResult obj in lstDetalle)
                        {
                            if (obj.TieneImpuesto == 0)
                            {
                                CASO = "INAFECTA";  // SIN IGV
                               
                            }
                            else
                            {
                                CASO = "EXONERADA";   // CON IGV 
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CASO;
        }
        string ObtenerCasoND(VBG04710_CABECERAResult objNDCabecera, List<VBG04710_DETALLEResult> lstDetalle)
        {
            string CASO = "";
            try
            {
                if (objNDCabecera.Leyenda_Codigo == "1002" & decimal.Parse(objNDCabecera.SumIGV_Importe.ToString()) > 0)
                {
                    CASO = "GGRAVADA"; // CON IGV
                }
                else if (objNDCabecera.Leyenda_Codigo == "1002" & decimal.Parse(objNDCabecera.SumIGV_Importe.ToString()) == 0)
                {
                    foreach (VBG04710_DETALLEResult obj in lstDetalle)
                    {
                        if (obj.TieneImpuesto == 0)
                        {
                            
                            CASO = "GINAFECTA";  // SIN IGV
                        }
                        else
                        {
                            CASO = "GEXONERADA";  // CON IGV 
                        }

                    }
                }
                else
                {
                    if (decimal.Parse(objNDCabecera.SumIGV_Importe.ToString()) > 0)
                    {
                        CASO = "GRAVADA"; // CON IGV
                    }
                    else
                    {
                        foreach (VBG04710_DETALLEResult obj in lstDetalle)
                        {
                            if (obj.TieneImpuesto == 0)
                            {
                               
                                CASO = "INAFECTA";  // SIN IGV
                            }
                            else
                            {
                                CASO = "EXONERADA";  // SIN IGV 
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CASO;
        }
        string ObtenerCasoNC(VBG04709_CABECERAResult objNDCabecera, List<VBG04709_DETALLEResult> lstDetalle)
        {
            string CASO = "";
            try
            {
                if (objNDCabecera.Leyenda_Codigo == "1002" & decimal.Parse(objNDCabecera.SumIGV_Importe.ToString()) > 0)
                {
                    CASO = "GGRAVADA"; // CON IGV
                }
                else if (objNDCabecera.Leyenda_Codigo == "1002" & decimal.Parse(objNDCabecera.SumIGV_Importe.ToString()) == 0)
                {
                    foreach (VBG04709_DETALLEResult obj in lstDetalle)
                    {
                        if (obj.TieneImpuesto == 0)
                        {
                           
                            CASO = "GINAFECTA";  // SIN IGV
                        }
                        else
                        {
                            CASO = "GEXONERADA";  // CON IGV 
                        }

                    }
                }
                else
                {
                    if (decimal.Parse(objNDCabecera.SumIGV_Importe.ToString()) > 0)
                    {
                        CASO = "GRAVADA"; // CON IGV
                    }
                    else
                    {
                        foreach (VBG04709_DETALLEResult obj in lstDetalle)
                        {
                            if (obj.TieneImpuesto == 0)
                            {
                               
                                CASO = "INAFECTA";  // SIN IGV
                            }
                            else
                            {
                                CASO = "EXONERADA";   // CON IGV  
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CASO;
        }


        List<FacturacionElectronicaOkWCF.ENComprobanteDetalle> LlenarDetallesFac(int Op, string TablaOrigen, string serie, ref string Archivo, string CASO, List<VBG04708_DETALLEResult> lstDetalle)
        {

            List<FacturacionElectronicaOkWCF.ENComprobanteDetalle> ListaDetalleComprobante = new List<FacturacionElectronicaOkWCF.ENComprobanteDetalle>();
            FacturacionElectronicaOkWCF.ENComprobanteDetalle objENDetalle; 

            try
            {
                int x = 1;
                foreach (VBG04708_DETALLEResult objFacturaDetalle in lstDetalle)
                {
                    objENDetalle = new FacturacionElectronicaOkWCF.ENComprobanteDetalle();
                    objENDetalle.Item = x; // 1;
                    objENDetalle.Cantidad = decimal.Parse(objFacturaDetalle.Linea_Cantidad);  // 1;
                    objENDetalle.Codigo = objFacturaDetalle.Linea_CodigoProducto;  // "08003               ";
                    objENDetalle.Descripcion = objFacturaDetalle.Linea_Descripcion;   // "Toyota Corolla 2015";

                    objENDetalle.CodigoTipoPrecio = objFacturaDetalle.Linea_TipoPrecio; //"01";

                    // Unidad de medida 
                    objENDetalle.UnidadMedidaEmisor = objFacturaDetalle.Linea_UnidadMedida; 

                    if (CASO == "GINAFECTA" || CASO == "GGRAVADA" || CASO == "GEXONERADA")
                    {
                        objENDetalle.ValorVentaUnitarioIncIgv = decimal.Round((decimal.Parse(objFacturaDetalle.Linea_ValorUnitario) * decimal.Parse(1.18.ToString())), 2);  // 118;
                        objENDetalle.ValorVentaUnitario = 0; 
                        objENDetalle.Total = 0;
                    }
                    else
                    {
                        if (CASO == "INAFECTA" || CASO == "EXONERADA")
                        {
                            objENDetalle.ValorVentaUnitarioIncIgv = decimal.Round(decimal.Parse(objFacturaDetalle.Linea_ValorUnitario), 2);  // 118;
                            objENDetalle.ValorVentaUnitario =       decimal.Round(decimal.Parse(objFacturaDetalle.Linea_ValorUnitario), 2);
                            objENDetalle.Total = decimal.Round((decimal.Parse(objFacturaDetalle.Linea_ValorVenta.ToString())), 2);
                        }
                        else
                        {
                            objENDetalle.ValorVentaUnitarioIncIgv = decimal.Round((decimal.Parse(objFacturaDetalle.Linea_ValorUnitario) * decimal.Parse(1.18.ToString())), 2);  // 118;
                            objENDetalle.ValorVentaUnitario = decimal.Round(decimal.Parse(objFacturaDetalle.Linea_ValorUnitario), 2);
                            objENDetalle.Total = decimal.Round((decimal.Parse(objFacturaDetalle.Linea_ValorVenta.ToString())), 2);
                        }
                    }

                    objENDetalle.Determinante = objFacturaDetalle.Determinante;
                    objENDetalle.UnidadComercial = objFacturaDetalle.Linea_UnidadMedida; // "NIU";

                    
                    objENDetalle.ComprobanteDetalleImpuestos = LlenarDetalleImpuesto(objFacturaDetalle.Linea_IGVTipoAfectacion, objFacturaDetalle.Linea_IGVCodigoTributo, objFacturaDetalle.Linea_IGVCodigoInternacional,
                       decimal.Parse(objFacturaDetalle.Linea_IGVImporte.ToString()), decimal.Parse(objFacturaDetalle.Linea_IGVImporte.ToString()), objFacturaDetalle.Linea_IGVNombreTributoCodigo, CASO).ToArray();


                    ListaDetalleComprobante.Add(objENDetalle);

                    x = x + 1; 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListaDetalleComprobante;
        }
        List<FacturacionElectronicaOkWCF.ENComprobanteDetalle> LlenarDetallesBol(int Op, string TablaOrigen, string serie, ref string Archivo, string CASO, List<VBG04711_DETALLEResult> lstDetalle)
        {

            List<FacturacionElectronicaOkWCF.ENComprobanteDetalle> ListaDetalleComprobante = new List<FacturacionElectronicaOkWCF.ENComprobanteDetalle>();
            FacturacionElectronicaOkWCF.ENComprobanteDetalle objENDetalle;

            try
            {
                int x = 1;
                foreach (VBG04711_DETALLEResult objBoletaDetalle in lstDetalle)
                {
                    objENDetalle = new FacturacionElectronicaOkWCF.ENComprobanteDetalle();
                    objENDetalle.Item = x; // 1;
                    objENDetalle.Cantidad = decimal.Parse(objBoletaDetalle.Linea_Cantidad);  // 1;
                    objENDetalle.Codigo = objBoletaDetalle.Linea_CodigoProducto;  // "08003               ";
                    objENDetalle.Descripcion = objBoletaDetalle.Linea_Descripcion;   // "Toyota Corolla 2015";

                    objENDetalle.UnidadMedidaEmisor = objBoletaDetalle.Linea_UnidadMedida;

                    objENDetalle.CodigoTipoPrecio = objBoletaDetalle.Linea_TipoPrecio; //"01";

                    if (CASO == "GINAFECTA" || CASO == "GGRAVADA" || CASO == "GEXONERADA")
                    {
                        objENDetalle.ValorVentaUnitarioIncIgv = decimal.Round((decimal.Parse(objBoletaDetalle.Linea_ValorUnitario) * decimal.Parse(1.18.ToString())), 2);  // 118;
                        objENDetalle.ValorVentaUnitario = 0;
                        objENDetalle.Total = 0;
                    }
                    else
                    {
                        if (CASO == "INAFECTA" || CASO == "EXONERADA")
                        {
                            objENDetalle.ValorVentaUnitarioIncIgv = decimal.Round(decimal.Parse(objBoletaDetalle.Linea_ValorUnitario), 2);  // 118;
                            objENDetalle.ValorVentaUnitario = decimal.Round(decimal.Parse(objBoletaDetalle.Linea_ValorUnitario), 2);
                            objENDetalle.Total = decimal.Round((decimal.Parse(objBoletaDetalle.Linea_ValorVenta.ToString())), 2);
                        }
                        else
                        {
                            objENDetalle.ValorVentaUnitarioIncIgv = decimal.Round((decimal.Parse(objBoletaDetalle.Linea_ValorUnitario) * decimal.Parse(1.18.ToString())), 4);  // 118;
                            objENDetalle.ValorVentaUnitario = decimal.Round(decimal.Parse(objBoletaDetalle.Linea_ValorUnitario), 2);
                            objENDetalle.Total = decimal.Round((decimal.Parse(objBoletaDetalle.Linea_ValorVenta.ToString())), 2);
                        }
                    }

                    objENDetalle.Determinante = objBoletaDetalle.Determinante;
                    objENDetalle.UnidadComercial = objBoletaDetalle.Linea_UnidadMedida; // "NIU";


                    objENDetalle.ComprobanteDetalleImpuestos = LlenarDetalleImpuesto(objBoletaDetalle.Linea_IGVTipoAfectacion, objBoletaDetalle.Linea_IGVCodigoTributo, objBoletaDetalle.Linea_IGVCodigoInternacional,
                       decimal.Parse(objBoletaDetalle.Linea_IGVImporte.ToString()), decimal.Parse(objBoletaDetalle.Linea_IGVImporte.ToString()), objBoletaDetalle.Linea_IGVNombreTributoCodigo, CASO).ToArray();


                    ListaDetalleComprobante.Add(objENDetalle);

                    x = x + 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ListaDetalleComprobante;

        }
        List<FacturacionElectronicaOkWCF.ENComprobanteDetalle> LlenarDetallesNC(int Op, string TablaOrigen, string serie,string Numero, ref string Archivo, string CASO, List<VBG04709_DETALLEResult> lstDetalle)
        {

            List<FacturacionElectronicaOkWCF.ENComprobanteDetalle> ListaDetalleComprobante = new List<FacturacionElectronicaOkWCF.ENComprobanteDetalle>();
            FacturacionElectronicaOkWCF.ENComprobanteDetalle objENDetalle;

            FacturaElectronica2WCFClient objFacturaWCF = new FacturaElectronica2WCFClient();
            List<VBG04709_DETALLEResult> lst = new List<VBG04709_DETALLEResult>();
            try
            {
                lst = lstDetalle; 

                int x = 1;
                foreach (VBG04709_DETALLEResult objNCDetalle in lst)
                {
                    objENDetalle = new FacturacionElectronicaOkWCF.ENComprobanteDetalle();
                    objENDetalle.Item = x; // 1;
                    objENDetalle.Cantidad = decimal.Parse(objNCDetalle.Linea_Cantidad);  // 1;
                    objENDetalle.Codigo = objNCDetalle.Linea_CodigoProducto;  // "08003               ";
                    objENDetalle.Descripcion = objNCDetalle.Linea_Descripcion;   // "Toyota Corolla 2015";

                    objENDetalle.UnidadMedidaEmisor = objNCDetalle.Linea_UnidadMedida;

                    objENDetalle.CodigoTipoPrecio = objNCDetalle.Linea_TipoPrecio; //"01";

                    if (CASO == "GINAFECTA" || CASO == "GGRAVADA" || CASO == "GEXONERADA")
                    {
                        objENDetalle.ValorVentaUnitarioIncIgv = decimal.Round((decimal.Parse(objNCDetalle.Linea_ValorUnitario) * decimal.Parse(1.18.ToString())), 2);  // 118;
                        objENDetalle.ValorVentaUnitario = 0;
                        objENDetalle.Total = 0;
                    }
                    else
                    {
                        if (CASO == "INAFECTA" || CASO == "EXONERADA")
                        {
                            objENDetalle.ValorVentaUnitarioIncIgv = decimal.Round(decimal.Parse(objNCDetalle.Linea_ValorUnitario), 2);  // 118;
                            objENDetalle.ValorVentaUnitario = decimal.Round(decimal.Parse(objNCDetalle.Linea_ValorUnitario), 2);
                            objENDetalle.Total = decimal.Round((objENDetalle.ValorVentaUnitarioIncIgv * objENDetalle.Cantidad), 2);
                        }
                        else
                        {
                            objENDetalle.ValorVentaUnitarioIncIgv = decimal.Round((decimal.Parse(objNCDetalle.Linea_ValorUnitario) * decimal.Parse(1.18.ToString())), 2);  // 118;
                            objENDetalle.ValorVentaUnitario = decimal.Round(decimal.Parse(objNCDetalle.Linea_ValorUnitario), 2);
                            objENDetalle.Total = decimal.Round((decimal.Parse(objNCDetalle.Linea_ValorVenta.ToString())), 2);
                        }
                    }

                    objENDetalle.Determinante = "1";

                    objENDetalle.UnidadComercial = objNCDetalle.Linea_UnidadMedida; // "NIU";

                    objENDetalle.ComprobanteDetalleImpuestos = LlenarDetalleImpuesto(objNCDetalle.Linea_IGVTipoAfectacion, objNCDetalle.Linea_IGVCodigoTributo, objNCDetalle.Linea_IGVCodigoInternacional,
                       decimal.Parse(objNCDetalle.Linea_IGVImporte), decimal.Parse(objNCDetalle.Linea_IGVImporte), objNCDetalle.Linea_IGVNombreTributoCodigo, CASO).ToArray();

                    objENDetalle.PorcentajeDescuento = double.Parse(objNCDetalle.Linea_Dcto.ToString());  //   150.00;
                    ListaDetalleComprobante.Add(objENDetalle);

                    x = x + 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return ListaDetalleComprobante;

        }
        List<FacturacionElectronicaOkWCF.ENComprobanteDetalle> LlenarDetallesND(int Op, string TablaOrigen, string serie, string Numero, ref string Archivo, string CASO, List<VBG04710_DETALLEResult> lstDetalle)
        {
            List<FacturacionElectronicaOkWCF.ENComprobanteDetalle> ListaDetalleComprobante = new List<FacturacionElectronicaOkWCF.ENComprobanteDetalle>();
            FacturacionElectronicaOkWCF.ENComprobanteDetalle objENDetalle;

            FacturaElectronica2WCFClient objNotaDebitoWCF = new FacturaElectronica2WCFClient();
            List<VBG04710_DETALLEResult> lst = new List<VBG04710_DETALLEResult>();
            try
            {
                lst = lstDetalle; 
                int x = 1;
                foreach (VBG04710_DETALLEResult objNDDetalle in lst)
                {
                    objENDetalle = new FacturacionElectronicaOkWCF.ENComprobanteDetalle();
                    objENDetalle.Item = x; // 1;
                    objENDetalle.Cantidad = decimal.Parse(objNDDetalle.Linea_Cantidad);  // 1;
                    objENDetalle.Codigo = objNDDetalle.Linea_CodigoProducto;  // "08003               ";
                    objENDetalle.Descripcion = objNDDetalle.Linea_Descripcion;   // "Toyota Corolla 2015";

                    objENDetalle.UnidadMedidaEmisor = objNDDetalle.Linea_UnidadMedida;

                    objENDetalle.CodigoTipoPrecio = objNDDetalle.Linea_TipoPrecio; //"01";

                    if (CASO == "GINAFECTA" || CASO == "GGRAVADA" || CASO == "GEXONERADA")
                    {
                        objENDetalle.ValorVentaUnitarioIncIgv = decimal.Round((decimal.Parse(objNDDetalle.Linea_ValorUnitario) * decimal.Parse(1.18.ToString())), 2);  // 118;
                        objENDetalle.ValorVentaUnitario = 0;
                        objENDetalle.Total = 0;
                    }
                    else
                    {
                        if (CASO == "INAFECTA" || CASO == "EXONERADA")
                        {
                            objENDetalle.ValorVentaUnitarioIncIgv = decimal.Round(decimal.Parse(objNDDetalle.Linea_ValorUnitario), 2);  // 118;
                            objENDetalle.ValorVentaUnitario =       decimal.Round(decimal.Parse(objNDDetalle.Linea_ValorUnitario), 2);
                            objENDetalle.Total = decimal.Round((objENDetalle.ValorVentaUnitarioIncIgv * objENDetalle.Cantidad), 2);
                        }
                        else
                        {
                            objENDetalle.ValorVentaUnitarioIncIgv = decimal.Round((decimal.Parse(objNDDetalle.Linea_ValorUnitario) * decimal.Parse(1.18.ToString())), 2);  // 118;
                            objENDetalle.ValorVentaUnitario = decimal.Round(decimal.Parse(objNDDetalle.Linea_ValorUnitario), 2);
                            objENDetalle.Total = decimal.Round((decimal.Parse(objNDDetalle.Linea_ValorVenta.ToString())), 2);
                        }
                    }

                    objENDetalle.Determinante = "1";

                    objENDetalle.UnidadComercial = objNDDetalle.Linea_UnidadMedida; // "NIU";

                    objENDetalle.ComprobanteDetalleImpuestos = LlenarDetalleImpuesto(objNDDetalle.Linea_IGVTipoAfectacion, objNDDetalle.Linea_IGVCodigoTributo, objNDDetalle.Linea_IGVCodigoInternacional,
                       decimal.Parse(objNDDetalle.Linea_IGVImporte), decimal.Parse(objNDDetalle.Linea_IGVImporte), objNDDetalle.Linea_IGVNombreTributoCodigo, CASO).ToArray();

                    objENDetalle.PorcentajeDescuento = double.Parse(objNDDetalle.Linea_Dcto.ToString());  //   150.00;


                    ListaDetalleComprobante.Add(objENDetalle);

                    x = x + 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return ListaDetalleComprobante;

        }


        List<FacturacionElectronicaOkWCF.ENComprobanteImpuestos> LlenarImpuestos(decimal ImporteExplicito, decimal ImporteTributo, string CodigoTributo, string DesTributo, string CodigoUN )
        {
            List<FacturacionElectronicaOkWCF.ENComprobanteImpuestos> ListaImpuestos = new List<FacturacionElectronicaOkWCF.ENComprobanteImpuestos>();
            FacturacionElectronicaOkWCF.ENComprobanteImpuestos objImpuestos = new FacturacionElectronicaOkWCF.ENComprobanteImpuestos();

            objImpuestos.ImporteExplicito = ImporteExplicito;  //  18;
            objImpuestos.ImporteTributo = ImporteTributo;  //  18;
            objImpuestos.CodigoTributo = CodigoTributo;// "1000";
            objImpuestos.DesTributo = DesTributo; // "IGV";
            objImpuestos.CodigoUN = CodigoUN; // "VAT";

            ListaImpuestos.Add(objImpuestos);
            return ListaImpuestos;
        }
        List<FacturacionElectronicaOkWCF.ENComprobanteDetalleImpuestos> LlenarDetalleImpuesto(  string AfectacionIGV, string CodigoTributo, string CodigoUN, decimal ImporteTributo, decimal ImporteExplicito, string DesTributo, string CASO)
        {

            List<FacturacionElectronicaOkWCF.ENComprobanteDetalleImpuestos> ListaDetalleImpuesto = new List<FacturacionElectronicaOkWCF.ENComprobanteDetalleImpuestos>();
            FacturacionElectronicaOkWCF.ENComprobanteDetalleImpuestos objDetalleImpuestos = new FacturacionElectronicaOkWCF.ENComprobanteDetalleImpuestos();

            if (CASO == "GINAFECTA")
            {
                objDetalleImpuestos.AfectacionIGV = "31"; 
            }
            else if (CASO == "GGRAVADA")
            {
                objDetalleImpuestos.AfectacionIGV = "15";   
            }
            else if (CASO == "GEXONERADA")
            {
                objDetalleImpuestos.AfectacionIGV = "21";
            }
            else if (CASO == "INAFECTA")
            {
                objDetalleImpuestos.AfectacionIGV = "30";
            }
            else if (CASO == "GRAVADA")
            {
                objDetalleImpuestos.AfectacionIGV = "10";
            }
            else if (CASO == "EXONERADA")
            {
                objDetalleImpuestos.AfectacionIGV = "20";
            }

            objDetalleImpuestos.CodigoTributo = CodigoTributo; //  "1000";
            objDetalleImpuestos.CodigoUN = CodigoUN; //  "VAT";
            objDetalleImpuestos.ImporteTributo = ImporteTributo; // 18;
            objDetalleImpuestos.ImporteExplicito = ImporteExplicito; //  18;
            objDetalleImpuestos.DesTributo = DesTributo; // "IGV";

            ListaDetalleImpuesto.Add(objDetalleImpuestos);

            return ListaDetalleImpuesto;

        }
        List<FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesObligatorios> LlenarMontosAdicionalesObligatoriosFactura(List<FacturacionElectronicaOkWCF.ENComprobanteDetalle> ListaDetalle )
        {
            List<FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesObligatorios> ListaMontosAdicionalesObligatorios = new List<FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesObligatorios>();
            FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesObligatorios objMontosObligatorios = new FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesObligatorios();

            for (int x = 0; x < 3; x++)
            {
                objMontosObligatorios = new FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesObligatorios();
                if (x == 0)
                {
                    objMontosObligatorios.Codigo = "1001";
                    objMontosObligatorios.Monto = 0 ;

                    foreach (FacturacionElectronicaOkWCF.ENComprobanteDetalle Item in ListaDetalle)
                    {
                        if (Item.ComprobanteDetalleImpuestos[0].AfectacionIGV == "10")
                        {
                            objMontosObligatorios.Monto = objMontosObligatorios.Monto + Item.Total;
                        }
                    }

                }
                else if (x == 1)
                {
                    objMontosObligatorios.Codigo = "1002";
                    objMontosObligatorios.Monto = 0;

                    foreach (FacturacionElectronicaOkWCF.ENComprobanteDetalle Item in ListaDetalle)
                    {
                        if (Item.ComprobanteDetalleImpuestos[0].AfectacionIGV == "30")
                        {
                            objMontosObligatorios.Monto = objMontosObligatorios.Monto + Item.Total;
                        }
                    }

                }
                else
                {
                    objMontosObligatorios.Codigo ="1003";
                    objMontosObligatorios.Monto = 0;

                    foreach (FacturacionElectronicaOkWCF.ENComprobanteDetalle Item in ListaDetalle)
                    {
                        if (Item.ComprobanteDetalleImpuestos[0].AfectacionIGV == "20")
                        {
                            objMontosObligatorios.Monto = objMontosObligatorios.Monto + Item.Total;
                        }
                    }
                }

                ListaMontosAdicionalesObligatorios.Add(objMontosObligatorios);
            }

            return ListaMontosAdicionalesObligatorios;
        }
        List<FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesObligatorios> LlenarMontosAdicionalesObligatoriosBoleta(List<FacturacionElectronicaOkWCF.ENComprobanteDetalle> ListaDetalle)
        {
            List<FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesObligatorios> ListaMontosAdicionalesObligatorios = new List<FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesObligatorios>();
            FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesObligatorios objMontosObligatorios = new FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesObligatorios();

            for (int x = 0; x < 3; x++)
            {
                objMontosObligatorios = new FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesObligatorios();
                if (x == 0)
                {
                    objMontosObligatorios.Codigo = "1001";
                    objMontosObligatorios.Monto = 0;

                    foreach (FacturacionElectronicaOkWCF.ENComprobanteDetalle Item in ListaDetalle)
                    {
                        if (Item.ComprobanteDetalleImpuestos[0].AfectacionIGV == "10")
                        {
                            objMontosObligatorios.Monto = objMontosObligatorios.Monto + Item.Total;
                        }
                    }

                }
                else if (x == 1)
                {
                    objMontosObligatorios.Codigo = "1002";
                    objMontosObligatorios.Monto = 0;

                    foreach (FacturacionElectronicaOkWCF.ENComprobanteDetalle Item in ListaDetalle)
                    {
                        if (Item.ComprobanteDetalleImpuestos[0].AfectacionIGV == "30")
                        {
                            objMontosObligatorios.Monto = objMontosObligatorios.Monto + Item.Total;
                        }
                    }

                }
                else
                {
                    objMontosObligatorios.Codigo = "1003";
                    objMontosObligatorios.Monto = 0;

                    foreach (FacturacionElectronicaOkWCF.ENComprobanteDetalle Item in ListaDetalle)
                    {
                        if (Item.ComprobanteDetalleImpuestos[0].AfectacionIGV == "20")
                        {
                            objMontosObligatorios.Monto = objMontosObligatorios.Monto + Item.Total;
                        }
                    }
                }

                ListaMontosAdicionalesObligatorios.Add(objMontosObligatorios);
            }

            return ListaMontosAdicionalesObligatorios;
        }


        List<FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesObligatorios> LlenarMontosAdicionalesObligatorios(List<FacturacionElectronicaOkWCF.ENComprobanteDetalle> ListaDetalle)
        {
            List<FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesObligatorios> ListaMontosAdicionalesObligatorios = new List<FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesObligatorios>();
            FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesObligatorios objMontosObligatorios = new FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesObligatorios();

            for (int x = 0; x < 3; x++)
            {
                objMontosObligatorios = new FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesObligatorios();
                if (x == 0)
                {
                    objMontosObligatorios.Codigo = "1001";
                    objMontosObligatorios.Monto = 0;

                    foreach (FacturacionElectronicaOkWCF.ENComprobanteDetalle Item in ListaDetalle)
                    {
                        if (Item.ComprobanteDetalleImpuestos[0].AfectacionIGV == "10")
                        {
                            objMontosObligatorios.Monto = objMontosObligatorios.Monto + Item.Total;
                        }
                    }
                }
                else if (x == 1)
                {
                    objMontosObligatorios.Codigo = Convert.ToString("1002");
                    objMontosObligatorios.Monto = 0;

                    foreach (FacturacionElectronicaOkWCF.ENComprobanteDetalle Item in ListaDetalle)
                    {
                        if (Item.ComprobanteDetalleImpuestos[0].AfectacionIGV == "30")
                        {
                            objMontosObligatorios.Monto = objMontosObligatorios.Monto + Item.Total;
                        }
                    }

                }
                else
                {
                    objMontosObligatorios.Codigo = Convert.ToString("1003");
                    objMontosObligatorios.Monto = 0;
                    foreach (FacturacionElectronicaOkWCF.ENComprobanteDetalle Item in ListaDetalle)
                    {
                        if (Item.ComprobanteDetalleImpuestos[0].AfectacionIGV == "20")
                        {
                            objMontosObligatorios.Monto = objMontosObligatorios.Monto + Item.Total;
                        }
                    }
                }

                ListaMontosAdicionalesObligatorios.Add(objMontosObligatorios);
            }

            return ListaMontosAdicionalesObligatorios;
        }
        List<FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesOtros> LlenarMontosAdicionalesOtrosFac(List<VBG04708_DETALLEResult> lstDetalle)
        {
            List<FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesOtros> ListaMontosAdicionalesOtros = new List<FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesOtros>();
            FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesOtros objMontosOtros = new FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesOtros();
            decimal PrecioIGV = 0;
            decimal Cantidad = 0;
            decimal ValorVentaG = 0; 
            try
            {
                objMontosOtros = new FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesOtros();
                objMontosOtros.Codigo = "1004";
                objMontosOtros.Monto = 0;
                foreach (VBG04708_DETALLEResult Item in lstDetalle)
                {
                    objMontosOtros.Codigo = "1004";
                    PrecioIGV = 0;
                    Cantidad = 0;
                    ValorVentaG = 0; 

                    PrecioIGV = decimal.Round((decimal.Parse(Item.Linea_ValorUnitario) * decimal.Parse(1.18.ToString())), 4);
                    Cantidad = decimal.Parse(Item.Linea_Cantidad);
                    ValorVentaG = PrecioIGV * Cantidad; 

                    objMontosOtros.Monto = objMontosOtros.Monto + decimal.Round(ValorVentaG, 4);
                }

                ListaMontosAdicionalesOtros.Add(objMontosOtros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListaMontosAdicionalesOtros;
        }
        List<FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesOtros> LlenarMontosAdicionalesOtrosBol(List<VBG04711_DETALLEResult> lstDetalle)
        {
            List<FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesOtros> ListaMontosAdicionalesOtros = new List<FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesOtros>();
            FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesOtros objMontosOtros = new FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesOtros();
            decimal PrecioIGV = 0;
            decimal Cantidad = 0;
            decimal ValorVentaG = 0;
            try
            {
                objMontosOtros = new FacturacionElectronicaOkWCF.ENComprobanteMontosAdicionalesOtros();
                objMontosOtros.Codigo = "1004";
                objMontosOtros.Monto = 0;
                foreach (VBG04711_DETALLEResult Item in lstDetalle)
                {
                    objMontosOtros.Codigo = "1004";
                    PrecioIGV = 0;
                    Cantidad = 0;
                    ValorVentaG = 0;

                    PrecioIGV = decimal.Round((decimal.Parse(Item.Linea_ValorUnitario) * decimal.Parse(1.18.ToString())), 6);
                    Cantidad = decimal.Parse(Item.Linea_Cantidad);
                    ValorVentaG = PrecioIGV * Cantidad;

                    objMontosOtros.Monto = objMontosOtros.Monto + decimal.Round(ValorVentaG, 4);
                }

                ListaMontosAdicionalesOtros.Add(objMontosOtros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListaMontosAdicionalesOtros;
        }
        List<FacturacionElectronicaOkWCF.ENComprobantePropiedadesAdicionales> LlenarPropiedadesAdicionales()
        {
            List<FacturacionElectronicaOkWCF.ENComprobantePropiedadesAdicionales> ListaPropiedadesAdicionales = new List<FacturacionElectronicaOkWCF.ENComprobantePropiedadesAdicionales>();
            FacturacionElectronicaOkWCF.ENComprobantePropiedadesAdicionales objPropiedadesAdicionales = new FacturacionElectronicaOkWCF.ENComprobantePropiedadesAdicionales();

            objPropiedadesAdicionales = new FacturacionElectronicaOkWCF.ENComprobantePropiedadesAdicionales();

            objPropiedadesAdicionales.Codigo = "1002";
            objPropiedadesAdicionales.Valor = "TRANSFERENCIA GRATUITA DE UN BIEN Y/ O SERVICIO PRESTADO GRATUITAMENTE";

            ListaPropiedadesAdicionales.Add(objPropiedadesAdicionales);

            return ListaPropiedadesAdicionales;
        }

        List<FacturacionElectronicaOkWCF.ENDetraccion> LlenarDetraccion()
        {
            List<FacturacionElectronicaOkWCF.ENDetraccion> ListaDetraccion = new List<FacturacionElectronicaOkWCF.ENDetraccion>();
            List<FacturacionElectronicaOkWCF.ENMonto> ListaMonto = new List<FacturacionElectronicaOkWCF.ENMonto>();
            FacturacionElectronicaOkWCF.ENMonto objMonto = new FacturacionElectronicaOkWCF.ENMonto();
            List<FacturacionElectronicaOkWCF.ENPorcentaje> ListaPorcentaje = new List<FacturacionElectronicaOkWCF.ENPorcentaje>();
            FacturacionElectronicaOkWCF.ENPorcentaje objPorcentaje = new FacturacionElectronicaOkWCF.ENPorcentaje();
            List<FacturacionElectronicaOkWCF.ENNumeroCuenta> ListaNumeroCuenta = new List<FacturacionElectronicaOkWCF.ENNumeroCuenta>();
            FacturacionElectronicaOkWCF.ENNumeroCuenta objNumeroCuenta = new FacturacionElectronicaOkWCF.ENNumeroCuenta();
            FacturacionElectronicaOkWCF.ENDetraccion objDetraccion = new FacturacionElectronicaOkWCF.ENDetraccion();
            List<FacturacionElectronicaOkWCF.ENBienesServicios> ListaBienes = new List<FacturacionElectronicaOkWCF.ENBienesServicios>();
            FacturacionElectronicaOkWCF.ENBienesServicios objBienes = new FacturacionElectronicaOkWCF.ENBienesServicios();
            //Llenamos Porcentaje
            objPorcentaje.Codigo = "2003";
            objPorcentaje.Valor = 10;

            ListaPorcentaje.Add(objPorcentaje);

            //Llenamos Monto
            objMonto.Valor = ((objComprobante.ImporteTotal) * (objPorcentaje.Valor)) / 100;
            objMonto.Codigo = "2003";

            ListaMonto.Add(objMonto);

            //Llenamos NumeroCuenta
            objNumeroCuenta.Valor = "";
            objNumeroCuenta.Codigo = "3001";

            ListaNumeroCuenta.Add(objNumeroCuenta);

            //Llenamos Bienes y Servicios
            //objBienes.Codigo = "3000";
            //objBienes.Valor = "004";

            //ListaBienes.Add(objBienes);

            objDetraccion.Monto = ListaMonto.ToArray();
            objDetraccion.NumeroCuenta = ListaNumeroCuenta.ToArray();
            objDetraccion.Porcentaje = ListaPorcentaje.ToArray();
            //objDetraccion.BienesServicios = ListaBienes.ToArray();

            ListaDetraccion.Add(objDetraccion);

            return ListaDetraccion;
        }
        List<FacturacionElectronicaOkWCF.ENComprobanteNotaDocRef> LlenarNotaDocref(string Serie, string Numero, string idTipoDoc, string FechaDocRef)
        {
            string TipoComprabanteREF = "", mes, dia ;
            DateTime FechaRef;
            string FechaRefString; 
            List<FacturacionElectronicaOkWCF.ENComprobanteNotaDocRef> ListaNotaDocRef = new List<FacturacionElectronicaOkWCF.ENComprobanteNotaDocRef>();
            FacturacionElectronicaOkWCF.ENComprobanteNotaDocRef objNotaDocRef = new FacturacionElectronicaOkWCF.ENComprobanteNotaDocRef();

            if (idTipoDoc == "1")
            { TipoComprabanteREF = "01"; }
            else if (idTipoDoc == "3")
            { TipoComprabanteREF = "03"; }

            FechaRef = DateTime.Parse(FechaDocRef);
            mes = FechaRef.Month.ToString(); 
            if (mes.Length == 1 )
            { mes = "0"+  mes;  }
            dia = FechaRef.Day.ToString();
            if (dia.Length == 1)
            { dia = "0" + dia; }

            FechaRefString = FechaRef.Year.ToString() + "-" + mes + "-" + dia ; 

            objNotaDocRef.Serie = Serie; // "BJ11";
            objNotaDocRef.Numero = Numero; // "2";
            objNotaDocRef.TipoComprobante = TipoComprabanteREF; // "03";
            objNotaDocRef.FechaDocRef = FechaRefString;  //  DateTime.Now.GetDateTimeFormats("yyyy-MM-dd").ToString();

            ListaNotaDocRef.Add(objNotaDocRef);

            return ListaNotaDocRef;
        }
        List<FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumento> LlenarMotivoDocumento(string CodigoMotivoEmision,
            string ReferenciaDocumentoSerie, string ReferenciaDocumentoNumero, string Sustento)
        {
            List<FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumento> ListaMotivoDocumento = new List<FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumento>();
            FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumento objMotivoDocumento = new FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumento();
            List<FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumentoSustento> ListaSustento = new List<FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumentoSustento>();
            FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumentoSustento objSustento = new FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumentoSustento();

            if(CodigoMotivoEmision.Length == 1)
            {
                CodigoMotivoEmision = "0" + CodigoMotivoEmision; 
            }

            objMotivoDocumento.CodigoMotivoEmision = CodigoMotivoEmision; // "03";
            objMotivoDocumento.SerieDocRef = ReferenciaDocumentoSerie; //  "BJ11";
            objMotivoDocumento.NumeroDocRef = ReferenciaDocumentoNumero; // "2";

            //Llenamos el Sustento
            objSustento.Sustento = Sustento; //  "Sustento a la NOTA CREDITO";

            ListaSustento.Add(objSustento);

            objMotivoDocumento.Sustentos = ListaSustento.ToArray();
            ListaMotivoDocumento.Add(objMotivoDocumento);

            return ListaMotivoDocumento;
        }

        List<FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumento> LlenarMotivoDocumentoND(string CodigoMotivoEmision,string ReferenciaDocumentoSerie, string ReferenciaDocumentoNumero, string Sustento)
         
        {
            List<FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumento> ListaMotivoDocumento = new List<FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumento>();
            FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumento objMotivoDocumento = new FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumento();
            List<FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumentoSustento> ListaSustento = new List<FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumentoSustento>();
            FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumentoSustento objSustento = new FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumentoSustento();

            if (CodigoMotivoEmision.Length == 1)
            {
                CodigoMotivoEmision = "0" + CodigoMotivoEmision;
            }

            objMotivoDocumento.CodigoMotivoEmision = CodigoMotivoEmision; // "03";

            if(CodigoMotivoEmision != "03")
            {
                objMotivoDocumento.SerieDocRef = ReferenciaDocumentoSerie; //  "BJ11";
                objMotivoDocumento.NumeroDocRef = ReferenciaDocumentoNumero; // "2";
            }

            //Llenamos el Sustento
            objSustento.Sustento = Sustento; //  "Sustento a la NOTA CREDITO";

            ListaSustento.Add(objSustento);

            objMotivoDocumento.Sustentos = ListaSustento.ToArray();
            ListaMotivoDocumento.Add(objMotivoDocumento);

            return ListaMotivoDocumento;
        }
        List<FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumento> LlenarMotivoDocumentoNC(string CodigoMotivoEmision,string ReferenciaDocumentoSerie, string ReferenciaDocumentoNumero, string Sustento)
        {
            //string Letra; 

            List<FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumento> ListaMotivoDocumento = new List<FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumento>();
            FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumento objMotivoDocumento = new FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumento();
            List<FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumentoSustento> ListaSustento = new List<FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumentoSustento>();
            FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumentoSustento objSustento = new FacturacionElectronicaOkWCF.ENComprobanteMotivoDocumentoSustento();

            //Letra = ReferenciaDocumentoSerie.Substring(0,1); 
            //if(Letra == "F")
            //{
                if (CodigoMotivoEmision.Length == 1)
                {
                    CodigoMotivoEmision = "0" + CodigoMotivoEmision;
                }
            //}
            //else
            //{
            //    CodigoMotivoEmision = "05";
            //}


     
            objMotivoDocumento.CodigoMotivoEmision = CodigoMotivoEmision; // "03";
            objMotivoDocumento.SerieDocRef = ReferenciaDocumentoSerie; //  "BJ11";
            objMotivoDocumento.NumeroDocRef = ReferenciaDocumentoNumero; // "2";

            //Llenamos el Sustento
            objSustento.Sustento = Sustento; //  "Sustento a la NOTA CREDITO";

            ListaSustento.Add(objSustento);

            objMotivoDocumento.Sustentos = ListaSustento.ToArray();
            ListaMotivoDocumento.Add(objMotivoDocumento);

            return ListaMotivoDocumento;
        }
        List<FacturacionElectronicaOkWCF.ENComprobantesOtrosDocumentos> LlenarOtrosDocumentos(string Serie, string Numero, string TipoDocReferencia, string FechaReferencia)
        {
            DateTime FechaREF;

            List<FacturacionElectronicaOkWCF.ENComprobantesOtrosDocumentos> ListaOtrosDocumentos = new List<FacturacionElectronicaOkWCF.ENComprobantesOtrosDocumentos>();
            FacturacionElectronicaOkWCF.ENComprobantesOtrosDocumentos objOtrosDocumentos = new FacturacionElectronicaOkWCF.ENComprobantesOtrosDocumentos();
            if (TipoDocReferencia == "1")
            { TipoDocReferencia = "01"; }
            else if (TipoDocReferencia == "3")
            { TipoDocReferencia = "03"; }

            objOtrosDocumentos.Serie = Serie;
            objOtrosDocumentos.Numero = Numero;
            objOtrosDocumentos.TipoDocReferencia = TipoDocReferencia;
            FechaREF = Convert.ToDateTime(FechaReferencia); 
            objOtrosDocumentos.FechaEmision = FechaREF.ToShortDateString() ; 

            ListaOtrosDocumentos.Add(objOtrosDocumentos);
            return ListaOtrosDocumentos;
        }
        List<FacturacionElectronicaOkWCF.ENPrepago> LlenarPrepago()
        {
            List<FacturacionElectronicaOkWCF.ENPrepago> ListaPrepago = new List<FacturacionElectronicaOkWCF.ENPrepago>();
            FacturacionElectronicaOkWCF.ENPrepago objPrepago = new FacturacionElectronicaOkWCF.ENPrepago();

            objPrepago.CodigoInstruccion = "F001-151";
            objPrepago.Codigo = "02";
            objPrepago.valor = 50;
            objPrepago.Monto = (objPrepago.valor * 1.18);
            objPrepago.Descripcion = "Anticipo de Factura";

            ListaPrepago.Add(objPrepago);
            return ListaPrepago;
        }

    }
}