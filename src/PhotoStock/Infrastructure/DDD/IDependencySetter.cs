namespace PhotoStock.Infrastructure.DDD
{
  public interface IDependencySetter
  {
    void SetEventPublisher(IEventBus eventBus);
  }
}