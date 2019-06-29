using System.Collections.Generic;
using System.Linq;
using Sales.Domain.Offer;
using Sales.Domain.Specification;

namespace Sales.Domain.Discount
{
  internal class NameContainsSpecification : CompositeSpecification<IEnumerable<OfferItem>>
  {
    private string _name;

    public NameContainsSpecification(string name)
    {
      _name = name;
    }

    public override bool IsSatisfiedBy(IEnumerable<OfferItem> candidate)
    {
      return candidate.Any(f => f.Name.Contains(_name));
    }    
  }
}