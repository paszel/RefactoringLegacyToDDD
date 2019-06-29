using System.Collections.Generic;
using System.Linq;

namespace Sales.Domain
{
  public class DiscountCalculator : IDiscountCalculator
  {
    private readonly IDateTimeProvider _dateTimeProvider;

    public DiscountCalculator(IDateTimeProvider dateTimeProvider)
    {
      _dateTimeProvider = dateTimeProvider;
    }

    public decimal Calculate(IEnumerable<OfferItem> availabeItems)
    {
      decimal discount = 0;
      // holiday
      if (_dateTimeProvider.Today.Day >= 1
          && _dateTimeProvider.Today.Month >= 7
          && _dateTimeProvider.Today.Day <= 30
          && _dateTimeProvider.Today.Month <= 8
          && availabeItems.Any(f => f.ProductType == ProductType.Printed))
      {
        discount = 10;
      }

      // grass day
      if (availabeItems.Any(f => f.Name.Contains("Grass")) && _dateTimeProvider.Today.Day == 26 && _dateTimeProvider.Today.Month == 8)
      {
        discount = 5;
      }


      return discount;
    }
  }
}