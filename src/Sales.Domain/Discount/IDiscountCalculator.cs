using System.Collections.Generic;

namespace Sales.Domain.Discount
{
  public interface IDiscountCalculator
  {
    decimal Calculate(IEnumerable<OfferItem> items);
  }
}