using System.Collections.Generic;
using Sales.Domain.Specification;

namespace Sales.Domain.Discount
{
  public class DiscountCalculator : IDiscountCalculator
  {
    private readonly IDiscountSpecificationFactory _discountSpecificationFactory;

    public DiscountCalculator(IDiscountSpecificationFactory discountSpecificationFactory)
    {
      _discountSpecificationFactory = discountSpecificationFactory;
    }

    public decimal Calculate(IEnumerable<OfferItem> items)
    {
      ISpecification<IEnumerable<OfferItem>> holidaySpecification = _discountSpecificationFactory.CreateHolidaySpecyfication();
      ISpecification<IEnumerable<OfferItem>> grassDaySpecification = _discountSpecificationFactory.CreateGrassDaySpecyfication();

      if (holidaySpecification.IsSatisfiedBy(items))
      {
        return 10;
      }

      if (grassDaySpecification.IsSatisfiedBy(items))
      {
        return 5;
      }

      return 0;
    }
  }
}