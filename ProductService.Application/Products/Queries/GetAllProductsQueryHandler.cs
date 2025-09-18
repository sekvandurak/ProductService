using ErrorOr;
using MapsterMapper;
using MediatR;
using ProductService.Application.Interfaces;
using ProductService.Application.Products.DTOs;

namespace ProductService.Application.Products.Queries;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ErrorOr<List<ProductDto>>>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<List<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync(cancellationToken);
        var dtos = _mapper.Map<List<ProductDto>>(products);
        return dtos;
    }
}