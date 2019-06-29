using Microsoft.EntityFrameworkCore;
using Sales.Domain;

namespace Sales.Infrastructure
{
  public class OrderContext : DbContext
  {
    private readonly string _connectionString;

    public OrderContext(string connectionString)
    {
      _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Order>(c =>
      {
        // map goeas here
      });
      modelBuilder.Entity<OrderItem>(c =>
      {
        // map goes here
      });
    }

  }
}