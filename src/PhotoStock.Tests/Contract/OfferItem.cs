using PhotoStock.Controllers;

namespace PhotoStock.Tests.Contract
{
  public class OfferItem
  {
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Id { get; set; }
    public ProductType ProductType { get; set; }
  }
}