# ---------- build stage ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# sadece csproj'ları kopyala (restore cache)
COPY ProductService.sln ./
COPY ProductService.Domain/ProductService.Domain.csproj ProductService.Domain/
COPY ProductService.Application/ProductService.Application.csproj ProductService.Application/
COPY ProductService.Infrastructure/ProductService.Infrastructure.csproj ProductService.Infrastructure/
COPY ProductService.Api/ProductService.Api.csproj ProductService.Api/

RUN dotnet restore ProductService.Api/ProductService.Api.csproj

# tüm kaynakları kopyala ve publish et
COPY . .
RUN dotnet publish ProductService.Api/ProductService.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# ---------- runtime stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# container içinde 8080'den dinle
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_HTTP_PORTS=8080

COPY --from=build /app/publish .
EXPOSE 8080

ENTRYPOINT ["dotnet", "ProductService.Api.dll"]
