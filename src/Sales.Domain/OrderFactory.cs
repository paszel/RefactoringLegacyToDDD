namespace Sales.Domain
{
  public class OrderFactory : IOrderFactory
  {
    private readonly INumberGenerator _numberGenerator;
    private readonly IEventBus _enevtPublisher;

    public OrderFactory(INumberGenerator numberGenerator, IEventBus enevtPublisher)
    {
      _numberGenerator = numberGenerator;
      _enevtPublisher = enevtPublisher;
    }

    public Order Create(string id, string clientId)
    {
      string number = _numberGenerator.GenerateNumber();

      Order o = new Order(clientId, id, number, OrderStatus.New);
      (o as IDependencySetter).SetEventPublisher(_enevtPublisher);
      return o;
    }
  }
}