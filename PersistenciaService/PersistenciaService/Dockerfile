# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos .csproj
COPY ../TechChallenge.Shared/TechChallenge.Shared.csproj ./TechChallenge.Shared/
COPY PersistenciaService.csproj ./

# Restaura as dependências
RUN dotnet restore PersistenciaService.csproj

# Copia o restante dos arquivos
COPY ../TechChallenge.Shared ./TechChallenge.Shared
COPY . .

# Remove diretórios bin e obj para evitar conflitos
RUN rm -rf ./TechChallenge.Shared/bin ./TechChallenge.Shared/obj

# Publica o projeto
RUN dotnet publish PersistenciaService.csproj -c Release -o /app/out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "PersistenciaService.dll"]
