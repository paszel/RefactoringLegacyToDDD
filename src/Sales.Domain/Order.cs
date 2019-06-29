using System.Collections.Generic;
using System.Linq;

namespace Sales.Domain
{
  public class Order
  {
    public string Id { get; set; }
    public List<OrderItem> Products { get; set; }
    public OrderStatus Status { get; set; }
    public string ClientId { get; set; }
    public string Number { get; set; }

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

    public Offer CalculateOffer(IDiscountCalculator discountCalculator, IProductRepository productRepository)
    {
      if (Status != OrderStatus.New)
      {
        throw new NotFoundException();
      }

      List<OfferItem> availabeItems = new List<OfferItem>();
      List<OfferItem> unavailableItems = new List<OfferItem>();

      decimal totalCost = 0;
      foreach (var orderItem in Products)
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

      return new Offer(ClientId, totalCost - discount, discount, availabeItems, unavailableItems);
    }
  }
}