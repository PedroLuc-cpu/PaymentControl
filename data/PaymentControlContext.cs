using cadastro.Model;
using Microsoft.EntityFrameworkCore;
using PaymentControl.model;

namespace PaymentControl.data
{
    public class PaymentControlContext : DbContext
    {
        public PaymentControlContext(DbContextOptions<PaymentControlContext> options) : base(options)
        { }
        public DbSet<AgendamentoModel> agendamentos { get; set; }
        public DbSet<ParticipanteModel> participantes { get; set; }
        public DbSet<RelatorioCobrancaModel> relatorioCobrancas { get; set; }
        public DbSet<ClienteModel> clientes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}