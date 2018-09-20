using GS.SISGEGS.WIN.MERLIN.CorreoWCF;
using GS.SISGEGS.WIN.MERLIN.MerlinWCF;

namespace GS.SISGEGS.WIN.MERLIN
{
    class Program
    {
        static void Main(string[] args)
        {
            MerlinWCFClient objMerlinClient = new MerlinWCFClient();
            var lista = objMerlinClient.NotificacionMerlin();

            foreach (var item in lista) {
                EnviarEmail("",item.EmailTO,item.EmailCC,item.Asunto,item.Mensaje);
                objMerlinClient.ActualizarNotificacionMerlin(item.IdNotificacion);
            }
        }

        static void EnviarEmail(string responsable, string to,string cc, string asunto, string mensaje)
        {
            CorreoWCFClient objCorreoWCF = new CorreoWCFClient();
            objCorreoWCF.MerlinEnviarCorreo(responsable, to, "",cc, asunto, mensaje);

        }
    }
}
