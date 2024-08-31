using CsvHelper.Configuration.Attributes;

namespace PaymentControl.model.Dtos
{
    public class ClienteDTO
    {
        [Name("idCliente")]
        public int IdCliente { get; set; }
        [Name("nome")]
        public string Nome { get; set; } = String.Empty;
        [Name("email")]
        public string Email { get; set; } = String.Empty;
        [Name("dataNasc")]
        public DateTime? DataNasc { get; set; }
        [Name("cpf")]
        public string Cpf { get; set; } = String.Empty;
        [Name("sexoMasc")]
        public bool? SexoMasc { get; set; }
        [Name("fisica")]
        public bool? Fisica { get; set; }
        [Name("dtCadastro")]
        public DateTime? DtCadastro { get; set; }
        [Name("rg")]
        public string Rg { get; set; } = String.Empty;
        [Name("observacao")]
        public string Observacao { get; set; } = String.Empty;
        [Name("trabalho")]
        public string Trabalho { get; set; } = String.Empty;
        [Name("obs_doc_fis")]
        public string ObsDocFis { get; set; } = String.Empty;
        [Name("booCliente")]
        public bool? BooCliente { get; set; }
        [Name("booFornecedor")]
        public bool? BooFornecedor { get; set; }
        [Name("inscMunicipal")]
        public string InscMunicipal { get; set; } = String.Empty;
        [Name("ativo")]
        public bool Ativo { get; set; }
        [Name("optSimplesNac")]
        public bool? OptSimplesNac { get; set; }
        [Name("booFuncionario")]
        public bool? BooFuncionario { get; set; }
        // [Name("imagem")]
        // public byte[]? Imagem { get; set; }
        [Name("orgExpedidor")]
        public string OrgExpedidor { get; set; } = String.Empty;
        [Name("contador")]
        public string Contador { get; set; } = String.Empty;
        [Name("suframa")]
        public string Suframa { get; set; } = String.Empty;
        [Name("clienteSistema")]
        public bool ClienteSistema { get; set; }
        [Name("validadeCertificado")]
        public DateTime ValidadeCertificado { get; set; }
        public List<RelatorioCobrancaDTO> boletos { get; set; }
    }
}