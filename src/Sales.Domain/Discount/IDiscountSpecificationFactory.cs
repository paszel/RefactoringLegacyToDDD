using System.Collections.Generic;
using Sales.Domain.Specification;

namespace Sales.Domain.Discount
{
  public interface IDiscountSpecificationFactory
  {
    ISpecification<IEnumerable<OfferItem>> CreateHolidaySpecyfication();
    ISpecification<IEnumerable<OfferItem>> CreateGrassDaySpecyfication();
  }
}