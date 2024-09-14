using Microsoft.EntityFrameworkCore;
using PaymentControl.data;
using PaymentControl.data.Mappings;
using PaymentControl.model;
using PaymentControl.model.Dtos;
using PaymentControl.Repositories.Interface;
using PaymentControl.Services;

namespace PaymentControl.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly PaymentControlContext _context;
        private readonly CsvService _csvService;
        public ClienteRepository(PaymentControlContext context, CsvService csvService)
        {
            _context = context;
            _csvService = csvService;
        }
        public async Task AddCliente(Stream clienteCsv)
        {
            bool isValidRecord(ClienteDTO record)
            {
                return record != null && !string.IsNullOrEmpty(record.Nome);
            }
            var validRecords = _csvService.LerArquivoCSV<ClienteDTO>(
                clienteCsv,
                header => header.Contains("idCliente") && header.Contains("nome"),
                isValidRecord,
                null,
                null,
                new ClienteHelperCsvMapping(),
                null
            );
            foreach (var record in validRecords)
            {
                var existingCliente = await _context.clientes
                .FirstOrDefaultAsync(c => c.Cpf == record.Cpf);

                if (existingCliente != null)
                {
                    // Atualize o cliente existente com os novos dados
                    existingCliente.Id_Cliente = record.IdCliente;
                    existingCliente.Nome = record.Nome;
                    existingCliente.Email = record.Email;
                    existingCliente.DataNasc = record.DataNasc;
                    existingCliente.SexoMasc = record.SexoMasc;
                    existingCliente.Fisica = record.Fisica;
                    existingCliente.DtCadastro = record.DtCadastro;
                    existingCliente.Rg = record.Rg;
                    existingCliente.Observacao = record.Observacao;
                    existingCliente.Trabalho = record.Trabalho;
                    existingCliente.ObsDocFis = record.ObsDocFis;
                    existingCliente.BooCliente = record.BooCliente;
                    existingCliente.BooFornecedor = record.BooFornecedor;
                    existingCliente.InscMunicipal = record.InscMunicipal;
                    existingCliente.Ativo = record.Ativo;
                    existingCliente.OptSimplesNac = record.OptSimplesNac;
                    existingCliente.BooFuncionario = record.BooFuncionario;
                    existingCliente.OrgExpedidor = record.OrgExpedidor;
                    existingCliente.Contador = record.Contador;
                    existingCliente.Suframa = record.Suframa;
                    existingCliente.ClienteSistema = record.ClienteSistema;
                    existingCliente.ValidadeCertificado = record.ValidadeCertificado;
                }
                else
                {
                    var cliente = new ClienteModel
                    {
                        Id_Cliente = record.IdCliente,
                        Nome = record.Nome,
                        Email = record.Email,
                        DataNasc = record.DataNasc,
                        Cpf = record.Cpf,
                        SexoMasc = record.SexoMasc,
                        Fisica = record.Fisica,
                        DtCadastro = record.DtCadastro,
                        Rg = record.Rg,
                        Observacao = record.Observacao,
                        Trabalho = record.Trabalho,
                        ObsDocFis = record.ObsDocFis,
                        BooCliente = record.BooCliente,
                        BooFornecedor = record.BooFornecedor,
                        InscMunicipal = record.InscMunicipal,
                        Ativo = record.Ativo,
                        OptSimplesNac = record.OptSimplesNac,
                        BooFuncionario = record.BooFuncionario,
                        // Imagem = record.Imagem,  // Comentado se não estiver no banco
                        OrgExpedidor = record.OrgExpedidor,
                        Contador = record.Contador,
                        Suframa = record.Suframa,
                        ClienteSistema = record.ClienteSistema,
                        ValidadeCertificado = record.ValidadeCertificado,
                    };
                    _context.clientes.Add(cliente);
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ClienteDTO> GetClienteById(int id)
        {
            var cliente = await _context.clientes
            .Include(c => c.Boletos)
            .FirstOrDefaultAsync(c => c.Id == id);
            var clienteDTO = new ClienteDTO()
            {
                IdCliente = cliente.Id,
                Nome = cliente.Nome,
                Boletos = (ICollection<RelatorioCobrancaModel>)cliente.Boletos.Select(b => new RelatorioCobrancaDTO
                {
                    Id = b.Id,
                    Sacador = b.Sacador,
                    NossoNumero = b.NossoNumero,
                    SeuNumero = b.SeuNumero,
                    Entrada = b.Entrada,
                    Vencimento = b.Vencimento,
                    Valor = b.Valor,
                }).ToList()

            };
            return clienteDTO;
        }

        public async Task<(List<ClienteDTO> Clientes, int TotalClientes)> GetClientes(int pageNumber, int pageSize, bool? clienteSistema = null)
        {
            // Inicia a query base para contar o total de clientes
            var query = _context.clientes.AsQueryable();

            // Aplica o filtro por cliente de sistema, se especificado
            if (clienteSistema.HasValue)
            {
                query = query.Where(c => c.ClienteSistema == clienteSistema.Value);
            }

            var totalClientes = await query.CountAsync();

            var clientes = await query
                .Include(c => c.Boletos) // Inclui os boletos relacionados aos clientes
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new ClienteDTO
                {
                    IdCliente = c.Id,
                    Nome = c.Nome,
                    Email = c.Email,
                    Cpf = c.Cpf,
                    DtCadastro = c.DtCadastro,
                    Observacao = c.Observacao,
                    BooCliente = c.BooCliente,
                    Ativo = c.Ativo,
                    ClienteSistema = c.ClienteSistema,

                    // Mapeia os boletos para o DTO correspondente
                    Boletos = (ICollection<RelatorioCobrancaModel>)c.Boletos.Select(b => new RelatorioCobrancaDTO
                    {
                        Id = b.Id,
                        Sacador = b.Sacador,
                        NossoNumero = b.NossoNumero,
                        SeuNumero = b.SeuNumero,
                        Entrada = b.Entrada,
                        Vencimento = b.Vencimento,
                        Valor = b.Valor,
                    }).ToList()
                })
                .ToListAsync();

            return (clientes, totalClientes);
        }

        public Task<List<ClienteDTO>> GetClientesArquivo()
        {
            throw new NotImplementedException();
        }
    }
}