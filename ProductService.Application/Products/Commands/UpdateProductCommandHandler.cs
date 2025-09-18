using ErrorOr;
using MediatR;
using ProductService.Application.Interfaces;
using ProductService.Domain.Products.ValueObjects;


namespace ProductService.Application.Products.Commands;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ErrorOr<Guid>>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(command.Id);
        if (product is null)
        {
            return Error.NotFound(
                code: "Product.NotFound",
                description: $"Product with id {command.Id} not found"
            );
        }

        if(command.Amount < 0)
        {
            return Error.Validation(
                code: "Product.InvalidAmount",
                description: "Amount cannot be negative"
            );
        }

        var money = new Money(command.Amount, command.Currency);

        product.Update(command.Name, money);

        await _productRepository.UpdateAsync(product, cancellationToken);
        await _productRepository.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}