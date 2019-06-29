using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using NUnit.Framework;
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

    [OneTimeSetUp]
    public void Setup()
    {
      string connectionString = DbTest.CreateEmptyDatabase();

      DbMigrations.Run(connectionString);

      _smtpClient = new SmtpTestClient();

      Bootstrap.Run(new string[0], builder =>
      {
        builder.RegisterInstance(_smtpClient).AsImplementedInterfaces();        
      }, connectionString);
      
      _clientId = "aaa111";
    }

    [SetUp]
    public void SetUp()
    {
      _smtpClient.SentEmails.Clear();
    }

    [Test]
    public async Task AddProductToOrder_should_return_not_found_when_order_confirmed()
    {
      // Setup
      string orderId = await _api.CreateOrder(_clientId);
      var products = await _api.GetProducts(null, 10);
      Product product = products.First(f => f.Name == "Rysunek1");

      await _api.AddProductToOrder(orderId, product.Id);
      Offer offer = await _api.CalculateOffer(orderId);
      await _api.Confirm(orderId, offer);

      ApiException exception = Assert.ThrowsAsync<ApiException>(
        async () => await _api.AddProductToOrder(orderId, product.Id));

      Assert.AreEqual(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Test]
    public async Task AddProductToOrder_should_return_bad_request_when_product_added()
    {
      string orderId = await _api.CreateOrder(_clientId);
      var products = await _api.GetProducts(null, 10);
      Product product = products.First(f => f.Name == "Rysunek1");

      await _api.AddProductToOrder(orderId, product.Id);

      ApiException exception = Assert.ThrowsAsync<ApiException>(
        async () => await _api.AddProductToOrder(orderId, product.Id));

      Assert.AreEqual(HttpStatusCode.BadRequest, exception.StatusCode);
    }

    [Test]
    public async Task WholeProcess()
    {
      string orderId = await _api.CreateOrder(_clientId);
      var products = await _api.GetProducts(null, 10); 
      Product product = products.First(f => f.Name == "Rysunek1");

      await _api.AddProductToOrder(orderId, product.Id);

      Offer offer = await _api.CalculateOffer(orderId);
      await _api.Confirm(orderId, offer);

      Contract.Shipment shipment = await _api.GetShipment(orderId);

      Assert.AreEqual(ShipmentStatus.WAITING, shipment.Status);
      Assert.AreEqual(1, _smtpClient.SentEmails.Count);
      Assert.AreEqual(TestSeeding.ClientEmail, _smtpClient.SentEmails[0].Recipients);

      await _api.Shipped(orderId);

      Contract.Shipment shipment2 = await _api.GetShipment(orderId);

      Assert.AreEqual(ShipmentStatus.SHIPPED, shipment2.Status);
      Assert.AreEqual(2, _smtpClient.SentEmails.Count);
      Assert.AreEqual(TestSeeding.ClientEmail, _smtpClient.SentEmails[1].Recipients);
    }
  }
}