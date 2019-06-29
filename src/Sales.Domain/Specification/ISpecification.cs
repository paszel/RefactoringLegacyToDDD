namespace Sales.Domain.Specification
{
  public interface ISpecification<T>
  {
    bool IsSatisfiedBy(T candidate);

    ISpecification<T> And(ISpecification<T> other);

    ISpecification<T> Or(ISpecification<T> other);

    ISpecification<T> Conjunction(params ISpecification<T>[] others);

    ISpecification<T> Disjunction(params ISpecification<T>[] others);

    ISpecification<T> Not();
  }
}