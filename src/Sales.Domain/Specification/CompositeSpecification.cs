using System.Collections.Generic;

namespace Sales.Domain.Specification
{
  public abstract class CompositeSpecification<T> : ISpecification<T>
  {
    public abstract bool IsSatisfiedBy(T candidate);

    public ISpecification<T> And(ISpecification<T> other)
    {
      return new AndSpecification<T>(this, other);
    }

    public ISpecification<T> Or(ISpecification<T> other)
    {
      return new OrSpecification<T>(this, other);
    }

    public ISpecification<T> Not()
    {
      return new NotSpecification<T>(this);
    }

    public ISpecification<T> Conjunction(params ISpecification<T>[] others)
    {
      List<ISpecification<T>> list = new List<ISpecification<T>>(others);
      list.Add(this);
      return new ConjunctionSpecification<T>(list);
    }

    public ISpecification<T> Disjunction(params ISpecification<T>[] others)
    {
      List<ISpecification<T>> list = new List<ISpecification<T>>(others);
      list.Add(this);
      return new DisjunctionSpecification<T>(list);
    }
  }
}
