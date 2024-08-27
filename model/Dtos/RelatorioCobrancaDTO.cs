using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace PaymentControl.model.Dtos
{
    public class RelatorioCobrancaDTO
    {
        [Name("Sacado")]
        public string Sacador { get; set; } = String.Empty;
        [Name("Nosso Número")]
        public string NossoNumero { get; set; } = String.Empty;
        [Name("Seu Número")]
        public string SeuNumero { get; set; } = String.Empty;
        [Name("Entrada")]
        public string? Entrada { get; set; }
        [Name("Vencimento")]
        public string? Vencimento { get; set; }
        [Name("Dt. Limite Pgto")]
        public string LimitePgto { get; set; } = String.Empty;
        [Name("Valor (R$)")]
        public string? Valor { get; set; }
    }
}