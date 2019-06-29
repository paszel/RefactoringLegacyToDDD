namespace Sales.Domain
{
  public interface IProductRepository
  {
    Product Get(string id);
  }
}