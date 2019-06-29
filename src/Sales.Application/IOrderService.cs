namespace Sales.Application
{
  public interface IOrderService
  {
    string CreateOrder( string clientId);
    void AddProduct(string orderId, string productId);
  }
}