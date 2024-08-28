using Microsoft.EntityFrameworkCore;
using PaymentControl.data;
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
            var validRecords = _csvService.LerArquivoCSV(relatorioCobrancaCsv);
            foreach (var record in validRecords)
            {
                var cobranca = new RelatorioCobrancaModel
                {
                    Sacador = record.Sacador,
                    NossoNumero = record.NossoNumero,
                    SeuNumero = record.SeuNumero,
                    Entrada = record.Entrada,
                    Vencimento = record.Vencimento,
                    LimitePgto = record.LimitePgto,
                    Valor = record.Valor,
                };
                _context.relatorioCobrancas.Add(cobranca);
            }
            await _context.SaveChangesAsync();
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