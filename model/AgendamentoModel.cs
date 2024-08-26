using System.ComponentModel.DataAnnotations;

namespace cadastro.Model
{
    public class AgendamentoModel : Base
    {
        public DateTime Data { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFim { get; set; }
        public required string Descricao { get; set; }
        public required string Local { get; set; }
        public required ParticipanteModel Participante { get; set; }
        public int Participante_id { get; set; }
    }
}