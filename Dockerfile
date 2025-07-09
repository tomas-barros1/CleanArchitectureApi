FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar os arquivos de projeto e restaurar dependências
COPY ["API/API.csproj", "API/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infra/Infra.csproj", "Infra/"]
COPY ["CrossCutting/CrossCutting.csproj", "CrossCutting/"]
RUN dotnet restore "API/API.csproj"

# Copiar todo o código fonte
COPY . .

# Build da aplicação
RUN dotnet build "API/API.csproj" -c Release -o /app/build

# Publicar a aplicação
RUN dotnet publish "API/API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "API.dll"]
