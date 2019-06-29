using Sales.Domain.Offer;

namespace Sales.Domain
{
  public interface IOrderFactory
  {
    Order Create(string id, string clientId);
  }
}