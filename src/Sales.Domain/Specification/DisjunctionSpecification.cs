using System.Collections.Generic;

namespace Sales.Domain.Specification
{
  public class DisjunctionSpecification<T> : CompositeSpecification<T>
  {
    private List<ISpecification<T>> _disjunction;

    public DisjunctionSpecification(List<ISpecification<T>> disjunction)
    {
      _disjunction = disjunction;
    }

    public override bool IsSatisfiedBy(T candidate)
    {
      foreach(ISpecification<T> spec in _disjunction)
      {
        if (spec.IsSatisfiedBy(candidate))
          return true;
      }

      return false;
    }
  }
}