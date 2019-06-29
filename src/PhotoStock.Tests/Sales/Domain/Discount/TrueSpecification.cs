using System.Collections.Generic;
using Sales.Domain;
using Sales.Domain.Offer;
using Sales.Domain.Specification;

namespace PhotoStock.Tests.Sales.Domain.Discount
{
  public class TrueSpecification : CompositeSpecification<IEnumerable<OfferItem>>
  {
    public override bool IsSatisfiedBy(IEnumerable<OfferItem> candidate)
    {
      return true;
    }
  }
}
