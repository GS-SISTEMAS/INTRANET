using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GS.SISGEGS.BE
{
    public class PedidoDetBE
    {
        private decimal? _IdAmarre;
        private decimal _IdOperacion;
        private string _TablaOrigen;
        private decimal _Linea;
        private string _ID_Item;
	    private string _ID_ItemPedido;
        private decimal _Item_ID;
        private decimal _Cantidad;
        private decimal _Precio;
        private decimal _Dcto;
        private decimal _DctoValor;
        private decimal _Importe;
        private decimal? _ID_ItemAnexo;
        private string _ID_CCosto;
        private string _ID_UnidadGestion;
        private string _ID_UnidadProyecto;
        private string _ID_UnidadInv;
        private decimal _FactorUnidadInv;
        private decimal _CantidadUnidadInv;
        private string _ID_UnidadDoc;
        private decimal _CantidadUnidadDoc;
        private string _Observaciones;
        private bool _Estado;

        public decimal IdOperacion
        {
            get
            {
                return _IdOperacion;
            }

            set
            {
                _IdOperacion = value;
            }
        }

        public string TablaOrigen
        {
            get
            {
                return _TablaOrigen;
            }

            set
            {
                _TablaOrigen = value;
            }
        }

        public decimal Linea
        {
            get
            {
                return _Linea;
            }

            set
            {
                _Linea = value;
            }
        }

        public string ID_Item
        {
            get
            {
                return _ID_Item;
            }

            set
            {
                _ID_Item = value;
            }
        }

        public string ID_ItemPedido
        {
            get
            {
                return _ID_ItemPedido;
            }

            set
            {
                _ID_ItemPedido = value;
            }
        }

        public decimal Item_ID
        {
            get
            {
                return _Item_ID;
            }

            set
            {
                _Item_ID = value;
            }
        }

        public decimal Cantidad
        {
            get
            {
                return _Cantidad;
            }

            set
            {
                _Cantidad = value;
            }
        }

        public decimal Precio
        {
            get
            {
                return _Precio;
            }

            set
            {
                _Precio = value;
            }
        }

        public decimal Dcto
        {
            get
            {
                return _Dcto;
            }

            set
            {
                _Dcto = value;
            }
        }

        public decimal DctoValor
        {
            get
            {
                return _DctoValor;
            }

            set
            {
                _DctoValor = value;
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

        public decimal? ID_ItemAnexo
        {
            get
            {
                return _ID_ItemAnexo;
            }

            set
            {
                _ID_ItemAnexo = value;
            }
        }

        public string ID_CCosto
        {
            get
            {
                return _ID_CCosto;
            }

            set
            {
                _ID_CCosto = value;
            }
        }

        public string ID_UnidadGestion
        {
            get
            {
                return _ID_UnidadGestion;
            }

            set
            {
                _ID_UnidadGestion = value;
            }
        }

        public string ID_UnidadProyecto
        {
            get
            {
                return _ID_UnidadProyecto;
            }

            set
            {
                _ID_UnidadProyecto = value;
            }
        }

        public string ID_UnidadInv
        {
            get
            {
                return _ID_UnidadInv;
            }

            set
            {
                _ID_UnidadInv = value;
            }
        }

        public decimal FactorUnidadInv
        {
            get
            {
                return _FactorUnidadInv;
            }

            set
            {
                _FactorUnidadInv = value;
            }
        }

        public decimal CantidadUnidadInv
        {
            get
            {
                return _CantidadUnidadInv;
            }

            set
            {
                _CantidadUnidadInv = value;
            }
        }

        public string ID_UnidadDoc
        {
            get
            {
                return _ID_UnidadDoc;
            }

            set
            {
                _ID_UnidadDoc = value;
            }
        }

        public decimal CantidadUnidadDoc
        {
            get
            {
                return _CantidadUnidadDoc;
            }

            set
            {
                _CantidadUnidadDoc = value;
            }
        }

        public string Observaciones
        {
            get
            {
                return _Observaciones;
            }

            set
            {
                _Observaciones = value;
            }
        }

        public decimal? IdAmarre
        {
            get
            {
                return _IdAmarre;
            }

            set
            {
                _IdAmarre = value;
            }
        }

        public bool Estado
        {
            get
            {
                return _Estado;
            }

            set
            {
                _Estado = value;
            }
        }
    }
}
