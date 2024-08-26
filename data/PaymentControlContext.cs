using cadastro.Model;
using Microsoft.EntityFrameworkCore;

namespace PaymentControl.data
{
    public class PaymentControlContext : DbContext
    {
        public PaymentControlContext(DbContextOptions<PaymentControlContext> options) : base(options)
        { }
        public DbSet<AgendamentoModel> agendamentos { get; set; }
        public DbSet<ParticipanteModel> participantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}