using System;
using System.Collections.Generic;

namespace PhotoStock.Controllers
{
  public interface IDiscountCalculator
  {
    decimal Calculate(DateTime today, IEnumerable<OfferItemDto> availabeItems);
  }
}