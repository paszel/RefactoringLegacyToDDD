namespace PhotoStock.Infrastructure
{
  public interface IEventBus
  {
    void Publish<T>(T @event);
  }
}
