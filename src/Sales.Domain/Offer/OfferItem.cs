using Sales.Domain.Product;

namespace Sales.Domain.Offer
{
  public class OfferItem
  {
    public OfferItem(string id, string name, decimal price, ProductType productType)
    {
      Id = id;
      Name = name;
      Price = price;
      ProductType = productType;
    }

    public ProductType ProductType { get; }
    public string Name { get; }
    public string Id { get; }
    public Money Price { get; }
  }
}