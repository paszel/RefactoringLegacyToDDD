using Sales.Domain;

namespace PhotoStock.Tests.Sales.Infrastructure
{
  public class TestBus : IEventBus
  {
    public void Publish<T>(T @event)
    {
      
    }
  }
}