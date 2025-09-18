using ErrorOr;
using MediatR;

namespace ProductService.Application.Products.Commands;

public record CreateProductCommand(string Name, decimal Amount, string Currency) : IRequest<ErrorOr<Guid>>;
