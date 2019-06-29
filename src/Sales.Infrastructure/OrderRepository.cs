using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Sales.Domain;

namespace Sales.Infrastructure
{
  public class OrderRepository : IOrderRepository
  {
    private readonly OrderContext _context;
    private readonly IEventPublisher _eventBus;

    public OrderRepository(OrderContext context, IEventPublisher eventBus)
    {
      _context = context;
      _eventBus = eventBus;
    }

    public Order Get(string orderId)
    {
      var item = _context.Set<Order>().Include("_products").SingleOrDefault(f => f.Id == orderId);
      (item as IDependencySetter).SetEventPublisher(_eventBus);
      return item;
    }

    public void Save(Order order)
    {
      if (_context.Entry(order).State == EntityState.Detached)
      {
        _context.Set<Order>().Add(order);
      }
      else
      {
        order.IncreaseVersion();
      }

      _context.SaveChanges();
    }
  }
}
