using Sales.Domain;

namespace PhotoStock.Tests.Sales.Infrastructure
{
  public class FakeBus : IEventPublisher
  {
    public void Publish<T>(T @event)
    {      
    }
  }
}