using cadastro.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cadastro.Database.Mappings
{
    public class ParticipanteMapping : BaseMap<ParticipanteModel>
    {
        public ParticipanteMapping() : base("participante")
        { }
        public override void Configure(EntityTypeBuilder<ParticipanteModel> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Name).HasColumnName("nome").HasColumnType("varchar(150)");
            builder.Property(x => x.LastName).HasColumnName("sobrenome").HasColumnType("varchar(150)");
            builder.Property(x => x.Email).HasColumnName("email").HasColumnType("varchar(150)");
            builder.Property(x => x.Active).HasColumnName("ativo").HasColumnType("bool").HasDefaultValue(false);
            builder.Property(x => x.StatusAgendamento).HasColumnName("status_agendamento").HasColumnType("int").HasDefaultValue(1);
        }
    }
}