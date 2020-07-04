using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Taxas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxaJurosController : ControllerBase
    {

        /// <summary>
        /// Método que retorna uma taxa de juros padrão.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retorna o valor da taxa padrão de 0.01</response>
        [HttpGet]
        public double Get()
        {
            return 0.01;
        }
    }
}
