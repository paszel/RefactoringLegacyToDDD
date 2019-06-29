using System;
using System.Collections.Generic;
using System.Text;
using Sales.Domain;
using Sales.Domain.Offer;

namespace Sales.Application.ConfirmOrder
{
  public class ConfirmOrderCommand
  {
    public ConfirmOrderCommand(string orderId, Offer seenOffer)
    {
      OrderId = orderId;
      SeenOffer = seenOffer;
    }

    public string OrderId { get; }
    public Offer SeenOffer { get; }
  }
}
