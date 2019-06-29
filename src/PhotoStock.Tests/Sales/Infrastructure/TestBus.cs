using Sales.Domain;

namespace PhotoStock.Tests.Sales.Infrastructure
{
  public class TestBus : IEventPublisher
  {
    public void Publish<T>(T @event)
    {
      
    }
  }
}