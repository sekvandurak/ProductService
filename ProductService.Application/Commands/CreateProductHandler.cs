using MediatR;
using ProductService.Application.Interfaces;
using ProductService.Application.Products.Commands;
using ProductService.Domain.Products.ValueObjects;

namespace ProductService.Application.Commands;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;

    public CreateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var money = new Money(request.Amount, request.Currency);
        var product = new Domain.Products.Entities.Product(request.Name, money);

        await _productRepository.AddAsync(product, cancellationToken);
        await _productRepository.SaveChangesAsync(cancellationToken);
        return product.Id;
    }
}