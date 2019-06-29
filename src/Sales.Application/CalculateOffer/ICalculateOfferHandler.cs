using Sales.Domain;

namespace Sales.Application.CalculateOffer
{
  public interface ICalculateOfferHandler
  {
    Offer Handle(CalculateOfferQuery query);
  }
}