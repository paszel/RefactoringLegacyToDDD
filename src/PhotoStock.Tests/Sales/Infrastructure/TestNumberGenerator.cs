using Sales.Domain;

namespace PhotoStock.Tests.Sales.Infrastructure
{
  public class TestNumberGenerator : INumberGenerator
  {
    public string GenerateNumber()
    {
      return "123";
    }
  }
}