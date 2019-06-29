using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Sales.Domain;

namespace Sales.Application
{
  public class OrderService : IOrderService
  {
    private readonly INumberGenerator _numberGenerator;
    private readonly IConfiguration _configuration;
    private readonly IDiscountCalculator _discountCalculator;
    private readonly IProductRepository _productRepository;

    public OrderService(INumberGenerator numberGenerator, IConfiguration configuration, IDiscountCalculator discountCalculator, IProductRepository productRepository)
    {
      _numberGenerator = numberGenerator;
      _configuration = configuration;
      _discountCalculator = discountCalculator;
      _productRepository = productRepository;
    }

    public void CreateOrder(string id, string clientId)
    {
      string number = _numberGenerator.GenerateNumber();

      Order o = new Order(){ ClientId = clientId, Id = id, Number = number, Status = OrderStatus.New };

      CreateConnection().Execute("INSERT INTO [Order](id,number,clientId) VALUES(@id,@number,@clientId)", o);
    }

    public void AddProduct(string orderId, string productId)
    {
      Order order = GetOrderInternal(orderId);
      order.AddProduct(productId);
      
      CreateConnection().Execute(
        "insert into OrderItem(orderId, productId)values(@orderId,@productId)",
        new { orderId = orderId, productId });
    }

    public Offer CalculateOffer(string orderId)
    {
      Order order = GetOrderInternal(orderId);
      return order.CalculateOffer(_discountCalculator, _productRepository);
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
