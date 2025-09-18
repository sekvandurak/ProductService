namespace ProductService.Domain.Common;

public abstract class ValueObject
{
    // Değer nesneleri için özel işaretleme sınıfı

    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;

        var other = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents().Aggregate(default(int), (hash, obj) => HashCode.Combine(hash, obj));
    }
}