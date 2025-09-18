# Architecture Guidelines

## Framework & Runtime
- Tüm projeler .NET **9.0** ile oluşturulmalı ve hedeflenmelidir.
- Projeler şu klasör yapısında tutulur:

    ProductService.sln
    ProductService.Api/
    ProductService.Application/
    ProductService.Domain/
    ProductService.Infrastructure/
    ProductService.Tests/
- Çalıştırma: `dotnet run --project ProductService.Api/ProductService.Api.csproj`

## Architecture Style
- **Clean Architecture + DDD** prensipleri.
- Katmanlar:
- **Api** (Presentation): Controller, Swagger, Global Error Handling (Middleware), Merkezi DI çağrısı
- **Application**: CQRS + MediatR, DTO/Use-case/Handler, arayüzler (port)
- **Domain**: Entities, Value Objects, Aggregates, DomainException, BaseEntity/AggregateRoot
- **Infrastructure**: EF Core + **PostgreSQL**, DbContext, Fluent Config, Migrations, Repository impl.

## Database
- **PostgreSQL** kullanılacaktır.
- EF Core provider: `Npgsql.EntityFrameworkCore.PostgreSQL`.
- Migration’lar `ProductService.Infrastructure/Persistence/Migrations/` altında.

## CQRS & Mediator
- Application katmanında **CQRS + MediatR**.
- Komutlar: `.../Products/Commands`, Sorgular: `.../Products/Queries`.
- Handler’lar Application içinde.

## Dependency Injection
- Her katmanda `DependencyInjection.cs`:
- `Application.DependencyInjection.AddApplication(...)`
- `Infrastructure.DependencyInjection.AddInfrastructure(IConfiguration)`
- `Api/Program.cs` üzerinden merkezi çağrı:
```csharp
builder.Services.AddApplication()
                .AddInfrastructure(builder.Configuration);



Global Error Handling

Middleware ile tek noktadan hata yakalama.

DomainException → 400, diğer beklenmeyenler → 500.

Dosya: ProductService.Api/Middleware/ErrorHandlingMiddleware.cs

Test

xUnit (unit + integration), Moq (mock).

Unit: Domain & Application, Integration: Api + Infrastructure.

Kurallar

Domain katmanı dışa bağımlılık içermez.

Application, Domain’e bağımlıdır; Infrastructure detaylarını bilmez.

Infrastructure, Application arayüzlerini uygular.

Api sadece Application/Infrastructure servislerini DI ile kullanır.


src/
 ├── ProductService.sln
 │
 ├── ProductService.Api/                 # Presentation Layer
 │    ├── Controllers/
 │    │    └── ProductsController.cs
 │    ├── Middleware/
 │    │    └── ErrorHandlingMiddleware.cs
 │    ├── Program.cs
 │    ├── appsettings.json
 │    └── appsettings.Development.json
 │
 ├── ProductService.Application/         # Application Layer (CQRS, MediatR)
 │    ├── Interfaces/
 │    │    └── IProductRepository.cs
 │    ├── Products/
 │    │    ├── Commands/
 │    │    │    └── CreateProductCommand.cs
 │    │    │    └── CreateProductHandler.cs
 │    │    ├── Queries/
 │    │    │    └── GetProductByIdQuery.cs
 │    │    │    └── GetProductByIdHandler.cs
 │    │    └── DTOs/
 │    │         └── ProductDto.cs
 │    └── DependencyInjection.cs
 │
 ├── ProductService.Domain/              # Domain Layer (Entities & Rules)
 │    ├── Common/
 │    │    ├── BaseEntity.cs
 │    │    ├── AggregateRoot.cs
 │    │    └── ValueObject.cs
 │    │
 │    ├── Exceptions/
 │    │    └── DomainException.cs
 │    │
 │    ├── Products/
 │    │    ├── Entities/
 │    │    │    └── Product.cs
 │    │    ├── ValueObjects/
 │    │    │    └── Money.cs
 │    │    └── Aggregates/
 │    │         └── ProductAggregate.cs
 │
 ├── ProductService.Infrastructure/      # Infrastructure Layer
 │    ├── Persistence/
 │    │    ├── ProductDbContext.cs
 │    │    ├── Configurations/
 │    │    │    └── ProductConfiguration.cs
 │    │    └── Migrations/
 │    ├── Repositories/
 │    │    └── ProductRepository.cs
 │    └── DependencyInjection.cs
 │
 └── ProductService.Tests/               # Test Layer
      ├── UnitTests/
      │    └── ProductTests.cs
      └── IntegrationTests/
           └── ProductApiTests.cs
