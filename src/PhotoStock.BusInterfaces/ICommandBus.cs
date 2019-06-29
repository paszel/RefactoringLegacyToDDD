namespace PhotoStock.BusInterfaces
{
  public interface ICommandBus
  {
    void Send<T>(T command);
  }
}
