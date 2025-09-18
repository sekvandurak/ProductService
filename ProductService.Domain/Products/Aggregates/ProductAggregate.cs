using ProductService.Domain.Products.Entities;

namespace ProductService.Domain.Products.Aggregates;

public class ProductAggregate
{
    public Product Product { get; private set; }

    public ProductAggregate(Product product)
    {
        Product = product;
    }
}