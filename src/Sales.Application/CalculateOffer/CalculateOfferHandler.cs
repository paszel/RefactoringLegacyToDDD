using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Sales.Domain;
using Sales.Domain.Discount;

namespace Sales.Application.CalculateOffer
{
  public class CalculateOfferHandler : ICalculateOfferHandler
  {
    private readonly IDiscountCalculator _discountCalculator;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public CalculateOfferHandler(IDiscountCalculator discountCalculator, IProductRepository productRepository, IOrderRepository orderRepository)
    {
      _discountCalculator = discountCalculator;
      _productRepository = productRepository;
      _orderRepository = orderRepository;
    }

    public Offer Handle(CalculateOfferQuery query)
    {
      Order order = _orderRepository.Get(query.OrderId);
      return order.CalculateOffer(_discountCalculator, _productRepository);
    }
  }
}
