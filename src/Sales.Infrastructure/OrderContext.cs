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
        c.ToTable("Order");
        c.HasKey("_id");
        c.Property("_id").HasColumnName("id");
        c.Property("_status").HasColumnName("status");
        c.Property("_number").HasColumnName("number");
        c.Property("_clientId").HasColumnName("clientId");
        c.HasMany<OrderItem>("_products");
      });
      modelBuilder.Entity<OrderItem>(c =>
      {
        c.ToTable("OrderItem");
        c.HasOne(x => x.Order).WithMany("_products").HasForeignKey("OrderId");
        c.Property<string>(f => f.ProductId);
      });
    }

  }
}