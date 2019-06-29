using System;
using System.Collections.Generic;
using Sales.Domain.Specification;

namespace Sales.Domain.Discount
{
  public class DateRangeSpecification : CompositeSpecification<IEnumerable<OfferItem>>
  {
    private readonly int _dayFrom;
    private readonly int _monthFrom;
    private readonly int _dayTo;
    private readonly int _monthTo;
    private readonly DateTime _today;

    public DateRangeSpecification(int dayFrom, int monthFrom, int dayTo, int monthTo, DateTime today)
    {
      _dayFrom = dayFrom;
      _monthFrom = monthFrom;
      _dayTo = dayTo;
      _monthTo = monthTo;
      _today = today;
    }

    public override bool IsSatisfiedBy(IEnumerable<OfferItem> candidate)
    {
      return _today.Day >= _dayFrom
             && _today.Month >= _dayFrom
             && _today.Day <= _dayTo
             && _today.Month <= _dayTo;
    }
  }
}