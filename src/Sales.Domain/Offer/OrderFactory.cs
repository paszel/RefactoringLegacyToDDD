namespace Sales.Domain.Offer
{
  public class OrderFactory : IOrderFactory
  {
    private readonly INumberGenerator _numberGenerator;
    private readonly IEventPublisher _enevtPublisher;

    public OrderFactory(INumberGenerator numberGenerator, IEventPublisher enevtPublisher)
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