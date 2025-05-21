# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ../TechChallenge.Shared/TechChallenge.Shared.csproj ./TechChallenge.Shared/
COPY ConsultaFunction.csproj ./

RUN dotnet restore ConsultaFunction.csproj

COPY ../TechChallenge.Shared ./TechChallenge.Shared
COPY . .

RUN dotnet publish ConsultaFunction.csproj -c Release -o /app/out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "ConsultaFunction.dll"]
