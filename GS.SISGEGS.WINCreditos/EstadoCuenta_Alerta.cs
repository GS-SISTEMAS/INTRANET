using System.Collections.Generic;
using System.Linq;
using GS.SISGEGS.DM;
using GS.SISGEGS.WINCreditos.ContratosWCF;
using GS.SISGEGS.WINCreditos.ResponsableContratoWCF;
using GS.SISGEGS.WINCreditos.CorreoWCF;
using GS.SISGEGS.WINCreditos.AgendaWCF;
using GS.SISGEGS.WINCreditos.EstadoCuentaWCF;
using System; 

namespace GS.SISGEGS.WINServ
{
    public class EstadoCuenta_Alerta
    {
        static void Main(string[] args)
        {

            for(int x = 1; x<=3; x++)
            {
                List<gsClientesCorreo_EnvioResult> lstCorreo;
                ResponsableContratoWCFClient objResponsablesWCF = new ResponsableContratoWCFClient();
                List<ResponsablesContrato_ListarResult> objListResponsables = new List<ResponsablesContrato_ListarResult>();
                objListResponsables = objResponsablesWCF.ResponsablesContrato_Listar().ToList();
                AgendaWCFClient objAgendaWCF = new AgendaWCFClient();

                string Titulo;
                int idEmpresa;
                int idUsuario;

                idEmpresa = x;
                if (x==3)
                { idEmpresa = 6;
                }
                Console.WriteLine("Iniciando Empresa: " + x.ToString());
                var responsable = "";
                var correoResponsable = "";
                var mensaje = "";
              
                idUsuario = 1;
                int vencidos;
                int cont = 0; 
                vencidos = 0;
                string ccCorreo;
                string ccResponsable;


                lstCorreo = objAgendaWCF.Agenda_ListarCorreos(idEmpresa, idUsuario, 0, null).ToList();

                Console.WriteLine("Total de Correos: " + lstCorreo.Count().ToString());

                foreach (gsClientesCorreo_EnvioResult Correo in lstCorreo)
                {
                    int tiene = 0;

                    //if (vencidos == 0)
                    //{

                    responsable = Correo.AgendaNombre;
                    correoResponsable = Correo.EMail;
                    correoResponsable = correoResponsable.Trim(); 


                    //correoResponsable = "cesar.coronel@gruposilvestre.com.pe";
                    ccResponsable = null;
                    ccCorreo = null;

                    //correoResponsable = "angelo.benavides@gruposilvestre.com.pe";

                    if (cont < 6)
                    {
                        //ccResponsable = "Monica Arone";
                        //ccCorreo = "monica.arone@gruposilvestre.com.pe";
                        //ccResponsable = "Cesar Coronel";
                        //ccCorreo = "cesar.coronel@gruposilvestre.com.pe";
                    }
                    else if (cont < 8)
                    {
                        ccResponsable = "Cesar Coronel";
                        ccCorreo = "cesar.coronel@gruposilvestre.com.pe";
                    }
                    else
                    {
                        ccResponsable = null;
                        ccCorreo = null;
                    }

                    mensaje = "";

                        Titulo = "Grupo Silvestre: Estado de Cuenta al " + DateTime.Now.ToShortDateString();
                        if (idEmpresa==1)
                        {
                            Titulo = "Silvestre: Estado de Cuenta al " + DateTime.Now.ToShortDateString();
                        }
                        if(idEmpresa==2)
                        {
                            Titulo = "NeoAgrum: Estado de Cuenta al " + DateTime.Now.ToShortDateString();
                        }
                        if(idEmpresa==6)
                        {
                            Titulo = "Inatec: Estado de Cuenta al " + DateTime.Now.ToShortDateString();
                        }

                        mensaje += "Estimado Cliente " + responsable + ",";
                        mensaje += "<br/><br/>";
                        mensaje += "Se le envía de forma electrónica su estado de cuenta. ";
                        mensaje += "<br/><br/>";
                        mensaje += "<b>Lista de documentos pendientes: </b>";
                        mensaje += "<br/><br/>";

                        mensaje += Reporte_Cargar(Correo, idEmpresa, idUsuario, ref tiene);

                    if(tiene == 1)
                    {
                        try
                        {

                            if(correoResponsable != null)
                            {
                                EnviarEmail(responsable, correoResponsable, ccResponsable, ccCorreo, Titulo, mensaje);
                                Console.WriteLine("Correo enviado Nro " + cont.ToString() + ": " + Correo.ID_Agenda.ToString());

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error Nro " + cont.ToString() + ": " + Correo.ID_Agenda.ToString() + " " + ex.Message.ToString());
                        }
                    }
                    cont = cont + 1;
                }
            }
        }

        static void EnviarEmail(string responsable, string to, string copiaResponsable, string cc,  string Titulo, string mensaje) {
            CorreoWCFClient objCorreoWCF = new CorreoWCFClient();

            try
            { objCorreoWCF.EnviarCorreo(responsable, to, copiaResponsable, cc, Titulo, mensaje); }
            catch (Exception ex)
            {
                Console.Write("Error: " + ex.Message.ToString()); 
            }
        }

        static string ListaEstadoCuenta(List<gsReporte_DocumentosPendientesResult> lstEstado, List<gsAgendaCliente_BuscarLimiteCreditoResult> lstResumen)
        {

            var mensaje = "<table border='1' cellpadding='0' cellspacing='0' width='100%' >";
            mensaje += CabeceraListaEstadoCuenta();
            double sumImporte = 0;
            double sumDolares = 0;
            double sumSoles = 0;
            int pintar = 0; 

            foreach (gsReporte_DocumentosPendientesResult Estado in lstEstado)
            {
                if (Estado.DiasMora>0)
                {

                    if(pintar == 0)
                    {
                        mensaje += "<tr style='background-color:#e5e5e5;font-size:85%;'> ";
                        mensaje += "<td colspan=10 align=left style='padding: 3px 3px 3px 3px;' >";
                        mensaje += "<b>Vencidos</b>";
                        mensaje += "</td>";
                        mensaje += "</tr>";
                        pintar = pintar + 1; 
                    }

                    mensaje += "<tr style='font-size:85%;' >";

                    mensaje += "<td style='padding: 5px 5px 5px 5px;' >";
                    mensaje += Estado.TipoDocumento;
                    mensaje += "</td>";
                    char pad = Convert.ToChar(0.ToString());
                    mensaje += "<td style='padding: 5px 5px 5px 5px;'>";
                    mensaje += Estado.Serie + "-" + Estado.Numero.PadLeft(8, pad);
                    mensaje += "</td>";

                    //mensaje += "<td>";
                    //mensaje += Estado.Referencia;
                    //mensaje += "</td>";

                    mensaje += "<td align=center style='padding: 3px 3px 3px 3px;'>";
                    mensaje += Convert.ToDateTime(Estado.Fecha).ToShortDateString();
                    mensaje += "</td>";

                    mensaje += "<td align=center style='padding: 3px 3px 3px 3px;'>";
                    mensaje += Convert.ToDateTime(Estado.FechaVencimiento).ToShortDateString();
                    mensaje += "</td>";

                    //mensaje += "<td>";
                    //mensaje += Estado.DiasMora;
                    //mensaje += "</td>";

                    mensaje += "<td style='padding: 3px 3px 3px 3px;'>";
                    mensaje += Estado.EstadoDoc;
                    mensaje += "</td>";

                    //mensaje += "<td width='400' colspan='2' > ";
                    //mensaje += Estado.Banco;
                    //mensaje += "</td>";

                    mensaje += "<td style='padding: 3px 3px 3px 3px;'>";
                    mensaje += Estado.NumeroUnico;
                    mensaje += "</td>";

                    mensaje += "<td  align=center style='padding: 3px 3px 3px 3px;'>";
                    mensaje += Estado.monedasigno;
                    mensaje += "</td>";



                    double Importe = Convert.ToDouble(Estado.Importe);
                    Importe = Math.Round(Importe, 2);
                    sumImporte = sumImporte + Importe;

                    mensaje += "<td align=right style='padding: 3px 3px 3px 3px;'>";
                    mensaje += String.Format("{0:#,##0.00}", Importe); // Importe.ToString() ;
                    mensaje += "</td>";

                    double DeudaSoles = Convert.ToDouble(Estado.DeudaSoles);
                    DeudaSoles = Math.Round(DeudaSoles, 2);
                    sumSoles = sumSoles + DeudaSoles;

                    mensaje += "<td align=right style='padding: 3px 3px 3px 3px;'>";
                    mensaje += String.Format("{0:#,##0.00}", DeudaSoles);  //DeudaSoles.ToString();
                    mensaje += "</td>";

                    double DeudaDolares = Convert.ToDouble(Estado.DeudaDolares);
                    DeudaDolares = Math.Round(DeudaDolares, 2);
                    sumDolares = sumDolares + DeudaDolares;

                    mensaje += "<td align=right style='padding: 3px 3px 3px 3px;'>";
                    mensaje += String.Format("{0:#,##0.00}", DeudaDolares);
                    mensaje += "</td>";

                    mensaje += "</tr>";

                }
            }

            if(sumDolares > 0 || sumSoles > 0 )
            {

                mensaje += "<tr style='font-size:85%;'> ";

                mensaje += "<td colspan=8 align=right style='padding: 3px 3px 3px 3px;' >";
                mensaje += "<b> Total </b>";
                mensaje += "</td>";

                mensaje += "<td align=right style='padding: 3px 3px 3px 3px;'> <b> ";
                mensaje += String.Format("S/{0:#,##0.00}", sumSoles); //  sumSoles.ToString("0.00");
                mensaje += "</b></td>";

                mensaje += "<td align=right style='padding: 3px 3px 3px 3px;'> <b>";
                mensaje += String.Format("${0:#,##0.00}", sumDolares);  //sumDolares.ToString("0.00");
                mensaje += "</b></td>";

                mensaje += "</tr>";
            }


            //mensaje += "</table>";

            double sumImporteN = 0;
            double sumDolaresN = 0;
            double sumSolesN = 0;
            pintar = 0; 
            foreach (gsReporte_DocumentosPendientesResult Estado in lstEstado)
            {
                if (Estado.DiasMora < 0)
                {
                    if (pintar == 0)
                    {
                        mensaje += "<tr style='background-color:#e5e5e5;font-size:85%;'> ";
                        mensaje += "<td colspan=10 align=left style='padding: 3px 3px 3px 3px;' >";
                        mensaje += "<b>No Vencidos</b>";
                        mensaje += "</td>";
                        mensaje += "</tr>";
                        pintar = pintar + 1;
                    }

                    mensaje += "<tr style='font-size:85%;' >";

                    mensaje += "<td style='padding: 5px 5px 5px 5px;' >";
                    mensaje += Estado.TipoDocumento;
                    mensaje += "</td>";
                    char pad = Convert.ToChar(0.ToString());
                    mensaje += "<td style='padding: 5px 5px 5px 5px;'>";
                    mensaje += Estado.Serie + "-" + Estado.Numero.PadLeft(8, pad);
                    mensaje += "</td>";

                    //mensaje += "<td>";
                    //mensaje += Estado.Referencia;
                    //mensaje += "</td>";

                    mensaje += "<td align=center style='padding: 3px 3px 3px 3px;'>";
                    mensaje += Convert.ToDateTime(Estado.Fecha).ToShortDateString();
                    mensaje += "</td>";

                    mensaje += "<td align=center style='padding: 3px 3px 3px 3px;'>";
                    mensaje += Convert.ToDateTime(Estado.FechaVencimiento).ToShortDateString();
                    mensaje += "</td>";

                    //mensaje += "<td>";
                    //mensaje += Estado.DiasMora;
                    //mensaje += "</td>";

                    mensaje += "<td style='padding: 3px 3px 3px 3px;'>";
                    mensaje += Estado.EstadoDoc;
                    mensaje += "</td>";

                    //mensaje += "<td width='400' colspan='2' > ";
                    //mensaje += Estado.Banco;
                    //mensaje += "</td>";

                    mensaje += "<td style='padding: 3px 3px 3px 3px;'>";
                    mensaje += Estado.NumeroUnico;
                    mensaje += "</td>";

                    mensaje += "<td  align=center style='padding: 3px 3px 3px 3px;'>";
                    mensaje += Estado.monedasigno;
                    mensaje += "</td>";



                    double Importe = Convert.ToDouble(Estado.Importe);
                    Importe = Math.Round(Importe, 2);
                    sumImporteN = sumImporteN + Importe;

                    mensaje += "<td align=right style='padding: 3px 3px 3px 3px;'>";
                    mensaje += String.Format("{0:#,##0.00}", Importe); // Importe.ToString() ;
                    mensaje += "</td>";

                    double DeudaSoles = Convert.ToDouble(Estado.DeudaSoles);
                    DeudaSoles = Math.Round(DeudaSoles, 2);
                    sumSolesN = sumSolesN + DeudaSoles;

                    mensaje += "<td align=right style='padding: 3px 3px 3px 3px;'>";
                    mensaje += String.Format("{0:#,##0.00}", DeudaSoles);  //DeudaSoles.ToString();
                    mensaje += "</td>";

                    double DeudaDolares = Convert.ToDouble(Estado.DeudaDolares);
                    DeudaDolares = Math.Round(DeudaDolares, 2);
                    sumDolaresN = sumDolaresN + DeudaDolares;

                    mensaje += "<td align=right style='padding: 3px 3px 3px 3px;'>";
                    mensaje += String.Format("{0:#,##0.00}", DeudaDolares);
                    mensaje += "</td>";

                    mensaje += "</tr>";
                }
               
            }

            if (sumSolesN > 0 || sumDolaresN > 0)
            {
                mensaje += "<tr style='font-size:85%;'> ";

                mensaje += "<td colspan=8 align=right style='padding: 3px 3px 3px 3px;' >";
                mensaje += "<b> Total </b>";
                mensaje += "</td>";

                mensaje += "<td align=right style='padding: 3px 3px 3px 3px;'> <b> ";
                mensaje += String.Format("S/{0:#,##0.00}", sumSolesN); //  sumSoles.ToString("0.00");
                mensaje += "</b></td>";

                mensaje += "<td align=right style='padding: 3px 3px 3px 3px;'> <b>";
                mensaje += String.Format("${0:#,##0.00}", sumDolaresN);  //sumDolares.ToString("0.00");
                mensaje += "</b></td>";

                mensaje += "</tr>";
            }

            mensaje += "<tr style='background-color:#e5e5e5;font-size:85%;'> ";
            mensaje += "<td colspan=8 align=center style='padding: 3px 3px 3px 3px;' >";
            mensaje += "<b> Total Pendiente </b>";
            mensaje += "</td>";

            double TotalSoles = 0;
            double TotalDolares = 0;
            TotalSoles = (sumSolesN + sumSoles);
            TotalDolares = (sumDolaresN + sumDolares); 

            mensaje += "<td align=right style='padding: 3px 3px 3px 3px;'> <b> ";
            mensaje += String.Format("S/{0:#,##0.00}", TotalSoles); //  sumSoles.ToString("0.00");
            mensaje += "</b></td>";

            mensaje += "<td align=right style='padding: 3px 3px 3px 3px;'> <b>";
            mensaje += String.Format("${0:#,##0.00}", TotalDolares);  //sumDolares.ToString("0.00");
            mensaje += "</b></td>";

            mensaje += "</tr>";



            mensaje += "</table>";


            return mensaje;
        }
        static string CabeceraListaEstadoCuenta() {
            var mensaje = "<tr width='100%' style='background-color:#ccc;font-size:85%;'>";
            mensaje += "<th style='padding: 3px 3px 3px 3px;' width='290px'  >  <b> Tipo Documento </b> </th>";
            mensaje += "<th style='padding: 3px 3px 3px 3px;' width='230px' ><b> Núm.Documento </b></th>";
            //mensaje += "<td><b> Referencia </b></td>";
            mensaje += "<th style='padding: 3px 3px 3px 3px;' width='210px'><b> F. Emisión </b></th>";
            mensaje += "<th style='padding: 3px 3px 3px 3px;' width='225px'><b> F. Vencimiento </b></th>";
            //mensaje += "<td><b> DíasMora </b></td>";
            mensaje += "<th style='padding: 3px 3px 3px 3px;' width='280px'><b> Estado Doc.</b></th>";
            //mensaje += "<td colspan=2 width='400px'><b>Banco</b></td>";
            mensaje += "<th style='padding: 3px 3px 3px 3px;' width='290px'><b> Número Único </b></th>";
            mensaje += "<th style='padding: 3px 3px 3px 3px;' ><b> Mon </b></th>";
            mensaje += "<th style='padding: 3px 3px 3px 3px;' ><b> Importe  </b></th>";
            mensaje += "<th style='padding: 3px 3px 3px 3px;' ><b>Pendiente(S/)</b></th>";
            mensaje += "<th style='padding: 3px 3px 3px 3px;' ><b>Pendiente($)</b></th>";
            mensaje += "</tr>";
            return mensaje;
        }

        static string Reporte_Cargar(gsClientesCorreo_EnvioResult Correo, int idEmpresa, int idUsuario, ref int tiene)
        {
            string mensaje = "";
         
            try
            {
                DateTime fecha1;
                DateTime fecha2;
                DateTime fecha3;
                DateTime fecha4;

                string Cliente;
                string Vendedor;
               

                mensaje = ""; 
                Cliente = "";
                Vendedor = "";
                List<gsReporte_DocumentosPendientesResult> lstEstado;
                List<gsAgendaCliente_BuscarLimiteCreditoResult> lstResumen; 

                try
                {
                    fecha2 = DateTime.Now;
                    fecha1 = fecha2.AddYears(-50);
                    fecha3 = fecha2.AddYears(-50);
                    fecha4 = fecha2.AddYears(50);

                    Cliente = null;
                    Vendedor = null;

                    Cliente = Correo.ID_Agenda;
                    lstEstado = ListarEstadoCuenta(idEmpresa, idUsuario,  Cliente, Vendedor, fecha1, fecha2, fecha3, fecha4, 0);
                    lstResumen = ListarClientesResumen(idEmpresa, idUsuario, lstEstado);

                    if(lstResumen.Count > 0)
                    {
                        mensaje = ListaEstadoCuenta(lstEstado, lstResumen);
                        tiene = 1; 
                    }
                    else
                    {
                        tiene = 0; 
                    }

                }
                catch (Exception ex)
                {
                    //lblMensaje.Text = ex.Message;
                    //lblMensaje.CssClass = "mensajeError";
                }
              
            }
            catch (Exception ex)
            {
                //lblMensaje.Text = "No se encontrarón resultados.";
                //lblMensaje.CssClass = "mensajeError";
            }
            return mensaje;
        }

        static List<gsReporte_DocumentosPendientesResult> ListarEstadoCuenta(int idEmpresa , int idUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {
            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();

            try
            {
                List<gsReporte_DocumentosPendientesResult> lstEstadoCuenta = new List<gsReporte_DocumentosPendientesResult>();

                if (codVendedor != null)
                {
                    if (codVendedor.Length > 3)
                    {
                        if (codVendedor == "666666")
                        {
                            codVendedor = null;

                            List<gsReporte_DocumentosPendientesResult> lst = objEstadoCuentaWCF.EstadoCuenta_ListarxCliente(idEmpresa, idUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos,0).OrderBy(e => e.ClienteNombre).OrderBy(e => e.FechaVencimiento).ToList();

                            var query_Estado = from c in lst
                                               where c.EstadoCliente == "LEGAL"
                                               orderby c.Fecha, c.ZonaCobranza ascending
                                               select new
                                               {
                                                   c.Banco,
                                                   c.ClienteNombre,
                                                   c.DeudaDolares,
                                                   c.DeudaSoles,
                                                   c.DiasMora,
                                                   c.EstadoCliente,
                                                   c.EstadoDoc,
                                                   c.FAceptada,
                                                   c.Fecha,
                                                   c.FechaVencimiento,
                                                   c.ID_Agenda,
                                                   c.ID_Doc,
                                                   c.ID_EstadoDoc,
                                                   c.ID_Moneda,
                                                   c.Id_TipoDoc,
                                                   c.ID_Vendedor,
                                                   c.ID_Zona,
                                                   c.Importe,
                                                   c.ImporteFinanciado,
                                                   c.ImportePagado,
                                                   c.ImportePendiente,
                                                   c.ImportePendiente_01a08,
                                                   c.ImportePendiente_01a30,
                                                   c.ImportePendiente_09a30,
                                                   c.ImportePendiente_121a360,
                                                   c.ImportePendiente_121aMas,
                                                   c.ImportePendiente_31a60,
                                                   c.ImportePendiente_361a720,
                                                   c.ImportePendiente_361aMas,
                                                   c.ImportePendiente_61a120,
                                                   c.ImportePendiente_61a90,
                                                   c.ImportePendiente_721aMas,
                                                   c.ImportePendiente_91a120,
                                                   c.ImportePendiente_NoVencido,
                                                   c.ImportePendiente_PorVencer30,
                                                   c.ImportePendiente_VenceHoy,
                                                   c.Moneda,
                                                   c.monedasigno,
                                                   c.No_Vendedor,
                                                   c.NroDocumento,
                                                   c.Numero,
                                                   c.NumeroUnico,
                                                   c.Origen,
                                                   c.OrigenOp,
                                                   c.Referencia,
                                                   c.Sede,
                                                   c.Serie,
                                                   c.Situacion,
                                                   c.TC,
                                                   c.TipoDocumento,
                                                   c.ZonaCobranza
                                               };

                            foreach (var QEstado in query_Estado.ToList())
                            {
                                gsReporte_DocumentosPendientesResult rowEstado = new gsReporte_DocumentosPendientesResult();
                                rowEstado.Banco = QEstado.Banco;
                                rowEstado.ClienteNombre = QEstado.ClienteNombre;
                                rowEstado.DeudaDolares = QEstado.DeudaDolares;
                                rowEstado.DeudaSoles = QEstado.DeudaSoles;
                                rowEstado.DiasMora = QEstado.DiasMora;
                                rowEstado.EstadoCliente = QEstado.EstadoCliente;
                                rowEstado.EstadoDoc = QEstado.EstadoDoc;
                                rowEstado.FAceptada = QEstado.FAceptada;
                                rowEstado.Fecha = QEstado.Fecha;
                                rowEstado.FechaVencimiento = QEstado.FechaVencimiento;
                                rowEstado.ID_Agenda = QEstado.ID_Agenda;
                                rowEstado.ID_Doc = QEstado.ID_Doc;
                                rowEstado.ID_EstadoDoc = QEstado.ID_EstadoDoc;
                                rowEstado.ID_Moneda = QEstado.ID_Moneda;
                                rowEstado.Id_TipoDoc = QEstado.Id_TipoDoc;
                                rowEstado.ID_Vendedor = QEstado.ID_Vendedor;
                                rowEstado.ID_Zona = QEstado.ID_Zona;
                                rowEstado.Importe = QEstado.Importe;
                                rowEstado.ImporteFinanciado = QEstado.ImporteFinanciado;
                                rowEstado.ImportePagado = QEstado.ImportePagado;
                                rowEstado.ImportePendiente = QEstado.ImportePendiente;
                                rowEstado.ImportePendiente_01a08 = QEstado.ImportePendiente_01a08;
                                rowEstado.ImportePendiente_01a30 = QEstado.ImportePendiente_01a30;
                                rowEstado.ImportePendiente_09a30 = QEstado.ImportePendiente_09a30;
                                rowEstado.ImportePendiente_121a360 = QEstado.ImportePendiente_121a360;
                                rowEstado.ImportePendiente_121aMas = QEstado.ImportePendiente_121aMas;
                                rowEstado.ImportePendiente_31a60 = QEstado.ImportePendiente_31a60;
                                rowEstado.ImportePendiente_361a720 = QEstado.ImportePendiente_361a720;
                                rowEstado.ImportePendiente_361aMas = QEstado.ImportePendiente_361aMas;
                                rowEstado.ImportePendiente_61a120 = QEstado.ImportePendiente_61a120;
                                rowEstado.ImportePendiente_61a90 = QEstado.ImportePendiente_61a90;
                                rowEstado.ImportePendiente_721aMas = QEstado.ImportePendiente_721aMas;

                                rowEstado.ImportePendiente_91a120 = QEstado.ImportePendiente_91a120;
                                rowEstado.ImportePendiente_NoVencido = QEstado.ImportePendiente_NoVencido;
                                rowEstado.ImportePendiente_PorVencer30 = QEstado.ImportePendiente_PorVencer30;
                                rowEstado.ImportePendiente_VenceHoy = QEstado.ImportePendiente_VenceHoy;
                                rowEstado.Moneda = QEstado.Moneda;
                                rowEstado.monedasigno = QEstado.monedasigno;
                                rowEstado.No_Vendedor = QEstado.No_Vendedor;
                                rowEstado.NroDocumento = QEstado.NroDocumento;
                                rowEstado.Numero = QEstado.Numero;
                                rowEstado.NumeroUnico = QEstado.NumeroUnico;
                                rowEstado.Origen = QEstado.Origen;
                                rowEstado.OrigenOp = QEstado.OrigenOp;
                                rowEstado.Referencia = QEstado.Referencia;
                                rowEstado.Sede = QEstado.Sede;

                                rowEstado.Serie = QEstado.Serie;
                                rowEstado.Situacion = QEstado.Situacion;
                                rowEstado.TC = QEstado.TC;
                                rowEstado.TipoDocumento = QEstado.TipoDocumento;
                                rowEstado.ZonaCobranza = QEstado.ZonaCobranza;

                                lstEstadoCuenta.Add(rowEstado);
                            }
                        }
                        else
                        {
                            List<gsReporte_DocumentosPendientesResult> lst = objEstadoCuentaWCF.EstadoCuenta_ListarxCliente( idEmpresa,idUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos,0).OrderBy(e => e.ClienteNombre).OrderBy(e => e.FechaVencimiento).ToList();

                            var query_Estado = from c in lst
                                               where c.EstadoCliente != "LEGAL"
                                               orderby c.Fecha, c.ZonaCobranza ascending
                                               select new
                                               {
                                                   c.Banco,
                                                   c.ClienteNombre,
                                                   c.DeudaDolares,
                                                   c.DeudaSoles,
                                                   c.DiasMora,
                                                   c.EstadoCliente,
                                                   c.EstadoDoc,
                                                   c.FAceptada,
                                                   c.Fecha,
                                                   c.FechaVencimiento,
                                                   c.ID_Agenda,
                                                   c.ID_Doc,
                                                   c.ID_EstadoDoc,
                                                   c.ID_Moneda,
                                                   c.Id_TipoDoc,
                                                   c.ID_Vendedor,
                                                   c.ID_Zona,
                                                   c.Importe,
                                                   c.ImporteFinanciado,
                                                   c.ImportePagado,
                                                   c.ImportePendiente,
                                                   c.ImportePendiente_01a08,
                                                   c.ImportePendiente_01a30,
                                                   c.ImportePendiente_09a30,
                                                   c.ImportePendiente_121a360,
                                                   c.ImportePendiente_121aMas,
                                                   c.ImportePendiente_31a60,
                                                   c.ImportePendiente_361a720,
                                                   c.ImportePendiente_361aMas,
                                                   c.ImportePendiente_61a120,
                                                   c.ImportePendiente_61a90,
                                                   c.ImportePendiente_721aMas,
                                                   c.ImportePendiente_91a120,
                                                   c.ImportePendiente_NoVencido,
                                                   c.ImportePendiente_PorVencer30,
                                                   c.ImportePendiente_VenceHoy,
                                                   c.Moneda,
                                                   c.monedasigno,
                                                   c.No_Vendedor,
                                                   c.NroDocumento,
                                                   c.Numero,
                                                   c.NumeroUnico,
                                                   c.Origen,
                                                   c.OrigenOp,
                                                   c.Referencia,
                                                   c.Sede,
                                                   c.Serie,
                                                   c.Situacion,
                                                   c.TC,
                                                   c.TipoDocumento,
                                                   c.ZonaCobranza
                                               };

                            foreach (var QEstado in query_Estado.ToList())
                            {
                                gsReporte_DocumentosPendientesResult rowEstado = new gsReporte_DocumentosPendientesResult();
                                rowEstado.Banco = QEstado.Banco;
                                rowEstado.ClienteNombre = QEstado.ClienteNombre;
                                rowEstado.DeudaDolares = QEstado.DeudaDolares;
                                rowEstado.DeudaSoles = QEstado.DeudaSoles;
                                rowEstado.DiasMora = QEstado.DiasMora;
                                rowEstado.EstadoCliente = QEstado.EstadoCliente;
                                rowEstado.EstadoDoc = QEstado.EstadoDoc;
                                rowEstado.FAceptada = QEstado.FAceptada;
                                rowEstado.Fecha = QEstado.Fecha;
                                rowEstado.FechaVencimiento = QEstado.FechaVencimiento;
                                rowEstado.ID_Agenda = QEstado.ID_Agenda;
                                rowEstado.ID_Doc = QEstado.ID_Doc;
                                rowEstado.ID_EstadoDoc = QEstado.ID_EstadoDoc;
                                rowEstado.ID_Moneda = QEstado.ID_Moneda;
                                rowEstado.Id_TipoDoc = QEstado.Id_TipoDoc;
                                rowEstado.ID_Vendedor = QEstado.ID_Vendedor;
                                rowEstado.ID_Zona = QEstado.ID_Zona;
                                rowEstado.Importe = QEstado.Importe;
                                rowEstado.ImporteFinanciado = QEstado.ImporteFinanciado;
                                rowEstado.ImportePagado = QEstado.ImportePagado;
                                rowEstado.ImportePendiente = QEstado.ImportePendiente;
                                rowEstado.ImportePendiente_01a08 = QEstado.ImportePendiente_01a08;
                                rowEstado.ImportePendiente_01a30 = QEstado.ImportePendiente_01a30;
                                rowEstado.ImportePendiente_09a30 = QEstado.ImportePendiente_09a30;
                                rowEstado.ImportePendiente_121a360 = QEstado.ImportePendiente_121a360;
                                rowEstado.ImportePendiente_121aMas = QEstado.ImportePendiente_121aMas;
                                rowEstado.ImportePendiente_31a60 = QEstado.ImportePendiente_31a60;
                                rowEstado.ImportePendiente_361a720 = QEstado.ImportePendiente_361a720;
                                rowEstado.ImportePendiente_361aMas = QEstado.ImportePendiente_361aMas;
                                rowEstado.ImportePendiente_61a120 = QEstado.ImportePendiente_61a120;
                                rowEstado.ImportePendiente_61a90 = QEstado.ImportePendiente_61a90;
                                rowEstado.ImportePendiente_721aMas = QEstado.ImportePendiente_721aMas;

                                rowEstado.ImportePendiente_91a120 = QEstado.ImportePendiente_91a120;
                                rowEstado.ImportePendiente_NoVencido = QEstado.ImportePendiente_NoVencido;
                                rowEstado.ImportePendiente_PorVencer30 = QEstado.ImportePendiente_PorVencer30;
                                rowEstado.ImportePendiente_VenceHoy = QEstado.ImportePendiente_VenceHoy;
                                rowEstado.Moneda = QEstado.Moneda;
                                rowEstado.monedasigno = QEstado.monedasigno;
                                rowEstado.No_Vendedor = QEstado.No_Vendedor;
                                rowEstado.NroDocumento = QEstado.NroDocumento;
                                rowEstado.Numero = QEstado.Numero;
                                rowEstado.NumeroUnico = QEstado.NumeroUnico;
                                rowEstado.Origen = QEstado.Origen;
                                rowEstado.OrigenOp = QEstado.OrigenOp;
                                rowEstado.Referencia = QEstado.Referencia;
                                rowEstado.Sede = QEstado.Sede;

                                rowEstado.Serie = QEstado.Serie;
                                rowEstado.Situacion = QEstado.Situacion;
                                rowEstado.TC = QEstado.TC;
                                rowEstado.TipoDocumento = QEstado.TipoDocumento;
                                rowEstado.ZonaCobranza = QEstado.ZonaCobranza;

                                lstEstadoCuenta.Add(rowEstado);
                            }
                        }
                    }

                }
                else
                {
                    List<gsReporte_DocumentosPendientesResult> lst = objEstadoCuentaWCF.EstadoCuenta_ListarxCliente( idEmpresa,idUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos,0).OrderBy(e => e.ClienteNombre).OrderBy(e => e.FechaVencimiento).ToList();
                    lstEstadoCuenta = lst;
                }

                //ViewState["lstEstadoCuenta"] = JsonHelper.JsonSerializer(lstEstadoCuenta);
                //grdEstadoCuenta.DataSource = lstEstadoCuenta;
                //grdEstadoCuenta.DataBind();
                //lblDate.Text = "1";
                return lstEstadoCuenta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        static List<gsReporte_DocumentosPendientesResumenClienteResult> ListarEstadoCuentaResumenCliente(int idEmpresa, int idUsuario, string codAgenda, string codVendedor, DateTime fechaEmisionInicial, DateTime fechaEmisionFinal, DateTime fechaVencimientoInicial, DateTime fechaVencimientoFinal, int vencidos)
        {

            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();
            try
            {
                List<gsReporte_DocumentosPendientesResumenClienteResult> lstDocumentos = objEstadoCuentaWCF.EstadoCuenta_ListarResumenCliente( idEmpresa, idUsuario, codAgenda, codVendedor, fechaEmisionInicial, fechaEmisionFinal, fechaVencimientoInicial, fechaVencimientoFinal, vencidos).ToList();
                //ViewState["lstEstadoCuentaResumenClienteTotal"] = JsonHelper.JsonSerializer(lstDocumentos);

                //grdEstadoCuentaCliente.DataSource = lstDocumentos;
                //grdEstadoCuentaCliente.DataBind();

                //lblMensajeResumenCliente.Text = "Se han encontrado " + lstDocumentos.Count.ToString() + " registro.";
                //lblMensajeResumenCliente.CssClass = "mensajeExito";

                //lblDate2.Text = "2";
                return lstDocumentos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static List<gsAgendaCliente_BuscarLimiteCreditoResult>  ListarClientesResumen(int idEmpresa, int idUsuario, List<gsReporte_DocumentosPendientesResult> lst)
        {
            EstadoCuentaWCFClient objEstadoCuentaWCF = new EstadoCuentaWCFClient();
            List<gsAgendaCliente_BuscarLimiteCreditoResult> lstLimiteCreditoAgenda;
            try
            {
                lstLimiteCreditoAgenda = new List<gsAgendaCliente_BuscarLimiteCreditoResult>();
                var queryAllAgenda = from DocumentosPendientes in lst select DocumentosPendientes.ID_Agenda;
                var queryAgenda = from AgendaPendiente in queryAllAgenda.Distinct() orderby AgendaPendiente ascending select AgendaPendiente;

                foreach (var agenda in queryAgenda)
                {
                    List<gsAgendaCliente_BuscarLimiteCreditoResult> LimiteCreditoAgenda = objEstadoCuentaWCF.EstadoCuenta_LimiteCreditoxCliente( idEmpresa, idUsuario, agenda.ToString(),0).ToList();
                    gsAgendaCliente_BuscarLimiteCreditoResult Limite = LimiteCreditoAgenda[0];
                    lstLimiteCreditoAgenda.Add(Limite);
                }

                //ViewState["lstResumenCliente"] = JsonHelper.JsonSerializer(lstLimiteCreditoAgenda);
                ////grdResumenCliente.DataSource = lstLimiteCreditoAgenda;
                ////grdResumenCliente.DataBind();
                //lblDate.Text = "1";

                return lstLimiteCreditoAgenda; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void ListarClientesResumenVencidos(List<gsAgendaCliente_BuscarLimiteCreditoResult> listaResumen, List<gsReporte_DocumentosPendientesResult>ListaDetalle)
        {
            List<gsAgendaCliente_BuscarLimiteCreditoResult> lstClienteResumen;
            List<gsAgendaCliente_BuscarLimiteCreditoResult> lstClienteResumenFinal = new List<gsAgendaCliente_BuscarLimiteCreditoResult>();
            gsAgendaCliente_BuscarLimiteCreditoResult ClienteResumenFinal;
            List<gsReporte_DocumentosPendientesResult> lstClienteDetalle;
            lstClienteResumen = ClienteResumen(listaResumen);
            lstClienteDetalle = ClienteDetalle(ListaDetalle);

            //DataTable dtTablaresumen = TablaLimiteCredito();
            try
            {
                foreach (gsAgendaCliente_BuscarLimiteCreditoResult ClienteResumen in lstClienteResumen)
                {
                    ClienteResumenFinal = new gsAgendaCliente_BuscarLimiteCreditoResult();
                    var query_Detalle = from c in lstClienteDetalle
                                        where c.ID_Agenda == ClienteResumen.ID_Agenda
                                        orderby c.ClienteNombre, c.FechaVencimiento
                                        select new
                                        {
                                            c.TC,
                                            c.ID_Moneda,
                                            c.ID_Agenda,


                                            Pendiente = c.ID_Moneda == 0 ? c.ImportePendiente :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente / c.TC :
                                                        c.ImportePendiente,

                                            Pendiente_PorVencer30 = c.ID_Moneda == 0 ? c.ImportePendiente_PorVencer30 :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_PorVencer30 / c.TC :
                                                        c.ImportePendiente_PorVencer30,

                                            Pendiente_NoVencido = c.ID_Moneda == 0 ? c.ImportePendiente_NoVencido :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_NoVencido / c.TC :
                                                        c.ImportePendiente_NoVencido,

                                            Pendiente_VenceHoy = c.ID_Moneda == 0 ? c.ImportePendiente_VenceHoy :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_VenceHoy / c.TC :
                                                        c.ImportePendiente_VenceHoy,
                                            Pendiente_01a30 = c.ID_Moneda == 0 ? c.ImportePendiente_01a30 :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_01a30 / c.TC :
                                                        c.ImportePendiente_01a30,
                                            Pendiente_31a60 = c.ID_Moneda == 0 ? c.ImportePendiente_31a60 :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_31a60 / c.TC :
                                                        c.ImportePendiente_31a60,
                                            Pendiente_61a120 = c.ID_Moneda == 0 ? c.ImportePendiente_61a120 :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_61a120 / c.TC :
                                                        c.ImportePendiente_61a120,
                                            Pendiente_121a360 = c.ID_Moneda == 0 ? c.ImportePendiente_121a360 :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_121a360 / c.TC :
                                                        c.ImportePendiente_121a360,
                                            Pendiente_361aMas = c.ID_Moneda == 0 ? c.ImportePendiente_361aMas :
                                                        c.ID_Moneda == 1 ? c.ImportePendiente_361aMas / c.TC :
                                                        c.ImportePendiente_361aMas
                                        };

                    var sumImportePendiente = query_Detalle.ToList().Select(c => c.Pendiente).Sum();
                    var sumImportePendiente_NoVencido = query_Detalle.ToList().Select(c => c.Pendiente_NoVencido).Sum();
                    var sumImportePendiente_VenceHoy = query_Detalle.ToList().Select(c => c.Pendiente_VenceHoy).Sum();

                    var sumImporte_PorVencer30 = query_Detalle.ToList().Select(c => c.Pendiente_PorVencer30).Sum();

                    var sumImportePendiente_01a30 = query_Detalle.ToList().Select(c => c.Pendiente_01a30).Sum();
                    var sumImportePendiente_31a60 = query_Detalle.ToList().Select(c => c.Pendiente_31a60).Sum();
                    var sumImportePendiente_61a120 = query_Detalle.ToList().Select(c => c.Pendiente_61a120).Sum();
                    var sumImportePendiente_121a360 = query_Detalle.ToList().Select(c => c.Pendiente_121a360).Sum();
                    var sumImportePendiente_361aMas = query_Detalle.ToList().Select(c => c.Pendiente_361aMas).Sum();

                    decimal NoVencido;
                    decimal DeudaVencida;
                    decimal CreditoDisponible;
                    NoVencido = Convert.ToDecimal(sumImportePendiente_NoVencido) + Convert.ToDecimal(sumImportePendiente_VenceHoy);
                    DeudaVencida = Convert.ToDecimal(sumImportePendiente_01a30) + Convert.ToDecimal(sumImportePendiente_31a60) + Convert.ToDecimal(sumImportePendiente_61a120) + Convert.ToDecimal(sumImportePendiente_121a360) + Convert.ToDecimal(sumImportePendiente_361aMas);
                    CreditoDisponible = Convert.ToDecimal(ClienteResumen.LineaCredito) - Convert.ToDecimal(sumImportePendiente);

                    string strsumNoVencido = string.Format("{0:#,##0.00}", NoVencido);

                    string strsumImporte_PorVencer30 = string.Format("{0:#,##0.00}", sumImporte_PorVencer30);

                    string strsumImportePendiente_01a30 = string.Format("{0:#,##0.00}", sumImportePendiente_01a30);
                    string strsumImportePendiente_31a60 = string.Format("{0:#,##0.00}", sumImportePendiente_31a60);
                    string strsumImportePendiente_61a120 = string.Format("{0:#,##0.00}", sumImportePendiente_61a120);
                    string strsumImportePendiente_121a360 = string.Format("{0:#,##0.00}", sumImportePendiente_121a360);
                    string strsumImportePendiente_361aMas = string.Format("{0:#,##0.00}", sumImportePendiente_361aMas);
                    string strsumImportePendiente = string.Format("{0:#,##0.00}", sumImportePendiente);
                    string strsumDeudaVencida = string.Format("{0:#,##0.00}", DeudaVencida);
                    string strLineaCredito = string.Format("{0:#,##0.00}", ClienteResumen.LineaCredito);
                    //string strTotalCredito = string.Format("{0:$ #,##0.00}", ClienteResumen.TotalCredito);
                    string strCreditoDisponible = string.Format("{0:#,##0.00}", CreditoDisponible);


                    ClienteResumenFinal.ID_Agenda = ClienteResumen.ID_Agenda;
                    ClienteResumenFinal.AgendaNombre = ClienteResumen.AgendaNombre;

                    ClienteResumenFinal.Aprobacion = ClienteResumen.Aprobacion;
                    ClienteResumenFinal.AprobadoDes = ClienteResumen.AprobadoDes;

                    ClienteResumenFinal.DiasCredito = ClienteResumen.DiasCredito;
                    ClienteResumenFinal.BloqueoLineaCredito = ClienteResumen.BloqueoLineaCredito;

                    ClienteResumenFinal.Estado = ClienteResumen.Estado;
                    ClienteResumenFinal.EstadoDes = ClienteResumen.EstadoDes;
                    ClienteResumenFinal.FechaVCMTLinea = ClienteResumen.FechaVCMTLinea;

                    ClienteResumenFinal.NoVencido = Convert.ToDecimal(strsumNoVencido);
                    ClienteResumenFinal.PorVencer30 = Convert.ToDecimal(strsumImporte_PorVencer30);

                    ClienteResumenFinal.Vencido01a30 = Convert.ToDecimal(strsumImportePendiente_01a30);
                    ClienteResumenFinal.Vencido31a60 = Convert.ToDecimal(strsumImportePendiente_31a60);
                    ClienteResumenFinal.Vencido61a120 = Convert.ToDecimal(strsumImportePendiente_61a120);
                    ClienteResumenFinal.Vencido121a360 = Convert.ToDecimal(strsumImportePendiente_121a360);
                    ClienteResumenFinal.Vencido361amas = Convert.ToDecimal(strsumImportePendiente_361aMas);
                    ClienteResumenFinal.DeudaVencida = Convert.ToDecimal(strsumDeudaVencida);
                    ClienteResumenFinal.DeudaTotal = Convert.ToDecimal(strsumImportePendiente);
                    ClienteResumenFinal.LineaCredito = Convert.ToDecimal(strLineaCredito);
                    ClienteResumenFinal.CreditoDisponible = Convert.ToDecimal(strCreditoDisponible);
                    lstClienteResumenFinal.Add(ClienteResumenFinal);

                }
                //ViewState["lstResumenCliente"] = JsonHelper.JsonSerializer(lstClienteResumenFinal);
                //grdResumenCliente.DataSource = lstClienteResumenFinal;
                //grdResumenCliente.DataBind();

                //lblDate.Text = "1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static List<gsAgendaCliente_BuscarLimiteCreditoResult> ClienteResumen(List<gsAgendaCliente_BuscarLimiteCreditoResult> lista)
        {
            List<gsAgendaCliente_BuscarLimiteCreditoResult> lst = lista; 
            return lst;
        }

        static List<gsReporte_DocumentosPendientesResult> ClienteDetalle(List<gsReporte_DocumentosPendientesResult> lista)
        {
            //DataTable dtTabla;
            List<gsReporte_DocumentosPendientesResult> lst = lista; 
            return lst;
        }
    }
}
