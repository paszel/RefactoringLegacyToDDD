using Autofac;
using Sales.Application;
using Sales.Application.AddProduct;
using Sales.Application.CalculateOffer;
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
      builder.RegisterType<CalculateOfferHandler>().AsImplementedInterfaces();
      builder.RegisterType<ConfigurationWrapper>().AsImplementedInterfaces();
      builder.RegisterType<ProductRepository>().AsImplementedInterfaces();
      builder.RegisterType<DiscountSpecificationFactory>().AsImplementedInterfaces();

      builder.RegisterType<CreateOrderHandler>().AsImplementedInterfaces();
      builder.RegisterType<AddProductHandler>().AsImplementedInterfaces();
      builder.RegisterType<CalculateOfferHandler>().AsImplementedInterfaces();
      builder.RegisterType<OrderFactory>().AsImplementedInterfaces();
    }
  }
}
