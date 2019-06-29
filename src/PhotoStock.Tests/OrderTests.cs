using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnitTestProject1;
using PhotoStock.Infrastructure;
using PhotoStock.Tests.Contract;
using PhotoStock.Tests.Infrastructure;
using RestEase;

namespace PhotoStock.Tests
{
  [TestFixture]
  public class OrderTests 
  {
    private static readonly string _url = "http://localhost:5000";
    private readonly IApi _api = RestClient.For<IApi>(_url);
    private string _clientId;

    [OneTimeSetUp]
    public void Setup()
    {
      //string connectionString = DbTest.CreateEmptyDatabase();

      //DbMigrations.Run(connectionString);

      Bootstrap.Run(new string[0]);
      
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
      // Assert offer / rysunek 1

      await _api.Confirm(orderId, offer);

      Shipment shipment = await _api.GetShipment(orderId);

      Assert.AreEqual(ShipmentStatus.WAITING, shipment.Status);

      await _api.Shipped(orderId);

      Shipment shipment2 = await _api.GetShipment(orderId);

      Assert.AreEqual(ShipmentStatus.SHIPPED, shipment2.Status);
    }
  }
}