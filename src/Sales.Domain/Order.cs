using System.Collections.Generic;
using System.Linq;
using Sales.Domain.Discount;

namespace Sales.Domain
{
  public class Order : AggregateRoot
  {
    private List<OrderItem> _products = new List<OrderItem>();
    private OrderStatus _status;
    private string _clientId;
    private string _number;

    protected Order() : base(null)
    { }

    internal Order(string clientId, string id, string number, OrderStatus status) : base(id)
    {
      _clientId = clientId;
      _number = number;
      _status = status;
      _products = new List<OrderItem>();
    }

    public void AddProduct(string productId)
    {
      if (_status != OrderStatus.New)
      {
        throw new NotFoundException();
      }

      if (_products.FirstOrDefault(f => f.ProductId == productId) != null)
      {
        throw new AlreadyExistsException("Product already exists");
      }

      _products.Add(new OrderItem(){ ProductId = productId });
    }

    public Offer CalculateOffer(IDiscountCalculator discountCalculator, IProductRepository productRepository)
    {
      if (_status != OrderStatus.New)
      {
        throw new NotFoundException();
      }

      List<OfferItem> availabeItems = new List<OfferItem>();
      List<OfferItem> unavailableItems = new List<OfferItem>();

      decimal totalCost = 0;
      foreach (var orderItem in _products)
      {
        Product product = productRepository.Get(orderItem.ProductId);
        if (product.Aviable)
        {
          OfferItem offerItem = new OfferItem()
          {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
          };

          availabeItems.Add(offerItem);
          totalCost += offerItem.Price;
        }
        else
        {
          OfferItem offerItem = new OfferItem { Id = product.Id, Name = product.Name, Price = product.Price };

          unavailableItems.Add(offerItem);
        }
      }

      decimal discount = discountCalculator.Calculate(availabeItems.Select(f => new OfferItem()
      {
        Name = f.Name,
        ProductType = f.ProductType
      }));

      return new Offer(_clientId, totalCost - discount, discount, availabeItems, unavailableItems);
    }

    public void SetStatus(OrderStatus status)
    {
      _status = status;
    }

    public bool SameAs(Order order)
    {
      return _clientId == order._clientId
             && _status == order._status
             && SameProducts(order._products);
    }

    private bool SameProducts(List<OrderItem> products)
    {
      if (_products.Count != products.Count)
      {
        return false;
      }

      foreach (OrderItem item in _products)
      {
        var r = products.FirstOrDefault(f => f.Id == item.Id);
        if (r == null || r.ProductId != item.ProductId)
        {
          return false;
        }
      }

      return true;
    }

  }
}