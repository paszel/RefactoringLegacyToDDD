using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using PhotoStock.BusInterfaces;
using PhotoStock.Controllers;
using Sales.Domain.Product;

namespace Invoice.Application
{
  public class CreateInvoiceHandler : ICommandHandler<CreateInvoiceCommand>
  {
    private readonly IConfiguration _configuration;

    public CreateInvoiceHandler(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public void Handle(CreateInvoiceCommand command)
    {
      var seenOffer = command.Offer;

      InvoiceType invoiceType = CreateConnection().QueryFirst<InvoiceType>("select c.invoiceType from Client c where id = @clientId", new { seenOffer.ClientId });

      decimal net = seenOffer.TotalCost;
      decimal tax = seenOffer.AvailabeItems.Sum(f => CalculateTax(f.ProductType, f.Price));

      string invoiceNumber = GenerateNumber(invoiceType);
      CreateConnection().Execute("insert into Invoice(orderId, number, net_amount, tax_amount)values(@id, @number, @net, @tax)", new { id = command.OrderId, number = invoiceNumber, net, tax });

      foreach (var oProduct in seenOffer.AvailabeItems)
      {
        tax = CalculateTax(oProduct.ProductType, oProduct.Price);
        CreateConnection()
          .Execute("insert into InvoiceItem(invoiceId, productName, net_amount, tax_amount)values(@invoiceId, @productName, @net, @tax)",
            new { invoiceId = command.OrderId, productName = oProduct.Name, net, tax });
      }
    }

    private string GenerateNumber(InvoiceType invoiceType)
    {
      int nr = CreateConnection().QueryFirst<int>(
        "select number from LastInvoiceNumber where invoiceType = @invoiceType;update LastInvoiceNumber set number = number + 1 where invoiceType = @invoiceType",
        new { invoiceType });

      return $"FV {DateTime.Today.Year}/{nr}";
    }


    public decimal CalculateTax(ProductType productType, decimal net)
    {
      decimal ratio;

      switch (productType)
      {
        case ProductType.Printed:
          ratio = 0.05M;
          break;

        case ProductType.Electronic:
          ratio = 0.23M;
          break;

        default:
          throw new ArgumentOutOfRangeException(productType + " not Handled");
      }

      return net * ratio;
    }

    private SqlConnection CreateConnection()
    {
      return new SqlConnection(_configuration["connectionString"]);
    }

  }
}