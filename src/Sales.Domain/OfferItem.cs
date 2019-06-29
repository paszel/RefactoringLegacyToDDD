namespace Sales.Domain
{
  public class OfferItem
  {
    public ProductType ProductType { get; set; }
    public string Name { get; set; }
    public string Id { get; set; }
    public Money Price { get; set; }
  }
}