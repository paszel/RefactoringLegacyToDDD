namespace PhotoStock.BusInterfaces
{
  public interface IEventBus
  {
    void Publish<T>(T @event);
  }
}
