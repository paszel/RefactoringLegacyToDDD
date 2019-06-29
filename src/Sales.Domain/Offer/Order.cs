using System;
using System.Collections.Generic;
using System.Linq;
using Sales.Domain.Discount;
using Sales.Domain.Product;

namespace Sales.Domain.Offer
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

    public Domain.Offer.Offer CalculateOffer(IDiscountCalculator discountCalculator, IProductRepository productRepository)
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
        Product.Product product = productRepository.Get(orderItem.ProductId);
        if (product.Aviable)
        {
          OfferItem offerItem = new OfferItem(product.Id,product.Name,product.Price,product.ProductType);

          availabeItems.Add(offerItem);
          totalCost += offerItem.Price;
        }
        else
        {
          OfferItem offerItem = new OfferItem(product.Id, product.Name, product.Price, product.ProductType);

          unavailableItems.Add(offerItem);
        }
      }

      decimal discount = discountCalculator.Calculate(availabeItems);

      return new Domain.Offer.Offer(_clientId, totalCost - discount, discount, availabeItems, unavailableItems);
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

    public void Confirm(Domain.Offer.Offer seenOffer, IDiscountCalculator discountCalculator,
      IProductRepository productRepository)
    {
      Domain.Offer.Offer current = CalculateOffer(discountCalculator, productRepository);

      if (current.SameAs(seenOffer))
      {
        throw new InvalidOperationException();
      }

      _status = OrderStatus.Confirmed;
    }
  }
}