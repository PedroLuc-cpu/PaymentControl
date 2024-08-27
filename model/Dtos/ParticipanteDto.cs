using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cadastro.Model;

namespace PaymentControl.model.Dtos
{
    public class ParticipanteDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public bool Active { get; set; }
        public int StatusAgendamento { get; set; }
        public List<AgendamentoModel> Agendamentos { get; set; } = new List<AgendamentoModel>();
    }
}