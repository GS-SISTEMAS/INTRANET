using System.Collections.Generic;
using System.Linq;
using GS.SISGEGS.DM;
using GS.SISGEGS.WINServ.MarcasWCF;
using GS.SISGEGS.WINServ.CorreoWCF;
using System;

namespace GS.SISGEGS.WINServ
{
    public class Marcas_Alerta
    {
        public void NotificacionVencimientoMarca()
        {

            //if (DateTime.Today.DayOfWeek.ToString() == "Monday") {
                        
            MarcasWCFClient objMarcasWCF = new MarcasWCFClient();
            List<ResponsablesRegistros_ListarResult> objListResponsables = new List<ResponsablesRegistros_ListarResult>();
            objListResponsables = objMarcasWCF.ResponsablesRegistros_Listar().ToList();

            var idArea = 0;
            var responsable = "";
            var correoResponsable = "";
            var mensaje = "";


            foreach (ResponsablesRegistros_ListarResult objResponsable in objListResponsables)
            {

                idArea = objResponsable.ID_Area;
                responsable = objResponsable.Responsable;
                correoResponsable = objResponsable.correoResponsable;
                mensaje += "Estimados,";
                mensaje += "<br/><br/>";
                mensaje += "Se les informa que los siguientes registros de marcas están por vencer. ";
                mensaje += "<br/><br/>";
                mensaje += "<b>Marcas por Vencer</b>";
                mensaje += "<br/><br/>";
                mensaje += ListaRegistrosMarcasPorVencer();

                EnviarEmail(responsable, correoResponsable, mensaje);
                mensaje = "";
            }
            //}
        }
        static void EnviarEmail(string responsable, string to, string mensaje)
        {
            CorreoWCFClient objCorreoWCF = new CorreoWCFClient();
            objCorreoWCF.EnviarCorreo(responsable, to,null,null, "Reporte de vencimiento de marcas", mensaje);
        }
        static string ListaRegistrosMarcasPorVencer()
        {
            MarcasWCFClient objMarcasWCF = new MarcasWCFClient();
            List<RegistroMarca_NotificacionResult> lstMarcas = new List<RegistroMarca_NotificacionResult>();

            lstMarcas = objMarcasWCF.RegistroMarca_Notificacion().ToList();
            var mensaje = "<table border='1' cellspacing='0'>";
            mensaje += CabeceraListaMarcas();
            foreach (RegistroMarca_NotificacionResult marca in lstMarcas)
            {

                mensaje += "<tr style='background-color:"+marca.Semaforo+"'>";

                mensaje += "<td>";
                mensaje += marca.idRegistroMarca;
                mensaje += "</td>";

                mensaje += "<td>";
                mensaje += marca.nombreComercial;
                mensaje += "</td>";

                mensaje += "<td>";
                mensaje += marca.Marca ;
                mensaje += "</td>";

                mensaje += "<td>";
                mensaje += marca.AbrevTipo;
                mensaje += "</td>";

                mensaje += "<td>";
                mensaje += marca.clase;
                mensaje += "</td>";

                mensaje += "<td>";
                mensaje += marca.nombrePais;
                mensaje += "</td>";

                mensaje += "<td>";
                mensaje += marca.certificado;
                mensaje += "</td>";

                mensaje += "<td>";
                mensaje += marca.fechaVencimiento;
                mensaje += "</td>";

                mensaje += "<td>";
                mensaje += marca.nombreTitular;
                mensaje += "</td>";

                mensaje += "</tr>";
            }
            mensaje += "</table>";
            return mensaje;
        }
        static string CabeceraListaMarcas()
        {
            var mensaje = "<tr style='background-color:#ccc'>";
            mensaje += "<td><b> Código </b></td>";
            mensaje += "<td><b> Empresa </b></td>";
            mensaje += "<td><b> Marca </b></td>";
            mensaje += "<td><b> Tipo </b></td>";
            mensaje += "<td><b> Clase </b></td>";
            mensaje += "<td><b> País </b></td>";
            mensaje += "<td><b> Certificado </b></td>";
            mensaje += "<td><b> Fecha de Vencimiento </b></td>";
            mensaje += "<td><b> Títular </b></td>";
            mensaje += "</tr>";
            return mensaje;
        }
    }
}
