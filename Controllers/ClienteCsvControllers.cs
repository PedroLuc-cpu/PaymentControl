using Microsoft.AspNetCore.Mvc;
using PaymentControl.Repositories.Interface;
using PaymentControl.Services;

namespace PaymentControl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteCsvControllers : Controller
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly CsvService _csvService;
        public ClienteCsvControllers(IClienteRepository clienteRepository, CsvService csvService)
        {
            _clienteRepository = clienteRepository;
            _csvService = csvService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClienteById(int id)
        {
            var cliente = await _clienteRepository.GetClienteById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost("uploadCliente")]
        public async Task<IActionResult> Upload([FromForm] ICollection<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("O arquivo csv não foi selecionado");
            }
            foreach (var file in files)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var readerStream = new MemoryStream(stream.ToArray()))
                    {
                        readerStream.Position = 0;
                        using (var reader = new StreamReader(readerStream))
                        {
                            var fileContent = await reader.ReadToEndAsync();
                            Console.WriteLine(fileContent);
                        }
                    }
                    stream.Position = 0;
                    await _clienteRepository.AddCliente(stream);
                }
            }
            return Ok();
        }
    }
}