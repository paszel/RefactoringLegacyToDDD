namespace Sales.Domain.Product
{
  public class Product
  {
    public bool CanBeSold()
    {
      return Available;
    }

    public bool Available { get; set; }
    public Money Price { get; set; }
    public bool Aviable { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
  }
}