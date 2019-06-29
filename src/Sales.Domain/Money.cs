namespace Sales.Domain
{
  public class Money : ValueObject
  {
    private decimal _amount;
    private string Currency => _currency;
    public static Money ZERO = 0;
    private string _currency = "PLN";

    private Money(decimal amount) : this(amount, "PLN")
    {      
    }

    private Money(double amount) : this((decimal)amount, "PLN")
    {      
    }

    public Money(decimal amount, string currency)
    {
      _amount = amount;
      _currency = currency;
    }

    public static implicit operator decimal(Money money)
    {
      return money._amount;
    }

    public static Money operator -(Money x)
    {
      return new Money(-x._amount);
    }

    public static Money operator *(Money money, decimal value)
    {
      return new Money(money._amount * value);
    }

    public static Money operator *(Money money, double value)
    {
      return new Money((decimal)((double)money._amount * value));
    }

    public static Money operator *(double value, Money money)
    {
      return new Money((decimal)((double)money._amount * value));
    }

    public static Money operator *(decimal value, Money money)
    {
      return new Money(money._amount * value);
    }

    public static Money operator +(Money money, Money value)
    {
      return new Money(money._amount + value._amount);
    }

    public static Money operator -(Money money, Money value)
    {
      return new Money(money._amount - value._amount);
    }

    public static bool operator >(Money money, Money value)
    {
      return money._amount > value._amount;
    }

    public static bool operator >=(Money money, Money value)
    {
      return money._amount >= value._amount;
    }

    public static bool operator <(Money money, Money value)
    {
      return money._amount < value._amount;
    }

    public static bool operator <=(Money money, Money value)
    {
      return money._amount <= value._amount;
    }

    public static implicit operator Money(decimal value)
    {
      return new Money(value);
    }

    public Money Add(Money net)
    {
      return new Money(_amount + net._amount);
    }
  }
}