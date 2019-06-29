using System.Collections.Generic;

namespace PhotoStock.Controllers
{
  public class OfferDto
  {
    public List<OfferItemDto> AvailabeItems { get; set; }
    public List<OfferItemDto> UnavailableItems { get; set; }

    public string ClientId { get; set; }
    public decimal TotalCost { get; set; }

    public decimal Discount { get; set; }

    //serialization
    public OfferDto()
    {
    }
    public OfferDto(string clientId, decimal totalCost, decimal discount, List<OfferItemDto> availabeItems, List<OfferItemDto> unavailableItems)
    {
      ClientId = clientId;
      TotalCost = totalCost;
      Discount = discount;
      AvailabeItems = availabeItems;
      UnavailableItems = unavailableItems;
    }
  }
}