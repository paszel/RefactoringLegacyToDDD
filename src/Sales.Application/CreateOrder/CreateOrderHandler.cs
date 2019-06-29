using System.Data.SqlClient;
using Dapper;
using Sales.Domain;

namespace Sales.Application
{
  public class CreateOrderHandler : ICommandHandler<CreateOrderCommand>
  {
    private readonly IConfiguration _configuration;
    private readonly IOrderFactory _orderFactory;

    public CreateOrderHandler(IConfiguration configuration, IOrderFactory orderFactory)
    {
      _configuration = configuration;
      _orderFactory = orderFactory;
    }

    public void Handle(CreateOrderCommand command)
    {
      Order o = _orderFactory.Create(command.Id, command.ClientId);
      
      CreateConnection().Execute("INSERT INTO [Order](id,number,clientId) VALUES(@id,@number,@clientId)", o);
    }

    private SqlConnection CreateConnection()
    {
      return new SqlConnection(_configuration["connectionString"]);
    }
  }
}