using System.Collections.Generic;

namespace Sales.Domain
{
  public class Offer
  {
    public Money TotalCost { get; }
    public Money Discount { get; }
    public List<OfferItem> AvailabeItems { get; }
    public List<OfferItem> UnavailableItems { get; }
    public string ClientId { get; }

    public Offer(string clientId, Money totalCost, Money discount, List<OfferItem> availabeItems, List<OfferItem> unavailableItems)
    {
      ClientId = clientId;
      TotalCost = totalCost;
      Discount = discount;
      AvailabeItems = availabeItems;
      UnavailableItems = unavailableItems;
    }
  }
}