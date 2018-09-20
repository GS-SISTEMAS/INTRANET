using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GS.SISGEGS.BE
{
    public class GlosaBE
    {
        private decimal _IdGlosa;
        private string _Descripcion;
        private decimal _BaseImponible;
        private decimal _Importe;

        public decimal IdGlosa
        {
            get
            {
                return _IdGlosa;
            }

            set
            {
                _IdGlosa = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return _Descripcion;
            }

            set
            {
                _Descripcion = value;
            }
        }

        public decimal BaseImponible
        {
            get
            {
                return _BaseImponible;
            }

            set
            {
                _BaseImponible = value;
            }
        }

        public decimal Importe
        {
            get
            {
                return _Importe;
            }

            set
            {
                _Importe = value;
            }
        }
    }
}
