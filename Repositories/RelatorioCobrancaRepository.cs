using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using PaymentControl.data;
using PaymentControl.data.Mappings;
using PaymentControl.model;
using PaymentControl.model.Dtos;
using PaymentControl.Repositories.Interface;
using PaymentControl.Services;

namespace PaymentControl.Repositories
{
    public class RelatorioCobrancaRepository : IRelatorioCobranca
    {
        private readonly PaymentControlContext _context;
        private readonly CsvService _csvService;
        public RelatorioCobrancaRepository(PaymentControlContext context, CsvService csvService)
        {
            _context = context;
            _csvService = csvService;
        }

        public async Task AddRelatorioCobranca(Stream relatorioCobrancaCsv)
        {
            var exclusionFilter = new Dictionary<string, List<string>>
            {
                { "Sacador", new List<string> { "Ordenado por:", "Gerado em:", "Cedente", "Tipo Consulta:", "Conta Corrente:", "Sacado" } }
            };

            var validRecords = _csvService.LerArquivoCSV<RelatorioCobrancaDTO>(
            relatorioCobrancaCsv,
            row => row[0] == "Sacado" && row.Contains("Nosso Número"),
            record => !string.IsNullOrWhiteSpace(record.Sacador) &&
                !string.IsNullOrEmpty(record.NossoNumero) &&
                !string.IsNullOrEmpty(record.SeuNumero) &&
                !string.IsNullOrEmpty(record.Entrada) &&
                !string.IsNullOrEmpty(record.Vencimento) &&
                !string.IsNullOrEmpty(record.Valor),
                null,
                null,
                new RelatorioCobrancaHelperCsvMapping(),
                exclusionFilter
            );

            foreach (var record in validRecords)
            {
                int clienteID = ExtractClienteIdFromSeuNumero(record.SeuNumero) ?? 0;
                var cliente = await _context.clientes.FindAsync(clienteID);
                if (cliente == null)
                {
                    int? observacaoNumero = ExtractClienteIdFromSeuNumero(cliente.Observacao ?? string.Empty);
                    if (observacaoNumero.HasValue)
                    {
                        cliente = await _context.clientes
                            .Where(c => ExtractClienteIdFromSeuNumero(c.Observacao) == observacaoNumero.Value)
                            .FirstOrDefaultAsync();
                        if (cliente == null)
                        {
                            throw new Exception($"Cliente com ID {clienteID} e número de observação {observacaoNumero.Value} não existe.");
                        }
                        else
                        {
                            throw new Exception($"O cliente com ID {clienteID} não existe e nenhum número de observação válido está disponível.");
                        }
                    }
                }
                var cobranca = new RelatorioCobrancaModel
                {
                    Sacador = record.Sacador,
                    NossoNumero = record.NossoNumero,
                    SeuNumero = record.SeuNumero,
                    Entrada = record.Entrada,
                    Vencimento = record.Vencimento,
                    LimitePgto = record.LimitePgto,
                    Valor = record.Valor,
                    idCliente = clienteID,
                };
                _context.relatorioCobrancas.Add(cobranca);
            }

            await _context.SaveChangesAsync();
        }

        private int? ExtractClienteIdFromSeuNumero(string seuNumero)
        {
            var match = Regex.Match(seuNumero, @"(?:COD AUT|COD LEG):\s*(\d+)");
            if (match.Success)
            {
                return int.Parse(match.Groups[1].Value);
            }
            match = Regex.Match(seuNumero, @"\d+");
            if (match.Success)
            {
                return int.Parse(match.Value);
            }
            return null;
        }

        public Task<List<RelatorioCobrancaDTO>> GetRelatorioCobrancaArquivo()
        {
            throw new NotImplementedException();
        }

        public async Task<List<RelatorioCobrancaDTO>> GetRelatorioCobrancas()
        {
            var cobrancas = await _context.relatorioCobrancas.AsNoTracking()
            .Select(c => new RelatorioCobrancaDTO
            {
                Sacador = c.Sacador,
                NossoNumero = c.NossoNumero,
                SeuNumero = c.SeuNumero,
                Entrada = c.Entrada,
                Vencimento = c.Vencimento,
                LimitePgto = c.LimitePgto,
                Valor = c.Valor,
            }).ToListAsync();
            return cobrancas;
        }
    }
}