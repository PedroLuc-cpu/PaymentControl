using cadastro.Database.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentControl.model;

namespace PaymentControl.data.Mappings
{
        public class ClienteMapping : BaseMap<ClienteModel>
        {
                public ClienteMapping() : base("clientes")
                { }

                public override void Configure(EntityTypeBuilder<ClienteModel> builder)
                {
                        base.Configure(builder);
                        builder.Property(x => x.Id_Cliente).HasColumnName("id_cliente").HasColumnType("int");
                        builder.Property(x => x.Nome).HasColumnName("nome").HasColumnType("varchar(75)").IsRequired();
                        builder.Property(x => x.Email).HasColumnName("email").HasColumnType("varchar(100)");
                        builder.Property(x => x.DataNasc).HasColumnName("data_nasc").HasColumnType("date");
                        builder.Property(x => x.Cpf).HasColumnName("cpf").HasColumnType("varchar(14)").IsRequired();
                        builder.HasIndex(x => x.Cpf).IsUnique();
                        builder.Property(x => x.SexoMasc).HasColumnName("sexo_masc").HasColumnType("boolean");
                        builder.Property(x => x.Fisica).HasColumnName("fisica").HasColumnType("boolean");
                        builder.Property(x => x.DtCadastro).HasColumnName("dt_cadastro").HasColumnType("date")
                                .HasDefaultValueSql("CURRENT_DATE");
                        builder.Property(x => x.Rg).HasColumnName("rg").HasColumnType("varchar(20)");
                        builder.Property(x => x.Observacao).HasColumnName("observacao").HasColumnType("varchar(255)");
                        builder.Property(x => x.Trabalho).HasColumnName("trabalho").HasColumnType("varchar(100)");
                        builder.Property(x => x.ObsDocFis).HasColumnName("obs_doc_fis").HasColumnType("varchar(255)");
                        builder.Property(x => x.BooCliente).HasColumnName("boo_cliente").HasColumnType("boolean")
                                .HasDefaultValue(true);
                        builder.Property(x => x.BooFornecedor).HasColumnName("boo_fornecedor").HasColumnType("boolean")
                                .HasDefaultValue(false);
                        builder.Property(x => x.InscMunicipal).HasColumnName("insc_municipal").HasColumnType("varchar(20)");
                        builder.Property(x => x.Ativo).HasColumnName("ativo").HasColumnType("boolean").HasDefaultValue(true);
                        builder.Property(x => x.OptSimplesNac).HasColumnName("opt_simples_nac").HasColumnType("boolean")
                                .HasDefaultValue(false);
                        builder.Property(x => x.BooFuncionario).HasColumnName("boo_funcionario").HasColumnType("boolean")
                                .HasDefaultValue(false);
                        builder.Property(x => x.Imagem).HasColumnName("imagem").HasColumnType("bytea");
                        builder.Property(x => x.OrgExpedidor).HasColumnName("org_expedidor").HasColumnType("varchar(15)");
                        builder.Property(x => x.Contador).HasColumnName("contador").HasColumnType("varchar(70)");
                        builder.Property(x => x.Suframa).HasColumnName("suframa").HasColumnType("varchar(50)");
                        builder.Property(x => x.ClienteSistema).HasColumnName("cliente_sistema").HasColumnType("boolean")
                                .HasDefaultValue(false).IsRequired();
                        builder.Property(x => x.ValidadeCertificado).HasColumnName("validade_certificado")
                                .HasColumnType("date").HasDefaultValue(new DateTime(1899, 12, 30));
                        builder.HasMany(c => c.Boletos)
                                .WithOne(c => c.Cliente)
                                .HasPrincipalKey(b => b.Id_Cliente);
                }
        }
}