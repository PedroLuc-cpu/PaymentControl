using cadastro.Model;

namespace PaymentControl.model
{
    public class ClienteModel : Base
    {
        public int? Id_Cliente { get; set; }
        public string Nome { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public DateTime? DataNasc { get; set; }
        public string Cpf { get; set; } = String.Empty;
        public bool? SexoMasc { get; set; }
        public bool? Fisica { get; set; }
        public DateTime? DtCadastro { get; set; }
        public string Rg { get; set; } = String.Empty;
        public string Observacao { get; set; } = String.Empty;
        public string Trabalho { get; set; } = String.Empty;
        public string ObsDocFis { get; set; } = String.Empty;
        public bool? BooCliente { get; set; }
        public bool? BooFornecedor { get; set; }
        public string InscMunicipal { get; set; } = String.Empty;
        public bool Ativo { get; set; }
        public bool? OptSimplesNac { get; set; }
        public bool? BooFuncionario { get; set; }
        public byte[]? Imagem { get; set; }
        public string OrgExpedidor { get; set; } = String.Empty;
        public string Contador { get; set; } = String.Empty;
        public string Suframa { get; set; } = String.Empty;
        public bool ClienteSistema { get; set; }
        public DateTime ValidadeCertificado { get; set; }
    }
}