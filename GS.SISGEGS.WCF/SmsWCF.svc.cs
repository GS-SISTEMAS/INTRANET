using System;
using System.Collections.Generic;
using GS.SISGEGS.DM;
using GS.SISGEGS.BL;


namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "SmsWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione SmsWCF.svc o SmsWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class SmsWCF : ISmsWCF
    {
         
        public List<SP_PerfilesEmpresaResult> Lista_PerfilesEmpresa(int idEmpresa)
        {
            SMSBL objSMS;
            try
            {
                objSMS = new SMSBL();
                return objSMS.Lista_PerfilesEmpresa(idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Registro_SMS(string text, string id_perfil)
        {
            SMSBL objSMS;
            try
            {
                objSMS = new SMSBL();
                objSMS.Registro_SMS(text, id_perfil);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
