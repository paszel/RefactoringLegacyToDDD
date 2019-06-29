using System.Collections.Generic;
using Sales.Domain.Specification;

namespace Sales.Domain.Discount
{
  public class DiscountSpecificationFactory : IDiscountSpecificationFactory
  {
    private readonly IDateTimeProvider _dateTimeProvider;

    public DiscountSpecificationFactory(IDateTimeProvider dateTimeProvider)
    {
      _dateTimeProvider = dateTimeProvider;
    }

    public ISpecification<IEnumerable<OfferItem>> CreateHolidaySpecyfication()
    {
      return new DateRangeSpecification(1, 7, 30, 8,_dateTimeProvider.Today).And(new ProductTypeSpecification(ProductType.Printed));
    }

    public ISpecification<IEnumerable<OfferItem>> CreateGrassDaySpecyfication()
    {
      return new NameContainsSpecification("Grass").And(new DaySpecification(26, 8, _dateTimeProvider.Today));      
    }
  }
}