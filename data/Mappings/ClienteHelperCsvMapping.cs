using System.Globalization;
using CsvHelper.Configuration;
using PaymentControl.helpers;
using PaymentControl.model.Dtos;

namespace PaymentControl.data.Mappings
{
    public class ClienteHelperCsvMapping : ClassMap<ClienteDTO>
    {
        public ClienteHelperCsvMapping()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.IdCliente).Name("idCliente");
            Map(m => m.Nome).Name("nome");
            Map(m => m.Email).Name("email");
            Map(m => m.DataNasc).Name("dataNasc");
            Map(m => m.Cpf).Name("cpf");
            Map(m => m.SexoMasc).Name("sexoMasc").TypeConverter<NullableBoolConverter>();
            Map(m => m.Fisica).Name("fisica").TypeConverter<NullableBoolConverter>();
            Map(m => m.DtCadastro).Name("dtCadastro");
            Map(m => m.Rg).Name("rg");
            Map(m => m.Observacao).Name("observacao");
            Map(m => m.Trabalho).Name("trabalho");
            Map(m => m.ObsDocFis).Name("obs_doc_fis");
            Map(m => m.BooCliente).Name("booCliente").TypeConverter<NullableBoolConverter>();
            Map(m => m.BooFornecedor).Name("booFornecedor").TypeConverter<NullableBoolConverter>();
            Map(m => m.Ativo).Name("ativo").TypeConverter<NullableBoolConverter>();
            Map(m => m.OptSimplesNac).Name("optSimplesNac").TypeConverter<NullableBoolConverter>();
            Map(m => m.BooFuncionario).Name("booFuncionario").TypeConverter<NullableBoolConverter>();
            // Map(m => m.Imagem).Name("imagem");
            Map(m => m.OrgExpedidor).Name("orgExpedidor");
            Map(m => m.Contador).Name("contador");
            Map(m => m.Suframa).Name("suframa");
            Map(m => m.ClienteSistema).Name("clienteSistema").TypeConverter<NullableBoolConverter>();
            Map(m => m.ValidadeCertificado).Name("validadeCertificado");
        }
    }
}