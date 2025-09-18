using MediatR;
using ProductService.Application.Interfaces;
using ProductService.Domain.Products.Entities;
using ProductService.Domain.Products.ValueObjects;

namespace ProductService.Application.Products.Commands;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _repository;

    public CreateProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var money = new Money(request.Amount, request.Currency);
        var product = new Product(request.Name, money);

        await _repository.AddAsync(product, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
