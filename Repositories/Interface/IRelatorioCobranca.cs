using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cadastro.Repositories.Interface;
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