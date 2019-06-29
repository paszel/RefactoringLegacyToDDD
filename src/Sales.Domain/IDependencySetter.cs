namespace Sales.Domain
{
  public interface IDependencySetter
  {
    void SetEventPublisher(IEventBus eventBus);
  }
}