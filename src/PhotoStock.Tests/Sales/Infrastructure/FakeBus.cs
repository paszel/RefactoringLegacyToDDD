using Sales.Domain;

namespace PhotoStock.Tests.Sales.Infrastructure
{
  public class FakeBus : IEventBus
  {
    public void Publish<T>(T @event)
    {      
    }
  }
}