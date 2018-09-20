using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GS.SISGEGS.BE
{
    public class DocVentaDev_KPIMotivoBE
    {
        private string motivo;
        private decimal porcentaje;
        private decimal monto;

        public string Motivo
        {
            get
            {
                return motivo;
            }

            set
            {
                motivo = value;
            }
        }

        public decimal Porcentaje
        {
            get
            {
                return porcentaje;
            }

            set
            {
                porcentaje = value;
            }
        }

        public decimal Monto
        {
            get
            {
                return monto;
            }

            set
            {
                monto = value;
            }
        }
    }
}
