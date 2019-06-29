using System.Collections.Generic;
using System.Threading.Tasks;
using NUnitTestProject1;
using RestEase;

namespace PhotoStock.Tests.Contract
{
  public interface IApi
  {
    [Post("/api/CreateOrder")]
    Task<string> CreateOrder([Query]string clientId);

    [Get("api/products")]
    Task<IEnumerable<Product>> GetProducts([Query]string fromId, [Query]int count);

    [Post("api/Order/{orderId}/AddProduct")]
    Task AddProductToOrder([Path]string orderId, [Query]string productId);

    [Get("/api/Order/{orderId}/CalculateOffer")]
    Task<Offer> CalculateOffer([Path]string orderId);

    [Post("/api/Order/{orderId}/Confirm")]
    Task Confirm([Path]string orderId, [Body] Offer seenOrder);

    [Get("api/shipments/{orderId}")]
    Task<Shipment> GetShipment([Path]string orderId);

    [Put("api/shipments/{orderId}/shipped")]
    Task Shipped([Path]string orderId);    
  }
}