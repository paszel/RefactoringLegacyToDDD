namespace PhotoStock.BusInterfaces
{
  public interface IEventHandler<T>
  {
    void Handle(T @event);
  }
}