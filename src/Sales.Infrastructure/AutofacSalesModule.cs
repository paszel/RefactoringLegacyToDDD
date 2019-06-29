using Autofac;
using Sales.Application;
using Sales.Domain;
using Sales.Domain.Discount;

namespace Sales.Infrastructure
{
  public class AutofacSalesModule : Module
  {
    private readonly string _connectionString;

    public AutofacSalesModule(string connectionString)
    {
      _connectionString = connectionString;
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<NumberGenerator>().AsImplementedInterfaces();
      builder.RegisterType<DiscountCalculator>().AsImplementedInterfaces();
      builder.RegisterType<DateTimeProvider>().AsImplementedInterfaces();
      builder.RegisterType<OrderService>().AsImplementedInterfaces();
      builder.RegisterType<ConfigurationWrapper>().AsImplementedInterfaces();
      builder.RegisterType<ProductRepository>().AsImplementedInterfaces();
      builder.RegisterType<DiscountSpecificationFactory>().AsImplementedInterfaces();
    }
  }
}
