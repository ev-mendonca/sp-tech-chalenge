# sp-tech-chalenge
Projeto contendo duas WebAPIs onde uma webAPI principal, chamada API, consome uma webAPI secundária, chamada Taxa, como um serviço.

Para rodar a aplicação é necessário a versão 3.1 do .Net Core instalada. Caso não possua, pode encontrá-la em https://dotnet.microsoft.com/download/dotnet-core/3.1.

Para usar a aplicação, basta rodar as duas APIs, informando no arquivo appsettings.json do projeto API, qual o endpoint base do serviço Taxa. Para facilitar a publicação das WebAPIs, principalmente na utilização do Docker, foi disponibilizada a possibilidade de informar o endpoint base do serviço de Taxa numa variável de ambiente. Para isto, basta adicionar a variável de ambiente com o nome EndpointTaxaService, contendo a url definida. A variável de ambiente é priorizada em relação ao endpoint definido no arquivo de configuração, portanto o endpoint do arquivo de configuração só será utilizado quando não houver a variável de ambiente. Para saber como usar esta variável no Docker, é só verificar o exemplo de publicação do Docker mais abaixo.

Para uma melhor visualização da documentação básica das APIs, as duas já vem configuradas com SwaggerUI, que roda a partir da raíz da aplicação.

Dísponível na aplicação um projeto de teste unitário em XUnit, para validar o endpoint calculajuros da Api principal.

# Usando com Docker - Disponível a partir da Versão v2.0
Caso queira utilizar a aplicação com Docker, as duas apis já possuem um Dockerfile, cada, com as instruções necessárias para montar a imagem.

Para isso é necessário ter instalado o Docker. Caso não possua, acesse https://docs.docker.com/get-docker/ e siga as instruções. A versão da Engine utilizada durante o desenvolvimento desta aplicação foi a 19.03.5

Caso tenha a necessidade de gerar sua propria imagem, basta gerar o publish das duas aplicações e montar as imagens com base nos arquivos Dockerfile, disponíveis em cada uma das aplicações. 

Se quiser apenas utilizar as aplicações em Docker, pode baixar as imagens que estão disponíveis no dockerHub, seguindo os comandos abaixo.

API: docker pull evmendonca/api_img
Taxa: docker pull evmendonca/taxas_img

Para utilizar as imagens, segue um exemplo abaixo:

- Primeiro crie o container do serviço de Taxas, pois será necessário ver o endpoint dele para colocar na variável de ambiente do serviço de taxas do container da API. Para isso rode o comando "docker run -d -p 5001:80 --name TaxasService evmendonca/taxas_img", onde 5001 é a porta para que possa acessar a api de Taxas a partir do seu navegador.

* Após, verifique qual é o Ipv4 do container TaxasService. Para isso rode o comando "docker network inspect bridge" e no JSON informado, procure pelo container com nome TaxaService, para verificar seu endereço Ipv4. No exemplo, usaremos o IP 172.17.0.2

- Agora crie o conatiner da API rodando o comando "docker run -d -p 4000:80 --name Api -e EndpointTaxaService=http://172.17.0.2/ evmendonca/api_img", onde 4000 é a porta que servirá para acessar a API a partir do seu navegador e o parametro "-e" serve para informarmos que a variavel de ambiente EndpointTaxaService receberá o endepoint http://172.17.0.2/
