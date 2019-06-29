using Autofac;
using Sales.Domain;

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
      builder.RegisterType<ConfigurationWrapper>().AsImplementedInterfaces();
    }
  }
}
