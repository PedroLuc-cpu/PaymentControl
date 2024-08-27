using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using PaymentControl.data.Mappings;
using PaymentControl.model.Dtos;

namespace PaymentControl.Services
{
    public class CsvService
    {
        public static void GravarArquivoCSV(string filePath, List<RelatorioCobrancaDTO> relatorioCobrancas)
        {
            var finalPath = Path.Combine(filePath, nameof(relatorioCobrancas) + ".csv");
            using (var writer = new StreamWriter(finalPath))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(relatorioCobrancas);
                }
            }
        }
        public static List<RelatorioCobrancaDTO> LerArquivoCSV(string filePath)
        {
            ManipularArquivoCSV(filePath);
            using var streamReader = new StreamReader(filePath);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null,
                MissingFieldFound = null,
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };
            using var csvReader = new CsvReader(streamReader, config);
            csvReader.Context.RegisterClassMap<RelatorioCobrancaMapping>();
            return csvReader.GetRecords<RelatorioCobrancaDTO>().ToList();
        }
        public static void ManipularArquivoCSV(string filePath)
        {
            var tempFilePath = Path.GetTempFileName();
            using (var reader = new StreamReader(filePath))
            using (var writer = new StreamWriter(tempFilePath))
            {
                string line;
                bool headerFound = false;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("Sacado") && line.Contains("Nosso Número") && line.Contains("Seu Número"))
                    {
                        headerFound = true;
                        writer.WriteLine(line);
                        continue;
                    }
                    if (headerFound && !string.IsNullOrWhiteSpace(line) && !line.All(c => c == ','))
                    {
                        writer.WriteLine(line);
                    }
                }
            }
            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }
    }
}