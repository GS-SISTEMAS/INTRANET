using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using GS.SISGEGS.WMSProduccion;
using System.IO;
using System.Net;

namespace GS.SISGEGS.WMSProduccion
{
    class Program
    {
        static void Main(string[] args)
        {

            string strlocalDirectory = @"C:\\WMS_TXT\\";
            string strsourcePrueba = "ftp://10.10.1.9/WMS/NEOAGRUM/IN/RECEPCION/";
            string strsourceSilvestre = "ftp://10.10.1.9/WMS/SILVESTRE/IN/RECEPCION/";
            string strsourceNeoagrum = "ftp://10.10.1.9/WMS/NEOAGRUM/IN/RECEPCION/";
            string strsourceInatec = "ftp://10.10.1.9/WMS/INATEC/IN/RECEPCION/";
            string strftpUser = "ftpwms";
            string strftpPassword = "wmsapia";


            var cboResult = "ZPrueba";
            var source = "";
            var ftpUser = strftpUser;
            var ftpPassword = strftpPassword;
            var empresa = 0;
            var usuariobd = 0;
            var mensaje = "";

            if (cboResult == "ZPrueba")
            {
                empresa = 3;
                usuariobd = 1;
                source = strsourcePrueba;
            }
            try
            {
                WMSProduccion_PendientesRecibir(empresa, usuariobd, ftpUser, ftpPassword, source, strlocalDirectory);
                mensaje = "Se generó el archivo de pendientes.";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }


        }

        static void WMSProduccion_PendientesRecibir(int empresa, int usuariobd, string ftpUser, string ftpPassword, string source, string strlocalDirectory)
        {
            WmsWCF.WmsWCFClient objWms = new WmsWCF.WmsWCFClient();
            var listaPendientes = objWms.WmsProduccion_PendientesRecibir(empresa, usuariobd); //1=SILVESTRE; 2=NEOAGRUM; 6=INATEC
            var fileName = "WMSProduccionPendientes_" + DateTime.Now.ToShortDateString().Replace("/", "") + ".txt";

            StringBuilder sbText = new StringBuilder();
            var strLinea = "";
            foreach (var pendiente in listaPendientes)
            {
                strLinea += pendiente.Op + "|";
                strLinea += pendiente.Kardex + "|";
                strLinea += pendiente.Codigo + "|";
                strLinea += pendiente.Descripcion + "|";
                strLinea += pendiente.Lote + "|";
                strLinea += pendiente.FechaFabricacion + "|";
                strLinea += pendiente.FechaVencimiento + "|";
                strLinea += pendiente.Unidad + "|";
                strLinea += pendiente.Cantidad + "|";

                sbText.AppendLine(strLinea);

                Console.Write(strLinea);

                strLinea = "";
            }

            var localDirectory = strlocalDirectory;

            var newFilePath = localDirectory + fileName;


            File.Create(newFilePath).Close();
            if (File.Exists(newFilePath))
            {
                File.WriteAllText(newFilePath, sbText.ToString());

                var filePath = newFilePath;
                var ftpUri = source;
                UploadFTP(filePath, ftpUri, ftpUser, ftpPassword);
            }
        }

        public static void UploadFTP(string FilePath, string RemotePath, string Login, string Password)

        {
            var mensaje = "";
            try
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
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
        }
    }
}
