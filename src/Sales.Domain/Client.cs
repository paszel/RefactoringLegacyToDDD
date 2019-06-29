namespace Sales.Domain
{
  public class Client
  {
    private Money _creditLeft;

    public void Charge(Money cost)
    {
      if (_creditLeft > cost)
      {
        _creditLeft -= cost;
      }
      else
      {
        throw new IsufficientCreditsException();
      }
    }
  }
}