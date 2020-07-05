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
        private readonly string _enviromentEndpoint = Environment.GetEnvironmentVariable("EndpointTaxaService");
        /// <summary>
        /// Consrtrutor que monta o serviço de TaxaService. Responsável por definir o clientFactory e qual o endpointBase da API consultada.
        /// Caso exista um variável de ambiente chamada EndpointTaxaService configuradao, esta será usada como endpointBase da api.
        /// Caso não exista, será procurado um endpoint padrão na lista de endpoints informado pelo parâmetro <paramref name="options"/>
        /// </summary>
        /// <param name="options">IOptions de BaseConfiguration, responsável por trazer a lista de Endpoints padrão da aplicação.</param>
        /// <param name="clientFactory">IHttpClientFactory usado para definir o clientFactory para requisições Http.</param>
        public TaxaService(IOptions<BaseConfiguration> options, IHttpClientFactory clientFactory)
        {
            _configuration = options.Value;
            _clientFactory = clientFactory;

            if(string.IsNullOrEmpty(_enviromentEndpoint))
            {
                var predicado = new Func<API.Configuration.Endpoint, bool>(x => x.Name == "ITaxaService");
                if (_configuration.Endpoints == null || !_configuration.Endpoints.Any(predicado))
                {
                    throw new Exception("Não foi definida uma variável de ambiente EndpointTaxaService e nem foi encontrado um endpoint padrão para o serviço ITaxaService");
                }

                _endpoint = _configuration.Endpoints.FirstOrDefault(predicado);
            }
            else
            {
                _endpoint = new Configuration.Endpoint(_enviromentEndpoint);
            }

            
        }

        /// <summary>
        /// Método que faz uma requisição GET para o endpoint TaxaJuros, do endpoint base definido no construtor, e retorna o valor retornado pela API.
        /// </summary>
        /// <returns></returns>
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

