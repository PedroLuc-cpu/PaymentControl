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

        public async Task<ParticipanteModel> GetParticipanteById(int id)
        {
            var participante = await _context.participantes.Include(x => x.Agendamentos).Where(x => x.Id == id).FirstOrDefaultAsync();

            if (participante == null)
            {
                throw new KeyNotFoundException($"Participante com Id {id} não encontrado.");
            }

            return participante;
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