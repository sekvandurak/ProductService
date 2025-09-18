using MediatR;
using ErrorOr;

namespace ProductService.Application.Products.Commands;

public record DeleteProductCommand(Guid Id) : IRequest<ErrorOr<Guid>>;
