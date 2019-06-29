namespace Sales.Domain
{
  public interface IEventBus
  {
    void Publish<T>(T @event);
  }
}
