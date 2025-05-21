# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY TechChallenge.Shared/TechChallenge.Shared.csproj ./TechChallenge.Shared/
COPY AtualizaContatoService.csproj ./

RUN dotnet restore AtualizaContatoService.csproj

COPY TechChallenge.Shared ./TechChallenge.Shared
RUN rm -rf ./TechChallenge.Shared/obj ./TechChallenge.Shared/bin

COPY AtualizaContatoService.csproj ./
COPY Controllers ./Controllers
COPY Data ./Data
COPY Models ./Models
COPY Program.cs .
COPY appsettings.json .
COPY appsettings.Development.json .


RUN dotnet publish AtualizaContatoService.csproj -c Release -o /app/out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "AtualizaContatoService.dll"]
