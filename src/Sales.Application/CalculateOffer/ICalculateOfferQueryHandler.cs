using Sales.Domain;

namespace Sales.Application.CalculateOffer
{
  public interface ICalculateOfferQueryHandler
  {
    Offer Handle(CalculateOfferQuery query);
  }
}