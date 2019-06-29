using Autofac;
using PhotoStock.Controllers;
using Process.OrderConfirmationProcess;

namespace Process
{
  public class AutofacProcessesModule : Module
  {
    private readonly string _connectionString;

    public AutofacProcessesModule(string connectionString)
    {
      _connectionString = connectionString;
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<ProcessOrderConfirmationHandler>().AsImplementedInterfaces();
      builder.RegisterType<SmtpClientWrapper>().AsImplementedInterfaces();
    }
  }
}
