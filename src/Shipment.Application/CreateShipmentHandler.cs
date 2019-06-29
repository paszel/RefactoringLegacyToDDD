using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using PhotoStock.BusInterfaces;

namespace Shipment.Application
{
  public class CreateShipmentHandler : ICommandHandler<CreateShipmentCommand>
  {
    private readonly IConfiguration _configuration;

    public CreateShipmentHandler(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public void Handle(CreateShipmentCommand command)
    {
      CreateConnection().Execute("insert into Shipment(orderId, status)values(@id, 1)", new { id = command.OrderId });
    }

    private SqlConnection CreateConnection()
    {
      return new SqlConnection(_configuration["connectionString"]);
    }
  }
}