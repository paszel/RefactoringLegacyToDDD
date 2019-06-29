namespace Sales.Application.CalculateOffer
{
  public class CalculateOfferQuery
  {
    public CalculateOfferQuery(string orderId)
    {
      OrderId = orderId;
    }

    public string OrderId { get; set; }
  }
}