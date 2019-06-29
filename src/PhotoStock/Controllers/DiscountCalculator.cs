using System;
using System.Collections.Generic;
using System.Linq;
using PhotoStock.Infrastructure;

namespace PhotoStock.Controllers
{
  public class DiscountCalculator : IDiscountCalculator
  {
    public DiscountCalculator()
    {
    }

    public decimal Calculate(DateTime today, IEnumerable<OfferItemDto> availabeItems)
    {
      // holiday
      if (today.Day >= 1 
          && today.Month >= 7
          && today.Day <= 30
          && today.Month <= 8
          && availabeItems.Any(f => f.ProductType == ProductType.Printed))
      {
        return 10;
      }
          
      // grass day
      if(availabeItems.Any(f => f.Name.Contains("Grass")) && today.Day == 26 && today.Month == 8)
      {
        return 5;
      }

      return 0;
    }
  }
}