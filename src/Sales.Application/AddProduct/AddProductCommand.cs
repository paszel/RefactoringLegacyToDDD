namespace Sales.Application.AddProduct
{
  public class AddProductCommand
  {
    public AddProductCommand(string orderId, string productId)
    {
      OrderId = orderId;
      ProductId = productId;
    }

    public string OrderId { get;  }
    public string ProductId { get; }
  }
}