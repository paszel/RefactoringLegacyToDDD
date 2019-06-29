namespace Sales.Domain
{
  public interface IClientRepository
  {
    Client Get(string id);
    void Save(Client client);
  }
}