using System.Collections.Generic;
using Sales.Domain;
using Sales.Domain.Specification;

namespace PhotoStock.Tests.Sales.Domain
{
  public class TrueSpecification : CompositeSpecification<IEnumerable<OfferItem>>
  {
    public override bool IsSatisfiedBy(IEnumerable<OfferItem> candidate)
    {
      return true;
    }
  }
}
