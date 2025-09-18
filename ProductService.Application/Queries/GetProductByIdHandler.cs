using MediatR;
using ProductService.Application.Interfaces;
using ProductService.Application.Products.DTOs;
using ProductService.Application.Queries;
using ErrorOr;
using System.Reflection;

namespace ProductService.Application.Products.Queries;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ErrorOr<ProductDto?>>
{
    private readonly IProductRepository _repository;

    public GetProductByIdHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<ProductDto?>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (product == null)
        {
            return Error.NotFound(
                code: "Product.Error",
                description: $"Product with {request.Id} not found"
            );
        }

        return new ProductDto(
                    product.Id,
                    product.Name,
                    product.Price.Amount,
                    product.Price.Currency
                    );
    }
}
