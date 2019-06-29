using System;
using System.Collections.Generic;
using System.Text;
using Sales.Domain.Offer;

namespace Sales.Domain
{
  public interface IOrderRepository 
  {
    Order Get(string orderId);
    void Save(Order order);
  }
}
