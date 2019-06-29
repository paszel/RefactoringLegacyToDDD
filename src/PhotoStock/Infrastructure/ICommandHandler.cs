namespace PhotoStock.Infrastructure
{
  public interface ICommandHandler<T>
  {
    void Handle(T command);
  }
}