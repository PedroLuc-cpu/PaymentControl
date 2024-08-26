
using cadastro.Model;
using PaymentControl.model.Dtos;

namespace cadastro.Repositories.Interface
{
    public interface IParticipanteRepository : IBaseRepository
    {
        Task<IEnumerable<ParticipanteDto>> GetParticipantes();
        Task<ParticipanteModel> GetParticipanteById(int id);
    }
}