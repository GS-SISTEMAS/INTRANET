using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS.SISGEGS.TCI.TipoCambio.FacturacionElectronicaWCF;
using GS.SISGEGS.TCI.TipoCambio.AgendaWCF; 


namespace GS.SISGEGS.TCI.TipoCambio
{
    class Program
    {
        static void Main(string[] args)
        {

            Obtener_TipoCambio(); 
        }


        static void Obtener_TipoCambio()
        {
            int idEmpresa;
         
            DateTime fechaInicial;
            DateTime fechaFinal;
            int contar = 1;
            int codUsuario = 1;
            string Empresa_RUC = ""; 

            AgendaWCFClient objAgendaWCF = new AgendaWCFClient();

            for (int x = 1; x <= 3; x++)
            {
                idEmpresa = x;
                if (x == 3)
                {
                    idEmpresa = 6;
                    Empresa_RUC = "20505467214";
                }

                if(x==1)
                {
                    Empresa_RUC = "20191503482"; 
                }
                if (x == 2)
                {
                    Empresa_RUC = "20509089923";
                }



                Console.WriteLine(  "Empresa:" + Empresa_RUC);

                int Dia = 1;
                fechaInicial = Convert.ToDateTime("2018/02/" + Dia.ToString());
                fechaFinal = fechaInicial;

                //fechaInicial = Convert.ToDateTime( DateTime.Now.ToShortDateString()); 
                //fechaFinal = fechaInicial;

                for (int i = 1; i <= 12; i++)
                {

                    Console.WriteLine("----------------------------------------"); 
                    try
                    {
                        RPT00015Result objTipoCambio = objAgendaWCF.Agenda_TipoCambio(idEmpresa, codUsuario, fechaInicial, fechaFinal, 1);
                        RegistrarTipoRegistrarTipoCambio(Empresa_RUC, objTipoCambio.Fecha, objTipoCambio.Operativo); 

                        Console.WriteLine(
                         //i.ToString()+ "  " +   
                      "Fecha Inicial:" + fechaInicial.ToShortDateString()
                      + " Fecha Final:" + fechaFinal.ToShortDateString()
                      + " Tipo Cambio:" + objTipoCambio.Operativo.ToString()
                      + " Fecha Genesys:" + objTipoCambio.Fecha.ToShortDateString()
                      );
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(
                            "Error:" +
                      "Fecha Inicial:" + fechaInicial.ToShortDateString()
                      + " Fecha Final:" + fechaFinal.ToShortDateString()
                      
                      );
                    }

                    //------------------------------------------------
                    fechaInicial = fechaInicial.AddDays(1);
                    fechaFinal = fechaInicial;
                    contar = contar + 1;
                }
               
            }
        }


        static void RegistrarTipoRegistrarTipoCambio( string Empresa_RUC, DateTime FechaReigstro, decimal TipoCambio)
        {
            FacturacionElectronicaWCF.WSComprobanteSoapClient oServicioOK2 = new FacturacionElectronicaWCF.WSComprobanteSoapClient();
            string cadena = "";
            FacturacionElectronicaWCF.ENEnvioTipoCambio oTipoCambio = new FacturacionElectronicaWCF.ENEnvioTipoCambio();
            try
            {

                //Opcional RegistrarTipoCambio   ----------------
   
                oTipoCambio.CodigoMoneda = "USD";
                oTipoCambio.FechaTipoCambio = FechaReigstro; 
                oTipoCambio.Ruc = Empresa_RUC; 
                oTipoCambio.TipoCambio = TipoCambio;

                bool Exito = oServicioOK2.RegistrarTipoCambio(oTipoCambio, ref cadena);

                if(Exito==true )
                {
                    Console.WriteLine("Registro Exitoso: " + oTipoCambio.FechaTipoCambio.ToShortDateString() + " Msj:" + Exito.ToString());
                }
                else
                {
                    Console.WriteLine("Error registrar: " + oTipoCambio.FechaTipoCambio.ToShortDateString() + " Exp:" + cadena);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error registrar: "+  oTipoCambio.FechaTipoCambio.ToShortDateString() + " Exp:" + cadena); 
            }
        }

    }
}
