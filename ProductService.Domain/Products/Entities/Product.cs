using ProductService.Domain.Common;
using ProductService.Domain.Products.ValueObjects;

namespace ProductService.Domain.Products.Entities;

public class Product : AggregateRoot
{
    public string Name { get; private set; }
    public Money Price { get; private set; }

    private Product() { } // EF Core için

    public Product(string name, Money price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty.");

        Name = name;
        Price = price;
    }

    public void UpdatePrice(Money newPrice)
    {
        Price = newPrice ?? throw new ArgumentNullException(nameof(newPrice));
    }

    public void Update(string name, Money price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty.");

        Name = name;
        Price = price ?? throw new ArgumentNullException(nameof(price));
    }


}
