namespace Sales.Domain.Product
{
  public interface IProductRepository
  {
    Product Get(string id);
  }
}