using Sales.Domain;

namespace Processes
{
  public class ProcessOrderConfirmationCommand
  {
    public string OrderId { get; }
    public Offer SeenOffer { get; }

    public ProcessOrderConfirmationCommand(string orderId, Offer seenOffer)
    {
      OrderId = orderId;
      SeenOffer = seenOffer;
    }
  }
}