using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cadastro.Model
{
    public class ParticipanteModel : Base
    {
        public string Name { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public bool Active { get; set; }
        public int StatusAgendamento { get; set; }
        public required List<AgendamentoModel> Agendamentos { get; set; }
    }

}