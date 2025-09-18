using ProductService.Domain.Common;

namespace ProductService.Domain.Products.ValueObjects;

public class Money : ValueObject
{
    public decimal Amount { get; private set; }
    public string Currency { get; private set; } = "USD";

    private Money() { } // EF Core için
    //neden ef coere için: ef core reflection ile nesne oluşturuyor, parametresiz constructor lazım

    public Money(decimal amount, string currency = "USD")
    {
        // Artık exception yok, Application katmanı validasyonu yapacak
        Amount = amount;
        Currency = string.IsNullOrWhiteSpace(currency) ? "USD" : currency;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}
