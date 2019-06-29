using System.Collections.Generic;

namespace Sales.Domain.Specification
{
  public class ConjunctionSpecification<T> : CompositeSpecification<T>
  {
    private List<ISpecification<T>> _conjunction;

    public ConjunctionSpecification(List<ISpecification<T>> conjunction)
    {
      _conjunction = conjunction;
    }

    public override bool IsSatisfiedBy(T candidate)
    {
      foreach(ISpecification<T> spec in _conjunction)
      {
        if (!spec.IsSatisfiedBy(candidate))
          return false;
      }

      return true;
    }
  }
}