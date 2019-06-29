namespace PhotoStock.Infrastructure
{
  public interface IEventHandler<T>
  {
    void Handle(T @event);
  }
}