using System.Net.Mail;
using PhotoStock.Infrastructure;

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
    private readonly IDateTimeProvider _dateTimeProvider;

    public ApiController(IConfiguration configuration, ISmtpClient smtpClient, IDateTimeProvider dateTimeProvider)
    {
      _configuration = configuration;
      _smtpClient = smtpClient;
      _dateTimeProvider = dateTimeProvider;
    }

    [HttpPost("CreateOrder")]
    public ActionResult<string> CreateOrder([FromQuery]string clientId)
    {
      string number = GenerateNumber();
      string id = Guid.NewGuid().ToString();
      CreateConnection().Execute("INSERT INTO [Order](id,number,clientId) VALUES(@id,@number,@clientId)",
        new { id, number, clientId });

      return Created($"api/Order/{id}", id);
    }

    private string GenerateNumber()
    {
      if (_configuration["Environment"] != "PROD")
      {
        return _configuration["Environment"] + "/Or/" + _dateTimeProvider.Now();
      }

      return "Or/ " + _dateTimeProvider.Now();
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
      OrderDto order = GetOrderInternal(id);
      if (order.Status != OrderStatus.New)
      {
        return NotFound();
      }

      if (order.Products.FirstOrDefault(f => f.Id == productId) != null)
      {
        return BadRequest("Product already exists");
      }

      CreateConnection().Execute(
        "insert into OrderItem(orderId, productId)values(@orderId,@productId)",
        new { orderId = id, productId });

      return Ok();
    }

    [HttpGet("Order/{id}/CalculateOffer")]
    public ActionResult<OfferDto> CalculateOffer(string id)
    {
      OrderDto o = GetOrderInternal(id);

      if (o.Status != OrderStatus.New)
      {
        return NotFound();
      }

      List<OfferItemDto> availabeItems = new List<OfferItemDto>();
      List<OfferItemDto> unavailableItems = new List<OfferItemDto>();

      decimal totalCost = 0;
      foreach (ProductDto product in o.Products)
      {
        if (product.Aviable)
        {
          OfferItemDto offerItem = new OfferItemDto()
          {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
          };

          availabeItems.Add(offerItem);
          totalCost += offerItem.Price;
        }
        else
        {
          OfferItemDto offerItem = new OfferItemDto { Id = product.Id, Name = product.Name, Price = product.Price };

          unavailableItems.Add(offerItem);
        }
      }

      decimal discount = 0;
      // holiday
      if (_dateTimeProvider.Today.Day >= 1 
          && _dateTimeProvider.Today.Month >= 7
          && _dateTimeProvider.Today.Day <= 30
          && _dateTimeProvider.Today.Month <= 8
          && availabeItems.Any(f => f.ProductType == ProductType.Printed))
      {
        discount = 10;
      }
          
      // grass day
      if(availabeItems.Any(f => f.Name.Contains("Grass")) && _dateTimeProvider.Today.Day == 26 && _dateTimeProvider.Today.Month == 8)
      {
        discount = 5;
      }

      return new OfferDto(o.ClientId, totalCost-discount, discount, availabeItems, unavailableItems);
    }

    [HttpPost("Order/{orderId}/Confirm")]
    public ActionResult Confirm([FromRoute]string orderId, [FromBody] OfferDto seenOffer)
    {
      OfferDto offer = CalculateOffer(orderId).Value;

      if (offer.AvailabeItems.Count != seenOffer.AvailabeItems.Count)
      {
        return BadRequest("Order changed");
      }

      if (offer.TotalCost != seenOffer.TotalCost || offer.ClientId != seenOffer.ClientId)
      {
        return BadRequest("Order changed");
      }
      foreach (OfferItemDto item in offer.AvailabeItems)
      {
        var seenProduct = seenOffer.AvailabeItems.FirstOrDefault(f => f.Id == item.Id);
        if (seenProduct == null)
          return BadRequest("Order changed");
        if (item.Price != seenProduct.Price)
          return BadRequest("Order changed");
      }

      decimal credit = CreateConnection()
        .QueryFirst<decimal>("select creditLeft from Client where id = @id", new { id = offer.ClientId });

      if (credit < seenOffer.TotalCost)
      {
        return BadRequest("Insufficient credit");
      }

      CreateConnection()
          .Execute("update Client set creditLeft = @amount where id = @id", new { amount = seenOffer.TotalCost, id = offer.ClientId });


      XmlSerializer xmlSerializer = new XmlSerializer(typeof(OfferDto));
      using (StringWriter sw = new StringWriter())
      {
        xmlSerializer.Serialize(sw, seenOffer);
        CreateConnection()
          .Execute("insert OrderSnapshot(orderId, orderData)values(@id,@orderData)",
            new { id = orderId, orderData = sw.GetStringBuilder().ToString() });
      }

      CreateConnection().Execute("update [Order] set Status = @status where id = @orderId", new { orderId, status = OrderStatus.Paid });

      CreateConnection().Execute("insert into Shipment(orderId, status)values(@id, 1)", new { id = orderId });

      var order = GetOrderInternal(orderId);

      string email = CreateConnection().QueryFirst<string>("select email from client where id = @id", new { id = order.ClientId });

      _smtpClient.Send("no-reply@photostock.com", email, "Order confirmation", $"your order (number: {order.Number}) has been queued for shipment");

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

      return $"FV {_dateTimeProvider.Today.Year}/{nr}";
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
