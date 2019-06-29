namespace Sales.Domain
{
  public interface IDependencySetter
  {
    void SetEventPublisher(IEventPublisher eventBus);
  }
}