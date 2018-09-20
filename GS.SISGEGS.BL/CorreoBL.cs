using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace GS.SISGEGS.BL
{
    public interface ICorreoBL {
        void EnvioCorreo(string to, string toCorreo, string cc, string ccCorreo, string asunto, string message);
        void MerlinEnvioCorreo(string to, string toCorreo, string cc, string ccCorreo, string asunto, string message);

        void MerlinEnvioCorreoAdjunto(string to, string toCorreo, string cc, string ccCorreo, string asunto, string mensaje, string FilePath); 
    }
    public class CorreoBL : ICorreoBL
    {
        public void EnvioCorreo(string to, string toCorreo, string cc, string ccCorreo, string asunto, string mensaje) {
            using (SmtpClient client = new SmtpClient())
            {
                try
                {

                    var host = ConfigurationManager.AppSettings.Get("SmtpHost");
                    var port = ConfigurationManager.AppSettings.Get("SmtpPort");
                    var credentialUser = ConfigurationManager.AppSettings.Get("SmtpCredentialUser");
                    var credentialPass = ConfigurationManager.AppSettings.Get("SmtpCredentialPass");

                    client.Host = host;
                    client.Port = int.Parse(port);
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = true;
                    client.Credentials = new NetworkCredential(credentialUser, credentialPass);

                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(credentialUser, "Créditos y Cobranzas");
                    message.To.Add(new MailAddress(toCorreo, to));
                    if (ccCorreo != null && cc != null) message.CC.Add(new MailAddress(ccCorreo, cc));
                    message.Subject = asunto;
                    message.IsBodyHtml = true;
                    message.Body = mensaje;
                    message.BodyEncoding = Encoding.UTF8;
                    client.Send(message);

                }
                catch (Exception ex)
                {
                    throw new ArgumentException( "Error: BL Metodo  EnvioCorreo, " + ex.Message);
                   //ex.Message + "Error consultar por las referencias de la sucursal en la base de datos.";
                }

              
            }
        }

        public void MerlinEnvioCorreo(string to, string toCorreo, string cc, string ccCorreo, string asunto, string mensaje)
        {
            using (SmtpClient client = new SmtpClient())
            {
                try
                {

                    var host = ConfigurationManager.AppSettings.Get("SmtpHost");
                    var port = ConfigurationManager.AppSettings.Get("SmtpPort");
                    var credentialUser = ConfigurationManager.AppSettings.Get("SmtpMerlinCredentialUser");
                    var credentialPass = ConfigurationManager.AppSettings.Get("SmtpMerlinCredentialPass");

                    client.Host = host;
                    client.Port = int.Parse(port);
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = true;
                    client.Credentials = new NetworkCredential(credentialUser, credentialPass);

                    MailMessage message = new MailMessage();
                    //AlternateView alterView = ContentToAlternateView(bodyHtml);
                    //message.AlternateViews.Add(alterView);
                    message.From = new MailAddress(credentialUser, "Merlin");

                    foreach (var correo in toCorreo.Split(';')) {
                        message.To.Add(new MailAddress(correo));
                    }

                    if (ccCorreo != null && cc != null) {
                        foreach (var copia in ccCorreo.Split(';')) {
                            message.CC.Add(new MailAddress(copia));
                        }
                    }

                    message.Subject = asunto;
                    message.IsBodyHtml = true;
                    message.Body = mensaje;
                    message.BodyEncoding = Encoding.UTF8;
 
                    client.Send(message);

                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message + "Error consultar por las referencias de la sucursal en la base de datos.");
                }


            }
        }

        public void MerlinEnvioCorreoAdjunto(string to, string toCorreo, string cc, string ccCorreo, string asunto, string mensaje, string FilePath)
        {
            using (SmtpClient client = new SmtpClient())
            {
                try
                {

                    var host = ConfigurationManager.AppSettings.Get("SmtpHost");
                    var port = ConfigurationManager.AppSettings.Get("SmtpPort");
                    var credentialUser = ConfigurationManager.AppSettings.Get("SmtpMerlinCredentialUser");
                    var credentialPass = ConfigurationManager.AppSettings.Get("SmtpMerlinCredentialPass");

                    client.Host = host;
                    client.Port = int.Parse(port);
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = true;
                    client.Credentials = new NetworkCredential(credentialUser, credentialPass);

                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(credentialUser, "Merlin");

                    foreach (var correo in toCorreo.Split(';'))
                    {
                        message.To.Add(new MailAddress(correo));
                    }

                    if (ccCorreo != null && cc != null)
                    {
                        foreach (var copia in ccCorreo.Split(';'))
                        {
                            message.CC.Add(new MailAddress(copia));
                        }
                    }

                    message.Subject = asunto;
                    message.IsBodyHtml = true;
                    message.Body = mensaje;
                    message.BodyEncoding = Encoding.UTF8;
                    message.Attachments.Add(new Attachment(FilePath));

                    client.Send(message);

                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message + "Error consultar por las referencias de la sucursal en la base de datos.");
                }


            }
        }
    }
}
