using System;
using System.Threading.Tasks;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculaJurosController : ControllerBase
    {
        private ITaxaService _taxaService;

       
        public CalculaJurosController(ITaxaService taxaService)
        {
            _taxaService = taxaService;
        }
        /// <summary>
        /// Método que retorna o cálculo de juros considerando os valores passados.
        /// </summary>
        /// <param name="valorInicial"> Valor inicial que será usado como base do cálculo</param>
        /// <param name="tempo"> Tempo decorrido em meses. O valor em meses deve estar entre 1 e 12</param>
        /// <returns></returns>
        /// <response code="200">Retorna o valor calculado com aplicação dos juros</response>
        /// <response code="400">Erro que será retornado quando o parâmetro mês não estiver dentro do intervalo permitido</response>
        [HttpGet]
        public async Task<IActionResult> Get(double valorInicial, int tempo)
        {
            if(tempo < 1 || tempo > 12)
            {
                return BadRequest("O parâmetro tempo deve estar entre os valores 1 e 12");
            }
            try
            {
                double taxaJuros = await _taxaService.TaxaJuros();
                double valorComJuros = 1 + taxaJuros;
                double resultado = (valorInicial * Math.Pow(valorComJuros, tempo));
                return Ok(resultado - (resultado % 0.01));
            }
            catch(Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        
    }
}
