using System.Globalization;
using CsvHelper.Configuration;
using PaymentControl.model.Dtos;

namespace PaymentControl.data.Mappings
{
    public class RelatorioCobrancaHelperCsvMapping : ClassMap<RelatorioCobrancaDTO>
    {
        public RelatorioCobrancaHelperCsvMapping()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Sacador).Name("Sacado");
            Map(m => m.NossoNumero).Name("Nosso Número");
            Map(m => m.SeuNumero).Name("Seu Número");
            Map(m => m.Entrada).Name("Entrada").TypeConverterOption.Format("dd/mm/yyyy");
            Map(m => m.Vencimento).Name("Vencimento").TypeConverterOption.Format("dd/mm/yyyy");
            Map(m => m.LimitePgto).Name("Dt. Limite Pgto");
            Map(m => m.Valor).Name("Valor (R$)").TypeConverterOption.NumberStyles(NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
        }
    }
}