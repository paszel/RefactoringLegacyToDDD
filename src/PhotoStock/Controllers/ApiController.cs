﻿using System.Net.Mail;
using PhotoStock.BusInterfaces;
using PhotoStock.Infrastructure;
using Processes;
using Sales.Application;
using Sales.Application.AddProduct;
using Sales.Application.CalculateOffer;
using Sales.Application.CreateOrder;
using Sales.Domain;
using Sales.Domain.Discount;

namespace PhotoStock.Controllers
{
  using System;
  using System.Collections.Generic;
  using System.Data.SqlClient;
  using System.IO;
  using System.Linq;
  using System.Xml.Serialization;
  using Dapper;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Configuration;

  [Route("api")]
  [ApiController]
  public class ApiController : ControllerBase
  {
    private readonly IConfiguration _configuration;
    private readonly ISmtpClient _smtpClient;
    private readonly ICalculateOfferHandler _calculateOfferHandler;
    private readonly ICommandBus _commandBus;

    public ApiController(IConfiguration configuration, ISmtpClient smtpClient, 
      ICalculateOfferHandler calculateOfferHandler, ICommandBus commandBus)
    {
      _configuration = configuration;
      _smtpClient = smtpClient;
      _calculateOfferHandler = calculateOfferHandler;
      _commandBus = commandBus;
    }

    [HttpPost("CreateOrder")]
    public ActionResult<string> CreateOrder([FromQuery]string clientId)
    {
      string idf = Guid.NewGuid().ToString();
      _commandBus.Send(new CreateOrderCommand(idf, clientId));
      return Created($"api/Order/{idf}", idf);
    }
    

    [HttpGet("Products")]
    public ActionResult<IEnumerable<ProductDto>> Get([FromQuery]string fromId, [FromQuery]int count)
    {
      if (fromId == null)
      {
        return CreateConnection().Query<ProductDto>("select top " + count + " * from Product").ToArray();
      }
      return CreateConnection().Query<ProductDto>("select top " + count + " * from Product where id > @fromId", new { fromId }).ToArray();
    }


    [HttpPost("Order/{id}/AddProduct")]
    public ActionResult AddProductToOrder([FromRoute] string id, [FromQuery] string productId)
    {
      try
      {
        _commandBus.Send(new AddProductCommand(id, productId));
      }
      catch (AlreadyExistsException)
      {
        return BadRequest();
      }
      catch (NotFoundException)
      {
        return NotFound();
      }

      return Ok();
    }

    [HttpGet("Order/{id}/CalculateOffer")]
    public ActionResult<OfferDto> CalculateOffer(string id)
    {
      var result = _calculateOfferHandler.Handle(new CalculateOfferQuery(id));
      return new OfferDto()
      {
        AvailabeItems = result.AvailabeItems.Select(f =>
            new OfferItemDto() {Id = f.Id, Price = f.Price, Name = f.Name, ProductType = (ProductType) f.ProductType}).ToList(),
        UnavailableItems = result.UnavailableItems.Select(f =>
            new OfferItemDto() {Id = f.Id, Price = f.Price, Name = f.Name, ProductType = (ProductType) f.ProductType}).ToList(),
        Discount = result.Discount,
        TotalCost = result.TotalCost,
        ClientId = result.ClientId
      };
    }

    [HttpPost("Order/{orderId}/Confirm")]
    public ActionResult Confirm([FromRoute]string orderId, [FromBody] OfferDto seenOffer)
    {
      _commandBus.Send(new ProcessOrderConfirmationCommand(orderId, new Offer(
        seenOffer.ClientId, 
        seenOffer.TotalCost,
        seenOffer.Discount,
        seenOffer.AvailabeItems.Select(f=>new OfferItem(f.Id,f.Name,f.Price, (Sales.Domain.Product.ProductType)f.ProductType,f.Discount)).ToList(),
        seenOffer.UnavailableItems.Select(f => new OfferItem(f.Id, f.Name, f.Price, (Sales.Domain.Product.ProductType)f.ProductType, f.Discount)).ToList())));

      ConfirmOrder(orderId, seenOffer);

      CreateShipment(orderId);

      SendConfirmation(orderId);

      CreateInvoice(orderId, seenOffer);
      
      return Ok();
    }

    private void CreateInvoice(string orderId, OfferDto seenOffer)
    {
      InvoiceType invoiceType = CreateConnection().QueryFirst<InvoiceType>("select c.invoiceType from Client c where id = @clientId", new { seenOffer.ClientId });

      decimal net = seenOffer.TotalCost;
      decimal tax = seenOffer.AvailabeItems.Sum(f => CalculateTax(f.ProductType, f.Price));

      string invoiceNumber = GenerateNumber(invoiceType);
      CreateConnection().Execute("insert into Invoice(orderId, number, net_amount, tax_amount)values(@id, @number, @net, @tax)", new { id = orderId, number = invoiceNumber, net, tax });

      foreach (var oProduct in seenOffer.AvailabeItems)
      {
        tax = CalculateTax(oProduct.ProductType, oProduct.Price);
        CreateConnection()
          .Execute("insert into InvoiceItem(invoiceId, productName, net_amount, tax_amount)values(@invoiceId, @productName, @net, @tax)",
            new { invoiceId = orderId, productName = oProduct.Name, net, tax });
      }

    }

    private void SendConfirmation(string orderId)
    {
      var order = GetOrderInternal(orderId);

      string email = CreateConnection().QueryFirst<string>("select email from client where id = @id", new { id = order.ClientId });

      _smtpClient.Send("no-reply@photostock.com", email, "Order confirmation", $"your order (number: {order.Number}) has been queued for shipment");

    }

    private void CreateShipment(string orderId)
    {
      CreateConnection().Execute("insert into Shipment(orderId, status)values(@id, 1)", new { id = orderId });
    }

    private void ConfirmOrder(string orderId, OfferDto seenOffer)
    {
      
      return Ok();
    }

    [HttpGet("Shipments/{orderId}")]
    public ActionResult<ShipmentDto> GetShipment([FromRoute] string orderId)
    {
      return CreateConnection().QueryFirst<ShipmentDto>("select * from shipment where orderId = @orderId", new { orderId });
    }

    [HttpPut("Shipments/{orderId}/Shipped")]
    public ActionResult Shipped([FromRoute] string orderId)
    {
      CreateConnection().Execute("update shipment set status = 2 where orderId = @orderId", new { orderId });

      OrderDto order = GetOrderInternal(orderId);

      string email = CreateConnection().QueryFirst<string>("select email from client where id = @id", new { id = order.ClientId });

      _smtpClient.Send("no-reply@photostock.com", email, "Shipment confirmation", $"your order number : {order.Number} ha been shipped");

      return Ok();
    }

    #region private
    private OrderDto GetOrderInternal(string id)
    {
      OrderDto o = CreateConnection().QueryFirst<OrderDto>("select * from [order] where id = @id", new { id });
      o.Products = CreateConnection().Query<ProductDto>("select p.* from product p join OrderItem p2o on p.id = p2o.productId where p2o.orderId = @id", new { id }).ToList();
      return o;
    }

    private SqlConnection CreateConnection()
    {
      return new SqlConnection(_configuration["connectionString"]);
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
    #endregion
  }
}
