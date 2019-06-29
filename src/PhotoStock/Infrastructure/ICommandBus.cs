namespace PhotoStock.Infrastructure
{
  public interface ICommandBus
  {
    void Send<T>(T command);
  }
}
