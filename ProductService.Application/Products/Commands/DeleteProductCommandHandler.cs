using ErrorOr;
using MediatR;
using ProductService.Application.Interfaces;

namespace ProductService.Application.Products.Commands;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ErrorOr<Guid>>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(command.Id);
        if (product is null)
        {
            return Error.NotFound(
                code: "Product.NotFound",
                description: $"Product with id {command.Id} not found"
            );
        }

        await _productRepository.DeleteAsync(product, cancellationToken);
        await _productRepository.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
