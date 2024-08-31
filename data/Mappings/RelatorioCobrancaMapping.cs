using cadastro.Database.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentControl.model;

namespace PaymentControl.data.Mappings
{
    public class RelatorioCobrancaMapping : BaseMap<RelatorioCobrancaModel>
    {
        public RelatorioCobrancaMapping() : base("relatorio_cobranca")
        { }

        public override void Configure(EntityTypeBuilder<RelatorioCobrancaModel> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Sacador).HasColumnName("sacador").HasColumnType("varchar(255)");
            builder.Property(x => x.NossoNumero).HasColumnName("nosso_numero").HasColumnType("varchar(255)");
            builder.Property(x => x.SeuNumero).HasColumnName("seu_numero").HasColumnType("varchar(255)");
            builder.Property(x => x.Entrada).HasColumnName("entrada").HasColumnType("varchar(10)");
            builder.Property(x => x.Vencimento).HasColumnName("vencimento").HasColumnType("varchar(10)");
            builder.Property(x => x.LimitePgto).HasColumnName("limite_pagamento").HasColumnType("varchar(30)");
            builder.Property(x => x.Valor).HasColumnName("valor").HasColumnType("varchar(30)");
            builder.HasOne(rc => rc.Cliente)
            .WithMany()
            .HasForeignKey(rc => rc.idCliente)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}