using PhotoStock.BusInterfaces;
using Sales.Domain;

namespace Sales.Infrastructure
{
  public class EventPublisherAdapter : IEventPublisher
  {
    private readonly IEventBus _eventBus;

    public EventPublisherAdapter(IEventBus eventBus)
    {
      _eventBus = eventBus;
    }

    public void Publish<T>(T @event)
    {
      _eventBus.Publish(@event);
    }
  }
}