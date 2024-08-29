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
            var validRecords = _csvService.LerArquivoCSV<ClienteDTO>(
                clienteCsv,
                row => row[0] == "idCliente",
                null,
                null,
                null,
                new ClienteHelperCsvMapping(),
                null
            );
            foreach (var record in validRecords)
            {
                var cliente = new ClienteModel
                {
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
                    Imagem = record.Imagem,
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

        public Task<List<ClienteDTO>> GetClientes()
        {
            throw new NotImplementedException();
        }

        public Task<List<ClienteDTO>> GetClientesArquivo()
        {
            throw new NotImplementedException();
        }
    }
}