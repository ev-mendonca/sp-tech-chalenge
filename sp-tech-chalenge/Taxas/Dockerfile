FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
MAINTAINER Everton Mendon�a
COPY bin/release/netcoreapp3.1/publish App/
WORKDIR App/
EXPOSE 80
ENTRYPOINT dotnet Taxas.dll
