using Sales.Domain;

namespace Sales.Application.CreateOrder
{
  public class CreateOrderHandler : ICommandHandler<CreateOrderCommand>
  {
    private readonly IOrderFactory _orderFactory;
    private readonly IOrderRepository _orderRepository;

    public CreateOrderHandler(IOrderFactory orderFactory, IOrderRepository orderRepository)
    {
      _orderFactory = orderFactory;
      _orderRepository = orderRepository;
    }

    public void Handle(CreateOrderCommand command)
    {
      Order o = _orderFactory.Create(command.Id, command.ClientId);
      _orderRepository.Save(o);
    }
  }
}