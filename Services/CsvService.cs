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
        /// <summary>
        /// Lê um arquivo CSV, valida e processa os registros com base nas funções fornecidas.
        /// Se solicitado, grava os registros válidos e inválidos em fluxos de saída separados.
        /// </summary>
        /// <typeparam name="T">O tipo de objeto que representa os registros do CSV.</typeparam>
        /// <param name="stream">O fluxo de entrada que contém o arquivo CSV.</param>
        /// <param name="isHeaderValid">Uma função que valida se o cabeçalho do CSV está correto. Deve retornar true se o cabeçalho for válido, false caso contrário.</param>
        /// <param name="isValid">Uma função que valida se um registro do CSV é válido. Deve retornar true se o registro for válido, false caso contrário.</param>
        /// <param name="validOutStream">Um fluxo de saída para onde os registros válidos serão escritos. Pode ser nulo se a gravação não for necessária.</param>
        /// <param name="invalidOutStream">Um fluxo de saída para onde os registros inválidos serão escritos. Pode ser nulo se a gravação não for necessária.</param>
        /// <param name="classMap">Um mapeamento de classe que define como os registros do CSV são mapeados para o tipo <typeparamref name="T"/>. Pode ser nulo se o mapeamento automático for suficiente.</param>
        /// <param name="exclusionFilters">Um dicionário de filtros de exclusão que especifica quais registros devem ser ignorados com base nos valores das propriedades.</param>
        /// <returns>Uma lista de registros válidos lidos do CSV.</returns>
        /// <exception cref="ArgumentNullException">Se o fluxo de entrada, isHeaderValid ou isValid for nulo.</exception>
        /// <exception cref="ArgumentException">Se o fluxo de entrada não for legível.</exception>
        /// <exception cref="InvalidOperationException">Se o classMap for nulo, mas é necessário.</exception>
        /// <exception cref="Exception">Se o cabeçalho esperado não for encontrado.</exception>
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

            if (isHeaderValid == null)
            {
                throw new ArgumentNullException(nameof(isHeaderValid), "O parâmetro isHeaderValid não pode ser nulo.");
            }

            if (isValid == null)
            {
                throw new ArgumentNullException(nameof(isValid), "O parâmetro isValid não pode ser nulo.");
            }

            var validRecords = new List<T>();
            var invalidRecords = new List<T>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null,
                BadDataFound = null,
                MissingFieldFound = null,
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };

            stream.Position = 0;
            using var streamReader = new StreamReader(stream);
            using var csvReader = new CsvReader(streamReader, config);

            bool headerFound = false;
            string[] foundHeader = null;

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
                foundHeader = row;
            }

            if (!headerFound)
            {
                var foundHeaderString = foundHeader != null ? string.Join(", ", foundHeader) : "Nenhum cabeçalho encontrado";
                throw new Exception($"Cabeçalho esperado não encontrado. Cabeçalho encontrado: {foundHeaderString}");
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
