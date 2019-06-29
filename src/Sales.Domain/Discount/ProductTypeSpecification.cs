using System.Collections.Generic;
using System.Linq;
using Sales.Domain.Product;
using Sales.Domain.Specification;

namespace Sales.Domain.Discount
{
  internal class ProductTypeSpecification : CompositeSpecification<IEnumerable<OfferItem>>
  {
    private readonly ProductType _productType;

    public ProductTypeSpecification(ProductType productType)
    {
      _productType = productType;
    }

    public override bool IsSatisfiedBy(IEnumerable<OfferItem> candidate)
    {
      return candidate.Any(f => f.ProductType == _productType);
    }
  }
}