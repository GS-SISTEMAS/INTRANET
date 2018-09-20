using System;
using System.Configuration;
using GS.WMS.ProduccionPendiente;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;


namespace GS.WMS.ProduccionPendiente
{
    class Program
    {
        public static void Main(string[] args)
        {
            var ftpUser = @"ftpwms";
            var ftpPassword = "wmsapia";
            //var ftpUri = "ftp://10.10.1.9/WMS/NEOAGRUM/IN/RECEPCION/";
            //var ftpUri = "ftp://10.10.1.9/WMS/SILVESTRE/IN/RECEPCION/";

            var ftpUri = "" ; 
            int idEmpresa = 0;


            for (int x = 1; x <= 2; x++)
            {
                if (x == 1)
                {
                    idEmpresa = 1;
                    ftpUri = "ftp://10.10.1.9/WMS/SILVESTRE/IN/RECEPCION/";

                }

                if (x == 2)
                {
                    idEmpresa = 2;
                    ftpUri = "ftp://10.10.1.9/WMS/NEOAGRUM/IN/RECEPCION/";
                   
                }
        

                WMSProduccion_PendientesRecibir(idEmpresa, 1, ftpUser, ftpPassword, ftpUri);
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

        static void WMSProduccion_PendientesRecibir(int empresa, int usuariobd, string ftpUser, string ftpPassword, string source)
        {
            string tiempo = "";
            tiempo = DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() +   DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString(); 
            WmsWCF.WmsWCFClient objWms = new WmsWCF.WmsWCFClient();
            var listaPendientes = objWms.WmsProduccion_PendientesRecibir(empresa, usuariobd); //1=SILVESTRE; 2=NEOAGRUM; 6=INATEC

            var strLinea = "";

            //foreach (var pendiente in listaPendientes)
            //{
            //    StringBuilder sbText = new StringBuilder();
            //    var fileName = "1PRD" + pendiente.Op + "_" + tiempo + ".txt";
            //    strLinea += pendiente.Op + "|";
            //    strLinea += pendiente.Kardex + "|";
            //    strLinea += pendiente.Codigo + "|";
            //    strLinea += pendiente.Descripcion + "|";
            //    strLinea += pendiente.Lote + "|";
            //    strLinea += pendiente.FechaFabricacion.Value.ToString("dd/MM/yyyy") + "|";
            //    strLinea += pendiente.FechaVencimiento.Value.ToString("dd/MM/yyyy") + "|";
            //    strLinea += pendiente.Unidad + "|";
            //    strLinea += pendiente.Cantidad + "|";

            //    sbText.AppendLine(strLinea);

            //    Console.Write(strLinea);

            //    var localDirectory = "C:\\WMS_TXT\\";

            //    var newFilePath = @"" + localDirectory + fileName;


            //    File.Create(newFilePath).Close();
            //    if (File.Exists(newFilePath))
            //    {
            //        File.WriteAllText(newFilePath, sbText.ToString());

            //        var filePath = newFilePath;
            //        var ftpUri = source;
            //        UploadFTP(filePath, ftpUri, ftpUser, ftpPassword);
            //    }

            //    strLinea = "";
            //}

          
        }

        static string getFormatDate()
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            var format = culture.DateTimeFormat.ShortDatePattern;
            //Console.WriteLine(format);
            switch (format)
            {
                case "dd/MM/yyyy": return "DMY";
                case "dd/M/yyyy": return "DMY";
                case "d/M/yyyy": return "DMY";
                case "d/MM/yyyy": return "DMY";
                case "MM/d/yyyy": return "MDY";
                case "MM/dd/yyyy": return "MDY";
                case "M/dd/yyyy": return "MDY";
                case "M/d/yyyy": return "MDY";
                default: return "DMY";
            }
        }
    }
}
