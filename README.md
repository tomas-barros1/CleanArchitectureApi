# Clean Architecture API

Uma API RESTful construída com ASP.NET Core 7.0 seguindo os princípios da Clean Architecture.

## 📋 Estrutura do Projeto

```
CleanArchitectureApi/
├── API/                 # Camada de apresentação
├── Application/         # Camada de aplicação
├── Domain/             # Camada de domínio
├── Infrastructure/     # Camada de infraestrutura
└── CrossCutting/      # Camada de injeção de dependências
```

## 🚀 Principais Características

- Clean Architecture
- SOLID Principles
- Dependency Injection
- Entity Framework Core
- Global Exception Handling
- DTOs e Auto Mapping
- Repository Pattern
- Docker Support

## 🛠️ Tecnologias Utilizadas

- .NET 7.0
- Entity Framework Core
- SQL Server
- Docker
- Swagger/OpenAPI

## 📦 Pré-requisitos

- .NET 7.0 SDK
- Docker (opcional)
- SQL Server ou SQL Server Docker

## 🏃‍♂️ Como Executar

### Usando .NET CLI

1. Clone o repositório

```bash
git clone [url-do-repositorio]
```

2. Navegue até a pasta do projeto

```bash
cd CleanArchitectureApi
```

3. Restaure os pacotes

```bash
dotnet restore
```

4. Execute as migrações

```bash
dotnet ef database update --project Infra --startup-project API
```

5. Execute o projeto

```bash
dotnet run --project API
```

### Usando Docker

1. Execute o docker-compose

```bash
docker-compose up -d
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

A aplicação usa as seguintes variáveis de ambiente que podem ser configuradas no `appsettings.json` ou através de variáveis de ambiente:

- `ConnectionStrings:DefaultConnection`: String de conexão com o banco de dados
- `Logging:LogLevel:Default`: Nível de log padrão
