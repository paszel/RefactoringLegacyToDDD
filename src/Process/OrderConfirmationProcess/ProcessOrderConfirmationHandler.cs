using System.Data.SqlClient;
using Dapper;
using Invoice.Application;
using PhotoStock.BusInterfaces;
using Sales.Application.ConfirmOrder;
using Sales.Domain;
using Sales.Domain.Offer;

namespace Process.OrderConfirmationProcess
{
  public class ProcessOrderConfirmationHandler : ICommandHandler<Process.OrderConfirmationProcess.ProcessOrderConfirmationCommand>
  {
    private readonly ICommandBus _commandBus;
    private readonly ISmtpClient _smtpClient;
    private readonly IConfiguration _configuration;

    public ProcessOrderConfirmationHandler(ICommandBus commandBus, ISmtpClient smtpClient, IConfiguration configuration)
    {
      _commandBus = commandBus;
      _smtpClient = smtpClient;
      _configuration = configuration;
    }

    public void Handle(Process.OrderConfirmationProcess.ProcessOrderConfirmationCommand command)
    {
      _commandBus.Send(new ConfirmOrderCommand(command.OrderId, command.SeenOffer));
      _commandBus.Send(new CreateShipmentCommand(command.OrderId));

      SendConfirmation(command.SeenOffer);

      _commandBus.Send(new CreateInvoiceCommand(command.OrderId, command.SeenOffer));
    }

    private void SendConfirmation(Offer offer)
    {
      string email = CreateConnection().QueryFirst<string>("select email from client where id = @id", new { id = offer.ClientId });

      _smtpClient.Send("no-reply@photostock.com", email, "Order confirmation", $"your order has been queued for shipment");

    }

    private SqlConnection CreateConnection()
    {
      return new SqlConnection(_configuration["connectionString"]);
    }
  }

}
