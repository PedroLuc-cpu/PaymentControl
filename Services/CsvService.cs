using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
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

        public List<T> LerArquivoCSV<T>(
            Stream stream,
            Func<string[], bool> isHeaderValid,
            Func<T, bool> isValid,
            Stream validOutStream = null,
            Stream invalidOutStream = null,
            ClassMap<T> classMap = null,
            Dictionary<string, List<string>> exclusionFilters = null
        )
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), "O fluxo de entrada não pode ser nulo.");
            }

            if (!stream.CanRead)
            {
                throw new ArgumentException("O fluxo de entrada não é legível.", nameof(stream));
            }

            var validRecords = new List<T>();
            var invalidRecords = new List<T>();

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
                if (isHeaderValid(row))
                {
                    headerFound = true;
                    csvReader.ReadHeader();
                    if (classMap != null)
                    {
                        csvReader.Context.RegisterClassMap(classMap);
                    }
                    else
                    {
                        throw new InvalidOperationException("ClassMap é obrigatório, mas não foi fornecido.");
                    }
                    break;
                }
            }
            if (!headerFound)
            {
                throw new Exception("Cabeçalho esperado não encontrado.");
            }

            var records = csvReader.GetRecords<T>().ToList();

            if (exclusionFilters != null)
            {
                records = records.Where(record =>
                {
                    foreach (var filter in exclusionFilters)
                    {
                        var property = typeof(T).GetProperty(filter.Key);
                        if (property != null)
                        {
                            var value = property.GetValue(record)?.ToString();
                            if (filter.Value.Any(exclude => value != null && value.Contains(exclude)))
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }).ToList();
            }

            foreach (var record in records)
            {
                if (isValid(record))
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
