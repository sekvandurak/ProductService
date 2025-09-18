📦 ProductService
🚀 Genel Bakış

ProductService, Clean Architecture + Domain-Driven Design (DDD) prensipleriyle geliştirilmiş bir .NET 9.0 projesidir.
Uygulama, ürün yönetimi için CQRS + MediatR kullanır, veri erişiminde EF Core + PostgreSQL ile çalışır ve beklenen/beklenmeyen hataları yönetmek için ErrorOr + Global Middleware entegrasyonuna sahiptir.

🏗️ Katmanlar
1. Api (Presentation Layer)

HTTP endpoint’leri (Controllers)

Swagger/OpenAPI dokümantasyonu

Global Error Handling (Middleware/ErrorHandlingMiddleware.cs)

ErrorOr → HTTP StatusCode mapping (Common/Errors/ErrorMapping.cs)

Merkezi Dependency Injection çağrısı (Program.cs)

2. Application Layer

CQRS (Commands/Queries) + MediatR

DTO’lar

Domain bağımsız iş kuralları

Repository arayüzleri

ErrorOr ile beklenen hata yönetimi

DependencyInjection.cs → servislerin kaydı

3. Domain Layer

Entities, Value Objects, Aggregates

BaseEntity, AggregateRoot, ValueObject

DomainException → domain kurallarının ihlali için

Tamamen bağımsız (hiçbir dış bağımlılık yok)

4. Infrastructure Layer

EF Core + PostgreSQL (Npgsql provider)

ProductDbContext + Entity konfigürasyonları (Fluent API)

Repository implementasyonları

Migration’lar (Persistence/Migrations/)

DependencyInjection.cs → DbContext ve repository kayıtları

5. Tests

xUnit + Moq

Unit Tests: Domain & Application

Integration Tests: Api + Infrastructure

🗄️ Veritabanı

PostgreSQL kullanılır.

Bağlantı dizesi: appsettings.json → "DefaultConnection"

Migration komutları:

dotnet ef migrations add InitialCreate -p ProductService.Infrastructure -s ProductService.Api
dotnet ef database update -p ProductService.Infrastructure -s ProductService.Api

📚 Kullanılan Kütüphaneler
Api

Swashbuckle.AspNetCore (Swagger/OpenAPI)

ErrorOr (Error handling mapping için de kullanılıyor)

Application

MediatR

MediatR.Extensions.Microsoft.DependencyInjection

ErrorOr

Infrastructure

Microsoft.EntityFrameworkCore

Microsoft.EntityFrameworkCore.Design

Npgsql.EntityFrameworkCore.PostgreSQL

Tests

xUnit

Moq

▶️ Çalıştırma
# Build
dotnet build

# Migration ekleme
dotnet ef migrations add InitialCreate -p ProductService.Infrastructure -s ProductService.Api

# Database güncelleme
dotnet ef database update -p ProductService.Infrastructure -s ProductService.Api

# API çalıştırma
dotnet run --project ProductService.Api/ProductService.Api.csproj


API çalıştığında Swagger arayüzü:
👉 http://localhost:5108/swagger

🔄 Hata Yönetimi

Middleware (Api/Middleware/ErrorHandlingMiddleware.cs)
Beklenmeyen (Unhandled) hataları yakalar → 500 Internal Server Error.

ErrorOr (Application + Api)
Beklenen hataları yönetir:

NotFound → 404

Validation → 400

Unauthorized → 401

📂 Klasör Yapısı
src/
 ├── ProductService.sln
 │
 ├── ProductService.Api/                 
 │    ├── Controllers/
 │    │    └── ProductsController.cs
 │    ├── Middleware/
 │    │    └── ErrorHandlingMiddleware.cs
 │    ├── Common/
 │    │    └── Errors/
 │    │         └── ErrorMapping.cs
 │    ├── Program.cs
 │    ├── appsettings.json
 │    └── appsettings.Development.json
 │
 ├── ProductService.Application/         
 │    ├── Interfaces/
 │    │    └── IProductRepository.cs
 │    ├── Products/
 │    │    ├── Commands/
 │    │    ├── Queries/
 │    │    └── DTOs/
 │    └── DependencyInjection.cs
 │
 ├── ProductService.Domain/              
 │    ├── Common/
 │    ├── Exceptions/
 │    └── Products/
 │
 ├── ProductService.Infrastructure/      
 │    ├── Persistence/
 │    │    ├── ProductDbContext.cs
 │    │    ├── Configurations/
 │    │    └── Migrations/
 │    ├── Repositories/
 │    └── DependencyInjection.cs
 │
 └── ProductService.Tests/               
      ├── UnitTests/
      └── IntegrationTests/
