using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Sales.Application.ToRemove;
using Sales.Domain;

namespace Sales.Application
{
  public class OrderService : IOrderService
  {
    private readonly INumberGenerator _numberGenerator;
    private readonly IConfiguration _configuration;

    public OrderService(INumberGenerator numberGenerator, IConfiguration configuration)
    {
      _numberGenerator = numberGenerator;
      _configuration = configuration;
    }

    public string CreateOrder(string clientId)
    {
      string id = Guid.NewGuid().ToString();
      string number = _numberGenerator.GenerateNumber();
      CreateConnection().Execute("INSERT INTO [Order](id,number,clientId) VALUES(@id,@number,@clientId)",
        new { id, number, clientId });
      return id;
    }

    public void AddProduct(string orderId, string productId)
    {
      OrderDto order = GetOrderInternal(orderId);
      if (order.Status != OrderStatus.New)
      {
        throw new NotFoundException();
      }

      if (order.Products.FirstOrDefault(f => f.Id == productId) != null)
      {
        throw new AlreadyExistsException("Product already exists");
      }

      CreateConnection().Execute(
        "insert into OrderItem(orderId, productId)values(@orderId,@productId)",
        new { orderId = orderId, productId });

    }

    private OrderDto GetOrderInternal(string id)
    {
      OrderDto o = CreateConnection().QueryFirst<OrderDto>("select * from [order] where id = @id", new { id });
      o.Products = CreateConnection().Query<ProductDto>("select p.* from product p join OrderItem p2o on p.id = p2o.productId where p2o.orderId = @id", new { id }).ToList();
      return o;
    }


    private SqlConnection CreateConnection()
    {
      return new SqlConnection(_configuration["connectionString"]);
    }
  }
}
