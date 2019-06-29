using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Sales.Domain;

namespace Sales.Application.AddProduct
{
  public class AddProductHandler : ICommandHandler<AddProductCommand>
  {
    private readonly IOrderRepository _orderRespository;

    public AddProductHandler(IOrderRepository orderRespository)
    {
      _orderRespository = orderRespository;
    }

    public void Handle(AddProductCommand command)
    {
      Order order = _orderRespository.Get(command.OrderId);
      order.AddProduct(command.ProductId);
      _orderRespository.Save(order);     
    }    
  }
}