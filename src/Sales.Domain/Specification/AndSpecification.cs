namespace Sales.Domain.Specification
{
  public class AndSpecification<T> : CompositeSpecification<T>
  {
    private ISpecification<T> _a;
    private ISpecification<T> _b;

    public AndSpecification(ISpecification<T> a, ISpecification<T> b)
    {
      _a = a;
      _b = b;
    }

    public override bool IsSatisfiedBy(T candidate)
    {
      return _a.IsSatisfiedBy(candidate) && _b.IsSatisfiedBy(candidate);
    }
  }
}