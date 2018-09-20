using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Net.Mail;
using GS.SISGEGS.BL;
namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "CorreoWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione CorreoWCF.svc o CorreoWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class CorreoWCF : ICorreoWCF
    {
        public void EnviarCorreo(string to, string toCorreo, string cc, string ccCorreo, string asunto, string mensaje)
        {
            CorreoBL objCorreoBL = new CorreoBL();
            try
            {
                objCorreoBL.EnvioCorreo(to, toCorreo, cc,ccCorreo, asunto, mensaje);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public void MerlinEnviarCorreo(string to, string toCorreo, string cc, string ccCorreo, string asunto, string mensaje)
        {
            CorreoBL objCorreoBL = new CorreoBL();
            try
            {
                objCorreoBL.MerlinEnvioCorreo(to, toCorreo, cc, ccCorreo, asunto, mensaje);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MerlinEnvioCorreoAdjunto(string to, string toCorreo, string cc, string ccCorreo, string asunto, string mensaje, string FilePath)
        {
            CorreoBL objCorreoBL = new CorreoBL();
            try
            {
                objCorreoBL.MerlinEnvioCorreoAdjunto(to, toCorreo, cc, ccCorreo, asunto, mensaje, FilePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
