using System.Linq;
using Microsoft.EntityFrameworkCore;
using PhotoStock.Infrastructure.DDD;
using Sales.Domain;

namespace PhotoStock.Infrastructure.DDD
{
  class GenericRepository<T> : IGenericRepository<T> where T : AggregateRoot
  {
    protected readonly GenericContext<T> _context;
    protected readonly IEventBus _eventBus;

    public GenericRepository(GenericContext<T> context, IEventBus eventBus)
    {
      _context = context;
      _eventBus = eventBus;
    }

    public T Get(string id)
    {
      var item = _context.Items.First(f => f.Id == id);
      (item as IDependencySetter).SetEventPublisher(_eventBus);
      return item;
    }

    public virtual void Save(T obj)
    {
      if (_context.Entry(obj).State == EntityState.Detached)
      {
        _context.Items.Add(obj);
      }

      _context.SaveChanges();
    }
  }
}