using System;
using System.Collections.Generic;
using Sales.Domain.Offer;
using Sales.Domain.Specification;

namespace Sales.Domain.Discount
{
  internal class DaySpecification : CompositeSpecification<IEnumerable<OfferItem>>
  {
    private readonly int _dayOfMonth;
    private readonly int _month;
    private readonly DateTime _today;
    
    public DaySpecification(int dayOfMonth, int month, DateTime today)
    {
      _dayOfMonth = dayOfMonth;
      _month = month;
      _today = today;    
    }

    public override bool IsSatisfiedBy(IEnumerable<OfferItem> candidate)
    {
      return _today.Day == _dayOfMonth && _today.Month == _month;
    }
  }
}