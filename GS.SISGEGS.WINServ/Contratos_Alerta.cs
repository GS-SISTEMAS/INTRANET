using System.Collections.Generic;
using System.Linq;
using GS.SISGEGS.DM;
using GS.SISGEGS.WINServ.ContratosWCF;
using GS.SISGEGS.WINServ.ResponsableContratoWCF;
using GS.SISGEGS.WINServ.CorreoWCF;

namespace GS.SISGEGS.WINServ
{
    public class Contratos_Alerta
    {
        static void Main(string[] args)
        {

            //ResponsableContratoWCFClient objResponsablesWCF = new ResponsableContratoWCFClient();
            //List<ResponsablesContrato_ListarResult> objListResponsables = new List<ResponsablesContrato_ListarResult>();
            //objListResponsables = objResponsablesWCF.ResponsablesContrato_Listar().ToList();

            //var idArea = 0;
            //var responsable = "";
            //var correoResponsable = "";
            //var mensaje = "";

           
            //foreach (ResponsablesContrato_ListarResult objResponsable in objListResponsables) {

            //    idArea = objResponsable.ID_Area;
            //    responsable = objResponsable.Responsable;
            //    correoResponsable = objResponsable.correoResponsable;
            //    mensaje += "Estimado " + responsable + ",";
            //    mensaje += "<br/><br/>";
            //    mensaje += "Se le informa que los siguientes contratos están por vencer. ";
            //    mensaje += "<br/><br/>";
            //    mensaje += "<b>Contratos por Vencer</b>";
            //    mensaje += "<br/><br/>";
            //    mensaje += ListaContratosPorVencer(idArea);

            //    EnviarEmail(responsable,correoResponsable,mensaje);

            //}
            Marcas_Alerta objMar = new Marcas_Alerta();
            objMar.NotificacionVencimientoMarca();
               
        }
        static void EnviarEmail(string responsable, string to, string mensaje) {
            CorreoWCFClient objCorreoWCF = new CorreoWCFClient();
            objCorreoWCF.EnviarCorreo(responsable, to,null,null, "Reporte de contratos por vencer", mensaje); 
        }
        static string ListaContratosPorVencer(int id_Area) {
            ContratosWCFClient objContratosWCF = new ContratosWCFClient();
            List<ContratosVencer_ListarResult> lstContratos = new List<ContratosVencer_ListarResult>();

            lstContratos = objContratosWCF.ContratosVencer_Listar(id_Area).ToList();
            var mensaje = "<table border='1' cellspacing='0'>";
            mensaje += CabeceraListaContratos();
            foreach (ContratosVencer_ListarResult contrato in lstContratos) {

                mensaje += "<tr>";

                mensaje += "<td>";
                mensaje += contrato.CodigoContrato;
                mensaje += "</td>";

                mensaje += "<td>";
                mensaje += contrato.nombreMateria;
                mensaje += "</td>";

                mensaje += "<td>";
                mensaje += contrato.nombreTipo;
                mensaje += "</td>";

                mensaje += "<td>";
                mensaje += contrato.nombreProveedor;
                mensaje += "</td>";

                mensaje += "<td>";
                mensaje += contrato.Contratante;
                mensaje += "</td>";

                mensaje += "<td>";
                mensaje += contrato.AreaResponsable;
                mensaje += "</td>";

                mensaje += "<td>";
                mensaje += contrato.FechaSuscripcion;
                mensaje += "</td>";

                mensaje += "<td>";
                mensaje += contrato.FechaVencimiento;
                mensaje += "</td>";

                mensaje += "<td>";
                mensaje += contrato.ObjetoContrato;
                mensaje += "</td>";

                mensaje += "<td>";
                mensaje += contrato.Renovacion;
                mensaje += "</td>";

                mensaje += "<td>";
                mensaje += contrato.Monto;
                mensaje += "</td>";

                mensaje += "</tr>";
            }
            mensaje += "</table>";
            return mensaje;
        }
        static string CabeceraListaContratos() {
            var mensaje = "<tr style='background-color:#ccc'>";
            mensaje += "<td><b> Código </b></td>";
            mensaje += "<td><b> Materia </b></td>";
            mensaje += "<td><b> Tipo </b></td>";
            mensaje += "<td><b> Cliente / Proveedor </b></td>";
            mensaje += "<td><b> Contratante </b></td>";
            mensaje += "<td><b> Area Responsable </b></td>";
            mensaje += "<td><b> Fecha de Suscripción </b></td>";
            mensaje += "<td><b> Fecha de Vencimiento </b></td>";
            mensaje += "<td><b> Objeto del Contrato </b></td>";
            mensaje += "<td><b> Renovación </b></td>";
            mensaje += "<td><b> Monto </b></td>";
            mensaje += "</tr>";
            return mensaje;
        }
    }
}
