namespace Sales.Domain.Offer
{
  public class OrderItem
  {
    public int Id { get; set; }
    public Order Order { get; set; }

    public string ProductId { get; set; }
  }
}