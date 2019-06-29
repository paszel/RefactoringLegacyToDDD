using System.Collections.Generic;
using Sales.Domain.Offer;

namespace Sales.Domain.Discount
{
  public interface IDiscountCalculator
  {
    decimal Calculate(IEnumerable<OfferItem> items);
  }
}