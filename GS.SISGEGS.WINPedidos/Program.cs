using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using GS.SISGEGS.DM;
using GS.SISGEGS.WINPedidos.OrdenVentaWCF;
using GS.SISGEGS.WINPedidos.AgendaWCF;
using GS.SISGEGS.WINPedidos.ImpuestoWCF;
using GS.SISGEGS.WINPedidos.ItemWCF;
using GS.SISGEGS.WINPedidos.GuiaWCF; 

namespace GS.SISGEGS.WINPedidos
{
    class Program
    {
        public static void Main(string[] args)
        {

            //--------------------------Credenciales---------------------------------------
            var ftpUser = @"ftpwms";
            var ftpPassword = "wmsapia";
            //int idEmpresa = 3;  //1=SILVESTRE; 2=NEOAGRUM; 6=INATEC

            int idEmpresa = 0;

            //--------------------Crear TXT Pedidos Pendidentes----------------------------
            //var CrearFilePath = @"C:\\Users\\cesar.coronel.GS\\Desktop\\Pruebas_TXT\\" + fileName;
            var CrearFilePath = @"C:\\WMS_TXT\\";
            //var LeerFilePath = @"C:\\WMS_TXT\\DescargaPedidos\\";
            var ftpUriCrear = "";
            //-----------------------------------------------------------------------------

            //---------------------Leer TXT Pedidos Despachados----------------------------
            var ftpUriLeer = ""; 
            //-----------------------------------------------------------------------------

            for (int x =1; x<=2; x++)
            {
                //if (x == 1)
                //{
                //    idEmpresa = 3;
                //ftpUriCrear = "ftp://10.10.1.9/WMS/SILVESTRE/IN/PRUEBAS/";
                //ftpUriLeer = "ftp://10.10.1.9/WMS/SILVESTRE/OUT/PRUEBAS/";
                //}

                if (x == 1)
                {
                    idEmpresa = 1;
                    //ftpUriCrear = "ftp://10.10.1.9/WMS/SILVESTRE/IN/PEDIDOS/";
                    //ftpUriLeer = "ftp://10.10.1.9/WMS/SILVESTRE/OUT/PEDIDOS/";
                    ftpUriCrear = "ftp://10.10.1.9/WMS/PRUEBAS/IN/PEDIDOS/";
                    ftpUriLeer = "ftp://10.10.1.9/WMS/PRUEBAS/OUT/PEDIDOS/";

                }

                if (x == 2)
                {
                    idEmpresa = 2;
                    //ftpUriCrear = "ftp://10.10.1.9/WMS/NEOAGRUM/IN/PEDIDOS/";
                    //ftpUriLeer = "ftp://10.10.1.9/WMS/NEOAGRUM/OUT/PEDIDOS/";
                    ftpUriCrear = "ftp://10.10.1.9/WMS/PRUEBAS/IN/PEDIDOS/";
                    ftpUriLeer = "ftp://10.10.1.9/WMS/PRUEBAS/OUT/PEDIDOS/";
                }


                CrearPedidosTXT_FTP(CrearFilePath, ftpUriCrear, ftpUser, ftpPassword, idEmpresa);
             }

        }

        public static void CrearPedidosTXT_FTP(string newFilePath, string RemotePath, string Login, string Password, int idEmpresa)

        {
            WmsWCF.WmsWCFClient objWms = new WmsWCF.WmsWCFClient();
            List<VBG00518_WMSResult> lista = new List<VBG00518_WMSResult>();

            lista = objWms.WmsPedidosPendientes_Envio(idEmpresa, 1).ToList(); //  1=SILVESTRE; 2=NEOAGRUM; 6=INATEC

            string EmpresaNom = "";

            if(idEmpresa ==1)
            {
                EmpresaNom = "Sil"; 
            }
            else if (idEmpresa == 2)
            {
                EmpresaNom = "Neo";
            }


            int f = 0;

            var listaPendientes = lista.OrderByDescending(x => x.Op).ToList();

            StringBuilder sbText = new StringBuilder();
            var strLinea = "";

            string newFilePathNew = ""; 
            string OpOld = "";
            string OpNew = "";
            int countOp = 0;
            var fileName = "";
            string segundos;


            Console.WriteLine("Inicio empresa:" + EmpresaNom);

            foreach (var pendiente in listaPendientes)
            {
                Console.WriteLine("Se registro OP: " + pendiente.Op.ToString() + ", item: " + pendiente.ID_Amarre);
            }


            //foreach (var pendiente in listaPendientes)
            //{
            //    if(countOp == 0)
            //    {
            //        OpOld = pendiente.Op.ToString();
            //        OpNew = pendiente.Op.ToString();
            //        countOp++; 
            //    }
            //    else
            //    {
            //        OpNew = pendiente.Op.ToString();
            //    }

                //    if(OpOld == OpNew)
                //    {
                //        strLinea += pendiente.Op + "|";

                //        strLinea += pendiente.ID_Agenda + "|";
                //        strLinea += pendiente.Agenda + "|";

                //        if(pendiente.Direccion.Length > 10)
                //        {
                //            strLinea += pendiente.Direccion.Substring(1, 10) + "|";
                //        }
                //        else
                //        {
                //            strLinea += pendiente.Direccion + "|";
                //        }


                //        //strLinea += pendiente.Provincia + "|";
                //        strLinea += pendiente.Departamento + "|";
                //        strLinea += pendiente.pais + "|";
                //        strLinea += pendiente.Ubigeo + "|";
                //        strLinea += pendiente.Telefono + "|";
                //        strLinea += pendiente.Observaciones + "|";
                //        //strLinea += pendiente.Fecha + "|";
                //        strLinea += pendiente.ID_Item + "|";
                //        //strLinea += pendiente.Nombre + "|";
                //        strLinea += pendiente.Cantidad + "|";
                //        strLinea += pendiente.ID_Vendedor + "|";
                //        //strLinea += pendiente.Vendedor + "|";

                //        strLinea += pendiente.ID_Amarre + "|";

                //        strLinea += pendiente.DireccionDestinoAgenda;

                //        sbText.AppendLine(strLinea);

                //        strLinea = "";
                //    }
                //    else
                //    {
                //        segundos = "";
                //        segundos = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                //        fileName = "";

                //        fileName = OpOld + "Ped_"+ EmpresaNom + "_"+ segundos + ".txt";
                //        newFilePathNew = newFilePath + fileName;

                //        File.Create(newFilePathNew).Close();

                //        if (File.Exists(newFilePathNew))
                //        {
                //            File.WriteAllText(newFilePathNew, sbText.ToString());

                //            //var filePath = sbText.ToString();
                //            var filePath = newFilePathNew;
                //            var ftpUri = RemotePath;

                //            var ftpUser = @Login;
                //            var ftpPassword = Password;

                //            UploadFTP(filePath, ftpUri, ftpUser, ftpPassword);
                //        }


                //        //-----------------------------------------------------------
                //        sbText = new StringBuilder();

                //        strLinea += pendiente.Op + "|";

                //        strLinea += pendiente.ID_Agenda + "|";
                //        strLinea += pendiente.Agenda + "|";

                //        if (pendiente.Direccion.Length > 10)
                //        {
                //            strLinea += pendiente.Direccion.Substring(1, 10) + "|";
                //        }
                //        else
                //        {
                //            strLinea += pendiente.Direccion + "|";
                //        }
                //        //strLinea += pendiente.Provincia + "|";
                //        strLinea += pendiente.Departamento + "|";
                //        strLinea += pendiente.pais + "|";
                //        strLinea += pendiente.Ubigeo + "|";
                //        strLinea += pendiente.Telefono + "|";
                //        strLinea += pendiente.Observaciones + "|";
                //        //strLinea += pendiente.Fecha + "|";
                //        strLinea += pendiente.ID_Item + "|";
                //        //strLinea += pendiente.Nombre + "|";
                //        strLinea += pendiente.Cantidad + "|";
                //        strLinea += pendiente.ID_Vendedor + "|";
                //        //strLinea += pendiente.Vendedor + "|";

                //        strLinea += pendiente.ID_Amarre + "|";

                //        strLinea += pendiente.DireccionDestinoAgenda;

                //        sbText.AppendLine(strLinea);

                //        strLinea = "";

                //        OpOld = pendiente.Op.ToString();
                //        OpNew = pendiente.Op.ToString();
                //    }
                //}
                //segundos = "";
                //segundos = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                //fileName = "";

                //fileName = OpOld + "Ped_" + EmpresaNom + "_" + segundos + ".txt";

                //newFilePathNew = newFilePath + fileName;

                //File.Create(newFilePathNew).Close();

                //if (File.Exists(newFilePathNew))
                //{
                //    File.WriteAllText(newFilePathNew, sbText.ToString());
                //    var filePath = newFilePathNew;
                //    var ftpUri = RemotePath;
                //    var ftpUser = @Login;
                //    var ftpPassword = Password;
                //    UploadFTP(filePath, ftpUri, ftpUser, ftpPassword);
                //}

                //Console.Write("Se registro el documento: " + fileName + ", correctamente.");

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
