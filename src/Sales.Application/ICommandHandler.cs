namespace Sales.Application
{
  public interface ICommandHandler<T>
  {
    void Handle(T command);
  }
}