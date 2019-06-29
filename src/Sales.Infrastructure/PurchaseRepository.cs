using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Sales.Domain.Purchase;

namespace Sales.Infrastructure
{
  class PurchaseRepository : IPurchaseRepository
  {
    private readonly IConfiguration _configuration;

    public PurchaseRepository(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public void Save(Purchase purchase)
    {
      CreateConnection()
        .Execute("insert OrderSnapshot(orderId, orderData)values(@id,@orderData)",
          new {Id = purchase.OrderId, OrderData = purchase.Data});
    }

    private SqlConnection CreateConnection()
    {
      return new SqlConnection(_configuration["connectionString"]);
    }
  }
}