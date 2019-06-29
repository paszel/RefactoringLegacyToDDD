namespace PhotoStock.BusInterfaces
{
  public interface ICommandHandler<T>
  {
    void Handle(T command);
  }
}