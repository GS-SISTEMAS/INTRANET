using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GS.SISGEGS.BE
{
    public class NotaCreditoKPIBE
    {
        private string periodo;
        private decimal total;
        private decimal totalMes;

        public string Periodo
        {
            get
            {
                return periodo;
            }

            set
            {
                periodo = value;
            }
        }

        public decimal Total
        {
            get
            {
                return total;
            }

            set
            {
                total = value;
            }
        }

        public decimal TotalMes
        {
            get
            {
                return totalMes;
            }

            set
            {
                totalMes = value;
            }
        }
    }
}
