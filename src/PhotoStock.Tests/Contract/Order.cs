namespace PhotoStock.Tests.Contract
{
  public class Order
  {
    public Product[] Products { get; set; }

    public OrderStatus Status { get; set; }
    public decimal DiscountPrice { get; set; }
  }
}