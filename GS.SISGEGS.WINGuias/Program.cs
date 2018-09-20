using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using GS.SISGEGS.DM;
using GS.SISGEGS.WINGuias.OrdenVentaWCF;
using GS.SISGEGS.WINGuias.AgendaWCF;
using GS.SISGEGS.WINGuias.ImpuestoWCF;
using GS.SISGEGS.WINGuias.ItemWCF;
using GS.SISGEGS.WINGuias.GuiaWCF;

namespace GS.SISGEGS.WINGuias
{
    class Program
    {
        public static void Main(string[] args)
        {
            string ruc_empresa = "";
            int idEmpresa = 0;
            //string segundos;
            //segundos = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            //var fileName = "Guias_" + DateTime.Now.ToShortDateString().Replace("/", "") + "_" + segundos + ".txt";

            ////--------------------------Credenciales---------------------------------------
            //var ftpUser = @"ftpwms";
            //var ftpPassword = "wmsapia";
            ////int idEmpresa = 3;  //1=SILVESTRE; 2=NEOAGRUM; 6=INATEC


 

            ////--------------------Crear TXT Pedidos Pendidentes----------------------------
            ////var CrearFilePath = @"C:\\Users\\cesar.coronel.GS\\Desktop\\Pruebas_TXT\\" + fileName;
            //var CrearFilePath = @"C:\\WMS_TXT\\" + fileName;
            ////var LeerFilePath = @"C:\\WMS_TXT\\DescargaPedidos\\";
            //var ftpUriCrear = "";
            ////-----------------------------------------------------------------------------

            ////---------------------Leer TXT Pedidos Despachados----------------------------
            //var ftpUriLeer = "";
            //var ftpUriFail = "";
            //var ftpUriSubmit = "";
            //-----------------------------------------------------------------------------

            for (int x = 1; x <= 2; x++)
            {
                if (x == 1)
                {
                    ruc_empresa = "SIL";
                    idEmpresa = 3;
                    //ftpUriCrear = "ftp://10.10.1.9/WMS/PRUEBAS/IN/PEDIDOS/";
                    //ftpUriLeer = "ftp://10.10.1.9/WMS/PRUEBAS/OUT/PEDIDOS/";
                    //ftpUriFail = "ftp://10.10.1.9/WMS/PRUEBAS/OUT/PEDIDOS/Fail/";
                    //ftpUriSubmit = "ftp://10.10.1.9/WMS/PRUEBAS/OUT/PEDIDOS/Submit/";
                    x = x + 10;
                }

                //if (x == 1)
                //{
                //    ruc_empresa = "SIL";
                //    idEmpresa = 1;
                //    //ftpUriCrear = "ftp://10.10.1.9/WMS/SILVESTRE/IN/PEDIDOS/";
                //    //ftpUriLeer = "ftp://10.10.1.9/WMS/SILVESTRE/OUT/PEDIDOS/";
                //    //ftpUriFail = "ftp://10.10.1.9/WMS/SILVESTRE/OUT/PEDIDOS/Fail/";
                //    //ftpUriSubmit = "ftp://10.10.1.9/WMS/SILVESTRE/OUT/PEDIDOS/Submit/";
                //}


                //if (x == 2)
                //{
                //    idEmpresa = 2;
                //    ruc_empresa = "NEO";
                //    //ftpUriCrear = "ftp://10.10.1.9/WMS/NEOAGRUM/IN/PEDIDOS/";
                //    //ftpUriLeer = "ftp://10.10.1.9/WMS/NEOAGRUM/OUT/PEDIDOS/";
                //    //ftpUriFail = "ftp://10.10.1.9/WMS/NEOAGRUM/OUT/PEDIDOS/Fail/";
                //    //ftpUriSubmit = "ftp://10.10.1.9/WMS/NEOAGRUM/OUT/PEDIDOS/Submit/";
                //}

                //LeerPedidosTXT_FTP(ftpUriLeer, ftpUriSubmit, ftpUriFail, ftpUser, ftpPassword, fileName, idEmpresa);
                LeerPedidosTXT_FTP(idEmpresa, ruc_empresa);

            }
        }

        //public static void LeerPedidosTXT_FTP(string RutaOrigen, string RutaSubmit, string RutaFail, string Login, string Password, string fileName, int idEmpresa, string NomEmpresa)
        public static void LeerPedidosTXT_FTP(int idEmpresa, string NomEmpresa)
        {
            //List<string> LstArchivo = new List<string>();
            //LstArchivo = LeerDirectorioFic(RutaOrigen, Login, Password);
            //Console.Write("Lista de archivos a cargar " + LstArchivo.Count().ToString() + ".");

            List<Cargar_Pedidos_ConfirmacionResult> Lista = Listar_Pedidos_Confirmacion(idEmpresa, NomEmpresa);

            //ReadFileFromFTP(idEmpresa, LstArchivo, RutaOrigen, RutaSubmit, RutaFail, Login, Password);

            if(Lista.Count > 0)
            {
                ReadFileFromFTP(idEmpresa, Lista);
            }
            else
            {
                Console.WriteLine("No se tiene pedidos confirmados para "+ NomEmpresa); 
            }


           
        }

        public static List<string> LeerDirectorioFic(string ficFTP, string user, string pass)
        {
            var mensaje = "";
            List<string> directories = new List<string>();
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ficFTP);
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());

                string line = streamReader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    directories.Add(line);
                    line = streamReader.ReadLine();
                }
                streamReader.Close();


            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return directories;

        }

        public static List<Cargar_Pedidos_ConfirmacionResult> Listar_Pedidos_Confirmacion(int idEmpresa, string ruc_empresa)
        {
            WmsWCF.WmsWCFClient objWMS = new WmsWCF.WmsWCFClient();
            List<Cargar_Pedidos_ConfirmacionResult> LstPedidos = new List<Cargar_Pedidos_ConfirmacionResult>(); 

            try
            {
                LstPedidos = objWMS.WmsPedidos_Confirmacion_Listar(idEmpresa,1, ruc_empresa).ToList(); 
            }
            catch (Exception ex)
            {
                throw ex; 
            }

            return LstPedidos;
        }


        //public static void ReadFileFromFTP(int idEmpresa, List<string> files, string RutaOrigen, string RutaSubmit, string RutaFail, string user, string pass)
        public static void ReadFileFromFTP(int idEmpresa, List<Cargar_Pedidos_ConfirmacionResult> ListaPedido )
        {
            List<gsInterfacePedidos_LeerResult> lstPedidos;
            //string Nombre; 
            int codigoUsuario = 1;
            int idOrdenVenta;
          

            //foreach (Cargar_Pedidos_ConfirmacionResult file in ListaPedido)
            //{

                try
                {
                    lstPedidos = new List<gsInterfacePedidos_LeerResult>();
 

                    foreach (Cargar_Pedidos_ConfirmacionResult file in ListaPedido)
                    {
                        gsInterfacePedidos_LeerResult Pedido = new gsInterfacePedidos_LeerResult();
                        //Pedido.NroPedido = "30313"; // file.NumeroDeDocumento; // Campos[0];                     //Op OV
                        //Pedido.ID_Item = "10210109003514"; //( file.CodigoDeArticulo;  //  Campos[1];                       //Item
                        //Pedido.Lote = "170831-0171-2050"; // file.CodigoDeLote; // Campos[2];                          //Lote
                        //Pedido.CantidadPedido = 6; // (decimal)file.UnidadesPedido; // decimal.Parse(Campos[3]);    //Cantidad
                        //Pedido.CantidadEntrega = 6; // (decimal)file.UnidadesEntregadas; // int.Parse(Campos[4]);       //Cantidad entregada
                        //Pedido.CantidadPendiente = 0; // (decimal)(file.Diferencia); // decimal.Parse(Campos[5]); //Diferencia
                        //Pedido.EstadoPedido = "D"; // file.Anticipado; // Campos[7];             // Estado
                        //Pedido.Id_Amarre = 76851; // decimal.Parse(file.IDDeLinea); // decimal.Parse(Campos[8]); // Id_Amarre
                        //Pedido.TransferidoTabla = "SILPT"; // file.Empresa;
                        //Pedido.Servicio = file.NumeroDeAlbaran;

                        /////////////-------------------------------------------------
                        Pedido.NroPedido = file.NumeroDeDocumento; // Campos[0];                     //Op OV
                        Pedido.ID_Item = file.CodigoDeArticulo;  //  Campos[1];                       //Item
                        Pedido.Lote = file.CodigoDeLote; // Campos[2];                          //Lote
                        Pedido.CantidadPedido = (decimal)file.UnidadesPedido; // decimal.Parse(Campos[3]);    //Cantidad
                        Pedido.CantidadEntrega = (decimal)file.UnidadesEntregadas; // int.Parse(Campos[4]);       //Cantidad entregada
                        Pedido.CantidadPendiente = (decimal)(file.Diferencia); // decimal.Parse(Campos[5]); //Diferencia
                        Pedido.EstadoPedido = file.Anticipado; // Campos[7];             // Estado
                        Pedido.Id_Amarre = decimal.Parse(file.IDDeLinea); // decimal.Parse(Campos[8]); // Id_Amarre
                        Pedido.TransferidoTabla = file.Empresa;
                        Pedido.Servicio = file.NumeroDeAlbaran;

                        lstPedidos.Add(Pedido);

                    }


                    lstPedidos = lstPedidos.OrderBy(x => x.Op).ToList(); 

                    foreach (gsInterfacePedidos_LeerResult pedido in lstPedidos)
                    {
                        try
                        {
                            WmsWCF.WmsWCFClient objWmsInsert = new WmsWCF.WmsWCFClient();
                            objWmsInsert.WmsPedidosPendientes_Insertar(idEmpresa, 1, pedido.NroPedido, pedido.ID_Item, pedido.Lote,
                                                                        decimal.Parse(pedido.CantidadPedido.ToString()), decimal.Parse(pedido.CantidadEntrega.ToString()),
                                                                        decimal.Parse(pedido.CantidadPendiente.ToString()),
                                                                        pedido.EstadoPedido, int.Parse(pedido.Id_Amarre.ToString())); //1=SILVESTRE; 2=NEOAGRUM; 6=INATEC

                            //objWmsInsert.WmsPedidosPendientes_UpdateEstilos(idEmpresa, codigoUsuario, pedido.TransferidoTabla, pedido.Servicio);

                        Console.Write("Se registro Op" + pedido.NroPedido + " - " + pedido.Id_Amarre);
                        }
                        catch (Exception ex)
                        {
                            Console.Write("Error: Registrar TXT: " + pedido.NroPedido.ToString() + "-" + pedido.ID_Item.ToString() + ", " + ex.Message.ToString());
                        }
                    }

                    Console.Write("Se registro los pedidos consumidos." );

                //---------------------------Pedido--------------------------------

                //var lstOpOV = lstPedidos.Select(x => x.NroPedido).Distinct();


                foreach (gsInterfacePedidos_LeerResult pedido in lstPedidos)
                {

                    try
                    {
                        GuiaWCFClient objGuiaVentaWCF = new GuiaWCFClient();
                        OrdenVentaWCFClient objOrdenVentaWCF = new OrdenVentaWCFClient();

                        gsOV_BuscarCabeceraResult objOrdenVentaCab;
                        gsOV_BuscarDetalleResult[] objOrdenVentaDet = null;
                        gsGuia_BuscarCabeceraResult objGuiaVentaCab = new gsGuia_BuscarCabeceraResult();
                        gsGuia_BuscarDetalleResult[] objGuiaVentaDet = null;
                        gsGuia_BuscarDetalleResult objGuiaVentaDetUpdate = null;
                        gsOV_BuscarDetalleResult objOrdenVenta_Linea = null;
                        List<DM.gsItem_BuscarResult> lstProductos = new List<DM.gsItem_BuscarResult>();
                        gsOV_BuscarImpuestoResult[] lstImpuestos = null;
                        GuiaVenta_LotesItemsResult[] lstLotes = null;

                        bool? bloqueado = false;
                        string mensajeBloqueo = null;
                        AgendaWCFClient objAgendaWCFClient;

                        VBG01134Result objAgendaCliente;

                        decimal? lineaCredito = null;
                        decimal? Id_Amarre = 0;
                        decimal? TC = 0;

                        DateTime? fechaVecimiento = null;

                        Console.Write("OP:" + pedido.NroPedido);
                        objAgendaWCFClient = new AgendaWCFClient();
                        objAgendaCliente = new VBG01134Result();

                        idOrdenVenta = int.Parse(pedido.NroPedido.ToString());
                        objOrdenVentaCab = objOrdenVentaWCF.OrdenVenta_Buscar_Guia(idEmpresa, codigoUsuario, idOrdenVenta, ref objOrdenVentaDet, ref lstImpuestos, ref bloqueado, ref mensajeBloqueo);

                        objAgendaCliente = objAgendaWCFClient.Agenda_BuscarCliente(idEmpresa, codigoUsuario, objOrdenVentaCab.ID_Agenda,
                            ref lineaCredito, ref fechaVecimiento, ref TC);
                        Id_Amarre = pedido.Id_Amarre;

                        objOrdenVenta_Linea = objOrdenVentaDet.ToList().FindAll(x => x.ID_Amarre == Id_Amarre).Single();

                        int idGuiaOp = 0;
                        int idGuiaOpLinea = 0;
                        int item_id = 0;

                        idGuiaOp = int.Parse(objOrdenVentaCab.OpGuia.ToString());
                        idGuiaOpLinea = int.Parse(objOrdenVenta_Linea.OpGuia.ToString());
                        item_id = int.Parse(objOrdenVenta_Linea.Item_ID.ToString());

                        if (objOrdenVenta_Linea == null)
                        {
                        }
                        else
                        {
                            if (idGuiaOpLinea > 0)
                            {
                                objGuiaVentaCab = objGuiaVentaWCF.GuiaVenta_Buscar(idEmpresa, codigoUsuario, idGuiaOp, ref objGuiaVentaDet, ref bloqueado, ref mensajeBloqueo);
                            }
                            else
                            {
                                objGuiaVentaCab = GuiaVenta_ObtenerCabecera(objOrdenVentaCab, idGuiaOp);
                                objGuiaVentaDet = GuiaVenta_ObtenerDetalle(objOrdenVentaCab, objOrdenVentaDet, idEmpresa, codigoUsuario).ToArray();
                            }

                            if (idGuiaOp > 0)
                            {
                                lstLotes = objGuiaVentaWCF.GuiaVenta_LotesItemBuscar(idEmpresa, codigoUsuario, int.Parse(idGuiaOp.ToString()), int.Parse(objOrdenVenta_Linea.Item_ID.ToString()));
                            }




                            List<GuiaVenta_LotesItemsResult> LotesUp = new List<GuiaVenta_LotesItemsResult>();
                            if (lstLotes == null)
                            {
                                LotesUp = new List<GuiaVenta_LotesItemsResult>();
                            }
                            else
                            {
                                LotesUp = ((GuiaVenta_LotesItemsResult[])lstLotes).ToList();
                            }

                            objGuiaVentaDetUpdate = GuiaVenta_ObtenerDetalle_Update(objGuiaVentaDet, lstPedidos, Id_Amarre, ref LotesUp, item_id);

                            lstLotes = (GuiaVenta_LotesItemsResult[])LotesUp.ToArray();



                            try
                            {
                                int Error = 0; 
                                int cont = 0; 
                                WmsWCF.WmsWCFClient objWmsInsert = new WmsWCF.WmsWCFClient();

                                List<VBG00971Result> Lista_LoteVar = objGuiaVentaWCF.GuiaVenta_BuscarLotesxItem(idEmpresa, 1,  int.Parse( pedido.NroPedido) , (int)item_id, (int)objGuiaVentaCab.ID_AlmacenAnexo, (int)pedido.Id_Amarre).ToList();

                                foreach (VBG00971Result Lote in Lista_LoteVar)
                                {
                                    if (Lote.Lote == pedido.Lote)
                                    {
                                        cont++;    
                                        break;
                                    }
                                }

                                if(cont == 0)
                                {
                                    objWmsInsert.WmsPedidosPendientes_Update(idEmpresa, codigoUsuario, pedido.Lote , (int)pedido.Id_Amarre, "F", "No se encuentra Lote para el Item");
                                }
                                else
                                {
                                    try
                                    {
                                        objGuiaVentaWCF.GuiaVenta_Registrar(idEmpresa, 1, objGuiaVentaCab, objGuiaVentaDetUpdate, decimal.Parse(idGuiaOp.ToString()), lstLotes);
                                        objWmsInsert.WmsPedidosPendientes_UpdateEstilos(idEmpresa, codigoUsuario, pedido.TransferidoTabla, pedido.Servicio);
                                        objWmsInsert.WmsPedidosPendientes_Update(idEmpresa, codigoUsuario, pedido.Lote, (int)pedido.Id_Amarre, "S", "Se registro correctamente.");

                                    }
                                    catch (Exception ex)
                                    {
                                        objWmsInsert.WmsPedidosPendientes_Update(idEmpresa, codigoUsuario, pedido.Lote, (int)pedido.Id_Amarre, "F", "Error, al registrar la Guia.");
                                        Console.Write("Error al registrar guias: " + ex.Message.ToString());
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                 
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write("Error: Registrar Guia: " + pedido.NroPedido.ToString() + "-" + pedido.ID_Item.ToString() + ", " + ex.Message.ToString());
                    }
                }

                ////------------------Mover archivos---------

                //    //MoverArchivos(RutaOrigen, RutaSubmit, file, user, pass);
                ////}
                }
                catch(Exception ex)
                {
                     Console.Write("Error: Registrar Guia, " + ex.Message.ToString());
                //MoverArchivos(RutaOrigen, RutaFail, file, user, pass);
                }
            //}
        }

        static gsGuia_BuscarCabeceraResult GuiaVenta_ObtenerCabecera(gsOV_BuscarCabeceraResult objOrdenVentaCab_R, decimal idGuiaVenta)
        {
            gsGuia_BuscarCabeceraResult objGuiaVentaCab;

            try
            {
                objGuiaVentaCab = new gsGuia_BuscarCabeceraResult();

                objGuiaVentaCab.Op = idGuiaVenta;
                objGuiaVentaCab.ID_Almacen = objOrdenVentaCab_R.ID_AgendaOrigen.ToString();
                objGuiaVentaCab.Fecha = DateTime.Now;
                objGuiaVentaCab.FechaInicioTraslado = DateTime.Now;
                objGuiaVentaCab.ID_MotivoTraslado = 1;
                objGuiaVentaCab.Serie = string.Empty;   // Revisar
                objGuiaVentaCab.Numero = 0;           // Revisar
                objGuiaVentaCab.ID_Agenda = objOrdenVentaCab_R.ID_Agenda;

                objGuiaVentaCab.ID_Envio = (decimal)objOrdenVentaCab_R.ID_Envio; // 2 
                objGuiaVentaCab.Observaciones = objOrdenVentaCab_R.Observaciones;

                objGuiaVentaCab.Transportista = objOrdenVentaCab_R.Transporte;  // Revisar
                objGuiaVentaCab.Chofer = null;
                objGuiaVentaCab.ID_AgendaAnexo = objOrdenVentaCab_R.ID_AgendaAnexo;

                objGuiaVentaCab.ID_AlmacenAnexo = (decimal)objOrdenVentaCab_R.ID_Almacen; // ID_AlmacenAnexo
                objGuiaVentaCab.ID_AgendaDireccion = objOrdenVentaCab_R.ID_AgendaDireccion;
                objGuiaVentaCab.ID_AgendaDireccion2 = objOrdenVentaCab_R.ID_AgendaDireccion2;
                objGuiaVentaCab.ID_Transportista = objOrdenVentaCab_R.ID_Transportista;

                objGuiaVentaCab.ID_Vehiculo = objOrdenVentaCab_R.ID_Vehiculo1;
                objGuiaVentaCab.ID_Vehiculo2 = objOrdenVentaCab_R.ID_Vehiculo2;
                objGuiaVentaCab.ID_Vehiculo3 = objOrdenVentaCab_R.ID_Vehiculo3;
                objGuiaVentaCab.ID_Chofer =  objOrdenVentaCab_R.ID_Chofer;
                objGuiaVentaCab.NotasDespacho = objOrdenVentaCab_R.NotasDespacho;

                objGuiaVentaCab.ID_CondicionCredito = objOrdenVentaCab_R.ID_CondicionCredito;
                objGuiaVentaCab.TransportistaRUC = objOrdenVentaCab_R.ID_Transportista; // Revisar

                objGuiaVentaCab.TransportistaDomicilio = string.Empty; // Revisar
                objGuiaVentaCab.TransportistaMarca = null;
                objGuiaVentaCab.TransportistaModelo = null;
                objGuiaVentaCab.TransportistaPlaca = null;
                objGuiaVentaCab.TransportistaCertInscripcion = null;
                objGuiaVentaCab.TransportistaChofer = null;
                objGuiaVentaCab.TransportistaLicencia = null;
                objGuiaVentaCab.CompPagoTipo = null;
                objGuiaVentaCab.CompPagoNro = null;

                objGuiaVentaCab.CompPagoFechaEmision = DateTime.Now;

                objGuiaVentaCab.ID_AgendaOrigen = objOrdenVentaCab_R.ID_AgendaOrigen;
                objGuiaVentaCab.DireccionOrigenSucursal = objOrdenVentaCab_R.ID_Almacen;
                objGuiaVentaCab.DireccionOrigenReferencia = objOrdenVentaCab_R.DireccionOrigenReferencia;
                objGuiaVentaCab.DireccionOrigenDireccion = objOrdenVentaCab_R.DireccionOrigenDireccion;

                objGuiaVentaCab.ID_AgendaDestino = objOrdenVentaCab_R.ID_AgendaDestino;
                objGuiaVentaCab.DireccionDestinoSucursal = objOrdenVentaCab_R.DireccionDestinoSucursal;
                objGuiaVentaCab.DireccionDestinoReferencia = objOrdenVentaCab_R.DireccionDestinoReferencia;
                objGuiaVentaCab.DireccionDestinoDireccion = objOrdenVentaCab_R.DireccionDestinoDireccion;

                objGuiaVentaCab.HoraAtencionOpcion1_Desde = objOrdenVentaCab_R.HoraAtencionOpcion1_Desde;
                objGuiaVentaCab.HoraAtencionOpcion1_Hasta = objOrdenVentaCab_R.HoraAtencionOpcion1_Hasta;
                objGuiaVentaCab.HoraAtencionOpcion2_Desde = objOrdenVentaCab_R.HoraAtencionOpcion2_Desde;
                objGuiaVentaCab.HoraAtencionOpcion2_Hasta = objOrdenVentaCab_R.HoraAtencionOpcion2_Hasta;
                objGuiaVentaCab.HoraAtencionOpcion3_Desde = objOrdenVentaCab_R.HoraAtencionOpcion3_Desde;
                objGuiaVentaCab.HoraAtencionOpcion3_Hasta = objOrdenVentaCab_R.HoraAtencionOpcion3_Hasta;

                

                return objGuiaVentaCab;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static List<gsGuia_BuscarDetalleResult> GuiaVenta_ObtenerDetalle(gsOV_BuscarCabeceraResult objOrdenVentaCab_R,
        gsOV_BuscarDetalleResult[] objOrdenVentaDet, int idEmpresa, int codigoUsuario)
        {

            List<gsGuia_BuscarDetalleResult> lstPedidoDet = new List<gsGuia_BuscarDetalleResult>();
            gsGuia_BuscarDetalleResult objProducto;

            try
            {

                foreach (gsOV_BuscarDetalleResult producto in objOrdenVentaDet)
                {
                    objProducto = new gsGuia_BuscarDetalleResult();

                    // exec VBG00495 @p1 output,19250,'OV',51646,'10210109009418',NULL,
                    // NULL,NULL,NULL,202,48.0000,0.0000,0.0000,48.0000,0.0000,'Unidad',1.0000,48.0000,'Unidad',48.0000,'Unidad',48.0000,NULL	

                    objProducto.ID_Amarre = 0; //Amarre Guia
                    objProducto.Op = 0;
                    objProducto.TablaOrigen = "OV";
                    objProducto.Linea = producto.ID_Amarre;
                    objProducto.ID_Item = producto.ID_Item;
                    objProducto.ID_ItemAnexo = producto.ID_ItemAnexo;

                    objProducto.ID_CCosto = producto.ID_CCosto;
                    objProducto.ID_UnidadGestion = producto.ID_UnidadGestion;
                    objProducto.ID_UnidadProyecto = producto.ID_UnidadProyecto;
                    objProducto.Item_ID = producto.Item_ID;

                    objProducto.CantidadBruta = producto.Cantidad;
                    objProducto.Bultos = 0;
                    objProducto.Tara = 0;
                    objProducto.Cantidad = producto.Cantidad;
                    objProducto.Ajuste = 0;

                    objProducto.ID_UnidadInv = producto.ID_UnidadInv;
                    objProducto.FactorUnidadInv = producto.FactorUnidadInv;
                    objProducto.CantidadUnidadInv = producto.CantidadUnidadInv; // Consultar como se calcula realmente

                    objProducto.ID_UnidadDoc = producto.ID_UnidadDoc;
                    objProducto.CantidadUnidadDoc = producto.CantidadUnidadDoc; // Consultar como se calcula realmente
                    objProducto.ID_UnidadControl = producto.ID_UnidadDoc;
                    objProducto.CantidadUnidadControl = producto.CantidadUnidadDoc;
                    objProducto.Observaciones = producto.Observaciones;

                    objProducto.Estado = (int)producto.Estado;

                    lstPedidoDet.Add(objProducto);
                }
                return lstPedidoDet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static gsGuia_BuscarDetalleResult GuiaVenta_ObtenerDetalle_Update(gsGuia_BuscarDetalleResult[] objGuiaVentaDet, 
            List<gsInterfacePedidos_LeerResult> lstPedido, decimal? Id_AmarreUp, ref List<GuiaVenta_LotesItemsResult> lstLotesI, int item_id)
        {

            decimal? id_amarreOV;
            decimal? id_amarreGuia;
            id_amarreOV = Id_AmarreUp;
            List<GuiaVenta_LotesItemsResult> LotesUp = new List<GuiaVenta_LotesItemsResult>() ;

            try
            {

                foreach (gsInterfacePedidos_LeerResult producto in lstPedido)
                {
                    id_amarreGuia = 0; 
                    if (id_amarreOV == producto.Id_Amarre)
                    {
                        GuiaVenta_LotesItemsResult Lote = new GuiaVenta_LotesItemsResult();

                        Lote.Linea = (decimal)producto.Id_Amarre;
                        Lote.Lote = producto.Lote;
                        Lote.Cantidad = (decimal)producto.CantidadEntrega;
                        Lote.CantidadUnidadControl = (decimal)producto.CantidadEntrega;
                        Lote.Item_ID = item_id;

                        foreach(GuiaVenta_LotesItemsResult lt in lstLotesI)
                        {
                            if (lt.Lote == Lote.Lote)
                            {
                                id_amarreGuia = lt.ID_Amarre;
                            }
                        }

                        Lote.ID_Amarre = (decimal)id_amarreGuia;

                        lstLotesI.RemoveAll(x => x.Lote == Lote.Lote);
                        lstLotesI.Add(Lote);
                    }
                }

                decimal CantidaEntrega = 0; 

                foreach (GuiaVenta_LotesItemsResult Lote in lstLotesI)
                {
                    if (id_amarreOV == Lote.Linea)
                    {
                        CantidaEntrega = CantidaEntrega + Lote.CantidadUnidadControl; 
                    }
                }

                gsGuia_BuscarDetalleResult GuiaDetalle = new gsGuia_BuscarDetalleResult();

                foreach (gsGuia_BuscarDetalleResult GuiaProducto in objGuiaVentaDet)
                {
                    if(id_amarreOV == GuiaProducto.Linea)
                    {
                        GuiaProducto.CantidadBruta = CantidaEntrega; 
                        GuiaProducto.Bultos = 0;
                        GuiaProducto.Tara = 0;
                        GuiaProducto.Cantidad = CantidaEntrega;
                        GuiaProducto.Ajuste = 0;

                        GuiaProducto.CantidadUnidadInv = CantidaEntrega; // Cantidad
                        GuiaProducto.CantidadUnidadDoc = CantidaEntrega; // Cantidad
                        GuiaProducto.CantidadUnidadControl = CantidaEntrega;   // Cantidad

                        GuiaDetalle = GuiaProducto;

                        break;
                    }
                }

                return GuiaDetalle; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------------------------------

        public static void MoverArchivos(string RutaOrigen, string RutaDestino, string Archivo, string user, string pass)
        {

            try
            {
                var CrearFilePath = @"C:\\WMS_TXT\\"; // + Archivo;
                string UserName;
                string Password;
                string LocalFolder;
                string FileName;
                string FTPFileFullPath;
                string ArchFilePath;
                string LocalFolderPath;
                string FTPFileFullPathDestino;

                UserName = user;
                Password = pass;
                LocalFolder = CrearFilePath;
                FTPFileFullPath = RutaOrigen + Archivo;
                FTPFileFullPathDestino = RutaDestino + Archivo;
                LocalFolderPath = CrearFilePath + Archivo;
                FileName = Archivo;
                ArchFilePath = RutaDestino + Archivo;


                //Download the file to local folder
                WebClient Webrequest = new WebClient();
                Webrequest.Credentials = new NetworkCredential(UserName, Password);

                byte[] newFileData = Webrequest.DownloadData(FTPFileFullPath);
                File.WriteAllBytes(LocalFolderPath, newFileData);

                UploadFTP(LocalFolderPath, RutaDestino, user, pass);

                DeleteFileOnFtpServer(FTPFileFullPath, user, pass);
            }
            catch (Exception ex)
            {
                
            }
        }

        public static bool DeleteFileOnFtpServer(string serverUri, string ftpUsername, string ftpPassword)
        {
            try
            {
                // Get the object used to communicate with the server.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);
                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                request.Method = WebRequestMethods.Ftp.DeleteFile;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                //Console.WriteLine("Delete status: {0}", response.StatusDescription);
                response.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void UploadFTP(string FilePath, string RemotePath, string Login, string Password)

        {

            using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                string url = Path.Combine(RemotePath, Path.GetFileName(FilePath));

                // Creo el objeto ftp
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(url);

                // Fijo las credenciales, usuario y contraseña
                ftp.Credentials = new NetworkCredential(Login, Password);

                // Le digo que no mantenga la conexión activa al terminar.
                ftp.KeepAlive = false;

                // Indicamos que la operación es subir un archivo...
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                // â€¦ en modo binario â€¦ (podria ser como ASCII)
                ftp.UseBinary = true;

                // Indicamos la longitud total de lo que vamos a enviar.
                ftp.ContentLength = fs.Length;

                // Desactivo cualquier posible proxy http.
                // Ojo pues de saltar este paso podría usar 
                // un proxy configurado en iexplorer
                ftp.Proxy = null;

                // Pongo el stream al inicio
                fs.Position = 0;

                // Configuro el buffer a 2 KBytes
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];

                int contentLen;

                // obtener el stream del socket sobre el que se va a escribir.
                using (Stream strm = ftp.GetRequestStream())
                {
                    // Leer del buffer 2kb cada vez
                    contentLen = fs.Read(buff, 0, buffLength);

                    // mientras haya datos en el buffer â€¦.
                    while (contentLen != 0)
                    {
                        // escribir en el stream de conexión
                        //el contenido del stream del fichero
                        strm.Write(buff, 0, contentLen);
                        contentLen = fs.Read(buff, 0, buffLength);
                    }

                }

            }

        }

    }
}
