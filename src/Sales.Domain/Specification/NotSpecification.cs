namespace Sales.Domain.Specification
{
  public class NotSpecification<T> : CompositeSpecification<T>
  {
    private ISpecification<T> _wrapped;

    public NotSpecification(ISpecification<T> wrapped)
    {
      _wrapped = wrapped;
    }

    public override bool IsSatisfiedBy(T candidate)
    {
      return !_wrapped.IsSatisfiedBy(candidate);
    }
  }
}