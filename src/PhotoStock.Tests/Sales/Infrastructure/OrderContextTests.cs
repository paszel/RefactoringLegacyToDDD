using FluentAssertions;
using NUnit.Framework;
using PhotoStock.Infrastructure;
using PhotoStock.Tests.Infrastructure;
using PhotoStock.Tests.Migrations;
using Sales.Domain;
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
        OrderRepository orderRepository = new OrderRepository(ctx, new FakeBus());

        o = new OrderFactory(new TestNumberGenerator(), new TestBus()).Create("12", "client123");
        o.AddProduct(TestSeeding.ProductId);

        orderRepository.Save(o);
      }

      using (OrderContext ctx = new OrderContext(connectionString))
      {
        OrderRepository orderRepository = new OrderRepository(ctx, new FakeBus());

        Order o2 = orderRepository.Get(o.Id);
        
        Assert.True(o2.SameAs(o));       
      }
    }
  }  
}
