using System;
using System.Collections.Generic;
using System.Text;
using Sales.Domain;

namespace Sales.Application.ConfirmOrder
{
  public class ConfirmOrderCommand
  {
    public string OrderId { get; set; }
    public Offer SeenOffer { get; set; }
  }
}
