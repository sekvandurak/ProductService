using ErrorOr;
using MediatR;
using ProductService.Application.Products.DTOs;

namespace ProductService.Application.Products.Queries;

public class GetAllProductsQuery : IRequest<ErrorOr<List<ProductDto>>>
{
    
}
