using System.Data.SqlClient;
using Dapper;
using Sales.Domain;

namespace Sales.Infrastructure
{
  class ProductRepository : IProductRepository
  {
    private readonly IConfiguration _configuration;

    public ProductRepository(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public Product Get(string id)
    {
      return CreateConnection().QueryFirst<Product>("select * from product where id = @id", new { id });
    }

    private SqlConnection CreateConnection()
    {
      return new SqlConnection(_configuration["connectionString"]);
    }
  }
}