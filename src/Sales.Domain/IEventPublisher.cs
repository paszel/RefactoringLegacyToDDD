namespace Sales.Domain
{
  public interface IEventPublisher
  {
    void Publish<T>(T @event);
  }
}
