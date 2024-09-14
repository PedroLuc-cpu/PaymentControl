using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cadastro.Model;

namespace PaymentControl.model
{
    public class RelatorioCobrancaModel : Base
    {
        public string Sacador { get; set; } = String.Empty;
        public string NossoNumero { get; set; } = String.Empty;
        public string SeuNumero { get; set; } = String.Empty;
        public string Entrada { get; set; } = String.Empty;
        public string Vencimento { get; set; } = String.Empty;
        public string LimitePgto { get; set; } = String.Empty;
        public string Valor { get; set; } = String.Empty;
        public int idCliente { get; set; }
        public ClienteModel Cliente { get; set; } = new ClienteModel();
    }
}