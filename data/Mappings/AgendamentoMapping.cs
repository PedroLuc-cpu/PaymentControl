using cadastro.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cadastro.Database.Mappings
{
    public class AgendamentoMapping : BaseMap<AgendamentoModel>
    {
        public AgendamentoMapping() : base("agendamento")
        { }

        public override void Configure(EntityTypeBuilder<AgendamentoModel> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Data).HasColumnName("data").HasColumnType("timestamp").HasDefaultValueSql("now()");
            builder.Property(x => x.HoraInicio).HasColumnName("hora_inicio").HasColumnType("timestamp");
            builder.Property(x => x.HoraFim).HasColumnName("hora_fim").HasColumnType("timestamp");
            builder.Property(x => x.Descricao).HasColumnName("descricao").HasColumnType("text");
            builder.Property(x => x.Local).HasColumnName("local").HasColumnType("varchar(100)");
            builder.Property(x => x.Participante_id).HasColumnName("participante_id").IsRequired();
            builder.HasOne(x => x.Participante).WithMany(x => x.Agendamentos).HasForeignKey(x => x.Participante_id);
        }

    }
}