using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using PaymentControl.data.Mappings;
using PaymentControl.model.Dtos;

namespace PaymentControl.Services
{
    public class CsvService
    {
        public void GravarArquivoCSV(Stream stream, List<RelatorioCobrancaDTO> relatorioCobrancas)
        {
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(relatorioCobrancas);
            }
        }

        public List<RelatorioCobrancaDTO> LerArquivoCSV(Stream stream, Stream validOutStream = null, Stream invalidOutStream = null)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), "O fluxo de entrada não pode ser nulo.");
            }

            if (!stream.CanRead)
            {
                throw new ArgumentException("O fluxo de entrada não é legível.", nameof(stream));
            }

            var validRecords = new List<RelatorioCobrancaDTO>();
            var invalidRecords = new List<RelatorioCobrancaDTO>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null,
                MissingFieldFound = null,
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };
            stream.Position = 0;
            using var streamReader = new StreamReader(stream);
            using var csvReader = new CsvReader(streamReader, config);

            bool headerFound = false;

            while (csvReader.Read())
            {
                var row = csvReader.Context.Parser.Record;
                if (row[0] == "Sacado" && row.Contains("Nosso Número"))
                {
                    headerFound = true;
                    csvReader.ReadHeader();
                    csvReader.Context.RegisterClassMap<RelatorioCobrancaHelperCsvMapping>();
                    break;
                }
            }

            if (!headerFound)
            {
                throw new Exception("Cabeçalho esperado não encontrado.");
            }

            var records = csvReader.GetRecords<RelatorioCobrancaDTO>();
            foreach (var record in records)
            {
                if (!string.IsNullOrWhiteSpace(record.Sacador) &&
                    !record.Sacador.Contains("Ordenado por:") &&
                    !record.Sacador.Contains("Gerado em:") &&
                    !record.Sacador.Contains("Cedente:") &&
                    !record.Sacador.Contains("Tipo Consulta:") &&
                    !record.Sacador.Contains("Conta Corrente:")
                )
                {
                    validRecords.Add(record);
                }
                else
                {
                    invalidRecords.Add(record);
                }
            }

            if (validOutStream != null)
            {
                using var validWriter = new StreamWriter(validOutStream);
                using var validCsv = new CsvWriter(validWriter, CultureInfo.InvariantCulture);
                validCsv.WriteRecords(validRecords);
            }

            if (invalidOutStream != null)
            {
                using var invalidWriter = new StreamWriter(invalidOutStream);
                using var invalidCsv = new CsvWriter(invalidWriter, CultureInfo.InvariantCulture);
                invalidCsv.WriteRecords(invalidRecords);
            }

            return validRecords;
        }
    }
}
