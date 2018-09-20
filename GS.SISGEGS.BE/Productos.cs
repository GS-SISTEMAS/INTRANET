using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GS.SISGEGS.BE
{
    public class Productos
    {

    }
    public class MantenimientoProductos
    {
        public string Id_Agenda { get; set; }
        public string AgendaNombre { get; set; }
        public string ItemCodigo { get; set; }
        public int Kardex { get; set; }
        public string NombreKardex { get; set; }
        public string Id_UnidadInv { get; set; }

        public double Precio             {get; set;}
        public int IdMoneda              {get; set;}
        public string Moneda             {get; set;}
        public double PrecioEspecial     {get; set;}
        public Boolean SinTermino        {get; set;}
        public DateTime FechaVigInicio   {get; set;}
        public DateTime? FechaVigFin { get; set; }
    }
}
