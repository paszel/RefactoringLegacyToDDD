namespace Sales.Domain.Purchase
{
  public class Purchase
  {
    public string OrderId { get; }
    public string Data { get; }

    public Purchase(string orderId, string data)
    {
      OrderId = orderId;
      Data = data;
    }
  }
}