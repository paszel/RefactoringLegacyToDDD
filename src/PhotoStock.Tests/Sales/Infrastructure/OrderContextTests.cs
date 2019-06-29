using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PhotoStock.Infrastructure;
using PhotoStock.Tests.Infrastructure;
using PhotoStock.Tests.Migrations;
using Sales.Domain;
using Sales.Domain.Offer;
using Sales.Infrastructure;

namespace PhotoStock.Tests.Sales.Infrastructure
{
  [TestFixture]
  public class OrderContextTests
  {
    [Test]
    public void MappingTest()
    {
      string connectionString = DbTest.CreateEmptyDatabase();

      DbMigrations.Run(connectionString);

      Order o;
      using (OrderContext ctx = new OrderContext(connectionString))
      {
        IOrderRepository orderRepository = new OrderRepository(ctx, new FakeBus());

        o = new OrderFactory(new TestNumberGenerator(), new TestBus()).Create("12", "client123");
        o.AddProduct(TestSeeding.ProductId);

        orderRepository.Save(o);
      }

      using (OrderContext ctx = new OrderContext(connectionString))
      {
        IOrderRepository orderRepository = new OrderRepository(ctx, new FakeBus());

        Order o2 = orderRepository.Get(o.Id);
        
        Assert.True(o2.SameAs(o));       
      }
    }


    [Test]
    public void ConcurrencyTest()
    {
      string connectionString = DbTest.CreateEmptyDatabase();

      DbMigrations.Run(connectionString);

      Order o;
      OrderContext ctx = new OrderContext(connectionString);
      OrderContext ctx2 = new OrderContext(connectionString);

      IOrderRepository orderRepository = new OrderRepository(ctx, new FakeBus());
      IOrderRepository orderRepository2 = new OrderRepository(ctx2, new FakeBus());

      o = new OrderFactory(new TestNumberGenerator(), new TestBus()).Create("12", "client123");
      o.AddProduct(TestSeeding.ProductId);

      orderRepository.Save(o);

      Order concurrencyOrder1 = orderRepository.Get(o.Id);
      Order concurrencyOrder2 = orderRepository2.Get(o.Id);

      concurrencyOrder1.AddProduct(TestSeeding.Product2Id);

      concurrencyOrder2.AddProduct(TestSeeding.Product3Id);

      orderRepository.Save(concurrencyOrder1);

      Assert.Throws<DbUpdateConcurrencyException>(() => orderRepository2.Save(concurrencyOrder2));      
    }
  }
}
