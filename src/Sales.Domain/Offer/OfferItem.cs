using Sales.Domain.Product;

namespace Sales.Domain
{
  public class OfferItem
  {
    public OfferItem(string id, string name, decimal price, ProductType productType, decimal discount)
    {
      Id = id;
      Name = name;
      Price = price;
      ProductType = productType;
      Discount = discount;
    }

    public ProductType ProductType { get; }
    public decimal Discount { get; }
    public string Name { get; }
    public string Id { get; }
    public Money Price { get; }
  }
}