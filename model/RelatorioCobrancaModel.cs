using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentControl.model
{
    public class RelatorioCobrancaModel
    {
        public string Sacador { get; set; } = String.Empty;
        public string NossoNumero { get; set; } = String.Empty;
        public string SeuNumero { get; set; } = String.Empty;
        public DateTime Entrada { get; set; }
        public DateTime Vencimento { get; set; }
        public float Valor { get; set; }
    }
}