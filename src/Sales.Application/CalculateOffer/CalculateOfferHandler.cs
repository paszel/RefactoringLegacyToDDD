using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Sales.Domain;
using Sales.Domain.Discount;

namespace Sales.Application.CalculateOffer
{
  public class CalculateOfferHandler : ICalculateOfferQueryHandler
  {
    private readonly IConfiguration _configuration;
    private readonly IDiscountCalculator _discountCalculator;
    private readonly IProductRepository _productRepository;

    public CalculateOfferHandler(IConfiguration configuration, IDiscountCalculator discountCalculator, IProductRepository productRepository)
    {
      _configuration = configuration;
      _discountCalculator = discountCalculator;
      _productRepository = productRepository;
    }

    public Offer Handle(CalculateOfferQuery query)
    {
      Order order = GetOrderInternal(query.OrderId);
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
