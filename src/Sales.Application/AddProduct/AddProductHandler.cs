using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Sales.Domain;

namespace Sales.Application.AddProduct
{
  public class AddProductHandler : ICommandHandler<AddProductCommand>
  {
    private readonly IConfiguration _configuration;

    public AddProductHandler(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public void Handle(AddProductCommand command)
    {
      Order order = GetOrderInternal(command.OrderId);
      order.AddProduct(command.ProductId);

      CreateConnection().Execute(
        "insert into OrderItem(orderId, productId)values(@orderId,@productId)",
        new { orderId = command.OrderId, command.ProductId });
    }

    private Order GetOrderInternal(string id)
    {
      Order o = CreateConnection().QueryFirst<Order>("select * from [order] where id = @id", new { id });
      o.Products = CreateConnection().Query<OrderItem>("select p.Id as ProductId from product p join OrderItem p2o on p.id = p2o.productId where p2o.orderId = @id", new { id }).ToList();
      return o;
    }


    private SqlConnection CreateConnection()
    {
      return new SqlConnection(_configuration["connectionString"]);
    }
  }
}