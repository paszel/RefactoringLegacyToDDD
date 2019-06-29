using System.Collections.Generic;

namespace Sales.Domain
{
  public interface IDiscountCalculator
  {
    decimal Calculate(IEnumerable<OfferItem> availabeItems);
  }
}