using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentControl.model;
using PaymentControl.model.Dtos;
using PaymentControl.Services;

namespace PaymentControl.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioCobrancaControllers : Controller
    {
        const string path = "C:\\Users\\dev01\\Documents\\Bemasoft Mineiros";

        [HttpGet]
        public ActionResult Get()
        {
            var cobranca = CsvService.LerArquivoCSV(path + "\\relatorioCobrancas.csv");
            return Ok(cobranca);
        }

        [HttpPost]
        public ActionResult Post([FromBody] RelatorioCobrancaDTO relatorioCobranca)
        {
            CsvService.GravarArquivoCSV(path, new List<RelatorioCobrancaDTO> { relatorioCobranca });
            return Ok();
        }
    }
}