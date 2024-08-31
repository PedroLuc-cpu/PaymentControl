using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cadastro.Repositories.Interface;
using PaymentControl.model.Dtos;

namespace PaymentControl.Repositories.Interface
{
    public interface IClienteRepository
    {
        Task<List<ClienteDTO>> GetClientesArquivo();
        Task<List<ClienteDTO>> GetClientes();
        Task AddCliente(Stream clienteCsv);
        Task<ClienteDTO> GetClienteById(int id);
    }
}