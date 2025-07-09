# Clean Architecture API

Uma API RESTful construída com ASP.NET 8 seguindo os princípios da Clean Architecture.

## 📋 Estrutura do Projeto

```
CleanArchitectureApi/
├── API/                 # Camada de apresentação
├── Application/         # Camada de aplicação
├── Domain/              # Camada de domínio
├── Infra/               # Camada de infraestrutura
├── CrossCutting/        # Injeção de dependências
├── Tests/               # Testes automatizados
```

## 🚀 Principais Características

- Clean Architecture
- SOLID Principles
- Injeção de Dependência
- Entity Framework Core
- Global Exception Handling
- DTOs e Auto Mapping
- Repository Pattern
- Docker Support
- Testes automatizados (xUnit, Moq)

## 🛠️ Tecnologias e Bibliotecas Utilizadas

- **.NET 8.0**
- **Entity Framework Core 8** (`Microsoft.EntityFrameworkCore.Design`, `Microsoft.EntityFrameworkCore.SqlServer`, `Microsoft.EntityFrameworkCore.Tools`)
- **SQL Server** (banco de dados relacional)
- **Swagger/OpenAPI** (`Swashbuckle.AspNetCore`)
- **Mapster** e **Mapster.DependencyInjection** (mapeamento e DI)
- **Microsoft.Extensions.DependencyInjection.Abstractions** (injeção de dependência)
- **Docker** (containerização)
- **xUnit** (testes unitários)
- **Moq** (mocks para testes)
- **coverlet.collector** (cobertura de testes)
- **Microsoft.NET.Test.Sdk** (infraestrutura de testes)
- **Microsoft.Data.SqlClient** (acesso a dados em testes)

## 📦 Pré-requisitos

- .NET 8.0 SDK
- Docker
- SQL Server ou Docker Compose

## 🏃‍♂️ Como Executar

### Usando Docker

1. Execute o docker-compose:

```bash
docker-compose up -d
```

2. Acesse o Swagger para testar a API:

```
http://localhost:8080/swagger
```

### Usando .NET CLI

1. Clone o repositório
2. Restaure os pacotes:

```bash
dotnet restore
```

3. Execute as migrações:

```bash
dotnet ef database update --project Infra --startup-project API
```

4. Execute a API:

```bash
dotnet run --project API
```

## 📚 Exemplos de Requests

### Listar livros

```bash
curl http://localhost:8080/api/books
```

### Criar livro

```bash
curl -X POST http://localhost:8080/api/books \
  -H "Content-Type: application/json" \
  -d '{"title":"Livro Teste","authorId":1,"genreId":1}'
```

## 🔬 Testes Automatizados

Os testes estão no diretório `Tests/` e usam xUnit, Moq e coverlet para cobertura.

Para rodar os testes:

```bash
dotnet test
```

## 📚 API Endpoints

### Books

- GET `/api/books` - Lista todos os livros
- GET `/api/books/{id}` - Obtém um livro específico
- POST `/api/books` - Cria um novo livro
- PUT `/api/books/{id}` - Atualiza um livro
- DELETE `/api/books/{id}` - Remove um livro

### Authors

- GET `/api/authors` - Lista todos os autores
- GET `/api/authors/{id}` - Obtém um autor específico
- POST `/api/authors` - Cria um novo autor
- PUT `/api/authors/{id}` - Atualiza um autor
- DELETE `/api/authors/{id}` - Remove um autor

### Genres

- GET `/api/genres` - Lista todos os gêneros
- GET `/api/genres/{id}` - Obtém um gênero específico
- POST `/api/genres` - Cria um novo gênero
- PUT `/api/genres/{id}` - Atualiza um gênero
- DELETE `/api/genres/{id}` - Remove um gênero

## 🔧 Configuração

A aplicação usa as seguintes variáveis de ambiente (podem ser configuradas no `appsettings.json` ou via Docker Compose):

- `ConnectionStrings:DefaultConnection`: String de conexão com o banco de dados
- `Logging:LogLevel:Default`: Nível de log padrão
- `ASPNETCORE_ENVIRONMENT`: Ambiente de execução (Development, Production)
