using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GS.SISGEGS.BE
{
    public class DocVenta_RentaProdBE
    {
        private string _Categoria;
        private int _Kardex;
        private string _Descripcion; 
        private decimal _ValorVenta;
        private decimal _Rentabilidad;
        private decimal _PorcVenta;

        public string Categoria
        {
            get
            {
                return _Categoria;
            }

            set
            {
                _Categoria = value;
            }
        }

        public int Kardex
        {
            get
            {
                return _Kardex;
            }

            set
            {
                _Kardex = value;
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

        public decimal ValorVenta
        {
            get
            {
                return _ValorVenta;
            }

            set
            {
                _ValorVenta = value;
            }
        }

        public decimal Rentabilidad
        {
            get
            {
                return _Rentabilidad;
            }

            set
            {
                _Rentabilidad = value;
            }
        }

        public decimal PorcVenta
        {
            get
            {
                return _PorcVenta;
            }

            set
            {
                _PorcVenta = value;
            }
        }
    }
}
