namespace PhotoStock.Controllers
{
  public class OfferItemDto
  {
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Id { get; set; }
    public ProductType ProductType { get; set; }
    public decimal Discount { get; set; }
  }
}