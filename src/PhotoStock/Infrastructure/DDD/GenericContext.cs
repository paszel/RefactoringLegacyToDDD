using Microsoft.EntityFrameworkCore;

namespace PhotoStock.Infrastructure.DDD
{
  public class GenericContext<T> : DbContext where T : class
  {
    private readonly string _connectionString;
    public DbSet<T> Items { get; set; }
    

    public GenericContext(string connectionString)
    {
      _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(_connectionString);
    }
  }
}