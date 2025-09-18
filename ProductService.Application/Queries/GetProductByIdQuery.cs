using MediatR;
using ProductService.Application.Products.DTOs;

namespace ProductService.Application.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto?>;