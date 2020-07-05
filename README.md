# sp-tech-chalenge
Projeto contendo duas WebAPIs onde uma webAPI principal, chamada API, consome uma webAPI secundária, chamada Taxa, como um serviço.

Para rodar a aplicação é necessário a versão 3.1 do .Net Core instalada. Caso não possua, pode encontrá-la em https://dotnet.microsoft.com/download/dotnet-core/3.1.

Para usar a aplicação, basta rodar as duas APIs, informando no arquivo appsettings.json do projeto API, qual o endpoint base do serviço Taxa.

Para uma melhor visualização da documentação básica das APIs, as duas já vem configuradas com SwaggerUI, que roda a partir da raíz da aplicação.

Dísponível na aplicação um projeto de teste unitário em XUnit, para validar o endpoint calculajuros da Api principal. 
