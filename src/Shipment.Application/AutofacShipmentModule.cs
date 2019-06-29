using Autofac;

namespace Shipment.Application
{
  public class AutofacShipmentModule : Module
  {
    private readonly string _connectionString;

    public AutofacShipmentModule(string connectionString)
    {
      _connectionString = connectionString;
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<CreateShipmentHandler>().AsImplementedInterfaces();      
    }
  }
}
