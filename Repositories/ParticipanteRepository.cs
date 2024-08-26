using cadastro.Model;
using cadastro.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using PaymentControl.data;
using PaymentControl.model.Dtos;

namespace cadastro.Repositories
{
    public class ParticipanteRepository : BaseRepository, IParticipanteRepository
    {
        private readonly PaymentControlContext _context;

        public ParticipanteRepository(PaymentControlContext context)
        {
            _context = context;
        }

        public Task<ParticipanteModel> GetParticipanteById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ParticipanteDto>> GetParticipantes()
        {
            return await _context.participantes.Select(x => new ParticipanteDto
            {
                Id = x.Id,
                Name = x.Name,
                Active = x.Active,
                StatusAgendamento = x.StatusAgendamento,
                Agendamentos = x.Agendamentos,
                Email = x.Email,
                LastName = x.LastName
            }).ToListAsync();
        }
    }
}