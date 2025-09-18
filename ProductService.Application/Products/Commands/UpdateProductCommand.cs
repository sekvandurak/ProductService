using MediatR;
using ErrorOr;

namespace ProductService.Application.Products.Commands;


public record UpdateProductCommand(Guid Id, string Name, decimal Amount, string Currency) : IRequest<ErrorOr<Guid>>;
