namespace PhotoStock.Controllers
{
  public class ProductDto
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ProductType ProductType { get; set; }
    public bool Aviable { get; set; }
  }
}