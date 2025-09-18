using Microsoft.Extensions.DependencyInjection;
using ProductService.Infrastructure.Persistence;
using ProductService.Application.Interfaces;
using ProductService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductService.Infrastructure.Persistence.Configurations;


namespace ProductService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        //postgresql
        services.AddDbContext<ProductDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(ProductDbContext).Assembly.FullName)));

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}