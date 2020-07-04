using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowMeTheCodeController : ControllerBase
    {
        /// <summary>
        /// Método que retorna a url com o código fonte no GitHub.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            return "https://github.com/ev-mendonca/sp-tech-chalenge";
        }
    }
}
