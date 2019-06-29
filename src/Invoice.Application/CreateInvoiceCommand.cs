using Sales.Domain.Offer;

namespace Invoice.Application
{
  public class CreateInvoiceCommand
  {
    public string OrderId { get; }
    public Offer Offer { get; }

    public CreateInvoiceCommand(string orderId, Offer offer)
    {
      OrderId = orderId;
      Offer = offer;
    }
  }
}