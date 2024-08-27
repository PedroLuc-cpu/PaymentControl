using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cadastro.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PaymentControl.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipanteControllers : ControllerBase
    {
        private readonly IParticipanteRepository _repository;
        public ParticipanteControllers(IParticipanteRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pacientes = await _repository.GetParticipantes();
            return pacientes.Any()
                            ? Ok(pacientes)
                            : NotFound("Participantes não encontrados");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var participante = await _repository.GetParticipanteById(id);
            return participante != null ? Ok(participante) : NotFound("Participante não encontrado");
        }
    }
}