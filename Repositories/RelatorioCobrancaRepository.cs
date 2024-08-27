using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentControl.model.Dtos;
using PaymentControl.Repositories.Interface;

namespace PaymentControl.Repositories
{
    public class RelatorioCobrancaRepository : IRelatorioCobranca
    {
        public Task<List<RelatorioCobrancaDTO>> GetRelatorioCobranca()
        {
            throw new NotImplementedException();
        }
    }
}