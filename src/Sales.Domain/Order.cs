using System.Collections.Generic;
using System.Linq;

namespace Sales.Domain
{
  public class Order
  {
    private readonly string _number;

    public Order(string clientId, string id,string number, OrderStatus @new)
    {
      _number = number;
    }

    public List<OrderItem> Products { get; set; }
    public OrderStatus Status { get; set; }


    public void AddProduct(string productId)
    {
      if (Status != OrderStatus.New)
      {
        throw new NotFoundException();
      }

      if (Products.FirstOrDefault(f => f.ProductId == productId) != null)
      {
        throw new AlreadyExistsException("Product already exists");
      }

      Products.Add(new OrderItem(){ ProductId = productId });
    }
  }
}