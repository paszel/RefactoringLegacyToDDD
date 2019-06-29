using System.Collections.Generic;
using System.Linq;

namespace Sales.Domain.Offer
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

    public bool SameAs(Offer offer)
    {
      if (offer.AvailabeItems.Count != AvailabeItems.Count)
      {
        return false;
      }

      if (offer.TotalCost != TotalCost || offer.ClientId != ClientId)
      {
        return false;
      }
      foreach (OfferItem item in offer.AvailabeItems)
      {
        var offerProducts = AvailabeItems.FirstOrDefault(f => f.Id == item.Id);
        if (offerProducts == null)
        {
          return false;
        }
        if (item.Price != offerProducts.Price)
        {
          return false;
        }
      }

      return true;
    }
  }
}