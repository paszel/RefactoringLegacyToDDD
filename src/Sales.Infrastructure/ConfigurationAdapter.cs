using Sales.Domain;

namespace Sales.Infrastructure
{
  public class ConfigurationAdapter : IConfiguration
  {
    private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

    public ConfigurationAdapter(Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public string this[string name] => _configuration[name];
  }
}