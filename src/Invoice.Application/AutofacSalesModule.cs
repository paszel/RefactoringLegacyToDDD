using Autofac;
using Invoice.Application;

namespace Sales.Infrastructure
{
  public class AutofacInvoiceModule : Module
  {
    private readonly string _connectionString;

    public AutofacInvoiceModule(string connectionString)
    {
      _connectionString = connectionString;
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<CreateInvoiceHandler>().AsImplementedInterfaces();      
    }
  }
}
