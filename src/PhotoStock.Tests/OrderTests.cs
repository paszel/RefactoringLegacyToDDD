using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnitTestProject1;
using PhotoStock.Controllers;
using PhotoStock.Infrastructure;
using PhotoStock.Tests.Contract;
using PhotoStock.Tests.Infrastructure;
using PhotoStock.Tests.Migrations;
using RestEase;

namespace PhotoStock.Tests
{
  [TestFixture]
  public class OrderTests 
  {
    private static readonly string _url = "http://localhost:5000";
    private readonly IApi _api = RestClient.For<IApi>(_url);
    private string _clientId;
    private SmtpTestClient _smtpClient;
    private TestDateTimeProvider _dateTimeProvider;

    [OneTimeSetUp]
    public void Setup()
    {
      string connectionString = DbTest.CreateEmptyDatabase();

      DbMigrations.Run(connectionString);

      _smtpClient = new SmtpTestClient();
      _dateTimeProvider = new TestDateTimeProvider();
      _dateTimeProvider.Current = new DateTime(1999,1,1);

      Bootstrap.Run(new string[0], builder =>
      {
        builder.RegisterInstance(_dateTimeProvider);
        builder.RegisterInstance(_smtpClient).AsImplementedInterfaces();
      }, connectionString);
      
      _clientId = "aaa111";
    }

    [Test]
    public async Task WholeProcess()
    {
      // Setup
      string orderId = await _api.CreateOrder(_clientId);
      var products = await _api.GetProducts(null, 10);
      Product product = products.First(f => f.Name == "Rysunek1");

      await _api.AddProductToOrder(orderId, product.Id);

      Offer offer = await _api.CalculateOffer(orderId);
      await _api.Confirm(orderId, offer);

      Shipment shipment = await _api.GetShipment(orderId);

      Assert.AreEqual(ShipmentStatus.WAITING, shipment.Status);
      Assert.AreEqual(1, _smtpClient.SentEmails.Count);
      Assert.AreEqual(TestSeeding.ClientEmail, _smtpClient.SentEmails[0].Recipients);

      await _api.Shipped(orderId);

      Shipment shipment2 = await _api.GetShipment(orderId);

      Assert.AreEqual(ShipmentStatus.SHIPPED, shipment2.Status);
      Assert.AreEqual(2, _smtpClient.SentEmails.Count);
      Assert.AreEqual(TestSeeding.ClientEmail, _smtpClient.SentEmails[1].Recipients);
    }
  }

  public class TestDateTimeProvider : IDateTimeProvider
  {
    public DateTime Current { get; set; }

    public DateTime Now()
    {
      return Current;
    }

    public DateTime Today
    {
      get { return Now().Date; }
    }
  }
}