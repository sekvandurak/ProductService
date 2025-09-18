using Mapster;
using ProductService.Application.Products.DTOs;
using ProductService.Domain.Products.Entities;

namespace ProductService.Application.Common.Mappings;

// IRegister comes from Mapster

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Domain -> DTO
        config.NewConfig<Product, ProductDto>()
        .Map(dest => dest.Amount, src => src.Price.Amount)
        .Map(dest => dest.Currency, src => src.Price.Currency);
    }
}