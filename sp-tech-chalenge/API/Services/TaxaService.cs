using API.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Services
{
    public class TaxaService : ITaxaService
    {
        private readonly BaseConfiguration _configuration;
        private readonly API.Configuration.Endpoint _endpoint;
        private readonly IHttpClientFactory _clientFactory;
        public TaxaService(IOptions<BaseConfiguration> options, IHttpClientFactory clientFactory)
        {
            _configuration = options.Value;
            _clientFactory = clientFactory;
            var predicado = new Func<API.Configuration.Endpoint, bool>(x => x.Name == "ITaxaService");
            if (_configuration.Endpoints == null || !_configuration.Endpoints.Any(predicado))
            {
                throw new Exception("Endpoint não encontrado para ITaxaService");
            }

            _endpoint = _configuration.Endpoints.FirstOrDefault(predicado);
        }

        public async Task<double> TaxaJuros()
        {
            try
            {
                var client = _clientFactory.CreateClient();
                var response = await client.GetAsync(_endpoint.Url + "taxaJuros");

                if (response.IsSuccessStatusCode)
                {
                    var responseValue = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<double>(responseValue);
                }
                else
                {
                    throw new Exception($"Erro na consulta da api {_endpoint.Url}");
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao consultar a api {_endpoint.Url}", ex);
            }
            
        }

    }
}

