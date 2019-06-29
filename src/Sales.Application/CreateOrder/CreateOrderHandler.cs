using System.Data.SqlClient;
using Dapper;
using Sales.Domain;

namespace Sales.Application
{
  public class CreateOrderHandler : ICommandHandler<CreateOrderCommand>
  {
    private readonly IConfiguration _configuration;
    private readonly INumberGenerator _numberGenerator;

    public CreateOrderHandler(IConfiguration configuration, INumberGenerator numberGenerator)
    {
      _configuration = configuration;
      _numberGenerator = numberGenerator;
    }

    public void Handle(CreateOrderCommand command)
    {
      string number = _numberGenerator.GenerateNumber();

      Order o = new Order() { ClientId = command.ClientId, Id = command.Id, Number = number, Status = OrderStatus.New };

      CreateConnection().Execute("INSERT INTO [Order](id,number,clientId) VALUES(@id,@number,@clientId)", o);
    }

    private SqlConnection CreateConnection()
    {
      return new SqlConnection(_configuration["connectionString"]);
    }
  }
}