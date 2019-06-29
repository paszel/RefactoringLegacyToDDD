using Sales.Domain;

namespace Sales.Infrastructure
{
  public class ConfigurationWrapper : IConfiguration
  {
    private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

    public ConfigurationWrapper(Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public string this[string name] => _configuration[name];
  }
}