namespace Sales.Domain.Purchase
{
  public interface IPurchaseRepository
  {
    void Save(Purchase purchase);
  }
}