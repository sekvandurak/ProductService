using MediatR;
using ProductService.Application.Interfaces;
using ProductService.Domain.Products.Entities;
using ProductService.Domain.Products.ValueObjects;
using ErrorOr;

namespace ProductService.Application.Products.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<Guid>>
{
    private readonly IProductRepository _repository;

    public CreateProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }
    public async Task<ErrorOr<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {

        //Basic validation sample
        if (request.Amount < 0)
        {
            return Error.Validation(
                code: "Product.InvalidAmount",
                description: "Amount must be bigger than zero"
            );
        }
        var money = new Money(request.Amount, request.Currency);
        var product = new Product(request.Name, money);

        await _repository.AddAsync(product, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
