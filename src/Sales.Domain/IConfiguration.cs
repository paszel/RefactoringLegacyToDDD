namespace Sales.Domain
{
  public interface IConfiguration
  {
    string this[string name] { get; }
  }
}