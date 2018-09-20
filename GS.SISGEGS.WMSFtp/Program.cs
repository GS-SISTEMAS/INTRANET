using System.IO;
using System.Net;
using System;
using System.Text;
using System.Collections.Generic;
using GS.SISGEGS.WMSFtp.WmsWCF;
using System.Configuration;

namespace GS.SISGEGS.WMSFtp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WMSPendientes();
        }

        static void WMSPendientes() {
            WmsWCF.WmsWCFClient objWms = new WmsWCF.WmsWCFClient();
            var listaPendientes = objWms.WmsPendientes_Envio(3, 1); //1=SILVESTRE; 2=NEOAGRUM; 6=INATEC
            var fileName = "WMS_Mercaderia_" + DateTime.Now.ToShortDateString().Replace("/", "")+ "_"+ DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString() + ".txt";

            StringBuilder sbText = new StringBuilder();
            var strLinea = "";
            foreach (var pendiente in listaPendientes)
            {
                strLinea += pendiente.Op + "|";
                strLinea += pendiente.ID_Agenda + "|";
                strLinea += pendiente.Agenda + "|";
                strLinea += pendiente.NoRegistro + "|";
                strLinea += pendiente.FechaOrden + "|";
                strLinea += pendiente.ID_Moneda + "|";
                strLinea += pendiente.Moneda + "|";
                strLinea += pendiente.Total + "|";
                strLinea += pendiente.ID_Item + "|";
                strLinea += pendiente.Nombre + "|";
                strLinea += pendiente.Unidad + "|";
                strLinea += pendiente.Cantidad;

                sbText.AppendLine(strLinea);

                strLinea = "";
            }

            //var newFilePath = @"D:\\" + fileName;
            var newFilePath = @"C:\\WMS_TXT\\" + fileName;
            File.Create(newFilePath).Close();
            if (File.Exists(newFilePath))
            {
                File.WriteAllText(newFilePath, sbText.ToString());

                var filePath = newFilePath;
                //SILVESTRE
                //var ftpUri = "ftp://10.10.1.9/WMS/SILVESTRE/IN/";

                //NEOAGRUM
                var ftpUri = "ftp://10.10.1.9/WMS/NEOAGRUM/IN/";

                var ftpUser = @"ftpwms";
                var ftpPassword = "wmsapia";

                UploadFTP(filePath, ftpUri, ftpUser, ftpPassword);
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
