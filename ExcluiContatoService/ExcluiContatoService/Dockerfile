# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ../TechChallenge.Shared/TechChallenge.Shared.csproj ./TechChallenge.Shared/
COPY ExcluiContatoService.csproj ./

RUN dotnet restore ExcluiContatoService.csproj

COPY ../TechChallenge.Shared ./TechChallenge.Shared
COPY . .

RUN dotnet publish ExcluiContatoService.csproj -c Release -o /app/out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "ExcluiContatoService.dll"]
