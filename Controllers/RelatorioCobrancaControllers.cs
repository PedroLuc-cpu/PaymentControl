using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentControl.model;
using PaymentControl.model.Dtos;
using PaymentControl.Repositories.Interface;
using PaymentControl.Services;

namespace PaymentControl.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioCobrancaControllers : Controller
    {
        private readonly IRelatorioCobranca _relatorioCobrancaRepository;
        private readonly CsvService _csvService;
        const string path = "C:\\Users\\dev01\\Documents\\Bemasoft Mineiros";
        public RelatorioCobrancaControllers(IRelatorioCobranca relatorioCobranca, CsvService csvService)
        {
            _relatorioCobrancaRepository = relatorioCobranca;
            _csvService = csvService;
        }
        [HttpGet("relatotio-arquivo")]
        public async Task<IActionResult> GetArquivo()
        {
            var filePath = Path.Combine(path, "relatorio_cobranca.csv.csv");
            //Func<string[], bool> headerValidation = row => row[0] == "Sacado" && row.Contains("Nosso Número");
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var validCobranca = await Task.Run(() =>
                    _csvService.LerArquivoCSV<RelatorioCobrancaDTO>(
                        stream,
                        row => row[0] == "Sacado" && row.Contains("Nosso Número"),
                        record => !string.IsNullOrWhiteSpace(record.Sacador) &&
                        !string.IsNullOrEmpty(record.NossoNumero) &&
                        !string.IsNullOrEmpty(record.SeuNumero) &&
                        !string.IsNullOrEmpty(record.Entrada) &&
                        !string.IsNullOrEmpty(record.Vencimento) &&
                        !string.IsNullOrEmpty(record.Valor)
                    )
                );
                return Ok(validCobranca);
            }
        }

        [HttpGet("relatorio-cobranca")]
        public async Task<IActionResult> Get()
        {
            var relatorios = await _relatorioCobrancaRepository.GetRelatorioCobrancas();
            return Ok(relatorios);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RelatorioCobrancaDTO relatorioCobranca)
        {
            await _relatorioCobrancaRepository.AddRelatorioCobranca(new MemoryStream());
            using (var stream = new FileStream(Path.Combine(path, "relatorio_cobranca.csv.csv"), FileMode.Append))
            {
                _csvService.GravarArquivoCSV(stream, new List<RelatorioCobrancaDTO> { relatorioCobranca });
            }
            return Ok();
        }
        [HttpPost("upload")]
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
                    await _relatorioCobrancaRepository.AddRelatorioCobranca(stream);
                }
            }
            return Ok();
        }
    }
}