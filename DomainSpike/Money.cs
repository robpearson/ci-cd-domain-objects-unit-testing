namespace DomainSpike;

public class Money : ValueObject
{
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative");
        }
        Amount = amount;
        Currency = currency ?? throw new ArgumentNullException(nameof(currency));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public Money Add(Money other)
    {
        if (Currency != other.Currency)
        {
            throw new ArgumentException("Currencies must match");
        }
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Multiply(int factor)
    {
        return new Money(Amount * factor, Currency);
    }
}