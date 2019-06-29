namespace Process.OrderConfirmationProcess
{
  public class CreateShipmentCommand
  {
    public string OrderId { get; }

    public CreateShipmentCommand(string orderId)
    {
      OrderId = orderId;
    }
  }
}