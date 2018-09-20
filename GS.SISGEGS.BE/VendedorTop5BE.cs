using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GS.SISGEGS.BE
{
    public class VendedorTop5BE
    {
        private int ranking;
        private string vendedor;
        private decimal valorventa;
        private decimal rentabilidad;
        private int updown;

        public int Ranking
        {
            get
            {
                return ranking;
            }

            set
            {
                ranking = value;
            }
        }

        public string Vendedor
        {
            get
            {
                return vendedor;
            }

            set
            {
                vendedor = value;
            }
        }

        public int Updown
        {
            get
            {
                return updown;
            }

            set
            {
                updown = value;
            }
        }

        public decimal Valorventa
        {
            get
            {
                return valorventa;
            }

            set
            {
                valorventa = value;
            }
        }

        public decimal Rentabilidad
        {
            get
            {
                return rentabilidad;
            }

            set
            {
                rentabilidad = value;
            }
        }
    }
}
