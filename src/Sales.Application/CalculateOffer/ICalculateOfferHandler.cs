using Sales.Domain;
using Sales.Domain.Offer;

namespace Sales.Application.CalculateOffer
{
  public interface ICalculateOfferHandler
  {
    Offer Handle(CalculateOfferQuery query);
  }
}