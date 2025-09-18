namespace ProductService.Application.Products.DTOs;

public record ProductDto(Guid Id, string Name, decimal Amount, string Currency);
