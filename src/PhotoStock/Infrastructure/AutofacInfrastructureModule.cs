using System;
using Autofac;
using PhotoStock.Controllers;
using Sales.Domain;
using Sales.Domain.Discount;

namespace PhotoStock.Infrastructure
{
  public class AutofacInfrastructureModule : Module
  {
    private readonly string _connectionString;

    public AutofacInfrastructureModule(string connectionString)
    {
      _connectionString = connectionString;
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<Bus>().AsImplementedInterfaces();
      builder.RegisterType<DateTimeProvider>().AsImplementedInterfaces();
      
      //builder.RegisterType<InvoiceContext>().WithParameter("connectionString", _connectionString);
      //builder.RegisterType<ProductContext>().WithParameter("connectionString", _connectionString);
      //builder.RegisterType<ShipmentContext>().WithParameter("connectionString", _connectionString);
      //builder.RegisterType<OrderContext>().WithParameter("connectionString", _connectionString);

      builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
        .Where(t => t.Namespace != null && t.Namespace.StartsWith("PhotoStock.Components") 
                    //&& !t.IsAssignableTo<DbContext>() 
                    //&& !t.IsAssignableTo<Exception>() 
                    //&& !t.IsAssignableTo<ValueObject>() 
                    //&& !t.IsAssignableTo<AggregateRoot>()
          ).AsImplementedInterfaces();

      builder.RegisterType<NumberGenerator>().AsImplementedInterfaces();
      builder.RegisterType<DiscountCalculator>().AsImplementedInterfaces();
      
      //builder.Register(f => f.Resolve<IDiscountFactory>().Create()).AsImplementedInterfaces();
    }
  }
}
