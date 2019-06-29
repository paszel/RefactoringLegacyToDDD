using System.IO;
using System.Xml.Serialization;

namespace Sales.Domain.Purchase
{
  public interface IPurchaseFactory
  {
    Purchase Create(string orderId, Offer.Offer offer);
  }

  class PurchaseFactory : IPurchaseFactory
  {
    public Purchase Create(string orderId, Offer.Offer offer)
    {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Offer.Offer));
      using (StringWriter sw = new StringWriter())
      {
        xmlSerializer.Serialize((TextWriter) sw, (object) offer);
        return new Purchase(orderId, sw.GetStringBuilder().ToString());
      }      
    }
  }
}