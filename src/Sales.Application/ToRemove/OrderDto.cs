using System.Collections.Generic;

namespace Sales.Application.ToRemove
{
  internal class OrderDto
  {
    public string Id { get; set; }
    public List<ProductDto> Products { get; set; }
    public OrderStatus Status { get; set; }
    public string ClientId { get; set; }
    public string Number { get; set; }
  }
}