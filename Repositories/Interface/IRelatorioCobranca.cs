using PaymentControl.model.Dtos;

namespace PaymentControl.Repositories.Interface
{
    public interface IRelatorioCobranca
    {
        Task<List<RelatorioCobrancaDTO>> GetRelatorioCobrancaArquivo();
        Task<List<RelatorioCobrancaDTO>> GetRelatorioCobrancas();
        Task AddRelatorioCobranca(Stream relatorioCobrancaCsv);
    }
}