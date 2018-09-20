using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GS.SISGEGS.BE
{
    public class Agenda_LimiteCreditoBE
    {
        private string _AgendaNombre;
        private string _MonedaNombre;
        private decimal? _LimCreditoMonedaVta;
        private decimal? _LimCreditoMonedaSol;
        private decimal? _SaldoCtaCteMonedaSol;
        private decimal? _CreditoDisponibleMonedaSol;
        private bool? _EvaluarLimCredito;

        public string AgendaNombre
        {
            get
            {
                return _AgendaNombre;
            }

            set
            {
                _AgendaNombre = value;
            }
        }

        public string MonedaNombre
        {
            get
            {
                return _MonedaNombre;
            }

            set
            {
                _MonedaNombre = value;
            }
        }

        public decimal? LimCreditoMonedaVta
        {
            get
            {
                return _LimCreditoMonedaVta;
            }

            set
            {
                _LimCreditoMonedaVta = value;
            }
        }

        public decimal? LimCreditoMonedaSol
        {
            get
            {
                return _LimCreditoMonedaSol;
            }

            set
            {
                _LimCreditoMonedaSol = value;
            }
        }

        public decimal? SaldoCtaCteMonedaSol
        {
            get
            {
                return _SaldoCtaCteMonedaSol;
            }

            set
            {
                _SaldoCtaCteMonedaSol = value;
            }
        }

        public decimal? CreditoDisponibleMonedaSol
        {
            get
            {
                return _CreditoDisponibleMonedaSol;
            }

            set
            {
                _CreditoDisponibleMonedaSol = value;
            }
        }

        public bool? EvaluarLimCredito
        {
            get
            {
                return _EvaluarLimCredito;
            }

            set
            {
                _EvaluarLimCredito = value;
            }
        }
    }
}
