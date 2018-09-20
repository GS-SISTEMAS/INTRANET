using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GS.SISGEGS.BE
{
    public class PedidoCabBE
    {
        private string _IdAgenda;
        private string _NroRegistro;
        private DateTime _FechaOrden;
        private DateTime _FechaDespacho;
        private DateTime _FechaEntrega;
        private DateTime _FechaVigencia;
        private DateTime _Fecha;
        private DateTime _FechaVencimiento;
        private decimal _IdEnvio;
        private decimal? _IdAgenciaAnexoReferencia;
        private string _IdVendedor;
        private decimal _IdMoneda;
        private decimal _Neto;
        private decimal _Descuento;
        private decimal _Subtotal;
        private decimal _Impuestos;
        private decimal _Total;
        private string _Observaciones;
        private int _Prioridad;
        private bool _EntregaParcial;
        private int _Estado;
        private int _IdPago;
        private decimal? _IdAgenciaAnexo;
        private decimal? _TEA;
        private decimal? _IdAgenciaDireccion1;
        private decimal? _IdAgenciaDireccion2;
        private string _ModoPago;
        private string _NotasDespacho;
        private int? _IdCondicionCredito;
        private string _NroOrdenCliente;
        private string _IdNaturalezaGasto;
        private string _IdAgendaOrigen;
        private decimal? _IdSucursalOrigen;
        private decimal? _IdReferenciaOrigen;
        private decimal? _IdDireccionOrigen;
        private string _IdAgendaDestino;
        private decimal? _IdSucursalDestino;
        private decimal? _IdReferenciaDestino;
        private decimal? _IdDireccionDestino;
        private int? _IdTipoDespacho;
        private int? _IdTipoPedido;
        private decimal? _IdDocumentoVenta;
        private decimal? _IdAlmacen;
        private string _IdTransportista;
        private string _IdChofer;
        private string _IdVehiculo1;
        private string _IdVehiculo2;
        private string _IdVehiculo3;
        private decimal? _HoraAtencionOpcional1_Desde;
        private decimal? _HoraAtencionOpcional1_Hasta;
        private decimal? _HoraAtencionOpcional2_Desde;
        private decimal? _HoraAtencionOpcional2_Hasta;
        private decimal? _HoraAtencionOpcional3_Desde;
        private decimal? _HoraAtencionOpcional3_Hasta;
        private int? _IdSede;
        private string _IdContacto;

        public string IdAgenda
        {
            get
            {
                return _IdAgenda;
            }

            set
            {
                _IdAgenda = value;
            }
        }

        public string NroRegistro
        {
            get
            {
                return _NroRegistro;
            }

            set
            {
                _NroRegistro = value;
            }
        }

        public DateTime FechaOrden
        {
            get
            {
                return _FechaOrden;
            }

            set
            {
                _FechaOrden = value;
            }
        }

        public DateTime FechaDespacho
        {
            get
            {
                return _FechaDespacho;
            }

            set
            {
                _FechaDespacho = value;
            }
        }

        public DateTime FechaEntrega
        {
            get
            {
                return _FechaEntrega;
            }

            set
            {
                _FechaEntrega = value;
            }
        }

        public DateTime FechaVigencia
        {
            get
            {
                return _FechaVigencia;
            }

            set
            {
                _FechaVigencia = value;
            }
        }

        public DateTime Fecha
        {
            get
            {
                return _Fecha;
            }

            set
            {
                _Fecha = value;
            }
        }

        public DateTime FechaVencimiento
        {
            get
            {
                return _FechaVencimiento;
            }

            set
            {
                _FechaVencimiento = value;
            }
        }

        public decimal IdEnvio
        {
            get
            {
                return _IdEnvio;
            }

            set
            {
                _IdEnvio = value;
            }
        }

        public decimal? IdAgenciaAnexoReferencia
        {
            get
            {
                return _IdAgenciaAnexoReferencia;
            }

            set
            {
                _IdAgenciaAnexoReferencia = value;
            }
        }

        public string IdVendedor
        {
            get
            {
                return _IdVendedor;
            }

            set
            {
                _IdVendedor = value;
            }
        }

        public decimal IdMoneda
        {
            get
            {
                return _IdMoneda;
            }

            set
            {
                _IdMoneda = value;
            }
        }

        public decimal Neto
        {
            get
            {
                return _Neto;
            }

            set
            {
                _Neto = value;
            }
        }

        public decimal Descuento
        {
            get
            {
                return _Descuento;
            }

            set
            {
                _Descuento = value;
            }
        }

        public decimal Subtotal
        {
            get
            {
                return _Subtotal;
            }

            set
            {
                _Subtotal = value;
            }
        }

        public decimal Impuestos
        {
            get
            {
                return _Impuestos;
            }

            set
            {
                _Impuestos = value;
            }
        }

        public decimal Total
        {
            get
            {
                return _Total;
            }

            set
            {
                _Total = value;
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

        public int Prioridad
        {
            get
            {
                return _Prioridad;
            }

            set
            {
                _Prioridad = value;
            }
        }

        public bool EntregaParcial
        {
            get
            {
                return _EntregaParcial;
            }

            set
            {
                _EntregaParcial = value;
            }
        }

        public int Estado
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

        public int IdPago
        {
            get
            {
                return _IdPago;
            }

            set
            {
                _IdPago = value;
            }
        }

        public decimal? IdAgenciaAnexo
        {
            get
            {
                return _IdAgenciaAnexo;
            }

            set
            {
                _IdAgenciaAnexo = value;
            }
        }

        public decimal? TEA
        {
            get
            {
                return _TEA;
            }

            set
            {
                _TEA = value;
            }
        }

        public decimal? IdAgenciaDireccion1
        {
            get
            {
                return _IdAgenciaDireccion1;
            }

            set
            {
                _IdAgenciaDireccion1 = value;
            }
        }

        public decimal? IdAgenciaDireccion2
        {
            get
            {
                return _IdAgenciaDireccion2;
            }

            set
            {
                _IdAgenciaDireccion2 = value;
            }
        }

        public string ModoPago
        {
            get
            {
                return _ModoPago;
            }

            set
            {
                _ModoPago = value;
            }
        }

        public string NotasDespacho
        {
            get
            {
                return _NotasDespacho;
            }

            set
            {
                _NotasDespacho = value;
            }
        }

        public int? IdCondicionCredito
        {
            get
            {
                return _IdCondicionCredito;
            }

            set
            {
                _IdCondicionCredito = value;
            }
        }

        public string NroOrdenCliente
        {
            get
            {
                return _NroOrdenCliente;
            }

            set
            {
                _NroOrdenCliente = value;
            }
        }

        public string IdNaturalezaGasto
        {
            get
            {
                return _IdNaturalezaGasto;
            }

            set
            {
                _IdNaturalezaGasto = value;
            }
        }

        public string IdAgendaOrigen
        {
            get
            {
                return _IdAgendaOrigen;
            }

            set
            {
                _IdAgendaOrigen = value;
            }
        }

        public decimal? IdSucursalOrigen
        {
            get
            {
                return _IdSucursalOrigen;
            }

            set
            {
                _IdSucursalOrigen = value;
            }
        }

        public decimal? IdReferenciaOrigen
        {
            get
            {
                return _IdReferenciaOrigen;
            }

            set
            {
                _IdReferenciaOrigen = value;
            }
        }

        public decimal? IdDireccionOrigen
        {
            get
            {
                return _IdDireccionOrigen;
            }

            set
            {
                _IdDireccionOrigen = value;
            }
        }

        public string IdAgendaDestino
        {
            get
            {
                return _IdAgendaDestino;
            }

            set
            {
                _IdAgendaDestino = value;
            }
        }

        public decimal? IdSucursalDestino
        {
            get
            {
                return _IdSucursalDestino;
            }

            set
            {
                _IdSucursalDestino = value;
            }
        }

        public decimal? IdReferenciaDestino
        {
            get
            {
                return _IdReferenciaDestino;
            }

            set
            {
                _IdReferenciaDestino = value;
            }
        }

        public decimal? IdDireccionDestino
        {
            get
            {
                return _IdDireccionDestino;
            }

            set
            {
                _IdDireccionDestino = value;
            }
        }

        public int? IdTipoDespacho
        {
            get
            {
                return _IdTipoDespacho;
            }

            set
            {
                _IdTipoDespacho = value;
            }
        }

        public int? IdTipoPedido
        {
            get
            {
                return _IdTipoPedido;
            }

            set
            {
                _IdTipoPedido = value;
            }
        }

        public decimal? IdDocumentoVenta
        {
            get
            {
                return _IdDocumentoVenta;
            }

            set
            {
                _IdDocumentoVenta = value;
            }
        }

        public decimal? IdAlmacen
        {
            get
            {
                return _IdAlmacen;
            }

            set
            {
                _IdAlmacen = value;
            }
        }

        public string IdTransportista
        {
            get
            {
                return _IdTransportista;
            }

            set
            {
                _IdTransportista = value;
            }
        }

        public string IdChofer
        {
            get
            {
                return _IdChofer;
            }

            set
            {
                _IdChofer = value;
            }
        }

        public string IdVehiculo1
        {
            get
            {
                return _IdVehiculo1;
            }

            set
            {
                _IdVehiculo1 = value;
            }
        }

        public string IdVehiculo2
        {
            get
            {
                return _IdVehiculo2;
            }

            set
            {
                _IdVehiculo2 = value;
            }
        }

        public string IdVehiculo3
        {
            get
            {
                return _IdVehiculo3;
            }

            set
            {
                _IdVehiculo3 = value;
            }
        }

        public decimal? HoraAtencionOpcional1_Desde
        {
            get
            {
                return _HoraAtencionOpcional1_Desde;
            }

            set
            {
                _HoraAtencionOpcional1_Desde = value;
            }
        }

        public decimal? HoraAtencionOpcional1_Hasta
        {
            get
            {
                return _HoraAtencionOpcional1_Hasta;
            }

            set
            {
                _HoraAtencionOpcional1_Hasta = value;
            }
        }

        public decimal? HoraAtencionOpcional2_Desde
        {
            get
            {
                return _HoraAtencionOpcional2_Desde;
            }

            set
            {
                _HoraAtencionOpcional2_Desde = value;
            }
        }

        public decimal? HoraAtencionOpcional2_Hasta
        {
            get
            {
                return _HoraAtencionOpcional2_Hasta;
            }

            set
            {
                _HoraAtencionOpcional2_Hasta = value;
            }
        }

        public decimal? HoraAtencionOpcional3_Desde
        {
            get
            {
                return _HoraAtencionOpcional3_Desde;
            }

            set
            {
                _HoraAtencionOpcional3_Desde = value;
            }
        }

        public decimal? HoraAtencionOpcional3_Hasta
        {
            get
            {
                return _HoraAtencionOpcional3_Hasta;
            }

            set
            {
                _HoraAtencionOpcional3_Hasta = value;
            }
        }

        public int? IdSede
        {
            get
            {
                return _IdSede;
            }

            set
            {
                _IdSede = value;
            }
        }

        public string IdContacto
        {
            get
            {
                return _IdContacto;
            }

            set
            {
                _IdContacto = value;
            }
        }
    }
}
