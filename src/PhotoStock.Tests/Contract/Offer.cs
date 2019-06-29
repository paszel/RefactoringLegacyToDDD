using System.Collections.Generic;

namespace PhotoStock.Tests.Contract
{
  public class Offer 
  {
    public decimal TotalCost { get; set; }
    public string ClientId { get; set; }
    public List<OfferItem> AvailabeItems { get; set; }
    public List<OfferItem> UnavailableItems { get; set; }
  }
}