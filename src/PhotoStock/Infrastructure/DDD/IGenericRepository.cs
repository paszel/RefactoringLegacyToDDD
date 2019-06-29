namespace PhotoStock.Infrastructure.DDD
{
  public interface IGenericRepository<T>
  {
    T Get(string id);
    void Save(T obj);
  }
}